using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the ColorCloudClient class to access the ColorUtilities APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ColorUtilities
    {

        private bool _initialized;
        private ColorCloudClient _colorCloudClient;
        private readonly Collection<RadioButton> _baseColorRadioButtons;

        public ColorUtilities()
        {
            InitializeComponent();

            // Group the 'Base Color' radio buttons in a collection for easier iteration
            _baseColorRadioButtons = new Collection<RadioButton>() { RdoDefaultColor, RdoRandomColor, RdoSpecificColor };
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco housing units data
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            try
            {
                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));
                Map.Basemap = basemap;

                Map.MapUnit = GeographyUnit.Meter;

                // Create a new ShapeFileFeatureLayer using a shapefile containing Frisco Census data
                var housingUnitsLayer = new ShapeFileFeatureLayer(SampleShared.Shapefile("Frisco 2010 Census Housing Units.shp"));
                housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a new ProjectionConverter to convert between Texas North Central (2276) and Spherical Mercator (3857)
                var projectionConverter = new ProjectionConverter(2276, 3857);
                housingUnitsLayer.FeatureSource.ProjectionConverter = projectionConverter;

                // Create a new overlay and add the census feature layer
                var housingUnitsOverlay = new LayerOverlay();
                housingUnitsOverlay.Layers.Add("Frisco Housing Units", housingUnitsLayer);
                Map.Overlays.Add("Frisco Housing Units Overlay", housingUnitsOverlay);

                // Create a legend adornment to display class breaks
                var legend = new LegendAdornmentLayer
                {
                    // Set up the legend adornment
                    Title = new LegendItem()
                    {
                        TextStyle = new TextStyle("Housing Unit Counts", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                    },
                    Location = AdornmentLocation.LowerRight
                };

                Map.AdornmentOverlay.Layers.Add("Legend", legend);

                // Get the extent of the features from the housing units shapefile, and set the map extent.
                housingUnitsLayer.Open();
                var housingUnitsLayerBBox = housingUnitsLayer.GetBoundingBox();
                Map.CenterPoint = housingUnitsLayerBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, housingUnitsLayerBBox, Map.MapWidth, Map.MapHeight);
                await Map.ZoomOutAsync();
                housingUnitsLayer.Close();

                // Initialize the ColorCloudClient using our ThinkGeo Cloud credentials
                _colorCloudClient = new ColorCloudClient
                {
                    ClientId = SampleShared.ClientId,
                    ClientSecret = SampleShared.ClientSecret,
                };

                // Set the initial color scheme for the housing units layer
                var colors = await GetColorsFromCloud();
                // If colors were successfully generated, update the map
                if (colors.Count > 0)
                {
                    await UpdateHousingUnitsLayerColorsAsync(colors);
                }

                await Map.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Make a request to the ThinkGeo Cloud for a new set of colors
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsFromCloud()
        {
            // Set the number of colors we want to generate
            const int numberOfColors = 6;

            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            // Generate colors based on the selected 'color type'
            var colors = await GetColors((CboColorType.SelectedItem as ComboBoxItem)?.Content.ToString(), numberOfColors);

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            return colors;
        }

        /// <summary>
        /// Update the colors for the housing units layers
        /// </summary>
        private async Task UpdateHousingUnitsLayerColorsAsync(IReadOnlyList<GeoColor> colors)
        {
            // Get the housing units layer from the Map
            var housingUnitsOverlay = (LayerOverlay)Map.Overlays["Frisco Housing Units Overlay"];
            var housingUnitsLayer = (ShapeFileFeatureLayer)housingUnitsOverlay.Layers["Frisco Housing Units"];

            // Clear the previous style from the housing units layer
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();

            // Create a new ClassBreakStyle to showcase the color family generated
            var classBreakStyle = new ClassBreakStyle();
            var classBreaks = new Collection<ClassBreak>();

            // Different features will be styled differently based on the 'H_UNITS' attribute of the features
            classBreakStyle.ColumnName = "H_UNITS";
            var classBreaksIntervals = new double[] { 0, 1000, 2000, 3000, 4000, 5000 };
            for (var i = 0; i < colors.Count; i++)
            {
                // Create a differently colored area style for housing units counts of 0, 1000, 2000, etc
                var areaStyle = new AreaStyle(new GeoSolidBrush(colors[colors.Count - i - 1]));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(classBreaksIntervals[i], areaStyle));
                classBreaks.Add(new ClassBreak(classBreaksIntervals[i], areaStyle));
            }

            // Add the ClassBreakStyle to the housing units layer
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            await GenerateNewLegendItemsAsync(classBreaks);

            // Refresh the overlay to redraw the features
            await housingUnitsOverlay.RefreshAsync();
        }

        private async Task GenerateNewLegendItemsAsync(Collection<ClassBreak> classBreaks)
        {
            // Clear the previous legend adornment
            var legend = (LegendAdornmentLayer)Map.AdornmentOverlay.Layers["Legend"];

            legend.LegendItems.Clear();
            // Add a LegendItems to the legend adornment for each ClassBreak
            foreach (var classBreak in classBreaks)
            {
                var legendItem = new LegendItem()
                {
                    ImageStyle = classBreak.DefaultAreaStyle,
                    TextStyle = new TextStyle($@">{classBreak.Value} units", new GeoFont("Verdana", 10), GeoBrushes.Black)
                };
                legend.LegendItems.Add(legendItem);
            }

            await Map.AdornmentOverlay.RefreshAsync();
        }

        /// <summary>
        /// Use the ColorCloudClient APIs to generate a set of colors based on the input parameters, and apply the new color scheme to a feature layer
        /// </summary>
        private async void GenerateColors_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get a new set of colors from the ThinkGeo Cloud
                var colors = await GetColorsFromCloud();

                // If colors were successfully generated, update the map
                if (colors.Count > 0)
                {
                    await UpdateHousingUnitsLayerColorsAsync(colors);
                }
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Ask the ThinkGeo Cloud for a family of colors. Hue and Quality answer one family;
        /// the other families come back as one list per base color the service chose, and
        /// this sample uses all of them.
        /// </summary>
        private async Task<Collection<GeoColor>> GetColors(string family, int numberOfColors)
        {
            // The base color: random, the HTML color code typed in, or one of the defaults
            GeoColor baseColor = null;
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked == true)?.Name)
            {
                case "RdoSpecificColor":
                    baseColor = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (baseColor == null) return new Collection<GeoColor>();
                    break;
                case "RdoDefaultColor":
                    baseColor = GetGeoColorFromDefaultColors();
                    break;
            }

            Dictionary<GeoColor, Collection<GeoColor>> families;
            switch (family)
            {
                case "Hue":
                    return baseColor == null
                        ? await _colorCloudClient.GetColorsInHueFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInHueFamilyAsync(baseColor, numberOfColors);
                case "Quality":
                    return baseColor == null
                        ? await _colorCloudClient.GetColorsInQualityFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInQualityFamilyAsync(baseColor, numberOfColors);
                case "Analogous":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInAnalogousFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInAnalogousFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Complementary":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInComplementaryFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInComplementaryFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Contrasting":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInContrastingFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInContrastingFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Tetrad":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInTetradFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInTetradFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Triad":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInTriadFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInTriadFamilyAsync(baseColor, numberOfColors);
                    break;
                default:
                    return new Collection<GeoColor>();
            }

            return new Collection<GeoColor>(families.Values.SelectMany(colors => colors).ToList());
        }

        /// <summary>
        /// Helper function using the GeoColor API to generate a color from an HTML string
        /// </summary>
        private static GeoColor GetGeoColorFromString(string htmlColorString)
        {
            GeoColor color = null;

            try
            {
                // Get a GeoColor based on an HTML color code
                color = GeoColor.FromHtml(htmlColorString);
            }
            catch
            {
                MessageBox.Show("Invalid HTML color string", "Error");
            }

            return color;
        }

        /// <summary>
        /// Helper function to get a GeoColor based on the selected Default Color in the UI
        /// </summary>
        private GeoColor GetGeoColorFromDefaultColors()
        {
            var color = GeoColors.White;
            var selectedColorItem = CboDefaultColor.SelectedItem as ComboBoxItem;

            switch (selectedColorItem?.Content.ToString())
            {
                case "Red":
                    color = GeoColors.Red;
                    break;
                case "Orange":
                    color = GeoColors.Orange;
                    break;
                case "Yellow":
                    color = GeoColors.Yellow;
                    break;
                case "Green":
                    color = GeoColors.Green;
                    break;
                case "Blue":
                    color = GeoColors.Blue;
                    break;
                case "Purple":
                    color = GeoColors.Purple;
                    break;
            }

            return color;
        }

        /// <summary>
        /// Helper function to change the tip shown for different Color Types
        /// </summary>
        private void CboColorType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxContent = (CboColorType.SelectedItem as ComboBoxItem)?.Content;

            if (comboBoxContent == null) return;
            switch (comboBoxContent.ToString())
            {
                case "Hue":
                    TxtColorCategoryDescription.Text = "Get a family of colors with the same hue and sequential variances in lightness and saturation";
                    break;
                case "Analogous":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on analogous hues";
                    break;
                case "Complementary":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on complementary hues";
                    break;
                case "Contrasting":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on contrasting hues";
                    break;
                case "Quality":
                    TxtColorCategoryDescription.Text = "Get a family of colors with qualitative variances in hue, but similar lightness and saturation";
                    break;
                case "Tetrad":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on a harmonious tetrad of hues";
                    break;
                case "Triad":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on a harmonious tried of hues";
                    break;
            }
        }
    }
}