
namespace ReadRawDevice
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ReadRawDevice.Core;

    /// <summary>
    /// Object that provide all functions to read from device
    /// </summary>
    internal class Reader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reader"/> class.
        /// </summary>
        internal Reader()
        {
        }

        internal Task SaveVolumeDataRaw(SystemVolume device, string outputFileName, IProgress<int> progressCallback, CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {


            }
            , token);
        }
    }
}
