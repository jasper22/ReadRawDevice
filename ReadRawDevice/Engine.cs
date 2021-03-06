﻿
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            volumeBuilder = new VolumeBuilder();
            deviceBuilder = new DeviceBuilder();
        }

        /// <summary>
        /// Builds the volumes asynchronously
        /// </summary>
        /// <returns></returns>
        public Task<VolumesCollection> BuildVolumesAsync(CancellationToken token)
        {
            if (deviceBuilder.IfUserAdmin() == false)
            {
                throw new System.Security.SecurityException("User must be administrator to access the hardware. Please re-login");
            }

            return Task.Factory.StartNew<VolumesCollection>(() =>
            {
                return volumeBuilder.Build(token);
            }, token);
        }

        /// <summary>
        /// Builds the devices asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task<DeviceCollection> BuildDevicesAsync(CancellationToken token)
        {
            if (deviceBuilder.IfUserAdmin() == false)
            {
                throw new System.Security.SecurityException("User must be administrator to access the hardware. Please re-login");
            }

            return Task.Factory.StartNew<DeviceCollection>(() =>
            {
                return deviceBuilder.Build(token);
            }
            , token);
        }

        ///// <summary>
        ///// Extract the raw data from the volume asynchronously
        ///// </summary>
        ///// <param name="device"><see cref="SystemVolume"/> that will be queried</param>
        ///// <param name="outputFile">File path and file name where to store raw data</param>
        ///// <param name="progress">Progress callback</param>
        ///// <returns>Task</returns>
        //public async Task ExtractVolumeAsync(SystemVolume device, string outputFile, IProgress<int> progress)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Extracts the disk asynchronously.
        /// </summary>
        /// <param name="device"><see cref="SystemDevice"/> that will be queried</param>
        /// <param name="outputFile">The output file.</param>
        /// <param name="progress">The progress callback</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Task that return as result the total number of read bytes</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "By design")]
        public Task<long> ExtractDiskAsync(SystemDevice device, string outputFile, IProgress<double> progress, CancellationToken token)
        {
            if (deviceBuilder.IfUserAdmin() == false)
            {
                throw new System.Security.SecurityException("User must be administrator to access the hardware. Please re-login");
            }

            //
            // If bufferSize will be tooo small (like: 512 bytes) the iteration of ReadFile will fail with E_FAIL or some SEH exception :(
            int sectorsReadAtOnce = Convert.ToInt32(device.SectorsCount / 100) + 1; // in 'sectors' not bytes !
            int bufferSize = sectorsReadAtOnce * device.BytesPerSector;
            //

            // Align to 512 exactly
            //while (bufferSize % device.BytesPerSector != 0)
            //    bufferSize--;

            byte[] buffer = new byte[bufferSize];
            long bytesRead = 0;
            uint lpNumberOfBytesRead = 0;
            bool functionResult = false;
            int win32err = 0;
            NativeOverlapped nativeOverlapped = new NativeOverlapped();

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
                        functionResult = UnsafeNativeMethods.ReadFile(deviceHandle, buffer, Convert.ToUInt32(buffer.Length), ref lpNumberOfBytesRead, ref nativeOverlapped);
                        win32err = Marshal.GetLastWin32Error();

                        if (functionResult)
                        {
                            bytesRead += lpNumberOfBytesRead;

                            writer.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            if (win32err == UnsafeNativeMethods.ERROR_SECTOR_NOT_FOUND)
                            {
                                // This is a device black-hole
                                // try to squeeze as much as I can
                                if (bufferSize == device.BytesPerSector)
                                {
                                    // That's the last one
                                    break;
                                }
                                else
                                {
                                    bufferSize = device.BytesPerSector;
                                    buffer = new byte[bufferSize];
                                }
                            }
                            else
                            {
                                throw new System.ComponentModel.Win32Exception(win32err);
                            }
                        }

                        if (progress != null)
                        {
                            progress.Report(Math.Round((double)((bytesRead * 100) / device.DiskSize.Value)));
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
                                buffer = new byte[(bytesRead + bufferSize) - device.DiskSize.Value];
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

                    if (win32err == 0)
                    {
                        win32err = Marshal.GetLastWin32Error();
                    }

                    var zz = new System.ComponentModel.Win32Exception(win32err);
                    System.Diagnostics.Trace.WriteLine("[]--- Exception in ExtractDiskAsync(): " + exp_gen.ToString());
                    System.Diagnostics.Trace.WriteLine("[]--- Exception in ExtractDiskAsync() (native) : " + zz.ToString());
                    return 0;
                }
            }
            , token);
        }

        /// <summary>
        /// Gets the <c>BinaryWriter</c> to file for output
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>BinaryWriter</c> for output file</returns>
        protected static BinaryWriter GetOutputStream(string fileName)
        {
            FileStream fileStream = null;
            BinaryWriter writer = null;


            try
            {
                fileStream = new FileStream(fileName, FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read);
                writer = new BinaryWriter(fileStream, System.Text.Encoding.Default);
                return writer;
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
