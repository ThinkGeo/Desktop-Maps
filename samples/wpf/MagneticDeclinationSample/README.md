# Magnetic Declination Sample for Wpf

### Description

In today's WPF project, we show you how to add the Magnetic Declination or Magnetic variation to the map, it's designed as an **AdormentLayer**, which is used for showing the angle on the horizontal plane between magnetic north (the direction in which the north end of a compass needle, corresponding to the direction of the Earth's magnetic field lines) and true north (the direction along a meridian towards the geographic North Pole). This angle varies depending on one's position on the Earth's surface, and over time. See
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/MagneticDeclinationSample-ForWpf/blob/master/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
magneticDeclinationAdornmentLayer = new MagneticDeclinationAdornmentLayer() { Location = (AdornmentLocation)(cmbLocation.SelectedIndex + 1) };
magneticDeclinationAdornmentLayer.TrueNorthPointStyle.SymbolSize = 25;
magneticDeclinationAdornmentLayer.TrueNorthLineStyle.InnerPen.Width = 2f;
magneticDeclinationAdornmentLayer.TrueNorthLineStyle.OuterPen.Width = 5f;
magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.InnerPen.Width = 2f;
magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.OuterPen.Width = 5f;

wpfMap1.AdornmentOverlay.Layers.Add(magneticDeclinationAdornmentLayer);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.MagneticDeclinationAdornmentLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.magneticdeclinationadornmentlayer)
- [ThinkGeo.MapSuite.Wpf.AdornmentOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.adornmentoverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
