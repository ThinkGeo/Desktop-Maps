﻿using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a FileGeoDatabase Layer on the map
    /// </summary>
    public partial class FileGeoDatabaseLayerSample : UserControl, IDisposable
    {
        public FileGeoDatabaseLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the FileGeoDatabase layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay fileGeoDatabaseOverlay = new LayerOverlay();
            mapView.Overlays.Add("overlay", fileGeoDatabaseOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            FileGeoDatabaseFeatureLayer fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer(@"../../../Data/FileGeoDatabase/zoning.gdb");
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
            mapView.Refresh();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();            
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

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

    }
}