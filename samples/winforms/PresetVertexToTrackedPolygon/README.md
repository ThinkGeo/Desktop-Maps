# Preset Vertex To Tracked Polygon Sample for WinForms

### Description

This Desktop sample shows how to extend TrackInteractiveOverlay to give the ability to add a preset vertex to a track polygon while tracking. This can be handy in the situations where you need to add a vertex outside the current extent of the map or when you need to add a vertex with precise X and Y values. In the custom PresetVertexTrackInteractiveOverlay, you can see how the protected function GetTrackingShapeCore was overridden to implement this tracking behavior of the polygon.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/PresetVertexToTrackedPolygonSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class PresetVertexTrackInteractiveOverlay : TrackInteractiveOverlay
{
    protected override BaseShape GetTrackingShapeCore()
    {
        BaseShape baseShape = base.GetTrackingShapeCore();
        if (baseShape is PolygonShape)
        {
            PolygonShape polygon = (PolygonShape)baseShape;
            if (polygon != null)
            {
                if (newAddedVertex != null)
                {
                    AddVertexIndexes.Add(Vertices.Count - 2);
                    PresetPointShapes.Add(new PointShape(newAddedVertex.X, newAddedVertex.Y));
                    newAddedVertex = null;
                }

                int index = 0;
                foreach (int addVertexIndex in AddVertexIndexes)
                {
                    PointShape presetPointShape = presetPointShapes[index];
                    index++;

                    if (presetPointShape != null)
                    {
                        Vertices[addVertexIndex] = new Vertex(presetPointShape);
                    }
                }
            }
        }
        return baseShape;
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
- [ThinkGeo.MapSuite.Shapes.BaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.baseshape)
- [ThinkGeo.MapSuite.Shapes.PolygonShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.polygonshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
