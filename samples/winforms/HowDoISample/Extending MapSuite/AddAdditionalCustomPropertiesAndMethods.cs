using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class AddAdditionalCustomPropertiesAndMethods : UserControl
    {
        public AddAdditionalCustomPropertiesAndMethods()
        {
            InitializeComponent();
        }

        private void AddAdditionalCustomPropertiesAndMethods_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            AdministrationShapeFileLayer worldLayer = new AdministrationShapeFileLayer("SampleData/Countries02.shp", SecurityLevel.AverageUsageLevel1);
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            // Converts the layer from Decimal Degree projection to Spherical Mercator which is the projection the base map is using. 
            worldLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(Projection.GetDecimalDegreesProjString(), Projection.GetSphericalMercatorProjString());

            AdministrationShapeFileLayer statesLayer = new AdministrationShapeFileLayer("SampleData/states.shp", SecurityLevel.AverageUsageLevel2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            // Converts the layer from Decimal Degree projection to Spherical Mercator which is the projection the base map is using. 
            statesLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(Projection.GetDecimalDegreesProjString(), Projection.GetSphericalMercatorProjString());

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            worldOverlay.Layers.Add("StatesLayer", statesLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);
        }

        private void cbxSecurityLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Layer layer in ((LayerOverlay)mapView.Overlays[0]).Layers)
            {
                layer.IsVisible = true;
                SecurityLevel securityLevel = ((AdministrationShapeFileLayer)layer).SecurityLevel;

                if (cbxSecurityLevel.SelectedItem.ToString() == "AverageUsageLevel1" && securityLevel == SecurityLevel.AverageUsageLevel2)
                {
                    layer.IsVisible = false;
                }
                else if (cbxSecurityLevel.SelectedItem.ToString() == "AverageUsageLevel2" && securityLevel == SecurityLevel.AverageUsageLevel1)
                {
                    layer.IsVisible = false;
                }
            }

            mapView.Refresh(mapView.Overlays["WorldOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox gbxFunctions;
        private ComboBox cbxSecurityLevel;
        private MapView mapView;
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxFunctions = new System.Windows.Forms.GroupBox();
            this.cbxSecurityLevel = new System.Windows.Forms.ComboBox();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.gbxFunctions.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxFunctions
            //
            this.gbxFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxFunctions.Controls.Add(this.cbxSecurityLevel);
            this.gbxFunctions.Location = new System.Drawing.Point(577, 3);
            this.gbxFunctions.Name = "gbxFunctions";
            this.gbxFunctions.Size = new System.Drawing.Size(163, 60);
            this.gbxFunctions.TabIndex = 3;
            this.gbxFunctions.TabStop = false;
            this.gbxFunctions.Text = "SecurityLevel";
            //
            // cbxSecurityLevel
            //
            this.cbxSecurityLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSecurityLevel.FormattingEnabled = true;
            this.cbxSecurityLevel.Items.AddRange(new object[] {
            "AdministrationLevel",
            "AverageUsageLevel1",
            "AverageUsageLevel2"});
            this.cbxSecurityLevel.Location = new System.Drawing.Point(6, 19);
            this.cbxSecurityLevel.Name = "cbxSecurityLevel";
            this.cbxSecurityLevel.Size = new System.Drawing.Size(151, 21);
            this.cbxSecurityLevel.TabIndex = 0;
            this.cbxSecurityLevel.Text = "AdministrationLevel";
            this.cbxSecurityLevel.SelectedIndexChanged += new System.EventHandler(this.cbxSecurityLevel_SelectedIndexChanged);
            //
            // winformsMap1
            //
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.Name = "winformsMap1";
            this.mapView.Size = new System.Drawing.Size(740, 528);
            this.mapView.TabIndex = 4;
            this.mapView.Text = "winformsMap1";
            //
            // AddAdditionalCustomPropertiesAndMethods
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFunctions);
            this.Controls.Add(this.mapView);
            this.Name = "AddAdditionalCustomPropertiesAndMethods";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.AddAdditionalCustomPropertiesAndMethods_Load);
            this.gbxFunctions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }

    public enum SecurityLevel
    {
        AdministrativeLevel = 0,
        AverageUsageLevel1 = 1,
        AverageUsageLevel2 = 2
    }

    public class AdministrationShapeFileLayer : ShapeFileFeatureLayer
    {
        private SecurityLevel securityLevel;

        public AdministrationShapeFileLayer(string pathFileName)
            : base(pathFileName)
        {
            securityLevel = SecurityLevel.AdministrativeLevel;
        }

        public AdministrationShapeFileLayer(string pathFileName, SecurityLevel securityLevel)
            : base(pathFileName)
        {
            this.securityLevel = securityLevel;
        }

        public SecurityLevel SecurityLevel
        {
            get
            {
                return securityLevel;
            }
            set
            {
                securityLevel = value;
            }
        }
    }
}