using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace GraphicLogoAdornment
{
    class GraphicLogoAdornmentLayer : AdornmentLayer
    {
        Bitmap logoImage;

        public GraphicLogoAdornmentLayer()
            : base()
        {
            Location = AdornmentLocation.LowerRight;
        }

        public Bitmap LogoImage
        {
            get { return logoImage; }
            set { logoImage = value; }
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            if (IsVisible)
            {
                ScreenPointF screenPointF = GetDrawingLocation(canvas, logoImage.Width, logoImage.Height);

                // If the canvas happens to be using GDI+ then we can do an optimization and skip
                // the GeoImage.  Otherwise we go the longer route in the else statement
                if (canvas is PlatformGeoCanvas)
                {
                    PlatformGeoCanvas gdiPlusGeoCanvas = canvas as PlatformGeoCanvas;
                    gdiPlusGeoCanvas.DrawScreenImageWithoutScaling(new GeoImage(logoImage), screenPointF.X + logoImage.Width * 0.5f, screenPointF.Y + logoImage.Height * 0.5f, DrawingLevel.LevelOne, 0, 0, 0);
                }
                else
                {
                    //  Here we have to convert the stream to a TIFF to be used in the GeoImage
                    Stream stream = new MemoryStream();
                    logoImage.Save(stream, ImageFormat.Tiff);
                    GeoImage geoImage = new GeoImage(stream);

                    canvas.DrawScreenImageWithoutScaling(geoImage, screenPointF.X + logoImage.Width * 0.5f, screenPointF.Y + logoImage.Height * 0.5f, DrawingLevel.LevelOne, 0, 0, 0);
                }
            }
        }
    }
}