using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace TopologyValidation
{
    public partial class MainForm : Form
    {
        TopologyTestCase currentTopologyTestCase;
        Collection<TopologyTestCase> loadedTopologyTestCases = new Collection<TopologyTestCase>();
        string testCasesFilePath = ConfigurationManager.AppSettings.Get("DefaultTestCasesFilePath");
        private bool fromEvent = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetToolTip();
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.None;
            LoadTestCases();

            winformsMap1.CurrentScale = new ZoomLevelSet().ZoomLevel10.Scale;
            currentTopologyTestCase = new PolygonsMustNotOverlapTopologyTestCase();
            winformsMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(TrackOverlay_TrackEnded);
            if (listboxNames.Items.Count > 0)
            {
                listboxNames.SelectedIndex = 0;
                txtCaseDescription.Text = loadedTopologyTestCases[listboxNames.SelectedIndex].Description;
            }
            cmbTopologyRules.SelectedIndex = 0;
            listboxNames.SelectedIndex = 0;
        }

        private void SetToolTip()
        {
            toolTip1.SetToolTip(btnRunTestCase, "Run Current Case");
            toolTip1.SetToolTip(btnImportExportWKT, "Import/Export WKT");
            toolTip1.SetToolTip(btnSaveTestCase, "Save Case");
            toolTip1.SetToolTip(btnSaveTestCaseInCurrentExtent, "Save Case Within Current Extent");
            toolTip1.SetToolTip(btnDeleteTestCase, "Delete Current Case");
            toolTip1.SetToolTip(btnTrackNormal, "Normal");
            toolTip1.SetToolTip(btnTrackPoint, "Point");
            toolTip1.SetToolTip(btnTrackLine, "Line");
            toolTip1.SetToolTip(btnTrackCoveredLine, "Covered line");
            toolTip1.SetToolTip(btnTrackPolygon, "Polygon");
            toolTip1.SetToolTip(btnTrackCoveredPolygon, "Covered polygon");
            toolTip1.SetToolTip(btnTrackDelete, "Clear");
        }

        private void cmbTopologyRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
            currentTopologyTestCase = GetTestCaseInstance(cmbTopologyRules.SelectedItem.ToString());
            if (!fromEvent)
            {
                listboxNames.SelectedIndex = -1;
            }
            else
            {
                fromEvent = false;
            }
            UpdateEditButtons();

            if (currentTopologyTestCase == null)
            {
                MessageBox.Show("The test case is not implemented");
            }
            ClearAll();
        }

        private void UpdateEditButtons()
        {
            btnTrackLine.Enabled = false;
            btnTrackPoint.Enabled = false;
            btnTrackPolygon.Enabled = false;
            btnTrackCoveredLine.Enabled = false;
            btnTrackCoveredPolygon.Enabled = false;

            switch (currentTopologyTestCase.TestCaseInputType)
            {
                case TestCaseInputType.Line:
                    EnableEditButtons(btnTrackLine);
                    break;
                case TestCaseInputType.LineAndLine:
                    EnableEditButtons(btnTrackLine, btnTrackCoveredLine);
                    break;
                case TestCaseInputType.LineAndPolygon:
                    EnableEditButtons(btnTrackLine, btnTrackPolygon);
                    break;
                case TestCaseInputType.Point:
                    EnableEditButtons(btnTrackPoint);
                    break;
                case TestCaseInputType.PointAndLine:
                    EnableEditButtons(btnTrackPoint, btnTrackLine);
                    break;
                case TestCaseInputType.PointAndPolygon:
                    EnableEditButtons(btnTrackPoint, btnTrackPolygon);
                    break;
                case TestCaseInputType.Polygon:
                    EnableEditButtons(btnTrackPolygon);
                    break;
                case TestCaseInputType.PolygonAndPolygon:
                    EnableEditButtons(btnTrackPolygon, btnTrackCoveredPolygon);
                    break;
                case TestCaseInputType.PointAndLineAndPolygon:
                default:
                    EnableEditButtons(btnTrackPoint, btnTrackLine, btnTrackPolygon);
                    break;
            }
        }

        private void EnableEditButtons(params Button[] buttons)
        {
            foreach (var item in buttons)
            {
                item.Enabled = true;
            }
        }

        private void btnTopologyResult_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (currentTopologyTestCase is LinesMustBeLargerThanClusterToleranceTopologyTestCase)
                {
                    ToleranceForm form = new ToleranceForm();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ((LinesMustBeLargerThanClusterToleranceTopologyTestCase)currentTopologyTestCase).Tolerance = form.Tolerance;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (currentTopologyTestCase is PolygonsMustBeLargerThanClusterToleranceTopologyTestCase)
                {
                    ToleranceForm form = new ToleranceForm();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ((PolygonsMustBeLargerThanClusterToleranceTopologyTestCase)currentTopologyTestCase).Tolerance = form.Tolerance;
                    }
                    else
                    {
                        return;
                    }
                }
                currentTopologyTestCase.GenerateTestResult();
                winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                currentTopologyTestCase.Draw(winformsMap1);

                string wkt = currentTopologyTestCase.GetOutputInString();
                if (string.IsNullOrEmpty(wkt))
                {
                    MessageBox.Show(string.Format("No result returned and no WKT is exported."));
                }
                else
                {
                    Clipboard.SetDataObject(wkt);
                    MessageBox.Show(string.Format("The WKT of {0} return records has been copied to Clipboard", currentTopologyTestCase.OutputFeatureLayer.InternalFeatures.Count), "Note");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnImportSingleWKT_Click(object sender, EventArgs e)
        {
            ShapesWktLoader wktImporter = new ShapesWktLoader(currentTopologyTestCase);
            if (wktImporter.ShowDialog() == DialogResult.OK)
            {
                ClearAll();
                if (currentTopologyTestCase is OneInputLayerTopologyTestCase)
                {
                    foreach (var item in wktImporter.Features[0])
                    {
                        ((OneInputLayerTopologyTestCase)currentTopologyTestCase).InputFeatureLayer.InternalFeatures.Add(item);
                    }
                }
                else if (currentTopologyTestCase is TwoInputLayersTopologyTestCase)
                {
                    foreach (var item in wktImporter.Features[0])
                    {
                        ((TwoInputLayersTopologyTestCase)currentTopologyTestCase).FirstInputFeatureLayer.InternalFeatures.Add(item);
                    }
                    foreach (var item in wktImporter.Features[1])
                    {
                        ((TwoInputLayersTopologyTestCase)currentTopologyTestCase).SecondInputFeatureLayer.InternalFeatures.Add(item);
                    }
                }
                winformsMap1.CurrentExtent = currentTopologyTestCase.GetBoundingBox();
                winformsMap1.CurrentExtent.ScaleUp(30);
                currentTopologyTestCase.OutputFeatureLayer.InternalFeatures.Clear();
                currentTopologyTestCase.Draw(winformsMap1);
            }
        }

        private void btnSaveToXML_Click(object sender, EventArgs e)
        {
            SaveTestCaseDialog saveTestCaseDialog = new SaveTestCaseDialog(currentTopologyTestCase.GetType().Name, "", testCasesFilePath, null);

            if (saveTestCaseDialog.ShowDialog() == DialogResult.OK)
            {
                currentTopologyTestCase.Name = saveTestCaseDialog.TestCaseName;
                currentTopologyTestCase.Description = saveTestCaseDialog.Description;
                currentTopologyTestCase.ToXml(testCasesFilePath + "\\" + saveTestCaseDialog.TestCaseName);
                LoadTestCases();
            }
        }

        private void btnDeleteTestCase_Click(object sender, EventArgs e)
        {
            if (listboxNames.SelectedValue != null)
            {
                if (MessageBox.Show("Are you sure you want to delete it?", "Delete Test Case", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string name = ((DataRowView)listboxNames.SelectedItem).Row.ItemArray[0].ToString();
                    if (File.Exists(testCasesFilePath + "\\" + name + ".xml"))
                    {
                        File.Delete(testCasesFilePath + "\\" + name + ".xml");
                    }

                    LoadTestCases();
                }
            }
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            ClearAll();
        }

        private void btnSaveCaseInCurrentExtent_Click(object sender, EventArgs e)
        {
            SaveTestCaseDialog saveTestCaseDialog = new SaveTestCaseDialog(currentTopologyTestCase.GetType().Name, "", testCasesFilePath, winformsMap1.CurrentExtent);

            if (saveTestCaseDialog.ShowDialog() == DialogResult.OK)
            {
                TopologyTestCase topologyTestCase = currentTopologyTestCase.GenerateTestCaseWithinExtent(winformsMap1.CurrentExtent);
                topologyTestCase.Name = saveTestCaseDialog.TestCaseName;
                topologyTestCase.Description = saveTestCaseDialog.Description;
                topologyTestCase.ToXml(testCasesFilePath + "\\" + saveTestCaseDialog.TestCaseName);
                LoadTestCases();
            }
        }

        private void lbTestCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxNames.SelectedValue != null && listboxNames.SelectedValue.ToString() != typeof(System.Data.DataRowView).ToString())
            {
                fromEvent = true;
                txtCaseDescription.Text = loadedTopologyTestCases[listboxNames.SelectedIndex].Description;
                ClearAll();

                TopologyTestCase testCase = loadedTopologyTestCases[listboxNames.SelectedIndex];
                winformsMap1.CurrentExtent = testCase.GetBoundingBox();
                winformsMap1.CurrentExtent.ScaleUp(30);
                string typeName = testCase.GetType().Name.Replace("TopologyTestCase", "");

                int count = 0;
                foreach (var item in cmbTopologyRules.Items)
                {
                    if (item.ToString() == typeName)
                    {
                        break;
                    }
                    count++;
                }
                cmbTopologyRules.SelectedIndex = count;

                testCase.Draw(winformsMap1);
                currentTopologyTestCase = testCase.Clone();
            }
        }

        private void TrackButton_Click(object sender, System.EventArgs e)
        {
            Button button = sender as Button;
            var twoInputTestCase = currentTopologyTestCase as TwoInputLayersTopologyTestCase;

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
                        if (btnTrackCoveredLine.Enabled)
                        {
                            twoInputTestCase.InputFeatureLayerType = InputFeatureLayerType.First;
                            winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                            winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = twoInputTestCase.FirstInputFeatureLayer.ZoomLevelSet;
                            twoInputTestCase.Draw(winformsMap1);
                        }
                        else
                        {
                            if (twoInputTestCase != null)
                            {
                                winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = twoInputTestCase.FirstInputFeatureLayer.ZoomLevelSet;
                            }
                            else
                            {
                                winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = ((OneInputLayerTopologyTestCase)currentTopologyTestCase).InputFeatureLayer.ZoomLevelSet;
                            }
                        }
                        break;
                    case "btnTrackCoveredLine":
                        twoInputTestCase.InputFeatureLayerType = InputFeatureLayerType.Second;
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Line;
                        winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                        winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = twoInputTestCase.SecondInputFeatureLayer.ZoomLevelSet;
                        twoInputTestCase.Draw(winformsMap1);
                        break;
                    case "btnTrackPolygon":
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
                        if (btnTrackCoveredPolygon.Enabled)
                        {
                            twoInputTestCase.InputFeatureLayerType = InputFeatureLayerType.First;
                            winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                            winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = twoInputTestCase.FirstInputFeatureLayer.ZoomLevelSet;
                            twoInputTestCase.Draw(winformsMap1);
                        }
                        else
                        {
                            if (twoInputTestCase != null)
                            {
                                winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = twoInputTestCase.FirstInputFeatureLayer.ZoomLevelSet;
                            }
                            else
                            {
                                winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = ((OneInputLayerTopologyTestCase)currentTopologyTestCase).InputFeatureLayer.ZoomLevelSet;
                            }
                        }
                        break;
                    case "btnTrackCoveredPolygon":
                        twoInputTestCase.InputFeatureLayerType = InputFeatureLayerType.Second;
                        winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
                        winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                        winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet = twoInputTestCase.SecondInputFeatureLayer.ZoomLevelSet;
                        twoInputTestCase.Draw(winformsMap1);
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

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            currentTopologyTestCase.AddInputShape(e.TrackShape);
        }

        private void ClearAll()
        {
            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
            winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            winformsMap1.Overlays.Clear();
            winformsMap1.Refresh();
            currentTopologyTestCase.Clear();
            winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
        }

        private void LoadTestCases()
        {
            loadedTopologyTestCases.Clear();
            listboxNames.DataSource = null;
            listboxNames.Items.Clear();

            if (Directory.Exists(testCasesFilePath))
            {
                Collection<TopologyTestCase> testCases = LoadTestCases(testCasesFilePath);
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("name", typeof(string)));
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("description", typeof(string)));

                foreach (TopologyTestCase testCase in testCases)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = testCase.Name;
                    dr["id"] = testCase.Id;
                    dr["description"] = testCase.Description;
                    dt.Rows.Add(dr);
                    loadedTopologyTestCases.Add(testCase);
                }

                listboxNames.DataSource = dt;
                listboxNames.DisplayMember = "name";
                listboxNames.ValueMember = "id";
            }
        }

        private Collection<string> GetTestCases()
        {
            Assembly assembly = Assembly.LoadFrom("TopologyValidation.exe");
            Collection<string> testCases = new Collection<string>();

            Type[] types = assembly.GetTypes();

            foreach (var item in types)
            {
                if (item.Name.EndsWith("TestCase") && item.Name != "TopologyTestCase")
                {
                    testCases.Add(item.FullName);
                }
            }
            return testCases;
        }

        public TopologyTestCase GetTestCaseInstance(string typeName)
        {
            Collection<string> testCases = GetTestCases();
            typeName = "TopologyValidation." + typeName + "TopologyTestCase";

            if (testCases.Contains(typeName))
            {
                return (TopologyTestCase)Activator.CreateInstance(Type.GetType(typeName));
            }
            return null;
        }

        public Collection<TopologyTestCase> LoadTestCases(string path)
        {
            Collection<TopologyTestCase> testCases = new Collection<TopologyTestCase>();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            foreach (var item in dirInfo.GetFiles("*.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(item.FullName);
                XmlNode rootNode = doc.SelectSingleNode("//Case");
                Collection<string> topologyTestCases = GetTestCases();

                XmlNode nodeTypeName = rootNode.SelectSingleNode("./TypeName");
                TopologyTestCase topologyTestCase = null;

                if (topologyTestCases.Contains(nodeTypeName.InnerText))
                {
                    topologyTestCase = (TopologyTestCase)Activator.CreateInstance(Type.GetType(nodeTypeName.InnerText));
                }

                topologyTestCase.FromXml(rootNode);
                testCases.Add(topologyTestCase);
            }

            return testCases;
        }
    }
}
