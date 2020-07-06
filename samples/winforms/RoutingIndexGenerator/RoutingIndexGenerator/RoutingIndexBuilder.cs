using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Text;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;

namespace RoutingIndexGenerator
{
    public class RoutingIndexBuilder
    {
        public event EventHandler<RoutingIndexBuilderEventArgs> BuildingShapeFileIndex;
        public event EventHandler<RoutingIndexBuilderEventArgs> ExtractingSqliteDatabase;
        public event EventHandler<RoutingIndexBuilderEventArgs> BuildingRoutableSegment;
        public event EventHandler<RoutingIndexBuilderEventArgs> BuildingRouteRTSegment;
        public event EventHandler<EventArgs> BuildingIndexFinished;

        private bool cancelBuilding;
        private int totalRecordCount;
        private int processedRecordCount;
        private Feature previousFeature;
        private Collection<string> requiredColumns;
        private FeatureSource routableFeatureSource;
        private RoutingIndexBuilderParameters buildRtgParameter;

        public RoutingIndexBuilder()
            : this(new RoutingIndexBuilderParameters())
        { }

        public RoutingIndexBuilder(RoutingIndexBuilderParameters buildRtgParameter)
        {
            this.buildRtgParameter = buildRtgParameter;
            this.buildRtgParameter.ClosedRoadOption.InitRegexes();
            this.buildRtgParameter.OneWayRoadOption.InitRegexes();
            RtgRoutingSource.GeneratingRoutableShapeFile += RtgRoutingSource_GeneratingRoutableShapeFile;
            RtgRoutingSource.BuildingRoutingData += RtgRoutingSource_BuildingRoutingData;
        }

        public void RemoveEvents()
        {
            RtgRoutingSource.GeneratingRoutableShapeFile -= RtgRoutingSource_GeneratingRoutableShapeFile;
            RtgRoutingSource.BuildingRoutingData -= RtgRoutingSource_BuildingRoutingData;
        }

        //This method uses lower memory than the other method, but only works with .sqlite files
        public void ExtractRoadsFromSourceFileToTempFileWithLowerMemory(ConfigModel config)
        {
            // If don't override routable file, let's skip this step.
            if (buildRtgParameter.OverrideRoutableFile || !File.Exists(buildRtgParameter.RoutableFilePathName))
            {
                FeatureSource source = null;

                if (config.FileType.Contains("shp"))
                    source = new ShapeFileFeatureSource(buildRtgParameter.SourceSqlitePathName);
                else
                {
                    SQLiteConnection sourceSQLiteConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", buildRtgParameter.SourceSqlitePathName));
                    source = new SqliteFeatureSource(sourceSQLiteConnection, config.TableName, config.FeatureIdColumn, config.GeometryColumnName);
                    ((SqliteFeatureSource)source).ConnectionString = sourceSQLiteConnection.ConnectionString;
                    sourceSQLiteConnection.Open();
                }

                source.Open();
                RectangleShape restrictExtent = buildRtgParameter.RestrictExtent == null ? source.GetBoundingBox() : buildRtgParameter.RestrictExtent;
                Collection<string> ids = new Collection<string>();
                ids = source.GetFeatureIdsInsideBoundingBox(restrictExtent);

                FeatureSource targetFeatureSource;

                if (buildRtgParameter.RoutableFileType == RoutableFileType.ShapeFile)
                {
                    Collection<DbfColumn> dbfColumns = new Collection<DbfColumn>();
                    Collection<FeatureSourceColumn> sourceColumns = source.GetColumns();
                    foreach (var column in sourceColumns)
                    {
                        // Create the DBFColumns based on different cases in SQLite source
                        DbfColumnType dbfColumnType = DbfColumnType.Null;
                        int columnLength = 0;
                        switch (column.TypeName.ToUpper())
                        {
                            case "INTEGER":
                                dbfColumnType = DbfColumnType.Numeric;
                                columnLength = 10;
                                break;
                            case "BLOB":
                                continue;
                            case "TEXT":
                                dbfColumnType = DbfColumnType.Character;
                                columnLength = 100;
                                break;
                            case "BIGINT":
                                dbfColumnType = DbfColumnType.Numeric;
                                columnLength = 20;
                                break;
                            default:
                                break;

                        }

                        dbfColumns.Add(new DbfColumn(column.ColumnName, dbfColumnType, columnLength, 0));
                    }
                    ShapeFileFeatureSource.CreateShapeFile(ShapeFileType.Polyline, buildRtgParameter.SourceSegmentsFilePath, dbfColumns, Encoding.UTF8, OverwriteMode.Overwrite);
                    targetFeatureSource = new ShapeFileFeatureSource(buildRtgParameter.SourceSegmentsFilePath, GeoFileReadWriteMode.ReadWrite);
                }
                else
                {
                    Collection<SqliteColumn> sqliteColumns = new Collection<SqliteColumn>();
                    foreach (string columnName in config.ExtractRequiredColumns)
                    {
                        SqliteColumn sqliteColumn = new SqliteColumn(columnName, SqliteColumnType.Text);
                        sqliteColumns.Add(sqliteColumn);
                    }

                    SqliteFeatureSource.CreateDatabase(buildRtgParameter.SourceSegmentsFilePath);
                    string targetSQLiteConnectionString = string.Format("Data Source={0};Version=3;", buildRtgParameter.SourceSegmentsFilePath);
                    SqliteFeatureSource.CreateTable(targetSQLiteConnectionString, "RouteSegments", sqliteColumns, buildRtgParameter.GeographyUnit);
                    targetFeatureSource = new SqliteFeatureSource(targetSQLiteConnectionString, "RouteSegments");
                }

                targetFeatureSource.Open();

                int processedRecordCount = 0;
                int totalRecordCount = ids.Count;
                targetFeatureSource.BeginTransaction();

                //load 10,000 features into memory at a time from the sqlite database
                int loadingFeatureCount = 10000;
                int startIndex = 0;
                int length = 0;

                string[] totalIds = new string[ids.Count];
                ids.CopyTo(totalIds, 0);

                while (startIndex + length < ids.Count)
                {
                    startIndex = startIndex + length;
                    length = Math.Min(ids.Count - startIndex, loadingFeatureCount);

                    string[] fetchIds = new string[length];
                    Array.Copy(totalIds, startIndex, fetchIds, 0, length);

                    Collection<Feature> features = source.GetFeaturesByIds(fetchIds, config.ExtractRequiredColumns);

                    foreach (Feature feature in features)
                    {
                        targetFeatureSource.AddFeature(feature);
                        processedRecordCount++;

                        //write 100,000 features to shapefile at a time
                        if (processedRecordCount % 100000 == 0)
                        {
                            targetFeatureSource.CommitTransaction();
                            targetFeatureSource.BeginTransaction();
                        }
                        OnExtractingSqliteDatabase(new RoutingIndexBuilderEventArgs(totalRecordCount, processedRecordCount));
                    }

                }
                targetFeatureSource.CommitTransaction();
                targetFeatureSource.Close();
                source.Close();
            }
        }

        public void StartBuildingRoutableFile()
        {
            if (buildRtgParameter.OverrideRoutableFile)
            {
                // reset the process status.
                this.processedRecordCount = 0;
                FeatureSource source;
                if (buildRtgParameter.RoutableFileType == RoutableFileType.ShapeFile)
                {
                    source = new ShapeFileFeatureSource(buildRtgParameter.SourceSegmentsFilePath);
                    source.Open();
                    this.totalRecordCount = source.GetCount();
                    RtgRoutingSource.GenerateRoutableShapeFile(buildRtgParameter.SourceSegmentsFilePath, buildRtgParameter.RoutableFilePathName, buildRtgParameter.OverrideRoutableFile ? OverwriteMode.Overwrite : OverwriteMode.DoNotOverwrite, buildRtgParameter.GeographyUnit, 2);
                }
                else
                {
                    string sourceSQLiteConnectionString = string.Format("Data Source={0};Version=3;", buildRtgParameter.SourceSegmentsFilePath);
                    source = new SqliteFeatureSource(sourceSQLiteConnectionString, "RouteSegments");
                    source.Open();
                    this.totalRecordCount = source.GetCount();
                    RtgRoutingSource.GenerateRoutableSQLiteFile(buildRtgParameter.SourceSegmentsFilePath, buildRtgParameter.RoutableFilePathName, buildRtgParameter.OverrideRoutableFile ? OverwriteMode.Overwrite : OverwriteMode.DoNotOverwrite, buildRtgParameter.GeographyUnit, 10);
                }
                source.Close();
            }
        }

        public void StartBuildingRtg()
        {
            // reset the process status.
            this.processedRecordCount = 0;
            if (buildRtgParameter.RoutableFileType == RoutableFileType.ShapeFile)
            {
                routableFeatureSource = new ShapeFileFeatureSource(buildRtgParameter.RoutableFilePathName);
                routableFeatureSource.Open();
                this.totalRecordCount = routableFeatureSource.GetCount();
                RtgRoutingSource.GenerateRoutingData(buildRtgParameter.RtgFilePathName, buildRtgParameter.RoutableFilePathName, buildRtgParameter.BuildRoutingDataMode, buildRtgParameter.GeographyUnit, DistanceUnit.Meter);
            }
            else
            {
                string routableSQLiteConnectionString = string.Format("Data Source={0};Version=3;", buildRtgParameter.RoutableFilePathName);
                routableFeatureSource = new SqliteFeatureSource(routableSQLiteConnectionString, "RouteSegments");
                routableFeatureSource.Open();
                this.totalRecordCount = routableFeatureSource.GetCount();
                RtgRoutingSource.GenerateRoutingData(buildRtgParameter.RtgFilePathName, buildRtgParameter.RoutableFilePathName, buildRtgParameter.BuildRoutingDataMode, buildRtgParameter.GeographyUnit, DistanceUnit.Meter, 10);
            }

            routableFeatureSource.Close();

            if (cancelBuilding == true)
            {
                File.Delete(Path.ChangeExtension(buildRtgParameter.RtgFilePathName, ".rtg"));
                File.Delete(Path.ChangeExtension(buildRtgParameter.RtgFilePathName, ".rtx"));
            }

            // clear up the temporary shape files extracted from sqlite.
            if (File.Exists(buildRtgParameter.SourceSqlitePathName) && File.Exists(buildRtgParameter.SourceSegmentsFilePath))
            {
                string tempShapefile = Path.GetFileNameWithoutExtension(buildRtgParameter.SourceSegmentsFilePath);
                string[] files = Directory.GetFiles(Path.GetDirectoryName(buildRtgParameter.SourceSegmentsFilePath));
                foreach (string file in files)
                {
                    if (Path.GetFileNameWithoutExtension(file).Equals(tempShapefile, StringComparison.InvariantCultureIgnoreCase))
                    {
                        File.Delete(file);
                    }
                }
            }
            OnBuildingIndexFinished(new EventArgs());
        }

        public void CancelBuildingRtgFile()
        {
            cancelBuilding = true;
        }

        private void RtgRoutingSource_GeneratingRoutableShapeFile(object sender, GeneratingRoutableShapeFileRoutingSourceEventArgs e)
        {
            if (previousFeature == null)
            {
                previousFeature = e.PreviousFeature;
            }

            // we add the previous feature is because this event may be triggered multi times in one feature.
            if (previousFeature.Id != e.PreviousFeature.Id)
            {
                previousFeature = e.PreviousFeature;
                this.processedRecordCount++;
                OnBuildingRoutableSegment(new RoutingIndexBuilderEventArgs(this.totalRecordCount, this.processedRecordCount));
            }
        }

        private void RtgRoutingSource_BuildingRoutingData(object sender, BuildingRoutingDataRtgRoutingSourceEventArgs e)
        {
            // Make progressBar move forward a step
            this.processedRecordCount++;
            OnBuildingRouteRTSegment(new RoutingIndexBuilderEventArgs(this.totalRecordCount, this.processedRecordCount));

            // Get the processing Feature
            if (requiredColumns == null)
            {
                requiredColumns = GetRequiredColumns();
            }

            routableFeatureSource.Open();
            Feature feature = routableFeatureSource.GetFeatureById(e.RouteSegment.FeatureId, requiredColumns);
            if (buildRtgParameter.IncludeOnewayOptions)
            {
                ProcessOneWayRoad(e, feature, requiredColumns);
            }

            if (buildRtgParameter.RouteIndexType == RoutingIndexType.Fastest)
            {
                if (buildRtgParameter.SpeedOption.SpeedSourceType == RoadSpeedSourceType.BasedOnRoadType && !string.IsNullOrEmpty(buildRtgParameter.SpeedOption.RoadTypeColumnName))
                {
                    string featureRoadClassValue = feature.ColumnValues[buildRtgParameter.SpeedOption.RoadTypeColumnName];
                    e.RouteSegment.Weight = GetDistance(e.RouteSegment.Distance) / GetSpeed(featureRoadClassValue);
                }
                else if (buildRtgParameter.SpeedOption.SpeedSourceType == RoadSpeedSourceType.DefinedInAttribution && !string.IsNullOrEmpty(buildRtgParameter.SpeedOption.SpeedColumnName))
                {
                    string speedString = feature.ColumnValues[buildRtgParameter.SpeedOption.SpeedColumnName];
                    float speed = float.TryParse(speedString, out speed) ? speed : buildRtgParameter.SpeedOption.DefaultSpeed;
                    e.RouteSegment.Weight = GetDistance(e.RouteSegment.Distance) / speed;
                }
                else
                {
                    e.RouteSegment.Weight = GetDistance(e.RouteSegment.Distance) / buildRtgParameter.SpeedOption.DefaultSpeed;
                }
            }

            e.Cancel = cancelBuilding;
        }

        private Collection<string> GetRequiredColumns()
        {
            Collection<string> requiredColumns = new Collection<string>();
            if (buildRtgParameter.IncludeOnewayOptions)
            {
                if (!string.IsNullOrEmpty(buildRtgParameter.OneWayRoadOption.OneWayColumnName))
                {
                    requiredColumns.Add(buildRtgParameter.OneWayRoadOption.OneWayColumnName);
                }
                if (!string.IsNullOrEmpty(buildRtgParameter.OneWayRoadOption.IndicatorColumnName))
                {
                    requiredColumns.Add(buildRtgParameter.OneWayRoadOption.IndicatorColumnName);
                }
            }

            string speedColumn = string.Empty;
            switch (buildRtgParameter.SpeedOption.SpeedSourceType)
            {
                case RoadSpeedSourceType.BasedOnRoadType:
                    speedColumn = buildRtgParameter.SpeedOption.RoadTypeColumnName;
                    break;
                case RoadSpeedSourceType.DefinedInAttribution:
                    speedColumn = buildRtgParameter.SpeedOption.SpeedColumnName;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(speedColumn))
            {
                requiredColumns.Add(speedColumn);
            }

            return requiredColumns;
        }

        private void ProcessOneWayRoad(BuildingRoutingDataRtgRoutingSourceEventArgs e, Feature roadFeature, Collection<string> oneWayColumns)
        {
            LineShape lineShape = GetLineShape(roadFeature);

            // handled feature is one-way road
            if (buildRtgParameter.OneWayRoadOption.MatchOneWayIdentifier(roadFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.OneWayColumnName]))
            {
                if (buildRtgParameter.OneWayRoadOption.MatchStartToEnd(roadFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.IndicatorColumnName]))
                {
                    e.RouteSegment.StartPointAdjacentIds.Clear();
                }
                else if (buildRtgParameter.OneWayRoadOption.MatchEndToStart(roadFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.IndicatorColumnName]))
                {
                    e.RouteSegment.EndPointAdjacentIds.Clear();
                }
                else if (buildRtgParameter.ClosedRoadOption.MatchClosedRoadValue(roadFeature.ColumnValues[buildRtgParameter.ClosedRoadOption.ClosedColumnName]))
                {
                    e.RouteSegment.StartPointAdjacentIds.Clear();
                    e.RouteSegment.EndPointAdjacentIds.Clear();
                }
            }

            // analysis adjacent one-way road features
            Collection<string> removedStartPointAdjacentIds = GetRemovedAdjacentIds(e.RouteSegment.StartPointAdjacentIds, new PointShape(lineShape.Vertices[0]), oneWayColumns);
            foreach (string id in removedStartPointAdjacentIds)
            {
                e.RouteSegment.StartPointAdjacentIds.Remove(id);
            }
            Collection<string> removedEndPointAdjacentIds = GetRemovedAdjacentIds(e.RouteSegment.EndPointAdjacentIds, new PointShape(lineShape.Vertices[lineShape.Vertices.Count - 1]), oneWayColumns);
            foreach (string id in removedEndPointAdjacentIds)
            {
                e.RouteSegment.EndPointAdjacentIds.Remove(id);
            }
        }

        private Collection<string> GetRemovedAdjacentIds(Collection<string> adjacentIds, PointShape intersectingPoint, Collection<string> oneWayColumns)
        {
            Collection<string> removedIds = new Collection<string>();
            Collection<Feature> adjacentFeatures = routableFeatureSource.GetFeaturesByIds(adjacentIds, oneWayColumns);

            foreach (Feature adjacentFeature in adjacentFeatures)
            {
                //Feature adjacentFeature = routableSQliteFeatureSource.GetFeatureById(id, oneWayColumns);
                if (buildRtgParameter.OneWayRoadOption.MatchOneWayIdentifier(adjacentFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.OneWayColumnName]))
                //if (adjacentFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.OneWayColumnName].Equals(buildRtgParameter.OneWayRoadOption.OneWayIdentifier, StringComparison.InvariantCultureIgnoreCase))
                {
                    LineShape adjacentLineShape = GetLineShape(adjacentFeature);
                    double distanceFromAdjacentStartToIntersecting = new PointShape(adjacentLineShape.Vertices[0]).GetDistanceTo(intersectingPoint, buildRtgParameter.GeographyUnit, DistanceUnit.Meter);
                    double distanceFromAdjacentEndToIntersecting = new PointShape(adjacentLineShape.Vertices[adjacentLineShape.Vertices.Count - 1]).GetDistanceTo(intersectingPoint, buildRtgParameter.GeographyUnit, DistanceUnit.Meter);
                    if (distanceFromAdjacentStartToIntersecting <= distanceFromAdjacentEndToIntersecting && buildRtgParameter.OneWayRoadOption.MatchEndToStart(adjacentFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.IndicatorColumnName]))
                    //if (distanceFromAdjacentStartToIntersecting <= distanceFromAdjacentEndToIntersecting && adjacentFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.IndicatorColumnName].Equals(buildRtgParameter.OneWayRoadOption.EndToStart))
                    {
                        removedIds.Add(adjacentFeature.Id);
                    }
                    else if (distanceFromAdjacentEndToIntersecting <= distanceFromAdjacentStartToIntersecting
                        && buildRtgParameter.OneWayRoadOption.MatchStartToEnd(adjacentFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.IndicatorColumnName]))
                    //&& adjacentFeature.ColumnValues[buildRtgParameter.OneWayRoadOption.IndicatorColumnName].Equals(buildRtgParameter.OneWayRoadOption.StartToEnd))
                    {
                        removedIds.Add(adjacentFeature.Id);
                    }
                    else if (buildRtgParameter.ClosedRoadOption.ClosedColumnName != string.Empty && buildRtgParameter.ClosedRoadOption.MatchClosedRoadValue(adjacentFeature.ColumnValues[buildRtgParameter.ClosedRoadOption.ClosedColumnName]))
                    //else if (buildRtgParameter.ClosedRoadOption.ClosedColumnName != string.Empty && adjacentFeature.ColumnValues[buildRtgParameter.ClosedRoadOption.ClosedColumnName].Equals(buildRtgParameter.ClosedRoadOption.ClosedRoadValue))
                    {
                        removedIds.Add(adjacentFeature.Id);
                    }
                }
            }
            return removedIds;
        }

        private float GetSpeed(String featureRoadSpeedClassValue)
        {
            float result = buildRtgParameter.SpeedOption.DefaultSpeed;
            foreach (var item in buildRtgParameter.SpeedOption.RoadSpeeds)
            {
                if (item.Key.Equals(featureRoadSpeedClassValue, StringComparison.OrdinalIgnoreCase))
                {
                    result = (float)item.Value;
                    break;
                }
            }

            return result;
        }

        private float GetDistance(float distanceMeter)
        {
            switch (buildRtgParameter.SpeedOption.SpeedUnit)
            {
                case SpeedUnit.KilometersPerHour:
                    return (float)(distanceMeter * 0.001);
                case SpeedUnit.MilesPerHour:
                    return (float)(distanceMeter * 0.00062137);
            }
            return distanceMeter;
        }

        private static LineShape GetLineShape(Feature lineFeature)
        {
            BaseShape baseShape = lineFeature.GetShape();

            LineShape lineShape = baseShape as LineShape;
            if (lineShape == null)
            {
                MultilineShape lineShapes = ((MultilineShape)baseShape);
                Collection<Vertex> vertices = new Collection<Vertex>();

                foreach (LineShape line in lineShapes.Lines)
                {
                    for (int i = 0; i < line.Vertices.Count; i++)
                    {
                        vertices.Add(line.Vertices[i]);
                    }
                }

                lineShape = new LineShape(vertices);
                lineShape.Id = baseShape.Id;
                lineShape.Tag = baseShape.Tag;
            }

            return lineShape;
        }

        protected virtual void OnBuildingRouteRTSegment(RoutingIndexBuilderEventArgs e)
        {
            var handler = BuildingRouteRTSegment;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBuildingRoutableSegment(RoutingIndexBuilderEventArgs e)
        {
            var handler = BuildingRoutableSegment;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnExtractingSqliteDatabase(RoutingIndexBuilderEventArgs e)
        {
            var handler = ExtractingSqliteDatabase;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBuildingIndexFinished(EventArgs e)
        {
            var handler = BuildingIndexFinished;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
