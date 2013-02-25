using System.Runtime.InteropServices;

namespace ReadRawDevice.Win32
{
    /// <summary>
    /// Special structure that represent a union of <see cref="PARTITION_INFORMATION_MBR"/> and <see cref="PARTITION_INFORMATION_GPT"/> structures
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct PARTITION_INFORMATION_UNION
    {
        /// <summary>
        /// <see cref="PARTITION_INFORMATION_MBR"/>
        /// </summary>
        [FieldOffset(0)]
        public PARTITION_INFORMATION_MBR Mbr;

        /// <summary>
        /// <see cref="PARTITION_INFORMATION_GPT"/>
        /// </summary>
        [FieldOffset(0)]
        public PARTITION_INFORMATION_GPT Gpt;
    }
}
