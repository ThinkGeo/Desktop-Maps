using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// An adornment layer of your own: range rings around the center of the view, drawn over the map.
    /// </summary>
    public partial class CustomAdornmentLayer
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
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));

            Map.AdornmentOverlay.Layers.Add(new RangeRingsAdornmentLayer { RingDistanceInMiles = 5 });

            Map.CenterPoint = new PointShape(-10780320, 3915120);
            Map.CurrentScale = 288900;

            _ = Map.RefreshAsync();
        }
    }

    /// <summary>
    /// Rings every few miles around the center of the view, each labelled with its distance. DrawCore
    /// draws straight onto the map's canvas, so an adornment can draw anything the canvas can.
    /// </summary>
    public class RangeRingsAdornmentLayer : AdornmentLayer
    {
        public double RingDistanceInMiles { get; set; } = 1;

        public GeoPen RingPen { get; set; } = new GeoPen(GeoColor.FromArgb(160, GeoColors.DodgerBlue), 3);

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            var extent = canvas.CurrentWorldExtent;
            var center = extent.GetCenterPoint();

            // Spherical Mercator draws a ground distance 1 / cos(latitude) times longer, so the
            // rings are widened by that much to stay true on the ground.
            var latitude = Math.Atan(Math.Sinh(center.Y / 6378137));
            var ringInMapMeters = Conversion.ConvertMeasureUnits(RingDistanceInMiles, DistanceUnit.Mile, DistanceUnit.Meter) / Math.Cos(latitude);

            var font = new GeoFont("Arial", 10, DrawingFontStyles.Bold);
            var halo = new GeoPen(GeoColors.Black, 3);

            // Ring after ring until one reaches past the view; zoomed far out, at most 50.
            for (var i = 1; i <= 50; i++)
            {
                var radius = ringInMapMeters * i;
                var ring = new EllipseShape(center, radius);
                canvas.DrawArea(ring, RingPen, DrawingLevel.LevelOne);
                canvas.DrawTextWithWorldCoordinate($"{RingDistanceInMiles * i} mi", font, GeoBrushes.LightSkyBlue, halo, center.X, center.Y + radius, DrawingLevel.LabelLevel);

                if (!extent.Contains(ring)) break;
            }
        }
    }
}
