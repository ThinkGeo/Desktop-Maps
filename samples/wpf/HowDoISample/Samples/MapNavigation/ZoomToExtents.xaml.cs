using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Zoom in and out, to a scale, to a layer's extent, to a point and to a rotation, and choose the scales the wheel stops at.
    /// </summary>
    public partial class ZoomToExtents
    {
        private ShapeFileFeatureSource _friscoCityBoundary;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Collection<double> _defaultScales;
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

            var shapefilePath = SampleShared.Shapefile("City_ETJ.shp");

            // The boundary never changes, so it is cut into tiles and drawn with the
            // basemap in one pass rather than by an overlay above it.
            var boundary = new FeatureSourceVectorTileSource();
            boundary.FeatureSources.Add("boundary", new ShapeFileFeatureSource(shapefilePath)
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            });
            var cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, cloud).AddStyle((string)Resources["StyleJson"], boundary));

            // Read a second time, for the extent the zoom buttons target. Nothing draws
            // it, so it is a FeatureSource and not a layer.
            _friscoCityBoundary = new ShapeFileFeatureSource(shapefilePath)
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 180000;
            _defaultScales = Map.ZoomScales;

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

        /// <summary>
        /// Zoom to a scale programmatically. Any scale is reachable - the map is not
        /// confined to the scales in ZoomScales.
        /// </summary>
        private void ZoomToScale_Click(object sender, RoutedEventArgs e)
        {
            _ = Map.ZoomToAsync(Convert.ToDouble(ZoomScale.Text));
        }


        /// <summary>
        /// ZoomScales is where a wheel notch or a double-click comes to rest. The map
        /// still passes through everything in between and ZoomTo can land anywhere;
        /// these are the resting places. Printed maps are drawn at named scales, so a
        /// map made for printing is easier to use when the wheel parks on them.
        /// <para>
        /// Index 0 is the most zoomed OUT, so the ladder runs from the largest scale
        /// down - written the other way round every scale reports zoom 0 and the
        /// wheel indexes from the wrong end.
        /// </para>
        /// <para>
        /// A ladder is also the reachable set: the wheel clamps to its ends, so one
        /// that starts at 1:1,000,000 cannot be zoomed out of. This one runs all the
        /// way from a world view down to a site plan, every rung a scale a map would
        /// actually be printed at.
        /// </para>
        /// </summary>
        private void ZoomScales_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            Map.ZoomScales = PrintScales.IsChecked == true
                ? new Collection<double>
                {
                    100000000, 50000000, 25000000, 10000000, 5000000, 2500000,
                    1000000, 500000, 250000, 100000, 50000, 25000,
                    10000, 5000, 2500, 1000, 500,
                }
                : _defaultScales;
        }

        private void LayerBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            // Nothing draws this data any more, so nothing else opens it either.
            _friscoCityBoundary.Open();
            var friscoCityBoundaryBBox = _friscoCityBoundary.GetBoundingBox();
            _friscoCityBoundary.Close();
            Map.CenterPoint = friscoCityBoundaryBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit,friscoCityBoundaryBBox, Map.MapWidth, Map.MapHeight);
            _ = Map.RefreshAsync();
        }

        private void CenterAt_Click(object sender, RoutedEventArgs e)
        {
            var pointInMercator = ProjectionConverter.Convert(4326, 3857, new PointShape(-96.82, 33.15));
            _ = Map.CenterAtAsync(pointInMercator);
        }

        // Register the dependency property.
        public static readonly DependencyProperty TxtCoordinatesProperty =
            DependencyProperty.Register(
                nameof(TxtCoordinates),
                typeof(string),
                typeof(ZoomToExtents),
                null);

        /// <summary>
        /// Gets or sets the text that represents the coordinates.
        /// This is a bindable property.
        /// </summary>
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

        private void RotationTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var binding = RotationTextBox.GetBindingExpression(TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }
    }
}
