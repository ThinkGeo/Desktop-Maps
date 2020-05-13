using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisibility = (bool)value;
            Visibility visibility = isVisibility ? Visibility.Visible : Visibility.Collapsed;
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            bool isVisibility = visibility == Visibility.Visible;
            return isVisibility;
        }
    }
}
