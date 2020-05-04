# TileMatrix


## Inheritance Hierarchy

+ `Object`
  + [`Matrix`](../ThinkGeo.Core/ThinkGeo.Core.Matrix.md)
    + **`TileMatrix`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`TileMatrix()`](#tilematrix)|
|[`TileMatrix(Double)`](#tilematrixdouble)|
|[`TileMatrix(Double,RectangleShape,GeographyUnit)`](#tilematrixdoublerectangleshapegeographyunit)|
|[`TileMatrix(Double,Int32,Int32,RectangleShape,GeographyUnit)`](#tilematrixdoubleint32int32rectangleshapegeographyunit)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`BoundingBox`](#boundingbox)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`CellHeight`](#cellheight)|`Double`|N/A|
|[`CellWidth`](#cellwidth)|`Double`|N/A|
|[`GeographyUnit`](#geographyunit)|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This property gets or sets the BoundingBoxUnit for the TileMatrix.|
|[`Id`](#id)|`String`|N/A|
|[`Scale`](#scale)|`Double`|This property gets or sets the Scale for the TileMatrix.|
|[`TileHeight`](#tileheight)|`Int32`|This property gets or sets the TileHeight for the TileMatrix.|
|[`TileWidth`](#tilewidth)|`Int32`|This property gets or sets the TileWidth for the TileMatrix.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`GetAllCells()`](#getallcells)|
|[`GetCell(Int64,Int64)`](#getcellint64int64)|
|[`GetCell(PointShape)`](#getcellpointshape)|
|[`GetColumnCount()`](#getcolumncount)|
|[`GetColumnIndex(PointShape)`](#getcolumnindexpointshape)|
|[`GetContainedCells(RectangleShape)`](#getcontainedcellsrectangleshape)|
|[`GetContainedRowColumnRange(RectangleShape)`](#getcontainedrowcolumnrangerectangleshape)|
|[`GetDefaultMatrix(Double,Int32,Int32,GeographyUnit)`](#getdefaultmatrixdoubleint32int32geographyunit)|
|[`GetHashCode()`](#gethashcode)|
|[`GetIntersectingCells(RectangleShape)`](#getintersectingcellsrectangleshape)|
|[`GetIntersectingRowColumnRange(RectangleShape)`](#getintersectingrowcolumnrangerectangleshape)|
|[`GetRowCount()`](#getrowcount)|
|[`GetRowIndex(PointShape)`](#getrowindexpointshape)|
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
|[`TileMatrix()`](#tilematrix)|
|[`TileMatrix(Double)`](#tilematrixdouble)|
|[`TileMatrix(Double,RectangleShape,GeographyUnit)`](#tilematrixdoublerectangleshapegeographyunit)|
|[`TileMatrix(Double,Int32,Int32,RectangleShape,GeographyUnit)`](#tilematrixdoubleint32int32rectangleshapegeographyunit)|

### Protected Constructors


### Public Properties

#### `BoundingBox`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `CellHeight`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `CellWidth`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `GeographyUnit`

**Summary**

   *This property gets or sets the BoundingBoxUnit for the TileMatrix.*

**Remarks**

   *When set a different BoundingBoxUnit, it will recaculate the parameters in TileMatrix. Also, The default boundingBox value depends on the BoundingBoxUint.*

**Return Value**

[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)

---
#### `Id`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Scale`

**Summary**

   *This property gets or sets the Scale for the TileMatrix.*

**Remarks**

   *When set a different Scale, it will recaculate the parameters in TileMatrix.*

**Return Value**

`Double`

---
#### `TileHeight`

**Summary**

   *This property gets or sets the TileHeight for the TileMatrix.*

**Remarks**

   *When set a different TileHeight, it will recaculate the parameters in TileMatrix.*

**Return Value**

`Int32`

---
#### `TileWidth`

**Summary**

   *This property gets or sets the TileWidth for the TileMatrix.*

**Remarks**

   *When set a different TileWidth, it will recaculate the parameters in TileMatrix.*

**Return Value**

`Int32`

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
#### `GetAllCells()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCell(Int64,Int64)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|row|`Int64`|N/A|
|column|`Int64`|N/A|

---
#### `GetCell(PointShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|intersectingPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetColumnCount()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetColumnIndex(PointShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|intersectingPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetContainedCells(RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetContainedRowColumnRange(RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RowColumnRange`](../ThinkGeo.Core/ThinkGeo.Core.RowColumnRange.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetDefaultMatrix(Double,Int32,Int32,GeographyUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TileMatrix`](../ThinkGeo.Core/ThinkGeo.Core.TileMatrix.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|N/A|
|tileWidth|`Int32`|N/A|
|tileHeight|`Int32`|N/A|
|unit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

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
#### `GetIntersectingCells(RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetIntersectingRowColumnRange(RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RowColumnRange`](../ThinkGeo.Core/ThinkGeo.Core.RowColumnRange.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetRowCount()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetRowIndex(PointShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|intersectingPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

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


