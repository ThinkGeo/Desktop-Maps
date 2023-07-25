using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class Samples : Form
    {
        List<MenuModel> menus;

        public Samples()
        {
            InitializeComponent();
        }

        private void Samples_Load(object sender, EventArgs e)
        {
            // Load the menu system from the JSON that represents the tree-view               
            menus = JsonConvert.DeserializeObject<List<MenuModel>>(File.ReadAllText("samples.json"));

            // Get all the tree nodes
            TreeNode[] treeNodes = GetTreeNodes(menus);

            //  Add all the tree nodes, we wrap it in an Begin.Updated to more efficiently redraw the tree
            treeViewLeft.BeginUpdate();
            this.treeViewLeft.Nodes.AddRange(treeNodes);

            // Expand and select the first sample
            treeViewLeft.Nodes[0].Expand();
            treeViewLeft.SelectedNode = treeViewLeft.Nodes[0].Nodes[0];
            treeViewLeft.EndUpdate();

            // Add the event so that when you resize the window the control resizes
            pnlOption.Resize -= PnlOption_Resize;
            pnlOption.Resize += PnlOption_Resize;
        }

        private TreeNode[] GetTreeNodes(List<MenuModel> menus)
        {
            // Go through the menu and build up all the tree nodes
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
            // Pass in the title and give back the applicable menu model
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

        // Resize and refresh the dynamic control after a resize of the main form
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
            // Get the model by the title in the tree
            MenuModel selectedNode = GetMenuModelByTitle(menus, e.Node.Text);

            // In the case of a menu item just select it's child
            if (selectedNode.Id == null)
            {
                selectedNode = selectedNode.Children[0];
            }

            var userControlType = Type.GetType(selectedNode.Id);
            UserControl currentUserControl = (UserControl)Activator.CreateInstance(userControlType);

            // Remove the old control and add the new one
            currentUserControl.Size = pnlOption.Size;               

            if (pnlOption.Controls.Count > 0)
            {
                pnlOption.Controls.Clear();
            }

            pnlOption.Controls.Add(currentUserControl);

            // Set the new samples title and description
            this.labelSampleName.Text = selectedNode.Title;
            this.labelSampleDescription.Text = selectedNode.Description;

            // Setup the source code view area
            string uri = GetHtmlPath(selectedNode.Source + ".cs");
            Uri webUri = new Uri(@uri);
            cSharpBrowser.Url = webUri;
        }

        private string GetHtmlPath(string filename)
        {
            // Find a place to store the new HTML file we will use to show the source code
            string myDocuments = Environment.GetEnvironmentVariable("TEMP");
            string path = myDocuments + @"\" + "ThinkGeoWinformsHowDoISamples";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Grab the .cs source file
            string mainFolder = ((new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent.Parent.Parent).FullName;
            if (mainFolder.EndsWith("bin"))
                mainFolder = ((new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent.Parent.Parent.Parent).FullName; // for x64 or x86 instead of AnyCPU

            mainFolder += "\\";
            string[] files = Directory.GetFiles(mainFolder, filename, SearchOption.AllDirectories);
            string tmpFileName = Path.GetFileNameWithoutExtension(new FileInfo(files[0]).Name) + ".htm";
            string htmlFileName = path + "\\" + tmpFileName;
            if (File.Exists(htmlFileName))
            {
                return htmlFileName;
            }

            // Read the .cs file and wrap it with some HTML to display the code with syntax highlighting
            string text;
            try
            {
                using (StreamReader streamReader = new StreamReader(files[0]))
                {
                    text = streamReader.ReadToEnd();
                }
                text = "<body oncontextmenu='return false;'><div class='divbody'><pre name='code' class='c-sharp:nocontrols'>" + RemoveRegion(text) + string.Format("</pre></div><link type='text/css' rel='stylesheet' href='{0}\\SyntaxHighlighter\\SyntaxHighlighter.css'></link><script language='javascript' src='{0}\\SyntaxHighlighter\\shCore.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter/shBrushCSharp.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter\\shBrushXml.js'></script><script language='javascript'>dp.SyntaxHighlighter.HighlightAll('code');</script></body>", mainFolder);

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
            // When we show the sample source we want to remove the designer generated code
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
            // Collapse or expand the code preview window
            splitContainerSampleSource.Panel1Collapsed = !splitContainerSampleSource.Panel1Collapsed;
        }

        private void treeViewLeft_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            // If we select a category from the tree view cancel the click
            MenuModel selectedNode = GetMenuModelByTitle(menus, e.Node.Text);
            if (selectedNode.Id == null)
            {
                e.Cancel = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            // When you type into the search bar and hit enter we find all of the samples via the JSON
            // and see what samples match the various key words entered.  Then we restrict the tree view
            // to only the samples that match
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text == "")
                {
                    treeViewLeft.CollapseAll();

                    foreach (TreeNode item in treeViewLeft.Nodes)
                    {
                        ResetTreeviewItemsColor(item);
                    }
                }
                else
                {
                    treeViewLeft.CollapseAll();

                    foreach (TreeNode item in treeViewLeft.Nodes)
                        EnableTreeviewItems(item);
                }
            }
        }

        private void ResetTreeviewItemsColor(TreeNode item)
        {
            // Enable just the tree view items that match the search criteria
            item.BackColor = Color.FromArgb(0, 45, 48, 53);

            foreach (TreeNode subItem in item.Nodes)
            {
                ResetTreeviewItemsColor(subItem);

                if (subItem.Nodes.Count > 0)
                    ResetTreeviewItemsColor(subItem);
            }
        }

        private void EnableTreeviewItems(TreeNode item)
        {
            // Enable just the tree view items that match the search criteria
            item.Collapse(false);

            EnableMatchingTreeNodes(item);

            foreach (TreeNode subItem in item.Nodes)
            {
                EnableMatchingTreeNodes(subItem);

                if (subItem.Nodes.Count > 0)
                    EnableTreeviewItems(subItem);
            }
        }

        private void EnableMatchingTreeNodes(TreeNode item)
        {
            // Enable just the tree view items that match the search criteria
            string[] searchTerms = txtSearch.Text.Split(' ');

            MenuModel menuModel = GetMenuModelByTitle(menus, item.Text);

            if (menuModel != null)
            {
                bool foundAllSearchTerms = false;

                foreach (var searchTerm in searchTerms)
                {
                    bool searchTermFound = false;

                    if (menuModel.Title.ToLower().Contains(searchTerm.ToLower()))
                    {
                        searchTermFound = true;
                        foundAllSearchTerms = true;
                    }
                    if (menuModel.Description != null)
                    {
                        if (menuModel.Description.ToLower().Contains(searchTerm.ToLower()))
                        {
                            searchTermFound = true;
                            foundAllSearchTerms = true;
                        }
                    }
                    if (!searchTermFound)
                    {
                        foundAllSearchTerms = false;
                        break;
                    }
                }

                if (foundAllSearchTerms)
                {
                    item.Expand();
                    item.BackColor = Color.White;
                    ExpandParents(item);
                }
            }
        }

        private void ExpandParents(TreeNode item)
        {
            // Given a tree node we walk up the parents and expand them
            TreeNode parent = (TreeNode)item.Parent;

            while (parent != null)
            {
                parent.Expand();
                if (parent.Parent is TreeNode)
                {
                    parent = (TreeNode)parent.Parent;
                }
                else
                {
                    parent = null;
                }
            }
        }
    }
}


