# BaseShape


## Inheritance Hierarchy

+ `Object`
  + **`BaseShape`**
    + [`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)
    + [`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)
    + [`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)
    + [`GeometryCollectionShape`](../ThinkGeo.Core/ThinkGeo.Core.GeometryCollectionShape.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`BaseShape()`](#baseshape)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Id`](#id)|`String`|The id of the shape.|
|[`Tag`](#tag)|`Object`|The tag of the shape.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Buffer(Double,GeographyUnit,DistanceUnit)`](#bufferdoublegeographyunitdistanceunit)|
|[`Buffer(Double,Int32,GeographyUnit,DistanceUnit)`](#bufferdoubleint32geographyunitdistanceunit)|
|[`Buffer(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`](#bufferdoubleint32buffercaptypegeographyunitdistanceunit)|
|[`CloneDeep()`](#clonedeep)|
|[`Contains(BaseShape)`](#containsbaseshape)|
|[`Contains(Feature)`](#containsfeature)|
|[`CreateShapeFromGeoJson(String)`](#createshapefromgeojsonstring)|
|[`CreateShapeFromWellKnownData(String)`](#createshapefromwellknowndatastring)|
|[`CreateShapeFromWellKnownData(Byte[])`](#createshapefromwellknowndatabyte[])|
|[`Crosses(BaseShape)`](#crossesbaseshape)|
|[`Crosses(Feature)`](#crossesfeature)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetCenterPoint()`](#getcenterpoint)|
|[`GetClosestPointTo(BaseShape,GeographyUnit)`](#getclosestpointtobaseshapegeographyunit)|
|[`GetClosestPointTo(Feature,GeographyUnit)`](#getclosestpointtofeaturegeographyunit)|
|[`GetCrossing(BaseShape)`](#getcrossingbaseshape)|
|[`GetDistanceTo(BaseShape,GeographyUnit,DistanceUnit)`](#getdistancetobaseshapegeographyunitdistanceunit)|
|[`GetDistanceTo(Feature,GeographyUnit,DistanceUnit)`](#getdistancetofeaturegeographyunitdistanceunit)|
|[`GetFeature()`](#getfeature)|
|[`GetFeature(IDictionary<String,String>)`](#getfeatureidictionary<stringstring>)|
|[`GetGeoJson()`](#getgeojson)|
|[`GetHashCode()`](#gethashcode)|
|[`GetShortestLineTo(BaseShape,GeographyUnit)`](#getshortestlinetobaseshapegeographyunit)|
|[`GetShortestLineTo(Feature,GeographyUnit)`](#getshortestlinetofeaturegeographyunit)|
|[`GetType()`](#gettype)|
|[`GetWellKnownBinary()`](#getwellknownbinary)|
|[`GetWellKnownBinary(WkbByteOrder)`](#getwellknownbinarywkbbyteorder)|
|[`GetWellKnownBinary(RingOrder)`](#getwellknownbinaryringorder)|
|[`GetWellKnownBinary(RingOrder,WkbByteOrder)`](#getwellknownbinaryringorderwkbbyteorder)|
|[`GetWellKnownText()`](#getwellknowntext)|
|[`GetWellKnownText(RingOrder)`](#getwellknowntextringorder)|
|[`GetWellKnownType()`](#getwellknowntype)|
|[`Intersects(BaseShape)`](#intersectsbaseshape)|
|[`Intersects(Feature)`](#intersectsfeature)|
|[`IsDisjointed(BaseShape)`](#isdisjointedbaseshape)|
|[`IsDisjointed(Feature)`](#isdisjointedfeature)|
|[`IsTopologicallyEqual(BaseShape)`](#istopologicallyequalbaseshape)|
|[`IsTopologicallyEqual(Feature)`](#istopologicallyequalfeature)|
|[`IsWithin(BaseShape)`](#iswithinbaseshape)|
|[`IsWithin(Feature)`](#iswithinfeature)|
|[`LoadFromWellKnownData(String)`](#loadfromwellknowndatastring)|
|[`LoadFromWellKnownData(Byte[])`](#loadfromwellknowndatabyte[])|
|[`Overlaps(BaseShape)`](#overlapsbaseshape)|
|[`Overlaps(Feature)`](#overlapsfeature)|
|[`Register(PointShape,PointShape,DistanceUnit,GeographyUnit)`](#registerpointshapepointshapedistanceunitgeographyunit)|
|[`Register(Feature,Feature,DistanceUnit,GeographyUnit)`](#registerfeaturefeaturedistanceunitgeographyunit)|
|[`Rotate(PointShape,Single)`](#rotatepointshapesingle)|
|[`Rotate(BaseShape,PointShape,Single)`](#rotatebaseshapepointshapesingle)|
|[`Rotate(Feature,PointShape,Single)`](#rotatefeaturepointshapesingle)|
|[`ScaleTo(Double)`](#scaletodouble)|
|[`ScaleTo(BaseShape,Double)`](#scaletobaseshapedouble)|
|[`ToString()`](#tostring)|
|[`Touches(BaseShape)`](#touchesbaseshape)|
|[`Touches(Feature)`](#touchesfeature)|
|[`TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreedoubledoublegeographyunitdistanceunit)|
|[`TranslateByDegree(Double,Double)`](#translatebydegreedoubledouble)|
|[`TranslateByDegree(BaseShape,Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreebaseshapedoubledoublegeographyunitdistanceunit)|
|[`TranslateByDegree(Feature,Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreefeaturedoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetdoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Double,Double)`](#translatebyoffsetdoubledouble)|
|[`TranslateByOffset(BaseShape,Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetbaseshapedoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Feature,Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetfeaturedoubledoublegeographyunitdistanceunit)|
|[`Validate(ShapeValidationMode)`](#validateshapevalidationmode)|

### Protected Methods Summary


|Name|
|---|
|[`BufferCore(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`](#buffercoredoubleint32buffercaptypegeographyunitdistanceunit)|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`ContainsCore(BaseShape)`](#containscorebaseshape)|
|[`CrossesCore(BaseShape)`](#crossescorebaseshape)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetCenterPointCore()`](#getcenterpointcore)|
|[`GetClosestPointToCore(BaseShape,GeographyUnit)`](#getclosestpointtocorebaseshapegeographyunit)|
|[`GetCrossingCore(BaseShape)`](#getcrossingcorebaseshape)|
|[`GetDistanceToCore(BaseShape,GeographyUnit,DistanceUnit)`](#getdistancetocorebaseshapegeographyunitdistanceunit)|
|[`GetGeoJsonCore()`](#getgeojsoncore)|
|[`GetShortestLineToCore(BaseShape,GeographyUnit)`](#getshortestlinetocorebaseshapegeographyunit)|
|[`GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`](#getwellknownbinarycoreringorderwkbbyteorder)|
|[`GetWellKnownTextCore(RingOrder)`](#getwellknowntextcoreringorder)|
|[`GetWellKnownTypeCore()`](#getwellknowntypecore)|
|[`IntersectsCore(BaseShape)`](#intersectscorebaseshape)|
|[`IsDisjointedCore(BaseShape)`](#isdisjointedcorebaseshape)|
|[`IsTopologicallyEqualCore(BaseShape)`](#istopologicallyequalcorebaseshape)|
|[`IsWithinCore(BaseShape)`](#iswithincorebaseshape)|
|[`LoadFromWellKnownDataCore(String)`](#loadfromwellknowndatacorestring)|
|[`LoadFromWellKnownDataCore(Byte[])`](#loadfromwellknowndatacorebyte[])|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OverlapsCore(BaseShape)`](#overlapscorebaseshape)|
|[`RegisterCore(PointShape,PointShape,DistanceUnit,GeographyUnit)`](#registercorepointshapepointshapedistanceunitgeographyunit)|
|[`RotateCore(PointShape,Single)`](#rotatecorepointshapesingle)|
|[`ScaleToCore(Double)`](#scaletocoredouble)|
|[`TouchesCore(BaseShape)`](#touchescorebaseshape)|
|[`TranslateByDegreeCore(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreecoredoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffsetCore(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetcoredoubledoublegeographyunitdistanceunit)|
|[`ValidateCore(ShapeValidationMode)`](#validatecoreshapevalidationmode)|

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

#### `BaseShape()`

**Summary**

   *This is the default constructor for BaseShape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|N/A||

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Properties

#### `Id`

**Summary**

   *The id of the shape.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Tag`

**Summary**

   *The tag of the shape.*

**Remarks**

   *N/A*

**Return Value**

`Object`

---

### Protected Properties


### Public Methods

#### `Buffer(Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the area containing all of the points within a given distance from this shape.*

**Remarks**

   *This method computes the area containing all of the points within a given distance from this shape. In this case, you will be using the rounded RoundedBufferCapStyle and the default 8 quadrant segments. The distance unit is determined by the distanceUnit argument. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that represents all of the points within a given distance from the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to buffer the current shape. The distance unit will be the one specified in the distanceUnit parameter.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the distance. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles for the operation.|

---
#### `Buffer(Double,Int32,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the area containing all of the points within a given distance from this shape.*

**Remarks**

   *This method computes the area containing all of the points within a given distance from this shape. In this case, you will be using the rounded RoundedBufferCapStyle. The distance unit is determined by the distanceUnit argument. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that represents all of the points within a given distance from the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to buffer the current shape. The distance unit will be the one specified in the distanceUnit parameter.|
|quadrantSegments|`Int32`|The quadrant segments are the number of points in each quarter circle. A good default is 8, but if you want smoother edges you can increase this number. The valid range for this number is from 3 to 100.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the distance. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles for the operation.|

---
#### `Buffer(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the area containing all of the points within a given distance from this shape.*

**Remarks**

   *This method computes the area containing all of the points within a given distance from this shape. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that represents all of the points within a given distance from the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to buffer the current shape. The distance unit will be the one specified in the distanceUnit parameter.|
|quadrantSegments|`Int32`|The quadrant segments are the number of points in each quarter circle. A good default is 8, but if you want smoother edges you can increase this number. The valid range for this number is from 3 to 100.|
|bufferCapType|[`BufferCapType`](../ThinkGeo.Core/ThinkGeo.Core.BufferCapType.md)|The bufferCapType determines how the caps of the buffered object look. They range from rounded to squared off.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the distance. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles for the operation.|

---
#### `CloneDeep()`

**Summary**

   *This method returns a complete copy of the shape without any references in common.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a complete copy of the shape without any references in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Contains(BaseShape)`

**Summary**

   *This method returns if the targetShape lies within the interior of the current shape.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the targetShape lies within the interior of the current shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `Contains(Feature)`

**Summary**

   *This method returns if the targetFeature lies within the interior of the current shape.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the targetFeature lies within the interior of the current shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `CreateShapeFromGeoJson(String)`

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
|geoJson|`String`|N/A|

---
#### `CreateShapeFromWellKnownData(String)`

**Summary**

   *This method creates a BaseShape from a string of well-known text.*

**Remarks**

   *This method creates a BaseShape from a string of well-known text. Well-known text allows you to describe geometries as a string of text. Well-known text is useful when you want to save a geometry in a format such as a text file, or when you simply want to cut and paste the text between other applications. An alternative to well-known text is well-known binary, which is a binary representation of a geometry object. We have methods that work with well-known binary as well. Below are some samples of what well-known text might look like for various kinds of geometries.POINT(5 17)LINESTRING(4 5,10 50,25 80)POLYGON((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3))MULTIPOINT(3.7 9.7,4.9 11.6)MULTILINESTRING((4 5,11 51,21 26),(-4 -7,-9 -7,-14 -3))MULTIPOLYGON(((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3)),((4 4,7 3,7 5,4 4)))*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The return type is a higher level shape constructed from the well-known text you passed into the method. Though the object is a higher level shape, such as a PolygonShape or MultiPointShape, you will need to cast it to that shape in order to use its unique properties.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wellKnownText|`String`|A string representing the geometry in well-known text format.|

---
#### `CreateShapeFromWellKnownData(Byte[])`

**Summary**

   *This method creates a BaseShape from a string of well-known binary.*

**Remarks**

   *This method creates a BaseShape from a string of well-known binary. Well-known binary allows you to describe geometries as a binary array. Well-known binary is useful when you want to save a geometry in an efficient format using as little space as possible. An alternative to well-known binary is well-known text, which is a textual representation of a geometry object. We have methods that work with well-known text as well.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The return type is a higher level shape constructed from the well-known binary you passed into the method. Though the object is a higher level shape, such as a PolygonShape or MultiPointShape, you will need to cast it to that shape in order to use its unique properties.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wellKnownBinary|`Byte[]`|An array of bytes representing the geometry in well-known binary format.|

---
#### `Crosses(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape share some but not all interior points.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape share some but not all interior points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `Crosses(Feature)`

**Summary**

   *This method returns if the current shape and the targetFeature share some but not all interior points.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetFeature share some but not all interior points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

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
#### `GetBoundingBox()`

**Summary**

   *This method calculates the smallest RectangleShape that encompasses the entire geometry.*

**Remarks**

   *The GetBoundingBox method calculates the smallest RectangleShape that can encompass the entire geometry by examining each point in the geometry. Depending on the number of PointShapes and complexity of the geometry, this operation can take longer for larger objects. If the shape is a PointShape, then the bounding box's upper left and lower right points will be equal. This will create a RectangleShape with no area. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The RectangleShape returned is the smallest RectangleShape that can encompass the entire geometry.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCenterPoint()`

**Summary**

   *This method returns the center point of the current shape's bounding box.*

**Remarks**

   *This method returns the center point of the current shape's bounding box. It is important to note that this is the center point of the bounding box. There are numerous ways to calculate the "center" of a geometry, such as its weighted center, etc. You can find other centers by examining the various methods of the shape itself. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A PointShape representing the center point of the current shape's bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetClosestPointTo(BaseShape,GeographyUnit)`

**Summary**

   *This method returns the point of the current shape that is closest to the target shape.*

**Remarks**

   *This method returns the point of the current shape that is closest to the target shape. It is often the case that the point returned is not a point of the object itself. An example would be a line with two points that are far apart from each other. If you set the targetShape to be a point midway between the points but a short distance away from the line, the method would return a point that is on the line but not either of the two points that make up the line. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A PointShape representing the closest point of the current shape to the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the closest point to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The geographic unit of the shape you are trying to find the closet point to.|

---
#### `GetClosestPointTo(Feature,GeographyUnit)`

**Summary**

   *This method returns the point of the current shape that is closest to the target feature.*

**Remarks**

   *This method returns the point of the current shape that is closest to the target feature. It is often the case that the point returned is not a point of the object itself. An example would be a line with two points that are far apart from each other. If you set the targetFeature to be a point midway between the points but a short distance away from the line, the method would return a point that is on the line but not either of the two points that make up the line. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A PointShape representing the closest point of the current shape to the targetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the closest point to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The geographic unit of the feature you are trying to find the closet point to.|

---
#### `GetCrossing(BaseShape)`

**Summary**

   *This method returns the crossing points between the current shape and the passed-in target shape.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipointShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipointShape.md)|This method returns the crossing points between the current shape and the passed-in target shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The target shape you wish to get crossing points with.|

---
#### `GetDistanceTo(BaseShape,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the distance between the current shape and the targetShape.*

**Remarks**

   *In this method we compute the closest distance between the two shapes. The returned unit will be in the unit of distance specified. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return type is the distance between this shape and the targetShape in the GeographyUnit of the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of the shape you are getting the distance to.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter is the unit of the distance you want the return value to be in.|

---
#### `GetDistanceTo(Feature,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the distance between the current shape and the targetFeature.*

**Remarks**

   *In this method we compute the closest distance between a shape and a feature. The returned unit will be in the unit of distance specified. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return type is the distance between this shape and the targetFeature in the GeographyUnit of the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of the shape which is contained in the targetFeature you are getting the distance to.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter is the unit of the distance you want the return value to be in.|

---
#### `GetFeature()`

**Summary**

   *Get a corresponding feature which has the same Id and BaseShape as the current shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature with the same Id and BaseShape as the current BaseShape, and with empty columnValues in it.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetFeature(IDictionary<String,String>)`

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
|columnValues|IDictionary<`String`,`String`>|N/A|

---
#### `GetGeoJson()`

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
#### `GetShortestLineTo(BaseShape,GeographyUnit)`

**Summary**

   *This method returns the shortest LineShape between this shape and the targetShape parameter.*

**Remarks**

   *This method returns a LineShape representing the shortest distance between the shape you're calling the method on and the targetShape. In some instances, based on the GeographicType or Projection, the line may not be straight. This effect is similar to what you might see on an international flight when the displayed flight path is curved. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|A LineShape representing the shortest distance between the shape you're calling the method on and the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The geographic unit of the Shape you are trying to find the distance to.|

---
#### `GetShortestLineTo(Feature,GeographyUnit)`

**Summary**

   *This method returns the shortest LineShape between this shape and the targetFeature. parameter.*

**Remarks**

   *This method returns a MultiLineShape representing the shortest distance between the shape you're calling the method on and the targetShape. In some instances, based on the GeographicType or Projection, the line may not be straight. This is effect is similar to what you might see on an international flight when the displayed flight path is curved. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|A MultiLineShape representing the shortest distance between the shape you're calling the method on and the targetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The geographic unit of the feature you are trying to find the distance to.|

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
#### `GetWellKnownBinary()`

**Summary**

   *This method returns a byte array that represents the shape in well-known binary.*

**Remarks**

   *This method returns a byte array that represents the shape in well-known binary. Well-known binary allows you to describe geometries as a binary array. Well-known binary is useful when you want to save a geometry in an efficient format using as little space as possible. An alternative to well-known binary is well-known text, which is a textual representation of a geometry object. We have methods that work with well known text as well. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|This method returns a byte array that represents the shape in well-known binary.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWellKnownBinary(WkbByteOrder)`

**Summary**

   *This method returns a byte array that represents the shape in well-known binary.*

**Remarks**

   *This method returns a byte array that represents the shape in well-known binary. Well-known binary allows you to describe geometries as a binary array. Well-known binary is useful when you want to save a geometry in an efficient format using as little space as possible. An alternative to well-known binary is well-known text, which is a textual representation of a geometry object. We have methods that work with well known text as well. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|This method returns a byte array that represents the shape in well-known binary.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|byteOrder|[`WkbByteOrder`](../ThinkGeo.Core/ThinkGeo.Core.WkbByteOrder.md)|This parameter is the byte order used to encode the well-known binary.|

---
#### `GetWellKnownBinary(RingOrder)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outerRingOrder|[`RingOrder`](../ThinkGeo.Core/ThinkGeo.Core.RingOrder.md)|N/A|

---
#### `GetWellKnownBinary(RingOrder,WkbByteOrder)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outerRingOrder|[`RingOrder`](../ThinkGeo.Core/ThinkGeo.Core.RingOrder.md)|N/A|
|byteOrder|[`WkbByteOrder`](../ThinkGeo.Core/ThinkGeo.Core.WkbByteOrder.md)|N/A|

---
#### `GetWellKnownText()`

**Summary**

   *This method returns the well-known text representation of this shape.*

**Remarks**

   *This method returns a string that represents the shape in well-known text. Well-known text allows you to describe geometries as a string of text. Well-known text is useful when you want to save a geometry in a format such as a text file, or when you simply want to cut and paste the text between other applications. An alternative to well-known text is well-known binary, which is a binary representation of a geometry object. We have methods that work with well-known binary as well. Below are some samples of what well-known text might look like for various kinds of geometries.POINT(5 17)LINESTRING(4 5,10 50,25 80)POLYGON((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3))MULTIPOINT(3.7 9.7,4.9 11.6)MULTILINESTRING((4 5,11 51,21 26),(-4 -7,-9 -7,-14 -3))MULTIPOLYGON(((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3)),((4 4,7 3,7 5,4 4))) As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string that represents the shape in well-known text.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWellKnownText(RingOrder)`

**Summary**

   *This method returns the well-known text representation of this shape.*

**Remarks**

   *This method returns a stringthat represents the shape in well-known text. Well-known text allows you to describe geometries as a string of text. Well-known text is useful when you want to save a geometry in a format such as a text file, or when you simply want to cut and paste the text between other applications. An alternative to well-known text is well-known binary, which is a binary representation of a geometry object. We have methods that work with well-known binary as well. Below are some samples of what well-known text might look like for various kinds of geometries.POINT(5 17)LINESTRING(4 5,10 50,25 80)POLYGON((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3))MULTIPOINT(3.7 9.7,4.9 11.6)MULTILINESTRING((4 5,11 51,21 26),(-4 -7,-9 -7,-14 -3))MULTIPOLYGON(((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3)),((4 4,7 3,7 5,4 4))) Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string that represents the shape in well-known text.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outerRingOrder|[`RingOrder`](../ThinkGeo.Core/ThinkGeo.Core.RingOrder.md)|N/A|

---
#### `GetWellKnownType()`

**Summary**

   *This method returns the well-known type for the shape.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|This method returns the well-known type for the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Intersects(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape have at least one point in common.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape have at least one point in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `Intersects(Feature)`

**Summary**

   *This method returns if the current shape and the targetFeature have at least one point in common.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetFeature have at least one point in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature you wish to compare the current one to.|

---
#### `IsDisjointed(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape have no points in common.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape have no points in common. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `IsDisjointed(Feature)`

**Summary**

   *This method returns if the current shape and the targetFeature have no points in common.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetFeature have no points in common. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you wish to compare the current one to.|

---
#### `IsTopologicallyEqual(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape are topologically equal.*

**Remarks**

   *Topologically equal means that the shapes are essentially the same. For example, let's say you have a line with two points, point A and point B. You also have another line that is made up of point A, point B and point C. Point A of line one shares the same vertex as point A of line two, and point B of line one shares the same vertex as point C of line two. They are both straight lines, so point B of line two would lie on the first line. Essentially the two lines are the same, with line 2 having just one extra point. Topologically they are the same line, so this method would return true. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape are topologically equal.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `IsTopologicallyEqual(Feature)`

**Summary**

   *This method returns if the current shape and the targetFeature are topologically equal.*

**Remarks**

   *Topologically equal means that the shapes are essentially the same. For example, let's say you have a line with two points, point A and point B. You also have another line that is made up of point A, point B and point C. Point A of line one shares the same vertex as point A of line two, and point B of line one shares the same vertex as point C of line two. They are both straight lines, so point B of line two would lie on the first line. Essentially the two lines are the same, with line 2 having just one extra point. Topologically they are the same line, so this method would return true. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetFeature are topologically equal.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `IsWithin(BaseShape)`

**Summary**

   *This method returns if the current shape lies within the interior of the targetShape.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape lies within the interior of the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `IsWithin(Feature)`

**Summary**

   *This method returns if the current shape lies within the interior of the targetFeature.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape lies within the interior of the targetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `LoadFromWellKnownData(String)`

**Summary**

   *This method hydrates the current shape with its data from well-known text.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wellKnownText|`String`|This parameter is the well-known text you will use to hydrate your object.|

---
#### `LoadFromWellKnownData(Byte[])`

**Summary**

   *This method hydrates the current shape with its data from well-known binary.*

**Remarks**

   *This is used when you want to hydrate a shape based on well-known binary. You can create the shape and then load the well-known binary using this method. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wellKnownBinary|`Byte[]`|This parameter is the well-known binary used to populate the shape.|

---
#### `Overlaps(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape share some but not all points in common.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape share some but not all points in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `Overlaps(Feature)`

**Summary**

   *This method returns if the current shape and the targetFeature share some but not all points in common.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetFeature share some but not all points in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `Register(PointShape,PointShape,DistanceUnit,GeographyUnit)`

**Summary**

   *This method returns a BaseShape which has been registered from its original coordinate system to another based on two anchor PointShapes.*

**Remarks**

   *Registering allows you to take a geometric shape generated in a planar system and attach it to the ground in a Geographic Unit.A common scenario is integrating geometric shapes from external programs (such as CAD software or a modeling system) and placing them onto a map. You may have the schematics of a building in a CAD system and the relationship between all the points of the building are in feet. You want to then take the CAD image and attach it to where it really exists on a map. You would use the register method to do this.Registering is also useful for scientific modeling, where software models things such as a plume of hazardous materials or the fallout from a volcano. The modeling software typically generates these models in a fictitious planar system. You would then use the register to take the abstract model and attach it to a map with real coordinates. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a BaseShape which has been registered from its original coordinate system to another based on two anchor PointShapes.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of origin.|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of destination.|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter is the DistanceUnit of the coordinate of origin.|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the GeographyUnit of the coordinate of destination.|

---
#### `Register(Feature,Feature,DistanceUnit,GeographyUnit)`

**Summary**

   *This method returns a BaseShape which has been registered from its original coordinate system to another based on two anchor PointShapes.*

**Remarks**

   *Registering allows you to take a geometric shape generated in a planar system and attach it to the ground in a Geographic Unit.A common scenario is integrating geometric shapes from external programs (such as CAD software or a modeling system) and placing them onto a map. You may have the schematics of a building in a CAD system and the relationship between all the points of the building are in feet. You want to then take the CAD image and attach it to where it really exists on a map. You would use the register method to do this.Registering is also useful for scientific modeling, where software models things such as a plume of hazardous materials or the fallout from a volcano. The modeling software typically generates these models in a fictitious planar system. You would then use the register to take the abstract model and attach it to a map with real coordinates. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a BaseShape which has been registered from its original coordinate system to another based on two anchor PointShapes.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the anchor PointFeature in the coordinate of origin.|
|toPoint|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the anchor PointFeature in the coordinate of destination.|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter is the DistanceUnit of the coordinate of origin.|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the GeographyUnit of the coordinate of destination.|

---
#### `Rotate(PointShape,Single)`

**Summary**

   *This method rotates a shape a number of degrees based on a pivot point.*

**Remarks**

   *This method returns a shape rotated by a number of degrees based on a pivot point. By placing the pivot point in the center of the shape you can achieve in-place rotation. By moving the pivot point outside of the center of the shape you can translate the shape in a circular motion. Moving the pivot point further outside of the center will make the circular area larger. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The pivotPoint represents the center of rotation.|
|degreeAngle|`Single`|The number of degrees of rotation required, from 0 to 360.|

---
#### `Rotate(BaseShape,PointShape,Single)`

**Summary**

   *This method returns a shape rotated by a number of degrees based on a pivot point.*

**Remarks**

   *This method returns a shape rotated by a number of degrees based on a pivot point. By placing the pivot point in the center of the shape you can achieve in-place rotation. By moving the pivot point outside of the center of the shape you can translate the shape in a circular motion. Moving the pivot point further outside of the center will make the circular area larger. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a shape rotated by a number of degrees based on a pivot point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceBaseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the basis for the rotation.|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The pivotPoint represents the center of rotation.|
|degreeAngle|`Single`|The number of degrees of rotation required, from 0 to 360.|

---
#### `Rotate(Feature,PointShape,Single)`

**Summary**

   *This method returns a feature rotated by a number of degrees based on a pivot point.*

**Remarks**

   *This method returns a feature rotated by a number of degrees based on a pivot point. By placing the pivot point in the center of the feature you can achieve in-place rotation. By moving the pivot point outside of the center of the feature you can translate the feature in a circular motion. Moving the pivot point further outside of the center will make the circular area larger. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a shape rotated by a number of degrees based on a pivot point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the basis for the rotation.|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The pivotPoint represents the center of rotation.|
|degreeAngle|`Single`|The number of degrees of rotation required, from 0 to 360.|

---
#### `ScaleTo(Double)`

**Summary**

   *This method increases or decreases the size of the shape by the specified scale factor given in the parameter.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is useful when you would like to increase or decrease the size of the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|Pepresents a value which scaleFactor to|

---
#### `ScaleTo(BaseShape,Double)`

**Summary**

   *This method increases or decreases the size of the shape by the specified scale factor given in the parameter.*

**Remarks**

   *It will call the instanced method ScaleTo internally.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method is useful when you would like to increase or decrease the size of the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|Represents a shape which you want to resize|
|scale|`Double`|Pepresents a value which scaleFactor to.|

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
#### `Touches(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape have at least one boundary point in common, but no interior points.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape have at least one boundary point in common, but no interior points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `Touches(Feature)`

**Summary**

   *This method returns of the current shape and the targetFeature have at least one boundary point in common, but no interior points.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns of the current shape and the targetFeature have at least one boundary point in common, but no interior points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature which contains a shape that you wish to compare the current one to.|

---
#### `TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves a base shape from one location to another based on a distance and a direction in degrees.*

**Remarks**

   *This method moves a base shape from one location to another based on angleInDegrees and distance parameter. With this overload, it is important to note that the distance is based on the supplied distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with a distanceUnit of miles, you're going to move this shape a number of miles based on the distance value and the angleInDegrees. In this way, you can easily move a shape in decimal degrees five miles to the north.If you pass a distance of 0, then the operation is ignored. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to move the shape using the angle specified. The distance unit will be the DistanceUnit specified in the distanceUnit parameter. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the shape, with 0 being up.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the measure of the translate. For example if you select miles as your distanceUnit then the distance will be calculated in miles.|

---
#### `TranslateByDegree(Double,Double)`

**Summary**

   *This method moves a base shape from one location to another based on a distance and a direction in degrees.*

**Remarks**

   *This method moves a base shape from one location to another based on angleInDegrees and distance parameter. With this overload, it is important to note that the distance is based on the supplied distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with a distanceUnit of miles, you're going to move this shape a number of miles based on the distance value and the angleInDegrees. In this way, you can easily move a shape in decimal degrees five miles to the north.If you pass a distance of 0, then the operation is ignored. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to move the shape using the angle specified. The distance unit will be the DistanceUnit specified in the distanceUnit parameter. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the shape, with 0 being up.|

---
#### `TranslateByDegree(BaseShape,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a shape repositioned from one location to another based on a distance and a direction in degrees.*

**Remarks**

   *This method returns a shape repositioned from one location to another based on angleInDegrees and distance parameter. With this overload, it is important to note that the distance is based on the supplied distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with a distanceUnit of miles, you're going to move this shape a number of miles based on the distance value and the angleInDegrees. In this way, you can easily move a shape in decimal degrees five miles to the north.If you pass a distance of 0, then the operation is ignored. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a shape repositioned from one location to another based on a distance and a direction in degrees.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the basis of the move.|
|distance|`Double`|The distance is the number of units to move the shape using the angle specified. The distance unit will be the one specified in the distanceUnit parameter. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the shape, with 0 being up.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the measure for the move. For example, if you select miles as your distance unit, then the distance will be calculated in miles.|

---
#### `TranslateByDegree(Feature,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a feature repositioned from one location to another, based on a distance and a direction in degrees.*

**Remarks**

   *This method returns a feature repositioned from one location to another based on angleInDegrees and distance parameter. With this overload, it is important to note that the distance is based on the supplied distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with a distanceUnit of miles, you're going to move this feature a number of miles based on the distance value and the angleInDegrees. In this way, you can easily move a shape in decimal degrees five miles to the north.If you pass a distance of 0, then the operation is ignored. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a feature repositioned from one location to another, based on a distance and a direction in degrees.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the basis of the move.|
|distance|`Double`|The distance is the number of units to move the shape using the angle specified. The distance unit will be the DistanceUnit specified in the distanceUnit parameter. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the feature, with 0 being up.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the measure for the move. For example, if you select miles as your distance unit, then the distance will be calculated in miles.|

---
#### `TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves a base shape from one location to another based on an X and Y offset distance.*

**Remarks**

   *This method moves a base shape from one location to another based on an X and Y offset distance. With this overload, it is important to note that the X and Y offset units are based on the distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this shape one unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you can easily move a shape in decimal degrees five miles on the X axis and 3 miles on the Y axis. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|xOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|yOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the base shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the measure for the move. For example, if you select miles as your distance unit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `TranslateByOffset(Double,Double)`

**Summary**

   *This method moves a base shape from one location to another based on an X and Y offset distance.*

**Remarks**

   *This method moves a base shape from one location to another based on an X and Y offset distance. With this overload, it is important to note that the X and Y offset units are based on the distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this shape one unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you can easily move a shape in decimal degrees five miles on the X axis and 3 miles on the Y axis. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|xOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|yOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|

---
#### `TranslateByOffset(BaseShape,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a shape repositioned from one location to another based on an X and Y offset distance.*

**Remarks**

   *This method returns a shape repositioned from one location to another based on an X and Y offset distance. With this overload, it is important to note that the X and Y offset units are based on the distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this shape one unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you can easily move a shape in decimal degrees five miles on the X axis and 3 miles on the Y axis. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a shape repositioned from one location to another based on an X and Y offset distance.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the basis of the moved shape.|
|xOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|yOffsetDistance|`Double`|This is the number of vertical units of movement in the distance unit specified in the distanceUnit parameter.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the measure for the move. For example, if you select miles as your distance unit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `TranslateByOffset(Feature,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a feature repositioned from one location to another based on an X and Y offset distance.*

**Remarks**

   *This method returns a feature repositioned from one location to another based on an X and Y offset distance. With this overload, it is important to note that the X and Y offset units are based on the distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this shape one unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you can easily move a shape in decimal degrees five miles on the X axis and 3 miles on the Y axis. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns a feature repositioned from one location to another based on an X and Y offset distance.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the basis of the moved feature.|
|xOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|yOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the measure for the move. For example, if you select miles as your distance unit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `Validate(ShapeValidationMode)`

**Summary**

   *This method returns a ShapeValidationResult based on a series of tests.*

**Remarks**

   *We use this method, with the simple enumeration, internally before doing any kind of other methods on the shape. In this way, we are able to verify the integrity of the shape itself. If you wish to test things such as whether a polygon self-intersects, we invite you to call this method with the advanced ShapeValidationMode. One thing to consider is that for complex polygon shapes this operation could take some time, which is why we only run the basic, faster test. If you are dealing with polygon shapes that are suspect, we suggest you run the advanced test. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`ShapeValidationResult`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationResult.md)|This method returns a ShapeValidationResult based on a series of tests.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|validationMode|[`ShapeValidationMode`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationMode.md)|This parameter determines whether the test is simple or advanced. In some cases, the advanced tests can take some time. The simple test is designed to always be fast.|

---

### Protected Methods

#### `BufferCore(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the area containing all of the points within a given distance from this shape.*

**Remarks**

   *This method computes the area containing all of the points within a given distance from this shape. In this case, you will be using the rounded RoundedBufferCapStyle and the default 8 quadrant segments. The distance unit is determined by the distanceUnit argument. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that represents all of the points within a given distance from the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to buffer the current shape. The distance unit will be the one specified in the distanceUnit parameter.|
|quadrantSegments|`Int32`|The number of quadrantSegments used in the buffer logic.|
|bufferCapType|[`BufferCapType`](../ThinkGeo.Core/ThinkGeo.Core.BufferCapType.md)|The bufferCapType used in the buffer logic.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the distance. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles for the operation.|

---
#### `CloneDeepCore()`

**Summary**

   *This method returns a complete copy of the shape without any references in common.*

**Remarks**

   *When you override this method, you need to ensure that there are no references in common between the original and copy.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a complete copy of the shape without any references in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ContainsCore(BaseShape)`

**Summary**

   *This method returns if the targetShape lies within the interior of the current shape.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the targetShape lies within the interior of the current shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `CrossesCore(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape share some but not all interior points.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape share some but not all interior points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

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
#### `GetBoundingBoxCore()`

**Summary**

   *This method calculates the smallest RectangleShape that encompasses the entire geometry.*

**Remarks**

   *The GetBoundingBox method calculates the smallest RectangleShape that can encompass the entire geometry by examining each point in the geometry. Depending on the number of PointShapes and complexity of the geometry, this operation can take longer for larger objects. If the shape is a PointShape, then the bounding box's upper left and lower right points will be equal. This will create a RectangleShape with no area. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The RectangleShape returned is the smallest RectangleShape that can encompass the entire geometry.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCenterPointCore()`

**Summary**

   *This method returns the center point of the current shape's bounding box.*

**Remarks**

   *This method returns the center point of the current shape's bounding box. It is important to note that this is the center point of the bounding box. There are numerous ways to calculate the "center" of a geometry, such as its weighted center, etc. You can find other centers by examining the various methods of the shape itself. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A PointShape representing the center point of the current shape's bounding box.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetClosestPointToCore(BaseShape,GeographyUnit)`

**Summary**

   *This method returns the point of the current shape that is closest to the target shape.*

**Remarks**

   *This method returns the point of the current shape that is closest to the target shape. It is often the case that the point returned is not a point of the object itself. An example would be a line with two points that are far apart from each other. If you set the targetShape to be a point midway between the points but a short distance away from the line, the method would return a point that is on the line but not either of the two points that make up the line. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A PointShape representing the closest point of the current shape to the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the closest point to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The geographic unit of the shape you are trying to find the closet point to.|

---
#### `GetCrossingCore(BaseShape)`

**Summary**

   *This method returns the crossing points between the current shape and the passed-in target shape.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipointShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipointShape.md)|This method returns the crossing points between the current shape and the passed-in target shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The target shape you wish to get crossing with.|

---
#### `GetDistanceToCore(BaseShape,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the distance between the current shape and the targetShape.*

**Remarks**

   *In this method we compute the closest distance between the two shapes. The returned unit will be in the unit of distance specified.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return type is the distance between this shape and the targetShape in the GeographyUnit of the shape. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|the geographic unit of the targetShape.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The returning distance unit.|

---
#### `GetGeoJsonCore()`

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
#### `GetShortestLineToCore(BaseShape,GeographyUnit)`

**Summary**

   *This method returns the shortest LineShape between this shape and the targetShape parameter.*

**Remarks**

   *This method returns a LineShape representing the shortest distance between the shape you're calling the method on and the targetShape. In some instances, based on the GeographicType or Projection, the line may not be straight. This is effect is similar to what you might see on an international flight when the displayed flight path is curved. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|A LineShape representing the shortest distance between the shape you're calling the method on and the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The geographic unit of the shape you are trying to find the distance to.|

---
#### `GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`

**Summary**

   *This method returns a byte array that represents the shape in well-known binary.*

**Remarks**

   *This method returns a byte array that represents the shape in well-known binary. Well-known binary allows you to describe geometries as a binary array. Well-known binary is useful when you want to save a geometry in an efficient format using as little space as possible. An alternative to well-known binary is well-known text, which is a textual representation of a geometry object. We have methods that work with well known text as well. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|This method returns a byte array that represents the shape in well-known binary.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outerRingOrder|[`RingOrder`](../ThinkGeo.Core/ThinkGeo.Core.RingOrder.md)|N/A|
|byteOrder|[`WkbByteOrder`](../ThinkGeo.Core/ThinkGeo.Core.WkbByteOrder.md)|This parameter specifies if the byte order is big- or little-endian.|

---
#### `GetWellKnownTextCore(RingOrder)`

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
|outerRingOrder|[`RingOrder`](../ThinkGeo.Core/ThinkGeo.Core.RingOrder.md)|N/A|

---
#### `GetWellKnownTypeCore()`

**Summary**

   *This method returns the well-known type for the shape.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|This method returns the well-known type for the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `IntersectsCore(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape have at least one point in common.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape have at least one point in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `IsDisjointedCore(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape have no points in common.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape have no points in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `IsTopologicallyEqualCore(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape are topologically equal.*

**Remarks**

   *Topologically equal means that the shapes are essentially the same. For example, let's say you have a line with two points, point A and point B. You also have another line that is made up of point A, point B and point C. Point A of line one shares the same vertex as point A of line two, and point B of line one shares the same vertex as point C of line two. They are both straight lines, so point B of line two would lie on the first line. Essentially the two lines are the same, with line 2 having just one extra point. Topologically they are the same line, so this method would return true. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape are topologically equal.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `IsWithinCore(BaseShape)`

**Summary**

   *This method returns if the current shape lies within the interior of the targetShape.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape lies within the interior of the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `LoadFromWellKnownDataCore(String)`

**Summary**

   *This method hydrates the current shape with its data from well-known text.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wellKnownText|`String`|This parameter is the well-known text you will use to hydrate your object.|

---
#### `LoadFromWellKnownDataCore(Byte[])`

**Summary**

   *This method hydrates the current shape with its data from well-known binary.*

**Remarks**

   *This is used when you want to hydrate a shape based on well-known binary. You can create the shape and then load the well-known binary using this method.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wellKnownBinary|`Byte[]`|This parameter is the well-known binary used to populate the shape.|

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
#### `OverlapsCore(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape share some but not all points in common.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape share some but not all points in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `RegisterCore(PointShape,PointShape,DistanceUnit,GeographyUnit)`

**Summary**

   *This method returns a BaseShape which has been registered from its original coordinate system to another based on two anchor PointShapes.*

**Remarks**

   *Registering allows you to take a geometric shape generated in a planar system and attach it to the ground in a Geographic Unit.A common scenario is integrating geometric shapes from external programs (such as CAD software or a modeling system) and placing them onto a map. You may have the schematics of a building in a CAD system and the relationship between all the points of the building are in feet. You want to then take the CAD image and attach it to where it really exists on a map. You would use the register method to do this.Registering is also useful for scientific modeling, where software models things such as a plume of hazardous materials or the fallout from a volcano. The modeling software typically generates these models in a fictitious planar system. You would then use the register to take the abstract model and attach it to a map with real coordinates. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a BaseShape which has been registered from its original coordinate system to another based on two anchor PointShapes.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of origin.|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of destination.|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter is the DistanceUnit of the coordinate of origin.|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the GeographyUnit of the coordinate of destination.|

---
#### `RotateCore(PointShape,Single)`

**Summary**

   *This method rotates a shape a number of degrees based on a pivot point.*

**Remarks**

   *This method rotates a shape by a number of degrees based on a pivot point. By placing the pivot point in the center of the shape you can achieve in-place rotation. By moving the pivot point outside of the center of the shape you can translate the shape in a circular motion. Moving the pivot point further outside of the center will make the circular area larger. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The pivotPoint represents the center of rotation.|
|degreeAngle|`Single`|The number of degrees of rotation required, from 0 to 360.|

---
#### `ScaleToCore(Double)`

**Summary**

   *This method increases or decreases the size of the shape by the specified scale factor given in the parameter.*

**Remarks**

   *This protected virtual method is called from the concrete public method ScaleTo. It does not take into account any transaction activity, as this is the responsibility of the concrete public method ScaleTo. This way, as a developer, if you choose to override this method you do not have to consider transactions at all.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is useful when you would like to increase or decrease the size of the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|Pepresents a value which scaleFactor to|

---
#### `TouchesCore(BaseShape)`

**Summary**

   *This method returns if the current shape and the targetShape have at least one boundary point in common, but no interior points.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current shape and the targetShape have at least one boundary point in common, but no interior points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

---
#### `TranslateByDegreeCore(Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves a base shape from one location to another based on a distance and a direction in degrees.*

**Remarks**

   *This method moves a base shape from one location to another based on angleInDegrees and distance parameter. With this overload, it is important to note that the distance is based on the supplied distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with a distanceUnit of miles, you're going to move this shape a number of miles based on the distance value and the angleInDegrees. In this way, you can easily move a shape in decimal degrees five miles to the north.If you pass a distance of 0 then the operation is ignored. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to move the shape using the angle specified. The distance unit will be the DistanceUnit specified in the distanceUnit parameter. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the shape, with 0 being up.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the measure of the translate. For example if you select miles as your distanceUnit then the distance will be calculated in miles.|

---
#### `TranslateByOffsetCore(Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves a base shape from one location to another based on an X and Y offset distance.*

**Remarks**

   *This method moves a base shape from one location to another based on an X and Y offset distance. With this overload, it is important to note that the X and Y offset units are based on the distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this shape one unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you can easily move a shape in decimal degrees five miles on the X axis and 3 miles on the Y axis. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|xOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|yOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the base shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the measure for the move. For example, if you select miles as your distance unit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `ValidateCore(ShapeValidationMode)`

**Summary**

   *This method returns a ShapeValidationResult based on a series of tests.*

**Remarks**

   *We use this method, with the simple enumeration, internally before doing any kind of other methods on the shape. In this way, we are able to verify the integrity of the shape itself. If you wish to test things such as whether a polygon self-intersects, we invite you to call this method with the advanced ShapeValidationMode. One thing to consider is that for complex polygon shapes this operation could take some time, which is why we only run the basic, faster test. If you are dealing with polygon shapes that are suspect, we suggest you run the advanced test. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`ShapeValidationResult`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationResult.md)|This method returns a ShapeValidationResult based on a series of tests.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|validationMode|[`ShapeValidationMode`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationMode.md)|This parameter determines whether the test is simple or advanced. In some cases, the advanced tests can take some time. The simple test is designed to always be fast.|

---

### Public Events


