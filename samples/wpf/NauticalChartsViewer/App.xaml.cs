using System.Windows;
using System.Windows.Threading;

namespace NauticalChartsViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += (sender, e) =>
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }
    }
}
