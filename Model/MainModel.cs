
namespace ReadRawDevice.Gui.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using ReadRawDevice.Core;

    /// <summary>
    /// Represent the main 'model' for 'view-model'
    /// </summary>
    internal class MainModel
    {
        private Engine engine = null;
        private Core.DeviceCollection deviceCollection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainModel"/> class.
        /// </summary>
        internal MainModel()
        {
            engine = new Engine();
        }

        /// <summary>
        /// Gets the devices collection.
        /// </summary>
        /// <value>
        /// The devices collection.
        /// </value>
        internal DeviceCollection DeviceCollection
        {
            get
            {
                return deviceCollection;
            }
        }

        /// <summary>
        /// Gets the list of devices.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns><c>ObservableCollection</c> of <see cref="SystemDevice"/> list</returns>
        internal Task<ObservableCollection<SystemDevice>> GetDevices(CancellationToken token)
        {
            return engine.BuildDevicesAsync(token)
                .ContinueWith<ObservableCollection<SystemDevice>>(tsk => {
                    this.deviceCollection = tsk.Result;
                    return new ObservableCollection<SystemDevice>(tsk.Result);
                }
                ,token
                , TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled
                , TaskScheduler.Current);
        }


        /// <summary>
        /// Extracts the disk to provided file
        /// </summary>
        /// <param name="device">The device to extract from</param>
        /// <param name="outputFileName">Name of the output file including path</param>
        /// <param name="progressCallback">The progress callback.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Task that execute the process and return total number of bytes read from device</returns>
        internal Task<long> ExtractDisk(SystemDevice device, string outputFileName, IProgress<double> progressCallback, CancellationToken token)
        {
            return engine.ExtractDiskAsync(device, outputFileName, progressCallback, token);
        }
    }
}
