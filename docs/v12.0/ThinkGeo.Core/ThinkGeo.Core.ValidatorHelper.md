# ValidatorHelper


## Inheritance Hierarchy

+ `Object`
  + **`ValidatorHelper`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`CheckAreaUnitIsValid(AreaUnit,String)`](#checkareaunitisvalidareaunitstring)|
|[`CheckAreIntegerStrings(IEnumerable<String>,String)`](#checkareintegerstringsienumerable<string>string)|
|[`CheckBufferCapTypeIsValid(BufferCapType,String)`](#checkbuffercaptypeisvalidbuffercaptypestring)|
|[`CheckBuildIndexModeIsValid(BuildIndexMode,String)`](#checkbuildindexmodeisvalidbuildindexmodestring)|
|[`CheckCanModifyColumnStructure(Boolean)`](#checkcanmodifycolumnstructureboolean)|
|[`CheckCanParseStringToDouble(String,String)`](#checkcanparsestringtodoublestringstring)|
|[`CheckCanvasHeightIsLargerThanZero(Double,String)`](#checkcanvasheightislargerthanzerodoublestring)|
|[`CheckCanvasWidthIsLargerThanZero(Double,String)`](#checkcanvaswidthislargerthanzerodoublestring)|
|[`CheckColumnNameIsInFeature(String,IEnumerable<Feature>)`](#checkcolumnnameisinfeaturestringienumerable<feature>)|
|[`CheckConnectionStringIsNotNull(String)`](#checkconnectionstringisnotnullstring)|
|[`CheckCustomStyleDuplicates(AreaStyle,LineStyle,PointStyle,TextStyle,Collection<Style>,Boolean)`](#checkcustomstyleduplicatesareastylelinestylepointstyletextstylecollection<style>boolean)|
|[`CheckDateTimeIsInRange(DateTime,String,DateTime,RangeCheckingInclusion,DateTime,RangeCheckingInclusion)`](#checkdatetimeisinrangedatetimestringdatetimerangecheckinginclusiondatetimerangecheckinginclusion)|
|[`CheckDbfColumnDecimalLengthIsValid(DbfColumnType,Int32)`](#checkdbfcolumndecimallengthisvaliddbfcolumntypeint32)|
|[`CheckDistanceUnitIsValid(DistanceUnit,String)`](#checkdistanceunitisvaliddistanceunitstring)|
|[`CheckDrawingLevelIsValid(DrawingLevel,String)`](#checkdrawinglevelisvaliddrawinglevelstring)|
|[`CheckDrawingLineCapIsValid(DrawingLineCap,String)`](#checkdrawinglinecapisvaliddrawinglinecapstring)|
|[`CheckDrawingLineJoinIsValid(DrawingLineJoin,String)`](#checkdrawinglinejoinisvaliddrawinglinejoinstring)|
|[`CheckExtentIsValid(RectangleShape,String)`](#checkextentisvalidrectangleshapestring)|
|[`CheckFeatureColumnValueContainsColon(String,String)`](#checkfeaturecolumnvaluecontainscolonstringstring)|
|[`CheckFeatureIsValid(Feature,String)`](#checkfeatureisvalidfeaturestring)|
|[`CheckFeatureIsValid(Feature)`](#checkfeatureisvalidfeature)|
|[`CheckFeatureSourceCanExecuteSqlQuery(Boolean)`](#checkfeaturesourcecanexecutesqlqueryboolean)|
|[`CheckFeatureSourceCollectionIsNotEmpty(Collection<FeatureSource>)`](#checkfeaturesourcecollectionisnotemptycollection<featuresource>)|
|[`CheckFeatureSourceIsEditable(Boolean)`](#checkfeaturesourceiseditableboolean)|
|[`CheckFeatureSourceIsInTransaction(Boolean)`](#checkfeaturesourceisintransactionboolean)|
|[`CheckFeatureSourceIsNotInTransaction(Boolean)`](#checkfeaturesourceisnotintransactionboolean)|
|[`CheckFeatureSourceIsOpen(Boolean)`](#checkfeaturesourceisopenboolean)|
|[`CheckFileIsExist(String)`](#checkfileisexiststring)|
|[`CheckFileIsNotExist(String)`](#checkfileisnotexiststring)|
|[`CheckGeoCanvasIsInDrawing(Boolean)`](#checkgeocanvasisindrawingboolean)|
|[`CheckGeoDashCapIsValid(GeoDashCap,String)`](#checkgeodashcapisvalidgeodashcapstring)|
|[`CheckGeographyUnitIsMeter(GeographyUnit,String)`](#checkgeographyunitismetergeographyunitstring)|
|[`CheckGeographyUnitIsValid(GeographyUnit,String)`](#checkgeographyunitisvalidgeographyunitstring)|
|[`CheckGeoImageIsValid(GeoImage,String,GeoCanvas)`](#checkgeoimageisvalidgeoimagestringgeocanvas)|
|[`CheckGroupLayerIsNotEmpty(GeoCollection<Layer>)`](#checkgrouplayerisnotemptygeocollection<layer>)|
|[`CheckHtmlColorIsValid(String,String)`](#checkhtmlcolorisvalidstringstring)|
|[`CheckIconImageAndIconFilePathAreInvalid(String,GeoImage)`](#checkiconimageandiconfilepathareinvalidstringgeoimage)|
|[`CheckIEnumerableIsNotNullNorEmpty(IEnumerable,String,String)`](#checkienumerableisnotnullnoremptyienumerablestringstring)|
|[`CheckImageFormatIsValid(String,Collection<String>,String)`](#checkimageformatisvalidstringcollection<string>string)|
|[`CheckImageFormatSupport(String)`](#checkimageformatsupportstring)|
|[`CheckImageIsNotNullInPointStyle(GeoImage)`](#checkimageisnotnullinpointstylegeoimage)|
|[`CheckInputValueIsInRange(Double,String,Double,RangeCheckingInclusion,Double,RangeCheckingInclusion)`](#checkinputvalueisinrangedoublestringdoublerangecheckinginclusiondoublerangecheckinginclusion)|
|[`CheckInputValueIsInRange(Double,String,Double,RangeCheckingInclusion,Double,RangeCheckingInclusion,String)`](#checkinputvalueisinrangedoublestringdoublerangecheckinginclusiondoublerangecheckinginclusionstring)|
|[`CheckInputValueIsInRange(Double,String,Double,Double)`](#checkinputvalueisinrangedoublestringdoubledouble)|
|[`CheckInputValueIsLargerThan(Double,String,Double,RangeCheckingInclusion)`](#checkinputvalueislargerthandoublestringdoublerangecheckinginclusion)|
|[`CheckInputValueIsLargerThanZero(Double,String)`](#checkinputvalueislargerthanzerodoublestring)|
|[`CheckInputValueIsLessThan(Double,String,Double,RangeCheckingInclusion)`](#checkinputvalueislessthandoublestringdoublerangecheckinginclusion)|
|[`CheckInputValueIsNotNaNNorInfinity(Double,String)`](#checkinputvalueisnotnannorinfinitydoublestring)|
|[`CheckInputValueIsValidDecimalDegree(Double,String)`](#checkinputvalueisvaliddecimaldegreedoublestring)|
|[`CheckItemInCollection(String,Collection<String>,String)`](#checkitemincollectionstringcollection<string>string)|
|[`CheckItemsInCollection(Collection<String>,Collection<String>,String)`](#checkitemsincollectioncollection<string>collection<string>string)|
|[`CheckLatitudeIsInRange(Double,String)`](#checklatitudeisinrangedoublestring)|
|[`CheckLayerHasBoundingBox(Boolean)`](#checklayerhasboundingboxboolean)|
|[`CheckLayerIsNotOpenedNorDrawing(Boolean)`](#checklayerisnotopenednordrawingboolean)|
|[`CheckLayerIsOpened(Boolean)`](#checklayerisopenedboolean)|
|[`CheckLineDashStyleIsValid(LineDashStyle,String)`](#checklinedashstyleisvalidlinedashstylestring)|
|[`CheckLongIsNotGreaterThanUInt32MaxValue(Int64)`](#checklongisnotgreaterthanuint32maxvalueint64)|
|[`CheckLongitudeIsInRange(Double,String)`](#checklongitudeisinrangedoublestring)|
|[`CheckNumberIsByte(Int32,String)`](#checknumberisbyteint32string)|
|[`CheckObjectIsNotNull(Object,String)`](#checkobjectisnotnullobjectstring)|
|[`CheckObjectIsNotNull(Object,String,String)`](#checkobjectisnotnullobjectstringstring)|
|[`CheckObjectIsTargetType(Object,Type,String)`](#checkobjectistargettypeobjecttypestring)|
|[`CheckObjectsAreNotAllNull(Object,Object,String,String)`](#checkobjectsarenotallnullobjectobjectstringstring)|
|[`CheckOverwriteModeIsValid(OverwriteMode,String)`](#checkoverwritemodeisvalidoverwritemodestring)|
|[`CheckPanDirectionIsValid(PanDirection,String)`](#checkpandirectionisvalidpandirectionstring)|
|[`CheckPointLineIsIntersected(PointShape,String,LineShape,String,Double)`](#checkpointlineisintersectedpointshapestringlineshapestringdouble)|
|[`CheckPointSymbolTypeIsValid(PointSymbolType,String)`](#checkpointsymboltypeisvalidpointsymboltypestring)|
|[`CheckPointTypeIsValid(PointType,String)`](#checkpointtypeisvalidpointtypestring)|
|[`CheckProjectionConverterIsOpen(Boolean)`](#checkprojectionconverterisopenboolean)|
|[`CheckQueryTypeIsValid(QueryType,String)`](#checkquerytypeisvalidquerytypestring)|
|[`CheckRandomColorTypeIsValid(RandomColorType,String)`](#checkrandomcolortypeisvalidrandomcolortypestring)|
|[`CheckRasterSourceIsOpen(Boolean)`](#checkrastersourceisopenboolean)|
|[`CheckRebuildRecordIdModeIsValid(BuildRecordIdMode,String)`](#checkrebuildrecordidmodeisvalidbuildrecordidmodestring)|
|[`CheckReturningColumnsTypeIsValid(ReturningColumnsType,String)`](#checkreturningcolumnstypeisvalidreturningcolumnstypestring)|
|[`CheckRingOrderIsValid(RingOrder,String)`](#checkringorderisvalidringorderstring)|
|[`CheckRtreeSpatialIndexIsOpen(Boolean)`](#checkrtreespatialindexisopenboolean)|
|[`CheckScaleIsLargerThanZero(Double,String)`](#checkscaleislargerthanzerodoublestring)|
|[`CheckScaleIsValid(Double,String)`](#checkscaleisvaliddoublestring)|
|[`CheckShapeIsAreaBaseShape(BaseShape)`](#checkshapeisareabaseshapebaseshape)|
|[`CheckShapeIsLineBaseShape(BaseShape)`](#checkshapeislinebaseshapebaseshape)|
|[`CheckShapeIsMultipointShape(BaseShape)`](#checkshapeismultipointshapebaseshape)|
|[`CheckShapeIsPointShape(BaseShape)`](#checkshapeispointshapebaseshape)|
|[`CheckShapeIsValid(BaseShape,String)`](#checkshapeisvalidbaseshapestring)|
|[`CheckShapeIsValidForOperation(BaseShape)`](#checkshapeisvalidforoperationbaseshape)|
|[`CheckShapeValidationModeIsValid(ShapeValidationMode,String)`](#checkshapevalidationmodeisvalidshapevalidationmodestring)|
|[`CheckSimplificationTypeIsValid(SimplificationType,String)`](#checksimplificationtypeisvalidsimplificationtypestring)|
|[`CheckSpatialIndexIsDeletable(Boolean)`](#checkspatialindexisdeletableboolean)|
|[`CheckStartingPointIsValid(StartingPoint,String)`](#checkstartingpointisvalidstartingpointstring)|
|[`CheckStatus()`](#checkstatus)|
|[`CheckStreamIsWritable(Stream,String)`](#checkstreamiswritablestreamstring)|
|[`CheckStringIsNotNullNorEmpty(String,String)`](#checkstringisnotnullnoremptystringstring)|
|[`CheckStringIsNotNullNorEmptyForOperation(String,String)`](#checkstringisnotnullnoremptyforoperationstringstring)|
|[`CheckStringIsNotNullNorWhiteSpace(String,String)`](#checkstringisnotnullnorwhitespacestringstring)|
|[`CheckStringIsValidDecimalDegree(String,String)`](#checkstringisvaliddecimaldegreestringstring)|
|[`CheckTypeIsSupport(Object,String)`](#checktypeissupportobjectstring)|
|[`CheckUriIsValid(Uri)`](#checkuriisvaliduri)|
|[`CheckWkbByteOrderIsValid(WkbByteOrder,String)`](#checkwkbbyteorderisvalidwkbbyteorderstring)|
|[`CheckWkbIsValid(Byte[],String)`](#checkwkbisvalidbyte[]string)|
|[`CheckWktIsValid(String,String)`](#checkwktisvalidstringstring)|
|[`CheckZoomLevelSetIsValid(ZoomLevelSet,String)`](#checkzoomlevelsetisvalidzoomlevelsetstring)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`MemberwiseClone()`](#memberwiseclone)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|N/A|

### Protected Constructors


### Public Properties


### Protected Properties


### Public Methods

#### `CheckAreaUnitIsValid(AreaUnit,String)`

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
|areaUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckAreIntegerStrings(IEnumerable<String>,String)`

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
|ids|IEnumerable<`String`>|N/A|
|parameterName|`String`|N/A|

---
#### `CheckBufferCapTypeIsValid(BufferCapType,String)`

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
|bufferCapType|[`BufferCapType`](../ThinkGeo.Core/ThinkGeo.Core.BufferCapType.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckBuildIndexModeIsValid(BuildIndexMode,String)`

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
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckCanModifyColumnStructure(Boolean)`

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
|canModifyColumnStructure|`Boolean`|N/A|

---
#### `CheckCanParseStringToDouble(String,String)`

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
|value|`String`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckCanvasHeightIsLargerThanZero(Double,String)`

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
|canvasHeight|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckCanvasWidthIsLargerThanZero(Double,String)`

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
|canvasWidth|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckColumnNameIsInFeature(String,IEnumerable<Feature>)`

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
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `CheckConnectionStringIsNotNull(String)`

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
|connectionString|`String`|N/A|

---
#### `CheckCustomStyleDuplicates(AreaStyle,LineStyle,PointStyle,TextStyle,Collection<Style>,Boolean)`

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
|defaultAreaStyle|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|N/A|
|defaultLineStyle|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|N/A|
|defaultPointStyle|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|
|defaultTextStyle|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|N/A|
|customStyles|Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>|N/A|
|isActive|`Boolean`|N/A|

---
#### `CheckDateTimeIsInRange(DateTime,String,DateTime,RangeCheckingInclusion,DateTime,RangeCheckingInclusion)`

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
|inputDate|`DateTime`|N/A|
|parameterName|`String`|N/A|
|minDate|`DateTime`|N/A|
|includeMinValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|
|maxDate|`DateTime`|N/A|
|includeMaxValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|

---
#### `CheckDbfColumnDecimalLengthIsValid(DbfColumnType,Int32)`

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
|columnType|[`DbfColumnType`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumnType.md)|N/A|
|decimalLength|`Int32`|N/A|

---
#### `CheckDistanceUnitIsValid(DistanceUnit,String)`

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
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckDrawingLevelIsValid(DrawingLevel,String)`

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
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckDrawingLineCapIsValid(DrawingLineCap,String)`

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
|drawingLineCap|[`DrawingLineCap`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineCap.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckDrawingLineJoinIsValid(DrawingLineJoin,String)`

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
|drawingLineJoin|[`DrawingLineJoin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineJoin.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckExtentIsValid(RectangleShape,String)`

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
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckFeatureColumnValueContainsColon(String,String)`

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
|value|`String`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckFeatureIsValid(Feature,String)`

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
|parameterName|`String`|N/A|

---
#### `CheckFeatureIsValid(Feature)`

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
#### `CheckFeatureSourceCanExecuteSqlQuery(Boolean)`

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
|canExecuteSqlQuery|`Boolean`|N/A|

---
#### `CheckFeatureSourceCollectionIsNotEmpty(Collection<FeatureSource>)`

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
|featureSources|Collection<[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)>|N/A|

---
#### `CheckFeatureSourceIsEditable(Boolean)`

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
|isEditable|`Boolean`|N/A|

---
#### `CheckFeatureSourceIsInTransaction(Boolean)`

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
|isInTransaction|`Boolean`|N/A|

---
#### `CheckFeatureSourceIsNotInTransaction(Boolean)`

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
|isInTransaction|`Boolean`|N/A|

---
#### `CheckFeatureSourceIsOpen(Boolean)`

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
|isOpen|`Boolean`|N/A|

---
#### `CheckFileIsExist(String)`

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

---
#### `CheckFileIsNotExist(String)`

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

---
#### `CheckGeoCanvasIsInDrawing(Boolean)`

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
|isDrawing|`Boolean`|N/A|

---
#### `CheckGeoDashCapIsValid(GeoDashCap,String)`

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
|geoDashCap|[`GeoDashCap`](../ThinkGeo.Core/ThinkGeo.Core.GeoDashCap.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckGeographyUnitIsMeter(GeographyUnit,String)`

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
|geographyUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckGeographyUnitIsValid(GeographyUnit,String)`

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
|geographyUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckGeoImageIsValid(GeoImage,String,GeoCanvas)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|parameterName|`String`|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|

---
#### `CheckGroupLayerIsNotEmpty(GeoCollection<Layer>)`

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
|layers|GeoCollection<[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)>|N/A|

---
#### `CheckHtmlColorIsValid(String,String)`

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
|htmlColor|`String`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckIconImageAndIconFilePathAreInvalid(String,GeoImage)`

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
|iconFilePath|`String`|N/A|
|iconImage|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

---
#### `CheckIEnumerableIsNotNullNorEmpty(IEnumerable,String,String)`

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
|values|`IEnumerable`|N/A|
|parameterName|`String`|N/A|
|exceptionMessage|`String`|N/A|

---
#### `CheckImageFormatIsValid(String,Collection<String>,String)`

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
|imageFormat|`String`|N/A|
|outputFormats|Collection<`String`>|N/A|
|exceptionMessage|`String`|N/A|

---
#### `CheckImageFormatSupport(String)`

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
|imageFormat|`String`|N/A|

---
#### `CheckImageIsNotNullInPointStyle(GeoImage)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

---
#### `CheckInputValueIsInRange(Double,String,Double,RangeCheckingInclusion,Double,RangeCheckingInclusion)`

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
|inputValue|`Double`|N/A|
|parameterName|`String`|N/A|
|minValue|`Double`|N/A|
|includeMinValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|
|maxValue|`Double`|N/A|
|includeMaxValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|

---
#### `CheckInputValueIsInRange(Double,String,Double,RangeCheckingInclusion,Double,RangeCheckingInclusion,String)`

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
|inputValue|`Double`|N/A|
|parameterName|`String`|N/A|
|minValue|`Double`|N/A|
|includeMinValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|
|maxValue|`Double`|N/A|
|includeMaxValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|
|exceptionMessage|`String`|N/A|

---
#### `CheckInputValueIsInRange(Double,String,Double,Double)`

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
|inputValue|`Double`|N/A|
|parameterName|`String`|N/A|
|minValue|`Double`|N/A|
|maxValue|`Double`|N/A|

---
#### `CheckInputValueIsLargerThan(Double,String,Double,RangeCheckingInclusion)`

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
|inputValue|`Double`|N/A|
|parameterName|`String`|N/A|
|minValue|`Double`|N/A|
|includeMinValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|

---
#### `CheckInputValueIsLargerThanZero(Double,String)`

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
|value|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckInputValueIsLessThan(Double,String,Double,RangeCheckingInclusion)`

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
|inputValue|`Double`|N/A|
|parameterName|`String`|N/A|
|maxValue|`Double`|N/A|
|includeMaxValue|[`RangeCheckingInclusion`](../ThinkGeo.Core/ThinkGeo.Core.RangeCheckingInclusion.md)|N/A|

---
#### `CheckInputValueIsNotNaNNorInfinity(Double,String)`

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
|value|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckInputValueIsValidDecimalDegree(Double,String)`

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
|value|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckItemInCollection(String,Collection<String>,String)`

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
|item|`String`|N/A|
|items|Collection<`String`>|N/A|
|exceptionMessage|`String`|N/A|

---
#### `CheckItemsInCollection(Collection<String>,Collection<String>,String)`

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
|items|Collection<`String`>|N/A|
|itemCollection|Collection<`String`>|N/A|
|exceptionMessage|`String`|N/A|

---
#### `CheckLatitudeIsInRange(Double,String)`

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
|latitude|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckLayerHasBoundingBox(Boolean)`

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
|hasBoundingBox|`Boolean`|N/A|

---
#### `CheckLayerIsNotOpenedNorDrawing(Boolean)`

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
|isOpenOrDrawing|`Boolean`|N/A|

---
#### `CheckLayerIsOpened(Boolean)`

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
|isOpen|`Boolean`|N/A|

---
#### `CheckLineDashStyleIsValid(LineDashStyle,String)`

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
|lineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckLongIsNotGreaterThanUInt32MaxValue(Int64)`

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
|value|`Int64`|N/A|

---
#### `CheckLongitudeIsInRange(Double,String)`

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
|longitude|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckNumberIsByte(Int32,String)`

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
|number|`Int32`|N/A|
|paramterName|`String`|N/A|

---
#### `CheckObjectIsNotNull(Object,String)`

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
|parameterObject|`Object`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckObjectIsNotNull(Object,String,String)`

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
|parameterObject|`Object`|N/A|
|parameterName|`String`|N/A|
|exceptionMessage|`String`|N/A|

---
#### `CheckObjectIsTargetType(Object,Type,String)`

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
|objectToTest|`Object`|N/A|
|targetType|`Type`|N/A|
|operationName|`String`|N/A|

---
#### `CheckObjectsAreNotAllNull(Object,Object,String,String)`

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
|firstObject|`Object`|N/A|
|secondObject|`Object`|N/A|
|firstParameterName|`String`|N/A|
|secondParameterName|`String`|N/A|

---
#### `CheckOverwriteModeIsValid(OverwriteMode,String)`

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
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckPanDirectionIsValid(PanDirection,String)`

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
|panDirection|[`PanDirection`](../ThinkGeo.Core/ThinkGeo.Core.PanDirection.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckPointLineIsIntersected(PointShape,String,LineShape,String,Double)`

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
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|pointShapeName|`String`|N/A|
|lineShape|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineShapeName|`String`|N/A|
|tolerance|`Double`|N/A|

---
#### `CheckPointSymbolTypeIsValid(PointSymbolType,String)`

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
|symbolType|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckPointTypeIsValid(PointType,String)`

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
|pointType|[`PointType`](../ThinkGeo.Core/ThinkGeo.Core.PointType.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckProjectionConverterIsOpen(Boolean)`

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
|isOpen|`Boolean`|N/A|

---
#### `CheckQueryTypeIsValid(QueryType,String)`

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
|queryType|[`QueryType`](../ThinkGeo.Core/ThinkGeo.Core.QueryType.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckRandomColorTypeIsValid(RandomColorType,String)`

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
|colorType|[`RandomColorType`](../ThinkGeo.Core/ThinkGeo.Core.RandomColorType.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckRasterSourceIsOpen(Boolean)`

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
|isOpen|`Boolean`|N/A|

---
#### `CheckRebuildRecordIdModeIsValid(BuildRecordIdMode,String)`

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
|rebuildRecordIdMode|[`BuildRecordIdMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildRecordIdMode.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckReturningColumnsTypeIsValid(ReturningColumnsType,String)`

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
|returningColumnsType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckRingOrderIsValid(RingOrder,String)`

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
|ringOrder|[`RingOrder`](../ThinkGeo.Core/ThinkGeo.Core.RingOrder.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckRtreeSpatialIndexIsOpen(Boolean)`

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
|isOpen|`Boolean`|N/A|

---
#### `CheckScaleIsLargerThanZero(Double,String)`

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
|imageScale|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckScaleIsValid(Double,String)`

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
|scale|`Double`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckShapeIsAreaBaseShape(BaseShape)`

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
#### `CheckShapeIsLineBaseShape(BaseShape)`

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
#### `CheckShapeIsMultipointShape(BaseShape)`

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
#### `CheckShapeIsPointShape(BaseShape)`

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
#### `CheckShapeIsValid(BaseShape,String)`

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
|parameterName|`String`|N/A|

---
#### `CheckShapeIsValidForOperation(BaseShape)`

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
#### `CheckShapeValidationModeIsValid(ShapeValidationMode,String)`

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
|shapeValidationMode|[`ShapeValidationMode`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationMode.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckSimplificationTypeIsValid(SimplificationType,String)`

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
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckSpatialIndexIsDeletable(Boolean)`

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
|isDeletable|`Boolean`|N/A|

---
#### `CheckStartingPointIsValid(StartingPoint,String)`

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
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckStatus()`

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
#### `CheckStreamIsWritable(Stream,String)`

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
|stream|`Stream`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckStringIsNotNullNorEmpty(String,String)`

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
|value|`String`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckStringIsNotNullNorEmptyForOperation(String,String)`

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
|value|`String`|N/A|
|exceptionMessage|`String`|N/A|

---
#### `CheckStringIsNotNullNorWhiteSpace(String,String)`

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
|value|`String`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckStringIsValidDecimalDegree(String,String)`

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
|value|`String`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckTypeIsSupport(Object,String)`

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
|instance|`Object`|N/A|
|typeName|`String`|N/A|

---
#### `CheckUriIsValid(Uri)`

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
|uri|`Uri`|N/A|

---
#### `CheckWkbByteOrderIsValid(WkbByteOrder,String)`

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
|wkbByteOrder|[`WkbByteOrder`](../ThinkGeo.Core/ThinkGeo.Core.WkbByteOrder.md)|N/A|
|parameterName|`String`|N/A|

---
#### `CheckWkbIsValid(Byte[],String)`

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
|wkb|`Byte[]`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckWktIsValid(String,String)`

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
|wkt|`String`|N/A|
|parameterName|`String`|N/A|

---
#### `CheckZoomLevelSetIsValid(ZoomLevelSet,String)`

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
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|
|parameterName|`String`|N/A|

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

### Protected Methods

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

### Public Events


