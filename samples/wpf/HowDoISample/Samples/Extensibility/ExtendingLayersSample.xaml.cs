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
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add(layerOverlay);

            RadiusLayer radiusLayer = new RadiusLayer();
            radiusLayer.RingDistanceUnit = DistanceUnit.Mile;
            radiusLayer.RingGeography = GeographyUnit.Meter;
            radiusLayer.RingDistance = 5;

            layerOverlay.Layers.Add(radiusLayer);

            mapView.CurrentExtent = new RectangleShape(-10812042.5236828, 3942445.36497713, -10748599.7905585, 3887792.89005685);
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    // This layer overrides the DrawCore and draws circles every x miles based on the center point
    // of the screen.  You notice in the DrawCore we can draw directly on the canvas which gives us
    // allot of power.  This is similar to custom styles where we can also draw directly on the canvas
    // from the style.
    public class RadiusLayer : Layer
    {
        public RadiusLayer()
        {
            RingDistanceUnit = DistanceUnit.Mile;
            RingGeography = GeographyUnit.Meter;
            RingDistance = 1;
            RingAreaStyle = new AreaStyle(new GeoPen(new GeoSolidBrush(GeoColor.FromArgb(50, GeoColors.Blue)), 4));
        }

        public double RingDistance { get; set; }

        public DistanceUnit RingDistanceUnit { get; set; }

        public GeographyUnit RingGeography { get; set; }

        public AreaStyle RingAreaStyle { get; set; }
        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            PointShape centerPoint = canvas.CurrentWorldExtent.GetCenterPoint();

            double currentRingDistance = RingDistance;
            MultipolygonShape circle = null;

            // Keep drawing rings until the only barley fit inside the current extent.
            do
            {
                circle = centerPoint.Buffer(currentRingDistance, RingGeography, RingDistanceUnit);

                canvas.DrawArea(circle, RingAreaStyle.OutlinePen, RingAreaStyle.FillBrush, DrawingLevel.LevelOne);
                currentRingDistance += RingDistance;
            } while (canvas.CurrentWorldExtent.Contains(circle));
        }
    }
}