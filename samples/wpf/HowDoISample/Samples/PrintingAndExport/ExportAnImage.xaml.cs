using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Save the map as a PNG: a snapshot of what is on screen, or layers drawn into an image off screen.
    /// </summary>
    public partial class ExportAnImage
    {
        private bool _initialized;
        private bool _offScreen;

        public ExportAnImage()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));

            Map.CenterPoint = new PointShape(-10779270, 3911750);
            Map.CurrentScale = 288900;

            var markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.Markers.Add(new Marker(Map.CenterPoint));
            Map.Overlays.Add(markerOverlay);

            _ = Map.RefreshAsync();
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _offScreen = (string)((RadioButton)sender).Tag == "offscreen";
            ModeHint.Text = _offScreen
                ? "The ThinkGeo raster basemap and Frisco's zoning drawn straight into an image with a GeoCanvas, at the map's extent and size. Nothing on screen is copied."
                : "Everything on the map, the marker included, copied into an image the size of the map.";
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

                var extent = Map.CurrentExtent;
                var image = _offScreen
                    ? await DrawLayersOffScreenAsync(extent, (int)Map.MapWidth, (int)Map.MapHeight)
                    : Map.GetSnapshot();
                image.Save(saveFileDialog.FileName);

                if (WriteWorldFile.IsChecked == true)
                {
                    WriteWorldAndProjectionFiles(saveFileDialog.FileName, extent, image.Width, image.Height);
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
            var basemapLayer = new ThinkGeoRasterMapsAsyncLayer(SampleShared.ClientId, SampleShared.ClientSecret);

            // The zoning data is in Texas North Central (2276); the image is drawn in Spherical Mercator (3857).
            var zoningLayer = new ShapeFileFeatureLayer(SampleShared.Shapefile("Zoning.shp"));
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

        /// <summary>
        /// The world file places the pixels: map units per pixel, then the world position of
        /// the center of the top-left pixel. With the .prj naming the projection, the PNG opens
        /// in place in any GIS.
        /// </summary>
        private static void WriteWorldAndProjectionFiles(string imagePath, RectangleShape extent, int width, int height)
        {
            var pixelWidth = extent.Width / width;
            var pixelHeight = extent.Height / height;
            File.WriteAllLines(Path.ChangeExtension(imagePath, ".pgw"), new[]
            {
                pixelWidth.ToString(CultureInfo.InvariantCulture),
                "0",
                "0",
                (-pixelHeight).ToString(CultureInfo.InvariantCulture),
                (extent.UpperLeftPoint.X + pixelWidth / 2).ToString(CultureInfo.InvariantCulture),
                (extent.UpperLeftPoint.Y - pixelHeight / 2).ToString(CultureInfo.InvariantCulture),
            });
            File.WriteAllText(Path.ChangeExtension(imagePath, ".prj"), Projection.ConvertEpsgToWkt(3857));
        }
    }
}
