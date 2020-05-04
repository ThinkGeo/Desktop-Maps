# MapArguments


## Inheritance Hierarchy

+ `Object`
  + **`MapArguments`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MapArguments()`](#maparguments)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ActualHeight`](#actualheight)|`Double`|Gets or sets the height of current map object in screen coordinate.|
|[`ActualWidth`](#actualwidth)|`Double`|Gets or sets the width of current map object in screen coordinate.|
|[`CurrentExtent`](#currentextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|Gets or sets the extent of current map.|
|[`CurrentResolution`](#currentresolution)|`Double`|Gets or sets a double value that indicates the current resolution of the map.|
|[`CurrentScale`](#currentscale)|`Double`|Gets or sets a double value that indicates the current scale of the map.|
|[`MapUnit`](#mapunit)|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|Gets or sets the GeographyUnit for the map.|
|[`MaxExtent`](#maxextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|Gets or sets the max extent of current map.|
|[`MaximumScale`](#maximumscale)|`Double`|This property gets and sets a maximum scale when navigating the map. When you keep zooming out, and the target scale is bigger than the maximum scale, the zooming operation will be stopped.|
|[`MinimumScale`](#minimumscale)|`Double`|This property gets and sets a minimum scale when navigating the map. When you keep zooming in, and the target scale is smaller than the minimum scale, the zooming operation will be stopped.|
|[`PivotScreenPoint`](#pivotscreenpoint)|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|[`PivotWorldPoint`](#pivotworldpoint)|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|[`RotationAngle`](#rotationangle)|`Single`|N/A|
|[`ScaleFactor`](#scalefactor)|`Single`|N/A|
|[`ZoomLevelScales`](#zoomlevelscales)|Collection<`Double`>|Gets a collection of double values that determines the zoomlevel scales.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetSnappedExtent(RectangleShape,ZoomSnapDirection)`](#getsnappedextentrectangleshapezoomsnapdirection)|
|[`GetSnappedZoomLevelIndex(RectangleShape)`](#getsnappedzoomlevelindexrectangleshape)|
|[`GetSnappedZoomLevelIndex(Double)`](#getsnappedzoomlevelindexdouble)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|
|[`ToWorldCoordinate(ScreenPointF)`](#toworldcoordinatescreenpointf)|

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
|[`MapArguments()`](#maparguments)|

### Protected Constructors


### Public Properties

#### `ActualHeight`

**Summary**

   *Gets or sets the height of current map object in screen coordinate.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `ActualWidth`

**Summary**

   *Gets or sets the width of current map object in screen coordinate.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `CurrentExtent`

**Summary**

   *Gets or sets the extent of current map.*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `CurrentResolution`

**Summary**

   *Gets or sets a double value that indicates the current resolution of the map.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `CurrentScale`

**Summary**

   *Gets or sets a double value that indicates the current scale of the map.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MapUnit`

**Summary**

   *Gets or sets the GeographyUnit for the map.*

**Remarks**

   *N/A*

**Return Value**

[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)

---
#### `MaxExtent`

**Summary**

   *Gets or sets the max extent of current map.*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `MaximumScale`

**Summary**

   *This property gets and sets a maximum scale when navigating the map. When you keep zooming out, and the target scale is bigger than the maximum scale, the zooming operation will be stopped.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MinimumScale`

**Summary**

   *This property gets and sets a minimum scale when navigating the map. When you keep zooming in, and the target scale is smaller than the minimum scale, the zooming operation will be stopped.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `PivotScreenPoint`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)

---
#### `PivotWorldPoint`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)

---
#### `RotationAngle`

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

`Single`

---
#### `ZoomLevelScales`

**Summary**

   *Gets a collection of double values that determines the zoomlevel scales.*

**Remarks**

   *N/A*

**Return Value**

Collection<`Double`>

---

### Protected Properties


### Public Methods

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
#### `GetSnappedExtent(RectangleShape,ZoomSnapDirection)`

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
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|zoomSnapDirection|[`ZoomSnapDirection`](ThinkGeo.UI.Wpf.ZoomSnapDirection.md)|N/A|

---
#### `GetSnappedZoomLevelIndex(RectangleShape)`

**Summary**

   *Gets a snapped zoom level index from the provided extent.*

**Remarks**

   *The extent will automatically snapped a closest scale from the ZoomLevelScale list, and create an extent back with the map's screen width and height.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|An integar value that indicates the snapped zoom level index.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|An extent to be snapped.|

---
#### `GetSnappedZoomLevelIndex(Double)`

**Summary**

   *Gets a snapped zoom level index from the provided scale.*

**Remarks**

   *The scale will automatically snapped a closest scale from the ZoomLevelScale list, and create an extent back with the map's screen width and height.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|An integar value that indicates the snapped zoom level index.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|A scale to be snapped.|

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
#### `ToWorldCoordinate(ScreenPointF)`

**Summary**

   *Converts a point from screen coordinate to world coordinate.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A point in world coordinate.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenCoordinate|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|A point in screen coordinate to be converted.|

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


