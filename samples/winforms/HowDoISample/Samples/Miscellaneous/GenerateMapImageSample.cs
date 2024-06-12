using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GenerateMapImageSample : UserControl
    {
        public GenerateMapImageSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Collection<Layer> layersToDraw = new Collection<Layer>();

            // Create the background world maps using vector tiles stored locally in our MBTiles file and also set the styling though a json file
            ThinkGeoMBTilesLayer mbTilesLayer = new ThinkGeoMBTilesLayer(@"./Data/Mbtiles/Frisco.mbtiles", new Uri(@"./Data/Json/thinkgeo-world-streets-light.json", UriKind.Relative));
            mbTilesLayer.Open();
            layersToDraw.Add(mbTilesLayer);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            ShapeFileFeatureLayer zoningLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Zoning.shp");
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
            canvas.BeginDrawing(geoImage, MapUtil.GetDrawingExtent(zoningLayer.GetBoundingBox(), 800, 600), GeographyUnit.Meter);

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

            // Create a new Bitmap using the stream as it's source
            Bitmap bitmap = new Bitmap(imageStream);

            // Set the source of the PictureBox to the image
            MapImage.Image = bitmap;
        }

        #region Component Designer generated code

        private PictureBox MapImage;
        private void InitializeComponent()
        {
            this.MapImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MapImage)).BeginInit();
            this.SuspendLayout();
            // 
            // MapImage
            // 
            this.MapImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapImage.Location = new System.Drawing.Point(0, 0);
            this.MapImage.Name = "MapImage";
            this.MapImage.Size = new System.Drawing.Size(1208, 682);
            this.MapImage.TabIndex = 0;
            this.MapImage.TabStop = false;
            // 
            // GenerateMapImageSample
            // 
            this.Controls.Add(this.MapImage);
            this.Name = "GenerateMapImageSample";
            this.Size = new System.Drawing.Size(1208, 682);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MapImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}