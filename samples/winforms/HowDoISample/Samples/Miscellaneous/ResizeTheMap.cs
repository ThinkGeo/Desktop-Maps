using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// This samples shows how to resize map while keeping width of world extend unchanged.
    /// </summary>
    public partial class ResizeTheMap : UserControl
    {
        private bool _requireExtentUpdateBySizeChange;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private System.Drawing.Size previousSize;

        public ResizeTheMap()
        {
            InitializeComponent();
            mapView.SizeChanged += MapView_SizeChanged;
        }

        /// <summary>
        /// Set up the map with open street map overlay to show a basic map
        /// </summary>
        private async void Form_Load(object sender, EventArgs e) 
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSnappingMode = ZoomLevelSnappingMode.None;
            previousSize = this.Size;

            InitializeMapViewInternal(mapView);

            var overlay = new OpenStreetMapOverlay
            {
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.OsmMaps
            };

            mapView.Overlays.Add("base", overlay);
            mapView.MapResizeMode = MapResizeMode.PreserveExtent;

            // Get the center point of the MaxExtents.OsmMaps
            var centerPoint = MaxExtents.OsmMaps.GetCenterPoint();
            mapView.CurrentExtent = GetDrawingExtent(centerPoint, mapView.ActualWidth, mapView.ActualHeight);

            await mapView.RefreshAsync();
        }

        private void MapView_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Extent changing from {e.OldExtent} to {e.NewExtent}.");

            if (!_requireExtentUpdateBySizeChange) return;
            var newExtent = MapUtil.GetDrawingExtent(e.OldExtent, mapView.ActualWidth, mapView.ActualHeight);
            _requireExtentUpdateBySizeChange = false;
            if (!(newExtent.Width > MaxExtents.OsmMaps.Width)) return; //Duplicated continents will be displayed
            e.Cancel = true;

            this.BeginInvoke(new MethodInvoker(async () => await UpdateExtent(e)));
            return;
        }

        private async Task UpdateExtent(CurrentExtentChangingMapViewEventArgs e)
        {
            var centerPoint = e.OldExtent.GetCenterPoint();
            mapView.CurrentExtent = GetDrawingExtent(centerPoint, mapView.ActualWidth, mapView.ActualHeight);
            await mapView.RefreshAsync();
        }

        private void MapView_CurrentScaleChanging(object sender, CurrentScaleChangingMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Scaling changing from {e.OldScale} to {e.NewScale}.");
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Extent changed from {e.OldExtent} to {e.NewExtent}.");
        }

        private void MapView_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Scaling changed from {e.OldScale} to {e.NewScale}.");
        }

        private async void MapView_SizeChanged(object sender, EventArgs e)
        {
            var newSize = this.ClientSize;

            // Cancel the previous task if not yet canceled
            // and then create a new cancellation token source for the current operation
            CancelPreviousOperation();
            _cancellationTokenSource = new CancellationTokenSource();

            // Ensure that mapView.CurrentExtent is not null
            var currentCenter = mapView.CurrentExtent?.GetCenterPoint();
            if (currentCenter == null)
            {
                return;
            }

            if (newSize.Width != previousSize.Width || newSize.Height != previousSize.Height)
            {
                await UpdateMapExtentAndZoomLevels(currentCenter);
            }

            // Update the previous size for comparison in the next event
            previousSize = newSize;
        }

        // Helper method to update the extent and zoom levels based on current map size
        private async Task UpdateMapExtentAndZoomLevels(PointShape currentCenter)
        {
            var newExtent = GetDrawingExtent(currentCenter, mapView.ActualWidth, mapView.ActualHeight);

            // Set custom zoom levels based on the new extent size
            var baseScale = MapUtil.GetScale(mapView.MapUnit, newExtent, mapView.ActualWidth, mapView.ActualHeight);
            var customZoomLevelSet = new ZoomLevelSet();
            baseScale = Math.Max(baseScale, customZoomLevelSet.ZoomLevel08.Scale);

            var scale = Math.Max(baseScale, customZoomLevelSet.ZoomLevel08.Scale);
            while (scale > customZoomLevelSet.ZoomLevel20.Scale)
            {
                customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(scale));
                scale /= 2;
            }

            mapView.ZoomLevelSet = customZoomLevelSet;
            mapView.CurrentExtent = newExtent;

            // Refresh the map view with the current cancellation token
            await mapView.RefreshAsync(OverlayRefreshType.Redraw, _cancellationTokenSource.Token);
        }

        // Helper method to cancel the previous operation
        private void CancelPreviousOperation()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        }

        // Helper method to calculate the new extent based on the center point and dimensions
        private static RectangleShape GetDrawingExtent(PointShape centerPoint, double width, double height)
        {
            var resolution = MaxExtents.OsmMaps.Width / width;
            var halfWidth = resolution * width / 2;
            var halfHeight = resolution * height / 2;

            return new RectangleShape(
                centerPoint.X - halfWidth,
                centerPoint.Y + halfHeight,
                centerPoint.X + halfWidth,
                centerPoint.Y - halfHeight
            );
        }

        private static void InitializeMapViewInternal(MapView mapView)
        {
            mapView.ZoomLevelSet = new ZoomLevelSet();

            mapView.ZoomLevelSnappingMode = ZoomLevelSnappingMode.None;
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel02);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel03);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel04);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel05);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel06);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel07);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel08);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel09);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel10);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel11);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel12);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel13);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel14);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel15);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel16);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel17);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel18);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel19);
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new MapView();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(1377, 743);
            mapView.TabIndex = 0;
            mapView.CurrentExtentChanging += MapView_CurrentExtentChanging;
            mapView.CurrentScaleChanging += MapView_CurrentScaleChanging;
            mapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
            mapView.CurrentScaleChanged += MapView_CurrentScaleChanged;
            // 
            // ResizeTheMap
            // 
            Controls.Add(mapView);
            Name = "ResizeTheMap";
            Size = new System.Drawing.Size(1377, 743);
            Load += new EventHandler(Form_Load);
            Load += new EventHandler(MapView_SizeChanged);
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}
