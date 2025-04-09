using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMS Layer on the map
    /// </summary>
    public partial class WMS : IDisposable
    {
        public WMS()
        {
            InitializeComponent();
        }

        private WmsAsyncLayer wms;
        private ThinkGeoRasterMapsAsyncLayer _thinkGeoRasterMapsAsyncLayer;
        private RectangleShape australiaBBox = new RectangleShape(14114144.61573416, 5195304.319841703, 16392171.878052138, 3442348.4809069675);
        
        /// <summary>
        /// Add the WMS layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;

            // Add Cloud Maps as a background overlay
            _thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
            };

            layerOverlay.Layers.Add(_thinkGeoRasterMapsAsyncLayer);
            MapView.Overlays.Add(layerOverlay);

            wms = new WmsAsyncLayer(new Uri("http://geo.vliz.be/geoserver/Dataportal/ows?service=WMS&"));
            wms.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wms.Parameters.Add("LAYERS", "eurobis_grid_15m-obisenv");
            wms.Parameters.Add("STYLES", "generic");
            wms.OutputFormat = "image/png";
            wms.Crs = "EPSG:3857";  // Coordinate system, typically EPSG:3857 for WMS with Spherical Mercator
            //wms.Transparency = 100;

            // Extent of Australia 
            MapView.CenterPoint = australiaBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, australiaBBox, MapView.MapWidth, MapView.MapHeight);

            var layerOverlay2 = new LayerOverlay();
            layerOverlay2.Opacity = 0.5;
            layerOverlay2.TileType = TileType.SingleTile;
            layerOverlay2.Layers.Add(wms);
            MapView.Overlays.Add(layerOverlay2);
            _ = MapView.RefreshAsync();
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (wms == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        wms.ProjectionConverter = null;
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = null;
                        MapView.CenterPoint = australiaBBox.GetCenterPoint();
                        MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, australiaBBox, MapView.MapWidth, MapView.MapHeight);
                        break;

                    case "3112":
                        wms.ProjectionConverter = new GdalProjectionConverter(3857, 6669);
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 6669);
                        var projectionAustraliaBBox = ProjectionConverter.Convert(3857, 6669, australiaBBox);
                        MapView.CenterPoint = projectionAustraliaBBox.GetCenterPoint();
                        MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, projectionAustraliaBBox, MapView.MapWidth, MapView.MapHeight);
                        break;

                    default:
                        return;
                }

                await wms.CloseAsync();
                await _thinkGeoRasterMapsAsyncLayer.CloseAsync();
                await wms.OpenAsync();
                await _thinkGeoRasterMapsAsyncLayer.OpenAsync();

                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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