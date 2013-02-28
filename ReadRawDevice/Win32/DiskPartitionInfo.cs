
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The DISK_PARTITION_INFO structure is used to report information about the disk's partition table.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552629(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Explicit)]
    internal struct DISK_PARTITION_INFO
    {
        /// <summary>
        /// Size of this structure in bytes. Set to sizeof(DISK_PARTITION_INFO).
        /// </summary>
        [FieldOffset(0)]
        public int SizeOfPartitionInfo;

        /// <summary>
        /// Takes a <see cref="PARTITION_STYLE"/> enumerated value that specifies the type of partition table the disk contains.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Struct declaration")]
        [FieldOffset(4)]
        public PARTITION_STYLE PartitionStyle;

        /// <summary>
        /// If PartitionStyle == MBR
        /// this value could be Signature or CheckSum
        /// 
        /// Signature
        /// Specifies the signature value, which uniquely identifies the disk. The Mbr member of the union is used to specify the disk signature 
        /// data for a disk formatted with a Master Boot Record (MBR) format partition table. Any other value indicates that the partition is 
        /// not a boot partition. This member is valid when PartitionStyle is PARTITION_STYLE_MBR.
        /// 
        /// CheckSum
        /// Specifies the checksum for the master boot record. The Mbr member of the union is used to specify the disk signature data for a disk 
        /// formatted with a Master Boot Record (MBR) format partition table. This member is valid when PartitionStyle is PARTITION_STYLE_MBR.
        /// 
        /// 
        /// if PartitionStyle == GPT
        /// 
        /// DiskId
        /// Specifies the GUID that uniquely identifies the disk. The Gpt member of the union is used to specify the disk signature data for 
        /// a disk that is formatted with a GUID Partition Table (GPT) format partition table. This member is valid when PartitionStyle is 
        /// PARTITION_STYLE_GPT. The GUID data type is described on the Using GUIDs in Drivers reference page.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Struct declaration")]
        [FieldOffset(8)]
        public uint MbrSignature;

        /// <summary>
        /// if PartitionStyle == GPT
        /// 
        /// DiskId
        /// Specifies the GUID that uniquely identifies the disk. The Gpt member of the union is used to specify the disk signature data for 
        /// a disk that is formatted with a GUID Partition Table (GPT) format partition table. This member is valid when PartitionStyle is 
        /// PARTITION_STYLE_GPT. The GUID data type is described on the Using GUIDs in Drivers reference page.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Struct declaration")]
        [FieldOffset(8)]
        public Guid DiskId;
    }
}
