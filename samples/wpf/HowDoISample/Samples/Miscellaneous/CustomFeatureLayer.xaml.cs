using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class CustomFeatureLayer : IDisposable
    {
        public CustomFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var layerOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile
            };
            MapView.Overlays.Add(layerOverlay);

            var radiusLayer = new RadiusLayer
            {
                RingDistanceUnit = DistanceUnit.Mile,
                RingGeography = GeographyUnit.Meter,
                RingDistance = 5
            };

            layerOverlay.Layers.Add(radiusLayer);
            MapView.CenterPoint = new PointShape(-10780320, 3915120);
            MapView.CurrentScale = 288900;

            _ = MapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    // This layer overrides the DrawCore and draws circles every x miles based on the center point
    // of the screen.  You notice in the DrawCore we can draw directly on the canvas which gives us
    // a lot of power.  This is similar to custom styles where we can also draw directly on the canvas
    // from the style.
    public class RadiusLayer : Layer
    {
        public double RingDistance { get; set; } = 1;

        public DistanceUnit RingDistanceUnit { get; set; } = DistanceUnit.Mile;

        public GeographyUnit RingGeography { get; set; } = GeographyUnit.Meter;

        public AreaStyle RingAreaStyle { get; set; } = new AreaStyle(new GeoPen(new GeoSolidBrush(GeoColor.FromArgb(50, GeoColors.Blue)), 4));

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            var centerPoint = canvas.CurrentWorldExtent.GetCenterPoint();

            var currentRingDistance = RingDistance;
            MultipolygonShape circle;

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