using System;
using System.Collections.Generic;
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

        public ZoomToBlackHole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            MapView.DefaultAnimationSettings.Duration = 2000;
            _zoomingExtents = GetZoomingExtents();

            MapView.CurrentScaleChanged += MapView_CurrentScaleChanged;

            MapView.DefaultAnimationSettings.Type = MapAnimationType.DrawWithAnimation;
            
            // stop the auto zooming whenever touching the map
            MapView.MapDoubleClick += MapView_MapDoubleClick;
            MapView.ZoomLevelSet = GetDefaultZoomLevelSet();

            _ = MapView.RefreshAsync();
        }

        private void MapView_MapDoubleClick(object sender, MapClickMapViewEventArgs e)
        {
            StopCurrentAnimation();
        }
        
        private void MapView_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            foreach (var overlay in MapView.Overlays)
            {
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (!(layerOverlay.Layers[0] is GeoImageLayer geoImageLayer))
                    continue;
                if (MapView.CurrentScale < geoImageLayer.LowerScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }
                if (MapView.CurrentScale > geoImageLayer.UpperScale)
                {
                    layerOverlay.Opacity = 0;
                    continue;
                }

                var upperRatio = 1 - MapView.CurrentScale / geoImageLayer.UpperScale;
                var lowerRatio = MapView.CurrentScale / geoImageLayer.LowerScale;

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


        private async Task ZoomToBlackHoleAsync(CancellationToken cancellationToken)
        {
            for (_currentPointIndex = 1; _currentPointIndex < _zoomingExtents.Count; _currentPointIndex++)
            {
                var (centerPoint, scale) = _zoomingExtents[_currentPointIndex];

                try
                {
                    await MapView.ZoomToAsync(centerPoint, scale, 0, cancellationToken: cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private void DefaultExtentButton_Click(object sender, RoutedEventArgs e)
        {
            StopCurrentAnimation();

            try
            {
                _ = MapView.ZoomToAsync(_zoomingExtents[0].centerPoint, _zoomingExtents[0].scale, 0,
                    _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            { }
        }

        private List<(PointShape CenterPoint, double Scale)> GetZoomingExtents()
        {
            var zoomingExtents = new List<(PointShape CenterPoint, double Scale)>();

            var firstLayer = (GeoImageLayer)((LayerOverlay)MapView.Overlays[0]).Layers[0];
            zoomingExtents.Add((firstLayer.CenterPoint, firstLayer.Scale));

            for (var i = 1; i < MapView.Overlays.Count; i++)
            {
                var overlay = MapView.Overlays[i];
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (layerOverlay.Layers.Count < 0)
                    continue;
                var geoImageLayer = (GeoImageLayer)layerOverlay.Layers[0];
                if (geoImageLayer == null)
                    continue;
                zoomingExtents.Add((geoImageLayer.CenterPoint, geoImageLayer.UpperScale));
            }

            var lastLayer = (GeoImageLayer)((LayerOverlay)MapView.Overlays[MapView.Overlays.Count - 1]).Layers[0];
            zoomingExtents.Add((lastLayer.CenterPoint, lastLayer.Scale));
            return zoomingExtents;
        }

        private ZoomLevelSet GetDefaultZoomLevelSet()
        {
            var zoomLevelSet = new ZoomLevelSet();

            foreach (var overlay in MapView.Overlays)
            {
                if (!(overlay is LayerOverlay layerOverlay))
                    continue;
                if (!(layerOverlay.Layers[0] is GeoImageLayer geoImageLayer))
                    continue;

                var zoomLevel = new ZoomLevel(geoImageLayer.Scale);
                zoomLevelSet.CustomZoomLevels.Add(zoomLevel);
            }

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
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}