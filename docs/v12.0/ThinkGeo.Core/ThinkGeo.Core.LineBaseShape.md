# LineBaseShape


## Inheritance Hierarchy

+ `Object`
  + [`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)
    + **`LineBaseShape`**
      + [`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)
      + [`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`LineBaseShape()`](#linebaseshape)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Id`](#id)|`String`|N/A|
|[`Tag`](#tag)|`Object`|N/A|

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
|[`ConvexHull()`](#convexhull)|
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
|[`GetIntersection(Feature)`](#getintersectionfeature)|
|[`GetIntersection(AreaBaseShape)`](#getintersectionareabaseshape)|
|[`GetLength(GeographyUnit,DistanceUnit)`](#getlengthgeographyunitdistanceunit)|
|[`GetLength(Int32,DistanceUnit)`](#getlengthint32distanceunit)|
|[`GetLength(String,DistanceUnit)`](#getlengthstringdistanceunit)|
|[`GetLength(Projection,DistanceUnit)`](#getlengthprojectiondistanceunit)|
|[`GetLength(Int32,DistanceUnit,DistanceCalculationMode)`](#getlengthint32distanceunitdistancecalculationmode)|
|[`GetLength(String,DistanceUnit,DistanceCalculationMode)`](#getlengthstringdistanceunitdistancecalculationmode)|
|[`GetLength(Projection,DistanceUnit,DistanceCalculationMode)`](#getlengthprojectiondistanceunitdistancecalculationmode)|
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
|[`ScaleDown(Double)`](#scaledowndouble)|
|[`ScaleDown(LineBaseShape,Double)`](#scaledownlinebaseshapedouble)|
|[`ScaleDown(Feature,Double)`](#scaledownfeaturedouble)|
|[`ScaleTo(Double)`](#scaletodouble)|
|[`ScaleUp(Double)`](#scaleupdouble)|
|[`ScaleUp(LineBaseShape,Double)`](#scaleuplinebaseshapedouble)|
|[`ScaleUp(Feature,Double)`](#scaleupfeaturedouble)|
|[`Simplify(GeographyUnit,Double,DistanceUnit,SimplificationType)`](#simplifygeographyunitdoubledistanceunitsimplificationtype)|
|[`Simplify(Double,SimplificationType)`](#simplifydoublesimplificationtype)|
|[`Simplify(LineBaseShape,GeographyUnit,Double,DistanceUnit,SimplificationType)`](#simplifylinebaseshapegeographyunitdoubledistanceunitsimplificationtype)|
|[`Simplify(LineBaseShape,Double,SimplificationType)`](#simplifylinebaseshapedoublesimplificationtype)|
|[`ToString()`](#tostring)|
|[`Touches(BaseShape)`](#touchesbaseshape)|
|[`Touches(Feature)`](#touchesfeature)|
|[`TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreedoubledoublegeographyunitdistanceunit)|
|[`TranslateByDegree(Double,Double)`](#translatebydegreedoubledouble)|
|[`TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetdoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Double,Double)`](#translatebyoffsetdoubledouble)|
|[`Union(LineBaseShape)`](#unionlinebaseshape)|
|[`Union(Feature)`](#unionfeature)|
|[`Union(IEnumerable<LineBaseShape>)`](#unionienumerable<linebaseshape>)|
|[`Union(IEnumerable<Feature>)`](#unionienumerable<feature>)|
|[`Validate(ShapeValidationMode)`](#validateshapevalidationmode)|

### Protected Methods Summary


|Name|
|---|
|[`BufferCore(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`](#buffercoredoubleint32buffercaptypegeographyunitdistanceunit)|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`ContainsCore(BaseShape)`](#containscorebaseshape)|
|[`ConvexHullCore()`](#convexhullcore)|
|[`CrossesCore(BaseShape)`](#crossescorebaseshape)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetCenterPointCore()`](#getcenterpointcore)|
|[`GetClosestPointToCore(BaseShape,GeographyUnit)`](#getclosestpointtocorebaseshapegeographyunit)|
|[`GetCrossingCore(BaseShape)`](#getcrossingcorebaseshape)|
|[`GetDistanceToCore(BaseShape,GeographyUnit,DistanceUnit)`](#getdistancetocorebaseshapegeographyunitdistanceunit)|
|[`GetGeoJsonCore()`](#getgeojsoncore)|
|[`GetIntersectionCore(AreaBaseShape)`](#getintersectioncoreareabaseshape)|
|[`GetLengthCore(GeographyUnit,DistanceUnit)`](#getlengthcoregeographyunitdistanceunit)|
|[`GetLengthCore(Projection,DistanceUnit,DistanceCalculationMode)`](#getlengthcoreprojectiondistanceunitdistancecalculationmode)|
|[`GetShortestLineToCore(BaseShape,GeographyUnit)`](#getshortestlinetocorebaseshapegeographyunit)|
|[`GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`](#getwellknownbinarycoreringorderwkbbyteorder)|
|[`GetWellKnownTextCore(RingOrder)`](#getwellknowntextcoreringorder)|
|[`GetWellKnownTypeCore()`](#getwellknowntypecore)|
|[`IntersectsCore(BaseShape)`](#intersectscorebaseshape)|
|[`IsDisjointedCore(BaseShape)`](#isdisjointedcorebaseshape)|
|[`IsPointBetweenVerteces(Vertex,Vertex,PointShape)`](#ispointbetweenvertecesvertexvertexpointshape)|
|[`IsTopologicallyEqualCore(BaseShape)`](#istopologicallyequalcorebaseshape)|
|[`IsWithinCore(BaseShape)`](#iswithincorebaseshape)|
|[`LoadFromWellKnownDataCore(String)`](#loadfromwellknowndatacorestring)|
|[`LoadFromWellKnownDataCore(Byte[])`](#loadfromwellknowndatacorebyte[])|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OverlapsCore(BaseShape)`](#overlapscorebaseshape)|
|[`RegisterCore(PointShape,PointShape,DistanceUnit,GeographyUnit)`](#registercorepointshapepointshapedistanceunitgeographyunit)|
|[`RotateCore(PointShape,Single)`](#rotatecorepointshapesingle)|
|[`ScaleDownCore(Double)`](#scaledowncoredouble)|
|[`ScaleToCore(Double)`](#scaletocoredouble)|
|[`ScaleUpCore(Double)`](#scaleupcoredouble)|
|[`SimplifyCore(Double,SimplificationType)`](#simplifycoredoublesimplificationtype)|
|[`TouchesCore(BaseShape)`](#touchescorebaseshape)|
|[`TranslateByDegreeCore(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreecoredoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffsetCore(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetcoredoubledoublegeographyunitdistanceunit)|
|[`UnionCore(IEnumerable<LineBaseShape>)`](#unioncoreienumerable<linebaseshape>)|
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

#### `LineBaseShape()`

**Summary**

   *This is the default constructor for AreaBaseShape.*

**Remarks**

   *This constructor simply calls the base constructor.*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Tag`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---

### Protected Properties


### Public Methods

#### `Buffer(Double,GeographyUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `Buffer(Double,Int32,GeographyUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|N/A|
|quadrantSegments|`Int32`|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `Buffer(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|N/A|
|quadrantSegments|`Int32`|N/A|
|bufferCapType|[`BufferCapType`](../ThinkGeo.Core/ThinkGeo.Core.BufferCapType.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `CloneDeep()`

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
|N/A|N/A|N/A|

---
#### `Contains(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `Contains(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `ConvexHull()`

**Summary**

   *This method returns the convex hull of the shape defined as the smallest convex ring that contains all the points in the shape.*

**Remarks**

   *This method is useful when you want to create a perimeter around the shape. For example if you had a MultiPolygon which represented buildings in a campus you could easily get the convex hull of the buildings and determine the perimeter of all of the buildings together. This also works with MultiPoint shapes where each point may represent a certain type of person you are doing statistics on. With convex hull you can get an idea of the regions those points are located in. As this is a concrete public method that wraps a Core method we reserve the right to add events and other logic to pre or post process data returned by the Core version of the method. In this way we leave our framework open on our end but also allow you the developer to extend our logic to your needs. If you have questions about this please contact support as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`RingShape`](../ThinkGeo.Core/ThinkGeo.Core.RingShape.md)|This method returns a RingShape defined as the smallest convex ring that contains all the points in the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Crosses(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `Crosses(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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
#### `GetCenterPoint()`

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
|N/A|N/A|N/A|

---
#### `GetClosestPointTo(BaseShape,GeographyUnit)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `GetClosestPointTo(Feature,GeographyUnit)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `GetCrossing(BaseShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipointShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipointShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

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
#### `GetDistanceTo(Feature,GeographyUnit,DistanceUnit)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetFeature()`

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
#### `GetIntersection(Feature)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `GetIntersection(AreaBaseShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetLength(GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the length of the line shape.*

**Remarks**

   *This is a useful method when you want to know the total length of a line-based shape. If the shape is a MultiLineShape, then the length is the sum of all of its lines. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This overload returns the length in the unit of your choice, based on the returningUnit parameter specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the return value. For example, if you select miles as your returningUnit, then the distance will be returned in miles.|

---
#### `GetLength(Int32,DistanceUnit)`

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
|shapeSrid|`Int32`|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetLength(String,DistanceUnit)`

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
|shapeProj4ProjectionParameters|`String`|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetLength(Projection,DistanceUnit)`

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
|shapeProjection|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetLength(Int32,DistanceUnit,DistanceCalculationMode)`

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
|shapeSrid|`Int32`|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

---
#### `GetLength(String,DistanceUnit,DistanceCalculationMode)`

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
|shapeProj4ProjectionParameters|`String`|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

---
#### `GetLength(Projection,DistanceUnit,DistanceCalculationMode)`

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
|shapeProjection|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

---
#### `GetShortestLineTo(BaseShape,GeographyUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `GetShortestLineTo(Feature,GeographyUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

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
|N/A|N/A|N/A|

---
#### `GetWellKnownBinary(WkbByteOrder)`

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
|byteOrder|[`WkbByteOrder`](../ThinkGeo.Core/ThinkGeo.Core.WkbByteOrder.md)|N/A|

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
#### `GetWellKnownText(RingOrder)`

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
#### `GetWellKnownType()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Intersects(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `Intersects(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `IsDisjointed(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `IsDisjointed(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `IsTopologicallyEqual(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `IsTopologicallyEqual(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `IsWithin(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `IsWithin(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `LoadFromWellKnownData(String)`

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
|wellKnownText|`String`|N/A|

---
#### `LoadFromWellKnownData(Byte[])`

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
|wellKnownBinary|`Byte[]`|N/A|

---
#### `Overlaps(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `Overlaps(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `Register(PointShape,PointShape,DistanceUnit,GeographyUnit)`

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
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `Register(Feature,Feature,DistanceUnit,GeographyUnit)`

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
|fromPoint|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|toPoint|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

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
#### `ScaleDown(Double)`

**Summary**

   *This method decreases the size of the LineShape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster as you apply the operation multiple times. There is also a ScaleUp method that will enlarge the shape as well. As this is a concrete public method that wraps a Core method we reserve the right to add events and other logic to pre or post process data returned by the Core version of the method. In this way we leave our framework open on our end but also allow you the developer to extend our logic to your needs. If you have questions about this please contact support as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to decrease the shape�s size.|

---
#### `ScaleDown(LineBaseShape,Double)`

**Summary**

   *This method returns a new shape that is decreases by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster as you apply the operation multiple times. There is also a ScaleUp method that will enlarge the shape as well. As this is a concrete public method that wraps a Core method we reserve the right to add events and other logic to pre or post process data returned by the Core version of the method. In this way we leave our framework open on our end but also allow you the developer to extend our logic to your needs. If you have questions about this please contact support as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|a scaled down line type shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceLineBaseShape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This parameter is the basis for the scale up up but is not modified.|
|percentage|`Double`|This is the percentage by which to decrease the shape�s size.|

---
#### `ScaleDown(Feature,Double)`

**Summary**

   *This method returns a new feature that is decreases by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the feature. Note that a larger percentage will scale the shape down faster as you apply the operation multiple times. There is also a ScaleUp method that will enlarge the shape as well. As this is a concrete public method that wraps a Core method we reserve the right to add events and other logic to pre or post process data returned by the Core version of the method. In this way we leave our framework open on our end but also allow you the developer to extend our logic to your needs. If you have questions about this please contact support as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|a scaled down line type feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceLine|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the basis for the scale up up but is not modified.|
|percentage|`Double`|This is the percentage by which to decrease the shape�s size.|

---
#### `ScaleTo(Double)`

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
|scale|`Double`|N/A|

---
#### `ScaleUp(Double)`

**Summary**

   *This method increases the size of the LineShape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster as you apply the operation multiple times. There is also a ScaleDown method that will shrink the shape as well. As this is a concrete public method that wraps a Core method we reserve the right to add events and other logic to pre or post process data returned by the Core version of the method. In this way we leave our framework open on our end but also allow you the developer to extend our logic to your needs. If you have questions about this please contact support as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to increase the shape�s size.|

---
#### `ScaleUp(LineBaseShape,Double)`

**Summary**

   *This method returns a new shape that is increased by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster as you apply the operation multiple times. There is also a ScaleDown method that will shrink the shape as well. As this is a concrete public method that wraps a Core method we reserve the right to add events and other logic to pre or post process data returned by the Core version of the method. In this way we leave our framework open on our end but also allow you the developer to extend our logic to your needs. If you have questions about this please contact support as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|a scaled line type shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceShape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This parameter is the basis for the scale up up but is not modified.|
|percentage|`Double`|This is the percentage by which to increase the shape�s size.|

---
#### `ScaleUp(Feature,Double)`

**Summary**

   *This method returns a new feature that is increased by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the feature. Note that a larger percentage will scale the shape up faster as you apply the operation multiple times. There is also a ScaleDown method that will shrink the shape as well. As this is a concrete public method that wraps a Core method we reserve the right to add events and other logic to pre or post process data returned by the Core version of the method. In this way we leave our framework open on our end but also allow you the developer to extend our logic to your needs. If you have questions about this please contact support as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|a scaled line type feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceLine|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the basis for the scale up up but is not modified.|
|percentage|`Double`|This is the percentage by which to increase the shape�s size.|

---
#### `Simplify(GeographyUnit,Double,DistanceUnit,SimplificationType)`

**Summary**

   *Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|the geography unit of the target shape|
|tolerance|`Double`|distance tolerance|
|toleranceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|the distance unit of tolerance|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|Specifies which algorthm will be use to simplify.|

---
#### `Simplify(Double,SimplificationType)`

**Summary**

   *Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tolerance|`Double`|distance tolerance|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|Specifies which algorthm will be use to simplify.|

---
#### `Simplify(LineBaseShape,GeographyUnit,Double,DistanceUnit,SimplificationType)`

**Summary**

   *Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|target shape which will be simplified.|
|targetShapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|the geography unit of the target shape|
|tolerance|`Double`|distance tolerance|
|toleranceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|the distance unit of tolerance|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|Specifies which algorthm will be use to simplify.|

---
#### `Simplify(LineBaseShape,Double,SimplificationType)`

**Summary**

   *Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|target shape which will be simplified.|
|tolerance|`Double`|distance tolerance|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|Specifies which algorthm will be use to simplify.|

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `Touches(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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
#### `TranslateByDegree(Double,Double)`

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
|xOffsetDistance|`Double`|N/A|
|yOffsetDistance|`Double`|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `TranslateByOffset(Double,Double)`

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
|xOffsetDistance|`Double`|N/A|
|yOffsetDistance|`Double`|N/A|

---
#### `Union(LineBaseShape)`

**Summary**

   *Calculates a new geometry that contains all the points in this LineBaseShape and input LineBaseShape*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|A set combining the points of this LineBaseShape and the points of input LineBaseShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|The target LineBasheShape with which to compute the union|

---
#### `Union(Feature)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `Union(IEnumerable<LineBaseShape>)`

**Summary**

   *Calculates a new geometry that contains all the points in this LineBaseShape and input LineBaseShape set.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|A set combining the points of this LineBaseShape and the points of input LineBaseShape set.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|lineBaseShapes|IEnumerable<[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)>|N/A|

---
#### `Union(IEnumerable<Feature>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeatures|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `Validate(ShapeValidationMode)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ShapeValidationResult`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|validationMode|[`ShapeValidationMode`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationMode.md)|N/A|

---

### Protected Methods

#### `BufferCore(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|N/A|
|quadrantSegments|`Int32`|N/A|
|bufferCapType|[`BufferCapType`](../ThinkGeo.Core/ThinkGeo.Core.BufferCapType.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `CloneDeepCore()`

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
|N/A|N/A|N/A|

---
#### `ContainsCore(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `ConvexHullCore()`

**Summary**

   *This method returns the convex hull of the shape defined as the smallest convex ring that contains all the points in the shape.*

**Remarks**

   *This method is useful when you want to create a perimeter around the shape. For example, if you had a MultiPolygon that represented buildings on a campus, you could easily get the convex hull of the buildings and determine the perimeter of all of the buildings together. This also works with MultiPoint shapes, where each point may represent a certain type of person you are doing statistics on. With convex hull, you can get an idea of the regions those points are located in.*

**Return Value**

|Type|Description|
|---|---|
|[`RingShape`](../ThinkGeo.Core/ThinkGeo.Core.RingShape.md)|This method returns a RingShape defined as the smallest convex ring that contains all the points in the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CrossesCore(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

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
#### `GetCenterPointCore()`

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
|N/A|N/A|N/A|

---
#### `GetClosestPointToCore(BaseShape,GeographyUnit)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `GetCrossingCore(BaseShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipointShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipointShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `GetDistanceToCore(BaseShape,GeographyUnit,DistanceUnit)`

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
#### `GetIntersectionCore(AreaBaseShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetLengthCore(GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the length of the line shape.*

**Remarks**

   *This is a useful method when you want to know the total length of a line-based shape. If the shape is a MultiLineShape, then the length is the sum of all of its lines. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This overload returns the length in the unit of your choice, based on the returningUnit parameter specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the return value. For example, if you select miles as your returningUnit, then the distance will be returned in miles.|

---
#### `GetLengthCore(Projection,DistanceUnit,DistanceCalculationMode)`

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
|shapeProjection|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

---
#### `GetShortestLineToCore(BaseShape,GeographyUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `IntersectsCore(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `IsDisjointedCore(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `IsPointBetweenVerteces(Vertex,Vertex,PointShape)`

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
|vertex1|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|vertex2|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|N/A|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `IsTopologicallyEqualCore(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `IsWithinCore(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `LoadFromWellKnownDataCore(String)`

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
|wellKnownText|`String`|N/A|

---
#### `LoadFromWellKnownDataCore(Byte[])`

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
|wellKnownBinary|`Byte[]`|N/A|

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `RegisterCore(PointShape,PointShape,DistanceUnit,GeographyUnit)`

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
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `RotateCore(PointShape,Single)`

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
#### `ScaleDownCore(Double)`

**Summary**

   *This method decreases the size of the LineShape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster as you apply the operation multiple times. There is also a ScaleUp method that will enlarge the shape as well. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to decrease the shape�s size.|

---
#### `ScaleToCore(Double)`

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
|scale|`Double`|N/A|

---
#### `ScaleUpCore(Double)`

**Summary**

   *This method increases the size of the LineShape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster as you apply the operation multiple times. There is also a ScaleDown method that will shrink the shape as well. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to increase the shape�s size.|

---
#### `SimplifyCore(Double,SimplificationType)`

**Summary**

   *Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|Simplify the LineBaseShape to MultilineShape depends on distance tolerance and different SimplificationType.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tolerance|`Double`|distance tolerance|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|Specifies which algorthm will be use to simplify.|

---
#### `TouchesCore(BaseShape)`

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
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `TranslateByDegreeCore(Double,Double,GeographyUnit,DistanceUnit)`

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
#### `TranslateByOffsetCore(Double,Double,GeographyUnit,DistanceUnit)`

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
|xOffsetDistance|`Double`|N/A|
|yOffsetDistance|`Double`|N/A|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `UnionCore(IEnumerable<LineBaseShape>)`

**Summary**

   *The protected virtual method used by "Union" that you can overwrite to implement your own logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|A set combining the points of this LineBaseShape and the points of input LineBaseShape set.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|lineBaseShapes|IEnumerable<[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)>|The target LineBasheShape set with which to compute the union|

---
#### `ValidateCore(ShapeValidationMode)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ShapeValidationResult`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|validationMode|[`ShapeValidationMode`](../ThinkGeo.Core/ThinkGeo.Core.ShapeValidationMode.md)|N/A|

---

### Public Events


