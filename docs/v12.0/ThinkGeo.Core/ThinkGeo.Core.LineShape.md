# LineShape


## Inheritance Hierarchy

+ `Object`
  + [`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)
    + [`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)
      + **`LineShape`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`LineShape()`](#lineshape)|
|[`LineShape(IEnumerable<Vertex>)`](#lineshapeienumerable<vertex>)|
|[`LineShape(String)`](#lineshapestring)|
|[`LineShape(Byte[])`](#lineshapebyte[])|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Id`](#id)|`String`|N/A|
|[`Tag`](#tag)|`Object`|N/A|
|[`Vertices`](#vertices)|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This property is the collection of points that make up the LineShape.|

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
|[`GetLineOnALine(StartingPoint,Single,Single)`](#getlineonalinestartingpointsinglesingle)|
|[`GetLineOnALine(StartingPoint,Single)`](#getlineonalinestartingpointsingle)|
|[`GetLineOnALine(StartingPoint,Double,Double,GeographyUnit,DistanceUnit)`](#getlineonalinestartingpointdoubledoublegeographyunitdistanceunit)|
|[`GetLineOnALine(StartingPoint,PointShape)`](#getlineonalinestartingpointpointshape)|
|[`GetLineOnALine(PointShape,PointShape)`](#getlineonalinepointshapepointshape)|
|[`GetPointOnALine(StartingPoint,Single)`](#getpointonalinestartingpointsingle)|
|[`GetPointOnALine(StartingPoint,Double,GeographyUnit,DistanceUnit)`](#getpointonalinestartingpointdoublegeographyunitdistanceunit)|
|[`GetPointPosition(PointShape,Double)`](#getpointpositionpointshapedouble)|
|[`GetShortestLineTo(BaseShape,GeographyUnit)`](#getshortestlinetobaseshapegeographyunit)|
|[`GetShortestLineTo(Feature,GeographyUnit)`](#getshortestlinetofeaturegeographyunit)|
|[`GetSublinePercentage(PointShape,Double)`](#getsublinepercentagepointshapedouble)|
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
|[`IsClosed()`](#isclosed)|
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
|[`RemoveVertex(Vertex)`](#removevertexvertex)|
|[`RemoveVertex(Vertex,LineShape)`](#removevertexvertexlineshape)|
|[`ReversePoints()`](#reversepoints)|
|[`Rotate(PointShape,Single)`](#rotatepointshapesingle)|
|[`ScaleDown(Double)`](#scaledowndouble)|
|[`ScaleTo(Double)`](#scaletodouble)|
|[`ScaleUp(Double)`](#scaleupdouble)|
|[`Simplify(GeographyUnit,Double,DistanceUnit,SimplificationType)`](#simplifygeographyunitdoubledistanceunitsimplificationtype)|
|[`Simplify(Double,SimplificationType)`](#simplifydoublesimplificationtype)|
|[`ToPolygonShape()`](#topolygonshape)|
|[`ToString()`](#tostring)|
|[`Touches(BaseShape)`](#touchesbaseshape)|
|[`Touches(Feature)`](#touchesfeature)|
|[`TranslateByDegree(Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreedoubledoublegeographyunitdistanceunit)|
|[`TranslateByDegree(Double,Double)`](#translatebydegreedoubledouble)|
|[`TranslateByOffset(Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetdoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(Double,Double)`](#translatebyoffsetdoubledouble)|
|[`Union(LineBaseShape)`](#unionlinebaseshape)|
|[`Union(Feature)`](#unionfeature)|
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
|[`GetLineOnALineCore(StartingPoint,Double,Double,GeographyUnit,DistanceUnit)`](#getlineonalinecorestartingpointdoubledoublegeographyunitdistanceunit)|
|[`GetPointOnALineCore(StartingPoint,Double,GeographyUnit,DistanceUnit)`](#getpointonalinecorestartingpointdoublegeographyunitdistanceunit)|
|[`GetPointPositionCore(PointShape,Double)`](#getpointpositioncorepointshapedouble)|
|[`GetShortestLineToCore(BaseShape,GeographyUnit)`](#getshortestlinetocorebaseshapegeographyunit)|
|[`GetSublinePercentageCore(PointShape,Double)`](#getsublinepercentagecorepointshapedouble)|
|[`GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`](#getwellknownbinarycoreringorderwkbbyteorder)|
|[`GetWellKnownTextCore(RingOrder)`](#getwellknowntextcoreringorder)|
|[`GetWellKnownTypeCore()`](#getwellknowntypecore)|
|[`IntersectsCore(BaseShape)`](#intersectscorebaseshape)|
|[`IsClosedCore()`](#isclosedcore)|
|[`IsDisjointedCore(BaseShape)`](#isdisjointedcorebaseshape)|
|[`IsTopologicallyEqualCore(BaseShape)`](#istopologicallyequalcorebaseshape)|
|[`IsWithinCore(BaseShape)`](#iswithincorebaseshape)|
|[`LoadFromWellKnownDataCore(String)`](#loadfromwellknowndatacorestring)|
|[`LoadFromWellKnownDataCore(Byte[])`](#loadfromwellknowndatacorebyte[])|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OverlapsCore(BaseShape)`](#overlapscorebaseshape)|
|[`RegisterCore(PointShape,PointShape,DistanceUnit,GeographyUnit)`](#registercorepointshapepointshapedistanceunitgeographyunit)|
|[`ReversePointsCore()`](#reversepointscore)|
|[`RotateCore(PointShape,Single)`](#rotatecorepointshapesingle)|
|[`Scale(Double)`](#scaledouble)|
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
|[`LineShape()`](#lineshape)|
|[`LineShape(IEnumerable<Vertex>)`](#lineshapeienumerable<vertex>)|
|[`LineShape(String)`](#lineshapestring)|
|[`LineShape(Byte[])`](#lineshapebyte[])|

### Protected Constructors


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
#### `Vertices`

**Summary**

   *This property is the collection of points that make up the LineShape.*

**Remarks**

   *None*

**Return Value**

Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>

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
#### `GetLineOnALine(StartingPoint,Single,Single)`

**Summary**

   *This method returns a BaseLineShape, based on a starting position and other factors.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This method returns a BaseLineShape, based on a starting position and other factors.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|startingPercentageOfTheLine|`Single`|This parameter defines the starting percentage into the line. Valid values must be greater than 0 and less than or equal to 100.|
|percentageOfTheLine|`Single`|This parameter defines the percentage into the line. Valid values must be greater than 0 and less than or equal to 100.|

---
#### `GetLineOnALine(StartingPoint,Single)`

**Summary**

   *This method returns a BaseLineShape, based on a starting position and other factors.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This method returns a BaseLineShape, based on a starting position and other factors.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|percentageOfLine|`Single`|This parameter defines the percentage into the line. Valid values must be greater than 0 and less than or equal to 100.|

---
#### `GetLineOnALine(StartingPoint,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a BaseLineShape, based on a starting position and other factors.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This method returns a BaseLineShape, based on a starting position and other factors.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|startingDistance|`Double`|The starting distance from which you will start getting the line. For example, if the line is 3 units long and you have a starting distance of 1 unit, the result will be the last two units of the line. Valid values must be greater than 0. The starting distance will be in the GeographyUnit of the shape.|
|distance|`Double`|The amount of the line you want to get after the startingDistance. Valid values must be greater than 0.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use for the distance parameter. For example, if you select miles as your distanceUnit, then the distance will be measured in miles.|

---
#### `GetLineOnALine(StartingPoint,PointShape)`

**Summary**

   *This method returns a BaseLineShape, based on a starting position and other factors.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This method returns a BaseLineShape, based on a starting position and other factors.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|endPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The BaseLineShape returned will be between the startingPoint and the endPointShape specified in this parameter.|

---
#### `GetLineOnALine(PointShape,PointShape)`

**Summary**

   *This method returns a BaseLineShape, based on a starting position and other factors.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This method returns a BaseLineShape based on a start PointShape and an end PointShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The startPointShape defines where you will start to get the line. If it does not stand on this LineShape, the closest point on the LineShape will be the start PointShape.|
|endPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The endPointShape defines where you will stop getting the line. If it does not stand on this LineShape, the closest point on the LineShape will be the end PointShape.|

---
#### `GetPointOnALine(StartingPoint,Single)`

**Summary**

   *This method returns a PointShape on the line, based on a percentage of the length of the line from the first or last vertex defined in the startingPoint parameter.*

**Remarks**

   *If you pass 100 or 0 as the percentage of the line, it will return either the first or last vertex, depending on the value of the startingPoint argument.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns a PointShape on the line, based on a percentage of the length of the line from the first or last vertex defined in the startingPoint parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|percentageOfLine|`Single`|This parameter defines the percentage into the line. Valid values are between 0 and 100.|

---
#### `GetPointOnALine(StartingPoint,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a PointShape on the line, based on a distance on the line from the first or last vertex defined in the startingPoint parameter.*

**Remarks**

   *Passing in a 0 distance will return either the first or last point on the line, depending upon the value of the startingPoint parameter.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns a PointShape on the line, based on a distance on the line from the first or last vertex defined in the startingPoint parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|distance|`Double`|This parameter specifies the distance into the line you wish to move in the unit specified by the distanceUnit parameter. Valid values must be greater than or equal to 0.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use for the distance parameter. For example, if you select miles as your distanceUnit, then the distance will be measured in miles.|

---
#### `GetPointPosition(PointShape,Double)`

**Summary**

   *Calculate the topology relationship between point and line.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointLineRelationship`](../ThinkGeo.Core/ThinkGeo.Core.PointLineRelationship.md)||

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|tolerance|`Double`|N/A|

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
#### `GetSublinePercentage(PointShape,Double)`

**Summary**

   *Calculate the percentage of pointShape is along the line.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|sublineEndpoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|tolerance|`Double`|N/A|

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
#### `IsClosed()`

**Summary**

   *This method determines whether the line is closed, meaning that the last point and first point are the same.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|The return value indicating whether the line is closed.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `RemoveVertex(Vertex)`

**Summary**

   *This method removes the selected vertex from line shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|If remove sucess it will return true, otherwise return false.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|selectedVertex|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|The selected vertex must be a vertex of line shape, otherwise it will return false and line shape will keep the same.|

---
#### `RemoveVertex(Vertex,LineShape)`

**Summary**

   *This method removes the selected vertex from line shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|If remove sucess it will return true, otherwise return false.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|selectedVertex|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|The selected vertex must be a vertex of line shape, otherwise it will return false and line shape will keep the same.|
|lineShape|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|The line shape will be removed one vertex.|

---
#### `ReversePoints()`

**Summary**

   *This method reverses the order of the points in the line.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

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
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tolerance|`Double`|N/A|
|simplificationType|[`SimplificationType`](../ThinkGeo.Core/ThinkGeo.Core.SimplificationType.md)|N/A|

---
#### `ToPolygonShape()`

**Summary**

   *This method generates a PolygonShape based the vertexes of the line.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|A generated polygon based on the vertexes of the line, otherwise return null.|

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
|targetShape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|

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

   *When you override this method, you need to ensure that there are no references in common between the original and the copy.*

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

   *This method calculates the smallest RectangleShape that encompasses the entire geometry.*

**Remarks**

   *The GetBoundingBox method calculates the smallest RectangleShape that can encompass the entire geometry by examining each point in the geometry. Depending on the number of PointShapes and complexity of the geometry this operation can take longer for larger objects. If the shape is a PointShape, than the bounding box's upper left and lower right points will be equal. This will create a RectangleShape with no area. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

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
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|

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

   *In this method, we compute the closest distance between the two shapes. The returned unit will be in the unit of distance specified.Overriding:Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return type is the distance between this shape and the targetShape in the GeographyUnit of the shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you are trying to find the distance to.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the return value. For example, if you select miles as your distanceUnit, then the distance will be returned in miles.|

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

   *This method returns the length of the LineShape.*

**Remarks**

   *This is a useful method when you want to know the total length of a line-based shape. If the shape is a MultiLineShape, then the length is the sum of all of its lines.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This overload returns the length in the unit of your choice, based on returningUnit you specify.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the return value. For example, if you select miles as your returningUnit, then the distance will be returned in miles.|

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
#### `GetLineOnALineCore(StartingPoint,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a BaseLineShape, based on a starting position and other factors.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This method returns a BaseLineShape, based on a starting position and other factors.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|startingDistance|`Double`|The starting distance from which you will start getting the line. For example, if the line is 3 units long and you have a starting distance of 1 unit, the result will be the last two units of the line. Valid values must be greater than 0. The starting distance will be in the GeographyUnit of the shape.|
|distance|`Double`|The amount of the line you want to get after the startingDistance. Valid values must be greater than 0.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use for the distance parameter. For example, if you select miles as your distanceUnit, then the distance will be measured in miles.|

---
#### `GetPointOnALineCore(StartingPoint,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns a PointShape on the line, based on a distance on the line from the first or last vertex defined in the startingPoint parameter.*

**Remarks**

   *Passing in a 0 distance will return either the first or last point on the line, depending on the value of the startingPoint parameter.*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns a PointShape on the line, based on a distance on the line from the first or last vertex defined in the startingPoint parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|startingPoint|[`StartingPoint`](../ThinkGeo.Core/ThinkGeo.Core.StartingPoint.md)|The startingPoint defines whether the method starts at the beginning or the end of the line.|
|distance|`Double`|This parameter specifies the distance into the line you wish to move in the unit specified by the distanceUnit parameter. Valid values must be greater than or equal to 0.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographyUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use for the distance parameter. For example, if you select miles as your distanceUnit, then the distance will be measured in miles.|

---
#### `GetPointPositionCore(PointShape,Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointLineRelationship`](../ThinkGeo.Core/ThinkGeo.Core.PointLineRelationship.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|tolerance|`Double`|N/A|

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
#### `GetSublinePercentageCore(PointShape,Double)`

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
|sublineEndpoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|tolerance|`Double`|N/A|

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

   *This method returns the well-known text representation of this shape.*

**Remarks**

   *This method returns a string that represents the shape in well-known text. Well-known text allows you to describe a geometry as a string of text. Well-known text is useful when you want to save a geometry in a format such as a text file, or when you simply want to cut and paste the text between other applications. An alternative to well-known text is well-known binary, which is a binary representation of a geometry object. We have methods that work with well-known binary as well. Below are some samples of what well-known text might look like for various kinds of geometric figures.POINT(5 17)LINESTRING(4 5,10 50,25 80)POLYGON((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3))MULTIPOINT(3.7 9.7,4.9 11.6)MULTILINESTRING((4 5,11 51,21 26),(-4 -7,-9 -7,-14 -3))MULTIPOLYGON(((2 2,6 2,6 6,2 6,2 2),(3 3,4 3,4 4,3 4,3 3)),((4 4,7 3,7 5,4 4)))*

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
#### `IsClosedCore()`

**Summary**

   *This method determines whether the line is closed, meaning that the last point and first point are the same.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|The return value indicating whether the line is closed.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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

   *This method returns a BaseShape which has been registered from its original coordinate system to another, based on two anchor PointShapes.*

**Remarks**

   *Registering allows you to take a geometric shape generated in a planar system and attach it to the ground in a Geographic Unit.A common scenario is integrating geometric shapes from external programs (such as CAD software or a modeling system) and placing them onto a map. You may have the schematics of a building in a CAD system and the relationship between all the points of the building are in feet. You want to then take the CAD image and attach it to where it really exists on a map. You would use the register method to do this.Registering is also useful for scientific modeling, where software models things such as a plume of hazardous materials or the fallout from a volcano. The modeling software typically generates these models in a fictitious planar system. You would then use the register to take the abstract model and attach it to a map with real coordinates.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns a BaseShape which has been registered from its original coordinate system to another, based on two anchor PointShapes.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of origin.|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the anchor PointShape in the coordinate of destination.|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter is the DistanceUnit of the coordinate of origin.|
|toUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the GeographyUnit of the coordinate of destination.|

---
#### `ReversePointsCore()`

**Summary**

   *This method reverses the order of the points in the line.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `RotateCore(PointShape,Single)`

**Summary**

   *This method rotates the shape a number of degrees based on a pivot point.*

**Remarks**

   *This method rotates the shape a number of degrees based on a pivot point. By placing the pivot point in the center of the shape, you can achieve in-place rotation. By moving the pivot point outside of the center of the shape, you can translate the shape in a circular motion. Moving the pivot point further outside of the center will make the circular area larger.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The pivotPoint represents the center of rotation.|
|degreeAngle|`Single`|The number of degrees of rotation required from 0 to 360.|

---
#### `Scale(Double)`

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
|factor|`Double`|N/A|

---
#### `ScaleDownCore(Double)`

**Summary**

   *This method decreases the size of the LineShape by the percentage given in the percentage parameter.*

**Remarks**

   *This method is useful when you would like to decrease the size of the shape. Note that a larger percentage will scale the shape down faster, as you are applying the operation multiple times. There is also a ScaleUp method that will enlarge the shape.*

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

   *This method increases the size of the LineShape by the percentage given in the percentage parameter.*

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
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

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

   *This method moves the base shape from one location to another based on a distance and a direction in degrees.*

**Remarks**

   *This method returns a shape repositioned from one location to another based on angleInDegrees and distance parameter. With this overload, it is important to note that the distance is based on the supplied distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with a distanceUnit of miles, you're going to move this shape a number of miles based on the distance value and the angleInDegrees. In this way, you can easily move a shape in decimal degrees five miles to the north.If you pass a distance of 0, then the operation is ignored.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to move the shape using the angle specified. The distance unit will be the one specified in the distanceUnit parameter. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the shape, with 0 being up.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the measure for the move. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles.|

---
#### `TranslateByOffsetCore(Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves the base shape from one location to another based on an X and Y offset distance.*

**Remarks**

   *This method returns a shape repositioned from one location to another based on an X and Y offset distance. With this overload, it is important to note that the X and Y offset units are based on the distanceUnit parameter. For example, if your shape is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this shape one unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you can easily move a shape in decimal degrees five miles on the X axis and 3 miles on the Y axis.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|xOffsetDistance|`Double`|This is the number of horizontal units of movement in the distance unit specified in the distanceUnit parameter.|
|yOffsetDistance|`Double`|This is the number of vertical units of movement in the distance unit specified in the distanceUnit parameter.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the measure for the move. For example, if you select miles as your distance unit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `UnionCore(IEnumerable<LineBaseShape>)`

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
|lineBaseShapes|IEnumerable<[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)>|N/A|

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


