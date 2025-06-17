using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to zoom, pan, and rotate the map control.
    /// </summary>
    public class NavigationMap : UserControl
    {
        private ThinkGeoCloudRasterMapsOverlay _backgroundOverlay;
        private PointShape _empireStateBuildingPosition;

        public NavigationMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            _backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_raster_light")
            };
            mapView.Overlays.Add(_backgroundOverlay);

            // Create the marker overlay to hold UI elements like labels
            var markerOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add(markerOverlay);

            // Convert Lat/Lon (EPSG:4326) to Spherical Mercator (EPSG:3857)
            _empireStateBuildingPosition = ProjectionConverter.Convert(4326, 3857, new PointShape(-73.9856654, 40.74843661));

            // Create a marker with both label and image content
            var marker = new Marker(_empireStateBuildingPosition)
            {
                //Content = markerContent,
                ImageSource = new BitmapImage(new Uri("/Resources/empire_state_building.png", UriKind.RelativeOrAbsolute)),
                Width = 32,
                Height = 64,
                YOffset = -32
            };

            // Add the marker to the overlay
            markerOverlay.Markers.Add(marker);

            // set up the map extent and refresh
            mapView.RotationAngle = -30;
            mapView.CurrentScale = 100000;
            mapView.CenterPoint = _empireStateBuildingPosition;

            mapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
            MapView_CurrentExtentChanged(null, null);

            mapView.RotationAngleChanging += MapView_RotationAngleChanging;
            themeCheckBox.CheckedChanged += ThemeCheckBox_CheckedChanged;
            compassButton.Click += CompassButton_Click;
            defaultExtentButton.Click += DefaultExtentButton_Click;

            _ = mapView.RefreshAsync();
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var center = mapView.CurrentExtent.GetCenterPoint();
            var centerInDecimalDegrees = ProjectionConverter.Convert(3857, 4326, center);
            float angle = (float)mapView.RotationAngle;

            centerPointLabel.Text = $"Center Point: (Lat: {centerInDecimalDegrees.Y:N4}, Lon: {centerInDecimalDegrees.X:N4})";
            rotationLabel.Text = $"Rotation: {angle:N0}";
            zoomLabel.Text = $"Zoom: {mapView.GetSnappedZoomLevelIndex(mapView.CurrentScale):N0}";
            scaleLabel.Text = $"Scale: {mapView.CurrentScale:N0}";

            ImageHelper.UpdateImage(compassButton, "icon_north_arrow.png", angle);
        }

        private void MapView_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            double currentRotation = e.NewRotationAngle;

            if (Math.Abs(currentRotation - lastRotationAngle) > 0.1) // Change threshold
            {
                lastRotationAngle = currentRotation;
                rotationLabel.Text = $"Rotation: {currentRotation:N0}";
                ImageHelper.UpdateImage(compassButton, "icon_north_arrow.png", (float)currentRotation);
            }
        }

        private async void ThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _backgroundOverlay.MapType = themeCheckBox.Checked == true
              ? ThinkGeoCloudRasterMapsMapType.Dark_V2_X2
              : ThinkGeoCloudRasterMapsMapType.Light_V2_X2;

            // You may need to reset the tile cache ID to avoid mixing dark/light tiles:
            _backgroundOverlay.TileCache = new FileRasterTileCache(@".\cache",
                themeCheckBox.Checked == true ? "thinkgeo_raster_dark" : "thinkgeo_raster_light");

            await mapView.RefreshAsync(); // Cancel the ongoing rendering
            await _backgroundOverlay.RefreshAsync();
        }

        private void CompassButton_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomToAsync(mapView.CenterPoint, mapView.CurrentScale, 0);
        }

        private void DefaultExtentButton_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomToAsync(_empireStateBuildingPosition, 100000, -30);
        }


        #region Component Designer generated code

        private MapView mapView;
        private Label centerPointLabel;
        private Label rotationLabel;
        private Label zoomLabel;
        private Label scaleLabel;
        private double lastRotationAngle = 0;
        private CheckBox themeCheckBox;
        private PictureBox compassButton;
        private PictureBox defaultExtentButton;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            centerPointLabel = new Label();
            rotationLabel = new Label();
            zoomLabel = new Label();
            scaleLabel = new Label();
            themeCheckBox = new CheckBox();
            compassButton = new PictureBox();
            defaultExtentButton = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)compassButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)defaultExtentButton).BeginInit();
            SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Dock = DockStyle.Fill;
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            // 
            // themeCheckBox
            // 
            themeCheckBox.BackColor = Color.LightGray;
            themeCheckBox.Font = new Font("Microsoft Sans Serif", 12F);
            themeCheckBox.ForeColor = Color.Black;
            themeCheckBox.Location = new Point(20, 600);
            themeCheckBox.Name = "themeCheckBox";
            themeCheckBox.Size = new Size(150, 25);
            themeCheckBox.Text = "Dark Theme";
            themeCheckBox.TextAlign = ContentAlignment.MiddleCenter;
            themeCheckBox.UseVisualStyleBackColor = false;
            themeCheckBox.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            themeCheckBox.Left = 20;
            themeCheckBox.Top = mapView.Height;
            themeCheckBox.TabIndex = 0;
            // 
            // compassButton
            // 
            compassButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            compassButton.BackColor = Color.Transparent;
            compassButton.Image = Properties.Resources.icon_north_arrow1;
            compassButton.Location = new Point(1203, 10);
            compassButton.Name = "compassButton";
            compassButton.Size = new Size(40, 40);
            compassButton.SizeMode = PictureBoxSizeMode.StretchImage;
            System.Drawing.Drawing2D.GraphicsPath pathOfCompassButton = new System.Drawing.Drawing2D.GraphicsPath();
            pathOfCompassButton.AddEllipse(0, 0, compassButton.Width, compassButton.Height);
            compassButton.Region = new Region(pathOfCompassButton);
            compassButton.TabIndex = 1;
            // 
            // defaultExtentButton
            // 
            defaultExtentButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            defaultExtentButton.BackColor = Color.Transparent;
            defaultExtentButton.Image = Properties.Resources.icon_globe_black;
            defaultExtentButton.Location = new Point(1203, 60);
            defaultExtentButton.Name = "defaultExtentButton";
            defaultExtentButton.Size = new Size(40, 40);
            defaultExtentButton.SizeMode = PictureBoxSizeMode.StretchImage;
            System.Drawing.Drawing2D.GraphicsPath pathOfDefaultExtentButton = new System.Drawing.Drawing2D.GraphicsPath();
            pathOfDefaultExtentButton.AddEllipse(0, 0, defaultExtentButton.Width, defaultExtentButton.Height);
            defaultExtentButton.Region = new Region(pathOfDefaultExtentButton);
            defaultExtentButton.TabIndex = 2;
            // 
            // labelZoom
            // 
            zoomLabel.BackColor = Color.LightGray;
            zoomLabel.Font = new Font("Microsoft Sans Serif", 12F);
            zoomLabel.ForeColor = Color.Black;
            zoomLabel.Location = new Point(600, 600);
            zoomLabel.Margin = new Padding(0);
            zoomLabel.Name = "labelZoom";
            zoomLabel.Size = new Size(150, 25);
            zoomLabel.Text = "Current Zoom:";
            zoomLabel.TextAlign = ContentAlignment.MiddleCenter;
            zoomLabel.Anchor = AnchorStyles.Bottom;
            zoomLabel.Left = mapView.Width /2 + 200;
            zoomLabel.Top = mapView.Height;
            // 
            // labelScale
            // 
            scaleLabel.BackColor = Color.LightGray;
            scaleLabel.Font = new Font("Microsoft Sans Serif", 12F);
            scaleLabel.ForeColor = Color.Black;
            scaleLabel.Location = new Point(750, 600);
            scaleLabel.Margin = new Padding(0);
            scaleLabel.Name = "labelScale";
            scaleLabel.Size = new Size(150, 25);
            scaleLabel.Text = "Current Scale";
            scaleLabel.TextAlign = ContentAlignment.MiddleCenter;
            scaleLabel.Anchor = AnchorStyles.Bottom;
            scaleLabel.Left = mapView.Width / 2 + 350;
            scaleLabel.Top = mapView.Height;
            // 
            // labelRotation
            // 
            rotationLabel.BackColor = Color.LightGray;
            rotationLabel.Font = new Font("Microsoft Sans Serif", 12F);
            rotationLabel.ForeColor = Color.Black;
            rotationLabel.Location = new Point(450, 600);
            rotationLabel.Margin = new Padding(0);
            rotationLabel.Name = "labelRotation";
            rotationLabel.Size = new Size(150, 25);
            rotationLabel.Text = "Rotation Angle";
            rotationLabel.TextAlign = ContentAlignment.MiddleCenter;
            rotationLabel.Anchor = AnchorStyles.Bottom;
            rotationLabel.Left = mapView.Width / 2 + 50;
            rotationLabel.Top = mapView.Height;
            // 
            // labelCenterPoint
            // 
            centerPointLabel.BackColor = Color.LightGray;
            centerPointLabel.Font = new Font("Microsoft Sans Serif", 12F);
            centerPointLabel.ForeColor = Color.Black;
            centerPointLabel.Location = new Point(500, 560);
            centerPointLabel.Name = "labelCenterPoint";
            centerPointLabel.Size = new Size(340, 25);
            centerPointLabel.TextAlign = ContentAlignment.MiddleCenter;
            centerPointLabel.Anchor = AnchorStyles.Bottom;
            centerPointLabel.Left = mapView.Width / 2 + 100;
            centerPointLabel.Top = mapView.Height - 40;
            // 
            // NavigationMap
            // 
            Controls.Add(mapView);
            Controls.Add(centerPointLabel);
            Controls.Add(rotationLabel);
            Controls.Add(zoomLabel);
            Controls.Add(scaleLabel);
            Controls.Add(themeCheckBox);
            Controls.Add(compassButton);
            Controls.Add(defaultExtentButton);
            Name = "NavigationMap";
            Size = new Size(1257, 669);
            Load += Form_Load;
            ((System.ComponentModel.ISupportInitialize)compassButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)defaultExtentButton).EndInit();
            ResumeLayout(false);

            centerPointLabel.BringToFront();
            rotationLabel.BringToFront();
            zoomLabel.BringToFront();
            scaleLabel.BringToFront();
            themeCheckBox.BringToFront();
            compassButton.BringToFront();
            defaultExtentButton.BringToFront();
        }

        #endregion Component Designer generated code

    }

    /// <summary>
    /// Helper class for common image processing tasks such as loading, rounding, and rotating images.
    /// Designed to simplify image manipulation for UI controls like buttons or picture boxes.
    /// </summary> 
    public static class ImageHelper
    {
        private static readonly string DefaultImageFolder = Path.Combine(Application.StartupPath, "Resources");

        public static string GetImagePath(string fileName)
        {
            return Path.Combine(DefaultImageFolder, fileName);
        }

        public static bool TryLoadImage(string fileName, out Image image)
        {
            image = null;
            string path = GetImagePath(fileName);
            if (File.Exists(path))
            {
                image = Image.FromFile(path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Rotates an image around its center with a transparent background.
        /// </summary>
        public static Image RotateImage(Image image, float angle)
        {
            if (image == null) return null;

            Bitmap rotatedImage = new Bitmap(image.Width, image.Height);
            rotatedImage.MakeTransparent();
            rotatedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                g.TranslateTransform(image.Width / 2f, image.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2f, -image.Height / 2f);
                g.DrawImage(image, new Point(0, 0));
            }

            return rotatedImage;
        }

        /// <summary>
        /// Updates the target PictureBox with a round and optionally rotated image.
        /// The old image is disposed internally.
        /// </summary>
        public static void UpdateImage(PictureBox pictureBox, string fileName, float angle = 0f)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }

            if (TryLoadImage(fileName, out var loadedImage))
            {
                pictureBox.Image = RotateImage(loadedImage, angle);
            }
        }
    }
}