
namespace ReadRawDevice.Gui.View
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReadRawDevice.Engine engine = new Engine();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Init();
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

        /// <summary>
        /// Handles the event of the Window_MouseDown control.
        /// Provide simple drag-move effect
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Window_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
