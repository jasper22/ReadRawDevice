
namespace ReadRawDevice.Gui
{
    using System;
    using System.IO;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            base.OnStartup(e);

            this.MainWindow = new View.MainWindow();
            this.MainWindow.Show();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string fileName = Path.GetRandomFileName();
            fileName = Path.Combine(System.Environment.CurrentDirectory, fileName);

            using (FileStream fStream = File.OpenWrite(fileName))
            {
                StreamWriter sw = new StreamWriter(fStream);
                sw.WriteLine("Type is: " + e.GetType().FullName);
                sw.WriteLine("Exception object: ");
                sw.WriteLine(e.ExceptionObject);
            }


            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.InvariantCulture, "Unexpected error occurred in application initialization.\n\nAdditional information stored in file: " + fileName),
                string.Format(System.Globalization.CultureInfo.InvariantCulture, "Read Raw device")
                , MessageBoxButton.OK
                , MessageBoxImage.Error);
        }
    }
}
