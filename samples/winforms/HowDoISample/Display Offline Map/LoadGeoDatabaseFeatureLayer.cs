using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadGeoDatabaseFeatureLayer : UserControl
    {
        public LoadGeoDatabaseFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                mapView.MapUnit = GeographyUnit.Meter;
                mapView.CurrentExtent = new RectangleShape(2149408.38465815, 246471.365609125, 2204046.63635703, 213231.081162168);
                mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));

                PersonalGeoDatabaseFeatureLayer worldLayer = new PersonalGeoDatabaseFeatureLayer(SampleHelper.Get("JORWD6gdb.mdb"));
                worldLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.White, 9.2F, GeoColors.DarkGray, 12.2F, true);
                worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                LayerOverlay staticOverlay = new LayerOverlay();
                staticOverlay.Layers.Add("WorldLayer", worldLayer);
                mapView.Overlays.Add(staticOverlay);
                 
            }
            catch (FileNotFoundException ex)
            {
                string message = "Could not find MapSuiteFdoExtension.dll assembly.\r\n" +
                                 "This dll is expected to be found in the MapSuiteFDOExtensionX86(X64) folder so you need to make this folder available within the System32 folder of your computer. Simply copy the MapSuiteFDOExtensionX86 or MapSuiteFDOExtensionX64 folder from [Install-Path}\\Developer Reference\\System32 to the ‘System32’ (‘System’ for x64) folder of your computer.\r\n" +
                                 "Additionally you need to add the FdoExtension.dll to this sample. You can reference this DLL from [Install-Path]\\Developer Reference\\Spatial Extensions\\Fdo Extension\\.\r\n\r\n" + ex.Message;
                MessageBox.Show(message, "FileNotFound", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            //
            // UserControl
            //
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}