
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The DISK_GEOMETRY_EX structure is a variable-length structure composed of a DISK_GEOMETRY structure followed by a DISK_PARTITION_INFO structure followed, in turn, by a DISK_DETECTION_INFO structure.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552618(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DISK_GEOMETRY_EX
    {
        /// <summary>
        /// <see cref="DISK_GEOMETRY"/> for a description of this member.
        /// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        public DISK_GEOMETRY Geometry;

        /// <summary>
        /// Contains the size in bytes of the disk.
        /// </summary>
        public long DiskSize;

        /// <summary>
        /// <see cref="DISK_PARTITION_INFO"/> for a description of this member.
        /// </summary>
        public DISK_PARTITION_INFO PartitionInformation;

        /// <summary>
        /// <see cref="DISK_DETECTION_INFO"/> for a description of this member.
        /// </summary>
        ///[Obsolete("Could not be calculated in C#. In C++ it using a special macro: DiskGeometryGetDetect() and it calculated with DISK_PARTITION_INFO.SizeOfPartitionInfo offset")]
        public DISK_DETECTION_INFO DiskDetectionInfo;
    }
}
