using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class EarthquakeViewModel : ViewModelBase
    {
        private string year;
        private string latitude;
        private string location;
        private string longitude;
        private string magnitude;
        private string depthInKilometer;

        private Feature epicenterFeature;
        private RelayCommand<object> zoomToFeatureCommand;

        public EarthquakeViewModel()
            : this(null)
        { }

        public EarthquakeViewModel(Feature epicenterFeature)
        {
            EpicenterFeature = epicenterFeature;
        }

        public string DepthInKilometer
        {
            get { return depthInKilometer; }
            set { depthInKilometer = value; }
        }

        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public string Magnitude
        {
            get { return magnitude; }
            set { magnitude = value; }
        }

        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        public Feature EpicenterFeature
        {
            get { return epicenterFeature; }
            set
            {
                epicenterFeature = value;
                if (epicenterFeature != null)
                {
                    DepthInKilometer = GetTextFromDoubleTypeColumnValue(epicenterFeature, Resources.DepthColumnName);
                    Magnitude = GetTextFromDoubleTypeColumnValue(epicenterFeature, Resources.MagnitudeColumnName);
                    Year = GetTextFromDoubleTypeColumnValue(epicenterFeature, Resources.YearColumnName);

                    Location = GetTextFromColumnValue(epicenterFeature, Resources.LocationColumnName);
                    Longitude = GetTextFromColumnValue(epicenterFeature, Resources.LongitudeColumnName);
                    Latitude = GetTextFromColumnValue(epicenterFeature, Resources.LatitudeColumnName);
                }
            }
        }

        public RelayCommand<object> ZoomToFeatureCommand
        {
            get
            {
                return zoomToFeatureCommand ?? (zoomToFeatureCommand = new RelayCommand<object>(dataContext =>
                {
                    MainWindowViewModel mainWindowViewModel = dataContext as MainWindowViewModel;
                    if (mainWindowViewModel != null)
                    {
                        mainWindowViewModel.SelectedEarthquake = this;
                        PointShape pointShape = epicenterFeature.GetShape() as PointShape;
                        if (pointShape != null)
                        {
                            mainWindowViewModel.MapControl.ZoomTo(pointShape, 288374.8974609375);
                        }
                    }
                }));
            }
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