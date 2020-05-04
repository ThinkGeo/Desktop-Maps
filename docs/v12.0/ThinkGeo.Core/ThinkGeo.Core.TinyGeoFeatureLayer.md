# TinyGeoFeatureLayer


## Inheritance Hierarchy

+ `Object`
  + [`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)
    + [`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)
      + **`TinyGeoFeatureLayer`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`TinyGeoFeatureLayer()`](#tinygeofeaturelayer)|
|[`TinyGeoFeatureLayer(String)`](#tinygeofeaturelayerstring)|
|[`TinyGeoFeatureLayer(String,String)`](#tinygeofeaturelayerstringstring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`Background`](#background)|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|
|[`BlueTranslation`](#bluetranslation)|`Single`|N/A|
|[`ColorMappings`](#colormappings)|Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingMarginInPixel`](#drawingmargininpixel)|`Single`|N/A|
|[`DrawingQuality`](#drawingquality)|[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)|N/A|
|[`DrawingTime`](#drawingtime)|`TimeSpan`|N/A|
|[`EditTools`](#edittools)|[`EditTools`](../ThinkGeo.Core/ThinkGeo.Core.EditTools.md)|N/A|
|[`FeatureIdsToExclude`](#featureidstoexclude)|Collection<`String`>|N/A|
|[`FeatureSource`](#featuresource)|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|N/A|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`HasBoundingBox`](#hasboundingbox)|`Boolean`|N/A|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|N/A|
|[`IsNegative`](#isnegative)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`Password`](#password)|`String`|This property gets or sets the password of the TinyGeo file.|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`QueryTools`](#querytools)|[`QueryTools`](../ThinkGeo.Core/ThinkGeo.Core.QueryTools.md)|N/A|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`RequestDrawingInterval`](#requestdrawinginterval)|`TimeSpan`|N/A|
|[`ThreadSafe`](#threadsafe)|[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)|N/A|
|[`TinyGeoPathFilename`](#tinygeopathfilename)|`String`|This property returns the path and file of the TinyGeo file you want to use.|
|[`Transparency`](#transparency)|`Single`|N/A|
|[`WrappingExtent`](#wrappingextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`WrappingMode`](#wrappingmode)|[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)|N/A|
|[`ZoomLevelSet`](#zoomlevelset)|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FetchedCount`](#fetchedcount)|`Int64`|N/A|
|[`FetchTime`](#fetchtime)|`TimeSpan`|N/A|
|[`IsOpenCore`](#isopencore)|`Boolean`|N/A|
|[`StyleTime`](#styletime)|`TimeSpan`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Close()`](#close)|
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
|[`Draw(GeoCanvas,Collection<SimpleCandidate>)`](#drawgeocanvascollection<simplecandidate>)|
|[`EncryptTinyGeoFile(String,String,String)`](#encrypttinygeofilestringstringstring)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetHashCode()`](#gethashcode)|
|[`GetOptimalPrecision(String,GeographyUnit,DistanceUnit,TinyGeoPrecisionMode)`](#getoptimalprecisionstringgeographyunitdistanceunittinygeoprecisionmode)|
|[`GetOptimalPrecision(FeatureLayer,GeographyUnit,DistanceUnit,TinyGeoPrecisionMode)`](#getoptimalprecisionfeaturelayergeographyunitdistanceunittinygeoprecisionmode)|
|[`GetTinyGeoFileType()`](#gettinygeofiletype)|
|[`GetType()`](#gettype)|
|[`Open()`](#open)|
|[`RequestDrawing()`](#requestdrawing)|
|[`RequestDrawing(RectangleShape)`](#requestdrawingrectangleshape)|
|[`RequestDrawing(IEnumerable<RectangleShape>)`](#requestdrawingienumerable<rectangleshape>)|
|[`RequestDrawing(TimeSpan)`](#requestdrawingtimespan)|
|[`RequestDrawing(TimeSpan,RequestDrawingBufferTimeType)`](#requestdrawingtimespanrequestdrawingbuffertimetype)|
|[`RequestDrawing(RectangleShape,TimeSpan)`](#requestdrawingrectangleshapetimespan)|
|[`RequestDrawing(RectangleShape,TimeSpan,RequestDrawingBufferTimeType)`](#requestdrawingrectangleshapetimespanrequestdrawingbuffertimetype)|
|[`RequestDrawing(IEnumerable<RectangleShape>,TimeSpan)`](#requestdrawingienumerable<rectangleshape>timespan)|
|[`RequestDrawing(IEnumerable<RectangleShape>,TimeSpan,RequestDrawingBufferTimeType)`](#requestdrawingienumerable<rectangleshape>timespanrequestdrawingbuffertimetype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`CloseCore()`](#closecore)|
|[`DrawAttributionCore(GeoCanvas,String)`](#drawattributioncoregeocanvasstring)|
|[`DrawCore(GeoCanvas,Collection<SimpleCandidate>)`](#drawcoregeocanvascollection<simplecandidate>)|
|[`DrawException(GeoCanvas,Exception)`](#drawexceptiongeocanvasexception)|
|[`DrawExceptionCore(GeoCanvas,Exception)`](#drawexceptioncoregeocanvasexception)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetWrappingFeaturesForDrawing(WrappingWorldDirection,RectangleShape,Double,Double,IEnumerable<String>,RectangleShape)`](#getwrappingfeaturesfordrawingwrappingworlddirectionrectangleshapedoubledoubleienumerable<string>rectangleshape)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnDrawingAttribution(DrawingAttributionLayerEventArgs)`](#ondrawingattributiondrawingattributionlayereventargs)|
|[`OnDrawingException(DrawingExceptionLayerEventArgs)`](#ondrawingexceptiondrawingexceptionlayereventargs)|
|[`OnDrawingFeatures(DrawingFeaturesEventArgs)`](#ondrawingfeaturesdrawingfeatureseventargs)|
|[`OnDrawingProgressChanged(DrawingProgressChangedEventArgs)`](#ondrawingprogresschangeddrawingprogresschangedeventargs)|
|[`OnDrawingWrappingFeatures(DrawingWrappingFeaturesFeatureLayerEventArgs)`](#ondrawingwrappingfeaturesdrawingwrappingfeaturesfeaturelayereventargs)|
|[`OnDrawnAttribution(DrawnAttributionLayerEventArgs)`](#ondrawnattributiondrawnattributionlayereventargs)|
|[`OnDrawnException(DrawnExceptionLayerEventArgs)`](#ondrawnexceptiondrawnexceptionlayereventargs)|
|[`OnRequestedDrawing(RequestedDrawingLayerEventArgs)`](#onrequesteddrawingrequesteddrawinglayereventargs)|
|[`OnRequestingDrawing(RequestingDrawingLayerEventArgs)`](#onrequestingdrawingrequestingdrawinglayereventargs)|
|[`OpenCore()`](#opencore)|
|[`SetupTools()`](#setuptools)|
|[`SetupToolsCore()`](#setuptoolscore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`StreamLoading`](#streamloading)|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|
|[`DrawingFeatures`](#drawingfeatures)|[`DrawingFeaturesEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFeaturesEventArgs.md)|N/A|
|[`DrawingWrappingFeatures`](#drawingwrappingfeatures)|[`DrawingWrappingFeaturesFeatureLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingWrappingFeaturesFeatureLayerEventArgs.md)|N/A|
|[`DrawingProgressChanged`](#drawingprogresschanged)|[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)|N/A|
|[`DrawingException`](#drawingexception)|[`DrawingExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionLayerEventArgs.md)|N/A|
|[`DrawnException`](#drawnexception)|[`DrawnExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnExceptionLayerEventArgs.md)|N/A|
|[`DrawingAttribution`](#drawingattribution)|[`DrawingAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAttributionLayerEventArgs.md)|N/A|
|[`DrawnAttribution`](#drawnattribution)|[`DrawnAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAttributionLayerEventArgs.md)|N/A|
|[`RequestedDrawing`](#requesteddrawing)|[`RequestedDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDrawingLayerEventArgs.md)|N/A|
|[`RequestingDrawing`](#requestingdrawing)|[`RequestingDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDrawingLayerEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`TinyGeoFeatureLayer()`](#tinygeofeaturelayer)|
|[`TinyGeoFeatureLayer(String)`](#tinygeofeaturelayerstring)|
|[`TinyGeoFeatureLayer(String,String)`](#tinygeofeaturelayerstringstring)|

### Protected Constructors


### Public Properties

#### `Attribution`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Background`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)

---
#### `BlueTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `ColorMappings`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>

---
#### `DrawingExceptionMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)

---
#### `DrawingMarginInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `DrawingQuality`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)

---
#### `DrawingTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `EditTools`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`EditTools`](../ThinkGeo.Core/ThinkGeo.Core.EditTools.md)

---
#### `FeatureIdsToExclude`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `FeatureSource`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)

---
#### `GreenTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `HasBoundingBox`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsGrayscale`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsNegative`

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
#### `IsVisible`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `KeyColors`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>

---
#### `MaxRecordsToDraw`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

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
#### `QueryTools`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`QueryTools`](../ThinkGeo.Core/ThinkGeo.Core.QueryTools.md)

---
#### `RedTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `RequestDrawingInterval`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `ThreadSafe`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)

---
#### `TinyGeoPathFilename`

**Summary**

   *This property returns the path and file of the TinyGeo file you want to use.*

**Remarks**

   *When you specify the path and file name it should be in the correct format as such however the file does not need to exists on the file system. This is to allow us to accept streams supplied by the developer at runtime. If you choose to provide a file that exists then we will attempt to use it. If we cannot find it then we will raise the SteamLoading event and allow you to supply the stream. For example you can pass in "C:\NotARealPath\File1.tgeo" which does not exists on the file system. When we raise the event for you to supply a stream we will pass to you the path and file name for you to differentiate the files.*

**Return Value**

`String`

---
#### `Transparency`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `WrappingExtent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `WrappingMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)

---
#### `ZoomLevelSet`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)

---

### Protected Properties

#### `FetchedCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int64`

---
#### `FetchTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `IsOpenCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `StyleTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---

### Public Methods

#### `CloneDeep()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)|N/A|

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
#### `Draw(GeoCanvas,Collection<SimpleCandidate>)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

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
#### `GetOptimalPrecision(String,GeographyUnit,DistanceUnit,TinyGeoPrecisionMode)`

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
|shapePathFilename|`String`|N/A|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|returningDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|precisionMode|[`TinyGeoPrecisionMode`](../ThinkGeo.Core/ThinkGeo.Core.TinyGeoPrecisionMode.md)|N/A|

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
#### `RequestDrawing()`

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
#### `RequestDrawing(RectangleShape)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `RequestDrawing(IEnumerable<RectangleShape>)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|

---
#### `RequestDrawing(TimeSpan)`

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
|bufferTime|`TimeSpan`|N/A|

---
#### `RequestDrawing(TimeSpan,RequestDrawingBufferTimeType)`

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
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

---
#### `RequestDrawing(RectangleShape,TimeSpan)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|bufferTime|`TimeSpan`|N/A|

---
#### `RequestDrawing(RectangleShape,TimeSpan,RequestDrawingBufferTimeType)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

---
#### `RequestDrawing(IEnumerable<RectangleShape>,TimeSpan)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|bufferTime|`TimeSpan`|N/A|

---
#### `RequestDrawing(IEnumerable<RectangleShape>,TimeSpan,RequestDrawingBufferTimeType)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

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

#### `CloneDeepCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)|N/A|

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
#### `DrawAttributionCore(GeoCanvas,String)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|attribution|`String`|N/A|

---
#### `DrawCore(GeoCanvas,Collection<SimpleCandidate>)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `DrawException(GeoCanvas,Exception)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|e|`Exception`|N/A|

---
#### `DrawExceptionCore(GeoCanvas,Exception)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|e|`Exception`|N/A|

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
#### `GetWrappingFeaturesForDrawing(WrappingWorldDirection,RectangleShape,Double,Double,IEnumerable<String>,RectangleShape)`

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
|wrappingFeatureDirection|[`WrappingWorldDirection`](../ThinkGeo.Core/ThinkGeo.Core.WrappingWorldDirection.md)|N/A|
|drawingWorldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Double`|N/A|
|canvasHeight|`Double`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|
|wrappingWorldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `OnDrawingAttribution(DrawingAttributionLayerEventArgs)`

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
|args|[`DrawingAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAttributionLayerEventArgs.md)|N/A|

---
#### `OnDrawingException(DrawingExceptionLayerEventArgs)`

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
|e|[`DrawingExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionLayerEventArgs.md)|N/A|

---
#### `OnDrawingFeatures(DrawingFeaturesEventArgs)`

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
|e|[`DrawingFeaturesEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFeaturesEventArgs.md)|N/A|

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
#### `OnDrawingWrappingFeatures(DrawingWrappingFeaturesFeatureLayerEventArgs)`

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
|e|[`DrawingWrappingFeaturesFeatureLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingWrappingFeaturesFeatureLayerEventArgs.md)|N/A|

---
#### `OnDrawnAttribution(DrawnAttributionLayerEventArgs)`

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
|args|[`DrawnAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAttributionLayerEventArgs.md)|N/A|

---
#### `OnDrawnException(DrawnExceptionLayerEventArgs)`

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
|e|[`DrawnExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnExceptionLayerEventArgs.md)|N/A|

---
#### `OnRequestedDrawing(RequestedDrawingLayerEventArgs)`

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
|eventArgs|[`RequestedDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDrawingLayerEventArgs.md)|N/A|

---
#### `OnRequestingDrawing(RequestingDrawingLayerEventArgs)`

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
|eventArgs|[`RequestingDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDrawingLayerEventArgs.md)|N/A|

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
#### `SetupTools()`

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
#### `SetupToolsCore()`

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

### Public Events

#### StreamLoading

*N/A*

**Remarks**

*If you choose you can pass in your own stream to represent the file. The stream can come from a variety of places such as isolated storage, a compressed file, and encrypted stream. When the Image is finished with the stream it will dispose of it so be sure to keep this in mind when passing the stream in. If you do not pass in a alternate stream the class will attempt to load the file from the file system using the TinyGeoPathFilename property.*

**Event Arguments**

[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)

#### DrawingFeatures

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingFeaturesEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFeaturesEventArgs.md)

#### DrawingWrappingFeatures

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingWrappingFeaturesFeatureLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingWrappingFeaturesFeatureLayerEventArgs.md)

#### DrawingProgressChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)

#### DrawingException

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionLayerEventArgs.md)

#### DrawnException

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnExceptionLayerEventArgs.md)

#### DrawingAttribution

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAttributionLayerEventArgs.md)

#### DrawnAttribution

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAttributionLayerEventArgs.md)

#### RequestedDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RequestedDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDrawingLayerEventArgs.md)

#### RequestingDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RequestingDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDrawingLayerEventArgs.md)


