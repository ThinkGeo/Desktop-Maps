# RotationProjectionConverter


## Inheritance Hierarchy

+ `Object`
  + [`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)
    + **`RotationProjectionConverter`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`RotationProjectionConverter()`](#rotationprojectionconverter)|
|[`RotationProjectionConverter(Double)`](#rotationprojectionconverterdouble)|
|[`RotationProjectionConverter(GeographyUnit)`](#rotationprojectionconvertergeographyunit)|
|[`RotationProjectionConverter(Double,GeographyUnit)`](#rotationprojectionconverterdoublegeographyunit)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Angle`](#angle)|`Double`|This property sets the angle of rotation.|
|[`CanConvertRasterToExternalProjection`](#canconvertrastertoexternalprojection)|`Boolean`|N/A|
|[`DecimalDegreeBoundary`](#decimaldegreeboundary)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`ExternalProjection`](#externalprojection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`InternalProjection`](#internalprojection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`PivotCenter`](#pivotcenter)|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|[`SourceUnit`](#sourceunit)|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`PreviousRotateAngle`](#previousrotateangle)|`Double`|N/A|
|[`PreviousRotationShape`](#previousrotationshape)|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|

### Public Methods Summary


|Name|
|---|
|[`Close()`](#close)|
|[`ConvertToExternalProjection(GeoImage,RectangleShape,RectangleShape,Int32,Int32)`](#converttoexternalprojectiongeoimagerectangleshaperectangleshapeint32int32)|
|[`ConvertToExternalProjection(GeoImage,RectangleShape)`](#converttoexternalprojectiongeoimagerectangleshape)|
|[`ConvertToExternalProjection(GeoImage,RectangleShape,RectangleShape)`](#converttoexternalprojectiongeoimagerectangleshaperectangleshape)|
|[`ConvertToExternalProjection(Double,Double)`](#converttoexternalprojectiondoubledouble)|
|[`ConvertToExternalProjection(BaseShape)`](#converttoexternalprojectionbaseshape)|
|[`ConvertToExternalProjection(Feature)`](#converttoexternalprojectionfeature)|
|[`ConvertToExternalProjection(IEnumerable<Feature>)`](#converttoexternalprojectionienumerable<feature>)|
|[`ConvertToExternalProjection(RectangleShape)`](#converttoexternalprojectionrectangleshape)|
|[`ConvertToExternalProjection(IEnumerable<Vertex>)`](#converttoexternalprojectionienumerable<vertex>)|
|[`ConvertToInternalProjection(Double,Double)`](#converttointernalprojectiondoubledouble)|
|[`ConvertToInternalProjection(BaseShape)`](#converttointernalprojectionbaseshape)|
|[`ConvertToInternalProjection(Feature)`](#converttointernalprojectionfeature)|
|[`ConvertToInternalProjection(IEnumerable<Vertex>)`](#converttointernalprojectionienumerable<vertex>)|
|[`ConvertToInternalProjection(RectangleShape)`](#converttointernalprojectionrectangleshape)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`GetUpdatedExtent(RectangleShape)`](#getupdatedextentrectangleshape)|
|[`Open()`](#open)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloseCore()`](#closecore)|
|[`ConvertToExternalProjectionCore(IEnumerable<Vertex>)`](#converttoexternalprojectioncoreienumerable<vertex>)|
|[`ConvertToExternalProjectionCore(GeoImage,RectangleShape,RectangleShape,Int32,Int32)`](#converttoexternalprojectioncoregeoimagerectangleshaperectangleshapeint32int32)|
|[`ConvertToInternalProjectionCore(IEnumerable<Vertex>)`](#converttointernalprojectioncoreienumerable<vertex>)|
|[`Finalize()`](#finalize)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OpenCore()`](#opencore)|
|[`UpdateToExternalProjection(Feature)`](#updatetoexternalprojectionfeature)|
|[`UpdateToInternalProjection(Feature)`](#updatetointernalprojectionfeature)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`RotationProjectionConverter()`](#rotationprojectionconverter)|
|[`RotationProjectionConverter(Double)`](#rotationprojectionconverterdouble)|
|[`RotationProjectionConverter(GeographyUnit)`](#rotationprojectionconvertergeographyunit)|
|[`RotationProjectionConverter(Double,GeographyUnit)`](#rotationprojectionconverterdoublegeographyunit)|

### Protected Constructors


### Public Properties

#### `Angle`

**Summary**

   *This property sets the angle of rotation.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `CanConvertRasterToExternalProjection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `DecimalDegreeBoundary`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `ExternalProjection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `InternalProjection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `IsOpen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `PivotCenter`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)

---
#### `SourceUnit`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)

---

### Protected Properties

#### `PreviousRotateAngle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `PreviousRotationShape`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)

---

### Public Methods

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
#### `ConvertToExternalProjection(GeoImage,RectangleShape,RectangleShape,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterProjectionResult`](../ThinkGeo.Core/ThinkGeo.Core.RasterProjectionResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|imageExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|width|`Int32`|N/A|
|height|`Int32`|N/A|

---
#### `ConvertToExternalProjection(GeoImage,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterProjectionResult`](../ThinkGeo.Core/ThinkGeo.Core.RasterProjectionResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|imageExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `ConvertToExternalProjection(GeoImage,RectangleShape,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterProjectionResult`](../ThinkGeo.Core/ThinkGeo.Core.RasterProjectionResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|imageExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `ConvertToExternalProjection(Double,Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|

---
#### `ConvertToExternalProjection(BaseShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `ConvertToExternalProjection(Feature)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `ConvertToExternalProjection(IEnumerable<Feature>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `ConvertToExternalProjection(RectangleShape)`

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
|rectangleShape|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `ConvertToExternalProjection(IEnumerable<Vertex>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|vertices|IEnumerable<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|N/A|

---
#### `ConvertToInternalProjection(Double,Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|

---
#### `ConvertToInternalProjection(BaseShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `ConvertToInternalProjection(Feature)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `ConvertToInternalProjection(IEnumerable<Vertex>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|vertices|IEnumerable<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|N/A|

---
#### `ConvertToInternalProjection(RectangleShape)`

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
|rectangleShape|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `GetUpdatedExtent(RectangleShape)`

**Summary**

   *This method returns an adjusted extend based on the angle of rotation.*

**Remarks**

   *This method returns an adjusted extend based on the angle of rotation. It is important that you update your current extent every time you adjust the angle of the projection. This will ensure the rotaion is performed properly.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extend based on the angle of rotation.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent before the rotation.|

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
#### `ConvertToExternalProjectionCore(IEnumerable<Vertex>)`

**Summary**

   *This method returns a projected vertices based on the coordinates passed in.*

**Remarks**

   *This method returns a projected vertex based on the coordinates passed in. You will need to override this method for the Projection class. Typically you can call the projection utility library that has interfaces for dozens of different types of projections.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This method returns a projected vertices based on the coordinates passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|verticies|IEnumerable<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|N/A|

---
#### `ConvertToExternalProjectionCore(GeoImage,RectangleShape,RectangleShape,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterProjectionResult`](../ThinkGeo.Core/ThinkGeo.Core.RasterProjectionResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|imageExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|width|`Int32`|N/A|
|height|`Int32`|N/A|

---
#### `ConvertToInternalProjectionCore(IEnumerable<Vertex>)`

**Summary**

   *This method returns a de-projected vertices based on the coordinates passed in.*

**Remarks**

   *This method returns a de-projected vertex based on the coordinates passed in. You will need to override this method for the Projection class. Typically you can call the projection utility library that has interfaces for dozens of different types of projections. The de-projection is important because inside of the FeatureSource you will in many cases to to and from various projections.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This method returns a de-projected vertices based on the coordinates passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|verticies|IEnumerable<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|N/A|

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
#### `UpdateToExternalProjection(Feature)`

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
#### `UpdateToInternalProjection(Feature)`

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

### Public Events


