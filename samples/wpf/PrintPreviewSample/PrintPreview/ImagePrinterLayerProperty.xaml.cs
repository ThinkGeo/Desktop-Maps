using System.Windows;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;

namespace PrintPreview
{
    /// <summary>
    /// Interaction logic for ImagePrinterLayerProperty.xaml
    /// </summary>
    public partial class ImagePrinterLayerProperty : Window
    {
        ImagePrinterLayer imagePrinterLayer = null;

        public ImagePrinterLayerProperty(ImagePrinterLayer imagePrinterLayer)
        {
            InitializeComponent();

            this.imagePrinterLayer = imagePrinterLayer;
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
            txtPath.Text = imagePrinterLayer.Image.PathFilename;
        }

        private void SetLayerProperties()
        {
            GeoImage image = new GeoImage(txtPath.Text);
            imagePrinterLayer.Image = image;

            imagePrinterLayer.Open();
            PointShape centerPoint = imagePrinterLayer.GetPosition().GetCenterPoint();
            imagePrinterLayer.SetPosition(image.Width, image.Height, centerPoint, PrintingUnit.Point);
        }

        private void LoadResize()
        {
            switch (imagePrinterLayer.ResizeMode)
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
                imagePrinterLayer.ResizeMode = PrinterResizeMode.Resizable;
            }
            else if (rbtnResizeFixed.IsChecked == true)
            {
                imagePrinterLayer.ResizeMode = PrinterResizeMode.Fixed;
            }
            else if (rbtnResizeMaintainRaio.IsChecked == true)
            {
                imagePrinterLayer.ResizeMode = PrinterResizeMode.MaintainAspectRatio;
            }
        }

        private void LoadDrag()
        {
            switch (imagePrinterLayer.DragMode)
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
                imagePrinterLayer.DragMode = PrinterDragMode.Fixed;
            }
            else if (rbtnDraggable.IsChecked == true)
            {
                imagePrinterLayer.DragMode = PrinterDragMode.Draggable;
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name 
            dlg.DefaultExt = ".*"; // Default file extension 
            dlg.Filter = "All files (*.*)|*.*"; // Filter files by extension 
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                txtPath.Text = dlg.FileName;
            }
        }


    }
}
