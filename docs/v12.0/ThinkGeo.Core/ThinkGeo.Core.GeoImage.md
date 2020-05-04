# GeoImage


## Inheritance Hierarchy

+ `Object`
  + **`GeoImage`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeoImage()`](#geoimage)|
|[`GeoImage(Int32,Int32)`](#geoimageint32int32)|
|[`GeoImage(Stream)`](#geoimagestream)|
|[`GeoImage(String)`](#geoimagestring)|
|[`GeoImage(Byte[])`](#geoimagebyte[])|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Height`](#height)|`Int32`|Gets the height.|
|[`NativeImage`](#nativeimage)|`Object`|N/A|
|[`Opacity`](#opacity)|`Single`|Gets or sets the opacity.|
|[`PathFilename`](#pathfilename)|`String`|Gets the path filename.|
|[`Width`](#width)|`Int32`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Clear(GeoColor)`](#cleargeocolor)|
|[`CreateCustomizedImage(Int32,Int32,GeoColorType,GeoAlphaType)`](#createcustomizedimageint32int32geocolortypegeoalphatype)|
|[`Crop(DrawingRectangle)`](#cropdrawingrectangle)|
|[`Dispose()`](#dispose)|
|[`DrawImage(GeoImage,Single,Single)`](#drawimagegeoimagesinglesingle)|
|[`DrawText(String,GeoFont,GeoBrush,ScreenPointF[])`](#drawtextstringgeofontgeobrushscreenpointf[])|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetImageBytes(GeoImageFormat,Int32)`](#getimagebytesgeoimageformatint32)|
|[`GetImageStream(GeoImageFormat,Int32)`](#getimagestreamgeoimageformatint32)|
|[`GetIntPtr()`](#getintptr)|
|[`GetType()`](#gettype)|
|[`Save(Stream,GeoImageFormat,Int32)`](#savestreamgeoimageformatint32)|
|[`Save(String,GeoImageFormat,Int32)`](#savestringgeoimageformatint32)|
|[`Scale(Int32,Int32)`](#scaleint32int32)|
|[`SetPixels(GeoColor[])`](#setpixelsgeocolor[])|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`ApplyTranslations(Boolean,Boolean,Single,Single,Single,Single)`](#applytranslationsbooleanbooleansinglesinglesinglesingle)|
|[`Dispose(Boolean)`](#disposeboolean)|
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
|[`GeoImage()`](#geoimage)|
|[`GeoImage(Int32,Int32)`](#geoimageint32int32)|
|[`GeoImage(Stream)`](#geoimagestream)|
|[`GeoImage(String)`](#geoimagestring)|
|[`GeoImage(Byte[])`](#geoimagebyte[])|

### Protected Constructors


### Public Properties

#### `Height`

**Summary**

   *Gets the height.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `NativeImage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `Opacity`

**Summary**

   *Gets or sets the opacity.*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `PathFilename`

**Summary**

   *Gets the path filename.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Width`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---

### Protected Properties


### Public Methods

#### `Clear(GeoColor)`

**Summary**

   *Clears the specified color.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|color|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The color.|

---
#### `CreateCustomizedImage(Int32,Int32,GeoColorType,GeoAlphaType)`

**Summary**

   *Creates the customized image.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)||

**Parameters**

|Name|Type|Description|
|---|---|---|
|width|`Int32`|The width.|
|height|`Int32`|The height.|
|colorType|[`GeoColorType`](../ThinkGeo.Core/ThinkGeo.Core.GeoColorType.md)|Type of the color.|
|alphaType|[`GeoAlphaType`](../ThinkGeo.Core/ThinkGeo.Core.GeoAlphaType.md)|Type of the alpha.|

---
#### `Crop(DrawingRectangle)`

**Summary**

   *Cuts the specified source rect.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)||

**Parameters**

|Name|Type|Description|
|---|---|---|
|srcRect|[`DrawingRectangle`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangle.md)|The source rect.|

---
#### `Dispose()`

**Summary**

   *This method implements the IDispose method. This method is the concrete wrapper for the abstract method DisposeCore.*

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
#### `DrawImage(GeoImage,Single,Single)`

**Summary**

   *Combines the images.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|geoImage|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|pointX|`Single`|N/A|
|pointY|`Single`|N/A|

---
#### `DrawText(String,GeoFont,GeoBrush,ScreenPointF[])`

**Summary**

   *Appends the text.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|text|`String`|The text.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|The font.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|The fill brush.|
|points|[`ScreenPointF[]`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|The points.|

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
#### `GetImageBytes(GeoImageFormat,Int32)`

**Summary**

   *Gets the image bytes.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|imageFormat|[`GeoImageFormat`](../ThinkGeo.Core/ThinkGeo.Core.GeoImageFormat.md)|The image format.|
|imageQuality|`Int32`|The image quality.|

---
#### `GetImageStream(GeoImageFormat,Int32)`

**Summary**

   *Gets the image stream.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Stream`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|imageFormat|[`GeoImageFormat`](../ThinkGeo.Core/ThinkGeo.Core.GeoImageFormat.md)|The image format.|
|imageQuality|`Int32`|The image quality.|

---
#### `GetIntPtr()`

**Summary**

   *Gets the pixels.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`IntPtr`||

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
#### `Save(Stream,GeoImageFormat,Int32)`

**Summary**

   *Saves the specified stream.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|stream|`Stream`|The stream.|
|imageFormat|[`GeoImageFormat`](../ThinkGeo.Core/ThinkGeo.Core.GeoImageFormat.md)|The image format.|
|quality|`Int32`|The quality.|

---
#### `Save(String,GeoImageFormat,Int32)`

**Summary**

   *Saves the specified file path.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|filePath|`String`|The file path.|
|imageFormat|[`GeoImageFormat`](../ThinkGeo.Core/ThinkGeo.Core.GeoImageFormat.md)|The image format.|
|quality|`Int32`|The quality.|

---
#### `Scale(Int32,Int32)`

**Summary**

   *Scales the specified width.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)||

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetWidth|`Int32`|The width.|
|targetHeight|`Int32`|The height.|

---
#### `SetPixels(GeoColor[])`

**Summary**

   *Sets the pixels.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pixels|[`GeoColor[]`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The pixels.|

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

#### `ApplyTranslations(Boolean,Boolean,Single,Single,Single,Single)`

**Summary**

   *Applies the translations.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)||

**Parameters**

|Name|Type|Description|
|---|---|---|
|isGrayscale|`Boolean`|if set to true [is grayscale].|
|isNegative|`Boolean`|if set to true [is negative].|
|transparency|`Single`|The transparency.|
|redTranslation|`Single`|The red translation.|
|greenTranslation|`Single`|The green translation.|
|blueTranslation|`Single`|The blue translation.|

---
#### `Dispose(Boolean)`

**Summary**

   *This method disposes all the unmanaged resource in the tile.*

**Remarks**

   *When implementing this method, consider the stop the background threading when drawing asynchronously.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|disposing|`Boolean`|N/A|

---
#### `Finalize()`

**Summary**

   *Finalizer of this tile object.*

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


