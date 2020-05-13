using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;

namespace PrintPreview
{
    /// <summary>
    /// Interaction logic for DataGridPrinterLayerProperty.xaml
    /// </summary>
    public partial class DataGridPrinterLayerProperty : Window
    {
        private DataGridPrinterLayer dataGridPrinterLayer = null;

        public DataGridPrinterLayerProperty(DataGridPrinterLayer dataGridPrinterLayer)
        {
            InitializeComponent();

            this.dataGridPrinterLayer = dataGridPrinterLayer;
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
            dv.DataSource = dataGridPrinterLayer.DataTable;

            foreach (DataColumn column in dataGridPrinterLayer.DataTable.Columns)
            {
                cbxColumns.Items.Add(column.ColumnName);
            }

            bool knownSize = false;
            lbFontSample.FontSize = dataGridPrinterLayer.TextFont.Size;
            for (int i = 0; i < cbxFontSize.Items.Count; i++)
            {
                if (dataGridPrinterLayer.TextFont.Size.ToString() == ((ComboBoxItem)cbxFontSize.Items[i]).Content.ToString())
                {
                    knownSize = true;
                    cbxFontSize.SelectedIndex = i;
                    break;
                }
            }
            // If the current size is not in the list, add one
            if (!knownSize)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = dataGridPrinterLayer.TextFont.Size;
                cbxFontSize.Items.Add(item);
                cbxFontSize.SelectedIndex = cbxFontSize.Items.Count - 1;
            }

            bool knownFontName = false;
            lbFontSample.FontFamily = new FontFamily(dataGridPrinterLayer.TextFont.FontName);
            for (int i = 0; i < cbxFontName.Items.Count; i++)
            {
                if (dataGridPrinterLayer.TextFont.FontName.ToString() == ((ComboBoxItem)cbxFontName.Items[i]).Content.ToString())
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
                item.Content = dataGridPrinterLayer.TextFont.FontName;
                cbxFontName.Items.Add(item);
                cbxFontName.SelectedIndex = cbxFontName.Items.Count - 1;
            }
        }

        private void SetLayerProperties()
        {
            dataGridPrinterLayer.DataTable = (DataTable)dv.DataSource;

            string fontName = cbxFontName.Text;
            float fontSize = float.Parse(cbxFontSize.Text);
            dataGridPrinterLayer.TextFont = new GeoFont(fontName, fontSize);
        }

        private void LoadResize()
        {
            switch (dataGridPrinterLayer.ResizeMode)
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
                dataGridPrinterLayer.ResizeMode = PrinterResizeMode.Resizable;
            }
            else if (rbtnResizeFixed.IsChecked == true)
            {
                dataGridPrinterLayer.ResizeMode = PrinterResizeMode.Fixed;
            }
            else if (rbtnResizeMaintainRaio.IsChecked == true)
            {
                dataGridPrinterLayer.ResizeMode = PrinterResizeMode.MaintainAspectRatio;
            }
        }

        private void LoadDrag()
        {
            switch (dataGridPrinterLayer.DragMode)
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
                dataGridPrinterLayer.DragMode = PrinterDragMode.Fixed;
            }
            else if (rbtnDraggable.IsChecked == true)
            {
                dataGridPrinterLayer.DragMode = PrinterDragMode.Draggable;
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = (DataTable)dv.DataSource;
            string columnName = txtColumnName.Text;
            if (dt.Columns.Contains(columnName))
            {
                MessageBox.Show("Duplicate Column Name");
            }
            else
            {
                dt.Columns.Add(columnName);
                txtColumnName.Text = "";
                cbxColumns.Items.Add(columnName);
            }
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = (DataTable)dv.DataSource;
            string deletingColumnName = cbxColumns.Text;

            if (dt.Columns.Contains(deletingColumnName))
            {
                dt.Columns.Remove(deletingColumnName);
            }

            cbxColumns.Items.Remove(deletingColumnName);
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
    }
}
