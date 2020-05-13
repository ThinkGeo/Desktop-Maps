using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class EfficientlyMoveAPlaneImage : UserControl
    {
        private DispatcherTimer timer;
        private int index;

        public EfficientlyMoveAPlaneImage()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += new EventHandler(timer_Tick);
            Unloaded += new RoutedEventHandler(EfficientlyMoveAPlaneImage_Unloaded);
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);

            ThinkGeoCloudRasterMapsOverlay worldMapKitDesktopOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(worldMapKitDesktopOverlay);

            // Setup the shapefile layer.
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Setup the mapshape layer.
            InMemoryFeatureLayer bitmapLayer = new InMemoryFeatureLayer();
            bitmapLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Image;
            bitmapLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(SampleHelper.Get("Prop Plane.png"));
            bitmapLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 1F, LineDashStyle.Dash, false);
            bitmapLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ProjectionConverter project = new ProjectionConverter(4326,3857);
            project.Open();
            PointShape planeShape = new PointShape(-95.2806, 38.9554);
            PointShape destinationPoint = new PointShape(36.04, 48.49);
            MultilineShape airLineShape = (MultilineShape)project.ConvertToExternalProjection(planeShape.GreatCircle(destinationPoint));
            airLineShape.Id = "AirLine";
            bitmapLayer.Open();
            bitmapLayer.EditTools.BeginTransaction();
            bitmapLayer.EditTools.Add(new Feature(project.ConvertToExternalProjection(planeShape).GetWellKnownBinary(), "Plane"));
            bitmapLayer.EditTools.Add(new Feature(airLineShape.GetWellKnownBinary(), "AirLine"));
            bitmapLayer.EditTools.CommitTransaction();
            bitmapLayer.Close();
            project.Close();

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            LayerOverlay planeOverlay = new LayerOverlay();
            planeOverlay.TileType = TileType.SingleTile;
            planeOverlay.Layers.Add("BitmapLayer", bitmapLayer);
            mapView.Overlays.Add("PlaneOverlay", planeOverlay);

            mapView.Refresh();

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LayerOverlay planeOverlay = (LayerOverlay)mapView.Overlays["PlaneOverlay"];
            InMemoryFeatureLayer bitmapLayer = (InMemoryFeatureLayer)planeOverlay.Layers["BitmapLayer"];
            PointShape pointShape = bitmapLayer.InternalFeatures["Plane"].GetShape() as PointShape;
            LineShape airLine = ((MultilineShape)bitmapLayer.InternalFeatures["AirLine"].GetShape()).Lines[0];
            index += 30;
            if (index < airLine.Vertices.Count)
            {
                Vertex newPosition = airLine.Vertices[index];
                pointShape.X = newPosition.X;
                pointShape.Y = newPosition.Y;
                pointShape.Id = "Plane";

                bitmapLayer.Open();
                bitmapLayer.EditTools.BeginTransaction();
                bitmapLayer.EditTools.Update(pointShape);
                bitmapLayer.EditTools.CommitTransaction();
                bitmapLayer.Close();
            }
            else
            {
                index = 0;
            }

            planeOverlay.Refresh();
        }

        private void EfficientlyMoveAPlaneImage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
        }
    }
}