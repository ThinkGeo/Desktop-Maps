using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class ESRIGridLayer : UserControl
    {
        public ESRIGridLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            var mos = new ShapeFileFeatureSource(@"../../../data/Frisco_Mosquitos.shp");
            mos.Open();
            var features = mos.GetAllFeatures(ReturningColumnsType.AllColumns);

            Dictionary<PointShape, double> points = new Dictionary<PointShape, double>();

            foreach (var feature in features)
            {
                if (feature.ColumnValues["DateCollec"] == "20190806")
                {
                    int male = 0;
                    int female = 0;

                    if (feature.ColumnValues["Male"] != "")
                        male = int.Parse(feature.ColumnValues["Male"]);

                    if (feature.ColumnValues["Female"] != "")
                        female = int.Parse(feature.ColumnValues["Female"]);

                    points.Add((PointShape)feature.GetShape(), male + female);
                }
            }
            FileStream stream = new FileStream(@"C:\temp\Mosquitos.grd", FileMode.Create, FileAccess.ReadWrite);
            GridFeatureSource.GenerateGrid(new GridDefinition(RectangleShape.ScaleUp(mos.GetBoundingBox(),30).GetBoundingBox(), 300, -999, points), new InverseDistanceWeightedGridInterpolationModel(), stream);
            stream.Close();

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ClassBreakStyle gridClassBreakStyle = new ClassBreakStyle("CellValue");
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColors.Transparent))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(12, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(24, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(36, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(48, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(60, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(72, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(84, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 0, 255, 0)))));

            GridFeatureLayer gridFeatureLayer = new GridFeatureLayer(@"..\..\..\data\Mosquitos.grd");
            gridFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            gridFeatureLayer.Open();

            LayerOverlay staticOverlay = new LayerOverlay();
            //staticOverlay.TileType = TileType.SingleTile;
            staticOverlay.Layers.Add("GridFeatureLayer", gridFeatureLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.CurrentExtent = gridFeatureLayer.GetBoundingBox();

            mapView.Refresh();
        }
    }
}
