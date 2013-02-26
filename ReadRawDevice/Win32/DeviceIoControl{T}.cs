
namespace ReadRawDevice.Win32
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Microsoft.Win32.SafeHandles;
    using ReadRawDevice.Core;

    /// <summary>
    /// Generic wrapper for DeviceIOControl function
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DeviceIoControl<T>
    {
        private static object lockMe = new object();

        /// <summary>
        /// Get data by calling DeviceIoControl function without input parameters
        /// </summary>
        /// <param name="device">Device to run the command on</param>
        /// <param name="controlCode">Control code to send to windows kernel <see cref="IoControlCode"/></param>
        /// <returns>Data/structure that received after function execution</returns>
        internal T GetDataForDevice(DeviceHandle device, IoControlCode controlCode)
        {
            bool functionResult = false;
            IntPtr ptrInputData = IntPtr.Zero;
            var structureSize = Marshal.SizeOf(typeof(T));
            int lpBytesReturned = 0;
            NativeOverlapped nativeOverlapped = new NativeOverlapped();

            try
            {
                // First try
                ptrInputData = Marshal.AllocHGlobal(structureSize);

                while (functionResult == false)
                {
                    lock (lockMe)
                    {
                        IntPtr handle = device.OpenDeviceHandle();

                        functionResult = UnsafeNativeMethods.DeviceIoControl(handle,
                                                                                controlCode,
                                                                                IntPtr.Zero,
                                                                                0,
                                                                                ptrInputData,
                                                                                structureSize,
                                                                                ref lpBytesReturned,
                                                                                ref nativeOverlapped);

                        device.CloseDeviceHandle();

                    }

                    if (functionResult == false)
                    {
                        if (Marshal.GetLastWin32Error() == UnsafeNativeMethods.ERROR_INSUFFICIENT_BUFFER)
                        {
                            Marshal.FreeHGlobal(ptrInputData);
                            checked { structureSize = structureSize * 2; }
                            ptrInputData = Marshal.AllocHGlobal(structureSize);
                        }
                        else
                        {
                            throw new Win32Exception("Could not acquire information from windows kernel.", new Win32Exception(Marshal.GetLastWin32Error()));
                        }
                    }
                }

                T data = (T)Marshal.PtrToStructure(ptrInputData, typeof(T));
                return data;
            }
            catch (Exception exp_gen)
            {
                throw new Win32Exception("Exception occurred while trying to work with Windows kernel", exp_gen);
            }
            finally
            {
                Marshal.FreeHGlobal(ptrInputData);
            }
        }

        /// <summary>
        /// Gets the drive layout.
        /// This is special case DeviceIoControl call. The <see cref="DRIVE_LAYOUT_INFORMATION_EX"/> contains dynamic array
        /// </summary>
        /// <param name="device">The device to query</param>
        /// <remarks>Code was taken from: http://brianhehir.blogspot.co.il/2011/12/kernel32dll-deviceiocontrol-in-c.html </remarks>
        /// <returns><c>Tuple</c> of <see cref="DRIVE_LAYOUT_INFORMATION_EX"/> and array of <see cref="PARTITION_INFORMATION_EX"/></returns>
        internal Tuple<DRIVE_LAYOUT_INFORMATION_EX, PARTITION_INFORMATION_EX[]> GetDriveLayout(DeviceHandle device)
        {
            bool functionResult = false;
            IntPtr ptrInputData = IntPtr.Zero;

            int numberOfExpectedPartitions = 1;

            // 48 = the number of bytes in DRIVE_LAYOUT_INFORMATION_EX up to
            // the first PARTITION_INFORMATION_EX in the array.
            // And each PARTITION_INFORMATION_EX is 144 bytes.
            var structureSize = 48 + (numberOfExpectedPartitions * 144);
            int lpBytesReturned = 0;
            NativeOverlapped nativeOverlapped = new NativeOverlapped();

            try
            {
                // First try
                ptrInputData = Marshal.AllocHGlobal(structureSize);

                while (functionResult == false)
                {
                    lock (lockMe)
                    {
                        IntPtr handle = device.OpenDeviceHandle();

                        functionResult = UnsafeNativeMethods.DeviceIoControl(handle,
                                                                                IoControlCode.IOCTL_DISK_GET_DRIVE_LAYOUT_EX,
                                                                                IntPtr.Zero,
                                                                                0,
                                                                                ptrInputData,
                                                                                structureSize,
                                                                                ref lpBytesReturned,
                                                                                ref nativeOverlapped);

                        device.CloseDeviceHandle();
                    }

                    if (functionResult == false)
                    {
                        if (Marshal.GetLastWin32Error() == UnsafeNativeMethods.ERROR_INSUFFICIENT_BUFFER)
                        {
                            Marshal.FreeHGlobal(ptrInputData);
                            checked 
                            {
                                numberOfExpectedPartitions++;
                                structureSize = 48 + (numberOfExpectedPartitions * 144);
                            }
                            ptrInputData = Marshal.AllocHGlobal(structureSize);
                        }
                        else
                        {
                            throw new Win32Exception("Could not acquire information from windows kernel.", new Win32Exception(Marshal.GetLastWin32Error()));
                        }
                    }
                }

                DRIVE_LAYOUT_INFORMATION_EX driveLayoutInfo = default(DRIVE_LAYOUT_INFORMATION_EX);
                driveLayoutInfo = (DRIVE_LAYOUT_INFORMATION_EX)Marshal.PtrToStructure(ptrInputData, typeof(DRIVE_LAYOUT_INFORMATION_EX));

                PARTITION_INFORMATION_EX[] partititons = new PARTITION_INFORMATION_EX[driveLayoutInfo.PartitionCount];
                for (int iCounter = 0; iCounter < partititons.Length; iCounter++)
                {
                    IntPtr partitionOffset = new IntPtr(ptrInputData.ToInt64() + 48 + (iCounter * 144));
                    partititons[iCounter] = (PARTITION_INFORMATION_EX)Marshal.PtrToStructure(partitionOffset, typeof(PARTITION_INFORMATION_EX));
                }

                return new Tuple<DRIVE_LAYOUT_INFORMATION_EX, PARTITION_INFORMATION_EX[]>(driveLayoutInfo, partititons);
            }
            catch (Exception exp_gen)
            {
                throw new Win32Exception("Exception occurred while trying to work with Windows kernel", exp_gen);
            }
            finally
            {
                Marshal.FreeHGlobal(ptrInputData);
            }
        }

        /// <summary>
        /// Function will check if provide <see cref="SystemVolume"/> is accessible
        /// It has a meaning for removable devices such as CDRom
        /// </summary>
        /// <param name="device">The device to query</param>
        /// <returns><c>true</c> if device is accessible and could be queried, otherwise <c>false</c></returns>
        internal static bool IsAccessible(DeviceHandle device)
        {
            bool functionResult = false;
            int lpBytesReturned = 0;
            NativeOverlapped nativeOverlapped = new NativeOverlapped();

            try
            {
                lock (lockMe)
                {
                    IntPtr handle = device.OpenDeviceHandle();


                    functionResult = UnsafeNativeMethods.DeviceIoControl(handle,
                                                                        IoControlCode.IOCTL_STORAGE_CHECK_VERIFY,
                                                                        IntPtr.Zero,
                                                                        0,
                                                                        IntPtr.Zero,
                                                                        0,
                                                                        ref lpBytesReturned,
                                                                        ref nativeOverlapped);

                    device.CloseDeviceHandle();

                    return functionResult;
                }
            }
            catch (Exception exp_gen)
            {
                throw new Win32Exception("Exception occurred while trying to work with Windows kernel", exp_gen);
            }
        }

        /// <summary>
        /// Gets the disk geometry extended information
        /// </summary>
        /// <param name="device">The device to query</param>
        /// <returns>Structure <see cref="DISK_GEOMETRY_EX"/></returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa365171(v=vs.85).aspx </remarks>
        internal DISK_GEOMETRY_EX GetDiskGeometryEx(DeviceHandle device)
        {
            bool functionResult = false;
            IntPtr ptrInputData = IntPtr.Zero;
            var structureSize = 256;
            int lpBytesReturned = 0;
            NativeOverlapped nativeOverlapped = new NativeOverlapped();

            try
            {
                // First try
                ptrInputData = Marshal.AllocHGlobal(structureSize);

                while (functionResult == false)
                {
                    lock (lockMe)
                    {
                        IntPtr handle = device.OpenDeviceHandle();

                        functionResult = UnsafeNativeMethods.DeviceIoControl(handle,
                                                                                IoControlCode.IOCTL_DISK_GET_DRIVE_GEOMETRY_EX,
                                                                                IntPtr.Zero,
                                                                                0,
                                                                                ptrInputData,
                                                                                structureSize,
                                                                                ref lpBytesReturned,
                                                                                ref nativeOverlapped);

                        device.CloseDeviceHandle();

                    }

                    if (functionResult == false)
                    {
                        if (Marshal.GetLastWin32Error() == UnsafeNativeMethods.ERROR_INSUFFICIENT_BUFFER)
                        {
                            Marshal.FreeHGlobal(ptrInputData);
                            checked { structureSize = structureSize * 2; }
                            ptrInputData = Marshal.AllocHGlobal(structureSize);
                        }
                        else
                        {
                            throw new Win32Exception("Could not acquire information from windows kernel.", new Win32Exception(Marshal.GetLastWin32Error()));
                        }
                    }
                }

                
                DISK_GEOMETRY_EX diskGeometryEx = new DISK_GEOMETRY_EX();
                diskGeometryEx.Geometry = (DISK_GEOMETRY) Marshal.PtrToStructure(ptrInputData, typeof(DISK_GEOMETRY));

                // Just after the offset of 'DISK_GEOMETRY' there's a DiskSize value
                diskGeometryEx.DiskSize = Marshal.ReadInt64(ptrInputData, Marshal.SizeOf(typeof(DISK_GEOMETRY)));

                // PartitionInfo offset = sizeof(diskGeometryEx.Geometry) + sizeof(diskGeometryEx.DiskSize)
                IntPtr partitionInfo = ptrInputData + Marshal.SizeOf(typeof(DISK_GEOMETRY)) + sizeof(long);

                diskGeometryEx.PartitionInformation = (DISK_PARTITION_INFO) Marshal.PtrToStructure(partitionInfo, typeof(DISK_PARTITION_INFO));

                // DISK_DETECTION_INFO offset = DISK_PARTITION_INFO + length of DISK_PARTITION_INFO.SizeOfPartitionInfo
                partitionInfo += diskGeometryEx.PartitionInformation.SizeOfPartitionInfo;

                diskGeometryEx.DiskDetectionInfo = (DISK_DETECTION_INFO)Marshal.PtrToStructure(partitionInfo, typeof(DISK_DETECTION_INFO));

                return diskGeometryEx;
            }
            catch (Exception exp_gen)
            {
                throw new Win32Exception("Exception occurred while trying to work with Windows kernel", exp_gen);
            }
            finally
            {
                Marshal.FreeHGlobal(ptrInputData);
            }
        }
    }
}
