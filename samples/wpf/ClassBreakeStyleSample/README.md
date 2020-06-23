# Class Break Style Sample for Wpf

### Description

In this project you will see how to use the ClassBreakStyle to group and render features by values. ClassBreakStyle is a useful technique as it allows you to group various features by the specified values, then applies differently style to the feature groups. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/thinkgeo_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ClassBreakeStyleSample/Screenshot.png)

### About the Code
Use a ClassBreakStyle to colorize each state differently depending on the range into which its population falls.  This value is found in the states ShapeFile DBF in the column named "POP1990".

```csharp
       
ClassBreakStyle statesStyle = new ClassBreakStyle("POP1990");
statesStyle.ClassBreaks.Add(
    new ClassBreak(value: 0, areaStyle: AreaStyles.CreateSimpleAreaStyle(
        fillBrushColor: GeoColors.LightGray,
        outlinePenColor: GeoColors.DarkGray,
        outlinePenWidth: 1)
    )
);
statesStyle.ClassBreaks.Add(
    new ClassBreak(value: 1000000, areaStyle: AreaStyles.CreateSimpleAreaStyle(
        fillBrushColor: GeoColors.LightBlue,
        outlinePenColor: GeoColors.CornflowerBlue,
        outlinePenWidth: 1)
    )
);
statesStyle.ClassBreaks.Add(
    new ClassBreak(value: 3500000, areaStyle: AreaStyles.CreateSimpleAreaStyle(
        fillBrushColor: GeoColors.SkyBlue,
        outlinePenColor: GeoColors.DeepSkyBlue,
        outlinePenWidth: 1)
    )
);
// Add stateStyle to the statesLayer and apply the style to all zoom levels.
statesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(statesStyle);
statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

```

### Getting Help

[ThinkGeo Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[ThinkGeo Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
