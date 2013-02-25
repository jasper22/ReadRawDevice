
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains information about a drive's GUID partition table (GPT) partitions.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa364003(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DRIVE_LAYOUT_INFORMATION_GPT
    {
        /// <summary>
        /// The GUID of the disk.
        /// </summary>
        public Guid DiskId;

        /// <summary>
        /// The starting byte offset of the first usable block.
        /// </summary>
        public long StartingUsableOffset;

        /// <summary>
        /// The size of the usable blocks on the disk, in bytes.
        /// </summary>
        public long UsableLength;

        /// <summary>
        /// The maximum number of partitions that can be defined in the usable block.
        /// </summary>
        public int MaxPartitionCount;
    }
}
