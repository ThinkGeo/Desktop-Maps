# TinyGeoFeatureSource


## Inheritance Hierarchy

+ `Object`
  + [`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)
    + **`TinyGeoFeatureSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`TinyGeoFeatureSource()`](#tinygeofeaturesource)|
|[`TinyGeoFeatureSource(String)`](#tinygeofeaturesourcestring)|
|[`TinyGeoFeatureSource(String,String)`](#tinygeofeaturesourcestringstring)|

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
|[`Password`](#password)|`String`|This property gets or sets the password of the TinyGeo file.|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`TinyGeoPathFilename`](#tinygeopathfilename)|`String`|This property returns the path and file of the TinyGeo file you want to use.|
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
|[`CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType)`](#createtinygeofilestringstringgeographyunitreturningcolumnstype)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType,Double)`](#createtinygeofilestringstringgeographyunitreturningcolumnstypedouble)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType,String)`](#createtinygeofilestringstringgeographyunitreturningcolumnstypestring)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType,String,Double)`](#createtinygeofilestringstringgeographyunitreturningcolumnstypestringdouble)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>)`](#createtinygeofilestringstringgeographyunitienumerable<string>)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,Double)`](#createtinygeofilestringstringgeographyunitienumerable<string>double)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,String)`](#createtinygeofilestringstringgeographyunitienumerable<string>string)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,String,Double)`](#createtinygeofilestringstringgeographyunitienumerable<string>stringdouble)|
|[`CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,String,Double,Encoding)`](#createtinygeofilestringstringgeographyunitienumerable<string>stringdoubleencoding)|
|[`CreateTinyGeoFile(String,FeatureLayer,GeographyUnit,IEnumerable<String>,String,Double,Encoding,WellKnownType)`](#createtinygeofilestringfeaturelayergeographyunitienumerable<string>stringdoubleencodingwellknowntype)|
|[`DecryptTinyGeoFile(String,String,String)`](#decrypttinygeofilestringstringstring)|
|[`DeleteColumn(String)`](#deletecolumnstring)|
|[`DeleteFeature(String)`](#deletefeaturestring)|
|[`EncryptTinyGeoFile(String,String,String)`](#encrypttinygeofilestringstringstring)|
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
|[`GetOptimalPrecision(FeatureLayer,GeographyUnit,DistanceUnit,TinyGeoPrecisionMode)`](#getoptimalprecisionfeaturelayergeographyunitdistanceunittinygeoprecisionmode)|
|[`GetOptimalPrecision(String,GeographyUnit,DistanceUnit,TinyGeoPrecisionMode)`](#getoptimalprecisionstringgeographyunitdistanceunittinygeoprecisionmode)|
|[`GetTinyGeoFileType()`](#gettinygeofiletype)|
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
|[`OnStreamLoading(StreamLoadingEventArgs)`](#onstreamloadingstreamloadingeventargs)|
|[`OpenCore()`](#opencore)|
|[`ProcessTransaction(RectangleShape,Collection<Feature>,Boolean)`](#processtransactionrectangleshapecollection<feature>boolean)|
|[`RaiseCustomColumnFetchEvent(Collection<Feature>,Collection<String>)`](#raisecustomcolumnfetcheventcollection<feature>collection<string>)|
|[`SpatialQueryCore(BaseShape,QueryType,IEnumerable<String>)`](#spatialquerycorebaseshapequerytypeienumerable<string>)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`StreamLoading`](#streamloading)|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|
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
|[`TinyGeoFeatureSource()`](#tinygeofeaturesource)|
|[`TinyGeoFeatureSource(String)`](#tinygeofeaturesourcestring)|
|[`TinyGeoFeatureSource(String,String)`](#tinygeofeaturesourcestringstring)|

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
#### `Password`

**Summary**

   *This property gets or sets the password of the TinyGeo file.*

**Remarks**

   *N/A*

**Return Value**

`String`

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
#### `TinyGeoPathFilename`

**Summary**

   *This property returns the path and file of the TinyGeo file you want to use.*

**Remarks**

   *When you specify the path and file name it should be in the correct format as such however the file does not need to exists on the file system. This is to allow us to accept streams supplied by the developer at runtime. If you choose to provide a file that exists then we will attempt to use it. If we cannot find it then we will raise the SteamLoading event and allow you to supply the stream. For example you can pass in "C:\NotARealPath\File1.tgeo" which does not exists on the file system. When we raise the event for you to supply a stream we will pass to you the path and file name for you to differentiate the files.*

**Return Value**

`String`

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
#### `CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|returningColumnType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies whether the columns info in shape file will be copied to TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType,Double)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|returningColumnType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies whether the columns info in shape file will be copied to TinyGeo file.|
|precisionInMeter|`Double`|This parameter spcifies in double what is the precision in Meter of the target TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType,String)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|returningColumnType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies whether the columns info in shape file will be copied to TinyGeo file.|
|password|`String`|This parameter spcifies the password of the target TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,ReturningColumnsType,String,Double)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|returningColumnType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies whether the columns info in shape file will be copied to TinyGeo file.|
|password|`String`|This parameter spcifies the password of the target TinyGeo file.|
|precisionInMeter|`Double`|This parameter spcifies in double what is the precision in Meter of the target TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|columnNames|IEnumerable<`String`>|This parameter specifies the columns in shape file which will be copied to TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,Double)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|columnNames|IEnumerable<`String`>|This parameter specifies the columns in shape file which will be copied to TinyGeo file.|
|precisionInMeter|`Double`|This parameter spcifies in double what is the precision in Meter of the target TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,String)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|columnNames|IEnumerable<`String`>|This parameter specifies the columns in shape file which will be copied to TinyGeo file.|
|password|`String`|This parameter spcifies the password of the target TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,String,Double)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|columnNames|IEnumerable<`String`>|This parameter specifies the columns in shape file which will be copied to TinyGeo file.|
|password|`String`|This parameter spcifies the password of the target TinyGeo file.|
|precisionInMeter|`Double`|This parameter spcifies in double what is the precision in Meter of the target TinyGeo file.|

---
#### `CreateTinyGeoFile(String,String,GeographyUnit,IEnumerable<String>,String,Double,Encoding)`

**Summary**

   *Static API to create a new TinyGeo file from an existed shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tinyGeoPathFilename|`String`|This parameter specifies the file name for the target TinyGeo file.|
|shapePathFilename|`String`|This parameter specifies the file name for the existed shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit of the data.|
|columnNames|IEnumerable<`String`>|This parameter specifies the columns in shape file which will be copied to TinyGeo file.|
|password|`String`|This parameter spcifies the password of the target TinyGeo file.|
|precisionInMeter|`Double`|This parameter spcifies in double what is the precision in Meter of the target TinyGeo file.|
|shapeEncoding|`Encoding`|This parameter specifies the encoding of the existed shape file.|

---
#### `CreateTinyGeoFile(String,FeatureLayer,GeographyUnit,IEnumerable<String>,String,Double,Encoding,WellKnownType)`

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
|tinyGeoPathFilename|`String`|N/A|
|featureLayer|[`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|columnNames|IEnumerable<`String`>|N/A|
|password|`String`|N/A|
|precisionInMeter|`Double`|N/A|
|shapeEncoding|`Encoding`|N/A|
|type|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|N/A|

---
#### `DecryptTinyGeoFile(String,String,String)`

**Summary**

   *Decrypt an existed encrypted TinyGeo File and save it as a new TinyGeo File.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|encryptedTinyGeoPathFilename|`String`|This parameter specifies the file name for the source encrypted TinyGeo file.|
|decryptedTinyGeoPathFilename|`String`|This parameter specifies the file name for the target decrypted TinyGeo file.|
|password|`String`|This parameter specified the password of the source encrypted TinyGeo file.|

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
#### `EncryptTinyGeoFile(String,String,String)`

**Summary**

   *Encrypt an existed TinyGeo File and save it as a new TinyGeo File.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|unencryptedTinyGeoPathFilename|`String`|This parameter specifies the file name for the source unencrypted TinyGeo file.|
|encryptedTinyGeoPathFilename|`String`|This parameter specifies the file name for the target encrypted TinyGeo file.|
|password|`String`|This parameter specified the password of the target encrypted TinyGeo file.|

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
#### `GetOptimalPrecision(FeatureLayer,GeographyUnit,DistanceUnit,TinyGeoPrecisionMode)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureLayer|[`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|returningDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|precisionMode|[`TinyGeoPrecisionMode`](../ThinkGeo.Core/ThinkGeo.Core.TinyGeoPrecisionMode.md)|N/A|

---
#### `GetOptimalPrecision(String,GeographyUnit,DistanceUnit,TinyGeoPrecisionMode)`

**Summary**

   *Get the precision of a shape file for creating a tinyGeo file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter specifies the file name for the shape file.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the Geography Unit for the shape file.|
|returningDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the Distance Unit for the return precision.|
|precisionMode|[`TinyGeoPrecisionMode`](../ThinkGeo.Core/ThinkGeo.Core.TinyGeoPrecisionMode.md)|This parameter specifies the mode of getting the return precision.|

---
#### `GetTinyGeoFileType()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TinyGeoFileType`](../ThinkGeo.Core/ThinkGeo.Core.TinyGeoFileType.md)|N/A|

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

   *This method opens the FeatureSource so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concreate public method Close. The close method plays an important role in the life cycle of the FeatureSource. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. It is recommended that if you override this method that you take the following things into account. This method may be called multiple times so we suggest you write the so that that a call to a closed FeatureSource is ignored and does not generate an error. We also suggest that in the close you free all resources that have been opened. Remember that the object will not be destroyed but will be re-opened possibly in the near future.*

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

   *This method returns all of the InternalFeatures in the FeatureSource.*

**Remarks**

   *This method returns all of the InternalFeatures in the FeatureSource. You will not need to consider anything about pending transactions as this will be handled in the non Core version of the method. The main purpose of this method is to be the anchor of all of our default virtual implementations within this class. We wanted as the framework developers to provide you the user with as much default virtual implementation as possible. To do this we needed a way to get access to all of the features. For example, we want to create a default implementation for finding all of the InternalFeatures in a bounding box. Because this is an abstract class we do not know the specifics of the underlying data or how its spatial indexes work. What we do know is that if we get all the records then we can brute force the answer. In this way if you inherited form this class and only implemented this one method we can provide default implementations for virtually every other API. While this is nice for you the developer if you decide to create your own FeatureSource it comes with a price. The price is that it is very inefficient. In the case we just discussed about finding all of the InternalFeatures in a bounding box we would not want to look at every record to fulfil this method. Instead we would want to override the GetFeaturesInsideBoundingBoxCore and implement specific code that would be fast. For example in Oracle Spatial there is a specific SQL statement to do this operation very quickly. The same holds true with other specific FeatureSource examples. Most default implementations in the FeatureSource call the GetFeaturesInsideBoundingBoxCore which by default calls the GetAllFeaturesCore. It is our advice that if you create your own FeatureSource that you ALWAYS override the GetFeatureInsideBoundingBox. It will ensure that nearly every other API will operate efficiently. Please see the specific API to determine what method it uses.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return decimalDegreesValue is a collection of all of the InternalFeatures in the FeatureSource.|

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

   *This method returns the bounding box which encompasses all of the features in the FeatureSource.*

**Remarks**

   *This protected virtual method is called from the concreate public method GetBoundingBox. It does not take into account any transaction activity as this is the responsibility of the concreate public method GetBoundingBox. In this way as a developer if you choose to override this method you do not have to consider transaction at all. The default implementation of GetBoundingBoxCore uses the GetAllRecordsCore method to calculate the bounding box of the FeatureSource. We strongly recommend that you provide your own implementation for this method that will be more efficient If you do not override this method the means it gets the BoundingBox is by calling the GetAllFeatureCore method and deriving it from each feature. This is a very inefficient way to get the BoundingBox in most data sources. It is highly recommended that you override this method and replace it with a highly optimized version. For example in a ShapeFile the BoundingBox is in the main header of the file. Similarly if you are using Oracle Spatial you can execute a simple query to get the BoundingBox of all of the record without returning them. In these ways you can greatly improve the performance of this method.*

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

   *This method returns the columns available for the FeatureSource.*

**Remarks**

   *As this is the abstract core version of the Columns method it is intended to be overridden in inherited version of the class. When overriding you will be responsible for getting a list of all of the columns supported by the FeatureSource. In this way the FeatureSource will know what columns are available and will remove any extra columns when making calls to other core methods. For example if you have a FeatureSource that has three columns of information and the user calls a method and requests four columns of information, something they can do with custom fields, we will first compare what they are asking for to the results of the GetColumnsCore. In this way we can strip out custom columns before calling other Core methods which are only responsible for returning data in the FeatureSource. For more information on custom fields you can see the documentation on the OnCustomFieldsFetch.*

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

   *This method returns all of the InternalFeatures of this FeatureSource inside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource inside of the specified bounding box. If you are overriding this method you will not need to consider anything about transactions as this is handled by the concreate version of this method. The default implementation of GetFeaturesInsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are inside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient. It is especially important for this method as many other default virtual methods use this for their calculations. We highly recommend when you override this method that you use any spatial indexes you have at your disposal to make this method as fast as possible.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return decimalDegreesValue is a collection of all of the InternalFeatures that are inside of the bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box you with to find InternalFeatures inside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

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
#### `OnStreamLoading(StreamLoadingEventArgs)`

**Summary**

   *This method allows you to raise the StreamLoading event.*

**Remarks**

   *This method allows you to raise the StreamLoading event. Normally events are not accessible to derived classes so we exposed a way to raise the event is necessary through this protected method.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|This parameter represents the event arguments you want to raise the StreamLoading event with.|

---
#### `OpenCore()`

**Summary**

   *This method opens the FeatureSource so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concreate public method Open. The open method play an important role as it is responsible for initializing the FeatureSource. Most methods on the FeatureSource will throw an exception if the state of the FeatureSource is not opened. When the map draws each layer it will open the FeatureSource as one of its first steps, then after it is finished drawing with that layer it will close it. In this way we are sure to release all resources used by the FeatureSource. When implementing this abstract method consider opening files for file based source, connecting to databases in the database based sources and so on. You will get a chance to close these in the Close method of the FeatureSource.*

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

#### StreamLoading

*N/A*

**Remarks**

*If you choose you can pass in your own stream to represent the file. The stream can come from a variety of places such as isolated storage, a compressed file, and encrypted stream. When the Image is finished with the stream it will dispose of it so be sure to keep this in mind when passing the stream in. If you do not pass in a alternate stream the class will attempt to load the file from the file system using the TinyGeoPathFilename property.*

**Event Arguments**

[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)

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


