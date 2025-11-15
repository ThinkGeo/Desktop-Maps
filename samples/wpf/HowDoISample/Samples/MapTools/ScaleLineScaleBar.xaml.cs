using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render a ScaleLine on the map in a variety of different of styles.
    /// </summary>
    public partial class ScaleLineScaleBar : IDisposable
    {
        private bool _initialized;
        private ScaleBarAdornmentLayer _scaleBarAdornmentLayer;
        private ScaleLineAdornmentLayer _scaleLineAdornmentLayer;

        public ScaleLineScaleBar()
        {
            InitializeComponent();

            // Fill ComboBox from the GeoHatchStyle enum
            foreach (AdornmentLocation style in Enum.GetValues(typeof(AdornmentLocation)))
            {
                CboLocations.Items.Add(style);
            }

            // Select a default value
            CboLocations.SelectedItem = AdornmentLocation.LowerRight;


            foreach (ScaleLineUnitSystem unitSystem in Enum.GetValues(typeof(ScaleLineUnitSystem)))
            {
                CboUnitSystems.Items.Add(unitSystem);
            }
            CboUnitSystems.SelectedItem = ScaleLineUnitSystem.ImperialAndMetric;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
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

            // Add Scale Line Adornment Layer
            var selectedUnitSystem = CboUnitSystems.SelectedItem is ScaleLineUnitSystem unitSystem ? unitSystem : ScaleLineUnitSystem.ImperialAndMetric;

            _scaleLineAdornmentLayer = new ScaleLineAdornmentLayer
            {
                Projection = new Projection(3857),
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightBlue, GeoColors.Red),
                UnitSystem = selectedUnitSystem
            };

            // Add ScaleBarAdornmentLayer
            _scaleBarAdornmentLayer = new ScaleBarAdornmentLayer
            {
                YOffsetInPixel = -50,
                Projection = new Projection(3857),
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightBlue, GeoColors.Red)
            };

            MapView.AdornmentOverlay.Layers.Add(_scaleLineAdornmentLayer);
            MapView.AdornmentOverlay.Layers.Add(_scaleBarAdornmentLayer);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;

            _initialized = true;
            _ = MapView.RefreshAsync();
        }


        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private void CboLocations_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized)
                return;

            if (!(CboLocations.SelectedItem is AdornmentLocation adornmentLocation)) return;
            _scaleBarAdornmentLayer.Location = adornmentLocation;
            _scaleLineAdornmentLayer.Location = adornmentLocation;

            _ = MapView.AdornmentOverlay.RefreshAsync();
        }

        private void CboUnitSystems_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized)
                return;

            if (!(CboUnitSystems.SelectedItem is ScaleLineUnitSystem unitSystem)) return;

            _scaleLineAdornmentLayer.UnitSystem = unitSystem;
            _ = MapView.AdornmentOverlay.RefreshAsync();
        }

        private void ChkCustomTextStyles_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            _scaleLineAdornmentLayer.AboveLabelTextStyle = new TextStyle(string.Empty, new GeoFont("Arial", 16, DrawingFontStyles.Italic), GeoBrushes.Blue);
            _scaleLineAdornmentLayer.AboveLabelTextStyle.TextPlacement = TextPlacement.Left;
            _scaleLineAdornmentLayer.AboveLabelTextStyle.YOffsetInPixel = -2;


            _scaleLineAdornmentLayer.BelowLabelTextStyle = new TextStyle(string.Empty, new GeoFont("Arial", 16), GeoBrushes.Red);
            _scaleLineAdornmentLayer.BelowLabelTextStyle.TextPlacement = TextPlacement.Left;
            _scaleLineAdornmentLayer.BelowLabelTextStyle.YOffsetInPixel = 2;

            _ = MapView.AdornmentOverlay.RefreshAsync();
        }

        private void ChkCustomTextStyles_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            // set them to null and internally the map will pick up the default styles.
            _scaleLineAdornmentLayer.AboveLabelTextStyle = null;
            _scaleLineAdornmentLayer.BelowLabelTextStyle = null;

            _ = MapView.AdornmentOverlay.RefreshAsync();
        }
    }
}
