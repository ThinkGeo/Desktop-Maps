# ShapeFileFeatureSource


## Inheritance Hierarchy

+ `Object`
  + [`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)
    + **`ShapeFileFeatureSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ShapeFileFeatureSource()`](#shapefilefeaturesource)|
|[`ShapeFileFeatureSource(String)`](#shapefilefeaturesourcestring)|
|[`ShapeFileFeatureSource(String,FileAccess)`](#shapefilefeaturesourcestringfileaccess)|
|[`ShapeFileFeatureSource(String,String)`](#shapefilefeaturesourcestringstring)|
|[`ShapeFileFeatureSource(String,String,FileAccess)`](#shapefilefeaturesourcestringstringfileaccess)|
|[`ShapeFileFeatureSource(String,String,FileAccess,Encoding)`](#shapefilefeaturesourcestringstringfileaccessencoding)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanExecuteSqlQuery`](#canexecutesqlquery)|`Boolean`|N/A|
|[`CanModifyColumnStructure`](#canmodifycolumnstructure)|`Boolean`|N/A|
|[`Encoding`](#encoding)|`Encoding`|This property get and set the encoding information for the dbf.|
|[`FeatureIdsToExclude`](#featureidstoexclude)|Collection<`String`>|N/A|
|[`GeoCache`](#geocache)|[`FeatureCache`](../ThinkGeo.Core/ThinkGeo.Core.FeatureCache.md)|N/A|
|[`Id`](#id)|`String`|N/A|
|[`IndexPathFilename`](#indexpathfilename)|`String`|This property gets and sets the path and file of the index you want to use.|
|[`IsEditable`](#iseditable)|`Boolean`|This property returns if the FeatureSource allows edits or is read only.|
|[`IsInTransaction`](#isintransaction)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsTransactionLive`](#istransactionlive)|`Boolean`|N/A|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`ReadWriteMode`](#readwritemode)|`FileAccess`|N/A|
|[`RequireIndex`](#requireindex)|`Boolean`|This property gets and sets the requirement of index when reading data. The default value is true.|
|[`ShapePathFilename`](#shapepathfilename)|`String`|This property returns the path and file of the shape file you want to use.|
|[`SimplificationAreaInPixel`](#simplificationareainpixel)|`Int32`|N/A|
|[`SimplifiedAreas`](#simplifiedareas)|Collection<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|[`TransactionBuffer`](#transactionbuffer)|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|N/A|
|[`UsingSpatialIndex`](#usingspatialindex)|`Boolean`|This property gets the shape file feature source with index or not.|

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
|[`AddColumnBoolean(String)`](#addcolumnbooleanstring)|
|[`AddColumnDate(String)`](#addcolumndatestring)|
|[`AddColumnDouble(String,Int32,Int32)`](#addcolumndoublestringint32int32)|
|[`AddColumnInteger(String,Int32)`](#addcolumnintegerstringint32)|
|[`AddColumnMemo(String)`](#addcolumnmemostring)|
|[`AddColumnMemo(String,Int32)`](#addcolumnmemostringint32)|
|[`AddColumnString(String,Int32)`](#addcolumnstringstringint32)|
|[`AddFeature(Feature)`](#addfeaturefeature)|
|[`AddFeature(BaseShape)`](#addfeaturebaseshape)|
|[`AddFeature(BaseShape,IDictionary<String,String>)`](#addfeaturebaseshapeidictionary<stringstring>)|
|[`BeginTransaction()`](#begintransaction)|
|[`BuildIndexFile(String)`](#buildindexfilestring)|
|[`BuildIndexFile(String,BuildIndexMode)`](#buildindexfilestringbuildindexmode)|
|[`BuildIndexFile(String,String,BuildIndexMode)`](#buildindexfilestringstringbuildindexmode)|
|[`BuildIndexFile(String,String,ProjectionConverter,BuildIndexMode)`](#buildindexfilestringstringprojectionconverterbuildindexmode)|
|[`BuildIndexFile(String,String,String,String,BuildIndexMode)`](#buildindexfilestringstringstringstringbuildindexmode)|
|[`BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode)`](#buildindexfilestringstringprojectionconverterstringstringbuildindexmode)|
|[`BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode,Encoding)`](#buildindexfilestringstringprojectionconverterstringstringbuildindexmodeencoding)|
|[`BuildIndexFile(IEnumerable<Feature>,String)`](#buildindexfileienumerable<feature>string)|
|[`BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter)`](#buildindexfileienumerable<feature>stringprojectionconverter)|
|[`BuildIndexFile(IEnumerable<Feature>,String,BuildIndexMode)`](#buildindexfileienumerable<feature>stringbuildindexmode)|
|[`BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter,BuildIndexMode)`](#buildindexfileienumerable<feature>stringprojectionconverterbuildindexmode)|
|[`BuildRecordIdColumn(String,String,BuildRecordIdMode)`](#buildrecordidcolumnstringstringbuildrecordidmode)|
|[`BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32)`](#buildrecordidcolumnstringstringbuildrecordidmodeint32)|
|[`BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32,Encoding)`](#buildrecordidcolumnstringstringbuildrecordidmodeint32encoding)|
|[`CanGetBoundingBoxQuickly()`](#cangetboundingboxquickly)|
|[`CanGetCountQuickly()`](#cangetcountquickly)|
|[`CloneDeep()`](#clonedeep)|
|[`CloneShapeFileStructure(String,String)`](#cloneshapefilestructurestringstring)|
|[`CloneShapeFileStructure(String,String,OverwriteMode)`](#cloneshapefilestructurestringstringoverwritemode)|
|[`CloneShapeFileStructure(String,String,OverwriteMode,Encoding)`](#cloneshapefilestructurestringstringoverwritemodeencoding)|
|[`Close()`](#close)|
|[`CommitTransaction()`](#committransaction)|
|[`CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>)`](#createshapefileshapefiletypestringienumerable<dbfcolumn>)|
|[`CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding)`](#createshapefileshapefiletypestringienumerable<dbfcolumn>encoding)|
|[`CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding,OverwriteMode)`](#createshapefileshapefiletypestringienumerable<dbfcolumn>encodingoverwritemode)|
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
|[`GetDataFromDbf(String,String)`](#getdatafromdbfstringstring)|
|[`GetDataFromDbf(String)`](#getdatafromdbfstring)|
|[`GetDataFromDbf(String,IEnumerable<String>)`](#getdatafromdbfstringienumerable<string>)|
|[`GetDataFromDbf(String,ReturningColumnsType)`](#getdatafromdbfstringreturningcolumnstype)|
|[`GetDataFromDbf(IEnumerable<String>)`](#getdatafromdbfienumerable<string>)|
|[`GetDataFromDbf(IEnumerable<String>,String)`](#getdatafromdbfienumerable<string>string)|
|[`GetDataFromDbf(IEnumerable<String>,IEnumerable<String>)`](#getdatafromdbfienumerable<string>ienumerable<string>)|
|[`GetDataFromDbf(IEnumerable<String>,ReturningColumnsType)`](#getdatafromdbfienumerable<string>returningcolumnstype)|
|[`GetDbfColumns()`](#getdbfcolumns)|
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
|[`GetShapeFileType()`](#getshapefiletype)|
|[`GetType()`](#gettype)|
|[`GetValidColumnNames(IEnumerable<String>)`](#getvalidcolumnnamesienumerable<string>)|
|[`GetValidColumnNames(IEnumerable<String>,Encoding)`](#getvalidcolumnnamesienumerable<string>encoding)|
|[`GetValidColumns(IEnumerable<DbfColumn>)`](#getvalidcolumnsienumerable<dbfcolumn>)|
|[`GetValidColumns(IEnumerable<DbfColumn>,Encoding)`](#getvalidcolumnsienumerable<dbfcolumn>encoding)|
|[`Open()`](#open)|
|[`Rebuild(String)`](#rebuildstring)|
|[`Rebuild(String,ShapeFileSortingMode,Int32)`](#rebuildstringshapefilesortingmodeint32)|
|[`RefreshColumns()`](#refreshcolumns)|
|[`RemoveEmptyAndExcludedFeatures(Collection<Feature>)`](#removeemptyandexcludedfeaturescollection<feature>)|
|[`Reproject(String,String,ProjectionConverter,OverwriteMode)`](#reprojectstringstringprojectionconverteroverwritemode)|
|[`RollbackTransaction()`](#rollbacktransaction)|
|[`SpatialQuery(BaseShape,QueryType,IEnumerable<String>)`](#spatialquerybaseshapequerytypeienumerable<string>)|
|[`SpatialQuery(BaseShape,QueryType,ReturningColumnsType)`](#spatialquerybaseshapequerytypereturningcolumnstype)|
|[`SpatialQuery(Feature,QueryType,IEnumerable<String>)`](#spatialqueryfeaturequerytypeienumerable<string>)|
|[`SpatialQuery(Feature,QueryType,ReturningColumnsType)`](#spatialqueryfeaturequerytypereturningcolumnstype)|
|[`ToString()`](#tostring)|
|[`UpdateColumn(String,FeatureSourceColumn)`](#updatecolumnstringfeaturesourcecolumn)|
|[`UpdateDbfData(String,String,String)`](#updatedbfdatastringstringstring)|
|[`UpdateDbfData(String,IEnumerable<String>,IEnumerable<String>)`](#updatedbfdatastringienumerable<string>ienumerable<string>)|
|[`UpdateFeature(Feature)`](#updatefeaturefeature)|
|[`UpdateFeature(BaseShape)`](#updatefeaturebaseshape)|
|[`UpdateFeature(BaseShape,IDictionary<String,String>)`](#updatefeaturebaseshapeidictionary<stringstring>)|
|[`Validate()`](#validate)|

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
|[`OnBuildingIndex(BuildingIndexShapeFileFeatureSourceEventArgs)`](#onbuildingindexbuildingindexshapefilefeaturesourceeventargs)|
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
|[`OnRebuilding(RebuildingShapeFileFeatureSourceEventArgs)`](#onrebuildingrebuildingshapefilefeaturesourceeventargs)|
|[`OnStreamLoading(StreamLoadingEventArgs)`](#onstreamloadingstreamloadingeventargs)|
|[`OpenCore()`](#opencore)|
|[`ProcessTransaction(RectangleShape,Collection<Feature>,Boolean)`](#processtransactionrectangleshapecollection<feature>boolean)|
|[`RaiseCustomColumnFetchEvent(Collection<Feature>,Collection<String>)`](#raisecustomcolumnfetcheventcollection<feature>collection<string>)|
|[`SpatialQueryCore(BaseShape,QueryType,IEnumerable<String>)`](#spatialquerycorebaseshapequerytypeienumerable<string>)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`StreamLoading`](#streamloading)|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|
|[`BuildingIndex`](#buildingindex)|[`BuildingIndexShapeFileFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.BuildingIndexShapeFileFeatureSourceEventArgs.md)|N/A|
|[`Rebuilding`](#rebuilding)|[`RebuildingShapeFileFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RebuildingShapeFileFeatureSourceEventArgs.md)|N/A|
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
|[`ShapeFileFeatureSource()`](#shapefilefeaturesource)|
|[`ShapeFileFeatureSource(String)`](#shapefilefeaturesourcestring)|
|[`ShapeFileFeatureSource(String,FileAccess)`](#shapefilefeaturesourcestringfileaccess)|
|[`ShapeFileFeatureSource(String,String)`](#shapefilefeaturesourcestringstring)|
|[`ShapeFileFeatureSource(String,String,FileAccess)`](#shapefilefeaturesourcestringstringfileaccess)|
|[`ShapeFileFeatureSource(String,String,FileAccess,Encoding)`](#shapefilefeaturesourcestringstringfileaccessencoding)|

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
#### `Encoding`

**Summary**

   *This property get and set the encoding information for the dbf.*

**Remarks**

   *N/A*

**Return Value**

`Encoding`

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
#### `IndexPathFilename`

**Summary**

   *This property gets and sets the path and file of the index you want to use.*

**Remarks**

   *When you specify the path and file name it should be in the correct format as such however the file does not need to exists on the file system. This is to allow us to accept streams supplied by the developer at runtime. If you choose to provide a file that exists then we will attempt to use it. If we cannot find it then we will raise the SteamLoading event and allow you to supply the stream. For example you can pass in "C:\NotARealPath\File1.idx" which does not exists on the file system. When we raise the event for you to supply a stream we will pass to you the path and file name for you to differentiate the files.*

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
#### `ReadWriteMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FileAccess`

---
#### `RequireIndex`

**Summary**

   *This property gets and sets the requirement of index when reading data. The default value is true.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `ShapePathFilename`

**Summary**

   *This property returns the path and file of the shape file you want to use.*

**Remarks**

   *When you specify the path and file name it should be in the correct format as such however the file does not need to exists on the file system. This is to allow us to accept streams supplied by the developer at runtime. If you choose to provide a file that exists then we will attempt to use it. If we cannot find it then we will raise the SteamLoading event and allow you to supply the stream. For example you can pass in "C:\NotARealPath\File1.shp" which does not exists on the file system. When we raise the event for you to supply a stream we will pass to you the path and file name for you to differentiate the files.*

**Return Value**

`String`

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
#### `UsingSpatialIndex`

**Summary**

   *This property gets the shape file feature source with index or not.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

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
#### `AddColumnBoolean(String)`

**Summary**

   *This method adds a new Boolean column to the DBF file associated with the shape file.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|This parameter is the column you want to add.|

---
#### `AddColumnDate(String)`

**Summary**

   *This method adds a new Date column to the DBF file associated with the shape file.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|This parameter is the column you want to add.|

---
#### `AddColumnDouble(String,Int32,Int32)`

**Summary**

   *This method adds a new Double column to the DBF file associated with the shape file.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|This parameter is the column you want to add.|
|totalLength|`Int32`|This is the total length of the field including both the digits to the left and right of the decimal point.|
|precisionLength|`Int32`|This parameter specifies how many digits after the decimal point you need to support.|

---
#### `AddColumnInteger(String,Int32)`

**Summary**

   *This method adds a new Integer column to the DBF file associated with the shape file.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|This parameter is the column you want to add.|
|length|`Int32`|This parameter specifies the length of the integer.|

---
#### `AddColumnMemo(String)`

**Summary**

   *This method adds a new Memo column to the DBF file associated with the shape file.*

**Remarks**

   *This method adds a new Memo column to the DBF file associated with the shape file. Internally the DBF holds an integer column that is a pointer to the data in the memo file. The pointer is measured in 512 byte chunks. Our default decimalDegreesValue for the size of the pointer column is 10 which means you can have 9,999,999,999 pointers to the 512 byte blocks. The ramification of this is that if you have more than this many records and each record uses more then 512 bytes as part of its memo then there will not be enough space for storage. If you have special needs for this please use the other overload that allows you to specify the number of digits you can use for the pointer.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|This parameter is the column you want to add.|

---
#### `AddColumnMemo(String,Int32)`

**Summary**

   *This method adds a new Memo column to the DBF file associated with the shape file.*

**Remarks**

   *This method adds a new Memo column to the DBF file associated with the shape file. Internally the DBF holds an integer column that is a pointer to the data in the memo file. The pointer is measured in 512 byte chunks. Our default decimalDegreesValue for the size of the pointer column is 10 which means you can have 9,999,999,999 pointers to the 512 byte blocks. The ramification of this is that if you have more than this many records and each record uses more than 512 bytes as part of its memo then there will not be enough space for storage. Conversely if you know you have few records then you can decrease this number. A good rule of thumb is to multiply the number of records by the number of 512 byte chunks you expect each record to use and then get the resulting number of digits resulting for the multiplication. Example You have 1,000,000 records and expect to have 4K, 8 chunks of 512 bytes, of memo data for each record. This means you will use multiple 1,000,000 * 8 which is 8,000,000 and then total the number of digits which in this case is 7. Assuming the numbers above a length of 7 will support your needs.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|This parameter is the column you want to add.|
|memoValueLength|`Int32`|This parameter is the number of digits you need to hold the pointers to the data in the memo file.|

---
#### `AddColumnString(String,Int32)`

**Summary**

   *This method adds a new String column to the DBF file associated with the shape file.*

**Remarks**

   *This method adds a new String column to the DBF file associated with the shape file.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|This parameter is the column you want to add.|
|length|`Int32`|This parameter is the number of characters that the string can hold.|

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
#### `BuildIndexFile(String)`

**Summary**

   *This method build a spatial index for the shape file which increases access speed.*

**Remarks**

   *This overload builds an index file with the same name as the shape file with only the extension being different. It will not do a rebuild if there is an existing index.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file name and path that you want to build an index for.|

---
#### `BuildIndexFile(String,BuildIndexMode)`

**Summary**

   *This method build a spatial index for the shape file which increases access speed.*

**Remarks**

   *This overload builds an index file with the same name as the shape file with only the extension being different. You can also specify if you want to rebuild an existing index file.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file name and path that you want to build an index for.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|

---
#### `BuildIndexFile(String,String,BuildIndexMode)`

**Summary**

   *This method build a spatial index for the shape file which increases access speed.*

**Remarks**

   *This overload builds an index file with the same name as the shape file with only the extension being different. You can also specify if you want to rebuild an existing index file.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file name and path that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|

---
#### `BuildIndexFile(String,String,ProjectionConverter,BuildIndexMode)`

**Summary**

   *This method build a spatial index for the shape file which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name and only build Index for those records satisfied the regularExpression. You can also specify if you want to rebuild an existing index file.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file name and path that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter specifies the projection used to build index file.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|

---
#### `BuildIndexFile(String,String,String,String,BuildIndexMode)`

**Summary**

   *This method build a spatial index for the shape file which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name and only build Index for those records satisfied the regularExpression. You can also specify if you want to rebuild an existing index file.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file name and path that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|columnName|`String`|The columnName to be used to get the value to match the regex expression.|
|regularExpression|`String`|This parameter specifies the regex expression to filter out thoese records to build index with.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|

---
#### `BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode)`

**Summary**

   *This method build a spatial index for the shape file which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name and only build Index for those records satisfied the regularExpression. You can also specify if you want to rebuild an existing index file.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file name and path that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter specifies the projection used to build index file.|
|columnName|`String`|The columnName to be used to get the value to match the regex expression.|
|regularExpression|`String`|This parameter specifies the regex expression to filter out thoese records to build index with.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|

---
#### `BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode,Encoding)`

**Summary**

   *This method build a spatial index for the shape file which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name and only build Index for those records satisfied the regularExpression. You can also specify if you want to rebuild an existing index file.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file name and path that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter specifies the projection used to build index file.|
|columnName|`String`|The columnName to be used to get the value to match the regex expression.|
|regularExpression|`String`|This parameter specifies the regular expression pattern to filter out thoese records to build index with.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|
|encoding|`Encoding`|This parameter determines the Enconding system used in the dbf, and this will be used if the dbf is encoded in a different encoding with default.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String)`

**Summary**

   *This method build a spatial index for a passed group of featurs which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name for a group of passed in features.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the target group of features that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter)`

**Summary**

   *This method build a spatial index for a passed group of featurs using the specified projection which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name for a group of passed in features.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the target group of features that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter specifies the projection used to build index file.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String,BuildIndexMode)`

**Summary**

   *This method build a spatial index for a passed group of featurs which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name for a group of passed in features.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the target group of features that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter,BuildIndexMode)`

**Summary**

   *This method build a spatial index for a passed group of featurs which increases access speed.*

**Remarks**

   *This overload builds an index file with the specified index file name for a group of passed in features.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the target group of features that you want to build an index for.|
|indexPathFilename|`String`|This parameter specifies the index file name.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter specifies the projection used to build index file.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines what will happen if there is an existing index file.|

---
#### `BuildRecordIdColumn(String,String,BuildRecordIdMode)`

**Summary**

   *Static API used to build RecordId, the id should start from 0 by default.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeFilename|`String`|The target shape file name to build record id based on.|
|fieldname|`String`|The fild name for the record id.|
|rebuildNeeded|[`BuildRecordIdMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildRecordIdMode.md)|The build record id mode.|

---
#### `BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32)`

**Summary**

   *Static API used to build RecordId from the specified starting id number.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeFilename|`String`|The target shape file name to build record id based on.|
|fieldname|`String`|The fild name for the record id.|
|rebuildNeeded|[`BuildRecordIdMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildRecordIdMode.md)|The build record id mode.|
|startNumber|`Int32`|The starting id number of the record id.|

---
#### `BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32,Encoding)`

**Summary**

   *Static API used to build RecordId from the specified starting id number.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeFilename|`String`|The target shape file name to build record id based on.|
|fieldname|`String`|The fild name for the record id.|
|rebuildNeeded|[`BuildRecordIdMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildRecordIdMode.md)|The build record id mode.|
|startNumber|`Int32`|The starting id number of the record id.|
|encoding|`Encoding`|This parameter specified the encoding information in dbf.|

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
#### `CloneShapeFileStructure(String,String)`

**Summary**

   *Clone out the structure from the source shape file to the target shape file. After cloning the structure, the targetShapeFile has the same type and same dbf columns with the source shape file but without any records in it.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShapePathFilename|`String`|The source shape file to be cloned.|
|targetShapePathFilename|`String`|The target shape file with same structure with the source one after the structure cloned.|

---
#### `CloneShapeFileStructure(String,String,OverwriteMode)`

**Summary**

   *Clone out the structure from the source shape file to the target shape file. After cloning the structure, the targetShapeFile has the same type and same dbf columns with the source shape file but without any records in it.*

**Remarks**

   *Exception will be thown when the target shape file not extis while the override mode is set to DoNotOverwrite.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShapePathFilename|`String`|The source shape file to be cloned.|
|targetShapePathFilename|`String`|The target shape file with same structure with the source one after the structure cloned.|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|This parameter specifies the override mode when the target shape file exists.|

---
#### `CloneShapeFileStructure(String,String,OverwriteMode,Encoding)`

**Summary**

   *Clone out the structure from the source shape file to the target shape file. After cloning the structure, the targetShapeFile has the same type and same dbf columns with the source shape file but without any records in it.*

**Remarks**

   *Exception will be thown when the target shape file not extis while the override mode is set to DoNotOverwrite.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShapePathFilename|`String`|The source shape file to be cloned.|
|targetShapePathFilename|`String`|The target shape file with same structure with the source one after the structure cloned.|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|This parameter specifies the override mode when the target shape file exists.|
|encoding|`Encoding`|This parameter specifies the encoding information in the source shape file.|

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
#### `CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>)`

**Summary**

   *Static API to create a new shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeType|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|This parameter specifies the the shape file type for the target shape file.|
|pathFilename|`String`|This parameter specifies the shape file name for the target shape file.|
|databaseColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|This parameter specifies the dbf column information for the target shape file.|

---
#### `CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding)`

**Summary**

   *Static API to create a new shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeType|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|This parameter specifies the the shape file type for the target shape file.|
|pathFilename|`String`|This parameter specifies the shape file name for the target shape file.|
|databaseColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|This parameter specifies the dbf column information for the target shape file.|
|encoding|`Encoding`|This parameter specifies the dbf encoding infromation for the target shape file.|

---
#### `CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding,OverwriteMode)`

**Summary**

   *Static API to create a new shape file.*

**Remarks**

   *Exception will be thown when the target shape file exist while the override mode is set to DoNotOverwrite.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeType|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|This parameter specifies the the shape file type for the target shape file.|
|pathFilename|`String`|This parameter specifies the shape file name for the target shape file.|
|databaseColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|This parameter specifies the dbf column information for the target shape file.|
|encoding|`Encoding`|This parameter specifies the dbf encoding infromation for the target shape file.|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|This parameter specifies the override mode when the target shape file exists.|

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
#### `GetDataFromDbf(String,String)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method gets data directly from the DBF file associated with the shape file. When you specify the Id and column name it will get the decimalDegreesValue from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method gets data directly from the DBF file associated with the shape file.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id for the Feature you want to find.|
|columnName|`String`|This parameter is the column name you want to return.|

---
#### `GetDataFromDbf(String)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method returns a dictionary holding all of the values from the DBF for the Id you specified. In the dictionary the key is the column name and values are the values from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,`String`>|This method returns a dictionary holding all of the values from the DBF for the Id you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id of the Feature you want.|

---
#### `GetDataFromDbf(String,IEnumerable<String>)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method returns a dictionary holding all of the values from the DBF for the Id you specified. In the dictionary the key is the column name and values are the values from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,`String`>|This method returns a dictionary holding all of the values from the DBF for the Id you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id of the Feature you want.|
|returningColumnNames|IEnumerable<`String`>|This parameter is the returning columns specified for the returning data.|

---
#### `GetDataFromDbf(String,ReturningColumnsType)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method returns a dictionary holding all of the values from the DBF for the Id you specified. In the dictionary the key is the column name and values are the values from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,`String`>|This method returns a dictionary holding all of the values from the DBF for the Id you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id of the Feature you want.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter is the returningColumnType specified for the returning data.|

---
#### `GetDataFromDbf(IEnumerable<String>)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified. In the dictionary the key is the column name and values are the values from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|Collection<Dictionary<`String`,`String`>>|This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter is the Ids of the Features you want.|

---
#### `GetDataFromDbf(IEnumerable<String>,String)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified. In the dictionary the key is the column name and values are the values from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|Collection<Dictionary<`String`,`String`>>|This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter is the Ids of the Features you want.|
|columnName|`String`|This parameter is the returning columnName of the Features you want.|

---
#### `GetDataFromDbf(IEnumerable<String>,IEnumerable<String>)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified. In the dictionary the key is the column name and values are the values from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|Collection<Dictionary<`String`,`String`>>|This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter is the Ids of the Features you want.|
|columnNames|IEnumerable<`String`>|This parameter is the returning columnNames of the Features you want.|

---
#### `GetDataFromDbf(IEnumerable<String>,ReturningColumnsType)`

**Summary**

   *This method gets data directly from the DBF file associated with the shape file.*

**Remarks**

   *This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified. In the dictionary the key is the column name and values are the values from the DBF.*

**Return Value**

|Type|Description|
|---|---|
|Collection<Dictionary<`String`,`String`>>|This method returns a collection of dictionary holding all of the values from the DBF for the Ids you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter is the Ids of the Features you want.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter is the returning column type of the Features you want.|

---
#### `GetDbfColumns()`

**Summary**

   *Get the dbf columns out from the shape file featureSource.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|The dbfColumns in the shape file FeatureSource.|

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
#### `GetShapeFileType()`

**Summary**

   *Get shape file type for the shape file featureSource.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|The shapeFileType for the ShapeFileFeatureSource.|

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
#### `GetValidColumnNames(IEnumerable<String>)`

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
|columnNames|IEnumerable<`String`>|N/A|

---
#### `GetValidColumnNames(IEnumerable<String>,Encoding)`

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
|columnNames|IEnumerable<`String`>|N/A|
|encoding|`Encoding`|N/A|

---
#### `GetValidColumns(IEnumerable<DbfColumn>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|

---
#### `GetValidColumns(IEnumerable<DbfColumn>,Encoding)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|
|encoding|`Encoding`|N/A|

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
#### `Rebuild(String)`

**Summary**

   *This method rebuilds the SHP, SHX, DBF, IDX and IDS files for the given shape file.*

**Remarks**

   *This method rebuilds the SHP, SHX, DBF, IDX and IDS files for the given shape file. When we do editing we have optimized the updates so that we do not need to rebuild the entire shape file. This leads to the shape file being out of order which may cause it not to open in other tools. One optimization is if you update a record instead of rebuilding a new shape file we mark the old record as null and add the edited record at the end of the shape file. This greatly increases the speed of committing shape file changes but will over time unorder the shape file. In addition we do a delete the DBF file will simply mark the record deleted and not compact the space. Rebuilding the shape file will correctly order the SPX and SHX along with compacting the DBF file and rebuild any index with the same any of the shape file if it exists. Note that if you have build custom index files where the name of the index differs from that of the shape file you will need to rebuild those manually using the BuildIndex methods.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter is the shape file you want to rebuild.|

---
#### `Rebuild(String,ShapeFileSortingMode,Int32)`

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
|shapePathFilename|`String`|N/A|
|sortingMode|[`ShapeFileSortingMode`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileSortingMode.md)|N/A|
|sridForSorting|`Int32`|N/A|

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
#### `Reproject(String,String,ProjectionConverter,OverwriteMode)`

**Summary**

   *This API provide a easy way to project features in a shape file into another projection and save it to shape file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShapeFile|`String`|This parameter specifies the source shape file to be projected.|
|targetShapeFile|`String`|This parameter specifies the target shape file to be saved for the projected features.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter is the projection to project the source shape file to target shape file. The source Shape file should be in the FromProjection of the Projection prameter, and the targetShapeFile will be in the ToProjection of the Projection.|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|This parameter specifies the override mode when the target shape file exists.|

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
#### `UpdateDbfData(String,String,String)`

**Summary**

   *This method updates data in the DBF file associated with the shape file.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id of the feature you want to update.|
|columnName|`String`|This parameter is the column name you want to update.|
|value|`String`|This parameter is the decimalDegreesValue you want to set.|

---
#### `UpdateDbfData(String,IEnumerable<String>,IEnumerable<String>)`

**Summary**

   *This method updates data in the DBF file associated with the shape file.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the Id of the feature you want to update.|
|columnNames|IEnumerable<`String`>|This parameter is the columnNames you want to update.|
|values|IEnumerable<`String`>|This parameter is the target values you want to set.|

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
#### `Validate()`

**Summary**

   *This method checks all features in a shapefile is supported by Mapsuite or not.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,`String`>|A dictionary which contains all the unsupported features. The key is the Indexs which failed to pass the check, the value contains the reason for its failure.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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

   *This method will commit the existing transaction to its underlying source of data.*

**Remarks**

   *This method will commit the existing transaction to its underlying source of data. It will pass back the results of how the commit went to include any error received. If you are implementing your own FeatureSource then this is one of the crucial methods you must create. It should be fairly straight forward that you will loop through the transaction buffer and add, edit or delete the InternalFeatures in your underlying data source. Remember to build and pass back the TransactionResult class so that users of your FeatureSource can respond to failures you may encounter committing the InternalFeatures. We will handle the end of the transaction and also the cleanup of the transaction buffer. Your task will be to commit the records and produce a TransactionResult return.The Transaction SystemThe transaction system of a FeatureSource sits on top of the inherited implementation of any specific source such as Oracle Spatial or Shape files. In this way it functions the same way for every FeatureSource. You start by calling the BeginTransaction. This allocates a collection of in memory change buffers that are used to store changes until you commit the transaction. So for example when you call the Add, Delete or Update method the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction you can call RollbackTransaction at any time and the in memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction you call the CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureSource is responsible for integrating your changes into the underlying FeatureSource. By default the IsLiveTransaction property is set to false which means that until you commit the changes the FeatureSource API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true then things function slightly differently. The live transaction concept means that all of the modification you perform during a transaction are live from the standpoint of the querying methods on the object.To setup an example imagine that you have a FeatureSource that has 10 records in it. Next you begin a transaction and then call GetAllFeatures, the result would be 10 records. After that you call a delete on one of the records and call the GetAllFeatures again, this time you only get nine records. You receive nine records even though the transaction has not yet been committed. In the same sense you could have added a new record or modified an existing one and those changes are considered live though not committed.In the case where you modify records such as expanding the size of a polygon those changes as well are reflected. So for example you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record but would return the larger records, in this case the larger record is returned. You can set this property to be false as well in which case all of the spatial related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In this case only after committing the transaction would the FeatureSource reflect the changes.*

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

   *This method returns the count of the number of records in this FeatureSource.*

**Remarks**

   *This protected virtual method is called from the concreate public method GetCount. It does not take into account any transaction activity as this is the responsibility of the concreate public method GetCount. In this way as a developer if you choose to override this method you do not have to consider transaction at all. The default implementation of GetCountCore uses the GetAllRecordsCore method to calculate how many records there are in the FeatureSource. We strongly recommend that you provide your own implementation for this method that will be more efficient If you do not override this method the means it gets the count is by calling the GetAllFeatureCore method and counting each feature. This is a very inefficient way to get the count in most data sources. It is highly recommended that you override this method and replace it with a highly optimized version. For example in a ShapeFile the record count is in the main header of the file. Similarly if you are using Oracle Spatial you can execute a simple query to get the count of all of the record without returning them. In these ways you can greatly improve the performance of this method.*

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

   *This method returns a collection of InternalFeatures by providing a group of Ids.*

**Remarks**

   *This method returns a collection of InternalFeatures by providing a group of Ids. The internal implementation calls the GetAllFeaturesCore. Because of this if you want an efficient version of this method then we high suggest you override the GetFeaturesByIdsCore method and provide a fast way to find a group of InternalFeatures by their Id.*

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

   *This method returns all of the InternalFeatures of this FeatureSource inside of the specified bounding box. If you are overriding this method you will not need to consider anything about transactions as this is handled by the concreate version of this method. The default implementation of GetFeaturesForDrawingCore uses the GetFeaturesInsodeBoundingBoxCore with some optimizations based on the screen width and height. For example we can determine is a feature is going to draw in only one to four pixels and in that case we may not draw the entire feature but just a subset.*

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

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.*

**Remarks**

   *This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box. If you are in a transaction and that transaction is live then it will also take that into consideration. The default implementation of GetFeaturesOutsideBoundingBoxCore uses the GetAllRecordsCore method to determine which InternalFeatures are outside of the bounding box. We strongly recommend that you provide your own implementation for this method that will be more efficient*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns all of the InternalFeatures of this FeatureSource outside of the specified bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the bounding box you with to find InternalFeatures outside of.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

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
#### `OnBuildingIndex(BuildingIndexShapeFileFeatureSourceEventArgs)`

**Summary**

   *This method allows you to raise the BuildingIndex event.*

**Remarks**

   *This method allows you to raise the BuildingIndex event. Normally events are not accessible to derived classes so we exposed a way to raise the event is necessary through this protected method.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`BuildingIndexShapeFileFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.BuildingIndexShapeFileFeatureSourceEventArgs.md)|This parameter represents the event arguments you want to raise the BuildingIndex event with.|

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
#### `OnRebuilding(RebuildingShapeFileFeatureSourceEventArgs)`

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
|e|[`RebuildingShapeFileFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RebuildingShapeFileFeatureSourceEventArgs.md)|N/A|

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

*If you choose you can pass in your own stream to represent the file. The stream can come from a variety of places such as isolated storage, a compressed file, and encrypted stream. When the Image is finished with the stream it will dispose of it so be sure to keep this in mind when passing the stream in. If you do not pass in a alternate stream the class will attempt to load the file from the file system using the PathFilename property.*

**Event Arguments**

[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)

#### BuildingIndex

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`BuildingIndexShapeFileFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.BuildingIndexShapeFileFeatureSourceEventArgs.md)

#### Rebuilding

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RebuildingShapeFileFeatureSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RebuildingShapeFileFeatureSourceEventArgs.md)

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


