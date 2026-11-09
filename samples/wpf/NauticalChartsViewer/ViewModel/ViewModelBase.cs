using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NauticalChartsViewer
{
    /// <summary>
    /// Base class for view models: raises <see cref="INotifyPropertyChanged.PropertyChanged"/> for
    /// data binding and exposes a <see cref="Cleanup"/> hook that detaches the view model from the
    /// <see cref="Messenger"/> when the owning view goes away.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Cleanup()
        {
            Messenger.Default.UnregisterAll(this);
        }
    }
}