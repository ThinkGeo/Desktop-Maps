# NoaaRadarRasterSource


## Inheritance Hierarchy

+ `Object`
  + [`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)
    + **`NoaaRadarRasterSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`NoaaRadarRasterSource()`](#noaaradarrastersource)|
|[`NoaaRadarRasterSource(String)`](#noaaradarrastersourcestring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`BlueTranslation`](#bluetranslation)|`Single`|N/A|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|N/A|
|[`IsNegative`](#isnegative)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`ScaleFactor`](#scalefactor)|`Double`|N/A|
|[`Transparency`](#transparency)|`Single`|N/A|
|[`UserAgent`](#useragent)|`String`|N/A|

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
|[`OnRadarUpdated(RadarUpdatedNoaaRadarRasterSourceEventArgs)`](#onradarupdatedradarupdatednoaaradarrastersourceeventargs)|
|[`OnRadarUpdating(RadarUpdatingNoaaRadarRasterSourceEventArgs)`](#onradarupdatingradarupdatingnoaaradarrastersourceeventargs)|
|[`OpenCore()`](#opencore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`RadarUpdated`](#radarupdated)|[`RadarUpdatedNoaaRadarRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RadarUpdatedNoaaRadarRasterSourceEventArgs.md)|N/A|
|[`RadarUpdating`](#radarupdating)|[`RadarUpdatingNoaaRadarRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RadarUpdatingNoaaRadarRasterSourceEventArgs.md)|N/A|
|[`OpeningRasterSource`](#openingrastersource)|[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)|N/A|
|[`OpenedRasterSource`](#openedrastersource)|[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)|N/A|
|[`ClosingRasterSource`](#closingrastersource)|[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)|N/A|
|[`ClosedRasterSource`](#closedrastersource)|[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`NoaaRadarRasterSource()`](#noaaradarrastersource)|
|[`NoaaRadarRasterSource(String)`](#noaaradarrastersourcestring)|

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
#### `GreenTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

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
#### `UserAgent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

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

   *N/A*

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
#### `GetImageCore(RectangleShape,Int32,Int32)`

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
#### `GetImageHeightCore()`

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
#### `GetImageWidthCore()`

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
#### `OnRadarUpdated(RadarUpdatedNoaaRadarRasterSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`RadarUpdatedNoaaRadarRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RadarUpdatedNoaaRadarRasterSourceEventArgs.md)|N/A|

---
#### `OnRadarUpdating(RadarUpdatingNoaaRadarRasterSourceEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`RadarUpdatingNoaaRadarRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RadarUpdatingNoaaRadarRasterSourceEventArgs.md)|N/A|

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

### Public Events

#### RadarUpdated

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RadarUpdatedNoaaRadarRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RadarUpdatedNoaaRadarRasterSourceEventArgs.md)

#### RadarUpdating

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RadarUpdatingNoaaRadarRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RadarUpdatingNoaaRadarRasterSourceEventArgs.md)

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


