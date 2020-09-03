using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.DisplayingLayers
{
    /// <summary>
    /// Interaction logic for GenerateMapImageSample.xaml
    /// </summary>
    public partial class GenerateMapImageSample : UserControl
    {
        public GenerateMapImageSample()
        {
            InitializeComponent();
        }

        private void MapImage_Loaded(object sender, RoutedEventArgs e)
        {            
            Collection<Layer> layersToDraw = new Collection<Layer>();

            // Create a background layer just for the background color and add it to the layers to draw collection
            BackgroundLayer background = new BackgroundLayer(GeoBrushes.WhiteSmoke);
            background.Open();
            layersToDraw.Add(background);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            ShapeFileFeatureLayer zoningLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Zoning.shp");
            zoningLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            zoningLayer.Open();

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoBrushes.Blue));
            zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;            
            layersToDraw.Add(zoningLayer);

            // Create a GeoCanvas to do the drawing
            GeoCanvas canvas = GeoCanvas.CreateDefaultGeoCanvas();

            // Create a GeoImage as the image to draw on
            GeoImage geoImage = new GeoImage(800, 600);
            
            // Start the drawing by specifying the image, extent and map units
            canvas.BeginDrawing(geoImage, MapUtil.GetDrawingExtent(zoningLayer.GetBoundingBox(), 800, 600),GeographyUnit.Meter);

            // This collection is used during drawing to pass labels in between layers so we can track collisions
            Collection<SimpleCandidate> labels = new Collection<SimpleCandidate>();

            // Loop through all the layers and draw them to the GeoCanvas
            // The flush is to compact styles that use different drawing levels
            foreach (var layer in layersToDraw)
            {
                layer.Draw(canvas, labels);
                canvas.Flush();
            }

            // End drawing, we can now use the GeoImage
            canvas.EndDrawing();

            // Create a memory stream and save the GeoImage as a standard PNG formatted image
            MemoryStream imageStream = new MemoryStream();
            geoImage.Save(imageStream, GeoImageFormat.Png);

            // Create a new ImageBitmap using the stream as it's source
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = imageStream;
            bitmapImage.EndInit();

            // Set the source of the image control to the BitmapImage
            MapImage.Source = bitmapImage;
        }
    }
}
