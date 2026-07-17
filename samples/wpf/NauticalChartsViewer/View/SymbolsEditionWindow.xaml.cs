using System.Windows;

namespace NauticalChartsViewer
{
   
    public partial class SymbolsEditionWindow : Window
    {
        public SymbolsEditionWindow()
        {
            InitializeComponent();
            var symbolsEditionViewModel = new SymbolsEditionViewModel();
            DataContext = symbolsEditionViewModel;
            Loaded += (s, e) =>
            {
                Messenger.Default.Register<WindowStateMessage>(this, HandleChartWindowMessage);
            };

            Unloaded += (sender, e) =>
            {
                Messenger.Default.UnregisterAll(this);
            };
        }

        private void HandleChartWindowMessage(WindowStateMessage message)
        {
            switch (message.WindowState)
            {
                case S57WindowState.Close:
                    Close();
                    break;
            }
        }
    }
}
