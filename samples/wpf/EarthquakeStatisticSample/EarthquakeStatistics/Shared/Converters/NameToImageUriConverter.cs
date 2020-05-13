using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class NameToImageUriConverter : IValueConverter
    {
        private static readonly Dictionary<string, Uri> layerIcons;

        static NameToImageUriConverter()
        {
            layerIcons = new Dictionary<string, Uri>();
            layerIcons.Add(Resources.HeatStyleLayerName, new Uri("/Image/heatmap.png", UriKind.RelativeOrAbsolute));
            layerIcons.Add(Resources.IsolineStyleLayerName, new Uri("/Image/IsoLine.png", UriKind.RelativeOrAbsolute));
            layerIcons.Add(Resources.PointStyleLayerName, new Uri("/Image/pointMap.png", UriKind.RelativeOrAbsolute));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string layerName = value as string;
            if (!string.IsNullOrEmpty(layerName) && layerIcons.ContainsKey(layerName))
            {
                return layerIcons[layerName];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}