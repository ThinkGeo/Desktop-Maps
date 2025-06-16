using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class NavigationMap : UserControl
    {
        private ThinkGeoCloudRasterMapsOverlay _backgroundOverlay;
        private PointShape _empireStateBuildingPosition;

        public NavigationMap()
        {
            InitializeComponent();
        }

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

            ((Control)mapView).SizeChanged += NavigationMap_SizeChanged;
            NavigationMap_SizeChanged(this, EventArgs.Empty);

            mapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
            MapView_CurrentExtentChanged(null, null);

            mapView.RotationAngleChanging += MapView_RotationAngleChanging;
            themeCheckBox.CheckedChanged += ThemeCheckBox_CheckedChanged;
            compassButton.Click += CompassButton_Click;
            defaultExtentButton.Click += DefaultExtentButton_Click;

            _ = mapView.RefreshAsync();
        }

        private void MapView_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            double currentRotation = mapView.RotationAngle;

            if (Math.Abs(currentRotation - lastRotationAngle) > 0.1) // Change threshold
            {
                lastRotationAngle = currentRotation;
                label2.Text = $"Rotation: {currentRotation:N0}";
                UpdateCompassImage((float)currentRotation);
            }
        }

        private void NavigationMap_SizeChanged(object sender, EventArgs e)
        {
            var x = mapView.Width / 2;
            var y = mapView.Height;

            label1.Location = new Point(x - 200, y - 80);
            label2.Location = new Point(x - 250, y - 50);
            label3.Location = new Point(x - 100, y - 50);
            label4.Location = new Point(x + 50 , y - 50);
            themeCheckBox.Location = new Point(15, y - 50);
        }

        private void CompassButton_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomToAsync(mapView.CenterPoint, mapView.CurrentScale, 0);
        }

        private void DefaultExtentButton_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomToAsync(_empireStateBuildingPosition, 100000, -30);
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

        public static Image RotateImage(Image image, float angle)
        {
            if (image == null) return null;

            // Create a new empty bitmap to hold rotated image
            Bitmap rotatedImage = new Bitmap(image.Width, image.Height);
            rotatedImage.MakeTransparent();
            rotatedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point to the center of the image
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                g.TranslateTransform(image.Width / 2f, image.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2f, -image.Height / 2f);

                // Draw the original image onto the rotated graphics object
                g.DrawImage(image, new Point(0, 0));
            }

            return rotatedImage;
        }

        private void UpdateCompassImage(float angle)
        {
            compassButton.Image?.Dispose();

            string imagePath = Path.Combine(Application.StartupPath, "Resources", "icon_north_arrow.png");

            if (File.Exists(imagePath))
            {
                using (var originalImage = Image.FromFile(imagePath))
                {
                    // Clone it first because we can't rotate a disposed image
                    var cloneImage = (Image)originalImage.Clone();
                    compassButton.Image = RotateImage(cloneImage, angle);
                }
            }
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var center = mapView.CurrentExtent.GetCenterPoint();
            var centerInDecimalDegrees = ProjectionConverter.Convert(3857, 4326, center);
            float angle = (float)mapView.RotationAngle;

            label1.Text = $"Center Point: (Lat: {centerInDecimalDegrees.Y:N4}, Lon: {centerInDecimalDegrees.X:N4})";

            label2.Text = $"Rotation: {angle:N0}";
            label3.Text = $"Zoom: {mapView.GetSnappedZoomLevelIndex(mapView.CurrentScale):N0}";
            label4.Text = $"Scale: {mapView.CurrentScale:N0}";

            compassButton.Image?.Dispose(); // Dispose previous if needed
            string imagePath = Path.Combine(Application.StartupPath, "Resources", "icon_north_arrow.png");
            using (Image originalImage = Image.FromFile(imagePath))
            {
                compassButton.Image = RotateImage((Image)originalImage.Clone(), angle);
            }
        }

        #region Component Designer generated code
        private Label label1;
        private Label label2;
        private Label label3; 
        private Label label4;
        private double lastRotationAngle = 0;
        private CheckBox themeCheckBox;
        private PictureBox compassButton;
        private PictureBox defaultExtentButton;
        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
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
            this.mapView.TabIndex = 0;
            // 
            // label1
            // 
            label1.BackColor = Color.LightGray;
            label1.Font = new Font("Microsoft Sans Serif", 12F);
            label1.ForeColor = Color.Black;
            label1.Name = "label1";
            label1.Size = new Size(340, 25);
            label1.TabIndex = 0;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.LightGray;
            label2.Font = new Font("Microsoft Sans Serif", 12F);
            label2.ForeColor = Color.Black;
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(150, 25);
            label2.TabIndex = 0;
            label2.Text = "Rotation Angle";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.LightGray;
            label3.Font = new Font("Microsoft Sans Serif", 12F);
            label3.ForeColor = Color.Black;
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(150, 25);
            label3.TabIndex = 7;
            label3.Text = "Current Zoom:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.LightGray;
            label4.Font = new Font("Microsoft Sans Serif", 12F);
            label4.ForeColor = Color.Black;
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(150, 25);
            label4.TabIndex = 12;
            label4.Text = "Current Scale";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // themeCheckBox
            // 
            themeCheckBox.BackColor = Color.LightGray;
            themeCheckBox.Font = new Font("Microsoft Sans Serif", 12F);
            themeCheckBox.ForeColor = Color.Black;
            themeCheckBox.Margin = new Padding(0);
            themeCheckBox.Name = "themeCheckBox";
            themeCheckBox.Size = new Size(150, 25);
            themeCheckBox.TabIndex = 0;
            themeCheckBox.Text = "Dark Theme";
            themeCheckBox.TextAlign = ContentAlignment.MiddleCenter;
            themeCheckBox.UseVisualStyleBackColor = false;
            // 
            // compassButton
            // 
            compassButton.Name = "compassButton";
            compassButton.Size = new Size(40, 40);
            compassButton.BackColor = Color.Transparent;
            compassButton.SizeMode = PictureBoxSizeMode.StretchImage;
            string imagePathOfCompassButton = Path.Combine(Application.StartupPath, "Resources", "icon_north_arrow.png");
            Image originalImageOfCompassButton = Image.FromFile(imagePathOfCompassButton);
            compassButton.Image = RotateImage(originalImageOfCompassButton, 0);
            System.Drawing.Drawing2D.GraphicsPath pathOfCompassButton = new System.Drawing.Drawing2D.GraphicsPath();
            pathOfCompassButton.AddEllipse(0, 0, compassButton.Width, compassButton.Height);
            compassButton.Region = new Region(pathOfCompassButton);
            compassButton.Location = new Point(1140, 10);
            compassButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(compassButton);
            // 
            // defaultExtentButton 
            // 
            defaultExtentButton.Name = "defaultExtentButton";
            defaultExtentButton.Size = new Size(40, 40);
            defaultExtentButton.BackColor = Color.Transparent;
            defaultExtentButton.SizeMode = PictureBoxSizeMode.StretchImage;
            string imagePathOfDefaultExtentButton = Path.Combine(Application.StartupPath, "Resources", "icon_globe_black.png");
            Image originalImageOfDefaultExtentButton = Image.FromFile(imagePathOfDefaultExtentButton);
            defaultExtentButton.Image = RotateImage(originalImageOfDefaultExtentButton, 0);
            System.Drawing.Drawing2D.GraphicsPath pathOfDefaultExtentButton = new System.Drawing.Drawing2D.GraphicsPath();
            pathOfDefaultExtentButton.AddEllipse(0, 0, defaultExtentButton.Width, defaultExtentButton.Height);
            defaultExtentButton.Region = new Region(pathOfDefaultExtentButton);
            defaultExtentButton.Location = new Point(1140, 60);
            defaultExtentButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(defaultExtentButton);
            // 
            // NavigationMap
            // 
            Controls.Add(mapView);
            Controls.Add(themeCheckBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(compassButton);
            Controls.Add(defaultExtentButton);
            Name = "NavigationMap";
            Size = new Size(1194, 560);
            Load += Form_Load;
            ResumeLayout(false);

            label1.BringToFront();
            label2.BringToFront();
            label3.BringToFront();
            label4.BringToFront();
            themeCheckBox.BringToFront();
            compassButton.BringToFront();
            defaultExtentButton.BringToFront();
        }

        #endregion Component Designer generated code

    }
}