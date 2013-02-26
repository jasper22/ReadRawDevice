
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The DISK_EX_INT13_INFO structure is used by the BIOS to report disk detection data for a partition with an extended INT13 format.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552610(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential), System.CLSCompliant(false)]
    public struct DISK_EX_INT13_INFO
    {
        /// <summary>
        /// Indicates the size of the buffer that the caller provides to the BIOS in which to return the requested drive data. 
        /// ExBufferSize must be 26 or greater. If ExBufferSize is less than 26, the BIOS returns an error . If ExBufferSize is between 30 
        /// and 66, the BIOS sets it to exactly 30 on exit. If ExBufferSize is 66 or greater, the BIOS sets it to exactly 66 on exit.
        /// </summary>
        public ushort ExBufferSize;

        /// <summary>
        /// Provides information about the drive. The following table describes the significance of each bit, where bit 0 is the least significant bit and bit 15 the most significant bit. A value of one in the indicated bit means that the feature described in the "Meaning" column is available. A value of zero in the indicated bit means that the feature is not available with this drive.
        /// </summary>
        public ushort ExFlags;

        /// <summary>
        /// Indicates the number of physical cylinders. This is one greater than the maximum cylinder number.
        /// </summary>
        public uint ExCylinders;

        /// <summary>
        /// Indicates the number of physical heads. This is one greater than the maximum head number.
        /// </summary>
        public uint ExHeads;

        /// <summary>
        /// Indicates the number of physical sectors per track. This number is the same as the maximum sector number.
        /// </summary>
        public uint ExSectorsPerTrack;

        /// <summary>
        /// Indicates the total count of sectors on the disk. This is one greater than the maximum logical block address.
        /// </summary>
        public ulong ExSectorsPerDrive;

        /// <summary>
        /// Indicates the sector size in bytes.
        /// </summary>
        public ushort ExSectorSize;

        /// <summary>
        /// Reserved
        /// </summary>
        public ushort ExReserved;
    }
}
