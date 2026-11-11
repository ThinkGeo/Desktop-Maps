using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Choose what the map keeps when it is resized: its scale, its scale and center, or its extent.
    /// </summary>
    public partial class ResizeTheMap : IDisposable
    {
        private bool _initialized;

        public ResizeTheMap()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

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

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
