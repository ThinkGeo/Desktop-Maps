# FilterStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + **`FilterStyle`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`FilterStyle()`](#filterstyle)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Conditions`](#conditions)|Collection<[`FilterCondition`](../ThinkGeo.Core/ThinkGeo.Core.FilterCondition.md)>|Gets the conditions.|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`Styles`](#styles)|Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>|Gets the styles.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Draw(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawienumerable<feature>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`Draw(IEnumerable<BaseShape>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawienumerable<baseshape>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawSample(GeoCanvas,DrawingRectangleF)`](#drawsamplegeocanvasdrawingrectanglef)|
|[`DrawSample(GeoCanvas)`](#drawsamplegeocanvas)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetRequiredColumnNames()`](#getrequiredcolumnnames)|
|[`GetType()`](#gettype)|
|[`SaveStyle(String)`](#savestylestring)|
|[`SaveStyle(Stream)`](#savestylestream)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`DrawCore(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawcoreienumerable<feature>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawSampleCore(GeoCanvas,DrawingRectangleF)`](#drawsamplecoregeocanvasdrawingrectanglef)|
|[`Finalize()`](#finalize)|
|[`GetRequiredColumnNamesCore()`](#getrequiredcolumnnamescore)|
|[`MemberwiseClone()`](#memberwiseclone)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`FilterStyle()`](#filterstyle)|

### Protected Constructors


### Public Properties

#### `Conditions`

**Summary**

   *Gets the conditions.*

**Remarks**

   *N/A*

**Return Value**

Collection<[`FilterCondition`](../ThinkGeo.Core/ThinkGeo.Core.FilterCondition.md)>

---
#### `Filters`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `IsActive`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `RequiredColumnNames`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `Styles`

**Summary**

   *Gets the styles.*

**Remarks**

   *N/A*

**Return Value**

Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>

---

### Protected Properties

#### `FiltersCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `IsDefault`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---

### Public Methods

#### `CloneDeep()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Draw(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `Draw(IEnumerable<BaseShape>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|shapes|IEnumerable<[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)>|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `DrawSample(GeoCanvas,DrawingRectangleF)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|drawingExtent|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|N/A|

---
#### `DrawSample(GeoCanvas)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|

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
#### `GetRequiredColumnNames()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|N/A|

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
#### `SaveStyle(String)`

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
|filePathName|`String`|N/A|

---
#### `SaveStyle(Stream)`

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
|stream|`Stream`|N/A|

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

#### `CloneDeepCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DrawCore(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the features on the view you provided.*

**Remarks**

   *This abstract method is called from the concrete public method Draw. In this method, we take the features you passed in and draw them on the view you provided. Each style (based on its properties) may draw each feature differently. When implementing this abstract method, consider each feature and its column data values. You can use the full power of the GeoCanvas to do the drawing. If you need column data for a feature, be sure to override the GetRequiredColumnNamesCore and add the columns you need to the collection. In many of the styles, we add properties to allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore, we read that property and add it to the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter represents the features you want to draw on the view.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the features on.|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

---
#### `DrawSampleCore(GeoCanvas,DrawingRectangleF)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|drawingExtent|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|N/A|

---
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
#### `GetRequiredColumnNamesCore()`

**Summary**

   *This method returns the column data for each feature that is required for the style to properly draw.*

**Remarks**

   *This abstract method is called from the concrete public method GetRequiredFieldNames. In this method, we return the column names that are required for the style to draw the feature properly. For example, if you have a style that colors areas blue when a certain column value is over 100, then you need to be sure you include that column name. This will ensure that the column data is returned to you in the feature when it is ready to draw. In many of the styles, we add properties to allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore we read that property and add it to the collection.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns a collection of column names that the style needs.|

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


