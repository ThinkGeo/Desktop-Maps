using System;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to label data using a TextStyle
    /// </summary>
    public partial class CreateAMultiColumnTextStyle : IDisposable
    {
        public CreateAMultiColumnTextStyle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, project and add styles to the Hotels, Streets, and Parks layer.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.DecimalDegree;

                MapView.Background = Brushes.DodgerBlue;

                // Create a layer with polygon data
                var countries02Layer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Countries02.shp");
                countries02Layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                    AreaStyle.CreateSimpleAreaStyle(GeoColors.SandyBrown, GeoColors.Black);

                var textStyle = new TextStyle
                {
                    TextContent = "{CNTRY_NAME}: " + Environment.NewLine + " Population:{POP_CNTRY}",
                    Font = new GeoFont("Arial", 10),
                    TextBrush = GeoBrushes.Black
                };

                countries02Layer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
                countries02Layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add layers to a layerOverlay
                var layerOverlay = new LayerOverlay();
                layerOverlay.Layers.Add(countries02Layer);

                // Add overlay to map
                MapView.Overlays.Add(layerOverlay);

                // Set the map extent
                MapView.CenterPoint = new PointShape(15.05,46.85);
                MapView.CurrentScale = 18496580;

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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