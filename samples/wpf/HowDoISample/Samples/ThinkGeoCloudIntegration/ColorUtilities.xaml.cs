using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the ColorCloudClient class to access the ColorUtilities APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ColorUtilities : IDisposable
    {
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
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Set the map's unit of measurement to meters (Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Create a new ShapeFileFeatureLayer using a shapefile containing Frisco Census data
                var housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp");
                housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a new ProjectionConverter to convert between Texas North Central (2276) and Spherical Mercator (3857)
                var projectionConverter = new ProjectionConverter(2276, 3857);
                housingUnitsLayer.FeatureSource.ProjectionConverter = projectionConverter;

                // Create a new overlay and add the census feature layer
                var housingUnitsOverlay = new LayerOverlay();
                housingUnitsOverlay.Layers.Add("Frisco Housing Units", housingUnitsLayer);
                MapView.Overlays.Add("Frisco Housing Units Overlay", housingUnitsOverlay);

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

                MapView.AdornmentOverlay.Layers.Add("Legend", legend);

                // Get the extent of the features from the housing units shapefile, and set the map extent.
                housingUnitsLayer.Open();
                var housingUnitsLayerBBox = housingUnitsLayer.GetBoundingBox();
                MapView.CenterPoint = housingUnitsLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, housingUnitsLayerBBox, MapView.MapWidth, MapView.MapHeight);
                await MapView.ZoomOutAsync();
                housingUnitsLayer.Close();

                // Initialize the ColorCloudClient using our ThinkGeo Cloud credentials
                _colorCloudClient = new ColorCloudClient
                {
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                // Set the initial color scheme for the housing units layer
                var colors = await GetColorsFromCloud();
                // If colors were successfully generated, update the map
                if (colors.Count > 0)
                {
                    await UpdateHousingUnitsLayerColorsAsync(colors);
                }

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Make a request to the ThinkGeo Cloud for a new set of colors
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsFromCloud()
        {
            // Set the number of colors we want to generate
            const int numberOfColors = 6;

            // Create a new collection to hold the colors generated
            var colors = new Collection<GeoColor>();

            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            // Generate colors based on the selected 'color type'
            switch ((CboColorType.SelectedItem as ComboBoxItem)?.Content.ToString())
            {
                case "Hue":
                    // Get a family of colors with the same hue and sequential variances in lightness and saturation
                    colors = await GetColorsByHue(numberOfColors);
                    break;
                case "Analogous":
                    // Get a family of colors based on analogous hues
                    colors = await GetAnalogousColors(numberOfColors);
                    break;
                case "Complementary":
                    // Get a family of colors based on complementary hues
                    colors = await GetComplementaryColors(numberOfColors);
                    break;
                case "Contrasting":
                    // Get a family of colors based on contrasting hues
                    colors = await GetContrastingColors(numberOfColors);
                    break;
                case "Quality":
                    // Get a family of colors with qualitative variances in hue, but similar lightness and saturation
                    colors = await GetQualityColors(numberOfColors);
                    break;
                case "Tetrad":
                    // Get a family of colors based on a harmonious tetrad of hues
                    colors = await GetTetradColors(numberOfColors);
                    break;
                case "Triad":
                    // Get a family of colors based on a harmonious tried of hues
                    colors = await GetTriadColors(numberOfColors);
                    break;
            }

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            return colors;
        }

        /// <summary>
        /// Update the colors for the housing units layers
        /// </summary>
        private async Task UpdateHousingUnitsLayerColorsAsync(IReadOnlyList<GeoColor> colors)
        {
            // Get the housing units layer from the MapView
            var housingUnitsOverlay = (LayerOverlay)MapView.Overlays["Frisco Housing Units Overlay"];
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
            var legend = (LegendAdornmentLayer)MapView.AdornmentOverlay.Layers["Legend"];

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

            await MapView.AdornmentOverlay.RefreshAsync();
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
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Get a family of colors with the same hue and sequential variances in lightness and saturation
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsByHue(int numberOfColors)
        {
            var hueColors = new Collection<GeoColor>();

            // Generate colors based on the parameters selected in the UI
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked != null && (bool)btn.IsChecked)?.Name)
            {
                case "RdoRandomColor":
                    // Use a random base color
                    hueColors = await _colorCloudClient.GetColorsInHueFamilyAsync(numberOfColors);
                    break;
                case "RdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    var colorFromInputString = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        hueColors = await _colorCloudClient.GetColorsInHueFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "RdoDefaultColor":
                    // Use a default color for the base color
                    hueColors = await _colorCloudClient.GetColorsInHueFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
            }

            return hueColors;
        }

        /// <summary>
        /// Get a family of colors based on analogous hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetAnalogousColors(int numberOfColors)
        {
            var analogousColors = new Collection<GeoColor>();
            var colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked != null && (bool)btn.IsChecked)?.Name)
            {
                case "RdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await _colorCloudClient.GetColorsInAnalogousFamilyAsync(numberOfColors);
                    break;
                case "RdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    var colorFromInputString = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await _colorCloudClient.GetColorsInAnalogousFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "RdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await _colorCloudClient.GetColorsInAnalogousFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all the colors generated
            foreach (var color in colorsDictionary.Values.SelectMany(colors => colors))
            {
                analogousColors.Add(color);
            }

            return analogousColors;
        }

        /// <summary>
        /// Get a family of colors based on complementary hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetComplementaryColors(int numberOfColors)
        {
            var complementaryColors = new Collection<GeoColor>();
            var colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked != null && (bool)btn.IsChecked)?.Name)
            {
                case "RdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await _colorCloudClient.GetColorsInComplementaryFamilyAsync(numberOfColors);
                    break;
                case "RdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    var colorFromInputString = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await _colorCloudClient.GetColorsInComplementaryFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "RdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await _colorCloudClient.GetColorsInComplementaryFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all the colors generated
            foreach (var color in colorsDictionary.Values.SelectMany(colors => colors))
            {
                complementaryColors.Add(color);
            }

            return complementaryColors;
        }

        /// <summary>
        /// Get a family of colors based on contrasting hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetContrastingColors(int numberOfColors)
        {
            var contrastingColors = new Collection<GeoColor>();
            var colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked != null && (bool)btn.IsChecked)?.Name)
            {
                case "RdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await _colorCloudClient.GetColorsInContrastingFamilyAsync(numberOfColors);
                    break;
                case "RdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    var colorFromInputString = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await _colorCloudClient.GetColorsInContrastingFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "RdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await _colorCloudClient.GetColorsInContrastingFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all the colors generated
            foreach (var color in colorsDictionary.Values.SelectMany(colors => colors))
            {
                contrastingColors.Add(color);
            }

            return contrastingColors;
        }

        /// <summary>
        /// Get a family of colors with qualitative variances in hue, but similar lightness and saturation
        /// </summary>
        private async Task<Collection<GeoColor>> GetQualityColors(int numberOfColors)
        {
            var qualityColors = new Collection<GeoColor>();

            // Generate colors based on the parameters selected in the UI
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked != null && (bool)btn.IsChecked)?.Name)
            {
                case "RdoRandomColor":
                    // Use a random base color
                    qualityColors = await _colorCloudClient.GetColorsInQualityFamilyAsync(numberOfColors);
                    break;
                case "RdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    var colorFromInputString = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        qualityColors = await _colorCloudClient.GetColorsInQualityFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "RdoDefaultColor":
                    // Use a default color for the base color
                    qualityColors = await _colorCloudClient.GetColorsInQualityFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
            }

            return qualityColors;
        }

        /// <summary>
        /// Get a family of colors based on a harmonious tetrad of hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetTetradColors(int numberOfColors)
        {
            var tetradColors = new Collection<GeoColor>();
            var colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked != null && (bool)btn.IsChecked)?.Name)
            {
                case "RdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await _colorCloudClient.GetColorsInTetradFamilyAsync(numberOfColors);
                    break;
                case "RdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    var colorFromInputString = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await _colorCloudClient.GetColorsInTetradFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "RdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await _colorCloudClient.GetColorsInTetradFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all the colors generated
            foreach (var color in colorsDictionary.Values.SelectMany(colors => colors))
            {
                tetradColors.Add(color);
            }

            return tetradColors;
        }

        /// <summary>
        /// Get a family of colors based on a harmonious tried of hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetTriadColors(int numberOfColors)
        {
            var triadColors = new Collection<GeoColor>();
            var colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (_baseColorRadioButtons.FirstOrDefault(btn => btn.IsChecked != null && (bool)btn.IsChecked)?.Name)
            {
                case "RdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await _colorCloudClient.GetColorsInTriadFamilyAsync(numberOfColors);
                    break;
                case "RdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    var colorFromInputString = GetGeoColorFromString(TxtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await _colorCloudClient.GetColorsInTriadFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "RdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await _colorCloudClient.GetColorsInTriadFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all the colors generated
            foreach (var color in colorsDictionary.Values.SelectMany(colors => colors))
            {
                triadColors.Add(color);
            }

            return triadColors;
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

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}