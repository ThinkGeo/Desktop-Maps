using System;
using System.Windows.Input;

namespace NauticalChartsViewer
{
    /// <summary>
    /// A lightweight <see cref="ICommand"/> whose <see cref="CanExecuteChanged"/> is routed
    /// through WPF's <see cref="CommandManager.RequerySuggested"/>, so bound controls re-query
    /// <see cref="CanExecute"/> automatically. This preserves the auto-requery behavior the app
    /// previously relied on from MvvmLight's CommandWpf.RelayCommand (CommunityToolkit.Mvvm's
    /// RelayCommand intentionally drops that integration and only refreshes via
    /// NotifyCanExecuteChanged).
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute();

        public void Execute(object parameter) => execute();
    }

    /// <summary>
    /// Generic counterpart of <see cref="RelayCommand"/> that passes a typed command parameter.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null) return true;
            if (parameter == null && typeof(T).IsValueType) return canExecute(default);
            return canExecute((T)parameter);
        }

        public void Execute(object parameter) => execute((T)parameter);
    }
}