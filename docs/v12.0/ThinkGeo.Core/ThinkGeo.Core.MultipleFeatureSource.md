# MultipleFeatureSource


## Inheritance Hierarchy

+ `Object`
  + [`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)
    + **`MultipleFeatureSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MultipleFeatureSource()`](#multiplefeaturesource)|
|[`MultipleFeatureSource(IEnumerable<FeatureSource>)`](#multiplefeaturesourceienumerable<featuresource>)|

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
|[`FeatureSources`](#featuresources)|Collection<[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)>|This property specify the FeatureSource collection within the MultipleFeatureSource.|
|[`GeoCache`](#geocache)|[`FeatureCache`](../ThinkGeo.Core/ThinkGeo.Core.FeatureCache.md)|N/A|
|[`Id`](#id)|`String`|N/A|
|[`IsEditable`](#iseditable)|`Boolean`|This property returns if the FeatureSource allows edits or is read only.|
|[`IsInTransaction`](#isintransaction)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsTransactionLive`](#istransactionlive)|`Boolean`|N/A|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`TransactionBuffer`](#transactionbuffer)|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|N/A|

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
|[`MultipleFeatureSource()`](#multiplefeaturesource)|
|[`MultipleFeatureSource(IEnumerable<FeatureSource>)`](#multiplefeaturesourceienumerable<featuresource>)|

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
#### `FeatureSources`

**Summary**

   *This property specify the FeatureSource collection within the MultipleFeatureSource.*

**Remarks**

   *N/A*

**Return Value**

Collection<[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)>

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

   *This property returns if the FeatureSource allows edits or is read only.*

**Remarks**

   *This property is useful to check if a specific FeatureSource accepts editing. If you call the BeginTransaction and this property is false then an exception will be raised. For developers who are creating or extending a FeatureSource it is expected that you override this virtual method if the new FeatureSource you are creating allows edits. By default the decimalDegreesValue if false meaning that if you want to allow edits you must override this method and return true.*

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
#### `TransactionBuffer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)

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

   *This method closes the FeatureSource and releases any resources it was using.*

**Remarks**

   *This API will close all FeatureSource included in this MultipleFEatureSource.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CommitTransactionCore(TransactionBuffer)`

**Summary**

   *This API is not supported in this concrete FeatureSource: MultipleFeatureSource.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TransactionResult`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResult.md)|The return decimalDegreesValue of this method is a TransactionResult class which gives you the status of the transaction you just committed. It includes how many of the updates, adds, and deletes were successful and any error that were encountered during the committing of the transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|transactions|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|This parameter encapsulates all of the adds, edits and deleted that make up the transaction. You will use this data to write the changes to your underlying data source.|

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

   *This method returns all of the InternalFeatures in the MutipleFeatureSource.*

**Remarks**

   *This returning collection of Features will include all the features counting all the FeatureSources in this MultipleFeautureSource.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of all of the InternalFeatures in the MutipleFeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

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

   *This method returns the bounding box which encompasses all of the FeatureSources in the MutlpleFeatureSource.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box which encompasses all of the FeatureSources in the MutlpleFeatureSource.|

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

   *This method returns the columns available for the FeatureSources within this MultipleFeatureSource.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|This method returns the columns available for the FeatureSources within this MultipleFeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCountCore()`

**Summary**

   *This method returns the count of the number of records in this FeatureSource.*

**Remarks**

   *This returning features count stands for the total count in all FeatureSource included in this MultipleFeatureSource.*

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

   *This method returns a collection of Features by providing a group of Ids.*

**Remarks**

   *This returning collection of Features will include all the features with the passed in Ids insides all the FeatureSources in this MultipleFeautureSource.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of Features by providing a group of Ids.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter represents the group of Ids which uniquely identified the Features in the FeatureSource.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesForDrawingCore(RectangleShape,Double,Double,IEnumerable<String>)`

**Summary**

   *This method returns the InternalFeatures that will be used for drawing.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSources with the MultipleFeatureSource in this MultipleFeautureSource. inside of the specified bounding box.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns the InternalFeatures that will be used for drawing.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the bounding box of the InternalFeatures you want to draw.|
|screenWidth|`Double`|This parameter is the width in screen pixels of the view you will draw on.|
|screenHeight|`Double`|This parameter is the height in screen pixels of the view you will draw on.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesInsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures of this MultipleFeatureSource inside of the specified bounding box.*

**Remarks**

   *This returning collection of Features will include all the features insides all the FeatureSources in this MultipleFeautureSource.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box you with to find InternalFeatures inside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesNearestToCore(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`

**Summary**

   *This method will get a user defined number of Features that are closest to the TargetShape from all the FeatureSources within the MutlipleFeatureSource.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape from all the FeatureSources within the MutlipleFeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you should to find close InternalFeatures to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of what the TargetShape and the FeatureSource is in such as feet, meters etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesOutsideBoundingBoxCore(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box from all the FeatureSources within the MutlipleFeatureSource.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box. If you are in a transaction and that transaction is live then it will also take that into consideration.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns all of the Features of this FeatureSource outside of the specified bounding box from all the FeatureSources within the MutlipleFeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box you with to find InternalFeatures outside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOfCore(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.This query will apply to all featureSource within this MultipleFeatureSource.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource use the same unit such as feet or meters. If they do not then the results will not be predictable or correct. If there is a current transaction and it is marked as live then the results will include any transaction Feature that applies. The implementation we provided create a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not the most efficient method for this operation. If you underlying data provider exposes a more efficient way we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. We recommend when you override GetFeaturesInsideBoundingBoxCore method that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the unit of the distance parameter such as feet, miles or kilometers etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

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

   *This method opens the FeatureSource so that it is initialized and ready to use.*

**Remarks**

   *This API will open all FeatureSource included in this MultipleFEatureSource.*

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

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified below. This spatial query will apply to all featureSource within this MultipleFeatureSource. Spatial Query Types:Disjoint - This method returns InternalFeatures where the specific Feature and the targetShape have no points in common.Intersects - This method returns InternalFeatures where the specific Feature and the targetShape have at least one point in common.Touches - This method returns InternalFeatures where the specific Feature and the targetShape have at least one boundary point in common, but no interior points.Crosses - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all interior points.Within - This method returns InternalFeatures where the specific Feature lies within the interior of the targetShape.Contains - This method returns InternalFeatures where the specific Feature lies within the interior of the current shape.Overlaps - This method returns InternalFeatures where the specific Feature and the targetShape share some but not all points in common.TopologicalEqual - This method returns InternalFeatures where the specific Feature and the target Shape are topologically equal. The default implementation of SpatialQueryCore uses the GetFeaturesInsideBoundingBoxCore method to pre-filter the spatial query. We strongly recommend that you provide your own implementation for this method that will be more efficient. We recommend when you override that method that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of Features that match the spatial query you executed based on the TargetShape.|

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


