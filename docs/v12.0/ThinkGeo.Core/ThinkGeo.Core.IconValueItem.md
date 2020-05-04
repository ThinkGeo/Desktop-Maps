# IconValueItem


## Inheritance Hierarchy

+ `Object`
  + **`IconValueItem`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`IconValueItem()`](#iconvalueitem)|
|[`IconValueItem(String,String,TextStyle)`](#iconvalueitemstringstringtextstyle)|
|[`IconValueItem(String,GeoImage,TextStyle)`](#iconvalueitemstringgeoimagetextstyle)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FieldValue`](#fieldvalue)|`String`|This property gets and sets the field value that has to match in the IconValueStyle.|
|[`IconFilePathName`](#iconfilepathname)|`String`|This property gets and sets the path and filename of the icon that will be drawn.|
|[`TextStyle`](#textstyle)|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|This property gets and sets the style that will be used to draw the text in the icon.|
|[`TextValueLengthMax`](#textvaluelengthmax)|`Int32`|This property gets and sets the maximum string length for this item to match.|
|[`TextValueLengthMin`](#textvaluelengthmin)|`Int32`|This property gets and sets the minimum string length for this item to match.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetIconImage()`](#geticonimage)|
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
|[`IconValueItem()`](#iconvalueitem)|
|[`IconValueItem(String,String,TextStyle)`](#iconvalueitemstringstringtextstyle)|
|[`IconValueItem(String,GeoImage,TextStyle)`](#iconvalueitemstringgeoimagetextstyle)|

### Protected Constructors


### Public Properties

#### `FieldValue`

**Summary**

   *This property gets and sets the field value that has to match in the IconValueStyle.*

**Remarks**

   *For an explanation on how the IconValueStyle works, see the IconValueStyle Class remarks.*

**Return Value**

`String`

---
#### `IconFilePathName`

**Summary**

   *This property gets and sets the path and filename of the icon that will be drawn.*

**Remarks**

   *If you need to use a GeoImage, you can set the GeoImage in the constructor or use the property.*

**Return Value**

`String`

---
#### `TextStyle`

**Summary**

   *This property gets and sets the style that will be used to draw the text in the icon.*

**Remarks**

   *None*

**Return Value**

[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)

---
#### `TextValueLengthMax`

**Summary**

   *This property gets and sets the maximum string length for this item to match.*

**Remarks**

   *This is an important property because it is used to ensure that that a properly sized icon is used to draw things like road signs. For example, you can set the minimum and maximum values so that the sign icon for a single-digit road number uses one icon, while a two-digit road uses another, wider sign icon in a separate IconValueItem.*

**Return Value**

`Int32`

---
#### `TextValueLengthMin`

**Summary**

   *This property gets and sets the minimum string length for this item to match.*

**Remarks**

   *This is an important property because it is used to ensure that that a properly sized icon is used to draw things like road signs. For example, you can set the minimum and maximum values so that the sign icon for a single-digit road number uses one icon, while a two-digit road uses another, wider sign icon in a separate IconValueItem.*

**Return Value**

`Int32`

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
#### `GetIconImage()`

**Summary**

   *This method gets the icon we will draw as a GeoImage.*

**Remarks**

   *This method is used when drawing to get the image as a GeoImage. We will internally either pass along the GeoImage the user set, or create a GeoImage from the IconFilePathName that was set.*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This method gets the icon we will draw as a GeoImage.|

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


