# Custom Track Line Sample for WinForms

### Description
In today’s project, we are going to see how to extend the TrackInteractiveOverlay in the Desktop edition to have the desired behavior when tracking a line. In this case, we show how to override the MouseDownCore function to have the line being tracked at left mouse click and have the last vertex added deleted at right mouse click.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/CustomTrackLineSample-ForWinForms/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class CustomTrackInteractiveOverlay : TrackInteractiveOverlay
{
    protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
    {
        
        if (interactionArguments.MouseButton != MapMouseButton.Right)
            return base.MouseDownCore(interactionArguments);
        else
            RemoveLastVertexAdded();
            return new InteractiveResult();
    }
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.TrackInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.trackinteractiveoverlay)
- [ThinkGeo.MapSuite.WinForms.InteractiveResult](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.interactiveresult)
- [ThinkGeo.MapSuite.WinForms.InteractionArguments](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.interactionarguments)
- [ThinkGeo.MapSuite.WinForms.MapMouseButton](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.mapmousebutton)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
