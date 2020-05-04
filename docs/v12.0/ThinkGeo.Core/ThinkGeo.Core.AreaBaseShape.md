# AreaBaseShape


## Inheritance Hierarchy

+ `Object`
  + [`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)
    + **`AreaBaseShape`**
      + [`EllipseShape`](../ThinkGeo.Core/ThinkGeo.Core.EllipseShape.md)
      + [`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)
      + [`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)
      + [`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)
      + [`RingShape`](../ThinkGeo.Core/ThinkGeo.Core.RingShape.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`AreaBaseShape()`](#areabaseshape)|

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
|[`Crosses(BaseShape)`](#crossesbaseshape)|
|[`Crosses(Feature)`](#crossesfeature)|
|[`Equals(Object)`](#equalsobject)|
|[`GetArea(GeographyUnit,AreaUnit)`](#getareageographyunitareaunit)|
|[`GetArea(Int32,AreaUnit)`](#getareaint32areaunit)|
|[`GetArea(String,AreaUnit)`](#getareastringareaunit)|
|[`GetArea(Projection,AreaUnit)`](#getareaprojectionareaunit)|
|[`GetArea(Int32,AreaUnit,DistanceCalculationMode)`](#getareaint32areaunitdistancecalculationmode)|
|[`GetArea(String,AreaUnit,DistanceCalculationMode)`](#getareastringareaunitdistancecalculationmode)|
|[`GetArea(Projection,AreaUnit,DistanceCalculationMode)`](#getareaprojectionareaunitdistancecalculationmode)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetCenterPoint()`](#getcenterpoint)|
|[`GetClosestPointTo(BaseShape,GeographyUnit)`](#getclosestpointtobaseshapegeographyunit)|
|[`GetClosestPointTo(Feature,GeographyUnit)`](#getclosestpointtofeaturegeographyunit)|
|[`GetConvexHull()`](#getconvexhull)|
|[`GetCrossing(BaseShape)`](#getcrossingbaseshape)|
|[`GetDifference(AreaBaseShape)`](#getdifferenceareabaseshape)|
|[`GetDifference(Feature)`](#getdifferencefeature)|
|[`GetDistanceTo(BaseShape,GeographyUnit,DistanceUnit)`](#getdistancetobaseshapegeographyunitdistanceunit)|
|[`GetDistanceTo(Feature,GeographyUnit,DistanceUnit)`](#getdistancetofeaturegeographyunitdistanceunit)|
|[`GetFeature()`](#getfeature)|
|[`GetFeature(IDictionary<String,String>)`](#getfeatureidictionary<stringstring>)|
|[`GetGeoJson()`](#getgeojson)|
|[`GetHashCode()`](#gethashcode)|
|[`GetIntersection(AreaBaseShape)`](#getintersectionareabaseshape)|
|[`GetIntersection(Feature)`](#getintersectionfeature)|
|[`GetPerimeter(GeographyUnit,DistanceUnit)`](#getperimetergeographyunitdistanceunit)|
|[`GetPerimeter(Int32,DistanceUnit)`](#getperimeterint32distanceunit)|
|[`GetPerimeter(String,DistanceUnit)`](#getperimeterstringdistanceunit)|
|[`GetPerimeter(Projection,DistanceUnit)`](#getperimeterprojectiondistanceunit)|
|[`GetPerimeter(Int32,DistanceUnit,DistanceCalculationMode)`](#getperimeterint32distanceunitdistancecalculationmode)|
|[`GetPerimeter(String,DistanceUnit,DistanceCalculationMode)`](#getperimeterstringdistanceunitdistancecalculationmode)|
|[`GetPerimeter(Projection,DistanceUnit,DistanceCalculationMode)`](#getperimeterprojectiondistanceunitdistancecalculationmode)|
|[`GetShortestLineTo(BaseShape,GeographyUnit)`](#getshortestlinetobaseshapegeographyunit)|
|[`GetShortestLineTo(Feature,GeographyUnit)`](#getshortestlinetofeaturegeographyunit)|
|[`GetSymmetricalDifference(AreaBaseShape)`](#getsymmetricaldifferenceareabaseshape)|
|[`GetSymmetricalDifference(Feature)`](#getsymmetricaldifferencefeature)|
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
|[`ScaleDown(AreaBaseShape,Double)`](#scaledownareabaseshapedouble)|
|[`ScaleDown(Feature,Double)`](#scaledownfeaturedouble)|
|[`ScaleTo(Double)`](#scaletodouble)|
|[`ScaleUp(Double)`](#scaleupdouble)|
|[`ScaleUp(AreaBaseShape,Double)`](#scaleupareabaseshapedouble)|
|[`ScaleUp(Feature,Double)`](#scaleupfeaturedouble)|
|[`Simplify(GeographyUnit,Double,DistanceUnit,SimplificationType)`](#simplifygeographyunitdoubledistanceunitsimplificationtype)|
|[`Simplify(Double,SimplificationType)`](#simplifydoublesimplificationtype)|
|[`Simplify(AreaBaseShape,GeographyUnit,Double,DistanceUnit,SimplificationType)`](#simplifyareabaseshapegeographyunitdoubledistanceunitsimplificationtype)|
|[`Simplify(AreaBaseShape,Double,SimplificationType)`](#simplifyareabaseshapedoublesimplificationtype)|
|[`Split(AreaBaseShape,AreaBaseShape)`](#splitareabaseshapeareabaseshape)|
|[`Split(Feature,Feature)`](#splitfeaturefeature)|
|[`ToString()`](#tostring)|
|[`Touches(BaseShape)`](#touchesbaseshape)|
|[`Touches(Feature)`](#touchesfeature)|
|[`TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreedoubledoublegeographyunitdistanceunit)|
|[`TranslateByDegree(Double,Double)`](#translatebydegreedoubledouble)|
|[`TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetdoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Double,Double)`](#translatebyoffsetdoubledouble)|
|[`Union(AreaBaseShape)`](#unionareabaseshape)|
|[`Union(Feature)`](#unionfeature)|
|[`Union(IEnumerable<Feature>)`](#unionienumerable<feature>)|
|[`Union(IEnumerable<AreaBaseShape>)`](#unionienumerable<areabaseshape>)|
|[`Validate(ShapeValidationMode)`](#validateshapevalidationmode)|

### Protected Methods Summary


|Name|
|---|
|[`BufferCore(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`](#buffercoredoubleint32buffercaptypegeographyunitdistanceunit)|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`ContainsCore(BaseShape)`](#containscorebaseshape)|
|[`CrossesCore(BaseShape)`](#crossescorebaseshape)|
|[`Finalize()`](#finalize)|
|[`GetAreaCore(GeographyUnit,AreaUnit)`](#getareacoregeographyunitareaunit)|
|[`GetAreaCore(Projection,AreaUnit,DistanceCalculationMode)`](#getareacoreprojectionareaunitdistancecalculationmode)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetCenterPointCore()`](#getcenterpointcore)|
|[`GetClosestPointToCore(BaseShape,GeographyUnit)`](#getclosestpointtocorebaseshapegeographyunit)|
|[`GetConvexHullCore()`](#getconvexhullcore)|
|[`GetCrossingCore(BaseShape)`](#getcrossingcorebaseshape)|
|[`GetDifferenceCore(AreaBaseShape)`](#getdifferencecoreareabaseshape)|
|[`GetDistanceToCore(BaseShape,GeographyUnit,DistanceUnit)`](#getdistancetocorebaseshapegeographyunitdistanceunit)|
|[`GetGeoJsonCore()`](#getgeojsoncore)|
|[`GetIntersectionCore(AreaBaseShape)`](#getintersectioncoreareabaseshape)|
|[`GetPerimeterCore(GeographyUnit,DistanceUnit)`](#getperimetercoregeographyunitdistanceunit)|
|[`GetPerimeterCore(Projection,DistanceUnit,DistanceCalculationMode)`](#getperimetercoreprojectiondistanceunitdistancecalculationmode)|
|[`GetShortestLineToCore(BaseShape,GeographyUnit)`](#getshortestlinetocorebaseshapegeographyunit)|
|[`GetSymmetricalDifferenceCore(AreaBaseShape)`](#getsymmetricaldifferencecoreareabaseshape)|
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
|[`ScaleDownCore(Double)`](#scaledowncoredouble)|
|[`ScaleToCore(Double)`](#scaletocoredouble)|
|[`ScaleUpCore(Double)`](#scaleupcoredouble)|
|[`SimplifyCore(Double,SimplificationType)`](#simplifycoredoublesimplificationtype)|
|[`TouchesCore(BaseShape)`](#touchescorebaseshape)|
|[`TranslateByDegreeCore(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreecoredoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffsetCore(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetcoredoubledoublegeographyunitdistanceunit)|
|[`UnionCore(AreaBaseShape)`](#unioncoreareabaseshape)|
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

#### `AreaBaseShape()`

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
#### `GetArea(GeographyUnit,AreaUnit)`

**Summary**

   *This method returns the area of the shape, defined as the size of the region enclosed by the figure.*

**Remarks**

   *You would use this method to find the area inside the shape.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return unit is based on a AreaUnit you specify in the returningUnit parameter, regardless of the shape's GeographyUnit.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|This is the AreaUnit you would like to use as the return value. For example, if you select square miles as your returningUnit, then the distance will be returned in square miles.|

---
#### `GetArea(Int32,AreaUnit)`

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
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|

---
#### `GetArea(String,AreaUnit)`

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
|shapeProjString|`String`|N/A|
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|

---
#### `GetArea(Projection,AreaUnit)`

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
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|

---
#### `GetArea(Int32,AreaUnit,DistanceCalculationMode)`

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
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

---
#### `GetArea(String,AreaUnit,DistanceCalculationMode)`

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
|shapeProjString|`String`|N/A|
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

---
#### `GetArea(Projection,AreaUnit,DistanceCalculationMode)`

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
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

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
#### `GetConvexHull()`

**Summary**

   *This method returns the convex hull of the shape, defined as the smallest convex ring that contains all of the points in the shape.*

**Remarks**

   *This method is useful when you want to create a perimeter around the shape. For example, if you had a MultiPolygon that represented buildings on a campus, you could easily get the convex hull of the buildings and determine the perimeter of all of the buildings together. This also works with MultiPoint shapes, where each point may represent a certain type of person you are doing statistics on. With convex hull, you can get an idea of the regions those points are located in.*

**Return Value**

|Type|Description|
|---|---|
|[`RingShape`](../ThinkGeo.Core/ThinkGeo.Core.RingShape.md)|This method returns the convex hull of the shape, defined as the smallest convex ring that contains all of the points in the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `GetDifference(AreaBaseShape)`

**Summary**

   *This method returns the difference between two shapes, defined as the set of all points which lie in the current shape but not in the targetShape.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that is the set of all points which lie in the current shape but not in the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the difference with.|

---
#### `GetDifference(Feature)`

**Summary**

   *This method returns the difference between current shape and the specified feature, defined as the set of all points which lie in the current shape but not in the targetShape.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that is the set of all points which lie in the current shape but not in the target feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feture you are trying to find the difference with.|

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
#### `GetIntersection(AreaBaseShape)`

**Summary**

   *This method returns the intersection of the current shape and the target shape, defined as the set of all points which lie in both the current shape and the target shape.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points which lie in both the current shape and the target shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the intersection with.|

---
#### `GetIntersection(Feature)`

**Summary**

   *This method returns the intersection of the current shape and the target feature, defined as the set of all points which lie in both the current shape and the target feature.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points which lie in both the current shape and the target feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the intersection with.|

---
#### `GetPerimeter(GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the perimeter of the shape, defined as the sum of the lengths of all its sides.*

**Remarks**

   *You would use this method to find the distance around the area shape.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return unit is based on a LengthUnit you specify in the returningUnit parameter, regardless of the shape's GeographyUnit.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the return value. For example, if you select miles as your returningUnit, then the distance will be returned in miles.|

---
#### `GetPerimeter(Int32,DistanceUnit)`

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
#### `GetPerimeter(String,DistanceUnit)`

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
|shapeProjString|`String`|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetPerimeter(Projection,DistanceUnit)`

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
#### `GetPerimeter(Int32,DistanceUnit,DistanceCalculationMode)`

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
#### `GetPerimeter(String,DistanceUnit,DistanceCalculationMode)`

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
|shapeProjString|`String`|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

---
#### `GetPerimeter(Projection,DistanceUnit,DistanceCalculationMode)`

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
#### `GetSymmetricalDifference(AreaBaseShape)`

**Summary**

   *This method returns the symmetrical difference between two shapes, defined as the set of all points which lie in the current shape or the targetShape but not both.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that is the set of all points which lie in the current shape or the targetShape but not both.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the symmetrical difference with.|

---
#### `GetSymmetricalDifference(Feature)`

**Summary**

   *This method returns the symmetrical difference between current shape and the specified feature, defined as the set of all points which lie in the current shape or the targetFeature but not both.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that is the set of all points which lie in the current shape or the targetFeature but not both.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the symmetrical difference with.|

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

   *This method decreases the size of the area shape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster, since you apply the operation multiple times. There is a ScaleUp method that will enlarge the shape as well.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster, since you apply the operation multiple times. There is a ScaleUp method that will enlarge the shape as well.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to decrease the shape's size.|

---
#### `ScaleDown(AreaBaseShape,Double)`

**Summary**

   *This method returns a new area shape that has been scaled down by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster, since you apply the operation multiple times. There is a ScaleUp method that will enlarge the shape as well.*

**Return Value**

|Type|Description|
|---|---|
|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster, since you apply the operation multiple times. There is a ScaleUp method that will enlarge the shape as well.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter is the shape to use as the base for the scaling.|
|percentage|`Double`|This is the percentage by which to decrease the shape's size.|

---
#### `ScaleDown(Feature,Double)`

**Summary**

   *This method returns a new area feature that has been scaled down by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the feature. Note that a larger percentage will scale the shape down faster, since you apply the operation multiple times. There is a ScaleUp method that will enlarge the shape as well.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method is useful when you would like to decrease the size of the feature. Note that a larger percentage will scale the shape down faster, since you apply the operation multiple times. There is a ScaleUp method that will enlarge the shape as well.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the shape to use as the base for the scaling.|
|percentage|`Double`|This is the percentage by which to decrease the shape's size.|

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

   *This method increases the size of the area shape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster, since you apply the operation multiple times. There is a ScaleDown method that will shrink the shape. as well.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to increase the shape's size.|

---
#### `ScaleUp(AreaBaseShape,Double)`

**Summary**

   *This method returns a new area shape that has been scaled up by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster, since you apply the operation multiple times. There is a ScaleDown method that will shrink the shape as well.*

**Return Value**

|Type|Description|
|---|---|
|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter is the shape to use as the base for the scaling.|
|percentage|`Double`|This is the percentage by which to increase the shape's size.|

---
#### `ScaleUp(Feature,Double)`

**Summary**

   *This method returns a new area shape that has been scaled up by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster, since you apply the operation multiple times. There is a ScaleDown method that will shrink the shape as well.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature to use as the base for the scaling.|
|percentage|`Double`|This is the percentage by which to increase the shape's size.|

---
#### `Simplify(GeographyUnit,Double,DistanceUnit,SimplificationType)`

**Summary**

   *This method performed a simplification operation based on the parameters passed in. Simplify permanently alters the input geometry so that the geometry becomes topologically consistent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|This method returns a simplification multipolgyon by the specified parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the geographic unit of this current shape you are performing the operation|
|tolerance|`Double`|This parameter specifes the tolerance to be used when simplification.|
|toleranceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifes the distance unit of the tolerance.|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|This prameter specifies the type of simplification operation.|

---
#### `Simplify(Double,SimplificationType)`

**Summary**

   *This method performed a simplification operation based on the parameters passed in. Simplify permanently alters the input geometry so that the geometry becomes topologically consistent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|This method returns a simplification multipolgyon by the specified parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tolerance|`Double`|This parameter specifes the tolerance to be used when simplification.|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|This prameter specifies the type of simplification operation.|

---
#### `Simplify(AreaBaseShape,GeographyUnit,Double,DistanceUnit,SimplificationType)`

**Summary**

   *This method performed a simplification operation based on the parameters passed in. Simplify permanently alters the input geometry so that the geometry becomes topologically consistent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|This method returns a simplification multipolgyon by the specified parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter specifies the area shape to be simplfied.|
|targetShapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the geographic unit of the shape you are performing the operation|
|tolerance|`Double`|This parameter specifes the tolerance to be used when simplification.|
|toleranceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifes the distance unit of the tolerance.|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|This prameter specifies the type of simplification operation.|

---
#### `Simplify(AreaBaseShape,Double,SimplificationType)`

**Summary**

   *This method performed a simplification operation based on the parameters passed in. Simplify permanently alters the input geometry so that the geometry becomes topologically consistent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|This method returns a simplification multipolgyon by the specified parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter specifies the area shape to be simplfied.|
|tolerance|`Double`|This parameter specifes the tolerance to be used when simplification.|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|This prameter specifies the type of simplification operation.|

---
#### `Split(AreaBaseShape,AreaBaseShape)`

**Summary**

   *This method returns a collection of MultiPolygonShapes split by the specified parameters.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)>|This method returns a collection of MultiPolygonShape split by the specified parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|areaToSplit|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter represents the shape to be split.|
|areaToSplitBy|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter represents the shape that will be used to perform the split.|

---
#### `Split(Feature,Feature)`

**Summary**

   *This method returns a collection of Features split by the specified parameters.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of Features split by the specified parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|areaToSplit|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter represents the feature to be split.|
|areaToSplitBy|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter represents the feature that will be used to perform the split.|

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
#### `Union(AreaBaseShape)`

**Summary**

   *This method returns the union of the current shape and the target shape, defined as the set of all points in the current shape or the target shape.*

**Remarks**

   *This is useful for adding area shapes together to form a larger area shape.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points which lie in the current shape or the target shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the union with.|

---
#### `Union(Feature)`

**Summary**

   *This method returns the union of the current shape and the target feature, defined as the set of all points in the current shape or the target feature.*

**Remarks**

   *This is useful for adding area shapes together to form a larger area shape.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points which lie in the current shape or the target feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the union with.|

---
#### `Union(IEnumerable<Feature>)`

**Summary**

   *This method returns the union of the current shape and the target features, defined as the set of all points in the current shape or the target features.*

**Remarks**

   *This is useful for adding area shapes together to form a larger area shape. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points which lie in the current shape or the target features. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeatures|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The target features you are trying to find the union with.|

---
#### `Union(IEnumerable<AreaBaseShape>)`

**Summary**

   *This method returns the union of the specified area shapes.*

**Remarks**

   *This is useful for adding area shapes together to form a larger area shape.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points that lie within the shapes you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|areaShapes|IEnumerable<[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)>|The shapes you are trying to find the union with.|

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
#### `GetAreaCore(GeographyUnit,AreaUnit)`

**Summary**

   *This method returns the area of the shape, defined as the size of the region enclosed by the figure.*

**Remarks**

   *You would use this method to find the area inside the shape. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return unit is based on a AreaUnit you specify in the returningUnit parameter, regardless of the shape's GeographyUnit.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|This is the AreaUnit you would like to use as the return value. For example, if you select square miles as your returningUnit, then the distance will be returned in square miles.|

---
#### `GetAreaCore(Projection,AreaUnit,DistanceCalculationMode)`

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
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|
|distanceCalculationMode|[`DistanceCalculationMode`](../ThinkGeo.Core/ThinkGeo.Core.DistanceCalculationMode.md)|N/A|

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
#### `GetConvexHullCore()`

**Summary**

   *This method returns the convex hull of the shape, defined as the smallest convex ring that contains all of the points in the shape.*

**Remarks**

   *This method is useful when you want to create a perimeter around the shape. For example, if you had a MultiPolygon that represented buildings on a campus, you could easily get the convex hull of the buildings and determine the perimeter of all of the buildings together. This also works with MultiPoint shapes, where each point may represent a certain type of person you are doing statistics on. With convex hull, you can get an idea of the regions those points are located in. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`RingShape`](../ThinkGeo.Core/ThinkGeo.Core.RingShape.md)|This method returns the convex hull of the shape, defined as the smallest convex ring that contains all of the points in the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `GetDifferenceCore(AreaBaseShape)`

**Summary**

   *This method returns the difference between two shapes, defined as the set of all points which lie in the current shape but not in the targetShape.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that is the set of all points which lie in the current shape but not in the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the difference with.|

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

   *This method returns the intersection of the current shape and the target shape, defined as the set of all points which lie in both the current shape and the target shape.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points which lie in both the current shape and the target shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the intersection with.|

---
#### `GetPerimeterCore(GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the perimeter of the shape, defined as the sum of the lengths of all its sides.*

**Remarks**

   *You would use this method to find the distance around the area shape. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return unit is based on a LengthUnit you specify in the returningUnit parameter, regardless of the shape's GeographyUnit.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the return value.  For example, if you select miles as your returningUnit, then the distance will be returned in miles.|

---
#### `GetPerimeterCore(Projection,DistanceUnit,DistanceCalculationMode)`

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
#### `GetSymmetricalDifferenceCore(AreaBaseShape)`

**Summary**

   *This method returns the symmetrical difference between two shapes, defined as the set of all points which lie in the current shape or the targetShape but not both.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that is the set of all points which lie in the current shape or the targetShape but not both.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the symmetrical difference with.|

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

   *This method decreases the size of the area shape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster, since you apply the operation multiple times. There is a ScaleUp method that will enlarge the shape as well. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to decrease the shape's size.|

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

   *This method increases the size of the area shape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster, since you apply the operation multiple times. There is a ScaleDown method that will shrink the shape as well. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Double`|This is the percentage by which to increase the shape's size.|

---
#### `SimplifyCore(Double,SimplificationType)`

**Summary**

   *This method performed a simplification operation based on the parameters passed in. Simplify permanently alters the input geometry so that the geometry becomes topologically consistent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|This method returns a simplification multipolgyon by the specified parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tolerance|`Double`|This parameter specifes the tolerance to be used when simplification.|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|This prameter specifies the type of simplification operation.|

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
#### `UnionCore(AreaBaseShape)`

**Summary**

   *This method returns the union of the current shape and the target shapes, defined as the set of all points in the current shape or the target shape.*

**Remarks**

   *This is useful for adding area shapes together to form a larger area shape. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|The return type is a MultiPolygonShape that contains the set of all points which lie in the current shape or the target shape. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the union with.|

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


