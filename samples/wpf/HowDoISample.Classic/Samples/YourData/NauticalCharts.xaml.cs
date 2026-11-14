using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show an S-57 electronic nautical chart.
    /// </summary>
    public partial class NauticalCharts : IDisposable
    {
        private bool _initialized;

        public NauticalCharts()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.DecimalDegree;

            // The chart layer draws all of its data only as a single tile.
            var chartOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            chartOverlay.Layers.Add(new NauticalChartsFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "S57", "US1GC09M", "US1GC09M.000")));
            Map.Overlays.Add(chartOverlay);

            Map.CenterPoint = new PointShape(-82.30999, 24.89084);
            Map.CurrentScale = 1156400;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
