
namespace ReadRawDevice.Win32
{
    using System;

    /// <summary>
    /// Define access mode for <c>CreateFile</c> function
    /// </summary>
    [Flags]
    internal enum FileAccess : uint
    {
        /// <summary>
        /// Read
        /// </summary>
        GenericRead = 0x80000000,
        
        /// <summary>
        /// Write
        /// </summary>
        GenericWrite = 0x40000000,
        
        /// <summary>
        /// Execute
        /// </summary>
        GenericExecute = 0x20000000,
        
        /// <summary>
        /// All access
        /// </summary>
        GenericAll = 0x10000000
    }
}
