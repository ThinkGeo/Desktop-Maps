﻿using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to style polygon data using an FleeBooleanStyle
    /// </summary>
    public partial class CreateFleeBooleanStyleSample : UserControl, IDisposable
    {                
        public CreateFleeBooleanStyleSample()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a layer with polygon data
            ShapeFileFeatureLayer countries02Layer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Countries02.shp");
            
            // Project the layer's data to match the projection of the map
            countries02Layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(countries02Layer);
            
            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Add the fleeBooleanStyle to the countries02 layer
            AddFleeBooleanStyle(countries02Layer);

            // Set the map extent
            mapView.CurrentExtent = MaxExtents.SphericalMercator;
            mapView.Refresh();
        }

        /// <summary>
        /// Create a fleeBooleanStyle and add it to the countries02 layer
        /// </summary>
        private void AddFleeBooleanStyle(ShapeFileFeatureLayer layer)
        {
            // Highlight the countries that are land locked and have a population greater than 10 million  
            string expression = "(POP_CNTRY>10000000 && LANDLOCKED=='Y')";
            FleeBooleanStyle landLockedCountryStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this  
            // to access the Convert.Toxxx methods to convert variable types  
            landLockedCountryStyle.StaticTypes.Add(typeof(System.Convert));

            // The math class might be handy to include but in this sample we do not use it  
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));  

            landLockedCountryStyle.ColumnVariables.Add("POP_CNTRY");
            landLockedCountryStyle.ColumnVariables.Add("LANDLOCKED");

            landLockedCountryStyle.CustomTrueStyles.Add(new AreaStyle(new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1), new GeoSolidBrush(GeoColors.Yellow)));
            landLockedCountryStyle.CustomFalseStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69)));

            // Add the landLockedCountryStyle to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(landLockedCountryStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the area style on every zoom level on the map. 
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}