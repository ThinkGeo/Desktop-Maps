# Create GRID Sample for WinForms

### Description

Today’s sample shows the new feature available in this may release Map Suite 5 for creating GRID files. A GRID is a raster format that defines a geographic space as an array of equally sized squares (cells) arranged in rows and columns. Each cell stores a numeric value that represents an attribute (such as elevation, surface slope, soil pH etc.) for that unit of space. Each GRID cell is referenced by its x, y coordinate location. Typically a GRID file is created based on some sample points with known values. In today’s sample, we take the example of creating a GRID file based on a point based shapefile representing soil pH values of some sample locations in a field. Using the Inverse Weighted Distance algorithm for interpolation, we create the GRID with the pH value for the entire extent of the field. Look at the code and comments for more details on how GRID files get generated and displayed on the map. This sample is a Desktop application but GRID can be used in all the editions of Map Suite.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/CreateGRIDSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0.0](https://www.nuget.org/packages?q=thinkgeo)

### About the Code

Working...

### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

Working...

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
