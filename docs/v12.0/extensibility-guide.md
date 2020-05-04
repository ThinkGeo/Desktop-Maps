# Extensibility Guide

## Integrating Custom Data Formats

<!-- 
YouTube video of "Extending Map Suite: Integrating Custom Data Formats"
Link: https://youtu.be/WjJIVY2SGVI
-->
<iframe width="560" height="315" src="https://www.youtube.com/embed/WjJIVY2SGVI" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

=== "MinimumFeatureLayer.cs"

    ```csharp
    using ThinkGeo.Core;

    namespace OledbPointFeatureSource
    {
        class MinimumFeatureLayer : FeatureLayer
        {
            public MinimumFeatureLayer()
            {
                this.FeatureSource = new MinimumFeatureSource();
            }
        }
    }
    ```

=== "MinimumFeatureSource.cs"

    ```csharp
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ThinkGeo.Core;

    namespace OledbPointFeatureSource
    {
        class MinimumFeatureSource : FeatureSource
        {
            protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> columnNames)
            {
                return new Collection<Feature>(new Feature[] { new Feature(-95.2806, 38.9554, "1") });
            }
        }
    }
    ```

=== "OledbPointFeatureLayer.cs"

    ```csharp
    using ThinkGeo.MapSuite.Core;

    namespace OledbPointFeatureSource
    {
        // The FeatureLayer is a wrapper to the FeatureSource and provides all of the drawing specific
        // methods such as the ZoomLevels and Styles.  As you can see it is just a simple wrapper and the
        // real work is already done for you in the FeatureLayer base class.
        class OledbPointFeatureLayer: FeatureLayer
        {
            OledbPointFeatureSource oledbPointFeatureSource;

            // It is important for compatability that you always have a default constructor that
            // takes no parameters.  This constructor will just chain to the more complex one.
            public OledbPointFeatureLayer()
                : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
            { }

            // We have provided another constructor that has all of the parameters we need for the FeatureLayer
            // to work.
            public OledbPointFeatureLayer(string tableName, string idColumnName, string xColumnName, string yColumnName, string connectionString)
                : base()
            {
                // Here is where we create our FeatureSource and set the internal property on the FeatureLayer
                oledbPointFeatureSource = new OledbPointFeatureSource(tableName, idColumnName, xColumnName, yColumnName, connectionString);
                this.FeatureSource = oledbPointFeatureSource;           
            }

            // The next dozen lines are all of the properties we need.  It is good form to always match
            // the parameters in your constructors with properties of the exact same name.
            public string TableName
            {
                get { return oledbPointFeatureSource.TableName; }
                set { oledbPointFeatureSource.TableName = value; }
            }

            public string ConnectionString
            {
                get { return oledbPointFeatureSource.ConnectionString; }
                set { oledbPointFeatureSource.ConnectionString = value; }
            }

            public string XColumnName
            {
                get { return oledbPointFeatureSource.XColumnName; }
                set { oledbPointFeatureSource.XColumnName = value; }
            }

            public string YColumnName
            {
                get { return oledbPointFeatureSource.YColumnName; }
                set { oledbPointFeatureSource.YColumnName = value; }
            }

            public string IdColumnName
            {
                get { return oledbPointFeatureSource.IdColumnName; }
                set { oledbPointFeatureSource.IdColumnName = value; }
            }
        }
    }
    ```

=== "OledbPointFeatureSource.cs"

    ```csharp
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.OleDb;
    using ThinkGeo.Core;

    namespace OledbPointFeatureSource
    {
        class OledbPointFeatureSource : FeatureSource
        {
            private OleDbConnection connection;
            private string connectionString;
            private string tableName;
            private string idColumnName;
            private string xColumnName;
            private string yColumnName;

            // It is important for compatability that you always have a default constructor that
            // takes no parameters.  This constructor will just chain to the more complex one.
            public OledbPointFeatureSource()
                : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
            { }

            // We have provided another constructor that has all of the parameters we need for the FeatureSource
            // to work.
            public OledbPointFeatureSource(string tableName, string idColumnName, string xColumnName, string yColumnName, string connectionString)
                : base()
            {
                TableName = tableName;
                IdColumnName = idColumnName;
                XColumnName = xColumnName;
                YColumnName = yColumnName;
                ConnectionString = connectionString;
            }

            // The next dozen lines are all of the properties we need.  It is good form to always match
            // the parameters in your constructors with properties of the exact same name.
            public string TableName
            {
                get { return tableName; }
                set { tableName = value; }
            }

            public string ConnectionString
            {
                get { return connectionString; }
                set { connectionString = value; }
            }

            public string XColumnName
            {
                get { return xColumnName; }
                set { xColumnName = value; }
            }

            public string YColumnName
            {
                get { return yColumnName; }
                set { yColumnName = value; }
            }

            public string IdColumnName
            {
                get { return idColumnName; }
                set { idColumnName = value; }
            }

            // Use the OpenCore to initialize your underlying data source.  The concreat Open method will ensure
            // that if the user calls the Open method multiple times in a row that you only get one call to 
            // OpenCore.
            protected override void OpenCore()
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
            }

            // Use the CloseCore to close your underlying data source.  It is important to note that the
            // CloseCore is not like the Dispose on other objects.  The FeatureSource is meant to be opened
            // and closed many times durring its lifetime.  Make sure that you clean up any objects that have 
            // unmanaged resources but do not put the object in a state that when Open is called it will fail.
            // The concreat Close method will ensure that if the user calle the Close multiple times in 
            // a row that you only get one call to CloseCore.
            protected override void CloseCore()
            {
                connection.Close();
            }

            // Here you need to query all of the features in your data source.  We use this method
            // as the basis of nearly all other virtual methods.  For example if you choose not to
            // override the GetCountCore then we will get all the features and count them as the default.
            // This is ineffecient however it produces the correct results.
            protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> columnNames)
            {
                OleDbCommand command = null;
                OleDbDataReader dataReader = null;
                Collection<Feature> returnFeatures = new Collection<Feature>();

                // Alays be sure to wrap imporant code that accesses resources that need
                // to be closed.  In the Finally we will ensure they always get cleaned up.
                try
                {
                    // We need to construct the part of the SQL statement for retuning the
                    // column data.  We only return the columns asked for byt the columnNames
                    // parameter of the function.  This ensures we do not get more than we need.
                    string columnsSQL = string.Empty;
                    foreach (string columnName in columnNames)
                    {
                        columnsSQL += "," + columnName;
                    }

                    // Here we build up and execute the query string using the columns the users defined in the properties
                    // such as the XColumnName and IdColumnName.
                    command = new OleDbCommand("SELECT " + idColumnName + " as TG_ID, " + xColumnName + " as TG_X, " + yColumnName + " as TG_Y " + columnsSQL + " FROM " + tableName, connection);
                    dataReader = command.ExecuteReader();

                    // We now loop though all of the results and build up our features that we need to return.
                    while (dataReader.Read())
                    {
                        Feature feature = new Feature(new PointShape(Convert.ToDouble(dataReader["TG_X"]), Convert.ToDouble(dataReader["TG_Y"])), dataReader["TG_ID"].ToString());

                        // This small part populates the column values that were requested.  They are data
                        // such as columns used for ClassBreakStyles or TextStyles for labeling.
                        foreach (string columnName in columnNames)
                        {
                            feature.ColumnValues.Add(columnName, dataReader[columnName].ToString());
                        }
                        returnFeatures.Add(feature);
                    }
                }
                finally
                {
                    // Cleanup any of the objects that need to be closed or disposed.
                    if (command != null) { command.Dispose(); }
                    if (dataReader != null) { dataReader.Dispose(); }
                }

                return returnFeatures;
            }

            // Though this method is not required for creating our new class it is an important one.
            // This method is used to get only the features inside of the bounding box passed in.  The reason
            // this is critical is that many other methods on the QueryTools such as Touches, Overlaps, Intersects,
            // and many others use this method as a first pass filter.  If you do not override this method then
            // the default code calls the GetAllFeatures and look at each to see if it is in the bounding box.
            // While this method will produce the correct result it will not perform as well as your custom code.
            protected override Collection<Feature> GetFeaturesInsideBoundingBoxCore(RectangleShape boundingBox, IEnumerable<string> returningColumnNames)
            {
                OleDbCommand command = null;
                OleDbDataReader dataReader = null;
                Collection<Feature> returnFeatures = new Collection<Feature>();

                // Alays be sure to wrap imporant code that accesses resources that need
                // to be closed.  In the Finally we will ensure they always get cleaned up.
                try
                {
                    // We need to construct the part of the SQL statement for retuning the
                    // column data.  We only return the columns asked for byt the columnNames
                    // parameter of the function.  This ensures we do not get more than we need.
                    string columnsSQL = string.Empty;
                    foreach (string columnName in returningColumnNames)
                    {
                        columnsSQL += "," + columnName;
                    }

                    // This whereSql is important becasue it is what is used to determine if the point is in
                    // the bounding box passed in.  While it is a bit complex it get your results quickly in
                    // large datasets so long as the X & Y columns are indexed.
                    string whereSql = " " + xColumnName + " <= " + boundingBox.LowerRightPoint.X + " AND " + xColumnName + " >= " + boundingBox.UpperLeftPoint.X + " AND " + yColumnName + " <= " + boundingBox.UpperLeftPoint.Y + " AND " + yColumnName + " >= " + boundingBox.LowerRightPoint.Y; 

                    command = new OleDbCommand("SELECT " + idColumnName + " as TG_ID, " + xColumnName + " as TG_X, " + yColumnName + " as TG_Y " + columnsSQL + " FROM " + tableName + " WHERE " + whereSql, connection);
                    dataReader = command.ExecuteReader();

                    // We now loop though all of the results and build up our features that we need to return.
                    while (dataReader.Read())
                    {
                        Feature feature = new Feature(Convert.ToDouble(dataReader["TG_X"]), Convert.ToDouble(dataReader["TG_Y"]), dataReader["TG_ID"].ToString());

                        // This small part populates the column values that were requested.  They are data
                        // such as columns used for ClassBreakStyles or TextStyles for labeling.
                        foreach (string columnName in returningColumnNames)
                        {
                            feature.ColumnValues.Add(columnName, dataReader[columnName].ToString());
                        }
                        returnFeatures.Add(feature);
                    }
                }
                finally
                {
                    // Cleanup any of the objects that need to be closed or disposed.
                    if (command != null) { command.Dispose(); }
                    if (dataReader != null) { dataReader.Dispose(); }
                }

                return returnFeatures;            
            }

            // This method returns all of the columns in the data source.  This method is not required however
            // if it is not overridden then the FeatureSource will not have any columns available to it.
            // Since having access to the column data is usefull for labeling and such we suggest you override it.
            protected override Collection<FeatureSourceColumn> GetColumnsCore()
            {
                OleDbCommand command = null;
                OleDbDataReader dataReader = null;
                Collection<FeatureSourceColumn> returnColumns = new Collection<FeatureSourceColumn>();

                // Alays be sure to wrap imporant code that accesses resources that need
                // to be closed.  In the Finally we will ensure they always get cleaned up.
                try
                {
                    // Here we have a query that will quickly return nothing.  As strange as it sounds it is a
                    // good way to get the table structure back without having to return any column data.
                    command = new OleDbCommand("SELECT * FROM " + tableName + " WHERE 1=2", connection);
                    dataReader = command.ExecuteReader();
            
                    // We now loop through and create our column list.  In the FeatureSourceColumn we can
                    // optionally provide the column type but it is just informational so we didn't code it.
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        FeatureSourceColumn featureSourceColumn = new FeatureSourceColumn(dataReader.GetName(i));
                        returnColumns.Add(featureSourceColumn);
                    }
                }
                finally
                {
                    // Cleanup any of the objects that need to be closed or disposed.
                    if (command != null) { command.Dispose(); }
                    if (dataReader != null) { dataReader.Dispose(); }
                }

                return returnColumns;
            }        

            // This is another method that does not need to be overridden but we suggest that you do.
            // This method gets the cound of all the features in the data source.  If you choose not to 
            // override it then the default will call the GetAllFeatures and count them.  This is not very
            // effecient so we suggest you override it.
            protected override int GetCountCore()
            {
                OleDbCommand command = null;            
                int count = 0;

                // Alays be sure to wrap imporant code that accesses resources that need
                // to be closed.  In the Finally we will ensure they always get cleaned up.
                try
                {
                    // Here we do a standard SQL count statement and return the results.
                    command = new OleDbCommand("SELECT COUNT(*) FROM " + tableName, connection);
                    count = Convert.ToInt32(command.ExecuteScalar());                
                }
                finally
                {
                    // Cleanup any of the objects that need to be closed or disposed.
                    if (command != null) { command.Dispose(); }                
                }
                return count;
            }
        }
    }
    ```

## Creating Custom Styles

<!--
YouTube video for "Extending Map Suite: Creating Custom Styles (Part 1 of 3)"
Link: https://youtu.be/ZIxKOACRDKM
-->
<iframe width="560" height="315" src="https://www.youtube.com/embed/ZIxKOACRDKM" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

=== "SizedPointStyle.cs"

    ```csharp
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ThinkGeo.Core;

    namespace ExtendingMapSuiteStyle
    {
        // Mark the class Serializable so that it workes in SQL Server state
        // and in every serialization context
        [Serializable]
        class SizedPointStyle : Style
        {
            private PointStyle pointStyle;
            private float ratio;
            private string sizeColumnName;

            public SizedPointStyle()
                : this(new PointStyle(), string.Empty, 1)
            { }

            public SizedPointStyle(PointStyle pointStyle, string sizeColumnName, float ratio)
            {
                this.pointStyle = pointStyle;
                this.sizeColumnName = sizeColumnName;
                this.ratio = ratio;
            }

            public PointStyle PointStyle
            {
                get { return pointStyle; }
                set { pointStyle = value; }
            }

            public float Ratio
            {
                get { return ratio; }
                set { ratio = value; }
            }

            public string SizeColumnName
            {
                get { return sizeColumnName; }
                set { sizeColumnName = value; }
            }

            protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
            {
                // Loop through each feaure and determine how large the point should 
                // be then adjust it's size.
                foreach (Feature feature in features)
                {
                    float sizeData = Convert.ToSingle(feature.ColumnValues[sizeColumnName]);
                    float symbolSize = sizeData / ratio;
                    pointStyle.SymbolSize = symbolSize;
                    pointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                }
            }

            protected override Collection<string> GetRequiredColumnNamesCore()
            {
                // Here we grab the columns from the pointStyle and then add
                // the sizeColumn name to make sure we pull back the column
                //  that we need to calculate the size
                Collection<string> columns = new Collection<string>();
                columns = pointStyle.GetRequiredColumnNames();
                if (!columns.Contains(sizeColumnName))
                {
                    columns.Add(sizeColumnName);
                }

                return columns;
            }

            protected override void DrawSampleCore(GeoCanvas canvas)
            {
                // Here we simply pass the call to the pointStyle's DrawSampleCore
                pointStyle.DrawSample(canvas);
            }
        }
    }
    ```

=== "TimeBasedPointStyle"

    ```csharp
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ThinkGeo.Core;

    namespace ExtendingMapSuiteStyle
    {
        // Mark the class Serializable so that it workes in SQL Server state
        // and in every serialization context
        [Serializable]
        class TimeBasedPointStyle : Style
        {
            private PointStyle daytimePointStyle;
            private PointStyle nighttimePointStyle;
            private string timeZoneColumnName;

            public TimeBasedPointStyle()
                : this(string.Empty, new PointStyle(), new PointStyle())
            { }

            public TimeBasedPointStyle(string timeZoneColumnName, PointStyle daytimePointStyle, PointStyle nighttimePointStyle)
            {
                this.timeZoneColumnName = timeZoneColumnName;
                this.daytimePointStyle = daytimePointStyle;
                this.nighttimePointStyle = nighttimePointStyle;
            }

            public PointStyle DaytimePointStyle
            {
                get { return daytimePointStyle; }
                set { daytimePointStyle = value; }
            }

            public PointStyle NighttimePointStyle
            {
                get { return nighttimePointStyle; }
                set { nighttimePointStyle = value; }
            }

            public string TimeZoneColumnName
            {
                get { return timeZoneColumnName; }
                set { timeZoneColumnName = value; }
            }


            protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
            {
                foreach (Feature feature in features)
                {
                    // Here we are going to do the calculation to see what
                    // time it is for each feature and draw the approperate style
                    float offsetToGmt = Convert.ToSingle(feature.ColumnValues[timeZoneColumnName]);
                    DateTime localTime = DateTime.UtcNow.AddHours(offsetToGmt);
                    if (localTime.Hour >= 7 && localTime.Hour <= 19)
                    {
                        // Daytime
                        daytimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                    }
                    else
                    {
                        //Nighttime
                        
                        nighttimePointStyle.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                    }
                }
            }

            protected override Collection<string> GetRequiredColumnNamesCore()
            {
                Collection<string> columns = new Collection<string>();

                // Grab any columns that the daytime style may need.
                Collection<string> daytimeColumns = daytimePointStyle.GetRequiredColumnNames();
                foreach (string column in daytimeColumns)
                {
                    if (!columns.Contains(column))
                    {
                        columns.Add(column);
                    }
                }

                // Grab any columns that the nighttime style may need.
                Collection<string> nighttimeColumns = nighttimePointStyle.GetRequiredColumnNames();
                foreach (string column in nighttimeColumns)
                {
                    if (!columns.Contains(column))
                    {
                        columns.Add(column);
                    }
                }

                // Make sure we add the timezone column
                if (!columns.Contains(timeZoneColumnName))
                {
                    columns.Add(timeZoneColumnName);
                }

                return columns;
            }

            protected override void DrawSampleCore(GeoCanvas canvas)
            {
                // Here is an example of why it is hard
                // to always draw a sample.  In this case
                // we just draw the daytime stlye.
                daytimePointStyle.DrawSample(canvas);
            }
        }
    }
    ```

=== "CachedValueStyle.cs"

    ```csharp
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ThinkGeo.Core;

    namespace ExtendingMapSuiteStyle
    {
        // Mark the class Serializable so that it workes in SQL Server state
        // and in every serialization context
        [Serializable]
        public class CachedValueStyle : Style
        {
            private string columnName;
            private Dictionary<string, ValueItem> valueItems = new Dictionary<string, ValueItem>();
            private Dictionary<string, string> valuesCache = new Dictionary<string, string>();

            public CachedValueStyle()
                : this(string.Empty, new Dictionary<string, ValueItem>())
            { }

            public CachedValueStyle(string columnName, Dictionary<string, ValueItem> valueItems)
            {
                this.columnName = columnName;
                this.valueItems = valueItems;
            }

            public string ColumnName
            {
                get { return columnName; }
                set { columnName = value; }
            }

            // We use a dictionary now instead of a collection to make lookups faster.
            public Dictionary<string, ValueItem> ValueItems
            {
                get { return valueItems; }
            }

            // Here you can pre-load all of the values by feature Id.
            // The key is the Feature.Id and the value is the value to match the
            // value on.
            public Dictionary<string, string> ValuesCache
            {
                get { return valuesCache; }
            }

            protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
            {
                foreach (Feature feature in features)
                {
                    string fieldValue = string.Empty;

                    // If we have items field value in the cache then let's use them!
                    // If not then get the value from the feature's column values
                    if (valuesCache.Count == 0)
                    {
                        fieldValue = feature.ColumnValues[columnName].Trim();
                    }
                    else
                    {
                        if (!ValuesCache.TryGetValue(feature.Id, out fieldValue))
                        {
                            fieldValue = string.Empty;
                        }
                    }

                    ValueItem valueItem = null;
                    // Check if the there is a value item in the dictionary that matches 
                    // our field value.
                    if (valueItems.ContainsKey(fieldValue))
                    {
                        valueItem = valueItems[fieldValue];
                    }

                    // If we cannot find a matcing value item in the collection do not draw anything.
                    if (valueItem != null)
                    {
                        // Call the draw on all of the default styles of the value item and also
                        // check if there are custom styles
                        Feature[] tmpFeatures = new Feature[1] { feature };
                        if (valueItem.CustomStyles.Count == 0)
                        {
                            valueItem.DefaultAreaStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                            valueItem.DefaultLineStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                            valueItem.DefaultPointStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                            valueItem.DefaultTextStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                        }
                        else
                        {
                            foreach (Style style in valueItem.CustomStyles)
                            {
                                style.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                            }
                        }
                    }
                }
            }

            protected override Collection<string> GetRequiredColumnNamesCore()
            {
                Collection<string> requiredFieldNames = new Collection<string>();

                // If we have provided cached values then there is no need to fetch the
                // column from our feature source.
                if (valuesCache.Count == 0)
                {
                    requiredFieldNames.Add(columnName);
                }

                foreach (ValueItem valueItem in valueItems.Values)
                {
                    // Check if we need any columns from custom styles
                    foreach (Style style in valueItem.CustomStyles)
                    {
                        Collection<string> tmpCollection = style.GetRequiredColumnNames();

                        foreach (string name in tmpCollection)
                        {
                            if (!requiredFieldNames.Contains(name))
                            {
                                requiredFieldNames.Add(name);
                            }
                        }
                    }

                    // Check if we need any columns from the DefaultTextStyle
                    Collection<string> fieldsInTextStyle = valueItem.DefaultTextStyle.GetRequiredColumnNames();
                    foreach (string fieldName in fieldsInTextStyle)
                    {
                        if (!requiredFieldNames.Contains(fieldName))
                        {
                            requiredFieldNames.Add(fieldName);
                        }
                    }

                    // Check if we need any columns from the DefaultPointStyle
                    Collection<string> fieldsInPointStyle = valueItem.DefaultPointStyle.GetRequiredColumnNames();
                    foreach (string fieldName in fieldsInPointStyle)
                    {
                        if (!requiredFieldNames.Contains(fieldName))
                        {
                            requiredFieldNames.Add(fieldName);
                        }
                    }

                    // Check if we need any columns from the DefaultLineStyle
                    Collection<string> fieldsInLineStyle = valueItem.DefaultLineStyle.GetRequiredColumnNames();
                    foreach (string fieldName in fieldsInLineStyle)
                    {
                        if (!requiredFieldNames.Contains(fieldName))
                        {
                            requiredFieldNames.Add(fieldName);
                        }
                    }

                    // Check if we need any columns from the DefaultAreaStyle
                    Collection<string> fieldsInAreaStyle = valueItem.DefaultAreaStyle.GetRequiredColumnNames();
                    foreach (string fieldName in fieldsInAreaStyle)
                    {
                        if (!requiredFieldNames.Contains(fieldName))
                        {
                            requiredFieldNames.Add(fieldName);
                        }
                    }
                }

                return requiredFieldNames;
            }
        }
    }
    ```

## Exploring Layers

<!--
YouTube video for "Extending Map Suite: Exploring Layers"
Link: https://youtu.be/BIGEDZw2jN8
-->
<iframe width="560" height="315" src="https://www.youtube.com/embed/BIGEDZw2jN8" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

=== "MapShape.cs"

    ```csharp
    using ThinkGeo.Core;

    namespace Samples
    {
        // This is our MapShape and it will be the single unit in our layer.
        public class MapShape
        {
            private Feature feature;
            private ZoomLevelSet zoomLevelSet;

            public MapShape()
                : this(new Feature())
            {
            }

            // Let's use this as a handy constructor if you already have
            // a feature or want to create one inline.
            public MapShape(Feature feature)
            {
                this.feature = feature;
                zoomLevelSet = new ZoomLevelSet();
            }

            //  This is the feature property, pretty simple.
            public Feature Feature
            {
                get { return feature; }
                set { feature = value; }
            }

            // This is the Zoom Level Set.  This high level object has all of
            // the logic in it for zoom levels, drawing and everything.
            public ZoomLevelSet ZoomLevels
            {
                get { return zoomLevelSet; }
                set { zoomLevelSet = value; }
            }
        }
    }
    ```

=== "MapShapeLayer.cs"

    ```csharp
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ThinkGeo.Core;

    namespace Samples
    {
        public class MapShapeLayer : Layer
        {
            private Dictionary<string, MapShape> mapShapes;

            public MapShapeLayer()
            {
                this.mapShapes = new Dictionary<string, MapShape>();
            }

            // Here is where you place all of your map shapes.
            public Dictionary<string, MapShape> MapShapes
            {
                get { return mapShapes; }
            }

            // This is a required overload of the Layer.  As you can see we simply
            // loop through all of our map shapes and then choose the correct zoom level.
            // After that, the zoom level class takes care of the heavy lifiting.  You
            // have to love how easy this framework is to re-use.
            protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
            {
                foreach (string mapShapeKey in mapShapes.Keys)
                {
                    MapShape mapShape = mapShapes[mapShapeKey];
                    ZoomLevel currentZoomLevel = mapShape.ZoomLevels.GetZoomLevelForDrawing(canvas.CurrentWorldExtent, canvas.Width, canvas.MapUnit);
                    if (currentZoomLevel != null)
                    {
                        if (canvas.CurrentWorldExtent.Intersects(mapShape.Feature.GetBoundingBox()))
                        {
                            currentZoomLevel.Draw(canvas, new Feature[] { mapShape.Feature }, new Collection<SimpleCandidate>(), labelsInAllLayers);
                        }
                    }
                }
            }
        }
    }
    ```

=== "MiniMapAdornmentLayer.cs"

    ```csharp
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ThinkGeo.Core;

    namespace Samples
    {
        public class MiniMapAdornmentLayer : AdornmentLayer
        {
            private Collection<Layer> layers;
            private int width;
            private int height;

            public MiniMapAdornmentLayer()
                : this(new Layer[] { }, 100, 100)
            { } 

            public MiniMapAdornmentLayer(int width, int height)
                : this(new Layer[] { }, width, height)
            { }

            public MiniMapAdornmentLayer(IEnumerable<Layer> layers, int width, int height)
            {
                this.layers = new Collection<Layer>();
                foreach (Layer layer in layers)
                {
                    this.layers.Add(layer);
                }

                this.width = width;
                this.height = height;
            }

            public Collection<Layer> Layers
            {
                get { return layers; }
            }

            public int Width
            {
                get { return width; }
                set { width = value; }
            }

            public int Height
            {
                get { return height; }
                set { height = value; }
            }

            protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
            {
                GeoImage miniImage = new GeoImage(width, height);
                RectangleShape scaledWorldExtent = MapEngine.GetDrawingExtent(canvas.CurrentWorldExtent, width, height);
                scaledWorldExtent.ScaleUp(300);
                GdiPlusGeoCanvas minCanvas = new GdiPlusGeoCanvas();

                minCanvas.BeginDrawing(miniImage, scaledWorldExtent, canvas.MapUnit);
                foreach (Layer layer in layers)
                {
                    layer.Draw(minCanvas, labelsInAllLayers);
                }

                minCanvas.DrawArea(RectangleShape.ScaleDown(minCanvas.CurrentWorldExtent, 1), new GeoPen(GeoColor.StandardColors.Gray, 2), DrawingLevel.LevelOne);
                minCanvas.DrawArea(canvas.CurrentWorldExtent, new GeoPen(GeoColor.StandardColors.Black, 2), DrawingLevel.LevelOne);

                minCanvas.EndDrawing();

                ScreenPointF drawingLocation = GetDrawingLocation(canvas, width, height);

                canvas.DrawScreenImageWithoutScaling(miniImage, (drawingLocation.X + width / 2) + 10, (drawingLocation.Y + height / 2) - 10, DrawingLevel.LevelOne, 0, 0, 0);
            }
        }
    }
    ```

=== "MultiGeoRasterLayer.cs"

    ```csharp
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.IO;
    using GisSharpBlog.NetTopologySuite.Geometries;
    using GisSharpBlog.NetTopologySuite.Index.Strtree;
    using ThinkGeo.Core;

    namespace Samples
    {
        // IMPORTANT: You have to reference NetTopologySuite.dll & GeoApi.dll in your project.  They come with Map Suite.

        // This class speeds up the loading of a large number of Raster layers by loading and drawing on demand only the files
        // in the current extent.  Normally if you had 100 Raster files you would need to load 100 layers; however, this has performance
        // issues--so we created this high level layer.  It loads a reference file that contains the bounding box, path and file information for 
        // all of the Raster files.  We load this information into an in-memory spatial index. When the map requests to draw the layer, we find the
        // Rasters that are in the current extent, create a layer on-the-fly, call their Draw method and then close them.  In this way, we load
        // on demand only the files that are in the current extent.

        // I have also included a small routine to build a reference file from a directory of Raster files.

        // Reference File Format: [UpperLeftPointX],[LowerRightPoint.X],[UpperLeftPoint.Y],[LowerRightPoint.Y],[Path & File Name to Raster]    
        
        // Sample:
        // -180.02197265625,-157.52197265625,-67.47802734375,-89.97802734375,..\..\App_Data\RasterImage\1.jpg
        // -112.52197265625,-90.02197265625,-67.47802734375,-89.97802734375,..\..\App_Data\RasterImage\10.jpg
        // 67.47802734375,89.97802734375,67.52197265625,45.02197265625,..\..\App_Data\RasterImage\100.jpg
        // 89.97802734375,112.47802734375,67.52197265625,45.02197265625,..\..\App_Data\RasterImage\101.jpg

        public class MultiGeoRasterLayer : Layer
        {
            private string rasterRefrencePathFileName;
            private STRtree spatialIndex;
            private double upperScale;
            private double lowerScale;
            private RectangleShape boundingBox;

            private const int upperLeftXPosition = 0;
            private const int upperLeftYPosition = 2;
            private const int lowerRightXPosition = 1;
            private const int lowerRightYPosition = 3;
            private const int pathFileNamePosition = 4;

            public MultiGeoRasterLayer()
                : this(string.Empty, double.MaxValue, double.MinValue)
            { }

            public MultiGeoRasterLayer(string rasterRefrencePathFileName)
                : this(rasterRefrencePathFileName, double.MaxValue, double.MinValue)
            { }

            public MultiGeoRasterLayer(string rasterRefrencePathFileName, double upperScale, double lowerScale)
            {
                this.rasterRefrencePathFileName = rasterRefrencePathFileName;
                this.upperScale = upperScale;
                this.lowerScale = lowerScale;
                boundingBox = new RectangleShape();
            }

            public string RasterRefrencePathFileName
            {
                get { return rasterRefrencePathFileName; }
                set { rasterRefrencePathFileName = value; }
            }

            public double UpperScale
            {
                get { return upperScale; }
                set { upperScale = value; }
            }

            public double LowerScale
            {
                get { return lowerScale; }
                set { lowerScale = value; }
            }

            // Here on the OpenCore we load our reference file and build the spatial index, which will be used in the DrawCore later.
            // You need to make sure the reference file is in the right format as described in the comments above.
            protected override void OpenCore()
            {
                if (File.Exists(rasterRefrencePathFileName))
                {
                    string[] rasterFiles = File.ReadAllLines(rasterRefrencePathFileName);
                    spatialIndex = new STRtree(rasterFiles.Length);

                    Collection<BaseShape> boundingBoxes = new Collection<BaseShape>();

                    foreach (string rasterLine in rasterFiles)
                    {
                        string[] parts = rasterLine.Split(new string[] { "," }, StringSplitOptions.None);
                        RectangleShape rasterBoundingBox = new RectangleShape(new PointShape(double.Parse(parts[upperLeftXPosition]), double.Parse(parts[upperLeftYPosition])), new PointShape(double.Parse(parts[lowerRightXPosition]), double.Parse(parts[lowerRightYPosition])));
                        Envelope envelope = new Envelope(double.Parse(parts[upperLeftXPosition]), double.Parse(parts[lowerRightXPosition]), double.Parse(parts[upperLeftYPosition]), double.Parse(parts[lowerRightYPosition]));
                        spatialIndex.Insert(envelope, parts[pathFileNamePosition]);
                    }
                    spatialIndex.Build();
                    boundingBox = ExtentHelper.GetBoundingBoxOfItems(boundingBoxes);
                }
                else
                {
                    throw new FileNotFoundException("The Raster reference file could not be found.", rasterRefrencePathFileName);
                }
            }

            // Here we set the spatial index to null to clean up the memory and get ready for serialization
            protected override void CloseCore()
            {
                spatialIndex = null;
            }

            // When we get to the Draw, things are easy.  First we check to make sure we are within our scales.
            // Next we look up the Raster files in the spatial index,
            // then open their layer, call their Draw and close them.
            protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
            {
                double currentScale = ExtentHelper.GetScale(canvas.CurrentWorldExtent, canvas.Width, canvas.MapUnit);

                if (currentScale >= lowerScale && currentScale <= upperScale)
                {
                    RectangleShape currentExtent = canvas.CurrentWorldExtent;
                    Envelope currentExtentEnvelope = new Envelope(currentExtent.UpperLeftPoint.X, currentExtent.LowerRightPoint.X, currentExtent.UpperLeftPoint.Y, currentExtent.LowerRightPoint.Y);
                    ArrayList rasters = (ArrayList)spatialIndex.Query(currentExtentEnvelope);

                    foreach (string file in rasters)
                    {
                        GdiPlusRasterLayer rasterRasterLayer = new GdiPlusRasterLayer(file);
                        rasterRasterLayer.Open();
                        rasterRasterLayer.Draw(canvas, labelsInAllLayers);
                        rasterRasterLayer.Close();
                    }
                }
            }

            // Here we let everyone know we support having a bounding box
            public override bool HasBoundingBox
            {
                get
                {
                    return true;
                }
            }

            //  We use the cached bounding box we set in the OpenCore
            protected override RectangleShape GetBoundingBoxCore()
            {
                return boundingBox;
            }

            // This is just a handy function to build a reference file from a directory.
            // You can tailor this code to fit your needs.
            public static void BuildReferenceFile(string newReferencepathFileName, string pathOfRasterFiles)
            {
                if (Directory.Exists(pathOfRasterFiles))
                {
                    string[] files = Directory.GetFiles(pathOfRasterFiles, "*.jpg");
                    StreamWriter streamWriter = null;

                    try
                    {
                        streamWriter = File.CreateText(newReferencepathFileName);

                        foreach (string file in files)
                        {
                            GdiPlusRasterLayer rasterRasterLayer = new GdiPlusRasterLayer(file);
                            rasterRasterLayer.Open();
                            RectangleShape boundingBox = rasterRasterLayer.GetBoundingBox();
                            rasterRasterLayer.Close();
                            streamWriter.WriteLine(string.Format("{0},{1},{2},{3},{4}", boundingBox.UpperLeftPoint.X, boundingBox.LowerRightPoint.X, boundingBox.UpperLeftPoint.Y, boundingBox.LowerRightPoint.Y, file));
                        }
                        streamWriter.Close();
                    }
                    finally
                    {
                        if (streamWriter != null) { streamWriter.Dispose(); }
                    }
                }
                else
                {
                    throw new DirectoryNotFoundException("The path containing the Raster files could not be found.");
                }
            }
        }
    }
    ```

=== "WatermarkAdornmentLayer.cs"

    ```csharp
    using System.Collections.ObjectModel;
    using ThinkGeo.Core;

    namespace Samples
    {
        public class WatermarkAdornmentLayer : AdornmentLayer
        {
            private string watermarkText = string.Empty;

            public WatermarkAdornmentLayer()
                : this("Watermark")
            { }

            public WatermarkAdornmentLayer(string watermarkText)
                : base()
            {
                this.watermarkText = watermarkText;
            }

            public string WatermarkText
            {
                get { return watermarkText; }
                set { watermarkText = value; }
            }

            protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
            {
                GeoFont font = new GeoFont("Arial", 12);
                GeoBrush brush = new GeoSolidBrush(GeoColor.StandardColors.Pink);
                Collection<ScreenPointF> textPath = new Collection<ScreenPointF>();

                int interval = 100;
                if (canvas.Width > interval)
                {
                    for (int x = 1; x < canvas.Width / interval; x++)
                    {
                        for (int y = 1; y < canvas.Height / interval; y++)
                        {
                            textPath.Add(new ScreenPointF(x * interval, y * interval));
                        }
                    }
                }
                else
                {
                    textPath.Add(new ScreenPointF(canvas.Width / 2, canvas.Height / 2));
                }

                canvas.DrawText(watermarkText, font, brush, textPath, DrawingLevel.LevelOne);
            }
        }
    }
    ```
