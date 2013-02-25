
namespace ReadRawDevice.Win32
{
    /// <summary>
    /// The STORAGE_DEVICE_NUMBER structure is used in conjunction with the IOCTL_STORAGE_GET_DEVICE_NUMBER request to retrieve the FILE_DEVICE_XXX device type,
    /// the device number, and, for a device that can be partitioned, the partition number assigned to a device by the driver when the device is started.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff566974(v=vs.85).aspx </remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct STORAGE_DEVICE_NUMBER
    {
        /// <summary>
        /// Specifies one of the system-defined FILE_DEVICE_XXX constants indicating the type of device (such as FILE_DEVICE_DISK, FILE_DEVICE_KEYBOARD, 
        /// and so forth) or a vendor-defined value for a new type of device. For more information, see Specifying Device Types. 
        /// </summary>
        /// <remarks>See also: <see cref="FileDeviceType"/></remarks>
        internal int DeviceType;

        /// <summary>
        /// Indicates the number of this device. This value is set to 0xFFFFFFFF (-1) for the disks that represent the physical paths of an MPIO disk.
        /// </summary>
        internal int DeviceNumber;

        /// <summary>
        /// Indicates the partition number of the device is returned in this member, if the device can be partitioned. Otherwise, -1 is returned. 
        /// </summary>
        internal int PartitionNumber;
    }
}
