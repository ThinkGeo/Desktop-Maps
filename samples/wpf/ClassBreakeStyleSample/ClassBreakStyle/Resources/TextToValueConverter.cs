using System;
using System.Windows.Data;

namespace ClassBreakStyleSample
{
    internal class TextToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = (string)value;
            double textValue = 0;

            return double.TryParse(text, out textValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
