
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides information about a drive's master boot record (MBR) partitions.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa364004(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DRIVE_LAYOUT_INFORMATION_MBR
    {
        /// <summary>
        /// The signature of the drive.
        /// </summary>
        public uint Signature;
    }
}
