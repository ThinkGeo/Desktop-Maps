# CustomZoomLevel for ThinkGeoCloudRasterOverlay for Wpf

### Description

The Map Suite WPF CustomZoomLevelForThinkGeoCloudRasterOverlay sample will guide you to how to draw map with custom zoomlevels. This CustomZoomLevelForThinkGeoCloudRasterOverlay sample supports Map Suite 10.0.0.0 and higher and will show you how to create a WPF application using Map Suite WPF components.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CustomZoomLevelForThinkGeoCloudRasterOverlay/Screenshot.gif)

### About the Code

``` csharp
ThinkGeoCloudMapsZoomLevelSet thinkGeoCloudMapsZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
ThinkGeoCloudMapsZoomLevelSet customZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
customZoomLevelSet.CustomZoomLevels.Clear();

customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[3].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[4].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[5].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[6].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[7].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[8].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[9].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[10].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[11].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[12].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[13].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[14].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[15].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[16].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[17].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[18].Scale));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[19].Scale));

double zoomlevel17Scale = thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[19].Scale;
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomlevel17Scale / 2));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomlevel17Scale / 4));
customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomlevel17Scale / 8));
```

### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.

