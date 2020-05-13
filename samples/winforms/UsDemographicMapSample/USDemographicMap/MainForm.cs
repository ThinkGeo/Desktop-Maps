/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public partial class MainForm : Form
    {
        private bool avoidUpdatingMap;
        private DemographicStyleBuilder currentStyleBuilder;
        private Feature PreviousHighlightFeature;
        private ShapeFileFeatureLayer censusStateFeatureLayer;
        private ShapeFileFeatureLayer customFeatureLayer;
        private ShapeFileFeatureLayer currentFeatureLayer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            map.CurrentExtent = new RectangleShape(-13059527, 6484023, -8632838, 2982361);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            map.Overlays.Add(thinkGeoCloudMapsOverlay);

            // Add Demographic Layer
            customFeatureLayer = new ShapeFileFeatureLayer();
            censusStateFeatureLayer = new ShapeFileFeatureLayer(@"../../App_Data/usStatesCensus2010.shp");
            censusStateFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            currentFeatureLayer = censusStateFeatureLayer;

            LayerOverlay demographicLayerOverlay = new LayerOverlay();
            demographicLayerOverlay.Layers.Add(currentFeatureLayer);
            map.Overlays.Add("DemographicLayerOverlayKey", demographicLayerOverlay);

            // Add Highlight Overlay
            HighlightOverlay USDemographicOverlay = new HighlightOverlay();
            map.Overlays.Add("HighlightOverlayKey", USDemographicOverlay);

            // Initialize style builder to Thematic
            currentStyleBuilder = new ThematicDemographicStyleBuilder();
            currentStyleBuilder.SelectedColumns.Add("Population");

            // Initialize the Legend
            LegendAdornmentLayer legendAdornmentLayer = new LegendAdornmentLayer();
            legendAdornmentLayer.Location = AdornmentLocation.LowerLeft;
            legendAdornmentLayer.Title = new LegendItem();
            legendAdornmentLayer.Title.ImageJustificationMode = LegendImageJustificationMode.JustifyImageRight;
            legendAdornmentLayer.Title.TopPadding = 10;
            legendAdornmentLayer.Title.BottomPadding = 10;
            legendAdornmentLayer.Title.TextStyle = new TextStyle("Population", new GeoFont("Segoe UI", 12), new GeoSolidBrush(GeoColor.SimpleColors.Black));
            map.AdornmentOverlay.Layers.Add(legendAdornmentLayer);
            //map.ZoomToScale(map.ZoomLevelSet.ZoomLevel14.Scale);
            // Update the controls and map.
            UpdateUIControls(currentStyleBuilder);
            UpdateMap(currentStyleBuilder);

            LoadDataSelectorUserControls();
        }

        // This method would be called whenever the style needs to updated
        private void dataSelectorUserControl_StyleUpdated(object sender, StyleUpdatedDataSelectorUserControlEventArgs e)
        {
            HideStyleControls();
            DataSelectorUserControl dataSelectorUserControl = sender as DataSelectorUserControl;

            LegendAdornmentLayer legendAdornmentLayer = (LegendAdornmentLayer)map.AdornmentOverlay.Layers[0];
            // Here we update the styleBuilder and UI according to the style builder type passed from the data selector user control.
            switch (e.DemographicStyleBuilderType)
            {
                case DemographicStyleBuilderType.ValueCircle:
                    currentStyleBuilder = new ValueCircleDemographicStyleBuilder();
                    trackBarValueCirleRadio.Visible = true;
                    lblValueCircle.Visible = true;
                    legendAdornmentLayer.Title.TextStyle.TextColumnName = e.ActivatedStyleSelectors[0].LegendTitle;
                    break;

                case DemographicStyleBuilderType.DotDensity:
                    currentStyleBuilder = new DotDensityDemographicStyleBuilder();
                    lblFewer.Visible = true;
                    lblMore.Visible = true;
                    trackBarDots.Visible = true;
                    lblDotDensityUnit.Visible = true;
                    legendAdornmentLayer.Title.TextStyle.TextColumnName = e.ActivatedStyleSelectors[0].LegendTitle;
                    break;

                case DemographicStyleBuilderType.PieChart:
                    currentStyleBuilder = new PieChartDemographicStyleBuilder();
                    foreach (StyleSelectorUserControl item in e.ActivatedStyleSelectors)
                    {
                        ((PieChartDemographicStyleBuilder)currentStyleBuilder).SelectedColumnAliases.Add(item.Alias);
                    }
                    legendAdornmentLayer.Title.TextStyle.TextColumnName = dataSelectorUserControl.Title;
                    break;

                case DemographicStyleBuilderType.Thematic:
                default:
                    currentStyleBuilder = new ThematicDemographicStyleBuilder();
                    lblDisplayEndColor.Visible = true;
                    lblColorWheelDirection.Visible = true;
                    cbxActiveEndColor.Visible = true;
                    cbxSelectColorWheelDirection.Visible = true;
                    legendAdornmentLayer.Title.TextStyle.TextColumnName = e.ActivatedStyleSelectors[0].LegendTitle;
                    lblDisplayColor.Text = "Display Start Color";
                    break;
            }

            foreach (StyleSelectorUserControl activatedStyleSelector in e.ActivatedStyleSelectors)
            {
                currentStyleBuilder.SelectedColumns.Add(activatedStyleSelector.ColumnName);
            }

            // Update UI and map.
            UpdateUIControls(currentStyleBuilder);
            UpdateMap(currentStyleBuilder);
        }

        private void ActiveColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentStyleBuilder.Color = new GeoColor(cbxActiveColor.SelectedColor.R, cbxActiveColor.SelectedColor.G, cbxActiveColor.SelectedColor.B);
            UpdateMap(currentStyleBuilder);
        }

        private void ActiveEndColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThematicDemographicStyleBuilder thematicDemographicStyle = (ThematicDemographicStyleBuilder)currentStyleBuilder;
            thematicDemographicStyle.EndColor = new GeoColor(cbxActiveEndColor.SelectedColor.R, cbxActiveEndColor.SelectedColor.G, cbxActiveEndColor.SelectedColor.B);
            UpdateMap(thematicDemographicStyle);
        }

        private void SelectColorWheelDirectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorWheelDirection direction = ColorWheelDirection.CounterClockwise;
            if (cbxSelectColorWheelDirection.SelectedIndex == 0) direction = ColorWheelDirection.Clockwise;

            ThematicDemographicStyleBuilder thematicDemographicStyle = (ThematicDemographicStyleBuilder)currentStyleBuilder;
            thematicDemographicStyle.ColorWheelDirection = direction;
            UpdateMap(thematicDemographicStyle);
        }

        private void ValueCirleRadioTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            ValueCircleDemographicStyleBuilder valueCircleDemographicStyle = (ValueCircleDemographicStyleBuilder)currentStyleBuilder;
            valueCircleDemographicStyle.RadiusRatio = trackBarValueCirleRadio.Value / 3d;
            UpdateMap(valueCircleDemographicStyle);
        }

        private void DotsTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            DotDensityDemographicStyleBuilder dotDensityDemographicStyle = (DotDensityDemographicStyleBuilder)currentStyleBuilder;
            dotDensityDemographicStyle.DotDensityValue = 50 * (trackBarDots.Value / 3d);
            UpdateMap(dotDensityDemographicStyle);
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            PointShape mouseLocation = ExtentHelper.ToWorldCoordinate(map.CurrentExtent, new ScreenPointF(e.X, e.Y), map.Width, map.Height);
            lblLocationX.Text = string.Format(CultureInfo.InvariantCulture, "X:{0:N6}", mouseLocation.X);
            lblLocationY.Text = string.Format(CultureInfo.InvariantCulture, "Y:{0:N6}", mouseLocation.Y);
            stpFooter.Refresh();

            if (!currentFeatureLayer.IsOpen)
            {
                return;
            }
            HighlightOverlay USDemographicOverlay = (HighlightOverlay)map.Overlays["HighlightOverlayKey"];
            USDemographicOverlay.UpdateHighlightFeature(currentFeatureLayer, mouseLocation);

            // Here we get tootip for the highlighted feature.
            bool mapNeedsUpdate = USDemographicOverlay.HighlightFeature != null &&
                (PreviousHighlightFeature == null || (PreviousHighlightFeature != null && PreviousHighlightFeature.Id != USDemographicOverlay.HighlightFeature.Id));
            if (mapNeedsUpdate)
            {
                toolTip1.Hide(map);
                toolTip1.Dispose();

                string resultText = string.Empty;
                foreach (string item in currentStyleBuilder.SelectedColumns)
                {
                    string columnName = TextFormatter.GetFormatedString(item, double.Parse(USDemographicOverlay.HighlightFeature.ColumnValues[item]));
                    resultText = string.Format("{0}{1}\n", resultText, columnName);
                }

                toolTip1 = new ToolTip();
                toolTip1.InitialDelay = 1000;
                toolTip1.SetToolTip(map, resultText);
                map.Refresh(USDemographicOverlay);
            }
            else if (USDemographicOverlay.HighlightFeature == null)
            {
                toolTip1.Hide(map);
                toolTip1.Dispose();
            }

            PreviousHighlightFeature = USDemographicOverlay.HighlightFeature;
        }

        private void CategoryBorder_Resize(object sender, EventArgs e)
        {
            pnlStyleControls.Top = pnlControl.Top + pnlControl.Height + 5;
        }

        private void UpdateUIControls(DemographicStyleBuilder activeStyleBuilder)
        {
            avoidUpdatingMap = true;
            foreach (var item in cbxActiveColor.SimpleColors)
            {
                GeoColor styleColor = activeStyleBuilder.Color;
                if (item.Value.R == styleColor.RedComponent
                    && item.Value.G == styleColor.GreenComponent
                    && item.Value.B == styleColor.BlueComponent)
                {
                    cbxActiveColor.SetSelectedColor(item.Key);
                    break;
                }
            }

            ThematicDemographicStyleBuilder activeThematicSetting = activeStyleBuilder as ThematicDemographicStyleBuilder;
            if (activeThematicSetting != null)
            {
                foreach (var item in cbxActiveEndColor.SimpleColors)
                {
                    GeoColor styleColor = activeThematicSetting.EndColor;
                    if (item.Value.R == styleColor.RedComponent
                        && item.Value.G == styleColor.GreenComponent
                        && item.Value.B == styleColor.BlueComponent)
                    {
                        cbxActiveEndColor.SetSelectedColor(item.Key);
                        break;
                    }
                }
                cbxSelectColorWheelDirection.SelectedIndex = (int)activeThematicSetting.ColorWheelDirection;
            }
            avoidUpdatingMap = false;
        }

        private void UpdateMap(DemographicStyleBuilder styleBuilder)
        {
            if (!avoidUpdatingMap)
            {
                Collection<Style> activeStyles = styleBuilder.GetStyles(currentFeatureLayer.FeatureSource);
                currentFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                foreach (Style activeStyle in activeStyles)
                {
                    currentFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(activeStyle);
                }

                LegendAdornmentLayer legendAdornmentLayer = (LegendAdornmentLayer)map.AdornmentOverlay.Layers[0];
                UpdateLegend(currentStyleBuilder, legendAdornmentLayer);
                map.Refresh();
            }
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

        private void AddPieGraphLegendItems(LegendAdornmentLayer legendAdornmentLayer)
        {
            PieZedGraphStyle zedGraphStyle = (PieZedGraphStyle)currentStyleBuilder.GetStyles(currentFeatureLayer.FeatureSource)[0];

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
            ValueCircleStyle valueCircleStyle = (ValueCircleStyle)currentStyleBuilder.GetStyles(currentFeatureLayer.FeatureSource)[0];

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
            CustomDotDensityStyle dotDensityStyle = (CustomDotDensityStyle)currentStyleBuilder.GetStyles(currentFeatureLayer.FeatureSource)[0];

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
            ClassBreakStyle thematicStyle = (ClassBreakStyle)currentStyleBuilder.GetStyles(currentFeatureLayer.FeatureSource)[0];

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

        private void HideStyleControls()
        {
            lblFewer.Visible = false;
            lblMore.Visible = false;
            trackBarDots.Visible = false;
            lblDotDensityUnit.Visible = false;

            trackBarValueCirleRadio.Visible = false;
            lblValueCircle.Visible = false;

            lblDisplayEndColor.Visible = false;
            lblColorWheelDirection.Visible = false;

            cbxActiveEndColor.Visible = false;
            cbxSelectColorWheelDirection.Visible = false;

            lblDisplayColor.Text = "Display Color:";
        }

        // Here we load all the nodes on the left from xml file.
        private void LoadDataSelectorUserControls()
        {
            XDocument xDocument = XDocument.Load(@"../../App_Data/CategoryList.xml");
            IEnumerable<XElement> elements = from category in xDocument.Element("DemographicMap").Elements("Category")
                                             select category;

            Collection<DataSelectorUserControl> userControls = new Collection<DataSelectorUserControl>();
            foreach (var element in elements)
            {
                DataSelectorUserControl dataSelector = null;
                if (element.Attribute("name").Value != "Custom Data")
                {
                    dataSelector = new DataSelectorUserControl();
                }
                else
                {
                    dataSelector = new CustomDataSelectorUserControl();
                    ((CustomDataSelectorUserControl)dataSelector).DataSelected += MainForm_CustomDataSelected;
                }

                dataSelector.Title = element.Attribute("name").Value;
                dataSelector.Image = new Bitmap(string.Format("{0}{1}", "../../", element.Attribute("icon").Value));

                foreach (var item in element.Elements("item"))
                {
                    StyleSelectorUserControl styleSelector = new StyleSelectorUserControl();
                    styleSelector.ColumnName = item.Element("columnName").Value;
                    styleSelector.Alias = item.Element("alias").Value;
                    styleSelector.LegendTitle = item.Element("legendTitle").Value;
                    dataSelector.AddStyleSelector(styleSelector);
                }

                if (dataSelector.GetStyleSelectorCount() >= 2)
                {
                    dataSelector.PieChartEnabled = true;
                }

                dataSelector.StyleUpdated += dataSelectorUserControl_StyleUpdated;
                dataSelector.Click += dataSelector_Click;
                flowLayoutPanel1.Controls.Add(dataSelector);
            }
            ((DataSelectorUserControl)(flowLayoutPanel1.Controls[0])).SetActiveStatus(true);
        }

        private void MainForm_CustomDataSelected(object sender, DataSelectedCustomDataSelectorUserControlEventArgs e)
        {
            customFeatureLayer = e.ShapeFileFeatureLayer;
            customFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            currentFeatureLayer = customFeatureLayer;
            currentStyleBuilder.SelectedColumns.Clear();
            map.CurrentExtent = e.ShapeFileFeatureLayer.GetBoundingBox();

            ((LayerOverlay)map.Overlays["DemographicLayerOverlayKey"]).Layers.Clear();
            ((LayerOverlay)map.Overlays["DemographicLayerOverlayKey"]).Layers.Add(currentFeatureLayer);
        }

        private void dataSelector_Click(object sender, EventArgs e)
        {
            DataSelectorUserControl clickedDataSelector = (DataSelectorUserControl)sender;
            foreach (DataSelectorUserControl control in flowLayoutPanel1.Controls.OfType<DataSelectorUserControl>())
            {
                if (clickedDataSelector != control && control.IsActive)
                {
                    control.SetActiveStatus(false);
                }
            }
            clickedDataSelector.SetActiveStatus(!clickedDataSelector.IsActive);
            if (!(clickedDataSelector is CustomDataSelectorUserControl))
            {
                currentFeatureLayer = censusStateFeatureLayer;
            }
            else
            {
                currentFeatureLayer = customFeatureLayer;
            }

            ((LayerOverlay)map.Overlays["DemographicLayerOverlayKey"]).Layers.Clear();
            ((LayerOverlay)map.Overlays["DemographicLayerOverlayKey"]).Layers.Add(currentFeatureLayer);
        }

        private void LeftSideBarPanel_PanelCollapseButtonClick(object sender, EventArgs e)
        {
            map.Width = Width - LeftSideBarPanel.Width;
            map.Left = LeftSideBarPanel.Width;
        }
    }
}