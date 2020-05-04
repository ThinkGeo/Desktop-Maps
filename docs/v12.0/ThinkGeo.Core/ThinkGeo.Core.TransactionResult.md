# TransactionResult


## Inheritance Hierarchy

+ `Object`
  + **`TransactionResult`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`TransactionResult()`](#transactionresult)|
|[`TransactionResult(Int32,Int32,Dictionary<String,String>,TransactionResultStatus)`](#transactionresultint32int32dictionary<stringstring>transactionresultstatus)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FailureReasons`](#failurereasons)|Dictionary<`String`,`String`>|This property gets and sets the dictionary that contains the reasons for failure.|
|[`TotalFailureCount`](#totalfailurecount)|`Int32`|This property gets and sets the total number of records that we committed unsuccessfully.|
|[`TotalSuccessCount`](#totalsuccesscount)|`Int32`|This property gets and sets the total number of records that we committed successfully.|
|[`TransactionResultStatus`](#transactionresultstatus)|[`TransactionResultStatus`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResultStatus.md)|This property gets and sets the result status of the transaction.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
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
|[`TransactionResult()`](#transactionresult)|
|[`TransactionResult(Int32,Int32,Dictionary<String,String>,TransactionResultStatus)`](#transactionresultint32int32dictionary<stringstring>transactionresultstatus)|

### Protected Constructors


### Public Properties

#### `FailureReasons`

**Summary**

   *This property gets and sets the dictionary that contains the reasons for failure.*

**Remarks**

   *If there are failing records we suggest you add the failure reasons to this dictionary. It is also suggested that you use the FeatureId as the key of the Dictionary.*

**Return Value**

Dictionary<`String`,`String`>

---
#### `TotalFailureCount`

**Summary**

   *This property gets and sets the total number of records that we committed unsuccessfully.*

**Remarks**

   *None*

**Return Value**

`Int32`

---
#### `TotalSuccessCount`

**Summary**

   *This property gets and sets the total number of records that we committed successfully.*

**Remarks**

   *None*

**Return Value**

`Int32`

---
#### `TransactionResultStatus`

**Summary**

   *This property gets and sets the result status of the transaction.*

**Remarks**

   *This property returns the results of the transaction. If all of the records committed fine then you get a success status. If any of the records fail then you get a failure status though some of the records may have committed.*

**Return Value**

[`TransactionResultStatus`](../ThinkGeo.Core/ThinkGeo.Core.TransactionResultStatus.md)

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


