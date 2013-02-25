
namespace ReadRawDevice.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Describes the geometry of disk devices and media.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363972(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DISK_GEOMETRY
    {
        /// <summary>
        /// The number of cylinders. See LARGE_INTEGER.
        /// </summary>
        public long Cylinders;

        /// <summary>
        /// The type of media. For a list of values, <see cref="MediaType"/>
        /// </summary>
        public MediaType MediaType;

        /// <summary>
        /// The number of tracks per cylinder.
        /// </summary>
        public int TracksPerCylinder;

        /// <summary>
        /// The number of sectors per track
        /// </summary>
        public int SectorsPerTrack;

        /// <summary>
        /// The number of bytes per sector.
        /// </summary>
        public int BytesPerSector;

        /// <summary>
        /// Gets the size of the disk (in bytes)
        /// </summary>
        public long DiskSize
        {
            get
            {
                return Cylinders * (long)TracksPerCylinder * (long)SectorsPerTrack * (long)BytesPerSector;
            }
        }

        /// <summary>
        /// Gets the total sectors count for this device
        /// </summary>
        /// <value>
        /// The sectors count.
        /// </value>
        public long SectorsCount
        {
            get
            {
                return Cylinders * TracksPerCylinder * SectorsPerTrack;
            }
        }
    }
}
