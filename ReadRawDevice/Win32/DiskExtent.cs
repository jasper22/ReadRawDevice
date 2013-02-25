
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a disk extent.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa363968(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DISK_EXTENT
    {
        /// <summary>
        /// The number of the disk that contains this extent.
        /// This is the same number that is used to construct the name of the disk, for example, the X in "\\?\PhysicalDriveX" or "\\?\HarddiskX".
        /// </summary>
        public int DiskNumber;

        /// <summary>
        /// The offset from the beginning of the disk to the extent, in bytes.
        /// </summary>
        public long StartingOffset;

        /// <summary>
        /// The number of bytes in this extent.
        /// </summary>
        public long ExtentLength;
    }
}
