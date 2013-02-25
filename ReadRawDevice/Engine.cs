
namespace ReadRawDevice
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Win32.SafeHandles;
    using ReadRawDevice.Core;
    using ReadRawDevice.Win32;

    /// <summary>
    /// Main 'engine' object for a app
    /// </summary>
    public class Engine
    {
        private VolumeBuilder volumeBuilder = null;
        private DeviceBuilder deviceBuilder = null;
        private Reader reader = null;
        private CancellationTokenSource tokenSource = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            volumeBuilder = new VolumeBuilder();
            deviceBuilder = new DeviceBuilder();
            reader = new Reader();
        }

        /// <summary>
        /// Prepares the token source for re-entrance
        /// </summary>
        private void PrepareTokenSource()
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
                tokenSource = null;
            }

            tokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Builds the volumes asynchronously
        /// </summary>
        /// <returns></returns>
        public Task<VolumesCollection> BuildVolumesAsync()
        {
            PrepareTokenSource();

            return Task.Factory.StartNew<VolumesCollection>(() =>
            {
                return volumeBuilder.Build(tokenSource.Token);
            }, tokenSource.Token);
        }

        /// <summary>
        /// Builds the devices asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task<DeviceCollection> BuildDevicesAsync()
        {
            PrepareTokenSource();

            return Task.Factory.StartNew<DeviceCollection>(() =>
            {
                return deviceBuilder.Build(tokenSource.Token);
            }
            , tokenSource.Token);
        }

        /// <summary>
        /// Extract the raw data from the volume asynchronously
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> that will be queried</param>
        /// <param name="outputFile">File path and file name where to store raw data</param>
        /// <param name="progress">Progress callback</param>
        /// <returns>Task</returns>
        public async Task ExtractVolumeAsync(SystemVolume device, string outputFile, IProgress<int> progress)
        {
            throw new NotImplementedException();
            //PrepareTokenSource();

            //await Task.Run(() => { });
        }

        //public Task<long> ExtractDiskAsync(SystemDevice device, string outputFile, IProgress<int> progress)
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions] 
        public Task<long> ExtractDiskAsync(SystemDevice device, string outputFile, IProgress<int> progress)
        {
            PrepareTokenSource();

            //
            // If bufferSize will be tooo small (like: 512 bytes) the iteration of ReadFile will fail with E_FAIL or some SEH exception :(
            int sectorsReadAtOnce = Convert.ToInt32(device.SectorsCount / 100) + 1; // in 'sectors' not bytes !
            int bufferSize = sectorsReadAtOnce * device.BytesPerSector;
            //

            // Allign to 512 exactly
            //while (bufferSize % device.BytesPerSector != 0)
            //    bufferSize--;

            byte[] buffer = new byte[bufferSize];
            long bytesRead = 0;
            uint lpNumberOfBytesRead = 0;
            bool functionResult = false;

            SystemDevice device2 = new SystemDevice("\\\\.\\PhysicalDrive" + device.DeviceNumber);
            GCHandle gcHandle = new GCHandle();

            return Task.Factory.StartNew<long>(() => {
                try
                {
                    IntPtr deviceHandle = device2.OpenDeviceHandle();
                    gcHandle = GCHandle.Alloc(deviceHandle);        // So it won't be collected by GC while I'm doing PInvoke

                    BinaryWriter writer = GetOutputStream(outputFile);

                    while(true)
                    {
                        System.Diagnostics.Debug.WriteLine("bytesRead is: " + bytesRead.ToString() + " /" + device.DiskSize.Value.ToString() + " buffer size: " + buffer.Length.ToString() + " left: " + (device.DiskSize.Value - bytesRead).ToString());

                        functionResult = UnsafeNativeMethods.ReadFile(deviceHandle, buffer, Convert.ToUInt32(buffer.Length), ref lpNumberOfBytesRead, IntPtr.Zero);

                        if (functionResult)
                        {
                            bytesRead += lpNumberOfBytesRead;

                            writer.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            int win32err = Marshal.GetLastWin32Error();
                            System.Diagnostics.Debug.WriteLine("Error: bytesRead is: " + bytesRead.ToString() + " /" + device.DiskSize.Value.ToString() + " buffer size: " + buffer.Length.ToString() + " left: " + (device.DiskSize.Value - bytesRead).ToString());

                            if (win32err == UnsafeNativeMethods.ERROR_SECTOR_NOT_FOUND)
                            {
                                // This is a device black-hole
                                break;
                            }
                            else
                            {
                                throw new System.ComponentModel.Win32Exception(win32err);
                            }
                        }

                        if (progress != null)
                        {
                            progress.Report((int)Math.Round((double)((bytesRead * 100) / device.DiskSize.Value)));
                        }

                        // Must not (!) increase position - everything will be read to NULL
                        //deviceStream.Position = iCounter;

                        if (bytesRead + bufferSize > device.DiskSize.Value)
                        {
                            if (device.DiskSize.Value == bytesRead)
                            {
                                // all done
                                break;
                            }
                            else 
                            {
                                // Collect leftovers
                                System.Diagnostics.Trace.WriteLine("Leftovers: bytesRead = " + bytesRead.ToString() + " buffferSize = " + bufferSize.ToString() + " diskSize = " + device.DiskSize.Value.ToString());
                                buffer = new byte[(bytesRead + bufferSize) - device.DiskSize.Value];
                                System.Diagnostics.Trace.WriteLine("Buffer re-sized to: " + buffer.Length.ToString() + " bytes");
                            }
                        }

                        GC.KeepAlive(deviceHandle);
                    }

                    writer.Flush();

                    gcHandle.Free();

                    device.CloseDeviceHandle();

                    return bytesRead;
                }
                catch (SEHException seh)
                {
                    gcHandle.Free();
                    System.Diagnostics.Trace.WriteLine("[]--- SEHException in ExtractDiskAsync(): " + seh.ToString());
                    return 0;
                }
                catch (Exception exp_gen)
                {
                    gcHandle.Free();

                    int win32err = Marshal.GetLastWin32Error();
                    var zz = new System.ComponentModel.Win32Exception(win32err);
                    System.Diagnostics.Trace.WriteLine("[]--- Exception in ExtractDiskAsync(): " + exp_gen.ToString());
                    System.Diagnostics.Trace.WriteLine("[]--- Exception in ExtractDiskAsync() (native) : " + zz.ToString());
                    return 0;
                }
            }
            , tokenSource.Token);
        }

        /// <summary>
        /// Gets the <c>BinaryWriter</c> to file for output
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>BinaryWriter</c> for output file</returns>
        protected BinaryWriter GetOutputStream(string fileName)
        {
            FileStream fileStream = null;
            BinaryWriter writer = null;


            try
            {
                fileStream = new FileStream(fileName, FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read);
                writer = new BinaryWriter(fileStream, System.Text.Encoding.Default);
                return writer;
            }
            catch (Exception exp_gen)
            {
                throw exp_gen;
            }
        }
    }
}
