using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ColorUtilitiesCloudServicesSample : UserControl
    {
        private ColorCloudClient colorCloudClient;
        private Collection<RadioButton> baseColorRadioButtons;

        public ColorUtilitiesCloudServicesSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new ShapeFileFeatureLayer using a shapefile containing Frisco Census data
            ShapeFileFeatureLayer housingUnitsLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Frisco 2010 Census Housing Units.shp");
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
            // loadingImage.Visibility = Visibility.Visible;
            // Generate colors based on the selected 'color type'
            switch (cboColorType.SelectedItem.ToString())
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
            //loadingImage.Visibility = Visibility.Hidden;

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
            for (int i = 0; i < colors.Count; i++)
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
        /// Get a family of colors with the same hue and sequential variances in lightness and saturation
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsByHue(int numberOfColors)
        {
            Collection<GeoColor> hueColors = new Collection<GeoColor>();

            // Generate colors based on the parameters selected in the UI
            if (rdoRandomColor.Checked)
            {
                // Use a random base color
                hueColors = await colorCloudClient.GetColorsInHueFamilyAsync(numberOfColors);
            }
            else if (rdoSpecificColor.Checked)
            {
                // Use the HTML color code specified for the base color
                GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                if (colorFromInputString != null)
                {
                    hueColors = await colorCloudClient.GetColorsInHueFamilyAsync(colorFromInputString, numberOfColors);
                }
            }
            else if (rdoDefaultColor.Checked)
            {
                // Use a default color for the base color
                hueColors = await colorCloudClient.GetColorsInHueFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);

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
            if (rdoRandomColor.Checked)
            {
                // Use a random base color
                colorsDictionary = await colorCloudClient.GetColorsInAnalogousFamilyAsync(numberOfColors);
            }
            else if (rdoSpecificColor.Checked)
            {
                // Use the HTML color code specified for the base color
                GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                if (colorFromInputString != null)
                {
                    colorsDictionary = await colorCloudClient.GetColorsInAnalogousFamilyAsync(colorFromInputString, numberOfColors);
                }
            }
            else if (rdoDefaultColor.Checked)
            {
                // Use a default color for the base color
                colorsDictionary = await colorCloudClient.GetColorsInAnalogousFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
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
            if (rdoRandomColor.Checked)
            {
                // Use a random base color
                colorsDictionary = await colorCloudClient.GetColorsInComplementaryFamilyAsync(numberOfColors);
            }
            else if (rdoSpecificColor.Checked)
            {
                // Use the HTML color code specified for the base color
                GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                if (colorFromInputString != null)
                {
                    colorsDictionary = await colorCloudClient.GetColorsInComplementaryFamilyAsync(colorFromInputString, numberOfColors);
                }
            }
            else if (rdoDefaultColor.Checked)
            {
                // Use a default color for the base color
                colorsDictionary = await colorCloudClient.GetColorsInComplementaryFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
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
            if (rdoRandomColor.Checked)
            {
                // Use a random base color
                colorsDictionary = await colorCloudClient.GetColorsInContrastingFamilyAsync(numberOfColors);
            }
            else if (rdoSpecificColor.Checked)
            {
                // Use the HTML color code specified for the base color
                GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                if (colorFromInputString != null)
                {
                    colorsDictionary = await colorCloudClient.GetColorsInContrastingFamilyAsync(colorFromInputString, numberOfColors);
                }
            }
            else if (rdoDefaultColor.Checked)
            {
                // Use a default color for the base color
                colorsDictionary = await colorCloudClient.GetColorsInContrastingFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
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
            if (rdoRandomColor.Checked)
            {
                // Use a random base color
                qualityColors = await colorCloudClient.GetColorsInQualityFamilyAsync(numberOfColors);
            }
            else if (rdoSpecificColor.Checked)
            {
                // Use the HTML color code specified for the base color
                GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                if (colorFromInputString != null)
                {
                    qualityColors = await colorCloudClient.GetColorsInQualityFamilyAsync(colorFromInputString, numberOfColors);
                }
            }
            else if (rdoDefaultColor.Checked)
            {
                // Use a default color for the base color
                qualityColors = await colorCloudClient.GetColorsInQualityFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
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
            if (rdoRandomColor.Checked)
            {
                // Use a random base color
                colorsDictionary = await colorCloudClient.GetColorsInTetradFamilyAsync(numberOfColors);
            }
            else if (rdoSpecificColor.Checked)
            {
                // Use the HTML color code specified for the base color
                GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                if (colorFromInputString != null)
                {
                    colorsDictionary = await colorCloudClient.GetColorsInTetradFamilyAsync(colorFromInputString, numberOfColors);
                }
            }
            else if (rdoDefaultColor.Checked)
            {
                // Use a default color for the base color
                colorsDictionary = await colorCloudClient.GetColorsInTetradFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
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
            if (rdoRandomColor.Checked)
            {
                // Use a random base color
                colorsDictionary = await colorCloudClient.GetColorsInTriadFamilyAsync(numberOfColors);
            }
            else if (rdoSpecificColor.Checked)
            {
                // Use the HTML color code specified for the base color
                GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                if (colorFromInputString != null)
                {
                    colorsDictionary = await colorCloudClient.GetColorsInTriadFamilyAsync(colorFromInputString, numberOfColors);
                }
            }
            else if (rdoDefaultColor.Checked)
            {
                // Use a default color for the base color
                colorsDictionary = await colorCloudClient.GetColorsInTriadFamilyAsync(GetGeoColorFromDefaultColors(), numberOfColors);
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
            var selectedColorItem = cboDefaultColor.SelectedItem as string;

            switch (selectedColorItem)
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



        private Panel panel1;
        private RadioButton rdoRandomColor;
        private Label label4;
        private Label txtColorCategoryDescription;
        private ComboBox cboColorType;
        private Label label2;
        private Label label1;
        private Button generate;
        private ComboBox cboDefaultColor;
        private TextBox txtSpecificColor;
        private RadioButton rdoDefaultColor;
        private RadioButton rdoSpecificColor;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.generate = new System.Windows.Forms.Button();
            this.cboDefaultColor = new System.Windows.Forms.ComboBox();
            this.txtSpecificColor = new System.Windows.Forms.TextBox();
            this.rdoDefaultColor = new System.Windows.Forms.RadioButton();
            this.rdoSpecificColor = new System.Windows.Forms.RadioButton();
            this.rdoRandomColor = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtColorCategoryDescription = new System.Windows.Forms.Label();
            this.cboColorType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(904, 645);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.generate);
            this.panel1.Controls.Add(this.cboDefaultColor);
            this.panel1.Controls.Add(this.txtSpecificColor);
            this.panel1.Controls.Add(this.rdoDefaultColor);
            this.panel1.Controls.Add(this.rdoSpecificColor);
            this.panel1.Controls.Add(this.rdoRandomColor);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtColorCategoryDescription);
            this.panel1.Controls.Add(this.cboColorType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(908, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 645);
            this.panel1.TabIndex = 1;
            // 
            // generate
            // 
            this.generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.generate.Location = new System.Drawing.Point(2, 318);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(312, 42);
            this.generate.TabIndex = 10;
            this.generate.Text = "Generate";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // cboDefaultColor
            // 
            this.cboDefaultColor.FormattingEnabled = true;
            this.cboDefaultColor.Items.AddRange(new object[] {
            "Red",
            "Orange",
            "Yellow",
            "Green",
            "Blue",
            "Purple"});
            this.cboDefaultColor.Location = new System.Drawing.Point(174, 288);
            this.cboDefaultColor.Name = "cboDefaultColor";
            this.cboDefaultColor.Size = new System.Drawing.Size(121, 24);
            this.cboDefaultColor.TabIndex = 9;
            this.cboDefaultColor.Text = "Red";
            // 
            // txtSpecificColor
            // 
            this.txtSpecificColor.Location = new System.Drawing.Point(174, 261);
            this.txtSpecificColor.Name = "txtSpecificColor";
            this.txtSpecificColor.Size = new System.Drawing.Size(113, 22);
            this.txtSpecificColor.TabIndex = 8;
            this.txtSpecificColor.Text = "#AB2567";
            // 
            // rdoDefaultColor
            // 
            this.rdoDefaultColor.AutoSize = true;
            this.rdoDefaultColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoDefaultColor.ForeColor = System.Drawing.Color.White;
            this.rdoDefaultColor.Location = new System.Drawing.Point(17, 286);
            this.rdoDefaultColor.Name = "rdoDefaultColor";
            this.rdoDefaultColor.Size = new System.Drawing.Size(108, 21);
            this.rdoDefaultColor.TabIndex = 7;
            this.rdoDefaultColor.TabStop = true;
            this.rdoDefaultColor.Text = "Default Color";
            this.rdoDefaultColor.UseVisualStyleBackColor = true;
            // 
            // rdoSpecificColor
            // 
            this.rdoSpecificColor.AutoSize = true;
            this.rdoSpecificColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSpecificColor.ForeColor = System.Drawing.Color.White;
            this.rdoSpecificColor.Location = new System.Drawing.Point(17, 259);
            this.rdoSpecificColor.Name = "rdoSpecificColor";
            this.rdoSpecificColor.Size = new System.Drawing.Size(112, 21);
            this.rdoSpecificColor.TabIndex = 6;
            this.rdoSpecificColor.TabStop = true;
            this.rdoSpecificColor.Text = "Specific Color";
            this.rdoSpecificColor.UseVisualStyleBackColor = true;
            // 
            // rdoRandomColor
            // 
            this.rdoRandomColor.AutoSize = true;
            this.rdoRandomColor.Checked = true;
            this.rdoRandomColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoRandomColor.ForeColor = System.Drawing.Color.White;
            this.rdoRandomColor.Location = new System.Drawing.Point(17, 232);
            this.rdoRandomColor.Name = "rdoRandomColor";
            this.rdoRandomColor.Size = new System.Drawing.Size(116, 21);
            this.rdoRandomColor.TabIndex = 5;
            this.rdoRandomColor.TabStop = true;
            this.rdoRandomColor.Text = "Random Color";
            this.rdoRandomColor.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Base Color";
            // 
            // txtColorCategoryDescription
            // 
            this.txtColorCategoryDescription.AutoSize = true;
            this.txtColorCategoryDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColorCategoryDescription.ForeColor = System.Drawing.Color.White;
            this.txtColorCategoryDescription.Location = new System.Drawing.Point(10, 129);
            this.txtColorCategoryDescription.Name = "txtColorCategoryDescription";
            this.txtColorCategoryDescription.Size = new System.Drawing.Size(259, 51);
            this.txtColorCategoryDescription.TabIndex = 3;
            this.txtColorCategoryDescription.Text = "Get a family of colors with the same hue\r\nand sequential variances in lightness\r\n" +
    "and saturation";
            // 
            // cboColorType
            // 
            this.cboColorType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboColorType.FormattingEnabled = true;
            this.cboColorType.Items.AddRange(new object[] {
            "Hue",
            "Analogous",
            "Complementary",
            "Contrasting",
            "Quality",
            "Tetrad",
            "Triad"});
            this.cboColorType.Location = new System.Drawing.Point(149, 81);
            this.cboColorType.Name = "cboColorType";
            this.cboColorType.Size = new System.Drawing.Size(121, 24);
            this.cboColorType.TabIndex = 2;
            this.cboColorType.Text = "Hue";
            this.cboColorType.SelectedIndexChanged += new System.EventHandler(this.cboColorType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Category:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Generate and Apply a New\r\nColor Group";
            // 
            // ColorUtilitiesCloudServicesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ColorUtilitiesCloudServicesSample";
            this.Size = new System.Drawing.Size(1226, 645);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }




        #endregion Component Designer generated code

        private async void generate_Click(object sender, EventArgs e)
        {
            // Get a new set of colors from the ThinkGeo Cloud
            Collection<GeoColor> colors = await GetColorsFromCloud();

            // If colors were successfully generated, update the map
            if (colors.Count > 0)
            {
                UpdateHousingUnitsLayerColors(colors);
            }
        }

        private void cboColorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxContent = (cboColorType.SelectedItem as string);

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
    }
}