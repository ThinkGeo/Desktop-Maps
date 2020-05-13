using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace Geocoding
{
    class MarkerLayer : Layer
    {
        private PointShape markerLocation;

        public MarkerLayer()
        {
        }

        public PointShape MarkerLocation
        {
            get
            {
                return markerLocation;
            }
            set
            {
                markerLocation = value;
            }
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {            
            MemoryStream memoryStream = new MemoryStream();
            Bitmap markerImage = Properties.Resources.blue_pin;
            markerImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            if (markerLocation != null)
            {
                canvas.DrawWorldImageWithoutScaling(new GeoImage(memoryStream), markerLocation.X, markerLocation.Y, DrawingLevel.LevelOne);
            }
            canvas.Flush();
        }

    }
}
