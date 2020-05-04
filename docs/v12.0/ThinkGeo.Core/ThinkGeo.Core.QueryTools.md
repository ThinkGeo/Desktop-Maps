# QueryTools


## Inheritance Hierarchy

+ `Object`
  + **`QueryTools`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`QueryTools(FeatureSource)`](#querytoolsfeaturesource)|

### Protected Constructors Summary


|Name|
|---|
|[`QueryTools()`](#querytools)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CanExecuteSqlQuery`](#canexecutesqlquery)|`Boolean`|This property specifies whether FeatureSource can excute a SQL query or not. If it is false, then it will throw an exception when the following APIs are called: ExecuteScalar, ExecuteNonQuery, ExecuteQuery|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`ExecuteNonQuery(String)`](#executenonquerystring)|
|[`ExecuteQuery(String)`](#executequerystring)|
|[`ExecuteScalar(String)`](#executescalarstring)|
|[`GetAllFeatures(IEnumerable<String>)`](#getallfeaturesienumerable<string>)|
|[`GetAllFeatures(ReturningColumnsType)`](#getallfeaturesreturningcolumnstype)|
|[`GetBoundingBoxById(String)`](#getboundingboxbyidstring)|
|[`GetBoundingBoxesByIds(IEnumerable<String>)`](#getboundingboxesbyidsienumerable<string>)|
|[`GetColumns()`](#getcolumns)|
|[`GetCount()`](#getcount)|
|[`GetFeatureById(String,IEnumerable<String>)`](#getfeaturebyidstringienumerable<string>)|
|[`GetFeatureById(String,ReturningColumnsType)`](#getfeaturebyidstringreturningcolumnstype)|
|[`GetFeaturesByColumnValue(String,String,ReturningColumnsType)`](#getfeaturesbycolumnvaluestringstringreturningcolumnstype)|
|[`GetFeaturesByColumnValue(String,String,IEnumerable<String>)`](#getfeaturesbycolumnvaluestringstringienumerable<string>)|
|[`GetFeaturesByColumnValue(String,String)`](#getfeaturesbycolumnvaluestringstring)|
|[`GetFeaturesByIds(IEnumerable<String>,IEnumerable<String>)`](#getfeaturesbyidsienumerable<string>ienumerable<string>)|
|[`GetFeaturesByIds(IEnumerable<String>,ReturningColumnsType)`](#getfeaturesbyidsienumerable<string>returningcolumnstype)|
|[`GetFeaturesContaining(BaseShape,IEnumerable<String>)`](#getfeaturescontainingbaseshapeienumerable<string>)|
|[`GetFeaturesContaining(BaseShape,ReturningColumnsType)`](#getfeaturescontainingbaseshapereturningcolumnstype)|
|[`GetFeaturesContaining(Feature,IEnumerable<String>)`](#getfeaturescontainingfeatureienumerable<string>)|
|[`GetFeaturesContaining(Feature,ReturningColumnsType)`](#getfeaturescontainingfeaturereturningcolumnstype)|
|[`GetFeaturesCrossing(BaseShape,IEnumerable<String>)`](#getfeaturescrossingbaseshapeienumerable<string>)|
|[`GetFeaturesCrossing(BaseShape,ReturningColumnsType)`](#getfeaturescrossingbaseshapereturningcolumnstype)|
|[`GetFeaturesCrossing(Feature,IEnumerable<String>)`](#getfeaturescrossingfeatureienumerable<string>)|
|[`GetFeaturesCrossing(Feature,ReturningColumnsType)`](#getfeaturescrossingfeaturereturningcolumnstype)|
|[`GetFeaturesDisjointed(BaseShape,IEnumerable<String>)`](#getfeaturesdisjointedbaseshapeienumerable<string>)|
|[`GetFeaturesDisjointed(BaseShape,ReturningColumnsType)`](#getfeaturesdisjointedbaseshapereturningcolumnstype)|
|[`GetFeaturesDisjointed(Feature,IEnumerable<String>)`](#getfeaturesdisjointedfeatureienumerable<string>)|
|[`GetFeaturesDisjointed(Feature,ReturningColumnsType)`](#getfeaturesdisjointedfeaturereturningcolumnstype)|
|[`GetFeaturesInsideBoundingBox(RectangleShape,IEnumerable<String>)`](#getfeaturesinsideboundingboxrectangleshapeienumerable<string>)|
|[`GetFeaturesInsideBoundingBox(RectangleShape,ReturningColumnsType)`](#getfeaturesinsideboundingboxrectangleshapereturningcolumnstype)|
|[`GetFeaturesIntersecting(BaseShape,IEnumerable<String>)`](#getfeaturesintersectingbaseshapeienumerable<string>)|
|[`GetFeaturesIntersecting(BaseShape,ReturningColumnsType)`](#getfeaturesintersectingbaseshapereturningcolumnstype)|
|[`GetFeaturesIntersecting(Feature,IEnumerable<String>)`](#getfeaturesintersectingfeatureienumerable<string>)|
|[`GetFeaturesIntersecting(Feature,ReturningColumnsType)`](#getfeaturesintersectingfeaturereturningcolumnstype)|
|[`GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`](#getfeaturesnearesttobaseshapegeographyunitint32ienumerable<string>)|
|[`GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,ReturningColumnsType)`](#getfeaturesnearesttobaseshapegeographyunitint32returningcolumnstype)|
|[`GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>)`](#getfeaturesnearesttofeaturegeographyunitint32ienumerable<string>)|
|[`GetFeaturesNearestTo(Feature,GeographyUnit,Int32,ReturningColumnsType)`](#getfeaturesnearesttofeaturegeographyunitint32returningcolumnstype)|
|[`GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`](#getfeaturesnearesttofeaturegeographyunitint32ienumerable<string>doubledistanceunit)|
|[`GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`](#getfeaturesnearesttobaseshapegeographyunitint32ienumerable<string>doubledistanceunit)|
|[`GetFeaturesOutsideBoundingBox(RectangleShape,IEnumerable<String>)`](#getfeaturesoutsideboundingboxrectangleshapeienumerable<string>)|
|[`GetFeaturesOutsideBoundingBox(RectangleShape,ReturningColumnsType)`](#getfeaturesoutsideboundingboxrectangleshapereturningcolumnstype)|
|[`GetFeaturesOverlapping(BaseShape,IEnumerable<String>)`](#getfeaturesoverlappingbaseshapeienumerable<string>)|
|[`GetFeaturesOverlapping(BaseShape,ReturningColumnsType)`](#getfeaturesoverlappingbaseshapereturningcolumnstype)|
|[`GetFeaturesOverlapping(Feature,IEnumerable<String>)`](#getfeaturesoverlappingfeatureienumerable<string>)|
|[`GetFeaturesOverlapping(Feature,ReturningColumnsType)`](#getfeaturesoverlappingfeaturereturningcolumnstype)|
|[`GetFeaturesTopologicalEqual(BaseShape,IEnumerable<String>)`](#getfeaturestopologicalequalbaseshapeienumerable<string>)|
|[`GetFeaturesTopologicalEqual(BaseShape,ReturningColumnsType)`](#getfeaturestopologicalequalbaseshapereturningcolumnstype)|
|[`GetFeaturesTopologicalEqual(Feature,IEnumerable<String>)`](#getfeaturestopologicalequalfeatureienumerable<string>)|
|[`GetFeaturesTopologicalEqual(Feature,ReturningColumnsType)`](#getfeaturestopologicalequalfeaturereturningcolumnstype)|
|[`GetFeaturesTouching(BaseShape,IEnumerable<String>)`](#getfeaturestouchingbaseshapeienumerable<string>)|
|[`GetFeaturesTouching(BaseShape,ReturningColumnsType)`](#getfeaturestouchingbaseshapereturningcolumnstype)|
|[`GetFeaturesTouching(Feature,IEnumerable<String>)`](#getfeaturestouchingfeatureienumerable<string>)|
|[`GetFeaturesTouching(Feature,ReturningColumnsType)`](#getfeaturestouchingfeaturereturningcolumnstype)|
|[`GetFeaturesWithin(BaseShape,IEnumerable<String>)`](#getfeatureswithinbaseshapeienumerable<string>)|
|[`GetFeaturesWithin(BaseShape,ReturningColumnsType)`](#getfeatureswithinbaseshapereturningcolumnstype)|
|[`GetFeaturesWithin(Feature,IEnumerable<String>)`](#getfeatureswithinfeatureienumerable<string>)|
|[`GetFeaturesWithin(Feature,ReturningColumnsType)`](#getfeatureswithinfeaturereturningcolumnstype)|
|[`GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`](#getfeatureswithindistanceofbaseshapegeographyunitdistanceunitdoubleienumerable<string>)|
|[`GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`](#getfeatureswithindistanceofbaseshapegeographyunitdistanceunitdoublereturningcolumnstype)|
|[`GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`](#getfeatureswithindistanceoffeaturegeographyunitdistanceunitdoubleienumerable<string>)|
|[`GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`](#getfeatureswithindistanceoffeaturegeographyunitdistanceunitdoublereturningcolumnstype)|
|[`GetFirstFeaturesWellKnownType()`](#getfirstfeatureswellknowntype)|
|[`GetHashCode()`](#gethashcode)|
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
|[`QueryTools(FeatureSource)`](#querytoolsfeaturesource)|

### Protected Constructors

#### `QueryTools()`

**Summary**

   *This is a constructor for the class.*

**Remarks**

   *This is the default constructor, though it is typically not intended to be used.*

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

#### `CanExecuteSqlQuery`

**Summary**

   *This property specifies whether FeatureSource can excute a SQL query or not. If it is false, then it will throw an exception when the following APIs are called: ExecuteScalar, ExecuteNonQuery, ExecuteQuery*

**Remarks**

   *The default implementation is false.*

**Return Value**

`Boolean`

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
#### `ExecuteNonQuery(String)`

**Summary**

   *Executes a SQL statement against a connection object.*

**Remarks**

   *You can use ExecuteNonQuery to perform catalog operations (for example, querying the structure of a database or creating database objects such as tables), or to change the data in a database by executing UPDATE, INSERT, or DELETE statements. Although ExecuteNonQuery does not return any rows, any output parameters or return values mapped to parameters are populated with data. For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|The number of rows affected.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The sqlStatement to be excuted.|

---
#### `ExecuteQuery(String)`

**Summary**

   *Executes the query and returns the result returned by the query.*

**Remarks**

   *Use the ExcuteScalar method to retrieve a single value from the database. This requires less code than use the ExcuteQuery method and then performing the operations necessary to generate the single value using the data.*

**Return Value**

|Type|Description|
|---|---|
|`DataTable`|The result set in the format of dataTable.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The SQL statement to be excuted.|

---
#### `ExecuteScalar(String)`

**Summary**

   *Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.*

**Remarks**

   *Use the ExcuteScalar method to retrieve a single value from the database. This requires less code than use the ExcuteQuery method and then performing the operations necessary to generate the single value using the data.*

**Return Value**

|Type|Description|
|---|---|
|`Object`|The first column of the first row in the result set.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sqlStatement|`String`|The SQL statement to be excuted.|

---
#### `GetAllFeatures(IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures in the FeatureSource.*

**Remarks**

   *This method returns all of the InternalFeatures in the FeatureSource. It will return whatever is returned by the GetAllFeaturesCore method, along with any of the additions or subtractions made if you are in a transaction and that transaction is configured to be live. The main purpose of this method is to be the anchor of all of our default virtual implementations within this class. We as the framework developers wanted to provide you the user with as much default virtual implementation as possible. To do this, we needed a way to get access to all of the features. For example, let's say we want to create a default implementation for finding all of the InternalFeatures in a bounding box. Because this is an abstract class, we do not know the specifics of the underlying data or how its spatial indexes work. What we do know is that if we get all of the records, then we can brute-force the answer. In this way, if you inherited from this class and only implemented this one method, we can provide default implementations for virtually every other API. While this is nice for you the developer if you decide to create your own FeatureSource, it comes with a price: namely, it is very inefficient. In the example we just discussed (about finding all of the InternalFeatures in a bounding box), we would not want to look at every record to fulfil this method. Instead, we would want to override the GetFeaturesInsideBoundingBoxCore and implement specific code that would be faster. For example, in Oracle Spatial there is a specific SQL statement to perform this operation very quickly. The same holds true with other specific FeatureSource examples. Most default implementations in the FeatureSource call the GetFeaturesInsideBoundingBoxCore, which by default calls the GetAllFeaturesCore. It is our advice that if you create your own FeatureSource that you ALWAYS override the GetFeatureInsideBoundingBox. This will ensure that nearly every other API will operate efficiently. Please see the specific API to determine what method it uses. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of all of the InternalFeatures in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetAllFeatures(ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures in the FeatureSource.*

**Remarks**

   *This method returns all of the InternalFeatures in the FeatureSource. It will return whatever is returned by the GetAllFeaturesCore method, along with any of the additions or subtractions made if you are in a transaction and that transaction is configured to be live. The main purpose of this method is to be the anchor of all of our default virtual implementations within this class. We as the framework developers wanted to provide you the user with as much default virtual implementation as possible. To do this, we needed a way to get access to all of the features. For example, let's say we want to create a default implementation for finding all of the InternalFeatures in a bounding box. Because this is an abstract class, we do not know the specifics of the underlying data or how its spatial indexes work. What we do know is that if we get all of the records, then we can brute-force the answer. In this way, if you inherited from this class and only implemented this one method, we can provide default implementations for virtually every other API. While this is nice for you the developer if you decide to create your own FeatureSource, it comes with a price: namely, it is very inefficient. In the example we just discussed (about finding all of the InternalFeatures in a bounding box), we would not want to look at every record to fulfil this method. Instead, we would want to override the GetFeaturesInsideBoundingBoxCore and implement specific code that would be faster. For example, in Oracle Spatial there is a specific SQL statement to perform this operation very quickly. The same holds true with other specific FeatureSource examples. Most default implementations in the FeatureSource call the GetFeaturesInsideBoundingBoxCore, which by default calls the GetAllFeaturesCore. It is our advice that if you create your own FeatureSource that you ALWAYS override the GetFeatureInsideBoundingBox. This will ensure that nearly every other API will operate efficiently. Please see the specific API to determine what method it uses. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of all of the InternalFeatures in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetBoundingBoxById(String)`

**Summary**

   *This method returns the bounding box for the Id specified.*

**Remarks**

   *This method returns the bounding box for the Id specified.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box for the Id specified.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the unique Id of the feature for which you want to find the bounding box.|

---
#### `GetBoundingBoxesByIds(IEnumerable<String>)`

**Summary**

   *This method returns a collection of bounding boxes based on the collection of Ids provided.*

**Remarks**

   *This method returns a collection of bounding boxes based on the collection of Ids provided.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|This method returns a collection of bounding boxes based on the collection of Ids provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter is the collection of Ids you want to find.|

---
#### `GetColumns()`

**Summary**

   *This method returns the collection of columns for this FeatureSource.*

**Remarks**

   *This method returns the collection of columns for this FeatureSource.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|This method returns the collection of columns for this FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCount()`

**Summary**

   *This method returns the count of all of the InternalFeatures in the FeatureSource.*

**Remarks**

   *This method returns the count of all of the InternalFeatures in the FeatureSource.*

**Return Value**

|Type|Description|
|---|---|
|`Int64`|This method returns the count of all of the InternalFeatures in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetFeatureById(String,IEnumerable<String>)`

**Summary**

   *This method returns an InternalFeature based on an Id provided.*

**Remarks**

   *This method returns an InternalFeature based on an Id provided.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns an InternalFeature based on an Id provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the unique Id for the feature you want to find.|
|returningColumnNames|IEnumerable<`String`>|This parameter is a list of column names you want returned with the Feature.|

---
#### `GetFeatureById(String,ReturningColumnsType)`

**Summary**

   *This method returns an InternalFeature based on an Id provided.*

**Remarks**

   *This method returns an InternalFeature based on an Id provided.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This method returns an InternalFeature based on an Id provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This parameter is the unique Id for the feature you want to find.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter is a list of column names you want returned with the Feature.|

---
#### `GetFeaturesByColumnValue(String,String,ReturningColumnsType)`

**Summary**

   *Get all of the features by passing a columnName and a specified columValue.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returnning features matches the columnValue.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|The specified columnName to match the columnValue.|
|columnValue|`String`|The specified columnValue to match those returning features.|
|returningColumnType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesByColumnValue(String,String,IEnumerable<String>)`

**Summary**

   *Get all of the features by passing a columnName and a specified columValue.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returnning features matches the columnValue.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|The specified columnName to match the columnValue.|
|columnValue|`String`|The specified columnValue to match those returning features.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesByColumnValue(String,String)`

**Summary**

   *Get all of the features by passing a columnName and a specified columValue.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The returnning features matches the columnValue.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|The specified columnName to match the columnValue.|
|columnValue|`String`|The specified columnValue to match those returning features.|

---
#### `GetFeaturesByIds(IEnumerable<String>,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures based on the collection of Ids provided.*

**Remarks**

   *This method returns a collection of InternalFeatures based on the collection of Ids provided.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures based on the collection of Ids provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter is the collection of Ids you want to find.|
|returningColumnNames|IEnumerable<`String`>|This parameter is a list of column names you want returned with the Features.|

---
#### `GetFeaturesByIds(IEnumerable<String>,ReturningColumnsType)`

**Summary**

   *This method returns a collection of InternalFeatures based on the collection of Ids provided.*

**Remarks**

   *This method returns a collection of InternalFeatures based on the collection of Ids provided.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures based on the collection of Ids provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ids|IEnumerable<`String`>|This parameter is the collection of Ids you want to find.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter is a list of column names you want returned with the Features.|

---
#### `GetFeaturesContaining(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that contain the target shape.*

**Remarks**

   *This method returns all of the InternalFeatures that contain the specified target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that contain the TargetShape you passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesContaining(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures that contain the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.ReturningColumnsType:NoColumns - This method ensures that the returning features contain no column values.AllColumns - This method ensures that the returning features contain all column values.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesContaining(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures that contain the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.ReturningColumnsType:NoColumns - This method ensures that the returning features contain no column values.AllColumns - This method ensures that the returning features contain all column values.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesContaining(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures based on the target Feature and the spatial query type specified.*

**Remarks**

   *This method returns all of the InternalFeatures that contain the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.ReturningColumnsType:NoColumns - This method ensures that the returning features contain no column values.AllColumns - This method ensures that the returning features contain all column values.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesCrossing(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that cross the target shape.*

**Remarks**

   *This method returns all of the InternalFeatures that cross the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Crossing - The Geometries share some but not all interior points, and the dimension of the intersection is less than that of at least one of the Geometries.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that cross the TargetShape you passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesCrossing(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that cross the target shape.*

**Remarks**

   *This method returns all of the InternalFeatures that cross the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.ReturningColumnsType:NoColumns - This method ensures that the returning features contain no column values.AllColumns - This method ensures that the returning features contain all column values.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesCrossing(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that cross the target Feature.*

**Remarks**

   *This method returns all of the Internalfeatures that cross the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.ReturningColumnsType:NoColumns - This method ensures that the returning features contain no column values.AllColumns - This method ensures that the returning features contain all column values.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesCrossing(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that cross the target Feature.*

**Remarks**

   *This method returns all of the Internalfeatures that cross the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.ReturningColumnsType:NoColumns - This method ensures that the returning features contain no column values.AllColumns - This method ensures that the returning features contain all column values.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesDisjointed(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that disjoint the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that disjoint the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Disjoint - The Geometries have no point in common.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesDisjointed(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that disjoint the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that disjoint the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Disjoint - The Geometries have no point in common.ReturningColumnsType:NoColumns - This method ensures that the returning features contain no column values.AllColumns - This method ensures that the returning features contain all column values.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesDisjointed(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that disjoint the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that disjoint the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Disjoint - The Geometries have no point in common.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesDisjointed(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that disjoint the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that disjoint the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Disjoint - The Geometries have no point in common.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the columns contained in the return features.|

---
#### `GetFeaturesInsideBoundingBox(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that are inside of the boundingBox.*

**Remarks**

   *This method returns all of the InternalFeatures that are inside of the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that are inside of the target rectangle shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies the target boundingBox used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesInsideBoundingBox(RectangleShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that are inside of the boundingBox.*

**Remarks**

   *This method returns all of the InternalFeatures that are inside of the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that are inside of the target rectangle shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies the target boundingBox used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesIntersecting(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that intersect the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that intersect the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Intersecting - The Geometries have at least one point in common (the inverse of Disjoint).*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesIntersecting(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that intersect the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that intersect the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Intersecting - The Geometries have at least one point in common (the inverse of Disjoint).*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesIntersecting(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that intersect the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that intersect the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Intersecting - The Geometries have at least one point in common (the inverse of Disjoint).*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesIntersecting(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that intersect the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that intersect the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Intersecting - The Geometries have at least one point in common (the inverse of Disjoint).*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided create a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you want to find close InternalFeatures to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,ReturningColumnsType)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided create a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you want to find close InternalFeatures to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetFeature.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided create a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature you want to find close InternalFeatures to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the feature.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,ReturningColumnsType)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetFeature.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided create a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature you want to find close InternalFeatures to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the feature.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesNearestTo(Feature,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is feature you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|
|searchRadius|`Double`|Limit the maximize distance proximately to search closest records.|
|unitOfSearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit of searchRadius parameter.|

---
#### `GetFeaturesNearestTo(BaseShape,GeographyUnit,Int32,IEnumerable<String>,Double,DistanceUnit)`

**Summary**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape.*

**Remarks**

   *This method returns a user defined number of InternalFeatures that are closest to the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a small bounding box around the TargetShape and then queries the features inside of it. If we reach the number of items to find, then we measure the returned InternalFeatures to find the nearest. If we do not find enough records, we scale up the bounding box and try again. As you can see, this is not the most efficient method. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesNearestCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a user defined number of InternalFeatures that are closest to the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape you want to find InternalFeatures close to.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of measurement that the TargetShape and the FeatureSource are in, such as feet, meters, etc.|
|maxItemsToFind|`Int32`|This parameter defines how many close InternalFeatures to find around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|
|searchRadius|`Double`|Limit the maximize distance proximately to search closest records.|
|unitOfSearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit of searchRadius parameter.|

---
#### `GetFeaturesOutsideBoundingBox(RectangleShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that are outside of the boundingBox.*

**Remarks**

   *This method returns all of the InternalFeatures that are outside of the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that are outside of the target rectangle shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies the target boundingBox used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesOutsideBoundingBox(RectangleShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that are outside of the boundingBox.*

**Remarks**

   *This method returns all of the InternalFeatures that are outside of the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that are outside of the target rectangle shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies the target boundingBox used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesOverlapping(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that overlap the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that overlap the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Overlapping - The Geometries share some but not all points in common, and the intersection has the same dimension as the Geometries themselves.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesOverlapping(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that overlap the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that overlap the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Overlapping - The Geometries share some but not all points in common, and the intersection has the same dimension as the Geometries themselves.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesOverlapping(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that overlap the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that overlap the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Overlapping - The Geometries share some but not all points in common, and the intersection has the same dimension as the Geometries themselves.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesOverlapping(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that overlap the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that overlap the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.Overlapping - The Geometries share some but not all points in common, and the intersection has the same dimension as the Geometries themselves.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTopologicalEqual(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that topologicalEqual the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that topologicalEqual the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTopologicalEqual(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that topologicalEqual the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that topologicalEqual the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTopologicalEqual(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that topologicalEqual the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that topologicalEqual the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTopologicalEqual(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that topologicalEqual the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that topologicalEqual the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTouching(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that touch the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that touch the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTouching(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that touch the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that touch the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTouching(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that touch the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that touch the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesTouching(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that touch the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that touch the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesWithin(BaseShape,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that are within the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that are within the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesWithin(BaseShape,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that are within the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that are within the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter specifies the target shape used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesWithin(Feature,IEnumerable<String>)`

**Summary**

   *This method returns all of the InternalFeatures that are within the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that are within the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesWithin(Feature,ReturningColumnsType)`

**Summary**

   *This method returns all of the InternalFeatures that are within the target Feature.*

**Remarks**

   *This method returns all of the InternalFeatures that are within the target shape. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The return value is a collection of InternalFeatures that match the spatial query you executed based on the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature used in the spatial query.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter specifies the column values in the return features.|

---
#### `GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the unit of the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOf(BaseShape,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the unit of the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,IEnumerable<String>)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetFeature.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the unit of the distance parameter, such as feet, miles, kilometers, etc.|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNames|IEnumerable<`String`>|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFeaturesWithinDistanceOf(Feature,GeographyUnit,DistanceUnit,Double,ReturningColumnsType)`

**Summary**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetFeature.*

**Remarks**

   *This method returns a collection of InternalFeatures that are within a certain distance of the TargetShape. It is important to note that the TargetShape and the FeatureSource must use the same unit, such as feet or meters. If they do not, then the results will not be predictable or correct. If there is a current transaction and it is marked as live, then the results will include any transaction Feature that applies. The implementation we provided creates a bounding box around the TargetShape using the distance supplied and then queries the features inside of it. This may not be the most efficient method for this operation. If your underlying data provider exposes a more efficient way, we recommend you override the Core version of this method and implement it. The default implementation of GetFeaturesWithinDistanceOfCore uses the GetFeaturesInsideBoundingBoxCore method for speed. We strongly recommend that you provide your own implementation for this method that will be more efficient. When you override the GetFeaturesInsideBoundingBoxCore method, we recommend that you use any spatial indexes you have at your disposal to make this method as fast as possible. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a collection of InternalFeatures that are within a certain distance of the TargetFeature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you wish to find InternalFeatures within a distance of.|
|unitOfData|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of data that the FeatureSource and TargetShape are in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This parameter specifies the unit of the distance parameter, such as feet, miles, kilometers, etc..|
|distance|`Double`|This parameter specifies the distance in which to find InternalFeatures around the TargetShape.|
|returningColumnNamesType|[`ReturningColumnsType`](../ThinkGeo.Core/ThinkGeo.Core.ReturningColumnsType.md)|This parameter allows you to select the field names of the column data you wish to return with each Feature.|

---
#### `GetFirstFeaturesWellKnownType()`

**Summary**

   *This method returns the well known type that represents the first feature from FeatureSource.*

**Remarks**

   *The default implementation of GetCountCore uses the GetAllRFeaturesCore method to get WellKnownType of the first feature from all features. We strongly recommend that you provide your own implementation for this method that will be more efficient.*

**Return Value**

|Type|Description|
|---|---|
|[`WellKnownType`](../ThinkGeo.Core/ThinkGeo.Core.WellKnownType.md)|This method returns the well known type that represents the first feature from FeatureSource.|

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


