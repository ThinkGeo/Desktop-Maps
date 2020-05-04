# Vertex


## Inheritance Hierarchy

+ `Object`
  + `ValueType`
    + **`Vertex`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`Vertex(Double,Double)`](#vertexdoubledouble)|
|[`Vertex(PointShape)`](#vertexpointshape)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`X`](#x)|`Double`|This property returns the horizontal value of the vertex.|
|[`Y`](#y)|`Double`|This property returns the vertical value of the vertex.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Add(Vertex)`](#addvertex)|
|[`Equals(Object)`](#equalsobject)|
|[`FindMiddleVertexBetweenTwoVertices(Vertex,Vertex)`](#findmiddlevertexbetweentwoverticesvertexvertex)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`GetDistanceFromVertex(Double,Double)`](#getdistancefromvertexdoubledouble)|
|[`GetDistanceTo(BaseShape,GeographyUnit,DistanceUnit)`](#getdistancetobaseshapegeographyunitdistanceunit)|
|[`GetDistanceTo(Vertex,GeographyUnit,DistanceUnit)`](#getdistancetovertexgeographyunitdistanceunit)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`Rotate(Vertex,Single)`](#rotatevertexsingle)|
|[`Rotate(PointShape,Single)`](#rotatepointshapesingle)|
|[`TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreedoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetdoubledoublegeographyunitdistanceunit)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`Vertex(Double,Double)`](#vertexdoubledouble)|
|[`Vertex(PointShape)`](#vertexpointshape)|

### Protected Constructors


### Public Properties

#### `X`

**Summary**

   *This property returns the horizontal value of the vertex.*

**Remarks**

   *None*

**Return Value**

`Double`

---
#### `Y`

**Summary**

   *This property returns the vertical value of the vertex.*

**Remarks**

   *None*

**Return Value**

`Double`

---

### Protected Properties


### Public Methods

#### `Add(Vertex)`

**Summary**

   *This method adds the target vertex to the current vertex.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetVertex|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|This parameter represents the vertex you wish to add to the current vertex.|

---
#### `Equals(Object)`

**Summary**

   *This method is an override of the Equals functionality.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|obj|`Object`|N/A|

---
#### `FindMiddleVertexBetweenTwoVertices(Vertex,Vertex)`

**Summary**

   *This method returns the middle Vertex of a straight line which two vertices are passing in.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|This method returns the middle Vertex between the vertices which passing in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|vertex1|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|start vertex of a straight line|
|vertex2|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|en vertex of a straight line|

---
#### `GetHashCode()`

**Summary**

   *This method is an override of the GetHashCode functionality.*

**Remarks**

   *None*

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

   *This method is an override of the ToString functionality.*

**Remarks**

   *None*

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
#### `GetDistanceFromVertex(Double,Double)`

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
|toX|`Double`|N/A|
|toY|`Double`|N/A|

---
#### `GetDistanceTo(BaseShape,GeographyUnit,DistanceUnit)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetDistanceTo(Vertex,GeographyUnit,DistanceUnit)`

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
|targetVertex|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

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
#### `Rotate(Vertex,Single)`

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
|vertex|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|degreeAngle|`Single`|N/A|

---
#### `Rotate(PointShape,Single)`

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
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|degreeAngle|`Single`|N/A|

---
#### `TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`

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
|distance|`Double`|N/A|
|angleInDegrees|`Double`|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`

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
|xOffset|`Double`|N/A|
|yOffset|`Double`|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|unitOfOffset|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---

### Public Events


