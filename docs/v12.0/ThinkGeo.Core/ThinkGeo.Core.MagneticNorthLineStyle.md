# MagneticNorthLineStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + [`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)
      + **`MagneticNorthLineStyle`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MagneticNorthLineStyle()`](#magneticnorthlinestyle)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CenterPen`](#centerpen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|[`CenterPenDrawingLevel`](#centerpendrawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`CustomLineStyles`](#customlinestyles)|Collection<[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)>|N/A|
|[`DirectionPointInterval`](#directionpointinterval)|`Double`|N/A|
|[`DirectionPointStyle`](#directionpointstyle)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`InnerPen`](#innerpen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|[`InnerPenDrawingLevel`](#innerpendrawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OuterPen`](#outerpen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|[`OuterPenDrawingLevel`](#outerpendrawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|N/A|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|N/A|

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
|[`OnDrawingDirectionPoint(DrawingDirectionPointEventArgs)`](#ondrawingdirectionpointdrawingdirectionpointeventargs)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`DrawingDirectionPoint`](#drawingdirectionpoint)|[`DrawingDirectionPointEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingDirectionPointEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`MagneticNorthLineStyle()`](#magneticnorthlinestyle)|

### Protected Constructors


### Public Properties

#### `CenterPen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `CenterPenDrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `CustomLineStyles`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)>

---
#### `DirectionPointInterval`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `DirectionPointStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)

---
#### `Filters`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `InnerPen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `InnerPenDrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

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
#### `OuterPen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `OuterPenDrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `RequiredColumnNames`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `XOffsetInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `YOffsetInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

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
#### `OnDrawingDirectionPoint(DrawingDirectionPointEventArgs)`

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
|drawingDirectionPointEventArgs|[`DrawingDirectionPointEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingDirectionPointEventArgs.md)|N/A|

---

### Public Events

#### DrawingDirectionPoint

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingDirectionPointEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingDirectionPointEventArgs.md)


