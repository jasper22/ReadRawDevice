
namespace ReadRawDevice.Win32
{
    using System;

    /// <summary>
    /// Define values for <see cref="DeviceIoControl"/> method (if buffered, direct, etc...)
    /// </summary>
    [Flags]
    internal enum IoMethod : uint
    {
        /// <summary>
        /// Buffered
        /// </summary>
        Buffered = 0,

        /// <summary>
        /// InDirect
        /// </summary>
        InDirect = 1,
        
        /// <summary>
        /// OutDirect
        /// </summary>
        OutDirect = 2,

        /// <summary>
        /// Neither
        /// </summary>
        Neither = 3
    }
}
