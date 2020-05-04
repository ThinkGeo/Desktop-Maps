# CloudMapClient


## Inheritance Hierarchy

+ `Object`
  + **`CloudMapClient`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`CloudMapClient(String,String)`](#cloudmapclientstringstring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ClientId`](#clientid)|`String`|A GIS Server NativeConfidential client ID.|
|[`ClientSecret`](#clientsecret)|`String`|A GIS Server NativeConfidential client secret.|
|[`TimeoutInSeconds`](#timeoutinseconds)|`Int32`|The request timeout, default 100 seconds|
|[`Uris`](#uris)|Collection<`Uri`>|N/A|
|[`WebProxy`](#webproxy)|`WebProxy`|The proxy used for requesting a Web Response.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`CreateWebRequest(String,String,String,String,Int32,IWebProxy,Boolean)`](#createwebrequeststringstringstringstringint32iwebproxyboolean)|
|[`Dispose()`](#dispose)|
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
|[`CloudMapClient(String,String)`](#cloudmapclientstringstring)|

### Protected Constructors


### Public Properties

#### `ClientId`

**Summary**

   *A GIS Server NativeConfidential client ID.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `ClientSecret`

**Summary**

   *A GIS Server NativeConfidential client secret.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `TimeoutInSeconds`

**Summary**

   *The request timeout, default 100 seconds*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Uris`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`Uri`>

---
#### `WebProxy`

**Summary**

   *The proxy used for requesting a Web Response.*

**Remarks**

   *N/A*

**Return Value**

`WebProxy`

---

### Protected Properties


### Public Methods

#### `CreateWebRequest(String,String,String,String,Int32,IWebProxy,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`WebRequest`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|apiPath|`String`|N/A|
|method|`String`|N/A|
|parameters|`String`|N/A|
|body|`String`|N/A|
|timeoutInSeconds|`Int32`|N/A|
|webProxy|`IWebProxy`|N/A|
|isNeedToken|`Boolean`|N/A|

---
#### `Dispose()`

**Summary**

   *Disposes BaseClient class.*

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


