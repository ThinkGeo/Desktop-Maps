using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn to resize the map while preserving the world extent with various map resize modes.
    /// </summary>
    public partial class ResizeTheMap
    {
        private bool _initialized;

        public ResizeTheMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the GpuStyleOverlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.ZoomStep = 0.15;
            Map.MapUnit = GeographyUnit.Meter;

            // Add GpuStyleOverlay as a background overlay
            var gpuStyleOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
            Map.Basemap = gpuStyleOverlay;

            Map.CenterPoint = new PointShape(-10336000, 5260000);
            Map.CurrentScale = 37000000;

            _ = Map.RefreshAsync();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            switch (radioButton.Content)
            {
                case "PreserveScale":
                    Map.MapResizeMode = MapResizeMode.PreserveScale;
                    break;
                case "PreserveScaleAndCenter":
                    Map.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
                    break;
                case "PreserveExtent":
                    Map.MapResizeMode = MapResizeMode.PreserveExtent;
                    break;
            }
        }
    }
}
