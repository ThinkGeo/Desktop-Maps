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
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            AdministrationShapeFileLayer worldLayer = new AdministrationShapeFileLayer(SampleHelper.Get("Countries02.shp"), SecurityLevel.AverageUsageLevel1);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));

            AdministrationShapeFileLayer statesLayer = new AdministrationShapeFileLayer(SampleHelper.Get("USStates.shp"), SecurityLevel.AverageUsageLevel2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            worldOverlay.Layers.Add("StatesLayer", statesLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-177.39584350585937, 83.113876342773437, -52.617362976074219, 14.550546646118164);

            winformsMap1.Refresh();
        }

        private void cbxSecurityLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Layer layer in ((LayerOverlay)winformsMap1.Overlays[0]).Layers)
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

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox gbxFunctions;
        private ComboBox cbxSecurityLevel;
        private MapView winformsMap1;
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
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
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
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 4;
            this.winformsMap1.Text = "winformsMap1";
            //
            // AddAdditionalCustomPropertiesAndMethods
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFunctions);
            this.Controls.Add(this.winformsMap1);
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