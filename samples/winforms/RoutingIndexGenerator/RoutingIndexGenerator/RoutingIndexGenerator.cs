using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;

namespace RoutingIndexGenerator
{
    public partial class RoutingIndexGenerator : Form
    {
        // If the area is more than 1,000,000 square miles, we would throw a warning as the index generating might take too much time. 
        private double warningAreaInSquareMiles = 1000000;
        private ConfigModel currentConfigModel;
        private RoutingIndexBuilder routingIndexbuilder;
        private Dictionary<string, ConfigModel> configModelsDictionary;

        public RoutingIndexGenerator()
        {
            InitializeComponent();

            configModelsDictionary = new Dictionary<string, ConfigModel>();

            if (File.Exists("InitConfig.xml"))
            {
                ConfigModel model = new ConfigModel("InitConfig.xml");
                string key = model.FileType.Split('|').Last();
                configModelsDictionary.Add(key, model);
            }

            // We will always load shapefile configuration by default, even there is no configuration file.
            if (!configModelsDictionary.Any(i => i.Value.FileType.Contains("*.shp")))
            {
                ConfigModel model = new ConfigModel(XDocument.Parse(Properties.Resources.DefaultConfig));
                string key = model.FileType.Split('|').Last();
                configModelsDictionary.Add(key, model);
            }
        }

        private void btnBrowseSqliteFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = GetSelectFileFilter();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string currentKey = string.Format("*.{0}", fileDialog.FileName.Split('.').Last());
                currentConfigModel = configModelsDictionary[currentKey];

                // Get the bounding box of the feature source. 

                FeatureSource featureSource = null;

                if (currentConfigModel.FileType.Contains("shp"))
                {
                    ShapeFileFeatureSource.BuildIndexFile(fileDialog.FileName, BuildIndexMode.DoNotRebuild);
                    featureSource = new ShapeFileFeatureSource(fileDialog.FileName);
                }
                else
                    featureSource = new SqliteFeatureSource(string.Format("Data Source={0};Version=3;", fileDialog.FileName), currentConfigModel.TableName, currentConfigModel.FeatureIdColumn, currentConfigModel.GeometryColumnName);

                featureSource.Open();
                WellKnownType wellKnownType = featureSource.GetFirstFeaturesWellKnownType();
                if (wellKnownType != WellKnownType.Line && wellKnownType != WellKnownType.Multiline)
                {
                    ShowErrorMessageBox();
                    return;
                }

                txtDataProviderFilePath.Text = fileDialog.FileName;
                txtRoutableFile.Text = Path.ChangeExtension(fileDialog.FileName, ".routable.sqlite");
                txtIndexFilePath.Text = Path.ChangeExtension(fileDialog.FileName, ".routable.rtg");

                RectangleShape boundingBox = featureSource.GetBoundingBox();
                txtUpperLeftX.Text = string.Format("{0},{1}", boundingBox.UpperLeftPoint.X, boundingBox.UpperLeftPoint.Y);
                txtLowerRightX.Text = string.Format("{0},{1}", boundingBox.LowerRightPoint.X, boundingBox.LowerRightPoint.Y);

                // Get the columns of the feature source. 
                Collection<FeatureSourceColumn> columns = featureSource.GetColumns();
                featureSource.Close();

                if (boundingBox.UpperLeftPoint.X < -181 || boundingBox.UpperLeftPoint.Y > 91 || boundingBox.LowerRightPoint.X > 181 || boundingBox.LowerRightPoint.Y < -91)
                {
                    cmbGeographyUnit.SelectedIndex = 2;
                }
                else
                {
                    cmbGeographyUnit.SelectedIndex = 0;
                }

                cmbRoutableFileType.SelectedIndex = 0;

                InitCombColumns(cmbOnewayRoadColumn, columns, currentConfigModel.IdentifierColumnName);
                InitCombColumns(cmbOnewayIndicatorColumn, columns, currentConfigModel.IndicatorColumnName);
                InitCombColumns(cmbRoadClassColumn, columns, currentConfigModel.SpeedColumnName);
                InitCombColumns(cmbSpeedColumn, columns, currentConfigModel.SpeedColumnName);
                InitCombColumns(cmbClosedRoadColumn, columns, currentConfigModel.ClosedColumnName);

                if (currentConfigModel.RoutingIndexType == 0)
                {
                    rbtnClassSpeed.Checked = true;
                    tbOptions.Enabled = true;
                }

                cmbRouteIndexType.SelectedIndex = currentConfigModel.RoutingIndexType;
                cmbSpeedUnit.SelectedIndex = currentConfigModel.SpeedUnit;

                txtDefaultSpeed.Text = currentConfigModel.DefaultSpeed.ToString();
                txtOneWayRoadValue.Text = currentConfigModel.IdentifierColumnValue;
                txtFromToValue.Text = currentConfigModel.IndicatorStartEndValue;

                btnGenerate.Enabled = true;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            RoutingIndexBuilderParameters buildRoutingIndexParameters = GetIndexBuildingParameters();
            if (buildRoutingIndexParameters == null)
            {
                return;
            }

            //create a new RoutingIndexFileBuilder.
            if (routingIndexbuilder != null)
            {
                routingIndexbuilder.RemoveEvents();
            }

            routingIndexbuilder = new RoutingIndexBuilder(buildRoutingIndexParameters);

            //Add an event to display refresh the ProgressBar.
            routingIndexbuilder.ExtractingSqliteDatabase += routingIndexbuilder_ExtractingSqliteDatabase;
            routingIndexbuilder.BuildingRoutableSegment += routingIndexbuilder_BuildingRoutableSegment;
            routingIndexbuilder.BuildingRouteRTSegment += routingIndexbuilder_BuildingRouteSegment;
            routingIndexbuilder.BuildingIndexFinished += routingIndexbuilder_BuildingIndexFinished;
            routingIndexbuilder.BuildingShapeFileIndex += routingIndexbuilder_BuildingShapeFileIndex;

            btnGenerate.Enabled = false;
            //Start building index file(.rtg).
            Task.Factory.StartNew(() =>
            {
                routingIndexbuilder.ExtractRoadsFromSourceFileToTempFileWithLowerMemory(currentConfigModel);
                routingIndexbuilder.StartBuildingRoutableFile();
                routingIndexbuilder.StartBuildingRtg();
            });
        }

        private void routingIndexbuilder_BuildingShapeFileIndex(object sender, RoutingIndexBuilderEventArgs e)
        {
            if (e.ProcessedRecordCount % 100 == 0 || e.ProcessedRecordCount == e.TotalRecordCount)
            {
                this.BeginInvoke(new Action(() =>
                {
                    lblProcessingMessage.Text = string.Format("Building shape file index: {0}/{1}", e.ProcessedRecordCount, e.TotalRecordCount);
                    pgbBuildProgress.Value = (int)(((double)e.ProcessedRecordCount / (double)e.TotalRecordCount) * 100);
                }));
            }
        }

        private void routingIndexbuilder_BuildingIndexFinished(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                btnGenerate.Enabled = true;
                MessageBox.Show(this, "Generation Completed!", "Completed");
            }));
        }

        private void routingIndexbuilder_ExtractingSqliteDatabase(object sender, RoutingIndexBuilderEventArgs e)
        {
            if (e.ProcessedRecordCount % 100 == 0 || e.ProcessedRecordCount == e.TotalRecordCount)
            {
                this.BeginInvoke(new Action(() =>
                {
                    lblProcessingMessage.Text = string.Format("Generating Index (1/3): Extracting source file: {0}/{1}", e.ProcessedRecordCount, e.TotalRecordCount);
                    pgbBuildProgress.Value = (int)(((double)e.ProcessedRecordCount / (double)e.TotalRecordCount) * 100); ;
                }));
            }
        }

        private void routingIndexbuilder_BuildingRoutableSegment(object sender, RoutingIndexBuilderEventArgs e)
        {
            if (e.ProcessedRecordCount % 100 == 0 || e.ProcessedRecordCount == e.TotalRecordCount)
            {
                this.BeginInvoke(new Action(() =>
                {
                    lblProcessingMessage.Text = string.Format("Generating Index (2/3) Building Routable sqlite file: {0}/{1}", e.ProcessedRecordCount, e.TotalRecordCount);
                    pgbBuildProgress.Value = (int)(((double)e.ProcessedRecordCount / (double)e.TotalRecordCount) * 100);
                }));
            }
        }

        private void routingIndexbuilder_BuildingRouteSegment(object sender, RoutingIndexBuilderEventArgs e)
        {
            if (e.ProcessedRecordCount % 100 == 0 || e.ProcessedRecordCount == e.TotalRecordCount)
            {
                this.BeginInvoke(new Action(() =>
                {
                    lblProcessingMessage.Text = string.Format("Generating Index (3/3) Building rtg file: {0}/{1}", e.ProcessedRecordCount, e.TotalRecordCount);
                    pgbBuildProgress.Value = (int)(((double)e.ProcessedRecordCount / (double)e.TotalRecordCount) * 100);
                }));
            }
        }

        private void btnSaveRtgFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = Path.ChangeExtension(txtDataProviderFilePath.Text, ".routable.rtg"); ;
            saveFileDialog.Filter = "Routing Index File(*.rtg)|*.rtg";
            saveFileDialog.DefaultExt = ".rtg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtIndexFilePath.Text = saveFileDialog.FileName;
            }
        }

        private void btnSaveRoutableFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = Path.ChangeExtension(txtDataProviderFilePath.Text, ".routable.shp");

            saveFileDialog.Filter = "Shape File(*.shp)|*.shp";
            saveFileDialog.DefaultExt = ".shp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtRoutableFile.Text = saveFileDialog.FileName;
            }
        }

        private void SpeedTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            lblRoadClassColumn.Enabled = rbtnClassSpeed.Checked;
            cmbRoadClassColumn.Enabled = rbtnClassSpeed.Checked;
            dgvRoadClassSpeed.Enabled = rbtnClassSpeed.Checked;
            cmbSpeedColumn.Enabled = rbtnRoadSpeed.Checked;
        }

        private void cmbRouteIndexType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool considerringSpeedLimit = cmbRouteIndexType.SelectedItem.ToString() == "Fastest";
            panelRouteSpeedOptions.Enabled = considerringSpeedLimit;
        }

        private void cmbSpeedUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpeedUnit.SelectedIndex == 0)
            {
                txtDefaultSpeed.Text = "50";
            }
            else
            {
                txtDefaultSpeed.Text = "30";
            }
            BindRoadClassSpeed();
        }

        private void cmbRoutableFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoutableFileType.SelectedItem.ToString().Equals("ShapeFile"))
            {
                txtRoutableFile.Text = Path.ChangeExtension(txtRoutableFile.Text, ".shp");
            }
            else
            {
                txtRoutableFile.Text = Path.ChangeExtension(txtRoutableFile.Text, ".sqlite");
            }
        }

        private void cmbOnewayRoadColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool oneWayIdentifierIsNotNull = (cmbOnewayRoadColumn.SelectedItem.ToString() != string.Empty);

            label8.Enabled = oneWayIdentifierIsNotNull;
            txtOneWayRoadValue.Enabled = oneWayIdentifierIsNotNull;
            panelOneWayIndicator.Enabled = oneWayIdentifierIsNotNull;
        }

        private void cmbOnewayIndicatorColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool oneWayIndicatorColumnNotNull = (cmbOnewayIndicatorColumn.SelectedItem.ToString() != string.Empty);
            panelOneWayIndicatorDetail.Enabled = oneWayIndicatorColumnNotNull;
        }

        private void cmbClosedRoadColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool closedRoadColumnNotNull = (cmbClosedRoadColumn.SelectedItem.ToString() != string.Empty);
            panelClosedRoad.Enabled = closedRoadColumnNotNull;
        }

        private void cmbRoadClassColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRoadClassSpeed();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (routingIndexbuilder != null)
            {
                routingIndexbuilder.CancelBuildingRtgFile();
            }
            this.Close();
        }

        private RoutingIndexBuilderParameters GetIndexBuildingParameters()
        {
            RoutingIndexBuilderParameters buildRoutingIndexParameters = new RoutingIndexBuilderParameters();
            buildRoutingIndexParameters.SourceSqlitePathName = txtDataProviderFilePath.Text;

            buildRoutingIndexParameters.GeographyUnit = (GeographyUnit)Enum.Parse(typeof(GeographyUnit), cmbGeographyUnit.SelectedItem.ToString());
            buildRoutingIndexParameters.RouteIndexType = (RoutingIndexType)Enum.Parse(typeof(RoutingIndexType), cmbRouteIndexType.SelectedItem.ToString());
            buildRoutingIndexParameters.RoutableFileType = (RoutableFileType)Enum.Parse(typeof(RoutableFileType), cmbRoutableFileType.SelectedItem.ToString());
            buildRoutingIndexParameters.RtgFilePathName = txtIndexFilePath.Text;
            buildRoutingIndexParameters.RoutableFilePathName = txtRoutableFile.Text;
            buildRoutingIndexParameters.BuildRoutingDataMode = chkRebuildRtgFile.Checked ? BuildRoutingDataMode.Rebuild : BuildRoutingDataMode.DoNotRebuild;
            buildRoutingIndexParameters.OverrideRoutableFile = chkRebuildRoutableFile.Checked ? true : false;

            //Get restrict extent if specify.
            if (this.txtUpperLeftX.Text.Split(',').Length == 2 && this.txtUpperLeftX.Text.Split(',').Length == 2)
            {
                buildRoutingIndexParameters.RestrictExtent = new RectangleShape(double.Parse(this.txtUpperLeftX.Text.Split(',')[0]), double.Parse(this.txtUpperLeftX.Text.Split(',')[1]), double.Parse(this.txtLowerRightX.Text.Split(',')[0]), double.Parse(this.txtLowerRightX.Text.Split(',')[1]));
                // Check if restrict extent is too large and give it a warning if does.
                if (buildRoutingIndexParameters.RestrictExtent.GetArea(buildRoutingIndexParameters.GeographyUnit, AreaUnit.SquareMiles) > warningAreaInSquareMiles)
                {
                    string routingAreaTooLargeWarning = "The selected area is too big and may take a long time, we suggest making the selected area smaller, do you want to continue anyway?";
                    if (DialogResult.No == MessageBox.Show(routingAreaTooLargeWarning, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        return null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Either Upper Left Point or Lower Right Point is not correctly");
                return null;
            }

            // Get the Speed Options. 
            if (cmbRouteIndexType.SelectedItem.ToString() == "Fastest")
            {
                buildRoutingIndexParameters.SpeedOption.SpeedUnit = (SpeedUnit)Enum.Parse(typeof(SpeedUnit), cmbSpeedUnit.SelectedIndex.ToString());
                buildRoutingIndexParameters.SpeedOption.DefaultSpeed = Convert.ToSingle(txtDefaultSpeed.Text);
                buildRoutingIndexParameters.SpeedOption.SpeedSourceType = rbtnClassSpeed.Checked ? RoadSpeedSourceType.BasedOnRoadType : RoadSpeedSourceType.DefinedInAttribution;
                buildRoutingIndexParameters.SpeedOption.SpeedColumnName = cmbSpeedColumn.Text;
                buildRoutingIndexParameters.SpeedOption.RoadTypeColumnName = cmbRoadClassColumn.Text;
                DataTable dataTable = (DataTable)dgvRoadClassSpeed.DataSource;
                buildRoutingIndexParameters.SpeedOption.RoadSpeeds.Clear();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    buildRoutingIndexParameters.SpeedOption.RoadSpeeds.Add(dataRow[0].ToString(), Convert.ToDouble(dataRow[1]));
                }
            }

            // Get the One way Road Options. 
            buildRoutingIndexParameters.OneWayRoadOption.OneWayColumnName = cmbOnewayRoadColumn.Text;
            if (buildRoutingIndexParameters.OneWayRoadOption.OneWayColumnName != string.Empty)
            {
                buildRoutingIndexParameters.IncludeOnewayOptions = true;
                buildRoutingIndexParameters.OneWayRoadOption.IndicatorColumnName = cmbOnewayIndicatorColumn.Text;
                buildRoutingIndexParameters.OneWayRoadOption.OneWayIdentifier = txtOneWayRoadValue.Text;
                buildRoutingIndexParameters.OneWayRoadOption.StartToEnd = txtFromToValue.Text;
                buildRoutingIndexParameters.OneWayRoadOption.EndToStart = txtToFromValue.Text;
            }

            // Get the closed Road Options.
            buildRoutingIndexParameters.ClosedRoadOption.ClosedColumnName = cmbClosedRoadColumn.Text;
            buildRoutingIndexParameters.ClosedRoadOption.ClosedRoadValue = txtColsedRoad.Text;

            return buildRoutingIndexParameters;
        }

        private void ShowErrorMessageBox()
        {
            MessageBox.Show(this, "The chosen data doesn't include line shapes. Please choose another one.", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private string GetSelectFileFilter()
        {
            string filter = string.Empty;
            string allExName = string.Empty;
            List<string> fileExNames = new List<string>();

            foreach (var item in configModelsDictionary)
            {
                if (string.IsNullOrEmpty(filter)) filter += item.Value.FileType;
                else
                {
                    filter += "|" + item.Value.FileType;
                }

                if (string.IsNullOrEmpty(allExName)) allExName += item.Key;
                else
                {
                    allExName += ";" + item.Key;
                }

                fileExNames.Add(item.Value.FileType.Split('|').Last());
            }

            filter += string.Format("|All Surpported Files({0})|{0}", allExName);

            return filter;
        }

        private void BindRoadClassSpeed()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("RoadClass");
            dataTable.Columns.Add("Speed");

            Dictionary<string, float> roadSpeed = currentConfigModel.RoadSpeedForKPH;
            if (cmbSpeedUnit.SelectedIndex == 1)
            {
                roadSpeed = currentConfigModel.RoadSpeedForMPH;
            }
            foreach (var item in roadSpeed)
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = item.Key;
                dr[1] = item.Value;
                dataTable.Rows.Add(dr);
            }
            dgvRoadClassSpeed.DataSource = dataTable;
        }

        private void InitCombColumns(ComboBox comboBox, Collection<FeatureSourceColumn> columns, string defaultValue)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("");
            foreach (FeatureSourceColumn item in columns)
            {
                comboBox.Items.Add(item.ColumnName);
                if (item.ColumnName.Equals(defaultValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    comboBox.SelectedIndex = comboBox.Items.Count - 1;
                }
            }
        }
    }
}
