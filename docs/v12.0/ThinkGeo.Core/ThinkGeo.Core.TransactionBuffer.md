# TransactionBuffer


## Inheritance Hierarchy

+ `Object`
  + **`TransactionBuffer`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`TransactionBuffer()`](#transactionbuffer)|
|[`TransactionBuffer(Dictionary<String,Feature>,Collection<String>,Dictionary<String,Feature>)`](#transactionbufferdictionary<stringfeature>collection<string>dictionary<stringfeature>)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AddBuffer`](#addbuffer)|Dictionary<`String`,[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This property gets the dictionary buffer that holds InternalFeatures to be added.|
|[`AddColumnBuffer`](#addcolumnbuffer)|Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|N/A|
|[`DeleteBuffer`](#deletebuffer)|Collection<`String`>|This property gets the dictionary buffer that holds InternalFeatures to be deleted.|
|[`DeleteColumnBuffer`](#deletecolumnbuffer)|Collection<`String`>|N/A|
|[`EditBuffer`](#editbuffer)|Dictionary<`String`,[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This property gets the dictionary buffer that holds InternalFeatures to be updated.|
|[`UpdateColumnBuffer`](#updatecolumnbuffer)|Dictionary<`String`,[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`AddColumn(FeatureSourceColumn)`](#addcolumnfeaturesourcecolumn)|
|[`AddFeature(Feature)`](#addfeaturefeature)|
|[`AddFeature(BaseShape)`](#addfeaturebaseshape)|
|[`AddFeature(BaseShape,Dictionary<String,String>)`](#addfeaturebaseshapedictionary<stringstring>)|
|[`Clear()`](#clear)|
|[`DeleteColumn(String)`](#deletecolumnstring)|
|[`DeleteFeature(String)`](#deletefeaturestring)|
|[`EditFeature(Feature)`](#editfeaturefeature)|
|[`EditFeature(BaseShape)`](#editfeaturebaseshape)|
|[`EditFeature(BaseShape,Dictionary<String,String>)`](#editfeaturebaseshapedictionary<stringstring>)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|
|[`UpdateColumn(String,FeatureSourceColumn)`](#updatecolumnstringfeaturesourcecolumn)|

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
|[`TransactionBuffer()`](#transactionbuffer)|
|[`TransactionBuffer(Dictionary<String,Feature>,Collection<String>,Dictionary<String,Feature>)`](#transactionbufferdictionary<stringfeature>collection<string>dictionary<stringfeature>)|

### Protected Constructors


### Public Properties

#### `AddBuffer`

**Summary**

   *This property gets the dictionary buffer that holds InternalFeatures to be added.*

**Remarks**

   *It is recommended that you use this dictionary for reviewing and not for adding new items. The reason is that the Add, Delete and Edit methods to various validation checks. For example if you call the DeleteFeature twice it will handle the case that you really only want to delete the record once. Another example is if you edit a record twice it will replace the existing edit with the new one.*

**Return Value**

Dictionary<`String`,[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>

---
#### `AddColumnBuffer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>

---
#### `DeleteBuffer`

**Summary**

   *This property gets the dictionary buffer that holds InternalFeatures to be deleted.*

**Remarks**

   *It is recommended that you use this dictionary for reviewing and not for adding new items. The reason is that the Add, Delete and Edit methods to various validation checks. For example if you call the DeleteFeature twice it will handle the case that you really only want to delete the record once. Another example is if you edit a record twice it will replace the existing edit with the new one.*

**Return Value**

Collection<`String`>

---
#### `DeleteColumnBuffer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `EditBuffer`

**Summary**

   *This property gets the dictionary buffer that holds InternalFeatures to be updated.*

**Remarks**

   *It is recommended that you use this dictionary for reviewing and not for adding new items. The reason is that the Add, Delete and Edit methods to various validation checks. For example if you call the DeleteFeature twice it will handle the case that you really only want to delete the record once. Another example is if you edit a record twice it will replace the existing edit with the new one.*

**Return Value**

Dictionary<`String`,[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>

---
#### `UpdateColumnBuffer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)>

---

### Protected Properties


### Public Methods

#### `AddColumn(FeatureSourceColumn)`

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
|featureSourceColumn|[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)|N/A|

---
#### `AddFeature(Feature)`

**Summary**

   *This method allows you to add InternalFeatures to the transaction buffer.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter represents the Feature you are adding to the transaction buffer.|

---
#### `AddFeature(BaseShape)`

**Summary**

   *This method allows you to add a shape into the buffer.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter represents the shape to be added.|

---
#### `AddFeature(BaseShape,Dictionary<String,String>)`

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
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|columnValues|Dictionary<`String`,`String`>|N/A|

---
#### `Clear()`

**Summary**

   *This method will clear all the items in AddBuffer, EditBuffer and DeleteBuffer.*

**Remarks**

   *This method will clear all the items in AddBuffer, EditBuffer and DeleteBuffer.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DeleteColumn(String)`

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
|columnName|`String`|N/A|

---
#### `DeleteFeature(String)`

**Summary**

   *This method allows you to add a placeholder to represent a Feature to be deleted.*

**Remarks**

   *This does not remove a feature from the TransactionBuffer but rather it add a "to be deleted record". In this way when the TransactionBuffer if processed we know what records need to be deleted.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|featureId|`String`|This parameter represents the unique Id for the specific Feature being passed in.|

---
#### `EditFeature(Feature)`

**Summary**

   *This method allows you to add a Feature to be updated.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter represents the Feature to be updated.|

---
#### `EditFeature(BaseShape)`

**Summary**

   *This method allows you to add a shape to be updated.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter represents the shape to be updated. The shape ID should be the same as the feature you are going to update.|

---
#### `EditFeature(BaseShape,Dictionary<String,String>)`

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
|baseShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|columnValues|Dictionary<`String`,`String`>|N/A|

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
#### `UpdateColumn(String,FeatureSourceColumn)`

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
|columnName|`String`|N/A|
|newFeatureSourceColumn|[`FeatureSourceColumn`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSourceColumn.md)|N/A|

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


