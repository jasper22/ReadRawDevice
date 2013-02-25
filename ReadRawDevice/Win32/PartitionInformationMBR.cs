
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains partition information specific to master boot record (MBR) disks.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa365450(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct PARTITION_INFORMATION_MBR
    {
        /// <summary>
        /// The type of partition. For a list of values <see cref="DiskPartitionType"/>
        /// </summary>
        public DiskPartitionType PartitionType;

        /// <summary>
        /// If the member is TRUE, the partition is a boot partition. When this structure is used with the IOCTL_DISK_SET_PARTITION_INFO_EX control code, the value of this parameter is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool BootIndicator;

        /// <summary>
        /// If this member is TRUE, the partition is of a recognized type. When this structure is used with the IOCTL_DISK_SET_PARTITION_INFO_EX control code, the value of this parameter is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool RecognizedPartition;

        /// <summary>
        /// The number of hidden sectors to be allocated when the partition table is created.
        /// </summary>
        public UInt32 HiddenSectors;
    }
}
