# Annotation Style Sample for Wpf

### Description

In this project you will see how to use the AnnotationStyle to display and edit a feature depending on the value of a specific property in its data source.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/thinkgeo_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/AnnotationStyleSample-ForWpf/blob/master/Screenshot.gif)

### About the Code

```csharp
       
protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
{
    foreach (var feature in features)
    {
        if (feature.GetWellKnownType() == WellKnownType.Line)
        {
            LineShape lineShape = (LineShape)feature.GetShape();
            Feature newFeature = new Feature(lineShape.Vertices[0]);
            newFeature.ColumnValues.Add(TextColumnName, feature.ColumnValues[TextColumnName]);
            var labelingCandidates = GetLabelingCandidates(newFeature, canvas, Font, XOffsetInPixel, YOffsetInPixel, RotationAngle);
            foreach (var labelingCandidate in labelingCandidates)
            {
                PolygonShape maskArea = ConvertPolygonShapeToWorldCoordinate(labelingCandidate.ScreenArea, canvas.CurrentWorldExtent, canvas.Width, canvas.Height);
                PointShape closestPoint = maskArea.GetClosestPointTo(new PointShape(lineShape.Vertices[1]), canvas.MapUnit);
                if (closestPoint != null)
                {
                    lineShape.Vertices[0] = new Vertex(closestPoint.X, closestPoint.Y);
                    LeaderLineStyle.Draw(new LineShape[] { lineShape }, canvas, labelsInThisLayer, labelsInAllLayers);
                }

                DrawMask(labelingCandidate, canvas, labelsInThisLayer, labelsInAllLayers);

                foreach (LabelInformation labelInfo in labelingCandidate.LabelInformation)
                {
                    var textPathInScreen = new ScreenPointF((float)labelInfo.PositionInScreenCoordinates.X, (float)labelInfo.PositionInScreenCoordinates.Y);
                    canvas.DrawText(labelInfo.Text, Font, TextBrush, HaloPen, new ScreenPointF[] { textPathInScreen }, DrawingLevel, 0, 0, DrawingTextAlignment.Default, (float)labelInfo.RotationAngle);
                }
            }
        }
    }
}

```

### Getting Help

[ThinkGeo Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/thinkgeo_desktop_for_wpf)

[ThinkGeo Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
