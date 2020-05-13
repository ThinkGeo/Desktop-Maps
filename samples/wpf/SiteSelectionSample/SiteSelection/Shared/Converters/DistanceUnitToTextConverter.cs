using System;
using System.Windows.Data;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
{
    internal class DistanceUnitToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string item = string.Empty;
            if (value is DistanceUnit)
            {
                DistanceUnit unit = (DistanceUnit)value;
                switch (unit)
                {
                    case DistanceUnit.Mile:
                        item = "Miles";
                        break;

                    case DistanceUnit.Kilometer:
                        item = "Kilometers";
                        break;
                }
            }

            return item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DistanceUnit unit = DistanceUnit.Kilometer;
            string text = value as string;
            if (text == "Miles")
            {
                unit = DistanceUnit.Mile;
            }
            else if (text == "Kilometers")
            {
                unit = DistanceUnit.Kilometer;
            }
            return unit;
        }
    }
}