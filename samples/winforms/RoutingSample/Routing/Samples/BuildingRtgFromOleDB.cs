using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeo.MapSuite.Routing;

namespace HowDoISamples
{
    public class BuildingRtgFromOleDB : UserControl
    {
        private const string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\SampleData\AustinStreets.mdb;User Id=admin;Password=;";
        private OleDbLineFeatureSource featureSource;

        public BuildingRtgFromOleDB()
        {
            InitializeComponent();
            featureSource = new OleDbLineFeatureSource("austinstreets", "TG_ID", "TG_Wkb", connectionString);
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            RoutingSource routingSource = new RtgRoutingSource(@"..\..\SampleData\AustinStreetFromOleDb.rtg");
            RoutingEngine routingEngine = new RoutingEngine(routingSource, new DijkstraRoutingAlgorithm(), featureSource);
            RoutingResult routingResult = routingEngine.GetRoute(txtStartId.Text, txtEndId.Text);

            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            winformsMap1.Overlays["RoutingOverlay"].Lock.EnterWriteLock();
            try
            {
                routingLayer.Routes.Clear();
                routingLayer.Routes.Add(routingResult.Route);
            }
            finally
            {
                winformsMap1.Overlays["RoutingOverlay"].Lock.ExitWriteLock();
            }

            winformsMap1.Refresh();
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-97.763384, 30.299707, -97.712382, 30.259309);

            OleDbLineFeatureLayer austinstreetsLayer = new OleDbLineFeatureLayer(featureSource);
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.LocalRoad3;
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay austinstreetsOverlay = new LayerOverlay();
            austinstreetsOverlay.Layers.Add("AustinstreetsLayer", austinstreetsLayer);
            winformsMap1.Overlays.Add("AustinstreetsOverlay", austinstreetsOverlay);

            RoutingLayer routingLayer = new RoutingLayer();
            austinstreetsLayer.Open();
            routingLayer.StartPoint = austinstreetsLayer.FeatureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            routingLayer.EndPoint = austinstreetsLayer.FeatureSource.GetFeatureById(txtEndId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            austinstreetsLayer.Close();
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private Button btnRoute;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtEndId;
        private TextBox txtStartId;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
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
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.btnRoute = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCoordinate
            // 
            this.lblCoordinate.AutoSize = true;
            this.lblCoordinate.Location = new System.Drawing.Point(204, 234);
            this.lblCoordinate.Name = "lblCoordinate";
            this.lblCoordinate.Size = new System.Drawing.Size(0, 13);
            this.lblCoordinate.TabIndex = 6;
            this.lblCoordinate.Visible = false;
            // 
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(90, 139);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(112, 23);
            this.btnRoute.TabIndex = 3;
            this.btnRoute.Text = "Get Shortest Path";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRoute);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 172);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 39);
            this.label3.TabIndex = 14;
            this.label3.Text = "The sample demonstrates how to build the\r\nrouting data from database, here it use" +
                "s \r\nMS Access as an example. ";
            // 
            // txtStartId
            // 
            this.txtStartId.Location = new System.Drawing.Point(95, 82);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.ReadOnly = true;
            this.txtStartId.Size = new System.Drawing.Size(112, 20);
            this.txtStartId.TabIndex = 1;
            this.txtStartId.Text = "4716";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start Feature Id:";
            // 
            // txtEndId
            // 
            this.txtEndId.Location = new System.Drawing.Point(95, 110);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.ReadOnly = true;
            this.txtEndId.Size = new System.Drawing.Size(112, 20);
            this.txtEndId.TabIndex = 2;
            this.txtEndId.Text = "9638";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "End Feature Id:";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.DesktopEdition.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.SingleThreaded;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            // 
            // BuildingRtgFromOleDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "BuildingRtgFromOleDB";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }

    public class OleDbLineFeatureLayer : FeatureLayer
    {
        public OleDbLineFeatureLayer()
            : this(new OleDbLineFeatureSource())
        { }

        public OleDbLineFeatureLayer(OleDbLineFeatureSource featureSource)
            : base()
        {
            FeatureSource = featureSource;
        }
    }

    public class OleDbLineFeatureSource : FeatureSource
    {
        private OleDbConnection connection;
        private string connectionString;
        private string tableName;
        private string idColumnName;
        private string wkbColumnName;

        public OleDbLineFeatureSource()
            : this(string.Empty, string.Empty, string.Empty, string.Empty)
        { }

        public OleDbLineFeatureSource(string tableName, string idColumnName, string wkbColumnName, string connectionString)
            : base()
        {
            TableName = tableName;
            IdColumnName = idColumnName;
            WkbColumnName = wkbColumnName;
            ConnectionString = connectionString;
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public string WkbColumnName
        {
            get { return wkbColumnName; }
            set { wkbColumnName = value; }
        }

        public string IdColumnName
        {
            get { return idColumnName; }
            set { idColumnName = value; }
        }

        protected override void OpenCore()
        {
            connection = new OleDbConnection(connectionString);
            connection.Open();
        }

        protected override void CloseCore()
        {
            connection.Close();
        }

        protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
        {
            OleDbCommand command = null;
            OleDbDataReader dataReader = null;
            Collection<Feature> returnFeatures = new Collection<Feature>();

            try
            {
                command = new OleDbCommand("SELECT " + idColumnName + " as TG_ID, " + wkbColumnName + " as TG_Wkb FROM " + tableName, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    byte[] wkb = Convert.FromBase64String(dataReader["TG_Wkb"].ToString());
                    Feature feature = new Feature(wkb, dataReader["TG_ID"].ToString());

                    foreach (string columnName in returningColumnNames)
                    {
                        feature.ColumnValues.Add(columnName, dataReader[columnName].ToString());
                    }
                    returnFeatures.Add(feature);
                }
            }
            finally
            {
                if (command != null) { command.Dispose(); }
                if (dataReader != null) { dataReader.Dispose(); }
            }

            return returnFeatures;
        }
    }

}
