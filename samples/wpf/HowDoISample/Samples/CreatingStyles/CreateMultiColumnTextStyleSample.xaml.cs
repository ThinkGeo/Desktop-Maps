using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to label data using a TextStyle
    /// </summary>
    public partial class CreateMultiColumnTextStyleSample : UserControl, IDisposable
    {
        public CreateMultiColumnTextStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, project and add styles to the Hotels, Streets, and Parks layer.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            mapView.Background = Brushes.DodgerBlue;

            // Create a layer with polygon data
            var countries02Layer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Countries02.shp");
            countries02Layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                AreaStyle.CreateSimpleAreaStyle(GeoColors.SandyBrown, GeoColors.Black);

            var textStyle = new TextStyle();
            textStyle.TextContent = "{CNTRY_NAME}: " + Environment.NewLine + " Population:{POP_CNTRY}";
            textStyle.Font = new GeoFont("Arial", 10);
            textStyle.TextBrush = GeoBrushes.Black;

            countries02Layer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
            countries02Layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add layers to a layerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(countries02Layer);

            // Add overlay to map
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-8.70, 62.60, 38.81, 31.11);
            await mapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
