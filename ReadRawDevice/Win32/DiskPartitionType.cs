
namespace ReadRawDevice.Win32
{
    /// <summary>
    /// The following table identifies the valid partition types that are used by disk drivers.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa363990(v=vs.85).aspx </remarks>
    internal enum DiskPartitionType : byte
    {
        /// <summary>
        /// An unused entry partition.
        /// </summary>
        PARTITION_ENTRY_UNUSED = 0x00,

        /// <summary>
        /// An extended partition.
        /// </summary>
        PARTITION_EXTENDED = 0x05,

        /// <summary>
        /// A FAT12 file system partition.
        /// </summary>
        PARTITION_FAT_12 = 0x01,

        /// <summary>
        /// A FAT16 file system partition.
        /// </summary>
        PARTITION_FAT_16 = 0x04,

        /// <summary>
        /// A FAT32 file system partition.
        /// </summary>
        PARTITION_FAT32 = 0x0B,

        /// <summary>
        /// An IFS partition (Installable File System)
        /// </summary>
        PARTITION_IFS = 0x07,

        /// <summary>
        /// A logical disk manager (LDM) partition.
        /// </summary>
        PARTITION_LDM = 0x42,

        /// <summary>
        /// An NTFT partition.
        /// </summary>
        PARTITION_NTFT = 0x80,

        /// <summary>
        /// A valid NTFT partition.
        /// The high bit of a partition type code indicates that a partition is part of an NTFT mirror or striped array.
        /// </summary>
        VALID_NTFT = 0xC0,

        /// <summary>
        /// Xenix partition of type 1
        /// </summary>
        PARTITION_XENIX_1 = 2,

        /// <summary>
        /// Xenix partition of type 2
        /// </summary>
        PARTITION_XENIX_2 = 3,

        /// <summary>
        /// Huge partition
        /// </summary>
        PARTITION_HUGE = 6,

        /// <summary>
        /// INT 13 
        /// </summary>
        PARTITION_XINT13 = 0xe,

        /// <summary>
        /// INT 13 extended
        /// </summary>
        PARTITION_XINT13_EXTENDED = 0xf,

        /// <summary>
        /// PREP
        /// </summary>
        PARTITION_PREP = 0x41,

        /// <summary>
        /// Unix
        /// </summary>
        PARTITION_UNIX = 0x63
    }
}
