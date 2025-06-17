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
using System.Windows.Threading;
using System.Xml;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class Samples
    {
        // A list of all the menu models
        private List<MenuModel> _menus;
        private readonly DispatcherTimer _changeTimer;

        public Samples()
        {
            // Set up the model
            var mainWindowVm = new MainWindowViewModel(this);
            mainWindowVm.PropertyChanged += MainWindowVm_PropertyChanged;
            DataContext = mainWindowVm;

            InitializeComponent();

            _changeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _changeTimer.Tick += ChangeTimer_Tick;
        }

        private void ChangeTimer_Tick(object sender, EventArgs e)
        {
            _changeTimer.Stop();
            UpdateUserControl();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var mainWindowVm = (MainWindowViewModel)DataContext;

            // Dynamically read the samples to load into the tree view
            var json = File.ReadAllText(@"./samples.json");
            _menus = JsonConvert.DeserializeObject<List<MenuModel>>(json);

            // Add each item into the tree view
            foreach (var item in _menus)
            {
                var treeItem = new TreeViewItem
                {
                    Header = item.Title
                };
                TreeView.Items.Add(treeItem);
                AddTreeItems(treeItem, item);
            }

            // Expand the first node and select the first sample
            var firstTreeNode = (TreeViewItem)TreeView.Items[7];
            if (firstTreeNode == null) return;
            firstTreeNode.IsExpanded = true;
            var firstSubTreeNode = (TreeViewItem)firstTreeNode.Items[1];
            if (firstSubTreeNode != null) firstSubTreeNode.IsSelected = true;
        }

        private static void AddTreeItems(TreeViewItem parentTreeviewItem, MenuModel menuModel)
        {
            // Add the tree view items to the tree recursively
            if (menuModel.Children != null)
            {
                foreach (var item in menuModel.Children)
                {
                    var subTreeItem = new TreeViewItem
                    {
                        Header = item.Title,
                        Tag = item
                    };
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
                if (_changeTimer.IsEnabled)
                {
                    _changeTimer.Stop();
                }
                _changeTimer.Start();
            }
            else if (e.PropertyName == nameof(MainWindowViewModel.CodeViewer))
            {
                // Update the code and XAML view
                UpdateCodeViewerLayout(vm?.CodeViewer);
            }
        }

        private void UpdateUserControl()
        {
            var vm = DataContext as MainWindowViewModel;
            // If we already had a sample loaded then unload it and try and reclaim the memory
            // at the moment we can't seem to reclaim the memory, but I think this is a WPF issue
            if (SampleContent.Children.Count > 0)
            {
                var oldControl = (UserControl)SampleContent.Children[0];
                if (oldControl is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                SampleContent.Children.Remove(oldControl);
                SampleContent.DataContext = null;
                oldControl.DataContext = null;
                oldControl = null;
                GC.Collect();
            }

            // Dynamically create the new user control based on the sample selected
            var sample = GetSample(vm?.SelectedMenu.Id);

            // Add the new sample user control to the XAML layout
            SampleContent.Children.Add(sample);

            // Update the CS & XAML code windows
            UpdateCodeViewerLayout(vm?.CodeViewer);
            CsharpCodeViewer.Text = GetFileContent($"../../../{vm?.SelectedMenu.Source}.xaml.cs");
            XamlCodeViewer.Text = ToXaml(sample);
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

            if (treeViewItem.Tag == null) return;
            var menuModel = (MenuModel)treeViewItem.Tag;
            if (menuModel.Id != null)
            {
                ((MainWindowViewModel)DataContext).SelectedMenu = GetMenuViewModelById(menuModel.Id);
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //  When you type into the search bar and hit enter we find all the samples via the JSON
            // and see what samples match the various keywords entered.  Then we restrict the tree view
            // to only the samples that match
            if (e.Key == Key.Return)
            {
                if (TxtSearch.Text == "")
                {
                    foreach (TreeViewItem item in TreeView.Items)
                        CollapseTreeviewItems(item, Visibility.Visible);
                }
                else
                {
                    foreach (TreeViewItem item in TreeView.Items)
                        CollapseTreeviewItems(item, Visibility.Collapsed);

                    foreach (TreeViewItem item in TreeView.Items)
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
            string[] searchTerms = TxtSearch.Text.Split(' ');

            if (item.Tag != null)
            {
                var menuModel = (MenuModel)item.Tag;

                bool foundAllSearchTerms = false;

                foreach (var searchTerm in searchTerms)
                {
                    var searchTermFound = false;

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

        private static void ExpandParents(TreeViewItem item)
        {
            // Given a tree node we walk up the parents and expand them
            var parent = (TreeViewItem)item.Parent;

            while (parent != null)
            {
                parent.IsExpanded = true;
                parent.Visibility = Visibility.Visible;
                if (parent.Parent is TreeViewItem viewItem)
                {
                    parent = viewItem;
                }
                else
                {
                    parent = null;
                }
            }
        }

        private static void CollapseTreeviewItems(TreeViewItem item, Visibility visibility)
        {
            // Given a tree view we expand or contract the parent nodes and set the correct visibility of the leafs
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
            // This fires on the scroll wheel over the tree view, so we support scrolling with the mouse wheel
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
