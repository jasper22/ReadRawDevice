
namespace ReadRawDevice.Gui.ViewModel.Commands
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Windows.Input;

    /// <summary>
    /// Represent 'Exit' command
    /// </summary>
    public class CommandExit : ICommand
    {
        private MainViewModel viewModel = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExit"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        internal CommandExit(MainViewModel viewModel)
        {
            Contract.Requires(viewModel != null);

            this.viewModel = viewModel;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return viewModel.CanExecuteExitCommand(parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            viewModel.ExecuteExitCommand(parameter);
        }
    }
}
