using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to style line data using a LineStyle
    /// </summary>
    public partial class CreateLineStyleSample : UserControl, IDisposable
    {
        public CreateLineStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, load Frisco Railroad shapefile data and add it to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10779675.1746605, 3914631.77546835, -10779173.5566652, 3914204.80300804);

            // Create a layer with line data
            ShapeFileFeatureLayer friscoRailroad = new ShapeFileFeatureLayer(@"./Data/Railroad/Railroad.shp");
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project the layer's data to match the projection of the map
            friscoRailroad.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            layerOverlay.Layers.Add("Railroad", friscoRailroad);

            // Add the overlay to the map
            mapView.Overlays.Add("overlay", layerOverlay);

            rbLineStyle.IsChecked = true;

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private void rbLineType_Checked(object sender, RoutedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["overlay"];
                ShapeFileFeatureLayer friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

                // Create a line style
                var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 10), new GeoPen(GeoBrushes.WhiteSmoke, 6));

                // Add the line style to the collection of custom styles for ZoomLevel 1.
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
                friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Refresh the layerOverlay to show the new style
                layerOverlay.Refresh();
            }
        }

        private void rbDashedLineType_Checked(object sender, RoutedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["overlay"];
                ShapeFileFeatureLayer friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

                var lineStyle = new LineStyle(
                    outerPen: new GeoPen(GeoColors.Black, 12),
                    innerPen: new GeoPen(GeoColors.White, 6)
                    {
                        DashStyle = LineDashStyle.Custom,
                        DashPattern = { 3f, 3f },
                        StartCap = DrawingLineCap.Flat,
                        EndCap = DrawingLineCap.Flat
                    }
                );

                // Add the line style to the collection of custom styles for ZoomLevel 1.
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
                friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Refresh the layerOverlay to show the new style
                layerOverlay.Refresh();
            }
        }

    }
}

