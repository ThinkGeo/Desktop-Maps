using System;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
	/// <summary>
	/// A small overview map that shows where the main map is looking as it pans and turns; click it to move the main map there.
	/// </summary>
	public partial class OverviewMap : IDisposable
	{
		private bool _initialized;
		private (double X, double Y, double Scale, double Rotation) _shown;

		public OverviewMap()
		{
			InitializeComponent();

			MainMap.MapTools.PanZoomBar.Visibility = Visibility.Collapsed;
			PreviewMap.MapTools.PanZoomBar.Visibility = Visibility.Collapsed;

			MainMap.SizeChanged += MainMapSizeChanged;
			Loaded += (s, e) => CompositionTarget.Rendering += OnRendering;
			Unloaded += (s, e) => CompositionTarget.Rendering -= OnRendering;
		}

		// The frame follows every move of the main map, mid-drag included: a frame drawn only
		// when an event says the extent has changed waits for the mouse button.
		private void OnRendering(object sender, EventArgs e)
		{
			if (!_initialized || MainMap.CenterPoint == null) return;

			var now = (MainMap.CenterPoint.X, MainMap.CenterPoint.Y, MainMap.CurrentScale, MainMap.RotationAngle);
			if (now == _shown) return;

			_shown = now;
			UpdatePreviewMap(MainMap.CenterPoint, MainMap.RotationAngle);
		}

		private void InitializeMaps()
		{
			InitPreviewMap();
			double miniMapScaleRatio = 50.0;
			InitializeMap(PreviewMap, 4622340 * miniMapScaleRatio, -10502930, 4330070);

			InitializeMap(MainMap, 4622340, -9338030, 3300450);

			_ = MainMap.RefreshAsync();
			_ = PreviewMap.RefreshAsync();
		}

		private void InitPreviewMap()
		{
			PreviewMap.MapTools.Logo.IsEnabled = false;
			PreviewMap.MapTools.PanZoomBar.IsEnabled = false;

			PreviewMap.ExtentOverlay.RotationMouseButton = MapMouseButton.None;
			PreviewMap.ExtentOverlay.PanMode = MapPanMode.Disabled;
			PreviewMap.ExtentOverlay.DoubleLeftClickMode = MapDoubleClickMode.Disabled;
			PreviewMap.ExtentOverlay.MouseWheelMode = MapMouseWheelMode.Disabled;

			PreviewMap.EditOverlay.CanAddVertex = false;
			PreviewMap.EditOverlay.CanRemoveVertex = false;
			PreviewMap.EditOverlay.CanReshape = false;
			PreviewMap.EditOverlay.CanResize = false;
			PreviewMap.EditOverlay.CanRotate = false;

			PreviewMap.ExtentOverlay.MapMouseDown += PreviewMap_MapMouseDown;

			PreviewMap.EditOverlay.DragControlPointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle =
				PointStyle.CreateSimpleStarStyle(GeoColors.Red, 10);
			PreviewMap.EditOverlay.DragControlPointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel =
				ApplyUntilZoomLevel.Level20;

			var boundingBoxStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(32, GeoColors.Red), GeoColors.Red);
			PreviewMap.EditOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = boundingBoxStyle;
			PreviewMap.EditOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
			PreviewMap.EditOverlay.EditShapesLayer.Open();
		}

		private void UpdatePreviewMap(PointShape centerPoint, double rotationAngle)
		{
			if (centerPoint == null || MainMap.ActualWidth <= 0 || MainMap.ActualHeight <= 0)
			{
				return;
			}

			var extentShape = GetExtentShape(centerPoint, rotationAngle);
			PreviewMap.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
			PreviewMap.EditOverlay.EditShapesLayer.InternalFeatures.Add(new Feature(extentShape));
			PreviewMap.EditOverlay.CalculateAllControlPoints();
			_ = PreviewMap.EditOverlay.RefreshAsync();
		}

		private void InitializeMap(MapView map, double scale, double x, double y)
		{
			map.MapUnit = GeographyUnit.Meter;

			var layerOverlay = new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey);

			map.Overlays.Add(layerOverlay);
			map.CenterPoint = new PointShape(x, y);
			map.CurrentScale = scale;
		}

		private void MainMapSizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (!_initialized)
			{
				if (e.NewSize.Width <= 0 || e.NewSize.Height <= 0 ||
					PreviewMap.ActualWidth <= 0 || PreviewMap.ActualHeight <= 0)
				{
					return;
				}

				_initialized = true;
				InitializeMaps();
			}

			UpdatePreviewMap(MainMap.CenterPoint, MainMap.RotationAngle);
		}

		private void CompassButton_Click(object sender, RoutedEventArgs e)
			=> _ = MainMap.ZoomToAsync(MainMap.CenterPoint, MainMap.CurrentScale, 0);

		private void PreviewMap_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
		{
			var worldLocation = new PointShape(e.InteractionArguments.WorldX, e.InteractionArguments.WorldY);
			var extentShape = GetExtentShape(worldLocation, MainMap.RotationAngle);
			PreviewMap.EditOverlay.EditShapesLayer.Clear();
			PreviewMap.EditOverlay.EditShapesLayer.InternalFeatures.Add(new Feature(extentShape));
			PreviewMap.EditOverlay.CalculateAllControlPoints();

			_ = MainMap.ZoomToAsync(worldLocation, MainMap.CurrentScale, MainMap.RotationAngle,
				new MapAnimationSettings { Duration = 10 });
		}

		private RingShape GetExtentShape(PointShape centerPoint, double rotationAngle)
		{
			var resolution = MapUtil.GetResolutionFromScale(MainMap.CurrentScale, MainMap.MapUnit);
			var halfWidth = MainMap.ActualWidth / 2 * resolution;
			var halfHeight = MainMap.ActualHeight / 2 * resolution;

			return GetRotatedExtent(centerPoint.X, centerPoint.Y, halfWidth, halfHeight, rotationAngle);
		}

		private static RingShape GetRotatedExtent(double centerX, double centerY,
			double halfWidth, double halfHeight,
			double rotationDegrees)
		{
			double rad = rotationDegrees * Math.PI / 180.0;
			double cos = Math.Cos(rad);
			double sin = Math.Sin(rad);

			// Define unrotated corners relative to center
			var corners = new (double x, double y)[]
			{
				(-halfWidth,  halfHeight), // UL
		        ( halfWidth,  halfHeight), // UR
		        ( halfWidth, -halfHeight), // LR
		        (-halfWidth, -halfHeight)  // LL
	        };

			var rotated = new Vertex[4];
			for (int i = 0; i < corners.Length; i++)
			{
				double x = corners[i].x;
				double y = corners[i].y;
				double xr = x * cos - y * sin;
				double yr = x * sin + y * cos;
				rotated[i] = new Vertex(centerX + xr, centerY + yr);
			}

			return new RingShape(rotated);
		}

		public void Dispose()
		{
			MainMap.Dispose();
			PreviewMap.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
