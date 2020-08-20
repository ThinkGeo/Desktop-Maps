using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace CacheGenerator
{
    class LayerProvider
    {
        // You can insert your layer to "layersToCache" in this function, they will be used for cache.
        public static Collection<Layer> GetLayersToCache()
        {
            Collection<Layer> layersToCache = new Collection<Layer>();

            ShapeFileFeatureLayer layer = new ShapeFileFeatureLayer(@"..\..\App_Data\cntry02.shp", GeoFileReadWriteMode.Read);
            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            layersToCache.Add(layer);

            return layersToCache;
        }

        // You can insert your scales to "scalesToCache" in this function, they will be used for cache.
        public static Collection<double> GetScalesToCache()
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();

            Collection<double> scalesToCache = new Collection<double>();
            
            scalesToCache.Add(591657550.5);
            scalesToCache.Add(295828775.3);
            scalesToCache.Add(147914387.6);
            scalesToCache.Add(73957193.82);
            scalesToCache.Add(36978596.91);
            scalesToCache.Add(18489298.45);
            scalesToCache.Add(9244649.227);
            scalesToCache.Add(4622324.614);
            scalesToCache.Add(2311162.307);
            scalesToCache.Add(1155581.153);
            scalesToCache.Add(577790.576699999);
            scalesToCache.Add(288895.288400004);
            scalesToCache.Add(144447.644199997);
            scalesToCache.Add(72223.8220900015);
            scalesToCache.Add(36111.9110399974);
            scalesToCache.Add(18055.9555200035);
            scalesToCache.Add(9027.97776099859);
            scalesToCache.Add(4513.98888000326);
            scalesToCache.Add(2256.99443999686);
            scalesToCache.Add(1128.4972200032);

            return scalesToCache;
        }
    }
}
