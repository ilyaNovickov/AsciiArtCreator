using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsciiArtCreator.Wpf.Framework.Commands
{
    internal class ActionCommand<T> : ICommand
    {
        private Action<T> execute;
        private Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ActionCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(T parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(T parameter)
        {

            this.execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T inputparameter)
                return this.canExecute == null || this.canExecute(inputparameter);
            return false;
        }

        public void Execute(object parameter)
        {
            if (parameter is T inputparameter)
                this.execute(inputparameter);
        }
    }
}
