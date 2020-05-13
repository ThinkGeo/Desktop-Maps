using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class CommandBase : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action execute;

        public CommandBase(Action execute)
            : this(execute, null)
        {
        }

        public CommandBase(Action execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
