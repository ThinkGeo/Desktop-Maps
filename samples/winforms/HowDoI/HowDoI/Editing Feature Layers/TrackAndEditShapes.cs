/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class TrackAndEditShapes : UserControl
    {
        public TrackAndEditShapes()
        {
            InitializeComponent();
        }

        private void TrackAndEditShapes_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);
            winformsMap1.Refresh();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnTrackNormal":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
                        break;

                    case "btnTrackPoint":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Point;
                        break;

                    case "btnTrackLine":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Line;
                        break;

                    case "btnTrackRectangle":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Rectangle;
                        break;

                    case "btnTrackSquare":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Square;
                        break;

                    case "btnTrackPolygon":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
                        break;

                    case "btnTrackCircle":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Circle;
                        break;

                    case "btnTrackEllipse":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Ellipse;
                        break;

                    case "btnTrackEdit":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
                        foreach (Feature feature in winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures)
                        {
                            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature);
                        }
                        winformsMap1.EditOverlay.CalculateAllControlPoints();
                        winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

                        winformsMap1.Refresh(new Overlay[] { winformsMap1.EditOverlay, winformsMap1.TrackOverlay });
                        break;

                    case "btnTrackDelete":
                        int lastIndex = winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Count - 1;

                        if (lastIndex >= 0)
                        {
                            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.RemoveAt(lastIndex);
                            winformsMap1.EditOverlay.CalculateAllControlPoints();
                        }

                        winformsMap1.Refresh(winformsMap1.EditOverlay);
                        break;

                    default:
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
                        break;
                }
            }
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private Button btnTrackNormal;
        private Button btnTrackPoint;
        private Button btnTrackLine;
        private Button btnTrackRectangle;
        private Button btnTrackSquare;
        private Button btnTrackPolygon;
        private Button btnTrackCircle;
        private Button btnTrackEllipse;
        private Button btnTrackEdit;
        private Button btnTrackDelete;
        private System.ComponentModel.IContainer components = null;
        private WinformsMap winformsMap1;

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
            this.btnTrackPoint = new System.Windows.Forms.Button();
            this.btnTrackNormal = new System.Windows.Forms.Button();
            btnTrackLine = new Button();
            btnTrackRectangle = new Button();
            btnTrackSquare = new Button();
            btnTrackPolygon = new Button();
            btnTrackCircle = new Button();
            btnTrackEllipse = new Button();
            btnTrackEdit = new Button();
            btnTrackDelete = new Button();

            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnTrackNormal);
            this.groupBox1.Controls.Add(this.btnTrackPoint);
            this.groupBox1.Controls.Add(this.btnTrackLine);
            this.groupBox1.Controls.Add(this.btnTrackRectangle);
            this.groupBox1.Controls.Add(this.btnTrackSquare);
            this.groupBox1.Controls.Add(this.btnTrackPolygon);
            this.groupBox1.Controls.Add(this.btnTrackCircle);
            this.groupBox1.Controls.Add(this.btnTrackEllipse);
            this.groupBox1.Controls.Add(this.btnTrackEdit);
            this.groupBox1.Controls.Add(this.btnTrackDelete);
            this.groupBox1.Location = new System.Drawing.Point(370, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 60);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";

            this.btnTrackNormal.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackNormal.Image = global::CSHowDoISamples.Properties.Resources.Normal;
            this.btnTrackNormal.Location = new System.Drawing.Point(7, 20);
            this.btnTrackNormal.Name = "btnTrackNormal";
            this.btnTrackNormal.Size = new System.Drawing.Size(30, 30);
            this.btnTrackNormal.TabIndex = 0;
            this.btnTrackNormal.UseVisualStyleBackColor = false;
            btnTrackNormal.Click += new EventHandler(button_Click);

            this.btnTrackPoint.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackPoint.Image = global::CSHowDoISamples.Properties.Resources.Point;
            this.btnTrackPoint.Location = new System.Drawing.Point(43, 20);
            this.btnTrackPoint.Name = "btnTrackPoint";
            this.btnTrackPoint.Size = new System.Drawing.Size(30, 30);
            this.btnTrackPoint.TabIndex = 1;
            this.btnTrackPoint.UseVisualStyleBackColor = false;
            btnTrackPoint.Click += new EventHandler(button_Click);

            this.btnTrackLine.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackLine.Image = global::CSHowDoISamples.Properties.Resources.Line;
            this.btnTrackLine.Location = new System.Drawing.Point(79, 20);
            this.btnTrackLine.Name = "btnTrackLine";
            this.btnTrackLine.Size = new System.Drawing.Size(30, 30);
            this.btnTrackLine.TabIndex = 2;
            this.btnTrackLine.UseVisualStyleBackColor = false;
            btnTrackLine.Click += new EventHandler(button_Click);

            this.btnTrackRectangle.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackRectangle.Image = global::CSHowDoISamples.Properties.Resources.Rectangle;
            this.btnTrackRectangle.Location = new System.Drawing.Point(115, 20);
            this.btnTrackRectangle.Name = "btnTrackRectangle";
            this.btnTrackRectangle.Size = new System.Drawing.Size(30, 30);
            this.btnTrackRectangle.TabIndex = 3;
            this.btnTrackRectangle.UseVisualStyleBackColor = false;
            btnTrackRectangle.Click += new EventHandler(button_Click);

            this.btnTrackSquare.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackSquare.Image = global::CSHowDoISamples.Properties.Resources.Square;
            this.btnTrackSquare.Location = new System.Drawing.Point(151, 20);
            this.btnTrackSquare.Name = "btnTrackSquare";
            this.btnTrackSquare.Size = new System.Drawing.Size(30, 30);
            this.btnTrackSquare.TabIndex = 4;
            this.btnTrackSquare.UseVisualStyleBackColor = false;
            btnTrackSquare.Click += new EventHandler(button_Click);

            this.btnTrackPolygon.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackPolygon.Image = global::CSHowDoISamples.Properties.Resources.Polygon;
            this.btnTrackPolygon.Location = new System.Drawing.Point(187, 20);
            this.btnTrackPolygon.Name = "btnTrackPolygon";
            this.btnTrackPolygon.Size = new System.Drawing.Size(30, 30);
            this.btnTrackPolygon.TabIndex = 5;
            this.btnTrackPolygon.UseVisualStyleBackColor = false;
            btnTrackPolygon.Click += new EventHandler(button_Click);

            this.btnTrackCircle.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackCircle.Image = global::CSHowDoISamples.Properties.Resources.Circle;
            this.btnTrackCircle.Location = new System.Drawing.Point(223, 20);
            this.btnTrackCircle.Name = "btnTrackCircle";
            this.btnTrackCircle.Size = new System.Drawing.Size(30, 30);
            this.btnTrackCircle.TabIndex = 6;
            this.btnTrackCircle.UseVisualStyleBackColor = false;
            btnTrackCircle.Click += new EventHandler(button_Click);

            this.btnTrackEllipse.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackEllipse.Image = global::CSHowDoISamples.Properties.Resources.Ellipse;
            this.btnTrackEllipse.Location = new System.Drawing.Point(259, 20);
            this.btnTrackEllipse.Name = "btnTrackEllipse";
            this.btnTrackEllipse.Size = new System.Drawing.Size(30, 30);
            this.btnTrackEllipse.TabIndex = 7;
            this.btnTrackEllipse.UseVisualStyleBackColor = false;
            btnTrackEllipse.Click += new EventHandler(button_Click);

            this.btnTrackEdit.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackEdit.Image = global::CSHowDoISamples.Properties.Resources.Edit;
            this.btnTrackEdit.Location = new System.Drawing.Point(295, 20);
            this.btnTrackEdit.Name = "btnTrackEdit";
            this.btnTrackEdit.Size = new System.Drawing.Size(30, 30);
            this.btnTrackEdit.TabIndex = 8;
            this.btnTrackEdit.UseVisualStyleBackColor = false;
            btnTrackEdit.Click += new EventHandler(button_Click);

            this.btnTrackDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackDelete.Image = global::CSHowDoISamples.Properties.Resources.Delete;
            this.btnTrackDelete.Location = new System.Drawing.Point(331, 20);
            this.btnTrackDelete.Name = "btnTrackDelete";
            this.btnTrackDelete.Size = new System.Drawing.Size(30, 30);
            this.btnTrackDelete.TabIndex = 9;
            this.btnTrackDelete.UseVisualStyleBackColor = false;
            btnTrackDelete.Click += new EventHandler(button_Click);

            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            //
            // DisplayShapeMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);

            this.Controls.Add(this.winformsMap1);
            this.Name = "DisplayShapeMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.TrackAndEditShapes_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}