
namespace ReadRawDevice.Win32
{
    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using ReadRawDevice.Core;

    /// <summary>
    /// Represent 'native'/low-level function to acccess physical devices, hardware, etc..
    /// </summary>
    public abstract class Native
    {
        /// <summary>
        /// The prefix that should be stripped from 'volume name' before it enter into QueryDosDevice function
        /// </summary>
        private const string STRIP_PREFIX = @"\\?\";

        /// <summary>
        /// Initializes a new instance of the <see cref="Native"/> class.
        /// </summary>
        internal Native()
        {
        }

        /// <summary>
        /// Ifs the user admin.
        /// </summary>
        /// <returns></returns>
        internal bool IfUserAdmin()
        {
            // Check if user is admin
            if (Priviligies.IfUserAdmin() == false)
            {
                //throw new System.Security.SecurityException("User must be administrator to access the hardware. Please re-login");
                return false;
            }

            if (Priviligies.GetVolumePathNamesForVolumeName_Available == false)
            {
                //throw new System.Security.SecurityException("Some internal functions is only available on Windows XP/2003 and above");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the volumes names.
        /// </summary>
        /// <returns>List of volumes</returns>
        /// <exception cref="System.ApplicationException">Internal program error. Could not acquire handle to device</exception>
        internal virtual IEnumerable<string> GetVolumesNames()
        {
            const int volumeNameLength = UnsafeNativeMethods.MAX_PATH;
            StringBuilder cVolumeName = new StringBuilder(UnsafeNativeMethods.MAX_PATH);
            List<string> volumeNames = new List<string>();
            SafeFileHandle volume_handle = null;

            try
            {
                volume_handle = UnsafeNativeMethods.FindFirstVolume(cVolumeName, volumeNameLength);

                if (volume_handle.IsInvalid)
                {
                    Win32Exception exp = new Win32Exception(Marshal.GetExceptionCode());
                    throw new Win32Exception("Internal program error. Could not acquire handle to device", exp);
                }

                do
                {
                    volumeNames.Add(cVolumeName.ToString());
                } while (UnsafeNativeMethods.FindNextVolume(volume_handle, cVolumeName, volumeNameLength));

                return volumeNames;
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                if (volume_handle != null)
                {
                    UnsafeNativeMethods.FindVolumeClose(volume_handle);

                    //if (volume_handle.IsClosed == false)
                    //{
                    //    volume_handle.Close();
                    //}
                }
            }
        }

        /// <summary>
        /// Function wil lreturn a list of devices name for provided volume name
        /// </summary>
        /// <param name="volumeNames">List of volume names (ex: \\?\Volume{b6b02346-65dd-11e1-aa19-806e6f6e6963}\)</param>
        /// <returns>Dictionary that contain a list of volume name (Key) and corresponding device name (Value)</returns>
        internal virtual IDictionary<string, string> GetDeviceNames(IEnumerable<string> volumeNames)
        {
            Contract.Requires(volumeNames != null);
            Contract.Requires(volumeNames.Count() > 1);

            StringBuilder lpTargetPath = new StringBuilder(UnsafeNativeMethods.MAX_PATH);
            IDictionary<string, string> preparedList = PrepareListForQueryDosDevice(volumeNames);

            try
            {
                var devices = from volName in preparedList
                              let tmpI = UnsafeNativeMethods.QueryDosDevice(volName.Value, lpTargetPath, UnsafeNativeMethods.MAX_PATH)
                              select new { VolumeName = volName.Key, DeviceName = lpTargetPath.ToString() };

                return devices.ToDictionary(x => x.VolumeName, x => x.DeviceName);
            }
            catch (Exception exp_gen)
            {
                throw new Win32Exception("Error occurred while enumerating devices list", exp_gen);
            }
        }

        /// <summary>
        /// Prepares the list for query dos device. Strip un-needed characters, etc...
        /// </summary>
        /// <param name="volumeNames">The volume names.</param>
        /// <returns>Dictionary prepared to enter to <c>QueryDosDevice</c>  function  (Original name = Key, Stripped/Read name = Value)</returns>
        private static IDictionary<string,string> PrepareListForQueryDosDevice(IEnumerable<string> volumeNames)
        {
            return (from volName in volumeNames.AsParallel()
                   select new {OriginalName = volName, Stripped = volName.Replace(STRIP_PREFIX, string.Empty).TrimEnd(new[] {'\\'})  })
                   .ToDictionary(x => x.OriginalName, x => x.Stripped);
        }

        /// <summary>
        /// Gets the path for volume names 
        /// </summary>
        /// <param name="devicesNames">The devices names <see cref="GetDeviceNames"/></param>
        /// <returns>Returns Dictionary that contains volume name (Key) and volume path (Value)</returns>
        internal virtual IDictionary<string, string> GetVolumesNamesPaths(IEnumerable<string> devicesNames)
        {
            //string lpszVolumePathNames = string.Empty;
            uint cchBuferLength = UnsafeNativeMethods.MAX_PATH * 2;
            StringBuilder lpszVolumePathNames = new StringBuilder(Convert.ToInt32(cchBuferLength));
            uint lpcchReturnLength = 0;

            var names = from devName in devicesNames
                        let tmpI = UnsafeNativeMethods.GetVolumePathNamesForVolumeName(devName, lpszVolumePathNames, cchBuferLength, ref lpcchReturnLength)
                        select new { DeviceName = devName, DevicePath = lpszVolumePathNames.ToString() };

            return names.ToDictionary(x => x.DeviceName, x => x.DevicePath);
        }

        /// <summary>
        /// Function will all physical disk that 'volume' is extended/spanned on it
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa365194(v=vs.85).aspx </remarks>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <returns><see cref="VOLUME_DISK_EXTENTS"/> structure that volume extends on</returns>
        internal virtual Nullable<VOLUME_DISK_EXTENTS> GetDiskExtendsForVolume(SystemVolume device)
        {
            DeviceIoControl<VOLUME_DISK_EXTENTS> deviceControl = new DeviceIoControl<VOLUME_DISK_EXTENTS>();
            return deviceControl.GetDataForDevice(device, IoControlCode.IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS);
        }

        /// <summary>
        /// Function will calculate/read 'Layout information' for provided <see cref="SystemVolume"/>
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <returns>Tuple of <see cref="DRIVE_LAYOUT_INFORMATION_EX"/> and array of <see cref="PARTITION_INFORMATION_EX"/></returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa365174(v=vs.85).aspx </remarks>
        internal virtual Tuple<DRIVE_LAYOUT_INFORMATION_EX, PARTITION_INFORMATION_EX[]> GetDriveLayout(SystemVolume device)
        {
            DeviceIoControl<DRIVE_LAYOUT_INFORMATION_EX> deviceControl = new DeviceIoControl<DRIVE_LAYOUT_INFORMATION_EX>();
            return deviceControl.GetDriveLayout(device);
        }

        /// <summary>
        /// Function will calculate/read 'Disk geometry' for provided <see cref="SystemVolume"/>
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <returns>Disk geometry information</returns>
        internal virtual DISK_GEOMETRY_EX GetDriveGeometry(DeviceHandle device)
        {
            DeviceIoControl<DISK_GEOMETRY_EX> deviceControl = new DeviceIoControl<DISK_GEOMETRY_EX>();
            return deviceControl.GetDiskGeometryEx(device);
        }

        /// <summary>
        /// Sets the volume information.
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// Could not acquire volume information
        /// </exception>
        internal virtual void SetVolumeInformation(SystemVolume device)
        {
            StringBuilder volumeName = new StringBuilder(UnsafeNativeMethods.MAX_PATH + 1);
            uint lpVolumeSerialNumber = 0;
            uint lpMaximumComponentLength = 0;
            //FileSystemFlags lpFileSystemFlags = 0;
            uint lpFileSystemFlags = 0;
            StringBuilder lpFileSystemNameBuffer = new StringBuilder(UnsafeNativeMethods.MAX_PATH + 1);
            bool functionResult = false;

            if (string.IsNullOrEmpty(device.DevicePath))
            {
                return;
            }

            if (DeviceIoControl<int>.IsAccessible(device) == false)
            {
                // Device is not accessible
                return;
            }

            functionResult = UnsafeNativeMethods.GetVolumeInformation(device.DevicePath,
                                                                        volumeName,
                                                                        Convert.ToUInt32(volumeName.Capacity),
                                                                        out lpVolumeSerialNumber,
                                                                        out lpMaximumComponentLength,
                                                                        out lpFileSystemFlags,
                                                                        lpFileSystemNameBuffer,
                                                                        Convert.ToUInt32(lpFileSystemNameBuffer.Capacity));

            if (functionResult == false)
            {
                throw new Win32Exception("Could not aquire volume information", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            device.SerialNumber = lpVolumeSerialNumber;
            device.FileSystemName = lpFileSystemNameBuffer.ToString();

        }

        /// <summary>
        /// Retrieves the device type, device number, and, for a partitionable device, the partition number of a device.
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <returns><see cref="STORAGE_DEVICE_NUMBER"/> structure</returns>
        internal virtual STORAGE_DEVICE_NUMBER GetDeviceNumber(DeviceHandle device)
        {
            DeviceIoControl<STORAGE_DEVICE_NUMBER> deviceControl = new DeviceIoControl<STORAGE_DEVICE_NUMBER>();
            return deviceControl.GetDataForDevice(device, IoControlCode.IOCTL_STORAGE_GET_DEVICE_NUMBER);
        }
        
        //////http://www.pinvoke.net/default.aspx/Structures/STORAGE_DEVICE_NUMBER.html
        ////// return a unique device number for the given device path
        ////int GetDeviceNumber(string DevicePath)
        ////{
        ////    int ans = -1;

        ////    IntPtr h = CreateFile(DevicePath.TrimEnd('\\'), 0, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
        ////    if (h.ToInt32() != INVALID_HANDLE_VALUE)
        ////    {
        ////        int requiredSize;
        ////        STORAGE_DEVICE_NUMBER Sdn = new STORAGE_DEVICE_NUMBER();
        ////        int nBytes = Marshal.SizeOf(Sdn);
        ////        IntPtr ptrSdn = Marshal.AllocHGlobal(nBytes);

        ////        if (DeviceIoControl(h, IOCTL_STORAGE_GET_DEVICE_NUMBER, IntPtr.Zero, 0, ptrSdn, nBytes, out requiredSize, IntPtr.Zero))
        ////        {
        ////            Sdn = (STORAGE_DEVICE_NUMBER)Marshal.PtrToStructure(ptrSdn, typeof(STORAGE_DEVICE_NUMBER));
        ////            // just my way of combining the relevant parts of the
        ////            // STORAGE_DEVICE_NUMBER into a single number
        ////            ans = (Sdn.DeviceType << 8) + Sdn.DeviceNumber;
        ////        }
        ////        Marshal.FreeHGlobal(ptrSdn);
        ////        CloseHandle(h);
        ////    }
        ////    return ans;
        ////}
    }
}
