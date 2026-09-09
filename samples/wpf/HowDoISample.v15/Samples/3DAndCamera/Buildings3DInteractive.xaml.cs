using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// 3D buildings over midtown Manhattan: the street style carries its own building
    /// extrusion layer, so a tilted camera is all it takes. RIGHT-drag or the bar on the
    /// right tilts; the buildings rise, lean with the perspective and occlude each other.
    /// </summary>
    public partial class Buildings3DInteractive
    {
        private bool _initialized;

        public Buildings3DInteractive()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.TiltInteractionEnabled = true;
            Map.MapTools.TiltBar.IsEnabled = true;
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized) return;

            _initialized = true;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));

            // A tight window over midtown Manhattan - dense tall buildings.
            Map.CurrentExtent = new RectangleShape(-8_236_900, 4_975_700, -8_235_100, 4_974_330);
            await Map.RefreshAsync();
            await Map.TiltToAsync(50, 600);
        }
    }
}
