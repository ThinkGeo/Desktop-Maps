# Weather Line Style Sample for Wpf

### Description

In this WPF sample, we learn how to extend **LineStyle** class to create a style for representing weather fronts, such as cold front, warm front, or occluded front for your weather maps. To achieve that styling a regular **LineStyle** is used for the front line itself. For symbolizing the type of front an icon is used. 

Notice the two handy properties to give you more control: Spacing property to adjust the distance in screen coordinate between each symbol on the line, and Side property to control on what side of the line front the symbols should appear. Of course, as you zoom in and out on the map the spacing between each symbol remain the same as it is set in screen coordinate.              

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WeatherLineStyleSample/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class customGeoImageLineStyle : LineStyle
{
    protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
    {
       PointStyle pointStyle = new PointStyle(geoImage);
       foreach (Feature feature in features)
        {
            LineShape lineShape = (LineShape)feature.GetShape();
            lineStyle.Draw(new BaseShape[] { lineShape }, canvas, labelsInThisLayer, labelsInAllLayers);
            double totalDist = 0;
            for (int i = 0; i < lineShape.Vertices.Count - 1; i++)
            {
                PointShape pointShape1 = new PointShape(lineShape.Vertices[i]);
                PointShape pointShape2 = new PointShape(lineShape.Vertices[i + 1]);

                LineShape tempLineShape = new LineShape();
                tempLineShape.Vertices.Add(lineShape.Vertices[i]);
                tempLineShape.Vertices.Add(lineShape.Vertices[i + 1]);
                double angle = GetAngleFromTwoVertices(lineShape.Vertices[i], lineShape.Vertices[i + 1]);
                
                if (side == SymbolSide.Left)
                {
                    if (angle >= 270) { angle = angle - 180; }
                }
                else
                {
                    if (angle <= 90) { angle = angle + 180; }
                }
                pointStyle.RotationAngle = (float)angle;
               
                float screenDist = ExtentHelper.GetScreenDistanceBetweenTwoWorldPoints(canvas.CurrentWorldExtent, pointShape1, 
                                                                                    pointShape2, canvas.Width, canvas.Height);
                double currentDist = Math.Round(pointShape1.GetDistanceTo(pointShape2, canvas.MapUnit, DistanceUnit.Meter), 2);
                double worldInterval = (currentDist * spacing) / screenDist;

                while (totalDist <= currentDist)
                {
                    PointShape tempPointShape = tempLineShape.GetPointOnALine(StartingPoint.FirstPoint, totalDist, canvas.MapUnit, DistanceUnit.Meter);
                    pointStyle.Draw(new BaseShape[] { tempPointShape }, canvas, labelsInThisLayer, labelsInAllLayers);
                    totalDist = totalDist + worldInterval;
                }
                totalDist = totalDist - currentDist;
             }
        }
    }
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.LineStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.linestyle)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)
- [ThinkGeo.MapSuite.Shapes.DistanceUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.distanceunit)
- [ThinkGeo.MapSuite.Styles.PointStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyle)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
