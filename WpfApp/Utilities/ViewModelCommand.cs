using System;
using System.Windows.Input;

namespace WpfApp.Utilities
{
    public class ViewModelCommand : ICommand
    {
        private readonly Action<object> _Execute;
        private readonly Predicate<object> _CanExecute;

        public ViewModelCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
