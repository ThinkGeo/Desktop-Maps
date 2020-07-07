using NetTopologySuite.GeometriesGraph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class Samples : Form
    {
        private readonly string mainFolder = ((new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent).FullName + "\\";
        private const string preStart = "<body oncontextmenu='return false;'><div class='divbody'><pre name='code' class='c-sharp:nocontrols'>";
        private const string preEndFormat = "</pre></div><link type='text/css' rel='stylesheet' href='{0}\\SyntaxHighlighter\\SyntaxHighlighter.css'></link><script language='javascript' src='{0}\\SyntaxHighlighter\\shCore.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter/shBrushCSharp.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter\\shBrushXml.js'></script><script language='javascript'>dp.SyntaxHighlighter.HighlightAll('code');</script></body>";
        List<MenuModel> menus;

        public Samples()
        {
            InitializeComponent();
        }

        private void Samples_Load(object sender, EventArgs e)
        {            
            pnlOption.Resize += PnlOption_Resize;

            menus = JsonConvert.DeserializeObject<List<MenuModel>>(File.ReadAllText("samples.json"));
            TreeNode[] treeNodes = GetTreeNodes(menus);
            this.treeViewLeft.Nodes.AddRange(treeNodes);

            treeViewLeft.BeginUpdate();
            treeViewLeft.Nodes[0].Expand();
            treeViewLeft.EndUpdate();
        }

        private TreeNode[] GetTreeNodes(List<MenuModel> menus)
        {
            TreeNode[] treeNodes = new TreeNode[menus.Count];
            for (int i = 0; i < menus.Count; i++)
            {
                treeNodes[i] = new TreeNode(menus[i].Title);
                foreach (MenuModel subMenu in menus[i].Children)
                {
                    TreeNode treeNode = new TreeNode(subMenu.Title);                   
                    treeNodes[i].Nodes.Add(treeNode);
                }
            }
            return treeNodes;
        }

        private MenuModel GetMenuModelByTitle(List<MenuModel> menus, string title)
        {
            MenuModel result = new MenuModel();
            foreach (MenuModel menu in menus)
            {
                if (menu.Title == title)
                {
                    result = menu;
                    break;
                }
                else
                {
                    foreach (MenuModel subMenu in menu.Children)
                    {
                        if (subMenu.Title == title)
                        {
                            result = subMenu;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private void PnlOption_Resize(object sender, EventArgs e)
        {
            foreach (Control control in pnlOption.Controls)
            {
                control.Size = pnlOption.Size;
                control.Refresh();
            }
        }

        private void treeViewLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MenuModel selectedNode = GetMenuModelByTitle(menus, e.Node.Text);
            if (selectedNode.Id == null)
            {
                selectedNode = selectedNode.Children[0];
            }
            var userControlType = Type.GetType(selectedNode.Id);
            UserControl currentUserControl = (UserControl)Activator.CreateInstance(userControlType);
         
            currentUserControl.Size = pnlOption.Size;
            pnlOption.Controls.Clear();
            pnlOption.Controls.Add(currentUserControl);
        
            this.labelSampleName.Text = selectedNode.Title;
            this.labelSampleDescription.Text = selectedNode.Description;

            //string uri = GetHtmlPath(selectedNode.Source + ".cs");
            //Uri webUri = new Uri(@uri);
            //cSharpBrowser.Url = webUri;
        }

        private string GetHtmlPath(string filename)
        {
            if (String.Compare(filename, "Load a Geotiff image.cs", true, CultureInfo.InvariantCulture) == 0)
            {
                filename = "LoadAStandardImageWithWorldFile.cs";
            }
            else if (String.Compare(filename, "ConvertGeoColorToOtherColors.cs", true, CultureInfo.InvariantCulture) == 0)
            {
                filename = "ConvertAGeoColorToAndFromOleWin32HtmlArgbColors.cs";
            }
            string myDocuments = Environment.GetEnvironmentVariable("TEMP");

            string path = myDocuments + "\\" + "MapSuiteCSharpHowDoISamples";

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
                text = preStart + RemoveRegion(text) + string.Format(preEndFormat, mainFolder);

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

        private static string RemoveRegion(string inputText)
        {
            string regionPattern, endregionPattern;
            regionPattern = "#region Component Designer generated code";
            endregionPattern = "#endregion";

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

        private void btnSource_Click(object sender, EventArgs e)
        {
            splitContainerSampleSource.Panel1Collapsed = !splitContainerSampleSource.Panel1Collapsed;
        }

        private void linkLabelSupport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://community.thinkgeo.com/");
        }

        private void treeViewLeft_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            MenuModel selectedNode = GetMenuModelByTitle(menus, e.Node.Text);
            if (selectedNode.Id == null)
            {
                e.Cancel = true;
            }
        }
    }
}


