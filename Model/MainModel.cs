
namespace ReadRawDevice.Gui.Model
{
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


        //protected async void Init()
        //{
        //    Core.DeviceCollection devColl = await engine.BuildDevicesAsync().ConfigureAwait(false);
        //    string path = System.IO.Path.Combine(Environment.CurrentDirectory, "diskOutput.bin");

        //    var device = devColl.Where(dev =>
        //    {
        //        return (dev.DiskSize.HasValue) && (dev.FriendlyName.Equals("Alex EFI_TEST USB Device"));
        //    }).First();

        //    IProgress<int> prog = new Progress<int>((val) => {
        //        System.Diagnostics.Trace.WriteLine("Progress is: " + val.ToString());
        //    });

        //    long bytesRead = await engine.ExtractDiskAsync(device, path, prog).ConfigureAwait(false);

        //    MessageBox.Show("All done.\nRead: " + bytesRead.ToString() + " bytes");
        //}

    }
}
