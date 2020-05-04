# WmsServerLayer


## Inheritance Hierarchy

+ `Object`
  + **`WmsServerLayer`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`WmsServerLayer()`](#wmsserverlayer)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Abstract`](#abstract)|`String`|Abstract of this requesting WMS layer.|
|[`BoudingBox`](#boudingbox)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`Cascaded`](#cascaded)|`Int32`|0: layer has not been retransmitted by a Cascading Map Server. n: layer has been retransmitted n times.|
|[`ChildLayers`](#childlayers)|[`WmsServerLayer[]`](../ThinkGeo.Core/ThinkGeo.Core.WmsServerLayer.md)|Return child layers.|
|[`Crs`](#crs)|`String[]`|' The Coordinate Reference Systems supported by the layer. '|
|[`FixedHeight`](#fixedheight)|`Int32`|0: WMS can resize map to arbitrary height. nonzero: map has a fixed height that cannot be changed by the WMS.|
|[`FixedWidth`](#fixedwidth)|`Int32`|0: WMS can resize map to arbitrary width. nonzero: map has a fixed width that cannot be changed by the WMS.|
|[`KeyWords`](#keywords)|`String[]`|KeyWords property of this requesting WMS layer.|
|[`MaxScale`](#maxscale)|`Double`|Maximum scale for which it is appropriate to display this layer.|
|[`MinScale`](#minscale)|`Double`|Minimum scale for which it is appropriate to display this layer.|
|[`Name`](#name)|`String`|Name of this requesting WMS layer.|
|[`NoSubsets`](#nosubsets)|`Boolean`|False: WMS can map a subset of the full bounding box. True: WMS can only map the entire bounding box.|
|[`Opaque`](#opaque)|`Boolean`|False: map data represents vector features that probably do not completely fill space. True: map data are mostly or completely opaque.|
|[`Queryable`](#queryable)|`Boolean`|Queryable property of this requesting WMS layer.|
|[`Styles`](#styles)|[`WmsLayerStyle[]`](../ThinkGeo.Core/ThinkGeo.Core.WmsLayerStyle.md)|The styles supported by the layer.|
|[`Title`](#title)|`String`|Title of this requesting WMS layer.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
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
|[`WmsServerLayer()`](#wmsserverlayer)|

### Protected Constructors


### Public Properties

#### `Abstract`

**Summary**

   *Abstract of this requesting WMS layer.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `BoudingBox`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `Cascaded`

**Summary**

   *0: layer has not been retransmitted by a Cascading Map Server. n: layer has been retransmitted n times.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `ChildLayers`

**Summary**

   *Return child layers.*

**Remarks**

   *N/A*

**Return Value**

[`WmsServerLayer[]`](../ThinkGeo.Core/ThinkGeo.Core.WmsServerLayer.md)

---
#### `Crs`

**Summary**

   *' The Coordinate Reference Systems supported by the layer. '*

**Remarks**

   *N/A*

**Return Value**

`String[]`

---
#### `FixedHeight`

**Summary**

   *0: WMS can resize map to arbitrary height. nonzero: map has a fixed height that cannot be changed by the WMS.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `FixedWidth`

**Summary**

   *0: WMS can resize map to arbitrary width. nonzero: map has a fixed width that cannot be changed by the WMS.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `KeyWords`

**Summary**

   *KeyWords property of this requesting WMS layer.*

**Remarks**

   *N/A*

**Return Value**

`String[]`

---
#### `MaxScale`

**Summary**

   *Maximum scale for which it is appropriate to display this layer.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MinScale`

**Summary**

   *Minimum scale for which it is appropriate to display this layer.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Name`

**Summary**

   *Name of this requesting WMS layer.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `NoSubsets`

**Summary**

   *False: WMS can map a subset of the full bounding box. True: WMS can only map the entire bounding box.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Opaque`

**Summary**

   *False: map data represents vector features that probably do not completely fill space. True: map data are mostly or completely opaque.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Queryable`

**Summary**

   *Queryable property of this requesting WMS layer.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Styles`

**Summary**

   *The styles supported by the layer.*

**Remarks**

   *N/A*

**Return Value**

[`WmsLayerStyle[]`](../ThinkGeo.Core/ThinkGeo.Core.WmsLayerStyle.md)

---
#### `Title`

**Summary**

   *Title of this requesting WMS layer.*

**Remarks**

   *N/A*

**Return Value**

`String`

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


