using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class AddEventInOleDbFeatureLayer : UserControl
    {
        public AddEventInOleDbFeatureLayer()
        {
            InitializeComponent();
        }

        private void AddEventInOleDbFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = ExtentHelper.GetDrawingExtent(new RectangleShape(-97.7612400054933, 30.2895641326905, -97.729482650757, 30.2669048309327), winformsMap1.Width, winformsMap1.Height);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));

            string connectString = string.Format(CultureInfo.InvariantCulture, "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='{0}'", Samples.RootDirectory + @"Data\Austinstreets.mdb");
            OleDbFeatureLayer oleDbFeatureLayer = new OleDbFeatureLayer("Austinstreets", "TG_ID", "TG_Wkb", connectString);
            ((OleDbFeatureSource)oleDbFeatureLayer.FeatureSource).ExecutingSqlStatement += new EventHandler<ExecutingSqlStatemenOleDbFeatureSourceEventArgs>(OleDbFeature_ExecutingSqlStatement);
            oleDbFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyles.LocalRoad2);
            oleDbFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("OleDbLayer", oleDbFeatureLayer);
            winformsMap1.Overlays.Add("OleDbOverlay", staticOverlay);

            winformsMap1.Refresh();
        }

        private void OleDbFeature_ExecutingSqlStatement(object sender, ExecutingSqlStatemenOleDbFeatureSourceEventArgs e)
        {
            switch (e.ExecutingSqlStatementType)
            {
                case ExecutingSqlStatementType.GetAllFeatures:
                    if (tabControl1.SelectedTab == tabPage2)
                    {
                        if (radioButton1.Checked)
                            e.SqlStatement += " where TG_ID between 1 and 7000";
                        else if (radioButton2.Checked)
                            e.SqlStatement += " where TG_ID>7001";
                    }
                    else
                    {
                        tbxSqlStr.Text = e.SqlStatement;
                        tbxType.Text = e.ExecutingSqlStatementType.ToString();
                    }
                    break;

                case ExecutingSqlStatementType.GetCount:
                    tbxSqlStr.Text = e.SqlStatement;
                    tbxType.Text = e.ExecutingSqlStatementType.ToString();
                    break;

                case ExecutingSqlStatementType.GetFirstGeometryType:
                    tbxSqlStr.Text = e.SqlStatement;
                    tbxType.Text = e.ExecutingSqlStatementType.ToString();
                    break;

                default:
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbFeatureLayer oleDbFeatureLayer = (OleDbFeatureLayer)winformsMap1.FindFeatureLayer("OleDbLayer");
            oleDbFeatureLayer.Open();
            if (comboBox1.SelectedItem.ToString() == "GetCount")
            {
                oleDbFeatureLayer.FeatureSource.GetCount();
            }
            else if (comboBox1.SelectedItem.ToString() == "GetAllFeatures")
            {
                oleDbFeatureLayer.FeatureSource.GetAllFeatures(new string[] { "TG_ID", "TG_Wkb" });
            }
            else if (comboBox1.SelectedItem.ToString() == "GetFirstGeometryType")
            {
                ((OleDbFeatureSource)oleDbFeatureLayer.FeatureSource).GetFirstGeometryType();
            }
            oleDbFeatureLayer.Close();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            OleDbFeatureLayer oleDbFeatureLayer = (OleDbFeatureLayer)winformsMap1.FindFeatureLayer("OleDbLayer");
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                if (radioButton.Checked)
                {
                    winformsMap1.Overlays["OleDbOverlay"].Lock.EnterWriteLock();
                    oleDbFeatureLayer.Open();
                    oleDbFeatureLayer.FeatureSource.GetAllFeatures(new string[] { "TG_ID", "TG_Wkb" });
                    oleDbFeatureLayer.Close();
                    winformsMap1.Overlays["OleDbOverlay"].Lock.ExitWriteLock();
                    winformsMap1.Refresh();
                }
            }
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private WinformsMap winformsMap1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox tbxType;
        private ComboBox comboBox1;
        private TextBox tbxSqlStr;
        private TabPage tabPage2;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private Label label5;
        private RadioButton radioButton1;

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
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxType = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbxSqlStr = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            //
            // tabControl1
            //
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(456, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(281, 135);
            this.tabControl1.TabIndex = 11;
            //
            // tabPage1
            //
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tbxType);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.tbxSqlStr);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(273, 109);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Get SQL of an Operation";
            this.tabPage1.UseVisualStyleBackColor = true;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Type:";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sql\r\nStatement:";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Operation:";
            //
            // tbxType
            //
            this.tbxType.Location = new System.Drawing.Point(69, 81);
            this.tbxType.Name = "tbxType";
            this.tbxType.ReadOnly = true;
            this.tbxType.Size = new System.Drawing.Size(188, 20);
            this.tbxType.TabIndex = 7;
            //
            // comboBox1
            //
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "GetCount",
            "GetAllFeatures",
            "GetFirstGeometryType"});
            this.comboBox1.Location = new System.Drawing.Point(69, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Text = "GetAllFeatures";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            //
            // tbxSqlStr
            //
            this.tbxSqlStr.Location = new System.Drawing.Point(69, 33);
            this.tbxSqlStr.Multiline = true;
            this.tbxSqlStr.Name = "tbxSqlStr";
            this.tbxSqlStr.ReadOnly = true;
            this.tbxSqlStr.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbxSqlStr.Size = new System.Drawing.Size(188, 42);
            this.tbxSqlStr.TabIndex = 6;
            //
            // tabPage2
            //
            this.tabPage2.Controls.Add(this.radioButton3);
            this.tabPage2.Controls.Add(this.radioButton2);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.radioButton1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(273, 109);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Change SQL of an Operation";
            this.tabPage2.UseVisualStyleBackColor = true;
            //
            // radioButton3
            //
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(26, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(36, 17);
            this.radioButton3.TabIndex = 15;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "All";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            //
            // radioButton2
            //
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(26, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(56, 17);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.Text = "7001~";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Which features need to be shown:";
            //
            // radioButton1
            //
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(26, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 17);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.Text = "1~7000";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            //
            // AddEventInOleDbFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "AddEventInOleDbFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.AddEventInOleDbFeatureLayer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        public class OleDbFeatureLayer : FeatureLayer
        {
            public OleDbFeatureLayer()
                : this(string.Empty, string.Empty, string.Empty, string.Empty)
            { }

            public OleDbFeatureLayer(string tableName, string featureIdColumn, string featurewkbColumn, string connectionString)
                : base()
            {
                FeatureSource = new OleDbFeatureSource(tableName, featureIdColumn, featurewkbColumn, connectionString);
            }
        }

        public class OleDbFeatureSource : FeatureSource
        {
            private OleDbConnection connection;
            private string connectionString;
            private string tableName;
            private string featureIdColumn;
            private string wkbFieldName;

            public OleDbFeatureSource()
                : this(string.Empty, string.Empty, string.Empty, string.Empty)
            { }

            public OleDbFeatureSource(string tableName, string featureIdColumn, string wkbFieldName, string connectionString)
                : base()
            {
                TableName = tableName;
                FeatureIdColumn = featureIdColumn;
                WkbFieldName = wkbFieldName;
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

            public string WkbFieldName
            {
                get { return wkbFieldName; }
                set { wkbFieldName = value; }
            }

            public string FeatureIdColumn
            {
                get { return featureIdColumn; }
                set { featureIdColumn = value; }
            }

            public WellKnownType GetFirstGeometryType()
            {
                OleDbCommand command = null;
                OleDbDataReader dataReader = null;
                WellKnownType type = WellKnownType.Invalid;
                string sqlStatement = string.Format("SELECT TOP 1 {0} FROM {1};", wkbFieldName, tableName);
                try
                {
                    command = new OleDbCommand(sqlStatement, connection);
                    ExecutingSqlStatemenOleDbFeatureSourceEventArgs e = new ExecutingSqlStatemenOleDbFeatureSourceEventArgs(command.CommandText, ExecutingSqlStatementType.GetFirstGeometryType);
                    OnExecutingSqlStatement(e);
                    dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        if (dataReader[0] != DBNull.Value)
                        {
                            type = (new Feature((byte[])(Convert.FromBase64String(dataReader[0].ToString())))).GetWellKnownType();
                        }
                    }
                }
                finally
                {
                    if (dataReader != null) { dataReader.Dispose(); }
                    if (command != null)
                    {
                        command.Connection.Close();
                        command.Dispose();
                    }
                }
                return type;
            }

            public event EventHandler<ExecutingSqlStatemenOleDbFeatureSourceEventArgs> ExecutingSqlStatement;

            protected virtual void OnExecutingSqlStatement(ExecutingSqlStatemenOleDbFeatureSourceEventArgs e)
            {
                EventHandler<ExecutingSqlStatemenOleDbFeatureSourceEventArgs> handler = ExecutingSqlStatement;
                if (handler != null) { handler(this, e); }
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
                    command = new OleDbCommand("SELECT " + featureIdColumn + " as TG_ID, " + wkbFieldName + " as TG_Wkb FROM " + tableName, connection);
                    ExecutingSqlStatemenOleDbFeatureSourceEventArgs e = new ExecutingSqlStatemenOleDbFeatureSourceEventArgs(command.CommandText, ExecutingSqlStatementType.GetAllFeatures);
                    OnExecutingSqlStatement(e);

                    command.CommandText = e.SqlStatement;
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

            protected override int GetCountCore()
            {
                int count = 0;
                OleDbCommand command = null;
                OleDbDataReader dataReader = null;
                string sqlStatement = "SELECT COUNT(*) FROM " + tableName;
                try
                {
                    command = new OleDbCommand(sqlStatement, connection);
                    ExecutingSqlStatemenOleDbFeatureSourceEventArgs e = new ExecutingSqlStatemenOleDbFeatureSourceEventArgs(command.CommandText, ExecutingSqlStatementType.GetCount);
                    OnExecutingSqlStatement(e);

                    dataReader = command.ExecuteReader();
                    if (dataReader.Read()) { count = Convert.ToInt32(dataReader[0], CultureInfo.InvariantCulture); }
                }
                finally
                {
                    if (dataReader != null) { dataReader.Dispose(); }
                    if (command != null)
                    {
                        command.Connection.Close();
                        command.Dispose();
                    }
                }
                return count;
            }

            protected override Collection<FeatureSourceColumn> GetColumnsCore()
            {
                string sqlStatement = string.Format("select * from {0} where 1=0;", tableName);
                OleDbCommand command = null;
                OleDbDataReader dataReader = null;
                Collection<FeatureSourceColumn> columnCollection = new Collection<FeatureSourceColumn>();
                command = new OleDbCommand(sqlStatement, connection);

                dataReader = command.ExecuteReader();
                if (dataReader != null) { dataReader.Dispose(); }
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.InvariantCulture;
                OleDbDataAdapter adpter = new OleDbDataAdapter(command);
                adpter.Fill(dataTable);

                foreach (DataColumn column in dataTable.Columns)
                {
                    string columnName = column.ColumnName;
                    string typeName = column.DataType.Name;
                    int maxLength = column.MaxLength;
                    if (columnName == wkbFieldName) { typeName = "geometry"; }
                    FeatureSourceColumn featureColumn = new FeatureSourceColumn(columnName, typeName, maxLength);
                    columnCollection.Add(featureColumn);
                }
                adpter.Dispose();
                dataTable.Dispose();

                return columnCollection;
            }
        }

        public class ExecutingSqlStatemenOleDbFeatureSourceEventArgs : EventArgs
        {
            private string sqlStatement;
            private ExecutingSqlStatementType executingStatementType;

            public ExecutingSqlStatemenOleDbFeatureSourceEventArgs(string sqlStatement)
                : this(sqlStatement, ExecutingSqlStatementType.Unknown)
            {
                this.sqlStatement = sqlStatement;
            }

            public ExecutingSqlStatemenOleDbFeatureSourceEventArgs(string sqlStatement, ExecutingSqlStatementType sqlStatementType)
                : base()
            {
                this.sqlStatement = sqlStatement;
                this.executingStatementType = sqlStatementType;
            }

            public string SqlStatement
            {
                get { return sqlStatement; }
                set { sqlStatement = value; }
            }

            public ExecutingSqlStatementType ExecutingSqlStatementType
            {
                get { return executingStatementType; }
                set { executingStatementType = value; }
            }
        }
    }
}