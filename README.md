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
