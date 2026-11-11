using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// An adornment layer of your own: rings every few miles around the center of the view, drawn over the map.
    /// </summary>
    public partial class CustomAdornmentLayer : IDisposable
    {
        private bool _initialized;

        public CustomAdornmentLayer()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            Map.AdornmentOverlay.Layers.Add(new RadiusLayer
            {
                RingDistanceUnit = DistanceUnit.Mile,
                RingGeography = GeographyUnit.Meter,
                RingDistance = 5
            });

            Map.CenterPoint = new PointShape(-10780320, 3915120);
            Map.CurrentScale = 288900;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Rings every RingDistance around the center of the view, until one no longer fits in it.
    /// DrawCore draws straight onto the map's canvas, so an adornment can draw anything the canvas can.
    /// </summary>
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
            do
            {
                circle = centerPoint.Buffer(currentRingDistance, RingGeography, RingDistanceUnit);
                canvas.DrawArea(circle, RingAreaStyle.OutlinePen, RingAreaStyle.FillBrush, DrawingLevel.LevelOne);
                currentRingDistance += RingDistance;
            } while (canvas.CurrentWorldExtent.Contains(circle));
        }
    }
}
