using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an ESRI Grid Layer on the map
    /// </summary>
    public partial class DisplayESRIGridFile : IDisposable
    {
        public DisplayESRIGridFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the ESRI Grid layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var staticOverlay = new LayerOverlay();
            staticOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(staticOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            var gridFeatureLayer = new GridFeatureLayer(@".\data\GridFile\Mosquitos.grd")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            // Add the layer to the overlay we created earlier.
            staticOverlay.Layers.Add("GridFeatureLayer", gridFeatureLayer);

            // Create a class break style based on the cell values and set area styles based on the values
            var gridClassBreakStyle = new ClassBreakStyle("CellValue");
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 0, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(12, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(24, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(36, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(48, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(60, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(72, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MaxValue, new AreaStyle(new GeoSolidBrush(GeoColors.Transparent))));

            // Take the class break style we created above and set it on zoom level 1 and then apply it to all zoom levels up to 20.
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.  
            gridFeatureLayer.Open();
            var gridFeatureLayerBBox = gridFeatureLayer.GetBoundingBox();
            MapView.CenterPoint = gridFeatureLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(MapView.MapUnit, gridFeatureLayerBBox, MapView.MapWidth, MapView.MapHeight);
            MapView.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

            // Refresh the map.
            _ = MapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        #region Code for creating the sample data in EsriGrid Layer

        //var mos = new ShapeFileFeatureSource(@"./Data/Frisco_Mosquitos.shp");
        //mos.Open();
        //var features = mos.GetAllFeatures(ReturningColumnsType.AllColumns);

        //Dictionary<PointShape, double> points = new Dictionary<PointShape, double>();

        //foreach (var feature in features)
        //{
        //    if (feature.ColumnValues["DateCollec"] == "20190806")
        //    {
        //        int male = 0;
        //        int female = 0;

        //        if (feature.ColumnValues["Male"] != "")
        //            male = int.Parse(feature.ColumnValues["Male"]);

        //        if (feature.ColumnValues["Female"] != "")
        //            female = int.Parse(feature.ColumnValues["Female"]);

        //        points.Add((PointShape)feature.GetShape(), male + female);
        //    }
        //}
        //FileStream stream = new FileStream(@"C:\temp\Mosquitos.grd", FileMode.Create, FileAccess.ReadWrite);
        //GridFeatureSource.GenerateGrid(new GridDefinition(RectangleShape.ScaleUp(mos.GetBoundingBox(),30).GetBoundingBox(), 300, -999, points), new InverseDistanceWeightedGridInterpolationModel(), stream);
        //stream.Close();

        #endregion

    }
}