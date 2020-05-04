# ShapeFileFeatureLayer


## Inheritance Hierarchy

+ `Object`
  + [`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)
    + [`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)
      + **`ShapeFileFeatureLayer`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ShapeFileFeatureLayer()`](#shapefilefeaturelayer)|
|[`ShapeFileFeatureLayer(String)`](#shapefilefeaturelayerstring)|
|[`ShapeFileFeatureLayer(String,FileAccess)`](#shapefilefeaturelayerstringfileaccess)|
|[`ShapeFileFeatureLayer(String,String)`](#shapefilefeaturelayerstringstring)|
|[`ShapeFileFeatureLayer(String,String,FileAccess)`](#shapefilefeaturelayerstringstringfileaccess)|
|[`ShapeFileFeatureLayer(String,String,FileAccess,Encoding)`](#shapefilefeaturelayerstringstringfileaccessencoding)|

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
|[`Encoding`](#encoding)|`Encoding`|This property gets and sets the encoding information for the DBF.|
|[`FeatureIdsToExclude`](#featureidstoexclude)|Collection<`String`>|N/A|
|[`FeatureSource`](#featuresource)|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|N/A|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`HasBoundingBox`](#hasboundingbox)|`Boolean`|This property checks to see if a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs. In ShapeFileFeatureLayer, we override this API and mark it as true.|
|[`IndexPathFilename`](#indexpathfilename)|`String`|This property returns the path and filename of the index file you want to represent.|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|N/A|
|[`IsNegative`](#isnegative)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`ProgressiveDrawingRecordsCount`](#progressivedrawingrecordscount)|`Int32`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`QueryTools`](#querytools)|[`QueryTools`](../ThinkGeo.Core/ThinkGeo.Core.QueryTools.md)|N/A|
|[`ReadWriteMode`](#readwritemode)|`FileAccess`|N/A|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`RequestDrawingInterval`](#requestdrawinginterval)|`TimeSpan`|N/A|
|[`RequireIndex`](#requireindex)|`Boolean`|This property gets and sets whether an index is required when reading data. The default value is true.|
|[`ShapePathFilename`](#shapepathfilename)|`String`|This property returns the path and filename of the Shape File you want to represent.|
|[`SimplificationAreaInPixel`](#simplificationareainpixel)|`Int32`|N/A|
|[`ThreadSafe`](#threadsafe)|[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)|N/A|
|[`Transparency`](#transparency)|`Single`|N/A|
|[`UsingSpatialIndex`](#usingspatialindex)|`Boolean`|This property gets whether the Shape File FeatureSource has an index or not.|
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
|[`BuildIndexFile(String)`](#buildindexfilestring)|
|[`BuildIndexFile(String,String,BuildIndexMode)`](#buildindexfilestringstringbuildindexmode)|
|[`BuildIndexFile(String,BuildIndexMode)`](#buildindexfilestringbuildindexmode)|
|[`BuildIndexFile(IEnumerable<Feature>,String)`](#buildindexfileienumerable<feature>string)|
|[`BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter)`](#buildindexfileienumerable<feature>stringprojectionconverter)|
|[`BuildIndexFile(IEnumerable<Feature>,String,BuildIndexMode)`](#buildindexfileienumerable<feature>stringbuildindexmode)|
|[`BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter,BuildIndexMode)`](#buildindexfileienumerable<feature>stringprojectionconverterbuildindexmode)|
|[`BuildIndexFile(String,String,String,String,BuildIndexMode)`](#buildindexfilestringstringstringstringbuildindexmode)|
|[`BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode)`](#buildindexfilestringstringprojectionconverterstringstringbuildindexmode)|
|[`BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode,Encoding)`](#buildindexfilestringstringprojectionconverterstringstringbuildindexmodeencoding)|
|[`BuildRecordIdColumn(String,String,BuildRecordIdMode)`](#buildrecordidcolumnstringstringbuildrecordidmode)|
|[`BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32)`](#buildrecordidcolumnstringstringbuildrecordidmodeint32)|
|[`BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32,Encoding)`](#buildrecordidcolumnstringstringbuildrecordidmodeint32encoding)|
|[`CloneDeep()`](#clonedeep)|
|[`CloneShapeFileStructure(String,String)`](#cloneshapefilestructurestringstring)|
|[`CloneShapeFileStructure(String,String,OverwriteMode)`](#cloneshapefilestructurestringstringoverwritemode)|
|[`CloneShapeFileStructure(String,String,OverwriteMode,Encoding)`](#cloneshapefilestructurestringstringoverwritemodeencoding)|
|[`Close()`](#close)|
|[`CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>)`](#createshapefileshapefiletypestringienumerable<dbfcolumn>)|
|[`CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding)`](#createshapefileshapefiletypestringienumerable<dbfcolumn>encoding)|
|[`CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding,OverwriteMode)`](#createshapefileshapefiletypestringienumerable<dbfcolumn>encodingoverwritemode)|
|[`Draw(GeoCanvas,Collection<SimpleCandidate>)`](#drawgeocanvascollection<simplecandidate>)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
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
|[`Reproject(String,String,ProjectionConverter,OverwriteMode)`](#reprojectstringstringprojectionconverteroverwritemode)|
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
|[`Validate()`](#validate)|

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
|[`ShapeFileFeatureLayer()`](#shapefilefeaturelayer)|
|[`ShapeFileFeatureLayer(String)`](#shapefilefeaturelayerstring)|
|[`ShapeFileFeatureLayer(String,FileAccess)`](#shapefilefeaturelayerstringfileaccess)|
|[`ShapeFileFeatureLayer(String,String)`](#shapefilefeaturelayerstringstring)|
|[`ShapeFileFeatureLayer(String,String,FileAccess)`](#shapefilefeaturelayerstringstringfileaccess)|
|[`ShapeFileFeatureLayer(String,String,FileAccess,Encoding)`](#shapefilefeaturelayerstringstringfileaccessencoding)|

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
#### `Encoding`

**Summary**

   *This property gets and sets the encoding information for the DBF.*

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

   *This property checks to see if a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs. In ShapeFileFeatureLayer, we override this API and mark it as true.*

**Remarks**

   *The default implementation in the base class returns false.*

**Return Value**

`Boolean`

---
#### `IndexPathFilename`

**Summary**

   *This property returns the path and filename of the index file you want to represent.*

**Remarks**

   *None*

**Return Value**

`String`

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
#### `ProgressiveDrawingRecordsCount`

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
#### `QueryTools`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`QueryTools`](../ThinkGeo.Core/ThinkGeo.Core.QueryTools.md)

---
#### `ReadWriteMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FileAccess`

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
#### `RequireIndex`

**Summary**

   *This property gets and sets whether an index is required when reading data. The default value is true.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `ShapePathFilename`

**Summary**

   *This property returns the path and filename of the Shape File you want to represent.*

**Remarks**

   *None*

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
#### `ThreadSafe`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)

---
#### `Transparency`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `UsingSpatialIndex`

**Summary**

   *This property gets whether the Shape File FeatureSource has an index or not.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

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

#### `BuildIndexFile(String)`

**Summary**

   *This method builds a spatial index for the layer.*

**Remarks**

   *This overload allows you to pass in the Shape File.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pathFilename|`String`|The path and filename to the Shape File.|

---
#### `BuildIndexFile(String,String,BuildIndexMode)`

**Summary**

   *This method build a spatial index for the shape file which increases accessspeed.*

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
#### `BuildIndexFile(String,BuildIndexMode)`

**Summary**

   *This method builds a spatial index for the layer.*

**Remarks**

   *This overload allows you to pass in the Shape File and determines if we rebuild an index file that already exists.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pathFilename|`String`|This parameter is the matching pattern that defines which Shape Files to include.|
|rebuildExistingIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines whether an index file will be rebuilt if it already exists.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String)`

**Summary**

   *This method builds a spatial index for the specified group of features.*

**Remarks**

   *This overload allows you to pass in a group of features and specify the index filename to use.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the features for which to build the index.|
|indexPathFilename|`String`|This parameter specifies the target index path and filename.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter)`

**Summary**

   *This method builds a spatial index for the specified group of features and target projection.*

**Remarks**

   *This overload allows you to pass in a group of features and specify a target projection and the index filename to use.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the features for which to build the index.|
|indexPathFilename|`String`|This parameter specifies the target index path and filename.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter specifies the projection to build index against those features.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String,BuildIndexMode)`

**Summary**

   *This method builds a spatial index for the specified group of features.*

**Remarks**

   *This overload allows you to pass in a group of features and specify the index filename to use.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the features for which to build the index.|
|indexPathFilename|`String`|This parameter determines the target index path filename.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines whether an index file will be rebuilt if it already exists.|

---
#### `BuildIndexFile(IEnumerable<Feature>,String,ProjectionConverter,BuildIndexMode)`

**Summary**

   *This method builds a spatial index for the specified group of features.*

**Remarks**

   *This overload allows you to pass in a group of features and specify a target projection and the index filename to use.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the features for which to build the index.|
|indexPathFilename|`String`|This parameter determines the target index path filename.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter determines the Projection to build index against those features.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines whether an index file will be rebuilt if it already exists.|

---
#### `BuildIndexFile(String,String,String,String,BuildIndexMode)`

**Summary**

   *This method builds a spatial index only for those features that satisfy a regular expression.*

**Remarks**

   *This overload allows you to pass in a Shape File and specify the index filename to use.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter specifies the Shape File for which to build the index.|
|indexPathFilename|`String`|This parameter determines the target index path filename.|
|columnName|`String`|This parameter determines the column name whose values will be tested against the regular expression.|
|regularExpression|`String`|This parameter represents the regular expression to test against each feature for inclusion in the index.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines whether an index file will be rebuilt if it already exists.|

---
#### `BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode)`

**Summary**

   *This method builds a spatial index only for those features that satisfy a regular expression, based on a passed-in projection.*

**Remarks**

   *This overload allows you to pass in a Shape File and specify the index filename to use.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter specifies the Shape File for which to build the index.|
|indexPathFilename|`String`|This parameter determines the target index path filename.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter determines the projection that will be used to build the index against those features that satisfy the regular expression.|
|columnName|`String`|This parameter determines the column name whose values will be tested against the regular expression.|
|regularExpression|`String`|This parameter represents the regular expression to test against each feature for inclusion in the index.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines whether an index file will be rebuilt if it already exists.|

---
#### `BuildIndexFile(String,String,ProjectionConverter,String,String,BuildIndexMode,Encoding)`

**Summary**

   *This method builds a spatial index only for those features that satisfy a regular expression, based on a passed-in projection.*

**Remarks**

   *This overload allows you to pass in a Shape File and specify the index filename to use.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapePathFilename|`String`|This parameter specifies the Shape File for which to build the index.|
|indexPathFilename|`String`|This parameter determines the target index path filename.|
|projectionConverter|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|This parameter determines the projection that will be used to build the index against those features that satisfy the regular expression.|
|columnName|`String`|This parameter determines the column name whose values will be tested against the regular expression.|
|regularExpression|`String`|This parameter represents the regular expression to test against each feature for inclusion in the index.|
|buildIndexMode|[`BuildIndexMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildIndexMode.md)|This parameter determines whether an index file will be rebuilt if it already exists.|
|encoding|`Encoding`|This parameter specifies the encoding information used in the source DBF file.|

---
#### `BuildRecordIdColumn(String,String,BuildRecordIdMode)`

**Summary**

   *Static API used to build RecordId. The Id will start from 0.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeFilename|`String`|The name of the target ShapeFile on which to base the newly built RecordId.|
|fieldname|`String`|The field name for the RecordId.|
|rebuildNeeded|[`BuildRecordIdMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildRecordIdMode.md)|The RecordId build mode.|

---
#### `BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32)`

**Summary**

   *Static API used to build RecordId from the specified starting Id number.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeFilename|`String`|The name of the target ShapeFile on which to base the newly built RecordId.|
|fieldname|`String`|The field name for the RecordId.|
|rebuildNeeded|[`BuildRecordIdMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildRecordIdMode.md)|The RecordId build mode.|
|startNumber|`Int32`|The starting Id number of the RecordId.|

---
#### `BuildRecordIdColumn(String,String,BuildRecordIdMode,Int32,Encoding)`

**Summary**

   *Static API used to build RecordId from the specified starting Id number.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeFilename|`String`|The name of the target ShapeFile on which to base the newly built RecordId.|
|fieldname|`String`|The field name for the RecordId.|
|rebuildNeeded|[`BuildRecordIdMode`](../ThinkGeo.Core/ThinkGeo.Core.BuildRecordIdMode.md)|The RecordId build mode.|
|startNumber|`Int32`|The starting Id number of the RecordId.|
|encoding|`Encoding`|This parameter specifies the encoding information in the DBF.|

---
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
#### `CloneShapeFileStructure(String,String)`

**Summary**

   *Clone the structure from the source ShapeFile to the target ShapeFile. After cloning the structure, the target ShapeFile will have the same type and the same DBF columns as the source ShapeFile, but without any records in it.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShapePathFilename|`String`|The source Shape File to be cloned.|
|targetShapePathFilename|`String`|The target Shape File, which will have the same structure as the source Shape File after cloning operation is complete.|

---
#### `CloneShapeFileStructure(String,String,OverwriteMode)`

**Summary**

   *Clone the structure from the source ShapeFile to the target ShapeFile. After cloning the structure, the target ShapeFile will have the same type and the same DBF columns as the source ShapeFile, but without any records in it.*

**Remarks**

   *An exception will be thown when the target ShapeFile does not exist and the overwrite mode is set to DoNotOverwrite.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShapePathFilename|`String`|The source Shape File to be cloned.|
|targetShapePathFilename|`String`|The target Shape File, which will have the same structure as the source Shape File after cloning operation is complete.|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|This parameter specifies the overwrite mode when the target ShapeFile already exists.|

---
#### `CloneShapeFileStructure(String,String,OverwriteMode,Encoding)`

**Summary**

   *Clone the structure from the source ShapeFile to the target ShapeFile. After cloning the structure, the target ShapeFile will have the same type and the same DBF columns as the source ShapeFile, but without any records in it.*

**Remarks**

   *An exception will be thown when the target ShapeFile does not exist and the overwrite mode is set to DoNotOverwrite.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShapePathFilename|`String`|The source Shape File to be cloned.|
|targetShapePathFilename|`String`|The target Shape File, which will have the same structure as the source Shape File after cloning operation is complete.|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|This parameter specifies the overwrite mode when the target ShapeFile already exists.|
|encoding|`Encoding`|This parameter specifies the encoding information in the source Shape File.|

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
#### `CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>)`

**Summary**

   *Static API to create a new Shape File.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeType|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|This parameter specifies the the Shape File type for the target Shape File.|
|pathFilename|`String`|This parameter specifies the Shape filename for the target Shape File.|
|databaseColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|This parameter specifies the DBF column information for the target Shape File.|

---
#### `CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding)`

**Summary**

   *Static API to create a new Shape File.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeType|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|This parameter specifies the the Shape File type for the target Shape File.|
|pathFilename|`String`|This parameter specifies the Shape filename for the target Shape File.|
|databaseColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|This parameter specifies the DBF column information for the target Shape File.|
|encoding|`Encoding`|This parameter specifies the DBF encoding infromation for the target Shape File.|

---
#### `CreateShapeFile(ShapeFileType,String,IEnumerable<DbfColumn>,Encoding,OverwriteMode)`

**Summary**

   *Static API to create a new Shape File.*

**Remarks**

   *An exception will be thown when the target Shape File does not exist while the overwrite mode is set to DoNotOverwrite.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeType|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|This parameter specifies the the Shape File type for the target Shape File.|
|pathFilename|`String`|This parameter specifies the Shape filename for the target Shape File.|
|databaseColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|This parameter specifies the DBF column information for the target Shape File.|
|encoding|`Encoding`|This parameter specifies the DBF encoding infromation for the target Shape File.|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|This parameter specifies the override mode when the target Shape File exists.|

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
#### `GetShapeFileType()`

**Summary**

   *Get the Shape File type for the Shape File FeatureSource.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ShapeFileType`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileType.md)|The ShapeFileType for the ShapeFile FeatureSource.|

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

*If you choose you can pass in your own stream to represent the file. The stream can come from a variety of places such as isolated storage, a compressed file, and encrypted stream. When the Image is finished with the stream it will dispose of it so be sure to keep this in mind when passing the stream in. If you do not pass in a alternate stream the class will attempt to load the file from the file system using the PathFilename property.*

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


