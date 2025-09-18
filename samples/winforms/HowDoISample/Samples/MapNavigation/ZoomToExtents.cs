using System;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to set the map extent using a variety of different methods.
    /// </summary>
    public class ZoomToExtents : UserControl
    {
        private ShapeFileFeatureLayer _friscoCityBoundary;
        private float _currentRotationAngle = 0;
        private int _currentZoomLevel = 0;
        private double _currentScale = 0;
        private double _lastRotationAngle = 0;

        public ZoomToExtents()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
            MapView_CurrentExtentChanged(null, null);

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Load the Frisco data to a layer
            _friscoCityBoundary = new ShapeFileFeatureLayer(@"./Data/Shapefile/City_ETJ.shp")
            {
                FeatureSource =
                {
                    // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            // Style the data so that we can see it on the map
            _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(16, GeoColors.Blue), GeoColors.DimGray, 2);
            _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add Frisco data to a LayerOverlay and add it to the map
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(_friscoCityBoundary);
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CenterPoint = new PointShape(-10778000, 3912000);
            mapView.CurrentScale = 180000;

            mapView.RotationAngleChanging += MapView_RotationAngleChanging;

            _ = mapView.RefreshAsync();
        }

        /// <summary>
        /// Zoom in on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double left-clicking on the map, or by using the scroll wheel.
        /// </summary>
        private void ZoomIn_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomInAsync();
        }

        /// <summary>
        /// Zoom out on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double right-clicking on the map, or by using the scroll wheel.
        /// </summary>
        private void zoomOut_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomOutAsync();
        }

        /// <summary>
        /// Zoom to a scale programmatically. Note that the scales are bound by a ZoomLevelSet.
        /// </summary>
        private async void ZoomToScale_Click(object sender, EventArgs e)
        {
            await mapView.ZoomToAsync(Convert.ToDouble(zoomScaleTextBox.Text));
        }

        /// <summary>
        /// Set the map extent to fix a layer's bounding box
        /// </summary>
        private void layerBoundingBox_Click(object sender, EventArgs e)
        {
            var friscoCityBoundaryBBox = _friscoCityBoundary.GetBoundingBox();
            mapView.CenterPoint = friscoCityBoundaryBBox.GetCenterPoint();
            mapView.CurrentScale = MapUtil.GetScale(mapView.MapUnit, friscoCityBoundaryBBox, mapView.MapWidth, mapView.MapHeight);
            _ = mapView.RefreshAsync();
        }

        /// <summary>
        /// Set the map extent to center at a point
        /// </summary>
        private void CenterAt_Click(object sender, EventArgs e)
        {
            var pointInMercator = ProjectionConverter.Convert(4326, 3857, new PointShape(-96.82, 33.15));
            _ = mapView.CenterAtAsync(pointInMercator);
        }

        /// <summary>
        /// Update the center point (latitude, longitude), rotation angle, zoom level, and scale when the extent changes.
        /// </summary>
        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var center = mapView.CurrentExtent.GetCenterPoint();
            var centerInDecimalDegrees = ProjectionConverter.Convert(3857, 4326, center);
            _currentRotationAngle = (float)mapView.RotationAngle;
            _currentZoomLevel = mapView.GetSnappedZoomLevelIndex(mapView.CurrentScale);
            _currentScale = mapView.CurrentScale;

            centerPointLabel.Text = $"Center Point: (Lat: {centerInDecimalDegrees.Y:N4}, Lon: {centerInDecimalDegrees.X:N4})";
            UpdateStatusLabel();
        }

        /// <summary>
        /// Update the rotation angle while the map is rotating.
        /// </summary>
        private void MapView_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            double currentRotation = e.NewRotationAngle;

            if (Math.Abs(currentRotation - _lastRotationAngle) > 0.1) // Change threshold
            {
                _lastRotationAngle = currentRotation;
                _currentRotationAngle = (float)currentRotation;
                UpdateStatusLabel();
            }
        }

        /// <summary>
        /// Rotate the map and update the rotateAngleTextBox value when sliding the track bar.
        /// </summary>
        private void RotateAngleTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int angle = ((TrackBar)sender).Value;
            rotateAngleTextBox.Text = angle.ToString();
            _ = mapView.ZoomToAsync(mapView.CenterPoint, mapView.CurrentScale, angle);
        }

        private void UpdateStatusLabel()
        {
            statusLabel.Text = $"Rotation: {_currentRotationAngle:N0}" + new string(' ', 3) +
                               $"Zoom: {_currentZoomLevel}" + new string(' ', 3) +
                               $"Scale: {_currentScale:N0}";
        }


        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private Label zoomingLabel;
        private Label layerBoundingBoxLabel;
        private Label centerAtLabel;
        private Label rotatingLabel;
        private Label centerPointLabel;
        private Label statusLabel;
        private Button zoomInButton;
        private Button zoomOutButton;
        private Button ZoomToScaleButton;
        private Button layerBoundingBoxButton;
        private Button centerAtButton;
        private TextBox zoomScaleTextBox;
        private TextBox rotateAngleTextBox;
        private TrackBar rotateAngleTrackBar;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            consolePanel = new Panel();
            zoomingLabel = new Label();
            layerBoundingBoxLabel = new Label();
            centerAtLabel = new Label();
            rotatingLabel = new Label();
            centerPointLabel = new Label();
            statusLabel = new Label();
            zoomInButton = new Button();
            zoomOutButton = new Button();
            ZoomToScaleButton = new Button();
            layerBoundingBoxButton = new Button();
            centerAtButton = new Button();
            zoomScaleTextBox = new TextBox();
            rotateAngleTextBox = new TextBox();
            rotateAngleTrackBar = new TrackBar();
            consolePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rotateAngleTrackBar).BeginInit();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(4, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.Margin = new System.Windows.Forms.Padding(0);
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(946, 634);
            mapView.TabIndex = 0;
            // 
            // consolePanel
            // 
            consolePanel.BackColor = Color.Gray;
            consolePanel.Controls.Add(zoomInButton);
            consolePanel.Controls.Add(zoomOutButton);
            consolePanel.Controls.Add(rotateAngleTextBox);
            consolePanel.Controls.Add(rotateAngleTrackBar);
            consolePanel.Controls.Add(rotatingLabel);
            consolePanel.Controls.Add(centerAtButton);
            consolePanel.Controls.Add(centerAtLabel);
            consolePanel.Controls.Add(layerBoundingBoxButton);
            consolePanel.Controls.Add(layerBoundingBoxLabel);
            consolePanel.Controls.Add(ZoomToScaleButton);
            consolePanel.Controls.Add(zoomScaleTextBox);
            consolePanel.Controls.Add(zoomingLabel);
            consolePanel.Dock = DockStyle.Right;
            consolePanel.Location = new Point(953, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new Size(302, 634);
            consolePanel.TabIndex = 1;
            // 
            // zoomingLabel
            // 
            zoomingLabel.AutoSize = true;
            zoomingLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            zoomingLabel.ForeColor = Color.White;
            zoomingLabel.Location = new Point(14, 17);
            zoomingLabel.Name = "zoomingLabel";
            zoomingLabel.Size = new Size(83, 20);
            zoomingLabel.Text = "Zooming:";
            // 
            // layerBoundingBoxLabel
            // 
            layerBoundingBoxLabel.AutoSize = true;
            layerBoundingBoxLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            layerBoundingBoxLabel.ForeColor = Color.White;
            layerBoundingBoxLabel.Location = new Point(17, 140);
            layerBoundingBoxLabel.Name = "layerBoundingBoxLabel";
            layerBoundingBoxLabel.Size = new Size(174, 20);
            layerBoundingBoxLabel.Text = "Layer Bounding Box:";
            // 
            // centerAtLabel
            // 
            centerAtLabel.AutoSize = true;
            centerAtLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            centerAtLabel.ForeColor = Color.White;
            centerAtLabel.Location = new Point(17, 225);
            centerAtLabel.Name = "centerAtLabel";
            centerAtLabel.Size = new Size(91, 20);
            centerAtLabel.Text = "Center At:";
            // 
            // rotatingLabel
            // 
            rotatingLabel.AutoSize = true;
            rotatingLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rotatingLabel.ForeColor = Color.White;
            rotatingLabel.Location = new Point(17, 315);
            rotatingLabel.Name = "rotatingLabel";
            rotatingLabel.Size = new Size(83, 20);
            rotatingLabel.Text = "Rotating:";
            // 
            // centerPointLabel
            // 
            centerPointLabel.AutoSize = true;
            centerPointLabel.Anchor = AnchorStyles.Bottom;
            centerPointLabel.BackColor = Color.DarkGray;
            centerPointLabel.Font = new Font("Microsoft Sans Serif", 12F,FontStyle.Bold);
            centerPointLabel.ForeColor = Color.White;
            centerPointLabel.Location = new Point(250, 540);
            centerPointLabel.Name = "centerPointLabel";
            centerPointLabel.TextAlign = ContentAlignment.MiddleCenter;
            centerPointLabel.Anchor = AnchorStyles.Bottom;
            centerPointLabel.Left = mapView.Width / 2 - 220;
            centerPointLabel.Top = mapView.Height - 90;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Anchor = AnchorStyles.Bottom;
            statusLabel.BackColor = Color.DarkGray;
            statusLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            statusLabel.ForeColor = Color.White;
            statusLabel.Location = new Point(250, 575);
            statusLabel.Margin = new Padding(0);
            statusLabel.Name = "statusLabel";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            statusLabel.Anchor = AnchorStyles.Bottom;
            statusLabel.Left = mapView.Width / 2 - 220;
            statusLabel.Top = mapView.Height - 55;
            // 
            // zoomInButton
            // 
            zoomInButton.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            zoomInButton.Location = new Point(14, 58);
            zoomInButton.Name = "zoomInButton";
            zoomInButton.Size = new Size(130, 30);
            zoomInButton.Text = "Zoom In";
            zoomInButton.UseVisualStyleBackColor = true;
            zoomInButton.Click += ZoomIn_Click;
            zoomInButton.TabIndex = 2;
            // 
            // zoomOutButton
            // 
            zoomOutButton.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            zoomOutButton.Location = new Point(145, 58);
            zoomOutButton.Name = "zoomOutButton";
            zoomOutButton.Size = new Size(130, 30);
            zoomOutButton.Text = "Zoom Out";
            zoomOutButton.UseVisualStyleBackColor = true;
            zoomOutButton.Click += zoomOut_Click;
            zoomOutButton.TabIndex = 3;
            // 
            // ZoomToScaleButton
            // 
            ZoomToScaleButton.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ZoomToScaleButton.Location = new Point(14, 90);
            ZoomToScaleButton.Name = "ZoomToScaleButton";
            ZoomToScaleButton.Size = new Size(130, 30);
            ZoomToScaleButton.Text = "Zoom To Scale";
            ZoomToScaleButton.UseVisualStyleBackColor = true;
            ZoomToScaleButton.Click += ZoomToScale_Click;
            ZoomToScaleButton.TabIndex = 4;
            // 
            // layerBoundingBoxButton
            // 
            layerBoundingBoxButton.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            layerBoundingBoxButton.Location = new Point(17, 172);
            layerBoundingBoxButton.Name = "layerBoundingBoxButton";
            layerBoundingBoxButton.Size = new Size(260, 30);
            layerBoundingBoxButton.Text = "Set Extent to Layer BBox";
            layerBoundingBoxButton.UseVisualStyleBackColor = true;
            layerBoundingBoxButton.Click += layerBoundingBox_Click;
            layerBoundingBoxButton.TabIndex = 6;
            // 
            // centerAtButton
            // 
            centerAtButton.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            centerAtButton.Location = new Point(17, 260);
            centerAtButton.Name = "featureBoundingBoxButton";
            centerAtButton.Size = new Size(260, 30);
            centerAtButton.Text = "Center At (33.15,-96.82)";
            centerAtButton.UseVisualStyleBackColor = true;
            centerAtButton.Click += CenterAt_Click;
            centerAtButton.TabIndex = 7;
            // 
            // zoomScaleTextBox
            // 
            zoomScaleTextBox.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            zoomScaleTextBox.Location = new Point(145, 90);
            zoomScaleTextBox.Multiline = true;
            zoomScaleTextBox.Name = "zoomScaleTextBox";
            zoomScaleTextBox.Size = new Size(130, 30);
            zoomScaleTextBox.Text = "10000";
            zoomScaleTextBox.TabIndex = 5;
            // 
            // rotateAngleTextBox
            // 
            rotateAngleTextBox.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rotateAngleTextBox.Location = new Point(232, 350);
            rotateAngleTextBox.Multiline = true;
            rotateAngleTextBox.Name = "rotateAngleTextBox";
            rotateAngleTextBox.Text = "0";
            rotateAngleTextBox.Size = new Size(45, 30);
            rotateAngleTextBox.TabIndex = 9;
            // 
            // rotateAngleTrackBar
            // 
            rotateAngleTrackBar.Name = "rotateAngleTrackBar";
            rotateAngleTrackBar.Location = new Point(17, 350);
            rotateAngleTrackBar.Minimum = 0;
            rotateAngleTrackBar.Maximum = 360;
            rotateAngleTrackBar.Value = 0;
            rotateAngleTrackBar.TickFrequency = 15;
            rotateAngleTrackBar.SmallChange = 1;
            rotateAngleTrackBar.LargeChange = 15;
            rotateAngleTrackBar.Size = new Size(220, 45);
            rotateAngleTrackBar.ValueChanged += RotateAngleTrackBar_ValueChanged;
            rotateAngleTrackBar.TabIndex = 8;
            // 
            // ZoomToExtents
            // 
            AutoSize = true;
            Controls.Add(mapView);
            Controls.Add(statusLabel);
            Controls.Add(centerPointLabel);
            Controls.Add(consolePanel);
            Name = "ZoomToExtents";
            Size = new Size(1255, 634);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rotateAngleTrackBar).EndInit();
            ResumeLayout(false);

            centerPointLabel.BringToFront();
            statusLabel.BringToFront();
        }

        #endregion Component Designer generated code


    }
}