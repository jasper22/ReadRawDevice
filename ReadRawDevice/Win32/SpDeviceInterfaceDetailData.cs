
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An SP_DEVICE_INTERFACE_DETAIL_DATA structure contains the path for a device interface.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552343(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
    {
        /// <summary>
        /// The size, in bytes, of the SP_DEVICE_INTERFACE_DETAIL_DATA structure. For more information, see the following Remarks section.
        /// </summary>
        public Int32 cbSize;

        /// <summary>
        /// A NULL-terminated string that contains the device interface path. This path can be passed to Win32 functions such as CreateFile.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DevicePath;
    }
}
