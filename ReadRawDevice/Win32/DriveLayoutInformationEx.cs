
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains extended information about a drive's partitions.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa364001(v=vs.85).aspx 
    /// Dynamic PARITION_INFORMATION_EX[] array could not be marshalled in CLR. So we need to threat it specially
    /// as described here: http://brianhehir.blogspot.co.il/2011/12/kernel32dll-deviceiocontrol-in-c.html
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DRIVE_LAYOUT_INFORMATION_EX
    {
        /// <summary>
        /// The style of the partitions on the drive enumerated by the PARTITION_STYLE enumeration.
        /// </summary>
        public PARTITION_STYLE PartitionStyle;

        /// <summary>
        /// The number of partitions on the drive. On hard disks with the MBR layout, this value will always be a multiple of 4. 
        /// Any partitions that are actually unused will have a partition type of PARTITION_ENTRY_UNUSED (0) set in the PartitionType member of 
        /// the PARTITION_INFORMATION_MBR structure of the Mbr member of the PARTITION_INFORMATION_EX structure of the PartitionEntry member of 
        /// this structure.
        /// </summary>
        public uint PartitionCount;

        /// <summary>
        /// Union of <see cref="DRIVE_LAYOUT_INFORMATION_MBR"/> and <see cref="DRIVE_LAYOUT_INFORMATION_GPT"/> structures
        /// </summary>
        public DRIVE_LAYOUT_INFORMATION_UNION DriveLayoutInformaiton;

        ///// <summary>
        ///// Related partitions
        ///// Forget partition entry, we can't marshal it directly as we don't know how big it is.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        //public PARTITION_INFORMATION_EX[] PartitionEntry;
    }
}
