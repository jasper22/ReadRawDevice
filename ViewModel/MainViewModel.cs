
namespace ReadRawDevice.Gui.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Documents;
    using System.Windows.Input;
    using Microsoft.Win32;
    using ReadRawDevice.Core;
    using ReadRawDevice.Gui.Model;

    /// <summary>
    /// Main view-model for main-view
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        private MainModel mainModel = null;

        /// <summary>
        /// 'Property changed' delegate
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Command 'exit'
        /// </summary>
        private ICommand commandExit = null;

        /// <summary>
        /// Command 'extract'
        /// </summary>
        private ICommand commandExtract = null;

        /// <summary>
        /// Command 'refresh'
        /// </summary>
        private ICommand commandRefresh = null;

        /// <summary>
        /// Text representation of current 'VisualState'
        /// </summary>
        private string viewModelVisualState = string.Empty;

        /// <summary>
        /// The system devices collection
        /// </summary>
        private ObservableCollection<SystemDevice> systemDevicesCollection;

        /// <summary>
        /// Visual State: Normal (default state)
        /// </summary>
        private const string VS_STATE_NORMAL = "VS_Normal";

        /// <summary>
        /// Visual State: Working (will display animation)
        /// </summary>
        private const string VS_STATE_WORKING = "VS_Working";

        /// <summary>
        /// Visual State: Error
        /// </summary>
        private const string VS_STATE_ERROR = "VS_Error";

        /// <summary>
        /// Visual state: Extracting
        /// </summary>
        private const string VS_STATE_EXTRACT = "VS_Extract";

        /// <summary>
        /// The cancellation token source
        /// </summary>
        private CancellationTokenSource tokenSource = null;

        /// <summary>
        /// The current progress value of disk extraction
        /// </summary>
        private double progressValue = 0;

        /// <summary>
        /// The error document to present in 'Error condition'
        /// </summary>
        private FlowDocument errorDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            tokenSource = new CancellationTokenSource();
            mainModel = new MainModel();
            systemDevicesCollection = null;
            this.SelectedItem = null;
        }

        #region Button titles
        /// <summary>
        /// Gets the window title.
        /// </summary>
        /// <value>
        /// The window title.
        /// </value>
        public string WindowTitle
        {
            get
            {
                return Localization.LocalizedResource["WindowTitle"].ToString();
            }
        }

        /// <summary>
        /// Gets the 'button exit' title.
        /// </summary>
        /// <value>
        /// The button exit title.
        /// </value>
        public string ButtonExitTitle
        {
            get
            {
                return Localization.LocalizedResource["ButtonExitTitle"].ToString();
            }
        }

        /// <summary>
        /// Gets the 'button start' title.
        /// </summary>
        /// <value>
        /// The button start title.
        /// </value>
        public string ButtonStartTitle
        {
            get
            {
                return Localization.LocalizedResource["ButtonStartTitle"].ToString();
            }
        }

        /// <summary>
        /// Gets the button refresh title.
        /// </summary>
        /// <value>
        /// The button refresh title.
        /// </value>
        public string ButtonRefreshTitle
        {
            get
            {
                return Localization.LocalizedResource["ButtonRefreshTitle"].ToString();
            }
        }
        #endregion

        #region ViewModelVisualState
        /// <summary>
        /// Gets the string representation of the view-model visual-state
        /// </summary>
        /// <value>
        /// The string representation of the view-model visual-state
        /// </value>
        /// <remarks>Names must be 1:1 to the Blend VisualState names</remarks>
        public string ViewModelVisualState
        {
            get
            {
                return viewModelVisualState;
            }

            internal set
            {
                viewModelVisualState = value;
                OnPropertyChanged();

            }
        }
        #endregion

        #region CommandExit
        /// <summary>
        /// Gets the command object for exit logic
        /// </summary>
        /// <value>
        /// The command exit.
        /// </value>
        public ICommand CommandExit
        {
            get
            {
                if (commandExit == null)
                {
                    commandExit = new Commands.CommandExit(this);
                }

                return commandExit;
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute exit command] the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can execute exit command] the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        internal bool CanExecuteExitCommand(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the exit command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        internal void ExecuteExitCommand(object parameter)
        {
            App.Current.Shutdown();            
        }
        #endregion

        #region CommandExtract
        /// <summary>
        /// Gets the command extract.
        /// </summary>
        /// <value>
        /// The command extract.
        /// </value>
        public ICommand CommandExtract
        {
            get
            {
                if (commandExtract == null)
                    commandExtract = new Commands.CommandExtract(this);

                return commandExtract;
            }
        }

        /// <summary>
        /// Executes the extract command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        internal async void ExecuteExtractCommand(object parameter)
        {
            this.ViewModelVisualState = VS_STATE_EXTRACT;
            string fileName = MainViewModel.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                this.ViewModelVisualState = VS_STATE_NORMAL;
                return;
            }

            IProgress<double> progressCallback = new Progress<double>((val) =>
            {
                this.ProgressValue = val;
            });

            tokenSource.Cancel();
            tokenSource.Dispose();
            tokenSource = new CancellationTokenSource();

            long totalBytesRead = 0;

            try
            {
                totalBytesRead = await mainModel.ExtractDisk(this.SelectedItem, fileName, progressCallback, tokenSource.Token);
                this.ViewModelVisualState = VS_STATE_NORMAL;
            }
            catch (Exception exp_gen)
            {
                this.ViewModelVisualState = VS_STATE_NORMAL;
                OnErrorState(exp_gen);
            }

        }

        /// <summary>
        /// Gets or sets the progress value of current disk extraction
        /// </summary>
        /// <value>
        /// The progress value (in percents)
        /// </value>
        public double ProgressValue
        {
            get
            {
                return progressValue;
            }

            set
            {
                progressValue = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the name of the output file.
        /// </summary>
        /// <returns>If user enter any file the full path and file name will be returned, otherwise string.Empty will be returned</returns>
        protected static string FileName
        {
            get
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    CheckFileExists = false,
                    CheckPathExists = true,
                    Filter = "Binary file|*.bin|All files|*.*",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    RestoreDirectory = true,
                    Title = Localization.LocalizedResource["OutputFileDialogTitle"].ToString()
                };

                bool? result = saveFileDialog.ShowDialog();
                if (result.HasValue == false)
                {
                    return string.Empty;
                }

                return saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute extract command] the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can execute extract command] the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        internal bool CanExecuteExtractCommand(object parameter)
        {
            if (this.SelectedItem == null)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region CommandRefresh
        /// <summary>
        /// Gets the command refresh.
        /// </summary>
        /// <value>
        /// The command refresh.
        /// </value>
        public ICommand CommandRefresh
        {
            get
            {
                if (commandRefresh == null)
                {
                    commandRefresh = new Commands.CommandRefresh(this);
                }

                return commandRefresh;
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute refresh command] the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can execute refresh command] the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        internal bool CanExecuteRefreshCommand(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the refresh command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal async void ExecuteRefreshCommand(object parameter)
        {
            this.ViewModelVisualState = VS_STATE_WORKING;

            tokenSource.Cancel();
            tokenSource.Dispose();
            tokenSource = new CancellationTokenSource();

            try
            {
                SystemDevicesCollection = await mainModel.GetDevices(tokenSource.Token);
                this.ViewModelVisualState = VS_STATE_NORMAL;
            }
            catch (Exception exp_gen)
            {
                OnErrorState(exp_gen);
            }
        }
        #endregion

        /// <summary>
        /// Rise <see cref="PropertyChangedEventHandler"/> with provided caller name
        /// </summary>
        /// <param name="caller">The property name that changed</param>
        private void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        /// <summary>
        /// Gets the <see cref="SystemDevice"/> collection.
        /// </summary>
        /// <value>
        /// The <see cref="SystemDevice"/> collection.
        /// </value>
        public ObservableCollection<SystemDevice> SystemDevicesCollection
        {
            get
            {
                if (systemDevicesCollection == null)
                {
                    systemDevicesCollection = new ObservableCollection<SystemDevice>(mainModel.DeviceCollection);
                }

                return systemDevicesCollection;
            }

            internal set
            {
                this.systemDevicesCollection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public SystemDevice SelectedItem
        {
            get;

            set;
        }

        /// <summary>
        /// Called when error occurred.
        /// Change VisualState to error and display error message
        /// </summary>
        /// <param name="error">The exception that occurred</param>
        protected virtual void OnErrorState(Exception error)
        {
            this.ViewModelVisualState = VS_STATE_ERROR;

            tokenSource.Cancel();
            tokenSource.Dispose();

            this.ErrorDocument = ErrorDocumentCreator.Create(error);
        }

        /// <summary>
        /// Gets the error document.
        /// </summary>
        /// <value>
        /// The error document.
        /// </value>
        public FlowDocument ErrorDocument
        {
            get
            {
                return errorDocument;
            }

            private set
            {
                errorDocument = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposingManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposingManaged)
        {
            if (disposingManaged)
            {
                if (tokenSource != null)
                {
                    tokenSource.Cancel();
                    tokenSource.Dispose();
                    tokenSource = null;
                }
            }

            // Unmanaged disposed here
        }
    }
}
