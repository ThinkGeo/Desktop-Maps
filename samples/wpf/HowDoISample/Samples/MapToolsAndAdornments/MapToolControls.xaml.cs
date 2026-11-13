using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Turn the map's built-in tools on and off - pan zoom bar, logo, mouse coordinates - and format the coordinates.
    /// </summary>
    public partial class MapToolControls
    {
        private bool _initialized;

        public MapToolControls()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));

            Map.MapTools.Logo.Source = new BitmapImage(new Uri("/Resources/generic-logo.png", UriKind.RelativeOrAbsolute));
            Map.MapTools.Logo.IsEnabled = true;
            Map.MapTools.Logo.Margin = new Thickness(0, 0, 0, 40);

            Map.MapTools.MouseCoordinate.IsEnabled = true;
            Map.MapTools.MouseCoordinate.FontSize = 20;
            Map.MapTools.MouseCoordinate.Projection = new Projection(Projection.GetSphericalMercatorProjString());

            Map.MapTools.PanZoomBar.IsEnabled = true;


            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            _ = Map.RefreshAsync();
        }

        private void CoordinateType_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            Map.MapTools.MouseCoordinate.CustomFormatted -= MouseCoordinate_CustomFormatted;

            switch ((string)((RadioButton)sender).Tag)
            {
                case "DMS":
                    Map.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.DegreesMinutesSeconds;
                    break;
                case "LonLat":
                    Map.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;
                default:
                    Map.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    Map.MapTools.MouseCoordinate.CustomFormatted += MouseCoordinate_CustomFormatted;
                    break;
            }
        }

        private static void MouseCoordinate_CustomFormatted(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            ((MouseCoordinateMapTool)sender).Foreground = new SolidColorBrush(Colors.Black);
            e.Result = $"X: {e.WorldCoordinate.X:N2}, Y: {e.WorldCoordinate.Y:N2}";
        }
    }
}
