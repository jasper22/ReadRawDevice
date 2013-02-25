
namespace ReadRawDevice.Win32
{
    using System;

    /// <summary>
    /// File attributes defenition
    /// </summary>
    [Flags]
    internal enum FileAttributes : uint
    {
        /// <summary>
        /// No attributes/not set
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Readonly
        /// </summary>
        Readonly = 0x00000001,

        /// <summary>
        /// Hidden
        /// </summary>
        Hidden = 0x00000002,

        /// <summary>
        /// System
        /// </summary>
        System = 0x00000004,

        /// <summary>
        /// Directory
        /// </summary>
        Directory = 0x00000010,

        /// <summary>
        /// Archive
        /// </summary>
        Archive = 0x00000020,

        /// <summary>
        /// Device
        /// </summary>
        Device = 0x00000040,

        /// <summary>
        /// Normal
        /// </summary>
        Normal = 0x00000080,

        /// <summary>
        /// Temporary
        /// </summary>
        Temporary = 0x00000100,

        /// <summary>
        /// SparseFile 
        /// </summary>
        SparseFile = 0x00000200,

        /// <summary>
        /// ReparsePoint
        /// </summary>
        ReparsePoint = 0x00000400,

        /// <summary>
        /// Compressed
        /// </summary>
        Compressed = 0x00000800,

        /// <summary>
        /// Offline
        /// </summary>
        Offline = 0x00001000,

        /// <summary>
        /// NotContentIndexed
        /// </summary>
        NotContentIndexed = 0x00002000,

        /// <summary>
        /// Encrypted
        /// </summary>
        Encrypted = 0x00004000,

        /// <summary>
        /// Write_Through
        /// </summary>
        Write_Through = 0x80000000,

        /// <summary>
        /// Overlapped
        /// </summary>
        Overlapped = 0x40000000,

        /// <summary>
        /// NoBuffering
        /// </summary>
        NoBuffering = 0x20000000,

        /// <summary>
        /// RandomAccess
        /// </summary>
        RandomAccess = 0x10000000,

        /// <summary>
        /// SequentialScan
        /// </summary>
        SequentialScan = 0x08000000,

        /// <summary>
        /// DeleteOnClose
        /// </summary>
        DeleteOnClose = 0x04000000,

        /// <summary>
        /// BackupSemantics
        /// </summary>
        BackupSemantics = 0x02000000,

        /// <summary>
        /// PosixSemantics
        /// </summary>
        PosixSemantics = 0x01000000,

        /// <summary>
        /// OpenReparsePoint
        /// </summary>
        OpenReparsePoint = 0x00200000,

        /// <summary>
        /// OpenNoRecall
        /// </summary>
        OpenNoRecall = 0x00100000,

        /// <summary>
        /// FirstPipeInstance
        /// </summary>
        FirstPipeInstance = 0x00080000
    }
}
