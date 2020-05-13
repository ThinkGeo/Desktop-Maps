using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;

namespace PrintPreview
{
    /// <summary>
    /// Interaction logic for LabelPrinterLayerProperty.xaml
    /// </summary>
    public partial class LabelPrinterLayerProperty : Window
    {
        LabelPrinterLayer labelPrinterLayer = null;

        public LabelPrinterLayerProperty(LabelPrinterLayer labelPrinterLayer)
        {
            InitializeComponent();

            this.labelPrinterLayer = labelPrinterLayer;
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
            txtText.Text = labelPrinterLayer.Text;
            lbFontSample.FontSize = labelPrinterLayer.Font.Size;
            bool knownSize = false;
            for (int i = 0; i < cbxFontSize.Items.Count; i++)
            {
                if (labelPrinterLayer.Font.Size.ToString() == ((ComboBoxItem)cbxFontSize.Items[i]).Content.ToString())
                {
                    cbxFontSize.SelectedIndex = i;
                    knownSize = true;
                    break;
                }
            }
            // If the current size is not in the list, add one
            if (!knownSize)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = labelPrinterLayer.Font.Size;
                cbxFontSize.Items.Add(item);
                cbxFontSize.SelectedIndex = cbxFontSize.Items.Count - 1;
            }

            bool knownFontName = false;
            lbFontSample.FontFamily = new FontFamily(labelPrinterLayer.Font.FontName);
            for (int i = 0; i < cbxFontName.Items.Count; i++)
            {
                if (labelPrinterLayer.Font.FontName.ToString() == ((ComboBoxItem)cbxFontName.Items[i]).Content.ToString())
                {
                    cbxFontName.SelectedIndex = i;
                    knownFontName = true;
                    break;
                }
            }
            // If the current font name is not in the list, add one
            if (!knownFontName)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = labelPrinterLayer.Font.FontName;
                cbxFontName.Items.Add(item);
                cbxFontName.SelectedIndex = cbxFontName.Items.Count - 1;
            }

            if (labelPrinterLayer.PrinterWrapMode == PrinterWrapMode.WrapText)
            {
                chkEnableWrapText.IsChecked = true;
            }
            else
            {
                chkEnableWrapText.IsChecked = false;
            }
        }

        private void SetLayerProperties()
        {
            labelPrinterLayer.Text = txtText.Text;
            string fontName = cbxFontName.Text;
            float fontSize = float.Parse(cbxFontSize.Text);
            labelPrinterLayer.Font = new GeoFont(fontName, fontSize);

            if (chkEnableWrapText.IsChecked == true)
            {
                labelPrinterLayer.PrinterWrapMode = PrinterWrapMode.WrapText;
            }
            else
            {
                labelPrinterLayer.PrinterWrapMode = PrinterWrapMode.AutoSizeText;
            }
        }

        private void Font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (e.AddedItems.Count > 0)
                {
                    if (sender == cbxFontName)
                    {
                        lbFontSample.FontFamily = new FontFamily(((ComboBoxItem)e.AddedItems[0]).Content.ToString());
                    }
                    if (sender == cbxFontSize)
                    {
                        lbFontSample.FontSize = double.Parse(((ComboBoxItem)e.AddedItems[0]).Content.ToString());
                    }
                }
                else
                {
                    if (sender == cbxFontName)
                    {
                        lbFontSample.FontFamily = new FontFamily(cbxFontName.Text);
                    }
                    if (sender == cbxFontSize)
                    {
                        lbFontSample.FontSize = double.Parse(cbxFontSize.Text);
                    }
                }

            }
        }

        private void LoadResize()
        {
            switch (labelPrinterLayer.ResizeMode)
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
                labelPrinterLayer.ResizeMode = PrinterResizeMode.Resizable;
            }
            else if (rbtnResizeFixed.IsChecked == true)
            {
                labelPrinterLayer.ResizeMode = PrinterResizeMode.Fixed;
            }
            else if (rbtnResizeMaintainRaio.IsChecked == true)
            {
                labelPrinterLayer.ResizeMode = PrinterResizeMode.MaintainAspectRatio;
            }
        }

        private void LoadDrag()
        {
            switch (labelPrinterLayer.DragMode)
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
                labelPrinterLayer.DragMode = PrinterDragMode.Fixed;
            }
            else if (rbtnDraggable.IsChecked == true)
            {
                labelPrinterLayer.DragMode = PrinterDragMode.Draggable;
            }
        }


    }
}
