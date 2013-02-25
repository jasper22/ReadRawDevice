
namespace ReadRawDevice
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using Microsoft.Win32.SafeHandles;
    using ReadRawDevice.Core;
    using ReadRawDevice.Win32;

    /// <summary>
    /// Internal object responsible for building <see cref="SystemDevice"/> objects
    /// </summary>
    internal class DeviceBuilder : Native
    {
        private CancellationToken token = CancellationToken.None;

        /// <summary>
        /// Builds the list of devices
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Collection of system devices</returns>
        internal DeviceCollection Build(CancellationToken token)
        {
            this.token = token;

            IEnumerable<SystemDevice> tmpListOfDevices = GetListOfDevices(token);
            List<SystemDevice> listOfDevices = new List<SystemDevice>(tmpListOfDevices);

            token.ThrowIfCancellationRequested();

            listOfDevices.All(dev =>
            {
                SetDeviceNumber(dev, token);
                SetDeviceSize(dev, token);
                return true;
            });

            listOfDevices.Count();

            return new DeviceCollection(listOfDevices);
        }

        /// <summary>
        /// Gets the list of devices.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>List of detected devices</returns>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// Could not execute SetupDi functions
        /// </exception>
        protected IEnumerable<SystemDevice> GetListOfDevices(CancellationToken token)
        {
            List<SystemDevice> listOfDevices = new List<SystemDevice>();

            int flags = (int) (SetupDiGetClassDevsFlags.DIGCF_ALLCLASSES |SetupDiGetClassDevsFlags.DIGCF_DEVICEINTERFACE| SetupDiGetClassDevsFlags.DIGCF_PRESENT);
            Guid aquireGuid = Guid.Empty;
            
            IntPtr deviceHandle = UnsafeNativeMethods.SetupDiGetClassDevs(ref aquireGuid, IntPtr.Zero, IntPtr.Zero, flags);
            if (deviceHandle.ToInt32() == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
            {
                throw new Win32Exception("Could not execute SetupDi functions", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            SP_DEVICE_INTERFACE_DATA spDeviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
            spDeviceInterfaceData.cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA));

            Guid classGuid = SetupDiInterfacesGuid.GUID_DEVINTERFACE_DISK;
            uint memberIndex = 0;
            bool functionResult = false;

            do
            {
                functionResult = UnsafeNativeMethods.SetupDiEnumDeviceInterfaces(deviceHandle, IntPtr.Zero, ref classGuid, memberIndex, ref spDeviceInterfaceData);
                if (functionResult == false)
                {
                    int nativeError = Marshal.GetLastWin32Error();

                    UnsafeNativeMethods.SetupDiDestroyDeviceInfoList(deviceHandle);

                    if (nativeError == UnsafeNativeMethods.ERROR_NO_MORE_ITEMS)
                    {
                        // all done
                        break;
                    }
                    else
                    {
                        // Other error occurred
                        throw new Win32Exception("Error occurred while enumerating devices", new Win32Exception(Marshal.GetLastWin32Error()));
                    }
                }

                SP_DEVINFO_DATA spDevInfoData = new SP_DEVINFO_DATA();
                spDevInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
                spDevInfoData.DevInst = 0;
                spDevInfoData.Reserved = IntPtr.Zero;

                SP_DEVICE_INTERFACE_DETAIL_DATA spDeviceInterfaceDetailedData = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                if (IntPtr.Size == 8)
                {
                    // for 64 bit operating systems
                    spDeviceInterfaceDetailedData.cbSize = 8;
                }
                else
                {
                    // for 32 bit systems
                    spDeviceInterfaceDetailedData.cbSize = 4 + Marshal.SystemDefaultCharSize; 
                }

                uint nRequiredSize = 0;
                uint bufferSize = 200;

                //
                // Should send twice - first time it's only return the 'nRequiredSize' value (but we already calculated it)
                bool success = UnsafeNativeMethods.SetupDiGetDeviceInterfaceDetail(deviceHandle, 
                                                                                    ref spDeviceInterfaceData,
                                                                                    ref spDeviceInterfaceDetailedData,
                                                                                    bufferSize, 
                                                                                    out nRequiredSize, 
                                                                                    ref spDevInfoData);

                success = UnsafeNativeMethods.SetupDiGetDeviceInterfaceDetail(deviceHandle,
                                                                                ref spDeviceInterfaceData,
                                                                                ref spDeviceInterfaceDetailedData,
                                                                                nRequiredSize, 
                                                                                out nRequiredSize,
                                                                                ref spDevInfoData);

                //if (success == false)
                //{
                //    // Still 'false' after sending twice
                //    UnsafeNativeMethods.SetupDiDestroyDeviceInfoList(deviceHandle);
                //    throw new Win32Exception("Could not receive details about device", new Win32Exception(Marshal.GetLastWin32Error()));
                //}

                // Device
                var dev = new SystemDevice(spDeviceInterfaceDetailedData.DevicePath);
                dev.FriendlyName = SetupDiGetDeviceProperty.GetProperty(deviceHandle, spDevInfoData, SetupDiGetDeviceRegistryPropertyEnum.SPDRP_FRIENDLYNAME);
                dev.DeviceClass = SetupDiGetDeviceProperty.GetProperty(deviceHandle, spDevInfoData, SetupDiGetDeviceRegistryPropertyEnum.SPDRP_CLASS);
                listOfDevices.Add(dev);

                // Next device
                memberIndex++;

            } while (true);

            deviceHandle = IntPtr.Zero;

            return listOfDevices;
        }

        /// <summary>
        /// Gets the name of the volume.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Volume name as string</returns>
        protected string GetVolumeName(string path)
        {
            StringBuilder lpszVolumeName = new StringBuilder(UnsafeNativeMethods.MAX_PATH);
            bool functionResult = false;

            path += "\\";

            functionResult = UnsafeNativeMethods.GetVolumeNameForVolumeMountPoint(path, lpszVolumeName, UnsafeNativeMethods.MAX_PATH);
            if (functionResult == false)
            {
                //Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                return string.Empty;
            }

            return lpszVolumeName.ToString();
        }

        /// <summary>
        /// Sets the size of disk the device.
        /// </summary>
        /// <param name="device">The device to query</param>
        /// <param name="token">The cancellation token.</param>
        protected void SetDeviceSize(SystemDevice device, CancellationToken token)
        {
            DISK_GEOMETRY_EX diskGeometry;

            if (DeviceIoControl<int>.IsAccessible(device) == false)
            {
                // Device is not ready
                return;
            }

            token.ThrowIfCancellationRequested();

            diskGeometry = base.GetDriveGeometry(device);

            token.ThrowIfCancellationRequested();

            device.DiskSize = diskGeometry.DiskSize;
            device.BytesPerSector = diskGeometry.Geometry.BytesPerSector;
            device.SectorsCount = diskGeometry.Geometry.SectorsCount;
        }

        /// <summary>
        /// Function will query device for unique device number and set it on <see cref="SystemDevice"/> object
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="token">The token.</param>
        protected void SetDeviceNumber(SystemDevice device, CancellationToken token)
        {
            STORAGE_DEVICE_NUMBER devNumber;

            if (DeviceIoControl<int>.IsAccessible(device) == false)
            {
                // Device is not ready
                return;
            }

            token.ThrowIfCancellationRequested();

            devNumber = base.GetDeviceNumber(device);

            token.ThrowIfCancellationRequested();

            device.DeviceNumber = devNumber.DeviceNumber;
        }
    }
}
