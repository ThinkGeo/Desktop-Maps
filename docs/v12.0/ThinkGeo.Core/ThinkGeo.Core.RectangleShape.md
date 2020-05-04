# RectangleShape


## Inheritance Hierarchy

+ `Object`
  + [`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)
    + [`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)
      + **`RectangleShape`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`RectangleShape()`](#rectangleshape)|
|[`RectangleShape(PointShape,PointShape)`](#rectangleshapepointshapepointshape)|
|[`RectangleShape(Double,Double,Double,Double)`](#rectangleshapedoubledoubledoubledouble)|
|[`RectangleShape(String)`](#rectangleshapestring)|
|[`RectangleShape(Byte[])`](#rectangleshapebyte[])|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Height`](#height)|`Double`|This property returns the height of the RectangleShape in the unit of the shape.|
|[`Id`](#id)|`String`|N/A|
|[`LowerLeftPoint`](#lowerleftpoint)|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This property returns the PointShape that represents the lower left point of the RectangleShape.|
|[`LowerRightPoint`](#lowerrightpoint)|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This property returns or sets the PointShape that represents the lower right point of the RectangleShape.|
|[`Tag`](#tag)|`Object`|N/A|
|[`UpperLeftPoint`](#upperleftpoint)|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This property returns or sets the PointShape that represents the upper left point of the RectangleShape.|
|[`UpperRightPoint`](#upperrightpoint)|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This property returns the PointShape that represents the upper right point of the RectangleShape.|
|[`Width`](#width)|`Double`|This property returns the width of the RectangleShape in the unit of the shape.|

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
|[`ExpandToInclude(BaseShape)`](#expandtoincludebaseshape)|
|[`ExpandToInclude(Feature)`](#expandtoincludefeature)|
|[`ExpandToInclude(IEnumerable<BaseShape>)`](#expandtoincludeienumerable<baseshape>)|
|[`ExpandToInclude(IEnumerable<Feature>)`](#expandtoincludeienumerable<feature>)|
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
|[`GetIntersection(RectangleShape)`](#getintersectionrectangleshape)|
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
|[`ScaleTo(Double)`](#scaletodouble)|
|[`ScaleUp(Double)`](#scaleupdouble)|
|[`Simplify(GeographyUnit,Double,DistanceUnit,SimplificationType)`](#simplifygeographyunitdoubledistanceunitsimplificationtype)|
|[`Simplify(Double,SimplificationType)`](#simplifydoublesimplificationtype)|
|[`ToPolygon()`](#topolygon)|
|[`ToString()`](#tostring)|
|[`Touches(BaseShape)`](#touchesbaseshape)|
|[`Touches(Feature)`](#touchesfeature)|
|[`TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreedoubledoublegeographyunitdistanceunit)|
|[`TranslateByDegree(Double,Double)`](#translatebydegreedoubledouble)|
|[`TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetdoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Double,Double)`](#translatebyoffsetdoubledouble)|
|[`Union(AreaBaseShape)`](#unionareabaseshape)|
|[`Union(Feature)`](#unionfeature)|
|[`Validate(ShapeValidationMode)`](#validateshapevalidationmode)|

### Protected Methods Summary


|Name|
|---|
|[`BufferCore(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`](#buffercoredoubleint32buffercaptypegeographyunitdistanceunit)|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`ContainsCore(BaseShape)`](#containscorebaseshape)|
|[`CrossesCore(BaseShape)`](#crossescorebaseshape)|
|[`ExpandToIncludeCore(BaseShape)`](#expandtoincludecorebaseshape)|
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
|[`RectangleShape()`](#rectangleshape)|
|[`RectangleShape(PointShape,PointShape)`](#rectangleshapepointshapepointshape)|
|[`RectangleShape(Double,Double,Double,Double)`](#rectangleshapedoubledoubledoubledouble)|
|[`RectangleShape(String)`](#rectangleshapestring)|
|[`RectangleShape(Byte[])`](#rectangleshapebyte[])|

### Protected Constructors


### Public Properties

#### `Height`

**Summary**

   *This property returns the height of the RectangleShape in the unit of the shape.*

**Remarks**

   *None*

**Return Value**

`Double`

---
#### `Id`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `LowerLeftPoint`

**Summary**

   *This property returns the PointShape that represents the lower left point of the RectangleShape.*

**Remarks**

   *None*

**Return Value**

[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)

---
#### `LowerRightPoint`

**Summary**

   *This property returns or sets the PointShape that represents the lower right point of the RectangleShape.*

**Remarks**

   *None*

**Return Value**

[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)

---
#### `Tag`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `UpperLeftPoint`

**Summary**

   *This property returns or sets the PointShape that represents the upper left point of the RectangleShape.*

**Remarks**

   *None*

**Return Value**

[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)

---
#### `UpperRightPoint`

**Summary**

   *This property returns the PointShape that represents the upper right point of the RectangleShape.*

**Remarks**

   *None*

**Return Value**

[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)

---
#### `Width`

**Summary**

   *This property returns the width of the RectangleShape in the unit of the shape.*

**Remarks**

   *None*

**Return Value**

`Double`

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
#### `ExpandToInclude(BaseShape)`

**Summary**

   *This method will expand the current RectangleShape to include the targetShape parameter.*

**Remarks**

   *There is also an overload that can expand the RectangleShape using a list of multiple shapes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is used to expand the current rectangle.|

---
#### `ExpandToInclude(Feature)`

**Summary**

   *This method will expand the current RectangleShape to include the target feature parameter.*

**Remarks**

   *There is also an overload that can expand the RectangleShape using a collection of features.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is used to expand the current feature.|

---
#### `ExpandToInclude(IEnumerable<BaseShape>)`

**Summary**

   *This method will expand the current RectangleShape to include the shapes in the targetShapes parameter.*

**Remarks**

   *There is also an overload that can expand the RectangleShape using a single shape.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShapes|IEnumerable<[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)>|This parameter is used to expand the current rectangle.|

---
#### `ExpandToInclude(IEnumerable<Feature>)`

**Summary**

   *This method will expand the current RectangleShape to include the features in the targetFeatures parameter.*

**Remarks**

   *There also is an overload that can expand the RectangleShape using a single feature.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeatures|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter is a group of features used to expand the current rectangle.|

---
#### `GetArea(GeographyUnit,AreaUnit)`

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
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|returningUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|N/A|

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RingShape`](../ThinkGeo.Core/ThinkGeo.Core.RingShape.md)|N/A|

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
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetDifference(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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
#### `GetIntersection(RectangleShape)`

**Summary**

   *This method returns the intersection of the current RectangleShape and the target shape, defined as the set of all points which lie in both the current shape and the target shape.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The return type is a RectangleShape that contains the set of all points which lie in both the current shape and the target shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The shape you are trying to find the intersection with.|

---
#### `GetIntersection(AreaBaseShape)`

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
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetIntersection(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `GetPerimeter(GeographyUnit,DistanceUnit)`

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
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

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
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetSymmetricalDifference(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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
|percentage|`Double`|N/A|

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
|percentage|`Double`|N/A|

---
#### `Simplify(GeographyUnit,Double,DistanceUnit,SimplificationType)`

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
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|tolerance|`Double`|N/A|
|toleranceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|N/A|

---
#### `Simplify(Double,SimplificationType)`

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
|tolerance|`Double`|N/A|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|N/A|

---
#### `ToPolygon()`

**Summary**

   *This method returns the current shape as a PolygonShape.*

**Remarks**

   *This method is mainly used to convert this GIS non-standard shape to a GIS standard shape.*

**Return Value**

|Type|Description|
|---|---|
|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|This method returns the current shape as a PolygonShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ToString()`

**Summary**

   *This method returns a formatted representation of the UpperLeftPoint.X, UpperLeftPoint.Y, LowerRightPoint.X and LowerRightPoint.Y value.*

**Remarks**

   *The value will be formatted in the standard string format: -180,90,180,-90"*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a formatted representation of the UpperLeftPoint.X, UpperLeftPoint.Y, LowerRightPoint.X and LowerRightPoint.Y value.|

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
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `Union(Feature)`

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
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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
#### `ExpandToIncludeCore(BaseShape)`

**Summary**

   *This method will expand the current RectangleShape to include the shape in the targetShape parameter.*

**Remarks**

   *There is also an overload that can expand the RectangleShape using a list of shapes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is used to expand the current rectangle.|

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

   *This method returns the area of the shape (defined as the size of the region enclosed by the figure).*

**Remarks**

   *You would use this method to find the area inside the shape.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return unit is based on the AreaUnit you specify in the returningUnit parameter, regardless of the shape's GeographyUnit.|

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

   *This method calculates the smallest RectangleShape that encompasses the entire geometry.*

**Remarks**

   *The GetBoundingBox method calculates the smallest RectangleShape that can encompass the entire geometry by examining each point in the geometry. Depending on the number of PointShapes and complexity of the geometry, this operation can take longer for larger objects. If the shape is a PointShape, then the bounding box's upper left and lower right points will be equal. This will create a RectangleShape with no area.*

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

   *This method returns the center point of the current shape's bounding box. It is important to note that this is the center point of the bounding box. There are numerous ways to calculate the "center" of a geometry such, as its weighted center, etc. You can find other centers by examining the various methods of the shape itself. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

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

   *This method returns the point of the current shape that is closest to the target shape. It is often the case that the point returned is not a point of the object itself. An example would be a line with two points that are far apart from each other. If you set the targetShape to be a point midway between the points but a short distance away from the line, the method would return a point that is on the line but not either of the two points that make up the line.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A PointShape representing the closest point of the current shape to the targetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the closest point to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|

---
#### `GetConvexHullCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RingShape`](../ThinkGeo.Core/ThinkGeo.Core.RingShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `GetDifferenceCore(AreaBaseShape)`

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
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetDistanceToCore(BaseShape,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the distance between the current shape and the targetShape.*

**Remarks**

   *In this method, we compute the closest distance between the two shapes. The returned unit will be in the unit of distance specified.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return type is the distance between this shape and the targetShape in the GeographyUnit of the shape. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The GeographyUnit of the targetShape.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The DistanceUnit of the returned value.|

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
|[`MultipolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.MultipolygonShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetPerimeterCore(GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the perimeter of the shape (defined as the sum of the lengths of all its sides).*

**Remarks**

   *You would use this method to find the distance around the area shape.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return unit is based on the LengthUnit you specify in the returningUnit parameter, regardless of the shape's GeographyUnit.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the return value. For example, if you select miles as your returningUnit, then the distance will be returned in miles.|

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
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`

**Summary**

   *This method returns a byte array that represents the shape in well-known binary.*

**Remarks**

   *This method returns a byte array that represents the shape in well-known binary. Well-known binary allows you to describe a geometry as a binary array. Well-known binary is useful when you want to save a geometry in an efficient format using as little space as possible. An alternative to well-known binary is well-known text, which is a textual representation of a geometry object. We have methods that work with well-known text as well.*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|This method returns a byte array that represents the shape in well-known binary.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outerRingOrder|[`RingOrder`](../ThinkGeo.Core/ThinkGeo.Core.RingOrder.md)|N/A|
|byteOrder|[`WkbByteOrder`](../ThinkGeo.Core/ThinkGeo.Core.WkbByteOrder.md)|This parameter specifies whether the byte order is big- or little-endian.|

---
#### `GetWellKnownTextCore(RingOrder)`

**Summary**

   *This method returns the well-known text representation of this shape.*

**Remarks**

   *This method returns a string that represents the shape in well-known text. Well-known text allows you to describe a geometry as a string of text. Well-known text is useful when you want to save a geometry in a format such as a text file, or when you simply want to cut and paste the text between other applications. An alternative to well-known text is well-known binary, which is a binary representation of a geometry object. We have methods that work with well-known binary as well. Below are some samples of what well-known text might look like for various kinds of geometric shapes.POINT(5 17)LINESTRING(4 5,10 50,25 80)POLYGON((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3))MULTIPOINT(3.7 9.7,4.9 11.6)MULTILINESTRING((4 5,11 51,21 26),(-4 -7,-9 -7,-14 -3))MULTIPOLYGON(((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3)),((4 4,7 3,7 5,4 4)))*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string that represents the shape in well-known text.|

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

   *This method returns whether the current shape and the targetShape have no points in common.*

**Remarks**

   *Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns whether the current shape and the targetShape have no points in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to compare the current one to.|

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

   *This method returns a MultipointShape which has been registered from its original coordinate system to another based on two anchor PointShapes.*

**Remarks**

   *The register method is useful, for example, when a group of points represented by a MultipointShape has been generated in a non-geographic coordinate in meters, and you want to plot the points on a map that is in decimal degrees.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a MultipointShape which has been registered from its original coordinate system to another based on two anchor PointShapes.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of origin.|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of destination.|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter is the GeographyUnit of the coordinate of origin.|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the GeographyUnit of the coordinate of destination.|

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

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster, as you are applying the operation multiple times. There is also a ScaleUp method that will enlarge the shape.|

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

   *This method is useful when you would like to increase the size of the shape. Note that a larger percentage will scale the shape up faster, as you are applying the operation multiple times. There is also a ScaleDown method that will shrink the shape.*

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
|tolerance|`Double`|N/A|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|N/A|

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

   *This method moves the shape from one location to another, based on a direction in degrees and distance.*

**Remarks**

   *This method moves the base shape from one location to another, based on an angleInDegrees and distance parameter. With this overload, it is important to note that the distance units are the same GeographicUnit as the shape. For example, if your shape is in decimal degrees and you call this method with a distance of 1, you're going to move this shape 1 decimal degree in direction of the angleInDegrees. In many cases it is more useful to specify the DistanceUnit of movement, such as in miles or yards, so for these scenarios there is another overload you may want to use instead.If you pass a distance of 0, then the operation is ignored.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to move the shape in the angle specified. The distance unit will be the same as the GeographyUnit for the shape. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the shape, with 0 being up.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the measure for the move. For example, if you select miles as your distanceUnit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `TranslateByOffsetCore(Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves the base shape from one location to another, based on an X and Y offset distance.*

**Remarks**

   *This method moves the base shape from one location to another, based on an X and Y offset distance. With this overload, it is important to note that the X and Y offset units are based on the distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this shape 1 unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you can easily move a shape in decimal degrees five miles to on the X axis and 3 miles on the Y axis.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|xOffsetDistance|`Double`|This is the number of horizontal units of movement in the DistanceUnit specified as the distanceUnit.|
|yOffsetDistance|`Double`|This is the number of horizontal units of movement in the DistanceUnit specified as the distanceUnit.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the measure for the move. For example, if you select miles as your distanceUnit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `UnionCore(AreaBaseShape)`

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
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

---
#### `ValidateCore(ShapeValidationMode)`

**Summary**

   *This method returns a ShapeValidationResult based on a series of tests.*

**Remarks**

   *We use this method, with the simple enumeration, internally before doing any kind of other methods on the shape. In this way, we are able to verify the integrity of the shape itself. If you wish to test things such as whether a polygon self-intersects, we invite you to call this method with the advanced ShapeValidationMode. One thing to consider is that for complex polygon shapes this operation could take some time, which is why we only run the basic, faster test. If you are dealing with polygon shapes that are suspect, we suggest you run the advanced test.*

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


