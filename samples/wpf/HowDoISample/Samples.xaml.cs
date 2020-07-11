using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class Samples : Window
    {
        // A list of all the menu models
        private List<MenuModel> menus;

        public Samples()
        {
            // Setup the model
            var mainWindowVm = new MainWindowViewModel();
            mainWindowVm.PropertyChanged += MainWindowVm_PropertyChanged;
            DataContext = mainWindowVm;

            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var mainWindowVm = (MainWindowViewModel)DataContext;

            // Dynamically read the samples to load into the tree view
            string json = File.ReadAllText(@"../../../samples.json");
            menus = JsonConvert.DeserializeObject<List<MenuModel>>(json);

            // Add each item into the tree view
            foreach (var item in menus)
            {
                var treeItem = new TreeViewItem();
                treeItem.Header = item.Title;
                treeView.Items.Add(treeItem);
                AddTreeItems(treeItem, item);
            }

            // Expand the first node and select the first sample
            TreeViewItem firstTreeNode = (TreeViewItem)treeView.Items[0];
            firstTreeNode.IsExpanded = true;
            TreeViewItem firstSubTreeNode = (TreeViewItem)firstTreeNode.Items[0];            
            firstSubTreeNode.IsSelected = true;
        }

        private void AddTreeItems(TreeViewItem parentTreeviewItem, MenuModel menuModel)
        {
            // Add the tree view items to the tree recursively
            if (menuModel.Children != null)
            {
                foreach (var item in menuModel.Children)
                {
                    var subTreeItem = new TreeViewItem();
                    subTreeItem.Header = item.Title;
                    subTreeItem.Tag = item;
                    parentTreeviewItem.Items.Add(subTreeItem);
                    AddTreeItems(subTreeItem, item);
                }
            }
        }

        private void MainWindowVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = sender as MainWindowViewModel;

            // If we select a new item then we need to load the new item
            if (e.PropertyName == nameof(MainWindowViewModel.SelectedMenu))
            {
                // If we already had a sample loaded then unload it and try and reclaim the memory
                // at the moment we can't seem to reclaim the memory but I think this is a WPF issue
                if (SampleContent.Children.Count > 0)
                {
                    UserControl oldCOntrol = (UserControl)SampleContent.Children[0];
                    SampleContent.Children.Remove(oldCOntrol);
                    SampleContent.DataContext = null;
                    oldCOntrol.DataContext = null;
                    oldCOntrol = null;
                    GC.Collect();
                }

                // Dynamically create the new user control based on the sample selected
                UserControl sample = GetSample(vm.SelectedMenu.Id);

                // Add the new sample user control to the XAML layout
                SampleContent.Children.Add(sample);

                // Update the CS & XAML code windows
                UpdateCodeViewerLayout(vm.CodeViewer);
                CsharpCodeViewer.Text = GetFileContent($"../../../{ vm.SelectedMenu.Source}.xaml.cs");
                XamlCodeViewer.Text = ToXaml(sample);
            }
            else if (e.PropertyName == nameof(MainWindowViewModel.CodeViewer))
            {
                // Update the code and XAML view
                UpdateCodeViewerLayout(vm.CodeViewer);
            }
        }

        private void UpdateCodeViewerLayout(CodeViewerViewModel codeViewer)
        {
            // Enable and disable
            var codeViewerVisible = codeViewer.IsCsharpCodeVisible || codeViewer.IsXamlCodeVisible;
            if (codeViewerVisible)
            {
                Grid.SetColumn(SampleContent, 2);
                SampleContent.SetValue(Grid.ColumnSpanProperty, DependencyProperty.UnsetValue);
                Splitter.Visibility = Visibility.Visible;
            }
            else
            {
                Grid.SetColumn(SampleContent, 0);
                Grid.SetColumnSpan(SampleContent, 3);
                Splitter.Visibility = Visibility.Collapsed;
            }
        }

        private static UserControl GetSample(string name)
        {
            // Load the new user control dynamically
            var userControlType = Type.GetType(name);
            Debug.Assert(userControlType != null, $"The type {name} is not found.");
            return (UserControl)Activator.CreateInstance(userControlType);
        }

        private void SidebarToggle_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            // Toggles the right hand side tree view collapsed   
            if (!IsLoaded) return;

            var sidebarToggle = (ToggleButton)sender;
            if (sidebarToggle.IsChecked.GetValueOrDefault())
            {
                Grid.SetColumn(Container, 2);
                Grid.SetColumn(SidebarToggle, 2);
                Container.SetValue(Grid.ColumnSpanProperty, DependencyProperty.UnsetValue);
            }
            else
            {
                Grid.SetColumn(Container, 0);
                Grid.SetColumn(SidebarToggle, 0);
                Grid.SetColumnSpan(Container, 3);
            }
        }

        private static string ToXaml(object userControl)
        {
            // Reads the XAML from the user control to display in the XAML tab
            var contentBuilder = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                NewLineOnAttributes = true,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };

            var dsm = new XamlDesignerSerializationManager(XmlWriter.Create(contentBuilder, settings))
            {
                XamlWriterMode = XamlWriterMode.Expression
            };

            XamlWriter.Save(userControl, dsm);
            return contentBuilder.ToString();
        }

        private void treeView_Selected(object sender, RoutedEventArgs e)
        {
            // Fired when a new treeview item is selected and then updates the model
            var treeViewItem = (TreeViewItem)e.Source;

            if (treeViewItem.Tag != null)
            {
                MenuModel menuModel = (MenuModel)treeViewItem.Tag;
                if (menuModel.Id != null)
                {
                    ((MainWindowViewModel)DataContext).SelectedMenu = GetMenuViewModelById(menuModel.Id);
                }
            }
        }

        private MenuViewModel GetMenuViewModelById(string id)
        {
            // Returns a model by the ID
            MenuViewModel menuViewModel = null;

            foreach (var menu in ((MainWindowViewModel)DataContext).Menus)
            {
                foreach (var child in menu.Children)
                {
                    if (child.Id == id)
                    {
                        menuViewModel = child;
                    }
                }
            }

            return menuViewModel;
        }

        private static string GetFileContent(string path)
        {
            // Reads a file and returns the text inside
            if (!File.Exists(path)) return string.Empty;
            return File.ReadAllText(path);
        }

        private void txtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //  When you type into the search bar and hit enter we find all of the samples via the JSON
            // and see what samples match the various key words entered.  Then we restrict the tree view
            // to only the samples that match
            if (e.Key == Key.Return)
            {
                if (txtSearch.Text == "")
                {
                    foreach (TreeViewItem item in treeView.Items)
                        CollapseTreeviewItems(item, Visibility.Visible);
                }
                else
                {
                    foreach (TreeViewItem item in treeView.Items)
                        CollapseTreeviewItems(item, Visibility.Collapsed);

                    foreach (TreeViewItem item in treeView.Items)
                        EnableTreeviewItems(item);
                }
            }
        }

        private void EnableTreeviewItems(TreeViewItem item)
        {
            // Enable just the tree view items that match the search criteria
            EnableMatchingTreeNodes(item);

            foreach (TreeViewItem subItem in item.Items)
            {
                EnableMatchingTreeNodes(subItem);

                if (subItem.HasItems)
                    EnableTreeviewItems(subItem);
            }
        }

        private void EnableMatchingTreeNodes(TreeViewItem item)
        {
            // Enable just the tree view items that match the search criteria
            string[] searchTerms = txtSearch.Text.Split(' ');

            if (item.Tag != null)
            {
                MenuModel menuModel = (MenuModel)item.Tag;

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
                    item.IsExpanded = true;
                    item.Visibility = Visibility.Visible;

                    ExpandParents(item);
                }
            }
        }

        private void ExpandParents(TreeViewItem item)
        {
            // Given a tree node we walk up the parents and expand them
            TreeViewItem parent = (TreeViewItem)item.Parent;

            while (parent != null)
            {
                parent.IsExpanded = true;
                parent.Visibility = Visibility.Visible;
                if (parent.Parent is TreeViewItem)
                {
                    parent = (TreeViewItem)parent.Parent;
                }
                else
                {
                    parent = null;
                }
            }
        }

        private void CollapseTreeviewItems(TreeViewItem item, Visibility visibility)
        {
            // Given a tree view we expand or contract the parent nodes and set the correct vivibility of the leafs
            item.IsExpanded = false;
            item.Visibility = visibility;

            foreach (TreeViewItem subItem in item.Items)
            {
                subItem.IsExpanded = false;
                subItem.Visibility = visibility;

                if (subItem.HasItems)
                    CollapseTreeviewItems(subItem, visibility);
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // This fires on the scroll wheel over the tree view so we support scrolling with the mouse wheel
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
