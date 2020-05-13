using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace BreadCrumbTrail
{
    class RotatedImageStyle : Style
    {
        private GeoImage geoImage;
        private string angleColumnName;

    public RotatedImageStyle()
        : this(new GeoImage(), string.Empty)
    { }

    public RotatedImageStyle(GeoImage geoImage, string angleColumnName)
    {
        this.geoImage = geoImage;
        this.angleColumnName = angleColumnName;

    }

    public GeoImage GeoImage
    {
        get { return geoImage; }
        set { GeoImage = value; }
    }

    public string AngleColumnName
    {
        get { return angleColumnName; }
        set { AngleColumnName = value; }
    }

    protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
    {
        
            foreach (Feature feature in features)
            {
                float angleData = Convert.ToSingle(feature.ColumnValues[angleColumnName]);
                PointShape pointShape = (PointShape)feature.GetShape();
                canvas.DrawWorldImageWithoutScaling(geoImage, pointShape.X, pointShape.Y, DrawingLevel.LevelFour, 0, 0, 360 - angleData); //angleData);
            }
        
       
    }

    protected override Collection<string> GetRequiredColumnNamesCore()
    {
        Collection<string> columns = new Collection<string>();
        if (!columns.Contains(angleColumnName))
        {
            columns.Add(angleColumnName);
        }
        return columns;
    }
    }
}
