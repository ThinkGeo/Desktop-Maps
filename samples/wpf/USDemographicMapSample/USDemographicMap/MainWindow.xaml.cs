using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using ThinkGeo.MapSuite.Layers;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSliderClicked;
        private MapModel mapModel;
        private MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mapModel = new MapModel(wpfMap);
            mainWindowViewModel = new MainWindowViewModel(mapModel);
            DataContext = mainWindowViewModel;

            mainWindowViewModel.UpdateMap();
            mapModel.MapControl.Refresh();
        }

        private void ColumnCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.SelectedCategory.Columns.Count(d => d.IsSelected) >= 2)
            {
                mainWindowViewModel.SelectedCategory.CanUsePieView = true;
                mainWindowViewModel.CreatePieDemographicStyle();
                mainWindowViewModel.UpdateMap();
            }
            else
            {
                mainWindowViewModel.SelectedCategory.CanUsePieView = false;
            }
        }

        private void DotDensityStyleDotsCountSlider_Click(object sender, RoutedEventArgs e)
        {
            isSliderClicked = true;
        }

        private void DotDensityStyleDotsCountSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            mainWindowViewModel.DotDensityDemographicStyle.DotDensityValue = 50 * (sldDotDensityDemographicStyleDotsCount.Value / 3);
            mainWindowViewModel.UpdateMap();
        }

        private void DotDensityStyleDotsCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isSliderClicked)
            {
                mainWindowViewModel.DotDensityDemographicStyle.DotDensityValue = 50 * (sldDotDensityDemographicStyleDotsCount.Value / 3);
                mainWindowViewModel.UpdateMap();
                isSliderClicked = false;
            }
        }

        private void ValueCircleStyleRadiusSlider_Click(object sender, RoutedEventArgs e)
        {
            isSliderClicked = true;
        }

        private void ValueCircleStyleRadiusSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            mainWindowViewModel.ValueCircleDemographicStyle.RadiusRatio = sldValueCircleDemographicStyleRadius.Value / 3;
            mainWindowViewModel.UpdateMap();
        }

        private void ValueCircleStyleRadiusSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isSliderClicked)
            {
                mainWindowViewModel.ValueCircleDemographicStyle.RadiusRatio = sldValueCircleDemographicStyleRadius.Value / 3;
                mainWindowViewModel.UpdateMap();
                isSliderClicked = false;
            }
        }

        private void ValueCirclesClick(object sender, RoutedEventArgs e)
        {
            CollapseControls();
            mainWindowViewModel.ValueCircleVisibility = Visibility.Visible;
            ColumnViewModel columnViewModel = sender.GetDataContext<ColumnViewModel>();
            if (columnViewModel != null && !(columnViewModel.Parent is CustomDataCategoryViewModel))
            {
                mapModel.DefaultFeatureLayer = mapModel.CensusStateFeatureLayer;
            }
            else
            {
                mapModel.DefaultFeatureLayer = mapModel.CustomDataFeatureLayer;
            }
            RenderDemographicStyle(sender, mainWindowViewModel.ValueCircleDemographicStyle);
        }

        private void ThematicColorsClick(object sender, RoutedEventArgs e)
        {
            CollapseControls();
            mainWindowViewModel.ThematicVisibility = Visibility.Visible;
            mainWindowViewModel.DisplayColorText = "Display Start Color:";
            ColumnViewModel columnViewModel = sender.GetDataContext<ColumnViewModel>();
            if (columnViewModel != null && !(columnViewModel.Parent is CustomDataCategoryViewModel))
            {
                mapModel.DefaultFeatureLayer = mapModel.CensusStateFeatureLayer;
            }
            else
            {
                mapModel.DefaultFeatureLayer = mapModel.CustomDataFeatureLayer;
            }
            RenderDemographicStyle(sender, mainWindowViewModel.ThematicDemographicStyle);
        }

        private void DotDensityClick(object sender, RoutedEventArgs e)
        {
            CollapseControls();
            mainWindowViewModel.DotDensityVisibility = Visibility.Visible;
            ColumnViewModel columnViewModel = sender.GetDataContext<ColumnViewModel>();
            if (columnViewModel != null && !(columnViewModel.Parent is CustomDataCategoryViewModel))
            {
                mapModel.DefaultFeatureLayer = mapModel.CensusStateFeatureLayer;
            }
            else
            {
                mapModel.DefaultFeatureLayer = mapModel.CustomDataFeatureLayer;
            }
            RenderDemographicStyle(sender, mainWindowViewModel.DotDensityDemographicStyle);
        }

        private void PieChartClick(object sender, RoutedEventArgs e)
        {
            CollapseControls();
            if (sender.GetDataContext<CustomDataCategoryViewModel>() == null)
            {
                mapModel.DefaultFeatureLayer = mapModel.CensusStateFeatureLayer;
            }
            else
            {
                mapModel.DefaultFeatureLayer = mapModel.CustomDataFeatureLayer;
            }
            mainWindowViewModel.CreatePieDemographicStyle();
            mainWindowViewModel.UpdateMap();
        }

        private void CollapseControls()
        {
            mainWindowViewModel.DotDensityVisibility = Visibility.Collapsed;
            mainWindowViewModel.ValueCircleVisibility = Visibility.Collapsed;
            mainWindowViewModel.ThematicVisibility = Visibility.Collapsed;
            mainWindowViewModel.DisplayColorText = "Display Color:";
        }

        private void RenderDemographicStyle(object sender, DemographicStyleBuilder demographicStyleBuilder)
        {
            FrameworkElement frameworkElement = (FrameworkElement)sender;
            ColumnViewModel columnViewModel = (ColumnViewModel)frameworkElement.DataContext;
            columnViewModel.IsSelected = true;
            foreach (var item in mainWindowViewModel.SelectedCategory.Columns)
            {
                item.CheckBoxVisiblity = Visibility.Hidden;
            }

            mainWindowViewModel.UpdateUIControls(demographicStyleBuilder);

            mainWindowViewModel.CurrentStyleBuilder.SelectedColumns.Clear();
            mainWindowViewModel.CurrentStyleBuilder.SelectedColumns.Add(columnViewModel.ColumnName);
            mapModel.LegendTitle = columnViewModel.LegendTitle;

            mainWindowViewModel.UpdateMap();
        }

        private void BrowseCustomDataClick(object sender, RoutedEventArgs e)
        {
            CustomDataCategoryViewModel customDataCategoryViewModel = ((FrameworkElement)sender).DataContext as CustomDataCategoryViewModel;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Shape File(*.shp)|*.shp";
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(openFileDialog.FileName);
                shapeFileFeatureLayer.Open();

                if (shapeFileFeatureLayer.GetShapeFileType() == ShapeFileType.Polygon)
                {
                    customDataCategoryViewModel.CustomDataFilePathName = openFileDialog.FileName;
                    customDataCategoryViewModel.Columns.Clear();

                    foreach (DbfColumn dbfColumn in shapeFileFeatureLayer.FeatureSource.GetColumns().OfType<DbfColumn>())
                    {
                        if (dbfColumn.ColumnType == DbfColumnType.Float
                            || dbfColumn.ColumnType == DbfColumnType.Numeric
                            || dbfColumn.ColumnType == DbfColumnType.DoubleInBinary
                            || dbfColumn.ColumnType == DbfColumnType.IntegerInBinary)
                        {
                            customDataCategoryViewModel.Columns.Add(new ColumnViewModel(dbfColumn.ColumnName, dbfColumn.ColumnName) { LegendTitle = dbfColumn.ColumnName, Parent = customDataCategoryViewModel });
                        }
                    }
                    customDataCategoryViewModel.CanUsePieView = customDataCategoryViewModel.Columns.Count >= 2;
                    mapModel.CustomDataFeatureLayer = shapeFileFeatureLayer;
                }
                else
                {
                    MessageBox.Show("The shapefile must be polygon type, please try another one.", "Warning");
                }
            }
        }
    }
}