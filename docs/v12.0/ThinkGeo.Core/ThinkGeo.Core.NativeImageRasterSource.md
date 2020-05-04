# NativeImageRasterSource


## Inheritance Hierarchy

+ `Object`
  + [`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)
    + **`NativeImageRasterSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`NativeImageRasterSource()`](#nativeimagerastersource)|
|[`NativeImageRasterSource(String)`](#nativeimagerastersourcestring)|
|[`NativeImageRasterSource(String,String)`](#nativeimagerastersourcestringstring)|
|[`NativeImageRasterSource(String,RectangleShape)`](#nativeimagerastersourcestringrectangleshape)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`BlueTranslation`](#bluetranslation)|`Single`|N/A|
|[`FileName`](#filename)|`String`|This property returns the filename part of the PathFilename.|
|[`FilePath`](#filepath)|`String`|This property returns the path part of the PathFilename.|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`ImagePathFilename`](#imagepathfilename)|`String`|This property returns the path and filename of the image you want to represent.|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|This property gets and sets whether the image shows in grayscale.|
|[`IsNegative`](#isnegative)|`Boolean`|This property gets and sets whether the image shows as negative (inverted colors).|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`ScaleFactor`](#scalefactor)|`Double`|N/A|
|[`Transparency`](#transparency)|`Single`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Close()`](#close)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetHashCode()`](#gethashcode)|
|[`GetHorizontalResolution()`](#gethorizontalresolution)|
|[`GetImage(RectangleShape,Int32,Int32)`](#getimagerectangleshapeint32int32)|
|[`GetImageHeight()`](#getimageheight)|
|[`GetImageWidth()`](#getimagewidth)|
|[`GetType()`](#gettype)|
|[`GetVerticalResolution()`](#getverticalresolution)|
|[`GetWorldFileText()`](#getworldfiletext)|
|[`Open()`](#open)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`CloseCore()`](#closecore)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetImageCore(RectangleShape,Int32,Int32)`](#getimagecorerectangleshapeint32int32)|
|[`GetImageHeightCore()`](#getimageheightcore)|
|[`GetImageWidthCore()`](#getimagewidthcore)|
|[`GetWrappingImageLeft(RectangleShape,Double,Double,RectangleShape)`](#getwrappingimageleftrectangleshapedoubledoublerectangleshape)|
|[`GetWrappingImageRight(RectangleShape,Double,Double,RectangleShape)`](#getwrappingimagerightrectangleshapedoubledoublerectangleshape)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnChangedSourceStatus(ChangedSourceStatusEventArgs)`](#onchangedsourcestatuschangedsourcestatuseventargs)|
|[`OnClosedRasterSource(ClosedRasterSourceEventArgs)`](#onclosedrastersourceclosedrastersourceeventargs)|
|[`OnClosingRasterSource(ClosingRasterSourceEventArgs)`](#onclosingrastersourceclosingrastersourceeventargs)|
|[`OnOpenedRasterSource(OpenedRasterSourceEventArgs)`](#onopenedrastersourceopenedrastersourceeventargs)|
|[`OnOpeningRasterSource(OpeningRasterSourceEventArgs)`](#onopeningrastersourceopeningrastersourceeventargs)|
|[`OnStreamLoading(StreamLoadingEventArgs)`](#onstreamloadingstreamloadingeventargs)|
|[`OpenCore()`](#opencore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`StreamLoading`](#streamloading)|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|
|[`OpeningRasterSource`](#openingrastersource)|[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)|N/A|
|[`OpenedRasterSource`](#openedrastersource)|[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)|N/A|
|[`ClosingRasterSource`](#closingrastersource)|[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)|N/A|
|[`ClosedRasterSource`](#closedrastersource)|[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`NativeImageRasterSource()`](#nativeimagerastersource)|
|[`NativeImageRasterSource(String)`](#nativeimagerastersourcestring)|
|[`NativeImageRasterSource(String,String)`](#nativeimagerastersourcestringstring)|
|[`NativeImageRasterSource(String,RectangleShape)`](#nativeimagerastersourcestringrectangleshape)|

### Protected Constructors


### Public Properties

#### `BlueTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `FileName`

**Summary**

   *This property returns the filename part of the PathFilename.*

**Remarks**

   *None*

**Return Value**

`String`

---
#### `FilePath`

**Summary**

   *This property returns the path part of the PathFilename.*

**Remarks**

   *None*

**Return Value**

`String`

---
#### `GreenTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `ImagePathFilename`

**Summary**

   *This property returns the path and filename of the image you want to represent.*

**Remarks**

   *When you specify the path and filename, it should be in the correct format as such; however, the file does not need to actually exist on the file system. This is to allow us to accept streams supplied by the developer at runtime. If you choose to provide a file that exists, we will attempt to use it. If we cannot find it, then we will raise the SteamLoading event and allow you to supply the stream. For example, you can pass in "C:\NotARealPath\File1.jpg" which does not exist on the file system. When we raise the event for you to supply a stream, we will pass to you the path and filename so you can differentiate the files.*

**Return Value**

`String`

---
#### `IsGrayscale`

**Summary**

   *This property gets and sets whether the image shows in grayscale.*

**Remarks**

   *This property allows you to show the image in grayscale.*

**Return Value**

`Boolean`

---
#### `IsNegative`

**Summary**

   *This property gets and sets whether the image shows as negative (inverted colors).*

**Remarks**

   *You can set this property to show the negative of the image.*

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
#### `RedTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `ScaleFactor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Transparency`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---

### Protected Properties


### Public Methods

#### `CloneDeep()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)|N/A|

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
#### `GetHorizontalResolution()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Single`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetImage(RectangleShape,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Int32`|N/A|
|canvasHeight|`Int32`|N/A|

---
#### `GetImageHeight()`

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
#### `GetImageWidth()`

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
#### `GetVerticalResolution()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Single`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWorldFileText()`

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
|[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloseCore()`

**Summary**

   *This method opens the Image so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concrete public method Close. The Close method plays an important role in the life cycle of the Image. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override this method, it is recommended that that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed FeatureSource is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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

   *This method returns the bounding box of the RasterSource.*

**Remarks**

   *This method returns the bounding box of the RasterSource.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box of the RasterSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetImageCore(RectangleShape,Int32,Int32)`

**Summary**

   *This method returns an image based on the worldExtent, image width and image height.*

**Remarks**

   *This method is responsible for returning the image based on the parameters passed in.*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This method returns an image based on the worldExtent, image width and image height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the worldExtent you want to draw.|
|canvasWidth|`Int32`|This parameter represents the width of the image you want to draw.|
|canvasHeight|`Int32`|This parameter represents the height of the image you want to draw.|

---
#### `GetImageHeightCore()`

**Summary**

   *This method returns the height of the image in screen coordinates.*

**Remarks**

   *This method returns the height of the image in screen coordinates.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|This method returns the height of the image in screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetImageWidthCore()`

**Summary**

   *This method returns the width of the image in screen coordinates.*

**Remarks**

   *This method returns the width of the image in screen coordinates.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|This method returns the width of the image in screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWrappingImageLeft(RectangleShape,Double,Double,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|wrappingExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetWrappingImageRight(RectangleShape,Double,Double,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|wrappingExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `OnChangedSourceStatus(ChangedSourceStatusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ChangedSourceStatusEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ChangedSourceStatusEventArgs.md)|N/A|

---
#### `OnClosedRasterSource(ClosedRasterSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)|N/A|

---
#### `OnClosingRasterSource(ClosingRasterSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)|N/A|

---
#### `OnOpenedRasterSource(OpenedRasterSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)|N/A|

---
#### `OnOpeningRasterSource(OpeningRasterSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)|N/A|

---
#### `OnStreamLoading(StreamLoadingEventArgs)`

**Summary**

   *This method allows you to raise the StreamLoading event.*

**Remarks**

   *This method allows you to raise the StreamLoading event. Normally events are not accessible to derived classes, so we exposed a way to raise the event as necessary through this protected method.*

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

   *This method opens the Image so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concrete public method Open. The Open method plays an important role, as it is responsible for initializing the Image. Most methods on the Image will throw an exception if the state of the Image is not opened. When the map draws each layer, it will open the Image as one of its first steps; then, after it is finished drawing with that layer, it will close it. In this way, we are sure to release all resources used by the Image. When implementing this abstract method, consider opening files for file-based sources, connecting to databases in the database-based sources and so on. You will get a chance to close these in the Close method of the Image.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Events

#### StreamLoading

*N/A*

**Remarks**

*If you wish, you can pass in your own stream to represent the image. The stream can come from a variety of places, including isolated storage, a compressed file, or an encrypted stream. When the image is finished with the stream it will dispose of it, so be sure to keep this in mind when passing the stream in. If you do not pass in an alternate stream, the class will attempt to load the file from the file system using the PathFilename property.*

**Event Arguments**

[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)

#### OpeningRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)

#### OpenedRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)

#### ClosingRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)

#### ClosedRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)


