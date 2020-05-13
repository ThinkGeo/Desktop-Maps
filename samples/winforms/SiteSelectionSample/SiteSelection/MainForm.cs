using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.SiteSelection.Properties;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public partial class MainForm : Form
    {
        private static readonly PointShape thinkGeoLocation = new PointShape(-10776838.0796536, 3912346.50475619);

        private bool isLoad;
        private MapModel mapModel;
        private OverlaySwitcher switcher;
        private AreaCreateMode createAreasMode;
        private Collection<QueryResultItem> queryResult;

        public MainForm()
        {
            InitializeComponent();
            InitializeMapModel();
            InitializeBackground();
            InitializeOverlaySwitcher();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (string name in mapModel.GetPoiLayerCandidateNames())
            {
                CandidatePoiLayersComboBox.Items.Add(name);
            }

            CandidatePoiLayersComboBox.SelectedIndex = 2;
            RouteRadioButton.Checked = true;
            PanRadioButton.Checked = true;
            RouteTimeTextBox.Text = Resources.MainForm_MainForm_Load__6;
            DistanceValueTextBox.Text = Resources.MainForm_MainForm_Load__2;
            DistanceUnitComboBox.SelectedIndex = 1;
            isLoad = true;

            ToolTip panToolTip = new ToolTip();
            panToolTip.SetToolTip(PanRadioButton, "Pan the map");
            ToolTip pointToolTip = new ToolTip();
            pointToolTip.SetToolTip(rbtnDrawPoint, "Highlight a country");
            ToolTip clearToolTip = new ToolTip();
            clearToolTip.SetToolTip(RadioButtonClear, "Remove pin and selection");

            UpdateQueryResult();
        }

        private void MapModel_PlottedPoint(object sender, PlottedPointChangedMapModelEventArgs e)
        {
            mapModel.PlottedPoint = e.PlottedPoint;
            UpdateQueryResult();
        }

        private void ApplyQueryButton_Click(object sender, EventArgs e)
        {
            UpdateQueryResult();
        }

        private void CandidatePoiLayersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapModel.QueryingFeatureLayer = mapModel.GetPoiFeatureLayerByName(CandidatePoiLayersComboBox.SelectedItem.ToString());
            UpdatePioTypeCandidates();
        }

        private void ControlMapMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            mapModel.TrackMode = PanRadioButton.Checked ? TrackMode.None : TrackMode.Point;
            radioButton.FlatAppearance.BorderColor = radioButton.Checked ? Color.FromArgb(136, 136, 136) : Color.White;
        }

        private void ClearRadioButton_Click(object sender, EventArgs e)
        {
            PanRadioButton.Checked = true;
            RadioButtonClear.Checked = false;
            QueryResultItemsDataGridView.Visible = false;

            mapModel.ServiceAreaLayer.Clear();
            mapModel.ClearPoiMarkers();
            mapModel.MapControl.Refresh();
        }

        private void LeftSideBarPanel_PanelCollapseButtonClick(object sender, EventArgs e)
        {
            map.Width = Width - leftSideBarPanel.Width - 15;
            map.Left = leftSideBarPanel.Width;
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            PointShape mouseLocation = ExtentHelper.ToWorldCoordinate(map.CurrentExtent, new ScreenPointF(e.X, e.Y), map.Width, map.Height);
            FooterLocationXToolStripStatusLabel.Text = string.Format(CultureInfo.InvariantCulture, "X:{0:0.##}", mouseLocation.X);
            FooterLocationYToolStripStatusLabel.Text = string.Format(CultureInfo.InvariantCulture, "Y:{0:0.##}", mouseLocation.Y);
            stpFooter.Refresh();
        }

        private static void Switcher_OverlaySwitched(object sender, OverlayChangedOverlaySwitcherEventArgs e)
        {
            BingMapsOverlay bingMapsOverlay = e.Overlay as BingMapsOverlay;
            if (bingMapsOverlay != null)
            {
                string applicationId = MapSuiteSampleHelper.GetBingMapsApplicationId(bingMapsOverlay.MapType);

                if (!string.IsNullOrEmpty(applicationId)) bingMapsOverlay.ApplicationId = applicationId;
                else e.Cancel = true;
            }
        }

        private void AreaTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            BufferPanel.Visible = false;
            RoutePanel.Visible = false;

            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                if (radioButton == RouteRadioButton)
                {
                    createAreasMode = AreaCreateMode.Route;
                    RouteRadioButton.Location = new Point(5, 12);
                    BufferRadioButton.Location = new Point(5, 80);

                    RoutePanel.Location = new Point(21, 42);
                    RoutePanel.Visible = true;
                }
                else
                {
                    createAreasMode = AreaCreateMode.Buffer;
                    RouteRadioButton.Location = new Point(5, 12);
                    BufferRadioButton.Location = new Point(5, 46);

                    BufferPanel.Location = new Point(21, BufferRadioButton.Location.Y + BufferRadioButton.Height + 5);
                    BufferPanel.Visible = true;
                }
            }
        }

        private void DistanceValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyChar = e.KeyChar;
            if ((keyChar < 48 || keyChar > 57) && keyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void DistanceValueTextBox_TextChanged(object sender, EventArgs e)
        {
            double value;
            bool isValueDouble = double.TryParse(DistanceValueTextBox.Text, out value);

            if (isValueDouble && value < 1)
            {
                DistanceValueTextBox.Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void QueryResultItemsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return;

            DataGridViewCell cell = QueryResultItemsDataGridView.Rows[e.RowIndex].Cells["Location"];
            if (cell.Value != null)
            {
                mapModel.MapControl.CurrentExtent = BaseShape.CreateShapeFromWellKnownData((string)cell.Value).GetBoundingBox();
                mapModel.MapControl.Refresh();
            }
        }

        private void OverlayVisiblePictureBox_Click(object sender, EventArgs e)
        {
            PictureBox tempPictureBox = (PictureBox)sender;
            tempPictureBox.Cursor = Cursors.Hand;
            if (switcher != null)
            {
                if (switcher.Visible)
                {
                    switcher.Visible = false;
                    tempPictureBox.Image = Resources.switcher_maxmize;
                }
                else
                {
                    switcher.Visible = true;
                    tempPictureBox.Image = Resources.switcher_minimize;
                }
            }
        }

        private void QueryResultItemsDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = QueryResultItemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    cell.Style.BackColor = Color.LightBlue;
                }
            }
        }

        private void QueryResultItemsDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = QueryResultItemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    cell.Style.BackColor = Color.White;
                }
            }
        }

        private void RouteTimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyChar = e.KeyChar;
            if (keyChar == 46)
            {
                if (DistanceValueTextBox.Text.Length <= 0)
                {
                    e.Handled = true;
                }
                else
                {
                    double value;
                    double oldValue;
                    bool isOldValueDouble = double.TryParse(DistanceValueTextBox.Text, out oldValue);
                    bool isNewValueDouble = double.TryParse(DistanceValueTextBox.Text + e.KeyChar.ToString(CultureInfo.InvariantCulture), out value);
                    if (isNewValueDouble == false)
                    {
                        e.Handled = isOldValueDouble;
                    }
                }
            }
            else if ((keyChar < 48 || keyChar > 57) && keyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void InitializeMapModel()
        {
            queryResult = new Collection<QueryResultItem>();

            mapModel = new MapModel(map);
            mapModel.PlottedPoint = thinkGeoLocation;
            mapModel.PlottedPointChanged += MapModel_PlottedPoint;
        }

        private void InitializeOverlaySwitcher()
        {
            Collection<Overlay> baseOverlays = new Collection<Overlay>();
            baseOverlays.Add(map.Overlays[Resources.ThinkGeoCloudMapsOverlayLight]);
            baseOverlays.Add(map.Overlays[Resources.ThinkGeoCloudMapsOverlayAerial]);
            baseOverlays.Add(map.Overlays[Resources.ThinkGeoCloudMapsOverlayHybrid]);
            baseOverlays.Add(map.Overlays[Resources.OSMOverlayName]);
            baseOverlays.Add(map.Overlays[Resources.BingMapsAerialOverlayName]);
            baseOverlays.Add(map.Overlays[Resources.BingMapsRoadOverlayName]);

            switcher = new OverlaySwitcher(baseOverlays, map);
            switcher.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            switcher.Location = new Point(Width - 238, 55);
            switcher.Size = new Size(220, 128);
            switcher.OverlayChanged += Switcher_OverlaySwitched;
            Controls.Add(switcher);
            switcher.BringToFront();

            PictureBox overlayVisiblePictureBox = new PictureBox();
            overlayVisiblePictureBox.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            overlayVisiblePictureBox.Image = Resources.switcher_minimize;
            overlayVisiblePictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            overlayVisiblePictureBox.Location = new Point(switcher.Location.X + switcher.Width - overlayVisiblePictureBox.Width - 8, switcher.Location.Y + 5);
            overlayVisiblePictureBox.Click += OverlayVisiblePictureBox_Click;
            Controls.Add(overlayVisiblePictureBox);
            overlayVisiblePictureBox.BringToFront();
            switcher.Refresh();
        }

        private void InitializeBackground()
        {
            Bitmap buildingTypeBitmap = new Bitmap(pnlBuildingType.Size.Width, pnlBuildingType.Size.Height);
            using (Graphics graphics = Graphics.FromImage(buildingTypeBitmap))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                Rectangle rect = new Rectangle(0, 0, buildingTypeBitmap.Width - 1, buildingTypeBitmap.Height - 1);
                GraphicsPath path = MapSuiteSampleHelper.CreateRoundPath(rect, 10);
                graphics.DrawPath(new Pen(Color.FromArgb(255, 200, 200, 200), 1), path);
            }
            pnlBuildingType.BackgroundImage = buildingTypeBitmap;

            Bitmap controlModeBitmap = new Bitmap(pnlControlMode.Size.Width, pnlControlMode.Size.Height);
            using (Graphics graphics = Graphics.FromImage(controlModeBitmap))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                Rectangle rect = new Rectangle(0, 0, controlModeBitmap.Width - 1, controlModeBitmap.Height - 1);
                GraphicsPath path = MapSuiteSampleHelper.CreateRoundPath(rect, 10);
                graphics.DrawPath(new Pen(Color.FromArgb(255, 200, 200, 200), 1), path);
            }
            pnlControlMode.BackgroundImage = controlModeBitmap;

            Bitmap areaTypeBitmap = new Bitmap(AreaTypePanel.Size.Width, AreaTypePanel.Size.Height);
            using (Graphics graphics = Graphics.FromImage(areaTypeBitmap))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                Rectangle rect = new Rectangle(0, 0, areaTypeBitmap.Width - 1, areaTypeBitmap.Height - 1);
                GraphicsPath path = MapSuiteSampleHelper.CreateRoundPath(rect, 10);
                graphics.DrawPath(new Pen(Color.FromArgb(255, 200, 200, 200), 1), path);
            }
            AreaTypePanel.BackgroundImage = areaTypeBitmap;
        }

        private void UpdatePioTypeCandidates()
        {
            CandidatePoiTypesComboBox.Items.Clear();
            CandidatePoiTypesComboBox.Items.Add(Resources.FindAllText);
            foreach (var candidateColumnValue in mapModel.GetColumnValueCandidates())
            {
                CandidatePoiTypesComboBox.Items.Add(candidateColumnValue);
            }

            CandidatePoiTypesComboBox.SelectedIndex = 0;
        }

        private void UpdateQueryResultDataGrid(IEnumerable<Feature> queryResults)
        {
            QueryResultItemsDataGridView.Visible = true;
            QueryResultItemsDataGridView.Rows.Clear();
            foreach (QueryResultItem item in queryResults.Select(f => new QueryResultItem(f)))
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Cells.Add(item.ImageButtonCell);
                newRow.Cells.Add(item.TextCell);
                newRow.Cells.Add(item.LocationCell);
                QueryResultItemsDataGridView.Rows.Add(newRow);
            }
        }

        private void UpdateQueryResult()
        {
            if (!isLoad) return;

            mapModel.QueryingFeatureLayer.Open();
            mapModel.ServiceAreaLayer.Open();

            Dictionary<string, object> parameters = GetServiceAreaAnalysisParameters();
            ServiceAreaAnalysis serviceAreaAnalysis = MapSuiteSampleHelper.GetAreaAnalysis(createAreasMode);
            Feature serviceAreaResult = serviceAreaAnalysis.CreateServiceAreaFeature(mapModel.PlottedPoint, mapModel.MapControl.MapUnit, parameters);

            mapModel.ServiceAreaLayer.Clear();
            mapModel.ServiceAreaLayer.InternalFeatures.Add(serviceAreaResult);

            if (mapModel.ServiceAreaLayer.InternalFeatures.Count > 0)
            {
                queryResult.Clear();
                Collection<Feature> queriedFeatures = GetQueryResultFeatures();
                UpdateQueryResultDataGrid(queriedFeatures);

                mapModel.AddMarkersByFeatures(queriedFeatures);
                mapModel.MapControl.CurrentExtent = mapModel.ServiceAreaLayer.GetBoundingBox();
                mapModel.MapControl.Refresh();
            }
        }

        private IEnumerable<Feature> GetFiltedHotelFeatures(IEnumerable<Feature> hotelFeatures)
        {
            Collection<Feature> filteredHotelFeatures = new Collection<Feature>();
            string property = ((string)CandidatePoiTypesComboBox.SelectedItem).Trim();
            string[] values = property.Split('~');
            double min;
            double max;
            if (values.Length == 2)
            {
                double.TryParse(values[0], out min);
                double.TryParse(values[1], out max);
            }
            else 
            {
                double.TryParse(values[0].Substring(2), out min);
                max = double.MaxValue;
            }
          

            foreach (Feature feature in hotelFeatures)
            {
                double columnValue;
                if (double.TryParse(feature.ColumnValues["ROOMS"], out columnValue) && columnValue >= min && columnValue < max)
                {
                    filteredHotelFeatures.Add(feature);
                }
                else if (string.IsNullOrEmpty(feature.ColumnValues["ROOMS"]))
                {
                    filteredHotelFeatures.Add(feature);
                }
            }

            return filteredHotelFeatures;
        }

        private Dictionary<string, object> GetServiceAreaAnalysisParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            switch (createAreasMode)
            {
                case AreaCreateMode.Buffer:
                    parameters[Resources.DistanceUnitKey] = DistanceUnitComboBox.SelectedIndex == 1 ? DistanceUnit.Mile : DistanceUnit.Kilometer;

                    double distance;
                    if (double.TryParse(DistanceValueTextBox.Text, out distance))
                    {
                        parameters[Resources.DistanceKey] = distance;
                    }
                    break;

                case AreaCreateMode.Route:
                    parameters[Resources.StreetShapePathFilenameKey] =
                        ConfigurationManager.AppSettings["StreetShapeFilePathName"];

                    int driveTimeInMinutes;
                    if (int.TryParse(RouteTimeTextBox.Text, out driveTimeInMinutes))
                    {
                        parameters[Resources.DriveTimeInMinutesKey] = driveTimeInMinutes;
                    }
                    break;
            }
            return parameters;
        }

        private Collection<Feature> GetQueryResultFeatures()
        {
            Collection<Feature> queryResultFeatures = new Collection<Feature>();
            if (mapModel.ServiceAreaLayer.InternalFeatures.Count == 0) return queryResultFeatures;

            BaseShape serviceAreaShape = mapModel.ServiceAreaLayer.InternalFeatures[0].GetShape();
            Collection<Feature> selectedFeatures = mapModel.QueryingFeatureLayer.QueryTools.GetFeaturesWithin(serviceAreaShape, ReturningColumnsType.AllColumns);

            if (CandidatePoiTypesComboBox.SelectedItem.ToString().Equals(Resources.FindAllText, StringComparison.OrdinalIgnoreCase))
            {
                foreach (Feature feature in selectedFeatures)
                {
                    queryResultFeatures.Add(feature);
                }
            }
            else if (mapModel.QueryingFeatureLayer != mapModel.HotelsLayer)
            {
                string columnName = MapSuiteSampleHelper.GetDefaultColumnNameByPoiType(mapModel.QueryingFeatureLayer.Name);
                foreach (Feature feature in selectedFeatures)
                {
                    if (feature.ColumnValues[columnName] == CandidatePoiTypesComboBox.SelectedItem.ToString()
                        || string.IsNullOrEmpty(feature.ColumnValues[columnName]))
                    {
                        queryResultFeatures.Add(feature);
                    }
                }
            }
            else
            {
                IEnumerable<Feature> filteredHotelItems = GetFiltedHotelFeatures(selectedFeatures);
                foreach (var feature in filteredHotelItems)
                {
                    queryResultFeatures.Add(feature);
                }
            }

            return queryResultFeatures;
        }
    }
}