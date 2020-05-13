/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.VehicleTracking.Properties;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public partial class MainForm : Form
    {
        private static readonly DateTime applicationStartTime = DateTime.Now;
        private static readonly DateTime adjustedStartTime = new DateTime(2009, 7, 10, 11, 31, 0);
        private static readonly string databasePathFilename = Path.GetFullPath(@"..\..\App_Data\VehicleTrackingDb.mdb");
        private static readonly RectangleShape defaultExtent = new RectangleShape(-10785308.531782, 3915876.77825787, -10778644.195048, 3911615.42458998);

        private Timer timer;
        private Point originalPoint;
        private OverlaySwitcher overlaySwitcher;
        private PictureBox overlaySwitcherButton;
        private UnitSystem measureUnitSystem;

        public MainForm()
        {
            InitializeComponent();
            InitializeHeaderPanel();
            InitializeBackground();
            DrawLeftPanelShadow();
            InitializeAutoRefreshTimer();

            originalPoint = CollapsPictureBox.Location;
            MeasureUnitComboBox.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeMap();

            UpdateSpatialFencesAndVehicles();
        }

        private void AutoRefreshButton_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                timer.Enabled = true;
                timer.Start();
                AutoRefreshStatusLabel.Text = Resources.OnText;
                AutoRefreshStatusLabel.Font = new Font(AutoRefreshStatusLabel.Font.FontFamily, AutoRefreshStatusLabel.Font.Size, FontStyle.Regular);
                AutoRefreshStatusLabel.ForeColor = Color.Black;
                AutoRefreshRadioButton.Checked = true;
            }
            else
            {
                timer.Enabled = false;
                timer.Stop();
                AutoRefreshStatusLabel.Text = Resources.OffText;
                AutoRefreshStatusLabel.Font = new Font(AutoRefreshStatusLabel.Font.FontFamily, AutoRefreshStatusLabel.Font.Size, FontStyle.Bold);
                AutoRefreshStatusLabel.ForeColor = Color.Red;
                AutoRefreshRadioButton.Checked = false;
            }
        }

        private void OverlaySwitcherPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox switcherPictureBox = (PictureBox)sender;
            overlaySwitcher.Visible = !overlaySwitcher.Visible;
            switcherPictureBox.Image = switcherPictureBox.Visible ? Resources.switcher_minimize : Resources.switcher_maxmize;
        }

        private void OverlaySwitcher_OverlayChanged(object sender, OverlayChangedOverlaySwitcherEventArgs e)
        {
            // the bing maps requires application Id,
            // we need to prompt a dialog to allow to input it before using it.
            BingMapsOverlay bingMapsOverlay = e.Overlay as BingMapsOverlay;
            if (bingMapsOverlay != null)
            {
                e.Cancel = ApplyBingMapsKey();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            AutoRefreshRadioButton.Checked = timer.Enabled;
            RefreshRadioButton.Checked = false;
            UpdateSpatialFencesAndVehicles();
        }

        private void WinformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            PointShape currentPoint = ExtentHelper.ToWorldCoordinate(winformsMap.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap.Width, winformsMap.Height);
            LocationYToolStripStatusLabel.Text = string.Format("Y:{0:N2}", currentPoint.Y);
            LocationXToolStripStatusLabel.Text = string.Format("X:{0:N2}", currentPoint.X);
        }

        private void VehicleNameLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            ZoomToCurrentVehicle(linkLabel.Text);
        }

        private void VehiclePictureBox_Click(object sender, EventArgs e)
        {
            if (VehiclePictureBox.DataBindings.Count > 0)
            {
                Image currentImage = ((PictureBox)sender).Image;
                Collection<Vehicle> dataSource = VehiclePictureBox.DataBindings[0].DataSource as Collection<Vehicle>;
                if (dataSource != null && currentImage != null)
                {
                    Vehicle vehicle = dataSource.FirstOrDefault(d => d.Icon == currentImage);
                    ZoomToCurrentVehicle(vehicle.Name);
                }
            }
        }

        private void RefreshRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                radioButton.FlatAppearance.BorderColor = radioButton.Checked ? Color.FromArgb(136, 136, 136) : Color.White;
            }
        }

        private void ZoomToCurrentVehicle(string vehicleName)
        {
            LayerOverlay traceOverlay = (LayerOverlay)winformsMap.Overlays["TraceOverlay"];
            InMemoryFeatureLayer vehicleFeatureLayer = traceOverlay.Layers[vehicleName] as InMemoryFeatureLayer;
            if (vehicleFeatureLayer != null)
            {
                winformsMap.ZoomIntoCenter(60, vehicleFeatureLayer.InternalFeatures[vehicleFeatureLayer.InternalFeatures.Count - 1]);
                winformsMap.Refresh();
            }
        }

        private void InitializeMap()
        {
            // Setup the map.
            winformsMap.MapUnit = GeographyUnit.Meter;
            winformsMap.BackColor = Color.FromArgb(255, 242, 239, 232);
            winformsMap.CurrentExtent = defaultExtent;
            winformsMap.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;

            // Add the various overlays such as vehicles, fences etc.
            SetupBaseOverlays();

            Collection<Overlay> baseOverlays = new Collection<Overlay>();
            baseOverlays.Add(winformsMap.Overlays[Resources.ThinkGeoCloudMapsLight]);
            baseOverlays.Add(winformsMap.Overlays[Resources.ThinkGeoCloudMapsAerial]);
            baseOverlays.Add(winformsMap.Overlays[Resources.ThinkGeoCloudMapsHybrid]);
            baseOverlays.Add(winformsMap.Overlays[Resources.OsmOverlayKey]);
            baseOverlays.Add(winformsMap.Overlays[Resources.BingAerialOverlayKey]);
            baseOverlays.Add(winformsMap.Overlays[Resources.BingRoadOverlayKey]);

            overlaySwitcher = new OverlaySwitcher(baseOverlays, winformsMap);
            overlaySwitcher.Location = new Point(winformsMap.Width - overlaySwitcher.Width - 18, 10);
            overlaySwitcher.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            overlaySwitcher.Cursor = Cursors.Hand;
            overlaySwitcher.OverlayChanged += OverlaySwitcher_OverlayChanged;

            overlaySwitcherButton = new PictureBox();
            overlaySwitcherButton.Image = Resources.switcher_minimize;
            overlaySwitcherButton.SizeMode = PictureBoxSizeMode.AutoSize;
            overlaySwitcherButton.Location = new Point(winformsMap.Width - overlaySwitcherButton.Width - 23, 15);
            overlaySwitcherButton.Click += OverlaySwitcherPictureBox_Click;

            winformsMap.Controls.Add(overlaySwitcherButton);
            winformsMap.Controls.Add(overlaySwitcher);
        }

        private void InitializeAutoRefreshTimer()
        {
            timer = new Timer();
            timer.Enabled = false;
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
        }

        private void UpdateSpatialFencesAndVehicles()
        {
            LayerOverlay traceOverlay = (LayerOverlay)winformsMap.Overlays["TraceOverlay"];
            LayerOverlay spatialFenceOverlay = (LayerOverlay)winformsMap.Overlays["SpatialFenceOverlay"];
            Task.Factory.StartNew((() =>
            {
                lock (spatialFenceOverlay)
                {
                    // Update the fences from database.
                    UpdateSpatialFences(spatialFenceOverlay);
                }
                lock (traceOverlay)
                {
                    // Update the vehicle locations on the map.
                    DateTime currentTime = GetAdjustedCurrentDateTime();
                    UpdateVehicles(traceOverlay, spatialFenceOverlay, currentTime);
                }
                winformsMap.BeginInvoke(new Action(() => winformsMap.Refresh()));
            }));
        }

        private static void UpdateSpatialFences(LayerOverlay spatialFenceOverlay)
        {
            // Get the spatial fence overlay and layer and then clear them
            InMemoryFeatureLayer spatialFenceLayer = (InMemoryFeatureLayer)spatialFenceOverlay.Layers["SpatialFenceLayer"];
            spatialFenceLayer.Clear();

            // Get the spatial fences from the database
            Collection<Feature> spatialFences;

            using (TrackingAccessProvider vehicleProvider = new TrackingAccessProvider(databasePathFilename))
            {
                spatialFences = vehicleProvider.GetSpatialFences();
            }

            // Insert fences from database into fence layer
            if (spatialFences != null)
            {
                foreach (Feature spatialFence in spatialFences)
                {
                    spatialFence.ColumnValues["Restricted"] = "Restricted";
                    spatialFenceLayer.InternalFeatures.Add(spatialFence);
                }
            }
        }

        // Here we update the vehicles from the database.  In this sample we create our data provider each time and
        // when we are finsihed with it we dispose it.  This is very safe however you may get better performance
        // if you were to cache this.  We wanted to sample to be clean and safe and it is up to your expertise to
        // enhance it further
        private void UpdateVehicles(LayerOverlay traceOverlay, LayerOverlay spatialFenceOverlay, DateTime currentTime)
        {
            Dictionary<int, Vehicle> currentVehicles;

            using (TrackingAccessProvider vehicleProvider = new TrackingAccessProvider(databasePathFilename))
            {
                currentVehicles = vehicleProvider.GetCurrentVehicles(currentTime);
            }

            // Loop through all the vehicle to add the history points
            if (currentVehicles != null && currentVehicles.All(v => !string.IsNullOrEmpty(v.Value.IconPath)))
            {
                foreach (int vehicleId in currentVehicles.Keys)
                {
                    Vehicle currentVehicle = currentVehicles[vehicleId];
                    currentVehicle.RestrictedAreaText = CheckIsInSpatialFence(currentVehicle, spatialFenceOverlay) ? "In restricted area" : "Out of restricted area";
                    InMemoryFeatureLayer currentVehicleLayer;

                    // Initialize vehicle overlay if it's not initialized
                    if (!traceOverlay.Layers.Contains(currentVehicle.Name))
                    {
                        // Create an InMemoryMarkerOverlay for the vehicle to hold the points and current location
                        currentVehicleLayer = new InMemoryFeatureLayer();
                        currentVehicleLayer.Open();
                        currentVehicleLayer.Name = currentVehicle.Id.ToString(CultureInfo.InvariantCulture);
                        currentVehicleLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(GetValueStyle(currentVehicle.IconPath));
                        currentVehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                        // Add all the required columns so we can populate later
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("IsCurrentPosition"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Speed"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("DateTime"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Longitude"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Latitude"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("VehicleName"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Duration"));
                        traceOverlay.Layers.Add(currentVehicle.Name, currentVehicleLayer);
                    }
                    else
                    {
                        // Find the overlay in the map
                        currentVehicleLayer = (InMemoryFeatureLayer)traceOverlay.Layers[currentVehicle.Name];
                    }

                    // Clear old vehicle's old positions
                    currentVehicleLayer.Clear();

                    // Add the vheicle's bread crumbs
                    foreach (Location historyLocation in currentVehicle.HistoryLocations.Take(6))
                    {
                        Feature breadcrumbFeature = new Feature(historyLocation.GetLocation().GetWellKnownBinary(), currentVehicle.Name + historyLocation.DateTime.ToString(CultureInfo.InvariantCulture));
                        breadcrumbFeature.ColumnValues["DateTime"] = historyLocation.DateTime.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["IsCurrentPosition"] = "IsNotCurrentPosition";
                        breadcrumbFeature.ColumnValues["Speed"] = historyLocation.Speed.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["Longitude"] = historyLocation.Longitude.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["Latitude"] = historyLocation.Latitude.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["VehicleName"] = currentVehicle.Name;
                        breadcrumbFeature.ColumnValues["Duration"] = currentVehicle.GetSpeedDuration().ToString(CultureInfo.InvariantCulture);
                        currentVehicleLayer.InternalFeatures.Add(breadcrumbFeature.Id, breadcrumbFeature);
                    }

                    // Add the vehicle's latest position
                    Feature latestPositionFeature = new Feature(currentVehicle.Location.GetLocation().GetWellKnownBinary(), currentVehicle.Name);
                    latestPositionFeature.ColumnValues["DateTime"] = currentVehicle.Location.DateTime.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["IsCurrentPosition"] = "IsCurrentPosition";
                    latestPositionFeature.ColumnValues["Speed"] = currentVehicle.Location.Speed.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["Longitude"] = currentVehicle.Location.Longitude.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["Latitude"] = currentVehicle.Location.Latitude.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["VehicleName"] = currentVehicle.Name;
                    latestPositionFeature.ColumnValues["Duration"] = currentVehicle.GetSpeedDuration().ToString(CultureInfo.InvariantCulture);
                    currentVehicleLayer.InternalFeatures.Add(latestPositionFeature.Id, latestPositionFeature);
                }
            }
            testPanel.BeginInvoke(new Action(() =>
            {
                if (currentVehicles != null)
                {
                    if (testPanel.Controls.Count == 0)
                    {
                        int index = 0;
                        foreach (var currentVehicle in currentVehicles)
                        {
                            VehicleStatus vehicleStatus = new VehicleStatus(currentVehicle.Value);
                            vehicleStatus.VehicleClick += (s, e) => ZoomToCurrentVehicle(e.VehicleName);
                            vehicleStatus.Location = new Point(3, 5 + index++ * vehicleStatus.Height);
                            testPanel.Controls.Add(vehicleStatus);
                        }
                    }
                    else if (testPanel.Controls.Count == currentVehicles.Count)
                    {
                        int index = 0;
                        foreach (var currentVehicle in currentVehicles)
                        {
                            ((VehicleStatus)testPanel.Controls[index++]).LoadStatus(currentVehicle.Value);
                        }
                    }
                }
            }));
        }

        private bool ApplyBingMapsKey()
        {
            bool cancel = false;
            string bingMapsKey = MapSuiteSampleHelper.GetBingMapsApplicationId();
            if (!string.IsNullOrEmpty(bingMapsKey))
            {
                foreach (BingMapsOverlay bingOverlay in winformsMap.Overlays.OfType<BingMapsOverlay>())
                {
                    bingOverlay.ApplicationId = bingMapsKey;
                }
            }
            else
            {
                cancel = true;
            }

            return cancel;
        }

        private static DateTime GetAdjustedCurrentDateTime()
        {
            // This vehicle tracking sample contains some simulated data
            // This method stores the real time when the application started and reflects to the start sample time
            // When the actual time increments 1 second, the sample time increments 6 seconds
            //
            // To make the application run in real time just have this method return to current date time
            double realSpentTime = TimeSpan.FromTicks(DateTime.Now.Ticks - applicationStartTime.Ticks).TotalSeconds;
            int sampleSpentTime = (int)(realSpentTime * 12);
            DateTime currentSampleTime = adjustedStartTime.AddSeconds(sampleSpentTime);

            return currentSampleTime;
        }

        private static ValueStyle GetValueStyle(string vehicleIconVirtualPath)
        {
            MemoryStream vehicleStream = new MemoryStream();
            Bitmap bitmap = MapSuiteSampleHelper.GetVehicleBitmap(vehicleIconVirtualPath);
            bitmap.Save(vehicleStream, ImageFormat.Png);
            vehicleStream.Seek(0, SeekOrigin.Begin);

            MemoryStream historyStream = new MemoryStream();
            Resources.ball_green.Save(historyStream, ImageFormat.Png);

            historyStream.Seek(0, SeekOrigin.Begin);
            GeoImage vehicleImage = new GeoImage(vehicleStream);
            GeoImage historyImage = new GeoImage(historyStream);

            ValueStyle iconValueStyle = new ValueStyle();
            iconValueStyle.ColumnName = "IsCurrentPosition";
            iconValueStyle.ValueItems.Add(new ValueItem("IsCurrentPosition", new PointStyle(vehicleImage)));
            iconValueStyle.ValueItems.Add(new ValueItem("IsNotCurrentPosition", new PointStyle(historyImage)));
            return iconValueStyle;
        }

        private static bool CheckIsInSpatialFence(Vehicle vehicle, LayerOverlay spatialFenceOverlay)
        {
            InMemoryFeatureLayer spatialFenceLayer = (InMemoryFeatureLayer)spatialFenceOverlay.Layers["SpatialFenceLayer"];

            // Get the point shape and then check if it is within any of the sptail fences using the QueryTools
            PointShape pointShape = new PointShape(vehicle.Location.Longitude, vehicle.Location.Latitude);
            bool isInSpatialFence = false;
            lock (spatialFenceLayer)
            {
                Collection<Feature> spatialFencesWithin = spatialFenceLayer.QueryTools.GetFeaturesContaining(pointShape, ReturningColumnsType.NoColumns);

                if (spatialFencesWithin.Count > 0)
                {
                    isInSpatialFence = true;
                }
            }
            return isInSpatialFence;
        }

        #region UI

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateSpatialFencesAndVehicles();
        }

        private void TrackOverlay_TrackStarting(object sender, TrackStartingTrackInteractiveOverlayEventArgs e)
        {
            if (winformsMap.TrackOverlay.TrackMode == TrackMode.Line || winformsMap.TrackOverlay.TrackMode == TrackMode.Polygon)
            {
                ClearMeasuresAndPopups();
            }
        }

        private void ClearMeasuresAndPopups()
        {
            foreach (Feature measureFeature in winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Where(f => f.Tag == "Measure").ToList())
            {
                winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove(measureFeature);
            }
            winformsMap.Refresh(winformsMap.TrackOverlay);
            ClearPopups();
        }

        private void ClearPopups()
        {
            List<UserControl> popups = winformsMap.Controls.OfType<UserControl>().Where(u => u.Tag is PointShape).ToList();

            foreach (UserControl popup in popups)
            {
                winformsMap.Controls.Remove(popup);
            }
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            if (winformsMap.TrackOverlay.TrackMode == TrackMode.Polygon)
            {
                PolygonShape polygonShape = e.TrackShape as PolygonShape;
                if (polygonShape != null)
                {
                    double area = -1;
                    string areaUnit = "sq.m.";
                    switch (measureUnitSystem)
                    {
                        case UnitSystem.Metric:
                            areaUnit = "sq.m.";
                            area = polygonShape.GetArea(GeographyUnit.Meter, AreaUnit.SquareMeters);
                            break;

                        case UnitSystem.Imperial:
                            areaUnit = "ac.";
                            area = polygonShape.GetArea(GeographyUnit.Meter, AreaUnit.Acres);
                            break;
                    }

                    if (area > 0)
                    {
                        PointShape point = new PointShape(polygonShape.OuterRing.Vertices[polygonShape.OuterRing.Vertices.Count - 2]);
                        ShowPopup(point, new MeasurementPopup(string.Format("Area: {0} {1}", area.ToString("N1"), areaUnit)));
                    }
                    winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Last().Tag = "Measure";
                }
            }

            if (winformsMap.TrackOverlay.TrackMode == TrackMode.Line)
            {
                string unit = string.Empty;
                LineShape lineShape = e.TrackShape as LineShape;
                if (lineShape != null)
                {
                    double lenth = 0;

                    switch (measureUnitSystem)
                    {
                        case UnitSystem.Imperial:
                            lenth = lineShape.GetLength(GeographyUnit.Meter, DistanceUnit.Feet);
                            unit = "ft";
                            if (lenth >= 5280)
                            {
                                unit = "mi";
                                lenth = Math.Round(lenth / 5280d, 1, MidpointRounding.AwayFromZero);
                            }
                            break;

                        case UnitSystem.Metric:
                            lenth = lineShape.GetLength(GeographyUnit.Meter, DistanceUnit.Meter);
                            unit = "m";
                            if (lenth >= 1000)
                            {
                                unit = "km";
                                lenth = Math.Round(lenth / 1000d, 1, MidpointRounding.AwayFromZero);
                            }
                            break;
                    }

                    string lengthString = lenth.ToString("N1");
                    ShowPopup(new PointShape(lineShape.Vertices[lineShape.Vertices.Count - 1]), new MeasurementPopup(string.Format("Total Length: {0} {1}", lengthString, unit)));
                    winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Last().Tag = "Measure";
                }
            }
        }

        private void ShowPopup(PointShape pointShape, UserControl popupUserControl)
        {
            ScreenPointF screenPointF = ExtentHelper.ToScreenCoordinate(winformsMap.CurrentExtent, pointShape, winformsMap.Width, winformsMap.Height);
            Point position = new Point((int)(screenPointF.X - popupUserControl.Width * 0.5), (int)(screenPointF.Y - popupUserControl.Height));
            popupUserControl.Location = position;
            popupUserControl.Tag = pointShape;
            winformsMap.Controls.Add(popupUserControl);
        }

        private void SetupBaseOverlays()
        {
            string cacheFolder = Path.Combine(Path.GetTempPath(), "TileCache");
            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldMapKitRoadOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitRoadOverlay.Name = Resources.ThinkGeoCloudMapsLight;
            worldMapKitRoadOverlay.IsVisible = true;
            worldMapKitRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsLight);
            worldMapKitRoadOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            winformsMap.Overlays.Add(Resources.ThinkGeoCloudMapsLight, worldMapKitRoadOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldMapKitAerialOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitAerialOverlay.Name = Resources.ThinkGeoCloudMapsAerial;
            worldMapKitAerialOverlay.IsVisible = false;
            worldMapKitAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsAerial);
            worldMapKitAerialOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial;
            winformsMap.Overlays.Add(Resources.ThinkGeoCloudMapsAerial, worldMapKitAerialOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldMapKitAerialWithLabelsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitAerialWithLabelsOverlay.Name = Resources.ThinkGeoCloudMapsHybrid;
            worldMapKitAerialWithLabelsOverlay.IsVisible = false;
            worldMapKitAerialWithLabelsOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsHybrid);
            worldMapKitAerialWithLabelsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid;
            winformsMap.Overlays.Add(Resources.ThinkGeoCloudMapsHybrid, worldMapKitAerialWithLabelsOverlay);

            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            openStreetMapOverlay.Name = Resources.OsmOverlayKey;
            openStreetMapOverlay.IsVisible = false;
            winformsMap.Overlays.Add(Resources.OsmOverlayKey, openStreetMapOverlay);

            BingMapsOverlay bingMapsAerialOverlay = new BingMapsOverlay();
            bingMapsAerialOverlay.Name = Resources.BingAerialOverlayKey;
            bingMapsAerialOverlay.IsVisible = false;
            bingMapsAerialOverlay.MapType = BingMapsMapType.Aerial;
            winformsMap.Overlays.Add(Resources.BingAerialOverlayKey, bingMapsAerialOverlay);

            BingMapsOverlay bingMapsRoadOverlay = new BingMapsOverlay();
            bingMapsRoadOverlay.Name = Resources.BingRoadOverlayKey;
            bingMapsRoadOverlay.IsVisible = false;
            bingMapsRoadOverlay.MapType = BingMapsMapType.Road;
            winformsMap.Overlays.Add(Resources.BingRoadOverlayKey, bingMapsRoadOverlay);

            // Initialize SpatialFenceLayer.
            InMemoryFeatureLayer spatialFenceLayer = new InMemoryFeatureLayer();
            spatialFenceLayer.Open();
            spatialFenceLayer.Columns.Add(new FeatureSourceColumn("Restricted", "Charater", 10));
            spatialFenceLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.FromArgb(255, 204, 204, 204), 2), new GeoSolidBrush(GeoColor.FromArgb(112, 255, 0, 0)));
            spatialFenceLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("Restricted", "Arial", 12, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.SimpleColors.White, 2);
            spatialFenceLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Initialize SpatialFenceOverlay.
            LayerOverlay spatialFenceOverlay = new LayerOverlay();
            spatialFenceOverlay.Name = "SpatialFenceOverlay";
            spatialFenceOverlay.Layers.Add("SpatialFenceLayer", spatialFenceLayer);
            LayerOverlay traceOverlay = new LayerOverlay();
            traceOverlay.Name = "TraceOverlay";

            winformsMap.Overlays.Add("SpatialFenceOverlay", spatialFenceOverlay);
            winformsMap.Overlays.Add("TraceOverlay", traceOverlay);
            ScaleBarAdornmentLayer scaleBarAdornmentLayer = new ScaleBarAdornmentLayer();
            scaleBarAdornmentLayer.UnitFamily = measureUnitSystem;
            winformsMap.AdornmentOverlay.Layers.Add("ScaleBar", scaleBarAdornmentLayer);
        }

        private void ControlMap_Click(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                winformsMap.TrackOverlay.TrackMode = TrackMode.None;
                measurePanel.Visible = false;
                editPanel.Visible = false;
                panel1.Top = 223;
                panel1.Left = 7;
                panel1.Refresh();
                VehiclesGroupPictureBox.Top = 260;
                VehiclesGroupPictureBox.Left = 7;
                dataRepeater.Top = 265;
                VehiclesGroupPictureBox.Refresh();
            }
        }

        private void DrawFence_Click(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                winformsMap.TrackOverlay.TrackEnded -= TrackOverlay_TrackEnded;
                winformsMap.TrackOverlay.TrackMode = TrackMode.Polygon;
                measurePanel.Visible = false;
                editPanel.Visible = true;
                MoveVehicleStatusList();
            }
        }

        private void MoveVehicleStatusList()
        {
            panel1.Top = 260;
            panel1.Left = 7;
            panel1.Refresh();
            VehiclesGroupPictureBox.Top = 294;
            VehiclesGroupPictureBox.Left = 7;
            dataRepeater.Top = 299;
            VehiclesGroupPictureBox.Refresh();
        }

        private void MeasureDistance_Click(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                winformsMap.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
                if (winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Count > 0)
                {
                    SaveSpatialFences();
                }
                else if (winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Count > 0)
                {
                    CancelEdit();
                }

                winformsMap.TrackOverlay.TrackMode = TrackMode.Line;
                measurePanel.Visible = true;
                editPanel.Visible = false;
                winformsMap.TrackOverlay.TrackMode = MeasureLineRadioButton.Checked ? TrackMode.Line : TrackMode.Polygon;
                winformsMap.TrackOverlay.TrackStarting -= TrackOverlay_TrackStarting;
                winformsMap.TrackOverlay.TrackStarting += TrackOverlay_TrackStarting;
                winformsMap.TrackOverlay.TrackEnded -= TrackOverlay_TrackEnded;
                winformsMap.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;
                MoveVehicleStatusList();
            }
        }

        private void MeasureLineClick(object sender, EventArgs e)
        {
            winformsMap.TrackOverlay.TrackMode = TrackMode.Line;
        }

        private void MeasurePolygonClick(object sender, EventArgs e)
        {
            winformsMap.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void CancelMeasureClick(object sender, EventArgs e)
        {
            rbtnCancelMeasure.Checked = false;
            ClearMeasuresAndPopups();
        }

        private RectangleShape GetBufferedRectangle(PointShape pointShape)
        {
            PointShape clickedPoint = pointShape;
            double currentResolution = winformsMap.CurrentScale / (39.3701 * 96);
            double offset = currentResolution * 8;
            RectangleShape rectangle = new RectangleShape(clickedPoint.X - offset, clickedPoint.Y + offset, clickedPoint.X + offset, clickedPoint.Y - offset);
            return rectangle;
        }

        private void TrackOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            PointShape clickedPoint = new PointShape(e.InteractionArguments.WorldX, e.InteractionArguments.WorldY);
            RectangleShape rect = GetBufferedRectangle(clickedPoint);
            Collection<Feature> intersectingFeatures = winformsMap.TrackOverlay.TrackShapeLayer.QueryTools.GetFeaturesIntersecting(rect, ReturningColumnsType.AllColumns);
            if (intersectingFeatures.Count > 0)
            {
                if (winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Count > 0)
                {
                    foreach (Feature item in winformsMap.EditOverlay.EditShapesLayer.InternalFeatures)
                    {
                        winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(item.Id, item);
                    }
                    winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
                }
                winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove(intersectingFeatures[0].Id);
                winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Add(intersectingFeatures[0]);
                winformsMap.EditOverlay.CalculateAllControlPoints();
                winformsMap.Refresh(new Overlay[] { winformsMap.TrackOverlay, winformsMap.EditOverlay });
            }
        }

        private void DrawNewFenceClick(object sender, EventArgs e)
        {
            DrawNewFence();
        }

        private void DrawNewFence()
        {
            CancelEdit();
            winformsMap.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
            winformsMap.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void EditFenceClick(object sender, EventArgs e)
        {
            ClearMeasuresAndPopups();
            winformsMap.TrackOverlay.TrackMode = TrackMode.None;
            LayerOverlay spatialFenceOverlay = (LayerOverlay)winformsMap.Overlays["SpatialFenceOverlay"];
            InMemoryFeatureLayer spatialFenceLayer = (InMemoryFeatureLayer)spatialFenceOverlay.Layers["SpatialFenceLayer"];

            // Move spatial fences from spatial fence layer to edit overlay for editing
            foreach (Feature feature in spatialFenceLayer.InternalFeatures)
            {
                if (!winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Contains(feature.Id))
                {
                    winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(feature.Id, feature);
                }
                if (!spatialFenceLayer.FeatureIdsToExclude.Contains(feature.Id))
                {
                    spatialFenceLayer.FeatureIdsToExclude.Add(feature.Id);
                }
            }

            winformsMap.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
            winformsMap.TrackOverlay.MapMouseClick += TrackOverlay_MapMouseClick;

            // Clear the spatial fences from spatial fence layer
            //spatialFenceLayer.InternalFeatures.Clear();

            UpdateVehicles((LayerOverlay)winformsMap.Overlays["TraceOverlay"], (LayerOverlay)winformsMap.Overlays["SpatialFenceOverlay"], GetAdjustedCurrentDateTime());
            winformsMap.Refresh(new Overlay[] { spatialFenceOverlay, winformsMap.TrackOverlay });
        }

        private void DeleteFenceClick(object sender, EventArgs e)
        {
            rbtnDeleteFence.Checked = false;
            if (winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Count == 0)
            {
                MessageBox.Show(Resources.ChooseFenceWarning, Resources.VehicleTrackingText);
            }
            else if (winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Count > 0)
            {
                if (MessageBox.Show(Resources.DeleteFenceWarning, Resources.VehicleTrackingText, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    winformsMap.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
                    winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
                    LayerOverlay spatialFenceOverlay = (LayerOverlay)winformsMap.Overlays["SpatialFenceOverlay"];
                    ((InMemoryFeatureLayer)spatialFenceOverlay.Layers["SpatialFenceLayer"]).InternalFeatures.Clear();
                    SaveSpatialFences();
                    UpdateSpatialFencesAndVehicles();
                }
            }
        }

        private void SaveFenceClick(object sender, EventArgs e)
        {
            rbtnSaveFence.Checked = false;
            winformsMap.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
            SaveSpatialFences();

            UpdateSpatialFencesAndVehicles();

            if (EditFenceRadioButton.Checked)
            {
                EditFenceRadioButton.Checked = false;
                DrawNewFenceRadioButton.Checked = true;
                DrawNewFence();
            }
        }

        private void SaveSpatialFences()
        {
            TrackingAccessProvider vehicleProvider = null;

            try
            {
                LayerOverlay spatialFenceOverlay = (LayerOverlay)winformsMap.Overlays["SpatialFenceOverlay"];
                InMemoryFeatureLayer spatialFenceLayer = (InMemoryFeatureLayer)spatialFenceOverlay.Layers["SpatialFenceLayer"];
                foreach (string item in spatialFenceLayer.FeatureIdsToExclude)
                {
                    if (spatialFenceLayer.InternalFeatures.Contains(item))
                    {
                        spatialFenceLayer.InternalFeatures.Remove(item);
                    }
                }
                spatialFenceLayer.FeatureIdsToExclude.Clear();
                // Save the new spatial fences from edit overlay into the spatial fence layer
                foreach (Feature feature in winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Where(f => (string)f.Tag != "Measure"))
                {
                    if (!spatialFenceLayer.InternalFeatures.Contains(feature.Id))
                    {
                        spatialFenceLayer.InternalFeatures.Add(feature.Id, feature);
                    }
                }

                foreach (Feature feature in winformsMap.EditOverlay.EditShapesLayer.InternalFeatures)
                {
                    if (!spatialFenceLayer.InternalFeatures.Contains(feature.Id))
                    {
                        spatialFenceLayer.InternalFeatures.Add(feature.Id, feature);
                    }
                }
                winformsMap.Refresh(spatialFenceOverlay);

                vehicleProvider = new TrackingAccessProvider(databasePathFilename);

                // Delete Spatial fences which is not in current spatial fence layer
                vehicleProvider.DeleteSpatialFencesExcluding(spatialFenceLayer.InternalFeatures);

                // Add or update the spatial fences that already exist
                foreach (Feature feature in spatialFenceLayer.InternalFeatures)
                {
                    // Update Spatial fence by feature Id
                    // if the affected data row number is 0, we will add a new row in the database
                    int updatedCount = vehicleProvider.UpdateSpatialFenceByFeature(feature);
                    if (updatedCount == 0)
                    {
                        vehicleProvider.InsertSpatialFence(feature);
                    }
                }
            }
            catch (OleDbException)
            {
                MessageBox.Show(Resources.Please_make_sureText + databasePathFilename + Resources.is_writableText, Resources.SaveSpatialFencesText);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Resources.SaveSpatialFencesText);
            }
            finally
            {
                // Turn the track mode to none and clear the features in the edit overlay
                winformsMap.TrackOverlay.TrackMode = TrackMode.None;
                winformsMap.TrackOverlay.TrackShapeLayer.Open();
                winformsMap.EditOverlay.EditShapesLayer.Open();
                winformsMap.TrackOverlay.TrackShapeLayer.Clear();
                winformsMap.EditOverlay.EditShapesLayer.Clear();
                ClearPopups();
                winformsMap.Refresh();
                if (vehicleProvider != null) { vehicleProvider.Dispose(); }
                if (EditFenceRadioButton.Checked || DrawNewFenceRadioButton.Checked)
                {
                    EditFenceRadioButton.Checked = false;
                    DrawNewFenceRadioButton.Checked = true;
                    DrawNewFence();
                }
            }
        }

        private void CancelEditClick(object sender, EventArgs e)
        {
            CancelEdit();
        }

        private void CancelEdit()
        {
            CancelEditRadioButton.Checked = false;
            winformsMap.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            foreach (Feature editFeature in winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Where(f => (string)f.Tag != "Measure").ToList())
            {
                winformsMap.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove(editFeature);
            }

            LayerOverlay spatialFenceOverlay = (LayerOverlay)winformsMap.Overlays["SpatialFenceOverlay"];
            InMemoryFeatureLayer spatialFenceLayer = (InMemoryFeatureLayer)spatialFenceOverlay.Layers["SpatialFenceLayer"];
            spatialFenceLayer.FeatureIdsToExclude.Clear();
            winformsMap.Refresh();
            if (EditFenceRadioButton.Checked)
            {
                EditFenceRadioButton.Checked = false;
                DrawNewFenceRadioButton.Checked = true;
                DrawNewFence();
            }
        }

        private void WinformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                RectangleShape rectangle = GetBufferedRectangle(e.WorldLocation);
                LayerOverlay traceOverlay = (LayerOverlay)winformsMap.Overlays["TraceOverlay"];
                ClearPopups();
                lock (traceOverlay.Layers)
                {
                    foreach (Layer layer in traceOverlay.Layers)
                    {
                        InMemoryFeatureLayer vehicleLayer = (InMemoryFeatureLayer)layer;
                        vehicleLayer.Open();
                        if (vehicleLayer.GetBoundingBox().Intersects(rectangle))
                        {
                            Collection<Feature> resultFeatures = vehicleLayer.QueryTools.GetFeaturesIntersecting(rectangle, ReturningColumnsType.AllColumns);
                            if (resultFeatures.Count > 0)
                            {
                                ShowPopup(e.WorldLocation, new VehicleTrailPopup(resultFeatures[0]));
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void WinformsMap1_CurrentExtentChanged(object sender, CurrentExtentChangedWinformsMapEventArgs e)
        {
            if (winformsMap.Controls.Count > 0)
            {
                Collection<Control> popups = new Collection<Control>();
                foreach (Control popup in winformsMap.Controls)
                {
                    popups.Add(popup);
                }
                foreach (Control popup in popups)
                {
                    PointShape tempPointShape = popup.Tag as PointShape;
                    if (tempPointShape != null)
                    {
                        winformsMap.Controls.Remove(popup);
                        ShowPopup(tempPointShape, (UserControl)popup);
                    }
                }
            }
        }

        private void MeasureUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            measureUnitSystem = (UnitSystem)Enum.Parse(typeof(UnitSystem), MeasureUnitComboBox.SelectedItem.ToString());
            if (winformsMap.AdornmentOverlay.Layers.Contains("ScaleBar"))
            {
                ScaleBarAdornmentLayer scaleBarAdornmentLayer = (ScaleBarAdornmentLayer)winformsMap.AdornmentOverlay.Layers["ScaleBar"];
                scaleBarAdornmentLayer.UnitFamily = measureUnitSystem;
                winformsMap.Refresh(winformsMap.AdornmentOverlay);
            }
        }

        private void WinformsMap1_Resize(object sender, EventArgs e)
        {
            if (overlaySwitcher != null && overlaySwitcherButton != null)
            {
                overlaySwitcher.Left = winformsMap.Width - overlaySwitcher.Width - 18;
                overlaySwitcherButton.Left = winformsMap.Width - overlaySwitcherButton.Width - 23;
                overlaySwitcher.Refresh();
                overlaySwitcherButton.Refresh();
            }
        }

        private void InitializeHeaderPanel()
        {
            Label mapSuiteLabel = new Label();
            mapSuiteLabel.AutoSize = true;
            mapSuiteLabel.BackColor = Color.Transparent;
            mapSuiteLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mapSuiteLabel.Location = new Point(12, 10);
            mapSuiteLabel.TabIndex = 0;
            mapSuiteLabel.Text = Resources.MapSuiteText;

            Label demographicLabel = new Label();
            demographicLabel.AutoSize = true;
            demographicLabel.BackColor = Color.Transparent;
            demographicLabel.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            demographicLabel.Location = new Point(89, 3);
            demographicLabel.TabIndex = 1;
            demographicLabel.Text = Resources.VehicleTrackingSampleText;

            headerPanel.Controls.Add(mapSuiteLabel);
            headerPanel.Controls.Add(demographicLabel);
        }

        private void InitializeBackground()
        {
            SetRoundBorder(ControlGroupPictureBox);
            SetRoundBorder(RefreshGroupPictureBox);
            SetRoundBorder(VehiclesGroupPictureBox);

            ToolTip autoRefreshToolTip = new ToolTip();
            autoRefreshToolTip.SetToolTip(AutoRefreshRadioButton, "Auto Refresh");
            ToolTip refreshToolTip = new ToolTip();
            refreshToolTip.SetToolTip(RefreshRadioButton, "Refresh");
            ToolTip panToolTip = new ToolTip();
            panToolTip.SetToolTip(PanRadioButton, "Pan map");
            ToolTip editTip = new ToolTip();
            editTip.SetToolTip(rbtnEdit, "Draw fences");
            ToolTip newFenceTip = new ToolTip();
            newFenceTip.SetToolTip(DrawNewFenceRadioButton, "Track new fences");
            ToolTip editFenceTip = new ToolTip();
            editFenceTip.SetToolTip(EditFenceRadioButton, "Edit the selected fence");
            ToolTip deleteFenceTip = new ToolTip();
            deleteFenceTip.SetToolTip(rbtnDeleteFence, "Delete");
            ToolTip saveFenceTip = new ToolTip();
            saveFenceTip.SetToolTip(rbtnSaveFence, "Save");
            ToolTip cancelFenceTip = new ToolTip();
            cancelFenceTip.SetToolTip(CancelEditRadioButton, "Cancel");
            ToolTip measureTip = new ToolTip();
            measureTip.SetToolTip(rbtnMeasure, "Measure distance");
            ToolTip areaTip = new ToolTip();
            areaTip.SetToolTip(rbtnMeasureArea, "Measure polygon");
            ToolTip lineTip = new ToolTip();
            lineTip.SetToolTip(MeasureLineRadioButton, "Measure line");
            ToolTip cancelTip = new ToolTip();
            cancelTip.SetToolTip(rbtnCancelMeasure, "Cancel");
        }

        private static void SetRoundBorder(PictureBox pictureBox)
        {
            Bitmap newBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                Rectangle rect = new Rectangle(0, 0, pictureBox.ClientRectangle.Width - 1, pictureBox.ClientRectangle.Height - 1);
                GraphicsPath path = MapSuiteSampleHelper.CreateRoundPath(rect, 10);
                g.DrawPath(new Pen(Color.FromArgb(255, 200, 200, 200), 1), path);
            }
            pictureBox.Image = newBitmap;
        }

        private void CollapsPictureBox_Click(object sender, EventArgs e)
        {
            controlPanel.Visible = !controlPanel.Visible;
            if (controlPanel.Visible)
            {
                CollapsPictureBox.Left = originalPoint.X;
                grayPanel.Left = CollapsPictureBox.Left + 10;
                winformsMap.Left = grayPanel.Left + 10;
                winformsMap.Width = Width - controlPanel.Width - 20;
                CollapsPictureBox.Image = Resources.collapse;
                CollapsPictureBox.Refresh();
                grayPanel.Refresh();
            }
            else
            {
                CollapsPictureBox.Left = 0;
                grayPanel.Left = CollapsPictureBox.Left + 10;
                winformsMap.Left = grayPanel.Left + 10;
                winformsMap.Width = Width - 20;
                CollapsPictureBox.Image = Resources.expand;
                CollapsPictureBox.Refresh();
                grayPanel.Refresh();
            }
        }

        private void CollapsPictureBox_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.LightBlue;
        }

        private void CollapsPictureBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            DrawLeftPanelShadow();
        }

        private void DrawLeftPanelShadow()
        {
            grayPanel.Height = winformsMap.Height;
            if (grayPanel.Width > 0 && grayPanel.Height > 0)
            {
                Rectangle drawingRectangle = new Rectangle(0, 0, grayPanel.Width, grayPanel.Height);
                LinearGradientBrush myBrush = new LinearGradientBrush(drawingRectangle, Color.Gray, Color.White, LinearGradientMode.Horizontal);
                Bitmap bitmap = new Bitmap(grayPanel.Width, grayPanel.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.FillRectangle(myBrush, drawingRectangle);
                grayPanel.Image = bitmap;
                grayPanel.Refresh();

                CollapsPictureBox.Top = (int)(grayPanel.Height * 0.5 - CollapsPictureBox.Height * 0.5 + grayPanel.Top);
                CollapsPictureBox.Refresh();

                SetRoundBorder(VehiclesGroupPictureBox);
            }
        }

        #endregion UI
    }
}