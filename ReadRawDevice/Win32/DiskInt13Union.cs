using System.Runtime.InteropServices;

namespace ReadRawDevice.Win32
{
    /// <summary>
    /// Un-named struct for <see cref="DISK_DETECTION_INFO"/> structure
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct DiskInt13Union
    {
        /// <summary>
        /// <see cref="DISK_INT13_INFO"/> for member description
        /// </summary>
        [FieldOffset(0)]
        public DISK_INT13_INFO Int13;

        /// <summary>
        /// <see cref="DISK_EX_INT13_INFO"/> for member description
        /// </summary>
        [FieldOffset(0)]
        public DISK_EX_INT13_INFO ExInt13;
    }
}
