# EditTools


## Inheritance Hierarchy

+ `Object`
  + **`EditTools`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`EditTools(FeatureSource)`](#edittoolsfeaturesource)|

### Protected Constructors Summary


|Name|
|---|
|[`EditTools()`](#edittools)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsEditable`](#iseditable)|`Boolean`|This property returns whether the FeatureLayer allows edits or is read only.|
|[`IsInTransaction`](#isintransaction)|`Boolean`|This property returns true if the FeatureLayer is in a transaction and false if it is not.|
|[`IsTransactionLive`](#istransactionlive)|`Boolean`|This property returns true if the features currently modified in a transaction are expected to reflect their state when calling other methods on the FeatureLayer, such as spatial queries.|
|[`TransactionBuffer`](#transactionbuffer)|[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)|This property allows you get and set the transaction buffer.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Add(BaseShape)`](#addbaseshape)|
|[`Add(Feature)`](#addfeature)|
|[`Add(BaseShape,Dictionary<String,String>)`](#addbaseshapedictionary<stringstring>)|
|[`BeginTransaction()`](#begintransaction)|
|[`CommitTransaction()`](#committransaction)|
|[`Delete(String)`](#deletestring)|
|[`Equals(Object)`](#equalsobject)|
|[`GetDifference(String,AreaBaseShape)`](#getdifferencestringareabaseshape)|
|[`GetDifference(String,Feature)`](#getdifferencestringfeature)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`RollbackTransaction()`](#rollbacktransaction)|
|[`Rotate(String,PointShape,Single)`](#rotatestringpointshapesingle)|
|[`ScaleDown(String,Double)`](#scaledownstringdouble)|
|[`ScaleUp(String,Double)`](#scaleupstringdouble)|
|[`ToString()`](#tostring)|
|[`TranslateByDegree(String,Double,Double,GeographyUnit,DistanceUnit)`](#translatebydegreestringdoubledoublegeographyunitdistanceunit)|
|[`TranslateByOffset(String,Double,Double,GeographyUnit,DistanceUnit)`](#translatebyoffsetstringdoubledoublegeographyunitdistanceunit)|
|[`Union(String,AreaBaseShape)`](#unionstringareabaseshape)|
|[`Union(String,Feature)`](#unionstringfeature)|
|[`Update(BaseShape)`](#updatebaseshape)|
|[`Update(Feature)`](#updatefeature)|
|[`Update(BaseShape,Dictionary<String,String>)`](#updatebaseshapedictionary<stringstring>)|

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
|[`EditTools(FeatureSource)`](#edittoolsfeaturesource)|

### Protected Constructors

#### `EditTools()`

**Summary**

   *This is a constructor for the class.*

**Remarks**

   *This is the default constructor. It is protected and not meant to be used.*

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

#### `IsEditable`

**Summary**

   *This property returns whether the FeatureLayer allows edits or is read only.*

**Remarks**

   *This property is useful to check if a specific FeatureLayer accepts editing. If you call BeginTransaction and this property is false, then an exception will be raised.*

**Return Value**

`Boolean`

---
#### `IsInTransaction`

**Summary**

   *This property returns true if the FeatureLayer is in a transaction and false if it is not.*

**Remarks**

   *To enter a transaction, you must first call the BeginTransaction method of the FeatureLayer. It is possible that some FeatureLayers are read only and do not allow edits. To end a transaction, you must either call CommitTransaction or RollbackTransaction.*

**Return Value**

`Boolean`

---
#### `IsTransactionLive`

**Summary**

   *This property returns true if the features currently modified in a transaction are expected to reflect their state when calling other methods on the FeatureLayer, such as spatial queries.*

**Remarks**

   *The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

`Boolean`

---
#### `TransactionBuffer`

**Summary**

   *This property allows you get and set the transaction buffer.*

**Remarks**

   *N/A*

**Return Value**

[`TransactionBuffer`](../ThinkGeo.Core/ThinkGeo.Core.TransactionBuffer.md)

---

### Protected Properties


### Public Methods

#### `Add(BaseShape)`

**Summary**

   *This method adds a new Feature to an existing transaction.*

**Remarks**

   *This method adds a new Feature to an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction. The Transaction System The transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This string is the ID that will uniquely identify this Feature while it is in a transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter represents the new shape that will be added to the transaction.|

---
#### `Add(Feature)`

**Summary**

   *This method adds a new Feature to an existing transaction.*

**Remarks**

   *This method adds a new Feature to an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction. The Transaction System The transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`String`|This string is the ID that will uniquely identify this Feature while it is in a transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter represents the new Feature that will be added to the transaction.|

---
#### `Add(BaseShape,Dictionary<String,String>)`

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
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|columnValues|Dictionary<`String`,`String`>|N/A|

---
#### `BeginTransaction()`

**Summary**

   *This method starts a new transaction if the FeatureLayer allows editing.*

**Remarks**

   *This method is used to start a transaction, assuming that the FeatureLayer allows editing. There are some additional prerequisites to beginning a transaction, such as ensuring that a transaction is not already in progress. You must also be sure that the FeatureSource has been opened. The Transaction System The transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CommitTransaction()`

**Summary**

   *This method will commit the existing transaction to its underlying source of data.*

**Remarks**

   *This method will commit the existing transaction to its underlying source of data. It will then pass back the results of the commit, including any error(s) received. Finally, it will free up the internal memory cache of any InternalFeatures added, updated or deleted. You will need to ensure that you have started a transaction by calling BeginTransaction.The Transaction SystemThe transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|[`TransactionResult`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResult.md)|The returned decimalDegreesValue of this method is a TransactionResult class, which gives you the status of the transaction you just committed. It includes how many of the updates, adds, and deletes were successful and any errors that were encountered during the committing of the transaction.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Delete(String)`

**Summary**

   *This method deletes a Feature from an existing transaction.*

**Remarks**

   *This method deletes a Feature from an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction. The Transaction System The transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer. In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object. As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed. In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|id|`String`|This string is the Id of the feature in the FeatureLayer that you wish to delete.|

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
#### `GetDifference(String,AreaBaseShape)`

**Summary**

   *This method returns the difference between two shapes, which are defined as the set of all points that lie in the Feature but not in the targetShape.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This is the Feature you want to remove area from.|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the difference with.|

---
#### `GetDifference(String,Feature)`

**Summary**

   *This method returns the difference between two features, which are defined as the set of all points which lie in the Feature but not in the targetFeature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This is the Feature you want to remove area from.|
|targetAreaFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the difference with.|

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
#### `RollbackTransaction()`

**Summary**

   *This method will cancel an existing transaction. It will free up the internal memory cache of any InternalFeatures added, updated or deleted.*

**Remarks**

   *This method will cancel an existing transaction. It will free up the internal memory cache of any InternalFeatures added, updated or deleted. You will need to ensure that you have started a transaction by calling BeginTransaction.The Transaction SystemThe transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Rotate(String,PointShape,Single)`

**Summary**

   *This method rotates the Feature any number of degrees based on a pivot point.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource. This method rotates the Feature based on a pivot point by a number of degrees. By placing the pivot point in the center of the Feature, you can achieve in-place rotation. By moving the pivot point outside of the center of the Feature, you can translate the shape in a circular motion. Moving the pivot point further outside of the center will make the circular area larger.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter is the Feature you want to rotate.|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The pivotPoint represents the center of rotation.|
|degreeAngle|`Single`|The number of degrees of rotation, from 0 to 360.|

---
#### `ScaleDown(String,Double)`

**Summary**

   *This method decreases the size of the feature by the percentage given in the percentage parameter.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource. This method is useful when you would like to decrease the size of the Feature. Note that a larger percentage will scale the shape down faster as you apply the operation multiple times. There is also a ScaleUp method that will expand the shape as well.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter is the Id of the Feature you want to scale.|
|percentage|`Double`|This is the percentage by which to decrease the Feature's size.|

---
#### `ScaleUp(String,Double)`

**Summary**

   *This method increases the size of the feature by the percentage given in the percentage parameter.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource. This method is useful when you would like to increase the size of the Feature. Note that a larger percentage will scale the shape up faster as you apply the operation multiple times. There is also a ScaleDown method that will shrink the shape as well.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter is the Id of the Feature you want to scale.|
|percentage|`Double`|This is the percentage by which to increase the Feature's size.|

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
#### `TranslateByDegree(String,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves the Feature from one location to another based on a distance and a direction in degrees.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource. This method moves the Feature from one location to another based on angleInDegrees and the distance parameter. With this overload, it is important to note that the distance units are based on the specified distanceUnit parameter. For example, if your Feature is in decimal degrees and you call this method with a specified distanceUnit of miles, you're going to move this shape a number of miles based on the distance and angleInDegrees. In this way, you could easily move a shape in decimal degrees five miles to the north.If you pass a distance of 0, the operation is ignored.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter is the Feature you want to move.|
|distance|`Double`|The distance is the number of units to move the shape using the angle specified. The distance unit will be the DistanceUnit specified in the distanceUnit parameter. The distance must be greater than or equal to 0.|
|angleInDegrees|`Double`|A number between 0 and 360 degrees that represents the direction you wish to move the shape, with zero being up.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the measure of the translate. For example, if you select miles as your distanceUnit, then the distance will be calculated in miles.|

---
#### `TranslateByOffset(String,Double,Double,GeographyUnit,DistanceUnit)`

**Summary**

   *This method moves the Feature from one location to another based on a X and Y offset distance.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource. This method moves the Feature from one location to another based on an X and Y offset distance. With this overload, it is important to note that the distance units are based on the specified distanceUnit parameter. For example, if your Feature is in decimal degrees and you call this method with an X offset of 1 and a Y offset of 1, you're going to move this Feature one unit of the distanceUnit in the horizontal direction and one unit of the distanceUnit in the vertical direction. In this way, you could easily move a Feature in decimal degrees five miles on the X axis and 3 miles on the Y axis.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter is the Feature you want to move.|
|xOffset|`Double`|This is the number of horizontal units of movement in the DistanceUnit specified in the distanceUnit parameter.|
|yOffset|`Double`|This is the number of horizontal units of movement in the DistanceUnit specified in the distanceUnit parameter.|
|shapeUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the GeographicUnit of the shape you are performing the operation on.|
|offsetUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the DistanceUnit you would like to use as the measure of the translate. For example, if you select miles as your distanceUnit, then the xOffsetDistance and yOffsetDistance will be calculated in miles.|

---
#### `Union(String,AreaBaseShape)`

**Summary**

   *This method returns the union of the Feature and the target shapes, which are defined as the set of all points in the Feature or the target shape.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource. This is useful for adding area shapes together to form a larger area shape.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter is the Feature you want to add the new area to.|
|targetShape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|The shape you are trying to find the union with.|

---
#### `Union(String,Feature)`

**Summary**

   *This method returns the union of the Feature and the target features, which are defined as the set of all points in the Feature or the target shape.*

**Remarks**

   *This method is a helpful function that allows you to easily edit InternalFeatures directly in the FeatureSource without having to retrieve them, convert them to a shape, manipulate them and put them back into the FeatureSource. This is useful for adding area shapes together to form a larger area shape.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter is the Feature you want to add the new area to.|
|targetAreaFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you are trying to find the union with.|

---
#### `Update(BaseShape)`

**Summary**

   *This method updates a Feature in an existing transaction.*

**Remarks**

   *This method updates a Feature in an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction.The Transaction SystemThe transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|The shape you wish to update in the transaction. The Id of the Shape should be the feature Id which you wish to update.|

---
#### `Update(Feature)`

**Summary**

   *This method updates a Feature in an existing transaction.*

**Remarks**

   *This method updates a Feature in an existing transaction. You will need to ensure that you have started a transaction by calling BeginTransaction.The Transaction SystemThe transaction system of a FeatureLayer sits on top of the inherited implementation of any specific source, such as Oracle Spatial or Shape files. In this way, it functions the same way for every FeatureLayer. You start by calling BeginTransaction. This allocates a collection of in-memory change buffers that are used to store changes until you commit the transaction. So, for example, when you call the Add, Delete or Update method, the changes to the feature are stored in memory only. If for any reason you choose to abandon the transaction, you can call RollbackTransaction at any time and the in-memory buffer will be deleted and the changes will be lost. When you are ready to commit the transaction, you call CommitTransaction and the collections of changes are then passed to the CommitTransactionCore method and the implementer of the specific FeatureLayer is responsible for integrating your changes into the underlying FeatureLayer. By default the IsLiveTransaction property is set to false, which means that until you commit the changes, the FeatureLayer API will not reflect any changes that are in the temporary editing buffer.In the case where the IsLiveTransaction is set to true, then things function slightly differently. The live transaction concept means that all of the modifications you perform during a transaction are live from the standpoint of the querying methods on the object.As an example, imagine that you have a FeatureLayer that has 10 records in it. Next, you begin a transaction and then call GetAllFeatures.  The result would be 10 records. After that, you call a delete on one of the records and call the GetAllFeatures again.  This time you only get nine records, even though the transaction has not yet been committed. In the same sense, you could have added a new record or modified an existing one and those changes would be considered live, though not committed.In the case where you modify records -- such as expanding the size of a polygon -- those changes are reflected as well. For example, you expand a polygon by doubling its size and then do a spatial query that would not normally return the smaller record, but instead would return the larger records.  In this case, the larger records are returned. You can set this property to be false, as well; in which case, all of the spatially related methods would ignore anything that is currently in the transaction buffer waiting to be committed. In such a case, only after committing the transaction would the FeatureLayer reflect the changes.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The Feature you wish to update in the transaction.|

---
#### `Update(BaseShape,Dictionary<String,String>)`

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
|columnValues|Dictionary<`String`,`String`>|N/A|

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


