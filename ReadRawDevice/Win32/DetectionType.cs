
namespace ReadRawDevice.Win32
{
    /// <summary>
    /// The DETECTION_TYPE enumeration type is used in conjunction with the IOCTL_DISK_GET_DRIVE_GEOMETRY_EX request and the DISK_GEOMETRY_EX structure to determine the type of formatting used by the BIOS to record the disk geometry.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552516(v=vs.85).aspx </remarks>
    internal enum DetectionType : uint
    {
        /// <summary>
        /// Indicates that the disk contains neither an INT 13h partition nor an extended INT 13h partition.
        /// </summary>
        DetectNone = 0,

        /// <summary>
        /// Indicates that the disk has a standard INT 13h partition.
        /// </summary>
        DetectInt13 = 1,

        /// <summary>
        /// Indicates that the disk contains an extended INT 13h partition.
        /// </summary>
        DetectExInt13 = 2
    }
}
