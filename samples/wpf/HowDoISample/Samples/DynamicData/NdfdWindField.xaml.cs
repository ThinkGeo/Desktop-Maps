using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The NDFD wind forecast as a VectorFieldSource, drawn the way weather apps
    /// draw wind: sixty-five thousand tracer particles advected through the field,
    /// streaming and fading continuously - AddParticleFlow is one call and the
    /// simulation runs itself from there. The toggle switches to the
    /// field's other rendering, magnitude-scaled arrows, and both read the same
    /// source object: one field, two renderings, the ValueGridSource pattern again.
    /// </summary>
    public partial class NdfdWindField
    {
        private GpuBasemap _basemap;
        private MapStyle _style;
        private VectorFieldSource _field;
        private bool _built;

        public NdfdWindField()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_built || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
            {
                return;
            }

            _built = true;
            try
            {
                LoadField();

                _style = new MapStyle();
                _style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
                _style.AddParticleFlow(_field);
                _basemap = new GpuBasemap(_style);
                Map.Basemap = _basemap;

                // The lower 48, where the forecast grid lives.
                Map.CurrentExtent = new RectangleShape(-14026255, 6446275, -7235766, 2632018);
                Status.Text = "NDFD surface wind - 65k particles advected, color is speed";
            }
            catch (Exception ex)
            {
                Status.Text = "Wind data unavailable: " + ex.Message;
            }

            await Map.RefreshAsync();
        }

        /// <summary>
        /// Reads the first forecast hour of speed and direction into one
        /// VectorFieldSource, in the grid's own Lambert projection - the renderings
        /// reproject, not this code.
        /// </summary>
        private void LoadField()
        {
            var speed = GdalGrid.ReadBand(Path.Combine(AppContext.BaseDirectory, "Data", "Ndfd", "ds.wspd.tif"));
            var from = GdalGrid.ReadBand(Path.Combine(AppContext.BaseDirectory, "Data", "Ndfd", "ds.wdir.tif"));

            // A meteorological wind direction is where the wind comes FROM; the arrow
            // points where it is going.
            var rows = speed.Values.GetLength(0);
            var columns = speed.Values.GetLength(1);
            var flow = new FlowVector[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    flow[row, column] = FlowVector.FromSpeedDirection(speed.Values[row, column], (from.Values[row, column] + 180f) % 360f);
                }
            }

            _field = new VectorFieldSource(flow, speed.Extent, speed.Projection);
        }

        private void Mode_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_style == null)
            {
                return;
            }

            // The document never changes, so this SetStyleAsync only re-applies the
            // registrations - no tile is rebuilt for the switch.
            _style.RemoveVectorField(_field).AddParticleFlow(_field);
            _ = _basemap.SetStyleAsync(_style);
            Status.Text = "NDFD surface wind - 65k particles advected, color is speed";
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (_style == null)
            {
                return;
            }

            _style.RemoveParticleFlow(_field).AddVectorField(_field, new VectorFieldStyle { MaxArrows = 3000 });
            _ = _basemap.SetStyleAsync(_style);
            Status.Text = "The same field as magnitude-scaled arrows";
        }
    }
}
