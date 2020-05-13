using System.Drawing;
using ThinkGeo.MapSuite.Drawing;

namespace PrintPreview
{
    public static class Helper
    {
        public static Font GetFont(GeoFont geoFont)
        {
            FontStyle style = FontStyle.Regular;
            switch (geoFont.Style)
            {
                case DrawingFontStyles.Regular:
                    style = FontStyle.Regular;
                    break;

                case DrawingFontStyles.Bold:
                    style = FontStyle.Bold;
                    break;

                case DrawingFontStyles.Italic:
                    style = FontStyle.Italic;
                    break;

                case DrawingFontStyles.Underline:
                    style = FontStyle.Underline;
                    break;

                case DrawingFontStyles.Strikeout:
                    style = FontStyle.Strikeout;
                    break;

                default:
                    break;
            }

            Font font = new Font(geoFont.FontName, geoFont.Size, style);

            return font;
        }

        public static GeoFont GetGeoFont(Font font)
        {
            DrawingFontStyles style = DrawingFontStyles.Regular;
            switch (font.Style)
            {
                case FontStyle.Bold:
                    style = DrawingFontStyles.Bold;
                    break;

                case FontStyle.Italic:
                    style = DrawingFontStyles.Italic;
                    break;

                case FontStyle.Regular:
                    style = DrawingFontStyles.Regular;
                    break;

                case FontStyle.Strikeout:
                    style = DrawingFontStyles.Strikeout;
                    break;

                case FontStyle.Underline:
                    style = DrawingFontStyles.Underline;
                    break;

                default:
                    break;
            }

            return new GeoFont(font.FontFamily.Name, font.Size, style);
        }

        public static Color GetColor(GeoColor geoColor)
        {
            return Color.FromArgb(geoColor.AlphaComponent, geoColor.RedComponent, geoColor.GreenComponent, geoColor.BlueComponent);
        }

        public static GeoColor GetGeoColor(Color color)
        {
            return GeoColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}