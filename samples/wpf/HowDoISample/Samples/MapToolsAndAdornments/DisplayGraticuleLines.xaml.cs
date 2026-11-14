using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Display graticule lines - a latitude/longitude grid labelled at the edges of the view.
    /// </summary>
    public partial class DisplayGraticuleLines
    {

        private bool _initialized;
        public DisplayGraticuleLines()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));
            Map.Basemap = basemap;

            // The graticule is computed in EPSG:4326; the projection tells it the map is in EPSG:3857.
            var graticuleLayer = new GraticuleAdornmentLayer
            {
                Projection = new Projection(3857)
            };
            graticuleLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            // On the adornment overlay the grid is redrawn on every pan tick and the degree
            // labels stay pinned to the map's edges.
            Map.AdornmentOverlay.Layers.Add("graticule", graticuleLayer);

            Map.CenterPoint = new PointShape(-10777200, 3911500);
            Map.CurrentScale = 36200;

            _ = Map.RefreshAsync();
        }
    }
}