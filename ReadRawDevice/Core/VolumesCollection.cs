
namespace ReadRawDevice.Core
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represent a read-only collection of <see cref="SystemVolume"/>
    /// </summary>
    public class VolumesCollection : ReadOnlyCollection<SystemVolume>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolumesCollection"/> class.
        /// </summary>
        public VolumesCollection()
            : base(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VolumesCollection"/> class.
        /// </summary>
        /// <param name="internalList">The internal list.</param>
        internal VolumesCollection(IList<SystemVolume> internalList)
            : base(internalList)
        {
        }
    }
}
