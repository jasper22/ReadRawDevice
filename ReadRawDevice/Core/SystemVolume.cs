
namespace ReadRawDevice.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ReadRawDevice.Win32;

    /// <summary>
    /// Represent a single system volume
    /// </summary>
    public class SystemVolume : DeviceHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemVolume"/> class.
        /// </summary>
        /// <param name="volumeName">Name of the volume.</param>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="devicePath">The device path.</param>
        internal SystemVolume(string volumeName, string deviceName, string devicePath)
            : base(GetFormattedVolumeName(volumeName))
        {
            this.VolumeName = volumeName;
            this.DeviceName = deviceName;
            this.DevicePath = devicePath;

            if (string.IsNullOrEmpty(this.DevicePath) == false)
            {
                System.IO.DriveInfo drvInfo = new System.IO.DriveInfo(this.DevicePath);
                try
                {
                    this.VolumeSize = drvInfo.TotalSize;
                }
                catch (Exception )
                {
                    this.VolumeSize = null;
                }
            }
        }

        /// <summary>
        /// Gets the name of the volume.
        /// </summary>
        /// <value>
        /// The name of the volume.
        /// </value>
        /// <example>\\?\Volume{b6b02346-65dd-11e1-aa19-806e6f6e6963}\</example>
        public string VolumeName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>
        /// The name of the device.
        /// </value>
        /// <example>\Device\HarddiskVolume1</example>
        public string DeviceName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the device path.
        /// </summary>
        /// <value>
        /// The device path.
        /// </value>
        /// <example>E:\</example>
        public string DevicePath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of disk this volume extends.
        /// </summary>
        /// <value>
        /// The number of disk extends.
        /// </value>
        public int? NumberOfDiskExtends
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the partition style (MBR, GPT, etc..)
        /// </summary>
        /// <value>
        /// The partition style.
        /// </value>
        public string PartitionStyle
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the size of the volume (in bytes)
        /// </summary>
        /// <value>
        /// The size of the volume in bytes
        /// </value>
        public long? VolumeSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the serial number.
        /// </summary>
        /// <value>
        /// The serial number.
        /// </value>
        [CLSCompliant(false)]
        public uint SerialNumber
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the name of the file system (NTFS, FAT, etc....)
        /// </summary>
        /// <value>
        /// The name of the file system.
        /// </value>
        public string FileSystemName
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the partition on this volume
        /// </summary>
        public IEnumerable<DevicePartition> Partitions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the volume formatted and stripped of un-needed characters
        /// </summary>
        /// <param name="deviceVolumeName">Name of the device volume.</param>
        /// <returns>Volume name formatted</returns>
        private static string GetFormattedVolumeName(string deviceVolumeName)
        {
            return deviceVolumeName.Substring(0, deviceVolumeName.Length - 1);
        }

        /// <summary>
        /// Sets the <see cref="VOLUME_DISK_EXTENTS"/> data on this object
        /// </summary>
        /// <param name="data">The data to be set</param>
        internal void SetDiskExtentInfo(Nullable<VOLUME_DISK_EXTENTS> data)
        {
            if (data.HasValue == false)
            {
                this.NumberOfDiskExtends = 0;
                return;
            }

            this.NumberOfDiskExtends = data.Value.NumberOfDiskExtents;
        }

        /// <summary>
        /// Sets the <see cref="DRIVE_LAYOUT_INFORMATION_EX"/> data on this object
        /// </summary>
        /// <param name="data">The data to be set</param>
        internal void SetDriveLayoutInfo(Tuple<DRIVE_LAYOUT_INFORMATION_EX, PARTITION_INFORMATION_EX[]> data)
        {
            if (data == null)
            {
                this.PartitionStyle = "<empty>";
                return;
            }

            this.PartitionStyle = data.Item1.PartitionStyle.ConvertToString();

            this.Partitions = new List<DevicePartition>((int)data.Item1.PartitionCount);

            this.Partitions = from singlePartition in data.Item2
                              select new DevicePartition(singlePartition);
        }

        ///// <summary>
        ///// Sets the <see cref="DISK_GEOMETRY_EX"/> data on this object
        ///// </summary>
        ///// <param name="data">The data to be set</param>
        //internal void SetDriveGeometry(Nullable<DISK_GEOMETRY_EX> data)
        //{
        //    if (data.HasValue == false)
        //    {
        //        return;
        //    }

        //    //this.DiskSize = data.Value.DiskSize;
        //}
    }
}
