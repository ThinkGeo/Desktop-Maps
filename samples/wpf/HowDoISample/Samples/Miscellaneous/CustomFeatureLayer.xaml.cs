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

        private bool _initialized;
        public CustomFeatureLayer()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var radiusLayer = new RadiusLayer
            {
                RingDistanceUnit = DistanceUnit.Mile,
                RingGeography = GeographyUnit.Meter,
                RingDistance = 5
            };

            Map.AdornmentOverlay.Layers.Add(radiusLayer);
            Map.CenterPoint = new PointShape(-10780320, 3915120);
            Map.CurrentScale = 288900;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    // This layer overrides the DrawCore and draws circles every x miles based on the center point
    // of the screen.  You notice in the DrawCore we can draw directly on the canvas which gives us
    // a lot of power.  This is similar to custom styles where we can also draw directly on the canvas
    // from the style.
    public class RadiusLayer : AdornmentLayer
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