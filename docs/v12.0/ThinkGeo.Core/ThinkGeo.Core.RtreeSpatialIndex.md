# RtreeSpatialIndex


## Inheritance Hierarchy

+ `Object`
  + [`SpatialIndex`](../ThinkGeo.Core/ThinkGeo.Core.SpatialIndex.md)
    + **`RtreeSpatialIndex`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`RtreeSpatialIndex()`](#rtreespatialindex)|
|[`RtreeSpatialIndex(String)`](#rtreespatialindexstring)|
|[`RtreeSpatialIndex(String,FileAccess)`](#rtreespatialindexstringfileaccess)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanDelete`](#candelete)|`Boolean`|N/A|
|[`DataFormat`](#dataformat)|[`RtreeSpatialIndexDataFormat`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexDataFormat.md)|N/A|
|[`HasIdx`](#hasidx)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`PageSize`](#pagesize)|`Int32`|Property PageSize.|
|[`PathFilename`](#pathfilename)|`String`|N/A|
|[`ReadWriteMode`](#readwritemode)|`FileAccess`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsOpenCore`](#isopencore)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`Add(BaseShape)`](#addbaseshape)|
|[`Add(Feature)`](#addfeature)|
|[`Close()`](#close)|
|[`CreatePointSpatialIndex(String)`](#createpointspatialindexstring)|
|[`CreatePointSpatialIndex(String,RtreeSpatialIndexPageSize)`](#createpointspatialindexstringrtreespatialindexpagesize)|
|[`CreatePointSpatialIndex(String,RtreeSpatialIndexPageSize,RtreeSpatialIndexDataFormat)`](#createpointspatialindexstringrtreespatialindexpagesizertreespatialindexdataformat)|
|[`CreateRectangleSpatialIndex(String)`](#createrectanglespatialindexstring)|
|[`CreateRectangleSpatialIndex(String,RtreeSpatialIndexPageSize)`](#createrectanglespatialindexstringrtreespatialindexpagesize)|
|[`CreateRectangleSpatialIndex(String,RtreeSpatialIndexPageSize,RtreeSpatialIndexDataFormat)`](#createrectanglespatialindexstringrtreespatialindexpagesizertreespatialindexdataformat)|
|[`Delete(Feature)`](#deletefeature)|
|[`Delete(BaseShape)`](#deletebaseshape)|
|[`DeleteRecord(BaseShape)`](#deleterecordbaseshape)|
|[`Dispose()`](#dispose)|
|[`Equals(Object)`](#equalsobject)|
|[`Flush()`](#flush)|
|[`GetBestPageSize(Int32)`](#getbestpagesizeint32)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetFeatureCount()`](#getfeaturecount)|
|[`GetFeatureIdsContainingRectangleShape(RectangleShape)`](#getfeatureidscontainingrectangleshaperectangleshape)|
|[`GetFeatureIdsIntersectingBoundingBox(RectangleShape,Double,Double,Int32,Collection<RectangleShape>)`](#getfeatureidsintersectingboundingboxrectangleshapedoubledoubleint32collection<rectangleshape>)|
|[`GetFeatureIdsIntersectingBoundingBox(RectangleShape)`](#getfeatureidsintersectingboundingboxrectangleshape)|
|[`GetFeatureIdsNearestTo(PointShape,Int32)`](#getfeatureidsnearesttopointshapeint32)|
|[`GetFeatureIdsWithinBoundingBox(RectangleShape)`](#getfeatureidswithinboundingboxrectangleshape)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`IsRtreeSpatialIndexFileValid(String)`](#isrtreespatialindexfilevalidstring)|
|[`Open()`](#open)|
|[`RefreshCache()`](#refreshcache)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`AddCore(Feature)`](#addcorefeature)|
|[`CloseCore()`](#closecore)|
|[`DeleteCore(Feature)`](#deletecorefeature)|
|[`Finalize()`](#finalize)|
|[`GetFeatureCountCore()`](#getfeaturecountcore)|
|[`GetFeatureIdsContainingRectangleShapeCore(RectangleShape)`](#getfeatureidscontainingrectangleshapecorerectangleshape)|
|[`GetFeatureIdsIntersectingBoundingBoxCore(RectangleShape)`](#getfeatureidsintersectingboundingboxcorerectangleshape)|
|[`GetFeatureIdsNearestToCore(PointShape,Int32)`](#getfeatureidsnearesttocorepointshapeint32)|
|[`GetFeatureIdsWithinBoundingBoxCore(RectangleShape)`](#getfeatureidswithinboundingboxcorerectangleshape)|
|[`GetRoot()`](#getroot)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnStreamLoading(StreamLoadingEventArgs)`](#onstreamloadingstreamloadingeventargs)|
|[`OpenCore()`](#opencore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`StreamLoading`](#streamloading)|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`RtreeSpatialIndex()`](#rtreespatialindex)|
|[`RtreeSpatialIndex(String)`](#rtreespatialindexstring)|
|[`RtreeSpatialIndex(String,FileAccess)`](#rtreespatialindexstringfileaccess)|

### Protected Constructors


### Public Properties

#### `CanDelete`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `DataFormat`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RtreeSpatialIndexDataFormat`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexDataFormat.md)

---
#### `HasIdx`

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
#### `PageSize`

**Summary**

   *Property PageSize.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `PathFilename`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `ReadWriteMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FileAccess`

---

### Protected Properties

#### `IsOpenCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---

### Public Methods

#### `Add(BaseShape)`

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
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `Add(Feature)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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
#### `CreatePointSpatialIndex(String)`

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
|pathFilename|`String`|N/A|

---
#### `CreatePointSpatialIndex(String,RtreeSpatialIndexPageSize)`

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
|pathFilename|`String`|N/A|
|pageSize|[`RtreeSpatialIndexPageSize`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexPageSize.md)|N/A|

---
#### `CreatePointSpatialIndex(String,RtreeSpatialIndexPageSize,RtreeSpatialIndexDataFormat)`

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
|pathFilename|`String`|N/A|
|pageSize|[`RtreeSpatialIndexPageSize`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexPageSize.md)|N/A|
|dataFormat|[`RtreeSpatialIndexDataFormat`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexDataFormat.md)|N/A|

---
#### `CreateRectangleSpatialIndex(String)`

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
|pathFilename|`String`|N/A|

---
#### `CreateRectangleSpatialIndex(String,RtreeSpatialIndexPageSize)`

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
|pathFilename|`String`|N/A|
|pageSize|[`RtreeSpatialIndexPageSize`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexPageSize.md)|N/A|

---
#### `CreateRectangleSpatialIndex(String,RtreeSpatialIndexPageSize,RtreeSpatialIndexDataFormat)`

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
|pathFilename|`String`|N/A|
|pageSize|[`RtreeSpatialIndexPageSize`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexPageSize.md)|N/A|
|dataFormat|[`RtreeSpatialIndexDataFormat`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexDataFormat.md)|N/A|

---
#### `Delete(Feature)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `Delete(BaseShape)`

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
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `DeleteRecord(BaseShape)`

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
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `Dispose()`

**Summary**

   *This method is targeting releasing or resetting unmanaged resources.*

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
#### `Flush()`

**Summary**

   *Write memory to disk if modified.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|true for success false for failure|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetBestPageSize(Int32)`

**Summary**

   *Static method for getting the best page size according to the record count of a ShapeFile.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RtreeSpatialIndexPageSize`](../ThinkGeo.Core/ThinkGeo.Core.RtreeSpatialIndexPageSize.md)|best page size|

**Parameters**

|Name|Type|Description|
|---|---|---|
|recordCount|`Int32`|Record count of ShapeFile.|

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
#### `GetFeatureCount()`

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
#### `GetFeatureIdsContainingRectangleShape(RectangleShape)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetFeatureIdsIntersectingBoundingBox(RectangleShape,Double,Double,Int32,Collection<RectangleShape>)`

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
|rectangleShape|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|simplifyPixelBufferSize|`Int32`|N/A|
|dimensionlessBoxes|Collection<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|

---
#### `GetFeatureIdsIntersectingBoundingBox(RectangleShape)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetFeatureIdsNearestTo(PointShape,Int32)`

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
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|maxReturningCount|`Int32`|N/A|

---
#### `GetFeatureIdsWithinBoundingBox(RectangleShape)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `IsRtreeSpatialIndexFileValid(String)`

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
|indexFileName|`String`|N/A|

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
#### `RefreshCache()`

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

#### `AddCore(Feature)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `CloseCore()`

**Summary**

   *Close a previously opened index file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|true for success false for failure|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DeleteCore(Feature)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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
#### `GetFeatureCountCore()`

**Summary**

   *Get the count of all records in all leaf nodes.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|record count|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetFeatureIdsContainingRectangleShapeCore(RectangleShape)`

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
|rectangleShape|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetFeatureIdsIntersectingBoundingBoxCore(RectangleShape)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetFeatureIdsNearestToCore(PointShape,Int32)`

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
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|maxReturningCount|`Int32`|N/A|

---
#### `GetFeatureIdsWithinBoundingBoxCore(RectangleShape)`

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
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetRoot()`

**Summary**

   *Get the root node.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Node`](../ThinkGeo.Core/ThinkGeo.Core.Node.md)|root node|

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
#### `OnStreamLoading(StreamLoadingEventArgs)`

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
|e|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|

---
#### `OpenCore()`

**Summary**

   *Open an existing index file as read only.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|true for success false for failure|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Events

#### StreamLoading

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)


