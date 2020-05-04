# MarkerOverlay


## Inheritance Hierarchy

+ `Object`
  + [`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)
    + **`MarkerOverlay`**
      + [`FeatureSourceMarkerOverlay`](ThinkGeo.UI.Wpf.FeatureSourceMarkerOverlay.md)
      + [`SimpleMarkerOverlay`](ThinkGeo.UI.Wpf.SimpleMarkerOverlay.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`MarkerOverlay()`](#markeroverlay)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`AutoRefreshInterval`](#autorefreshinterval)|`TimeSpan`|N/A|
|[`CanRefreshRegion`](#canrefreshregion)|`Boolean`|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`IsBase`](#isbase)|`Boolean`|N/A|
|[`IsEmpty`](#isempty)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`MapArguments`](#maparguments)|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsOverlayInitialized`](#isoverlayinitialized)|`Boolean`|N/A|
|[`PreviousExtent`](#previousextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`PreviousScale`](#previousscale)|`Double`|N/A|

### Public Methods Summary


|Name|
|---|
|[`Close()`](#close)|
|[`Dispose()`](#dispose)|
|[`Draw(RectangleShape)`](#drawrectangleshape)|
|[`Draw(RectangleShape,OverlayRefreshType)`](#drawrectangleshapeoverlayrefreshtype)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetHashCode()`](#gethashcode)|
|[`GetMarkersForDrawing(RectangleShape)`](#getmarkersfordrawingrectangleshape)|
|[`GetType()`](#gettype)|
|[`Initialize(MapArguments)`](#initializemaparguments)|
|[`LoadState(Byte[])`](#loadstatebyte[])|
|[`Open()`](#open)|
|[`PanTo(RectangleShape)`](#pantorectangleshape)|
|[`Refresh(RectangleShape)`](#refreshrectangleshape)|
|[`Refresh()`](#refresh)|
|[`Refresh(IEnumerable<RectangleShape>)`](#refreshienumerable<rectangleshape>)|
|[`Refresh(TimeSpan)`](#refreshtimespan)|
|[`Refresh(TimeSpan,RequestDrawingBufferTimeType)`](#refreshtimespanrequestdrawingbuffertimetype)|
|[`Refresh(RectangleShape,TimeSpan)`](#refreshrectangleshapetimespan)|
|[`Refresh(RectangleShape,TimeSpan,RequestDrawingBufferTimeType)`](#refreshrectangleshapetimespanrequestdrawingbuffertimetype)|
|[`Refresh(IEnumerable<RectangleShape>,TimeSpan)`](#refreshienumerable<rectangleshape>timespan)|
|[`Refresh(IEnumerable<RectangleShape>,TimeSpan,RequestDrawingBufferTimeType)`](#refreshienumerable<rectangleshape>timespanrequestdrawingbuffertimetype)|
|[`SaveState()`](#savestate)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloseCore()`](#closecore)|
|[`Dispose(Boolean)`](#disposeboolean)|
|[`DrawAttribution(GeoCanvas)`](#drawattributiongeocanvas)|
|[`DrawAttributionCore(GeoCanvas)`](#drawattributioncoregeocanvas)|
|[`DrawCore(RectangleShape,OverlayRefreshType)`](#drawcorerectangleshapeoverlayrefreshtype)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetMarkersForDrawingCore(RectangleShape)`](#getmarkersfordrawingcorerectangleshape)|
|[`InitializeCore(MapArguments)`](#initializecoremaparguments)|
|[`LoadStateCore(Byte[])`](#loadstatecorebyte[])|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnDrawing(DrawingOverlayEventArgs)`](#ondrawingdrawingoverlayeventargs)|
|[`OnDrawingAttribution(DrawingAttributionOverlayEventArgs)`](#ondrawingattributiondrawingattributionoverlayeventargs)|
|[`OnDrawn(DrawnOverlayEventArgs)`](#ondrawndrawnoverlayeventargs)|
|[`OnDrawnAttribution(DrawnAttributionOverlayEventArgs)`](#ondrawnattributiondrawnattributionoverlayeventargs)|
|[`OnRefreshing(OverlayRefreshType)`](#onrefreshingoverlayrefreshtype)|
|[`OpenCore()`](#opencore)|
|[`PanToCore(RectangleShape)`](#pantocorerectangleshape)|
|[`RefreshCore(RectangleShape)`](#refreshcorerectangleshape)|
|[`RefreshCore()`](#refreshcore)|
|[`SaveStateCore()`](#savestatecore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`Drawing`](#drawing)|[`DrawingOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingOverlayEventArgs.md)|N/A|
|[`Drawn`](#drawn)|[`DrawnOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnOverlayEventArgs.md)|N/A|
|[`DrawingAttribution`](#drawingattribution)|[`DrawingAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingAttributionOverlayEventArgs.md)|N/A|
|[`DrawnAttribution`](#drawnattribution)|[`DrawnAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnAttributionOverlayEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|N/A|

### Protected Constructors

#### `MarkerOverlay()`

**Summary**

   *Constructor of the marker overlay.*

**Remarks**

   *N/A*

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

#### `Attribution`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `AutoRefreshInterval`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `CanRefreshRegion`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `DrawingExceptionMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)

---
#### `IsBase`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsEmpty`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsVisible`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `MapArguments`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `OverlayCanvas`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Canvas`

---

### Protected Properties

#### `IsOverlayInitialized`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `PreviousExtent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `PreviousScale`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---

### Public Methods

#### `Close()`

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
#### `Dispose()`

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
#### `Draw(RectangleShape)`

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
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `Draw(RectangleShape,OverlayRefreshType)`

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
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

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
#### `GetBoundingBox()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `GetMarkersForDrawing(RectangleShape)`

**Summary**

   *This method gets a marker collection for drawing on the overlay.*

**Remarks**

   *This is a wrapper method for the abstract method GetDrawingMarkersCore.*

**Return Value**

|Type|Description|
|---|---|
|GeoCollection<[`Marker`](ThinkGeo.UI.Wpf.Marker.md)>|An marker collection for drawing.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the extent in world coordinate for requesting markers.|

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
#### `Initialize(MapArguments)`

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
|mapArguments|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|

---
#### `LoadState(Byte[])`

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
|state|`Byte[]`|N/A|

---
#### `Open()`

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
#### `PanTo(RectangleShape)`

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
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `Refresh(RectangleShape)`

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
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `Refresh()`

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
#### `Refresh(IEnumerable<RectangleShape>)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|

---
#### `Refresh(TimeSpan)`

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
|bufferTime|`TimeSpan`|N/A|

---
#### `Refresh(TimeSpan,RequestDrawingBufferTimeType)`

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
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

---
#### `Refresh(RectangleShape,TimeSpan)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|bufferTime|`TimeSpan`|N/A|

---
#### `Refresh(RectangleShape,TimeSpan,RequestDrawingBufferTimeType)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

---
#### `Refresh(IEnumerable<RectangleShape>,TimeSpan)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|bufferTime|`TimeSpan`|N/A|

---
#### `Refresh(IEnumerable<RectangleShape>,TimeSpan,RequestDrawingBufferTimeType)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

---
#### `SaveState()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|N/A|

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

#### `CloseCore()`

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
#### `Dispose(Boolean)`

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
|disposing|`Boolean`|N/A|

---
#### `DrawAttribution(GeoCanvas)`

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
#### `DrawAttributionCore(GeoCanvas)`

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
#### `DrawCore(RectangleShape,OverlayRefreshType)`

**Summary**

   *This method creates markers in the provided world extent and places on the overlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the extent in world coordinate for filtering markers.|
|overlayRefreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|This parameter indicates if the elements in the overlay needs to be refreshed.|

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
#### `GetBoundingBoxCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetMarkersForDrawingCore(RectangleShape)`

**Summary**

   *This abstract method gets a collection of marker for drawing on the overlay.*

**Remarks**

   *When implementing this method, consider requesting points in the provided world extent, then create marker controls from these points.*

**Return Value**

|Type|Description|
|---|---|
|GeoCollection<[`Marker`](ThinkGeo.UI.Wpf.Marker.md)>|A collection of markers which are used for placing on the overlay.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the extent in world coordinate for creating markers.|

---
#### `InitializeCore(MapArguments)`

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
|mapArguments|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|

---
#### `LoadStateCore(Byte[])`

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
|state|`Byte[]`|N/A|

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
#### `OnDrawing(DrawingOverlayEventArgs)`

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
|e|[`DrawingOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingOverlayEventArgs.md)|N/A|

---
#### `OnDrawingAttribution(DrawingAttributionOverlayEventArgs)`

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
|args|[`DrawingAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingAttributionOverlayEventArgs.md)|N/A|

---
#### `OnDrawn(DrawnOverlayEventArgs)`

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
|e|[`DrawnOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnOverlayEventArgs.md)|N/A|

---
#### `OnDrawnAttribution(DrawnAttributionOverlayEventArgs)`

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
|args|[`DrawnAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnAttributionOverlayEventArgs.md)|N/A|

---
#### `OnRefreshing(OverlayRefreshType)`

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
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `OpenCore()`

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
#### `PanToCore(RectangleShape)`

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
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `RefreshCore(RectangleShape)`

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
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `RefreshCore()`

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
#### `SaveStateCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Events

#### Drawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingOverlayEventArgs.md)

#### Drawn

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnOverlayEventArgs.md)

#### DrawingAttribution

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingAttributionOverlayEventArgs.md)

#### DrawnAttribution

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnAttributionOverlayEventArgs.md)


