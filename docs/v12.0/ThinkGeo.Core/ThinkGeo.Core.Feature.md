# Feature


## Inheritance Hierarchy

+ `Object`
  + **`Feature`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`Feature()`](#feature)|
|[`Feature(BaseShape)`](#featurebaseshape)|
|[`Feature(Byte[])`](#featurebyte[])|
|[`Feature(Byte[],String)`](#featurebyte[]string)|
|[`Feature(String)`](#featurestring)|
|[`Feature(String,String)`](#featurestringstring)|
|[`Feature(BaseShape,IDictionary<String,String>)`](#featurebaseshapeidictionary<stringstring>)|
|[`Feature(BaseShape,IEnumerable<String>)`](#featurebaseshapeienumerable<string>)|
|[`Feature(String,String,IDictionary<String,String>)`](#featurestringstringidictionary<stringstring>)|
|[`Feature(String,String,IEnumerable<String>)`](#featurestringstringienumerable<string>)|
|[`Feature(Byte[],String,IEnumerable<String>)`](#featurebyte[]stringienumerable<string>)|
|[`Feature(Byte[],String,IDictionary<String,String>)`](#featurebyte[]stringidictionary<stringstring>)|
|[`Feature(Vertex)`](#featurevertex)|
|[`Feature(Vertex,String)`](#featurevertexstring)|
|[`Feature(Vertex,String,IEnumerable<String>)`](#featurevertexstringienumerable<string>)|
|[`Feature(Vertex,String,IDictionary<String,String>)`](#featurevertexstringidictionary<stringstring>)|
|[`Feature(Double,Double)`](#featuredoubledouble)|
|[`Feature(Double,Double,String)`](#featuredoubledoublestring)|
|[`Feature(Double,Double,String,IEnumerable<String>)`](#featuredoubledoublestringienumerable<string>)|
|[`Feature(Double,Double,String,IDictionary<String,String>)`](#featuredoubledoublestringidictionary<stringstring>)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ColumnValues`](#columnvalues)|Dictionary<`String`,`String`>|This property gets a dictionary of values to represent the column data related to this Feature.|
|[`Id`](#id)|`String`|This property gets the Id for the Feature.|
|[`Tag`](#tag)|`Object`|The tag of the Feature.|

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
|[`CloneDeep(IEnumerable<String>)`](#clonedeepienumerable<string>)|
|[`CloneDeep(ReturningColumnsType)`](#clonedeepreturningcolumnstype)|
|[`CloneDeep()`](#clonedeep)|
|[`Contains(Feature)`](#containsfeature)|
|[`CreateFeatureFromGeoJson(String)`](#createfeaturefromgeojsonstring)|
|[`CreateFeatureFromWellKnownData(Byte[])`](#createfeaturefromwellknowndatabyte[])|
|[`CreateFeatureFromWellKnownData(String)`](#createfeaturefromwellknowndatastring)|
|[`Crosses(Feature)`](#crossesfeature)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetConvexHull()`](#getconvexhull)|
|[`GetDifference(Feature)`](#getdifferencefeature)|
|[`GetGeoJson()`](#getgeojson)|
|[`GetHashCode()`](#gethashcode)|
|[`GetIntersection(Feature)`](#getintersectionfeature)|
|[`GetInvalidReason()`](#getinvalidreason)|
|[`GetShape()`](#getshape)|
|[`GetType()`](#gettype)|
|[`GetWellKnownBinary()`](#getwellknownbinary)|
|[`GetWellKnownBinary(WkbByteOrder)`](#getwellknownbinarywkbbyteorder)|
|[`GetWellKnownBinary(RingOrder)`](#getwellknownbinaryringorder)|
|[`GetWellKnownBinary(RingOrder,WkbByteOrder)`](#getwellknownbinaryringorderwkbbyteorder)|
|[`GetWellKnownText()`](#getwellknowntext)|
|[`GetWellKnownText(RingOrder)`](#getwellknowntextringorder)|
|[`GetWellKnownType()`](#getwellknowntype)|
|[`Intersects(Feature)`](#intersectsfeature)|
|[`IsDisjointed(Feature)`](#isdisjointedfeature)|
|[`IsGeometryValid()`](#isgeometryvalid)|
|[`IsTopologicallyEqual(Feature)`](#istopologicallyequalfeature)|
|[`IsWithin(Feature)`](#iswithinfeature)|
|[`Overlaps(Feature)`](#overlapsfeature)|
|[`SetWellKnownBinary(Byte[])`](#setwellknownbinarybyte[])|
|[`ToString()`](#tostring)|
|[`Touches(Feature)`](#touchesfeature)|
|[`Union(Feature)`](#unionfeature)|
|[`Union(IEnumerable<Feature>)`](#unionienumerable<feature>)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetGeoJsonCore()`](#getgeojsoncore)|
|[`GetShapeCore()`](#getshapecore)|
|[`GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`](#getwellknownbinarycoreringorderwkbbyteorder)|
|[`GetWellKnownTextCore(RingOrder)`](#getwellknowntextcoreringorder)|
|[`GetWellKnownTypeCore()`](#getwellknowntypecore)|
|[`MemberwiseClone()`](#memberwiseclone)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`Feature()`](#feature)|
|[`Feature(BaseShape)`](#featurebaseshape)|
|[`Feature(Byte[])`](#featurebyte[])|
|[`Feature(Byte[],String)`](#featurebyte[]string)|
|[`Feature(String)`](#featurestring)|
|[`Feature(String,String)`](#featurestringstring)|
|[`Feature(BaseShape,IDictionary<String,String>)`](#featurebaseshapeidictionary<stringstring>)|
|[`Feature(BaseShape,IEnumerable<String>)`](#featurebaseshapeienumerable<string>)|
|[`Feature(String,String,IDictionary<String,String>)`](#featurestringstringidictionary<stringstring>)|
|[`Feature(String,String,IEnumerable<String>)`](#featurestringstringienumerable<string>)|
|[`Feature(Byte[],String,IEnumerable<String>)`](#featurebyte[]stringienumerable<string>)|
|[`Feature(Byte[],String,IDictionary<String,String>)`](#featurebyte[]stringidictionary<stringstring>)|
|[`Feature(Vertex)`](#featurevertex)|
|[`Feature(Vertex,String)`](#featurevertexstring)|
|[`Feature(Vertex,String,IEnumerable<String>)`](#featurevertexstringienumerable<string>)|
|[`Feature(Vertex,String,IDictionary<String,String>)`](#featurevertexstringidictionary<stringstring>)|
|[`Feature(Double,Double)`](#featuredoubledouble)|
|[`Feature(Double,Double,String)`](#featuredoubledoublestring)|
|[`Feature(Double,Double,String,IEnumerable<String>)`](#featuredoubledoublestringienumerable<string>)|
|[`Feature(Double,Double,String,IDictionary<String,String>)`](#featuredoubledoublestringidictionary<stringstring>)|

### Protected Constructors


### Public Properties

#### `ColumnValues`

**Summary**

   *This property gets a dictionary of values to represent the column data related to this Feature.*

**Remarks**

   *This property holds the column data related to this Feature. You can find the values in the dictionary using the column name as the key. Most methods that query and return InternalFeatures allow you to specify which columns of data you want returned with the results. You can also freely add and modify the data, as it is simply an in-memory dictionary. Any values added, deleted or updated will have no effect unless the Feature is part of a transaction.*

**Return Value**

Dictionary<`String`,`String`>

---
#### `Id`

**Summary**

   *This property gets the Id for the Feature.*

**Remarks**

   *The Id is a string that represents the unique identifier for this Feature. If the feature is returned from a FeatureSource, the Id will be the unique field descriptor used by the FeatureSource. For Shape Files this may be an integer, but for spatial databases the Id may be a GUID.*

**Return Value**

`String`

---
#### `Tag`

**Summary**

   *The tag of the Feature.*

**Remarks**

   *N/A*

**Return Value**

`Object`

---

### Protected Properties


### Public Methods

#### `Buffer(Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the area containing all of the points within a given distance from this feature.*

**Remarks**

   *This method computes the area containing all of the points within a given distance from this feature. In this case, you will be using the rounded RoundedBufferCapStyle and the default 8 quadrant segments. The distance unit is determined by the distanceUnit argument. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The return type is a Feature that represents all of the points within a given distance from the feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to buffer the current shape. The distance unit will be the one specified in the distanceUnit parameter.|
|featureUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the distance. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles for the operation.|

---
#### `Buffer(Double,Int32,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the area containing all of the points within a given distance from this feature.*

**Remarks**

   *This method computes the area containing all of the points within a given distance from this feature. In this case, you will be using the rounded RoundedBufferCapStyle. The distance unit is determined by the distanceUnit argument. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The return type is a Feature that represents all of the points within a given distance from the feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to buffer the current shape. The distance unit will be the one specified in the distanceUnit parameter.|
|quadrantSegments|`Int32`|The quadrant segments are the number of points in each quarter circle. A good default is 8, but if you want smoother edges you can increase this number. The valid range for this number is from 3 to 100.|
|featureUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the feature you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the distance. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles for the operation.|

---
#### `Buffer(Double,Int32,BufferCapType,GeographyUnit,DistanceUnit)`

**Summary**

   *This method computes the area containing all of the points within a given distance from this feature.*

**Remarks**

   *This method computes the area containing all of the points within a given distance from this feature. In this case, you will be using the rounded RoundedBufferCapStyle and the default 8 quadrant segments. The distance unit is determined by the distanceUnit argument. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The return type is a Feature that represents all of the points within a given distance from the feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance is the number of units to buffer the current feature. The distance unit will be the one specified in the distanceUnit parameter.|
|quadrantSegments|`Int32`|The number of quadrantSegments used in the buffer logic.|
|bufferCapType|[`BufferCapType`](../ThinkGeo.Core/ThinkGeo.Core.BufferCapType.md)|The bufferCapType used in the buffer logic.|
|featureUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the feature you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use as the distance. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles for the operation.|

---
#### `CloneDeep(IEnumerable<String>)`

**Summary**

   *This method clones the entire structure, creating a totally separate copy.*

**Remarks**

   *This method will return a complete copy of the Feature. As this is a deep clone, there are no shared references between the source and the copy.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns a clone of the entire structure, creating a totally separate copy.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|This parameter represents the columnar data fields that you wish to include in the clone.|

---
#### `CloneDeep(ReturningColumnsType)`

**Summary**

   *This method clones the entire structure, creating a totally separate copy.*

**Remarks**

   *This method will return a complete copy of the Feature. As this is a deep clone, there are no shared references between the source and the copy.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns a clone of the entire structure, creating a totally separate copy.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select a type from the ReturningColumnsType that you wish to return with.|

---
#### `CloneDeep()`

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
#### `Contains(Feature)`

**Summary**

   *This method returns if the targetFeature lies within the interior of the current feature.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the targetFeature lies within the interior of the current feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `CreateFeatureFromGeoJson(String)`

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
|geoJson|`String`|N/A|

---
#### `CreateFeatureFromWellKnownData(Byte[])`

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
|wellKnownBinary|`Byte[]`|N/A|

---
#### `CreateFeatureFromWellKnownData(String)`

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
|wellKnownText|`String`|N/A|

---
#### `Crosses(Feature)`

**Summary**

   *This method returns if the current feature and the targetFeature share some but not all interior points.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current feature and the targetFeature share some but not all interior points.|

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

   *This method returns the bounding box of the Feature.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box of the Feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetConvexHull()`

**Summary**

   *This method returns the convex hull of the feature, defined as the smallest convex ring that contains all of the points in the feature.*

**Remarks**

   *This method is useful when you want to create a perimeter around the shape. For example, if you had a MultiPolygon that represented buildings on a campus, you could easily get the convex hull of the buildings and determine the perimeter of all of the buildings together. This also works with MultiPoint shapes, where each point may represent a certain type of person you are doing statistics on. With convex hull, you can get an idea of the regions those points are located in.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns the convex hull of the feature, defined as the smallest convex ring that contains all of the points in the feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetDifference(Feature)`

**Summary**

   *This method returns the difference between current feature and the specified feature, defined as the set of all points which lie in the current feature but not in the targetFeature.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The return type is a Feature that is the set of all points which lie in the current feature but not in the target feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feture you are trying to find the difference with.|

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

   *This method returns the intersection of the current feature and the target feature, defined as the set of all points which lie in both the current feature and the target feature.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The return type is a Feature that contains the set of all points which lie in both the current feature and the target feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the intersection with.|

---
#### `GetInvalidReason()`

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
#### `GetShape()`

**Summary**

   *This method returns the shape class that represents the Feature.*

**Remarks**

   *This method allows you to get a shape class from a Feature. Because the Feature stores the geometry for itself in well-known binary, it may take some time to generate a shape class if the geometry is complex.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns the shape class that represents the Feature.|

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
#### `GetWellKnownBinary()`

**Summary**

   *This method returns the well-known binary that represents the Feature.*

**Remarks**

   *This will return a copy of the well-known binary that represents the Feature.*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|This method returns the well-known binary that represents the Feature.|

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

   *This method returns the well-known text that represents the Feature.*

**Remarks**

   *This method allows you to get the well-known text from a Feature. Because the Feature stores the geometry for itself in well-known binary, it may take some time to generate the text if the geometry is complex.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns the well-known text that represents the Feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWellKnownText(RingOrder)`

**Summary**

   *This method returns the well-known text representation of this feature.*

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

   *This method returns the well known type that represents the Feature.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|This method returns the well known type that represents the Feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Intersects(Feature)`

**Summary**

   *This method returns if the current feature and the targetFeature have at least one point in common.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current feature and the targetFeature have at least one point in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature you wish to compare the current one to.|

---
#### `IsDisjointed(Feature)`

**Summary**

   *This method returns if the current feature and the targetFeature have no points in common.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current feature and the targetFeature have no points in common. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you wish to compare the current one to.|

---
#### `IsGeometryValid()`

**Summary**

   *Using NTS to retrieve the geometry validation.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|returns True if valid.  Otherwise, for example in the case of self intersection, returns false.  The reason for being invalid can be found with GetInvalidReason()|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `IsTopologicallyEqual(Feature)`

**Summary**

   *This method returns if the current feature and the targetFeature are topologically equal.*

**Remarks**

   *Topologically equal means that the shapes are essentially the same. For example, let's say you have a line with two points, point A and point B. You also have another line that is made up of point A, point B and point C. Point A of line one shares the same vertex as point A of line two, and point B of line one shares the same vertex as point C of line two. They are both straight lines, so point B of line two would lie on the first line. Essentially the two lines are the same, with line 2 having just one extra point. Topologically they are the same line, so this method would return true. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current feature and the targetFeature are topologically equal.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `IsWithin(Feature)`

**Summary**

   *This method returns if the current feature lies within the interior of the targetFeature.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current feature lies within the interior of the targetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `Overlaps(Feature)`

**Summary**

   *This method returns if the current feature and the targetFeature share some but not all points in common.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns if the current feature and the targetFeature share some but not all points in common.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature that contains a shape you wish to compare the current one to.|

---
#### `SetWellKnownBinary(Byte[])`

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
#### `Touches(Feature)`

**Summary**

   *This method returns of the current feature and the targetFeature have at least one boundary point in common, but no interior points.*

**Remarks**

   *As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns of the current feature and the targetFeature have at least one boundary point in common, but no interior points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The targetFeature which contains a shape that you wish to compare the current one to.|

---
#### `Union(Feature)`

**Summary**

   *This method returns the union of the current feature and the target feature, defined as the set of all points in the current feature or the target feature.*

**Remarks**

   *This is useful for adding area features together to form a larger area shape.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The return type is a Feature that contains the set of all points which lie in the current feature or the target feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the union with.|

---
#### `Union(IEnumerable<Feature>)`

**Summary**

   *This method returns the union of the specified features.*

**Remarks**

   *This is useful for adding area features together to form a larger area feature.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The return type is a Feature that contains the set of all points that lie within the features you specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeatures|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The features you are trying to find the union with.|

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
#### `GetShapeCore()`

**Summary**

   *This method returns the shape class that represents the Feature.*

**Remarks**

   *This method allows you to get a shape class from a Feature. Because the Feature stores the geometry for itself in well-known binary, it may take some time to generate a shape class if the geometry is complex.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns the shape class that represents the Feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWellKnownBinaryCore(RingOrder,WkbByteOrder)`

**Summary**

   *This method returns a byte array that represents the feature in well-known binary.*

**Remarks**

   *This method returns a byte array that represents the feature in well-known binary. Well-known binary allows you to describe geometries as a binary array. Well-known binary is useful when you want to save a geometry in an efficient format using as little space as possible. An alternative to well-known binary is well-known text, which is a textual representation of a geometry object. We have methods that work with well known text as well. Overriding: Please ensure that you validate the parameters being passed in and raise the exceptions defined above.*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|This method returns a byte array that represents the feature in well-known binary.|

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


