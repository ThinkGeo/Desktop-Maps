using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Wpf;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string displayColorText;
        private double coordinateX;
        private double coordinateY;
        private ToolTip toolTip;
        private Visibility dotDensityVisibility;
        private Visibility valueCircleVisibility;
        private Visibility thematicVisibility;
        private ObservableCollection<DataCategoryViewModel> categoryList;
        private ObservableCollection<GeoColorViewModel> geoColorList;
        private ObservableCollection<ColorWheelDirection> colorWheelDirections;
        private GeoColorViewModel selectedColorItem;
        private GeoColorViewModel selectedEndColorItem;
        private DataCategoryViewModel selectedCategory;
        private ValueCircleDemographicStyleBuilder valueCircleStyleBuilder;
        private DotDensityDemographicStyleBuilder dotDensityStyleBuilder;
        private ThematicDemographicStyleBuilder thematicStyleBuilder;
        private PieChartDemographicStyleBuilder pieStyleBuilder;
        private DemographicStyleBuilder currentStyleBuilder;
        private MapModel mapModel;

        public MainWindowViewModel(MapModel mapModel)
        {
            this.mapModel = mapModel;

            InitializeProperties();
            InitializetionDataCategories();
            InitializeGeoSimpleColorsList();
            InitializeEvents(mapModel);

            UpdateUIControls(ThematicDemographicStyle);
            SelectedCategory = CategoryList[0];
            CurrentStyleBuilder.SelectedColumns.Add(SelectedCategory.Columns[0].ColumnName);
            mapModel.LegendTitle = SelectedCategory.Columns[0].LegendTitle;
        }

        public Visibility DotDensityVisibility
        {
            get { return dotDensityVisibility; }
            set
            {
                dotDensityVisibility = value;
                RaisePropertyChanged(() => DotDensityVisibility);
            }
        }

        public Visibility ValueCircleVisibility
        {
            get { return valueCircleVisibility; }
            set
            {
                valueCircleVisibility = value;
                RaisePropertyChanged(() => ValueCircleVisibility);
            }
        }

        public Visibility ThematicVisibility
        {
            get { return thematicVisibility; }
            set
            {
                thematicVisibility = value;
                RaisePropertyChanged(() => ThematicVisibility);
            }
        }

        public string DisplayColorText
        {
            get { return displayColorText; }
            set
            {
                displayColorText = value;
                RaisePropertyChanged(() => DisplayColorText);
            }
        }

        public ObservableCollection<DataCategoryViewModel> CategoryList
        {
            get { return categoryList; }
        }

        public ObservableCollection<ColorWheelDirection> ColorWheelDirections
        {
            get { return colorWheelDirections; }
        }

        public DotDensityDemographicStyleBuilder DotDensityDemographicStyle
        {
            get { return dotDensityStyleBuilder; }
        }

        public ObservableCollection<GeoColorViewModel> GeoColorList
        {
            get { return geoColorList; }
        }

        public PieChartDemographicStyleBuilder PieDemographicStyle
        {
            get { return pieStyleBuilder; }
        }

        public GeoColorViewModel SelectedColorItem
        {
            get { return selectedColorItem; }
            set
            {
                selectedColorItem = value;
                RaisePropertyChanged(() => SelectedColorItem);
                if (CurrentStyleBuilder != null && CurrentStyleBuilder.Color != selectedColorItem.GeoColor)
                {
                    CurrentStyleBuilder.Color = selectedColorItem.GeoColor;
                    UpdateMap();
                }
            }
        }

        public DataCategoryViewModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }

        public ColorWheelDirection SelectedColorWheelDirection
        {
            get { return thematicStyleBuilder.ColorWheelDirection; }
            set
            {
                thematicStyleBuilder.ColorWheelDirection = value;
                RaisePropertyChanged(() => SelectedColorWheelDirection);
                if (CurrentStyleBuilder != null)
                {
                    UpdateMap();
                }
            }
        }

        public GeoColorViewModel SelectedEndColorItem
        {
            get { return selectedEndColorItem; }
            set
            {
                selectedEndColorItem = value;
                RaisePropertyChanged(() => SelectedEndColorItem);
                if (ThematicDemographicStyle != null && ThematicDemographicStyle.EndColor != selectedEndColorItem.GeoColor)
                {
                    ThematicDemographicStyle.EndColor = selectedEndColorItem.GeoColor;
                    UpdateMap();
                }
            }
        }

        public ThematicDemographicStyleBuilder ThematicDemographicStyle
        {
            get { return thematicStyleBuilder; }
        }

        public ValueCircleDemographicStyleBuilder ValueCircleDemographicStyle
        {
            get { return valueCircleStyleBuilder; }
        }

        public DemographicStyleBuilder CurrentStyleBuilder
        {
            get { return currentStyleBuilder; }
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

        public void CreatePieDemographicStyle()
        {
            PieDemographicStyle.SelectedColumnAliases.Clear();
            PieDemographicStyle.SelectedColumns.Clear();

            foreach (var item in SelectedCategory.Columns)
            {
                item.CheckBoxVisiblity = Visibility.Visible;
            }

            foreach (var item in SelectedCategory.Columns.Where(d => d.IsSelected))
            {
                PieDemographicStyle.SelectedColumns.Add(item.ColumnName);
                PieDemographicStyle.SelectedColumnAliases.Add(item.Alias);
            }

            UpdateUIControls(PieDemographicStyle);
            mapModel.LegendTitle = SelectedCategory.Title;
        }

        public void UpdateMap()
        {
            Collection<Styles.Style> activeStyles = CurrentStyleBuilder.GetStyles(mapModel.DefaultFeatureLayer.FeatureSource);
            mapModel.DefaultFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            foreach (var activeStyle in activeStyles)
            {
                mapModel.DefaultFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(activeStyle);
            }
            mapModel.DefaultFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            UpdateLegend(CurrentStyleBuilder, mapModel.LegendAdornmentLayer);
            mapModel.MapControl.Refresh(new Overlay[] { mapModel.DemographicLayerOverlay, mapModel.MapControl.AdornmentOverlay });
        }

        public void UpdateUIControls(DemographicStyleBuilder activeSettingViewModel)
        {
            GeoColor[] colors = GeoColorList.Select(c => c.GeoColor).ToArray();
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].Equals(activeSettingViewModel.Color))
                {
                    currentStyleBuilder = activeSettingViewModel;
                    SelectedColorItem = GeoColorList[i];
                    break;
                }
            }
            ThematicDemographicStyleBuilder thematicDemographicStyleBuilder = activeSettingViewModel as ThematicDemographicStyleBuilder;
            if (thematicDemographicStyleBuilder != null)
            {
                GeoColor styleColor = thematicDemographicStyleBuilder.EndColor;
                for (int i = 0; i < colors.Length; i++)
                {
                    if (colors[i].Equals(styleColor))
                    {
                        currentStyleBuilder = activeSettingViewModel;
                        SelectedEndColorItem = GeoColorList[i];
                        break;
                    }
                }
            }
        }

        private void InitializeProperties()
        {
            toolTip = new ToolTip();
            DotDensityVisibility = Visibility.Collapsed;
            ValueCircleVisibility = Visibility.Collapsed;
            DisplayColorText = "Display Color:";
            colorWheelDirections = new ObservableCollection<ColorWheelDirection>();
            colorWheelDirections.Add(ColorWheelDirection.Clockwise);
            colorWheelDirections.Add(ColorWheelDirection.CounterClockwise);
            categoryList = new ObservableCollection<DataCategoryViewModel>();
            valueCircleStyleBuilder = new ValueCircleDemographicStyleBuilder();
            dotDensityStyleBuilder = new DotDensityDemographicStyleBuilder();
            pieStyleBuilder = new PieChartDemographicStyleBuilder();
            thematicStyleBuilder = new ThematicDemographicStyleBuilder();
        }

        private void InitializeEvents(MapModel mapModel)
        {
            mapModel.MapControl.MapTools.MouseCoordinate.CustomFormatted += MouseCoordinate_CustomFormatted;
            mapModel.MapControl.MouseMove += WpfMap_MouseMove;
        }

        private void WpfMap_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip.IsOpen = false;
            mapModel.HighlightOverlay.HighlightFeature = null;
            if (!mapModel.HighlightOverlay.IsPanningMap)
            {
                Point screenLocation = e.GetPosition(mapModel.MapControl);
                PointShape worldLoction = ExtentHelper.ToWorldCoordinate(mapModel.MapControl.CurrentExtent, new ScreenPointF((float)screenLocation.X, (float)screenLocation.Y), (float)mapModel.MapControl.ActualWidth, (float)mapModel.MapControl.ActualHeight);

                // Here we get tootip for the highlighted feature. 
                mapModel.DefaultFeatureLayer.Open();
                List<string> columnNames = mapModel.DefaultFeatureLayer.FeatureSource.GetColumns().Select(x => x.ColumnName).ToList();
                Collection<Feature> nearestFeatures = mapModel.DefaultFeatureLayer.QueryTools.GetFeaturesNearestTo(worldLoction, mapModel.MapControl.MapUnit, 1, columnNames, 1, DistanceUnit.Meter);
                if (nearestFeatures.Count > 0)
                {
                    var highlightedFeature = nearestFeatures[0];
                    mapModel.HighlightOverlay.HighlightFeature = highlightedFeature;
                    mapModel.HighlightOverlay.Refresh();
                    var content = GetToolTip(highlightedFeature);
                    toolTip.Content = content;
                    toolTip.IsOpen = true;
                }
            }
            mapModel.HighlightOverlay.Refresh();
        }

        private void MouseCoordinate_CustomFormatted(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            CoordinateX = e.WorldCoordinate.X;
            CoordinateY = e.WorldCoordinate.Y;
        }

        private void InitializeGeoSimpleColorsList()
        {
            geoColorList = new ObservableCollection<GeoColorViewModel>();
            PropertyInfo[] propertyInfos = GeoColor.SimpleColors.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object value = propertyInfo.GetValue(GeoColor.SimpleColors, null);
                string name = propertyInfo.Name;

                if (name.ToUpperInvariant() == "TRANSPARENT")
                {
                    continue;
                }

                if (value.GetType() == typeof(GeoColor))
                {
                    GeoColorViewModel colorItem = new GeoColorViewModel((GeoColor)value, name);
                    geoColorList.Add(colorItem);
                }
            }
        }

        // Here we load all the nodes on the left from xml file. 
        private void InitializetionDataCategories()
        {
            var xDoc = XDocument.Load(MapSuiteSampleHelper.GetValueFromConfiguration("CategoryFilePath"));
            var elements = from category in xDoc.Element("DemographicMap").Elements("Category")
                           select category;

            foreach (var element in elements)
            {
                DataCategoryViewModel category = null;
                string title = element.Attribute("name").Value;
                category = title != "Custom Data" ? new DataCategoryViewModel() : new CustomDataCategoryViewModel();
                category.Title = title;
                category.CategoryImage = new BitmapImage(new Uri(string.Format(CultureInfo.InvariantCulture, "{0}{1}", "pack://application:,,,/MapSuiteUSDemographicMap;component/", element.Attribute("icon").Value), UriKind.RelativeOrAbsolute));

                foreach (var item in element.Elements("item"))
                {
                    ColumnViewModel categoryItem = new ColumnViewModel();
                    categoryItem.Parent = category;
                    categoryItem.ColumnName = item.Element("columnName").Value;
                    categoryItem.Alias = item.Element("alias").Value;
                    categoryItem.LegendTitle = item.Element("legendTitle").Value;
                    category.Columns.Add(categoryItem);
                }
                category.CanUsePieView = category.Columns.Count >= 2;
                CategoryList.Add(category);
            }
        }

        public StackPanel GetToolTip(Feature feature)
        {
            StackPanel popupPanel = new StackPanel();
            popupPanel.Margin = new Thickness(5);

            if (mapModel.DefaultFeatureLayer == mapModel.CensusStateFeatureLayer)
            {
                TextBlock stateName = new TextBlock();
                stateName.FontWeight = FontWeights.Bold;
                stateName.FontFamily = new FontFamily("Segoe UI");
                stateName.Text = feature.ColumnValues["NAME"];
                stateName.FontSize = 14;
                stateName.Margin = new Thickness(0, 0, 0, 10);

                popupPanel.Children.Add(stateName);
                popupPanel.Children.Add(new Separator() { Margin = new Thickness(0, 0, 0, 10) });
            }

            foreach (var column in CurrentStyleBuilder.SelectedColumns)
            {
                if (feature.ColumnValues.ContainsKey(column))
                {
                    TextBlock columnText = new TextBlock();
                    columnText.FontFamily = new FontFamily("Segoe UI");
                    columnText.Text = TextFormatter.GetFormatedString(column, double.Parse(feature.ColumnValues[column]));
                    columnText.FontSize = 12;
                    columnText.Margin = new Thickness(0, 0, 0, 3);

                    popupPanel.Children.Add(columnText);
                }
            }
            return popupPanel;
        }

        private void UpdateLegend(DemographicStyleBuilder styleBuilder, LegendAdornmentLayer legendAdornmentLayer)
        {
            legendAdornmentLayer.LegendItems.Clear();

            if (styleBuilder is ThematicDemographicStyleBuilder)
            {
                AddThematicLegendItems(legendAdornmentLayer);
            }
            else if (styleBuilder is DotDensityDemographicStyleBuilder)
            {
                AddDotDensityLegendItems(legendAdornmentLayer);
            }
            else if (styleBuilder is ValueCircleDemographicStyleBuilder)
            {
                AddValueCircleLegendItems(legendAdornmentLayer);
            }
            else if (styleBuilder is PieChartDemographicStyleBuilder)
            {
                AddPieGraphLegendItems(legendAdornmentLayer);
            }

            legendAdornmentLayer.ContentResizeMode = LegendContentResizeMode.Fixed;
            legendAdornmentLayer.Height = GetLegendHeight(legendAdornmentLayer);
            legendAdornmentLayer.Width = GetLegendWidth(legendAdornmentLayer);
        }

        private float GetLegendWidth(LegendAdornmentLayer legendAdornmentLayer)
        {
            PlatformGeoCanvas gdiPlusGeoCanvas = new PlatformGeoCanvas();
            LegendItem title = legendAdornmentLayer.Title;
            float width = gdiPlusGeoCanvas.MeasureText(title.TextStyle.TextColumnName, new GeoFont("Segoe UI", 12)).Width
               + title.ImageWidth + title.ImageRightPadding + title.ImageLeftPadding + title.TextRightPadding + title.TextLeftPadding + title.LeftPadding + title.RightPadding;

            foreach (LegendItem legendItem in legendAdornmentLayer.LegendItems)
            {
                float legendItemWidth = gdiPlusGeoCanvas.MeasureText(legendItem.TextStyle.TextColumnName, new GeoFont("Segoe UI", 10)).Width
                    + legendItem.ImageWidth + legendItem.ImageRightPadding + legendItem.ImageLeftPadding + legendItem.TextRightPadding + legendItem.TextLeftPadding + legendItem.LeftPadding + legendItem.RightPadding;
                if (width < legendItemWidth)
                {
                    width = legendItemWidth;
                }
            }
            return width;
        }

        private float GetLegendHeight(LegendAdornmentLayer legendAdornmentLayer)
        {
            PlatformGeoCanvas gdiPlusGeoCanvas = new PlatformGeoCanvas();
            LegendItem title = legendAdornmentLayer.Title;
            float titleMeasuredHeight = gdiPlusGeoCanvas.MeasureText(title.TextStyle.TextColumnName, new GeoFont("Segoe UI", 12)).Height;
            float legendHeight = new[] { titleMeasuredHeight, title.ImageHeight, title.Height }.Max();
            float height = legendHeight + Math.Max(title.ImageTopPadding, title.TextTopPadding) + title.TopPadding + Math.Max(title.ImageBottomPadding, title.TextBottomPadding) + title.BottomPadding;

            foreach (LegendItem legendItem in legendAdornmentLayer.LegendItems)
            {
                float itemLegendHeight = Math.Max(gdiPlusGeoCanvas.MeasureText(legendItem.TextStyle.TextColumnName, new GeoFont("Segoe UI", 10)).Height, legendItem.ImageHeight);
                float itemHeight = itemLegendHeight + Math.Max(legendItem.ImageTopPadding, legendItem.TextTopPadding) + legendItem.TopPadding + Math.Max(legendItem.ImageBottomPadding, legendItem.TextBottomPadding) + legendItem.BottomPadding;

                height += itemHeight;
            }
            return height;
        }

        private void AddPieGraphLegendItems(LegendAdornmentLayer legendAdornmentLayer)
        {
            PieZedGraphStyle zedGraphStyle = (PieZedGraphStyle)CurrentStyleBuilder.GetStyles(mapModel.DefaultFeatureLayer.FeatureSource)[0];

            foreach (KeyValuePair<string, GeoColor> item in zedGraphStyle.PieSlices)
            {
                LegendItem legendItem = new LegendItem();
                legendItem.ImageWidth = 20;
                legendItem.TextRightPadding = 5;
                legendItem.RightPadding = 5;
                legendItem.ImageStyle = new AreaStyle(new GeoSolidBrush(item.Value));
                legendItem.TextStyle = new TextStyle(item.Key, new GeoFont("Segoe UI", 10), new GeoSolidBrush(GeoColor.SimpleColors.Black));
                legendAdornmentLayer.LegendItems.Add(legendItem);
            }
        }

        private void AddValueCircleLegendItems(LegendAdornmentLayer legendAdornmentLayer)
        {
            ValueCircleStyle valueCircleStyle = (ValueCircleStyle)CurrentStyleBuilder.GetStyles(mapModel.DefaultFeatureLayer.FeatureSource)[0];

            // Here we generate 4 legend items, with the area of 160, 320, 640 and 1280 square pixels seperately.
            int[] circleAreas = new int[] { 160, 320, 640, 1280 };
            foreach (int circleArea in circleAreas)
            {
                LegendItem legendItem = new LegendItem();
                double radius = Math.Sqrt(circleArea / Math.PI);
                legendItem.ImageStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(valueCircleStyle.InnerColor), new GeoPen(new GeoSolidBrush(valueCircleStyle.OuterColor)), (int)(radius * 2));
                AreaStyle maskStyle = new AreaStyle(new GeoPen(GeoColor.StandardColors.LightGray, 1), new GeoSolidBrush(GeoColor.SimpleColors.Transparent));
                legendItem.ImageMask = maskStyle;
                legendItem.ImageWidth = 48;
                legendItem.TextTopPadding = 16;
                legendItem.TextRightPadding = 5;
                legendItem.BottomPadding = 16;
                legendItem.TopPadding = 16;
                legendItem.RightPadding = 5;

                double drawingRadius = circleArea / valueCircleStyle.DrawingRadiusRatio * valueCircleStyle.BasedScale / valueCircleStyle.DefaultZoomLevel.Scale;
                double ratio = (valueCircleStyle.MaxValidValue - valueCircleStyle.MinValidValue) / (valueCircleStyle.MaxCircleAreaInDefaultZoomLevel - valueCircleStyle.MinCircleAreaInDefaultZoomLevel);
                double resultValue = (drawingRadius - valueCircleStyle.MinCircleAreaInDefaultZoomLevel) * ratio + valueCircleStyle.MinValidValue;

                string text = TextFormatter.GetFormatedStringForLegendItem(valueCircleStyle.ColumnName, resultValue);
                legendItem.TextStyle = new TextStyle(text, new GeoFont("Segoe UI", 10), new GeoSolidBrush(GeoColor.SimpleColors.Black));

                legendAdornmentLayer.LegendItems.Add(legendItem);
            }
        }

        private void AddDotDensityLegendItems(LegendAdornmentLayer legendAdornmentLayer)
        {
            CustomDotDensityStyle dotDensityStyle = (CustomDotDensityStyle)CurrentStyleBuilder.GetStyles(mapModel.DefaultFeatureLayer.FeatureSource)[0];

            // Here we generate 4 legend items, for 5, 10, 20 and 50 points seperately.
            int[] pointCounts = new int[] { 5, 10, 20, 50 };
            foreach (int pointCount in pointCounts)
            {
                LegendItem legendItem = new LegendItem();
                legendItem.ImageMask = new AreaStyle(new GeoPen(GeoColor.StandardColors.LightGray, 1), new GeoSolidBrush(GeoColor.SimpleColors.Transparent));
                legendItem.ImageWidth = 48;
                legendItem.TextTopPadding = 16;
                legendItem.TextRightPadding = 5;
                legendItem.BottomPadding = 16;
                legendItem.TopPadding = 16;
                legendItem.RightPadding = 5;
                CustomDotDensityStyle legendDotDensityStyle = (CustomDotDensityStyle)dotDensityStyle.CloneDeep();
                legendDotDensityStyle.DrawingPointsNumber = pointCount;
                legendItem.ImageStyle = legendDotDensityStyle;

                string text = string.Format(CultureInfo.InvariantCulture, "{0:0.####}", TextFormatter.GetFormatedStringForLegendItem(dotDensityStyle.ColumnName, (pointCount / dotDensityStyle.PointToValueRatio)));
                legendItem.TextStyle = new TextStyle(text, new GeoFont("Segoe UI", 10), new GeoSolidBrush(GeoColor.SimpleColors.Black));

                legendAdornmentLayer.LegendItems.Add(legendItem);
            }
        }

        private void AddThematicLegendItems(LegendAdornmentLayer legendAdornmentLayer)
        {
            ClassBreakStyle thematicStyle = (ClassBreakStyle)CurrentStyleBuilder.GetStyles(mapModel.DefaultFeatureLayer.FeatureSource)[0];

            for (int i = 0; i < thematicStyle.ClassBreaks.Count; i++)
            {
                LegendItem legendItem = new LegendItem();

                if (i < thematicStyle.ClassBreaks.Count)
                {
                    legendItem.ImageStyle = thematicStyle.ClassBreaks[i].DefaultAreaStyle;
                    legendItem.ImageWidth = 20;
                    legendItem.TextRightPadding = 5;
                    legendItem.RightPadding = 5;

                    string text = string.Empty;
                    if (i != thematicStyle.ClassBreaks.Count - 1)
                    {
                        text = string.Format("{0:#,0.####} ~ {1:#,0.####}",
                            TextFormatter.GetFormatedStringForLegendItem(thematicStyle.ColumnName, thematicStyle.ClassBreaks[i].Value),
                            TextFormatter.GetFormatedStringForLegendItem(thematicStyle.ColumnName, thematicStyle.ClassBreaks[i + 1].Value));
                    }
                    else
                    {
                        text = string.Format("> {0:#,0.####}",
                            TextFormatter.GetFormatedStringForLegendItem(thematicStyle.ColumnName, thematicStyle.ClassBreaks[i].Value));
                    }
                    legendItem.TextStyle = new TextStyle(text, new GeoFont("Segoe UI", 10), new GeoSolidBrush(GeoColor.SimpleColors.Black));
                }
                legendAdornmentLayer.LegendItems.Add(legendItem);
            }
        }
    }
}