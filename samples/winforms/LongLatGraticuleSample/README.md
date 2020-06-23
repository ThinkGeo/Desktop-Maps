# Long Lat Graticule Sample for WinForms

### Description
After the projects on North Arrow and Compass, we created another project related to Adornment Layer to have a more stylish map and give more information to the user navigating the map.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/LongLatGraticuleSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class GraticuleAdornmentLayer : AdornmentLayer
{
    protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
    {
        RectangleShape currentExtent = canvas.CurrentWorldExtent;
        double currentMinX = currentExtent.UpperLeftPoint.X;
        double currentMaxX = currentExtent.UpperRightPoint.X;
        double currentMaxY = currentExtent.UpperLeftPoint.Y;
        double currentMinY = currentExtent.LowerLeftPoint.Y;

        double increment;
        increment = GetIncrement(currentExtent.Width, graticuleDensity);

        Collection<GraticuleLabel> meridianGraticuleLabels = new Collection<GraticuleLabel>();
        Collection<GraticuleLabel> parallelGraticuleLabels = new Collection<GraticuleLabel>();
        
        double x = 0;
        for (x = CeilingNumber(currentExtent.UpperLeftPoint.X, increment); x <= currentExtent.UpperRightPoint.X; x += increment)
        {
            LineShape lineShapeMeridian = new LineShape();
            lineShapeMeridian.Vertices.Add(new Vertex(x, currentMaxY));
            lineShapeMeridian.Vertices.Add(new Vertex(x, currentMinY));
            canvas.DrawLine(lineShapeMeridian, new GeoPen(graticuleColor,0.5F), DrawingLevel.LevelFour);
            
            ScreenPointF meridianLabelPosition = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent,x,currentMaxY,canvas.Width,canvas.Height);
            meridianGraticuleLabels.Add(new GraticuleLabel(FormatLatLong(x,LineType.Meridian,increment), meridianLabelPosition));
         }

        double y = 0;
        for (y = CeilingNumber(currentExtent.LowerLeftPoint.Y, increment); y <= currentExtent.UpperRightPoint.Y; y += increment)
        {
            LineShape lineShapeParallel = new LineShape();
            lineShapeParallel.Vertices.Add(new Vertex(currentMaxX, y));
            lineShapeParallel.Vertices.Add(new Vertex(currentMinX, y));
            canvas.DrawLine(lineShapeParallel, new GeoPen(graticuleColor, 0.5F), DrawingLevel.LevelFour);

            //Gets the label and screen position of each parallel.
            ScreenPointF parallelLabelPosition = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent, currentMinX, y, canvas.Width, canvas.Height);
            parallelGraticuleLabels.Add(new GraticuleLabel(FormatLatLong(y,LineType.Parallel,increment), parallelLabelPosition));
        }

       foreach (GraticuleLabel meridianGraticuleLabel in meridianGraticuleLabels)
       {
           Collection<ScreenPointF> locations = new Collection<ScreenPointF>();
           locations.Add(new ScreenPointF(meridianGraticuleLabel.location.X, meridianGraticuleLabel.location.Y + 6));

           canvas.DrawText(meridianGraticuleLabel.label, new GeoFont("Arial", 10), new GeoSolidBrush(GeoColor.StandardColors.Navy),
               new GeoPen(GeoColor.StandardColors.White, 2), locations, DrawingLevel.LevelFour, 8, 0, 0);
       }

       foreach (GraticuleLabel parallelGraticuleLabel in parallelGraticuleLabels)
       {
           Collection< ScreenPointF> locations = new Collection<ScreenPointF>();
           locations.Add(new ScreenPointF(parallelGraticuleLabel.location.X,parallelGraticuleLabel.location.Y));

           canvas.DrawText(parallelGraticuleLabel.label, new GeoFont("Arial", 10), new GeoSolidBrush(GeoColor.StandardColors.Navy),
               new GeoPen(GeoColor.StandardColors.White,2), locations, DrawingLevel.LevelFour, 8, 0, 90);
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

- [ThinkGeo.MapSuite.Layers.AdornmentLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.adornmentlayer)
- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)
- [ThinkGeo.MapSuite.Drawing.GeoFont](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geofont)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoPen](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geopen)
- [ThinkGeo.MapSuite.Drawing.DrawingLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.drawinglevel)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
