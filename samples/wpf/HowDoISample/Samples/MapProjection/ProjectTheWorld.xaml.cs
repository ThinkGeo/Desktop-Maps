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
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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

            MapView.CurrentExtent = new RectangleShape(-139.971925820039, 140.267236484135, 151.824949179961, -111.099951015865);
            RdoPolar.IsChecked = true;

            await MapView.RefreshAsync();
        }

        private async void Radial_Checked(object sender, RoutedEventArgs e)
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
                    MapView.CurrentExtent = new RectangleShape(-139.971925820039, 140.267236484135, 151.824949179961, -111.099951015865);
                    break;
                case "MGA Zone 55":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=utm +zone=55 +south +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs");
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CurrentExtent = new RectangleShape(-4415962.270035205, 10196887.263572674, 3690059.470408367, 3223755.308540492);
                    break;
                case "Albers Equal Area Conic":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=aea +lat_1=20 +lat_2=60 +lat_0=40 +lon_0=-96 +x_0=0 +y_0=0 +ellps=GRS80 +datum=NAD83 +units=m no_defs");
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CurrentExtent = new RectangleShape(-4167908.28780145, 3251198.24423971, 3952761.55210086, -3744318.54555566);
                    break;
                case "Polar Stereographic":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=stere +lat_0=90 +lat_ts=70 +lon_0=-45 +k=1 +x_0=0 +y_0=0 +a=6378273 +b=6356889.449 +units=m +no_defs s");
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CurrentExtent = new RectangleShape(-7944508.96033433, 6176987.61570341, 8296830.7194703, -7814045.96388732);
                    break;
                case "Equal Area Cylindrical":
                    // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.
                    layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=cea +lon_0=0 +x_0=0 +y_0=0 +lat_ts=45 +ellps=WGS84 +datum=WGS84 +units=m +no_defs");
                    layer.FeatureSource.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CurrentExtent = new RectangleShape(-17364804.1443055, 14762778.3057369, 15059282.8174688, -13168814.679406);
                    break;
            }

            await MapView.RefreshAsync();
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