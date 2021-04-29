# Desktop Maps v10

## Repository Layout

`/samples`: A collection of samples for ThinkGeo version 10 for WPF and Winforms editions.  These are no longer supported but may contain valuable insight into various features of those products.


# [Bread Crumb Trail Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/BreadCrumbTrailSample)
An early project of the Code Community, “Vehicle Direction”, showed how to rotate the icon of a moving vehicle based on the direction. In today’s project, we are going one step further and we are showing how to display dynamically a bread crumb trail as a trailing tail behind the moving vehicle. For this purpose, we are creating a new LineShape with the latest points at every new position.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/BreadCrumbTrailSample/Screenshot.gif)

# [Building 3D Style Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/Building3DSample)

This project shows to create simulated 3D buildings with WinForms Map control and shapefile.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/Building3DSample/Screenshot.png)

# [Cache Generator Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CacheGeneratorSample)

This utility can help us to generate tile cache.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CacheGeneratorSample/Screenshot.png)

# [Centering On Moving Vehicle With Tolerance Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CenteringOnMovingVehiclesWithTolerance)

In the previous project, we showed how to center the map on a moving vehicle. While this is great, it has the disadvantage of having to refresh the map each time the vehicle changes position. In this project, we respond to this inconvenience by using a set tolerance used for determining if the map needs to be refreshed or not. If the vehicle moves within a rectangle of a certain size located in the center of the current extent of the map, the map will not refresh and only the moving vehicle will. If it moves outside the tolerance area, the entire map will be refreshed and the tolerance recalculated.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CenteringOnMovingVehiclesWithTolerance/Screenshot.gif)

# [ThinkGeo Cloud Maps Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CloudMapSample)

This sample demonstrates how you can display ThinkGeo Cloud Maps in your Map Suite GIS applications. It will show you how to use the XYZFileBitmapTileCache to improve the performance of map rendering. ThinkGeoCloudMapsOverlay uses the ThinkGeo Cloud XYZ Tile Server as raster map tile server. It supports 5 different map styles:
- Light
- Dark
- Aerial
- Hybrid
- TransparentBackground

ThinkGeo Cloud Maps support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CloudMapSample/Screenshot.gif)

# [ThinkGeo Cloud Vector Maps Sample for Winforms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CloudVectorMapsSample)

This sample demonstrates how you can draw the map with Vector Tiles requested from ThinkGeo Cloud Services in your Map Suite GIS applications, with any style you want from [StyleJSON (Mapping Definition Grammar)](https://wiki.thinkgeo.com/wiki/thinkgeo_stylejson). It will show you how to use the XyzFileBitmapTileCache and XyzFileVectorTileCache to improve the performance of map rendering. It supports have 3 built-in default map styles and more awasome styles from StyleJSON file you passed in, by 'Custom':
- Light
- Dark
- TransparentBackground
- Custom

ThinkGeo Cloud Vector Maps support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CloudVectorMapsSample/Screenshot.gif)

# [Cluster Point Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/ClusterPointSample)

ClusterPointStyle is integrated into Map Suite's styles. In this project you will see how to use the ClusterPointStyle for clustering various features into one. Sometimes, the map may have too many features which are stacked on top of each other making the map illegible at higher zoom levels. Clustering is a useful technique as it allows you to group together various features into one labeled symbol with the count of all the features. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/ClusterPointSample/Screenshot.png)

# [Create GRID Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CreateGRIDSample)

Today’s sample shows the new feature available in this may release Map Suite 5 for creating GRID files. A GRID is a raster format that defines a geographic space as an array of equally sized squares (cells) arranged in rows and columns. Each cell stores a numeric value that represents an attribute (such as elevation, surface slope, soil pH etc.) for that unit of space. Each GRID cell is referenced by its x, y coordinate location. Typically a GRID file is created based on some sample points with known values. In today’s sample, we take the example of creating a GRID file based on a point based shapefile representing soil pH values of some sample locations in a field. Using the Inverse Weighted Distance algorithm for interpolation, we create the GRID with the pH value for the entire extent of the field. Look at the code and comments for more details on how GRID files get generated and displayed on the map. This sample is a Desktop application but GRID can be used in all the editions of Map Suite.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CreateGRIDSample/Screenshot.png)

# [Create Inner Ring Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CreateInnerRingSample)

In this Desktop project we create inner rings for a polygon, based on another polygon, using the **GetDifference** method. We also learn how to perform a Union on a collection of polygons, and how to set up the **TrackEnded** event.

To use this app, you need to track a polygon, double click to end it, and it will create an inner ring based on the unioned result of the polygons that are completely within the tracked polygon. You can look at the **TrackEnded** event handler to see all the different operations that are taking place for that task.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CreateInnerRingSample/Screenshot.gif)

# [Custom Track Line Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CustomTrackLineSample)

In today’s project, we are going to see how to extend the TrackInteractiveOverlay in the Desktop edition to have the desired behavior when tracking a line. In this case, we show how to override the MouseDownCore function to have the line being tracked at left mouse click and have the last vertex added deleted at right mouse click.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CustomTrackLineSample/Screenshot.gif)

# [Custom Track Polygon Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/CustomTrackPolygonSample)

Learn how to extend the **TrackInteractiveOverlay** to add behaviors, like deleting the last added vertex when right-clicking the track line.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CustomTrackPolygonSample/ScreenShot.png)

# [Detect Gps Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DetectGpsSample)

Upon request of our users, today we publish a project that is the Desktop version of “Detect GPS” for Web. Notice how we use ValueStyle and change the column value of the feature based on the Spatial Query feature at each new position. We chose this structure so that you can have more flexibility for adding more than one moving vehicle features to the InMemoryFeatureLayer. For that, you can pretty much keep the same code and just add an outer loop for looping thru all the moving features.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DetectGpsSample/Screenshot.png)


# [Display Cad File Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DisplayCadFileSample)

This sample demonstrates how you can read data from an CAD file(*.dwg, *.dxf) in your Map Suite GIS applications, and how to render it with CAD embedded style as well as a customized style. This Cad File support would work in all of the Map Suite controls such as Wpf, Web, MVC and WebApi. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DisplayCadFileSample/Screenshot.png)


# [Display File GeoDatabase Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DisplayFileGeoDatabaseSample)

This sample demonstrates how you can read data from an ESRI FileGeodatabase, and you will find the code as straightforward as consuming any other data source in Map Suite.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DisplayFileGeoDatabaseSample/Screenshot.png)

# [Display Wms Raster Layer Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DisplayWmsRasterLayerSample)

This sample demonstrates how you use WmsRasterLayer to render wms server in your Map Suite GIS applications. This WmsRasterLayer support would work in all of the Map Suite controls such as Wpf, Web, MVC and WebApi. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DisplayWmsRasterLayerSample/Screenshot.png)

# [Distance Query On Projected Layers Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DistanceQueryOnProjectionLayersSample)

This sample shows how to use the method GetFeaturesWithDistanceOf when the data is projected. We just input the unit which is projected to, do not need to mind what the real unit is for the internal data before projection.  It works fine with Map Suite Assemblies 4.5.54.0 or later.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DistanceQueryOnProjectionLayersSample/ScreenShot.png)

# [Drag Icon Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DragIconSample)

In today’s sample, we show how to drag icons representing vehicle on the map. This is a handy feature if you want to give your users the ability to drag and drop some non stationary features such as vehicles. You can see that to accomplish this functionality, you can use EditInteractiveOverlay as it already has all the necessary logic for dragging purposes. Look at the code to see how to set up that overlay to have the expected behavior.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DragIconSample/Screenshot.gif)

# [Drag Point Advanced Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DragPointAdvancedSample)

This project is an example of how extensible **EditInteractiveOverlay** is for the editing needs of the user. You can see how to change the styles of the vertex being dragged based on the Shift key, and how to have the vertex snap to any feature.

Thanks to the protected override functions such as **KeyDownCore**, **KeyUpCore**, **MouseUpCore** and **DrawCore**, these functionalities can easily be implemented by inheriting from **EditInteractiveOverlay**.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DragPointAdvancedSample/Screenshot.gif)

# [Drag Point Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DragPointSample)

In this project, we focus our attention on how to control the style of the control points. You will see how to override the **DrawCore** function of **EditInteractiveOverlay**.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DragPointSample/Screenshot.gif)

# [Drag Point With Label Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DragPointWithLabelSample)

In this project, dedicated to **EditInteractiveOverlay**, you will see how easy it is to add some labeling to your dragged control point, showing dynamic information. Here we show how to display the distance from the dragged control point to the closest point of a reference shape. Also, to augment the user experience, the closest point of the reference shape is also shown varying as the control point is dragged around.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DragPointWithLabelSample/Screenshot.gif)

# [Drag Vertex Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DragVertexSample)

The purpose of today’s project is not so much as show a new technology as to show an improvement in an existing one. We are already familiar with EditInteractiveOverlay with projects such as “Dragged PointStyle with Label”, “Snap To Layer” and “Snapping to Vertex”. But those projects showed EditInteractiveOverlay on small shapes. Using EditIntercticeOverlay on complex shapes became not very responsive. We improved that and now you can drag, resize, rotate and drag individual vertex of a complex polygon made of thousand of vertices with good responsiveness as you can see in this example.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DragVertexSample/Screenshot.gif)

# [Dragging Icon Advanced Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DraggingIconAdvancedSample)

This project is a more complete version of a previous project “Dragging Icon”. In addition to showing how to use EditInteractiveOverlay for dragging and dropping features represented by an icon, we also show how to add new features on the map by left double clicking on the map. You will also see how to remove a feature by right double clicking on its icon.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DraggingIconAdvancedSample/Screenshot.gif)

# [Draw Custom Exception Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DrawCustomExceptionSample)

This sample shows how you can suppress and draw exceptions in desktop overlays instead of throwing them. There is a little-known feature in the Map Suite Desktop Edition Overlay class that allows you to draw an exception in the event an exception is thrown during the drawing process. We have a default image we draw in this case; however, you can override this using the DrawExceptionCore method and draw whatever you want. By default we always throw expections, but to start drawing them you can use the Overlay.DrawExceptionMode property.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DrawCustomExceptionSample/Screenshot.gif)

# [Dynamic Track Shapes Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/DynamicTrackShapesSample)

This Desktop project shows how to handle **TrackOverlay** to obtain dynamic information about the shape being tracked.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DynamicTrackShapesSample/ScreenShot.png)

# [Earthquakes Heat Map Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/EarthQuakeHeatMapSample)

After the project on displaying swine flu data using the heat map technique, here you learn how to apply parameters other than strictly spatial distribution to affect the coloring of the map.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EarthQuakeHeatMapSample/ScreenShot.png)

# [Ecw Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/EcwSample)

The Ecw Sample template represents an ECW file for drawing on the map.

ECW: ECW is a wavelet image compression system developed by ER Mapper.It allows you to combine and compress large sets of satellite images into a single file. 
The images can be accessed very quickly at a variety of scales. It is very popular in the GIS community.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EcwSample/ScreenShot.png)

# [Edit Geometry Of Shapefile Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/EditGeometryOfShapeFile)

The purpose of this sample is to show how to update the geometry of a feature of a shapefile in one step. This sample is useful for anyone wanting to actualize the geometry part of its data. You can see how only a few lines of code are necessary for this process, and that the spatial index gets automatically updated after calling the committing the change. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EditGeometryOfShapeFile/ScreenShot.png)

# [Edit Grid Layer Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/EditGridLayerSample)

This WinForms project demonstrates how you can update a grid shape file using a spatial query. The elements in the file are rendered using ClassBreakStyles, and change when the values of the Features are incremented.

We have dramatically improved the performance for GridFeatureLayer on MapSuite10.0. The performance compare is as below:

| **Feature Count**                             | **Version**   | **Times(ms)**     |
| --------------------------------------------- | ------------- | ----------------- |
| 1164800                                       | Old           | 13105             | 
| 1164800                                       | New           | 3735              |
| 11680                                         | Old           | 188               |
| 11680                                         | New           | 175               |

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EditGridLayerSample/Screenshot.png)

# [Edit Rectangles Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/EditRectanglesSample)

This sample shows how to extend the **EditInteractiveOverlay** rectangles as shapes, rather than polygon shapes, by setting special column values. For features (both *Well Known Text* and *Well Known Binary*), the concept of a rectangle is not supported and typically rectangles are handled as polygons. This feature allows users to modify the rectangle but requires that the modification keep a rectangular form. The rectangle doesn't need to be straight as long as all of the corner angles are at 90 degrees relative to each other.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EditRectanglesSample/Screenshot.gif)

# [Feature Ids To Exclude Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/FeaturesToExcludeSample)

The purpose of this project is to show how to use the **FeatureIdsToExclude** collection of **FeatureLayer**. In the project, you will see how you can exclude some features from being part of the **GetFeaturesNearestTo** function. Using that collection is a handy method for not taking into account some features in doing spatial queries, searching and even drawing without having to change the structure of the layer or create another layer.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/FeaturesToExcludeSample/Screenshot.gif)

# [Gdal Raster Layer Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/GdalRasterLayerSample)

This sample demonstrates how you can load raster format data supported by Gdal.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/GdalRasterLayerSample/Screenshot.png)

# [Geocoding Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/GeocodingSample)

The Map Suite Geocoder “How Do I?” solution offers a series of useful how-to examples for using the Map Suite Geocoder component. The bundled solution comes with a small set of sample data from Chicago, IL and demonstrates geocoding, reverse geocoding, batch geocoding, use of fuzzy matching logic, getting the closest street number to a point, and much more. Full source code is included in both C# and VB.NET languages; simply select your preferred language to download the associated solution.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/GeocodingSample/Screenshot.gif)

# [Get Features Clicked On Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/GetFeaturesClickedOnSample)

The purpose of this project is to show the technique for finding the feature the user clicked on. To give the user the expected behavior, a buffer in screen coordinates needs to be set so that the feature gets selected within a constant distance in screen coordinates to where the user clicked, regardless of the zoom level. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/GetFeaturesClickedOnSample/ScreenShot.png)
