# Track Overlay With Esc Sample for WinForms

### Description
This project shows how to implement aborting a trackshape with Esc key as you could do it in MapSuite 2.x.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/TrackOverlayWithEscSample-ForWinForms/blob/master/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
if (cancel)
{
    winformsMap1.TrackOverlay.Lock.EnterWriteLock();
    try
    {
        int count = winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Count;
        if (count > 0)
        {
            winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove("InTrackingFeature");
        }
    }
    finally
    {
        //Commit the changes.
        winformsMap1.TrackOverlay.Lock.ExitWriteLock();
    }

    cancel = false;
    if (winformsMap1.TrackOverlay.TrackMode == TrackMode.Polygon)
    {
        //Polygon needs to call MouseDoubleClick. Event that marks finishing tracking a Polygon. 
        //Same thing for Line.
        winformsMap1.TrackOverlay.MouseDoubleClick(new InteractionArguments());
    }
    else
    {
        //All other shapes have their tracking finalized at MouseUp event.
        winformsMap1.TrackOverlay.MouseUp(new InteractionArguments());
    }

    winformsMap1.Refresh();
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.TrackMode](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.trackmode)
- [ThinkGeo.MapSuite.WinForms.TrackInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.trackinteractiveoverlay)
- [ThinkGeo.MapSuite.WinForms.OverlayLock](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.overlaylock)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
