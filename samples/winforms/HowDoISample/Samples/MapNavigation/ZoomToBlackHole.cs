using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class ZoomToBlackHole : UserControl
    {
        private List<(PointShape centerPoint, double scale)> _zoomingExtents;
        private int _currentPointIndex;
        private CancellationTokenSource _cancellationTokenSource;

        public ZoomToBlackHole()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            mapView.MapUnit = GeographyUnit.Meter;

            // Helper method to create overlays from image layers
            var firstImageOverlay = AddGeoImageOverlay("ImageOverlay0", "Data/Jpg/m87_0.jpg", new PointShape(0, 0), 200000, 50000, 6000, 1.0f);
            AddGeoImageOverlay("ImageOverlay1", "Data/Jpg/m87_1.jpg", new PointShape(960, 3140), 20000, 5000, 3000, 0);
            AddGeoImageOverlay("ImageOverlay2", "Data/Jpg/m87_2.jpg", new PointShape(1026, 3138), 7000, 5000, 200, 0);
            AddGeoImageOverlay("ImageOverlay3", "Data/Jpg/m87_3.jpg", new PointShape(1008, 3140), 1000, 650, 40, 0);
            AddGeoImageOverlay("ImageOverlay4", "Data/Jpg/m87_4.jpg", new PointShape(1019, 3159), 400, 234, 20, 0);
            AddGeoImageOverlay("ImageOverlay5", "Data/Jpg/m87_5.jpg", new PointShape(1005.1, 3152.5), 100, 48, 3, 0);
            AddGeoImageOverlay("ImageOverlay6", "Data/Jpg/m87_6.jpg", new PointShape(1002.3, 3151.8), 40, 20, 1, 0);
            AddGeoImageOverlay("ImageOverlay7", "Data/Jpg/m87_7.jpg", new PointShape(1001.05, 3151.15), 8, 2.2, 0.5, 0);
            AddGeoImageOverlay("ImageOverlay8", "Data/Jpg/m87_8.jpg", new PointShape(1000.775, 3151.0768), 2, 1.34, 0, 0);
            AddGeoImageOverlay("ImageOverlay9", "Data/Jpg/m87_9.jpg", new PointShape(1000.698, 3151.046), 0.7, 0.03, 0, 0);

            _zoomingExtents = GetZoomingExtents();
            mapView.CurrentScaleChanged += MapView_CurrentScaleChanged;
            mapView.ZoomScales = GetDefaultZoomLevelSet();

            // Zoom to the first image overlay's extent
            var firstOverlay = mapView.Overlays["ImageOverlay0"] as LayerOverlay;
            if (firstOverlay != null && firstOverlay.Layers.Count > 0)
            {
                var geoImageLayer = firstOverlay.Layers[0] as GeoImageLayer;
                if (geoImageLayer != null)
                {
                    // Center the map and zoom to the image scale
                    mapView.CenterPoint = geoImageLayer.CenterPoint;
                    mapView.CurrentScale = geoImageLayer.Scale;
                }
            }

            scaleLabel.Text = $"Scale: {mapView.CurrentScale:N2}";
            mapView.CurrentScaleChanged += (s, args) =>
            {
                scaleLabel.Text = $"Scale: {mapView.CurrentScale:N2}";
            };

            _ = mapView.RefreshAsync();
        }

        private async void MapView_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            foreach (var overlay in mapView.Overlays)
            {
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (layerOverlay.Layers.Count == 0)
                    continue;
                if (!(layerOverlay.Layers[0] is GeoImageLayer geoImageLayer))
                    continue;

                double currentScale = mapView.CurrentScale;
                if (currentScale < geoImageLayer.LowerScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }
                if (currentScale > geoImageLayer.UpperScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }

                double upperRatio = 1 - currentScale / geoImageLayer.UpperScale;
                double lowerRatio = currentScale / geoImageLayer.LowerScale;

                if (upperRatio < 0.4)
                    layerOverlay.Opacity = upperRatio * 2.5;
                else if (lowerRatio < 2)
                    layerOverlay.Opacity = lowerRatio / 2;
                else
                    layerOverlay.Opacity = 1;
            }

            await mapView.RefreshAsync();

            System.Diagnostics.Debug.WriteLine($"Current Scale: {mapView.CurrentScale}");

        }

        // Updated AddGeoImageOverlay to return LayerOverlay
        private LayerOverlay AddGeoImageOverlay(string overlayName, string imagePath, PointShape center, double maxVisibleScale, double imageScale, double minVisibleScale, float opacity)
        {
            var imageLayer = new GeoImageLayer(imagePath)
            {
                UpperScale = maxVisibleScale,
                Scale = imageScale,
                LowerScale = minVisibleScale,
                CenterPoint = center,
            };

            var overlay = new LayerOverlay
            {
                Name = overlayName,
                TileType = TileType.SingleTile,
                Opacity = opacity,
                ThrowingExceptionMode = ThrowingExceptionMode.ThrowException
            };

            overlay.Layers.Add(imageLayer);

            // Add using the overlayName as the dictionary key
            mapView.Overlays.Add(overlayName, overlay);

            return overlay;
        }


        private void ZoomToBlackHoleButton_Click(object sender, EventArgs e)
        {
            StopCurrentAnimation();
            _ = ZoomToBlackHoleAsync(_cancellationTokenSource.Token);
        }

        private async Task ZoomToBlackHoleAsync(CancellationToken cancellationToken)
        {
            for (_currentPointIndex = 1; _currentPointIndex < _zoomingExtents.Count; _currentPointIndex++)
            {
                var (centerPoint, scale) = _zoomingExtents[_currentPointIndex];

                try
                {
                    await mapView.ZoomToAsync(centerPoint, scale, 0, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private List<(PointShape CenterPoint, double Scale)> GetZoomingExtents()
        {
            var zoomingExtents = new List<(PointShape CenterPoint, double Scale)>();

            var firstLayer = (GeoImageLayer)((LayerOverlay)mapView.Overlays[0]).Layers[0];
            zoomingExtents.Add((firstLayer.CenterPoint, firstLayer.Scale));

            for (var i = 1; i < mapView.Overlays.Count; i++)
            {
                var overlay = mapView.Overlays[i];
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (layerOverlay.Layers.Count == 0)
                    continue;
                var geoImageLayer = (GeoImageLayer)layerOverlay.Layers[0];
                if (geoImageLayer == null)
                    continue;
                zoomingExtents.Add((geoImageLayer.CenterPoint, geoImageLayer.UpperScale));
            }

            var lastLayer = (GeoImageLayer)((LayerOverlay)mapView.Overlays[mapView.Overlays.Count - 1]).Layers[0];
            zoomingExtents.Add((lastLayer.CenterPoint, lastLayer.Scale));
            return zoomingExtents;
        }

        private Collection<double> GetDefaultZoomLevelSet()
        {
            if (_zoomingExtents == null)
                _zoomingExtents = GetZoomingExtents();

            var zoomLevelSet = new Collection<double>();
            foreach (var zoomingExtent in _zoomingExtents)
                zoomLevelSet.Add(zoomingExtent.scale);

            return zoomLevelSet;
        }

        private void StopCurrentAnimation()
        {
            // _cancellationTokenSource.CancelAsync stops all the Async Methods which are using _cancellationTokenSource.Token
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        #region Component Designer generated code

        private Button zoomToBlackHoleButton;
        private Label scaleLabel;
        private MapView mapView;

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
            this.zoomToBlackHoleButton.Click += ZoomToBlackHoleButton_Click;
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
            Load += Form_Load;
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
