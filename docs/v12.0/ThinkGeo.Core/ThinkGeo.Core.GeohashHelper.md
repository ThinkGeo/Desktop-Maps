# GeohashHelper


## Inheritance Hierarchy

+ `Object`
  + **`GeohashHelper`**

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
|[`ConvertToGeohash(PointShape,Int32)`](#converttogeohashpointshapeint32)|
|[`ConvertToGeohash(Vertex,Int32)`](#converttogeohashvertexint32)|
|[`ConvertToGeohash(Double,Double,Int32)`](#converttogeohashdoubledoubleint32)|
|[`ConvertToPointShape(String)`](#converttopointshapestring)|
|[`Equals(Object)`](#equalsobject)|
|[`GetAdjacentGeohash(Double,Double,GeohashAjacentDirection)`](#getadjacentgeohashdoubledoublegeohashajacentdirection)|
|[`GetAdjacentGeohash(Double,Double,GeohashAjacentDirection,Int32)`](#getadjacentgeohashdoubledoublegeohashajacentdirectionint32)|
|[`GetAdjacentGeohash(PointShape,GeohashAjacentDirection)`](#getadjacentgeohashpointshapegeohashajacentdirection)|
|[`GetAdjacentGeohash(PointShape,GeohashAjacentDirection,Int32)`](#getadjacentgeohashpointshapegeohashajacentdirectionint32)|
|[`GetAdjacentGeohash(Vertex,GeohashAjacentDirection)`](#getadjacentgeohashvertexgeohashajacentdirection)|
|[`GetAdjacentGeohash(Vertex,GeohashAjacentDirection,Int32)`](#getadjacentgeohashvertexgeohashajacentdirectionint32)|
|[`GetAdjacentGeohash(String,GeohashAjacentDirection)`](#getadjacentgeohashstringgeohashajacentdirection)|
|[`GetAdjacentGeohashes(String)`](#getadjacentgeohashesstring)|
|[`GetAdjacentGeohashes(PointShape)`](#getadjacentgeohashespointshape)|
|[`GetAdjacentGeohashes(PointShape,Int32)`](#getadjacentgeohashespointshapeint32)|
|[`GetAdjacentGeohashes(Vertex)`](#getadjacentgeohashesvertex)|
|[`GetAdjacentGeohashes(Vertex,Int32)`](#getadjacentgeohashesvertexint32)|
|[`GetAdjacentGeohashes(Double,Double)`](#getadjacentgeohashesdoubledouble)|
|[`GetAdjacentGeohashes(Double,Double,Int32)`](#getadjacentgeohashesdoubledoubleint32)|
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

#### `ConvertToGeohash(PointShape,Int32)`

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
|pointShapeInDecimalDegree|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|precision|`Int32`|N/A|

---
#### `ConvertToGeohash(Vertex,Int32)`

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
|vertexInDecimalDegree|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|precision|`Int32`|N/A|

---
#### `ConvertToGeohash(Double,Double,Int32)`

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
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|precision|`Int32`|N/A|

---
#### `ConvertToPointShape(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|geohash|`String`|N/A|

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
#### `GetAdjacentGeohash(Double,Double,GeohashAjacentDirection)`

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
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|direction|[`GeohashAjacentDirection`](../ThinkGeo.Core/ThinkGeo.Core.GeohashAjacentDirection.md)|N/A|

---
#### `GetAdjacentGeohash(Double,Double,GeohashAjacentDirection,Int32)`

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
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|direction|[`GeohashAjacentDirection`](../ThinkGeo.Core/ThinkGeo.Core.GeohashAjacentDirection.md)|N/A|
|precision|`Int32`|N/A|

---
#### `GetAdjacentGeohash(PointShape,GeohashAjacentDirection)`

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
|pointShapeInDecimalDegree|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|direction|[`GeohashAjacentDirection`](../ThinkGeo.Core/ThinkGeo.Core.GeohashAjacentDirection.md)|N/A|

---
#### `GetAdjacentGeohash(PointShape,GeohashAjacentDirection,Int32)`

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
|pointShapeInDecimalDegree|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|direction|[`GeohashAjacentDirection`](../ThinkGeo.Core/ThinkGeo.Core.GeohashAjacentDirection.md)|N/A|
|precision|`Int32`|N/A|

---
#### `GetAdjacentGeohash(Vertex,GeohashAjacentDirection)`

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
|vertexInDecimalDegree|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|direction|[`GeohashAjacentDirection`](../ThinkGeo.Core/ThinkGeo.Core.GeohashAjacentDirection.md)|N/A|

---
#### `GetAdjacentGeohash(Vertex,GeohashAjacentDirection,Int32)`

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
|vertexInDecimalDegree|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|direction|[`GeohashAjacentDirection`](../ThinkGeo.Core/ThinkGeo.Core.GeohashAjacentDirection.md)|N/A|
|precision|`Int32`|N/A|

---
#### `GetAdjacentGeohash(String,GeohashAjacentDirection)`

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
|geohash|`String`|N/A|
|direction|[`GeohashAjacentDirection`](../ThinkGeo.Core/ThinkGeo.Core.GeohashAjacentDirection.md)|N/A|

---
#### `GetAdjacentGeohashes(String)`

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
|geohash|`String`|N/A|

---
#### `GetAdjacentGeohashes(PointShape)`

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
|pointShapeInDecimalDegree|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetAdjacentGeohashes(PointShape,Int32)`

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
|pointShapeInDecimalDegree|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|precision|`Int32`|N/A|

---
#### `GetAdjacentGeohashes(Vertex)`

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
|vertexInDecimalDegree|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|

---
#### `GetAdjacentGeohashes(Vertex,Int32)`

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
|vertexInDecimalDegree|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|precision|`Int32`|N/A|

---
#### `GetAdjacentGeohashes(Double,Double)`

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
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|

---
#### `GetAdjacentGeohashes(Double,Double,Int32)`

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
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|precision|`Int32`|N/A|

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


