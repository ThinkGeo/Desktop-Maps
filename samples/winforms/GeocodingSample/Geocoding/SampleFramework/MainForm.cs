using System;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Globalization;

namespace Geocoding
{
    public partial class MainForm : Form
    {
        enum CodeType
        {
            CSharp,
            VisualBasic
        }

        private readonly string mainFolder = ((new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent).FullName + "\\SampleFramework\\";
        private const string preStart = "<body oncontextmenu='return false;'><div class='divbody'><pre name='code' class='c-sharp:nocontrols'>";
        private const string preEndFormat = "</pre></div><link type='text/css' rel='stylesheet' href='{0}\\SyntaxHighlighter\\SyntaxHighlighter.css'></link><script language='javascript' src='{0}\\SyntaxHighlighter\\shCore.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter/shBrushCSharp.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter\\shBrushXml.js'></script><script language='javascript'>dp.SyntaxHighlighter.HighlightAll('code');</script></body>";
        private string currentSample;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Samples_Load(object sender, EventArgs e)
        {
            treeViewLeft.Nodes["RootNode"].Expand();
            treeViewLeft.Nodes["RootNode"].Nodes[0].Expand();
        }

        private void treeViewLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tabctrlShow.SelectedTab = tabpageSample;
            currentSample = e.Node.Name;
            pnlOption.Controls.Clear();

            switch (e.Node.Name)
            {
                case "GeocodingInTexas":
                    pnlOption.Controls.Add(new GeocodingInTexas());
                    break;
                case "ReverseGeocodingInTexas":
                    pnlOption.Controls.Add(new ReverseGeocodingInTexas());
                    break;
                case "BatchGeocodingInTexas":
                    pnlOption.Controls.Add(new BatchGeocodingInTexas());
                    break;
                case "BuildIndexFile":
                    pnlOption.Controls.Add(new BuildIndexFile());
                    break;
                case "CreateTextFileMatchingPlugIn":
                    pnlOption.Controls.Add(new CreateTextFileMatchingPlugIn());
                    break;
                case "CreateDatabaseMatchingPlugin":
                    pnlOption.Controls.Add(new CreateDatabaseMatchingPlugIn());
                    break;
                case "FuzzyLogicSearching":
                    pnlOption.Controls.Add(new FuzzyLogicSearching());
                    break;
                case "CensusTractSearching":
                    pnlOption.Controls.Add(new CensusTractSearching());
                    break;
                case "CensusBlockSearching":
                    pnlOption.Controls.Add(new CensusBlockSearching());
                    break;
                case "CensusBlockGroupSearching":
                    pnlOption.Controls.Add(new CensusBlockGroupSearching());
                    break;
                case "PostCodeSearching":
                    pnlOption.Controls.Add(new PostCodeSearching());
                    break;    
                case "GetClosestStreetNumberInTexas":
                    pnlOption.Controls.Add(new GetClosestStreetNumberInTexas());
                    break;
                case "IpAddressMatchingPlugin":
                    pnlOption.Controls.Add(new IpAddressMatchingPlugIn());
                    break;
                default:
                    break;
            }
            Text = "Geocoder C# Sample Application - " + e.Node.Text;
        }

        private void tabpageCSCode_Enter(object sender, EventArgs e)
        {
            string uri;
            if (currentSample == "RootNode" || currentSample == "BasicSamples" || currentSample == "AdvancedSamples")
            {
                uri = mainFolder + "MS3 Samples - Welcome Page.html";
            }
            else
            {
                uri = GetHtmlPath(currentSample + ".cs", CodeType.CSharp);
            }
            Uri webUri = new Uri(@uri);
            cSharpBrowser.Url = webUri;
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

            string[] files = Directory.GetFiles(new DirectoryInfo(mainFolder).Parent.FullName, filename, SearchOption.AllDirectories);

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
                text = preStart + RemoveRegion(text, codeType) + string.Format(CultureInfo.InvariantCulture, preEndFormat, mainFolder);

                using (StreamWriter streamWriter = new StreamWriter(htmlFileName))
                {
                    streamWriter.Write(text);
                }
            }
            catch (IOException ex)
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
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Products/GISComponentsforNETDevelopers/MapSuiteGeocoder/tabid/730/Default.aspx");
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
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Products/GISComponentsforNETDevelopers/MapSuiteGeocoder/FAQ/tabid/741/Default.aspx");
        }

        private void lnkTestimonials_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=130");
        }

        private void lnkContactUs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=147");
        }        

        private void treeViewLeft_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Name == "AdvancedSamples")
            {
                FormAdvancedSampleTips advancedSampleTips = new FormAdvancedSampleTips();
                advancedSampleTips.ShowDialog();
            }
        }

    }
}


