using System;
using System.Windows.Input;

namespace PICalculator.App.ViewModels
{
    internal class DelegateCommand : ICommand
    {
        private readonly Action<object> executeAction;
        private readonly Func<object, bool> canExecuteAction;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteAction)
        {
            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            this.canExecuteAction = canExecuteAction;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecuteAction?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            this.executeAction(parameter);
        }

        internal void InvokeCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
