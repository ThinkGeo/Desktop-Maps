using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FileGeoDatabaseLayerSample : UserControl
    {
        public FileGeoDatabaseLayerSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay fileGeoDatabaseOverlay = new LayerOverlay();
            mapView.Overlays.Add(fileGeoDatabaseOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            FileGeoDatabaseFeatureLayer fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer(@"./Data/FileGeoDatabase/zoning.gdb");
            fileGeoDatabaseFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            fileGeoDatabaseFeatureLayer.ActiveLayer = "zoning";

            // Add the layer to the overlay we created earlier.
            fileGeoDatabaseOverlay.Layers.Add("Zoning", fileGeoDatabaseFeatureLayer);

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Black, new GeoSolidBrush(new GeoColor(50, GeoColors.Blue)));
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.  
            fileGeoDatabaseFeatureLayer.Open();
            mapView.CurrentExtent = fileGeoDatabaseFeatureLayer.GetBoundingBox();

            //Refresh the map.
            await mapView.RefreshAsync();
        }

        #region Create Sample Data
        //if (Directory.Exists("zoning.gdb"))
        //    Directory.Delete("zoning.gdb", true);

        //FileGeoDatabaseFeatureSource.CreateFileGeoDatabase(@"C:\repo\thinkgeo-desktop-maps\samples\wpf\HowDoISample\Data\FriscoZoning\zoning.gdb");
        //FileGeoDatabaseFeatureSource.CreateTable(@"C:\repo\thinkgeo-desktop-maps\samples\wpf\HowDoISample\Data\FriscoZoning\zoning.gdb", "zoning", WellKnownType.Multipolygon, new string[] { "zoning", "pd", "sup", "ord1", "ord2" });


        //FileGeoDatabaseFeatureLayer fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer(@"C:\repo\thinkgeo-desktop-maps\samples\wpf\HowDoISample\Data\FriscoZoning\zoning.gdb");
        //fileGeoDatabaseFeatureLayer.ActiveLayer = "zoning";
        //    fileGeoDatabaseFeatureLayer.Open();

        //    var features = fileGeoDatabaseFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);



        //FileGeoDatabaseFeatureSource fileGeoDatabaseFeatureSource = new FileGeoDatabaseFeatureSource(@"C:\repo\thinkgeo-desktop-maps\samples\wpf\HowDoISample\Data\FriscoZoning\zoning.gdb", FileAccess.ReadWrite);
        //fileGeoDatabaseFeatureSource.Open();

        //// If the shapefile doesnt have an index file we build it now then open it getting it ready for the copy
        //// ShapeFileFeatureSource.BuildIndexFile(@"C:\Users\davidrehagen\Downloads\Frisco Data\FriscoPOI\Restaurants.shp", BuildIndexMode.DoNotRebuild);
        //ShapeFileFeatureSource shapeFileFeatureSource = new ShapeFileFeatureSource(@"C:\repo\thinkgeo-desktop-maps\samples\wpf\HowDoISample\Data\FriscoZoning\Zoning.shp");
        //ShapeFileFeatureSource.BuildIndexFile(shapeFileFeatureSource.ShapePathFilename, BuildIndexMode.DoNotRebuild);
        //shapeFileFeatureSource.Open();

        //// Read all of the features
        //Collection<Feature> features = shapeFileFeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

        //// Begin the transaction
        //fileGeoDatabaseFeatureSource.BeginTransaction();
        //int counter = 0;

        //foreach (var feature in features)
        //{
        //    // Add the feature from the shapefile to the sqlite featuresource
        //    fileGeoDatabaseFeatureSource.AddFeature(feature);

        //    // Every 1000 features we will commit since the transactions are kept in memory
        //    if (counter % 1000 == 0 && counter != 0)
        //    {
        //        fileGeoDatabaseFeatureSource.CommitTransaction();
        //        fileGeoDatabaseFeatureSource.BeginTransaction();
        //    }

        //    counter++;
        //}

        //// We do a final commit
        //fileGeoDatabaseFeatureSource.CommitTransaction();

        //// Close all the feature sources
        //fileGeoDatabaseFeatureSource.Close();
        //shapeFileFeatureSource.Close();
        //MessageBox.Show(counter.ToString() + " records processed!");
        #endregion

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1208, 682);
            this.mapView.TabIndex = 0;
            // 
            // FileGeoDatabaseLayerSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "FileGeoDatabaseLayerSample";
            this.Size = new System.Drawing.Size(1208, 682);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}