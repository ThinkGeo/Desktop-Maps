# PerformanceSample-ForWpf

### Description
This is a WPF desktop sample for drawing performance test of MapSuite product.
When running the sample, it will render 16,000 count of rectangle shape features at first, these features will be distributed in 4 layers averagely. After clicking Start button the sample application will update 1,600 count of rectangle shape features per 1,000 milliseconds, the time cost of features drawing will display in the application footer.
Customer can modify the update rate, update features count, and enable or disable the layers. 

### Key Points
- The rectangle shape is added to an InMemoryFeatureLayer, the layer uses a ValueStyle to draw the shape. The ValueStyle has 4 count of ValueItems which the item Id is from 0 to 3 and the item AreaStyle uses 4 different of fill colors. The shape also has a column value of 0 at the first time, the ValueStyle will use this value to draw the shape using corresponding AreaStyle.

- In each update, the application will choose 1,600 count of features from all valid features randomly to modify their column values. The column value will be modified to 0, 1, 2 or 3 circularly.

- The CustomLayerOverlay will be used in the update, the application will create a new InMemoryFeatureLayer and add it to this overlay when updating, all determined 1,600 count of features will be added to the layer. The previous overlay will be removed if it exists in the map, then a new overlay will be added to the map. Before removing the previous overlay, the all resources (include all layer tiles and background images) from the overlay will be drawn on a GeoImage, then set the GeoImage to the background image of the new overlay. We do this to make sure only 1,600 count of features would be rendered in each update, and the previous updated features would be displayed correctly in the map.

- Customer can check or uncheck the layers in the right list of the application form, the application will only choose the features which in the checked layers when updating, and the unchecked layers will be hidden in the map.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PerformanceSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
private void FeatureUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
{
    // create a new focus layer that use the layer style from any source layers
    var sourceOverlay = Map.Overlays["SourceOverlay"] as LayerOverlay;
    var layerStyle = (sourceOverlay.Layers.First() as InMemoryFeatureLayer).ZoomLevelSet.ZoomLevel01.CustomStyles.First();
    var focusLayer = new InMemoryFeatureLayer();
    focusLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(layerStyle);
    focusLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

    // change column value of any valid source features randomly, then add the feature to new focus layer
    for (int i = 0; i < UpdateFeatureCount; i++)
    {
        var sourceLayer = sourceOverlay.Layers[featureUpdateRadom.Next(0, layerCount)] as InMemoryFeatureLayer;
        if (sourceLayer.IsVisible)
        {
            var sourceFeature = sourceLayer.InternalFeatures[$"{featureUpdateRadom.Next(0, featureColumnCount)}.{featureUpdateRadom.Next(0, featureRowCount)}"];
            sourceFeature.ColumnValues["State"] = ((Convert.ToInt32(sourceFeature.ColumnValues["State"]) + 1) % 5).ToString();
            focusLayer.InternalFeatures.Add(sourceFeature);
        }
    }

    Dispatcher.Invoke(new Action(() =>
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // save the old overlay as a geoImage, then remove it
        GeoImage focusOverlayBackgroundImage = null;
        if (Map.Overlays.Contains("FocusOverlay"))
        {
            var dirtyFocusOverlay = Map.Overlays["FocusOverlay"] as CustomLayerOverlay;
            focusOverlayBackgroundImage = dirtyFocusOverlay.ToGeoImage(Map.CurrentExtent, Map.MapUnit);
            Map.Overlays.Remove(dirtyFocusOverlay);
            dirtyFocusOverlay.Dispose();
        }

        // create a new FocusOverlay, add it to map, then refresh it
        var focusOverlay = new CustomLayerOverlay(focusOverlayBackgroundImage)
        {
            TileType = TileType.SingleTile,
            DrawingQuality = DrawingQuality.HighSpeed
        };
        focusOverlay.Layers.Add(focusLayer);
        Map.Overlays.Add("FocusOverlay", focusOverlay);
        Map.Refresh(Map.CurrentExtent, focusOverlay);

        stopwatch.Stop();
        InformationTextBlock.Text = $"Refresh Time (ms): {stopwatch.ElapsedMilliseconds}";
    }));
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
