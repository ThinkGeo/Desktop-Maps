# Rendering Points Performance Test Sample for WinForms

### Description
1. In Map Suite, a shape (a shape object inherited from the BaseShape class such as PointShape, LineShape, etc) is heavier than a feature. You can treat a feature as a holder of a byte array (Well Known Binary), it’s lightweight and doesn’t have too many methods to manipulate its core data (Well Known Binary). Shape, however, is heavy, it provides all the info as well as methods against the data.  You can cast from a feature to a shape and vice versa.
 
1. To update a feature, usually, we need to convert a feature to a shape, update the shape and then convert it back to a feature. This will create a new shape and a new feature, which is more straightforward but the cost is high. Below is a method updating a point feature by adding 1 to its X and Y.

```csharp
       private void UpdateFeatureThroughShape(Feature feature)
        {
            byte[] wkb = feature.GetWellKnownBinary();
            PointShape pointShape = new PointShape(wkb);
            pointShape.X += 1;
            pointShape.Y += 1;
            feature = pointShape.GetFeature();
        }
```

3. You can update a feature by directly updating its WKB. The following method updates a point feature by adding 1 to its X and Y. It’s the most efficient way where we manipulate a byte array without creating any new object. It is not straightforward and you need to have a deep understanding of the format of WKB. That’s what we are doing in this sample. We will add more APIs to Feature to make it straightforward and efficient.

```csharp
       private void UpdateFeatureThroughWKB(Feature feature)
        {
            byte[] wkb = feature.GetWellKnownBinary();
            double x = BitConverter.ToDouble(wkb, 5);
            double y = BitConverter.ToDouble(wkb, 13);
            BitConverter.GetBytes(x + 1).CopyTo(wkb, 5);
            BitConverter.GetBytes(y + 1).CopyTo(wkb, 13);
            feature.SetWellKnownBinary(wkb);
        }
```

4. The “Grid size in Pixel” textbox let you set the size of the grid in which up to one point can be displayed, the bigger the grid is, the more sparse the points will be. This is an optimization with which we can avoid showing too many points on the map.  Play with it and you can see the performance differences.


Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/RenderingPointsPerformanceTestSample-ForWinForms/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Package.

[MapSuiteDesktopForWinForms-BareBone 10.4.1](https://www.nuget.org/packages/MapSuiteDesktopForWinForms-BareBone/10.4.1)

[ThinkGeo.MapSuite 10.4.0](https://www.nuget.org/packages/ThinkGeo.MapSuite/10.4.0)

[ThinkGeo.MapSuite.Layers.Grids 10.4.0](https://www.nuget.org/packages/ThinkGeo.MapSuite.Layers.Grids/10.4.0)

### About the Code

>**Adding points to the pointFeatureLayer**
```csharp
        for (int i = 0; i < pointCount; i++)
        {
            var randomX = random.NextDouble();
            var x = testExtent.UpperLeftPoint.X + testExtent.Width * randomX;
            var randomY = random.NextDouble();
            var y = testExtent.LowerLeftPoint.Y + testExtent.Height * randomY;
            
            pointFeatureLayer.InternalFeatures.Add(new Feature(x, y));
        }
            
        layerOverlay.Layers.Add(pointFeatureLayer);
```

>**Optimize the points in the pointFeatureLayer**
```csharp
        double resolutionX = boundingBox.Width / screenWidth * GridSizeInPixel;
        double resolutionY = boundingBox.Height / screenHeight * GridSizeInPixel;

        foreach (var feature in drawingFeatures)
        {
            byte[] wkb = feature.GetWellKnownBinary();
            double x = BitConverter.ToDouble(wkb, 5);
            double y = BitConverter.ToDouble(wkb, 13);

            int gridCol = Convert.ToInt32(Math.Floor((x - layerBoundingBox.UpperLeftPoint.X) / resolutionX));
            int gridRow = Convert.ToInt32(Math.Floor((layerBoundingBox.UpperLeftPoint.Y - y) / resolutionY));

            if (!grids.ContainsKey($"{gridCol}-{gridRow}"))
            {
                simplifiedDrawingFeautres.Add(feature);
                grids.Add($"{gridCol}-{gridRow}", true);
            }
        } 
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop Product Description](https://thinkgeo.com/desktop)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](https://www.thinkgeo.com)


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.

