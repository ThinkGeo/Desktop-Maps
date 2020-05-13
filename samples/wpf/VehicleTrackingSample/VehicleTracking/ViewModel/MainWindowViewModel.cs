using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.VehicleTracking.Properties;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly DateTime adjustedStartTime = DateTime.Parse("2009/7/10 11:31:0");
        private static readonly DateTime applicationStartTime = DateTime.Now;
        private static readonly string dataRootPath;

        private bool isBusy;
        private bool autoRefresh;
        private double coordinateX;
        private double coordinateY;

        private WpfMap mapControl;
        private MapModel mapModel;
        private ControlMapMode mapMode;
        private MeasureMode measureMode;
        private DrawFenceMode drawFenceMode;
        private UnitSystem selectedUnitSystem;
        private DispatcherTimer dispatcherTimer;
        private Visibility editPanelVisibility;
        private Visibility measurePanelVisibility;
        private AutoRefreshMode autoRefreshMode;
        private Collection<UnitSystem> unitSystems;
        private ObservableCollection<VehicleViewModel> vehicles;

        private CommandBase refreshCommand;
        private CommandBase saveFenceCommand;
        private CommandBase cancelEditCommand;
        private CommandBase deleteFenceCommand;
        private CommandBase cancelMeasureCommand;

        static MainWindowViewModel()
        {
            dataRootPath = ConfigurationManager.AppSettings["dataRootPath"];
        }

        public MainWindowViewModel()
            : this(null)
        { }

        public MainWindowViewModel(WpfMap map)
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(5000);
            dispatcherTimer.Tick += AutoRefreshTimer_Tick;

            vehicles = new ObservableCollection<VehicleViewModel>();
            unitSystems = new Collection<UnitSystem>();
            unitSystems.Add(UnitSystem.Imperial);
            unitSystems.Add(UnitSystem.Metric);
            selectedUnitSystem = UnitSystem.Metric;
            autoRefreshMode = AutoRefreshMode.On;
            autoRefresh = true;
            drawFenceMode = DrawFenceMode.DrawNewFence;
            measureMode = MeasureMode.Line;
            mapMode = ControlMapMode.Pan;
            measurePanelVisibility = Visibility.Collapsed;
            editPanelVisibility = Visibility.Collapsed;

            MapControl = map;

            dispatcherTimer.Start();
        }

        public WpfMap MapControl
        {
            get { return mapControl; }
            set
            {
                mapControl = value;
                RaisePropertyChanged(() => MapControl);

                if (mapControl != null)
                {
                    mapModel = new MapModel(mapControl);
                    mapModel.MapControl.MapTools.MouseCoordinate.CustomFormatted += MouseCoordinate_CustomFormatted;
                    mapModel.MapControl.OverlaysDrawing += WpfMap1_OverlaysDrawing;
                    mapModel.MapControl.OverlaysDrawn += WpfMap1_OverlaysDrawn;

                    UpdateSpatialFencesAndVehicles();
                }
            }
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

        public bool AutoRefresh
        {
            get { return autoRefresh; }
            set
            {
                autoRefresh = value;
                RaisePropertyChanged(() => AutoRefresh);
                if (value)
                {
                    dispatcherTimer.Start();
                    AutoRefreshMode = AutoRefreshMode.On;
                }
                else
                {
                    dispatcherTimer.Stop();
                    AutoRefreshMode = AutoRefreshMode.Off;
                }
            }
        }

        public ControlMapMode MapMode
        {
            get { return mapMode; }
            set
            {
                mapMode = value;
                RaisePropertyChanged(() => MapMode);
                switch (value)
                {
                    case ControlMapMode.Pan:
                        MeasurePanelVisibility = Visibility.Collapsed;
                        EditPanelVisibility = Visibility.Collapsed;
                        mapModel.TrackMode = TrackMode.None;
                        break;

                    case ControlMapMode.DrawFence:
                        mapModel.MapControl.TrackOverlay.TrackEnded -= TrackOverlay_TrackEnded;
                        mapModel.MapControl.TrackOverlay.TrackMode = TrackMode.Polygon;
                        mapModel.MapControl.Cursor = Cursors.Arrow;

                        MeasurePanelVisibility = Visibility.Collapsed;
                        EditPanelVisibility = Visibility.Visible;
                        break;

                    case ControlMapMode.Measure:
                        mapModel.MapControl.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
                        if (mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Count > 0)
                        {
                            SaveSpatialFences();
                        }
                        else if (mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Count > 0)
                        {
                            CancelEditCommand.Execute(null);
                        }
                        mapModel.MapControl.Cursor = Cursors.Arrow;
                        mapModel.TrackMode = measureMode == MeasureMode.Line ? TrackMode.Line : TrackMode.Polygon;
                        mapModel.MapControl.TrackOverlay.TrackStarting -= TrackOverlay_TrackStarting;
                        mapModel.MapControl.TrackOverlay.TrackStarting += TrackOverlay_TrackStarting;
                        mapModel.MapControl.TrackOverlay.TrackEnded -= TrackOverlay_TrackEnded;
                        mapModel.MapControl.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;

                        MeasurePanelVisibility = Visibility.Visible;
                        EditPanelVisibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        public MeasureMode MeasureMode
        {
            get { return measureMode; }
            set
            {
                measureMode = value;
                RaisePropertyChanged(() => MeasureMode);
                switch (value)
                {
                    case MeasureMode.Line:
                        mapModel.TrackMode = TrackMode.Line;
                        break;

                    case MeasureMode.Polygon:
                        mapModel.TrackMode = TrackMode.Polygon;
                        break;
                }
            }
        }

        public DrawFenceMode DrawFenceMode
        {
            get { return drawFenceMode; }
            set
            {
                drawFenceMode = value;
                RaisePropertyChanged(() => DrawFenceMode);

                switch (value)
                {
                    case DrawFenceMode.DrawNewFence:
                        ResetFences();
                        break;

                    case DrawFenceMode.EditFence:
                        ClearMeasuresAndPopups();
                        mapModel.MapControl.TrackOverlay.TrackMode = TrackMode.None;

                        // Move spatial fences from spatial fence layer to edit overlay for editing
                        foreach (Feature feature in mapModel.SpatialFenceLayer.InternalFeatures)
                        {
                            if (!mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Contains(feature.Id))
                            {
                                mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(feature.Id, feature);
                            }
                            if (!mapModel.SpatialFenceLayer.FeatureIdsToExclude.Contains(feature.Id))
                            {
                                mapModel.SpatialFenceLayer.FeatureIdsToExclude.Add(feature.Id);
                            }
                        }

                        mapModel.MapControl.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
                        mapModel.MapControl.TrackOverlay.MapMouseClick += TrackOverlay_MapMouseClick;

                        UpdateVehicles(mapModel.TraceOverlay, GetAdjustedCurrentDateTime());
                        mapModel.MapControl.Refresh(mapModel.SpatialFenceOverlay);
                        mapModel.MapControl.Refresh(mapModel.MapControl.TrackOverlay);
                        break;
                }
            }
        }

        public Visibility EditPanelVisibility
        {
            get { return editPanelVisibility; }
            set
            {
                editPanelVisibility = value;
                RaisePropertyChanged(() => EditPanelVisibility);
            }
        }

        public Visibility MeasurePanelVisibility
        {
            get { return measurePanelVisibility; }
            set
            {
                measurePanelVisibility = value;
                RaisePropertyChanged(() => MeasurePanelVisibility);
            }
        }

        public CommandBase RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = new CommandBase(UpdateSpatialFencesAndVehicles));
            }
        }

        public CommandBase CancelMeasureCommand
        {
            get
            {
                return cancelMeasureCommand ?? (cancelMeasureCommand = new CommandBase(ClearMeasuresAndPopups));
            }
        }

        public CommandBase SaveFenceCommand
        {
            get
            {
                return saveFenceCommand ?? (saveFenceCommand = new CommandBase(() =>
                {
                    mapModel.MapControl.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
                    SaveSpatialFences();

                    UpdateSpatialFencesAndVehicles();

                    if (drawFenceMode == DrawFenceMode.EditFence)
                    {
                        DrawFenceMode = DrawFenceMode.DrawNewFence;
                        ResetFences();
                    }
                }));
            }
        }

        public CommandBase CancelEditCommand
        {
            get
            {
                return cancelEditCommand ?? (cancelEditCommand = new CommandBase(() =>
                {
                    mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

                    foreach (var editFeature in mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Where(f => f.Tag != "Measure").ToList())
                    {
                        mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove(editFeature);
                    }

                    mapModel.SpatialFenceLayer.FeatureIdsToExclude.Clear();
                    mapModel.MapControl.Refresh();
                    if (drawFenceMode == DrawFenceMode.EditFence)
                    {
                        DrawFenceMode = DrawFenceMode.DrawNewFence;
                        ResetFences();
                    }
                }));
            }
        }

        public CommandBase DeleteFenceCommand
        {
            get
            {
                return deleteFenceCommand ?? (deleteFenceCommand = new CommandBase(() =>
                {
                    if (mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Count > 0)
                    {
                        if (MessageBox.Show(Resources.DeleteFenceWarning, "Vehicle Tracking", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            mapModel.MapControl.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
                            mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
                            mapModel.SpatialFenceLayer.InternalFeatures.Clear();
                            SaveSpatialFences();
                            UpdateSpatialFencesAndVehicles();
                        }
                    }
                    else
                    {
                        MessageBox.Show(Resources.ChooseFenceWarning, "Vehicle Tracking");
                    }
                }));
            }
        }

        public AutoRefreshMode AutoRefreshMode
        {
            get { return autoRefreshMode; }
            set
            {
                autoRefreshMode = value;
                RaisePropertyChanged(() => AutoRefreshMode);
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

        public Collection<UnitSystem> UnitSystems
        {
            get { return unitSystems; }
        }

        public UnitSystem SelectedUnitSystem
        {
            get { return selectedUnitSystem; }
            set
            {
                selectedUnitSystem = value;
                mapModel.UpdateUnitSystem(SelectedUnitSystem);
                RaisePropertyChanged(() => SelectedUnitSystem);
            }
        }

        public ObservableCollection<VehicleViewModel> Vehicles
        {
            get { return vehicles; }
        }

        private void TrackOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            PointShape clickedPoint = new PointShape(e.InteractionArguments.WorldX, e.InteractionArguments.WorldY);
            RectangleShape rect = MapSuiteSampleHelper.GetBufferedRectangle(clickedPoint, mapModel.MapControl.CurrentResolution);
            Collection<Feature> intersectingFeatures = mapModel.MapControl.TrackOverlay.TrackShapeLayer.QueryTools.GetFeaturesIntersecting(rect, ReturningColumnsType.AllColumns);
            if (intersectingFeatures.Count > 0)
            {
                if (mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Count > 0)
                {
                    foreach (var item in mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures)
                    {
                        mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(item.Id, item);
                    }
                    mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
                }
                mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove(intersectingFeatures[0].Id);
                mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Add(intersectingFeatures[0]);
                mapModel.MapControl.EditOverlay.CalculateAllControlPoints();
                mapModel.MapControl.Refresh(mapModel.MapControl.TrackOverlay);
                mapModel.MapControl.Refresh(mapModel.MapControl.EditOverlay);
            }
        }

        private void MouseCoordinate_CustomFormatted(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            CoordinateX = e.WorldCoordinate.X;
            CoordinateY = e.WorldCoordinate.Y;
        }

        private void WpfMap1_OverlaysDrawing(object sender, OverlaysDrawingWpfMapEventArgs e)
        {
            IsBusy = true;
        }

        private void WpfMap1_OverlaysDrawn(object sender, OverlaysDrawnWpfMapEventArgs e)
        {
            IsBusy = false;
        }

        private void AutoRefreshTimer_Tick(object sender, EventArgs e)
        {
            UpdateSpatialFencesAndVehicles();
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            if (mapModel.MapControl.TrackOverlay.TrackMode == TrackMode.Polygon)
            {
                PolygonShape polygonShape = e.TrackShape as PolygonShape;
                if (polygonShape != null)
                {
                    double area = -1;
                    string areaUnit = "sq.m.";
                    switch (SelectedUnitSystem)
                    {
                        case UnitSystem.Metric:
                            areaUnit = "sq.m.";
                            area = polygonShape.GetArea(GeographyUnit.Meter, AreaUnit.SquareMeters);
                            break;

                        case UnitSystem.Imperial:
                            areaUnit = "ac.";
                            area = polygonShape.GetArea(GeographyUnit.Meter, AreaUnit.Acres);
                            break;
                    }
                    if (area > 0)
                    {
                        string content = string.Format(CultureInfo.InvariantCulture, "Area: {0} {1}", area.ToString("N1"), areaUnit);
                        ShowPopup(new PointShape(polygonShape.OuterRing.Vertices[polygonShape.OuterRing.Vertices.Count - 2]), content);
                    }
                    mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.LastOrDefault().Tag = "Measure";
                }
            }

            if (mapModel.MapControl.TrackOverlay.TrackMode == TrackMode.Line)
            {
                string unit = string.Empty;
                LineShape lineShape = e.TrackShape as LineShape;
                if (lineShape != null)
                {
                    double lenth = 0;
                    if (SelectedUnitSystem == UnitSystem.Metric)
                    {
                        lenth = lineShape.GetLength(GeographyUnit.Meter, DistanceUnit.Meter);
                        unit = "m";
                        if (lenth >= 1000)
                        {
                            unit = "km";
                            lenth = Math.Round(lenth / 1000d, 1, MidpointRounding.AwayFromZero);
                        }
                    }
                    else if (SelectedUnitSystem == UnitSystem.Imperial)
                    {
                        lenth = lineShape.GetLength(GeographyUnit.Meter, DistanceUnit.Feet);
                        unit = "ft";
                        if (lenth >= 5280)
                        {
                            unit = "mi";
                            lenth = Math.Round(lenth / 5280d, 1, MidpointRounding.AwayFromZero);
                        }
                    }

                    string lengthString = lenth.ToString("N1");
                    string content = string.Format(CultureInfo.InvariantCulture, "Total Length: {0} {1}", lengthString, unit);
                    ShowPopup(new PointShape(lineShape.Vertices[lineShape.Vertices.Count - 1]), content);
                    mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.LastOrDefault().Tag = "Measure";
                }
            }
        }

        private void TrackOverlay_TrackStarting(object sender, TrackStartingTrackInteractiveOverlayEventArgs e)
        {
            if (mapModel.TrackMode == TrackMode.Line || mapModel.TrackMode == TrackMode.Polygon)
            {
                ClearMeasuresAndPopups();
            }
        }

        private void UpdateSpatialFencesAndVehicles()
        {
            DateTime currentTime = GetAdjustedCurrentDateTime();
            Task.Factory.StartNew(() =>
            {
                lock (mapModel.SpatialFenceOverlay)
                {
                    // Update the fences from database.
                    UpdateSpatialFences();
                }
                lock (mapModel.TraceOverlay)
                {
                    // Update the vehicle locations on the map.
                    UpdateVehicles(mapModel.TraceOverlay, currentTime);
                }

                Application.Current.Dispatcher.BeginInvoke(new Action(mapModel.MapControl.Refresh));
            });
        }

        // Here we update the vehicles from the database.  In this sample we create our data provider each time and
        // when we are finsihed with it we dispose it.  This is very safe however you may get better performance
        // if you were to cache this.  We wanted to sample to be clean and safe and it is up to your expertise to
        // enhance it further
        private void UpdateVehicles(LayerOverlay traceOverlay, DateTime currentTime)
        {
            Dictionary<int, Vehicle> currentVehicles;
            TrackingAccessProvider vehicleProvider = new TrackingAccessProvider(dataRootPath);
            currentVehicles = vehicleProvider.GetCurrentVehicles(currentTime);


            // Loop through all the vehicle to add the history points
            if (currentVehicles != null && currentVehicles.All(v => !string.IsNullOrEmpty(v.Value.IconPath)))
            {
                InMemoryFeatureLayer vehicleTrailLayer;

                // Initialize vehicle overlay if it's not initialized
                if (!traceOverlay.Layers.Contains("VehicleTrail"))
                {
                    // Create an InMemoryMarkerOverlay for the vehicle to hold the points and current location
                    vehicleTrailLayer = new InMemoryFeatureLayer();
                    vehicleTrailLayer.Open();
                    vehicleTrailLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = GetVehicleTrailStyle();
                    vehicleTrailLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                    // Add all the required columns so we can populate later
                    vehicleTrailLayer.FeatureSource.Open();
                    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Speed"));
                    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("DateTime"));
                    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Longitude"));
                    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Latitude"));
                    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("VehicleName"));
                    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Duration"));
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        lock (traceOverlay.Layers)
                        {
                            traceOverlay.Layers.Add("VehicleTrail", vehicleTrailLayer);
                        }
                    }));
                }
                else
                {
                    // Find the overlay in the map
                    vehicleTrailLayer = (InMemoryFeatureLayer)traceOverlay.Layers["VehicleTrail"];
                }
                vehicleTrailLayer.InternalFeatures.Clear();
                foreach (int vehicleId in currentVehicles.Keys)
                {
                    Vehicle currentVehicle = currentVehicles[vehicleId];
                    VehicleViewModel tempVehicleViewModel = Vehicles.FirstOrDefault(v => v.OwnerName == currentVehicle.Name);
                    if (tempVehicleViewModel == null)
                    {
                        tempVehicleViewModel = new VehicleViewModel(currentVehicle, mapModel);
                        Application.Current.Dispatcher.BeginInvoke(new Action(() => Vehicles.Add(tempVehicleViewModel)));
                    }
                    else
                    {
                        tempVehicleViewModel.Vehicle = currentVehicle;
                        tempVehicleViewModel.Load();
                    }

                    bool isInSpatialFence = IsInSpatialFence(currentVehicle);
                    tempVehicleViewModel.Area = isInSpatialFence ? "In restricted area" : "Out of restricted area";

                    // Add the vheicle's bread crumbs
                    foreach (Location historyLocation in currentVehicle.HistoryLocations.Take(6))
                    {
                        Feature breadcrumbFeature = new Feature(historyLocation.GetLocation().GetWellKnownBinary(), currentVehicle.Name + historyLocation.DateTime.ToString(CultureInfo.InvariantCulture));
                        breadcrumbFeature.ColumnValues["DateTime"] = historyLocation.DateTime.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["Speed"] = historyLocation.Speed.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["Longitude"] = historyLocation.Longitude.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["Latitude"] = historyLocation.Latitude.ToString(CultureInfo.InvariantCulture);
                        breadcrumbFeature.ColumnValues["VehicleName"] = currentVehicle.Name;
                        breadcrumbFeature.ColumnValues["Duration"] = currentVehicle.GetSpeedDuration().ToString(CultureInfo.InvariantCulture);
                        vehicleTrailLayer.InternalFeatures.Add(breadcrumbFeature.Id, breadcrumbFeature);
                    }

                    InMemoryFeatureLayer currentVehicleLayer;
                    if (!traceOverlay.Layers.Contains(currentVehicle.Name))
                    {
                        // Create an InMemoryMarkerOverlay for the vehicle to hold the points and current location
                        currentVehicleLayer = new InMemoryFeatureLayer();
                        currentVehicleLayer.Open();
                        currentVehicleLayer.Name = currentVehicle.Id.ToString(CultureInfo.InvariantCulture);
                        currentVehicleLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = GetCurrentVehicleStyle(currentVehicle.IconPath);
                        currentVehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                        // Add all the required columns so we can populate later
                        currentVehicleLayer.FeatureSource.Open();
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Speed"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("DateTime"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Longitude"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Latitude"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("VehicleName"));
                        currentVehicleLayer.Columns.Add(new FeatureSourceColumn("Duration"));
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            lock (traceOverlay.Layers)
                            {
                                traceOverlay.Layers.Add(currentVehicle.Name, currentVehicleLayer);
                            }
                        }));
                    }
                    else
                    {
                        // Find the overlay in the map
                        currentVehicleLayer = (InMemoryFeatureLayer)traceOverlay.Layers[currentVehicle.Name];
                    }
                    currentVehicleLayer.InternalFeatures.Clear();
                    // Add the vehicle's latest position
                    Feature latestPositionFeature = new Feature(currentVehicle.Location.GetLocation().GetWellKnownBinary(), currentVehicle.Name);
                    latestPositionFeature.ColumnValues["DateTime"] = currentVehicle.Location.DateTime.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["Speed"] = currentVehicle.Location.Speed.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["Longitude"] = currentVehicle.Location.Longitude.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["Latitude"] = currentVehicle.Location.Latitude.ToString(CultureInfo.InvariantCulture);
                    latestPositionFeature.ColumnValues["VehicleName"] = currentVehicle.Name;
                    latestPositionFeature.ColumnValues["Duration"] = currentVehicle.GetSpeedDuration().ToString(CultureInfo.InvariantCulture);
                    currentVehicleLayer.InternalFeatures.Add(latestPositionFeature.Id, latestPositionFeature);
                }
            }
        }

        private void UpdateSpatialFences()
        {
            // Get the spatial fence overlay and layer and then clear them
            mapModel.SpatialFenceLayer.InternalFeatures.Clear();

            // Get the spatial fences from the database
            Collection<Feature> spatialFences;
            TrackingAccessProvider vehicleProvider = new TrackingAccessProvider(dataRootPath);
            spatialFences = vehicleProvider.GetSpatialFences();

            // Insert fences from database into fence layer
            if (spatialFences != null)
            {
                foreach (Feature spatialFence in spatialFences)
                {
                    spatialFence.ColumnValues["Restricted"] = "Restricted";
                    mapModel.SpatialFenceLayer.InternalFeatures.Add(spatialFence.Id, spatialFence);
                }
            }
        }

        private bool IsInSpatialFence(Vehicle vehicle)
        {
            InMemoryFeatureLayer spatialFenceLayer = (InMemoryFeatureLayer)mapModel.SpatialFenceOverlay.Layers["SpatialFenceLayer"];

            // Get the point shape and then check if it is within any of the sptail fences using the QueryTools
            PointShape pointShape = new PointShape(vehicle.Location.Longitude, vehicle.Location.Latitude);
            bool isInSpatialFence = false;
            lock (spatialFenceLayer)
            {
                spatialFenceLayer.Open();
                Collection<Feature> spatialFencesWithin = spatialFenceLayer.QueryTools.GetFeaturesContaining(pointShape, ReturningColumnsType.NoColumns);
                if (spatialFencesWithin.Count > 0)
                {
                    isInSpatialFence = true;
                }
            }
            return isInSpatialFence;
        }

        private void SaveSpatialFences()
        {
            TrackingAccessProvider vehicleProvider = null;

            try
            {
                foreach (var item in mapModel.SpatialFenceLayer.FeatureIdsToExclude)
                {
                    if (mapModel.SpatialFenceLayer.InternalFeatures.Contains(item))
                    {
                        mapModel.SpatialFenceLayer.InternalFeatures.Remove(item);
                    }
                }
                mapModel.SpatialFenceLayer.FeatureIdsToExclude.Clear();
                foreach (Feature feature in mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Where(f => f.Tag != "Measure"))
                {
                    if (!mapModel.SpatialFenceLayer.InternalFeatures.Contains(feature.Id))
                    {
                        mapModel.SpatialFenceLayer.InternalFeatures.Add(feature.Id, feature);
                    }
                }

                foreach (Feature feature in mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures)
                {
                    if (!mapModel.SpatialFenceLayer.InternalFeatures.Contains(feature.Id))
                    {
                        mapModel.SpatialFenceLayer.InternalFeatures.Add(feature.Id, feature);
                    }
                }
                mapModel.SpatialFenceOverlay.Refresh();

                vehicleProvider = new TrackingAccessProvider(dataRootPath);

                // Delete Spatial fences which is not in current spatial fence layer
                vehicleProvider.DeleteSpatialFencesExcluding(mapModel.SpatialFenceLayer.InternalFeatures);

                // Add or update the spatial fences that already exist
                foreach (Feature feature in mapModel.SpatialFenceLayer.InternalFeatures)
                {
                    // Update Spatial fence by feature Id
                    // if the affected data row number is 0, we will add a new row in the database
                    vehicleProvider.UpdateSpatialFenceByFeature(feature);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Save Spatial Fences");
            }
            finally
            {
                mapModel.MapControl.TrackOverlay.TrackMode = TrackMode.None;
                mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                mapModel.MapControl.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
                mapModel.PopupOverlay.Popups.Clear();
                mapModel.MapControl.Refresh();
                if (drawFenceMode == DrawFenceMode.EditFence || drawFenceMode == DrawFenceMode.DrawNewFence)
                {
                    DrawFenceMode = DrawFenceMode.DrawNewFence;
                    ResetFences();
                }
            }
        }

        private void ResetFences()
        {
            CancelEditCommand.Execute(null);
            mapModel.MapControl.TrackOverlay.MapMouseClick -= TrackOverlay_MapMouseClick;
            mapModel.MapControl.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void ShowPopup(PointShape pointShape, string content)
        {
            Popup popup = new Popup(pointShape);
            PopupUserControl popupUserControl = new PopupUserControl(content);
            popupUserControl.PopupOverlay = mapModel.PopupOverlay;

            popup.Content = popupUserControl;
            mapModel.PopupOverlay.Popups.Add(popup);
            mapModel.PopupOverlay.Refresh();
        }

        private void ClearMeasuresAndPopups()
        {
            foreach (var measureFeature in mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Where(f => f.Tag == "Measure").ToList())
            {
                mapModel.MapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove(measureFeature);
            }

            mapModel.PopupOverlay.Popups.Clear();
            mapModel.MapControl.Refresh(mapModel.MapControl.TrackOverlay);
            mapModel.MapControl.Refresh(mapModel.PopupOverlay);
        }

        private static DateTime GetAdjustedCurrentDateTime()
        {
            // This vehicle tracking sample contains some simulated data
            // This method stores the real time when the application started and reflects to the start sample time
            // When the actual time increments 1 second, the sample time increments 6 seconds
            //
            // To make the application run in real time just have this method return to current date time
            //
            double sampleSecondPerActualSecond = 12;
            double realSpentTime = TimeSpan.FromTicks(DateTime.Now.Ticks - applicationStartTime.Ticks).TotalSeconds;
            int sampleSpentTime = (int)(realSpentTime * sampleSecondPerActualSecond);
            DateTime currentSampleTime = adjustedStartTime.AddSeconds(sampleSpentTime);

            return currentSampleTime;
        }

        private static PointStyle GetVehicleTrailStyle()
        {
            Stream historyStream = MapSuiteSampleHelper.GetResourceStream("trail point.png");
            historyStream.Seek(0, SeekOrigin.Begin);

            return new PointStyle(new GeoImage(historyStream));
        }

        private static PointStyle GetCurrentVehicleStyle(string iconVirtualPath)
        {
            Stream vehicleStream = MapSuiteSampleHelper.GetResourceStream(iconVirtualPath);
            GeoImage vehicleImage = new GeoImage(vehicleStream);

            return new PointStyle(vehicleImage);
        }
    }
}