using System;
using System.Globalization;
using System.Windows.Data;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class TrackModeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;
            TrackMode trackMode = (TrackMode)value;
            string description = parameter.ToString().ToUpperInvariant(); ;
            if (description == "PAN")
            {
                result = trackMode == TrackMode.None;
            }
            else if (description == "DRAWPOINT")
            {
                result = trackMode == TrackMode.Point;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TrackMode result = TrackMode.None;
            bool isChecked = (bool)value;
            string description = parameter.ToString().ToUpperInvariant();
            if (description == "PAN")
            {
                result = isChecked ? TrackMode.None : TrackMode.Point;
            }
            else if (description == "DRAWPOINT")
            {
                result = isChecked ? TrackMode.Point : TrackMode.None;
            }

            return result;
        }
    }
}
