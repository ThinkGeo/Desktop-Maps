using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool isBusy;
        private double coordinateX;
        private double coordinateY;
        private WpfMap mapControl;
        private MapModel mapModel;
        private EarthquakeQueryFilter queryFilter;
        private EarthquakeViewModel selectedEarthquake;
        private FeatureLayerViewModel selectedFeatureLayer;
        private Collection<TrackMode> mapControlModes;
        private Collection<EarthquakeViewModel> queryResults;
        private RelayCommand clearAllCommand;
        private RelayCommand<TrackMode> changMapModeCommand;
        private ObservableCollection<FeatureLayerViewModel> displayTypes;
        private ObservableCollection<EarthquakeViewModel> filteredQueryResults;

        public MainWindowViewModel()
            : this(null)
        { }

        public MainWindowViewModel(WpfMap wpfMap)
        {
            MapControl = wpfMap;
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public WpfMap MapControl
        {
            get { return mapControl; }
            set
            {
                mapControl = value;
                if (mapControl != null)
                {
                    mapModel = new MapModel(mapControl);
                    InitializeProperties();
                    InitializeEvents();
                }
            }
        }

        public double CoordinateX
        {
            get { return coordinateX; }
            set
            {
                coordinateX = value;
                RaisePropertyChanged(() => CoordinateX);
            }
        }

        public double CoordinateY
        {
            get { return coordinateY; }
            set
            {
                coordinateY = value;
                RaisePropertyChanged(() => CoordinateY);
            }
        }

        public TrackMode SelectedMapMode
        {
            get { return mapModel.TrackMode; }
            set
            {
                mapModel.TrackMode = value;
                RaisePropertyChanged(() => SelectedMapMode);
            }
        }

        public EarthquakeQueryFilter QueryFilter
        {
            get { return queryFilter; }
        }

        public Collection<TrackMode> MapControlModes
        {
            get
            {
                return mapControlModes ?? (mapControlModes = new Collection<TrackMode>
                {
                    TrackMode.None,
                    TrackMode.Polygon,
                    TrackMode.Rectangle,
                    TrackMode.Circle
                });
            }
        }

        public EarthquakeViewModel SelectedEarthquake
        {
            get { return selectedEarthquake; }
            set
            {
                selectedEarthquake = value;
                RaisePropertyChanged(() => SelectedEarthquake);
                if (selectedEarthquake != null && selectedEarthquake.EpicenterFeature != null)
                {
                    mapModel.UpdateHighlightMarker(selectedEarthquake.EpicenterFeature);
                }
            }
        }

        public FeatureLayerViewModel SelectedFeatureLayer
        {
            get { return selectedFeatureLayer; }
            set
            {
                if (selectedFeatureLayer != null) selectedFeatureLayer.IsVisible = false;

                selectedFeatureLayer = value;
                selectedFeatureLayer.IsVisible = true;

                mapModel.SetEarthquakeLegendsVisible(selectedFeatureLayer.FeatureLayer);
                mapModel.MapControl.Refresh();

                RaisePropertyChanged(() => SelectedFeatureLayer);
            }
        }

        public ObservableCollection<FeatureLayerViewModel> DisplayTypes
        {
            get { return displayTypes; }
        }

        public ObservableCollection<EarthquakeViewModel> FilteredQueryResults
        {
            get { return filteredQueryResults; }
        }

        public RelayCommand ClearAllCommand
        {
            get
            {
                return clearAllCommand ?? (clearAllCommand = new RelayCommand(() =>
                {
                    mapModel.ClearTrackResult();
                    FilteredQueryResults.Clear();
                    queryResults.Clear();
                }));
            }
        }

        public RelayCommand<TrackMode> ChangMapModeCommand
        {
            get
            {
                return changMapModeCommand ?? (changMapModeCommand = new RelayCommand<TrackMode>(currentMode =>
                {
                    if (currentMode == SelectedMapMode && currentMode != TrackMode.None) SelectedMapMode = MapControlModes.FirstOrDefault();
                    else SelectedMapMode = currentMode;
                }));
            }
        }

        private void MapModel_QueryAreaChanged(object sender, QueryAreaChangedMapModelEventArgs e)
        {
            FeatureLayer featureLayer = selectedFeatureLayer.FeatureLayer;
            featureLayer.Open();
            Collection<Feature> features = featureLayer.FeatureSource.GetFeaturesWithinDistanceOf(e.QueryFeature, mapModel.MapControl.MapUnit, DistanceUnit.Meter, 0.0001, ReturningColumnsType.AllColumns);
            queryResults.Clear();
            foreach (Feature feature in features)
            {
                queryResults.Add(new EarthquakeViewModel(feature));
            }

            FilterEarthquake();
        }

        private void StyleLayersOverLay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            {
                IsBusy = true;
            }
        }

        private void StyleLayersOverLay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            {
                IsBusy = false;
            }
        }

        private void MapModel_MapMouseCoordinateChanged(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            CoordinateX = e.WorldCoordinate.X;
            CoordinateY = e.WorldCoordinate.Y;
        }

        private void QueryFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FilterEarthquake();
        }

        private void InitializeProperties()
        {
            queryFilter = new EarthquakeQueryFilter();
            queryResults = new Collection<EarthquakeViewModel>();
            displayTypes = new ObservableCollection<FeatureLayerViewModel>();
            filteredQueryResults = new ObservableCollection<EarthquakeViewModel>();
            SelectedMapMode = MapControlModes.FirstOrDefault();

            foreach (FeatureLayer featureLayer in mapModel.StyleLayerOverlay.Layers.OfType<FeatureLayer>())
            {
                DisplayTypes.Add(new FeatureLayerViewModel(featureLayer));
            }
            SelectedFeatureLayer = DisplayTypes.FirstOrDefault();
        }

        private void InitializeEvents()
        {
            mapModel.QueryAreaChanged += MapModel_QueryAreaChanged;
            mapModel.StyleLayerOverlay.Drawn += StyleLayersOverLay_Drawn;
            mapModel.StyleLayerOverlay.Drawing += StyleLayersOverLay_Drawing;
            mapModel.MapControl.MapTools.MouseCoordinate.CustomFormatted += MapModel_MapMouseCoordinateChanged;
            queryFilter.PropertyChanged += QueryFilter_PropertyChanged;
        }

        private void FilterEarthquake()
        {
            FilteredQueryResults.Clear();

            Proj4Projection mercatorToWgs84Projection = new Proj4Projection();
            mercatorToWgs84Projection.InternalProjectionParametersString = Proj4Projection.GetSphericalMercatorParametersString();
            mercatorToWgs84Projection.ExternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            mercatorToWgs84Projection.Open();

            for (int i = queryResults.Count - 1; i >= 0; i--)
            {
                EarthquakeViewModel resultItem = queryResults[i];

                double latitude, longitude;
                if (double.TryParse(resultItem.Latitude, out latitude) && double.TryParse(resultItem.Longitude, out longitude))
                {
                    PointShape point = new PointShape(longitude, latitude);
                    point = (PointShape)mercatorToWgs84Projection.ConvertToExternalProjection(point);

                    EarthquakeViewModel newResultItem = new EarthquakeViewModel(resultItem.EpicenterFeature);
                    newResultItem.Latitude = point.Y.ToString("f3", CultureInfo.InvariantCulture);
                    newResultItem.Longitude = point.X.ToString("f3", CultureInfo.InvariantCulture);

                    double year, depth, magnitude;
                    double.TryParse(newResultItem.Magnitude, out magnitude);
                    double.TryParse(newResultItem.DepthInKilometer, out depth);
                    double.TryParse(newResultItem.Year, out year);

                    if ((magnitude >= queryFilter.StartMagnitudeRange && magnitude <= queryFilter.EndMagnitudeRange || newResultItem.Magnitude == Resources.UnknownString)
                        && (depth <= queryFilter.EndDepthRange && depth >= queryFilter.StartDepthRange || newResultItem.DepthInKilometer == Resources.UnknownString)
                        && (year >= queryFilter.StartYearRange && year <= queryFilter.EndYearRange) || newResultItem.Year == Resources.UnknownString)
                    {
                        FilteredQueryResults.Add(newResultItem);
                    }
                }
            }

            mercatorToWgs84Projection.Close();
            mapModel.RefreshMarkersByFeatures(FilteredQueryResults.Select(f => f.EpicenterFeature));
        }
    }
}