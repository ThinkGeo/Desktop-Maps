# WfsFeatureSource


## Inheritance Hierarchy

+ `Object`
  + [`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)
    + **`WfsFeatureSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`WfsFeatureSource()`](#wfsfeaturesource)|
|[`WfsFeatureSource(String,String)`](#wfsfeaturesourcestringstring)|

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
|[`LastXmlResponse`](#lastxmlresponse)|`String`|The xml text represnets last respone, it will pass out by RequestedData event as parameter.|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`ServiceLocationUrl`](#servicelocationurl)|`String`|The url of wfs service.|
|[`TimeoutInSeconds`](#timeoutinseconds)|`Int32`|This property specifies the timeout of the web request in seconds.  The default timeout value is 20 seconds.|
|[`TransactionBuffer`](#transactionbuffer)|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|N/A|
|[`TypeName`](#typename)|`String`|The typename in the specify wfs service.|
|[`WebProxy`](#webproxy)|`IWebProxy`|This property gets or sets the proxy used for requesting a Web Response.|
|[`WfsNamespace`](#wfsnamespace)|[`WfsNamespace`](../ThinkGeo.Core/ThinkGeo.Core.WfsNamespace.md)|Add ogc as prefix to in some cases, currently it is only works in API GetFeaturesByColumnValue.|

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
|[`GetCapabilities(String)`](#getcapabilitiesstring)|
|[`GetCapabilities(Uri)`](#getcapabilitiesuri)|
|[`GetCapabilities(Uri,IWebProxy)`](#getcapabilitiesuriiwebproxy)|
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
|[`GetLayers(String)`](#getlayersstring)|
|[`GetLayers(Uri)`](#getlayersuri)|
|[`GetLayers(Uri,IWebProxy)`](#getlayersuriiwebproxy)|
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
|[`OnRequestedData(RequestedDataWfsFeatureSourceEventArgs)`](#onrequesteddatarequesteddatawfsfeaturesourceeventargs)|
|[`OnRequestingData(RequestingDataWfsFeatureSourceEventArgs)`](#onrequestingdatarequestingdatawfsfeaturesourceeventargs)|
|[`OnSendingWebRequest(SendingWebRequestEventArgs)`](#onsendingwebrequestsendingwebrequesteventargs)|
|[`OnSentWebRequest(SentWebRequestEventArgs)`](#onsentwebrequestsentwebrequesteventargs)|
|[`OpenCore()`](#opencore)|
|[`ProcessTransaction(RectangleShape,Collection<Feature>,Boolean)`](#processtransactionrectangleshapecollection<feature>boolean)|
|[`RaiseCustomColumnFetchEvent(Collection<Feature>,Collection<String>)`](#raisecustomcolumnfetcheventcollection<feature>collection<string>)|
|[`SendWebRequest(WebRequest)`](#sendwebrequestwebrequest)|
|[`SendWebRequestCore(WebRequest)`](#sendwebrequestcorewebrequest)|
|[`SpatialQueryCore(BaseShape,QueryType,IEnumerable<String>)`](#spatialquerycorebaseshapequerytypeienumerable<string>)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`SendingWebRequest`](#sendingwebrequest)|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|
|[`SentWebRequest`](#sentwebrequest)|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|
|[`RequestingData`](#requestingdata)|[`RequestingDataWfsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDataWfsFeatureSourceEventArgs.md)|N/A|
|[`RequestedData`](#requesteddata)|[`RequestedDataWfsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDataWfsFeatureSourceEventArgs.md)|N/A|
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
|[`WfsFeatureSource()`](#wfsfeaturesource)|
|[`WfsFeatureSource(String,String)`](#wfsfeaturesourcestringstring)|

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
#### `LastXmlResponse`

**Summary**

   *The xml text represnets last respone, it will pass out by RequestedData event as parameter.*

**Remarks**

   *N/A*

**Return Value**

`String`

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
#### `ServiceLocationUrl`

**Summary**

   *The url of wfs service.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `TimeoutInSeconds`

**Summary**

   *This property specifies the timeout of the web request in seconds.  The default timeout value is 20 seconds.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `TransactionBuffer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)

---
#### `TypeName`

**Summary**

   *The typename in the specify wfs service.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `WebProxy`

**Summary**

   *This property gets or sets the proxy used for requesting a Web Response.*

**Remarks**

   *N/A*

**Return Value**

`IWebProxy`

---
#### `WfsNamespace`

**Summary**

   *Add ogc as prefix to in some cases, currently it is only works in API GetFeaturesByColumnValue.*

**Remarks**

   *N/A*

**Return Value**

[`WfsNamespace`](../ThinkGeo.Core/ThinkGeo.Core.WfsNamespace.md)

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
#### `GetCapabilities(String)`

**Summary**

   *Get capabilities from the specific wfs service url.*

**Remarks**

   *Every OGC Web Service (OWS), including a Web Feature Service, must have the ability to describe its capabilities by returning service metadata in response to a GetCapabilities request. Specifically, every web feature service must support the KVP encoded form of the GetCapabilities request over HTTP GET so that a client can always know how to obtain a capabilities document. The capabilities response document contains the following sections: 1. Service Identification section The service identification section provides information about the WFS service itself. 2. Service Provider section The service provider section provides metadata about the organization operating the WFS server. 3. Operation Metadata section The operations metadata section provides metadata about the operations defined in this specification and implemented by a particular WFS server. This metadata includes the DCP, parameters and constraints for each operation. 4. FeatureType list section This section defines the list of feature types (and operations on each feature type) that are available from a web feature service. Additional information, such as the default SRS, any other supported SRSs, or no SRS whatsoever (for non-spatial feature types), for WFS requests is provided for each feature type. 5. ServesGMLObjectType list section This section defines the list of GML Object types, not derived from gml:AbstractFeatureType, that are available from a web feature service that supports the GetGMLObject operation. These types may be defined in a base GML schema, or in an application schema using its own namespace. 6. SupportsGMLObjectType list section The Supports GML Object Type section defines the list of GML Object types that a WFS server would be capable of serving if it was deployed to serve data. described by an application schema that either used those GML Object types directly (for non-abstract types), or defined derived types based on those types. 7. Filter capabilities section The schema of the Filter Capabilities Section is defined in the Filter Encoding Implementation Specification [3]. This is an optional section. If it exists, then the WFS should support the operations advertised therein. If the Filter Capabilities Section is not defined, then the client should assume that the server only supports the minimum default set of filter operators.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The xml text represents capabilities of this wfs server.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serviceLocationUrl|`String`|The url of wfs service.|

---
#### `GetCapabilities(Uri)`

**Summary**

   *Get capabilities from the specific wfs service url.*

**Remarks**

   *Every OGC Web Service (OWS), including a Web Feature Service, must have the ability to describe its capabilities by returning service metadata in response to a GetCapabilities request. Specifically, every web feature service must support the KVP encoded form of the GetCapabilities request over HTTP GET so that a client can always know how to obtain a capabilities document. The capabilities response document contains the following sections: 1. Service Identification section The service identification section provides information about the WFS service itself. 2. Service Provider section The service provider section provides metadata about the organization operating the WFS server. 3. Operation Metadata section The operations metadata section provides metadata about the operations defined in this specification and implemented by a particular WFS server. This metadata includes the DCP, parameters and constraints for each operation. 4. FeatureType list section This section defines the list of feature types (and operations on each feature type) that are available from a web feature service. Additional information, such as the default SRS, any other supported SRSs, or no SRS whatsoever (for non-spatial feature types), for WFS requests is provided for each feature type. 5. ServesGMLObjectType list section This section defines the list of GML Object types, not derived from gml:AbstractFeatureType, that are available from a web feature service that supports the GetGMLObject operation. These types may be defined in a base GML schema, or in an application schema using its own namespace. 6. SupportsGMLObjectType list section The Supports GML Object Type section defines the list of GML Object types that a WFS server would be capable of serving if it was deployed to serve data. described by an application schema that either used those GML Object types directly (for non-abstract types), or defined derived types based on those types. 7. Filter capabilities section The schema of the Filter Capabilities Section is defined in the Filter Encoding Implementation Specification [3]. This is an optional section. If it exists, then the WFS should support the operations advertised therein. If the Filter Capabilities Section is not defined, then the client should assume that the server only supports the minimum default set of filter operators.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The xml text represents capabilities of this wfs server.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|

---
#### `GetCapabilities(Uri,IWebProxy)`

**Summary**

   *Get capabilities from the specific wfs service url.*

**Remarks**

   *Every OGC Web Service (OWS), including a Web Feature Service, must have the ability to describe its capabilities by returning service metadata in response to a GetCapabilities request. Specifically, every web feature service must support the KVP encoded form of the GetCapabilities request over HTTP GET so that a client can always know how to obtain a capabilities document. The capabilities response document contains the following sections: 1. Service Identification section The service identification section provides information about the WFS service itself. 2. Service Provider section The service provider section provides metadata about the organization operating the WFS server. 3. Operation Metadata section The operations metadata section provides metadata about the operations defined in this specification and implemented by a particular WFS server. This metadata includes the DCP, parameters and constraints for each operation. 4. FeatureType list section This section defines the list of feature types (and operations on each feature type) that are available from a web feature service. Additional information, such as the default SRS, any other supported SRSs, or no SRS whatsoever (for non-spatial feature types), for WFS requests is provided for each feature type. 5. ServesGMLObjectType list section This section defines the list of GML Object types, not derived from gml:AbstractFeatureType, that are available from a web feature service that supports the GetGMLObject operation. These types may be defined in a base GML schema, or in an application schema using its own namespace. 6. SupportsGMLObjectType list section The Supports GML Object Type section defines the list of GML Object types that a WFS server would be capable of serving if it was deployed to serve data. described by an application schema that either used those GML Object types directly (for non-abstract types), or defined derived types based on those types. 7. Filter capabilities section The schema of the Filter Capabilities Section is defined in the Filter Encoding Implementation Specification [3]. This is an optional section. If it exists, then the WFS should support the operations advertised therein. If the Filter Capabilities Section is not defined, then the client should assume that the server only supports the minimum default set of filter operators.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The xml text represents capabilities of this wfs server.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|
|webProxy|`IWebProxy`|The proxy of the wfs service.|

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
#### `GetLayers(String)`

**Summary**

   *Get layer names from specific wfs service url.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The collection represent layer names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serviceLocationUrl|`String`|The url of wfs service.|

---
#### `GetLayers(Uri)`

**Summary**

   *Get layer names from specific wfs service url.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The collection represent layer names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|

---
#### `GetLayers(Uri,IWebProxy)`

**Summary**

   *Get layer names from specific wfs service url.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The collection represent layer names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|
|webProxy|`IWebProxy`|The proxy of the wfs service.|

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
#### `OnRequestedData(RequestedDataWfsFeatureSourceEventArgs)`

**Summary**

   *This method allows you to raise the RequestedData event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the RequestedData event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event: This event is called after the requesting data by url from wfs server.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|requestedDataWfsFeatureSourceEventArgs|[`RequestedDataWfsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDataWfsFeatureSourceEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnRequestingData(RequestingDataWfsFeatureSourceEventArgs)`

**Summary**

   *This method allows you to raise the RequestingData event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the RequestingData event. This may be useful if you plan to extend the FeatureSource and you need access to the event. Details on the event: This event is called before the requesting data by url from wfs server.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|requestingDataWfsFeatureSourceEventArgs|[`RequestingDataWfsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDataWfsFeatureSourceEventArgs.md)|This parameter is the event arguments which define the parameters passed to the recipient of the event.|

---
#### `OnSendingWebRequest(SendingWebRequestEventArgs)`

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
|e|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|

---
#### `OnSentWebRequest(SentWebRequestEventArgs)`

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
|e|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|

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
#### `SendWebRequest(WebRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`WebResponse`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|webRequest|`WebRequest`|N/A|

---
#### `SendWebRequestCore(WebRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`WebResponse`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|webRequest|`WebRequest`|N/A|

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

#### SendingWebRequest

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)

#### SentWebRequest

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)

#### RequestingData

*N/A*

**Remarks**

*This event is called before the requesting data by url from wfs server.*

**Event Arguments**

[`RequestingDataWfsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDataWfsFeatureSourceEventArgs.md)

#### RequestedData

*N/A*

**Remarks**

*This event is called after the requesting data by url from wfs server.*

**Event Arguments**

[`RequestedDataWfsFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDataWfsFeatureSourceEventArgs.md)

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


