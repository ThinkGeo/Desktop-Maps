# GeoSerializationFormatter


## Inheritance Hierarchy

+ `Object`
  + **`GeoSerializationFormatter`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`GeoSerializationFormatter()`](#geoserializationformatter)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Encoding`](#encoding)|`Encoding`|set or get the readEncoding.|

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
|[`Load(Stream)`](#loadstream)|
|[`Save(GeoObjectModel,Stream)`](#savegeoobjectmodelstream)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`LoadCore(Stream)`](#loadcorestream)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`SaveCore(GeoObjectModel,Stream)`](#savecoregeoobjectmodelstream)|

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

#### `GeoSerializationFormatter()`

**Summary**

   *Creates an instance of GeoSerializationFormatter.*

**Remarks**

   *N/A*

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

#### `Encoding`

**Summary**

   *set or get the readEncoding.*

**Remarks**

   *N/A*

**Return Value**

`Encoding`

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
#### `Load(Stream)`

**Summary**

   *Load a stream into a GeoObjectModel.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoObjectModel`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectModel.md)|The GeoObjectModel recreated from the stream.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|stream|`Stream`|The Stream to load from.|

---
#### `Save(GeoObjectModel,Stream)`

**Summary**

   *Save serialized data into a stream.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|model|[`GeoObjectModel`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectModel.md)|The GeoObjectModel to create serialized data for.|
|stream|`Stream`|The Stream to save serialized data into.|

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
#### `LoadCore(Stream)`

**Summary**

   *Load a stream into a GeoObjectModel.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoObjectModel`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectModel.md)|The GeoObjectModel recreated from the stream.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|stream|`Stream`|The Stream to load from.|

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
#### `SaveCore(GeoObjectModel,Stream)`

**Summary**

   *Format a GeoObjectModel into serialized data, and save the data into a stream.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|model|[`GeoObjectModel`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectModel.md)|The GeoObjectModel to create serialized data for.|
|stream|`Stream`|The Stream to save serialized data into.|

---

### Public Events


