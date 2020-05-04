# RasterSource


## Inheritance Hierarchy

+ `Object`
  + **`RasterSource`**
    + [`GeoTiffRasterSource`](../ThinkGeo.Core/ThinkGeo.Core.GeoTiffRasterSource.md)
    + [`NativeImageRasterSource`](../ThinkGeo.Core/ThinkGeo.Core.NativeImageRasterSource.md)
    + [`NoaaRadarRasterSource`](../ThinkGeo.Core/ThinkGeo.Core.NoaaRadarRasterSource.md)
    + [`WmsRasterSource`](../ThinkGeo.Core/ThinkGeo.Core.WmsRasterSource.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`RasterSource()`](#rastersource)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`BlueTranslation`](#bluetranslation)|`Single`|This property gets and sets the amount of blue to apply to the image.|
|[`GreenTranslation`](#greentranslation)|`Single`|This property gets and sets the amount of green to apply to the image.|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|This property gets and sets if the image should be converted to grayscale.|
|[`IsNegative`](#isnegative)|`Boolean`|This property gets and sets whether the image should be converted to negative (inverse colors).|
|[`IsOpen`](#isopen)|`Boolean`|This property returns true if the RasterSource is open and false if it is not.|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`RedTranslation`](#redtranslation)|`Single`|This property gets and sets the amount of red to apply to the image.|
|[`ScaleFactor`](#scalefactor)|`Double`|The scale factor when drawing the primitive image. For example I am looking for an image with 100*100px, If the ScaleFactor is set to 2, it means the component will get the image with 200*200px.  We want to keep it as 1 (by default) for most cases.|
|[`Transparency`](#transparency)|`Single`|This property gets and sets the amount of transparency to apply to the image.|

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
|[`GenerateWorldFileText(RectangleShape,Int32,Int32)`](#generateworldfiletextrectangleshapeint32int32)|
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
|[`OpenCore()`](#opencore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`OpeningRasterSource`](#openingrastersource)|[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)|N/A|
|[`OpenedRasterSource`](#openedrastersource)|[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)|N/A|
|[`ClosingRasterSource`](#closingrastersource)|[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)|N/A|
|[`ClosedRasterSource`](#closedrastersource)|[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|N/A|

### Protected Constructors

#### `RasterSource()`

**Summary**

   *This is the default new constructor for the RasterSource.*

**Remarks**

   *None*

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

#### `BlueTranslation`

**Summary**

   *This property gets and sets the amount of blue to apply to the image.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `GreenTranslation`

**Summary**

   *This property gets and sets the amount of green to apply to the image.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `IsGrayscale`

**Summary**

   *This property gets and sets if the image should be converted to grayscale.*

**Remarks**

   *None*

**Return Value**

`Boolean`

---
#### `IsNegative`

**Summary**

   *This property gets and sets whether the image should be converted to negative (inverse colors).*

**Remarks**

   *None*

**Return Value**

`Boolean`

---
#### `IsOpen`

**Summary**

   *This property returns true if the RasterSource is open and false if it is not.*

**Remarks**

   *Various methods on the RasterSource require that it be in an open state. If one of those methods is called when the state is not open, the method will throw an exception. To enter the open state, you must call the RasterSource's Open method. The method will raise an exception if the current RasterSource is already open.*

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

   *This property gets and sets the amount of red to apply to the image.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `ScaleFactor`

**Summary**

   *The scale factor when drawing the primitive image. For example I am looking for an image with 100*100px, If the ScaleFactor is set to 2, it means the component will get the image with 200*200px.  We want to keep it as 1 (by default) for most cases.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Transparency`

**Summary**

   *This property gets and sets the amount of transparency to apply to the image.*

**Remarks**

   *None*

**Return Value**

`Single`

---

### Protected Properties


### Public Methods

#### `CloneDeep()`

**Summary**

   *Create a copy of RasterSource using the deep clone process.*

**Remarks**

   *The difference between deep clone and shallow clone is: when shallow cloned, only the object is copied, but the contained objects are not; while in deep clone it does copy the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)|A cloned RasterSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Close()`

**Summary**

   *This method closes the RasterSource and releases any resources it was using.*

**Remarks**

   *This method is the concrete wrapper for the abstract method CloseCore. The Close method plays an important role in the life cycle of the RasterSource. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override the Core version of this method, it is recommended that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed RasterSource is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

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
#### `GenerateWorldFileText(RectangleShape,Int32,Int32)`

**Summary**

   *This method returns a string that represents the image's world file based on the parameters passed in.*

**Remarks**

   *This method returns a string that represents the image's world file. The world file is a file type that can accompany image files. It contains information about the image's position, resolution and other spatial-related items. It is common to have this kind of file associated with generic image types such as JPG, BMP, and normal TIFF because they do not have a mechanism to store this data internally. Modern GIS image types such as JPEG2000, ECW, and MrSid typically have this information stored internally. We provide this method in the event that you want to create your own world file from any image that either already has one or has its data stored internally.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string that represents the image's world file based on the parameters passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the worldExtent of the image in world coordinates.|
|imageWidth|`Int32`|This parameter is the width of the image in screen coordinates.|
|imageHeight|`Int32`|This parameter is the height of the image in screen coordinates.|

---
#### `GetBoundingBox()`

**Summary**

   *This method returns the bounding box of the RasterSource.*

**Remarks**

   *This method returns the bounding box of the RasterSource. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box of the RasterSource.|

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

   *This method returns the horizontal resolution of the image.*

**Remarks**

   *This method returns the horizontal resolution of the image. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the horizontal resolution of the image.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetImage(RectangleShape,Int32,Int32)`

**Summary**

   *This method returns an image based on the worldExtent and image width and height.*

**Remarks**

   *This method is responsible for returning the image based on the parameters passed in. As the core version of this method is abstract, you will need to override it when creating your own RasterSource. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This method returns an image based on the worldExtent and image width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the worldExtent you want to draw.|
|canvasWidth|`Int32`|This parameter represents the width of the image you want to draw.|
|canvasHeight|`Int32`|This parameter represents the height of the image you want to draw.|

---
#### `GetImageHeight()`

**Summary**

   *This method returns the height of the image in screen coordinates.*

**Remarks**

   *This method returns the height of the image in screen coordinates. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|This method returns the height of the image in screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetImageWidth()`

**Summary**

   *This method returns the width of the image in screen coordinates.*

**Remarks**

   *This method returns the width of the image in screen coordinates. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|This method returns the width of the image in screen coordinates.|

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

   *This method returns the vertical resolution of the image.*

**Remarks**

   *This method returns the vertical resolution of the image. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the vertical resolution of the image.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWorldFileText()`

**Summary**

   *This method returns a string that represents the image's world file.*

**Remarks**

   *This method wraps the Core version of this method and returns a string that represents the image's world file. The world file is a file type that can accompany image files. It contains information about the image's position, resolution and other spatial-related items. It is common to have this kind of file associated with generic image types such as JPG, BMP, and normal TIFF because they do not have a mechanism to store this data internally. Modern GIS image types such as JPEG2000, ECW, and MrSid typically have this information stored internally. We provide this method in the event that you want to create your own world file from any image that either already has one or has its data stored internally. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string that represents the image's world file.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Open()`

**Summary**

   *This method opens the RasterSource so that it is initialized and ready to use.*

**Remarks**

   *This method is the concrete wrapper for the abstract method OpenCore. The Open method plays an important role, as it is responsible for initializing the RasterSource. Most methods on the RasterSource will throw an exception if the state of the RasterSource is not opened. When the map draws each layer, it will open the RasterSource as one of its first steps; then, after it is finished drawing with that layer, it will close it. In this way, we are sure to release all resources used by the RasterSource. When implementing the abstract method, consider opening files for file-based sources, connecting to databases in the database-based sources and so on. You will get a chance to close these in the Close method of the RasterSource. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

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

   *Create a copy of RasterSource using the deep clone process. The default implementation uses serialization.*

**Remarks**

   *The difference between deep clone and shallow clone is: when shallow cloned, only the object is copied, but the contained objects are not; while in deep clone it does copy the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)|A cloned RasterSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloseCore()`

**Summary**

   *This method opens the RasterSource so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concrete public method Close. The Close method plays an important role in the life cycle of the RasterSource. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override this method, it is recommended that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed RasterSource is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future.*

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

   *This method returns an image based on the worldExtent and image width and height.*

**Remarks**

   *This method is responsible for returning the image based on the parameters passed in. As the core version of this method is abstract, you will need to override it when creating our own RasterSource.*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This method returns an image based on the worldExtent and image width and height.|

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

   *This abstract method is called from the concrete method GetImageHeight. You need to override it if you inherit from the RasterSource to return the height of your image. It returns the height of the image in screen coordinates.*

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

   *This abstract method is called from the concrete method GetImageWidth. You need to override it if you inherit from the RasterSource to return the width of your image. It returns the width of the image in screen coordinates.*

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

   *This method allows you to raise the ClosedRasterSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the ClosedRasterSource event. This may be useful if you plan to extend the RasterSource and you need access to the event. Details on the event: This event is called after the closing of the RasterSource. Technically, this event is called after the calling of the Close method on the RasterSource and after the protected CloseCore method. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)|This parameter is the event arguments that define the parameters passed to the recipient of the event.|

---
#### `OnClosingRasterSource(ClosingRasterSourceEventArgs)`

**Summary**

   *This method allows you to raise the ClosingRasterSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the ClosingRasterSource event. This may be useful if you plan to extend the RasterSource and you need access to the event. Details on the event: This event is called before the closing of the RasterSource. Technically, this event is called after the calling of the Close method on the RasterSource, but before the protected CloseCore method. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)|This parameter is the event arguments that define the parameters passed to the recipient of the event.|

---
#### `OnOpenedRasterSource(OpenedRasterSourceEventArgs)`

**Summary**

   *This method allows you to raise the OpenedRasterSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the OpenedRasterSource event. This may be useful if you plan to extend the RasterSource and you need access to the event. Details on the event: This event is called after the opening of the RasterSource. Technically, this event is called after the calling of the Open method on the RasterSource and after the protected OpenCore method is called. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)|This parameter is the event arguments that define the parameters passed to the recipient of the event.|

---
#### `OnOpeningRasterSource(OpeningRasterSourceEventArgs)`

**Summary**

   *This method allows you to raise the OpeningRasterSource event from a derived class.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the OpeningRasterSource event. This may be useful if you plan to extend the RasterSource and you need access to the event. Details on the event: This event is called before the opening of the RasterSource. Technically, this event is called after the calling of the Open method on the RasterSource, but before the protected OpenCore method. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)|This parameter is the event arguments that define the parameters passed to the recipient of the event.|

---
#### `OpenCore()`

**Summary**

   *This method opens the RasterSource so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concrete public method Open. The Open method plays an important role, as it is responsible for initializing the RasterSource. Most methods on the RasterSource will throw an exception if the state of the RasterSource is not opened. When the map draws each layer, it will open the RasterSource as one of its first steps; then, after it is finished drawing with that layer, it will close it. In this way, we are sure to release all resources used by the RasterSource. When implementing this abstract method, consider opening files for file-based sources, connecting to databases in the database-based sources and so on. You will get a chance to close these in the Close method of the RasterSource.*

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

#### OpeningRasterSource

*N/A*

**Remarks**

*This event is called before the opening of the RasterSource. Technically, this event is called after the calling of the Open method on the RasterSource, but before the protected OpenCore method. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)

#### OpenedRasterSource

*N/A*

**Remarks**

*This event is called after the opening of the RasterSource. Technically, this event is called after the calling of the Open method on the RasterSource and after the protected OpenCore method is called. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)

#### ClosingRasterSource

*N/A*

**Remarks**

*This event is called before the closing of the RasterSource. Technically, this event is called after the calling of the Close method on the RasterSource, but before the protected CloseCore method. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)

#### ClosedRasterSource

*N/A*

**Remarks**

*This event is called after the closing of the RasterSource. Technically, this event is called after the calling of the Close method on the RasterSource and after the protected CloseCore method. It is typical that the RasterSource may be opened and closed may times during the life cycle of your application. The type of control the MapEngine is embedded in will dictate how often this happens. For example, in the case of the Web Edition, each time a RasterSource is in the Ajax or Post Back part of the page cycle, it will close the RasterSource before returning back to the client. This is to conserve resources, as the web is a connection-less environment. In the case of the Desktop Edition, we can keep the RasterSource open, knowing that we can maintain a persistent connection.*

**Event Arguments**

[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)


