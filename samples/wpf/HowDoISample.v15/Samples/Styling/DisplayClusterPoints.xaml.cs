using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Clustering as markers: the sightings are bucketed on a screen-pixel grid for
    /// the current view, one clickable marker per bucket. Clusters are a property of
    /// the VIEW, so they are rebuilt when the view settles instead of being drawn
    /// into tiles - which is also what makes them clickable.
    /// </summary>
    public partial class DisplayClusterPoints
    {
        /// <summary>Cluster cell size, in screen pixels.</summary>
        private const double CellPixels = 64;

        private readonly SimpleMarkerOverlay _markers = new SimpleMarkerOverlay();

        // A drag or zoom fires the extent event continuously; recluster once it lands.
        private readonly DispatcherTimer _recluster = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(150) };

        private List<PointShape> _sightings;
        private bool _initialized;

        public DisplayClusterPoints()
        {
            InitializeComponent();
            _recluster.Tick += (_, _) =>
            {
                _recluster.Stop();
                if (IsLoaded)
                {
                    Recluster();
                }
            };
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));

            _sightings = ReadSightings();
            Map.Overlays.Add(_markers);

            Map.CenterPoint = new PointShape(-10780320, 3915120);
            Map.CurrentScale = 288900;
            Map.CurrentExtentChanged += (_, _) => { _recluster.Stop(); _recluster.Start(); };

            Recluster();
            _ = Map.RefreshAsync();
        }

        private void Recluster()
        {
            var worldPerPixel = Map.CurrentExtent.Width / Math.Max(1, Map.MapWidth);
            var cell = CellPixels * worldPerPixel;

            var buckets = new Dictionary<(int Column, int Row), List<PointShape>>();
            foreach (var sighting in _sightings)
            {
                var key = ((int)Math.Floor(sighting.X / cell), (int)Math.Floor(sighting.Y / cell));
                if (!buckets.TryGetValue(key, out var bucket))
                {
                    buckets[key] = bucket = new List<PointShape>();
                }

                bucket.Add(sighting);
            }

            _markers.Markers.Clear();
            foreach (var bucket in buckets.Values)
            {
                var center = new PointShape(bucket.Average(p => p.X), bucket.Average(p => p.Y));
                _markers.Markers.Add(bucket.Count == 1 ? SightingMarker(center) : ClusterMarker(center, bucket));
            }

            _ = _markers.RefreshAsync();
        }

        private static Marker SightingMarker(PointShape at) => new Marker(at)
        {
            ImageSource = null,
            Content = new Ellipse
            {
                Width = 12,
                Height = 12,
                Fill = new SolidColorBrush(Color.FromRgb(0xA0, 0x52, 0x2D)),
                Stroke = Brushes.White,
                StrokeThickness = 2,
            },
            ToolTip = "one sighting",
        };

        private Marker ClusterMarker(PointShape at, List<PointShape> members)
        {
            var size = Math.Min(56, 26 + (Math.Sqrt(members.Count) * 4));
            var disc = new Grid { Width = size, Height = size, Cursor = System.Windows.Input.Cursors.Hand };
            disc.Children.Add(new Ellipse
            {
                Fill = new SolidColorBrush(Color.FromArgb(0xD8, 0x2E, 0x6E, 0xA6)),
                Stroke = Brushes.White,
                StrokeThickness = 2,
            });
            disc.Children.Add(new TextBlock
            {
                Text = members.Count.ToString(System.Globalization.CultureInfo.InvariantCulture),
                Foreground = Brushes.White,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            });

            disc.MouseLeftButtonDown += (_, e) =>
            {
                e.Handled = true;
                ZoomInto(members);
            };

            return new Marker(at)
            {
                ImageSource = null,
                Content = disc,
                ToolTip = FormattableString.Invariant($"{members.Count} sightings - click to zoom in"),
            };
        }

        private void ZoomInto(List<PointShape> members)
        {
            var minX = members.Min(p => p.X);
            var maxX = members.Max(p => p.X);
            var minY = members.Min(p => p.Y);
            var maxY = members.Max(p => p.Y);

            // A cluster of near-coincident points has no area to zoom to; step the
            // scale instead.
            if (maxX - minX < 1 && maxY - minY < 1)
            {
                Map.CenterPoint = new PointShape((minX + maxX) / 2, (minY + maxY) / 2);
                Map.CurrentScale /= 4;
            }
            else
            {
                var pad = Math.Max(maxX - minX, maxY - minY) * 0.3;
                Map.CurrentExtent = new RectangleShape(minX - pad, maxY + pad, maxX + pad, minY - pad);
            }

            _ = Map.RefreshAsync();
        }

        private static List<PointShape> ReadSightings()
        {
            var source = new ShapeFileFeatureSource(SampleShared.Shapefile("Frisco_Coyote_Sightings.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };

            source.Open();
            var features = source.GetAllFeatures(ReturningColumnsType.NoColumns);
            source.Close();

            return features.Select(f => f.GetShape()).OfType<PointShape>().ToList();
        }
    }
}
