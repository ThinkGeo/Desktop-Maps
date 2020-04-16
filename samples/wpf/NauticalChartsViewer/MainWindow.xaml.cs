using System.Globalization;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private string defaultSampleDataFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
            @"\SampleData\Chicago\US4IL10M.000";

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => mainViewModel.Cleanup();
        }


        private void WpfMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point currentPoint = e.GetPosition(WpfMap);

            PointShape worldPoint = MapUtil.ToWorldCoordinate(WpfMap.CurrentExtent, new ScreenPointF((float)currentPoint.X, (float)currentPoint.Y), (float)WpfMap.ActualWidth, (float)WpfMap.ActualHeight);

            CurrentX.Text = worldPoint.X.ToString("f6", CultureInfo.InvariantCulture);
            CurrentY.Text = worldPoint.Y.ToString("f6", CultureInfo.InvariantCulture);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainViewModel = new MainViewModel(WpfMap);
            DataContext = mainViewModel;
            
            // Load the sample data
            ChartsManagmentWindow chartsManagmentWindow = new ChartsManagmentWindow();
            ChartsManagmentViewModel chartsManagmentViewModel = ((ChartsManagmentViewModel)chartsManagmentWindow.DataContext);
            chartsManagmentViewModel.LoadFile(defaultSampleDataFile);
            chartsManagmentViewModel.SelectedItems.Add(chartsManagmentViewModel.SelectedItem);
            chartsManagmentViewModel.HandleOkCommand();
        }

        private void WpfMap_CurrentScaleChanging(object sender, CurrentScaleChangingMapViewEventArgs e)
        {                                                  
            CurrentScale.Text = string.Format("1:{0:N}", e.NewScale);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}