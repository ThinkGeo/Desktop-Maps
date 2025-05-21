using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class VehicleNavigation : UserControl
    {
        private Collection<Vertex> _gpsPoints;
        private readonly List<Vertex> _visitedVertices = new List<Vertex>();
        private int _currentGpsPointIndex;
        private Marker _vehicleMarker;
        private bool _disposed;
        private bool _showOverview;

        private CancellationTokenSource _cancellationTokenSource;
        private ThinkGeoCloudRasterMapsOverlay _backgroundOverlay;
        private LayerOverlay _routesOverlay;
        private SimpleMarkerOverlay _markerOverlay;
        private InMemoryFeatureLayer _routeLayer;
        private InMemoryFeatureLayer _visitedRoutesLayer;

        private const double DefaultScale = 5000;

        public VehicleNavigation()
        {
            InitializeComponent();
            this.Load += Form_Load;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get the parent form
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.FormClosing += ParentForm_FormClosing;
            }
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._disposed = true;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
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

            _gpsPoints = CollectGpsData();

            _routeLayer = InitRouteLayerFromGpsPoints(_gpsPoints);
            _visitedRoutesLayer = new InMemoryFeatureLayer();
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle =
                LineStyle.CreateSimpleLineStyle(GeoColors.Green, 6, true);
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _routesOverlay = new LayerOverlay();
            _routesOverlay.Layers.Add("route", _routeLayer);
            _routesOverlay.Layers.Add("visited", _visitedRoutesLayer);
            mapView.Overlays.Add(_routesOverlay);

            // Marker Overlay
            _markerOverlay = new SimpleMarkerOverlay();
            _vehicleMarker = new Marker(new PointShape(_gpsPoints[0].X, _gpsPoints[0].Y))
            {
                ImageSource = new BitmapImage(new Uri("/Resources/vehicle_location.png", UriKind.RelativeOrAbsolute)),
                Width = 24,
                Height = 24
            };
            _markerOverlay.Markers.Add(_vehicleMarker);
            mapView.Overlays.Add(_markerOverlay);

            //mapView.CurrentExtentChanged += MapView_CurrentExtentChanged;

            mapView.CenterPoint = new PointShape(_gpsPoints[0]);
            mapView.CurrentScale = DefaultScale;

            // Defer animation trigger until control is fully loaded
            this.BeginInvoke(new Action(() =>
            {
                _routeLayer.Open(); 
                var fromExtent = mapView.CurrentExtent;
                var toExtent = _routeLayer.GetBoundingBox();
                _routeLayer.Close(); 
                AnimateTo(fromExtent, toExtent);
            }));

            _ = ZoomToGpsPointsAsync(_gpsPoints);
        }

        private void AnimateTo(RectangleShape fromExtent, RectangleShape toExtent, int durationMs = 1000)
        {
            int steps = 20;
            int currentStep = 0;
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = durationMs / steps;

            double fromResolution = GetResolution(fromExtent);
            double toResolution = GetResolution(toExtent);

            timer.Tick += (s, e) =>
            {
                currentStep++;
                double progress = (double)currentStep / steps;

                // Interpolate center
                var fromCenter = fromExtent.GetCenterPoint();
                var toCenter = toExtent.GetCenterPoint();
                var currentCenter = new PointShape(
                    fromCenter.X + (toCenter.X - fromCenter.X) * progress,
                    fromCenter.Y + (toCenter.Y - fromCenter.Y) * progress);

                // Interpolate resolution
                double currentResolution = fromResolution + (toResolution - fromResolution) * progress;
                var currentExtent = GetExtentFromCenterAndResolution(currentCenter, currentResolution);

                mapView.CurrentExtent = currentExtent;
                mapView.Refresh();

                // Call your method like WPF does
                UpdateRoutesAndMarker(progress);

                if (currentStep >= steps)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }

        private double GetResolution(RectangleShape extent)
        {
            return extent.Width / mapView.Width;
        }

        private RectangleShape GetExtentFromCenterAndResolution(PointShape center, double resolution)
        {
            double width = resolution * mapView.Width;
            double height = resolution * mapView.Height;

            return new RectangleShape(
                center.X - width / 2, center.Y + height / 2,
                center.X + width / 2, center.Y - height / 2);
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
                    await Task.Delay(1000);
                }

                if (_disposed)
                    break;
            }
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

            // Remove old marker if needed
            _markerOverlay.Markers.Clear();

            // Re-create the marker
            _vehicleMarker = new Marker(new PointShape(x, y))
            {
                ImageSource = new BitmapImage(new Uri("/Resources/vehicle_location.png", UriKind.RelativeOrAbsolute)),
                RotateAngle = angle,
                Width = 24,
                Height = 24
            };

            // Add it back
            _markerOverlay.Markers.Add(_vehicleMarker);

            _ = _markerOverlay.RefreshAsync();
            Application.DoEvents(); 
            Task.Delay(500);   
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

                //UpdateRoutesAndMarker(0, angle);
                await mapView.ZoomToAsync(centerPoint, DefaultScale, angle, cancellationToken);
            }
        }

        private static InMemoryFeatureLayer InitRouteLayerFromGpsPoints(Collection<Vertex> gpsPoints)
        {
            var lineShape = new LineShape();
            foreach (var point in gpsPoints)
                lineShape.Vertices.Add(point);

            var layer = new InMemoryFeatureLayer();
            layer.InternalFeatures.Add(new Feature(lineShape));
            layer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Yellow, 6, true);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return layer;
        }

        private static Collection<Vertex> CollectGpsData()
        {
            var gpsPoints = new Collection<Vertex>();
            var lines = File.ReadAllLines(@"./Data/Csv/vehicle-route.csv");

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

        private static double GetRotationAngle(int currentIndex, IReadOnlyList<Vertex> gpsPoints)
        {
            Vertex current = gpsPoints[currentIndex < gpsPoints.Count - 1 ? currentIndex : currentIndex - 1];
            Vertex next = gpsPoints[currentIndex < gpsPoints.Count - 1 ? currentIndex + 1 : currentIndex];

            double angle = Math.Atan2(next.X - current.X, next.Y - current.Y) * 180 / Math.PI;
            return -angle;
        }

        private void RefreshCancellationToken()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        #region Component Designer generated code

        private Button zoomToBlackHoleButton;
        private Label scaleLabel;
        private MapView mapView;
        private PictureBox northArrowPictureBox;
        private CheckBox aerialBackgroundCheckBox;
        private Button overviewButton;
        //private Timer rotationTimer;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            zoomToBlackHoleButton = new Button();
            scaleLabel = new Label();
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
            this.mapView.Controls.Add(this.zoomToBlackHoleButton);
            // 
            // zoomToBlackHoleButton
            // 
            zoomToBlackHoleButton.Text = "Zoom To M87 Black Hole";
            zoomToBlackHoleButton.Location = new System.Drawing.Point(550, 670);
            zoomToBlackHoleButton.Size = new System.Drawing.Size(147, 36);
            zoomToBlackHoleButton.TabIndex = 11;
            zoomToBlackHoleButton.UseVisualStyleBackColor = true;
            //this.zoomToBlackHoleButton.Click += ZoomToBlackHoleButton_Click;
            // 
            // scaleLabel
            // 
            scaleLabel.ForeColor = System.Drawing.Color.Blue;
            scaleLabel.Font = new System.Drawing.Font("Segoe UI", 12);
            scaleLabel.AutoSize = true;
            scaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            scaleLabel.Anchor = AnchorStyles.Top;
            scaleLabel.Location = new System.Drawing.Point(550, 20);
            scaleLabel.Visible = true;
            scaleLabel.Text = "Scale: 0.00";
            // 
            // NavigationMap
            // 
            this.Controls.Add(this.mapView);
            this.Controls.Add(zoomToBlackHoleButton);
            this.Controls.Add(scaleLabel);
            Name = "ZoomToBlackHole";
            Size = new System.Drawing.Size(1194, 560);
            //Load += Form_Load;
            ResumeLayout(false);
            //
            // Make sure the controls are on top of the mapView
            //
            zoomToBlackHoleButton.BringToFront();
            scaleLabel.BringToFront();
        }

        #endregion Component Designer generated code
    }
}
