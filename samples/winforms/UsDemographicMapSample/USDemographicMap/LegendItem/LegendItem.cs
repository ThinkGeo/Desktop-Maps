using System.Drawing;
using ThinkGeo.MapSuite.Core;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public abstract class MyLegendItem
    {
        private Bitmap image;
        private string title;
        private Size previewSourceSize;

        protected MyLegendItem()
            : this(null, string.Empty)
        { }

        public MyLegendItem(Bitmap image, string title)
        {
            this.Image = image;
            this.Title = title;
        }

        public Size PreviewSourceSize
        {
            get { return previewSourceSize; }
            set { previewSourceSize = value; }
        }

        public Bitmap Image
        {
            get { return image; }
            set { image = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public void Draw(Style style)
        {
            GdiPlusGeoCanvas geoCanvas = new GdiPlusGeoCanvas();
            DrawCore(geoCanvas, style);
        }

        protected abstract void DrawCore(GeoCanvas geoCanvas, Style style);
    }
}