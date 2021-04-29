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
