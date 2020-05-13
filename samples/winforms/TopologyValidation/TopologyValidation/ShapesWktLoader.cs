using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public partial class ShapesWktLoader : Form
    {
        private bool isTwoInput;
        Collection<Collection<Feature>> features;
        private string wkt1;
        private string wkt2;

        public Collection<Collection<Feature>> Features
        {
            get { return features; }
        }

        public ShapesWktLoader(TopologyTestCase testCase)
        {
            InitializeComponent();
            if (testCase is OneInputLayerTopologyTestCase)
            {
                isTwoInput = false;
                wkt1 = GetWktFromFeatureLayer(((OneInputLayerTopologyTestCase)testCase).InputFeatureLayer);
            }
            else if (testCase is TwoInputLayersTopologyTestCase)
            {
                isTwoInput = true;
                wkt1 = GetWktFromFeatureLayer(((TwoInputLayersTopologyTestCase)testCase).FirstInputFeatureLayer);
                wkt2 = GetWktFromFeatureLayer(((TwoInputLayersTopologyTestCase)testCase).SecondInputFeatureLayer);
            }
            else
            {
                throw new Exception("TestCase not supported.");
            }
        }

        private void WktImporter_Load(object sender, EventArgs e)
        {
            features = new Collection<Collection<Feature>>();

            if (isTwoInput)
            {
                listBox1.Items.Add("First Layer");
                listBox1.Items.Add("Second Layer");
                secondLayerWktTextBox.Text = wkt2;
            }
            else
            {
                listBox1.Items.Add("Input Layer");
            }
            tbWkt.Text = wkt1;
            listBox1.SelectedIndex = 0;
            fromWktRadio.Checked = true;
            secondFromWktRadio.Checked = true;
        }

        private string GetWktFromFeatureLayer(InMemoryFeatureLayer inMemoryFeatureLayer)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            if (inMemoryFeatureLayer.InternalFeatures.Count > 0)
            {
                foreach (Feature feature in inMemoryFeatureLayer.InternalFeatures)
                {
                    sb.AppendLine(feature.GetWellKnownText() + ";");
                }

                result = sb.ToString();
                result = result.Remove(result.LastIndexOf(";"));
            }

            return result;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (fromWktRadio.Checked)
            {
                wkt1 = tbWkt.Text;
                features.Add(GetFeaturesFromWkt(wkt1));
            }
            else
            {
                try
                {
                    ShapeFileFeatureLayer layer = new ShapeFileFeatureLayer(shapeFilePathTextBox.Text, GeoFileReadWriteMode.Read);
                    layer.Open();
                    features.Add(layer.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns));
                    layer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
            }

            if (secondFromWktRadio.Checked)
            {
                wkt2 = secondLayerWktTextBox.Text;
                features.Add(GetFeaturesFromWkt(wkt2));
            }
            else
            {
                try
                {
                    ShapeFileFeatureLayer layer = new ShapeFileFeatureLayer(secondLayerShapeFileTextBox.Text, GeoFileReadWriteMode.Read);
                    layer.Open();
                    features.Add(layer.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns));
                    layer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private Collection<Feature> GetFeaturesFromWkt(string wkt)
        {
            Collection<Feature> results = new Collection<Feature>();
            string[] xmls = wkt.Split(';');

            for (int i = 0; i < xmls.Length; i++)
            {
                if (xmls[i] != "")
                {
                    results.Add(new Feature(xmls[i]));
                }
            }
            return results;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 1)
            {
                firstGroupBox.Enabled = false;
                firstGroupBox.Visible = false;
                secondGroupBox.Visible = true;
                secondGroupBox.Enabled = true;
            }
            else
            {
                firstGroupBox.Enabled = true;
                firstGroupBox.Visible = true;
                secondGroupBox.Visible = false;
                secondGroupBox.Enabled = false;
            }
        }

        private void fromWktRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (fromWktRadio.Checked)
            {
                SetControlEnabledStatus(false, shapeFilePathTextBox, browseButton);
                SetControlEnabledStatus(true, tbWkt);
            }
            else
            {
                SetControlEnabledStatus(true, shapeFilePathTextBox, browseButton);
                SetControlEnabledStatus(false, tbWkt);
            }
        }

        private void SetControlEnabledStatus(bool enabled, params Control[] controls)
        {
            foreach (var item in controls)
            {
                item.Enabled = enabled;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.RestoreDirectory = true;
            open.Filter = "Shape Files (*.shp) | *.shp";
            open.Multiselect = false;

            if (open.ShowDialog() == DialogResult.OK)
            {
                shapeFilePathTextBox.Text = open.FileName;
            }
        }

        private void secondBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.RestoreDirectory = true;
            open.Filter = "Shape Files (*.shp) | *.shp";
            open.Multiselect = false;

            if (open.ShowDialog() == DialogResult.OK)
            {
                secondLayerShapeFileTextBox.Text = open.FileName;
            }
        }

        private void secondFromWktRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (secondFromWktRadio.Checked)
            {
                SetControlEnabledStatus(false, secondLayerShapeFileTextBox, secondBrowseButton);
                SetControlEnabledStatus(true, secondLayerWktTextBox);
            }
            else
            {
                SetControlEnabledStatus(true, secondLayerShapeFileTextBox, secondBrowseButton);
                SetControlEnabledStatus(false, secondLayerWktTextBox);
            }
        }
    }
}
