
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains partition information for standard AT-style master boot record (MBR) and Extensible Firmware Interface (EFI) disks.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa365448(v=vs.85).aspx </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1900:ValueTypeFieldsShouldBePortable", MessageId = "Name", Justification = "By design")]
    [StructLayout(LayoutKind.Explicit), System.CLSCompliant(false)]
    internal struct PARTITION_INFORMATION_EX
    {
        /// <summary>
        /// The format of the partition. For a list of values, see PARTITION_STYLE.
        /// </summary>
        [FieldOffset(0)]
        public PARTITION_STYLE PartitionStyle;
        //public uint PartitionStyle;

        /// <summary>
        /// The starting offset of the partition
        /// </summary>
        [FieldOffset(8)]
        public long StartingOffset;

        /// <summary>
        /// The size of the partition, in bytes.
        /// </summary>
        [FieldOffset(16)]
        public long PartitionLength;

        /// <summary>
        /// The number of the partition (1-based).
        /// </summary>
        [FieldOffset(24)]
        public uint PartitionNumber;

        /// <summary>
        /// If this member is TRUE, the partition is rewritable. The value of this parameter should be set to TRUE.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Must exist")]
        [FieldOffset(28)]
        [MarshalAs(UnmanagedType.U1)]
        public bool RewritePartition;

        /// <summary>
        /// Union of <see cref="PARTITION_INFORMATION_MBR"/> and <see cref="PARTITION_INFORMATION_GPT"/> structures
        /// </summary>
        [FieldOffset(32)]
        public PARTITION_INFORMATION_UNION DriveLayoutInformaiton;
    }
}
