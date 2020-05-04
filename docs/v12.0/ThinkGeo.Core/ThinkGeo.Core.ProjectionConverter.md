# ProjectionConverter


## Inheritance Hierarchy

+ `Object`
  + **`ProjectionConverter`**
    + [`RotationProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.RotationProjectionConverter.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ProjectionConverter()`](#projectionconverter)|
|[`ProjectionConverter(String,String)`](#projectionconverterstringstring)|
|[`ProjectionConverter(Int32,Int32)`](#projectionconverterint32int32)|
|[`ProjectionConverter(String,Int32)`](#projectionconverterstringint32)|
|[`ProjectionConverter(Int32,String)`](#projectionconverterint32string)|
|[`ProjectionConverter(Projection,Projection)`](#projectionconverterprojectionprojection)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanConvertRasterToExternalProjection`](#canconvertrastertoexternalprojection)|`Boolean`|N/A|
|[`DecimalDegreeBoundary`](#decimaldegreeboundary)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`ExternalProjection`](#externalprojection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|Gets or sets the Proj4 text parameter for the to projection. This parameter typically look like "+proj=utm +zone=33 +ellps=WGS84 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs".|
|[`InternalProjection`](#internalprojection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|Gets or sets the Proj4 text parameter for the from projection. This parameter typically look like "+Proj=longlat +ellps=WGS84 +datum=WGS84 +no_defs".|
|[`IsOpen`](#isopen)|`Boolean`|This property gets the state of the projection (whether it is opened or closed).|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
|[`Open()`](#open)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloseCore()`](#closecore)|
|[`ConvertToExternalProjectionCore(GeoImage,RectangleShape,RectangleShape,Int32,Int32)`](#converttoexternalprojectioncoregeoimagerectangleshaperectangleshapeint32int32)|
|[`ConvertToExternalProjectionCore(IEnumerable<Vertex>)`](#converttoexternalprojectioncoreienumerable<vertex>)|
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
|[`ProjectionConverter()`](#projectionconverter)|
|[`ProjectionConverter(String,String)`](#projectionconverterstringstring)|
|[`ProjectionConverter(Int32,Int32)`](#projectionconverterint32int32)|
|[`ProjectionConverter(String,Int32)`](#projectionconverterstringint32)|
|[`ProjectionConverter(Int32,String)`](#projectionconverterint32string)|
|[`ProjectionConverter(Projection,Projection)`](#projectionconverterprojectionprojection)|

### Protected Constructors


### Public Properties

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

   *Gets or sets the Proj4 text parameter for the to projection. This parameter typically look like "+proj=utm +zone=33 +ellps=WGS84 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs".*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `InternalProjection`

**Summary**

   *Gets or sets the Proj4 text parameter for the from projection. This parameter typically look like "+Proj=longlat +ellps=WGS84 +datum=WGS84 +no_defs".*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `IsOpen`

**Summary**

   *This property gets the state of the projection (whether it is opened or closed).*

**Remarks**

   *This method will reflect whether the projection is opened or closed. It is set in the concrete methods Open and Close, so if you inherit from this class and override OpenCore or CloseCore, you will not need to be concerned with setting this property.*

**Return Value**

`Boolean`

---

### Protected Properties


### Public Methods

#### `Close()`

**Summary**

   *This method closes the projection and gets it ready for serialization if necessary.*

**Remarks**

   *This method closes the projection and gets it ready for serialization if necessary. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

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

   *This method returns a projected vertex based on the coordinates passed in.*

**Remarks**

   *This method returns a projected vertex based on the coordinates passed in. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|This method returns a projected vertex based on the coordinates passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|This parameter is the X decimalDegreesValue of the point that will be projected.|
|y|`Double`|This parameter is the Y decimalDegreesValue of the point that will be projected.|

---
#### `ConvertToExternalProjection(BaseShape)`

**Summary**

   *This method returns a projected BaseShape based on the baseShape passed in.*

**Remarks**

   *This method returns a projected baseShape based on the BaseShape passed in. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a projected baseShape for the passed-in BaseShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the baseShape (in decimalDegreesValue) to be projected.|

---
#### `ConvertToExternalProjection(Feature)`

**Summary**

   *This method returns a projected Feature based on the Feature passed in.*

**Remarks**

   *This method returns a projected Feature based on the Feature passed in. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns a projected Feature for the passed-in Feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the Feature that contains a BaseShape in decimalDegreesValue to be projected.|

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

   *This method returns a projected rectangle based on the rectangle passed in.*

**Remarks**

   *This method returns a projected rectangle based on the rectangle passed in.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a projected rectangle based on the rectangle passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|rectangleShape|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the rectangle you want to project.|

---
#### `ConvertToExternalProjection(IEnumerable<Vertex>)`

**Summary**

   *This method returns projected vertices based on the coordinates passed in.*

**Remarks**

   *This method returns a projected vertex based on the coordinates passed in. You will need to override this method for the Projection class. Typically, you can call the projection utility library that has interfaces for dozens of different types of projections.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This method returns projected vertices based on the coordinates passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|vertices|IEnumerable<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This parameter is the vertices that will be projected.|

---
#### `ConvertToInternalProjection(Double,Double)`

**Summary**

   *This method returns a de-projected vertex based on the coordinates passed in.*

**Remarks**

   *This method returns a de-projected vertex based on the coordinates passed in. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|This method returns a de-projected vertex based on the coordinates passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|This parameter is the X decimalDegreesValue of the point that will be de-projected.|
|y|`Double`|This parameter is the Y decimalDegreesValue of the point that will be de-projected.|

---
#### `ConvertToInternalProjection(BaseShape)`

**Summary**

   *This method returns a de-projected BaseShape based on the BaseShape passed in.*

**Remarks**

   *This method returns a de-projected BaseShape based on the BaseShape passed in. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a de-projected BaseShape for the passed in BaseShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the baseShape (in decimalDegreesValue) to be de-projected.|

---
#### `ConvertToInternalProjection(Feature)`

**Summary**

   *This method returns a de-projected Feature based on the Feature passed in.*

**Remarks**

   *This method returns a de-projected Feature based on the Feature passed in. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns a de-projected Feature for the passed-in Feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the Feature that contains a BaseShape in decimalDegreesValue to be de-projected.|

---
#### `ConvertToInternalProjection(IEnumerable<Vertex>)`

**Summary**

   *This method returns de-projected vertices based on the coordinates passed in.*

**Remarks**

   *This method returns a de-projected vertex based on the coordinates passed in. The de-projection is important because, inside of the FeatureSource, you will in many cases go to and from various projections.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This method returns de-projected vertices based on the coordinates passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|vertices|IEnumerable<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This parameter is the vertices that will be de-projected.|

---
#### `ConvertToInternalProjection(RectangleShape)`

**Summary**

   *This method returns a de-projected rectangle based on the rectangle passed in.*

**Remarks**

   *This method returns a de-projected rectangle based on the rectangle passed in.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a de-projected rectangle based on the rectangle passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|rectangleShape|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the rectangle you want to de-project.|

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
#### `Open()`

**Summary**

   *This method opens the projection and gets it ready to use.*

**Remarks**

   *This method opens the projection and gets it ready to use. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

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

   *This method closes the projection and gets it ready for serialization if necessary.*

**Remarks**

   *As this is the core version of the Close method, it is intended to be overridden in an inherited version of the class. When overriding, you will be responsible freeing any state you have maintained and getting the class ready for serialization if necessary. Note that the object may be opened again, so you want to make sure you can open and close the object multiple times without any ill effects.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `ConvertToExternalProjectionCore(IEnumerable<Vertex>)`

**Summary**

   *This method returns projected vertices based on the coordinates passed in.*

**Remarks**

   *This method returns a projected vertex based on the coordinates passed in. You will need to override this method for the Projection class. Typically, you can call the projection utility library that has interfaces for dozens of different types of projections.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This method returns projected vertices based on the coordinates passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|verticies|IEnumerable<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|N/A|

---
#### `ConvertToInternalProjectionCore(IEnumerable<Vertex>)`

**Summary**

   *This method returns de-projected vertices based on the coordinates passed in.*

**Remarks**

   *This method returns a de-projected vertex based on the coordinates passed in. You will need to override this method for the Projection class. Typically, you can call the projection utility library that has interfaces for dozens of different types of projections. The de-projection is important because, inside of the FeatureSource, you will in many cases go to and from various projections.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This method returns de-projected vertices based on the coordinates passed in.|

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

   *This method opens the projection and gets it ready to use.*

**Remarks**

   *As this is the core version of the Open method, it is intended to be overridden in an inherited version of the class. When overriding, you will be responsible for getting the projection classes' state ready for doing projections.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

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


