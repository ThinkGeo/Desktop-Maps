using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ReverseGeocodingCloudServicesSample: UserControl
    {
        public ReverseGeocodingCloudServicesSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // If want to know more srids, please refer Projections.rtf in Documentation folder.
            ProjectionConverter proj4Projection = new ProjectionConverter(3857, 2163);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.FeatureSource.ProjectionConverter = proj4Projection;

            worldLayer.Open();
            mapView.CurrentExtent = worldLayer.GetBoundingBox();
            worldLayer.Close();

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.TileType = TileType.SingleTile;
            staticOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean)));
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);
        }

        private Panel panel1;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox txtSearchResultsBestMatch;
        private Button btnSearch;
        private TextBox txtLocationCategoriesDescription;
        private ComboBox cboLocationCategories;
        private TextBox txtMaxResults;
        private TextBox txtSearchRadius;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox txtCoordinates;
        private Label label2;
        private Label label1;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCoordinates = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearchRadius = new System.Windows.Forms.TextBox();
            this.txtMaxResults = new System.Windows.Forms.TextBox();
            this.cboLocationCategories = new System.Windows.Forms.ComboBox();
            this.txtLocationCategoriesDescription = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchResultsBestMatch = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
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
            this.mapView.Size = new System.Drawing.Size(855, 588);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Controls.Add(this.txtSearchResultsBestMatch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtLocationCategoriesDescription);
            this.panel1.Controls.Add(this.cboLocationCategories);
            this.panel1.Controls.Add(this.txtMaxResults);
            this.panel1.Controls.Add(this.txtSearchRadius);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCoordinates);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(861, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 588);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Click on the Map or enter a Location\r\nto Reverse Geocode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Location";
            // 
            // txtCoordinates
            // 
            this.txtCoordinates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCoordinates.Location = new System.Drawing.Point(3, 89);
            this.txtCoordinates.Name = "txtCoordinates";
            this.txtCoordinates.Size = new System.Drawing.Size(295, 26);
            this.txtCoordinates.TabIndex = 2;
            this.txtCoordinates.Text = "3915241.03,-10779570.57";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(21, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Search Results:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(21, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Maximum Radius:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(21, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Categorys:";
            // 
            // txtSearchRadius
            // 
            this.txtSearchRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchRadius.Location = new System.Drawing.Point(171, 135);
            this.txtSearchRadius.Name = "txtSearchRadius";
            this.txtSearchRadius.Size = new System.Drawing.Size(127, 26);
            this.txtSearchRadius.TabIndex = 6;
            this.txtSearchRadius.Text = "400";
            // 
            // txtMaxResults
            // 
            this.txtMaxResults.Location = new System.Drawing.Point(171, 167);
            this.txtMaxResults.Name = "txtMaxResults";
            this.txtMaxResults.Size = new System.Drawing.Size(127, 22);
            this.txtMaxResults.TabIndex = 7;
            this.txtMaxResults.Text = "10";
            // 
            // cboLocationCategories
            // 
            this.cboLocationCategories.FormattingEnabled = true;
            this.cboLocationCategories.Location = new System.Drawing.Point(171, 200);
            this.cboLocationCategories.Name = "cboLocationCategories";
            this.cboLocationCategories.Size = new System.Drawing.Size(127, 24);
            this.cboLocationCategories.TabIndex = 8;
            // 
            // txtLocationCategoriesDescription
            // 
            this.txtLocationCategoriesDescription.BackColor = System.Drawing.Color.Gray;
            this.txtLocationCategoriesDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocationCategoriesDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLocationCategoriesDescription.Location = new System.Drawing.Point(3, 230);
            this.txtLocationCategoriesDescription.Multiline = true;
            this.txtLocationCategoriesDescription.Name = "txtLocationCategoriesDescription";
            this.txtLocationCategoriesDescription.Size = new System.Drawing.Size(295, 47);
            this.txtLocationCategoriesDescription.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSearch.Location = new System.Drawing.Point(3, 283);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(295, 29);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearchResultsBestMatch
            // 
            this.txtSearchResultsBestMatch.BackColor = System.Drawing.Color.Gray;
            this.txtSearchResultsBestMatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchResultsBestMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchResultsBestMatch.Location = new System.Drawing.Point(6, 318);
            this.txtSearchResultsBestMatch.Multiline = true;
            this.txtSearchResultsBestMatch.Name = "txtSearchResultsBestMatch";
            this.txtSearchResultsBestMatch.Size = new System.Drawing.Size(295, 47);
            this.txtSearchResultsBestMatch.TabIndex = 11;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(6, 371);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(292, 214);
            this.tabControl.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(284, 185);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Address";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(284, 185);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Place";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(284, 185);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Road";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ReverseGeocodingCloudServicesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ReverseGeocodingCloudServicesSample";
            this.Size = new System.Drawing.Size(1162, 588);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}