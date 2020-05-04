# Matrix


## Inheritance Hierarchy

+ `Object`
  + **`Matrix`**
    + [`TileMatrix`](../ThinkGeo.Core/ThinkGeo.Core.TileMatrix.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`Matrix()`](#matrix)|
|[`Matrix(String,Double,Double,RectangleShape)`](#matrixstringdoubledoublerectangleshape)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`BoundingBox`](#boundingbox)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This property gets or sets the BouningBox of the Matrix.|
|[`CellHeight`](#cellheight)|`Double`|This property gets the cell height of the Matrix.|
|[`CellWidth`](#cellwidth)|`Double`|This property gets the cell width of the Matrix.|
|[`Id`](#id)|`String`|This property gets or sets the id of the Matrix.|

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
|N/A|

### Protected Constructors

#### `Matrix()`

**Summary**

   *This method is the default protected constructor.*

**Remarks**

   *If you use this constructor, you have to set the properties correctly before use it.*

**Return Value**

|Type|Description|
|---|---|
|N/A||

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Matrix(String,Double,Double,RectangleShape)`

**Summary**

   *This method is the default protected constructor.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|N/A||

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter specified the id of the Matrix.|
|cellWidth|`Double`|This parameter specified the cell width(in DecimalDegrees) of the Matrix.|
|cellHeight|`Double`|This parameter specified the cell height(in DecimalDegrees) of the Matrix.|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specified the boundingBox of the Matrix.|

---

### Public Properties

#### `BoundingBox`

**Summary**

   *This property gets or sets the BouningBox of the Matrix.*

**Remarks**

   *The bounding box of the Matrix is related with the referencePoint, cellWidth, cellHeight and the rowCount and columnCount.*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `CellHeight`

**Summary**

   *This property gets the cell height of the Matrix.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `CellWidth`

**Summary**

   *This property gets the cell width of the Matrix.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Id`

**Summary**

   *This property gets or sets the id of the Matrix.*

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
#### `GetAllCells()`

**Summary**

   *This method returns all the cells of the TileMatrix.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|This method returns a collection of MatrixCell.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCell(Int64,Int64)`

**Summary**

   *Get the cell by passing a specified row and column.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)|The returning cell by specified the row and column.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|row|`Int64`|This parameter specifies the row based on 1.|
|column|`Int64`|This parameter specifies the row based on 1.|

---
#### `GetCell(PointShape)`

**Summary**

   *Get the cell by passing a specified point shape location.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)|The returning cell by specifing the target point location.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|intersectingPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The parameter specified the target point location.|

---
#### `GetColumnCount()`

**Summary**

   *This method gets the column count of the Matrix.*

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

   *Get the column index by passing a specified point shape location.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|The returning column index by specifing the target point location.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|intersectingPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The parameter specified the target point location.|

---
#### `GetContainedCells(RectangleShape)`

**Summary**

   *This method returns the contained cells of the TileMatrix.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|This method returns a collection of MatrixCell which contained in the passed in extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies extent which is used to get the tiles back from.|

---
#### `GetContainedRowColumnRange(RectangleShape)`

**Summary**

   *This method returns the RowColumnRange of the TileMatrix contained the passed in extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RowColumnRange`](../ThinkGeo.Core/ThinkGeo.Core.RowColumnRange.md)|This method returns the RowColumnRange contained the passed in extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies extent which is used to get the tiles back from.|

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

   *This method returns the intersecting cells of the TileMatrix.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|This method returns a collection of MatrixCell which intersecting with the passed in extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies extent which is used to get the tiles back from.|

---
#### `GetIntersectingRowColumnRange(RectangleShape)`

**Summary**

   *This method returns the RowColumnRange of the TileMatrix intersects the passed in extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RowColumnRange`](../ThinkGeo.Core/ThinkGeo.Core.RowColumnRange.md)|This method returns the RowColumnRange intersects the passed in extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies extent which is used to get the tiles back from.|

---
#### `GetRowCount()`

**Summary**

   *This method gets the row count of the Matrix.*

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

   *Get the row index by passing a specified point shape location.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|The returning row index by specifing the target point location.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|intersectingPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The parameter specified the target point location.|

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


