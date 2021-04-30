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

# [Get Zoom Level Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/GetZoomLevelsSample)

This example demonstrates how to get the zoom level of the map each time we change its extent. Using custom zoom levels, you will see how to get the zoom level with its characteristics such as the upper and lower scale defining it. You can read the comments inside the project to better understand the relationship of scales with zoom levels.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/GetZoomLevelsSample/Screenshot.gif)

# [Graphic Logo Adornment Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/GraphicLogoAdornments)

This sample shows how you can display your logo on the map using an AdornmentLayer. The advantage of using an Adornment is that the graphic stays in place and doesn't move as you pan your map. The sample should work for various kinds of logos and allow you to change the position using the AdornmentLayer's properties.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/GraphicLogoAdornments/Screenshot.png)

# [Heat Map Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/HeatMapSample)

Heat maps is a technique increasingly used in various fields such in biology and other fields. See http://en.wikipedia.org/wiki/Heat_map. They are also used for displaying areas of webs page most frequently scanned by users. http://csscreme.com/heat-maps/.

At ThinkGeo, we are taking this concept to GIS and applying it to geographic maps. Heat maps are a great way to give the users a visually compelling representation of the distribution and intensity of geographic phenomenon. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/HeatMapSample/ScreenShot.png)

# [Hello World Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/HelloWorldSample)

This sample shows you how to get started building your first application with the Map Suite Desktop for WinForms 10.0.0.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/HelloWorldSample/Screenshot.png)

# [Here Real Time Traffic Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/HereRealTimeTrafficSample)

This sample demonstrates how you use HERE Real-time Traffic to render map in your Map Suite GIS applications.
Before running this sample, you need to config "AppId" and "AppCode" in App.Config. If you have ESRI or Here developer account, you can generate them on [Here's official Web Site](https://developer.here.com/).

HERE Real-time Traffic provides the closest thing to a live depiction of the road. It identifies where, when and why traffic congestion occurs, and delivers up-to-the-minute information on the road conditions and incidents that could set a driver back.

This HereRealTimeTrafficLayer is supported in all of the Map Suite controls such as WPF, Web, MVC and WebAPI.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/HereRealTimeTrafficSample/Screenshot.gif)

# [How Do I Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/HowDoI)

The "How Do I?" samples collection is a comprehensive set containing dozens of interactive samples.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/HowDoI/Screenshot.png)

# [Intersect Line Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/IntersectLineSample)

In today’s project, we show how to split a line based on an intersecting line. To accomplish this task, basically two steps are needed. First, you need to find the crossing point using the GetCrossing function and then you split the line based on the crossing point using the GetLineOnLine function. If you are in the utilities industry working with electric network, gas pipes etc, you will find this project useful.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/IntersectLineSample/Screenshot.gif)

# [Jpeg2000 Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/Jpeg2000Sample)

The Jpeg2000 Sample template represents a .JP2 (JPEG2000) image type to be drawn on the map.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/Jpeg2000Sample/ScreenShot.png)

# [Kml Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/KmlSample)

KML is the file format for displaying geographic data in a Google Earth browser such as Google Earth. Now, you can also display such a file on a Map Suite control. Thanks to its flexible architecture to extent to new file formats, in today’s project we wrote the logic for supporting KML. Look at the class KmlfeatureSource inheriting from FeatureSource to see how the logic for reading FML files was implemented. As well, you can see the class KmlStyle inheriting from Style for the drawing logic. And feel free to modify and improve those classes with your own implementations.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/KmlSample/ScreenShot.png)

# [Labeling Based On Size Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/LabelingBasedOnSize)

This project shows some advanced uses of the **ClassBreakStyle** to show how to label countries based on the area. You will notice that we also take advantage of the various zoom level sets for labeling purposes. The result is an eye pleasing labeling of the countries,  with the size proportionate to the countries’ area, with more countries' labels appearing as you zoom in.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/LabelingBasedOnSize/ScreenShot.png)

# [Line Style With Increment Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/LineStyleWithIncrementsSample)

In this WinForms desktop project, we show how to create a custom **LineStyle** for showing distance increment at a regular interval (every tenth kilometer). Having this **LineStyle** can be very handy when dealing with line networks, such as roads or railways.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/LineStyleWithIncrementsSample/ScreenShot.png)

# [Long Lat Graticule Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/LongLatGraticuleSample)

After the projects on North Arrow and Compass, we created another project related to Adornment Layer to have a more stylish map and give more information to the user navigating the map.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/LongLatGraticuleSample/ScreenShot.png)

# [MDI Form Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/MDIFormSample)

In today’s project, we show how to have the WinformsMap control in a MDI form. Using an MDI form with a map background can represent a challenge. You will see in this project the technique to display properly a child form on top of the map. You can also notice how the map is accessed from the child form by plotting points on it from the child form.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/MDIFormSample/Screenshot.png)

# [Mr Sid Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/MrSidSample)

MrSID (pronounced Mister Sid) is an acronym that stands for multiresolution seamless image database. In order to run this sample, development build 10.0.0.0 or later is required.

This sample includes a map with MrSID as base overlay, the MrSID shows the image data of the world range.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/MrSidSample/ScreenShot.png)

# [Multi Geo Raster Layer Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/MultiGeoRasterLayerSample)

MapSuite API has RasterLayer from which inherits MrSIDRasterLayer and ECWRasterLayer etc. If we have many raster files, we would need to add all the raster files as separate layer. However this has a performance issue. In this project, we show how to create a class MultiGeoRasterLayer that treats all the raster file as one layer.
              
This class show how to do that using JPEG images with its associating JGW world file. It speeds up the loading of a large number of Raster layers by loading and drawing on demand only the files in the current extent. It loads a reference file that contains the bounding box, path and file information for all of the Raster files. We load this information into an in-memory spatial index. When the map requests to draw the layer, we find the Rasters that are in the current extent, create a layer on-the-fly, call their Draw method and then close them. In this way, we load on demand only the files that are in the current extent.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/MultiGeoRasterLayerSample/Screenshot.png)

# [Multiple Labels Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/MultiplelabelsSample)

This sample shows how you can display multiple labels for a given point or feature. You can do this by setting a single **TextStyle** or multiple **TextStyles**. If you use a single **TextStyle**, you can simply use a pattern like "[ColumnName1][ColumnName2]..." and when Map Suite displays the text it will combine the values of the columns in your pattern. If you use a different styling method, you will need to manually control the offset of each piece of text to avoid overlapping.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/MultiplelabelsSample/Screenshot.gif)

# [Nldas Ascii Grid Layer Sample for WinFoms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/NLDASAnalysisSample)

This WinForms project demonstrates how you can create a North American Land Data Assimilation System (NLDAS) grid layer. In this sample, you can find what NLDAS grid cells are fully contained in a given Shape file and what cells are partially contained. For each partially contained cell we can calculate what percent of the cell area is contained within the ShapeFile. Please refer to [NLDAS grid](https://ldas.gsfc.nasa.gov/nldas/NLDASspecs.php) for details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/NLDASAnalysisSample/Screenshot.png)

# [Native Tab File Support Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/NativeTabFilesSupportSample)

One of the most exciting new features in MapSuite 5.0 is native TAB file support. In the past, the FDO extension was used for displaying TAB files in MapSuite. Now with the new TabFeatureLayer, we have a simpler and more stable method of working with TAB files. 

This Code Community project demonstrates how to load and display a TAB file using the new TabFeatureLayer. The example also allows you to add, edit and delete features from the TAB file.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/NativeTabFilesSupportSample/Screenshot.png)

# [Numbered Grid Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/NumberedGridSample)

In many atlases, you can see maps with a numbered grid to give the page reference for a more detailed map. In today’s project, we show how to construct such a grid. Based on the extent of the feature, the number of columns and rows, a grid can be created with the page number in each cell.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/NumberedGridSample/Screenshot.png)

# [Open Street Map Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/OpenStreetMapSample)

OpenStreetMap (OSM) is a collaborative project to create free geographic data for the entire world. It can be thought of as a "Free Wiki World Map". The latest version of MapSuite now supports this.  

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/OpenStreetMapSample/ScreenShot.png)

# [Overlays Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/OverlaysSample)

Discover how to use Overlays to build up your map, or to add existing basemaps to your application. 
The sample can show the following four basemaps:
  1. Google Maps
  2. Bing Maps
  3. World Kit Maps
  4. Open street Maps

It can display different styles of maps by setting the map type.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/OverlaysSample/Screenshot.png)

# [Preset Vertex To Tracked Polygon Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/PresetVertexToTrackedPolygon)

This Desktop sample shows how to extend TrackInteractiveOverlay to give the ability to add a preset vertex to a track polygon while tracking. This can be handy in the situations where you need to add a vertex outside the current extent of the map or when you need to add a vertex with precise X and Y values. In the custom PresetVertexTrackInteractiveOverlay, you can see how the protected function GetTrackingShapeCore was overridden to implement this tracking behavior of the polygon.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/PresetVertexToTrackedPolygon/Screenshot.png)

# [Quickstart Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/QuickstartSample)

The Winforms QuickStart Guide will guide you through the process of creating a sample application and will help you become familiar with Map Suite. This QuickStart Guide supports Map Suite 10.0.0.0 and higher and will show you how to create a Winforms application.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/QuickstartSample/Screenshot.png)

# [Raster Projection Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/RasterProjectionSample)

This Sample template represents an projection transform for ECW raster image. This sample can work on both Linux and Windows. 

ECW: ECW is a wavelet image compression system developed by ER Mapper.It allows you to combine and compress large sets of satellite images into a single file. 
The images can be accessed very quickly at a variety of scales. It is very popular in the GIS community.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/RasterProjectionSample/ScreenShot.png)

# [Rendering Points Performance Test Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/RenderingPointsPerformanceTestSample)

1. In Map Suite, a shape (a shape object inherited from the BaseShape class such as PointShape, LineShape, etc) is heavier than a feature. You can treat a feature as a holder of a byte array (Well Known Binary), it’s lightweight and doesn’t have too many methods to manipulate its core data (Well Known Binary). Shape, however, is heavy, it provides all the info as well as methods against the data.  You can cast from a feature to a shape and vice versa.

1. To update a feature, usually, we need to convert a feature to a shape, update the shape and then convert it back to a feature. This will create a new shape and a new feature, which is more straightforward but the cost is high. Below is a method updating a point feature by adding 1 to its X and Y.

```csharp
       private void UpdateFeatureThroughShape(Feature feature)
        {
            byte[] wkb = feature.GetWellKnownBinary();
            PointShape pointShape = new PointShape(wkb);
            pointShape.X += 1;
            pointShape.Y += 1;
            feature = pointShape.GetFeature();
        }
```

3. You can update a feature by directly updating its WKB. The following method updates a point feature by adding 1 to its X and Y. It’s the most efficient way where we manipulate a byte array without creating any new object. It is not straightforward and you need to have a deep understanding of the format of WKB. That’s what we are doing in this sample. We will add more APIs to Feature to make it straightforward and efficient.

```csharp
       private void UpdateFeatureThroughWKB(Feature feature)
        {
            byte[] wkb = feature.GetWellKnownBinary();
            double x = BitConverter.ToDouble(wkb, 5);
            double y = BitConverter.ToDouble(wkb, 13);
            BitConverter.GetBytes(x + 1).CopyTo(wkb, 5);
            BitConverter.GetBytes(y + 1).CopyTo(wkb, 13);
            feature.SetWellKnownBinary(wkb);
        }
```

4. The “Grid size in Pixel” textbox let you set the size of the grid in which up to one point can be displayed, the bigger the grid is, the more sparse the points will be. This is an optimization with which we can avoid showing too many points on the map.  Play with it and you can see the performance differences.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/RenderingPointsPerformanceTestSample/Screenshot.gif)

# [Routing Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/RoutingIndexGenerator)

The Map Suite Routing Index Generator is a utility that will allow you to generate routing index files (“.rtg” and “.rtx”) from World Streets .sqlite database. These routing index files will be used by the Map Suite Routing Extension in order to calculate routes and driving directions. This utility allows you to specify things that one-way road information, as well as configuring the road speed and type of routes you would like to calculate. It is easily extendable to allow you to add code to deal with other routing situations.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/RoutingIndexGenerator/Screenshot.png)

# [Routing Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/RoutingSample)

[Sample Data Download](http://wiki.thinkgeo.com/wiki/_media/routing/routing_howdoi_samples_data.zip)

The Map Suite Routing “How Do I?” solution offers a series of useful how-to examples for using the Map Suite Routing extension. The bundled solution comes with a small set of routable street data from Austin, TX and demonstrates simple routing, avoiding specific areas, getting turn-by-turn directions, optimizing for the Traveling Salesman Problem, and much more. Full source code is included in both C# and VB.NET languages; simply select your preferred language to download the associated solution.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/RoutingSample/ScreenShot.png)

# [Select At Track Shape Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/SelectAtTrackShapes)

In today’s Desktop project, we combine the skills we learned in the samples “Spatial Query A Feature Layer” and “Track And Edit Shapes”. You can see how we use the event TrackEnded to get the RectangleShape from the tracked shape of TrackOverlay to do the spatial query. In this example, we use a Rectangle but you could also very easily use another shape such as Polygon, Circle, etc.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/SelectAtTrackShapes/Screenshot.gif)

# [Shapefile Encryption Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/ShapeFileEncryptionSample)

In today’s project, we are looking at a way to encrypt shapefiles to prevent them from being used outside the application. We show how to encrypt and decrypt shapefiles using streams. You will see that a very simple encryption algorithm is used but by looking at the example, you will be able to implement your own.

Disclaimer: This encryption system can be used only on small shapefiles due to the amount of memory used. A typical use would be to encrypt some valuable small shapefiles you don't want your users to access. In the future, Map Suite will provide a full encryption system as an API.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/ShapeFileEncryptionSample/Screenshot.png)

# [Site Selection Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/SiteSelectionSample)

The Site Selection sample template allows you to view, understand, interpret, and visualize spatial data in many ways that reveal relationships, patterns, and trends. In the example illustrated, the user can apply the features of GIS to analyze spatial data to efficiently choose a suitable site for a new retail outlet.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/SiteSelectionSample/ScreenShot.png)

# [Snap To Layer Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/SnapToLayerSample)

This project shows how you can apply snapping to an EditInteractiveOverlay. There are many aspects to snapping. Previously, we showed a project with the mouse pointer snapping to the closest vertex of an editable polygon. Today, we show the technique to drag a control point and have it snap to the closest vertex of a layer if within tolerance. The tolerance can be set in world (meter, feet etc) or screen (pixels) coordinates. Notice that we are also using the technique showed in the previous project “Dragged Point Style”.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/SnapToLayerSample/Screenshot.gif)

# [Snapping To Vertex Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/SnappingToVertex)

This project is the first one of a series that will be dedicated to snapping using InteractiveOverlay. In this project, we show how to snap the mouse pointer to the closest vertex of an editable polygon if it is within a set tolerance. The tolerance is shown as a circle around the vertices. This technique can also be applied to snapping vertices within the same shape, between different shapes or even based on a layer. Those different types of snapping will be the subject of future projects related to snapping using InteractiveOverlay.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/SnappingToVertex/Screenshot.gif)

# [Stream Loading For Native Image Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/StreamLoadingForNativeImage)

As an alternative to loading an Image with the image file from the file system, you can choose to pass your own stream. This project shows you how to use the event StreamLoading of GdiPlusRasterSource for this purpose. In this project, we show how to do this using a Tiff image but you can also use that event for ShapeFileFeatureSource as we show in a previous project “Shapefile Encryption”. Keep in mind that this technique only works with images besides MrSid, ECW and Jpeg2000. These types of images do not work because the providers do not support streams in their decoding SDKs.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/StreamLoadingForNativeImage/ScreenShot.png)

# [Topology Validation Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/TopologyValidation)

Starting with version 6.0.113.0, Map Suite includes topology validation capabilities. This Topology Validator sample application makes it simple to visualize and test topology rules. You simply select the case you want to test, use our drawing tools to create the shapes necessary, and then hit the play button to run the test. The error cases are highlighted on the screen and saved to the test, and their Well-Known Text (WKT) is copied to your clipboard. At any time, you can view or edit the WKT which makes up the case. We save our test cases as XML files that contain not only the test case features but also the errors. This makes it a snap for you to share your cases with us or other colleagues. 

For more details including a list of topology cases that you can test, see [this article](http://gis.thinkgeo.com/Support/DiscussionForums/tabid/143/aff/16/aft/10443/afv/topic/Default.aspx#topology) or watch the [video overview](http://youtu.be/IuLGTRsVtQc)

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/TopologyValidation/Screenshot.png)

# [Track Overlay With Esc Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/TrackOverlayWithEsc)

This project shows how to implement aborting a trackshape with Esc key as you could do it in MapSuite 2.x.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/TrackOverlayWithEsc/ScreenShot.png)

# [Track Zoom In Without Shift Key Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/TrackZoomInWithoutShift)

This project is for DesktopEdition users. In Map Suite 2.x, different modes of the map are offered to the developer to choose what kind of behavior the map has at a mouse action. In Map Suite 3.x, we went away from modes to have a more flexible model using InteracticeOverlay. By default, track zoom in is done by clicking and dragging the mouse on the map while holding the Shift key. This is very convenient but what if you want to offer the users the same experience as with 2.x, where the same mouse action has different behavior such as TrackZoomIn and Pan through the use of modes.

In this project, we show how to build your own InteractiveOverlay to emulate in Map Suite 3.x the mode behavior 2.x has.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/TrackZoomInWithoutShift/Screenshot.gif)

# [Tracked Shapes To WKT Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/TrackedShapesToWKT)

In today’s project, we show how to save a tracked shape to WKT (Well Known Text). You will notice that we make the distinction between the shape being tracked and the finished tracked shape thanks to two different events of TrackOverlay, TrackEnding and TrackEnded.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/TrackedShapesToWKT/Screenshot.png)

# [US Demographic Map Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/UsDemographicMapSample)

The Demographic and Lifestyle sample template gives you a head start on your statistics project, which includes details about race, age, gender, land usage, and more for all the states in U.S. The template contains pre-styled layers that can be used as-is, or as the foundation for adding your own map notes and layers.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/UsDemographicMapSample/ScreenShot.png)

# [Us Earthquake Statistics Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/UsEarthquakeStatistics)

The Earthquake Statistics sample template is a statistical report system for earthquakes that have occurred in the past few years across the United States. It can help you generate infographics and analyze the severely afflicted areas, or used as supporting evidence when recommending measures to minimize the damage in future quakes.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/UsEarthquakeStatistics/ScreenShot.png)

# [Vehicle Tracking Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/VehicleTrackingSample)

The Vehicle Tracking sample template gives you a head start on your next tracking project. With a working code example to draw from, you can spend more of your time implementing the features you care about and less time thinking about how to accomplish the basic functionality of a tracking system.

Because this sample needs Microsoft.Jet.OLEDB provider, please **run in x86 mode**.
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/VehicleTrackingSample/ScreenShot.png)

# [Vertex Tolerance Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/VortexTolerenceSample)

Basically, this project shows the opposite of yesterday’s project “Snap To Layer”. Instead of having the dragged control point snapping to a vertex if within a tolerance, we show how to not allow a control point get within a set tolerance. This technique can be handy to implement if you have a requirement to have vertices of a shape being no less than a certain distance between each others. To implement that technique, again we use the power and flexibility of the EditInteractiveOverlay of the Desktop edition.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/VortexTolerenceSample/Screenshot.png)

# [World Streets Layer SDK On Linux for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/WorldStreetLayersSDKLinux)

The World Streets Vector Layer Explorer is a tool that enables you to view the SQLite World Streets data using Map Suite WinForms and provides complete performance metrics.

This project requires a full or evaluation version of Map Suite WinForms Edition.

This sample passed on Linux with Mono Runtime. On Windows platform, it is required to replace the ThinkGeo.MapSuite.Layers.SqliteForLinux with ThinkGeo.MapSuite.Layers.Sqlite package.

**To run the sample, please unzip the database file at WorldStreetsLayerSample/App_Data/DallasCounty-3857-20170218.zip, and change the connection string in WorldStreetsLayerSample/App.config to connect database that you extracted to.**

Working...

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/WorldStreetLayersSDKLinux/screenshot.png)

# [World Streets Layer SDK On Windows for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/WorldStreetsLayerSDKWindows)

The World Streets Vector Layer Explorer is a tool that enables you to view the SQLite World Streets data using Map Suite WinForms and provides complete performance metrics.

This project requires a full or evaluation version of Map Suite WinForms Edition.

This sample passed on Linux with Mono Runtime. On Windows platform, it is required to replace the ThinkGeo.MapSuite.Layers.SqliteForLinux with ThinkGeo.MapSuite.Layers.Sqlite package.

**To run the sample, please unzip the database file at WorldStreetsLayerSample/App_Data/DallasCounty-3857-20170218.zip, and change the connection string in WorldStreetsLayerSample/App.config to connect database that you extracted to.**

Working...

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/WorldStreetsLayerSDKWindows/screenshot.png)

# [Use Wpf Map Control Sample for WinForms](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/winforms/WpfMapControlSample)

The wpf map control supports multi-thread to render map, so it performs better than winforms map control. This sample demonstrates how you can use wpf map control in your winforms applications.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/WpfMapControlSample/Screenshot.gif)

# [Adjacent Road Search Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/AdjacentRoadSearchSample)

This WPF project shows how to get a route between two adjacent roads, even if they don't intersect within an allowable tolerance.

It’s based on Map Suite Geometry Topology module and does not require the Map Suite Routing Extension.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/AdjacentRoadSearchSample/Screenshot.gif)

# [Annotation Style Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/AnnotatingStyleSample)

In this project you will see how to use the AnnotationStyle to display and edit a feature depending on the value of a specific property in its data source.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/AnnotatingStyleSample/Screenshot.gif)

# [Apply DirectionPointStyle for LineStyle for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ApplyDirectionalPointStyle)

The Map Suite WPF ApplyDirectionPointStyleForLineStyle sample will guide you to draw lineStyle's direction Point on map. The direction Point can be an image or a glyph, it not only rotates the icon accross the angle of the road, but also provides a way to customize the rotation of the direction point. The arrows highlighted in the red circle in the following screenshot are customized based on the line's attributes. Please check out the source for detail. This sample supports Map Suite 10.5.8 and higher. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ApplyDirectionalPointStyle/Screenshot.gif)

This sample shows you how to use these four different types of maps as background in Map Suite Desktop (WPF) Edition.
 - **ThinkGeo Cloud Maps**
 - **Open Street Map**
 - **Bing Maps**
 - **Google Maps**

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/BackgroundMapSwitchingSample/Screenshot.gif)

# [Building 3D Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/Building3DLayerSample)

This project shows to create simulated 3D buildings on WPF map control and OsmBuildingOnlineServiceFeatureLayer.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/Building3DLayerSample/Screenshot.png)

# [BuildingSamples-ForWpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/BuildingSample)

In this WPF project, we show how to use `BuildingOverlay`, `BuildingStyle` and `OsmBuildingOnlineServiceFeatureLayer`.

This repo contains three projects.
- BuildingOverlay represents a WPF specific overlay that simulates the 2.5D building rendering.
- BuildingStyle represents a style for building that is compatible with the other products such as WPF, WinForms and Web.
- OsmBuildingOnlineServiceFeatureLayer represents the online data source of feature layer.

![Build Preview](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/BuildingSample/Screenshot.gif)

# [Centering And Rotating Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CenteringAndRotatingSample)

This project is the Wpf version of the services edition sample “Centering and Rotating On Moving Feature”, where we learn how to have the map always centered and rotated based on the location and direction of a moving vehicle. This issues addresses some issues you have to be aware of regarding both ShapeFileFeatureLayer and InMemoryFeatureLayer when applying rotation to the map.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CenteringAndRotatingSample/Screenshot.gif)

# [Class Break Style Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ClassBreakeStyleSample)

In this project you will see how to use the ClassBreakStyle to group and render features by values. ClassBreakStyle is a useful technique as it allows you to group various features by the specified values, then applies differently style to the feature groups. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ClassBreakeStyleSample/Screenshot.png)

# [Clipping On Line Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ClippingOnLineLayerSample)

This Wpf project completes the series of projects dedicated to the clipping geoprocessing. We already saw how to perform clipping on a polygon based layer in “Clipping” and on a point layer in “Clipping On Point Layer”. Here we show how to perform the clipping geopressing on a line based layer. As for the same operation on a polygon based layer, the key geometric function is GetIntersection. We will also appreciate the operation of creating a layer from scratch as in addition to the geometric operation itself, geoprocessing also involves creating a result layer from the original layers, the clipping layer and the clipped layer.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ClippingOnLineLayerSample/Screenshot.gif)

# [ThinkGeo Cloud Color Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CloudColorSample)

This sample demonstrates how you can use ThinkGeo Cloud Client to get colors from ThinkGeo GIS Server. It supports 7 different colors:
- Hue
- Quality
- Analogous
- Complementary
- Contrasting
- Tetradic
- Triadic

ThinkGeo Cloud Client support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CloudColorSample/Screenshot.gif)

# [ThinkGeo Cloud Elevation Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CloudElevationSample)

This **Sample**  demonstrates how you can use ThinkGeo Cloud Client to get elevation data from ThinkGeo GIS Server, and shows the elevation data of a road in the form of a line chart.

There are two ways to get the elevation data of the line. First, get the points on the line by setting the interval distance. The other, is to take the points by setting the number of points to be fetched. Then query the elevation data of the point.

ThinkGeo Cloud Client support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CloudElevationSample/Screenshot.gif)

# [ThinkGeo Cloud Geocoding Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CloudGeocodingSample)

This **Sample**  demonstrates how you can use ThinkGeo Cloud Client to get a geographic location from ThinkGeo GIS Server by a street address.

ThinkGeo Cloud Client support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CloudGeocodingSample/Screenshot.gif)

# [ThinkGeo Cloud Maps Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CloudMapsSample)

This sample demonstrates how you can display ThinkGeo Cloud Maps in your Map Suite GIS applications. It will show you how to use the XYZFileBitmapTileCache to improve the performance of map rendering. ThinkGeoCloudMapsOverlay uses the ThinkGeo Cloud XYZ Tile Server as raster map tile server. It supports 5 different map styles:
- Light
- Dark
- Aerial
- Hybrid
- TransparentBackground

ThinkGeo Cloud Maps support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CloudMapsSample/Screenshot.gif)

# [ThinkGeo Cloud Reverse Geocoding Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CloudReverseGeocoding)

This **Sample**  demonstrates how you can use ThinkGeo Cloud Client to get meaningful addresses from ThinkGeo GIS Server by a geographic location. It ships with an optimized set of worldwide coverage of cities and towns, but any customized data can be supported as well.

ThinkGeo Cloud Client support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CloudReverseGeocoding/Screenshot.gif)

# [ThinkGeo Cloud Vector Maps Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CloudVectorMapsSample)

This sample demonstrates how you can draw the map with Vector Tiles requested from ThinkGeo Cloud Services in your Map Suite GIS applications, with any style you want from [StyleJSON (Mapping Definition Grammar)](https://wiki.thinkgeo.com/wiki/thinkgeo_stylejson). It will show you how to use the XyzFileBitmapTileCache and XyzFileVectorTileCache to improve the performance of map rendering. It supports have 3 built-in default map styles and more awasome styles from StyleJSON file you passed in, by 'Custom':
- Light
- Dark
- TransparentBackground
- Custom

ThinkGeo Cloud Vector Maps support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CloudVectorMapsSample/Screenshot.gif)

# [Cluster Feature Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ClusterFeatureLayerSample)

The sample shows how to use ClusterFeatureLayer to render the specified column data as a pie chart. Allows users to compare data visually.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ClusterFeatureLayerSample/Screenshot.gif)

# [Color Replacement Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ColorReplacementSample)

In today’s WPF project, we show you how to replace a specific color in a raster image, with the advantage of new added API Color Mapping. For the example, in this project, the lake in Green can be replaced with blue.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ColorReplacementSample/Screenshot.gif)

# [Combine Overlay Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CombineOverlaySample)

In today’s Wpf project, we show a technique of using a common FeatureSource for two different Overlays. From a physical shapefile representing cities, a regular LayerOverlay is used for the higher zoom levels while a FeatureSourceMarkerOverlay is used for the lower zoom levels. FeatureSourceMarkerOverlay is a Wpf specific overlay offering features for a better user experience such a Tooltips and ImageSource.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CombineOverlaySample/Screenshot.gif)

# [Connecting Two Shapes With Arraw Line Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ConnectingTwoShapesWithArrawLineSample)

In this sample, two polygons are connected by a arrowline, and if the polygons are moved the connecting arrowline will be moved automatically as well.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ConnectingTwoShapesWithArrawLineSample/Screenshot.gif)

# [Custom Parameters Projection Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CustomParametersProjectionSample)

In today’s Wpf project, we learn more about projection and how to handle a special case where the projection information in the PRJ file of a shapefile is not found as an EPSG or ESRI code in the spatial reference web site (www.spatial-reference.org). The strategy in this case is to build a proj4 string based on the parameters found in the PRJ file. Note that you must pay special attention to the false easting or northing parameters as they are always expressed in meters even if the projection is in another unit such as feet. For this example, we show how to convert from the local projection in Lambert Conformal Conic with regional specific parameters to Geodetic (WGS84) to match the World Map Kit.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CustomParametersProjectionSample/Screenshot.gif)

# [Custom Rotation Projection Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CustomRotationProjectionSample)

In today’s project, we show how to create your own projection class that allows projecting a layer from any internal projection to any external while doing a rotation at the same time. Having the capability to apply those two operations in one step may come handy if the original layer was created in a different projection with the north at an angle and you want to align it on the base map.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CustomRotationProjectionSample/Screenshot.gif)

# [CustomZoomLevel for ThinkGeoCloudRasterOverlay for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/CustomZoomLevelForThinkGeoCloudRasterOverlay)

The Map Suite WPF CustomZoomLevelForThinkGeoCloudRasterOverlay sample will guide you to how to draw map with custom zoomlevels. This CustomZoomLevelForThinkGeoCloudRasterOverlay sample supports Map Suite 10.0.0.0 and higher and will show you how to create a WPF application using Map Suite WPF components.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CustomZoomLevelForThinkGeoCloudRasterOverlay/Screenshot.gif)

# [Default Value Style Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DefaultValueStyleSample)

In today’s Wpf project, we demonstrate the extensibility of ThinkGeo API by creating a custom Style. Inheriting from **ValueStyle**, we create a Default Value Style that handles the drawing of features that don’t have a value defined in the **ValueStyle**. Here we are using a point based layer with **PointStyle**. Note that the Default **ValueStyle** would also work with line and polygon based layers.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DefaultValueStyleSample/Screenshot.gif)

# [Delay Drawing Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DelayDrawingSample)

This WPF project shows how to use the Delay Map Drawing feature to control whether or not the layer is redrawn after a specified delay. This option is very helpful for anyone wanting to do something before actually refreshing the map - such as editing the elements, adding an animation, etc.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DelayDrawingSample/Screenshot.gif)

# [Display File GeoDatabase Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DisplayFileGeoDatabase)

This sample demonstrates how you can read data from an ESRI FileGeodatabase, and you will find the code as straightforward as consuming any other data source in Map Suite.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DisplayFileGeoDatabase/Screenshot.gif)

# [Display Iso Lines Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DisplayIsoLinesSample)

In this sample we show how you can use Map Suite to add isolines (commonly known as contour lines) to your .NET application. Isolines are a way to visualize breaks between different groups of data such as elevation levels, soil properties, or just about anything else you can imagine. This sample also shows the various steps in creating isolines, including the gathering of point data, creating a grid using interpolation, and finally, picking your isoline break levels. We also quickly dive into some more advanced options such as generating isolines on the fly.

To bring this all together, check out our [instructional video](https://www.youtube.com/watch?v=eejtCTftpzo) that will walk you through the process of setting up and working with isolines in Map Suite.

Please note that you will need version 5.0.87.0 or newer of Map Suite in order to use isolines. For more information on how to upgrade, see the [Map Suite Daily Builds Guide](http://wiki.thinkgeo.com/wiki/map_suite_daily_builds_guide).

From 6.0.187.0, the sample has been updated that polygons can also be returned as IsoLines results. You need version 6.0.187.0 or newer of Map Suite in order to use this sample.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DisplayIsoLinesSample/Screenshot.gif)

# [Display MsSql GeoDatabase Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DisplayMsSQLDatabaseSample)

This sample demonstrates how you can read data from an MsSql database, and you will find the code as straightforward as consuming any other data source in Map Suite.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DisplayMsSQLDatabaseSample/Screenshot.png)

# [Display MsSql GeoDatabase Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DisplayMsSQLDatabaseSample)

This sample demonstrates how you can read data from an MsSql database, and you will find the code as straightforward as consuming any other data source in Map Suite.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DisplayMsSQLDatabaseSample/Screenshot.png)

# [Display Oracle Data Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DisplayOracleDataSample)

Discover how to use OracleFeatureLayer to build up your map. Use oracle data to render map.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DisplayOracleDataSample/Screenshot.png)

# [Distance Query On Wrap Dateline Mode Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DistanceQuerryOnWrapDateLineModeSample)

This WPF sample shows how to take advantage of the projection class **WrapDatelineProjection** for doing spatial queries when on a “virtual map”. From this spatial query example, the developer should understand how the same principle using the same projection class can be applied for other operations such as Spatial Queries, Identify, etc.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DistanceQuerryOnWrapDateLineModeSample/Screenshot.gif)

# [Draggable Labels Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DraggableLabelSample)

In this WPF project, you will learn how to place labels from a shapefile into a **SimpleMarkerOverlay**. Using the **DragMode** property of the **SimpleMarkerOverlay**, the user can then drag the labels to place them at the desired location for the most pleasing labeling effect. This project will be later completed to show how to save the state of the dragged labels from the **SimpleMarkerOverlay** and reload them.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DraggableLabelSample/Screenshot.gif)

# [Dynamic Marker Overlay Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/DynamicMarkerOverlaySample)

In today’s WPF project, we show you how to retrieve data from a REST service and display them as markers with different styles based on its attributes. You can click on any marker to call a WCF service which returns data from the server. A popup displays a chart with information. All the markers and related information are updated dynamically after a specific time interval.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DynamicMarkerOverlaySample/Screenshot.gif)

# [Earthquake Statistics Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/EarthquakeStatisticSample)

The Earthquake Statistics sample template is a statistical report system for earthquakes that have occurred in the past few years across the United States. It can help you generate infographics and analyze the severely afflicted areas, or used as supporting evidence when recommending measures to minimize the damage in future quakes.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/EarthquakeStatisticSample/Screenshot.gif)

# [Edit Attribute Of Shapefile Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/EditAttributeOfShapeFile)

The purpose of this Wpf sample is to show how to edit the attributes of a feature of a shapefile. This sample is useful for anyone wanting to actualize the attributes part of its data by simply clicking on the desired feature on the map and updating its attributes in a textbox. You will find the editing part of the code in the **KeyDown** event of the textbox.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/EditAttributeOfShapeFile/Screenshot.gif)

# [Elevation Grade Of Line Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ElevationGradeOfLineSample)

In this sample, we show how you can use Map Suite [Elevation SDK](https://thinkgeo.com/gisserver#feature) to get the elevation values of a specific line for your .NET application. It allows you to customize your query to get data as detailed as you need it to be. Draw elevation profiles for your  hiking or biking trip, contral the granularity of the response, sample elevation values at controllable intervals along a route should be shown up.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ElevationGradeOfLineSample/Screenshot.png)

# [Elevation Statistics Sample for WPF](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ElevationStatisticsSample)

This **Sample**   shows the elevation data of a road in the form of a line [chart][1].

- **Elevation SDK** - support query elevation data by points, line and polygon based on [SRTM][2] and Ned13 elevation source data.
 - **For point** - Create a buffer and aggregate all points within buffer to create average for the elevation of the point. 
 - **For line** - There are two ways to get the elevation data of the line. First, get the points on the line by setting the interval distance. The other, is to take the points by setting the number of points to be fetched. Then query the elevation data of the point.
 - **For polygon** - By setting the interval distance, clip the polygon to the grids and get all the center of the grids where the polygon is located. Now, determine whether the center points are within the surface or inside the surface (use improved arc-length method).
 
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ElevationStatisticsSample/Screenshot.png)

# [Filter Style Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/FilterStyleSample)

This project outlines how to apply the FilterStyle to a layer's display. By using this style, the map will filter the features queried from the source file by checking if a specified column value fits the input condition. This effect can be applied to all the Map Suite products.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/FilterStyleSample/Screenshot.gif)

# [Find Nearest Cross Streets Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/FindNearestCrossSreetsSample)

This sample demonstrates how to geocode an address using Map Suite Geocoder. It then returns and highlights the low and high cross streets.          

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/FindNearestCrossSreetsSample/ScreenShot.png)

# [Find Shortest Line And Splitting Lines Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/FindShortestLineAndSplittingLinesSample)

This sample will show you how to find the closest line between two features by using the GetShortestLineTo API and how to split a line at a given point using the GetLineOnALine API. These APIs can be very useful when doing spatial analysis and editing of features. This sample also allows you to dynamically alter the test features using the EditOverlay so you can try out different scenarios and see the results quickly. A MapShapes layer is used to display and style the individual results.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/FindShortestLineAndSplittingLinesSample/Screenshot.gif)

# [Four Color Map Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/FourColorMapSample)

Any map can be colored using four colors in such a way that adjacent regions receive different colors.
The sample discover how to use ShapeFileFeatureLayer to get four color features, then use ValueStyle to render the four color map.

At present, the four color map only supports polygon, and doesn't support point and line.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/FourColorMapSample/Screenshot.gif)

# [Friends Network Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/FriendsNetworkSample)

In this WPF project we show how you can create a friends network using a point with a circle symbol. It’s a combination of PointStyle and TextStyle, including a description with a mask that keeps the labels in the same layer. It was originally required by a customer at [http://community.thinkgeo.com/t/label-on-a-circle-with-lot-of-points/8193/6](http://community.thinkgeo.com/t/label-on-a-circle-with-lot-of-points/8193/6), it’s a solution with many applications.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/FriendsNetworkSample/Screenshot.gif)

# [GPS Exchange Format Feature Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/GPSExchangeFormatFeautureLayer)

This sample demonstrates how to read GPS EXchange Format file(*.gpx) with Map Suite. GPX (GPS Exchange Format) is a light-weight XML data format for the interchange of GPS data (waypoints, routes, and tracks) between applications and Web services on the Internet, which you can find more information:here. Now Map Suite supports the GPX 1.0 and 1.1 schema. This sample works with Map Suite development branch daily build 7.0.275.0 or later.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/GPSExchangeFormatFeautureLayer/Screenshot.png)

# [Get Feature Clicked On With Projection Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/GetFeatureClickedOnWithProjectionSample)

This WPF project shows a technique for finding the feature of a point, line or polygon-based layer that the user clicked on.  As with the earlier sample entitled "Get Feature Clicked On", in order to give the user the expected behavior, a buffer in screen coordinates needs to be set so that the selected feature is chosen within a constant distance in screen coordinates from where the user clicked on, regardless of the zoom level.  In addition, this sample also addresses a limitation of the earlier sample by showing the contrast between using the functions GetFeaturesNearestTo and GetFeaturesWithinDistanceOf.

While the function GetFeaturesNearestTo as used in "Get Feature Clicked On" works quickly and is adequate in most cases, it is not guaranteed to get the closest feature.  In contrast, with the new function GetFeaturesWithinDistanceOf, you can loop through the features within a specified tolerance and get their exact distance.  While this approach requires some more code, it is guaranteed to get the closest feature in any case.

This sample also demonstrates another aspect of distance queries. We have the layers projected to the Google Map projection from WGS84, and you'll notice that the aforementioned distance functions (GetFeaturesWithinDistanceOf and GetFeaturesWithinDistanceOf) work seamlessly without requiring the developer to worry whether the layers are projected or not.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/GetFeatureClickedOnWithProjectionSample/Screenshot.gif)

# [Get Features From Arc GIS Server Sample for WPF](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/GetFeatureFromArcGISServer)

In this wpf-based project, it illustrates how to get the features from the ArcGIS Restful Server. On the left side of the screenshot shows the raster data from ArcGIS Server and on the other side shows the features from ArcGIS Restful Server. In order to run this project, you will need the Development Build 9.0.482.0 or later.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/GetFeatureFromArcGISServer/Screenshot.png)

# [Google Map To Geodetic Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/GoogleMapToGeodeticSample)

In this Wpf project, we are showing a trick for getting the current extent of the map with Google Map as an image and displaying it on a map in decimal degrees unit. You may be in a situation where you want the details and accuracy of Google Map, especially the satellite view but want to have it displayed on your map in decimal degrees. In this sample, you will see how to get the Google Map image and create the accompanying world file for decimal degrees. Notice that we are realizing an affine transformation on the image to go to Geodetic as illustrated in the case 3

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/GoogleMapToGeodeticSample/Screenshot.gif)

# [Graticule With Google Projection Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/GraticuleWithGoogleProjectionSample)

In this Wpf project, we explore more features of the **GraticuleAdornmentLayer**, which shows meridians and parallels at various intervals based on the zoom level. Being natively in Geodetic, **GraticuleAdornmentLayer** can be set to any projection. In this project’s example, you have the graticule showing with World Map Kit in Spherical Mercator (Google Map projection). Also, note how easily you can change the appearance with properties such as **GraticuleLineStyle** and **GraticuleTextFont**.
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/GraticuleWithGoogleProjectionSample/ScreenShot.png)

# [Hello World Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/HelloWorldSample)

This sample shows you how to get started building your first application with the Map Suite Desktop for Wpf 10.0.0.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/HelloWorldSample/Screenshot.png)

# [Highlight At Mouse Hover Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/HighlightAtMouseHoverSample)

In this WPF project, we show you how to highlight a feature when the user hovers the mouse pointer over it. This effect can be realized in Map Suite by using a timer and doing some spatial querying based on the location of the mouse pointer. This sample works with area-based features but the same could be accomplished with point- or line-based features simply by using different spatial queries.
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/HighlightAtMouseHoverSample/ScreenShot.png)

# [How Do I Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/HowDoI)

The “How Do I?” samples collection is a comprehensive set containing dozens of interactive samples. Available in C#, these samples are designed to hit all the highlights of Map Suite, from simply adding a layer to a map to performing spatial queries and applying a thematic style. Consider this collection your “encyclopedia” of all the Map Suite basics and a great starting place for new users.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/HowDoI/Screenshot.gif)

# [Image Style Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ImageStyleSample)

As you probably already know, using the Map Suite API, you can easily display a point-based feature as an image. But how do you do the same thing for a line or a polygon-based feature? In this WPF project, we show you how to create custom Image Styles for both line and polygon features. With the new ImageAreaStyle, you can display a polygon feature that uses an image as its fill. You can see how an image for forest and water is used in the sample project. And with the new ImageLineStyle, you can do the same thing with line features. You'll see how an image of a pavement texture is used to represent streets.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ImageStyleSample/Screenshot.png)

# [Labeling Flight Lines Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/LabelingFlightLinesSample)

This sample shows how to offset arc shaped flight lines so multiple from point to point are visible and then show popup labels providing information on the flights the lines represent.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/LabelingFlightLinesSample/Screenshot.gif)

# [Large Scale Map Printing Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/LargeScaleMapPrintingSample)

This WPF project is the second in our series of samples on printing. The

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/LargeScaleMapPrintingSample/ScreenShot.png)

# [Local Datum UTM Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/LocalDatumUTMSample)

The purpose of this WPF sample is to get familiar with the concept of datum and datum shift in UTM and Geographic coordinates. Using local data, you might be required to apply datum transformation to your data to go from an old datum to a more recent datum. Here we are taking the example of Australia, which went to change its datum (from AGD84 to GDA94) for its mapping purposes. We show how to apply the datum transformation in both the longitude/latitude coordinate system and in the local UTM systems (AMG based on AGD84 and MGA based on GD94). 

See the code and note the comments in the **MouseMove** event where all the projection logic is taken place. Notice that the correction is about 200 meters. Once you understand for the case for Australia, you will be able to apply the same principles for your own datums in UTM. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/LocalDatumUTMSample/ScreenShot.png)

# [MBTiles Extractor Sample for WPF](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MBTilesExtractorSample)

The MBTiles Extractor allows you to create new smaller subsets from the MBTiles database. You simply specify the bounding box by tracking a rectangle shape on the map for the new area, then it will create a new SQLite database for that regions.

*.MBTile format can be supported in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MBTilesExtractorSample/Screenshot.gif)

# [ThinkGeo MBTiles Maps Sample for WPF](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MBTilesMapSample)

This sample demonstrates how you can draw the map with Vector Tiles saved in *.MBTiles in your Map Suite GIS applications, with any style you want from [StyleJSON (Mapping Defination Grammar)](https://wiki.thinkgeo.com/wiki/thinkgeo_stylejson). It will show you how to use the XyzFileBitmapTileCache to improve the performance of map rendering. It supports have 3 built-in default map styles and more awasome styles from StyleJSON file you passed in, by 'Custom':
- Light
- Dark
- TransparentBackground
- Custom

If you want the *.mbtile file of any area in the world, or you have any requirement of building *.mbtile file based on your own data, such as shape file, Oracle, MsSql and more, please contact support@thinkgeo.com.

*.MBTile format can be supported in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MBTilesMapSample/Screenshot.gif)

# [Magnetic Declination Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MagneticDeclinationSample)

In today's WPF project, we show you how to add the Magnetic Declination or Magnetic variation to the map, it's designed as an **AdormentLayer**, which is used for showing the angle on the horizontal plane between magnetic north (the direction in which the north end of a compass needle, corresponding to the direction of the Earth's magnetic field lines) and true north (the direction along a meridian towards the geographic North Pole). This angle varies depending on one's position on the Earth's surface, and over time. See
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MagneticDeclinationSample/ScreenShot.png)

# [Map Loading Progress Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MapLoadingProgressSample)

Discover how to use progress bar to show the LayerOverlay rendering progress. The project is simple and meant to show only the basic logic; to extend it, you could add some other information or a loading image instead of a progress bar.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MapLoadingProgressSample/Screenshot.png)

# [Map Touch Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MapTouchSample)

Map Suite WPF Edition supports touch events. This sample shows how to add a marker to map by MapTap event.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MapTouchSample/Screenshot.png)

# [Mini Map Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MiniMapSample)

This project shows how to create a simple mini map to give a reference of where you are when you zoomed in. As for the MiniMapAdormentLayer inherits from AdornmentLayer.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MiniMapSample/Screenshot.png)

# [Multi Line Labeling Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MultiLineLabelingStyle)

For labeling purpose, **TextStyle** has a property called LabelAllPolygonParts that will label all the parts making up a polygon based feature. Unfortunately, we don’t have an equivalent API for labeling all the parts of a line based feature. But thanks to the flexible framework of Map Suite, we show in this WPF sample how easily you can expand the **TextStyle** class to allow this labeling capability. Look at the custom class **MultiLinetextStyle** and how **DrawCore** function is overridden to have the expected labeling behavior.
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MultiLineLabelingStyle/ScreenShot.png)

# [Multiple Dot Density Styles Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MultipleDotDensityStyleSample)

This sample takes a detailed look at the **DotDensityStyle**. You can combine different **DotDensityStyles** on one FeatureLayer using different columns. Demographic data are usually well fit to be used with that type of style. Here each dot of a certain color represents 100,000 persons of the same age group by state. Using multiple **DotDensityStyles**, the dimension of age group demographics is added to the representation of population density. 

Note that the **PointToValueRatio** is used to set how many people a dot is going to represent. For example, if you want a dot to represent 100,000 persons, you set it to 0.00001 (1 / 100000). If you want it to be 500,000 persons, you set it to 0.000002 (1 / 500000).
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MultipleDotDensityStyleSample/ScreenShot.png)

# [Multiple Jpeg2000 Raster Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/MultipleJpeg2000RasterLayerSample)

This sample shows how to create a customized raster layer for loading multiple files. In this project, we create the MultipleJpeg2000RasterLayer to inherit from Layer to load multiple .jp2 files. You can use the world files or a RectangleShape which is the extent of the image in world coordinate.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MultipleJpeg2000RasterLayerSample/Screenshot.png)

# [NOAA Globel Weather Station Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/NOAAGlobalWeatherStationLayer)

This WPF project, demonstrates how to query and display real time NOAA weather station data directly as a **Layer**, which allows you to display up-to-date weather station data from around the world on top of your maps. The weather station data is sourced from NOAA and is refreshed every 15 minutes in the background. As these classes are inherited from the **FeatureSource** and **FeatureLayer**, many properties are querable and can be used to create a variety of maps or analysis of current weather patterns. This feature can be applied in all Map Suite products.             

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/NOAAGlobalWeatherStationLayer/ScreenShot.png)

# [Nautical Charts Viewer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/NauticalCharts)

ThinkGeo Nautical Charts is a standalone Nuget Package which works with ThinkGeo 10.X Desktop/Web products. It reads and displays S-57 Electronic Navigational Charts(ENC) from International Hydrographic Organization(IHO), it also reads the style info defined in an S-52 file.

This sample happens to be written for WPF but the Nautical Charts Package works for Web project as well. Download and open it in Visual Studio, hit F5 and Bang, you are good to go. After you have the charts viewer running, use File -> Open to load an S-57 data, (it can be downloaded from NOAA’s website), the nautical charts will then appear on the map (it generates an index file the first time the data is loaded).

ThinkGeo uses a default style which can be easily modified. Nautical Charts Viewer provide the ability to switch the map between 5 modes: Day Bright, Day Black, Day White, Dusk and Night. It has 3 verbose mode of “All”, “Standard” and “Base”. It can switch boundaries between dashed line and triangles, it can show/hide different labeling, show different languages, etc. If you are a developer, dig in the code and you can see it’s as simple as creating a layer and set up the properties like following, and it can do more than it shows in this sample

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/NauticalCharts/Screenshot.gif)

 # [Overlays Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/OverlaysSample)

Discover how to use Overlays to build up your map, or to add existing basemaps to your application. 
The sample can show the following four basemaps:
  1. Google Maps
  2. Bing Maps
  3. ThinkGeo Cloud Maps
  4. Open street Maps

It can display different styles of maps by setting the map type. Note: you do need to have Bing Maps API key and Google Maps API key to be able to use these two basemaps. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/OverlaysSample/Screenshot.png)

# [PerformanceSample-ForWpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/PerformanceSample)

This is a WPF desktop sample for drawing performance test of MapSuite product.
When running the sample, it will render 16,000 count of rectangle shape features at first, these features will be distributed in 4 layers averagely. After clicking Start button the sample application will update 1,600 count of rectangle shape features per 1,000 milliseconds, the time cost of features drawing will display in the application footer.
Customer can modify the update rate, update features count, and enable or disable the layers. 

### Key Points
- The rectangle shape is added to an InMemoryFeatureLayer, the layer uses a ValueStyle to draw the shape. The ValueStyle has 4 count of ValueItems which the item Id is from 0 to 3 and the item AreaStyle uses 4 different of fill colors. The shape also has a column value of 0 at the first time, the ValueStyle will use this value to draw the shape using corresponding AreaStyle.

- In each update, the application will choose 1,600 count of features from all valid features randomly to modify their column values. The column value will be modified to 0, 1, 2 or 3 circularly.

- The CustomLayerOverlay will be used in the update, the application will create a new InMemoryFeatureLayer and add it to this overlay when updating, all determined 1,600 count of features will be added to the layer. The previous overlay will be removed if it exists in the map, then a new overlay will be added to the map. Before removing the previous overlay, the all resources (include all layer tiles and background images) from the overlay will be drawn on a GeoImage, then set the GeoImage to the background image of the new overlay. We do this to make sure only 1,600 count of features would be rendered in each update, and the previous updated features would be displayed correctly in the map.

- Customer can check or uncheck the layers in the right list of the application form, the application will only choose the features which in the checked layers when updating, and the unchecked layers will be hidden in the map.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PerformanceSample/Screenshot.png)

# [Place search world reverse geocoding sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/PlaceSearchWordReverseGeocoding)

In this sample, we show how you can use Map Suite [World Reverse Geocoding SDK](https://thinkgeo.com/gisserver#feature) to turn a geographic location into meaningful addresses. It ships with an optimized set of worldwide coverage of cities and towns, but any customized data can be supported as well.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PlaceSearchWordReverseGeocoding/Screenshot.gif)

# [Polygon Shapes To Multipolygon Shape Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/PolygonShapesToMultipolygonShape)

In this Wpf project, we show how to create a MultipolygonShape from various PolygonShapes. Since a collection of PolygonShapes cannot be directly cast to a MultipolygonShape, we show the technique to build a MultipolygonShape passing an IEnumerable of PolygonShape. This is a Wpf sample and you will need the references for MapSuiteCore.dll and WpfDesktopEdition.dll.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PolygonShapesToMultipolygonShape/Screenshot.gif)

# [Popup Overlay Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/PopUpOverlaySample)

In this Wpf project, we explore more capabilities of the PopupOverlay and its collection of Popups. With the Content property, we can add a TextBox or any other control to a Popup. For example, here we show how to have the column value of a selected feature appear in a Popup on the user MapClick event. Notice how that column value can be easily changed by typing in the TextBox of the Popup and committing the edit.
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PopUpOverlaySample/ScreenShot.png)

# [Print Map With Multi Pages Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/PrintMapWithMultiplePages)

In this sample we show you how to add robust printing support to your Map Suite applications for the WPF. Using the code in this sample, you'll be able to build a Print Preview interface that lets your users interactively arrange items on a virtual page before printing the result to a printer, exporting to a PDF or to a bitmap image. The printing system also print maps with multi pages or print multi maps to one page.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PrintMapWithMultiplePages/Screenshot.gif)

# [Print Popup And Marker Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/PrintPopUpAndMarkerSample)

Popup and marker need to use click events etc, so they inherit Control class. SimpleMarkerOverlay and PopupOverlay can't be printed directly, and They need to be converted to image. This sample demonstrates how to print popup and marker by PrinterGeoCanvas. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PrintPopUpAndMarkerSample/Screenshot.png)

# [Print Preview Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/PrintPreviewSample)

In this sample we show you how to add robust printing support to your Map Suite applications for the desktop, WPF, web or services environments. Using the code in this sample, you'll be able to build a Print Preview interface that lets your users interactively arrange items (such as a map, scale line, labels, data grid or image) on a virtual page before printing the result to a printer, exporting to a PDF or to a bitmap image. Maps are printed using vector graphics so you can be sure the output will look great on anything from a PDF to a large plotter. The printing system also includes low-level report building classes that make it easy to generate reports in the web or services environment. 

To help you understand the sample, as well as Map Suite's new printing system upon which it is based, check out our [instructional video](http://download.thinkgeo.com/Videos/Wiki/MapSuitePrintingSystemIntroduction.wmv) that will introduce you to all of these concepts and walk you through the sample solution. 

Please note that you will need version 5.0.102.0 or newer of Map Suite in order to use the new printing features. For more information on how to upgrade, see the [Map Suite Daily Builds Guide](http://wiki.thinkgeo.com/wiki/map_suite_daily_builds_guide). 

Note: Users of Map Suite Web, Silverlight and Services Editions will not have access to the interactive drag-and-drop page layout interface pictured here. However, these editions can still be used to programmatically design page layouts in code and then export them to a printer.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PrintPreviewSample/Screenshot.png)

# [Quickstart Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/QuickStart)

The Map Suite WPF QuickStart Guide will guide you through the process of creating a sample application and will help you become familiar with Map Suite. This QuickStart Guide supports Map Suite 10.0.0.0 and higher and will show you how to create a WPF application using Map Suite WPF components.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/QuickStart/Screenshot.png)

# [Raster Layer Print Quality Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/RasterLayerPrintQuality)

This WPF project is the 3rd sample on the printing series. It demonstrates how to print your maps in high quality. This new feature for Map Suite is available in version 9.0.483.0 or later. From the sample you will better understand how to use DPI for handling print quality. The sample is based on raster images, but could also be used with vector data.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/RasterLayerPrintQuality/Screenshot.png)

# [Reproject Wmts Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ReprojectWmtsSample)

In today’s project, we show how to create your own projection class that allows projecting a WMTS layer from any internal projection to any external. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ReprojectWmtsSample/Screenshot.png)


# [Rotate Events Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/RotateEventsSample)

This sample shows how to take the advantage of a touchable screen, to play with the map using fingers. You would see we can pan the map with one finger, zoom in/out or rotate a map using 2 fingers. Not only that, we can add a marker, and track/edit a shape (point, line or polygons) by tapping on the screen. And marker/popup/label won't rotate with the map. It is straightforward to use and checking out the code, you would see it is very simple to implement with Map Suite! It is available in 8.0.48.0 or later. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/RotateEventsSample/Screenshot.png)

# [Routing Data Explorer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/RoutingDataExplorer)

This is a simple routing data viewer sample viewer which demonstrates how to use the RoutingEngine to get the shortest or fastest route in your Map Suite GIS applications.

How to use this tool:

 1. **Load rtg data:**
  Click menu "**File->Load**" browse to a rtg file. You also require a shapefile for the routing source file in the same folder, otherwise it display message "Could not find shp file in the same folder where you seleted".
 2. **Start routing:**
  Use left click for a start point and right click for an end point. If there is no route within **200** meters(you can modify this in source code), the explorer displays a message "There’s no road within 200 meters to where you clicked on".

This RoutingEngine supports routing in all of the Map Suite controls such as WinForms, Web, MVC and WebApi.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/RoutingDataExplorer/Screenshot.gif)

# [SQLite Bitmap Tile Cache Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/SQLiteBitmapTileCache)

This sample shows how you can cache the tile images in SQLite database.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SQLiteBitmapTileCache/Screenshot.gif)

# [Save Load State Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/SaveAndLoadStates)

The purpose of this Wpf project is to show how to use the new SaveState and LoadState of the SimpleMarkerOverlay. We show how you can simply drag the icons to change their location, save their state to a file and then reload the state from that file. For this sample to work, you will need to use the latest

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SaveAndLoadStates/ScreenShot.png)

# [Select And Drag Feature Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/SelectAndDragFeatureSample)

In this Wpf project, we show how to select a feature from a shapefile based on a column value using the GetFeaturesByColumnValue. We also show to setup the EditOverlay to give the user the ability to drag the selected feature. In this project, you can also see how to get the world coordinates at the mouse move event. Notice that the code for doing this is quite different in Wpf compared to the winforms edition.
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SelectAndDragFeatureSample/ScreenShot.png)

# [Show A Legend Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ShowALegendSample)

In today’s project we learn how to display a simple legend using the new and improved LegendAdornmentLayer. The improved LegendAdornmentLayer was added to Map Suite 5.0 and provides an easy to use API for creating legend adornments. The LegendAdornmentLayer is part of Map Suite Core which allows you to access this powerful feature across all Map Suite products. 
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ShowALegendSample/ScreenShot.png)

# [Site Selection Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/SiteSelectionSample)

The Site Selection sample template allows you to view, understand, interpret, and visualize spatial data in many ways that reveal relationships, patterns, and trends. In the example illustrated, the user can apply the features of GIS to analyze spatial data to efficiently choose a suitable site for a new retail outlet.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SiteSelectionSample/Screenshot.gif)

# [Snap To Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/SnapToLayerSample)

The purpose of this Wpf project is to address some limitations of the class SnapToLayerEditInteractiveOverlay found in a previous sample Snap To Layer.  

This class allowed the snapping of the mouse pointer to the closest vertex of a polygon if it is within a set tolerance. While this worked great for simple polygons, there was a performance limitation with complex polygons made of many vertices. This Wpf sample addresses this limitation and allows responsive dragging and snapping of vertex regardless of the size of polygon to snap to.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SnapToLayerSample/ScreenShot.png)

# [Spatial Query With ThinkGeo Cloud Vector Maps Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/SpatialQueryVectorMapsSample)

This sample demonstrates how you can query the features of Vector Tiles requested from ThinkGeo Cloud Services in your Map Suite GIS applications, such as searching the nearest places to your clicked place or the roads in the area with a specific distance you specified.

ThinkGeo Cloud Vector Maps support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SpatialQueryVectorMapsSample/Screenshot.gif)

# [Styles With Inmemory Feature Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/StylesWithinMemoryFeatureLayer)

In this WPF project, we show how to build an InMemoryFeatureLayer from a text file. You'll notice how the columns are set up so that styles can be used as if the InMemoryFeatureLayer were a static layer such as a Shapefile. Here, we apply a Class Breask Style and a Text Style to our InMemoryFeatureLayer. What we learn in this sample can be applied to all the different editions of Map Suite.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/StylesWithinMemoryFeatureLayer/Screenshot.png)

# [Touch Events Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/TouchEventsSample)

This sample shows how to take advantage of touch screen to manipulate that map using one’s fingers. Learn how the map can be panned, zoomed in and out or rotated using two fingers. On addition, markers can be added, and shapes can be added and edited by tapping the screen. 

The sample code makes this straight forward to implement. 
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/TouchEventsSample/ScreenShot.png)

# [Tracked Shapes To File Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/TrackedShapeFile)

In this Wpf sample, we show how to persist the tracked shapes by saving the WKT (Well Known Text) to a file using the TrackEnded event. Also, you can see how to retrieve the WKT from files to create the features for the TrackShapeLayer of the TrackOverlay when loading the map. You will need the MapSuiteCore.dll and WpfDesktopEdition.dll references for this sample.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/TrackedShapeFile/Screenshot.gif)

# [US Demographic Map Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/USDemographicMapSample)

The Demographic and Lifestyle sample template gives you a head start on your statistics project, which includes details about race, age, gender, land usage, and more for all the states in U.S. The template contains pre-styled layers that can be used as-is, or as the foundation for adding your own map notes and layers.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/USDemographicMapSample/Screenshot.gif)

# [Usgs Dem Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/UsgsDemSample)

This sample demonstrates how you can read data from an DEM file in your Map Suite GIS applications, and how to render it with DEM embedded value style as well as a customized style. This DEM File support would work in all of the Map Suite controls such as Wpf, Web, Android and iOS.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/UsgsDemSample/Screenshot.png)

# [Vehicle Tracking Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/VehicleTrackingSample)

The Vehicle Tracking sample template gives you a head start on your next tracking project. With a working code example to draw from, you can spend more of your time implementing the features you care about and less time thinking about how to accomplish the basic functionality of a tracking system.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/VehicleTrackingSample/Screenshot.gif)

# [Weather Line Style Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/WeatherLineStyleSample)

In this WPF sample, we learn how to extend **LineStyle** class to create a style for representing weather fronts, such as cold front, warm front, or occluded front for your weather maps. To achieve that styling a regular **LineStyle** is used for the front line itself. For symbolizing the type of front an icon is used. 

Notice the two handy properties to give you more control: Spacing property to adjust the distance in screen coordinate between each symbol on the line, and Side property to control on what side of the line front the symbols should appear. Of course, as you zoom in and out on the map the spacing between each symbol remain the same as it is set in screen coordinate.              

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WeatherLineStyleSample/ScreenShot.png)

# [Wmts Layer Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/WmtsLayerSample)

This project shows how to consume data from a WMTS Server using WmtsLayer. You would find the code pretty straightforward, just like displaying a shapefile, while behind the scenes we request tiles from the server asynchronously and efficiently, and stitch them into a proper map. 

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WmtsLayerSample/Screenshot.png)

# [Wmts Tiled Overlay Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/WmtsTileOverlaySample)

This project shows how to consume data from a WMTS Server using WmtsOverlay. You would find the code pretty straightforward, while behind the scenes we request tiles from the server asynchronously and efficiently, and stitch them into a proper map.  This class is introduced from version 6.0.187.0, besides this WmtsOverlay, we also have WmtsLayer available.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WmtsTileOverlaySample/Screenshot.png)

# [World Map Kit Data Extractor Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/WorldMapKitDataExtractor)

The World Map Kit Data Extractor allows you to create new smaller subsets from the World Map Kit SQLite master database. You simply specify the bounding box (or a shape file) for the new area then it will create a new SQLite database for that regions. There are options to preserve high level data allowing you to keep higher extent features, about 700 meg of data, such as the world, countries, high level roads etc. as a backdrop to the cut out area. If you choose not to preserve the high level data the tool crosscuts every layer leaving you with the smallest dataset possible. The tool works with multiple projections but does not re-project. Specifying the srid allows you to configure your bounding box in decimal degrees regardless of the source database projection. We will consider enhancing the tool to support projection based on feedback.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WorldMapKitDataExtractor/Screenshot.png)

# [Wrap Dateline Mode Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/WrapdateLineModeSample)

In this Wpf project, we show a new property of Overlay, WrapDatelineMode. This property that allows to continuously pan west or east with world map was already available in the dev branch. This concept was shown in the previous sample “WrapDatelineMode”. Now it is fully supported in Map Suite 5 and you can see in this sample how the behavior for the map in both Decimal Degrees (Lat/Long) and Spherical Mercator (Google Map/Bing map projection). In order to run this sample, you will need the latest release for Wpf with references to MapSuiteCore.dll and WpfDesktopEdition.dll.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WrapdateLineModeSample/Screenshot.gif)

# [Zoom To Full Extent Wpf Sample for Wpf](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/support/v10/samples/wpf/ZoomToFullExtent)

This is a simple project that shows how to set the current extent based on a collection of layers. You can use this technique for the common task of having the map set to the full extent. Instead of having to manually set the full extent, you can pass all the layers you want the full extent to be based on.
              
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ZoomToFullExtent/ScreenShot.png)


