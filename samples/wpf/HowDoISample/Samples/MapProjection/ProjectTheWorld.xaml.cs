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
        private bool _initialized;

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
            layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add("world overlay", layerOverlay);

            var wvtServerUri = "https://tiles.preludemaps.com/styles/Savannah_Light_v4/style.json";
            _worldLayer = new MvtTilesAsyncLayer(wvtServerUri);
            _worldLayer.ProjectionConverter = new ProjectionConverter(3857, 4326);

            // Add the layer to the overlay we created earlier.
            layerOverlay.Layers.Add("world layer", _worldLayer);

            MapView.CenterPoint = new PointShape(5.92651, 14.58364);
            MapView.CurrentScale = 147648000;

            _initialized = true;
            _ = MapView.RefreshAsync();
        }

        private MvtTilesAsyncLayer _worldLayer;

        private void Radial_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            var radioButton = (RadioButton)sender;

            switch (radioButton.Content)
            {
                // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.

                case "Decimal Degrees":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, 4326);
                    _worldLayer.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.DecimalDegree;
                    MapView.CenterPoint = new PointShape(5.92651, 14.58364);
                    MapView.CurrentScale = 147648000;
                    break;
                case "MGA Zone 55":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString1);
                    _worldLayer.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CenterPoint = new PointShape(-945060, 7010600);
                    MapView.CurrentScale = 18489350;
                    break;
                case "Equal Area - Albers Conic":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString2);
                    _worldLayer.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CenterPoint = new PointShape(-107570, -246560);
                    MapView.CurrentScale = 36978690;
                    break;
                case "Polar Stereographic":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString3);
                    _worldLayer.ProjectionConverter.Open();
                    MapView.MapUnit = GeographyUnit.Meter;
                    MapView.CenterPoint = new PointShape(176160, -818530);
                    MapView.CurrentScale = 73957380;
                    break;
                case "Equal Area - Cylindrical":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString4);
                    _worldLayer.ProjectionConverter.Open();
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