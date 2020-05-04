# WkbFileFeatureSource


## Inheritance Hierarchy

+ `Object`
  + [`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)
    + **`WkbFileFeatureSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`WkbFileFeatureSource()`](#wkbfilefeaturesource)|
|[`WkbFileFeatureSource(String)`](#wkbfilefeaturesourcestring)|
|[`WkbFileFeatureSource(String,FileAccess)`](#wkbfilefeaturesourcestringfileaccess)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanExecuteSqlQuery`](#canexecutesqlquery)|`Boolean`|N/A|
|[`CanModifyColumnStructure`](#canmodifycolumnstructure)|`Boolean`|N/A|
|[`FeatureIdsToExclude`](#featureidstoexclude)|Collection<`String`>|N/A|
|[`GeoCache`](#geocache)|[`FeatureCache`](../ThinkGeo.Core/ThinkGeo.Core.FeatureCache.md)|N/A|
|[`Id`](#id)|`String`|N/A|
|[`IsEditable`](#iseditable)|`Boolean`|N/A|
|[`IsInTransaction`](#isintransaction)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsTransactionLive`](#istransactionlive)|`Boolean`|N/A|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`ReadWriteMode`](#readwritemode)|`FileAccess`|N/A|
|[`SimplificationAreaInPixel`](#simplificationareainpixel)|`Int32`|N/A|
|[`SimplifiedAreas`](#simplifiedareas)|Collection<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|[`TransactionBuffer`](#transactionbuffer)|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|N/A|
|[`WkbPathFilename`](#wkbpathfilename)|`String`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanExecuteSqlQueryCore`](#canexecutesqlquerycore)|`Boolean`|N/A|
|[`CanModifyColumnStructureCore`](#canmodifycolumnstructurecore)|`Boolean`|N/A|
|[`FeatureSourceColumns`](#featuresourcecolumns)|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|N/A|
|[`IsOpenCore`](#isopencore)|`Boolean`|N/A|

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
|[`CreateWkbFile(String,WkbFileType,IEnumerable<FeatureSourceColumn>,IEnumerable<Feature>)`](#createwkbfilestringwkbfiletypeienumerable<featuresourcecolumn>ienumerable<feature>)|
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
|[`GetColumnNamesOutsideFeatureSource(IEnumerable<String>)`](#getcolumnnamesoutsidefeaturesourceienumerable<string>)|
|[`GetColumns()`](#getcolumns)|
|[`GetCount()`](#getcount)|
|[`GetDistinctColumnValues(String)`](#getdistinctcolumnvaluesstring)|
|[`GetFeatureById(String,IEnumerable<String>)`](#getfeaturebyidstringienumerable<string>)|
|[`GetFeatureById(String,ReturningColumnsType)`](#getfeaturebyidstringreturningcolumnstype)|
|[`GetFeatureIds()`](#getfeatureids)|
|[`GetFeatureIdsForDrawing(RectangleShape,Double,Double)`](#getfeatureidsfordrawingrectangleshapedoubledouble)|
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
|[`GetWkbFileType()`](#getwkbfiletype)|
|[`Open()`](#open)|
|[`RefreshColumns()`](#refreshcolumns)|
|[`RemoveEmptyAndExcludedFeatures(Collection<Feature>)`](#removeemptyandexcludedfeaturescollection<feature>)|
|[`RollbackTransaction()`](#rollbacktransaction)|
|[`SpatialQuery(BaseShape,QueryType,IEnumerable<String>)`](#spatialquerybaseshapequerytypeienumerable<string>)|
|[`SpatialQuery(BaseShape,QueryType,ReturningColumnsType)`](#spatialquerybaseshapequerytypereturningcolumnstype)|
|[`SpatialQuery(Feature,QueryType,IEnumerable<String>)`](#spatialqueryfeaturequerytypeienumerable<string>)|
|[`SpatialQuery(Feature,QueryType,ReturningColumnsType)`](#spatialqueryfeaturequerytypereturningcolumnstype)|
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
|[`CommitTransactionCore(TransactionBuffer)`](#committransactioncoretransactionbuffer)|
|[`ExecuteNonQueryCore(String)`](#executenonquerycorestring)|
|[`ExecuteQueryCore(String)`](#executequerycorestring)|
|[`ExecuteScalarCore(String)`](#executescalarcorestring)|
|[`Finalize()`](#finalize)|
|[`GetAllFeaturesCore(IEnumerable<String>)`](#getallfeaturescoreienumerable<string>)|
|[`GetAllFeaturesCore(IEnumerable<String>,Int32,Int32)`](#getallfeaturescoreienumerable<string>int32int32)|
|[`GetBoundingBoxByIdCore(String)`](#getboundingboxbyidcorestring)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetColumnNamesInsideFeatureSource(IEnumerable<String>)`](#getcolumnnamesinsidefeaturesourceienumerable<string>)|
|[`GetColumnNamesOutsideFeatureSourceCall(IEnumerable<String>)`](#getcolumnnamesoutsidefeaturesourcecallienumerable<string>)|
|[`GetColumnsCore()`](#getcolumnscore)|
|[`GetCountCore()`](#getcountcore)|
|[`GetDistinctColumnValuesCore(String)`](#getdistinctcolumnvaluescorestring)|
|[`GetFeatureIdsCore()`](#getfeatureidscore)|
|[`GetFeatureIdsForDrawingCore(RectangleShape,Double,Double)`](#getfeatureidsfordrawingcorerectangleshapedoubledouble)|
|[`GetFeatureIdsInsideBoundingBoxCore(RectangleShape)`](#getfeatureidsinsideboundingboxcorerectangleshape)|
|[`GetFeaturesByColumnValueCore(String,String,IEnumerable<String>)`](#getfeaturesbycolumnvaluecorestringstringienumerable<string>)|
|[`GetFeaturesByIdsCore(IEnumerable<String>,IEnumerable<String>)`](#getfeaturesbyidscoreienumerable<string>ienumerable<string>)|
|[`GetFeaturesForDrawingCore(RectangleShape,Double,Double,IEnumerable<String>)`](#getfeaturesfordrawingcorerectangleshapedoubledoubleienumerable<string>)|
|[`GetFeaturesInsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`](#getfeaturesinsideboundingboxcorerectangleshapeienumerable<string>)|
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
|[`WkbFileFeatureSource()`](#wkbfilefeaturesource)|
|[`WkbFileFeatureSource(String)`](#wkbfilefeaturesourcestring)|
|[`WkbFileFeatureSource(String,FileAccess)`](#wkbfilefeaturesourcestringfileaccess)|

### Protected Constructors


### Public Properties

#### `CanExecuteSqlQuery`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `GeoCache`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsInTransaction`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsOpen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsTransactionLive`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)

---
#### `ReadWriteMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FileAccess`

---
#### `SimplificationAreaInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `SimplifiedAreas`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>

---
#### `TransactionBuffer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)

---
#### `WkbPathFilename`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---

### Protected Properties

#### `CanExecuteSqlQueryCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>

---
#### `IsOpenCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `AddFeature(BaseShape)`

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Close()`

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
#### `CommitTransaction()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TransactionResult`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CreateWkbFile(String,WkbFileType,IEnumerable<FeatureSourceColumn>,IEnumerable<Feature>)`

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
|pathFilename|`String`|N/A|
|wkbFileType|[`WkbFileType`](../ThinkGeo.Core/ThinkGeo.Core.WkbFileType.md)|N/A|
|columns|IEnumerable<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|N/A|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

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
|id|`String`|N/A|

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
|sqlStatement|`String`|N/A|

---
#### `ExecuteQuery(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DataTable`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|N/A|

---
#### `ExecuteScalar(String)`

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
|sqlStatement|`String`|N/A|

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

---
#### `GetAllFeatures(ReturningColumnsType)`

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
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetBoundingBox()`

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
|N/A|N/A|N/A|

---
#### `GetBoundingBoxById(String)`

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
#### `GetBoundingBoxByIds(IEnumerable<String>)`

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
|ids|IEnumerable<`String`>|N/A|

---
#### `GetBoundingBoxesByIds(IEnumerable<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|N/A|

---
#### `GetColumnNamesOutsideFeatureSource(IEnumerable<String>)`

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
#### `GetColumns()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCount()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|N/A|

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeatureById(String,ReturningColumnsType)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

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
#### `GetFeatureIdsForDrawing(RectangleShape,Double,Double)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|

---
#### `GetFeatureIdsInsideBoundingBox(RectangleShape)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetFeaturesByColumnValue(String,String,ReturningColumnsType)`

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
|columnName|`String`|N/A|
|columnValue|`String`|N/A|
|returningColumnType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesByColumnValue(String,String,IEnumerable<String>)`

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
|columnName|`String`|N/A|
|columnValue|`String`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesByColumnValue(String,String)`

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
|columnName|`String`|N/A|
|columnValue|`String`|N/A|

---
#### `GetFeaturesByIds(IEnumerable<String>,IEnumerable<String>)`

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
|ids|IEnumerable<`String`>|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesByIds(IEnumerable<String>,ReturningColumnsType)`

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
|ids|IEnumerable<`String`>|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesForDrawing(RectangleShape,Double,Double,IEnumerable<String>)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesForDrawing(RectangleShape,Double,Double,ReturningColumnsType)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesInsideBoundingBox(RectangleShape,IEnumerable<String>)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesInsideBoundingBox(RectangleShape,ReturningColumnsType)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfFeatureSource|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,ReturningColumnsType)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,ReturningColumnsType)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|
|searchRadius|`Double`|N/A|
|unitOfSearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|
|searchRadius|`Double`|N/A|
|unitOfSearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetFeaturesOutsideBoundingBox(RectangleShape,IEnumerable<String>)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesOutsideBoundingBox(RectangleShape,ReturningColumnsType)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distance|`Double`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distance|`Double`|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distance|`Double`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distance|`Double`|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `GetFirstFeaturesWellKnownType()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|N/A|

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
#### `GetWkbFileType()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`WkbFileType`](../ThinkGeo.Core/ThinkGeo.Core.WkbFileType.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Open()`

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
#### `RefreshColumns()`

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
#### `SpatialQuery(BaseShape,QueryType,IEnumerable<String>)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `SpatialQuery(BaseShape,QueryType,ReturningColumnsType)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

---
#### `SpatialQuery(Feature,QueryType,IEnumerable<String>)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `SpatialQuery(Feature,QueryType,ReturningColumnsType)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|N/A|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `UpdateFeature(BaseShape)`

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloseCore()`

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
#### `CommitTransactionCore(TransactionBuffer)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TransactionResult`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|transactions|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|N/A|

---
#### `ExecuteNonQueryCore(String)`

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
|sqlStatement|`String`|N/A|

---
#### `ExecuteQueryCore(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DataTable`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|N/A|

---
#### `ExecuteScalarCore(String)`

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
|sqlStatement|`String`|N/A|

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
#### `GetAllFeaturesCore(IEnumerable<String>)`

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
|N/A|N/A|N/A|

---
#### `GetColumnNamesInsideFeatureSource(IEnumerable<String>)`

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCountCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|N/A|

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
#### `GetFeatureIdsForDrawingCore(RectangleShape,Double,Double)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|

---
#### `GetFeatureIdsInsideBoundingBoxCore(RectangleShape)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetFeaturesByColumnValueCore(String,String,IEnumerable<String>)`

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
|columnName|`String`|N/A|
|columnValue|`String`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesByIdsCore(IEnumerable<String>,IEnumerable<String>)`

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
|ids|IEnumerable<`String`>|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesForDrawingCore(RectangleShape,Double,Double,IEnumerable<String>)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesInsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesNearestToCore(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|maxItemsToFind|`Int32`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesOutsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFeaturesWithinDistanceOfCore(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distance|`Double`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

---
#### `GetFirstFeaturesWellKnownTypeCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetReturningColumnNames(ReturningColumnsType)`

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
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|

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
|e|[`ClosedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedFeatureSourceEventArgs.md)|N/A|

---
#### `OnClosingFeatureSource(ClosingFeatureSourceEventArgs)`

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
|e|[`ClosingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingFeatureSourceEventArgs.md)|N/A|

---
#### `OnCommittedTransaction(CommittedTransactionEventArgs)`

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
|e|[`CommittedTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittedTransactionEventArgs.md)|N/A|

---
#### `OnCommittingTransaction(CommittingTransactionEventArgs)`

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
|e|[`CommittingTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittingTransactionEventArgs.md)|N/A|

---
#### `OnCustomColumnFetch(CustomColumnFetchEventArgs)`

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
|e|[`CustomColumnFetchEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CustomColumnFetchEventArgs.md)|N/A|

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
|e|[`OpenedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedFeatureSourceEventArgs.md)|N/A|

---
#### `OnOpeningFeatureSource(OpeningFeatureSourceEventArgs)`

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
|e|[`OpeningFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningFeatureSourceEventArgs.md)|N/A|

---
#### `OpenCore()`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|

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

*N/A*

**Event Arguments**

[`CustomColumnFetchEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CustomColumnFetchEventArgs.md)

#### CommittingTransaction

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`CommittingTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittingTransactionEventArgs.md)

#### CommittedTransaction

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`CommittedTransactionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.CommittedTransactionEventArgs.md)

#### OpeningFeatureSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OpeningFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningFeatureSourceEventArgs.md)

#### OpenedFeatureSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OpenedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedFeatureSourceEventArgs.md)

#### ClosingFeatureSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClosingFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingFeatureSourceEventArgs.md)

#### ClosedFeatureSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClosedFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedFeatureSourceEventArgs.md)


