# Custom Track Polygon Sample for WinForms

### Description
Learn how to extend the **TrackInteractiveOverlay** to add behaviors, like deleting the last added vertex when right-clicking the track line.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/CustomTrackPolygonSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
public class CustomTrackInteractiveOverlay : TrackInteractiveOverlay
{
    protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
    {
        System.Diagnostics.Debug.WriteLine(this.Vertices.Count);

        if (interactionArguments.MouseButton != MapMouseButton.Right)
        {
            return base.MouseDownCore(interactionArguments);
        }
        else
        {
            if (this.Vertices.Count >= 5)
            {
                RemoveLastVertexAdded();
                MouseDownCount = MouseDownCount - 1;
            }

            return new InteractiveResult();
        }
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
- [ThinkGeo.MapSuite.WinForms.InteractionArguments](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.interactionarguments)
- [ThinkGeo.MapSuite.WinForms.InteractiveResult](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.interactiveresult)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
