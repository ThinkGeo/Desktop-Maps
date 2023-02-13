using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly List<MenuModel> _menus;

        private MenuViewModel _selectedMenu;

        public MainWindowViewModel(DependencyObject view)
        {
            if (DesignerProperties.GetIsInDesignMode(view))
            {
                _menus = new List<MenuModel>();
                var group1 = new MenuModel
                {
                    Title = "ThinkGeo Base Maps",
                    Children = new List<MenuModel>()
                };
                group1.Children.Add(new MenuModel
                {
                    Id = "VectorTiles",
                    Title = "Vector Tiles"
                });

                group1.Children.Add(new MenuModel
                {
                    Id = "VectorTiles1",
                    Title = "Vector Tiles on Aerial Imagery"
                });
                _menus.Add(group1);

                var group2 = new MenuModel
                {
                    Title = "ThinkGeo Cloud Services",
                    Children = new List<MenuModel>()
                };

                group2.Children.Add(new MenuModel
                {
                    Id = "ServiceArea",
                    Title = "Get Service Area"
                });

                group2.Children.Add(new MenuModel
                {
                    Id = "RouteCostMatrix",
                    Title = "Route Cost Matrix"
                });
                _menus.Add(group2);
            }
            else
            {
                _menus = JsonConvert.DeserializeObject<List<MenuModel>>(File.ReadAllText("samples.json"));
            }
            Menus = new ObservableCollection<MenuViewModel>(_menus.Select(m => new MenuViewModel(m)));


            CodeViewer = new CodeViewerViewModel();
            CodeViewer.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(CodeViewer));
        }

        public ICommand ExpandedMenusCommand
        {
            get
            {
                return new RelayCommand<MenuViewModel>(selectedMenu =>
                {
                    foreach (var menu in Menus)
                    {
                        menu.Expanded = menu == selectedMenu;
                    }
                });
            }
        }

        public ObservableCollection<MenuViewModel> Menus { get; }

        public CodeViewerViewModel CodeViewer { get; }

        public MenuViewModel SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                if (_selectedMenu != value)
                {
                    _selectedMenu = value;
                    _selectedMenu.IsSelected = true;
                    CodeViewer.CsharpCode = GetFileContent($"{_selectedMenu.Source}.xaml.cs");
                    CodeViewer.XamlCode = GetResourceContent($"{_selectedMenu.Source}.baml");
                    OnPropertyChanged(nameof(SelectedMenu));
                }
            }
        }


        private static string GetFileContent(string path)
        {
            if (!File.Exists(path)) return string.Empty;
            return File.ReadAllText(path);
        }

        // TODO: Load xaml from assembly.
        private static string GetResourceContent(string path)
        {
            var source = path.Replace("\\", "/");


            return string.Empty;
        }
    }
}
