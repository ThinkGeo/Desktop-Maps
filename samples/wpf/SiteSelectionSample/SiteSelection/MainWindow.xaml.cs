using System.Windows;

namespace ThinkGeo.MapSuite.SiteSelection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MapModel mapModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mapModel = new MapModel(wpfMap1);
            DataContext = new MainWindowViewModel(mapModel);
        }
    }
}