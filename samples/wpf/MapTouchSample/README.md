# Map Touch Sample for Wpf

### Description
Map Suite WPF Edition supports touch events. This sample shows how to add a marker to map by MapTap event.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MapTouchSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Map1.MapTap += Map1_MapTap;

private void Map1_MapTap(object sender, MapTapWpfMapEventArgs e)
{
    Marker marker = new Marker(e.WorldX, e.WorldY);
    marker.ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute));
    marker.Width = 20;
    marker.Height = 34;
    marker.YOffset = -17;

    markerOverlay.Markers.Add(marker);
    markerOverlay.Refresh();
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.MapTapWpfMapEventArgs](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Wpf.MapTapWpfMapEventArgs)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in MainWindow.xaml.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
