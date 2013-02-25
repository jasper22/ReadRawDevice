
namespace ReadRawDevice.Win32
{
    /// <summary>
    /// Represents the format of a partition.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa365452(v=vs.85).aspx </remarks>
    internal enum PARTITION_STYLE : uint
    {
        /// <summary>
        /// Master boot record (MBR) format. This corresponds to standard AT-style MBR partitions.
        /// </summary>
        PARTITION_STYLE_MBR = 0,

        /// <summary>
        /// GUID Partition Table (GPT) format.
        /// </summary>
        PARTITION_STYLE_GPT = 1,

        /// <summary>
        /// Partition not formatted in either of the recognized formats—MBR or GPT.
        /// </summary>
        PARTITION_STYLE_RAW = 2,
    }
}
