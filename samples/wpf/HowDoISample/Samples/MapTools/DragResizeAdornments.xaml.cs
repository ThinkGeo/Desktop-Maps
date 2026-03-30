using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Drag/Resize adornments sample.
    /// </summary>
    public partial class DragResizeAdornments : IDisposable
    {
        private readonly List<AdornmentLayer> _adornments = new List<AdornmentLayer>();
        private bool _initialized;

        public DragResizeAdornments()
        {
            InitializeComponent();
        }

        private void MapView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            MapView.MapUnit = GeographyUnit.Meter;

            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp");
            housingUnitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var legend = CreateLegend();
            MapView.AdornmentOverlay.Layers.Add(legend);

            AddClassBreakStyle(housingUnitsLayer, legend);

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(housingUnitsLayer);
            MapView.Overlays.Add(layerOverlay);

            var scaleLine = new ScaleLineAdornmentLayer
            {
                Projection = new Projection(3857),
                Location = AdornmentLocation.LowerLeft
            };
            var scaleBar = new ScaleBarAdornmentLayer
            {
                Projection = new Projection(3857),
                Location = AdornmentLocation.LowerLeft,
                YOffsetInPixel = -45
            };

            var logo = new LogoAdornmentLayer(new GeoImage(@"..\..\..\Resources\generic-logo.png"))
            {
                Location = AdornmentLocation.LowerRight
            };

            var magneticNorth = new MagneticDeclinationAdornmentLayer(AdornmentLocation.UpperLeft)
            {
                Projection = new Projection(3857)
            };
            magneticNorth.TrueNorthPointStyle.SymbolSize = 25;
            magneticNorth.TrueNorthLineStyle.InnerPen.Width = 2f;
            magneticNorth.TrueNorthLineStyle.OuterPen.Width = 5f;
            magneticNorth.MagneticNorthLineStyle.InnerPen.Width = 2f;
            magneticNorth.MagneticNorthLineStyle.OuterPen.Width = 5f;

            MapView.AdornmentOverlay.Layers.Add(scaleLine);
            MapView.AdornmentOverlay.Layers.Add(scaleBar);
            MapView.AdornmentOverlay.Layers.Add(logo);
            MapView.AdornmentOverlay.Layers.Add(magneticNorth);

            _adornments.AddRange(new AdornmentLayer[]
            {
                scaleLine,
                scaleBar,
                logo,
                magneticNorth,
                legend
            });

            housingUnitsLayer.Open();
            var housingUnitsLayerBBox = housingUnitsLayer.GetBoundingBox();
            MapView.CenterPoint = housingUnitsLayerBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, housingUnitsLayerBBox, MapView.MapWidth, MapView.MapHeight) * 1.5;
            housingUnitsLayer.Close();

            _initialized = true;
            ApplyAdornmentInteraction();

            _ = MapView.RefreshAsync();
        }

        private static LegendAdornmentLayer CreateLegend()
        {
            var legend = new LegendAdornmentLayer
            {
                Title = new LegendItem
                {
                    TextStyle = new TextStyle("Housing Units", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
                Location = AdornmentLocation.UpperRight,
                ResizeMode = AdornmentResizeMode.MaintainAspectRatio
            };

            return legend;
        }

        private static void AddClassBreakStyle(FeatureLayer layer, LegendAdornmentLayer legend)
        {
            var housingUnitsStyle = new ClassBreakStyle("H_UNITS");

            var classBreakIntervals = new double[] { 0, 1000, 2000, 3000, 4000, 5000 };
            var colors = GeoColor.GetColorsInHueFamily(GeoColors.Red, classBreakIntervals.Length).Reverse().ToList();

            for (var i = 0; i < classBreakIntervals.Length; i++)
            {
                var classBreak = new ClassBreak(classBreakIntervals[i], AreaStyle.CreateSimpleAreaStyle(new GeoColor(192, colors[i]), GeoColors.White));
                housingUnitsStyle.ClassBreaks.Add(classBreak);

                var legendItem = new LegendItem
                {
                    ImageStyle = classBreak.DefaultAreaStyle,
                    TextStyle = new TextStyle($@">{classBreak.Value} units", new GeoFont("Verdana", 10), GeoBrushes.Black)
                };
                legend.LegendItems.Add(legendItem);
            }

            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(housingUnitsStyle);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        private void DragMode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            ApplyAdornmentInteraction();
        }

        private void ResizeMode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            ApplyAdornmentInteraction();
        }

        private void ApplyAdornmentInteraction()
        {
            var dragMode = RdoDragYes.IsChecked == true ? AdornmentDragMode.Draggable : AdornmentDragMode.Fixed;
            var resizeMode = RdoResizeYes.IsChecked == true ? AdornmentResizeMode.MaintainAspectRatio : AdornmentResizeMode.Fixed;

            foreach (var adornment in _adornments)
            {
                adornment.DragMode = dragMode;
                adornment.ResizeMode = resizeMode;
            }

            _ = MapView.AdornmentOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
