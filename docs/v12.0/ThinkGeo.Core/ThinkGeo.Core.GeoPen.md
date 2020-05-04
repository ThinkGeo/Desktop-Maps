# GeoPen


## Inheritance Hierarchy

+ `Object`
  + **`GeoPen`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeoPen()`](#geopen)|
|[`GeoPen(GeoBrush)`](#geopengeobrush)|
|[`GeoPen(GeoColor)`](#geopengeocolor)|
|[`GeoPen(GeoColor,Single)`](#geopengeocolorsingle)|
|[`GeoPen(GeoBrush,Single)`](#geopengeobrushsingle)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Brush`](#brush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This property gets and sets the brush for this GeoPen.|
|[`Color`](#color)|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This property gets and sets the GeoColor for this GeoPen.|
|[`DashCap`](#dashcap)|[`GeoDashCap`](../ThinkGeo.Core/ThinkGeo.Core.GeoDashCap.md)|This property gets and sets the dash cap for this GeoPen.|
|[`DashPattern`](#dashpattern)|Collection<`Single`>|This property gets the dash pattern for this GeoPen.|
|[`DashStyle`](#dashstyle)|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This property gets and sets the dash style for this GeoPen.|
|[`EndCap`](#endcap)|[`DrawingLineCap`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineCap.md)|This property gets and sets the end cap for this GeoPen.|
|[`Id`](#id)|`Int64`|The id of the GeoBrush. This is always used as a key when in the cached brushes.|
|[`LineJoin`](#linejoin)|[`DrawingLineJoin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineJoin.md)|This property gets and sets the line join for this GeoPen.|
|[`MiterLimit`](#miterlimit)|`Single`|This property gets and sets the miter limit for this GeoPen.|
|[`StartCap`](#startcap)|[`DrawingLineCap`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineCap.md)|This property gets and sets the start cap for this GeoPen.|
|[`Width`](#width)|`Single`|This property gets and sets the width for this GeoPen.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`SetLineCap(DrawingLineCap,DrawingLineCap,GeoDashCap)`](#setlinecapdrawinglinecapdrawinglinecapgeodashcap)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloneDeepCore()`](#clonedeepcore)|
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
|[`GeoPen()`](#geopen)|
|[`GeoPen(GeoBrush)`](#geopengeobrush)|
|[`GeoPen(GeoColor)`](#geopengeocolor)|
|[`GeoPen(GeoColor,Single)`](#geopengeocolorsingle)|
|[`GeoPen(GeoBrush,Single)`](#geopengeobrushsingle)|

### Protected Constructors


### Public Properties

#### `Brush`

**Summary**

   *This property gets and sets the brush for this GeoPen.*

**Remarks**

   *None*

**Return Value**

[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)

---
#### `Color`

**Summary**

   *This property gets and sets the GeoColor for this GeoPen.*

**Remarks**

   *N/A*

**Return Value**

[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)

---
#### `DashCap`

**Summary**

   *This property gets and sets the dash cap for this GeoPen.*

**Remarks**

   *None*

**Return Value**

[`GeoDashCap`](../ThinkGeo.Core/ThinkGeo.Core.GeoDashCap.md)

---
#### `DashPattern`

**Summary**

   *This property gets the dash pattern for this GeoPen.*

**Remarks**

   *Assigning a value other than null (Nothing in Visual Basic) to this property will set the DashStyle property for this GeoPen to Custom. The elements in the dashArray array set the length of each dash and space in the dash pattern. The first element sets the length of a dash, the second element sets the length of a space, the third element sets the length of a dash, and so on. The length of each dash and space in the dash pattern is the product of the element value in the array and the width of the GeoPen.*

**Return Value**

Collection<`Single`>

---
#### `DashStyle`

**Summary**

   *This property gets and sets the dash style for this GeoPen.*

**Remarks**

   *None*

**Return Value**

[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)

---
#### `EndCap`

**Summary**

   *This property gets and sets the end cap for this GeoPen.*

**Remarks**

   *None*

**Return Value**

[`DrawingLineCap`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineCap.md)

---
#### `Id`

**Summary**

   *The id of the GeoBrush. This is always used as a key when in the cached brushes.*

**Remarks**

   *N/A*

**Return Value**

`Int64`

---
#### `LineJoin`

**Summary**

   *This property gets and sets the line join for this GeoPen.*

**Remarks**

   *None*

**Return Value**

[`DrawingLineJoin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineJoin.md)

---
#### `MiterLimit`

**Summary**

   *This property gets and sets the miter limit for this GeoPen.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `StartCap`

**Summary**

   *This property gets and sets the start cap for this GeoPen.*

**Remarks**

   *None*

**Return Value**

[`DrawingLineCap`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineCap.md)

---
#### `Width`

**Summary**

   *This property gets and sets the width for this GeoPen.*

**Remarks**

   *None*

**Return Value**

`Single`

---

### Protected Properties


### Public Methods

#### `CloneDeep()`

**Summary**

   *Create a copy of GeoPen using the deep clone process.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|A cloned GeoPen.|

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
#### `SetLineCap(DrawingLineCap,DrawingLineCap,GeoDashCap)`

**Summary**

   *This method allows you to set the start, end and dash caps at one time.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startCap|[`DrawingLineCap`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineCap.md)|This parameter specifies the start cap to be used.|
|endCap|[`DrawingLineCap`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLineCap.md)|This parameter specifies the end cap to be used.|
|dashCap|[`GeoDashCap`](../ThinkGeo.Core/ThinkGeo.Core.GeoDashCap.md)|This parameter specifies the dash cap to be used.|

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

   *Create a copy of GeoPen using the deep clone process. The default implementation uses serialization.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|A cloned GeoPen.|

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


