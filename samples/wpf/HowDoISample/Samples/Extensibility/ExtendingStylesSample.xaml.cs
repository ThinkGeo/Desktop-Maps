using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class ExtendingStylesSample : IDisposable
    {
        public ExtendingStylesSample()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var worldCapitalsLayer = new ShapeFileFeatureLayer(@".\Data\Shapefile\WorldCapitals.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(4326, 3857)
                    }
            };
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldCapitals", worldCapitalsLayer);
            MapView.Overlays.Add("Overlay", worldOverlay);

            MapView.CurrentExtent = new RectangleShape(-15360785.1188513, 14752615.1010077, 16260907.558937, -12603279.9259404);

            await MapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private async void TimeBasedPointStyle_Click(object sender, RoutedEventArgs e)
        {
            var worldCapitalsLayer = MapView.FindFeatureLayer("WorldCapitals");

            var timeBasedPointStyle = new TimeBasedPointStyle
            {
                TimeZoneColumnName = "TimeZone",
                DaytimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Yellow, 12, GeoColors.Black),
                NighttimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Gray, 12, GeoColors.Black)
            };

            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(timeBasedPointStyle);

            await MapView.RefreshAsync();
        }

        private async void SizedBasedPointStyle_Click(object sender, RoutedEventArgs e)
        {
            var worldCapitalsLayer = MapView.FindFeatureLayer("WorldCapitals");

            var sizedpointStyle = new SizedPointStyle(PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 1), "population", 500000);

            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(sizedpointStyle);

            await MapView.RefreshAsync();
        }
    }

    // This style draws points on the capitols with their color based on the current time and if
    // we think it's daylight there or not.
    internal class TimeBasedPointStyle : Core.Style
    {
        public TimeBasedPointStyle()
            : this(string.Empty, new PointStyle(), new PointStyle())
        { }

        public TimeBasedPointStyle(string timeZoneColumnName, PointStyle daytimePointStyle, PointStyle nighttimePointStyle)
        {
            TimeZoneColumnName = timeZoneColumnName;
            DaytimePointStyle = daytimePointStyle;
            NighttimePointStyle = nighttimePointStyle;
        }

        public PointStyle DaytimePointStyle { get; set; }

        public PointStyle NighttimePointStyle { get; set; }

        public string TimeZoneColumnName { get; set; }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                // Here we are going to do the calculation to see what
                // time it is for each feature and draw the appropriate style
                var offsetToGmt = Convert.ToSingle(feature.ColumnValues[TimeZoneColumnName]);
                var localTime = DateTime.UtcNow.AddHours(offsetToGmt);
                if (localTime.Hour >= 7 && localTime.Hour <= 19)
                {
                    // Daytime
                    DaytimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                }
                else
                {
                    //Nighttime
                    NighttimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                }
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            var columns = new Collection<string>();

            // Grab any columns that the daytime style may need.
            var daytimeColumns = DaytimePointStyle.GetRequiredColumnNames();
            foreach (var column in daytimeColumns)
            {
                if (!columns.Contains(column))
                {
                    columns.Add(column);
                }
            }

            // Grab any columns that the nighttime style may need.
            var nighttimeColumns = NighttimePointStyle.GetRequiredColumnNames();
            foreach (var column in nighttimeColumns)
            {
                if (!columns.Contains(column))
                {
                    columns.Add(column);
                }
            }

            // Make sure we add the timezone column
            if (!columns.Contains(TimeZoneColumnName))
            {
                columns.Add(TimeZoneColumnName);
            }

            return columns;
        }
    }

    // This style draws a point sized with the population of the capitol.  It uses the DrawCore of the style
    // to draw directly on the canvas.  It can also leverage other styles to draw on the canvas as well.
    internal class SizedPointStyle : Core.Style
    {
        public SizedPointStyle()
            : this(new PointStyle(), string.Empty, 1)
        { }

        public SizedPointStyle(PointStyle pointStyle, string sizeColumnName, float ratio)
        {
            PointStyle = pointStyle;
            SizeColumnName = sizeColumnName;
            Ratio = ratio;
        }

        public PointStyle PointStyle { get; set; }

        public float Ratio { get; set; }

        public string SizeColumnName { get; set; }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            // Loop through each feature and determine how large the point should 
            // be then adjust it's size.
            foreach (var feature in features)
            {
                var sizeData = Convert.ToSingle(feature.ColumnValues[SizeColumnName]);
                var symbolSize = sizeData / Ratio;
                PointStyle.SymbolSize = symbolSize;
                PointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            // Here we grab the columns from the pointStyle and then add
            // the sizeColumn name to make sure we pull back the column
            //  that we need to calculate the size
            var columns = PointStyle.GetRequiredColumnNames();
            if (!columns.Contains(SizeColumnName))
            {
                columns.Add(SizeColumnName);
            }
            return columns;
        }
    }
}