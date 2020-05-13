using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Portable;
using ThinkGeo.MapSuite.Shapes;

namespace NLDASAnalysis
{
    public class NldasAsciiGridFeatureSource : FeatureSource
    {
        private string gridPathFilename;
        private int columnsCount;
        private int rowsCount;
        private Collection<FeatureSourceColumn> featureSourceColumns;
        private const string dataValueColumnName = "GridNumber";
        private RectangleShape gridExtent;
        private double cellSize;
        private PointShape lowerLeftPoint;
        private string[] gridPathfilenameData;
        private RtreeSpatialIndex rTreeIndex;
        private string indexPathFilename;
        private bool requireIndex = true;

        public bool RequireIndex
        {
            get { return requireIndex; }
            set { requireIndex = value; }
        }

        public NldasAsciiGridFeatureSource(string gridPathFilename)
        {
            this.gridPathFilename = gridPathFilename;
            indexPathFilename = Path.ChangeExtension(gridPathFilename, ".idx");
        }

        protected override void OpenCore()
        {
            OpenWithGridMatrix();
            OpenRtree(GeoFileReadWriteMode.ReadWrite);
            base.OpenCore();
        }

        private void OpenRtree(GeoFileReadWriteMode rTreeFileAccess)
        {
            if (rTreeIndex == null)
            {
                rTreeIndex = new RtreeSpatialIndex(indexPathFilename, rTreeFileAccess);
            }

            if (requireIndex)
            {
                rTreeIndex.Open();
            }

            if (!rTreeIndex.HasIdx && requireIndex)
            {
                throw new InvalidOperationException("Index file is not exists, please use BuildIndexFile");
            }
        }

        private void OpenWithGridMatrix()
        {
            gridPathfilenameData = File.ReadAllLines(gridPathFilename);
            var firstData = gridPathfilenameData.FirstOrDefault().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var lastData = gridPathfilenameData.LastOrDefault().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            columnsCount = int.Parse(lastData[0]);
            rowsCount = int.Parse(lastData[1]);
            var lowerLeftX = double.Parse(firstData[3]);
            var lowerLeftY = double.Parse(firstData[2]);
            var upperRightX = double.Parse(lastData[3]);
            var upperRightY = double.Parse(lastData[2]);
            gridExtent = new RectangleShape(lowerLeftX, upperRightY, upperRightX, lowerLeftY);
            cellSize = (upperRightY - lowerLeftY) / rowsCount;
            lowerLeftPoint = new PointShape(lowerLeftX, lowerLeftY);
        }

        protected override void CloseCore()
        {
            columnsCount = 0;
            rowsCount = 0;

            base.CloseCore();
        }

        protected override RectangleShape GetBoundingBoxCore()
        {
            ValidatorHelper.CheckFeatureSourceIsOpen(IsOpen);

            return gridExtent;
        }

        protected override int GetCountCore()
        {
            ValidatorHelper.CheckFeatureSourceIsOpen(IsOpen);

            return (columnsCount - 1) * (rowsCount - 1);
        }

        protected override Collection<FeatureSourceColumn> GetColumnsCore()
        {
            ValidatorHelper.CheckFeatureSourceIsOpen(IsOpen);

            if (featureSourceColumns == null)
            {
                featureSourceColumns = new Collection<FeatureSourceColumn>();
                string name = dataValueColumnName;
                int length = 50;
                string typeName = "String";

                FeatureSourceColumn tmp = new FeatureSourceColumn(name, typeName, length);
                featureSourceColumns.Add(tmp);
            }

            return featureSourceColumns;
        }

        public static void BuildIndexFile(string gridPathFilename, BuildIndexMode buildIndexMode)
        {
            ValidatorHelper.CheckStringIsNotNullNorEmpty(gridPathFilename, "gridPathFilename");
            ValidatorHelper.CheckBuildIndexModeIsValid(buildIndexMode, "buildIndexMode");

            var indexPathFilename = PclSystem.Path.ChangeExtension(gridPathFilename, ".idx");
            NldasAsciiGridFeatureSource featureSource = new NldasAsciiGridFeatureSource(gridPathFilename);
            featureSource.RequireIndex = false;

            featureSource.Open();

            var features = featureSource.GetAllFeatures(ReturningColumnsType.NoColumns);

            if (!(PclSystem.File.Exists(indexPathFilename) && PclSystem.File.Exists(PclSystem.Path.ChangeExtension(indexPathFilename, ".ids"))) || buildIndexMode == BuildIndexMode.Rebuild)
            {
                RtreeSpatialIndex rTreeIndex = new RtreeSpatialIndex(indexPathFilename, GeoFileReadWriteMode.ReadWrite);

                WellKnownType featureType = features.First().GetWellKnownType();

                RtreeSpatialIndex.CreateRectangleSpatialIndex(indexPathFilename, RtreeSpatialIndexPageSize.EightKilobytes, RtreeSpatialIndexDataFormat.Float);

                rTreeIndex.Open();

                foreach (Feature feature in features)
                {
                    rTreeIndex.Add(feature);
                }

                rTreeIndex.Flush();
                rTreeIndex.Close();
            }

            featureSource.Close();
        }

        protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
        {
            ValidatorHelper.CheckFeatureSourceIsOpen(IsOpen);
            var allFeatures = new Collection<Feature>();

            for (int i = 0; i < rowsCount - 1; i++)
            {
                for (int j = 0; j < columnsCount - 1; j++)
                {
                    PointShape upperLeftPoint = new PointShape(gridExtent.UpperLeftPoint.X + j * cellSize, gridExtent.UpperLeftPoint.Y - i * cellSize);
                    PointShape lowerRightPoint = new PointShape(upperLeftPoint.X + cellSize, upperLeftPoint.Y - cellSize);
                    var rectangleShape = new RectangleShape(upperLeftPoint, lowerRightPoint);
                    var feature = new Feature(rectangleShape, new Collection<string>() { $"GridNumber:{i + 1},{j + 1}" });
                    feature.Id = $"{i}_{j}";
                    allFeatures.Add(feature);
                }
            }

            return allFeatures;
        }

        protected override Collection<Feature> GetFeaturesInsideBoundingBoxCore(RectangleShape boundingBox, IEnumerable<string> returningColumnNames)
        {
            ValidatorHelper.CheckFeatureSourceIsOpen(IsOpen);
            ValidatorHelper.CheckObjectIsNotNull(boundingBox, "boungingBox");
            ValidatorHelper.CheckShapeIsValidForOperation(boundingBox);

            Collection<Feature> returnValues = new Collection<Feature>();
            if (rTreeIndex.HasIdx)
            {
                Collection<string> ids = rTreeIndex.GetFeatureIdsIntersectingBoundingBox(boundingBox);
                returnValues = GetFeaturesByIdsCore(ids, returningColumnNames);
            }
            else
            {
                returnValues = base.GetFeaturesInsideBoundingBoxCore(boundingBox, returningColumnNames);
            }
            return returnValues;
        }

        protected override Collection<Feature> GetFeaturesByIdsCore(IEnumerable<string> ids, IEnumerable<string> returningColumnNames)
        {
            var featuresByIds = new Collection<Feature>();
            foreach (var id in ids)
            {
                var idData = id.Split('_');
                var rowNumber = int.Parse(idData[0]);
                var colNumber = int.Parse(idData[1]);

                PointShape upperLeftPoint = new PointShape(gridExtent.UpperLeftPoint.X + colNumber * cellSize, gridExtent.UpperLeftPoint.Y - rowNumber * cellSize);
                PointShape lowerRightPoint = new PointShape(upperLeftPoint.X + cellSize, upperLeftPoint.Y - cellSize);
                var rectangleShape = new RectangleShape(upperLeftPoint, lowerRightPoint);
                var feature = new Feature(rectangleShape, new Collection<string>() { $"GridNumber:{rowNumber + 1},{colNumber + 1}" });
                feature.Id = id;

                featuresByIds.Add(feature);
            }

            return featuresByIds;
        }

        protected override Collection<Feature> GetFeaturesForDrawingCore(RectangleShape boundingBox, double screenWidth, double screenHeight, IEnumerable<string> returningColumnNames)
        {
            double resolution = Math.Max(boundingBox.Width / screenWidth, boundingBox.Height / screenHeight);
            double cellSizeInScreen = cellSize / resolution;
            int skipCellCount = Math.Max((int)Math.Floor(1 / cellSizeInScreen), 1);
            bool skiped = skipCellCount > 1;

            Collection<Feature> featuresInsideBoundingBox = new Collection<Feature>();
            if (boundingBox.Intersects(gridExtent))
            {
                double upperLeftX = Math.Max(gridExtent.UpperLeftPoint.X, boundingBox.UpperLeftPoint.X);
                double upperLeftY = Math.Min(gridExtent.UpperLeftPoint.Y, boundingBox.UpperLeftPoint.Y);
                double lowerRightX = Math.Min(gridExtent.LowerRightPoint.X, boundingBox.LowerRightPoint.X);
                double lowerRightY = Math.Max(gridExtent.LowerRightPoint.Y, boundingBox.LowerRightPoint.Y);

                int startColumnIndex = Convert.ToInt32(Math.Floor(Math.Round((upperLeftX - gridExtent.UpperLeftPoint.X) / cellSize, 8)));
                int endColumnIndex = Convert.ToInt32(Math.Ceiling(Math.Round((lowerRightX - gridExtent.UpperLeftPoint.X) / cellSize, 8)));
                int startRowIndex = Convert.ToInt32(Math.Floor(Math.Round((gridExtent.UpperLeftPoint.Y - upperLeftY) / cellSize, 8)));
                int endRowIndex = Convert.ToInt32(Math.Ceiling(Math.Round((gridExtent.UpperLeftPoint.Y - lowerRightY) / cellSize, 8)));

                for (int i = startRowIndex; i < endRowIndex; i += skipCellCount)
                {
                    for (int j = startColumnIndex; j < endColumnIndex; j += skipCellCount)
                    {
                        double x1 = lowerLeftPoint.X + j * cellSize;
                        double y1 = lowerLeftPoint.Y + rowsCount * cellSize - i * cellSize;

                        double realCellSize = skiped ? resolution : cellSize;
                        double x2 = x1 + realCellSize;
                        double y2 = y1 - realCellSize;

                        Feature feature = new Feature(GetWellKnownBinary(x1, y1, x2, y2));
                        feature.Id = $"{i}_{j}";
                        feature.ColumnValues.Add(dataValueColumnName, $"{i + 1},{j + 1}");
                        featuresInsideBoundingBox.Add(feature);
                    }
                }
            }

            return featuresInsideBoundingBox;
        }

        private static byte[] wkbHeader = new byte[] { 1, 3, 0, 0, 0, 1, 0, 0, 0, 5, 0, 0, 0 };
        private byte[] GetWellKnownBinary(double x1, double y1, double x2, double y2)
        {
            byte[] wkb = new byte[93];
            Buffer.BlockCopy(wkbHeader, 0, wkb, 0, 13);
            Buffer.BlockCopy(BitConverter.GetBytes(x1), 0, wkb, 13, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(y1), 0, wkb, 21, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(x2), 0, wkb, 29, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(y1), 0, wkb, 37, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(x2), 0, wkb, 45, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(y2), 0, wkb, 53, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(x1), 0, wkb, 61, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(y2), 0, wkb, 69, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(x1), 0, wkb, 77, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(y1), 0, wkb, 85, 8);
            return wkb;
        }
    }
}
