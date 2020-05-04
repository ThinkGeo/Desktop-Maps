# SimpleFilterCondition


## Inheritance Hierarchy

+ `Object`
  + [`FilterCondition`](../ThinkGeo.Core/ThinkGeo.Core.FilterCondition.md)
    + **`SimpleFilterCondition`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`SimpleFilterCondition()`](#simplefiltercondition)|
|[`SimpleFilterCondition(String,SimpleFilterConditionType,String)`](#simplefilterconditionstringsimplefilterconditiontypestring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ColumnName`](#columnname)|`String`|N/A|
|[`Expression`](#expression)|`String`|N/A|
|[`IsLeftBracket`](#isleftbracket)|`Boolean`|N/A|
|[`IsRightBracket`](#isrightbracket)|`Boolean`|N/A|
|[`Logical`](#logical)|`Boolean`|N/A|
|[`MatchType`](#matchtype)|[`SimpleFilterConditionType`](../ThinkGeo.Core/ThinkGeo.Core.SimpleFilterConditionType.md)|N/A|
|[`MatchValue`](#matchvalue)|`String`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`RegexOptions`](#regexoptions)|`RegexOptions`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetMatchingFeatures(IEnumerable<Feature>)`](#getmatchingfeaturesienumerable<feature>)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`GetMatchingFeaturesCore(IEnumerable<Feature>)`](#getmatchingfeaturescoreienumerable<feature>)|
|[`GetMatchResult(Collection<String>)`](#getmatchresultcollection<string>)|
|[`IsMatch(Feature)`](#ismatchfeature)|
|[`IsMatch(KeyValuePair<String,String>)`](#ismatchkeyvaluepair<stringstring>)|
|[`IsMatchCore(KeyValuePair<String,String>)`](#ismatchcorekeyvaluepair<stringstring>)|
|[`IsMatchCore(Feature)`](#ismatchcorefeature)|
|[`MemberwiseClone()`](#memberwiseclone)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`SimpleFilterCondition()`](#simplefiltercondition)|
|[`SimpleFilterCondition(String,SimpleFilterConditionType,String)`](#simplefilterconditionstringsimplefilterconditiontypestring)|

### Protected Constructors


### Public Properties

#### `ColumnName`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Expression`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `IsLeftBracket`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsRightBracket`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Logical`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `MatchType`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`SimpleFilterConditionType`](../ThinkGeo.Core/ThinkGeo.Core.SimpleFilterConditionType.md)

---
#### `MatchValue`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `RegexOptions`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`RegexOptions`

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
#### `GetMatchingFeatures(IEnumerable<Feature>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

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
#### `GetMatchingFeaturesCore(IEnumerable<Feature>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `GetMatchResult(Collection<String>)`

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
|values|Collection<`String`>|N/A|

---
#### `IsMatch(Feature)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

---
#### `IsMatch(KeyValuePair<String,String>)`

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
|value|KeyValuePair<`String`,`String`>|N/A|

---
#### `IsMatchCore(KeyValuePair<String,String>)`

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
|value|KeyValuePair<`String`,`String`>|N/A|

---
#### `IsMatchCore(Feature)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

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


