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
            scalesToCache.Add(zoomLevelSet.ZoomLevel01.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel02.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel03.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel04.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel05.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel06.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel07.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel08.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel09.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel10.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel11.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel12.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel13.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel14.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel15.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel16.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel17.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel18.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel19.Scale);
            scalesToCache.Add(zoomLevelSet.ZoomLevel20.Scale);

            return scalesToCache;
        }
    }
}
