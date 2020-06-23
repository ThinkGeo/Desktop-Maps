# Line Style With Increment Sample for WinForms

### Description
In this WinForms desktop project, we show how to create a custom **LineStyle** for showing distance increment at a regular interval (every tenth kilometer). Having this **LineStyle** can be very handy when dealing with line networks, such as roads or railways.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/LineStyleWithIncrementsSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class CustomIncrementLineStyle : LineStyle
{
    protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
    {
        LineShape drawingLineShape; 
        PointShape linePoint;
        LineBaseShape lineShape; 
        LineStyle lineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Black, 1f, false);
        LineShape tangentLineShape; 
        ScreenPointF screenPointF; 
        double angle, decDistance = 0; 
        
        foreach (Feature feature in features)
        {
            drawingLineShape = (LineShape)feature.GetShape();

            while (!(decDistance > drawingLineShape.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer)))
            {
                linePoint = drawingLineShape.GetPointOnALine(StartingPoint.FirstPoint, decDistance, GeographyUnit.Meter, DistanceUnit.Kilometer);

                tangentLineShape = GetTangentForLinePosition(drawingLineShape, decDistance);
                angle = GetAngleFromTwoVertices(tangentLineShape.Vertices[0], tangentLineShape.Vertices[1]);

                angle += 90.0;
                if (angle >= 360.0) { angle = angle - 180;}

                screenPointF = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent, linePoint, (float)canvas.Width, (float)canvas.Height);  

                canvas.DrawText("    " + decDistance.ToString(), new GeoFont("Arial", 12, DrawingFontStyles.Bold),
                    new GeoSolidBrush(GeoColor.StandardColors.Black), new GeoPen(GeoColor.StandardColors.White),
                    new ScreenPointF[] { screenPointF }, DrawingLevel.LabelLevel, 0f, 0f, Convert.ToSingle(angle));

                double dblTranslateAngle = GetOrthogonalFromVertex(tangentLineShape.Vertices[0], tangentLineShape.Vertices[1], Side.Right);

                double worldDist = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints
                (canvas.CurrentWorldExtent, 0, 0, 5, 0, canvas.Width, canvas.Height, GeographyUnit.Meter, DistanceUnit.Meter);

                PointShape pointShape2 = (PointShape)BaseShape.TranslateByDegree
                    (linePoint, worldDist, dblTranslateAngle, GeographyUnit.Meter, DistanceUnit.Meter);

                lineShape = new LineShape(new Vertex[] {
	            new Vertex(linePoint),
	            new Vertex(pointShape2) });

                lineStyle.Draw(new BaseShape[] { lineShape }, canvas, labelsInThisLayer, labelsInAllLayers);
                decDistance += 0.1;
            }
            lineStyle.Draw(features, canvas, labelsInThisLayer, labelsInAllLayers);
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

- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.LineBaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.linebaseshape)
- [ThinkGeo.MapSuite.Shapes.LineBaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.linebaseshape)
- [ThinkGeo.MapSuite.Styles.LineStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.linestyle)
- [ThinkGeo.MapSuite.Shapes.DistanceUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.distanceunit)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
