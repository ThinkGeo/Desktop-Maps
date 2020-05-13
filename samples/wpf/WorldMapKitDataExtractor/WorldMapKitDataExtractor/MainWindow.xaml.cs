using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;

namespace WorldMapKitDataExtractor
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                EnableUI(false);

                Extractor extractor = new Extractor(txtInputDatabase.Text, txtOutputDatabase.Text);
                extractor.InputDataPrj = Proj4Projection.GetEpsgParametersString(int.Parse(txtInputDatabaseSrid.Text));
                extractor.UpdateStatus = value => { Dispatcher.Invoke(() => { lblStatus.Content = string.Format("Status: {0}", value); }); };
                extractor.PreserveCountryLevelData = chkPreserveCountryLevelData.IsChecked == true;
                Collection<BoundingBoxWithZoomLevels> boundariesFeatures = new Collection<BoundingBoxWithZoomLevels>();

                if(extractor.PreserveCountryLevelData)
                {
                    RectangleShape boundingBox = new RectangleShape();
                    switch(txtInputDatabaseSrid.Text)
                    {
                        case "4326":
                            boundingBox = new RectangleShape(-180, 90, 180, -90);
                            break;
                        case "3857":
                            boundingBox = new RectangleShape(-20026376, 20048966, 20026376, -20048966);
                            break;
                        default:
                            boundingBox = new RectangleShape(0, 0, 0, 0);
                            break;
                    }
                    boundariesFeatures.Add(new BoundingBoxWithZoomLevels(boundingBox, (int)cmbCountryStartZoomLevel.SelectedValue, (int)cmbCountryEndZoomLevel.SelectedValue));
                }

                if (rbtnShapeFile.IsChecked == true)
                {
                    ShapeFileFeatureLayer shapeFileLayer = new ShapeFileFeatureLayer(txtShapeFile.Text);
                    shapeFileLayer.Open();
                    string prjFilePath = Path.ChangeExtension(txtShapeFile.Text, ".prj");
                    if (File.Exists(prjFilePath))
                        extractor.BoundaryPrj = Proj4Projection.ConvertPrjToProj4(File.ReadAllText(prjFilePath));
                    else
                        extractor.BoundaryPrj = Proj4Projection.GetEpsgParametersString(int.Parse(shapeFileSrid.Text));

                    var selectItems = featureTable.SelectedItems.Count == 0 ? featureTable.Items.Cast<DataRowView>() : featureTable.SelectedItems.Cast<DataRowView>();
                    foreach (DataRowView row in selectItems)
                    {
                        boundariesFeatures.Add(new BoundingBoxWithZoomLevels(shapeFileLayer.QueryTools.GetFeatureById(row.Row[0].ToString(), ReturningColumnsType.NoColumns).GetBoundingBox(), 1, 20));
                    }
                }
                else
                {
                    BoundingBoxRow row1 = new BoundingBoxRow(txtUpperLeft1, txtLowerRight1, cmbStartZoomLevel1, cmbEndZoomLevel1, chkEnable1);
                    BoundingBoxRow row2 = new BoundingBoxRow(txtUpperLeft2, txtLowerRight2, cmbStartZoomLevel2, cmbEndZoomLevel2, chkEnable2);
                    BoundingBoxRow row3 = new BoundingBoxRow(txtUpperLeft3, txtLowerRight3, cmbStartZoomLevel3, cmbEndZoomLevel3, chkEnable3);
                    BoundingBoxRow row4 = new BoundingBoxRow(txtUpperLeft4, txtLowerRight4, cmbStartZoomLevel4, cmbEndZoomLevel4, chkEnable4);
                    BoundingBoxRow row5 = new BoundingBoxRow(txtUpperLeft5, txtLowerRight5, cmbStartZoomLevel5, cmbEndZoomLevel5, chkEnable5);

                    BoundingBoxRow[] boundingBoxRows = { row1, row2, row3, row4, row5 };

                    extractor.BoundaryPrj = Proj4Projection.GetEpsgParametersString(int.Parse(boundarySrid.Text));

                    foreach (BoundingBoxRow row in boundingBoxRows)
                    {
                        if ((bool)row.chkRowEnabled.IsChecked)
                        {
                            var uppperLeftPoint = row.txtUpperLeftPoint.Text.Split(',').Select(a => double.Parse(a.Trim())).ToArray();
                            var lowerRightPoint = row.txtLowerRightPoint.Text.Split(',').Select(a => double.Parse(a.Trim())).ToArray();

                            RectangleShape boundingBox = new RectangleShape(uppperLeftPoint[0], uppperLeftPoint[1], lowerRightPoint[0], lowerRightPoint[1]);

                            boundariesFeatures.Add(new BoundingBoxWithZoomLevels(boundingBox, (int)row.cmbStartZoomLevel.SelectedValue, (int)row.cmbEndZoomLevel.SelectedValue));
                        }
                    }
                }

                Task.Run(() => extractor.ExtractDataByShape(boundariesFeatures)).ContinueWith(task => EnableUI(true));
            }
        }

        private bool ValidateInput()
        {
            bool result = true;
            StringBuilder errorMessage = new StringBuilder();
            int srid;
            if (!File.Exists(txtInputDatabase.Text))
                errorMessage.AppendLine("Please enter correct input database file path.");

            if (!int.TryParse(txtInputDatabaseSrid.Text, out srid))
                errorMessage.AppendLine("Please enter correct SRID for input database.");

            if (string.IsNullOrEmpty(txtOutputDatabase.Text))
                errorMessage.AppendLine("Please enter correct output database file path.");

            if (rbtnShapeFile.IsChecked == true)
            {
                if (!File.Exists(txtShapeFile.Text))
                    errorMessage.AppendLine("Please enter correct shape file path.");

                if (!File.Exists(Path.ChangeExtension(txtShapeFile.Text, ".prj")) && !int.TryParse(shapeFileSrid.Text, out srid))
                    errorMessage.AppendLine("Please enter correct SRID for boundary shaplefile.");
            }

            if (rbtnBoundingBox.IsChecked == true)
            {
                if(!int.TryParse(boundarySrid.Text, out srid))
                    errorMessage.AppendLine("Please enter correct SRID for boundary.");

                if((bool)chkPreserveCountryLevelData.IsChecked)
                {
                    if((int)cmbCountryStartZoomLevel.SelectedValue > (int)cmbCountryEndZoomLevel.SelectedValue)
                        errorMessage.AppendLine("Invalid ZoomLevel Input for Preserve Country-Level Data.");
                }

                BoundingBoxRow row1 = new BoundingBoxRow(txtUpperLeft1, txtLowerRight1, cmbStartZoomLevel1, cmbEndZoomLevel1, chkEnable1);
                BoundingBoxRow row2 = new BoundingBoxRow(txtUpperLeft2, txtLowerRight2, cmbStartZoomLevel2, cmbEndZoomLevel2, chkEnable2);
                BoundingBoxRow row3 = new BoundingBoxRow(txtUpperLeft3, txtLowerRight3, cmbStartZoomLevel3, cmbEndZoomLevel3, chkEnable3);
                BoundingBoxRow row4 = new BoundingBoxRow(txtUpperLeft4, txtLowerRight4, cmbStartZoomLevel4, cmbEndZoomLevel4, chkEnable4);
                BoundingBoxRow row5 = new BoundingBoxRow(txtUpperLeft5, txtLowerRight5, cmbStartZoomLevel5, cmbEndZoomLevel5, chkEnable5);

                BoundingBoxRow[] boundingBoxRows = { row1, row2, row3, row4, row5 };

                foreach (BoundingBoxRow row in boundingBoxRows)
                {
                    if ((bool)row.chkRowEnabled.IsChecked)
                    {
                        var upperLeftPoint = row.txtUpperLeftPoint.Text.Split(',').Select(a => double.Parse(a.Trim())).ToArray();
                        var lowerRightPoint = row.txtLowerRightPoint.Text.Split(',').Select(a => double.Parse(a.Trim())).ToArray();

                        if(upperLeftPoint[0] > lowerRightPoint[0] || upperLeftPoint[1] < lowerRightPoint[1])
                            errorMessage.AppendLine("Invalid BoundingBox Input.");

                        if ((int)row.cmbStartZoomLevel.SelectedValue > (int)row.cmbEndZoomLevel.SelectedValue || (int)row.cmbStartZoomLevel.SelectedValue < 0 || (int)row.cmbEndZoomLevel.SelectedValue < 0)
                            errorMessage.AppendLine("Invalid ZoomLevel Input.");
                    }
                }

                if(!((bool)chkEnable1.IsChecked || (bool)chkEnable2.IsChecked || (bool)chkEnable3.IsChecked || (bool)chkEnable4.IsChecked || (bool)chkEnable5.IsChecked))
                    errorMessage.AppendLine("Please enable a bounding box.");
            }



            if (!string.IsNullOrEmpty(errorMessage.ToString()))
            {
                System.Windows.MessageBox.Show(errorMessage.ToString(), "Warning");
                result = false;
            }

            return result;
        }

        private void EnableUI(bool enable)
        {
            Dispatcher.Invoke(() =>
            {
                btnOk.IsEnabled = enable;
                btnCancel.IsEnabled = enable;
                btnOutputDatabase.IsEnabled = enable;
                btnOpenShapeFile.IsEnabled = enable;
                btnInputDatabase.IsEnabled = enable;
            });
        }

        private void ExtractorTypeChanged(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (rbtnBoundingBox.IsChecked == true)
                {
                    shapeFilePanel.Visibility = Visibility.Hidden;
                    boundingBoxPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    shapeFilePanel.Visibility = Visibility.Visible;
                    boundingBoxPanel.Visibility = Visibility.Hidden;
                }
            }
        }

        private void btnInputDatabase_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".sqlite";
            dlg.Filter = "SQLite Database (.sqlite)|*.sqlite";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtInputDatabase.Text = dlg.FileName;
        }

        private void btnOutputDatabase_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".sqlite";
            dlg.Filter = "SQLite Database (.sqlite)|*.sqlite";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtOutputDatabase.Text = dlg.FileName;
        }

        private void btnOpenShapeFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".shp";
            dlg.Filter = "Shape File (.shp)|*.shp";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtShapeFile.Text = dlg.FileName;

            ShapeFileFeatureSource shapeFile = new ShapeFileFeatureSource(txtShapeFile.Text);
            shapeFile.Open();
            Collection<Feature> features = shapeFile.GetAllFeatures(ReturningColumnsType.AllColumns);
            shapeFile.Close();

            DataTable dt = new DataTable();
            bool isColumnName = true;

            foreach (var feature in features)
            {
                var columnValues = feature.ColumnValues;
                if (isColumnName)
                {
                    dt.Columns.Add(new DataColumn("FeatureId"));
                    foreach (var column in columnValues.Keys)
                    {
                        dt.Columns.Add(new DataColumn(column));
                    }
                    isColumnName = false;
                }

                DataRow row = dt.NewRow();
                row["FeatureId"] = feature.Id;
                foreach (var item in columnValues)
                {
                    row[item.Key] = item.Value;
                }
                dt.Rows.Add(row);
            }

            featureTable.ItemsSource = dt.DefaultView;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox[] zoomLevelBoxes = { cmbEndZoomLevel1, cmbEndZoomLevel2, cmbEndZoomLevel3, cmbEndZoomLevel4, cmbEndZoomLevel5,
                                                                  cmbStartZoomLevel1, cmbStartZoomLevel2, cmbStartZoomLevel3, cmbStartZoomLevel4, cmbStartZoomLevel5,
                                                                  cmbCountryStartZoomLevel, cmbCountryEndZoomLevel };

            foreach (System.Windows.Controls.ComboBox comboBox in zoomLevelBoxes)
            {
                int[] zoomLevels = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                comboBox.ItemsSource = zoomLevels;
            }

            cmbStartZoomLevel1.SelectedIndex = 7;
            cmbEndZoomLevel1.SelectedIndex = 19;
            cmbCountryStartZoomLevel.SelectedIndex = 0;
            cmbCountryEndZoomLevel.SelectedIndex = 6;
        }
    }
}
