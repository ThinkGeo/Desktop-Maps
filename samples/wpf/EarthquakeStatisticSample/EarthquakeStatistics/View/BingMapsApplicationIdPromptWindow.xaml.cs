using System.Diagnostics;
using System.Windows;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
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
            get { return ApplicationIdTextBlock.Text; }
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.bingmapsportal.com/");
        }
    }
}