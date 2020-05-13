using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using System.Drawing;


namespace CSharpWinformsSamples
{
    public class DisplayAMarker : UserControl
    {
        public DisplayAMarker()
        {
            InitializeComponent();
        }

        private void DisplayMap_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-155.733, 95.60, 104.42, -81.9);

            WorldMapKitWmsDesktopOverlay worldMapKitOverlay = new WorldMapKitWmsDesktopOverlay();
            winformsMap1.Overlays.Add(worldMapKitOverlay);

            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.MapControl = winformsMap1;
            markerOverlay.DragMode = MarkerDragMode.CopyWithShiftKey;
            winformsMap1.Overlays.Add("MarkerOverlay", markerOverlay);

            Marker marker = new Marker(-95.2806, 38.9554);
            marker.Image = new Bitmap(@"..\..\SampleData\Data\United States.png");
            marker.Width = 32;
            marker.Height = 32;
            marker.YOffsetInScreen = -17;
            markerOverlay.Markers.Add(marker);

            winformsMap1.Refresh();
        }

        private WinformsMap winformsMap1;

        #region Component Designer generated code

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
            ThinkGeo.MapSuite.DesktopEdition.EditInteractiveOverlay editInteractiveOverlay1 = new ThinkGeo.MapSuite.DesktopEdition.EditInteractiveOverlay();
            ThinkGeo.MapSuite.DesktopEdition.ExtentInteractiveOverlay extentInteractiveOverlay1 = new ThinkGeo.MapSuite.DesktopEdition.ExtentInteractiveOverlay();
            ThinkGeo.MapSuite.DesktopEdition.TrackInteractiveOverlay trackInteractiveOverlay1 = new ThinkGeo.MapSuite.DesktopEdition.TrackInteractiveOverlay();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
            this.SuspendLayout();
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.Gray;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            editInteractiveOverlay1.IsVisible = true;
            editInteractiveOverlay1.Name = null;
            this.winformsMap1.EditOverlay = editInteractiveOverlay1;
            extentInteractiveOverlay1.IsVisible = true;
            extentInteractiveOverlay1.Name = null;
            this.winformsMap1.ExtentOverlay = extentInteractiveOverlay1;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            trackInteractiveOverlay1.IsVisible = true;
            trackInteractiveOverlay1.Name = null;
            trackInteractiveOverlay1.TrackMode = ThinkGeo.MapSuite.DesktopEdition.TrackMode.None;
            this.winformsMap1.TrackOverlay = trackInteractiveOverlay1;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            this.winformsMap1.ExtentOverlay.ZoomPercentage = 40;
            // 
            // DisplayShapeMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "DisplayShapeMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplayMap_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
