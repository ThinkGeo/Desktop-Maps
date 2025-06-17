using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to programmatically zoom, pan, and rotate the map control.
    /// </summary>
    public partial class ZoomToBlackHole : UserControl
    {
        private List<(PointShape centerPoint, double scale)> _zoomingExtents;
        private int _currentPointIndex;
        private CancellationTokenSource _cancellationTokenSource;
        private BackgroundLayer _blackBackgroundLayer;
        private LayerOverlay _backgroundOverlay;

        public ZoomToBlackHole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            mapView.DefaultAnimationSettings.Duration = 2000;
            mapView.DefaultAnimationSettings.Type = MapAnimationType.DrawWithAnimation;

            mapView.MapUnit = GeographyUnit.Meter;

            AddBlackBackgroundOverlay();

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

            defaultExtentButton.Click += (s, args) => 
            {
                StopCurrentAnimation();

                // Add the background overlay only if it's not already added
                if (!mapView.Overlays.Contains(_backgroundOverlay))
                {
                    AddBlackBackgroundOverlay();
                }

                _ = mapView.ZoomToAsync(_zoomingExtents[0].centerPoint, _zoomingExtents[0].scale, 0, _cancellationTokenSource.Token);
            };

            _ = mapView.RefreshAsync();
        }

        private void AddBlackBackgroundOverlay()
        {
            _blackBackgroundLayer = new BackgroundLayer(new GeoSolidBrush(GeoColors.Black));
            _backgroundOverlay = new LayerOverlay();
            _backgroundOverlay.Layers.Add("BlackBackground", _blackBackgroundLayer);

            // Make it the bottom-most overlay
            mapView.Overlays.Insert(0, _backgroundOverlay);
        }

        private void MapView_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            foreach (var overlay in mapView.Overlays)
            {
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (!(layerOverlay.Layers[0] is GeoImageLayer geoImageLayer))
                    continue;
                if (mapView.CurrentScale < geoImageLayer.LowerScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }
                if (mapView.CurrentScale > geoImageLayer.UpperScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }

                var upperRatio = 1 - mapView.CurrentScale / geoImageLayer.UpperScale;
                var lowerRatio = mapView.CurrentScale / geoImageLayer.LowerScale;

                if (upperRatio < 0.4)
                    layerOverlay.Opacity = upperRatio * 2.5;
                else if (lowerRatio < 2)
                    layerOverlay.Opacity = lowerRatio / 2;
                else
                    layerOverlay.Opacity = 1;
            }
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
            for (_currentPointIndex = 2; _currentPointIndex < _zoomingExtents.Count; _currentPointIndex++)
            {
                var (centerPoint, scale) = _zoomingExtents[_currentPointIndex];

                if (_currentPointIndex >= 10)
                {
                    mapView.Overlays.Remove(_backgroundOverlay);
                }

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

            var firstLayer = (GeoImageLayer)((LayerOverlay)mapView.Overlays[1]).Layers[0];
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
        private PictureBox defaultExtentButton;
        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            zoomToBlackHoleButton = new Button();
            scaleLabel = new Label();
            defaultExtentButton = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)defaultExtentButton).BeginInit();
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
            // 
            // zoomToBlackHoleButton
            // 
            zoomToBlackHoleButton.BackColor = Color.DarkGray;
            zoomToBlackHoleButton.Font = new Font("Segoe UI", 14F);
            zoomToBlackHoleButton.Location = new Point(470, 500);
            zoomToBlackHoleButton.Name = "zoomToBlackHoleButton";
            zoomToBlackHoleButton.Size = new Size(230, 35);
            zoomToBlackHoleButton.Text = "Zoom To M87 Black Hole";
            zoomToBlackHoleButton.UseVisualStyleBackColor = true;
            zoomToBlackHoleButton.Click += ZoomToBlackHoleButton_Click;
            zoomToBlackHoleButton.Anchor = AnchorStyles.Bottom;
            zoomToBlackHoleButton.Left = mapView.Width / 2 + 70;
            zoomToBlackHoleButton.TabIndex = 0;
            // 
            // scaleLabel
            // 
            scaleLabel.Anchor = AnchorStyles.Top;
            scaleLabel.AutoSize = true;
            scaleLabel.BackColor = Color.Black;
            scaleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            scaleLabel.ForeColor = Color.White;
            scaleLabel.Location = new Point(490, 20);
            scaleLabel.Name = "scaleLabel";
            scaleLabel.Size = new Size(115, 30);
            scaleLabel.Text = "Scale: 0.00";
            scaleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // defaultExtentButton
            // 
            defaultExtentButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            defaultExtentButton.BackColor = Color.Transparent;
            defaultExtentButton.Image = Properties.Resources.icon_globe_black;
            defaultExtentButton.Location = new Point(1140, 10);
            defaultExtentButton.Name = "defaultExtentButton";
            defaultExtentButton.Size = new Size(40, 40);
            defaultExtentButton.SizeMode = PictureBoxSizeMode.StretchImage;
            defaultExtentButton.TabStop = false;
            System.Drawing.Drawing2D.GraphicsPath pathOfDefaultExtentButton = new System.Drawing.Drawing2D.GraphicsPath();
            pathOfDefaultExtentButton.AddEllipse(0, 0, defaultExtentButton.Width, defaultExtentButton.Height);
            defaultExtentButton.Region = new Region(pathOfDefaultExtentButton);
            defaultExtentButton.TabIndex = 1;
            // 
            // ZoomToBlackHole
            // 
            Controls.Add(mapView);
            Controls.Add(defaultExtentButton);
            Controls.Add(zoomToBlackHoleButton);
            Controls.Add(scaleLabel);
            Name = "ZoomToBlackHole";
            Size = new Size(1194, 560);
            Load += Form_Load;
            ((System.ComponentModel.ISupportInitialize)defaultExtentButton).EndInit();
            ResumeLayout(false);
            PerformLayout();
            //
            // Make sure the controls are on top of the mapView
            //
            zoomToBlackHoleButton.BringToFront();
            scaleLabel.BringToFront();
            defaultExtentButton.BringToFront();
        }

        #endregion Component Designer generated code
    }
}
