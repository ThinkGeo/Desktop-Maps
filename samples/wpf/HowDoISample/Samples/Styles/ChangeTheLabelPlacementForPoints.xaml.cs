using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ChangeTheLabelPlacementForPoints : UserControl
    {
        public ChangeTheLabelPlacementForPoints()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-14070784, 6240993, -7458406, 2154936);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates_3857.shp"));
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer majorCitiesShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("MajorCities_3857.shp"));
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateCompoundCircleStyle(GeoColors.White, 10F, GeoColors.Black, 1F, GeoColors.Black, 7F);
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("AREANAME", "Arial", 10, DrawingFontStyles.Regular, GeoColors.Black, 0, -8);
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay statesOverlay = new LayerOverlay();
            statesOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            statesOverlay.Layers.Add("World", worldLayer);
            statesOverlay.Layers.Add("States", statesLayer);
            mapView.Overlays.Add("StatesOverlay", statesOverlay);

            LayerOverlay cityOverlay = new LayerOverlay();
            cityOverlay.TileType = TileType.SingleTile;
            cityOverlay.Layers.Add("MajorCitiesShapes", majorCitiesShapeLayer);
            mapView.Overlays.Add("CityOverlay", cityOverlay);

            mapView.Refresh();
        }

        private void cbxPointPlacement_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                TextPlacement placement;
                switch (cbxPointPlacement.SelectedItem.ToString().Split(':')[1].Trim())
                {
                    case "Center":
                        placement = TextPlacement.Center;
                        break;

                    case "CenterLeft":
                        placement = TextPlacement.Left;
                        break;

                    case "CenterRight":
                        placement = TextPlacement.Right;
                        break;

                    case "LowerCenter":
                        placement = TextPlacement.Lower;
                        break;

                    case "LowerLeft":
                        placement = TextPlacement.LowerLeft;
                        break;

                    case "LowerRight":
                        placement = TextPlacement.LowerRight;
                        break;

                    case "UpperCenter":
                        placement = TextPlacement.Upper;
                        break;

                    case "UpperLeft":
                        placement = TextPlacement.UpperLeft;
                        break;

                    case "UpperRight":
                        placement = TextPlacement.UpperRight;
                        break;

                    default:
                        placement = TextPlacement.Right;
                        break;
                }

                FeatureLayer labelPlacementLayer = mapView.FindFeatureLayer("MajorCitiesShapes");
                labelPlacementLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.TextPlacement = placement;

                mapView.Overlays["CityOverlay"].Refresh();
            }
        }
    }
}