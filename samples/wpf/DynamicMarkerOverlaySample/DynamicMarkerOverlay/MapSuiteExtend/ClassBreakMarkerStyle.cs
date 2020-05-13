using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace DynamicMarkerOverlay
{
    public class ClassBreakMarkerStyle : MarkerStyle
    {
        private string columnName;
        private BreakValueInclusion breakValueInclusion;
        private Collection<MarkerClassBreak> classBreaks;

        public ClassBreakMarkerStyle() : this(String.Empty, BreakValueInclusion.IncludeValue, new Collection<MarkerClassBreak>())
        { }

        public ClassBreakMarkerStyle(string columnName) : this(columnName, BreakValueInclusion.IncludeValue, new Collection<MarkerClassBreak>())
        { }

        public ClassBreakMarkerStyle(string columnName, BreakValueInclusion breakValueInclusion) : this(columnName, breakValueInclusion, new Collection<MarkerClassBreak>())
        { }

        public ClassBreakMarkerStyle(string columnName, BreakValueInclusion breakValueInclusion, Collection<MarkerClassBreak> classBreaks) : base()
        {
            this.columnName = columnName;
            this.breakValueInclusion = breakValueInclusion;
            this.classBreaks = classBreaks;
        }

        public string ColumnName
        {
            get
            {
                return columnName;
            }
            set
            {
                columnName = value;
            }
        }

        public BreakValueInclusion BreakValueInclusion
        {
            get { return breakValueInclusion; }
            set
            {
                breakValueInclusion = value;
            }
        }
        public Collection<MarkerClassBreak> ClassBreaks
        {
            get { return classBreaks; }
        }

        public override GeoCollection<Marker> GetMarkers(IEnumerable<Feature> features)
        {
            GeoCollection<Marker> returnMarkers = new GeoCollection<Marker>();

            foreach (Feature feature in features)
            {
                double columnValue = double.Parse(feature.ColumnValues[ColumnName].Trim(), CultureInfo.InvariantCulture);
                MarkerClassBreak classBreak = GetClassBreak(columnValue);
                if (classBreak != null)
                {
                    Collection<Marker> tempMarkers = classBreak.DefaultMarkerStyle.GetMarkers(new Collection<Feature>() { feature });
                    foreach (Marker marker in tempMarkers)
                    {
                        // To make sure the cursor is pointer when move mouse to marker, use the events as following to change cursor
                        marker.Cursor = Cursors.Hand;

                        returnMarkers.Add(marker);
                    }

                }
            }

            return returnMarkers;
        }
    
        private MarkerClassBreak GetClassBreak(double breakValue)
        {
            MarkerClassBreak result = classBreaks[classBreaks.Count - 1];

            if (breakValueInclusion == BreakValueInclusion.IncludeValue)
            {
                if (breakValue < classBreaks[0].Value)
                {
                    return null;
                }

                for (int i = 1; i < classBreaks.Count; i++)
                {
                    if (breakValue < classBreaks[i].Value)
                    {
                        result = classBreaks[i - 1];
                        break;
                    }
                    else if (breakValue == classBreaks[i].Value)
                    {
                        result = classBreaks[i];
                        break;
                    }
                }
            }
            else
            {
                if (breakValue <= classBreaks[0].Value)
                {
                    return null;
                }

                for (int i = 1; i < classBreaks.Count; i++)
                {
                    if (breakValue < classBreaks[i].Value)
                    {
                        result = classBreaks[i - 1];
                        break;
                    }
                }
            }

            return result;
        }
    }
}
