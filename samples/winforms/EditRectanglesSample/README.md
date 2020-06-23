# Edit Rectangles Sample for WinForms

### Description

This sample shows how to extend the **EditInteractiveOverlay** rectangles as shapes, rather than polygon shapes, by setting special column values. For features (both *Well Known Text* and *Well Known Binary*), the concept of a rectangle is not supported and typically rectangles are handled as polygons. This feature allows users to modify the rectangle but requires that the modification keep a rectangular form. The rectangle doesn't need to be straight as long as all of the corner angles are at 90 degrees relative to each other.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EditRectanglesSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class CustomEditInteractiveOverlay : EditInteractiveOverlay
{
    protected override IEnumerable<Feature> CalculateResizeControlPointsCore(Feature feature)
    {
        if (feature.ColumnValues.ContainsKey("Edit") && feature.ColumnValues["Edit"] == "rectangle")
        {
            Collection<Feature> resizeControlPoints = new Collection<Feature>();

            PolygonShape polygonShape = feature.GetShape() as PolygonShape;
            if (polygonShape != null)
            {
                foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                {
                    resizeControlPoints.Add(new Feature(vertex, feature.Id));
                }
            }
            return resizeControlPoints;
        }

        return base.CalculateResizeControlPointsCore(feature);
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

- [ThinkGeo.MapSuite.WinForms.EditInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.editinteractiveoverlay)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.PolygonShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.polygonshape)
- [ThinkGeo.MapSuite.Shapes.Vertex](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.vertex)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
