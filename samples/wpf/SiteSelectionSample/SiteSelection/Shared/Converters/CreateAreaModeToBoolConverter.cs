using System;
using System.Globalization;
using System.Windows.Data;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class CreateAreaModeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            CreateAreasMode areasMode = (CreateAreasMode)value;
            string parameterValue = parameter as string;
            if (parameterValue == "Buffer")
            {
                result = areasMode == CreateAreasMode.Buffer;
            }
            else if (parameterValue == "Route")
            {
                result = areasMode == CreateAreasMode.Route;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CreateAreasMode result = CreateAreasMode.Buffer;
            bool boolValue = (bool)value;
            string parameterValue = parameter as string;
            if (parameterValue == "Buffer")
            {
                result = boolValue ? CreateAreasMode.Buffer : CreateAreasMode.Route;
            }
            else if (parameterValue == "Route")
            {
                result = boolValue ? CreateAreasMode.Route : CreateAreasMode.Buffer;
            }
            return result;
        }
    }
}