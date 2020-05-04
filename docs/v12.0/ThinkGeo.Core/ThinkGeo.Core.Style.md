# Style


## Inheritance Hierarchy

+ `Object`
  + **`Style`**
    + [`HeatStyle`](../ThinkGeo.Core/ThinkGeo.Core.HeatStyle.md)
    + [`NoaaWeatherStationStyle`](../ThinkGeo.Core/ThinkGeo.Core.NoaaWeatherStationStyle.md)
    + [`PointBaseStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseStyle.md)
    + [`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)
    + [`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)
    + [`PositionStyle`](../ThinkGeo.Core/ThinkGeo.Core.PositionStyle.md)
    + [`ClassBreakStyle`](../ThinkGeo.Core/ThinkGeo.Core.ClassBreakStyle.md)
    + [`ClusterPointStyle`](../ThinkGeo.Core/ThinkGeo.Core.ClusterPointStyle.md)
    + [`CompositeStyle`](../ThinkGeo.Core/ThinkGeo.Core.CompositeStyle.md)
    + [`DotDensityStyle`](../ThinkGeo.Core/ThinkGeo.Core.DotDensityStyle.md)
    + [`FilterStyle`](../ThinkGeo.Core/ThinkGeo.Core.FilterStyle.md)
    + [`FleeBooleanStyle`](../ThinkGeo.Core/ThinkGeo.Core.FleeBooleanStyle.md)
    + [`GradientStyle`](../ThinkGeo.Core/ThinkGeo.Core.GradientStyle.md)
    + [`RegexStyle`](../ThinkGeo.Core/ThinkGeo.Core.RegexStyle.md)
    + [`StyleJsonStyle`](../ThinkGeo.Core/ThinkGeo.Core.StyleJsonStyle.md)
    + [`ValueStyle`](../ThinkGeo.Core/ThinkGeo.Core.ValueStyle.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`Style()`](#style)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`IsActive`](#isactive)|`Boolean`|This property gets and sets the active status of the style.|
|[`Name`](#name)|`String`|This property gets and set the name of the style.|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|This property gets the collection of fields that are required for the style.|

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
|[`LoadStyle(Uri)`](#loadstyleuri)|
|[`LoadStyle(Stream)`](#loadstylestream)|
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
|[`ParseSegments(String,Char,Char,Action<String>)`](#parsesegmentsstringcharcharaction<string>)|

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

#### `Style()`

**Summary**

   *This is the default constructor for the style and should be called by inherited classes.*

**Remarks**

   *This is the default constructor for the style and should be called by inherited classes.*

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

   *This property gets and sets the active status of the style.*

**Remarks**

   *If the style is not active then it will not draw.*

**Return Value**

`Boolean`

---
#### `Name`

**Summary**

   *This property gets and set the name of the style.*

**Remarks**

   *This name is not used by the system; it is only for the developer. However, it can be used if you generate your own legend.*

**Return Value**

`String`

---
#### `RequiredColumnNames`

**Summary**

   *This property gets the collection of fields that are required for the style.*

**Remarks**

   *This property gets the collection of fields that are required for the style. These are in addition to any other columns you specify in styles that inherit from this one. For example, if you have use a ValueStyle and it requires a column name for the value comparison, then that column does not need to be in this collection. You only use the RequiredColumnNames for columns you need beyond those required by specific inherited styles.*

**Return Value**

Collection<`String`>

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

   *Create a copy of style using the deep clone process.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)|A cloned style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Draw(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the features on the view you provided.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DrawCore. In this method, we take the features you passed in and draw them on the view you provided. Each style (based on its properties) may draw each feature differently. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter represents the features you want to draw on the view.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the features on.|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

---
#### `Draw(IEnumerable<BaseShape>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the shapes on the view you provided.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DrawCore. In this method, we take the shapes you passed in and draw them on the view you provided. Each style (based on its properties) may draw each shape differently. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapes|IEnumerable<[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)>|This parameter represents the shapes you want to draw on the view.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the shapes on.|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

---
#### `DrawSample(GeoCanvas,DrawingRectangleF)`

**Summary**

   *This method draws a sample feature on the view you provided.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DrawSampleCore. In this method we draw a sample style on the view you provided. This is typically used to display a legend or other sample area. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the features on.|
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

   *This method returns the column data for each feature that is required for the style to properly draw.*

**Remarks**

   *This method is the concrete wrapper for the abstract method GetRequiredColumnNamesCore. In this method, we return the column names that are required for the style to draw the feature properly. For example, if you have a style that colors areas blue when a certain column value is over 100, then you need to be sure you include that column name. This will ensure that the column data is returned to you in the feature when it is ready to draw. In many of the styles, we add properties to allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore we read that property and add it to the collection. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns a collection of column names that the style needs.|

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
#### `LoadStyle(Uri)`

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
|styleUri|`Uri`|N/A|

---
#### `LoadStyle(Stream)`

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
|styleStream|`Stream`|N/A|

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

   *Create a copy of style using the deep clone process. The default implementation method uses serialization.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)|A cloned style.|

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
|`Void`|None|

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

   *This method draws a sample feature on the view you provided.*

**Remarks**

   *This virtual method is called from the concrete public method Draw. In this method, we draw a sample style on the view you provided. This is typically used to display a legend or other sample area. When implementing this virtual method, consider the view size and draw the sample image appropriately. You should keep in mind that the sample typically shows up on a legend.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the features on.|
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
#### `ParseSegments(String,Char,Char,Action<String>)`

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
|content|`String`|N/A|
|start|`Char`|N/A|
|end|`Char`|N/A|
|oneParsed|Action<`String`>|N/A|

---

### Public Events


