using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;
using ThinkGeo.MapSuite;

namespace ThinkGeo.MapSuite.RoutingSamples
{
    public partial class Samples : Form
    {
        enum CodeType
        {
            CSharp,
            VisualBasic
        }

        public static readonly string rootDirectory = Path.GetFullPath(ConfigurationManager.AppSettings["RootDirectory"]);

        private readonly string mainFolder = ((new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent).FullName + "\\";
        private const string preStart = "<body oncontextmenu='return false;'><div class='divbody'><pre name='code' class='c-sharp:nocontrols'>";
        private const string preEndFormat = "</pre></div><link type='text/css' rel='stylesheet' href='{0}\\SyntaxHighlighter\\SyntaxHighlighter.css'></link><script language='javascript' src='{0}\\SyntaxHighlighter\\shCore.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter/shBrushCSharp.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter\\shBrushXml.js'></script><script language='javascript'>dp.SyntaxHighlighter.HighlightAll('code');</script></body>";
        private string currentSample;
        private GeoCollection<TreeNode> keywords;

        public Samples()
        {
            InitializeComponent();
        }

        private void Samples_Load(object sender, EventArgs e)
        {
            treeViewLeft.Nodes["RootNode"].ExpandAll();
            keywords = new GeoCollection<TreeNode>();
            GenerateKeywords(treeViewLeft.Nodes);
        }

        private void GenerateKeywords(TreeNodeCollection treeNodes)
        {
            foreach (TreeNode item in treeNodes)
            {
                if (item.Tag != null)
                {
                    keywords.Add(item.Tag.ToString(), item);
                }

                if (item.Nodes.Count > 0)
                {
                    GenerateKeywords(item.Nodes);
                }
            }
        }

        private void treeViewLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                tabctrlShow.SelectedTab = tabpageSample;
                currentSample = e.Node.Name;
                pnlOption.Controls.Clear();

                switch (e.Node.Name)
                {
                    case "SimpleRouting":
                        pnlOption.Controls.Add(new SimpleRouting());
                        break;
                    case "ExtendingRoutingSource":
                        pnlOption.Controls.Add(new ExtendingRoutingSource());
                        break;
                    case "IntermediatePointsSupport":
                        pnlOption.Controls.Add(new IntermediatePointsSupport());
                        break;
                    case "RoutingDirections":
                        pnlOption.Controls.Add(new RoutingDirections());
                        break;
                    case "ServiceAreaDefinition":
                        pnlOption.Controls.Add(new ServiceAreaDefinition());
                        break;
                    case "OptimizeRoadType":
                        pnlOption.Controls.Add(new OptimizeRoadType());
                        break;
                    case "OneWayRouting":
                        pnlOption.Controls.Add(new OneWayRouting());
                        break;
                    case "UseDifferentAlgorithms":
                        pnlOption.Controls.Add(new UseDifferentAlgorithms());
                        break;
                    case "GetRoadInformationByRoadId":
                        pnlOption.Controls.Add(new GetRoadInformationByRoadId());
                        break;
                    case "BuildingRoutingData":
                        pnlOption.Controls.Add(new BuildingRoutingData());
                        break;
                    case "BuildingRoutingDataEvent":
                        pnlOption.Controls.Add(new BuildingRoutingDataEvent());
                        break;
                    case "RouteAvoidingCertainArea":
                        pnlOption.Controls.Add(new RouteAvoidingCertainArea());
                        break;
                    case "GetShortestPathByCoordinates":
                        pnlOption.Controls.Add(new GetShortestPathByCoordinates());
                        break;
                    case "RouteOnlyInASpecificArea":
                        pnlOption.Controls.Add(new RouteOnlyInASpecificArea());
                        break;
                    case "EditRoutingIndexFile":
                        pnlOption.Controls.Add(new EditRoutingIndexFile());
                        break;
                    case "RoutingAroundRoadblocks":
                        pnlOption.Controls.Add(new RoutingAroundRoadblocks());
                        break;
                    case "TravelingSalesmanProblemAnalysis":
                        pnlOption.Controls.Add(new TravelingSalesmanProblemAnalysis());
                        break;
                    case "DifferentProjectionsWithRouting":
                        pnlOption.Controls.Add(new DifferentProjectionsWithRouting());
                        break;
                    case "TSPAnalysisWithFixedEnds":
                        pnlOption.Controls.Add(new TSPAnalysisWithFixedEnds());
                        break;
                    case "POIonRoute":
                        pnlOption.Controls.Add(new POIonRoute());
                        break;
                    default:
                        break;
                }
                this.Text = "Winform C# Sample Application - " + e.Node.Text;
            }
        }

        private void tabpageCSCode_Enter(object sender, EventArgs e)
        {
            string uri;
            if (currentSample == "RootNode")
            {
                uri = mainFolder + "MS3 Samples - Welcome Page.html";
            }
            else
            {
                uri = GetHtmlPath(currentSample + ".cs", CodeType.CSharp);
            }
            Uri webUri = new Uri(@uri);
        }

        private void miViewCode_Click(object sender, EventArgs e)
        {
            tabctrlShow.SelectedTab = tabpageCSCode;
        }

        private void tabctrlShow_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Image image = new Bitmap(Properties.Resources.TabButtonOn);
            for (int i = 0; i < tabctrlShow.TabPages.Count; i++)
            {
                g.DrawImage(image, tabctrlShow.GetTabRect(i));
                Rectangle rectSample = tabctrlShow.GetTabRect(i);
                rectSample.Offset(25, 4);
                g.DrawString(tabctrlShow.TabPages[i].Text, new Font("Microsoft Sans Serif", 8.25f), new SolidBrush(Color.Black), rectSample);
            }
        }

        private void tmrRotator_Tick(object sender, EventArgs e)
        {
        }

        private string GetHtmlPath(string filename, CodeType codeType)
        {
            string myDocuments = Environment.GetEnvironmentVariable("TEMP");

            string path = myDocuments;
            if (codeType == CodeType.CSharp)
            {
                path = path + "\\" + "MapSuiteCSharpHowDoISamples";
            }
            else
            {
                path = path + "\\" + "MapSuiteVBHowDoISamples";
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] files = Directory.GetFiles(mainFolder, filename, SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                return mainFolder + "MS3 Samples - Source Code Not Available in Beta.html";
            }

            string tmpFileName = Path.GetFileNameWithoutExtension(new FileInfo(files[0]).Name) + ".htm";
            string htmlFileName = path + "\\" + tmpFileName;
            if (File.Exists(htmlFileName))
            {
                return htmlFileName;
            }

            string text;
            try
            {
                using (StreamReader streamReader = new StreamReader(files[0]))
                {
                    text = streamReader.ReadToEnd();
                }
                text = preStart + RemoveRegion(text, codeType) + string.Format(preEndFormat, mainFolder);

                using (StreamWriter streamWriter = new StreamWriter(htmlFileName))
                {
                    streamWriter.Write(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }

            return htmlFileName;
        }

        private static string RemoveRegion(string inputText, CodeType codeType)
        {
            string regionPattern, endregionPattern;
            if (codeType == CodeType.CSharp)
            {
                regionPattern = "#region Component Designer generated code";
                endregionPattern = "#endregion";
            }
            else
            {
                regionPattern = "#Region \"Component Designer generated code\"";
                endregionPattern = "#End Region";
            }

            int indexOfRegion = inputText.IndexOf(regionPattern, StringComparison.Ordinal);
            if (indexOfRegion == -1)
            {
                return inputText;
            }
            string firstPart = inputText.Substring(0, indexOfRegion).Trim();

            int indexOfEndregion = inputText.IndexOf(endregionPattern, StringComparison.Ordinal);
            if (indexOfEndregion == -1 || indexOfEndregion <= indexOfRegion)
            {
                return inputText;
            }
            string secondPart = inputText.Substring(indexOfEndregion + endregionPattern.Length);
            return firstPart + secondPart;
        }

        private void lnkProductInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Products/GISComponentsforNETDevelopers/MapSuiteRouting/tabid/723/Default.aspx");
        }

        private void lnkDiscussionForums_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=143");
        }

        private void lnkOnlineStore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/store/");
        }

        private void lnkFAQ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Products/GISComponentsforNETDevelopers/MapSuiteRouting/FAQ/tabid/724/Default.aspx");
        }

        private void lnkTestimonials_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=130");
        }

        private void lnkContactUs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=147");
        }

        private void FindNodeInHierarchy(TreeNodeCollection nodes, string strSearchValue)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                //if (nodes[i].Text.ToUpper().Contains(strSearchValue.ToUpper()))
                //{
                //    lbxSearchResult.Items.Add(nodes[i]);
                //}

                foreach (string item in keywords.GetKeys())
                {
                    if (item.ToUpperInvariant().Contains(strSearchValue))
                    {
                        if (!lbxSearchResult.Items.Contains(keywords[item]))
                        {
                            lbxSearchResult.Items.Add(keywords[item]);
                        }
                    }
                }

                if (nodes[i].Nodes.Count > 0)
                {
                    FindNodeInHierarchy(nodes[i].Nodes, strSearchValue);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbxSearchResult.Items.Clear();
            FindNodeInHierarchy(treeViewLeft.Nodes, txtSearch.Text.ToUpperInvariant());
            //lbxSearchResult.Visible = true;
            treeViewLeft.Visible = false;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                btnSearch_Click(txtSearch, e);
            }
        }

        private void btnHideResult_Click(object sender, EventArgs e)
        {
            //lbxSearchResult.Visible = false;
            treeViewLeft.Visible = true;
        }

        private void lbxSearchResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = lbxSearchResult.SelectedItem as TreeNode;
            treeViewLeft_AfterSelect(this, new TreeViewEventArgs(node));
        }

        private void txtSearch_GotFocus(object sender, System.EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }
    }
}


