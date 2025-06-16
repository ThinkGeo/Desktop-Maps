using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using Point = System.Windows.Point;
using Timer = System.Windows.Forms.Timer;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to programmatically zoom, pan, and rotate the map control.
    /// </summary>
    public partial class VehicleNavigation : UserControl
    {
        private double _currentCompassAngle = 0f;
        private double _targetCompassAngle = 0f;
        private Timer _compassAnimationTimer;
        private const float CompassRotationSpeed = 40f;

        private Collection<Vertex> _gpsPoints;
        private readonly List<Vertex> _visitedVertices = new List<Vertex>();
        private int _currentGpsPointIndex;
        private Marker _vehicleMarker;
        private bool _disposed;
        private bool _showOverview;

        private CancellationTokenSource _cancellationTokenSource;
        private ThinkGeoCloudRasterMapsOverlay _backgroundOverlay;

        private FeatureLayerWpfDrawingOverlay _routesOverlay;
        private SimpleMarkerOverlay _markerOverlay;
        private InMemoryFeatureLayer _routeLayer;
        private InMemoryFeatureLayer _visitedRoutesLayer;

        private const double DefaultScale = 5000;

        public VehicleNavigation()
        {
            InitializeComponent();
            this.HandleDestroyed += VehicleNavigation_HandleDestroyed;
        }

        private void VehicleNavigation_HandleDestroyed(object sender, EventArgs e)
        {
            this._disposed = true;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //mapView.MapUnit = GeographyUnit.Meter;
            _cancellationTokenSource = new CancellationTokenSource();
            
            // Add Cloud Maps as a background overlay
            _backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_raster_light")
            };
            mapView.Overlays.Add(_backgroundOverlay);

            mapView.DefaultAnimationSettings = new MapAnimationSettings
            {
                Type = MapAnimationType.DrawWithAnimation,
                Duration = 1500,
                Easing = null
            };

            _gpsPoints = CollectGpsData();

            // Create the Layer for the Route
            _routeLayer = InitRouteLayerFromGpsPoints(_gpsPoints);
            _visitedRoutesLayer = new InMemoryFeatureLayer();
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle =
                LineStyle.CreateSimpleLineStyle(GeoColors.Green, 6, true);
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _routesOverlay = new FeatureLayerWpfDrawingOverlay();
            _routesOverlay.UpdateDataWhileTransforming = true;
            _routesOverlay.FeatureLayers.Add(_routeLayer);
            _routesOverlay.FeatureLayers.Add(_visitedRoutesLayer);
            mapView.Overlays.Add(_routesOverlay);

            // Create a marker overlay to show where the vehicle is
            _markerOverlay = new SimpleMarkerOverlay();
            // Create the marker of the vehicle
            _vehicleMarker = new Marker()
            {
                Position = new Point(_gpsPoints[0].X, _gpsPoints[0].Y),
                ImageSource = new BitmapImage(new Uri("/Resources/vehicle_location.png", UriKind.RelativeOrAbsolute)),
                Width = 24,
                Height = 24
            };
            _markerOverlay.Markers.Add(_vehicleMarker);
            mapView.Overlays.Add(_markerOverlay);

            mapView.CurrentExtentChangedInAnimation += MapView_CurrentExtentChangedInAnimation;
            ((Control)mapView).SizeChanged += VehicleNavigation_SizeChanged;
            VehicleNavigation_SizeChanged(this, EventArgs.Empty);
            mapView.RotationAngleChanging += MapView_RotationAngleChanging;

            mapView.CenterPoint = new PointShape(_gpsPoints[0]);
            mapView.CurrentScale = DefaultScale;

            _ = ZoomToGpsPointsAsync(_gpsPoints);
        }

        private async Task ZoomToGpsPointsAsync(Collection<Vertex> gpsPoints)
        {
            await mapView.RefreshAsync();

            for (_currentGpsPointIndex = 0; _currentGpsPointIndex < gpsPoints.Count; _currentGpsPointIndex++)
            {
                try
                {
                    await ZoomToGpsPointAsync(gpsPoints, _currentGpsPointIndex, _cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    await Task.Delay(500);
                }

                if (_disposed)
                    break;
            }
        }

        private void MapView_CurrentExtentChangedInAnimation(object sender, 
            CurrentExtentChangedInAnimationMapViewEventArgs e)
        {
            if (!MapUtil.IsSameDouble(e.FromResolution, e.ToResolution))
                return;

            UpdateRoutesAndMarker(e.Progress);
        }

        private void UpdateRoutesAndMarker(double progress, double angle = 0)
        {
            if (_currentGpsPointIndex == 0)
                return;

            if (_currentGpsPointIndex >= _gpsPoints.Count)
                return;

            var fromPoint = _gpsPoints[_currentGpsPointIndex - 1];
            var toPoint = _gpsPoints[_currentGpsPointIndex];

            var x = (toPoint.X - fromPoint.X) * progress + fromPoint.X;
            var y = (toPoint.Y - fromPoint.Y) * progress + fromPoint.Y;

            if (_visitedVertices.Count > 0 && !MapUtil.IsSamePoint(_visitedVertices[_visitedVertices.Count - 1], _gpsPoints[_currentGpsPointIndex - 1]))
            {
                _visitedVertices.RemoveAt(_visitedVertices.Count - 1);
            }

            UpdateVisitedRoutes(new Vertex(x, y));

            _vehicleMarker.RotateAngle = angle;
            _vehicleMarker.Position = new Point(x, y);

            _ = _routesOverlay.RefreshAsync();
            _ = _markerOverlay.RefreshAsync();
        }

        private void UpdateVisitedRoutes(Vertex newVertex)
        {
            _visitedVertices.Add(newVertex);

            if (_visitedVertices.Count < 2)
                return;

            var lineShape = new LineShape(_visitedVertices);
            _visitedRoutesLayer.InternalFeatures.Clear();
            _visitedRoutesLayer.InternalFeatures.Add(new Feature(lineShape));
        }

        private async Task ZoomToGpsPointAsync(Collection<Vertex> gpsPoints, int gpsPointIndex, CancellationToken cancellationToken)
        {
            if (gpsPointIndex >= gpsPoints.Count)
                return;

            var angle = GetRotationAngle(gpsPointIndex, gpsPoints);

            if (_showOverview)
            {
                var totalTime = 1000.0; // Set a 1-second animation
                var currentTime = DateTime.Now;

                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                        await Task.Delay(500);
                    double duration = (DateTime.Now - currentTime).TotalMilliseconds;
                    var process = duration / totalTime;

                    if (process > 1)
                        break;

                    UpdateRoutesAndMarker(process, angle);

                    await _routesOverlay.RefreshAsync();
                    await Task.Delay(1);
                }
            }
            else
            {
                var currentLocation = gpsPoints[gpsPointIndex];
                var centerPoint = new PointShape(currentLocation);
                // Recenter the map to display the GPS location 200 pixels towards the bottom for improved visibility.
                centerPoint = MapUtil.OffsetPointWithScreenOffset(centerPoint, 0, 200, angle, DefaultScale, mapView.MapUnit);

                await mapView.ZoomToAsync(centerPoint, DefaultScale, angle, cancellationToken);
            }
        }

        private static Collection<Vertex> CollectGpsData()
        {
            var gpsPoints = new Collection<Vertex>();
            var lines = File.ReadAllLines(@"./Data/Csv/vehicle-route.csv");

            // Convert GPS Points from Lat/Lon (srid:4326) to Spherical Mercator (Srid:3857), which is the projection of the base map
            var converter = new ProjectionConverter(4326, 3857);
            converter.Open();

            foreach (var location in lines)
            {
                var posItems = location.Split(',');
                var lat = double.Parse(posItems[0]);
                var lon = double.Parse(posItems[1]);
                var vertexInSphericalMercator = converter.ConvertToExternalProjection(lon, lat);
                gpsPoints.Add(vertexInSphericalMercator);
            }

            converter.Close();
            return gpsPoints;
        }

        private static InMemoryFeatureLayer InitRouteLayerFromGpsPoints(Collection<Vertex> gpsPoints)
        {
            var lineShape = new LineShape();
            foreach (var point in gpsPoints)
                lineShape.Vertices.Add(point);

            // create the layers for the routes.
            var routeLayer = new InMemoryFeatureLayer();
            routeLayer.InternalFeatures.Add(new Feature(lineShape));
            routeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Yellow, 6, true);
            routeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return routeLayer;
        }

        private static double GetRotationAngle(int currentIndex, IReadOnlyList<Vertex> gpsPoints)
        {
            Vertex currentLocation;
            Vertex nextLocation;

            if (currentIndex < gpsPoints.Count - 1)
            {
                currentLocation = gpsPoints[currentIndex];
                nextLocation = gpsPoints[currentIndex + 1];
            }
            else
            {
                currentLocation = gpsPoints[currentIndex - 1];
                nextLocation = gpsPoints[currentIndex];
            }

            double angle;
            if (nextLocation.X - currentLocation.X != 0)
            {
                var dx = nextLocation.X - currentLocation.X;
                var dy = nextLocation.Y - currentLocation.Y;

                angle = Math.Atan2(dx, dy) / Math.PI * 180; // get the angle in degrees 
                angle = -angle;
            }
            else
            {
                angle = nextLocation.Y - currentLocation.Y >= 0 ? 0 : 180;
            }

            return angle;
        }

        private void RefreshCancellationTokenAsync()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void AerialBackgroundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCancellationTokenAsync();

            _backgroundOverlay.MapType = aerialBackgroundCheckBox.Checked
                ? ThinkGeoCloudRasterMapsMapType.Aerial_V2_X2
                : ThinkGeoCloudRasterMapsMapType.Light_V2_X2;
            _ = _backgroundOverlay.RefreshAsync();
        }

        private void OverviewButton_Click(object sender, EventArgs e)
        {
            _showOverview = !_showOverview;
            if (_showOverview)
            {
                overviewButton.Text = "Tracking Mode";

                RefreshCancellationTokenAsync();

                var boundingBox = _routeLayer.GetBoundingBox();
                var center = boundingBox.GetCenterPoint();

                // Multiply the current scale by 1.5 to zoom out 50%.
                var scale = MapUtil.GetScale(mapView.MapUnit, boundingBox, mapView.MapWidth, mapView.MapHeight) * 1.5;

                _ = mapView.ZoomToAsync(center, scale, 0);
            }
            else
            {
                overviewButton.Text = "Overview Mode";
            }
        }

        private void MapView_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            double currentRotation = mapView.RotationAngle;

            if (Math.Abs(currentRotation - lastRotationAngle) > 0.1) // Change threshold
            {
                lastRotationAngle = currentRotation;
                UpdateCompassImage((float)currentRotation);
            }
        }

        private void VehicleNavigation_SizeChanged(object sender, EventArgs e)
        {
            var x = mapView.Width;
            var y = mapView.Height;

            overviewButton.Location = new System.Drawing.Point(x - 170, y - 60);
            aerialBackgroundCheckBox.Location = new System.Drawing.Point(20, y - 60);
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
                g.DrawImage(image, new System.Drawing.PointF(0, 0));
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

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    mapView?.Dispose(); // Dispose managed resources here
                }
                _disposed = true;  // Dispose unmanaged resources here (if any)
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
       
        private MapView mapView;
        private Button overviewButton;
        private PictureBox compassButton;
        private CheckBox aerialBackgroundCheckBox;
        private double lastRotationAngle = 0;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            overviewButton = new Button();
            compassButton = new PictureBox();
            aerialBackgroundCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Dock = DockStyle.Fill;
            this.mapView.BackColor = System.Drawing.Color.Black;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 0.001;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.TabIndex = 0;
            this.mapView.Controls.Add(this.overviewButton);
            this.Controls.Add(this.aerialBackgroundCheckBox);
            // 
            // overviewButton
            // 
            overviewButton.Text = "Overview Mode";
            overviewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            overviewButton.Size = new System.Drawing.Size(147, 36);
            overviewButton.TabIndex = 11;
            overviewButton.UseVisualStyleBackColor = true;
            overviewButton.Click += OverviewButton_Click;
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
            compassButton.Location = new System.Drawing.Point(1140, 10);
            compassButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(compassButton);
            // 
            // aerialBackgroundCheckBox
            // 
            aerialBackgroundCheckBox.Name = "AerialBackgroundCheckBox";
            aerialBackgroundCheckBox.Text = "Aerial Background";
            aerialBackgroundCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            aerialBackgroundCheckBox.ForeColor = System.Drawing.Color.Black;
            aerialBackgroundCheckBox.BackColor = System.Drawing.Color.LightGray;
            aerialBackgroundCheckBox.Size = new System.Drawing.Size(147, 36);
            aerialBackgroundCheckBox.UseVisualStyleBackColor = true;
            aerialBackgroundCheckBox.CheckedChanged += AerialBackgroundCheckBox_CheckedChanged;
            // 
            // NavigationMap
            // 
            this.Controls.Add(this.mapView);
            this.Controls.Add(overviewButton);
            Name = "VehicleNavigation";
            Size = new System.Drawing.Size(1194, 560);
            Load += Form_Load;
            ResumeLayout(false);
            //
            // Make sure the controls are on top of the mapView
            //
            overviewButton.BringToFront();
            compassButton.BringToFront();
            aerialBackgroundCheckBox.BringToFront();
        }

        #endregion Component Designer generated code
    }
}
