using System.Collections.ObjectModel;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public static class MapViewHelper
    {
        /// <summary>
        /// Initializes a MapView with the standard ThinkGeo zoom scales.
        /// </summary>
        /// <param name="mapView">The MapView instance to initialize.</param>
        public static void InitializeDefaultZoomScales(MapView mapView)
        {
            if (mapView == null) return;

            mapView.ZoomScales = new Collection<double>
            {
                73957377, 36978688, 18489344, 9244672,
                4622336, 2311168, 1155584, 577792,
                288896, 144448, 72224, 36112,
                18056, 9028, 4514, 2257, 1128, 564, 282, 141
            };
        }
    }
}
