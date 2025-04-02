using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class ProjectTheWorld : IDisposable
    {
        public ProjectTheWorld()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.DecimalDegree;

            // Set the background of the map to a water color
            MapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#42B2EE"));

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            MapView.Overlays.Add("world overlay", layerOverlay);

            // Create the world layer, it will be decimal degrees at first, but we will be able to change it
            var worldLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Countries02.shp");

            // Set up the styles for the countries for zoom level 1 and then apply it until zoom level 20
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.DarkSlateGray;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromHtml("#C9E1BE"));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to the overlay we created earlier.
            layerOverlay.Layers.Add("world layer", worldLayer);

            MapView.CenterPoint = new PointShape(5.92651, 14.58364);
            MapView.CurrentScale = 147648000;
            RdoPolar.IsChecked = true;

            _ = MapView.RefreshAsync();
        }

        private void Radial_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            var layer = MapView.FindFeatureLayer("world layer");

            if (layer == null) return;
            switch (radioButton.Content)
            {
                case "Decimal Degrees":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 4326);
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.DecimalDegree;
                    MapView.CenterPoint = new PointShape(5.92651, 14.58364);
                    MapView.CurrentScale = 147648000;
                    break;
                case "MGA Zone 55":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString1);
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CenterPoint = new PointShape(-362950, 6710320);
                    MapView.CurrentScale = 36912000;
                    break;
                case "Albers Equal Area Conic":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString2);
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CenterPoint = new PointShape(-107570, -246560);
                    MapView.CurrentScale = 36978690;
                    break;
                case "Polar Stereographic":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString3);
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CenterPoint = new PointShape(176160, -818530);
                    MapView.CurrentScale = 73957380;
                    break;
                case "Equal Area Cylindrical":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString4);
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CenterPoint = new PointShape(-1152760, 796980);
                    MapView.CurrentScale = 147648000;
                    break;
            }

            _ = MapView.RefreshAsync();
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