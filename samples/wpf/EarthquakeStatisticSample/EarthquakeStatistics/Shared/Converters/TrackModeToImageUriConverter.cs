using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class TrackModeToImageUriConverter : IValueConverter
    {
        private static readonly Dictionary<TrackMode, Uri> mapControlModes;

        static TrackModeToImageUriConverter()
        {
            mapControlModes = new Dictionary<TrackMode, Uri>();
            mapControlModes.Add(TrackMode.None, new Uri("/Image/pan.png", UriKind.RelativeOrAbsolute));
            mapControlModes.Add(TrackMode.Polygon, new Uri("/Image/Draw_Polygon.png", UriKind.RelativeOrAbsolute));
            mapControlModes.Add(TrackMode.Rectangle, new Uri("/Image/Draw_Rectangle.png", UriKind.RelativeOrAbsolute));
            mapControlModes.Add(TrackMode.Circle, new Uri("/Image/Draw_Circle.png", UriKind.RelativeOrAbsolute));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TrackMode layerName = (TrackMode)value;
            if (mapControlModes.ContainsKey(layerName))
            {
                return mapControlModes[layerName];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}