
namespace ReadRawDevice.Core
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represent a read-only collection of <see cref="SystemDevice"/>
    /// </summary>
    public class DeviceCollection : ReadOnlyCollection<SystemDevice>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceCollection"/> class.
        /// </summary>
        public DeviceCollection()
            : base(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceCollection"/> class.
        /// </summary>
        /// <param name="listOfDevices">The list of devices.</param>
        public DeviceCollection(IList<SystemDevice> listOfDevices)
            : base(listOfDevices)
        {
        }
    }
}
