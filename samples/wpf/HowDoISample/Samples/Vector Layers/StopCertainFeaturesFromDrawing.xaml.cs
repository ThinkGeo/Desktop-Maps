using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class StopCertainFeaturesFromDrawing : UserControl
    {
        public StopCertainFeaturesFromDrawing()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-15963215, 20037508, 12990985, -13516534);
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.DeepOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.RequiredColumnNames.Add("POP_CNTRY");
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.DrawingFeatures += new EventHandler<DrawingFeaturesEventArgs>(worldLayer_DrawingFeatures);

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            mapView.Refresh();
        }

        private void worldLayer_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            Collection<Feature> featuresToDrawn = new Collection<Feature>();
            foreach (Feature feature in e.FeaturesToDraw)
            {
                double population = Convert.ToDouble(feature.ColumnValues["POP_CNTRY"], CultureInfo.InvariantCulture);
                if (population > 10000000)
                {
                    featuresToDrawn.Add(feature);
                }
            }

            e.FeaturesToDraw.Clear();
            foreach (Feature feature in featuresToDrawn)
            {
                e.FeaturesToDraw.Add(feature);
            }
        }
    }
}