
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a physical location on a disk. It is the output buffer for the IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS control code.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa365727%28VS.85%29.aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct VOLUME_DISK_EXTENTS 
    {
        /// <summary>
        /// The number of disks in the volume (a volume can span multiple disks).
        /// An extent is a contiguous run of sectors on one disk. When the number of extents returned is greater than one (1), the error 
        /// code ERROR_MORE_DATA is returned. You should call DeviceIoControl again, allocating enough buffer space based on the value of 
        /// NumberOfDiskExtents after the first DeviceIoControl call.
        /// </summary>
        public int NumberOfDiskExtents;
            
        /// <summary>
        /// An array of DISK_EXTENT structures.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct /*, SizeConst = 0x10  */)]
        public DISK_EXTENT [] Extents;

        public VOLUME_DISK_EXTENTS(int size)
        {
            NumberOfDiskExtents = 0;
            Extents = new DISK_EXTENT[size];
        }
    }
}
