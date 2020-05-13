# Mini Map Sample for Wpf

### Description

This project shows how to create a simple mini map to give a reference of where you are when you zoomed in. As for the MiniMapAdormentLayer inherits from AdornmentLayer.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/MiniMapSample-ForWpf/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
{
   GeoImage miniImage = new GeoImage(width, height);
   RectangleShape scaledWorldExtent = ExtentHelper.GetDrawingExtent(canvas.CurrentWorldExtent, width, height);
   scaledWorldExtent.ScaleUp(200);
   PlatformGeoCanvas minCanvas = new PlatformGeoCanvas();

   minCanvas.BeginDrawing(miniImage, scaledWorldExtent, canvas.MapUnit);
   foreach (Layer layer in layers)
   {
       layer.Draw(minCanvas, labelsInAllLayers);
   }

   minCanvas.DrawArea(RectangleShape.ScaleDown(minCanvas.CurrentWorldExtent, 1), new GeoPen(GeoColor.StandardColors.Gray, 2), DrawingLevel.LevelOne);
   minCanvas.DrawArea(canvas.CurrentWorldExtent, new GeoPen(GeoColor.StandardColors.Black, 2), DrawingLevel.LevelOne);

   minCanvas.EndDrawing();

   ScreenPointF drawingLocation = GetDrawingLocation(canvas, width, height);

   canvas.DrawScreenImageWithoutScaling(miniImage, (drawingLocation.X + width / 2) + 10, (drawingLocation.Y + height / 2) - 10, DrawingLevel.LevelOne, 0, 0, 0);
}
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.AdornmentLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.AdornmentLayer)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method map_Loaded() in MainWindow.xaml.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
