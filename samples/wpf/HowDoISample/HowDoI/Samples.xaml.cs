using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Xml;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Samples : Window
    {
        public static readonly string RootDirectory = @"../../../SampleData/";

        public Samples()
        {
            var mainWindowVm = new MainWindowViewModel();
            mainWindowVm.PropertyChanged += MainWindowVm_PropertyChanged;
            DataContext = mainWindowVm;
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var mainWindowVm = (MainWindowViewModel)DataContext;

            mainWindowVm.Menus[0].Expanded = true;
            mainWindowVm.SelectedMenu = mainWindowVm.Menus[0].Children[0];
        }

        private void MainWindowVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = sender as MainWindowViewModel;
            if (e.PropertyName == nameof(MainWindowViewModel.SelectedMenu))
            {
                var sample = GetSample(vm.SelectedMenu.Id);
                SampleContent.Content = sample;
                UpdateCodeViewerLayout(vm.CodeViewer);
                CsharpCodeViewer.Text = vm.CodeViewer.CsharpCode;
                XamlCodeViewer.Text = ToXaml(sample);
            }
            else if (e.PropertyName == nameof(MainWindowViewModel.CodeViewer))
            {
                UpdateCodeViewerLayout(vm.CodeViewer);
                CsharpCodeViewer.Text = vm.CodeViewer.CsharpCode;
                XamlCodeViewer.Text = ToXaml(SampleContent.Content);
            }
        }

        private void UpdateCodeViewerLayout(CodeViewerViewModel codeViewer)
        {
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
            var userControlType = Type.GetType(name);
            Debug.Assert(userControlType != null, $"The type {name} is not found.");
            return (UserControl)Activator.CreateInstance(userControlType);
        }

        private void SidebarToggle_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
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
    }
}
