using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.SiteSelection.Properties;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly string findAllTextKey = "All";
        private static readonly string noResultKey = "No results found.";

        private bool isBusy;
        private double coordinateX;
        private double coordinateY;
        private double bufferDistance;
        private int driveTimeInMinute;
        private string selectedPoiType;
        private MapModel mapModel;
        private CommandBase clearAllCommand;
        private CommandBase applySettingsCommand;
        private DistanceUnit bufferDistanceUnit;
        private CreateAreasMode createAreasMode;
        private Collection<DistanceUnit> bufferDistanceUnits;
        private ObservableCollection<string> poiTypeCandidates;
        private ObservableCollection<QueryResultItemViewModel> queryResult;

        public MainWindowViewModel(MapModel mapModel)
            : base()
        {
            this.mapModel = mapModel;
            this.mapModel.TrackMode = TrackMode.None;
            this.mapModel.PlottedPointChanged += MapModel_PlacedPositionChanged;
            this.mapModel.MapControl.MapTools.MouseCoordinate.CustomFormatted += MapModel_MapMouseCoordinateChanged;
            this.mapModel.PlottedPoint = new PointShape(-10776838.0796536, 3912346.50475619);

            bufferDistance = 2;
            driveTimeInMinute = 6;
            coordinateX = mapModel.PlottedPoint.X;
            coordinateY = mapModel.PlottedPoint.Y;
            createAreasMode = CreateAreasMode.Route;
            bufferDistanceUnit = DistanceUnit.Mile;
            poiTypeCandidates = new ObservableCollection<string>();
            queryResult = new ObservableCollection<QueryResultItemViewModel>();
            SelectedPoiFeatureLayer = mapModel.PoiFeatureLayers[Resources.RestaurantsLayerKey];
            bufferDistanceUnits = new Collection<DistanceUnit>(new[] { DistanceUnit.Mile, DistanceUnit.Kilometer });

            SearchPointOfInterests();
        }

        public CommandBase ApplySettingsCommand
        {
            get
            {
                return applySettingsCommand ?? (applySettingsCommand = new CommandBase(SearchPointOfInterests));
            }
        }

        public double BufferDistance
        {
            get { return bufferDistance; }
            set
            {
                bufferDistance = value;
                RaisePropertyChanged(() => BufferDistance);
            }
        }

        public DistanceUnit BufferDistanceUnit
        {
            get { return bufferDistanceUnit; }
            set
            {
                bufferDistanceUnit = value;
                RaisePropertyChanged(() => BufferDistanceUnit);
                UpdateUnitSystem(value);
            }
        }

        public Collection<DistanceUnit> BufferDistanceUnits
        {
            get { return bufferDistanceUnits; }
        }

        public ObservableCollection<string> PoiTypeCandidates
        {
            get { return poiTypeCandidates; }
        }

        public CommandBase ClearAllCommand
        {
            get
            {
                return clearAllCommand ?? (clearAllCommand = new CommandBase(() =>
                {
                    QueryResult.Clear();
                    mapModel.PlottedPoint = null;
                    mapModel.ServiceAreaFeatures.Clear();
                    mapModel.AddMarkersByFeatures(null);
                    mapModel.MapControl.Refresh();
                }));
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

        public CreateAreasMode CreateAreasMode
        {
            get { return createAreasMode; }
            set
            {
                createAreasMode = value;
                RaisePropertyChanged(() => CreateAreasMode);
            }
        }

        public int DrivingTimeInMinute
        {
            get { return driveTimeInMinute; }
            set
            {
                driveTimeInMinute = value;
                RaisePropertyChanged(() => DrivingTimeInMinute);
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            protected set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public TrackMode TrackMode
        {
            get { return mapModel.TrackMode; }
            set
            {
                mapModel.TrackMode = value;
                RaisePropertyChanged(() => TrackMode);
            }
        }

        public ObservableCollection<ShapeFileFeatureLayer> PoiFeatureLayers
        {
            get { return new ObservableCollection<ShapeFileFeatureLayer>(mapModel.PoiFeatureLayers); }
        }

        public ObservableCollection<QueryResultItemViewModel> QueryResult
        {
            get { return queryResult; }
        }

        public string SelectedPoiType
        {
            get { return selectedPoiType; }
            set
            {
                selectedPoiType = value;
                RaisePropertyChanged(() => SelectedPoiType);

                UpdateQueryResult();
            }
        }

        public ShapeFileFeatureLayer SelectedPoiFeatureLayer
        {
            get { return mapModel.QueryingFeatureLayer; }
            set
            {
                mapModel.QueryingFeatureLayer = value;
                RaisePropertyChanged(() => SelectedPoiFeatureLayer);

                UpdatePoiTypeCandidates();
            }
        }

        private Dictionary<string, object> CollectServiceAnalysisParameters(CreateAreasMode targetMode)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            switch (targetMode)
            {
                case CreateAreasMode.Route:
                    parameters[Resources.StreetShapePathFilenameKey] = ConfigurationManager.AppSettings["StreetShapeFilePathName"];
                    parameters[Resources.DriveTimeInMinutesKey] = driveTimeInMinute;
                    break;

                default:
                    parameters[Resources.DistanceKey] = BufferDistance;
                    parameters[Resources.DistanceUnitKey] = BufferDistanceUnit;
                    break;
            }

            return parameters;
        }

        private void FilterHotelFeatures(IEnumerable<Feature> selectedFeatures)
        {
            if (string.IsNullOrEmpty(SelectedPoiType)) return;

            string property = SelectedPoiType.Trim();
            string[] values = property.Split('~');
            foreach (var feature in selectedFeatures)
            {
                double min, max, columnValue;
                if ((double.TryParse(values[0], out min)
                    && double.TryParse(values[1], out max)
                    && double.TryParse(feature.ColumnValues["ROOMS"], out columnValue)
                    && columnValue >= min && columnValue < max)
                    || string.IsNullOrEmpty(feature.ColumnValues["ROOMS"]))
                {
                    QueryResult.Add(new QueryResultItemViewModel(feature, mapModel));
                }
            }
        }

        private void MapModel_MapMouseCoordinateChanged(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            CoordinateX = e.WorldCoordinate.X;
            CoordinateY = e.WorldCoordinate.Y;
        }

        private void MapModel_PlacedPositionChanged(object sender, PlottedPointChangedMapModelEventArgs e)
        {
            mapModel.PlottedPoint = e.PlottedPoint;
            SearchPointOfInterests();
        }

        private void SearchPointOfInterests()
        {
            PointShape queryingPoint = mapModel.PlottedPoint;
            if (queryingPoint == null) return;

            IsBusy = true;
            Task.Factory.StartNew(() =>
            {
                Dictionary<string, object> parameters = CollectServiceAnalysisParameters(CreateAreasMode);
                ServiceAreaAnalysis analysis = MapSuiteSampleHelper.GetAreaAnalysis(CreateAreasMode);
                Feature serviceArea = analysis.CreateServiceAreaFeature(queryingPoint, mapModel.MapControl.MapUnit, parameters);
                if (serviceArea != null)
                {
                    mapModel.ServiceAreaFeatures.Clear();
                    mapModel.ServiceAreaFeatures.Add(serviceArea);
                }

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    mapModel.QueryingFeatureLayer.Open();
                    if (mapModel.ServiceAreaFeatures.Count > 0)
                    {
                        mapModel.MapControl.CurrentExtent = mapModel.ServiceAreaFeatures[0].GetBoundingBox();
                        UpdateQueryResult();
                    }

                    IsBusy = false;
                }));
            });
        }

        private void UpdatePoiTypeCandidates()
        {
            poiTypeCandidates.Clear();
            poiTypeCandidates.Add(findAllTextKey);

            Collection<string> candidateColumnValues = mapModel.GetColumnValueCandidates();
            foreach (var columnValue in candidateColumnValues)
            {
                poiTypeCandidates.Add(columnValue);
            }

            SelectedPoiType = PoiTypeCandidates.First();
        }

        private void UpdateQueryResult()
        {
            Collection<Feature> selectedFeatures = mapModel.GetFeaturesWithinServiceArea();

            QueryResult.Clear();
            if (SelectedPoiType == findAllTextKey)
            {
                foreach (var feature in selectedFeatures)
                {
                    QueryResult.Add(new QueryResultItemViewModel(feature, mapModel));
                }
            }
            else if (mapModel.QueryingFeatureLayer != mapModel.PoiFeatureLayers[Resources.HotelsLayerKey])
            {
                string columnName = MapSuiteSampleHelper.GetDefaultColumnNameByPoiType(mapModel.QueryingFeatureLayer.Name);
                foreach (var feature in selectedFeatures)
                {
                    string columnValue = feature.ColumnValues[columnName];
                    if (columnValue == SelectedPoiType || string.IsNullOrEmpty(columnValue))
                    {
                        QueryResult.Add(new QueryResultItemViewModel(feature, mapModel));
                    }
                }
            }
            else
            {
                FilterHotelFeatures(selectedFeatures);
            }

            mapModel.AddMarkersByFeatures(QueryResult.Select(q => q.Feature));
            mapModel.MapControl.Refresh();

            CommandManager.InvalidateRequerySuggested();
            if (QueryResult.Count == 0)
            {
                QueryResult.Add(new QueryResultItemViewModel(noResultKey));
            }
        }

        private void UpdateUnitSystem(DistanceUnit value)
        {
            switch (value)
            {
                case DistanceUnit.Kilometer:
                    mapModel.UpdateUnitSystem(UnitSystem.Metric);
                    break;

                case DistanceUnit.Mile:
                    mapModel.UpdateUnitSystem(UnitSystem.Imperial);
                    break;
            }
        }
    }
}