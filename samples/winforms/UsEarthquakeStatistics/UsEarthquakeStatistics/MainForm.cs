/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public partial class MainForm : Form
    {
        private static readonly RectangleShape defaultExtent = new RectangleShape(-19062735.6816748, 9273256.52450252, -5746827.16371793, 2673516.56066139);
        private OverlaySwitcher switcher;
        private FeatureLayer activeStyleLayer;
        private InMemoryFeatureLayer markerMemoryLayer;
        private Collection<EarthquakeItem> earthquakeItems;
        private InMemoryFeatureLayer markerMemoryHighlightLayer;

        public MainForm()
        {
            InitializeComponent();
            InitializeToolTips();
            InitializeBackground();

            earthquakeItems = new Collection<EarthquakeItem>();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeMap();
            InitializeOverlaySwitcher();

            PanRadioButton.Checked = true;
            HeatMapRadioButton.Checked = true;
        }

        private void InitializeMap()
        {
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            map.CurrentExtent = defaultExtent;

            ScaleBarAdornmentLayer scaleBarAdormentLayer = new ScaleBarAdornmentLayer();
            scaleBarAdormentLayer.UnitFamily = UnitSystem.Imperial;
            map.AdornmentOverlay.Layers.Add(scaleBarAdormentLayer);

            RadiusMearsureTrackInteractiveOverlay trackOverlay = new RadiusMearsureTrackInteractiveOverlay(DistanceUnit.Mile, map.MapUnit);
            trackOverlay.TrackEnded += TrackOverlay_TrackEnded;
            map.TrackOverlay = trackOverlay;

            InitializeOverlays();

            map.Refresh();
        }

        private void InitializeOverlays()
        {
            string cacheFolder = Path.Combine(Path.GetTempPath(), "TileCache");

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlayLight = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            thinkGeoCloudMapsOverlayLight.Name = Resources.ThinkGeoCloudMapsOverlayLight;
            thinkGeoCloudMapsOverlayLight.IsVisible = true;
            thinkGeoCloudMapsOverlayLight.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsOverlayLight);
            thinkGeoCloudMapsOverlayLight.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            map.Overlays.Add(Resources.ThinkGeoCloudMapsOverlayLight, thinkGeoCloudMapsOverlayLight);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlayAerial = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            thinkGeoCloudMapsOverlayAerial.Name = Resources.ThinkGeoCloudMapsOverlayAerial;
            thinkGeoCloudMapsOverlayAerial.IsVisible = false;
            thinkGeoCloudMapsOverlayAerial.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsOverlayAerial);
            thinkGeoCloudMapsOverlayAerial.MapType = ThinkGeoCloudRasterMapsMapType.Aerial;
            map.Overlays.Add(Resources.ThinkGeoCloudMapsOverlayAerial, thinkGeoCloudMapsOverlayAerial);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlayHybrid = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            thinkGeoCloudMapsOverlayHybrid.Name = Resources.ThinkGeoCloudMapsOverlayHybrid;
            thinkGeoCloudMapsOverlayHybrid.IsVisible = false;
            thinkGeoCloudMapsOverlayHybrid.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsOverlayHybrid);
            thinkGeoCloudMapsOverlayHybrid.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid;
            map.Overlays.Add(Resources.ThinkGeoCloudMapsOverlayHybrid, thinkGeoCloudMapsOverlayHybrid);

            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            openStreetMapOverlay.Name = Resources.OSMOverlayName;
            openStreetMapOverlay.IsVisible = false;
            openStreetMapOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.OpenStreetMapKey);
            map.Overlays.Add(Resources.OpenStreetMapKey, openStreetMapOverlay);

            BingMapsOverlay bingMapsAerialOverlay = new BingMapsOverlay();
            bingMapsAerialOverlay.Name = Resources.BingMapsAerialOverlayName;
            bingMapsAerialOverlay.MapType = WinForms.BingMapsMapType.Aerial;
            bingMapsAerialOverlay.IsVisible = false;
            bingMapsAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsAerial);
            map.Overlays.Add(Resources.BingMapsAerial, bingMapsAerialOverlay);

            BingMapsOverlay bingMapsRoadOverlay = new BingMapsOverlay();
            bingMapsRoadOverlay.Name = Resources.BingMapsRoadOverlayName;
            bingMapsRoadOverlay.MapType = WinForms.BingMapsMapType.Road;
            bingMapsRoadOverlay.IsVisible = false;
            bingMapsRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsRoad);
            map.Overlays.Add(Resources.BingMapsRoad, bingMapsRoadOverlay);

            LayerOverlay styleLayersOverLay = new LayerOverlay();
            map.Overlays.Add(Resources.StyleLayerOverLayKey, styleLayersOverLay);

            Proj4Projection wgs84ToMercatorProjection = new Proj4Projection();
            wgs84ToMercatorProjection.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            wgs84ToMercatorProjection.ExternalProjectionParametersString = Proj4Projection.GetSphericalMercatorParametersString();

            EarthquakeHeatFeatureLayer heatLayer = new EarthquakeHeatFeatureLayer(new ShapeFileFeatureSource(ConfigurationManager.AppSettings["DataShapefileFileName"]));
            heatLayer.IsVisible = false;
            heatLayer.HeatStyle = new HeatStyle(10, 100, Resources.MagnitudeColumnName, 0, 12, 100, DistanceUnit.Kilometer);
            heatLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            heatLayer.Name = Resources.HeatStyleLayerName;
            styleLayersOverLay.Layers.Add(heatLayer);

            ShapeFileFeatureLayer pointLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["DataShapefileFileName"]);
            pointLayer.IsVisible = false;
            pointLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            pointLayer.Name = Resources.PointStyleLayerName;
            pointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.Red, 6, GeoColor.StandardColors.White, 1);
            pointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            styleLayersOverLay.Layers.Add(pointLayer);

            FeatureSource isoLineFeatureSource = new ShapeFileFeatureSource(ConfigurationManager.AppSettings["DataShapefileFileName"]);
            EarthquakeIsoLineFeatureLayer isoLineLayer = new EarthquakeIsoLineFeatureLayer(isoLineFeatureSource);
            isoLineLayer.IsVisible = false;
            isoLineLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            isoLineLayer.Name = Resources.IsolineStyleLayerName;
            styleLayersOverLay.Layers.Add(isoLineLayer);

            LayerOverlay queryResultMarkerOverlay = new LayerOverlay();
            map.Overlays.Add("QueryResultMarkerOverlay", queryResultMarkerOverlay);

            PointStyle highlightStyle = new PointStyle();
            highlightStyle.CustomPointStyles.Add(PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(50, GeoColor.SimpleColors.Blue), 20, GeoColor.SimpleColors.LightBlue, 1));
            highlightStyle.CustomPointStyles.Add(PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.LightBlue, 9, GeoColor.SimpleColors.Blue, 1));

            markerMemoryLayer = new InMemoryFeatureLayer();
            markerMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.Orange, 8, GeoColor.SimpleColors.White, 1);
            markerMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            queryResultMarkerOverlay.Layers.Add(markerMemoryLayer);

            markerMemoryHighlightLayer = new InMemoryFeatureLayer();
            markerMemoryHighlightLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = highlightStyle;
            markerMemoryHighlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            queryResultMarkerOverlay.Layers.Add(markerMemoryHighlightLayer);
        }

        private void InitializeOverlaySwitcher()
        {
            Collection<Overlay> baseOverlays = new Collection<Overlay>();
            baseOverlays.Add(map.Overlays[Resources.ThinkGeoCloudMapsOverlayLight]);
            baseOverlays.Add(map.Overlays[Resources.ThinkGeoCloudMapsOverlayAerial]);
            baseOverlays.Add(map.Overlays[Resources.ThinkGeoCloudMapsOverlayHybrid]);

            baseOverlays.Add(map.Overlays[Resources.OpenStreetMapKey]);
            baseOverlays.Add(map.Overlays[Resources.BingMapsAerial]);
            baseOverlays.Add(map.Overlays[Resources.BingMapsRoad]);

            switcher = new OverlaySwitcher(baseOverlays, map);
            switcher.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            switcher.Location = new Point(Width - 243, 65);
            switcher.Size = new Size(220, 125);
            Controls.Add(switcher);
            switcher.BringToFront();
            switcher.Refresh();
            switcher.OverlayChanged += OverlaySwitcher_OverlayChanged;

            PictureBox overlayPictureBox = new PictureBox();
            overlayPictureBox.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            overlayPictureBox.Image = Resources.switcher_minimize;
            overlayPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            overlayPictureBox.Location = new Point(switcher.Location.X + switcher.Width - overlayPictureBox.Width - 8, switcher.Location.Y + 5);
            overlayPictureBox.Click += OverlaySwitcherPictureBox_Click;
            Controls.Add(overlayPictureBox);
            overlayPictureBox.BringToFront();
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            Feature queryFeature = UnionTrackedShapes();
            if (queryFeature == null) return;

            activeStyleLayer.FeatureSource.Open();
            Collection<Feature> features = activeStyleLayer.FeatureSource.GetFeaturesWithinDistanceOf(queryFeature, map.MapUnit, DistanceUnit.Meter, 0.0001, ReturningColumnsType.AllColumns);

            earthquakeItems.Clear();
            foreach (Feature feature in features)
            {
                earthquakeItems.Add(new EarthquakeItem(feature));
            }

            FilterEarthquake();
        }

        private Feature UnionTrackedShapes()
        {
            try
            {
                Feature[] allPolygonFeatures = map.TrackOverlay.TrackShapeLayer.InternalFeatures.Where(f => f.GetShape() is PolygonShape).ToArray();
                return SqlTypesGeometryHelper.Union(allPolygonFeatures);
            }
            catch
            {
                return null;
            }
        }

        private void FilterEarthquake()
        {
            QueryResultItemsDataGridView.Rows.Clear();

            Proj4Projection mercatorToWgs84Projection = new Proj4Projection();
            mercatorToWgs84Projection.InternalProjectionParametersString = Proj4Projection.GetSphericalMercatorParametersString();
            mercatorToWgs84Projection.ExternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            mercatorToWgs84Projection.Open();

            Collection<EarthquakeItem> items = new Collection<EarthquakeItem>();
            for (int i = earthquakeItems.Count - 1; i >= 0; i--)
            {
                EarthquakeItem resultItem = earthquakeItems[i];

                double latitude, longitude;
                if (double.TryParse(resultItem.LatitudeCell.Value.ToString(), out latitude) && double.TryParse(resultItem.LongitudeCell.Value.ToString(), out longitude))
                {
                    PointShape point = new PointShape(longitude, latitude);
                    point = (PointShape)mercatorToWgs84Projection.ConvertToExternalProjection(point);

                    EarthquakeItem newResultItem = new EarthquakeItem(resultItem.EpicenterFeature);
                    newResultItem.LatitudeCell = new DataGridViewTextBoxCell { Value = point.Y.ToString("f3") };
                    newResultItem.LongitudeCell = new DataGridViewTextBoxCell { Value = point.X.ToString("f3") };

                    double year, depth, magnitude;
                    double.TryParse(newResultItem.MagnitudeCell.Value.ToString(), out magnitude);
                    double.TryParse(newResultItem.DepthInKilometerCell.Value.ToString(), out depth);
                    double.TryParse(newResultItem.YearCell.Value.ToString(), out year);

                    if ((magnitude >= MagnitudeSelectionRangeSlider.ValueLeft && magnitude <= MagnitudeSelectionRangeSlider.ValueRight || newResultItem.MagnitudeCell.Value.ToString() == Resources.UnknownString)
                        && (depth <= DepthSelectionRangeSlider.ValueRight && depth >= DepthSelectionRangeSlider.ValueLeft || newResultItem.DepthInKilometerCell.Value.ToString() == Resources.UnknownString)
                        && (year >= DateSelectionRangeSlider.ValueLeft && year <= DateSelectionRangeSlider.ValueRight) || newResultItem.YearCell.Value.ToString() == Resources.UnknownString)
                    {
                        DataGridViewRow newRow = new DataGridViewRow();
                        newRow.Cells.Add(newResultItem.ImageButtonCell);
                        newRow.Cells.Add(newResultItem.YearCell);
                        newRow.Cells.Add(newResultItem.LongitudeCell);
                        newRow.Cells.Add(newResultItem.LatitudeCell);
                        newRow.Cells.Add(newResultItem.DepthInKilometerCell);
                        newRow.Cells.Add(newResultItem.MagnitudeCell);
                        newRow.Cells.Add(newResultItem.LocationPointCell);
                        QueryResultItemsDataGridView.Rows.Add(newRow);

                        items.Add(newResultItem);
                    }
                }
            }

            mercatorToWgs84Projection.Close();
            RefreshMarkersByFeatures(items.Select(f => f.EpicenterFeature));
        }

        private void MapType_CheckedChanged(object sender, EventArgs e)
        {
            LayerOverlay styleLayerOverlay = (LayerOverlay)map.Overlays[Resources.StyleLayerOverLayKey];
            foreach (Layer layer in styleLayerOverlay.Layers)
            {
                layer.IsVisible = false;
            }

            RadioButton currentRadioButton = (RadioButton)sender;
            string layerName = currentRadioButton.Tag.ToString();
            FeatureLayer currentLayer = styleLayerOverlay.Layers.OfType<FeatureLayer>().FirstOrDefault(l => l.Name == layerName);
            if (currentLayer != null)
            {
                activeStyleLayer = currentLayer;
                activeStyleLayer.IsVisible = true;

                SetEarthquakeLegendsVisible(activeStyleLayer);

                map.Refresh(styleLayerOverlay);
                map.Refresh(map.AdornmentOverlay);
            }
        }

        private void SetEarthquakeLegendsVisible(Layer currentLayer)
        {
            EarthquakeIsoLineFeatureLayer earthquakeFeatureLayer = currentLayer as EarthquakeIsoLineFeatureLayer;
            LegendAdornmentLayer earthquakeLegendLayer = GetEarthquakeLegendLayer(earthquakeFeatureLayer);
            if (earthquakeLegendLayer != null)
            {
                earthquakeLegendLayer.IsVisible = earthquakeFeatureLayer != null;
            }
        }

        private LegendAdornmentLayer GetEarthquakeLegendLayer(EarthquakeIsoLineFeatureLayer earthquakeFeatureLayer)
        {
            LegendAdornmentLayer isoLevelLegendLayer = null;
            if (map.AdornmentOverlay.Layers.Contains(Resources.IsoLineLevelLegendLayerName))
            {
                isoLevelLegendLayer = (LegendAdornmentLayer)map.AdornmentOverlay.Layers[Resources.IsoLineLevelLegendLayerName];
            }
            else if (earthquakeFeatureLayer != null)
            {
                isoLevelLegendLayer = new LegendAdornmentLayer();
                isoLevelLegendLayer.Width = 85;
                isoLevelLegendLayer.Height = 320;
                isoLevelLegendLayer.Location = AdornmentLocation.LowerRight;
                isoLevelLegendLayer.ContentResizeMode = LegendContentResizeMode.Fixed;

                LegendItem flagLegendItem = new LegendItem();
                flagLegendItem.TextStyle = new TextStyle("Magnitude", new GeoFont("Arial", 10), new GeoSolidBrush(GeoColor.StandardColors.Black));
                flagLegendItem.TextLeftPadding = -20;
                isoLevelLegendLayer.LegendItems.Add(flagLegendItem);

                map.AdornmentOverlay.Layers.Add(Resources.IsoLineLevelLegendLayerName, isoLevelLegendLayer);

                for (int index = 0; index < earthquakeFeatureLayer.IsoLineLevels.Count; index++)
                {
                    LegendItem legendItem = new LegendItem();
                    legendItem.TextStyle = new TextStyle(earthquakeFeatureLayer.IsoLineLevels[index].ToString("f2"), new GeoFont("Arial", 10), new GeoSolidBrush(GeoColor.StandardColors.Black));
                    legendItem.ImageStyle = earthquakeFeatureLayer.LevelClassBreakStyle.ClassBreaks[index].DefaultAreaStyle;
                    legendItem.ImageWidth = 25;

                    isoLevelLegendLayer.LegendItems.Add(legendItem);
                }
            }

            return isoLevelLegendLayer;
        }

        private void MagnitudeSelectionRangeSlider_ValueChanged(object sender, EventArgs e)
        {
            MagnitudeLowerLabel.Text = MagnitudeSelectionRangeSlider.ValueLeft.ToString(CultureInfo.InvariantCulture);
            MagnitudeUpperLabel.Text = MagnitudeSelectionRangeSlider.ValueRight.ToString(CultureInfo.InvariantCulture);
        }

        private void DateSelectionRangeSlider_ValueChanged(object sender, EventArgs e)
        {
            DateLowerLabel.Text = DateSelectionRangeSlider.ValueLeft.ToString(CultureInfo.InvariantCulture);
            DateUpperLabel.Text = DateSelectionRangeSlider.ValueRight.ToString(CultureInfo.InvariantCulture);
        }

        private void DepthSelectionRangeSlider_ValueChanged(object sender, EventArgs e)
        {
            DepthLowerLabel.Text = DepthSelectionRangeSlider.ValueLeft.ToString(CultureInfo.InvariantCulture);
            DepthUpperLabel.Text = DepthSelectionRangeSlider.ValueRight.ToString(CultureInfo.InvariantCulture);
        }

        private void MapModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton != null)
            {
                radioButton.FlatAppearance.BorderColor = radioButton.Checked ? Color.FromArgb(136, 136, 136) : Color.White;
                if (radioButton.Checked)
                {
                    if (radioButton.Equals(PanRadioButton))
                    {
                        map.TrackOverlay.TrackMode = TrackMode.None;
                    }
                    else if (radioButton.Equals(rbtnDrawPolygon))
                    {
                        map.TrackOverlay.TrackMode = TrackMode.Polygon;
                    }
                    else if (radioButton.Equals(rbtnDrawRectangle))
                    {
                        map.TrackOverlay.TrackMode = TrackMode.Rectangle;
                    }
                    else if (radioButton.Equals(rbtnDrawCircle))
                    {
                        map.TrackOverlay.TrackMode = TrackMode.Circle;
                    }
                }
            }
        }

        private void ConfigurationSlider_MouseUp(object sender, MouseEventArgs e)
        {
            FilterEarthquake();
        }

        private void ClearTrackResult_Click(object sender, EventArgs e)
        {
            PanRadioButton.Checked = true;

            map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            markerMemoryLayer.InternalFeatures.Clear();
            markerMemoryHighlightLayer.InternalFeatures.Clear();

            map.Refresh(map.TrackOverlay);
            map.Refresh(map.Overlays["QueryResultMarkerOverlay"]);
            earthquakeItems.Clear();
            QueryResultItemsDataGridView.Rows.Clear();
        }

        private void OverlaySwitcher_OverlayChanged(object sender, OverlayChangedOverlaySwitcherEventArgs e)
        {
            BingMapsOverlay bingMapsOverlay = e.Overlay as BingMapsOverlay;
            if (bingMapsOverlay != null)
            {
                e.Cancel = ApplyBingMapsKey();
            }
        }

        private bool ApplyBingMapsKey()
        {
            bool cancel = false;
            string bingMapsKey = MapSuiteSampleHelper.GetBingMapsKey();
            if (!string.IsNullOrEmpty(bingMapsKey))
            {
                foreach (BingMapsOverlay bingOverlay in map.Overlays.OfType<BingMapsOverlay>())
                {
                    bingOverlay.ApplicationId = bingMapsKey;
                }
            }
            else
            {
                cancel = true;
            }

            return cancel;
        }

        private void QueryResultItemsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                DataGridViewCell cell = QueryResultItemsDataGridView.Rows[e.RowIndex].Cells["cluLocation"];
                if (cell.Value != null)
                {
                    map.CurrentExtent = BaseShape.CreateShapeFromWellKnownData((string)cell.Value).GetBoundingBox();
                    map.Refresh();
                }
            }
        }

        private void QueryResultItemsDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            UpdateDataGridCellBackgroundColor(e, Color.LightBlue);
        }

        private void QueryResultItemsDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            UpdateDataGridCellBackgroundColor(e, Color.White);
        }

        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            PointShape mouseLocation = ExtentHelper.ToWorldCoordinate(map.CurrentExtent, new ScreenPointF(e.X, e.Y), map.Width, map.Height);
            lblFooterLocationX.Text = string.Format(CultureInfo.InvariantCulture, "X:{0:N2}", mouseLocation.X);
            lblFooterLocationY.Text = string.Format(CultureInfo.InvariantCulture, "Y:{0:N2}", mouseLocation.Y);
        }

        private void OverlaySwitcherPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox tempPictureBox = (PictureBox)sender;
            tempPictureBox.Cursor = Cursors.Hand;
            if (switcher != null)
            {
                switcher.Visible = !switcher.Visible;
                tempPictureBox.Image = switcher.Visible ? Resources.switcher_minimize : Resources.switcher_maxmize;
            }
        }

        private void RefreshMarkersByFeatures(IEnumerable<Feature> features)
        {
            markerMemoryLayer.InternalFeatures.Clear();
            markerMemoryHighlightLayer.InternalFeatures.Clear();

            if (features != null)
            {
                foreach (Feature item in features)
                {
                    markerMemoryLayer.InternalFeatures.Add(item);
                }
            }

            map.Refresh(map.Overlays["QueryResultMarkerOverlay"]);
        }

        private void UpdateDataGridCellBackgroundColor(DataGridViewCellEventArgs e, Color newColor)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = QueryResultItemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    cell.Style.BackColor = newColor;
                }
            }
        }

        private void InitializeBackground()
        {
            pnlQueryTool.BackgroundImage = GetRoundRectangleBitmap(pnlQueryTool);
            pnlConfigurationTools.BackgroundImage = GetRoundRectangleBitmap(pnlConfigurationTools);
        }

        private void InitializeToolTips()
        {
            ToolTip panToolTip = new ToolTip();
            panToolTip.SetToolTip(PanRadioButton, "Pan the Map");

            ToolTip drawCircleToolTip = new ToolTip();
            drawCircleToolTip.SetToolTip(rbtnDrawCircle, "Draw Circle Range");

            ToolTip drawPolygonToolTip = new ToolTip();
            drawPolygonToolTip.SetToolTip(rbtnDrawPolygon, "Draw Polygon Range");

            ToolTip drawRectangleToolTip = new ToolTip();
            drawRectangleToolTip.SetToolTip(rbtnDrawRectangle, "Draw Rectangle Range");

            ToolTip clearSelectionToolTip = new ToolTip();
            clearSelectionToolTip.SetToolTip(rbtnClearAll, "Clear Selection");

            ToolTip heatMapToolTip = new ToolTip();
            heatMapToolTip.SetToolTip(HeatMapRadioButton, "Heat Map");

            ToolTip pointMapToolTip = new ToolTip();
            pointMapToolTip.SetToolTip(rbtnPointMap, "Regular Point Map");

            ToolTip isoLineMapToolTip = new ToolTip();
            isoLineMapToolTip.SetToolTip(rbtnIsoMap, "IsoLines Map");
        }

        private void QueryResultItemsDataGridView_SizeChanged(object sender, EventArgs e)
        {
            cluEmpty.Width = QueryResultItemsDataGridView.Width
                - cluDepth.Width
                - cluLatitude.Width
                - cluLongitude.Width
                - cluMagnitude.Width
                - cluYear.Width
                - cluZoomto.Width
                - 10;
        }

        private void LeftSideBarPanel_PanelCollapseButtonClick(object sender, EventArgs e)
        {
            dataAndMapSpliter.Width = Width - leftSideBarPanel.Width - 15;
            dataAndMapSpliter.Left = leftSideBarPanel.Width;
        }

        private static Bitmap GetRoundRectangleBitmap(Control panel)
        {
            Bitmap panelBitmap = new Bitmap(panel.Width - 1, panel.Height - 1);
            using (Graphics g = Graphics.FromImage(panelBitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                Rectangle drawingRectangle = new Rectangle(0, 0, panelBitmap.Width, panelBitmap.Height);
                GraphicsPath path = MapSuiteSampleHelper.CreateRoundPath(drawingRectangle, 10);
                g.DrawPath(new Pen(Color.LightGray, 1), path);
            }
            return panelBitmap;
        }
    }
}