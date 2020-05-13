using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace WorldMapKitDataExtractor
{
    public class Extractor
    {
        private string inputDataPath;
        private string outputDataPath;
        private string inputDataPrj;
        private string boundaryPrj;
        private bool preserveCountryLevelData;

        public Extractor(string inputDataPath, string outputDataPath, string inputDataPrj = null, string boundaryPrj = null, bool preserveCountryLevelData = true)
        {
            this.inputDataPath = inputDataPath;
            this.outputDataPath = outputDataPath;
            this.inputDataPrj = inputDataPrj;
            this.boundaryPrj = boundaryPrj;
            this.preserveCountryLevelData = preserveCountryLevelData;
        }

        public Action<string> UpdateStatus { get; set; }

        public string InputDataPath
        {
            get { return inputDataPath; }
            set { inputDataPath = value; }
        }

        public string OutputDataPath
        {
            get { return outputDataPath; }
            set { outputDataPath = value; }
        }

        public string InputDataPrj
        {
            get { return inputDataPrj; }
            set { inputDataPrj = value; }
        }

        public string BoundaryPrj
        {
            get { return boundaryPrj; }
            set { boundaryPrj = value; }
        }

        public bool PreserveCountryLevelData
        {
            get { return preserveCountryLevelData; }
            set { preserveCountryLevelData = value; }
        }

        public void ExtractDataByShape(Collection<BoundingBoxWithZoomLevels> boundaryFeatureSet)
        {
            UpdateStatus("Beginning to extract world map kit data by shape file.");

            Collection<BoundingBoxWithZoomLevels> projectedBoundaryFeaturesWithZoomLevels = new Collection<BoundingBoxWithZoomLevels>();
            Collection<RectangleShape> projectedBoundingBoxes = new Collection<RectangleShape>();

            Proj4Projection proj = new Proj4Projection(boundaryPrj, inputDataPrj);
            proj.Open();
            foreach (BoundingBoxWithZoomLevels boundaryFeatureWithZoomLevels in boundaryFeatureSet)
            {
                RectangleShape projectedBoundaryFeature = proj.ConvertToExternalProjection(boundaryFeatureWithZoomLevels.BoundingBox);
                projectedBoundaryFeaturesWithZoomLevels.Add(new BoundingBoxWithZoomLevels(projectedBoundaryFeature, boundaryFeatureWithZoomLevels.StartingZoomLevel, boundaryFeatureWithZoomLevels.EndingZoomLevel));
                projectedBoundingBoxes.Add(projectedBoundaryFeature);
            }
            proj.Close();

            var tableWithBoundingBoxes = GetTablesWithExtents(inputDataPath, projectedBoundaryFeaturesWithZoomLevels);

            SQLiteConnection.CreateFile(outputDataPath);
            CopyTables(tableWithBoundingBoxes, inputDataPath, outputDataPath);

            SQLiteConnection targetDatabaseConnection = new SQLiteConnection($"Data Source={outputDataPath};Version=3;");
            targetDatabaseConnection.Open();
            ProcessBaselandByShapes(projectedBoundingBoxes, targetDatabaseConnection);

            UpdateStatus("Executing vacuum for output database.");
            ExecuteNonQueryCommand("VACUUM;", targetDatabaseConnection);

            targetDatabaseConnection.Close();
            targetDatabaseConnection.Dispose();
            UpdateStatus("Done extracting world map kit data!");
        }

        private void CopyTables(Dictionary<string, Collection<RectangleShape>> tableWithExtents, string sourceDataPath, string outputDataPath)
        {
            Proj4Projection proj4 = new Proj4Projection(inputDataPrj, inputDataPrj);
            GeographyUnit geographyUnit = proj4.GetInternalGeographyUnit();

            string sourceDatabaseConnectionString = $"Data Source={sourceDataPath};Version=3;";
            string targetDatabaseConnectionString = $@"Data Source={outputDataPath};Version=3;";

            var tablesWithFeatuerCount = GetTablesWithFeatureCount(tableWithExtents, inputDataPath);
            long totalCount = tablesWithFeatuerCount.Values.Sum();

            long index = 0;
            DateTime beginTime = DateTime.Now;

            var tables = tableWithExtents.Keys;
            foreach (var table in tables)
            {
                SqliteFeatureSource source = new SqliteFeatureSource(sourceDatabaseConnectionString, table);
                source.Open();

                Collection<SqliteColumn> sqliteColumns = new Collection<SqliteColumn>();
                var featureColumns = source.GetColumns();
                foreach (var featureColumn in featureColumns)
                {
                    if (featureColumn.ColumnName.Equals("geometry") || featureColumn.ColumnName.Equals("id"))
                        continue;

                    SqliteColumn sqliteColumn = new SqliteColumn();
                    sqliteColumn.ColumnName = featureColumn.ColumnName;
                    SqliteColumnType sqliteColumnType;
                    if (!Enum.TryParse(featureColumn.TypeName, true, out sqliteColumnType))
                    {
                        switch (featureColumn.TypeName)
                        {
                            case "bigint":
                            case "INT":
                                sqliteColumn.ColumnType = SqliteColumnType.Integer;
                                break;
                            case "double precision":
                                sqliteColumn.ColumnType = SqliteColumnType.Real;
                                break;
                            case "character varying":
                            case "numeric":
                                sqliteColumn.ColumnType = SqliteColumnType.Text;
                                break;
                        }
                    }
                    else
                        sqliteColumn.ColumnType = (SqliteColumnType)Enum.Parse(typeof(SqliteColumnType), featureColumn.TypeName, true);

                    sqliteColumns.Add(sqliteColumn);
                }

                SqliteFeatureSource.CreateTable(targetDatabaseConnectionString, table, sqliteColumns, geographyUnit);
                SqliteFeatureSource tempSource = new SqliteFeatureSource(targetDatabaseConnectionString, table);
                tempSource.Open();
                tempSource.BeginTransaction();

                int featureCount = 0;
                HashSet<string> allFeatureIdsInOneTable = new HashSet<string>();
                Collection<RectangleShape> boundingBoxes = tableWithExtents[table];
                foreach (RectangleShape boundingBox in boundingBoxes)
                {
                    Collection<RectangleShape> subBoundingBoxes = new Collection<RectangleShape>() { boundingBox };
                    if (tablesWithFeatuerCount[table] > 700000) subBoundingBoxes = CutBoundingBoxByDegrees(boundingBox);

                    bool updated = false;
                    foreach (RectangleShape subBoundingBox in subBoundingBoxes)
                    {
                        Collection<Feature> allFeaturesInOneTable = source.GetFeaturesInsideBoundingBox(subBoundingBox, ReturningColumnsType.AllColumns);
                        foreach (var feature in allFeaturesInOneTable.Where(x => !allFeatureIdsInOneTable.Contains(x.Id)))
                        {
                            ShapeValidationResult validationResult = feature.GetShape().Validate(ShapeValidationMode.Simple);
                            if (validationResult.IsValid && feature.GetShape().Intersects(subBoundingBox))
                            {
                                tempSource.AddFeature(feature);
                                featureCount++;
                            }

                            allFeatureIdsInOneTable.Add(feature.Id);
                            index++;
                            updated = true;
                        }

                        if (featureCount >= 10000)
                        {
                            tempSource.CommitTransaction();
                            featureCount = 0;
                            tempSource.BeginTransaction();
                        }

                        if (updated)
                        {
                            var elapsedTime = DateTime.Now.Subtract(beginTime);
                            var remainingTime = TimeSpan.FromSeconds(((double)elapsedTime.TotalSeconds / index) * (totalCount - index));
                            UpdateStatus($"Committed {index}/{totalCount} features, elapsed {elapsedTime.ToString(@"hh\:mm\:ss")}, remaining {remainingTime.ToString(@"hh\:mm\:ss")}");
                        }
                    }
                }

                tempSource.CommitTransaction();
                tempSource.Close();
            }
        }

        private Dictionary<string, long> GetTablesWithFeatureCount(Dictionary<string, Collection<RectangleShape>> tablesWithBoundingShapes, string sqliteDbPath)
        {
            SQLiteConnection sourceDatabaseConnection = new SQLiteConnection($"Data Source={sqliteDbPath};Version=3;");
            sourceDatabaseConnection.Open();
            Dictionary<string, long> totalCountPerLayers = new Dictionary<string, long>();

            int index = 0;
            foreach (var layerWithBoundingShapes in tablesWithBoundingShapes)
            {
                var tableName = layerWithBoundingShapes.Key;
                Collection<RectangleShape> boundingBoxes = layerWithBoundingShapes.Value;
                foreach (var boundingBox in boundingBoxes)
                {
                    string queryCommand = $"SELECT  COUNT(Id) AS COUNT FROM idx_{tableName}_geometry WHERE minx < { boundingBox.LowerRightPoint.X} AND maxx > {boundingBox.LowerLeftPoint.X} AND miny < {boundingBox.UpperLeftPoint.Y} AND maxy > {boundingBox.LowerLeftPoint.Y}";
                    SQLiteCommand command = new SQLiteCommand(queryCommand, sourceDatabaseConnection);
                    var count = command.ExecuteScalar();
                    if (count != null)
                        if (totalCountPerLayers.ContainsKey(tableName))
                            totalCountPerLayers[tableName] = totalCountPerLayers[tableName] + (long)count;
                        else
                            totalCountPerLayers.Add(tableName, (long)count);

                    command.Dispose();
                }
                index++;
                UpdateStatus($"{totalCountPerLayers.Values.Sum()} features need to extract, {index} / {tablesWithBoundingShapes.Keys.Count} tables has calculated.");
            }
            sourceDatabaseConnection.Close();

            return totalCountPerLayers;
        }

        private void ProcessBaselandByShapes(Collection<RectangleShape> boundingBoxes, SQLiteConnection connection)
        {
            UpdateStatus("Processing baseland by shapes.");
            if (!preserveCountryLevelData)
                return;

            //remove full world bounding box from boundary features
            boundingBoxes.RemoveAt(0);

            SqliteFeatureSource baselandFeatureSource = new SqliteFeatureSource(connection.ConnectionString, "osm_baseland_polygon", "id", "geometry");
            SqliteFeatureSource baseland1mFeatureSource = new SqliteFeatureSource(connection.ConnectionString, "osm_baseland1m_polygon", "id", "geometry");

            baselandFeatureSource.Open();
            baseland1mFeatureSource.Open();

            UpdateStatus("Querying baseland features from osm_baseland_polygon table.");
            List<Feature> baselandFeatures = new List<Feature>();
            foreach (var boundingBox in boundingBoxes)
            {
                Collection<Feature> features = baselandFeatureSource.SpatialQuery(boundingBox, QueryType.Intersects, ReturningColumnsType.NoColumns);
                baselandFeatures.AddRange(features);
            }

            UpdateStatus("Querying baseland1m features from osm_baseland1m_polygon table.");
            Collection<Feature> baseland1mFeatures = baseland1mFeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns);

            if (baselandFeatures.Count == 0 || baseland1mFeatures.Count == 0)
            {
                UpdateStatus("Cannot process baseland by shapes - one or more tables is empty");
                return;
            }

            Collection<Feature> newBaselandFeatures = new Collection<Feature>();

            int counter = 0;
            UpdateStatus($"Getting <1000 intersection features for baseland feature.");
            foreach (Feature feature in baselandFeatures)
            {
                Feature validFeature = SqlTypesGeometryHelper.MakeValid(feature);
                Feature intersectionFeature = GetInterSection(validFeature, boundingBoxes);
                if (!(intersectionFeature == null))
                {
                    newBaselandFeatures.Add(intersectionFeature);
                    counter++;
                    if (counter % 1000 == 0)
                    {
                        UpdateStatus($"Getting {counter} intersection features for baseland feature.");
                    }
                }
            }

            counter = 0;
            UpdateStatus($"Getting <1000 difference features for baseland1m feature.");
            foreach (Feature feature in baseland1mFeatures)
            {
                if (IsDisjointed(feature, boundingBoxes))
                {
                    newBaselandFeatures.Add(feature);
                    counter++;
                }
                else
                {
                    Feature validFeature = SqlTypesGeometryHelper.MakeValid(feature);
                    Feature difference = GetDifference(validFeature, boundingBoxes);

                    if (difference != null)
                    {
                        newBaselandFeatures.Add(difference);
                        counter++;
                    }
                }
                if (counter != 0 && counter % 1000 == 0)
                {
                    UpdateStatus($"Getting {counter} difference features for baseland1m feature.");
                }
            }

            baselandFeatureSource.BeginTransaction();
            UpdateStatus("Deleting table osm_baseland_polygon.");
            baselandFeatureSource.ExecuteNonQuery("DELETE FROM osm_baseland_polygon;");
            UpdateStatus("Deleting table idx_osm_baseland_polygon_geometry.");
            baselandFeatureSource.ExecuteNonQuery("DELETE FROM idx_osm_baseland_polygon_geometry;");

            counter = 0;
            UpdateStatus($"Adding <1000 new features.");
            foreach (Feature f in newBaselandFeatures)
            {
                Feature feature = new Feature(f.GetWellKnownBinary());
                baselandFeatureSource.AddFeature(feature);
                counter++;
                if (counter % 1000 == 0)
                {
                    UpdateStatus($"Adding {counter} new features.");
                }
            }
            UpdateStatus("Commiting sqlite database transaction.");
            baselandFeatureSource.CommitTransaction();
        }

        private void ExecuteNonQueryCommand(string commandText, SQLiteConnection connection)
        {
            SQLiteCommand command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }
        
        private Collection<RectangleShape> CutBoundingBoxByDegrees(RectangleShape boundingBox)
        {
            Collection<RectangleShape> boundingBoxes = new Collection<RectangleShape>();
            Proj4Projection projection = new Proj4Projection(4326, inputDataPrj);
            projection.Open();
            int OneDegreeInExternalProjection = (int)projection.ConvertToExternalProjection(new RectangleShape(0, 1, 1, 0)).Height;

            for (int w = 0; w < boundingBox.Width; w += OneDegreeInExternalProjection)
            {
                double minX = boundingBox.LowerLeftPoint.X + w;
                double maxX = Math.Min(minX + OneDegreeInExternalProjection, boundingBox.LowerRightPoint.X);

                for (int h = 0; h < boundingBox.Height; h += OneDegreeInExternalProjection)
                {
                    double minY = boundingBox.LowerLeftPoint.Y + h;
                    double maxY = Math.Min(minY + OneDegreeInExternalProjection, boundingBox.UpperLeftPoint.Y);

                    boundingBoxes.Add(new RectangleShape(minX, maxY, maxX, minY));
                }
            }

            return boundingBoxes;
        }

        private Dictionary<string, Collection<RectangleShape>> GetTablesWithExtents(string databasePath, Collection<BoundingBoxWithZoomLevels> boundaryFeatures)
        {
            Dictionary<string, Collection<RectangleShape>> tablesWithBoundingBoxes = new Dictionary<string, Collection<RectangleShape>>();

            //this is the set of layers we use to determine which tables to grab for each bounding area
            string connectionString = $"Data Source={databasePath};Version=3;";
            WorldStreetsLayer wmkLayer = new WorldStreetsLayer(connectionString);
            wmkLayer.Open();
            var zoomLevelWithTables = GetZoomLevelWithTables(wmkLayer);
            foreach (BoundingBoxWithZoomLevels boundingBoxParameters in boundaryFeatures)
            {
                for (int zoomLevel = boundingBoxParameters.StartingZoomLevel; zoomLevel <= boundingBoxParameters.EndingZoomLevel; zoomLevel++)
                {
                    var tables = zoomLevelWithTables[zoomLevel];
                    foreach (var table in tables)
                    {
                        if (!tablesWithBoundingBoxes.Keys.Contains(table))
                            tablesWithBoundingBoxes.Add(table, new Collection<RectangleShape>());
                    }
                }

                foreach (string table in tablesWithBoundingBoxes.Keys)
                {
                    bool exist = false;
                    foreach (var boundingBox in tablesWithBoundingBoxes[table])
                    {
                        if (boundingBox.IsTopologicallyEqual(boundingBoxParameters.BoundingBox))
                            exist = true;
                    }

                    if (!exist)
                        tablesWithBoundingBoxes[table].Add(boundingBoxParameters.BoundingBox);
                }
            }

            return tablesWithBoundingBoxes;
        }

        private Dictionary<int, Collection<string>> GetZoomLevelWithTables(WorldStreetsLayer worldSteetsLayer)
        {
            Dictionary<int, Collection<string>> zoomLevelWithTables = new Dictionary<int, Collection<string>>();

            for (int i = 1; i <= 20; i++)
                zoomLevelWithTables.Add(i, new Collection<string>());

            foreach (SqliteFeatureLayer layer in worldSteetsLayer.Layers)
            {
                Collection<ZoomLevel> layerZoomLevels = layer.ZoomLevelSet.GetZoomLevels();

                for (int i = 1; i <= 20; i++)
                {
                    ZoomLevel zoomLevel = layerZoomLevels[i - 1];
                    if (zoomLevel.CustomStyles.Count != 0 || !IsDefaultStyleSet(zoomLevel))
                    {
                        if ((int)zoomLevel.ApplyUntilZoomLevel == 0)
                        {
                            if (!zoomLevelWithTables[i].Contains(layer.TableName))
                                zoomLevelWithTables[i].Add(layer.TableName);
                        }
                        else
                        {
                            for (int j = i; j <= (int)zoomLevel.ApplyUntilZoomLevel; j++)
                            {
                                if (!zoomLevelWithTables[j].Contains(layer.TableName))
                                    zoomLevelWithTables[j].Add(layer.TableName);
                            }
                        }
                    }
                }
            }

            return zoomLevelWithTables;
        }

        private bool IsDefaultStyleSet(ZoomLevel zoomLevel)
        {
            return IsDefaultAreaStyle(zoomLevel.DefaultAreaStyle) && IsDefaultLineStyle(zoomLevel.DefaultLineStyle) && IsDefaultPointStyle(zoomLevel.DefaultPointStyle) && IsDefaultTextStyle(zoomLevel.DefaultTextStyle);
        }

        private bool IsDefaultAreaStyle(AreaStyle style)
        {
            if (!style.IsActive)
            {
                return true;
            }

            bool isDefault = false;

            if (style.OutlinePen.Color.IsTransparent && style.FillSolidBrush.Color.IsTransparent && style.OutlinePen.Brush is GeoSolidBrush)
            {
                isDefault = true;
            }

            if (isDefault && style.Advanced.FillCustomBrush != null)
            {
                isDefault = false;
            }

            if (isDefault && style.CustomAreaStyles.Count != 0)
            {
                isDefault = false;
            }

            return isDefault;
        }

        private bool IsDefaultLineStyle(LineStyle style)
        {
            if (!style.IsActive)
            {
                return true;
            }

            bool isDefault = false;

            if (style.OuterPen.Color.IsTransparent && style.InnerPen.Color.IsTransparent && style.CenterPen.Color.IsTransparent)
            {
                if (style.OuterPen.Brush is GeoSolidBrush && style.InnerPen.Brush is GeoSolidBrush && style.CenterPen.Brush is GeoSolidBrush)
                {
                    isDefault = true;
                }
            }

            if (style.CustomLineStyles.Count > 0)
            {
                isDefault = false;
            }

            return isDefault;
        }

        private bool IsDefaultPointStyle(PointStyle style)
        {
            if (!style.IsActive)
            {
                return true;
            }

            switch (style.PointType)
            {
                case PointType.Symbol:
                    bool isDefault = false;
                    if (style.SymbolPen.Color.IsTransparent && style.SymbolSolidBrush.Color.IsTransparent && style.SymbolPen.Brush is GeoSolidBrush)
                    {
                        isDefault = true;
                    }

                    if (isDefault && style.Advanced.CustomBrush != null)
                    {
                        isDefault = false;
                    }

                    return isDefault && style.CustomPointStyles.Count == 0;

                case PointType.Bitmap:
                    return style.Image == null && style.CustomPointStyles.Count == 0;

                case PointType.Character:
                    return style.CharacterSolidBrush.Color.IsTransparent && style.CustomPointStyles.Count == 0;
                default:
                    return true;
            }
        }

        private bool IsDefaultTextStyle(TextStyle style)
        {
            if (!style.IsActive)
            {
                return true;
            }

            bool isDefault = false;

            if (style.TextSolidBrush.Color.IsTransparent)
            {
                isDefault = true;
            }

            if (isDefault && style.Advanced.TextCustomBrush != null)
            {
                isDefault = false;
            }

            return isDefault;
        }

        private Feature GetInterSection(Feature feature, Collection<RectangleShape> boundingBoxes)
        {
            Feature result = null;

            foreach (var boundingBox in boundingBoxes)
            {
                var intersection = feature.GetIntersection(boundingBox.GetFeature());
                if (result == null)
                {
                    result = intersection;
                }
                else if (intersection != null)
                {
                    result = result.Union(intersection);
                }
            }

            return result;
        }

        private bool IsDisjointed(Feature feature, Collection<RectangleShape> boundingBoxes)
        {
            bool result = true;
            foreach (var boundingBox in boundingBoxes)
            {
                result = result && feature.IsDisjointed(boundingBox.GetFeature());
            }
            return result;
        }

        private Feature GetDifference(Feature feature, Collection<RectangleShape> boundingBoxes)
        {
            Feature result = null;
            foreach (var boundingBox in boundingBoxes)
            {
                if (result == null)
                {
                    result = SqlTypesGeometryHelper.GetDifference(feature, boundingBox.GetFeature());
                }
                else
                {
                    result = SqlTypesGeometryHelper.GetDifference(result, boundingBox.GetFeature());
                }
            }

            return result;
        }
    }
}
