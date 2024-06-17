using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CustomStyles : UserControl
    {
        public CustomStyles()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var worldCapitalsLayer = new ShapeFileFeatureLayer(@"..\..\..\Data\Shapefile\WorldCapitals.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(4326, 3857)
                    }
            };
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldCapitals", worldCapitalsLayer);
            mapView.Overlays.Add("Overlay", worldOverlay);

            mapView.CurrentExtent = new RectangleShape(-15360785.1188513, 14752615.1010077, 16260907.558937, -12603279.9259404);

            await mapView.RefreshAsync();
        }

        protected override void Dispose(bool disposing)
        {
            mapView.Dispose();
            GC.SuppressFinalize(this);
        }

        private Panel panel1;
        private Button TimeBasedPointStyle;
        private Label label1;
        private Button SizedBasedPointStyle;
        private Label label2;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SizedBasedPointStyle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeBasedPointStyle = new System.Windows.Forms.Button();
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
            this.mapView.ForeColor = System.Drawing.Color.Black;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapFocusMode = ThinkGeo.Core.MapFocusMode.Default;
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1296, 599);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.SizedBasedPointStyle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TimeBasedPointStyle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(993, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 599);
            this.panel1.TabIndex = 1;
            // 
            // SizedBasedPointStyle
            // 
            this.SizedBasedPointStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SizedBasedPointStyle.Location = new System.Drawing.Point(18, 143);
            this.SizedBasedPointStyle.Name = "SizedBasedPointStyle";
            this.SizedBasedPointStyle.Size = new System.Drawing.Size(267, 32);
            this.SizedBasedPointStyle.TabIndex = 3;
            this.SizedBasedPointStyle.Text = "Apply Style";
            this.SizedBasedPointStyle.UseVisualStyleBackColor = true;
            this.SizedBasedPointStyle.Click += new System.EventHandler(this.SizedBasedPointStyle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Capitol Population Point Style:";
            // 
            // TimeBasedPointStyle
            // 
            this.TimeBasedPointStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TimeBasedPointStyle.Location = new System.Drawing.Point(18, 56);
            this.TimeBasedPointStyle.Name = "TimeBasedPointStyle";
            this.TimeBasedPointStyle.Size = new System.Drawing.Size(267, 32);
            this.TimeBasedPointStyle.TabIndex = 1;
            this.TimeBasedPointStyle.Text = "Apply Style";
            this.TimeBasedPointStyle.UseVisualStyleBackColor = true;
            this.TimeBasedPointStyle.Click += new System.EventHandler(this.TimeBasedPointStyle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Daylight Point Style:";
            // 
            // CustomStyles
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "CustomStyles";
            this.Size = new System.Drawing.Size(1296, 599);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion Component Designer generated code

        private async void TimeBasedPointStyle_Click(object sender, EventArgs e)
        {
            var worldCapitalsLayer = mapView.FindFeatureLayer("WorldCapitals");

            var timeBasedPointStyle = new TimeBasedPointStyle
            {
                TimeZoneColumnName = "TimeZone",
                DaytimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Yellow, 12, GeoColors.Black),
                NighttimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Gray, 12, GeoColors.Black)
            };

            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(timeBasedPointStyle);

            await mapView.RefreshAsync();
        }

        private async void SizedBasedPointStyle_Click(object sender, EventArgs e)
        {
            var worldCapitalsLayer = mapView.FindFeatureLayer("WorldCapitals");

            var sizedpointStyle = new SizedPointStyle(PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 1), "population", 500000);

            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(sizedpointStyle);

            await mapView.RefreshAsync();
        }

    }

    // This style draws points on the capitols with their color based on the current time and if
    // we think it's daylight there or not.
    class TimeBasedPointStyle : ThinkGeo.Core.Style
    {
        private PointStyle daytimePointStyle;
        private PointStyle nighttimePointStyle;
        private string timeZoneColumnName;

        public TimeBasedPointStyle()
            : this(string.Empty, new PointStyle(), new PointStyle())
        { }

        public TimeBasedPointStyle(string timeZoneColumnName, PointStyle daytimePointStyle, PointStyle nighttimePointStyle)
        {
            this.timeZoneColumnName = timeZoneColumnName;
            this.daytimePointStyle = daytimePointStyle;
            this.nighttimePointStyle = nighttimePointStyle;
        }

        public PointStyle DaytimePointStyle
        {
            get { return daytimePointStyle; }
            set { daytimePointStyle = value; }
        }

        public PointStyle NighttimePointStyle
        {
            get { return nighttimePointStyle; }
            set { nighttimePointStyle = value; }
        }

        public string TimeZoneColumnName
        {
            get { return timeZoneColumnName; }
            set { timeZoneColumnName = value; }
        }


        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                // Here we are going to do the calculation to see what
                // time it is for each feature and draw the appropriate style
                var offsetToGmt = Convert.ToSingle(feature.ColumnValues[timeZoneColumnName]);
                var localTime = DateTime.UtcNow.AddHours(offsetToGmt);
                if (localTime.Hour >= 7 && localTime.Hour <= 19)
                {
                    // Daytime
                    daytimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                }
                else
                {
                    //Nighttime

                    nighttimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                }
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            Collection<string> columns = new Collection<string>();

            // Grab any columns that the daytime style may need.
            Collection<string> daytimeColumns = daytimePointStyle.GetRequiredColumnNames();
            foreach (string column in daytimeColumns)
            {
                if (!columns.Contains(column))
                {
                    columns.Add(column);
                }
            }

            // Grab any columns that the nighttime style may need.
            Collection<string> nighttimeColumns = nighttimePointStyle.GetRequiredColumnNames();
            foreach (string column in nighttimeColumns)
            {
                if (!columns.Contains(column))
                {
                    columns.Add(column);
                }
            }

            // Make sure we add the timezone column
            if (!columns.Contains(timeZoneColumnName))
            {
                columns.Add(timeZoneColumnName);
            }

            return columns;
        }
    }

    // This style draws a point sized with the population of the capitol.  It uses the DrawCore of the style
    // to draw directly on the canvas.  It can also leverage other styles to draw on the canvas as well.
    class SizedPointStyle : Style
    {
        private PointStyle pointStyle;
        private float ratio;
        private string sizeColumnName;

        public SizedPointStyle()
            : this(new PointStyle(), string.Empty, 1)
        { }

        public SizedPointStyle(PointStyle pointStyle, string sizeColumnName, float ratio)
        {
            this.pointStyle = pointStyle;
            this.sizeColumnName = sizeColumnName;
            this.ratio = ratio;
        }

        public PointStyle PointStyle
        {
            get { return pointStyle; }
            set { pointStyle = value; }
        }

        public float Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }

        public string SizeColumnName
        {
            get { return sizeColumnName; }
            set { sizeColumnName = value; }
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            // Loop through each feature and determine how large the point should 
            // be then adjust it's size.
            foreach (var feature in features)
            {
                float sizeData = Convert.ToSingle(feature.ColumnValues[sizeColumnName]);
                float symbolSize = sizeData / ratio;
                pointStyle.SymbolSize = symbolSize;
                pointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            // Here we grab the columns from the pointStyle and then add
            // the sizeColumn name to make sure we pull back the column
            //  that we need to calculate the size
            Collection<string> columns = new Collection<string>();
            columns = pointStyle.GetRequiredColumnNames();
            if (!columns.Contains(sizeColumnName))
            {
                columns.Add(sizeColumnName);
            }

            return columns;
        }
    }
}