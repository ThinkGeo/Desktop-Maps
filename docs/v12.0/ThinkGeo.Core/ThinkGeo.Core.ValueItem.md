# ValueItem


## Inheritance Hierarchy

+ `Object`
  + **`ValueItem`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ValueItem()`](#valueitem)|
|[`ValueItem(String,AreaStyle)`](#valueitemstringareastyle)|
|[`ValueItem(String,LineStyle)`](#valueitemstringlinestyle)|
|[`ValueItem(String,PointStyle)`](#valueitemstringpointstyle)|
|[`ValueItem(String,TextStyle)`](#valueitemstringtextstyle)|
|[`ValueItem(String,Collection<Style>)`](#valueitemstringcollection<style>)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CustomStyles`](#customstyles)|Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>|This property gets the collection of custom styles.|
|[`DefaultAreaStyle`](#defaultareastyle)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This property gets and sets the default AreaStyle. You should use this style if your features are area-based.|
|[`DefaultLineStyle`](#defaultlinestyle)|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This property gets and sets the default LineStyle. You should use this style if your features are line-based.|
|[`DefaultPointStyle`](#defaultpointstyle)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|This property gets and sets the default PointStyle. You should use this style if your features are point-based.|
|[`DefaultTextStyle`](#defaulttextstyle)|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|This property gets and sets the default TextStyle. You should use this style if your features are text-based (such as labels).|
|[`Value`](#value)|`String`|This property gets and sets the value that we use to match with the feature data. If the value matches, we use the style for this item.|

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
|[`ValueItem()`](#valueitem)|
|[`ValueItem(String,AreaStyle)`](#valueitemstringareastyle)|
|[`ValueItem(String,LineStyle)`](#valueitemstringlinestyle)|
|[`ValueItem(String,PointStyle)`](#valueitemstringpointstyle)|
|[`ValueItem(String,TextStyle)`](#valueitemstringtextstyle)|
|[`ValueItem(String,Collection<Style>)`](#valueitemstringcollection<style>)|

### Protected Constructors


### Public Properties

#### `CustomStyles`

**Summary**

   *This property gets the collection of custom styles.*

**Remarks**

   *The custom styles allow you to use styles other than the default style properties of the class. In this way, you can use a DotDensityStyle or any other style in the API.*

**Return Value**

Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>

---
#### `DefaultAreaStyle`

**Summary**

   *This property gets and sets the default AreaStyle. You should use this style if your features are area-based.*

**Remarks**

   *The default style allows you to directly set properties on the styles without having to create a new style each time. You can start simply by setting properties like color, etc. This makes modifying styles very easy.*

**Return Value**

[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)

---
#### `DefaultLineStyle`

**Summary**

   *This property gets and sets the default LineStyle. You should use this style if your features are line-based.*

**Remarks**

   *The default style allows you to directly set properties on the styles without having to create a new style each time. You can start simply by setting properties like color, etc. This makes modifying styles very easy.*

**Return Value**

[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)

---
#### `DefaultPointStyle`

**Summary**

   *This property gets and sets the default PointStyle. You should use this style if your features are point-based.*

**Remarks**

   *The default style allows you to directly set properties on the styles without having to create a new style each time. You can start simply by setting properties like color, etc. This makes modifying styles very easy.*

**Return Value**

[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)

---
#### `DefaultTextStyle`

**Summary**

   *This property gets and sets the default TextStyle. You should use this style if your features are text-based (such as labels).*

**Remarks**

   *The default style allows you to directly set properties on the styles without having to create a new style each time. You can start simply by setting properties like color, etc. This makes modifying styles very easy.*

**Return Value**

[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)

---
#### `Value`

**Summary**

   *This property gets and sets the value that we use to match with the feature data. If the value matches, we use the style for this item.*

**Remarks**

   *The value must match exactly. If more complex matching is required, we suggest you use the RegexStyle. The RegexStyle enables you to use regular expressions, which are very powerful for fuzzy matching.*

**Return Value**

`String`

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


