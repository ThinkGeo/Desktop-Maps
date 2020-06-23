# Apply DirectionPointStyle for LineStyle for Wpf

### Description

The Map Suite WPF ApplyDirectionPointStyleForLineStyle sample will guide you to draw lineStyle's direction Point on map. The direction Point can be an image or a glyph, it not only rotates the icon accross the angle of the road, but also provides a way to customize the rotation of the direction point. The arrows highlighted in the red circle in the following screenshot are customized based on the line's attributes. Please check out the source for detail. This sample supports Map Suite 10.5.8 and higher. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ApplyDirectionalPointStyle/Screenshot.gif)

### About the Code


``` csharp
// Set up the line style with white inner pen and black center pen. 
var lineStyle = new LineStyle(new GeoPen(GeoColors.Black, 16) { StartCap = DrawingLineCap.Round, EndCap = DrawingLineCap.Round }, new GeoPen(GeoColors.White, 13) { StartCap = DrawingLineCap.Round, EndCap = DrawingLineCap.Round });
// Set up the required column name for the style. We will customize the line style based on this column value. 
lineStyle.RequiredColumnNames.Add("FENAME");
            
// Set up the style for Direction Point and set up the event for customization. 
lineStyle.DirectionPointStyle = new PointStyle(new GeoImage("AppData\\Arrow.png"));
lineStyle.DrawingDirectionPoint += LineStyle_DrawingPointStyle;
            
private void LineStyle_DrawingPointStyle(object sender, DrawingDirectionPointEventArgs e)
{
    // Customize the direction point for the line feature whose "FENAME" column equals to "Mo-Pac". 
    if (e.Line.ColumnValues["FENAME"] == "Mo-Pac")
    {
        e.RotationAngle = 0;
    }
}
```

### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.

