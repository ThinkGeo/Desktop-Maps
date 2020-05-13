using System.Windows.Media;
using ThinkGeo.MapSuite.Drawing;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class GeoColorViewModel
    {
        private string colorName;
        private SolidColorBrush colorBrush;
        private GeoColor geoColor;

        public GeoColorViewModel()
            : this(GeoColor.StandardColors.Transparent, "Transparent")
        { }

        public GeoColorViewModel(GeoColor color, string colorName)
        {
            ColorBrush = new SolidColorBrush(Color.FromArgb(color.AlphaComponent, color.RedComponent, color.GreenComponent, color.BlueComponent));
            ColorName = colorName;
            GeoColor = color;
        }

        public string ColorName
        {
            get { return colorName; }
            set { colorName = value; }
        }

        public SolidColorBrush ColorBrush
        {
            get { return colorBrush; }
            set { colorBrush = value; }
        }

        public GeoColor GeoColor
        {
            get { return geoColor; }
            set { geoColor = value; }
        }
    }
}