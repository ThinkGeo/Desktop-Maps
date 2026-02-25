using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ThinkGeo.UI.Wpf.HowDoI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	public partial class Samples
	{
		private const string DefaultSampleId = "ThinkGeo.UI.Wpf.HowDoI.SampleFeatureIndexViewer";

		// A list of all the menu models
		private List<MenuModel> _menus;
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
			AllowTrailingCommas = true
		};
		private MenuModel _selectedMenu;
		private bool _updatingCodeToggles;

		public Samples()
		{
			InitializeComponent();
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			// Dynamically read the samples to load into the tree view
			var json = File.ReadAllText(@"./Samples/samples.json");

			_menus = JsonSerializer.Deserialize<List<MenuModel>>(json, JsonOptions) ?? new List<MenuModel>();

			// Add each item into the tree view
			foreach (var item in _menus)
			{
				var treeItem = new TreeViewItem
				{
					Header = item.Title,
					Tag = item
				};
				TreeView.Items.Add(treeItem);
				AddTreeItems(treeItem, item);
			}

			SelectDefaultSample();

			UpdateCodeViewerVisibility();
		}

		private void SelectDefaultSample()
		{
			if (TreeView.Items.Count == 0) return;

			if (TrySelectTreeItemBySampleId(DefaultSampleId)) return;

			var firstRootNode = TreeView.Items[0] as TreeViewItem;
			if (firstRootNode == null) return;

			firstRootNode.IsExpanded = true;
			var firstLeafNode = firstRootNode.Items.Count > 0 ? firstRootNode.Items[0] as TreeViewItem : null;
			if (firstLeafNode != null)
				firstLeafNode.IsSelected = true;
			else
				firstRootNode.IsSelected = true;
		}

		private bool TrySelectTreeItemBySampleId(string sampleId)
		{
			foreach (var item in TreeView.Items)
			{
				if (item is TreeViewItem treeItem && TrySelectTreeItemBySampleId(treeItem, sampleId))
				{
					return true;
				}
			}

			return false;
		}

		private static bool TrySelectTreeItemBySampleId(TreeViewItem treeItem, string sampleId)
		{
			if (treeItem.Tag is MenuModel menuModel && string.Equals(menuModel.Id, sampleId, StringComparison.Ordinal))
			{
				ExpandParents(treeItem);
				treeItem.IsExpanded = true;
				treeItem.IsSelected = true;
				return true;
			}

			foreach (var child in treeItem.Items)
			{
				if (child is TreeViewItem childTreeItem && TrySelectTreeItemBySampleId(childTreeItem, sampleId))
				{
					treeItem.IsExpanded = true;
					return true;
				}
			}

			return false;
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

		private static MenuModel FindMenuById(IEnumerable<MenuModel> menus, string id)
		{
			if (menus == null) return null;
			foreach (var menu in menus)
			{
				if (string.Equals(menu.Id, id, StringComparison.Ordinal))
					return menu;
				var match = FindMenuById(menu.Children, id);
				if (match != null) return match;
			}
			return null;
		}

		private void UpdateUserControl()
		{
			if (_selectedMenu == null) return;

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
				GC.Collect();
			}

			// Dynamically create the new user control based on the sample selected
			var sample = GetSample(_selectedMenu?.Id);

			// Add the new sample user control to the XAML layout
			SampleContent.Children.Add(sample);

			// Update the CS & XAML code windows
			UpdateCodeViewerLayout();
			CsharpCodeViewer.Text = GetFileContent($"../../../{_selectedMenu?.Source}.xaml.cs");
			XamlCodeViewer.Text = GetFileContent($"../../../{_selectedMenu?.Source}.xaml");
		}

		private void UpdateCodeViewerLayout()
		{
			// Enable and disable
			var codeViewerVisible = CsharpCodeViewer.Visibility == Visibility.Visible || XamlCodeViewer.Visibility == Visibility.Visible;
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
			if (string.IsNullOrEmpty(name)) return new UserControl();
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

		private void treeView_Selected(object sender, RoutedEventArgs e)
		{
			// Fired when a new treeview item is selected and then updates the model
			var treeViewItem = (TreeViewItem)e.Source;

			if (treeViewItem.Tag == null) return;
			var menuModel = (MenuModel)treeViewItem.Tag;
			if (menuModel?.Id == null) return;
			SelectMenu(menuModel.Id);
		}

		private void SelectMenu(string id)
		{
			_selectedMenu = FindMenuById(_menus, id);
			UpdateSelectedSampleUi();
		}

		internal void NavigateToSample(string id)
		{
			if (string.IsNullOrWhiteSpace(id)) return;

			TrySelectTreeItemBySampleId(id);
			SelectMenu(id);
		}

		private void UpdateSelectedSampleUi()
		{
			if (_selectedMenu == null) return;

			TitleText.Text = _selectedMenu.Title ?? string.Empty;
			DescriptionText.Text = _selectedMenu.Description ?? string.Empty;

			UpdateUserControl();
			UpdateCodeViewerVisibility();
		}

		internal static string GetFileContent(string path)
		{
			// Reads a file and returns the text inside
			if (!File.Exists(path)) return string.Empty;
			return File.ReadAllText(path);
		}

		// TODO: Load xaml from assembly.
		internal static string GetResourceContent(string path)
		{
			var source = path.Replace("\\", "/");


			return string.Empty;
		}

		private void UpdateCodeViewerVisibility()
		{
			if (CsSwitcher == null || XamlSwitcher == null || CsharpCodeViewer == null || XamlCodeViewer == null)
				return;

			bool showCs = CsSwitcher.IsChecked == true;
			bool showXaml = XamlSwitcher.IsChecked == true;

			CsharpCodeViewer.Visibility = showCs ? Visibility.Visible : Visibility.Collapsed;
			XamlCodeViewer.Visibility = showXaml ? Visibility.Visible : Visibility.Collapsed;

			UpdateCodeViewerLayout();
		}

		private void CsSwitcher_OnChecked(object sender, RoutedEventArgs e)
		{
			if (_updatingCodeToggles) return;
			_updatingCodeToggles = true;
			if (CsSwitcher.IsChecked == true && XamlSwitcher.IsChecked == true)
			{
				XamlSwitcher.IsChecked = false;
			}
			_updatingCodeToggles = false;
			UpdateCodeViewerVisibility();
		}

		private void XamlSwitcher_OnChecked(object sender, RoutedEventArgs e)
		{
			if (_updatingCodeToggles) return;
			_updatingCodeToggles = true;
			if (XamlSwitcher.IsChecked == true && CsSwitcher.IsChecked == true)
			{
				CsSwitcher.IsChecked = false;
			}
			_updatingCodeToggles = false;
			UpdateCodeViewerVisibility();
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
			var parent = item.Parent as TreeViewItem;

			while (parent != null)
			{
				parent.IsExpanded = true;
				parent.Visibility = Visibility.Visible;
				parent = parent.Parent as TreeViewItem;
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

	class MenuModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
		public List<MenuModel> Children { get; set; }
	}

	class HyperlinkHelper
	{
		public static readonly DependencyProperty LaunchBrowserProperty =
			DependencyProperty.RegisterAttached("LaunchBrowser", typeof(bool),
				typeof(HyperlinkHelper), new PropertyMetadata(false,
					HyperlinkHelper_LaunchBrowserChanged));

		public static bool GetLaunchBrowser(DependencyObject d)
		{
			return (bool)d.GetValue(LaunchBrowserProperty);
		}

		public static void SetLaunchBrowser(DependencyObject d, bool value)
		{
			d.SetValue(LaunchBrowserProperty, value);
		}

		private static void HyperlinkHelper_LaunchBrowserChanged(object
			sender, DependencyPropertyChangedEventArgs e)
		{
			var d = (UIElement)sender;
			if ((bool)e.NewValue)
				d.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(Hyperlink_RequestNavigateEvent));
			else
				d.RemoveHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(Hyperlink_RequestNavigateEvent));
		}

		private static void Hyperlink_RequestNavigateEvent(object sender, RequestNavigateEventArgs e)
		{
			var process = new Process { StartInfo = { UseShellExecute = true, FileName = e.Uri.AbsoluteUri } };
			process.Start();
			e.Handled = true;
		}
	}
}
