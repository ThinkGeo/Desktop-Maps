/*===========================================
   Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
   a Client ID and Secret. These were sent to you via email when you signed up
   with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    public class MainViewModel : ViewModelBase
    {
        private const string boundingBoxPreviewOverlayName = "BoundingBoxPreview";
        private const string chartsOverlayName = "ChartsOverlay";
        private const string GraticuleOverlayName = "GraticuleOverlay";
        private const string highlightOverlayName = "highlight";
        private const string ThinkGeoCloudMapsOverlayName = "ThinkGeoCloudMapsOverlay";

        private InMemoryFeatureLayer boundingBoxPreviewLayer;
        private ChartSelectedItem chartSelectedItem = new ChartSelectedItem(string.Empty, null);
        private double overlayOpacity = 1;
        private bool isIdentify = false;
        private bool canHandleExecute = true;
        private bool isOnLoading;
        private bool showOpacityPanel;
        private MapView map;
        private ChartMessage lastLoadedChartMessage;
        private Collection<object> menuItems;

        private Collection<MenuItemMessageHandler> messageHandlers;

        private ICommand clearSelectionCommand;
        private ICommand opacityPanelCloseCommand;
        private ICommand toolBarCommand;

        private Collection<BaseMenuItem> areaDrawingModes;
        private Collection<BaseMenuItem> baseMaps;
        private Collection<BaseMenuItem> colorSchemas;
        private Collection<BaseMenuItem> displayCategorys;
        private Collection<BaseMenuItem> pointDrawingModes;
        private Collection<BaseMenuItem> symbolLabels;
        private BaseMenuItem selectedAreaDrawingMode;
        private BaseMenuItem selectedBaseMap;
        private BaseMenuItem selectedColorSchema;
        private BaseMenuItem selectedDisplayCategory;
        private FeatureInfo selectedFeatureInfo;
        private BaseMenuItem selectedPointDrawingMode;
        private BaseMenuItem selectedSymbolLabel;
        private BaseMenuItem showContourText;
        private BaseMenuItem showingGradicule;
        private BaseMenuItem showLightDescriptions;
        private BaseMenuItem showLights;
        private BaseMenuItem showSoundingText;


        public MainViewModel(MapView map)
        {
            this.map = map;
            map.MapClick += WpfMap_MapClick;
            menuItems = new Collection<object>(MenuItemHelper.GetMenus());

            LoadMessageHandlers();
            SetToolbarMenuItems();

            Messenger.Default.Register<ChartMessage>(this, m => ChartSelectedItem = new ChartSelectedItem(string.Empty, null));
            Messenger.Default.Register<MenuItemMessage>(this, "ShowOpacityPanel", m => ShowOpacityPanel = true);
            Messenger.Default.Register<MenuItemMessage>(this, HandleMenuItemMessage);
            Messenger.Default.Register<ToolBarMessage>(this, HandleToolBarMessage);
            Messenger.Default.Register<ChartMessage>(this, "LoadCharts", HandleLoadChartMessage);
            Messenger.Default.Register<ChartMessage>(this, "ReloadCharts", HandleReloadChartsMessage);
            Messenger.Default.Register<ChartMessage>(this, "UnloadCharts", HandleUnloadChartMessage);
            Messenger.Default.Register<ChartSelectedItemMessage>(this, HandleChartSelectedItemMessage);
            Messenger.Default.Register<SafeWaterDepthSettingMessage>(this, HandleSafeWaterDepthMessage);

            map.MapUnit = GeographyUnit.Meter;
            map.ZoomScales = new ThinkGeoCloudMapsZoomLevelSet().GetScales();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            //ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            //map.Overlays.Add(ThinkGeoCloudMapsOverlayName, baseOverlay);

            InitBoundingBoxPreviewOverlay(map);
        }

        public Collection<BaseMenuItem> AreaDrawingModes
        {
            get { return areaDrawingModes; }
        }

        public Collection<BaseMenuItem> BaseMaps
        {
            get { return baseMaps; }
        }

        public ChartSelectedItem ChartSelectedItem
        {
            get { return chartSelectedItem; }
            set
            {
                if (chartSelectedItem != value)
                {
                    chartSelectedItem = value;
                    OnPropertyChanged(nameof(ChartSelectedItem));
                }
            }
        }

        public ICommand ClearSelectionCommand
        {
            get { return clearSelectionCommand ?? (clearSelectionCommand = new RelayCommand(HandleClearSelectionCommand)); }
        }

        public Collection<BaseMenuItem> ColorSchemas
        {
            get { return colorSchemas; }
        }

        public Collection<BaseMenuItem> DisplayCategorys
        {
            get { return displayCategorys; }
        }

        public bool IsIdentify
        {
            get { return isIdentify; }
            set
            {
                map.Cursor = (value ? Cursors.Cross : null);
                isIdentify = value;
            }
        }

        public bool IsOnLoading
        {
            get { return isOnLoading; }
            set
            {
                isOnLoading = value;
                OnPropertyChanged(nameof(IsOnLoading));
            }
        }

        public Collection<object> MenuItems
        {
            get { return menuItems; }
        }

        public ICommand OpacityPanelCloseCommand
        {
            get { return opacityPanelCloseCommand ?? (opacityPanelCloseCommand = new RelayCommand(() => ShowOpacityPanel = false)); }
        }

        public double OverlayOpacity
        {
            get { return overlayOpacity; }
            set
            {
                if (Math.Abs(overlayOpacity - value) > double.Epsilon)
                {
                    overlayOpacity = value;
                    ApplyOverlayOpacity();
                    OnPropertyChanged(nameof(OverlayOpacity));
                }
            }
        }

        public Collection<BaseMenuItem> PointDrawingModes
        {
            get { return pointDrawingModes; }
        }

        public BaseMenuItem SelectedAreaDrawingMode
        {
            get { return selectedAreaDrawingMode; }
            set
            {
                if (selectedAreaDrawingMode != value)
                {
                    selectedAreaDrawingMode = value;
                    if (canHandleExecute)
                    {
                        value.SelectedCommand.Execute(null);
                    }
                    OnPropertyChanged(nameof(SelectedAreaDrawingMode));
                }
            }
        }

        public BaseMenuItem SelectedBaseMap
        {
            get { return selectedBaseMap; }
            set
            {
                selectedBaseMap = value;
                if (canHandleExecute)
                {
                    value.SelectedCommand.Execute(null);
                }
                OnPropertyChanged(nameof(SelectedBaseMap));
            }
        }

        public BaseMenuItem SelectedColorSchema
        {
            get { return selectedColorSchema; }
            set
            {
                if (selectedColorSchema != value)
                {
                    selectedColorSchema = value;
                    if (canHandleExecute)
                    {
                        value.SelectedCommand.Execute(null);
                    }
                    OnPropertyChanged(nameof(SelectedColorSchema));
                }
            }
        }

        public BaseMenuItem SelectedDisplayCategory
        {
            get { return selectedDisplayCategory; }
            set
            {
                if (selectedDisplayCategory != value)
                {
                    selectedDisplayCategory = value;
                    if (canHandleExecute)
                    {
                        value.SelectedCommand.Execute(null);
                    }
                    OnPropertyChanged(nameof(SelectedDisplayCategory));
                }
            }
        }

        public FeatureInfo SelectedFeatureInfo
        {
            get { return selectedFeatureInfo; }
            set
            {
                if (selectedFeatureInfo != value)
                {
                    selectedFeatureInfo = value;
                    HandleFeatureSelectedChanged(value);
                    OnPropertyChanged(nameof(SelectedFeatureInfo));
                }
            }
        }

        public BaseMenuItem SelectedPointDrawingMode
        {
            get { return selectedPointDrawingMode; }
            set
            {
                if (selectedPointDrawingMode != value)
                {
                    selectedPointDrawingMode = value;
                    if (canHandleExecute)
                    {
                        value.SelectedCommand.Execute(null);
                    }
                    OnPropertyChanged(nameof(SelectedPointDrawingMode));
                }
            }
        }

        public BaseMenuItem SelectedSymbolLabel
        {
            get { return selectedSymbolLabel; }
            set
            {
                if (selectedSymbolLabel != value)
                {
                    selectedSymbolLabel = value;
                    if (canHandleExecute)
                    {
                        value.SelectedCommand.Execute(null);
                    }
                    OnPropertyChanged(nameof(SelectedSymbolLabel));
                }
            }
        }

        public BaseMenuItem ShowContourText
        {
            get { return showContourText; }
            set { showContourText = value; }
        }

        public BaseMenuItem ShowingGradicule
        {
            get { return showingGradicule; }
            set
            {
                if (showingGradicule != value)
                {
                    showingGradicule = value;
                    OnPropertyChanged(nameof(ShowingGradicule));
                }
            }
        }

        public BaseMenuItem ShowLightDescriptions
        {
            get { return showLightDescriptions; }
            set { showLightDescriptions = value; }
        }

        public BaseMenuItem ShowLights
        {
            get { return showLights; }
            set { showLights = value; }
        }

        public bool ShowOpacityPanel
        {
            get { return showOpacityPanel; }
            set
            {
                if (showOpacityPanel != value)
                {
                    showOpacityPanel = value;
                    OnPropertyChanged(nameof(ShowOpacityPanel));
                }
            }
        }

        public BaseMenuItem ShowSoundingText
        {
            get { return showSoundingText; }
            set { showSoundingText = value; }
        }

        public Collection<BaseMenuItem> SymbolLabels
        {
            get { return symbolLabels; }
        }

        public ICommand ToolBarCommand
        {
            get { return toolBarCommand ?? (toolBarCommand = new RelayCommand<string>(HandleToolBarCommand)); }
        }

        public override void Cleanup()
        {
            map.MapClick -= WpfMap_MapClick;
            base.Cleanup();
        }

        internal async void ApplyOverlayOpacity()
        {
            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay overlay = ((LayerOverlay)map.Overlays[chartsOverlayName]);
                overlay.Opacity = OverlayOpacity;
                await map.RefreshAsync(overlay);
            }
        }

        private static BaseMenuItem GetMenuByAction(string action, Collection<object> sourceMenus)
        {
            if (sourceMenus != null)
            {
                foreach (object item in sourceMenus)
                {
                    if (item is CompositeMenuItem)
                    {
                        CompositeMenuItem compositeMenuItem = ((CompositeMenuItem)item);
                        BaseMenuItem menu = GetMenuByAction(action, compositeMenuItem.Children);
                        if (menu != null)
                        {
                            return menu;
                        }
                    }
                    else if (item is SingleMenuItem)
                    {
                        SingleMenuItem singleMenuItem = ((SingleMenuItem)item);
                        if (string.Compare(singleMenuItem.Action, action, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            return singleMenuItem;
                        }
                    }
                }
            }
            return null;
        }

        private static Collection<BaseMenuItem> GetMenusByGroupName(string groupName, Collection<object> sourceMenus)
        {
            var menus = new Collection<BaseMenuItem>();
            if (sourceMenus != null)
            {
                foreach (object item in sourceMenus)
                {
                    if (item is CompositeMenuItem)
                    {
                        CompositeMenuItem compositeMenuItem = ((CompositeMenuItem)item);
                        Collection<BaseMenuItem> recursions = GetMenusByGroupName(groupName, compositeMenuItem.Children);
                        foreach (BaseMenuItem recursionItem in recursions)
                        {
                            menus.Add(recursionItem);
                        }
                    }
                    else if (item is SingleMenuItem)
                    {
                        SingleMenuItem singleMenuItem = ((SingleMenuItem)item);
                        if (string.Compare(singleMenuItem.GroupName, groupName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            menus.Add((BaseMenuItem)item);
                        }
                    }
                }
            }
            return menus;
        }

        private LayerOverlay CreateHighlightLayerOverlay(Feature feature)
        {
            LayerOverlay highlightOverlay = new LayerOverlay();
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            //GeoHatchBrush fillBrush = new GeoHatchBrush(GeoHatchStyle.Cross, GeoColor.FromArgb(100, 0, 255, 0));
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateHatchStyle(GeoHatchStyle.BackwardDiagonal, GeoColor.FromArgb(150, 254, 255, 39), GeoColors.Transparent);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(50, GeoColors.Green), 3, false);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryFeatureLayer.InternalFeatures.Add(feature);

            highlightOverlay.Layers.Add(inMemoryFeatureLayer);
            return highlightOverlay;
        }

        private void HandleChartSelectedItemMessage(ChartSelectedItemMessage message)
        {
            throw new System.NotImplementedException();
        }

        private void HandleClearSelectionCommand()
        {
            ChartSelectedItem = new ChartSelectedItem(chartSelectedItem.FullName, new List<FeatureInfo>());
        }

        private async void HandleFeatureSelectedChanged(object item)
        {
            FeatureInfo featureInfo = item as FeatureInfo;

            if (!map.Overlays.Contains(chartsOverlayName))
            {
                return;
            }
            if (map.Overlays.Contains(highlightOverlayName))
            {
                map.Overlays.Remove(highlightOverlayName);
            }

            if (featureInfo != null)
            {
                LayerOverlay overlay = map.Overlays[chartsOverlayName] as LayerOverlay;
                NauticalChartsFeatureLayer layer = overlay.Layers[featureInfo.LayerName] as NauticalChartsFeatureLayer;
                layer.Open();
                Feature feature = layer.QueryTools.GetFeatureById(featureInfo.Id, ReturningColumnsType.AllColumns);
                layer.Close();

                if (feature != null)
                {
                    LayerOverlay highlightOverlay = CreateHighlightLayerOverlay(feature);
                    map.Overlays.Add(highlightOverlayName, highlightOverlay);
                    // Highlight the selected feature in place; do not move/zoom the map to it.
                }
            }

            await map.RefreshAsync();
        }

        private async void HandleLoadChartMessage(ChartMessage message)
        {
            await LoadChartsAsync(message, preserveExtent: false);
        }

        // Re-runs the load with the last-loaded charts so that edits saved by the
        // "Edit Symbol File" editor (which writes back to Globals.StyleFilePath) show up
        // on the map. The current view is preserved instead of zooming to full extent.
        private async void HandleReloadChartsMessage(ChartMessage message)
        {
            if (lastLoadedChartMessage != null)
            {
                await LoadChartsAsync(lastLoadedChartMessage, preserveExtent: true);
            }
        }

        private async Task LoadChartsAsync(ChartMessage message, bool preserveExtent)
        {
            lastLoadedChartMessage = message;
            RectangleShape savedExtent = preserveExtent ? map.CurrentExtent : null;
            Globals.EnsureStyleFile();
            LayerOverlay overlay = null;
            if (message.Charts != null)
            {
                if (map.Overlays.Contains(chartsOverlayName))
                {
                    overlay = ((LayerOverlay)map.Overlays[chartsOverlayName]);
                }
                else
                {
                    overlay = new LayerOverlay()
                    {
                        TileType = TileType.SingleTile,
                    };
                    map.Overlays.Insert(1, chartsOverlayName, overlay);
                }

                overlay.Layers.Clear();
                ChartSelectedItem = new ChartSelectedItem(string.Empty, null);
                IEnumerable<ChartItem> charts = message.Charts;
                RectangleShape boundingBox = null;
                foreach (ChartItem item in charts)
                {
                    if (!File.Exists(item.IndexFileName))
                    {
                        NauticalChartsFeatureSource.BuildIndexFile(item.FileName, BuildIndexMode.DoNotRebuild);
                    }
                    NauticalChartsFeatureLayer layer = new NauticalChartsFeatureLayer(item.FileName, Globals.StyleFilePath);
                    if (map.MapUnit == GeographyUnit.Meter)
                    {
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
                    }

                    layer.DrawingFeatures += hydrographyLayer_DrawingFeatures;

                    layer.IsDepthContourTextVisible = Globals.IsDepthContourTextVisible;
                    layer.IsLightDescriptionVisible = Globals.IsLightDescriptionVisible;
                    layer.IsSoundingTextVisible = Globals.IsSoundingTextVisible;
                    layer.SymbolTextDisplayMode = Globals.SymbolTextDisplayMode;
                    layer.DisplayCategory = Globals.DisplayMode;
                    layer.DefaultColorSchema = Globals.CurrentColorSchema;
                    layer.SymbolDisplayMode = Globals.CurrentSymbolDisplayMode;
                    layer.BoundaryDisplayMode = Globals.CurrentBoundaryDisplayMode;

                    layer.SafetyDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(Globals.SafetyDepth, Globals.CurrentDepthUnit);
                    layer.ShallowDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(Globals.ShallowDepth, Globals.CurrentDepthUnit);
                    layer.DeepDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(Globals.DeepDepth, Globals.CurrentDepthUnit);
                    layer.SafetyContourDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(Globals.SafetyContour, Globals.CurrentDepthUnit);

                    layer.DrawingMode = NauticalChartsDrawingMode.HightQuality;
                    layer.IsFullLightLineVisible = Globals.IsFullLightLineVisible;
                    layer.IsMetaObjectsVisible = Globals.IsMetaObjectsVisible;
                    layer.IsShallowWaterPatternVisible = Globals.IsShallowWaterPatternVisible;
                    layer.IsIsolatedDangerInShallowWaterVisible = Globals.IsIsolatedDangerVisible;
                    layer.IsMinimumScaleEnabled = Globals.IsMinimumScaleEnabled;
                    layer.DepthShades = Globals.CurrentDepthShades;
                    layer.StylingType = Globals.CurrentStylingType;
                    // Plain fallback styles, used only when StylingType == StandardStyling (S-52 off),
                    // so toggling "S-52 Styling" off shows the raw ENC geometry instead of a blank map.
                    layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.SteelBlue), GeoColors.Gray);
                    layer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Gray, 1, false);
                    layer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.DarkRed, 4);
                    layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                    layer.Name = item.FileName;
                    layer.Open();
                    if (boundingBox == null)
                    {
                        boundingBox = layer.GetBoundingBox();
                    }
                    else
                    {
                        boundingBox.ExpandToInclude(layer.GetBoundingBox());
                    }

                    boundingBoxPreviewLayer.InternalFeatures.Add(layer.GetHashCode().ToString(), new Feature(layer.GetBoundingBox()));

                    layer.Close();
                    overlay.Layers.Add(item.FileName, layer);
                }
                // On reload (after a symbol edit) keep the user's current view; on a fresh
                // load zoom to the charts' full extent.
                RectangleShape targetExtent = (preserveExtent && savedExtent != null) ? savedExtent : boundingBox;
                RectangleShape preserveBoundingBox = null;
                if (targetExtent != null)
                {
                    map.CurrentExtent = targetExtent;
                    preserveBoundingBox = targetExtent;
                }

                //SetupAnimationForOverlay(overlay);

                ApplyOverlayOpacity();

                map.Visibility = Visibility.Hidden;

                await map.RefreshAsync();
                
                if (!map.CurrentExtent.Equals(preserveBoundingBox))
                {
                    map.CurrentExtent = preserveBoundingBox;
                    await map.Overlays[chartsOverlayName].RefreshAsync();
                    await map.RefreshAsync(); // Redraw with the correct extent
                }

                map.Visibility = Visibility.Visible;
            }
        }

        private void HandleMenuItemMessage(MenuItemMessage message)
        {
            if (message != null && message.MenuItem != null && !string.IsNullOrEmpty(message.MenuItem.Action))
            {
                foreach (var messageHandler in messageHandlers)
                {
                    if (messageHandler.Actions.Contains(message.MenuItem.Action))
                    {
                        messageHandler.Handle(Application.Current.MainWindow, map, message);
                    }
                }
            }
        }

        private async void HandleSafeWaterDepthMessage(SafeWaterDepthSettingMessage message)
        {
            if (!map.Overlays.Contains(chartsOverlayName))
            {
                return;
            }

            LayerOverlay overlay = map.Overlays[chartsOverlayName] as LayerOverlay;
            var layers = overlay.Layers.OfType<NauticalChartsFeatureLayer>();
            foreach (NauticalChartsFeatureLayer layer in layers)
            {
                layer.SafetyDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(message.SafetyWaterDepth, message.DepthUnit);
                layer.ShallowDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(message.ShallowWaterDepth, message.DepthUnit);
                layer.DeepDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(message.DeepWaterDepth, message.DepthUnit);
                layer.SafetyContourDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(message.SafetyContourDepth, message.DepthUnit);
            }
            Globals.SafetyDepth = message.SafetyWaterDepth;
            Globals.DeepDepth = message.DeepWaterDepth;
            Globals.ShallowDepth = message.ShallowWaterDepth;
            Globals.SafetyContour = message.SafetyContourDepth;
            Globals.CurrentDepthUnit = message.DepthUnit;
            await map.RefreshAsync();
        }

        private void HandleToolBarCommand(string action)
        {
            var message = new ToolBarMessage(action);
            Messenger.Default.Send(message);
        }

        private void HandleToolBarMessage(ToolBarMessage message)
        {
            if (message != null && !string.IsNullOrEmpty(message.Action))
            {
                switch (message.Action.ToLower())
                {
                    case "loadcharts":
                        {
                            var window = new ChartsManagmentWindow();
                            window.Owner = Application.Current.MainWindow;
                            window.ShowDialog();
                        }
                        break;
                }
            }
        }

        private async void HandleUnloadChartMessage(ChartMessage message)
        {
            if (message.Charts != null)
            {
                if (map.Overlays.Contains(chartsOverlayName))
                {
                    LayerOverlay overlay = ((LayerOverlay)map.Overlays[chartsOverlayName]);
                    foreach (ChartItem item in message.Charts)
                    {
                        for (int i = overlay.Layers.Count - 1; i >= 0; i--)
                        {
                            LayerBase layer = overlay.Layers[i];
                            if (item.FileName == layer.Name)
                            {
                                overlay.Layers.Remove(layer);

                                boundingBoxPreviewLayer.InternalFeatures.Remove(layer.GetHashCode().ToString());
                                break;
                            }
                        }
                        if (ChartSelectedItem != null && ChartSelectedItem.FullName == item.FileName)
                        {
                            ChartSelectedItem = new ChartSelectedItem(string.Empty, null);
                        }
                    }

                    RectangleShape boundingBox = null;
                    foreach (Layer layer in overlay.Layers)
                    {
                        layer.Open();
                        if (boundingBox == null)
                        {
                            boundingBox = layer.GetBoundingBox();
                        }
                        else
                        {
                            boundingBox.ExpandToInclude(layer.GetBoundingBox());
                        }
                        layer.Close();
                    }
                    if (boundingBox != null)
                    {
                        map.CurrentExtent = boundingBox;
                    }

                    await map.RefreshAsync();
                }
            }
        }

        private void hydrographyLayer_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            if (!IsHydrographyLayerVisiable())
            {
                e.Cancel = true;
            }
        }

        private async void InitBoundingBoxPreviewOverlay(MapView map)
        {
            boundingBoxPreviewLayer = new InMemoryFeatureLayer();
            boundingBoxPreviewLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Blue);
            boundingBoxPreviewLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay boundingBoxPreviewOverlay = new LayerOverlay();
            boundingBoxPreviewOverlay.Layers.Add(boundingBoxPreviewLayer);
            await boundingBoxPreviewOverlay.OpenAsync();
            map.Overlays.Add(boundingBoxPreviewOverlayName, boundingBoxPreviewOverlay);
        }

        private bool IsHydrographyLayerVisiable()
        {
            if (boundingBoxPreviewLayer != null)
            {
                if (boundingBoxPreviewLayer.GetBoundingBox().Intersects(map.CurrentExtent))
                {
                    return true;
                }
            }

            return false;
        }

        private void LoadMessageHandlers()
        {
            // Explicit registry of the toolbar/menu command handlers. To add a command, create a
            // MenuItemMessageHandler subclass, add it here, and add its <menuItem action="..."> to
            // Resource/Menus.xml. (Previously these were discovered via MEF; a plain list is simpler
            // and compile-checked for a single-assembly sample.)
            messageHandlers = new Collection<MenuItemMessageHandler>
            {
                new AreaDrawingModeMenuItemMessageHandler(),
                new ChartManagmentMenuItemMessageHandler(),
                new ColorSchemaMenuItemMessageHandler(),
                new DisplayCategoryMenuItemMessageHandler(),
                new ExitMenuItemMessageHandler(),
                new GraticleMenuItemMessageHandler(),
                new HomePageMenuItemMessageHandler(),
                new IndexBuildingMenuItemMessageHandler(),
                new LightsMenuItemMessageHandler(),
                new MetaObjectsMenuItemMessageHandler(),
                new OpacityMenuItemMessageHandler(),
                new PointDrawingModeMenuItemMessageHandler(),
                new SafeWaterDepthMenuItemMessageHandler(),
                new S52OptionsMenuItemMessageHandler(),
                new SymbolsCreatingMenuItemMessageHandler(),
                new SymbolsEditionMenuItemMessageHandler(),
                new TextVisibilibyMenuItemMessageHandler(),
                new WorldMapShowingMenuItemMessageHandler(),
            };
        }

        private void MenuItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Compare(e.PropertyName, "IsChecked", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                BaseMenuItem menuItem = sender as BaseMenuItem;
                if (menuItem.IsChecked)
                {
                    canHandleExecute = false;
                    switch (menuItem.GroupName.ToLowerInvariant())
                    {
                        case "s52areadrawingmode":
                            SelectedAreaDrawingMode = menuItem;
                            break;

                        case "colorschema":
                            SelectedColorSchema = menuItem;
                            break;

                        case "s52pointdrawingmode":
                            SelectedPointDrawingMode = menuItem;
                            break;

                        case "showworldmap":
                            SelectedBaseMap = menuItem;
                            break;

                        case "displaycategory":
                            SelectedDisplayCategory = menuItem;
                            break;

                        case "textlabel":
                            SelectedSymbolLabel = menuItem;
                            break;
                    }
                    canHandleExecute = true;
                }
            }
        }

        //private void overlay_Drawing(object sender, DrawingOverlayEventArgs e)
        //{
        //    if (IsHydrographyLayerVisiable())
        //    {
        //        IsOnLoading = true;
        //    }
        //}

        //private void overlay_Drawn(object sender, DrawnOverlayEventArgs e)
        //{
        //    IsOnLoading = false;
        //}

        private void SetToolbarMenuItems()
        {
            areaDrawingModes = GetMenusByGroupName("s52areadrawingmode", menuItems);
            foreach (BaseMenuItem menuItem in areaDrawingModes)
            {
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
            }
            SelectedAreaDrawingMode = areaDrawingModes.FirstOrDefault(p => p.IsChecked);
            colorSchemas = GetMenusByGroupName("colorschema", menuItems);
            foreach (BaseMenuItem menuItem in colorSchemas)
            {
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
            }
            SelectedColorSchema = colorSchemas.FirstOrDefault(p => p.IsChecked);
            pointDrawingModes = GetMenusByGroupName("s52pointdrawingmode", menuItems);
            foreach (BaseMenuItem menuItem in pointDrawingModes)
            {
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
            }
            SelectedPointDrawingMode = pointDrawingModes.FirstOrDefault(p => p.IsChecked);

            baseMaps = GetMenusByGroupName("showworldmap", menuItems);
            foreach (BaseMenuItem menuItem in baseMaps)
            {
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
            }
            SelectedBaseMap = baseMaps.FirstOrDefault(m => m.IsChecked);

            displayCategorys = GetMenusByGroupName("displaycategory", menuItems);
            foreach (BaseMenuItem menuItem in displayCategorys)
            {
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
            }
            SelectedDisplayCategory = displayCategorys.FirstOrDefault(m => m.IsChecked);

            symbolLabels = GetMenusByGroupName("textlabel", menuItems);
            foreach (BaseMenuItem menuItem in symbolLabels)
            {
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
            }
            SelectedSymbolLabel = symbolLabels.FirstOrDefault(p => p.IsChecked);

            showingGradicule = GetMenuByAction("graticule", menuItems);
            showLights = GetMenuByAction("lights", menuItems);

            showContourText = GetMenuByAction("contourlabel", menuItems);
            showSoundingText = GetMenuByAction("soundinglabel", menuItems);
            showLightDescriptions = GetMenuByAction("lightdescription", menuItems);
        }

        //private void SetupAnimationForOverlay(LayerOverlay overlay)
        //{
        //    overlay.Drawing -= overlay_Drawing;
        //    overlay.Drawing += overlay_Drawing;
        //    overlay.Drawn -= overlay_Drawn;
        //    overlay.Drawn += overlay_Drawn;
        //}

        private async void WpfMap_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (isIdentify)
            {
                PointShape point = e.WorldLocation;
                if (!map.Overlays.Contains(chartsOverlayName))
                {
                    return;
                }
                LayerOverlay overlay = map.Overlays[chartsOverlayName] as LayerOverlay;

                var features = new Collection<Feature>();
                NauticalChartsFeatureLayer hydrographyFeatureLayer = null;
                foreach (var item in overlay.Layers)
                {
                    NauticalChartsFeatureLayer itemLayer = item as NauticalChartsFeatureLayer;
                    itemLayer.Open();
                    features = itemLayer.QueryTools.GetFeaturesIntersecting(point.GetBoundingBox(), ReturningColumnsType.AllColumns);

                    if (features.Count > 0)
                    {
                        hydrographyFeatureLayer = itemLayer;
                        break;
                    }
                }

                if (features.Count > 0)
                {
                    List<FeatureInfo> selectedFeatures = new List<FeatureInfo>();

                    foreach (var item in features)
                    {
                        double area = double.MaxValue;
                        PolygonShape areaShape = item.GetShape() as PolygonShape;
                        if (areaShape != null)
                        {
                            area = areaShape.GetArea(map.MapUnit, AreaUnit.SquareMeters);
                        }
                        NauticalChartsFeatureDescription description = hydrographyFeatureLayer.GetFeatureDescription(item);
                        selectedFeatures.Add(new FeatureInfo(item, description, hydrographyFeatureLayer.Name, area));
                    }

                    if (map.Overlays.Contains(highlightOverlayName))
                    {
                        map.Overlays.Remove(highlightOverlayName);
                    }

                    IEnumerable<FeatureInfo> featureInfos = selectedFeatures.OrderBy(p => p.Area);
                    SelectedFeatureInfo = featureInfos.FirstOrDefault();
                    NauticalChartsFeatureSource featureSource = hydrographyFeatureLayer.FeatureSource as NauticalChartsFeatureSource;
                    if (featureSource != null)
                    {
                        ChartSelectedItem = new ChartSelectedItem(featureSource.FilePath, featureInfos);
                    }
                }
                else
                {
                    if (map.Overlays.Contains(highlightOverlayName))
                    {
                        map.Overlays.Remove(highlightOverlayName);
                    }
                    await map.RefreshAsync();
                }
            }
        }
    }
}
