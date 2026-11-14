using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Save the map as a PNG: a snapshot of what is on screen, or layers drawn into an image off screen.
    /// </summary>
    public partial class ExportAnImage : IDisposable
    {
        private bool _initialized;

        public ExportAnImage()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            Map.CenterPoint = new PointShape(-10779270, 3911750);
            Map.CurrentScale = 288900;

            var markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.Markers.Add(new Marker(Map.CenterPoint));
            Map.Overlays.Add(markerOverlay);

            _ = Map.RefreshAsync();
        }

        private async void Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "PNG image|*.png",
                    FileName = "map.png",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                };
                if (saveFileDialog.ShowDialog() != true) return;

                var image = OffScreen.IsChecked == true
                    ? await DrawLayersOffScreenAsync(Map.CurrentExtent, (int)Map.MapWidth, (int)Map.MapHeight)
                    : Map.GetSnapshot();
                using (image)
                {
                    image.Save(saveFileDialog.FileName);
                }

                var preview = new BitmapImage();
                preview.BeginInit();
                preview.CacheOption = BitmapCacheOption.OnLoad;
                preview.UriSource = new Uri(saveFileDialog.FileName);
                preview.EndInit();
                Preview.Source = preview;
                Status.Text = "Saved " + saveFileDialog.FileName;
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        /// <summary>
        /// Draws layers into a GeoImage with a GeoCanvas: no map control is involved.
        /// </summary>
        private static async Task<GeoImage> DrawLayersOffScreenAsync(RectangleShape extent, int width, int height)
        {
            var basemapLayer = new ThinkGeoRasterMapsAsyncLayer(SampleKeys.ClientId, SampleKeys.ClientSecret);

            // The zoning data is in Texas North Central (2276); the image is drawn in Spherical Mercator (3857).
            var zoningLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Zoning.shp"));
            zoningLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoBrushes.Blue));
            zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var image = new GeoImage(width, height);
            var canvas = GeoCanvas.CreateDefaultGeoCanvas();
            canvas.BeginDrawing(image, extent, GeographyUnit.Meter);

            // The labels already placed, so a later layer's labels do not collide with them.
            var labels = new Collection<SimpleCandidate>();

            await basemapLayer.OpenAsync();
            await basemapLayer.DrawAsync(canvas, labels);
            await basemapLayer.CloseAsync();
            canvas.Flush();

            zoningLayer.Open();
            zoningLayer.Draw(canvas, labels);
            zoningLayer.Close();
            canvas.Flush();

            canvas.EndDrawing();
            return image;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
