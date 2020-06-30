using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateAUsgsDemFeatureLayer : UserControl
    {
        public CreateAUsgsDemFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            LayerOverlay layerOverlay = new LayerOverlay();
            UsgsDemFeatureLayer layer = new UsgsDemFeatureLayer(SampleHelper.Get("mobile-e.dem"));
            layer.DrawingQuality = DrawingQuality.HighSpeed;

            layer.Open();
            DataTable informations = CreateInformationTable((UsgsDemFeatureSource)layer.FeatureSource);
            informationDataGrid.DataSource = informations.DefaultView;

            ClassBreakStyle classBreakStyle = new ClassBreakStyle();
            classBreakStyle.ColumnName = layer.DataValueColumnName;
            Collection<GeoColor> colors = GeoColor.GetColorsInQualityFamily(GeoColors.Blue, 100);
            double cellValue = (layer.MaxElevation - layer.MinElevation) / 100;
            for (int i = 0; i < colors.Count; i++)
                classBreakStyle.ClassBreaks.Add(new ClassBreak(layer.MinElevation + cellValue * i, new AreaStyle(new GeoSolidBrush(colors[i]))));

            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(layer);
            mapView.Overlays.Add(layerOverlay);

            mapView.CurrentExtent = layer.GetBoundingBox();
        }


        private DataTable CreateInformationTable(UsgsDemFeatureSource featureSource)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Key", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Value", typeof(string)));
            AddRecordToDataTable(dataTable, "Origin", featureSource.OriginCode);
            AddRecordToDataTable(dataTable, "Level", featureSource.QualityLevel.ToString());
            AddRecordToDataTable(dataTable, "ResolutionX", featureSource.ResolutionX.ToString());
            AddRecordToDataTable(dataTable, "ResolutionY", featureSource.ResolutionY.ToString());
            AddRecordToDataTable(dataTable, "ResolutionZ", featureSource.ResolutionZ.ToString());
            AddRecordToDataTable(dataTable, "MinElevation", featureSource.MinElevation.ToString());
            AddRecordToDataTable(dataTable, "MaxElevation", featureSource.MaxElevation.ToString());
            AddRecordToDataTable(dataTable, "ElevationUnit", featureSource.ElevationUnit.ToString());
            AddRecordToDataTable(dataTable, "BoundingBox", featureSource.GetBoundingBox().ToString());

            return dataTable;
        }

        private void AddRecordToDataTable(DataTable dataTable, string key, string value)
        {
            var newRow = dataTable.NewRow();
            newRow[0] = key;
            newRow[1] = value;

            dataTable.Rows.Add(newRow);
        }

        private DataGridView informationDataGrid;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.informationDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.informationDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.UI.WinForms.MapResizeMode.PreserveScale;
            this.mapView.MapUnit = ThinkGeo.Core.GeographyUnit.DecimalDegree;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(740, 528);
            this.mapView.TabIndex = 0;
            // 
            // informationDataGrid
            // 
            this.informationDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.informationDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.informationDataGrid.Location = new System.Drawing.Point(541, 0);
            this.informationDataGrid.Name = "informationDataGrid";
            this.informationDataGrid.Size = new System.Drawing.Size(199, 217);
            this.informationDataGrid.TabIndex = 1;
            // 
            // CreateAUsgsDemFeatureLayer
            // 
            this.Controls.Add(this.informationDataGrid);
            this.Controls.Add(this.mapView);
            this.Name = "CreateAUsgsDemFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.informationDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}