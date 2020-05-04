# ClassBreak


## Inheritance Hierarchy

+ `Object`
  + **`ClassBreak`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ClassBreak()`](#classbreak)|
|[`ClassBreak(Double,AreaStyle)`](#classbreakdoubleareastyle)|
|[`ClassBreak(Double,PointStyle)`](#classbreakdoublepointstyle)|
|[`ClassBreak(Double,LineStyle)`](#classbreakdoublelinestyle)|
|[`ClassBreak(Double,TextStyle)`](#classbreakdoubletextstyle)|
|[`ClassBreak(Double,Collection<Style>)`](#classbreakdoublecollection<style>)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CustomStyles`](#customstyles)|Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>|This property gets a collection of custom styles used to draw the class break.|
|[`DefaultAreaStyle`](#defaultareastyle)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This property gets and sets the default AreaStyle used to draw the class break.|
|[`DefaultLineStyle`](#defaultlinestyle)|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This property gets and sets the default LineStyle used to draw the class break.|
|[`DefaultPointStyle`](#defaultpointstyle)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|This property gets and sets the default PointStyle used to draw the class break.|
|[`DefaultTextStyle`](#defaulttextstyle)|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|This property gets and sets the default TextStyle used to draw the class break.|
|[`Value`](#value)|`Double`|This property get and sets the break value.|

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
|[`ClassBreak()`](#classbreak)|
|[`ClassBreak(Double,AreaStyle)`](#classbreakdoubleareastyle)|
|[`ClassBreak(Double,PointStyle)`](#classbreakdoublepointstyle)|
|[`ClassBreak(Double,LineStyle)`](#classbreakdoublelinestyle)|
|[`ClassBreak(Double,TextStyle)`](#classbreakdoubletextstyle)|
|[`ClassBreak(Double,Collection<Style>)`](#classbreakdoublecollection<style>)|

### Protected Constructors


### Public Properties

#### `CustomStyles`

**Summary**

   *This property gets a collection of custom styles used to draw the class break.*

**Remarks**

   *If you set these styles, then when the data for a feature is within the current break it will use this style to draw. If you do not wish to use the default style properties, then you can use this collection to specify any types of styles you want to use.*

**Return Value**

Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>

---
#### `DefaultAreaStyle`

**Summary**

   *This property gets and sets the default AreaStyle used to draw the class break.*

**Remarks**

   *If you set this style, then when the data for a feature is within the current break it will use this style to draw. If you use the default styles, then you should only use one. The one you use should match your feature data. For example, if your features are lines, then you should use the DefaultLineStyle.*

**Return Value**

[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)

---
#### `DefaultLineStyle`

**Summary**

   *This property gets and sets the default LineStyle used to draw the class break.*

**Remarks**

   *If you set this style, then when the data for a feature is within the current break it will use this style to draw. If you use the default styles, then you should only use one. The one you use should match your feature data. For example, if your features are lines, then you should use the DefaultLineStyle.*

**Return Value**

[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)

---
#### `DefaultPointStyle`

**Summary**

   *This property gets and sets the default PointStyle used to draw the class break.*

**Remarks**

   *If you set this style, then when the data for a feature is within the current break it will use this style to draw. If you use the default styles, then you should only use one. The one you use should match your feature data. For example, if your features are lines, then you should use the DefaultLineStyle.*

**Return Value**

[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)

---
#### `DefaultTextStyle`

**Summary**

   *This property gets and sets the default TextStyle used to draw the class break.*

**Remarks**

   *If you set this style, then when the data for a feature is within the current break it will use this style to draw. If you use the default styles, then you should only use one. The one you use should match your feature data. For example, if your features are lines then you should use the DefaultLineStyle.*

**Return Value**

[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)

---
#### `Value`

**Summary**

   *This property get and sets the break value.*

**Remarks**

   *This value determines where the break is in the ClassBreakStyle. Please see the ClassBreakStyle class remarks for a full description of how the ClassBreakStyle works.*

**Return Value**

`Double`

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


