using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Wpf;

namespace MultipleJpeg2000RasterLayerSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string directory = txtFolder.Text;
            if (!Directory.Exists(directory))
            {
                return;
            }

            // Gets the files path.
            var filesPath = Directory.GetFiles(directory).Where(filePath => { return Path.GetExtension(filePath).ToLowerInvariant().Equals(".jp2"); });
            // Instantiates MultipleJpeg2000RasterLayer with the files path.
            var layer = new MultipleJpeg2000RasterLayer(filesPath);

            // Adds the MultipleJpeg2000RasterLayer into the map.
            var overlay = new LayerOverlay();
            overlay.Layers.Add(layer);
            wpfMap.Overlays.Add(overlay);

            layer.Open();
            wpfMap.MapUnit = GeographyUnit.Meter;
            wpfMap.CurrentExtent = layer.GetBoundingBox();

            wpfMap.Refresh();
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFolder.Text = fbd.SelectedPath;
                btnLoad.IsEnabled = true;
            }
        }
    }
}
