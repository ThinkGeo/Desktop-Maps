# Graphic Logo Adornment Sample for WinForms

### Description

This sample shows how you can display your logo on the map using an AdornmentLayer. The advantage of using an Adornment is that the graphic stays in place and doesn't move as you pan your map. The sample should work for various kinds of logos and allow you to change the position using the AdornmentLayer's properties.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/GraphicLogoAdornmentSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0.0](https://www.nuget.org/packages?q=thinkgeo)

### About the Code

```CSharp
GraphicLogoAdornmentLayer graphicLogoAdornmentLayer = new GraphicLogoAdornmentLayer();
graphicLogoAdornmentLayer.LogoImage = new Bitmap(@"..\..\Data\logo.png");
winformsMap1.AdornmentOverlay.Layers.Add(graphicLogoAdornmentLayer);
```

### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:



### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
