
namespace ReadRawDevice.Win32
{
    using System;

    /// <summary>
    /// Represent a sharing mode
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363858(v=vs.85).aspx </remarks>
    [Flags]
    internal enum  FileShare : uint
    {
        /// <summary>
        /// Prevents other processes from opening a file or device if they request delete, read, or write access.
        /// </summary>
        FILE_SHARE_ZERO = 0x00000000,

        /// <summary>
        /// Enables subsequent open operations on a file or device to request read access.
        /// Otherwise, other processes cannot open the file or device if they request read access.
        /// If this flag is not specified, but the file or device has been opened for read access, the function fails.
        /// </summary>
        FILE_SHARE_READ = 0x00000001,

        /// <summary>
        /// Enables subsequent open operations on a file or device to request write access.
        /// Otherwise, other processes cannot open the file or device if they request write access.
        /// If this flag is not specified, but the file or device has been opened for write access or has a file mapping with write access, the function fails.
        /// </summary>
        FILE_SHARE_WRITE = 0x00000002,

        /// <summary>
        /// Enables subsequent open operations on a file or device to request delete access.
        /// Otherwise, other processes cannot open the file or device if they request delete access.
        /// If this flag is not specified, but the file or device has been opened for delete access, the function fails.
        /// Note  Delete access allows both delete and rename operations.
        /// </summary>
        FILE_SHARE_DELETE = 0x00000004
    }
}
