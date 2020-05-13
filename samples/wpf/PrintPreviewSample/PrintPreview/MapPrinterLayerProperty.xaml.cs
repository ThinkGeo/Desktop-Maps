using System.Windows;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace PrintPreview
{
    /// <summary>
    /// Interaction logic for MapPrinterLayerProperty.xaml
    /// </summary>
    public partial class MapPrinterLayerProperty : Window
    {
        MapPrinterLayer mapPrinterLayer = null;
        bool isEditing = false;

        public MapPrinterLayerProperty(MapPrinterLayer mapPrinterLayer)
        {
            InitializeComponent();

            this.mapPrinterLayer = mapPrinterLayer;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLayerProperties();
            LoadResize();
            LoadDrag();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            SetLayerProperties();
            SetResize();
            SetDrag();

            DialogResult = true;
            Close();
        }

        private void LoadLayerProperties()
        {
            if (mapPrinterLayer.Layers.Count > 0
                && mapPrinterLayer.Layers[0].GetType() == typeof(ShapeFileFeatureLayer))
            {
                txtPath.Text = ((ShapeFileFeatureLayer)mapPrinterLayer.Layers[0]).ShapePathFilename;
                isEditing = true;
            }

            if (isEditing)
            {
                txtPath.IsEnabled = false;
                btnBrowse.IsEnabled = false;
            }
        }

        private void SetLayerProperties()
        {
            ShapeFileFeatureLayer shapeLayer = new ShapeFileFeatureLayer(txtPath.Text);
            shapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            shapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            shapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.SimpleColors.Black, 1));
            shapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.Capital1;
            shapeLayer.Open();

            if (isEditing)
            {
                mapPrinterLayer.Layers.RemoveAt(0);
            }
            else
            {
                mapPrinterLayer.SetPosition(500, 500, 0, 0, PrintingUnit.Point);
                mapPrinterLayer.MapExtent = shapeLayer.GetBoundingBox();
            }
            mapPrinterLayer.Layers.Add(shapeLayer);
        }

        private void LoadResize()
        {
            switch (mapPrinterLayer.ResizeMode)
            {
                case PrinterResizeMode.Fixed:
                    rbtnResizeFixed.IsChecked = true;
                    break;
                case PrinterResizeMode.MaintainAspectRatio:
                    rbtnResizeMaintainRaio.IsChecked = true;
                    break;
                case PrinterResizeMode.Resizable:
                    rbtnReshape.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        private void SetResize()
        {
            if (rbtnReshape.IsChecked == true)
            {
                mapPrinterLayer.ResizeMode = PrinterResizeMode.Resizable;
            }
            else if (rbtnResizeFixed.IsChecked == true)
            {
                mapPrinterLayer.ResizeMode = PrinterResizeMode.Fixed;
            }
            else if (rbtnResizeMaintainRaio.IsChecked == true)
            {
                mapPrinterLayer.ResizeMode = PrinterResizeMode.MaintainAspectRatio;
            }
        }

        private void LoadDrag()
        {
            switch (mapPrinterLayer.DragMode)
            {
                case PrinterDragMode.Fixed:
                    rbtnDragFixed.IsChecked = true;
                    break;
                case PrinterDragMode.Draggable:
                    rbtnDraggable.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        private void SetDrag()
        {
            if (rbtnDragFixed.IsChecked == true)
            {
                mapPrinterLayer.DragMode = PrinterDragMode.Fixed;
            }
            else if (rbtnDraggable.IsChecked == true)
            {
                mapPrinterLayer.DragMode = PrinterDragMode.Draggable;
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name 
            dlg.DefaultExt = ".*"; // Default file extension 
            dlg.Filter = "Shape files (*.shp)|*.shp";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                txtPath.Text = dlg.FileName;
            }
        }
    }
}
