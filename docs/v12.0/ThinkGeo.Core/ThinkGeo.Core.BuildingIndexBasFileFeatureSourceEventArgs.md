# BuildingIndexBasFileFeatureSourceEventArgs


## Inheritance Hierarchy

+ `Object`
  + `EventArgs`
    + **`BuildingIndexBasFileFeatureSourceEventArgs`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`BuildingIndexBasFileFeatureSourceEventArgs()`](#buildingindexbasfilefeaturesourceeventargs)|
|[`BuildingIndexBasFileFeatureSourceEventArgs(Int32,Int64,Feature,DateTime,Boolean)`](#buildingindexbasfilefeaturesourceeventargsint32int64featuredatetimeboolean)|
|[`BuildingIndexBasFileFeatureSourceEventArgs(Int32,Int64,Feature,DateTime,Boolean,String)`](#buildingindexbasfilefeaturesourceeventargsint32int64featuredatetimebooleanstring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Cancel`](#cancel)|`Boolean`|Gets or sets to see if we need to cancel the building index of current record.|
|[`CurrentFeature`](#currentfeature)|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Gets the current feature for building rTree index.|
|[`CurrentRecordOffset`](#currentrecordoffset)|`Int64`|Gets the current record index for building rTree index.|
|[`RecordCount`](#recordcount)|`Int32`|Gets the total record count to build rTree index.|
|[`ShapePathFilename`](#shapepathfilename)|`String`|N/A|
|[`StartProcessTime`](#startprocesstime)|`DateTime`|Gets the starting process time for building the index.|

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
|[`BuildingIndexBasFileFeatureSourceEventArgs()`](#buildingindexbasfilefeaturesourceeventargs)|
|[`BuildingIndexBasFileFeatureSourceEventArgs(Int32,Int64,Feature,DateTime,Boolean)`](#buildingindexbasfilefeaturesourceeventargsint32int64featuredatetimeboolean)|
|[`BuildingIndexBasFileFeatureSourceEventArgs(Int32,Int64,Feature,DateTime,Boolean,String)`](#buildingindexbasfilefeaturesourceeventargsint32int64featuredatetimebooleanstring)|

### Protected Constructors


### Public Properties

#### `Cancel`

**Summary**

   *Gets or sets to see if we need to cancel the building index of current record.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CurrentFeature`

**Summary**

   *Gets the current feature for building rTree index.*

**Remarks**

   *N/A*

**Return Value**

[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)

---
#### `CurrentRecordOffset`

**Summary**

   *Gets the current record index for building rTree index.*

**Remarks**

   *N/A*

**Return Value**

`Int64`

---
#### `RecordCount`

**Summary**

   *Gets the total record count to build rTree index.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `ShapePathFilename`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `StartProcessTime`

**Summary**

   *Gets the starting process time for building the index.*

**Remarks**

   *N/A*

**Return Value**

`DateTime`

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


