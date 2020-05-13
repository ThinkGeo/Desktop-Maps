using System;
using System.Globalization;
using System.Windows.Data;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class TrackModeToToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string toolTip;
            switch ((TrackMode)value)
            {
                case TrackMode.Circle:
                    toolTip = "Draw Circle Range";
                    break;
                case TrackMode.Polygon:
                    toolTip = "Draw Polygon Range";
                    break;
                case TrackMode.Rectangle:
                    toolTip = "Draw Rectangle Range";
                    break;
                default:
                    toolTip = "Pan the Map";
                    break;
            }
            return toolTip;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}