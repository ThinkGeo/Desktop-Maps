using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class RefreshPointsRandomly : UserControl
    {
        private readonly RectangleShape pointsBoundary = new RectangleShape(-180, 90, 180, -90);

        private System.Threading.Timer movePointsTimer;
        private System.Threading.Timer changeColorTimer;
        private static Random random = new Random();

        private delegate void ToUIThreadDelegate();

        public RefreshPointsRandomly()
        {
            InitializeComponent();

            movePointsTimer = new System.Threading.Timer(new TimerCallback(MovePoints));
            changeColorTimer = new System.Threading.Timer(new TimerCallback(ChangeColor));
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-90, 90, 90, -90);

            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            InMemoryFeatureLayer pointsLayer = new InMemoryFeatureLayer();
            pointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 10, new GeoSolidBrush(GeoColor.GetRandomGeoColor(RandomColorType.All)));
            pointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            PointShape pointShape = GetRandomPoint(pointsBoundary);

            LayerOverlay pointsOverlay = new LayerOverlay();
            pointsOverlay.Layers.Add("PointsLayer", pointsLayer);
            winformsMap1.Overlays.Add("PointsOverlay", pointsOverlay);

            winformsMap1.Refresh();
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            int pointsCount = Convert.ToInt32(cbxPointCount.SelectedItem);
            InMemoryFeatureLayer pointsLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("PointsLayer");
            Overlay pointsOverlay = winformsMap1.Overlays["PointsOverlay"];

            pointsLayer.InternalFeatures.Clear();
            for (int i = 0; i < pointsCount; i++)
            {
                PointShape pointShape = GetRandomPoint(pointsBoundary);
                string id = GetRandomDirection().ToString();
                Feature feature = new Feature(pointShape.X, pointShape.Y, id);
                pointsLayer.InternalFeatures.Add(feature);
            }

            winformsMap1.Refresh(pointsOverlay);

            // Starts the move points timer
            movePointsTimer.Change(0, Convert.ToInt32(cbxRedrawInterval.SelectedItem));

            // Starts the color changing timer
            changeColorTimer.Change(0, Convert.ToInt32(cbxChangeColorInterval.SelectedItem));

            btnBegin.Enabled = false;
            btnStop.Enabled = true;
        }

        private void MovePoints(object obj)
        {
            this.BeginInvoke(new ToUIThreadDelegate(MovePointsOnUIThread));
        }

        private void MovePointsOnUIThread()
        {
            InMemoryFeatureLayer pointsLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("PointsLayer");
            Overlay pointsOverlay = winformsMap1.Overlays["PointsOverlay"];

            Collection<Feature> features = new Collection<Feature>();
            for (int i = 0; i < pointsLayer.InternalFeatures.Count; i++)
            {
                PointShape shape = pointsLayer.InternalFeatures[i].GetShape() as PointShape;
                string id = pointsLayer.InternalFeatures[i].Id;
                PanDirection direction = (PanDirection)PanDirection.Parse(typeof(PanDirection), id, true);
                if (shape.X > pointsBoundary.UpperLeftPoint.X && shape.X < pointsBoundary.LowerRightPoint.X && shape.Y < pointsBoundary.UpperLeftPoint.Y && shape.Y > pointsBoundary.LowerRightPoint.Y)
                {
                    UpdatePointShape(shape, direction);
                }
                else
                {
                    id = ReverseDirection(shape, direction).ToString();
                }
                features.Add(new Feature(shape.X, shape.Y, id));
            }

            pointsLayer.InternalFeatures.Clear();
            foreach (Feature item in features)
            {
                pointsLayer.InternalFeatures.Add(item);
            }

            winformsMap1.Refresh(pointsOverlay);
        }

        private void ChangeColor(object obj)
        {
            this.BeginInvoke(new ToUIThreadDelegate(ChangeColorOnUIThread));
        }

        private void ChangeColorOnUIThread()
        {
            InMemoryFeatureLayer pointsLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("PointsLayer");
            Overlay pointsOverlay = winformsMap1.Overlays["PointsOverlay"];

            pointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 10, new GeoSolidBrush(GeoColor.GetRandomGeoColor(RandomColorType.All)));

            winformsMap1.Refresh(pointsOverlay);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            movePointsTimer.Change(0, Timeout.Infinite);
            changeColorTimer.Change(0, Timeout.Infinite);

            btnBegin.Enabled = true;
            btnStop.Enabled = false;
        }

        private void RefreshPointsRandomly_ParentChanged(object sender, EventArgs e)
        {
            movePointsTimer.Change(0, Timeout.Infinite);
            changeColorTimer.Change(0, Timeout.Infinite);

            btnBegin.Enabled = true;
            btnStop.Enabled = false;
        }

        private static PanDirection ReverseDirection(PointShape shape, PanDirection direction)
        {
            PanDirection newDirection = PanDirection.Down;

            switch (direction)
            {
                case PanDirection.Up:
                    newDirection = PanDirection.Down;
                    break;

                case PanDirection.UpperRight:
                    newDirection = PanDirection.LowerLeft;
                    break;

                case PanDirection.Right:
                    newDirection = PanDirection.Left;
                    break;

                case PanDirection.LowerRight:
                    newDirection = PanDirection.UpperLeft;
                    break;

                case PanDirection.Down:
                    newDirection = PanDirection.Up;
                    break;

                case PanDirection.LowerLeft:
                    newDirection = PanDirection.UpperRight;
                    break;

                case PanDirection.Left:
                    newDirection = PanDirection.Right;
                    break;

                case PanDirection.UpperLeft:
                    newDirection = PanDirection.LowerRight;
                    break;

                default:
                    break;
            }

            UpdatePointShape(shape, newDirection);

            return newDirection;
        }

        private static void UpdatePointShape(PointShape shape, PanDirection direction)
        {
            switch (direction)
            {
                case PanDirection.Up:
                    shape.Y++;
                    break;

                case PanDirection.UpperRight:
                    shape.X++;
                    shape.Y++;
                    break;

                case PanDirection.Right:
                    shape.X++;
                    break;

                case PanDirection.LowerRight:
                    shape.X++;
                    shape.Y--;
                    break;

                case PanDirection.Down:
                    shape.Y--;
                    break;

                case PanDirection.LowerLeft:
                    shape.X--;
                    shape.Y--;
                    break;

                case PanDirection.Left:
                    shape.X--;
                    break;

                case PanDirection.UpperLeft:
                    shape.X--;
                    shape.Y++;
                    break;

                default:
                    break;
            }
        }

        public static PointShape GetRandomPoint(RectangleShape currentExtent)
        {
            double x = random.Next((int)currentExtent.UpperLeftPoint.X, (int)currentExtent.LowerRightPoint.X);
            double y = random.Next((int)currentExtent.LowerRightPoint.Y, (int)currentExtent.UpperLeftPoint.Y);

            PointShape pointShape = new PointShape(x, y);

            return pointShape;
        }

        public static PanDirection GetRandomDirection()
        {
            int i = random.Next(8);

            PanDirection panDirection = (PanDirection)i;

            return panDirection;
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        private MapView winformsMap1;
        private Button btnStop;
        private ComboBox cbxPointCount;
        private Label label3;
        private Label label4;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.cbxChangeColorInterval = new System.Windows.Forms.ComboBox();
            this.btnBegin = new System.Windows.Forms.Button();
            this.cbxPointCount = new System.Windows.Forms.ComboBox();
            this.cbxRedrawInterval = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.cbxChangeColorInterval);
            this.groupBox1.Controls.Add(this.btnBegin);
            this.groupBox1.Controls.Add(this.cbxPointCount);
            this.groupBox1.Controls.Add(this.cbxRedrawInterval);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(540, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 250);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 39);
            this.label4.TabIndex = 26;
            this.label4.Text = "Add Points into map and choose \r\nthe interval to refresh the \r\nPoints\' position a" +
                "nd color.";
            //
            // btnStop
            //
            this.btnStop.Location = new System.Drawing.Point(110, 214);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // cbxChangeColorInterval
            //
            this.cbxChangeColorInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChangeColorInterval.FormattingEnabled = true;
            this.cbxChangeColorInterval.Items.AddRange(new object[] {
            "2000",
            "1000",
            "600",
            "300"});
            this.cbxChangeColorInterval.Location = new System.Drawing.Point(14, 182);
            this.cbxChangeColorInterval.Name = "cbxChangeColorInterval";
            this.cbxChangeColorInterval.Size = new System.Drawing.Size(170, 21);
            this.cbxChangeColorInterval.TabIndex = 4;
            this.cbxChangeColorInterval.SelectedIndex = 2;
            //
            // btnBegin
            //
            this.btnBegin.Location = new System.Drawing.Point(14, 214);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(75, 23);
            this.btnBegin.TabIndex = 24;
            this.btnBegin.Text = "Begin";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            //
            // cbxPointCount
            //
            this.cbxPointCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPointCount.FormattingEnabled = true;
            this.cbxPointCount.Items.AddRange(new object[] {
            "100",
            "200",
            "500",
            "1000",
            "2000"});
            this.cbxPointCount.Location = new System.Drawing.Point(14, 88);
            this.cbxPointCount.Name = "cbxPointCount";
            this.cbxPointCount.Size = new System.Drawing.Size(170, 21);
            this.cbxPointCount.TabIndex = 3;
            this.cbxPointCount.SelectedIndex = 0;
            //
            // cbxRedrawInterval
            //
            this.cbxRedrawInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRedrawInterval.FormattingEnabled = true;
            this.cbxRedrawInterval.Items.AddRange(new object[] {
            "2000",
            "1000",
            "600",
            "300"});
            this.cbxRedrawInterval.Location = new System.Drawing.Point(14, 135);
            this.cbxRedrawInterval.Name = "cbxRedrawInterval";
            this.cbxRedrawInterval.Size = new System.Drawing.Size(170, 21);
            this.cbxRedrawInterval.TabIndex = 3;
            this.cbxRedrawInterval.SelectedIndex = 2;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Point count:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Change color (ms):";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Redraw Interval (ms):";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 100;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 3;
            this.winformsMap1.Text = "winformsMap1";
            // this.winformsMap1.ZoomLevelSnapping = ThinkGeo.UI.WinForms.ZoomLevelSnappingMode.Default;
            //
            // RefreshPointsRandomly
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "RefreshPointsRandomly";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.ParentChanged += new EventHandler(RefreshPointsRandomly_ParentChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private Button btnBegin;
        private Label label2;
        private Label label1;
        private ComboBox cbxRedrawInterval;
        private ComboBox cbxChangeColorInterval;

        #endregion Component Designer generated code
    }
}