using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadRawDevice.Gui.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReadRawDevice.Engine engine = new Engine();

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        protected async void Init()
        {
            Core.DeviceCollection devColl = await engine.BuildDevicesAsync().ConfigureAwait(false);
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "diskOutput.bin");

            var device = devColl.Where(dev =>
            {
                return (dev.DiskSize.HasValue) && (dev.FriendlyName.Equals("Alex EFI_TEST USB Device"));
            }).First();

            IProgress<int> prog = new Progress<int>((val) => {
                System.Diagnostics.Trace.WriteLine("Progress is: " + val.ToString());
            });

            long bytesRead = await engine.ExtractDiskAsync(device, path, prog).ConfigureAwait(false);

            MessageBox.Show("All done.\nRead: " + bytesRead.ToString() + " bytes");
        }
    }
}
