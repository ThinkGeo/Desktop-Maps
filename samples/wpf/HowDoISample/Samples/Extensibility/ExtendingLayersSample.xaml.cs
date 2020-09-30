using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class ExtendingLayersSample : UserControl, IDisposable
    {
        public ExtendingLayersSample()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-155.733, 95.60, 104.42, -81.9);

            BackgroundLayer backgroundLayer = new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean));

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"..\..\..\Data\Shapefile\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the mapShapeLayer to the MapEngine
            CustomMapShapeLayer mapShapeLayer = new CustomMapShapeLayer();

            CustomMapShape mapShape1 = new CustomMapShape(new Feature(-104, 42));
            mapShape1.ZoomLevels.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Red,20);
            mapShape1.ZoomLevels.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            mapShapeLayer.MapShapes.Add("1", mapShape1);

            CustomMapShape mapShape2 = new CustomMapShape(new Feature(104, 39));
            mapShape2.ZoomLevels.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleStarStyle(GeoColors.Black,30);
            mapShape2.ZoomLevels.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            mapShapeLayer.MapShapes.Add("2", mapShape2);

            worldLayer.Open();
            Feature feature3 = worldLayer.QueryTools.GetFeatureById("222", ReturningColumnsType.AllColumns);
            CustomMapShape mapShape3 = new CustomMapShape(feature3);
            mapShape3.ZoomLevels.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Green);
            mapShape3.ZoomLevels.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            mapShapeLayer.MapShapes.Add("3", mapShape3);

            BaseShape shape = new PointShape(-71, -52).GetShortestLineTo(new PointShape(38, 8), GeographyUnit.DecimalDegree);
            CustomMapShape mapShape4 = new CustomMapShape(new Feature(shape));
            mapShape4.ZoomLevels.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue,2,true);
            mapShape4.ZoomLevels.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            mapShapeLayer.MapShapes.Add("4", mapShape4);

            LayerOverlay worldOverlay = new LayerOverlay();
            mapView.Overlays.Add(worldOverlay);
            
            worldOverlay.Layers.Add(backgroundLayer);
            worldOverlay.Layers.Add(worldLayer);
            worldOverlay.Layers.Add(mapShapeLayer);
           
            mapView.Refresh();
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    public class CustomMapShapeLayer : Layer
    {
        private Dictionary<string, CustomMapShape> mapShapes;

        public CustomMapShapeLayer()
        {
            this.mapShapes = new Dictionary<string, CustomMapShape>();
        }

        // Here is where you place all of your map shapes.
        public Dictionary<string, CustomMapShape> MapShapes
        {
            get { return mapShapes; }
        }

        // This is a required overload of the Layer.  As you can see we simply
        // loop through all of our map shapes and then choose the correct zoom level.
        // After that, the zoom level class takes care of the heavy lifting.  You
        // have to love how easy this framework is to re-use.
        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (string mapShapeKey in mapShapes.Keys)
            {
                CustomMapShape mapShape = mapShapes[mapShapeKey];
                ZoomLevel currentZoomLevel = mapShape.ZoomLevels.GetZoomLevelForDrawing(canvas.CurrentWorldExtent, canvas.Width, canvas.MapUnit);
                if (currentZoomLevel != null)
                {
                    if (canvas.CurrentWorldExtent.Intersects(mapShape.Feature.GetBoundingBox()))
                    {
                        currentZoomLevel.Draw(canvas, new Feature[] { mapShape.Feature }, new Collection<SimpleCandidate>(), labelsInAllLayers);
                    }
                }
            }
        }
    }
    public class CustomMapShape
    {
        private Feature feature;
        private ZoomLevelSet zoomLevelSet;

        public CustomMapShape()
            : this(new Feature())
        {
        }

        // Let's use this as a handy constructor if you already have
        // a feature or want to create one inline.
        public CustomMapShape(Feature feature)
        {
            this.feature = feature;
            zoomLevelSet = new ZoomLevelSet();
        }

        //  This is the feature property, pretty simple.
        public Feature Feature
        {
            get { return feature; }
            set { feature = value; }
        }

        // This is the Zoom Level Set.  This high level object has all of
        // the logic in it for zoom levels, drawing and everything.
        public ZoomLevelSet ZoomLevels
        {
            get { return zoomLevelSet; }
            set { zoomLevelSet = value; }
        }
    }

}