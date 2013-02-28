
namespace ReadRawDevice.Core
{
    using System;
    using ReadRawDevice.Win32;

    /// <summary>
    /// Represent a single partition on disk (or volume)
    /// </summary>
    public class DevicePartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DevicePartition"/> class.
        /// </summary>
        /// <param name="partititonInformation">The partititon information.</param>
        internal DevicePartition(PARTITION_INFORMATION_EX partititonInformation)
        {
            this.PartitionStyle = partititonInformation.PartitionStyle.ConvertToString();
            this.StartingOffset = partititonInformation.StartingOffset;
            this.PartitionLength = partititonInformation.PartitionLength;
            this.Number = Convert.ToInt32(partititonInformation.PartitionNumber);

            switch(partititonInformation.PartitionStyle)
            {
                case PARTITION_STYLE.PARTITION_STYLE_MBR:
                    this.BootIndicator = partititonInformation.DriveLayoutInformaiton.Mbr.BootIndicator;
                    this.PartitionType = partititonInformation.DriveLayoutInformaiton.Mbr.PartitionType.ConvertToString();
                    this.HiddenSectors = Convert.ToInt32(partititonInformation.DriveLayoutInformaiton.Mbr.HiddenSectors);
                    break;
                case PARTITION_STYLE.PARTITION_STYLE_GPT:
                    this.Name = partititonInformation.DriveLayoutInformaiton.Gpt.Name;
                    this.GptId = partititonInformation.DriveLayoutInformaiton.Gpt.PartitionId;
                    this.GptPartitionType = partititonInformation.DriveLayoutInformaiton.Gpt.PartitionType;
                    this.GptAttributes = Convert.ToInt64(partititonInformation.DriveLayoutInformaiton.Gpt.Attributes);
                    break;
                case PARTITION_STYLE.PARTITION_STYLE_RAW:
                    break;
            }
        }

        /// <summary>
        /// Gets the partition style (MBR, GPR, etc...)
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
        /// Gets the starting offset of this partition on the device
        /// </summary>
        /// <value>
        /// The starting offset.
        /// </value>
        public long StartingOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of the partition (in bytes)
        /// </summary>
        /// <value>
        /// The length of the partition in bytes
        /// </value>
        public long PartitionLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number (1 based)
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int Number
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating if the member is TRUE, the partition is a boot partition. When this structure is used with the 
        /// IOCTL_DISK_SET_PARTITION_INFO_EX control code, the value of this parameter is ignored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if bootable; otherwise, <c>false</c>.
        /// </value>
        public bool? BootIndicator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the partition (NTFS, FAT32, etc...)
        /// </summary>
        /// <value>
        /// The type of the partition.
        /// </value>
        public string PartitionType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the hidden sectors.
        /// </summary>
        /// <value>
        /// The hidden sectors.
        /// </value>
        public int? HiddenSectors
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of GPT partition
        /// </summary>
        /// <value>
        /// The name of GPT partition
        /// </value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the GPT id.
        /// </summary>
        /// <value>
        /// The GPT id.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpt", Justification = "Name hint")]
        public Guid? GptId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the GPT partition.
        /// </summary>
        /// <value>
        /// The type of the GPT partition.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpt", Justification = "Name hint")]
        public Guid? GptPartitionType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the GPT attributes.
        /// </summary>
        /// <value>
        /// The GPT attributes.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpt", Justification = "Name hint")]
        public long GptAttributes
        {
            get;
            private set;
        }
    }
}
