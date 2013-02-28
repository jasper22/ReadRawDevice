
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains GUID partition table (GPT) partition information.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa365449(v=vs.85).aspx </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1900:ValueTypeFieldsShouldBePortable", MessageId = "Name", Justification = "By design")]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode), CLSCompliant(false)]
    internal struct PARTITION_INFORMATION_GPT
    {
        /// <summary>
        /// A GUID that identifies the partition type.
        /// Each partition type that the EFI specification supports is identified by its own GUID, which is published by the developer of the partition.
        /// This member can be one of the following values.
        /// </summary>
        [FieldOffset(0)]
        public Guid PartitionType;

        /// <summary>
        /// The GUID of the partition.
        /// </summary>
        [FieldOffset(16)]
        public Guid PartitionId;

        /// <summary>
        /// The Extensible Firmware Interface (EFI) attributes of the partition.
        /// This member can be one or more of the following values.
        /// </summary>
        [FieldOffset(32)]
        public ulong Attributes;

        /// <summary>
        /// A wide-character string that describes the partition.
        /// </summary>
        [FieldOffset(40)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Name;
    }
}
