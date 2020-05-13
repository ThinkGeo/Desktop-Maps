# US Demographic Map Sample for Wpf

### Description
The Demographic and Lifestyle sample template gives you a head start on your statistics project, which includes details about race, age, gender, land usage, and more for all the states in U.S. The template contains pre-styled layers that can be used as-is, or as the foundation for adding your own map notes and layers.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/USDemographicMapSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class CustomDotDensityStyle : DotDensityStyle
{
    protected override void DrawSampleCore(GeoCanvas canvas, DrawingRectangleF drawingExtent)
    {
        base.DrawSampleCore(canvas, drawingExtent);
        PointShape upperLeftPoint = ExtentHelper.ToWorldCoordinate(canvas.CurrentWorldExtent, drawingExtent.CenterX - drawingExtent.Width / 2, drawingExtent.CenterY - drawingExtent.Height / 2, canvas.Width, canvas.Height);
        PointShape lowerRightPoint = ExtentHelper.ToWorldCoordinate(canvas.CurrentWorldExtent, drawingExtent.CenterX + drawingExtent.Width / 2, drawingExtent.CenterY + drawingExtent.Height / 2, canvas.Width, canvas.Height);
        RectangleShape rectangle = new RectangleShape(upperLeftPoint, lowerRightPoint);
        rectangle.ScaleDown(10);

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

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.DotDensityStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.dotdensitystyle)
- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Shapes.DrawingRectangleF](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.drawingrectanglef)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Shapes.BaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.baseshape)
- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)
- [ThinkGeo.MapSuite.Drawing.GeoFont](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geofont)
- [ThinkGeo.MapSuite.Drawing.DrawingFontStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.drawingfontstyles)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.DrawingFontStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.drawingfontstyles)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method InitializeMap() in MapModel.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
