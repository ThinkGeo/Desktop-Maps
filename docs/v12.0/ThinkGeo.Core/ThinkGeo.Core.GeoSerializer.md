# GeoSerializer


## Inheritance Hierarchy

+ `Object`
  + **`GeoSerializer`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeoSerializer()`](#geoserializer)|
|[`GeoSerializer(GeoSerializationFormatter)`](#geoserializergeoserializationformatter)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Formatter`](#formatter)|[`GeoSerializationFormatter`](../ThinkGeo.Core/ThinkGeo.Core.GeoSerializationFormatter.md)|Gets or sets the Formatter. The Formatter determines what kind of serialized data will be created.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Deserialize(String,FileAccess)`](#deserializestringfileaccess)|
|[`Deserialize(Stream)`](#deserializestream)|
|[`Deserialize(String)`](#deserializestring)|
|[`Deserialize(Uri)`](#deserializeuri)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`Serialize(Object,String)`](#serializeobjectstring)|
|[`Serialize(Object,Stream)`](#serializeobjectstream)|
|[`Serialize(Object)`](#serializeobject)|
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
|[`GeoSerializer()`](#geoserializer)|
|[`GeoSerializer(GeoSerializationFormatter)`](#geoserializergeoserializationformatter)|

### Protected Constructors


### Public Properties

#### `Formatter`

**Summary**

   *Gets or sets the Formatter. The Formatter determines what kind of serialized data will be created.*

**Remarks**

   *N/A*

**Return Value**

[`GeoSerializationFormatter`](../ThinkGeo.Core/ThinkGeo.Core.GeoSerializationFormatter.md)

---

### Protected Properties


### Public Methods

#### `Deserialize(String,FileAccess)`

**Summary**

   *Recreates an object from a file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|The recreated object.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourcePathFilename|`String`|The file path to deserialize from.|
|readWriteMode|`FileAccess`|N/A|

---
#### `Deserialize(Stream)`

**Summary**

   *Recreates an object from a stream.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|The recreated object.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceStream|`Stream`|The Stream to deserialize from.|

---
#### `Deserialize(String)`

**Summary**

   *Recreates an object from a string.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|The recreated object.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceString|`String`|The string to deserialize from.|

---
#### `Deserialize(Uri)`

**Summary**

   *Recreates an object from an Url.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|The recreated object.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceUri|`Uri`|The Url to deserialize from.|

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
#### `Serialize(Object,String)`

**Summary**

   *Serialize an object and save the serialized data to a file.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|objectToSerialize|`Object`|The object to be serialized.|
|targetPathFilename|`String`|The path to save the serialized data to.|

---
#### `Serialize(Object,Stream)`

**Summary**

   *Serialize an object and save the serialized data to a stream.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|objectToSerialize|`Object`|The object to be serialized.|
|targetStream|`Stream`|The stream to save the serialized data into.|

---
#### `Serialize(Object)`

**Summary**

   *Serialize an object and save the serialized data to a string.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|The string that contains the serialized data.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|objectToSerialize|`Object`|The object to be serialized.|

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


