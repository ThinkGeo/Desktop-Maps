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
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            Map.MapTools.Logo.Source = new BitmapImage(new Uri(@"..\..\..\Resources\generic-logo.png", UriKind.RelativeOrAbsolute));
            Map.MapTools.Logo.IsEnabled = true;
            Map.MapTools.Logo.Margin = new Thickness(0, 0, 0, 40);

            Map.MapTools.MouseCoordinate.IsEnabled = true;
            Map.MapTools.MouseCoordinate.FontSize = 20;
            Map.MapTools.MouseCoordinate.Projection = new Projection(Projection.GetSphericalMercatorProjString());

            Map.MapTools.PanZoomBar.IsEnabled = true;

            Map.MapTools.ScaleLine.IsEnabled = true;

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            _initialized = true;

            _ = Map.RefreshAsync();
        }

        private void CoordinateType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized) return;

            Map.MapTools.MouseCoordinate.CustomFormatted -= MouseCoordinate_CustomMouseCoordinateFormat;

            var selectedValue = ((ComboBoxItem)CoordinateType.SelectedItem).Content.ToString();
            switch (selectedValue)
            {
                case "DMS":
                    Map.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.DegreesMinutesSeconds;
                    break;
                case "Lat, Lon":
                    Map.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;
                default:
                    Map.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    Map.MapTools.MouseCoordinate.CustomFormatted += MouseCoordinate_CustomMouseCoordinateFormat;
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
            Map.MapTools.MouseCoordinate.CustomFormatted -= MouseCoordinate_CustomMouseCoordinateFormat;

            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
