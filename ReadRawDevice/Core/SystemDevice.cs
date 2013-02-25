
namespace ReadRawDevice.Core
{
    /// <summary>
    /// Represent a single system device
    /// </summary>
    public class SystemDevice : DeviceHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemDevice"/> class.
        /// </summary>
        /// <param name="devicePath">The device path.</param>
        internal SystemDevice(string devicePath)
            : base(devicePath)
        {
            this.DevicePath = devicePath;
        }

        /// <summary>
        /// Gets the 'path' to device
        /// </summary>
        public string DevicePath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the 'friendly' name of device
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        public string FriendlyName
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the device class.
        /// </summary>
        /// <value>
        /// The device class.
        /// </value>
        public string DeviceClass
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the size of the disk (int bytes)
        /// </summary>
        /// <value>
        /// The size of the disk (in bytes)
        /// </value>
        public long? DiskSize
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the device number.
        /// This number enter as a last number to full device path such as: \\.\PhysicalDrive{X}
        /// </summary>
        /// <value>
        /// The device number.
        /// </value>
        public int DeviceNumber
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the bytes per sector.
        /// </summary>
        /// <value>
        /// The bytes per sector.
        /// </value>
        /// <remarks>ReadFile () function should align the reading buffer to this number</remarks>
        public int BytesPerSector
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the total sectors count for this device
        /// </summary>
        /// <value>
        /// The sectors count.
        /// </value>
        public long SectorsCount
        {
            get;
            internal set;
        }
    }
}
