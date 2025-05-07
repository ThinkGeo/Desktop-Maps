using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GetMapSnapShot : UserControl
    {
        public GetMapSnapShot()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light

            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10810995, 3939081, -10747552, 3884429);

            // Add a marker in the center of the map. 
            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            var marker = new Marker(mapView.CurrentExtent.GetCenterPoint());
            simpleMarkerOverlay.Markers.Add(marker);
            mapView.Overlays.Add(simpleMarkerOverlay);

            await mapView.RefreshAsync();
        }

        private void Snapshot_Click(object sender, EventArgs e)
        {
            var snapShot = mapView.GetSnapshot();
            snapShot.Save(@".\snapshot.png");

            var fullPath = Path.GetFullPath(@".\snapshot.png");
            MessageBox.Show($@"The snapshot image was saved at this path: {fullPath}");
        }

        #region Component Designer generated code

        private Panel panel1;
        private Button snapshot;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.snapshot = new System.Windows.Forms.Button();
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
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(891, 560);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.snapshot);
            this.panel1.Location = new System.Drawing.Point(894, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 560);
            this.panel1.TabIndex = 1;
            // 
            // snapshot
            // 
            this.snapshot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.snapshot.ForeColor = System.Drawing.Color.Black;
            this.snapshot.Location = new System.Drawing.Point(3, 16);
            this.snapshot.Name = "snapshot";
            this.snapshot.Size = new System.Drawing.Size(293, 35);
            this.snapshot.TabIndex = 9;
            this.snapshot.Text = "Get Snapshot";
            this.snapshot.UseVisualStyleBackColor = true;
            this.snapshot.Click += Snapshot_Click;
            // 
            // GetMapSnapShot
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GetMapSnapShot";
            this.Size = new System.Drawing.Size(1194, 560);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}