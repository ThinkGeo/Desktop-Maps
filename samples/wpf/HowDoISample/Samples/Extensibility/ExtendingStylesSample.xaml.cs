using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class ExtendingStylesSample : UserControl, IDisposable
    {
        public ExtendingStylesSample()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer worldCapitalsLayer = new ShapeFileFeatureLayer(@".\Data\Shapefile\WorldCapitals.shp");
            worldCapitalsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldCapitals",worldCapitalsLayer);
            mapView.Overlays.Add("Overlay",worldOverlay);

            mapView.CurrentExtent = new RectangleShape(-15360785.1188513, 14752615.1010077, 16260907.558937, -12603279.9259404);

            await mapView.RefreshAsync();
        }
        

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private async void TimeBasedPointStyle_Click(object sender, RoutedEventArgs e)
        {
            FeatureLayer worldCapitalsLayer = mapView.FindFeatureLayer("WorldCapitals");

            TimeBasedPointStyle timeBasedPointStyle = new TimeBasedPointStyle();
            timeBasedPointStyle.TimeZoneColumnName = "TimeZone";
            timeBasedPointStyle.DaytimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Yellow, 12, GeoColors.Black);
            timeBasedPointStyle.NighttimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Gray, 12, GeoColors.Black);

            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(timeBasedPointStyle);

            await mapView.RefreshAsync();
        }

        private async void SizedBasedPointStyle_Click(object sender, RoutedEventArgs e)
        {
            FeatureLayer worldCapitalsLayer = mapView.FindFeatureLayer("WorldCapitals");

            SizedPointStyle sizedpointStyle = new SizedPointStyle(PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 1), "population", 500000);

            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(sizedpointStyle);

            await mapView.RefreshAsync();
        }
    }

    // This style draws points on the capitols with their color based on the current time and if
    // we think it's daylight there or not.
    class TimeBasedPointStyle : ThinkGeo.Core.Style
    {
        private PointStyle daytimePointStyle;
        private PointStyle nighttimePointStyle;
        private string timeZoneColumnName;

        public TimeBasedPointStyle()
            : this(string.Empty, new PointStyle(), new PointStyle())
        { }

        public TimeBasedPointStyle(string timeZoneColumnName, PointStyle daytimePointStyle, PointStyle nighttimePointStyle)
        {
            this.timeZoneColumnName = timeZoneColumnName;
            this.daytimePointStyle = daytimePointStyle;
            this.nighttimePointStyle = nighttimePointStyle;
        }

        public PointStyle DaytimePointStyle
        {
            get { return daytimePointStyle; }
            set { daytimePointStyle = value; }
        }

        public PointStyle NighttimePointStyle
        {
            get { return nighttimePointStyle; }
            set { nighttimePointStyle = value; }
        }

        public string TimeZoneColumnName
        {
            get { return timeZoneColumnName; }
            set { timeZoneColumnName = value; }
        }


        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (Feature feature in features)
            {
                // Here we are going to do the calculation to see what
                // time it is for each feature and draw the appropriate style
                float offsetToGmt = Convert.ToSingle(feature.ColumnValues[timeZoneColumnName]);
                DateTime localTime = DateTime.UtcNow.AddHours(offsetToGmt);
                if (localTime.Hour >= 7 && localTime.Hour <= 19)
                {
                    // Daytime
                    daytimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                }
                else
                {
                    //Nighttime

                    nighttimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                }
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            Collection<string> columns = new Collection<string>();

            // Grab any columns that the daytime style may need.
            Collection<string> daytimeColumns = daytimePointStyle.GetRequiredColumnNames();
            foreach (string column in daytimeColumns)
            {
                if (!columns.Contains(column))
                {
                    columns.Add(column);
                }
            }

            // Grab any columns that the nighttime style may need.
            Collection<string> nighttimeColumns = nighttimePointStyle.GetRequiredColumnNames();
            foreach (string column in nighttimeColumns)
            {
                if (!columns.Contains(column))
                {
                    columns.Add(column);
                }
            }

            // Make sure we add the timezone column
            if (!columns.Contains(timeZoneColumnName))
            {
                columns.Add(timeZoneColumnName);
            }

            return columns;
        }
    }

    // This style draws a point sized with the population of the capitol.  It uses the DrawCore of the style
    // to draw directly on the canvas.  It can also leverage other styles to draw on the canvas as well.
    class SizedPointStyle : ThinkGeo.Core.Style
    {
        private PointStyle pointStyle;
        private float ratio;
        private string sizeColumnName;

        public SizedPointStyle()
            : this(new PointStyle(), string.Empty, 1)
        { }

        public SizedPointStyle(PointStyle pointStyle, string sizeColumnName, float ratio)
        {
            this.pointStyle = pointStyle;
            this.sizeColumnName = sizeColumnName;
            this.ratio = ratio;
        }

        public PointStyle PointStyle
        {
            get { return pointStyle; }
            set { pointStyle = value; }
        }

        public float Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }

        public string SizeColumnName
        {
            get { return sizeColumnName; }
            set { sizeColumnName = value; }
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            // Loop through each feature and determine how large the point should 
            // be then adjust it's size.
            foreach (Feature feature in features)
            {
                float sizeData = Convert.ToSingle(feature.ColumnValues[sizeColumnName]);
                float symbolSize = sizeData / ratio;
                pointStyle.SymbolSize = symbolSize;
                pointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
            }
        }        

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            // Here we grab the columns from the pointStyle and then add
            // the sizeColumn name to make sure we pull back the column
            //  that we need to calculate the size
            Collection<string> columns = new Collection<string>();
            columns = pointStyle.GetRequiredColumnNames();
            if (!columns.Contains(sizeColumnName))
            {
                columns.Add(sizeColumnName);
            }

            return columns;
        }
    }

}