using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Showcase how to toggle and customize multiple map tools at once.
    /// </summary>
    public partial class MapToolControls : IDisposable
    {
        private bool _initialized;

        public MapToolControls()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
                return;

            MapView.MapUnit = GeographyUnit.Meter;

            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            MapView.MapTools.Logo.Source = new BitmapImage(new Uri(@"..\..\..\Resources\generic-logo.png", UriKind.RelativeOrAbsolute));
            MapView.MapTools.Logo.IsEnabled = true;
            MapView.MapTools.Logo.Margin = new Thickness(0, 0, 0, 40);

            MapView.MapTools.MouseCoordinate.IsEnabled = true;
            MapView.MapTools.MouseCoordinate.FontSize = 20;
            MapView.MapTools.MouseCoordinate.Projection = new Projection(Projection.GetSphericalMercatorProjString());

            MapView.MapTools.PanZoomBar.IsEnabled = true;

            MapView.MapTools.ScaleLine.IsEnabled = true;

            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;

            _initialized = true;

            _ = MapView.RefreshAsync();
        }

        private void CoordinateType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized) return;

            MapView.MapTools.MouseCoordinate.CustomFormatted -= MouseCoordinate_CustomMouseCoordinateFormat;

            var selectedValue = ((ComboBoxItem)CoordinateType.SelectedItem).Content.ToString();
            switch (selectedValue)
            {
                case "DMS":
                    MapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.DegreesMinutesSeconds;
                    break;
                case "Lat, Lon":
                    MapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;
                default:
                    MapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    MapView.MapTools.MouseCoordinate.CustomFormatted += MouseCoordinate_CustomMouseCoordinateFormat;
                    break;
            }
        }

        /// <summary>
        /// Event handler that formats the MouseCoordinates to use WorldCoordinates.
        /// </summary>
        private static void MouseCoordinate_CustomMouseCoordinateFormat(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            ((MouseCoordinateMapTool)sender).Foreground = new SolidColorBrush(Colors.Black);
            e.Result = $"X: {e.WorldCoordinate.X:N2}, Y: {e.WorldCoordinate.Y:N2}";
        }

        public void Dispose()
        {
            MapView.MapTools.MouseCoordinate.CustomFormatted -= MouseCoordinate_CustomMouseCoordinateFormat;

            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
