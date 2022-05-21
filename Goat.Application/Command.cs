using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Goat.Application
{
    public class Command : ICommand
    {
        private readonly Action? _action;
        private readonly Action<object>? _parametrizedAction;

        public event EventHandler CanExecuteChanged;

        public Command(Action action)
        {
            _action = action;
        }

        public Command(Action<object> parametrizedAction)
        {
            _parametrizedAction = parametrizedAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_parametrizedAction != null)
            {
                _parametrizedAction(parameter);
            }
            else
            {
                _action();
            }
        }
    }
}
