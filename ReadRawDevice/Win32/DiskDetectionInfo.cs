
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The DISK_DETECTION_INFO structure contains the detected drive parameters that are supplied by an x86 PC BIOS on boot.
    /// </summary>
    /// <remarks>MSND: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552601(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DISK_DETECTION_INFO
    {
        /// <summary>
        /// Contains the quantity, in bytes, of retrieved detect information.
        /// </summary>
        public ulong SizeOfDetectInfo;

        /// <summary>
        /// <see cref="DetectionType"/> for member explanation
        /// </summary>
        public DetectionType DetectionType;

        /// <summary>
        /// Union of <see cref="DISK_INT13_INFO"/> and <see cref="DISK_EX_INT13_INFO"/> structs
        /// If DetectionType == DetectInt13 then it contains structure DISK_INT13_INFO
        /// if DetectionType == DetectExInt13 then it contains structure DISK_EX_INT13_INFO
        /// </summary>
        public DiskInt13Union DiskInt13Union;
    }
}
