# GeoObjectNode


## Inheritance Hierarchy

+ `Object`
  + **`GeoObjectNode`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeoObjectNode()`](#geoobjectnode)|
|[`GeoObjectNode(String)`](#geoobjectnodestring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attributes`](#attributes)|Dictionary<`String`,`String`>|Gets the Attribute of the node.|
|[`Children`](#children)|Collection<[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)>|Gets the Children of the node.|
|[`IsDefaultValue`](#isdefaultvalue)|`Boolean`|Gets or sets the IsDefaultValue of the node.|
|[`Name`](#name)|`String`|Gets or sets the Name of the node.|
|[`Parent`](#parent)|[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)|Gets or sets the Parent of the node.|
|[`Value`](#value)|`String`|Gets or sets the Value of the node.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FirstChild`](#firstchild)|[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)|N/A|
|[`IsNonGenericSequence`](#isnongenericsequence)|`Boolean`|N/A|
|[`LastChild`](#lastchild)|[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)|N/A|

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
|[`GetAttribute(String)`](#getattributestring)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`RemoveAttribute(String)`](#removeattributestring)|
|[`SetAttribute(String,String)`](#setattributestringstring)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`GeoObjectNode()`](#geoobjectnode)|
|[`GeoObjectNode(String)`](#geoobjectnodestring)|

### Protected Constructors


### Public Properties

#### `Attributes`

**Summary**

   *Gets the Attribute of the node.*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,`String`>

---
#### `Children`

**Summary**

   *Gets the Children of the node.*

**Remarks**

   *N/A*

**Return Value**

Collection<[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)>

---
#### `IsDefaultValue`

**Summary**

   *Gets or sets the IsDefaultValue of the node.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Name`

**Summary**

   *Gets or sets the Name of the node.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Parent`

**Summary**

   *Gets or sets the Parent of the node.*

**Remarks**

   *N/A*

**Return Value**

[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)

---
#### `Value`

**Summary**

   *Gets or sets the Value of the node.*

**Remarks**

   *N/A*

**Return Value**

`String`

---

### Protected Properties

#### `FirstChild`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)

---
#### `IsNonGenericSequence`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `LastChild`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoObjectNode`](../ThinkGeo.Core/ThinkGeo.Core.GeoObjectNode.md)

---

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

   *Returns a string that contains the name, children count and attribute count of this node.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|The string that contains the name, children count and attribute count of this node.|

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
#### `GetAttribute(String)`

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
|attributeName|`String`|N/A|

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
#### `RemoveAttribute(String)`

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
|attributeName|`String`|N/A|

---
#### `SetAttribute(String,String)`

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
|attributeName|`String`|N/A|
|attributeValue|`String`|N/A|

---

### Public Events


