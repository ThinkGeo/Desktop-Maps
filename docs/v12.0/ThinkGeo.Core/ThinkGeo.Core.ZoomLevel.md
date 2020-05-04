# ZoomLevel


## Inheritance Hierarchy

+ `Object`
  + **`ZoomLevel`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ZoomLevel()`](#zoomlevel)|
|[`ZoomLevel(Double)`](#zoomleveldouble)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ApplyUntilZoomLevel`](#applyuntilzoomlevel)|[`ApplyUntilZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ApplyUntilZoomLevel.md)|This property gets and sets the zoom to which we will use this zoom level's styles.|
|[`CustomStyles`](#customstyles)|Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>|This property gets a collection of custom styles used to draw.|
|[`DefaultAreaStyle`](#defaultareastyle)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This property gets and sets the default AreaStyle used to draw.|
|[`DefaultLineStyle`](#defaultlinestyle)|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This property gets and sets the default LineStyle used to draw.|
|[`DefaultPointStyle`](#defaultpointstyle)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|This property gets and sets the default PointStyle used to draw.|
|[`DefaultTextStyle`](#defaulttextstyle)|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|This property gets and sets the default TextStyle used to draw.|
|[`IsActive`](#isactive)|`Boolean`|This property gets and sets whether the ZoomLevel is active and should draw.|
|[`Name`](#name)|`String`|This property gets and sets the name for the ZoomLevel.|
|[`Scale`](#scale)|`Double`|This property gets and sets the scale for the ZoomLevel.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`Draw(GeoCanvas,IEnumerable<Feature>,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawgeocanvasienumerable<feature>collection<simplecandidate>collection<simplecandidate>)|
|[`Draw(GeoCanvas,IEnumerable<BaseShape>,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawgeocanvasienumerable<baseshape>collection<simplecandidate>collection<simplecandidate>)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetRequiredColumnNames()`](#getrequiredcolumnnames)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`DrawCore(GeoCanvas,IEnumerable<Feature>,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawcoregeocanvasienumerable<feature>collection<simplecandidate>collection<simplecandidate>)|
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
|[`ZoomLevel()`](#zoomlevel)|
|[`ZoomLevel(Double)`](#zoomleveldouble)|

### Protected Constructors


### Public Properties

#### `ApplyUntilZoomLevel`

**Summary**

   *This property gets and sets the zoom to which we will use this zoom level's styles.*

**Remarks**

   *This property allows you to apply the current ZoomLevel's styles across many ZoomLevels. For example, you may want to display roads as a thin line from ZoomLevel10 through ZoomLevel15. To accomplish this easily, you can set the correct styles on ZoomLevel10 and then set its ApplyUntilZoomLevel property to Level15. This will mean that Level10's style will be used until Level15. There is no need to set Level11, Level12, Level13 and so on.*

**Return Value**

[`ApplyUntilZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ApplyUntilZoomLevel.md)

---
#### `CustomStyles`

**Summary**

   *This property gets a collection of custom styles used to draw.*

**Remarks**

   *This is a collection of styles to draw. If you only need to draw one style, then we suggest you do not use this collection and instead use one of the default styles, such as DefaultAreaStyle, DefaultLineStyle, DefaultTextStyle, etc.*

**Return Value**

Collection<[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)>

---
#### `DefaultAreaStyle`

**Summary**

   *This property gets and sets the default AreaStyle used to draw.*

**Remarks**

   *If you set this style, then it will be used for drawing. If you use the default styles, you should only use one. The one you use should match your feature data. For example, if your features are lines, then you should use the DefaultLineStyle.*

**Return Value**

[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)

---
#### `DefaultLineStyle`

**Summary**

   *This property gets and sets the default LineStyle used to draw.*

**Remarks**

   *If you set this style, then it will be used for drawing. If you use the default styles, you should only use one. The one you use should match your feature data. For example, if your features are lines, then you should use the DefaultLineStyle.*

**Return Value**

[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)

---
#### `DefaultPointStyle`

**Summary**

   *This property gets and sets the default PointStyle used to draw.*

**Remarks**

   *If you set this style, then it will be used for drawing. If you use the default styles, you should only use one. The one you use should match your feature data. For example, if your features are lines, then you should use the DefaultLineStyle.*

**Return Value**

[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)

---
#### `DefaultTextStyle`

**Summary**

   *This property gets and sets the default TextStyle used to draw.*

**Remarks**

   *If you set this style, then it will be used for drawing. If you use the default styles, you should only use one. The one you use should match your feature data. For example, if your features are lines, then you should use the DefaultLineStyle.*

**Return Value**

[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)

---
#### `IsActive`

**Summary**

   *This property gets and sets whether the ZoomLevel is active and should draw.*

**Remarks**

   *Setting the value to false means that this zoom level will not be considered in the ZoomLevelSet and thus will not draw.*

**Return Value**

`Boolean`

---
#### `Name`

**Summary**

   *This property gets and sets the name for the ZoomLevel.*

**Remarks**

   *The name is user defined. It is useful to set, as it may be used for higher level components such as legends, etc.*

**Return Value**

`String`

---
#### `Scale`

**Summary**

   *This property gets and sets the scale for the ZoomLevel.*

**Remarks**

   *The scale*

**Return Value**

`Double`

---

### Protected Properties

#### `IsDefault`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---

### Public Methods

#### `Draw(GeoCanvas,IEnumerable<Feature>,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the ZoomLevel.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DrawCore. This method draws the representation of the Layer based on the extent you provided. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the Canvas used to draw the InternalFeatures.|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter is the collection of features that we will draw.|
|currentLayerLabels|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter is the collection of labels in the current layer.|
|allLayerLabels|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter is the collection of labels in all layers.|

---
#### `Draw(GeoCanvas,IEnumerable<BaseShape>,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the ZoomLevel.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DrawCore. This method draws the representation of the Layer based on the extent you provided. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the Canvas used to draw the InternalFeatures.|
|shapes|IEnumerable<[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)>|This parameter is the collection of shapes that we will draw.|
|currentLayerLabels|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter is the collection of labels in the current layer.|
|allLayerLabels|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter is the collection of labels in all layers.|

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

   *This method returns the column data for each feature that is required for the styles to properly draw.*

**Remarks**

   *In this method, we return the column names that are required for the styles to draw the feature properly. For example, if you have a style that colors areas blue when a certain column value is over 100, then you need to be sure you include the column name. This will ensure that the column data is returned to you in the feature when it is ready to draw. In many of the styles, we add properties to allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore, we read that property and add it to the collection.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns a collection containing the required column names.|

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

#### `DrawCore(GeoCanvas,IEnumerable<Feature>,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the ZoomLevel.*

**Remarks**

   *This method draws the representation of the Layer based on the extent you provided.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the Canvas used to draw the InternalFeatures.|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter is the collection of feature that we will draw.|
|currentLayerLabels|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter is the collection of labels in the current layer.|
|allLayerLabels|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter is the collection of labels in all layers.|

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


