# FeatureSource


## Inheritance Hierarchy

+ `Object`
  + **`FeatureSource`**
    + [`InMemoryFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureSource.md)
    + [`DelimitedFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.DelimitedFeatureSource.md)
    + [`GpxFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.GpxFeatureSource.md)
    + [`GraticuleFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.GraticuleFeatureSource.md)
    + [`GridFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.GridFeatureSource.md)
    + [`InMemoryGridFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryGridFeatureSource.md)
    + [`MultipleFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.MultipleFeatureSource.md)
    + [`NoaaWeatherStationFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.NoaaWeatherStationFeatureSource.md)
    + [`NoaaWeatherWarningsFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.NoaaWeatherWarningsFeatureSource.md)
    + [`OsmBuildingOnlineFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.OsmBuildingOnlineFeatureSource.md)
    + [`MultipleShapeFileFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.MultipleShapeFileFeatureSource.md)
    + [`ShapeFileFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileFeatureSource.md)
    + [`SqliteFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.SqliteFeatureSource.md)
    + [`TabFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.TabFeatureSource.md)
    + [`TinyGeoFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.TinyGeoFeatureSource.md)
    + [`TobinBasFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.TobinBasFeatureSource.md)
    + [`UsgsDemFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.UsgsDemFeatureSource.md)
    + [`WfsFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.WfsFeatureSource.md)
    + [`WkbFileFeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.WkbFileFeatureSource.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`FeatureSource()`](#featuresource)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanExecuteSqlQuery`](#canexecutesqlquery)|`Boolean`|This property specifies whether the FeatureSource can excute a SQL query or not. If it is false, then it will throw exception when these APIs are calleds: ExecuteScalar, ExecuteNonQuery, ExecuteQuery|
|[`CanModifyColumnStructure`](#canmodifycolumnstructure)|`Boolean`|N/A|
|[`FeatureIdsToExclude`](#featureidstoexclude)|Collection<`String`>|A collection of strings representing record id of features not to get in the Layer.|
|[`GeoCache`](#geocache)|[`FeatureCache`](../ThinkGeo.Core/ThinkGeo.Core.FeatureCache.md)|The cache system.|
|[`Id`](#id)|`String`|N/A|
|[`IsEditable`](#iseditable)|`Boolean`|This property returns whether the FeatureSource allows edits or is read-only.|
|[`IsInTransaction`](#isintransaction)|`Boolean`|This property returns true if the FeatureSource is in a transaction and false if it is not.|
|[`IsOpen`](#isopen)|`Boolean`|This property returns true if the FeatureSource is open and false if it is not.|
|[`IsTransactionLive`](#istransactionlive)|`Boolean`|This property returns true if the features currently modified in a transaction are expected to reflect their state when calling other methods on the FeatureSource, such as spatial queries.|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This property holds the projection object that is used within the FeatureSource to ensure that features inside of the FeatureSource are projected.|
|[`TransactionBuffer`](#transactionbuffer)|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|The TransactionBuffer used in the Transaction System.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanExecuteSqlQueryCore`](#canexecutesqlquerycore)|`Boolean`|This property specifies whether the FeatureSource can excute a SQL query or not. If it is false, then it will throw exception when these APIs are calleds: ExecuteScalar, ExecuteNonQuery, ExecuteQuery|
|[`CanModifyColumnStructureCore`](#canmodifycolumnstructurecore)|`Boolean`|N/A|
|[`FeatureSourceColumns`](#featuresourcecolumns)|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|This property gets the columns of the feature source.|
|[`IsOpenCore`](#isopencore)|`Boolean`|This property returns true if the FeatureSource is open and false if it is not.|

### Public Methods Summary


|Name|
|---|
|[`AddColumn(FeatureSourceColumn)`](#addcolumnfeaturesourcecolumn)|
|[`AddFeature(Feature)`](#addfeaturefeature)|
|[`AddFeature(BaseShape)`](#addfeaturebaseshape)|
|[`AddFeature(BaseShape,IDictionary<String,String>)`](#addfeaturebaseshapeidictionary<stringstring>)|
|[`BeginTransaction()`](#begintransaction)|
|[`CanGetBoundingBoxQuickly()`](#cangetboundingboxquickly)|
|[`CanGetCountQuickly()`](#cangetcountquickly)|
|[`CloneDeep()`](#clonedeep)|
|[`Close()`](#close)|
|[`CommitTransaction()`](#committransaction)|
|[`ConvertToDataTable(IEnumerable<Feature>,IEnumerable<String>)`](#converttodatatableienumerable<feature>ienumerable<string>)|
|[`DeleteColumn(String)`](#deletecolumnstring)|
|[`DeleteFeature(String)`](#deletefeaturestring)|
|[`Equals(Object)`](#equalsobject)|
|[`ExecuteNonQuery(String)`](#executenonquerystring)|
|[`ExecuteQuery(String)`](#executequerystring)|
|[`ExecuteScalar(String)`](#executescalarstring)|
|[`GetAllFeatures(ReturningColumnsType,Int32)`](#getallfeaturesreturningcolumnstypeint32)|
|[`GetAllFeatures(ReturningColumnsType,Int32,Int32)`](#getallfeaturesreturningcolumnstypeint32int32)|
|[`GetAllFeatures(IEnumerable<String>,Int32,Int32)`](#getallfeaturesienumerable<string>int32int32)|
|[`GetAllFeatures(IEnumerable<String>)`](#getallfeaturesienumerable<string>)|
|[`GetAllFeatures(ReturningColumnsType)`](#getallfeaturesreturningcolumnstype)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetBoundingBoxById(String)`](#getboundingboxbyidstring)|
|[`GetBoundingBoxByIds(IEnumerable<String>)`](#getboundingboxbyidsienumerable<string>)|
|[`GetBoundingBoxesByIds(IEnumerable<String>)`](#getboundingboxesbyidsienumerable<string>)|
|[`GetColumnNameAlias(String,ICollection<String>)`](#getcolumnnamealiasstringicollection<string>)|
|[`GetColumnNamesOutsideFeatureSource(IEnumerable<String>)`](#getcolumnnamesoutsidefeaturesourceienumerable<string>)|
|[`GetColumns()`](#getcolumns)|
|[`GetCount()`](#getcount)|
|[`GetDistinctColumnValues(String)`](#getdistinctcolumnvaluesstring)|
|[`GetFeatureById(String,IEnumerable<String>)`](#getfeaturebyidstringienumerable<string>)|
|[`GetFeatureById(String,ReturningColumnsType)`](#getfeaturebyidstringreturningcolumnstype)|
|[`GetFeatureIds()`](#getfeatureids)|
|[`GetFeatureIdsInsideBoundingBox(RectangleShape)`](#getfeatureidsinsideboundingboxrectangleshape)|
|[`GetFeaturesByColumnValue(String,String,ReturningColumnsType)`](#getfeaturesbycolumnvaluestringstringreturningcolumnstype)|
|[`GetFeaturesByColumnValue(String,String,IEnumerable<String>)`](#getfeaturesbycolumnvaluestringstringienumerable<string>)|
|[`GetFeaturesByColumnValue(String,String)`](#getfeaturesbycolumnvaluestringstring)|
|[`GetFeaturesByIds(IEnumerable<String>,IEnumerable<String>)`](#getfeaturesbyidsienumerable<string>ienumerable<string>)|
|[`GetFeaturesByIds(IEnumerable<String>,ReturningColumnsType)`](#getfeaturesbyidsienumerable<string>returningcolumnstype)|
|[`GetFeaturesForDrawing(RectangleShape,Double,Double,IEnumerable<String>)`](#getfeaturesfordrawingrectangleshapedoubledoubleienumerable<string>)|
|[`GetFeaturesForDrawing(RectangleShape,Double,Double,ReturningColumnsType)`](#getfeaturesfordrawingrectangleshapedoubledoublereturningcolumnstype)|
|[`GetFeaturesInsideBoundingBox(RectangleShape,IEnumerable<String>)`](#getfeaturesinsideboundingboxrectangleshapeienumerable<string>)|
|[`GetFeaturesInsideBoundingBox(RectangleShape,ReturningColumnsType)`](#getfeaturesinsideboundingboxrectangleshapereturningcolumnstype)|
|[`GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`](#getfeaturesnearesttobaseshapegeographyunitint32ienumerable<string>)|
|[`GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,ReturningColumnsType)`](#getfeaturesnearesttobaseshapegeographyunitint32returningcolumnstype)|
|[`GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>)`](#getfeaturesnearesttofeaturegeographyunitint32ienumerable<string>)|
|[`GetFeaturesNearestTo(Feature,GeographyUnit,Int32,ReturningColumnsType)`](#getfeaturesnearesttofeaturegeographyunitint32returningcolumnstype)|
|[`GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`](#getfeaturesnearesttobaseshapegeographyunitint32ienumerable<string>doubledistanceunit)|
|[`GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`](#getfeaturesnearesttofeaturegeographyunitint32ienumerable<string>doubledistanceunit)|
|[`GetFeaturesOutsideBoundingBox(RectangleShape,IEnumerable<String>)`](#getfeaturesoutsideboundingboxrectangleshapeienumerable<string>)|
|[`GetFeaturesOutsideBoundingBox(RectangleShape,ReturningColumnsType)`](#getfeaturesoutsideboundingboxrectangleshapereturningcolumnstype)|
|[`GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`](#getfeatureswithindistanceofbaseshapegeographyunitdistanceunitdoubleienumerable<string>)|
|[`GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`](#getfeatureswithindistanceofbaseshapegeographyunitdistanceunitdoublereturningcolumnstype)|
|[`GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`](#getfeatureswithindistanceoffeaturegeographyunitdistanceunitdoubleienumerable<string>)|
|[`GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`](#getfeatureswithindistanceoffeaturegeographyunitdistanceunitdoublereturningcolumnstype)|
|[`GetFirstFeaturesWellKnownType()`](#getfirstfeatureswellknowntype)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`Open()`](#open)|
|[`RefreshColumns()`](#refreshcolumns)|
|[`RemoveEmptyAndExcludedFeatures(Collection<Feature>)`](#removeemptyandexcludedfeaturescollection<feature>)|
|[`RollbackTransaction()`](#rollbacktransaction)|
|[`SpatialQuery(BaseShape,QueryType,IEnumerable<String>)`](#spatialquerybaseshapequerytypeienumerable<string>)|
|[`SpatialQuery(BaseShape,QueryType,ReturningColumnsType)`](#spatialquerybaseshapequerytypereturningcolumnstype)|
|[`SpatialQuery(Feature,QueryType,IEnumerable<String>)`](#spatialqueryfeaturequerytypeienumerable<string>)|
|[`SpatialQuery(Feature,QueryType,ReturningColumnsType)`](#spatialqueryfeaturequerytypereturningcolumnstype)|
|[`SplitColumnNames(IEnumerable<String>)`](#splitcolumnnamesienumerable<string>)|
|[`ToString()`](#tostring)|
|[`UpdateColumn(String,FeatureSourceColumn)`](#updatecolumnstringfeaturesourcecolumn)|
|[`UpdateFeature(Feature)`](#updatefeaturefeature)|
|[`UpdateFeature(BaseShape)`](#updatefeaturebaseshape)|
|[`UpdateFeature(BaseShape,IDictionary<String,String>)`](#updatefeaturebaseshapeidictionary<stringstring>)|

### Protected Methods Summary


|Name|
|---|
|[`CanGetBoundingBoxQuicklyCore()`](#cangetboundingboxquicklycore)|
|[`CanGetCountQuicklyCore()`](#cangetcountquicklycore)|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`CloseCore()`](#closecore)|
|[`CombineFieldValues(Dictionary<String,String>,IEnumerable<String>)`](#combinefieldvaluesdictionary<stringstring>ienumerable<string>)|
|[`CommitTransactionCore(TransactionBuffer)`](#committransactioncoretransactionbuffer)|
|[`ExecuteNonQueryCore(String)`](#executenonquerycorestring)|
|[`ExecuteQueryCore(String)`](#executequerycorestring)|
|[`ExecuteScalarCore(String)`](#executescalarcorestring)|
|[`Finalize()`](#finalize)|
|[`GetAllFeaturesCore(IEnumerable<String>,Int32,Int32)`](#getallfeaturescoreienumerable<string>int32int32)|
|[`GetAllFeaturesCore(IEnumerable<String>)`](#getallfeaturescoreienumerable<string>)|
|[`GetBoundingBoxByIdCore(String)`](#getboundingboxbyidcorestring)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetColumnNamesInsideFeatureSource(IEnumerable<String>)`](#getcolumnnamesinsidefeaturesourceienumerable<string>)|
|[`GetColumnNamesOutsideFeatureSourceCall(IEnumerable<String>)`](#getcolumnnamesoutsidefeaturesourcecallienumerable<string>)|
|[`GetColumnsCore()`](#getcolumnscore)|
|[`GetCountCore()`](#getcountcore)|
|[`GetDistinctColumnValuesCore(String)`](#getdistinctcolumnvaluescorestring)|
|[`GetFeatureIdsCore()`](#getfeatureidscore)|
|[`GetFeatureIdsInsideBoundingBoxCore(RectangleShape)`](#getfeatureidsinsideboundingboxcorerectangleshape)|
|[`GetFeaturesByColumnValueCore(String,String,IEnumerable<String>)`](#getfeaturesbycolumnvaluecorestringstringienumerable<string>)|
|[`GetFeaturesByIdsCore(IEnumerable<String>,IEnumerable<String>)`](#getfeaturesbyidscoreienumerable<string>ienumerable<string>)|
|[`GetFeaturesForDrawingCore(RectangleShape,Double,Double,IEnumerable<String>)`](#getfeaturesfordrawingcorerectangleshapedoubledoubleienumerable<string>)|
|[`GetFeaturesInsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`](#getfeaturesinsideboundingboxcorerectangleshapeienumerable<string>)|
|[`GetFeaturesNearestFrom(Collection<Feature>,BaseShape,GeographyUnit,Int32)`](#getfeaturesnearestfromcollection<feature>baseshapegeographyunitint32)|
|[`GetFeaturesNearestToCore(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`](#getfeaturesnearesttocorebaseshapegeographyunitint32ienumerable<string>)|
|[`GetFeaturesOutsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`](#getfeaturesoutsideboundingboxcorerectangleshapeienumerable<string>)|
|[`GetFeaturesWithinDistanceOfCore(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`](#getfeatureswithindistanceofcorebaseshapegeographyunitdistanceunitdoubleienumerable<string>)|
|[`GetFirstFeaturesWellKnownTypeCore()`](#getfirstfeatureswellknowntypecore)|
|[`GetReturningColumnNames(ReturningColumnsType)`](#getreturningcolumnnamesreturningcolumnstype)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnClosedFeatureSource(ClosedFeatureSourceEventArgs)`](#onclosedfeaturesourceclosedfeaturesourceeventargs)|
|[`OnClosingFeatureSource(ClosingFeatureSourceEventArgs)`](#onclosingfeaturesourceclosingfeaturesourceeventargs)|
|[`OnCommittedTransaction(CommittedTransactionEventArgs)`](#oncommittedtransactioncommittedtransactioneventargs)|
|[`OnCommittingTransaction(CommittingTransactionEventArgs)`](#oncommittingtransactioncommittingtransactioneventargs)|
|[`OnCustomColumnFetch(CustomColumnFetchEventArgs)`](#oncustomcolumnfetchcustomcolumnfetcheventargs)|
|[`OnDrawingProgressChanged(DrawingProgressChangedEventArgs)`](#ondrawingprogresschangeddrawingprogresschangedeventargs)|
|[`OnGettingColumns(GettingColumnsFeatureSourceEventArgs)`](#ongettingcolumnsgettingcolumnsfeaturesourceeventargs)|
|[`OnGettingFeaturesByIds(GettingFeaturesByIdsFeatureSourceEventArgs)`](#ongettingfeaturesbyidsgettingfeaturesbyidsfeaturesourceeventargs)|
|[`OnGettingFeaturesForDrawing(GettingFeaturesForDrawingFeatureSourceEventArgs)`](#ongettingfeaturesfordrawinggettingfeaturesfordrawingfeaturesourceeventargs)|
|[`OnGottenColumns(GottenColumnsFeatureSourceEventArgs)`](#ongottencolumnsgottencolumnsfeaturesourceeventargs)|
|[`OnOpenedFeatureSource(OpenedFeatureSourceEventArgs)`](#onopenedfeaturesourceopenedfeaturesourceeventargs)|
|[`OnOpeningFeatureSource(OpeningFeatureSourceEventArgs)`](#onopeningfeaturesourceopeningfeaturesourceeventargs)|
|[`OpenCore()`](#opencore)|
|[`ProcessTransaction(RectangleShape,Collection<Feature>,Boolean)`](#processtransactionrectangleshapecollection<feature>boolean)|
|[`RaiseCustomColumnFetchEvent(Collection<Feature>,Collection<String>)`](#raisecustomcolumnfetcheventcollection<feature>collection<string>)|
|[`SpatialQueryCore(BaseShape,QueryType,IEnumerable<String>)`](#spatialquerycorebaseshapequerytypeienumerable<string>)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`DrawingProgressChanged`](#drawingprogresschanged)|[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)|N/A|
|[`GettingColumns`](#gettingcolumns)|[`GettingColumnsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingColumnsFeatureSourceEventArgs.md)|N/A|
|[`GottenColumns`](#gottencolumns)|[`GottenColumnsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GottenColumnsFeatureSourceEventArgs.md)|N/A|
|[`GettingFeaturesByIds`](#gettingfeaturesbyids)|[`GettingFeaturesByIdsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingFeaturesByIdsFeatureSourceEventArgs.md)|N/A|
|[`GettingFeaturesForDrawing`](#gettingfeaturesfordrawing)|[`GettingFeaturesForDrawingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingFeaturesForDrawingFeatureSourceEventArgs.md)|N/A|
|[`CustomColumnFetch`](#customcolumnfetch)|[`CustomColumnFetchEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CustomColumnFetchEventArgs.md)|N/A|
|[`CommittingTransaction`](#committingtransaction)|[`CommittingTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittingTransactionEventArgs.md)|N/A|
|[`CommittedTransaction`](#committedtransaction)|[`CommittedTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittedTransactionEventArgs.md)|N/A|
|[`OpeningFeatureSource`](#openingfeaturesource)|[`OpeningFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningFeatureSourceEventArgs.md)|N/A|
|[`OpenedFeatureSource`](#openedfeaturesource)|[`OpenedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedFeatureSourceEventArgs.md)|N/A|
|[`ClosingFeatureSource`](#closingfeaturesource)|[`ClosingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingFeatureSourceEventArgs.md)|N/A|
|[`ClosedFeatureSource`](#closedfeaturesource)|[`ClosedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedFeatureSourceEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|N/A|

### Protected Constructors

#### `FeatureSource()`

**Summary**

   *This is the default constructor for the abstract FeatureSource class.*

**Remarks**

   *As this method is protected, you may only add code to this method if you override it from an inheriting class.*

**Return Value**

|Type|Description|
|---|---|
|N/A||

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Properties

#### `CanExecuteSqlQuery`

**Summary**

   *This property specifies whether the FeatureSource can excute a SQL query or not. If it is false, then it will throw exception when these APIs are calleds: ExecuteScalar, ExecuteNonQuery, ExecuteQuery*

**Remarks**

   *The default implementation is false.*

**Return Value**

`Boolean`

---
#### `CanModifyColumnStructure`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `FeatureIdsToExclude`

**Summary**

   *A collection of strings representing record id of features not to get in the Layer.*

**Remarks**

   *This string collection is a handy place to specify what records not to get from the source. Suppose you have a shape file of roads and you want to hide the roads within a particular rectangle, simply execute GetFeaturesInsideBoundingBox() and add the id of the return features to the collection and forget about them. Since you can set this by Layer it makes is easy to determine what to and what not to.*

**Return Value**

Collection<`String`>

---
#### `GeoCache`

**Summary**

   *The cache system.*

**Remarks**

   *You must set IsActive to true for the Cache system. The default is not active.*

**Return Value**

[`FeatureCache`](../ThinkGeo.Core/ThinkGeo.Core.FeatureCache.md)

---
#### `Id`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `IsEditable`

**Summary**

   *This property returns whether the FeatureSource allows edits or is read-only.*

**Remarks**

   *This property is useful to check if a specific FeatureSource accepts editing. If you call BeginTransaction and this property is false, then an exception will be raised. For developers who are creating or extending a FeatureSource, it is expected that you override this virtual method if the new FeatureSource you are creating allows edits. By default, the decimalDegreesValue is false, meaning that if you want to allow edits you must override this method and return true.*

**Return Value**

`Boolean`

---
#### `IsInTransaction`

**Summary**

   *This property returns true if the FeatureSource is in a transaction and false if it is not.*

**Remarks**

   *To enter a transaction, you must first call the BeginTransaction method of the FeatureSource. It is possible that some FeatureSources are read-only and do not allow edits. To end a transaction, you must either call CommitTransaction or RollbackTransaction.*

**Return Value**

`Boolean`

---
#### `IsOpen`

**Summary**

   *This property returns true if the FeatureSource is open and false if it is not.*

**Remarks**

   *Various methods on the FeatureSource require that it be in an open state. If one of those methods is called when the state is not open, then the method will throw an exception. To enter the open state, you must call the FeatureSource's Open method. The method will raise an exception if the current FeatureSource is already open.*

**Return Value**

`Boolean`

---
#### `IsTransactionLive`

**Summary**

   *This property returns true if the features currently modified in a transaction are expected to reflect their state when calling other methods on the FeatureSource, such as spatial queries.*

**Remarks**

   *The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

`Boolean`

---
#### `MaxRecordsToDraw`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Projection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `ProjectionConverter`

**Summary**

   *This property holds the projection object that is used within the FeatureSource to ensure that features inside of the FeatureSource are projected.*

**Remarks**

   *By default this property is null, meaning that the data being passed back from any methods on the FeatureSource will be in the coordinate system of the raw data. When you specify a projection object in the property, all incoming and outgoing method calls will subject the features to projection. For example, if the spatial database you are using has all of its data stored in decimal degrees, but you want to see the data in UTM, you would create a projection object that goes from decimal degrees to UTM and set that as the projection. With this one property set, we will ensure that it will seem to you the developer that all of the data in the FeatureSource is in UTM. That means every spatial query will return UTM projected shapes. You can even pass in UTM shapes for the parameters. Internally, we will ensure that the shapes are converted to and from the projection without any intervention on the developer's part. In fact, even when you override virtual or abstract core methods in the FeatureSource, you will not need to know about projections at all. Simply work with the data in its native coordinate system. We will handle all of the projection at the high level method.*

**Return Value**

[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)

---
#### `TransactionBuffer`

**Summary**

   *The TransactionBuffer used in the Transaction System.*

**Remarks**

   *The Transaction System The transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)

---

### Protected Properties

#### `CanExecuteSqlQueryCore`

**Summary**

   *This property specifies whether the FeatureSource can excute a SQL query or not. If it is false, then it will throw exception when these APIs are calleds: ExecuteScalar, ExecuteNonQuery, ExecuteQuery*

**Remarks**

   *The default implementation is false.*

**Return Value**

`Boolean`

---
#### `CanModifyColumnStructureCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `FeatureSourceColumns`

**Summary**

   *This property gets the columns of the feature source.*

**Remarks**

   *None.*

**Return Value**

Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>

---
#### `IsOpenCore`

**Summary**

   *This property returns true if the FeatureSource is open and false if it is not.*

**Remarks**

   *Various methods on the FeatureSource require that it be in an open state. If one of those methods is called when the state is not open, then the method will throw an exception. To enter the open state, you must call the FeatureSource's Open method. The method will raise an exception if the current FeatureSource is already open.*

**Return Value**

`Boolean`

---

### Public Methods

#### `AddColumn(FeatureSourceColumn)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureSourceColumn|[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)|N/A|

---
#### `AddFeature(Feature)`

**Summary**

   *This method adds a new Feature to an existing transaction.*

**Remarks**

   *This method adds a new Feature to an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction. The Transaction System The transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This string is the ID that will uniquely identify this Feature while it is in a transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter represents the new Feature that will be added to the transaction.|

---
#### `AddFeature(BaseShape)`

**Summary**

   *This method adds a new Feature (composed of the passed-in BaseShape) to an existing transaction.*

**Remarks**

   *This method adds a new Feature (composed of the passed-in BaseShape) to an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction. The Transaction System The transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This string is the ID that will uniquely identify this Feature while it is in a transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter represents the BaseShape that will be used to make the new Feature that will be added to the transaction.|

---
#### `AddFeature(BaseShape,IDictionary<String,String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|columnValues|IDictionary<`String`,`String`>|N/A|

---
#### `BeginTransaction()`

**Summary**

   *This method starts a new transaction if the FeasuteSource allows editing*

**Remarks**

   *This method is used to start a transaction, assuming that the FeatureSource allows editing. There are some additional prerequisites to beginning a transaction, such as ensuring that a transaction is not already in progress. You must also be sure that the FeatureSource has been opened. The Transaction System The transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CanGetBoundingBoxQuickly()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CanGetCountQuickly()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloneDeep()`

**Summary**

   *Create a copy of a FeatureSource using the deep clone process.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|A cloned FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Close()`

**Summary**

   *This method closes the FeatureSource and releases any resources it was using.*

**Remarks**

   *This method is the concrete wrapper for the abstract method CloseCore. The Close method plays an important role in the life cycle of the FeatureSource. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override the Core version of this method, it is recommended that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed FeatureSource is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CommitTransaction()`

**Summary**

   *This method will commit the existing transaction to its underlying source of data.*

**Remarks**

   *This method is the concrete wrapper for the virtual method CommitTransactionCore. As this is the concrete version, the real work is done in the Core version of the method. It will commit the existing transaction to its underlying source of data. It will then pass back the results of the commit, including any error(s) received. Lastly, it will free up the internal memory cache of any InternalFeatures added, updated or deleted. You will need to ensure that you have started a transaction by calling BeginTransaction.The Transaction SystemThe transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`TransactionResult`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResult.md)|The returned decimalDegreesValue of this method is a TransactionResult class, which gives you the status of the transaction you just committed. It includes how many of the updates, adds, and deletes were successful and any errors that were encountered during the committing of the transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ConvertToDataTable(IEnumerable<Feature>,IEnumerable<String>)`

**Summary**

   *This method is a static API to get information about a group of passed-in features with the specified returningColumns, in the format of a DataTable.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DataTable`|A DateTable of information about those passed-in features and the returning columnNames.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the target features.|
|columnNames|IEnumerable<`String`>|This parameter specifies the returning columnNames for the features.|

---
#### `DeleteColumn(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|N/A|

---
#### `DeleteFeature(String)`

**Summary**

   *This method deletes a Feature from an existing transaction.*

**Remarks**

   *This method deletes a Feature from an existing transaction. You will need to ensure that you have started a transaction by calling the BeginTransaction. The Transaction System The transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This string is the Id of the feature in the FeatureSource you wish to delete.|

---
#### `Equals(Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|obj|`Object`|N/A|

---
#### `ExecuteNonQuery(String)`

**Summary**

   *Executes a SQL statement against a connection object.*

**Remarks**

   *You can use ExecuteNonQuery to perform catalog operations (for example, querying the structure of a database or creating database objects such as tables), or to change the data in a database by executing UPDATE, INSERT, or DELETE statements. Although ExecuteNonQuery does not return any rows, any output parameters or return values mapped to parameters are populated with data. For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|The number of rows affected.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The sqlStatement to be excuted.|

---
#### `ExecuteQuery(String)`

**Summary**

   *Executes the query and returns the result returned by the query.*

**Remarks**

   *Use the ExcuteScalar method to retrieve a single value from the database. This requires less code than using the ExcuteQuery method and then performing the operations necessary to generate the single value using the data.*

**Return Value**

|Type|Description|
|---|---|
|`DataTable`|The result set in the format of dataTable.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The sqlStatement to be excuted.|

---
#### `ExecuteScalar(String)`

**Summary**

   *Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.*

**Remarks**

   *Use the ExcuteScalar method to retrieve a single value from the database. This requires less code than using the ExcuteQuery method and then performing the operations necessary to generate the single value using the data.*

**Return Value**

|Type|Description|
|---|---|
|`Object`|The first column of the first row in the result set.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The sqlStatement to be excuted.|

---
#### `GetAllFeatures(ReturningColumnsType,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnTypes|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|
|startIndex|`Int32`|N/A|

---
#### `GetAllFeatures(ReturningColumnsType,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnTypes|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|
|startIndex|`Int32`|N/A|
|takeCount|`Int32`|N/A|

---
#### `GetAllFeatures(IEnumerable<String>,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|N/A|
|startIndex|`Int32`|N/A|
|takeCount|`Int32`|N/A|

---
#### `GetAllFeatures(IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures in the FeatureSource.*

**Remarks**

   *This method returns all of the InternalFeatures in the FeatureSource. It will return whatever is returned by the GetAllFeaturesCore method, along with any of the additions or subtractions made if you are in a transaction and that transaction is configured to be live. The main purpose of this method is to be the anchor of all of our default virtual implementations within this class. We as the framework developers wanted to provide you the user with as much default virtual implementation as possible. To do this, we needed a way to get access to all of the features. For example, let's say we want to create a default implementation for finding all of the InternalFeatures in a bounding box. Because this is an abstract class, we do not know the specifics of the underlying data or how its spatial indexes work. What we do know is that if we get all of the records, then we can brute-force the answer. In this way, if you inherited from this class and only implemented this one method, we can provide default implementations for virtually every other API. While this is nice for you the developer if you decide to create your own FeatureSource, it comes with a price: namely, it is very inefficient. In the example we just discussed (about finding all of the InternalFeatures in a bounding box), we would not want to look at every record to fulfil this method. Instead, we would want to override the GetFeaturesInsideBoundingBoxCore and implement specific code that would be faster. For example, in Oracle Spatial there is a specific SQL statement to perform this operation very quickly. The same holds true with other specific FeatureSource examples. Most default implementations in the FeatureSource call the GetFeaturesInsideBoundingBoxCore, which by default calls the GetAllFeaturesCore. It is our advice that if you create your own FeatureSource that you ALWAYS override the GetFeatureInsideBoundingBox. This will ensure that nearly every other API will operate efficiently. Please see the specific API to determine what method it uses. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of all of the InternalFeatures in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetAllFeatures(ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures in the FeatureSource.*

**Remarks**

   *This method returns all of the InternalFeatures in the FeatureSource. It will return whatever is returned by the GetAllFeaturesCore method, along with any of the additions or subtractions made if you are in a transaction and that transaction is configured to be live. The main purpose of this method is to be the anchor of all of our default virtual implementations within this class. We as the framework developers wanted to provide you the user with as much default virtual implementation as possible. To do this, we needed a way to get access to all of the features. For example, let's say we want to create a default implementation for finding all of the InternalFeatures in a bounding box. Because this is an abstract class, we do not know the specifics of the underlying data or how its spatial indexes work. What we do know is that if we get all of the records, then we can brute-force the answer. In this way, if you inherited from this class and only implemented this one method, we can provide default implementations for virtually every other API. While this is nice for you the developer if you decide to create your own FeatureSource, it comes with a price: namely, it is very inefficient. In the example we just discussed (about finding all of the InternalFeatures in a bounding box), we would not want to look at every record to fulfil this method. Instead, we would want to override the GetFeaturesInsideBoundingBoxCore and implement specific code that would be faster. For example, in Oracle Spatial there is a specific SQL statement to perform this operation very quickly. The same holds true with other specific FeatureSource examples. Most default implementations in the FeatureSource call the GetFeaturesInsideBoundingBoxCore, which by default calls the GetAllFeaturesCore. It is our advice that if you create your own FeatureSource that you ALWAYS override the GetFeatureInsideBoundingBox. This will ensure that nearly every other API will operate efficiently. Please see the specific API to determine what method it uses. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of all of the InternalFeatures in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetBoundingBox()`

**Summary**

   *This method returns the bounding box which encompasses all of the features in the FeatureSource.*

**Remarks**

   *This method is the concrete wrapper for the virtual method GetBoundingBoxCore. It will return whatever is returned by the GetBoundingBoxCore method, along with any additions or subtractions made if you are in a transaction and that transaction is configured to be live. To determine what the default implementation of the abstract GetBoundingBoxCore method is, please see the documentation for it. The default implementation of GetBoundingBoxCore uses the GetAllRecordsCore method to calculate the bounding box of the FeatureSource. We strongly recommend that you provide your own implementation for this method that will be more efficient. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box which encompasses all of the features in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetBoundingBoxById(String)`

**Summary**

   *This method returns a bounding box based on the InternalFeatures Id specified.*

**Remarks**

   *This method returns a bounding box by providing an Id. The internal implementation calls the GetFeaturesByIdsCore and only passes one Id in the collection. That method in turn calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, then we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a bounding box based on the InternalFeatures Id specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter represents the Id for the InternalFeatures whose bounding box you want.|

---
#### `GetBoundingBoxByIds(IEnumerable<String>)`

**Summary**

   *This method returns a collection of bounding boxes based on the Feature Ids specified.*

**Remarks**

   *This method returns a bounding boxes by providing a goupd of Ids. The internal implementation calls the GetFeaturesByIdsCore. That method in turn calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, then we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a collection of bounding boxes based on the Feature Ids specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter represents the group of Ids for the InternalFeatures whose bounding boxes you want.|

---
#### `GetBoundingBoxesByIds(IEnumerable<String>)`

**Summary**

   *This method returns a collection of bounding boxes based on the Feature Ids specified.*

**Remarks**

   *This method returns a bounding boxes by providing a goupd of Ids. The internal implementation calls the GetFeaturesByIdsCore. That method in turn calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, then we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|This method returns a collection of bounding boxes based on the Feature Ids specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter represents the group of Ids for the InternalFeatures whose bounding boxes you want.|

---
#### `GetColumnNameAlias(String,ICollection<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|N/A|
|columnNames|ICollection<`String`>|N/A|

---
#### `GetColumnNamesOutsideFeatureSource(IEnumerable<String>)`

**Summary**

   *This method returns the field names that are not in the FeatureSource from a list of provided field names.*

**Remarks**

   *This is a protected method that is intended to help developers who want to implement or extend one of our FeatureSources. It is important to note that, as a general rule, returning column data of a Feature or a set of InternalFeatures happens inside the non-Core methods and we usually take care of it. However, as a developer, if you wish to add a new public method, then you will need to handle the projection yourself. Let's say, for example, that you want to add a new Find method called FindLargeFeatures. You pass in a group of columns to return. Most of the time, the requested columns will actually be in the FeatureSource, but sometimes they will not. The way we allow users to get data from outside of the Feature Source is by raising an event called CustomColumnFetch. This way, we allow people to provide data that is outside of the FeatureSource. Since you will be implementing your own public method, you will want to support this as all of our other public methods do. When you first enter the public method, you will want to separate out the fields that are in the FeatureSource from those that are not. You can call this method and the GetColumnNamesOutsideFeatureSource. If inside your public method you need to call any of our Core methods, then you need to make sure that you only pass in the list of column names that are in the FeatureSource. We assume that Core methods are simple and they do not handle this complexity. With the list of non-FeatureSource column names, you simply loop through each column name for each record and call the OnCustomColumnFetch method while passing in the proper parameters. This will allow you give the user a chance to provide the values for the Feature's fields that were not in the FeatureSource. After that, you combine your results and pass them back out as the return. public Collection<Feature> FindLargeFeatures(double AreaSize, IEnumerable <string> columnsToReturn) {Step 1: Separate the columns that are in the FeatureSource from those that are not.  Step 2: Call any Core Methods and only pass in the columns that are in the FeatureSourceStep3: For Each feature/column name combination, call the OnCustomFiedlFetch and allow your user to provide the custom data.  Step4: Integrate the custom data with the result of the Core method plus any processing you did. Then return the consolidated result. }*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns the field names that are not in the FeatureSource from a list of provided field names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|This parameter is a list of field names, where not every field name may be in the FeatureSource.|

---
#### `GetColumns()`

**Summary**

   *This method returns the columns available for the FeatureSource and caches them.*

**Remarks**

   *As this is the concrete method wrapping the GetColumnsCore, it is important to note that this method will cache the results to GetColumnsCore. What this means is that the first time this method is called it will call GetCollumnsCore, which is protected, and cache the results. The next time this method is called it will not call GetColumnsCore again. This was done to increase speed, as this is a critical method that is used very often in the internal code of the class. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|This method returns the columns available for the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCount()`

**Summary**

   *This method returns the count of the number of records in this FeatureSource.*

**Remarks**

   *This method is the concrete wrapper for the virtual method GetCountCore. It will return whatever is returned by the GetCountCore method, along with any additions or subtractions made if you are in a transaction and that transaction is configured to be live. To determine what the default implementation of the abstract GetCountCore method is, please see the documentation for it. The default implementation of GetCountCore uses the GetAllRecordsCore method to calculate how many records there are in the FeatureSource. We strongly recommend that you provide your own implementation for this method that will be more efficient. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|This method returns the count of the number of records in this FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetDistinctColumnValues(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`DistinctColumnValue`](../ThinkGeo.Core/ThinkGeo.Core.DistinctColumnValue.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|N/A|

---
#### `GetFeatureById(String,IEnumerable<String>)`

**Summary**

   *This method returns a Feature by providing its Id in the FeatureSource.*

**Remarks**

   *This method returns a collection of InternalFeatures by providing a group of Ids. The internal implementation calls the GetFeaturesByIdsCore and only passes one Id in the collection. That method in turn calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns a Feature by providing its Id in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id which uniquely identifies it in the FeatureSource.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeatureById(String,ReturningColumnsType)`

**Summary**

   *This method returns a Feature by providing its Id in the FeatureSource.*

**Remarks**

   *This method returns a collection of InternalFeatures by providing a group of Ids. The internal implementation calls the GetFeaturesByIdsCore and only passes one Id in the collection. That method in turn calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, then we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns a Feature by providing its Id in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id which uniquely identifies it in the FeatureSource.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeatureIds()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetFeatureIdsInsideBoundingBox(RectangleShape)`

**Summary**

   *This method returns the Ids of all of the InternalFeatures of this FeatureSource that are inside of the bounding box.*

**Remarks**

   *This method returns the Ids of all of the InternalFeatures of this FeatureSource that are inside of the specified bounding box. If you are overriding this method you will not need to consider anything about transactions, as this is handled by the concrete version of this method. The default implementation of GetFeatureIdsInsideBoundingBoxCore uses the GetfeaturesInsideBoundingBoxCore method to determine which InternalFeatures are inside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. That is especially important for this method, as many other default virtual methods use this for their calculations. When you override this method, we highly recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The Ids of all of the InternalFeatures of this FeatureSource that are inside of the bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatureIds inside of|

---
#### `GetFeaturesByColumnValue(String,String,ReturningColumnsType)`

**Summary**

   *Get all of the features by passing a columnName and a specified columValue.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returnning features matches the columnValue.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|The specified columnName to match the columnValue.|
|columnValue|`String`|The specified columnValue to match those returning features.|
|returningColumnType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeaturesByColumnValue(String,String,IEnumerable<String>)`

**Summary**

   *Get all of the features by passing a columnName and a specified columValue.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returnning features matches the columnValue.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|The specified columnName to match the columnValue.|
|columnValue|`String`|The specified columnValue to match those returning features.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesByColumnValue(String,String)`

**Summary**

   *Get all of the features by passing a columnName and a specified columValue.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returnning features matches the columnValue.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|The specified columnName to match the columnValue.|
|columnValue|`String`|The specified columnValue to match those returning features.|

---
#### `GetFeaturesByIds(IEnumerable<String>,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures by providing a group of Ids.*

**Remarks**

   *This method returns a collection of InternalFeatures by providing a group of Ids. The internal implementation calls the GetFeaturesByIdsCore. That method in turn calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, then we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures by providing a group of Ids.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter represents the group of Ids which uniquely identifies the InternalFeatures in the FeatureSource.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesByIds(IEnumerable<String>,ReturningColumnsType)`

**Summary**

   *This method returns a collection of InternalFeatures by providing a group of Ids.*

**Remarks**

   *This method returns a collection of InternalFeatures by providing a group of Ids. The internal implementation calls the GetFeaturesByIdsCore. That method in turn calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, then we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures by providing a group of Ids.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter represents the group of Ids which uniquely identifies the InternalFeatures in the FeatureSource.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeaturesForDrawing(RectangleShape,Double,Double,IEnumerable<String>)`

**Summary**

   *This method returns the InternalFeatures that will be used for drawing.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource that are inside of the specified bounding box. If you are in a transaction and that transaction is live, this method will also take that into consideration. The default implementation of GetFeaturesForDrawing uses the GetFeaturesInsodeBoundingBoxCore with some optimizations based on the screen width and height. For example, we can determine if a feature is going to draw in only one to four pixels and in that case we may not draw the entire feature but just a subset of it instead. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns the InternalFeatures that will be used for drawing.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the bounding box of the InternalFeatures you want to draw.|
|screenWidth|`Double`|This parameter is the width of the view, in screen pixels, that you will draw on.|
|screenHeight|`Double`|This parameter is the height of the view, in screen pixels, that you will draw on.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesForDrawing(RectangleShape,Double,Double,ReturningColumnsType)`

**Summary**

   *This method returns the InternalFeatures that will be used for drawing.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource that are inside of the specified bounding box. If you are in a transaction and that transaction is live, this method will also take that into consideration. The default implementation of GetFeaturesForDrawing uses the GetFeaturesInsodeBoundingBoxCore with some optimizations based on the screen width and height. For example, we can determine if a feature is going to draw in only one to four pixels and in that case we may not draw the entire feature but just a subset of it instead. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns the InternalFeatures that will be used for drawing.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the bounding box of the InternalFeatures that you want to draw.|
|screenWidth|`Double`|This parameter is the width of the view, in screen pixels, that you will draw on.|
|screenHeight|`Double`|This parameter is the height of the view, in screen pixels, that you will draw on.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType you wish to return with each Feature.|

---
#### `GetFeaturesInsideBoundingBox(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures of this FeatureSource inside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource inside of the specified bounding box. If you are in a transaction and that transaction is live, this method will also take that into consideration. The default implementation of GetFeaturesInsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are inside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. That is especially important for this method, as many other default virtual methods use this for their calculations. When you override this method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of all of the InternalFeatures that are inside of the bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatures inside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesInsideBoundingBox(RectangleShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures of this FeatureSource inside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource inside of the specified bounding box. If you are in a transaction and that transaction is live, this method will also take that into consideration. The default implementation of GetFeaturesInsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are inside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. That is especially important for this method, as many other default virtual methods use this for their calculations. When you override this method, we highly recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of all of the InternalFeatures that are inside of the bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatures inside of.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you want to find InternalFeatures close to.|
|unitOfFeatureSource|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,ReturningColumnsType)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,ReturningColumnsType)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the target feature you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|
|searchRadius|`Double`|Limit the maximize distance proximately to search closest records.|
|unitOfSearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit of searchRadius parameter.|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is feature you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|
|searchRadius|`Double`|Limit the maximize distance proximately to search closest records.|
|unitOfSearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit of distanceLimits parameter.|

---
#### `GetFeaturesOutsideBoundingBox(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box. If you are in a transaction and that transaction is live, this method will also take that into consideration. The default implementation of GetFeaturesOutsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are outside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatures outside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesOutsideBoundingBox(RectangleShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box. If you are in a transaction and that transaction is live, this method will also take that into consideration. The default implementation of GetFeaturesOutsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are outside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatures outside of.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the measurement unit for the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the measurement unit for the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the measurement unit for the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the measurement unit for the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `GetFirstFeaturesWellKnownType()`

**Summary**

   *This method returns the well known type that represents the first feature from FeatureSource.*

**Remarks**

   *This method is the concrete wrapper for the virtual method GetFirstFeaturesWellKnownTypeCore. It will return whatever is returned by the GetFirstFeaturesWellKnownTypeCore method, along with any additions or subtractions made if you are in a transaction and that transaction is configured to be live. To determine what the default implementation of the abstract GetCountCore method is, please see the documentation for it. The default implementation of GetFirstFeaturesWellKnownTypeCore uses the GetAllFeaturesCore method to get WellKnownType of the first feature from all features. We strongly recommend that you provide your own implementation for this method that will be more efficient. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|This method returns the well known type that represents the first feature from FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetHashCode()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetType()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Type`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Open()`

**Summary**

   *This method opens the FeatureSource so that it is initialized and ready to use.*

**Remarks**

   *This method is the concrete wrapper for the abstract method OpenCore. The Open method plays an important role, as it is responsible for initializing the FeatureSource. Most methods on the FeatureSource will throw an exception if the state of the FeatureSource is not opened. When the map draws each layer, it will open the FeatureSource as one of its first steps, then after it is finished drawing with that layer it will close it. In this way we are sure to release all resources used by the FeatureSource. When implementing the abstract method, consider opening files for file-based sources, connecting to databases in the database-based sources and so on. You will get a chance to close these in the Close method of the FeatureSource. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `RefreshColumns()`

**Summary**

   *This method refresh the columns available for the FeatureSource and caches them.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method refresh the columns available for the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `RemoveEmptyAndExcludedFeatures(Collection<Feature>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `RollbackTransaction()`

**Summary**

   *This method will cancel an existing transaction. It will free up the internal memory cache of any InternalFeatures added, updated or deleted.*

**Remarks**

   *This method will cancel an existing transaction. It will free up the internal memory cache of any InternalFeatures added, updated or deleted. You will need to ensure that you have started a transaction by calling BeginTransaction.The Transaction SystemThe transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `SpatialQuery(BaseShape,QueryType,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified below. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Spatial Query Types:Disjoint - This method returns InternalFeatures where the specific Feature and the targetShape have no points in common.Intersects - This method returns InternalFeatures where the specific Feature and the targetShape have at least one point in common.Touches - This method returns InternalFeatures where the specific Feature and the targetShape have at least one boundary point in common, but no interior points.Crosses - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all interior points.Within - This method returns InternalFeatures where the specific Feature lies within the interior of the targetShape.Contains - This method returns InternalFeatures where the specific Feature lies within the interior of the current shape.Overlaps - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all points in common.TopologicalEqual - This method returns InternalFeatures where the specific Feature and the target Shape are topologically equal. The default implementation of SpatialQueryCore uses the GetFeaturesInsideBoundingBoxCore method to pre-filter the spatial query. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override this method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|This parameter specifies what kind of spatial query you wish to perform.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `SpatialQuery(BaseShape,QueryType,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified below. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Spatial Query Types:Disjoint - This method returns InternalFeatures where the specific Feature and the targetShape have no points in common.Intersects - This method returns InternalFeatures where the specific Feature and the targetShape have at least one point in common.Touches - This method returns InternalFeatures where the specific Feature and the targetShape have at least one boundary point in common, but no interior points.Crosses - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all interior points.Within - This method returns InternalFeatures where the specific Feature lies within the interior of the targetShape.Contains - This method returns InternalFeatures where the specific Feature lies within the interior of the current shape.Overlaps - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all points in common.TopologicalEqual - This method returns InternalFeatures where the specific Feature and the target Shape are topologically equal. The default implementation of SpatialQueryCore uses the GetFeaturesInsideBoundingBoxCore method to pre-filter the spatial query. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override this method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return decimalDegreesValue is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|This parameter specifies what kind of spatial query you wish to perform.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `SpatialQuery(Feature,QueryType,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified below. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Spatial Query Types:Disjoint - This method returns InternalFeatures where the specific Feature and the targetShape have no points in common.Intersects - This method returns InternalFeatures where the specific Feature and the targetShape have at least one point in common.Touches - This method returns InternalFeatures where the specific Feature and the targetShape have at least one boundary point in common, but no interior points.Crosses - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all interior points.Within - This method returns InternalFeatures where the specific Feature lies within the interior of the targetShape.Contains - This method returns InternalFeatures where the specific Feature lies within the interior of the current shape.Overlaps - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all points in common.TopologicalEqual - This method returns InternalFeatures where the specific Feature and the target Shape are topologically equal. The default implementation of SpatialQueryCore uses the GetFeaturesInsideBoundingBoxCore method to pre-filter the spatial query. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override this method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|This parameter specifies what kind of spatial query you wish to perform.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `SpatialQuery(Feature,QueryType,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified below. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Spatial Query Types:Disjoint - This method returns InternalFeatures where the specific Feature and the targetShape have no points in common.Intersects - This method returns InternalFeatures where the specific Feature and the targetShape have at least one point in common.Touches - This method returns InternalFeatures where the specific Feature and the targetShape have at least one boundary point in common, but no interior points.Crosses - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all interior points.Within - This method returns InternalFeatures where the specific Feature lies within the interior of the targetShape.Contains - This method returns InternalFeatures where the specific Feature lies within the interior of the current shape.Overlaps - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all points in common.TopologicalEqual - This method returns InternalFeatures where the specific Feature and the target Shape are topologically equal. The default implementation of SpatialQueryCore uses the GetFeaturesInsideBoundingBoxCore method to pre-filter the spatial query. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override this method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|This parameter specifies what kind of spatial query you wish to perform.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with each Feature.|

---
#### `SplitColumnNames(IEnumerable<String>)`

**Summary**

   *This method will split the column names based on our column syntax.*

**Remarks**

   *This method is meant to be used by advanced users who are creating their own new methods on the FeatureSource. We have a system where you can concatenate column names you specify anywhere in the system to create a unique string. For example, let's say you have a dataset that has the following columns: Name, Grade and Class, and you want to create a custom label for it. Whenever you specify the column you wanted to use in the label, you could use a string like this: "[Name] received a [Grade] in [Class]" and this would be valid. Behind the scenes, this method parses the string and returns the column names separately. Then, after we have the data, there is another helper method called CombineFieldValues that will add them back together again. All of this happens normally in the concrete methods of the FeatureSource and inheriting classes; however, if you want to extend the classes and create your own concrete methods, then we suggest you use this to be compliant with the concatenation system.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This returns a single list of column names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnNames|IEnumerable<`String`>|This parameter represents the column names you want to split.|

---
#### `ToString()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `UpdateColumn(String,FeatureSourceColumn)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|N/A|
|newFeatureSourceColumn|[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)|N/A|

---
#### `UpdateFeature(Feature)`

**Summary**

   *This method updates a Feature in an existing transaction.*

**Remarks**

   *This method updates a Feature in an existing transaction. You will need to ensure that you have started a transaction by calling the BeginTransaction.The Transaction SystemThe transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The Feature you wish to update in the transaction.|

---
#### `UpdateFeature(BaseShape)`

**Summary**

   *This method updates a Feature (composed of the passed-in BaseShape) in an existing transaction.*

**Remarks**

   *This method updates a Feature (composed of the passed-in BaseShape) in an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction.The Transaction SystemThe transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape that will be used to make the new Feature that you wish to update in the transaction.|

---
#### `UpdateFeature(BaseShape,IDictionary<String,String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|columnValues|IDictionary<`String`,`String`>|N/A|

---

### Protected Methods

#### `CanGetBoundingBoxQuicklyCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CanGetCountQuicklyCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloneDeepCore()`

**Summary**

   *Create a copy of a FeatureSource using the deep clone process. The default implementation uses serialization.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|A cloned FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloseCore()`

**Summary**

   *This method closes the FeatureSource and releases any resources it was using.*

**Remarks**

   *This protected virtual method is called from the concrete public method Close. The close method plays an important role in the life cycle of the FeatureSource. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override this method, it is recommended that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed FeatureSource is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CombineFieldValues(Dictionary<String,String>,IEnumerable<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,`String`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnValues|Dictionary<`String`,`String`>|N/A|
|originalColumnNames|IEnumerable<`String`>|N/A|

---
#### `CommitTransactionCore(TransactionBuffer)`

**Summary**

   *This method will commit the existing transaction to its underlying source of data.*

**Remarks**

   *This method will commit the existing transaction to its underlying source of data. It will then pass back the results of the commit, including any error(s) received. If you are implementing your own FeatureSource, this is one of the crucial methods you must create. It should be fairly straightforward that you will loop through the transaction buffer and add, edit or delete the InternalFeatures in your underlying data source. Remember to build and pass back the TransactionResult class so that users of your FeatureSource can respond to failures you may encounter while committing the InternalFeatures. We will handle the end of the transaction and also the cleanup of the transaction buffer. Your task will be to commit the records and produce a TransactionResult return.The Transaction SystemThe transaction system of a FeatureSource sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureSource. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureSource API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureSource that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureSource reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|[`TransactionResult`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResult.md)|The returned decimalDegreesValue of this method is a TransactionResult class which gives you the status of the transaction you just committed. It includes how many of the updates, adds, and deletes were successful, as well as any error(s) that were encountered during the committing of the transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|transactions|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|This parameter encapsulates all of the adds, edits and deletes that make up the transaction. You will use this data to write the changes to your underlying data source.|

---
#### `ExecuteNonQueryCore(String)`

**Summary**

   *Executes a SQL statement against a connection object.*

**Remarks**

   *You can use ExecuteNonQuery to perform catalog operations (for example, querying the structure of a database or creating database objects such as tables), or to change the data in a database by executing UPDATE, INSERT, or DELETE statements. Although ExecuteNonQuery does not return any rows, any output parameters or return values mapped to parameters are populated with data. For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|The number of rows affected.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The sqlStatement to be excuted.|

---
#### `ExecuteQueryCore(String)`

**Summary**

   *Executes the query and returns the result returned by the query.*

**Remarks**

   *Use the ExcuteScalar method to retrieve a single value from the database. This requires less code than using the ExcuteQuery method and then performing the operations necessary to generate the single value using the data.*

**Return Value**

|Type|Description|
|---|---|
|`DataTable`|The result set in the format of dataTable.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The sqlStatement to be excuted.|

---
#### `ExecuteScalarCore(String)`

**Summary**

   *Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.*

**Remarks**

   *Use the ExcuteScalar method to retrieve a single value from the database. This requires less code than using the ExcuteQuery method and then performing the operations necessary to generate the single value using the data.*

**Return Value**

|Type|Description|
|---|---|
|`Object`|The first column of the first row in the result set.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The sqlStatement to be excuted.|

---
#### `Finalize()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetAllFeaturesCore(IEnumerable<String>,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|N/A|
|startIndex|`Int32`|N/A|
|takeCount|`Int32`|N/A|

---
#### `GetAllFeaturesCore(IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures in the FeatureSource.*

**Remarks**

   *This method returns all of the InternalFeatures in the FeatureSource. You will not need to consider anything about pending transactions, as this will be handled in the non-Core version of the method. The main purpose of this method is to be the anchor of all of our default virtual implementations within this class. We as the framework developers wanted to provide you the user with as much default virtual implementation as possible. To do this, we needed a way to get access to all of the features. For example, let's say we want to create a default implementation for finding all of the InternalFeatures in a bounding box. Because this is an abstract class, we do not know the specifics of the underlying data or how its spatial indexes work. What we do know is that if we get all of the records, then we can brute-force the answer. In this way, if you inherited from this class and only implemented this one method, we can provide default implementations for virtually every other API. While this is nice for you the developer if you decide to create your own FeatureSource, it comes with a price: namely, it is very inefficient. In the example we just discussed (about finding all of the InternalFeatures in a bounding box), we would not want to look at every record to fulfil this method. Instead, we would want to override the GetFeaturesInsideBoundingBoxCore and implement specific code that would be faster. For example, in Oracle Spatial there is a specific SQL statement to perform this operation very quickly. The same holds true with other specific FeatureSource examples. Most default implementations in the FeatureSource call the GetFeaturesInsideBoundingBoxCore, which by default calls the GetAllFeaturesCore. It is our advice that if you create your own FeatureSource that you ALWAYS override the GetFeatureInsideBoundingBox. This will ensure that nearly every other API will operate efficiently. Please see the specific API to determine what method it uses.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of all of the InternalFeatures in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetBoundingBoxByIdCore(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|N/A|

---
#### `GetBoundingBoxCore()`

**Summary**

   *This method returns the bounding box which encompasses all of the features in the FeatureSource.*

**Remarks**

   *This protected virtual method is called from the concrete public method GetBoundingBox. It does not take into account any transaction activity, as this is the responsibility of the concrete public method GetBoundingBox. In this way, as a developer, if you choose to override this method you do not have to consider transactions at all. The default implementation of GetBoundingBoxCore uses the GetAllRecordsCore method to calculate the bounding box of the FeatureSource. We strongly recommend that you provide your own implementation for this method that will be more efficient. If you do not override this method, it will get the BoundingBox by calling the GetAllFeatureCore method and deriving it from each feature. This is a very inefficient way to get the BoundingBox in most data sources. It is highly recommended that you override this method and replace it with a highly optimized version. For example, in a ShapeFile the BoundingBox is in the main header of the file. Similarly, if you are using Oracle Spatial, you can execute a simple query to get the BoundingBox of all the records without returning them. In these ways you can greatly improve the performance of this method.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box which encompasses all of the features in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetColumnNamesInsideFeatureSource(IEnumerable<String>)`

**Summary**

   *This method returns the field names that are in the FeatureSource from a list of provided field names.*

**Remarks**

   *This is a protected method that is intended to help developers who want to implement or extend one of our FeatureSources. It is important to note that, as a general rule, returning column data of a Feature or a set of InternalFeatures happens inside the non-Core methods and we usually take care of it. However, as a developer, if you wish to add a new public method, then you will need to handle the projection yourself. Let's say, for example, that you want to add a new Find method called FindLargeFeatures. You pass in a group of columns to return. Most of the time, the requested columns will actually be in the FeatureSource, but sometimes they will not. The way we allow users to get data from outside of the Feature Source is by raising an event called CustomColumnFetch. This way, we allow people to provide data that is outside of the FeatureSource. Since you will be implementing your own public method, you will want to support this as all of our other public methods do. When you first enter the public method, you will want to separate out the fields that are in the FeatureSource from those that are not. You can call this method and the GetColumnNamesOutsideFeatureSource. If inside your public method you need to call any of our Core methods, then you need to make sure that you only pass in the list of column names that are in the FeatureSource. We assume that Core methods are simple and they do not handle this complexity. With the list of non-FeatureSource column names, you simply loop through each column name for each record and call the OnCustomColumnFetch method while passing in the proper parameters. This will allow you give the user a chance to provide the values for the Feature's fields that were not in the FeatureSource. After that, you combine your results and pass them back out as the return. public Collection<Feature> FindLargeFeatures(double AreaSize, IEnumerable <string> columnsToReturn) {Step 1: Separate the columns that are in the FeatureSource from those that are not.  Step 2: Call any Core Methods and only pass in the columns that are in the FeatureSourceStep3: For Each feature/column name combination, call the OnCustomFiedlFetch and allow your user to provide the custom data.  Step4: Integrate the custom data with the result of the Core method plus any processing you did. Then return the consolidated result. }*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns the field names that are in the FeatureSource from a list of provided field names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|This parameter is a list of column names, where not every field name may be in the FeatureSource.|

---
#### `GetColumnNamesOutsideFeatureSourceCall(IEnumerable<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetColumnsCore()`

**Summary**

   *This method returns the columns available for the FeatureSource.*

**Remarks**

   *As this is the virtual core version of the Columns method, it is intended to be overridden in inherited version of the class. When overriding, you will be responsible for getting a list of all of the columns supported by the FeatureSource. In this way, the FeatureSource will know what columns are available and will remove any extra columns when making calls to other core methods. For example, if you have a FeatureSource that has three columns of information and the user calls a method that requests four columns of information (something they can do with custom fields), we will first compare what they are asking for to the results of the GetColumnsCore. This way we can strip out custom columns before calling other Core methods, which are only responsible for returning data in the FeatureSource. For more information on custom fields, please see the documentation on OnCustomFieldsFetch.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|This method returns the columns available for the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCountCore()`

**Summary**

   *This method returns the count of the number of records in this FeatureSource.*

**Remarks**

   *This protected virtual method is called from the concrete public method GetCount. It does not take into account any transaction activity, as this is the responsibility of the concrete public method GetCount. This way, as a developer, if you choose to override this method you do not have to consider transactions at all. The default implementation of GetCountCore uses the GetAllRecordsCore method to calculate how many records there are in the FeatureSource. We strongly recommend that you provide your own implementation for this method that will be more efficient. If you do not override this method, it will get the count by calling the GetAllFeatureCore method and counting each feature. This is a very inefficient way to get the count in most data sources. It is highly recommended that you override this method and replace it with a highly optimized version. For example, in a ShapeFile the record count is in the main header of the file. Similarly, if you are using Oracle Spatial, you can execute a simple query to get the count of all of the records without returning them. In these ways you can greatly improve the performance of this method.*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|This method returns the count of the number of records in this FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetDistinctColumnValuesCore(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`DistinctColumnValue`](../ThinkGeo.Core/ThinkGeo.Core.DistinctColumnValue.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|N/A|

---
#### `GetFeatureIdsCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetFeatureIdsInsideBoundingBoxCore(RectangleShape)`

**Summary**

   *This method returns the Ids of all of the InternalFeatures of this FeatureSource that are inside of the bounding box.*

**Remarks**

   *This method returns the Ids of all of the InternalFeatures of this FeatureSource that are inside of the specified bounding box. If you are overriding this method you will not need to consider anything about transactions, as this is handled by the concrete version of this method. The default implementation of GetFeatureIdsInsideBoundingBoxCore uses the GetfeaturesInsideBoundingBoxCore method to determine which InternalFeatures are inside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. That is especially important for this method, as many other default virtual methods use this for their calculations. When you override this method, we highly recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The Ids of all of the InternalFeatures of this FeatureSource that are inside of the bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatureIds inside of|

---
#### `GetFeaturesByColumnValueCore(String,String,IEnumerable<String>)`

**Summary**

   *Get all of the features by passing a columnName and a specified columValue.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returnning features matches the columnValue.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|The specified columnName to match the columnValue.|
|columnValue|`String`|The specified columnValue to match those returning features.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesByIdsCore(IEnumerable<String>,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures by providing a group of Ids.*

**Remarks**

   *This method returns a collection of InternalFeatures by providing a group of Ids. The internal implementation calls the GetAllFeaturesCore. Because of this, if you want a more efficient version of this method, then we highly suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures by providing a group of Ids.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter represents the group of Ids which uniquely identified the InternalFeatures in the FeatureSource.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesForDrawingCore(RectangleShape,Double,Double,IEnumerable<String>)`

**Summary**

   *This method returns the InternalFeatures that will be used for drawing.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource that are inside of the specified bounding box. If you are overriding this method you will not need to consider anything about transactions, as this is handled by the concrete version of this method. The default implementation of GetFeaturesForDrawingCore uses the GetFeaturesInsodeBoundingBoxCore with some optimizations based on the screen width and height. For example, we can determine if a feature is going to draw in only one to four pixels and in that case we may not draw the entire feature but just a subset of it instead.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns the InternalFeatures that will be used for drawing.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the bounding box of the InternalFeatures that you want to draw.|
|screenWidth|`Double`|This parameter is the width of the view, in screen pixels, that you will draw on.|
|screenHeight|`Double`|This parameter is the height of the view, in screen pixels, that you will draw on.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesInsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures of this FeatureSource that are inside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource that are inside of the specified bounding box. If you are overriding this method you will not need to consider anything about transactions, as this is handled by the concrete version of this method. The default implementation of GetFeaturesInsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are inside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. That is especially important for this method, as many other default virtual methods use this for their calculations. When you override this method, we highly recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returned decimalDegreesValue is a collection of all of the InternalFeatures that are inside of the bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatures inside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data that you wish to return with each Feature.|

---
#### `GetFeaturesNearestFrom(Collection<Feature>,BaseShape,GeographyUnit,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|possibleResults|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|

---
#### `GetFeaturesNearestToCore(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesOutsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box. If you are in a transaction and that transaction is live, this method will also take that into consideration. The default implementation of GetFeaturesOutsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are outside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box that you wish to find InternalFeatures outside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOfCore(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the measurement unit for the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFirstFeaturesWellKnownTypeCore()`

**Summary**

   *This method returns the well known type that represents the first feature from FeatureSource.*

**Remarks**

   *This protected virtual method is called from the concrete public method GetFirstFeaturesWellKnownType. It does not take into account any transaction activity, as this is the responsibility of the concrete public method GetFirstFeaturesWellKnownType. This way, as a developer, if you choose to override this method you do not have to consider transactions at all. The default implementation of GetCountCore uses the GetAllRFeaturesCore method to get WellKnownType of the first feature from all features. We strongly recommend that you provide your own implementation for this method that will be more efficient. If you do not override this method, it will get the count by calling the GetAllFeaturesCore method and get WellKnownType of the first feature from all features. This is a very inefficient way to get the count in most data sources. It is highly recommended that you override this method and replace it with a highly optimized version. For example, in a ShapeFile the record count is in the main header of the file. Similarly, if you are using Oracle Spatial, you can execute a simple query to get the count of all of the records without returning them. In these ways you can greatly improve the performance of this method.*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|This method returns the well known type that represents the first feature from FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetReturningColumnNames(ReturningColumnsType)`

**Summary**

   *Get returning columnNames according to the returningColumnType.*

**Remarks**

   *The concreted FeatureSource can override this logic if needed.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The returning ColumnNames based on the given returningColumnNamesType.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|The passed in returningColumnType.|

---
#### `MemberwiseClone()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `OnClosedFeatureSource(ClosedFeatureSourceEventArgs)`

**Summary**

   *This method allows you to raise the ClosedFeatureSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the ClosedFeatureSource event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event: This event is called after the closing of the FeatureSource. Technically, this event is called after the calling of the Close method on the FeatureSource and after the protected CloseCore method. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ClosedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedFeatureSourceEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnClosingFeatureSource(ClosingFeatureSourceEventArgs)`

**Summary**

   *This method allows you to raise the ClosingFeatureSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the ClosingFeatureSource event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event: This event is called before the closing of the FeatureSource. Technically, this event is called after the calling of the Close method on the FeatureSource, but before the protected CloseCore method. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ClosingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingFeatureSourceEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnCommittedTransaction(CommittedTransactionEventArgs)`

**Summary**

   *This method allows you to raise the CommittedTransaction event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the CommittedTransaction event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event:This event is raised after the CommitTransactionCore is called and allows you access to the TransactionBuffer and the TransactionResults object before CommitTransaction method is returned. With this event, you can analyze the results of the transaction and do any cleanup code necessary. In the event some of the records did not commit, you can handle these items here. The TransactionResults object is passed out of the CommitTransaction method so you could analyze it then; however, this is the only place where you have access to both the TransactionResults object and the TransactionBuffer object at the same time. These are useful together to try and determine what went wrong and possibly try and re-commit them. At the time of this event, you will technically be out of the current transaction.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`CommittedTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittedTransactionEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnCommittingTransaction(CommittingTransactionEventArgs)`

**Summary**

   *This method allows you to raise the CommittingTransaction event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the CommittingTransaction event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event: This event is raised before the CommitTransactionCore is called and allows you access to the TransactionBuffer before the transaction is committed. It also allows you to cancel the pending transaction. The TransactionBuffer is the object that stores all of the pending transactions and is accessible through this event to allow you either add, remove or modify transactions. In the event that you cancel the CommitTransaction method, the transaction remains intact and you will still be editing. This makes it a nice place to possibly check for connectivity before the TransactionCore code is run, which is where the records are actually committed. Calling the RollBackTransaction method is the only way to permanently cancel a pending transaction without committing it.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`CommittingTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittingTransactionEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnCustomColumnFetch(CustomColumnFetchEventArgs)`

**Summary**

   *This method allows you to raise the CustomColumnFetch event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the CustomColumnFetch event. This may be useful if you plan to extend the FeatureSource and you need access to user-definable field data. Details on the event: This event is raised when fields are requested in a feature source method that do not exist in the feature source. It allows you supplement the data from any outside source you may have. It is used primarily when you have data relating to a particular feature or set of features that is not within source of the data. For example, you may have a shape file of the world whose .dbf component describes the area and population of each country. Additionally, in an outside SQL Server table, you may also have data about the countries, and it is this data that you wish to use for determining how you want to color each country. To integrate this SQL data, you simply create a file name that does not exist in the .dbf file.  Whenever Map Suite is queried to return records that specifically require this field, the FeatureSource will raise this event and allow you the developer to supply the data. In this way, you can query the SQL table and store the data in some sort of collection, and then when the event is raised, simply supply that data. As this is an event, it will raise for each feature and field combination requested. This means that the event can be raised quite often, and we suggest that you cache the data you wish to supply in memory. We recommend against sending out a new SQL query each time this event is raised. Image that you are supplementing two columns and your query returns 2,000 rows. This means that if you requested those fields, the event would be raised 4,000 times.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`CustomColumnFetchEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CustomColumnFetchEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnDrawingProgressChanged(DrawingProgressChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)|N/A|

---
#### `OnGettingColumns(GettingColumnsFeatureSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`GettingColumnsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingColumnsFeatureSourceEventArgs.md)|N/A|

---
#### `OnGettingFeaturesByIds(GettingFeaturesByIdsFeatureSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`GettingFeaturesByIdsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingFeaturesByIdsFeatureSourceEventArgs.md)|N/A|

---
#### `OnGettingFeaturesForDrawing(GettingFeaturesForDrawingFeatureSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`GettingFeaturesForDrawingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingFeaturesForDrawingFeatureSourceEventArgs.md)|N/A|

---
#### `OnGottenColumns(GottenColumnsFeatureSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`GottenColumnsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GottenColumnsFeatureSourceEventArgs.md)|N/A|

---
#### `OnOpenedFeatureSource(OpenedFeatureSourceEventArgs)`

**Summary**

   *This method allows you to raise the OpenedFeatureSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the OpenedFeatureSource event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event: This event is called after the opening of the FeatureSource. Technically, this event is called after the calling of the Open method on the FeatureSource and after the protected OpenCore method is called. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OpenedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedFeatureSourceEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnOpeningFeatureSource(OpeningFeatureSourceEventArgs)`

**Summary**

   *This method allows you to raise the OpeningFeatureSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the OpeningFeatureSource event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event: This event is called before the opening of the FeatureSource. Technically, this event is called after the calling of the Open method on the FeatureSource, but before the protected OpenCore method. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OpeningFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningFeatureSourceEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OpenCore()`

**Summary**

   *This method opens the FeatureSource so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concrete public method Open. The Open method plays an important role, as it is responsible for initializing the FeatureSource. Most methods on the FeatureSource will throw an exception if the state of the FeatureSource is not opened. When the map draws each layer, it will open the FeatureSource as one of its first steps, then after it is finished drawing with that layer it will close it. In this way we are sure to release all resources used by the FeatureSource. When implementing this virtual method ,consider opening files for file-based sources, connecting to databases in the database-based sources and so on. You will get a chance to close these in the Close method of the FeatureSource.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ProcessTransaction(RectangleShape,Collection<Feature>,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|returnFeatures|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|needUpdateProjection|`Boolean`|N/A|

---
#### `RaiseCustomColumnFetchEvent(Collection<Feature>,Collection<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeatures|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fieldNamesOutsideOfSource|Collection<`String`>|N/A|

---
#### `SpatialQueryCore(BaseShape,QueryType,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified below. If you override this method, you do not need to consider transactions. It is suggested that if you are looking to speed up this method that you first override the GetFeaturesInsideBoundingBoxCore and then re-test this method, as it relies heavily on that method internally. See more information below.Spatial Query Types:Disjoint - This method returns InternalFeatures where the specific Feature and the targetShape have no points in common.Intersects - This method returns InternalFeatures where the specific Feature and the targetShape have at least one point in common.Touches - This method returns InternalFeatures where the specific Feature and the targetShape have at least one boundary point in common, but no interior points.Crosses - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all interior points.Within - This method returns InternalFeatures where the specific Feature lies within the interior of the targetShape.Contains - This method returns InternalFeatures where the specific Feature lies within the interior of the current shape.Overlaps - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all points in common.TopologicalEqual - This method returns InternalFeatures where the specific Feature and the target Shape are topologically equal. The default implementation of SpatialQueryCore uses the GetFeaturesInsideBoundingBoxCore method to pre-filter the spatial query. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override this method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return decimalDegreesValue is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|This parameter specifies what kind of spatial query you wish to perform.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---

### Public Events

#### DrawingProgressChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)

#### GettingColumns

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`GettingColumnsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingColumnsFeatureSourceEventArgs.md)

#### GottenColumns

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`GottenColumnsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GottenColumnsFeatureSourceEventArgs.md)

#### GettingFeaturesByIds

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`GettingFeaturesByIdsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingFeaturesByIdsFeatureSourceEventArgs.md)

#### GettingFeaturesForDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`GettingFeaturesForDrawingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingFeaturesForDrawingFeatureSourceEventArgs.md)

#### CustomColumnFetch

*N/A*

**Remarks**

*This event is raised when fields are requested in a feature source method that do not exist in the feature source. It allows you to supplement the data from any outside source you have. It is used primarily when you have data relating to a particular feature or set of features that is not within source of the data. For example, you may have a shape file of the world whose .dbf component describes the area and population of each country. Additionally, in an outside SQL Server table, you may also have data about the countries, and it is this data that you wish to use for determining how you want to color each country. To integrate this SQL data, you simply create a file name that does not exist in the .dbf file.  Whenever Map Suite is queried to return records that specifically require this field, the FeatureSource will raise this event and allow you the developer to supply the data. In this way, you can query the SQL table and store the data in some sort of collection, and then when the event is raised, simply supply that data. As this is an event, it will raise for each feature and field combination requested. This means that the event can be raised quite often, and we suggest that you cache the data you wish to supply in memory. We recommend against sending out a new SQL query each time this event is raised. Image that you are supplementing two columns and your query returns 2,000 rows. This means that if you requested those fields, the event would be raised 4,000 times.*

**Event Arguments**

[`CustomColumnFetchEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CustomColumnFetchEventArgs.md)

#### CommittingTransaction

*N/A*

**Remarks**

*This event is raised before the CommitTransactionCore is called and allows you access to the TransactionBuffer before the transaction is committed. It also allows you to cancel the pending transaction. The TransactionBuffer is the object that stores all of the pending transactions and is accessible through this event to allow you to either add, remove or modify transactions. In the event that you cancel the CommitTransaction method, the transaction remains intact and you will still be editing. This makes it a nice place to possibly check for connectivity before the TransactionCore code is run, which is where the records are actually committed. Calling the RollBackTransaction method is the only way to permanently cancel a pending transaction without committing it.*

**Event Arguments**

[`CommittingTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittingTransactionEventArgs.md)

#### CommittedTransaction

*N/A*

**Remarks**

*This event is raised after the CommitTransactionCore is called and allows you access to the TransactionBuffer and the TransactionResults object before CommitTransaction method is returned. With this event, you can analyse the results of the transaction and do any cleanup code necessary. In the event some of the records did not commit, you can handle those items here. The TransactionResults object is passed out of the CommitTransaction method so you could analyze it then; however, this is the only place where you have access to both the TransactionResults object and the TransactionBuffer object at the same time. These are useful together to try and determine what went wrong and possibly try and re-commit them. At the time of this event you will technically be out of the current transaction.*

**Event Arguments**

[`CommittedTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittedTransactionEventArgs.md)

#### OpeningFeatureSource

*N/A*

**Remarks**

*This event is called before the opening of the FeatureSource. Technically, this event is called after the calling of the Open method on the FeatureSource, but before the protected OpenCore method. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`OpeningFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningFeatureSourceEventArgs.md)

#### OpenedFeatureSource

*N/A*

**Remarks**

*This event is called after the opening of the FeatureSource. Technically, this event is called after the calling of the Open method on the FeatureSource and after the protected OpenCore method is called. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`OpenedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedFeatureSourceEventArgs.md)

#### ClosingFeatureSource

*N/A*

**Remarks**

*This event is called before the closing of the FeatureSource. Technically, this event is called after the calling of the Close method on the FeatureSource, but before the protected CloseCore method. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`ClosingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingFeatureSourceEventArgs.md)

#### ClosedFeatureSource

*N/A*

**Remarks**

*This event is called after the closing of the FeatureSource. Technically, this event is called after the calling of the Close method on the FeatureSource and after the protected CloseCore method. It is typical that the FeatureSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a FeatureSource is in the Ajax or Post Back part of the page cycle, it will close the FeatureSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the FeaureSources open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`ClosedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedFeatureSourceEventArgs.md)


