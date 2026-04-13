using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
	/// <summary>
	/// Interaction logic for HatchStyles.xaml
	/// </summary>
	public partial class HatchStyles : UserControl
	{
		private LayerOverlay _layerOverlay = new LayerOverlay();
		private FeatureLayerWpfDrawingOverlay _wpfDrawingOverlay = new FeatureLayerWpfDrawingOverlay();
		private GeoHatchStyle _currentHatchStyle = GeoHatchStyle.Cross;
		private bool _initialized;
		private ShapeFileFeatureLayer _parksLayer;

		public HatchStyles()
		{
			InitializeComponent();

			var styles = Enum.GetValues(typeof(GeoHatchStyle))
				.Cast<GeoHatchStyle>()
				.OrderBy(s => s.ToString(), StringComparer.OrdinalIgnoreCase);

			CboHatchStyles.Items.Clear();
			foreach (var style in styles)
				CboHatchStyles.Items.Add(style);

			// Select a default value
			CboHatchStyles.SelectedItem = GeoHatchStyle.Cross;
		}

		/// <summary>
		/// Set up the map with the ThinkGeo Cloud Maps overlay. Also, project and add styles to the Hotels, Streets, and Parks layer.
		/// </summary>
		private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

			_initialized = true;
			// Set the map's unit of measurement to meters(Spherical Mercator)
			Map.MapUnit = GeographyUnit.Meter;

			// Set the map background color
			Map.Background = new SolidColorBrush(Color.FromRgb(234, 232, 226));

			_parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

			// Project the layer's data to match the projection of the map
			_parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
			// Add Styles to the layers
			StyleParksLayer(_currentHatchStyle);

			_layerOverlay = new LayerOverlay();
			_layerOverlay.Layers.Add(_parksLayer);
			_layerOverlay.TileType = TileType.SingleTile;
			Map.Overlays.Add(_layerOverlay);

			_wpfDrawingOverlay = new FeatureLayerWpfDrawingOverlay();
			_wpfDrawingOverlay.Visibility = Visibility.Hidden;
			_wpfDrawingOverlay.FeatureLayers.Add(_parksLayer);
			Map.Overlays.Add(_wpfDrawingOverlay);

			// Set the map extent
			Map.CenterPoint = new PointShape(-10777610, 3909120);
			Map.CurrentScale = 2260;

			_initialized = true;
			_ = Map.RefreshAsync();
		}

		private void CboHatchStyles_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!_initialized)
				return;

			if (!(CboHatchStyles.SelectedItem is GeoHatchStyle selectedStyle)) return;
			_currentHatchStyle = selectedStyle;

			StyleParksLayer(_currentHatchStyle);
			_ = _layerOverlay.RefreshAsync();
			_ = _wpfDrawingOverlay.RefreshAsync();
		}

		private void ComboBoxItem_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (!(sender is ComboBoxItem item)) return;
			if (!(item.Content is GeoHatchStyle selectedStyle)) return;
			if (_currentHatchStyle == selectedStyle) return;
			_currentHatchStyle = selectedStyle;

			StyleParksLayer(_currentHatchStyle);
			_ = _layerOverlay.RefreshAsync();
			_ = _wpfDrawingOverlay.RefreshAsync();
		}

		/// <summary>
		/// Adds an AreaStyle and TextStyle to the Parks Layer
		/// </summary>
		private void StyleParksLayer(GeoHatchStyle hatchStyle)
		{
			var areaStyle = AreaStyle.CreateHatchStyle(
					hatchStyle,
				GeoColors.Black,
				GeoColors.Transparent,
				GeoColors.Blue,
			5,
			LineDashStyle.Solid,
			0f,
			0f);

			var textStyle = new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DarkGreen)
			{
				FittingPolygon = true,
				HaloPen = new GeoPen(GeoBrushes.White, 2),
				DrawingLevel = DrawingLevel.LabelLevel,
				AllowLineCarriage = true,
				FittingPolygonInScreen = true
			};
			_parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
			_parksLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
			_parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
		}

		private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
		{
			if (sender is CheckBox checkBox)
			{
				_layerOverlay.IsVisible = !checkBox.IsChecked.GetValueOrDefault();
				_wpfDrawingOverlay.IsVisible = checkBox.IsChecked.GetValueOrDefault();
			}
		}

		public void Dispose()
		{
			// Dispose of unmanaged resources.
			Map.Dispose();
			// Suppress finalization.
			GC.SuppressFinalize(this);
		}
	}
}
