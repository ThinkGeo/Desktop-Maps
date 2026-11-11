using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Fly with ZoomToAsync from galaxy M87 down to the shadow of its black hole, through ten pictures nested inside each other.
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

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            _cancellationTokenSource = new CancellationTokenSource();

            foreach (var overlay in Map.Overlays)
            {
                if (overlay is LayerOverlay layerOverlay && layerOverlay.Layers[0] is GeoImageLayer geoImageLayer)
                    geoImageLayer.ImagePathFilename = Path.Combine(AppContext.BaseDirectory, geoImageLayer.ImagePathFilename);
            }

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

        private async void DefaultExtentButton_Click(object sender, RoutedEventArgs e)
        {
            StopCurrentAnimation();
            try
            {
                await Map.ZoomToAsync(_zoomingExtents[0].centerPoint, _zoomingExtents[0].scale, 0, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
            }
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
                catch (OperationCanceledException)
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
                if (!(Map.Overlays[i] is LayerOverlay layerOverlay))
                    continue;
                if (layerOverlay.Layers.Count == 0)
                    continue;
                if (!(layerOverlay.Layers[0] is GeoImageLayer geoImageLayer))
                    continue;
                zoomingExtents.Add((geoImageLayer.CenterPoint, geoImageLayer.UpperScale));
            }

            var lastLayer = (GeoImageLayer)((LayerOverlay)Map.Overlays[Map.Overlays.Count - 1]).Layers[0];
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
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
