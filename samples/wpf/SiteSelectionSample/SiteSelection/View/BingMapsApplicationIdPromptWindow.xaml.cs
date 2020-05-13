using System.Diagnostics;
using System.Windows;

namespace ThinkGeo.MapSuite.SiteSelection
{
    /// <summary>
    /// Interaction logic for InputBingMapKeyWindow.xaml
    /// </summary>
    public partial class BingMapsApplicationIdPromptWindow : Window
    {
        public BingMapsApplicationIdPromptWindow()
        {
            InitializeComponent();
        }

        public string ApplicationId
        {
            get { return txtkeyBox.Text; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.bingmapsportal.com/");
        }
    }
}