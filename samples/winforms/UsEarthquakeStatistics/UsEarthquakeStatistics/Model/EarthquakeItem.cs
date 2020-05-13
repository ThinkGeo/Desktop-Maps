using System.Drawing;
using System.Windows.Forms;

using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class EarthquakeItem
    {
        private static readonly Icon findIcon = Icon.FromHandle(Resources.find.GetHicon());

        private Feature epicenterFeature;
        private DataGridViewTextBoxCell yearCell;
        private DataGridViewTextBoxCell depth_KMCell;
        private DataGridViewImageCell imageButtonCell;
        private DataGridViewTextBoxCell latitudeCell;
        private DataGridViewTextBoxCell locationCell;
        private DataGridViewTextBoxCell longitudeCell;
        private DataGridViewTextBoxCell magnitudeCell;
        private DataGridViewTextBoxCell locationPointCell;

        public EarthquakeItem()
            : this(null)
        { }

        public EarthquakeItem(Feature epicenterFeature)
        {
            imageButtonCell = new DataGridViewImageCell();
            imageButtonCell.Style.SelectionBackColor = Color.Transparent;
            imageButtonCell.Value = findIcon;

            depth_KMCell = new DataGridViewTextBoxCell();
            yearCell = new DataGridViewTextBoxCell();
            locationCell = new DataGridViewTextBoxCell();
            longitudeCell = new DataGridViewTextBoxCell();
            latitudeCell = new DataGridViewTextBoxCell();
            magnitudeCell = new DataGridViewTextBoxCell();
            locationPointCell = new DataGridViewTextBoxCell();

            EpicenterFeature = epicenterFeature;
        }

        public DataGridViewTextBoxCell DepthInKilometerCell
        {
            get { return depth_KMCell; }
            set { depth_KMCell = value; }
        }

        public DataGridViewImageCell ImageButtonCell
        {
            get { return imageButtonCell; }
            set { imageButtonCell = value; }
        }

        public DataGridViewTextBoxCell LatitudeCell
        {
            get { return latitudeCell; }
            set { latitudeCell = value; }
        }

        public DataGridViewTextBoxCell LocationCell
        {
            get { return locationCell; }
            set { locationCell = value; }
        }

        public DataGridViewTextBoxCell LongitudeCell
        {
            get { return longitudeCell; }
            set { longitudeCell = value; }
        }

        public DataGridViewTextBoxCell MagnitudeCell
        {
            get { return magnitudeCell; }
            set { magnitudeCell = value; }
        }

        public Feature EpicenterFeature
        {
            get { return epicenterFeature; }
            set
            {
                epicenterFeature = value;
                if (epicenterFeature != null)
                {
                    depth_KMCell.Value = GetTextFromDoubleTypeColumnValue(epicenterFeature, Resources.DepthColumnName);
                    yearCell.Value = GetTextFromDoubleTypeColumnValue(epicenterFeature, Resources.YearColumnName);
                    locationCell.Value = GetTextFromColumnValue(epicenterFeature, Resources.LocationColumnName);
                    longitudeCell.Value = GetTextFromColumnValue(epicenterFeature, Resources.LongitudeColumnName);
                    latitudeCell.Value = GetTextFromColumnValue(epicenterFeature, Resources.LatitudeColumnName);
                    magnitudeCell.Value = GetTextFromDoubleTypeColumnValue(epicenterFeature, Resources.MagnitudeColumnName);
                    locationPointCell.Value = epicenterFeature.GetWellKnownText();
                }
            }
        }

        public DataGridViewTextBoxCell YearCell
        {
            get { return yearCell; }
            set { yearCell = value; }
        }

        public DataGridViewTextBoxCell LocationPointCell
        {
            get { return locationPointCell; }
            set { locationPointCell = value; }
        }

        private static string GetTextFromColumnValue(Feature feature, string columnName)
        {
            string text = string.Empty;
            if (feature.ColumnValues.ContainsKey(columnName))
            {
                text = feature.ColumnValues[columnName];
            }
            return text;
        }

        private static string GetTextFromDoubleTypeColumnValue(Feature feature, string columnName)
        {
            string text = Resources.UnknownString;
            if (feature.ColumnValues.ContainsKey(columnName))
            {
                double value;
                if (double.TryParse(feature.ColumnValues[columnName], out value))
                {
                    text = value >= 0 ? feature.ColumnValues[columnName] : Resources.UnknownString;
                }
            }
            return text;
        }
    }
}