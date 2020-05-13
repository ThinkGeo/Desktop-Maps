using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DetermineTheLengthOfALineFeature : UserControl
    {
        public DetermineTheLengthOfALineFeature()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-10881016, 3541873, -10879054, 3540253);


            ShapeFileFeatureLayer roadLayer = new ShapeFileFeatureLayer(SampleHelper.Get("austinstreets_3857.shp"));
            roadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            roadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.White, 9.2F, GeoColors.DarkGray, 12.2F, true);

            LayerOverlay roadOverlay = new LayerOverlay();
            roadOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));
            roadOverlay.Layers.Add("RoadLayer", roadLayer);
            mapView.Overlays.Add("RoadOverlay", roadOverlay);

            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.White, 9.2F, GeoColors.DarkGray, 12.2F, true);
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.InnerPen.Brush = new GeoSolidBrush(GeoColor.FromArgb(150, GeoColors.Blue));
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay highlightOverlay = new LayerOverlay();
            highlightOverlay.Layers.Add("HighlightLayer", highlightLayer);
            mapView.Overlays.Add("HighlightOverlay", highlightOverlay);

            PopupOverlay popupOverlay = new PopupOverlay();
            mapView.Overlays.Add("PopupOverlay", popupOverlay);

            mapView.Refresh();
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            FeatureLayer worldLayer = mapView.FindFeatureLayer("RoadLayer");
            InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("HighlightLayer");
            Overlay highlightOverlay = mapView.Overlays["HighlightOverlay"];

            // Find the road the user clicked on.
            worldLayer.Open();
            Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new string[1] { "FENAME" });
            worldLayer.Close();

            //Determine the length of the road.
            if (selectedFeatures.Count > 0)
            {
                LineBaseShape lineShape = (LineBaseShape)selectedFeatures[0].GetShape();
                highlightLayer.Open();
                highlightLayer.InternalFeatures.Clear();
                highlightLayer.InternalFeatures.Add(new Feature(lineShape));
                highlightLayer.Close();
                ProjectionConverter project = new ProjectionConverter(3857, 4326);
                project.Open();
                double length = ((LineBaseShape)project.ConvertToExternalProjection(lineShape)).GetLength(GeographyUnit.DecimalDegree, DistanceUnit.Meter);
                project.Close();
                string lengthMessage = string.Format(CultureInfo.InvariantCulture, "{0} has a length of {1:F2} meters.", selectedFeatures[0].ColumnValues["FENAME"].Trim(), length);

                Popup popup = new Popup(e.WorldLocation);
                popup.Content = lengthMessage;
                PopupOverlay popupOverlay = (PopupOverlay)mapView.Overlays["PopupOverlay"];
                popupOverlay.Popups.Clear();
                popupOverlay.Popups.Add(popup);

                highlightOverlay.Refresh();
                popupOverlay.Refresh();
            }
        }
    }
}