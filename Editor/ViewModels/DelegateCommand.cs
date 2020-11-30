using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Editor
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<Object> _execute;
        private readonly Func<Object, Boolean> _canExecute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }



        public DelegateCommand(Action<Object> execute) : this(null, execute) { }

        public DelegateCommand(Func<Object, Boolean> canExecute, Action<Object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }


    }
}
