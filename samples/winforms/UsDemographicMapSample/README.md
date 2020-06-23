# US Demographic Map Sample for WinForms

### Description

The Demographic and Lifestyle sample template gives you a head start on your statistics project, which includes details about race, age, gender, land usage, and more for all the states in U.S. The template contains pre-styled layers that can be used as-is, or as the foundation for adding your own map notes and layers.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/UsDemographicMapSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
internal class CustomDotDensityStyle : DotDensityStyle
{
    protected override void DrawSampleCore(GeoCanvas canvas, DrawingRectangleF drawingExtent)
    {
        base.DrawSampleCore(canvas, drawingExtent);
        PointShape upperLeftPoint = ExtentHelper.ToWorldCoordinate(canvas.CurrentWorldExtent, drawingExtent.CenterX - drawingExtent.Width / 2, drawingExtent.CenterY - drawingExtent.Height / 2, canvas.Width, canvas.Height);
        PointShape lowerRightPoint = ExtentHelper.ToWorldCoordinate(canvas.CurrentWorldExtent, drawingExtent.CenterX + drawingExtent.Width / 2, drawingExtent.CenterY + drawingExtent.Height / 2, canvas.Width, canvas.Height);
        RectangleShape rectangle = new RectangleShape(upperLeftPoint, lowerRightPoint);
        rectangle.ScaleDown(10);

        // Here draw the points on Legend Image
        Random random = new Random(DateTime.Now.Millisecond);
        Collection<BaseShape> drawingPoints = new Collection<BaseShape>();
        for (int i = 0; i < DrawingPointsNumber; i++)
        {
            double x = rectangle.LowerLeftPoint.X + random.NextDouble() * (rectangle.Width);
            double y = rectangle.LowerLeftPoint.Y + random.NextDouble() * (rectangle.Height);
            drawingPoints.Add(new PointShape(x, y));
        }
        TextStyle textStyle = new TextStyle(DrawingPointsNumber.ToString(), new GeoFont("Arial", 20, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.FromArgb(180, GeoColor.FromHtml("#d3d3d3"))));
        textStyle.DrawSample(canvas, drawingExtent);
        CustomPointStyle.Draw(drawingPoints, canvas, new Collection<SimpleCandidate>(), new Collection<SimpleCandidate>());
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

- [ThinkGeo.MapSuite.Shapes.DrawingRectangleF](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.drawingrectanglef)
- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Shapes.BaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.baseshape)
- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
