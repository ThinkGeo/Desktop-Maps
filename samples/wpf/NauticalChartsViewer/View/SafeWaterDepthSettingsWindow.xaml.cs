using System.Windows;

namespace NauticalChartsViewer
{
    /// <summary>
    /// Interaction logic for SafeWaterDepthSettingsWindow.xaml
    /// </summary>
    public partial class SafeWaterDepthSettingsWindow : Window
    {
        private SafeWaterDepthSettingsViewModel safeWaterDepthSettingViewModel;

        public SafeWaterDepthSettingsWindow()
        {
            InitializeComponent();

            safeWaterDepthSettingViewModel = new SafeWaterDepthSettingsViewModel();
            DataContext = safeWaterDepthSettingViewModel;

            Loaded += (s, e) =>
            {
                Messenger.Default.Register<WindowStateMessage>(this, "SafeWaterDepthSettingsWindow", HandleWindowStateMessage);
            };

            Unloaded += (sender, e) =>
            {
                safeWaterDepthSettingViewModel.Cleanup();
                Messenger.Default.UnregisterAll(this);
            };
        }

        private void HandleWindowStateMessage(WindowStateMessage message)
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
