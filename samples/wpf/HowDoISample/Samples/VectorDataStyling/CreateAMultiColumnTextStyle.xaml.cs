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

        private bool _initialized;
        public CreateAMultiColumnTextStyle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, project and add styles to the Hotels, Streets, and Parks layer.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.DecimalDegree;

            Map.Background = Brushes.DodgerBlue;

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
            Map.Overlays.Add(layerOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(15.05, 46.85);
            Map.CurrentScale = 18496580;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}