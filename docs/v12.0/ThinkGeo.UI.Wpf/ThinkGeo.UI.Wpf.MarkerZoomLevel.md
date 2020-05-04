# MarkerZoomLevel


## Inheritance Hierarchy

+ `Object`
  + **`MarkerZoomLevel`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MarkerZoomLevel()`](#markerzoomlevel)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ApplyUntilZoomLevel`](#applyuntilzoomlevel)|[`ApplyUntilZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ApplyUntilZoomLevel.md)|Gets or sets the zoomlevel to which the styles will be applied.|
|[`CustomMarkerStyle`](#custommarkerstyle)|[`MarkerStyle`](ThinkGeo.UI.Wpf.MarkerStyle.md)|Gets or sets a custom style that can be any type of MarkerStyle.|
|[`DefaultPointMarkerStyle`](#defaultpointmarkerstyle)|[`PointMarkerStyle`](ThinkGeo.UI.Wpf.PointMarkerStyle.md)|Gets default style that is applied to the markers if the CustomMarkerStyle is not defined.|
|[`IsStyleDefined`](#isstyledefined)|`Boolean`|Gets whether the style is defined.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetMarkers(IEnumerable<Feature>)`](#getmarkersienumerable<feature>)|
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
|[`MarkerZoomLevel()`](#markerzoomlevel)|

### Protected Constructors


### Public Properties

#### `ApplyUntilZoomLevel`

**Summary**

   *Gets or sets the zoomlevel to which the styles will be applied.*

**Remarks**

   *Gets or sets the zoomlevel to which the styles will be applied.*

**Return Value**

[`ApplyUntilZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ApplyUntilZoomLevel.md)

---
#### `CustomMarkerStyle`

**Summary**

   *Gets or sets a custom style that can be any type of MarkerStyle.*

**Remarks**

   *The CustomMarkerStyle has a higher priority than the DefaultMarkerStyle. When you define both styles, the CustomMarkerStyle will be applied. The CustomMarkerStyle can be any kind of MarkStyle while DefaultMarkerStyle can not.*

**Return Value**

[`MarkerStyle`](ThinkGeo.UI.Wpf.MarkerStyle.md)

---
#### `DefaultPointMarkerStyle`

**Summary**

   *Gets default style that is applied to the markers if the CustomMarkerStyle is not defined.*

**Remarks**

   *Gets the style that is applied to the markers if the CustomMarkerStyle is not defined.*

**Return Value**

[`PointMarkerStyle`](ThinkGeo.UI.Wpf.PointMarkerStyle.md)

---
#### `IsStyleDefined`

**Summary**

   *Gets whether the style is defined.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

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
#### `GetMarkers(IEnumerable<Feature>)`

**Summary**

   *This method returns a collection of markers that is applied with the styles when the current zoomlevel falls into the ranges that defined.*

**Remarks**

   *This method returns a collection of markers that is applied with the styles when the current zoomlevel falls into the ranges that defined.*

**Return Value**

|Type|Description|
|---|---|
|GeoCollection<[`Marker`](ThinkGeo.UI.Wpf.Marker.md)>|A Collection of markers that created from the feature collection.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|A collection of features that is applied with the styles when the current zoomlevel falls into the ranges that defined.|

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


