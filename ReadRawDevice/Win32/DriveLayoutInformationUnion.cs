
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represent a structure that a union of <see cref="DRIVE_LAYOUT_INFORMATION_MBR"/> and <see cref="DRIVE_LAYOUT_INFORMATION_GPT"/>
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa364001(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Explicit)]
    internal struct DRIVE_LAYOUT_INFORMATION_UNION
    {
        /// <summary>
        /// <see cref="DRIVE_LAYOUT_INFORMATION_MBR"/>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Struct declaration")]
        [FieldOffset(0)]
        public DRIVE_LAYOUT_INFORMATION_MBR Mbr;

        /// <summary>
        /// <see cref="DRIVE_LAYOUT_INFORMATION_GPT"/>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Struct declaration")]
        [FieldOffset(0)]
        public DRIVE_LAYOUT_INFORMATION_GPT Gpt;
    }
}
