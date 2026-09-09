using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Any XYZ tile server on the map: point <see cref="XyzRasterTileSource"/> at a
    /// {z}/{x}/{y} URL template and add it - OpenStreetMap stands in for "your server".
    /// </summary>
    public partial class XyzTileServer
    {
        private bool _initialized;

        public XyzTileServer()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Any {z}/{x}/{y} template works here - this one happens to be OpenStreetMap,
            // whose tile policy asks every application for a descriptive user agent.
            var source = new XyzRasterTileSource("https://tile.openstreetmap.org/{z}/{x}/{y}.png")
            {
                UserAgent = "ThinkGeo HowDoI Sample (support@thinkgeo.com)",
            };

            // One call puts the service on the map - no style JSON is written or needed.
            Map.Basemap = new GpuBasemap(new MapStyle().AddRaster(source, "xyz"));

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 50000;
            _ = Map.RefreshAsync();
        }
    }
}