using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to programmatically zoom, pan, and rotate the map control.
    /// </summary>
    public partial class ZoomToBlackHole : IDisposable
    {
        private List<(PointShape centerPoint, double scale)> _zoomingExtents;
        private int _currentPointIndex;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _initialized;

        public ZoomToBlackHole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            _cancellationTokenSource = new CancellationTokenSource();

            Map.DefaultAnimationSettings.Duration = 2000;
            Map.DefaultAnimationSettings.Type = MapAnimationType.DrawWithAnimation;

            _zoomingExtents = GetZoomingExtents();

            Map.CurrentScaleChanged += Map_CurrentScaleChanged;
            Map.ZoomScales = GetDefaultZoomLevelSet();

            _ = Map.RefreshAsync();
        }

        private void Map_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            foreach (var overlay in Map.Overlays)
            {
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (!(layerOverlay.Layers[0] is GeoImageLayer geoImageLayer))
                    continue;
                if (Map.CurrentScale < geoImageLayer.LowerScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }
                if (Map.CurrentScale > geoImageLayer.UpperScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }

                var upperRatio = 1 - Map.CurrentScale / geoImageLayer.UpperScale;
                var lowerRatio = Map.CurrentScale / geoImageLayer.LowerScale;

                if (upperRatio < 0.4)
                    layerOverlay.Opacity = upperRatio * 2.5;
                else if (lowerRatio < 2)
                    layerOverlay.Opacity = lowerRatio / 2;
                else
                    layerOverlay.Opacity = 1;
            }
        }

        private void ZoomToBlackHoleButton_Click(object sender, RoutedEventArgs e)
        {
            StopCurrentAnimation();
            _ = ZoomToBlackHoleAsync(_cancellationTokenSource.Token);
        }

        private void DefaultExtentButton_Click(object sender, RoutedEventArgs e)
        {
            StopCurrentAnimation();
            _ = Map.ZoomToAsync(_zoomingExtents[0].centerPoint, _zoomingExtents[0].scale, 0, _cancellationTokenSource.Token);
        }

        private async Task ZoomToBlackHoleAsync(CancellationToken cancellationToken)
        {
            for (_currentPointIndex = 1; _currentPointIndex < _zoomingExtents.Count; _currentPointIndex++)
            {
                var (centerPoint, scale) = _zoomingExtents[_currentPointIndex];

                try
                {
                    await Map.ZoomToAsync(centerPoint, scale, 0, cancellationToken);
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

            var firstLayer = (GeoImageLayer)((LayerOverlay)Map.Overlays[0]).Layers[0];
            zoomingExtents.Add((firstLayer.CenterPoint, firstLayer.Scale));

            for (var i = 1; i < Map.Overlays.Count; i++)
            {
                var overlay = Map.Overlays[i];
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (layerOverlay.Layers.Count < 0)
                    continue;
                var geoImageLayer = (GeoImageLayer)layerOverlay.Layers[0];
                if (geoImageLayer == null)
                    continue;
                zoomingExtents.Add((geoImageLayer.CenterPoint, geoImageLayer.UpperScale));
            }

            var lastLayer = (GeoImageLayer)((LayerOverlay)Map.Overlays[Map.Overlays.Count - 1]).Layers[0];
            zoomingExtents.Add((lastLayer.CenterPoint, lastLayer.Scale));
            return zoomingExtents;
        }

        private Collection<double> GetDefaultZoomLevelSet()
        {
            if (_zoomingExtents != null)
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

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}

