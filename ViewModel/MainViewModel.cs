
namespace ReadRawDevice.Gui.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Main view-model for main-view
    /// </summary>
    public class MainViewModel
    {
        private ICommand commandExit = null;
        private ICommand commandExtract = null;

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
        internal void ExecuteExtractCommand(object parameter)
        {
            throw new NotImplementedException();
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
            return false;
        }
        #endregion

    }
}
