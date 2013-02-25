
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The DISK_INT13_INFO structure is used by the BIOS to report disk detection data for a partition with an INT13 format.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552624(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DISK_INT13_INFO
    {
        /// <summary>
        /// Corresponds to the Device/Head register defined in the AT Attachment (ATA) specification. When zero, the fourth bit of this register 
        /// indicates that drive zero is selected. When 1, it indicates that drive one is selected. The values of bits 0, 1, 2, 3, and 6 depend 
        /// on the command in the command register. Bits 5 and 7 are no longer used. For more information about the values that the Device/Head 
        /// register can hold, see the ATA specification.
        /// </summary>
        public ushort DriveSelect;

        /// <summary>
        /// Indicates the maximum number of cylinders on the disk.
        /// </summary>
        public ulong MaxCylinders;

        /// <summary>
        /// Indicates the number of sectors per track.
        /// </summary>
        public ushort SectorsPerTrack;

        /// <summary>
        /// Indicates the maximum number of disk heads.
        /// </summary>
        public ushort MaxHeads;

        /// <summary>
        /// Indicates the number of drives.
        /// </summary>
        public ushort NumberDrives;
    }
}
