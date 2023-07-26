using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the ColorCloudClient class to access the ColorUtilities APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ColorUtilitiesCloudServicesSample : UserControl, IDisposable
    {
        private ColorCloudClient colorCloudClient;
        private Collection<RadioButton> baseColorRadioButtons;

        public ColorUtilitiesCloudServicesSample()
        {
            InitializeComponent();

            // Group the 'Base Color' radio buttons in a collection for easier iteration
            baseColorRadioButtons = new Collection<RadioButton>() { rdoDefaultColor, rdoRandomColor, rdoSpecificColor };
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco housing units data
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new ShapeFileFeatureLayer using a shapefile containing Frisco Census data
            ShapeFileFeatureLayer housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp");
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new ProjectionConverter to convert between Texas North Central (2276) and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            housingUnitsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Create a new overlay and add the census feature layer
            LayerOverlay housingUnitsOverlay = new LayerOverlay();
            housingUnitsOverlay.Layers.Add("Frisco Housing Units", housingUnitsLayer);
            mapView.Overlays.Add("Frisco Housing Units Overlay", housingUnitsOverlay);

            // Create a legend adornment to display classbreaks
            LegendAdornmentLayer legend = new LegendAdornmentLayer();

            // Set up the legend adornment
            legend.Title = new LegendItem()
            {
                TextStyle = new TextStyle("Housing Unit Counts", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            };
            legend.Location = AdornmentLocation.LowerRight;
            mapView.AdornmentOverlay.Layers.Add("Legend", legend);

            // Get the exttent of the features from the housing units shapefile, and set the map extent.
            housingUnitsLayer.Open();
            mapView.CurrentExtent = housingUnitsLayer.GetBoundingBox();
            mapView.ZoomOut();
            housingUnitsLayer.Close();

            // Initialize the ColorCloudClient using our ThinkGeo Cloud credentials
            colorCloudClient = new ColorCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Set the initial color scheme for the housing units layer
            Collection<GeoColor> colors = await GetColorsFromCloud();
            // If colors were successfully generated, update the map
            if (colors.Count > 0)
            {
                UpdateHousingUnitsLayerColors(colors);
            }

            mapView.Refresh();
        }

        /// <summary>
        /// Make a request to the ThinkGeo Cloud for a new set of colors
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsFromCloud()
        {
            // Set the number of colors we want to generate
            int numberOfColors = 6;

            // Create a new collection to hold the colors generated
            Collection<GeoColor> colors = new Collection<GeoColor>();

            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            // Generate colors based on the selected 'color type'
            switch ((cboColorType.SelectedItem as ComboBoxItem).Content.ToString())
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
                default:
                    break;
            }

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

            return colors;
        }

        /// <summary>
        /// Update the colors for the housing units layers
        /// </summary>
        private void UpdateHousingUnitsLayerColors(Collection<GeoColor> colors)
        {
            // Get the housing units layer from the MapView
            LayerOverlay housingUnitsOverlay = (LayerOverlay)mapView.Overlays["Frisco Housing Units Overlay"];
            ShapeFileFeatureLayer housingUnitsLayer = (ShapeFileFeatureLayer)housingUnitsOverlay.Layers["Frisco Housing Units"];

            // Clear the previous style from the housing units layer
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();

            // Create a new ClassBreakStyle to showcase the color family generated
            ClassBreakStyle classBreakStyle = new ClassBreakStyle();
            Collection<ClassBreak> classBreaks = new Collection<ClassBreak>();

            // Different features will be styled differently based on the 'H_UNITS' attribute of the features
            classBreakStyle.ColumnName = "H_UNITS";
            double[] classBreaksIntervals = new double[] { 0, 1000, 2000, 3000, 4000, 5000 };
            for(int i = 0; i < colors.Count; i++)
            { 
                // Create a differently colored area style for housing units counts of 0, 1000, 2000, etc
                AreaStyle areaStyle = new AreaStyle(new GeoSolidBrush(colors[colors.Count - i - 1]));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(classBreaksIntervals[i], areaStyle));
                classBreaks.Add(new ClassBreak(classBreaksIntervals[i], areaStyle));
            }

            // Add the ClassBreakStyle to the housing units layer
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            GenerateNewLegendItems(classBreaks);

            // Refresh the overlay to redraw the features
            housingUnitsOverlay.Refresh();
        }

        private void GenerateNewLegendItems(Collection<ClassBreak> classBreaks)
        {
            // Clear the previous legend adornment
            LegendAdornmentLayer legend = (LegendAdornmentLayer)mapView.AdornmentOverlay.Layers["Legend"];

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

            mapView.AdornmentOverlay.Refresh();
        }

        /// <summary>
        /// Use the ColorCloudClient APIs to generate a set of colors based on the input parameters, and apply the new color scheme to a feature layer
        /// </summary>
        private async void GenerateColors_Click(object sender, RoutedEventArgs e)
        {
            // Get a new set of colors from the ThinkGeo Cloud
            Collection<GeoColor> colors = await GetColorsFromCloud();

            // If colors were successfully generated, update the map
            if (colors.Count > 0)
            {
                UpdateHousingUnitsLayerColors(colors);
            }
        }

        /// <summary>
        /// Get a family of colors with the same hue and sequential variances in lightness and saturation
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsByHue(int numberOfColors)
        {
            Collection<GeoColor> hueColors = new Collection<GeoColor>();

            // Generate colors based on the parameters selected in the UI
            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    // Use a random base color
                    hueColors = await colorCloudClient.GetColorsInHueFamilyAsync(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        hueColors = await colorCloudClient.GetColorsInHueFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    // Use a default color for the base color
                    hueColors = await colorCloudClient.GetColorsInHueFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            return hueColors;
        }

        /// <summary>
        /// Get a family of colors based on analogous hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetAnalogousColors(int numberOfColors)
        {
            Collection<GeoColor> analogousColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await colorCloudClient.GetColorsInAnalogousFamilyAsync(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await colorCloudClient.GetColorsInAnalogousFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await colorCloudClient.GetColorsInAnalogousFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all of the colors generated
            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    analogousColors.Add(color);
                }
            }

            return analogousColors;
        }

        /// <summary>
        /// Get a family of colors based on complementary hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetComplementaryColors(int numberOfColors)
        {
            Collection<GeoColor> complementaryColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await colorCloudClient.GetColorsInComplementaryFamilyAsync(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await colorCloudClient.GetColorsInComplementaryFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await colorCloudClient.GetColorsInComplementaryFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all of the colors generated
            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    complementaryColors.Add(color);
                }
            }

            return complementaryColors;
        }

        /// <summary>
        /// Get a family of colors based on contrasting hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetContrastingColors(int numberOfColors)
        {
            Collection<GeoColor> contrastingColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await colorCloudClient.GetColorsInContrastingFamilyAsync(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await colorCloudClient.GetColorsInContrastingFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await colorCloudClient.GetColorsInContrastingFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all of the colors generated
            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    contrastingColors.Add(color);
                }
            }

            return contrastingColors;
        }

        /// <summary>
        /// Get a family of colors with qualitative variances in hue, but similar lightness and saturation
        /// </summary>
        private async Task<Collection<GeoColor>> GetQualityColors(int numberOfColors)
        {
            Collection<GeoColor> qualityColors = new Collection<GeoColor>();

            // Generate colors based on the parameters selected in the UI
            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    // Use a random base color
                    qualityColors = await colorCloudClient.GetColorsInQualityFamilyAsync(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        qualityColors = await colorCloudClient.GetColorsInQualityFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    // Use a default color for the base color
                    qualityColors = await colorCloudClient.GetColorsInQualityFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            return qualityColors;
        }

        /// <summary>
        /// Get a family of colors based on a harmonious tetrad of hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetTetradColors(int numberOfColors)
        {
            Collection<GeoColor> tetradColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await colorCloudClient.GetColorsInTetradFamilyAsync(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await colorCloudClient.GetColorsInTetradFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await colorCloudClient.GetColorsInTetradFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all of the colors generated
            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    tetradColors.Add(color);
                }
            }

            return tetradColors;
        }

        /// <summary>
        /// Get a family of colors based on a harmonious tried of hues
        /// </summary>
        private async Task<Collection<GeoColor>> GetTriadColors(int numberOfColors)
        {
            Collection<GeoColor> triadColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            // Generate colors based on the parameters selected in the UI
            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    // Use a random base color
                    colorsDictionary = await colorCloudClient.GetColorsInTriadFamilyAsync(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    // Use the HTML color code specified for the base color
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = await colorCloudClient.GetColorsInTriadFamilyAsync(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    // Use a default color for the base color
                    colorsDictionary = await colorCloudClient.GetColorsInTriadFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            // Some color generation APIs use multiple base colors based on the original input color
            // These APIs return a dictionary where the 'keys' are the base colors and the 'values' are the colors generated from that base
            // For this sample we will simply utilize all of the colors generated
            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    triadColors.Add(color);
                }
            }

            return triadColors;
        }

        /// <summary>
        /// Helper function using the GeoColor API to generate a color from an HTML string
        /// </summary>
        private GeoColor GetGeoColorFromString(string htmlColorString)
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
            GeoColor color = GeoColors.White;
            var selectedColorItem = cboDefaultColor.SelectedItem as ComboBoxItem;

            switch (selectedColorItem.Content.ToString())
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
                default:
                    break;
            }

            return color;
        }

        /// <summary>
        /// Helper function to change the tip shown for different Color Types
        /// </summary>
        private void cboColorType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxContent = (cboColorType.SelectedItem as ComboBoxItem).Content;

            if (comboBoxContent != null)
            {
                switch (comboBoxContent.ToString())
                {
                    case "Hue":
                        txtColorCategoryDescription.Text = "Get a family of colors with the same hue and sequential variances in lightness and saturation";
                        break;
                    case "Analogous":
                        txtColorCategoryDescription.Text = "Get a family of colors based on analogous hues";
                        break;
                    case "Complementary":
                        txtColorCategoryDescription.Text = "Get a family of colors based on complementary hues";
                        break;
                    case "Contrasting":
                        txtColorCategoryDescription.Text = "Get a family of colors based on contrasting hues";
                        break;
                    case "Quality":
                        txtColorCategoryDescription.Text = "Get a family of colors with qualitative variances in hue, but similar lightness and saturation";
                        break;
                    case "Tetrad":
                        txtColorCategoryDescription.Text = "Get a family of colors based on a harmonious tetrad of hues";
                        break;
                    case "Triad":
                        txtColorCategoryDescription.Text = "Get a family of colors based on a harmonious tried of hues";
                        break;
                    default:
                        break;
                }
            }
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

    }
}
