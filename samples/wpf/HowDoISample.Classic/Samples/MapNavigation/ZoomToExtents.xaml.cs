using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Zoom in and out, to a scale, to a layer's extent, to a point and to a rotation.
    /// </summary>
    public partial class ZoomToExtents : IDisposable
    {
        private ShapeFileFeatureLayer _friscoCityBoundary;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _isSyncingRotationAngle;
        private bool _initialized;

        public ZoomToExtents()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.CurrentExtentChanged += Map_CurrentExtentChanged;
            Map.RotationAngleChanging += Map_RotationAngleChanging;

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _friscoCityBoundary = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "City_ETJ.shp"))
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(16, GeoColors.Blue), GeoColors.DimGray, 2);
            _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(_friscoCityBoundary);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 180000;

            _ = Map.RefreshAsync();
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            _ = Map.ZoomInAsync();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            _ = Map.ZoomOutAsync();
        }

        private void ZoomToScale_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ZoomScale.Text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var scale) || scale <= 0) return;

            _ = Map.ZoomToAsync(scale);
        }

        private void LayerBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            var friscoCityBoundaryBBox = _friscoCityBoundary.GetBoundingBox();
            Map.CenterPoint = friscoCityBoundaryBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, friscoCityBoundaryBBox, Map.MapWidth, Map.MapHeight);
            _ = Map.RefreshAsync();
        }

        private void CenterAt_Click(object sender, RoutedEventArgs e)
        {
            var pointInMercator = ProjectionConverter.Convert(4326, 3857, new PointShape(-96.82, 33.15));
            _ = Map.CenterAtAsync(pointInMercator);
        }

        public static readonly DependencyProperty TxtCoordinatesProperty =
            DependencyProperty.Register(
                nameof(TxtCoordinates),
                typeof(string),
                typeof(ZoomToExtents),
                null);

        public string TxtCoordinates
        {
            get => (string)GetValue(TxtCoordinatesProperty);
            set => SetValue(TxtCoordinatesProperty, value);
        }

        private void Map_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var currentExtent = e.NewExtent ?? Map.CurrentExtent;
            if (currentExtent == null) return;

            var center = currentExtent.GetCenterPoint();
            var centerInDecimalDegrees = ProjectionConverter.Convert(3857, 4326, center);
            TxtCoordinates = $"Center Point: (Lat: {centerInDecimalDegrees.Y:N4}, Lon: {centerInDecimalDegrees.X:N4})";
        }

        private void Map_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            _isSyncingRotationAngle = true;
            RotateAngle.Value = Math.Round(e.NewRotationAngle, MidpointRounding.AwayFromZero);
            _isSyncingRotationAngle = false;
        }

        private async void RotateAngle_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isSyncingRotationAngle || !IsLoaded) return;

            var centerPoint = Map.CurrentExtent?.GetCenterPoint() ?? Map.CenterPoint;
            if (centerPoint == null) return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Map.ZoomToAsync(centerPoint, Map.CurrentScale, RotateAngle.Value, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void RotationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var binding = RotationTextBox.GetBindingExpression(TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }

        public void Dispose()
        {
            Map.CurrentExtentChanged -= Map_CurrentExtentChanged;
            Map.RotationAngleChanging -= Map_RotationAngleChanging;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
