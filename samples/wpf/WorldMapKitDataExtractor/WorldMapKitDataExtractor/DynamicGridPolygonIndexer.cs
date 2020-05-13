using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Shapes;

namespace WorldMapKitDataExtractor
{
    public static class DynamicGridPolygonIndexer
    {
        //Get all Cells without cutting polygon.  restricts how many levels to iterate based on minimum cell area
        public static GeoCollection<Feature> GetIntersectingGridCells(BaseShape source, GeographyUnit sourceUnit, double minimumCellArea, AreaUnit minimumCellAreaUnit)
        {
            //find bounding box of set of boundary
            RectangleShape boundingBox = source.GetBoundingBox();

            Stack<RectangleShape> processingStack = new Stack<RectangleShape>();
            processingStack.Push(boundingBox);

            Collection<RectangleShape> insideRectangles = new Collection<RectangleShape>();
            Collection<RectangleShape> intersectingRectangles = new Collection<RectangleShape>();

            while (processingStack.Count > 0)
            {
                RectangleShape currentBoundingBox = processingStack.Pop();
                bool toBeSplit = false;

                if (source.Contains(currentBoundingBox))
                {
                    //if inside a boundary add to inside rectangles holder
                    insideRectangles.Add(currentBoundingBox);
                }
                else if (source.Intersects(currentBoundingBox))
                {
                    //if intersecting
                    //we multiply the minimum cell area by 9 to do this check because if it is below it we are splitting it into 9 equal parts
                    //this guarantees that after it is split they new cells are still larger than the minimum area.
                    if (currentBoundingBox.GetArea(sourceUnit, minimumCellAreaUnit) > minimumCellArea * 9)
                    {
                        //if it is too big it needs to be split
                        toBeSplit = true;
                    }
                    else
                    {
                        //if small enough add to intersecting rectangles holder
                        intersectingRectangles.Add(currentBoundingBox);
                    }
                }

                if (toBeSplit)
                {
                    //split bounding boxes
                    Collection<RectangleShape> splitBoundingBoxes = splitRectangles(currentBoundingBox);
                    //and add each to stack
                    foreach (RectangleShape splitBoundingBox in splitBoundingBoxes)
                    {
                        processingStack.Push(splitBoundingBox);
                    }
                }
            }

            GeoCollection<Feature> cells = new GeoCollection<Feature>();

            foreach (var rect in insideRectangles)
            {
                RectangleShape bboxShape = rect;
                Feature bbox = new Feature(bboxShape);

                bbox.ColumnValues["type"] = "in";
                cells.Add(bbox);
            }

            foreach (var rect in intersectingRectangles)
            {
                RectangleShape bboxShape = rect;
                Feature bbox = new Feature(bboxShape);

                bbox.ColumnValues["type"] = "intersecting";
                cells.Add(bbox);
            }

            return cells;
        }

        private static Collection<RectangleShape> splitRectangles(RectangleShape currentBoundingBox)
        {
            double x1, x2, x3, x4, y1, y2, y3, y4;

            x1 = currentBoundingBox.UpperLeftPoint.X;
            x4 = currentBoundingBox.UpperRightPoint.X;

            x2 = x1 + (x4 - x1) / 3.0;
            x3 = x4 - (x4 - x1) / 3.0;

            y1 = currentBoundingBox.UpperLeftPoint.Y;
            y4 = currentBoundingBox.LowerLeftPoint.Y;

            y2 = y1 + (y4 - y1) / 3.0;
            y3 = y4 - (y4 - y1) / 3.0;

            Collection<RectangleShape> splitBoundingBoxes = new Collection<RectangleShape>();
            splitBoundingBoxes.Add(new RectangleShape(x1, y1, x2, y2));
            splitBoundingBoxes.Add(new RectangleShape(x2, y1, x3, y2));
            splitBoundingBoxes.Add(new RectangleShape(x3, y1, x4, y2));
            splitBoundingBoxes.Add(new RectangleShape(x1, y2, x2, y3));
            splitBoundingBoxes.Add(new RectangleShape(x2, y2, x3, y3));
            splitBoundingBoxes.Add(new RectangleShape(x3, y2, x4, y3));
            splitBoundingBoxes.Add(new RectangleShape(x1, y3, x2, y4));
            splitBoundingBoxes.Add(new RectangleShape(x2, y3, x3, y4));
            splitBoundingBoxes.Add(new RectangleShape(x3, y3, x4, y4));

            return splitBoundingBoxes;
        }
    }
}
