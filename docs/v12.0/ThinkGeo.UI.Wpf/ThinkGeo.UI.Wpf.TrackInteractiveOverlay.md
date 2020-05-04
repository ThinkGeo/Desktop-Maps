# TrackInteractiveOverlay


## Inheritance Hierarchy

+ `Object`
  + [`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)
    + [`InteractiveOverlay`](ThinkGeo.UI.Wpf.InteractiveOverlay.md)
      + **`TrackInteractiveOverlay`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`TrackInteractiveOverlay()`](#trackinteractiveoverlay)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`AutoRefreshInterval`](#autorefreshinterval)|`TimeSpan`|N/A|
|[`CanRefreshRegion`](#canrefreshregion)|`Boolean`|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingMarginPercentage`](#drawingmarginpercentage)|`Double`|N/A|
|[`IsBase`](#isbase)|`Boolean`|N/A|
|[`IsEmpty`](#isempty)|`Boolean`|This property override the logic in its base class by watching the feature count in trackShapeLayer. If it is empty ,we can skip drawing it for better performance.|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`MapArguments`](#maparguments)|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|N/A|
|[`PolygonTrackMode`](#polygontrackmode)|[`PolygonTrackMode`](ThinkGeo.UI.Wpf.PolygonTrackMode.md)|N/A|
|[`RenderMode`](#rendermode)|[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)|N/A|
|[`TrackMode`](#trackmode)|[`TrackMode`](ThinkGeo.UI.Wpf.TrackMode.md)|Gets a mode of TrackOverlay.|
|[`TrackShapeLayer`](#trackshapelayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|This property gets the TrackShape layers which holds the track shapes.|
|[`TrackShapesInProcessLayer`](#trackshapesinprocesslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|N/A|
|[`VertexCountInQuarter`](#vertexcountinquarter)|`Int32`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsOverlayInitialized`](#isoverlayinitialized)|`Boolean`|N/A|
|[`MouseDownCount`](#mousedowncount)|`Int32`|Gets and sets the the times of mouse click.|
|[`PreviousExtent`](#previousextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`PreviousScale`](#previousscale)|`Double`|N/A|
|[`Vertices`](#vertices)|Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>|This property gets the vertices to make up the track shape. This is a protected property which probablly need to be used in its sub classes.|

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
|[`GetTrackingShape()`](#gettrackingshape)|
|[`GetType()`](#gettype)|
|[`Initialize(MapArguments)`](#initializemaparguments)|
|[`KeyDown(KeyEventInteractionArguments)`](#keydownkeyeventinteractionarguments)|
|[`KeyUp(KeyEventInteractionArguments)`](#keyupkeyeventinteractionarguments)|
|[`LoadState(Byte[])`](#loadstatebyte[])|
|[`ManipulationCompleted(InteractionArguments)`](#manipulationcompletedinteractionarguments)|
|[`ManipulationDelta(InteractionArguments)`](#manipulationdeltainteractionarguments)|
|[`ManipulationStarted(InteractionArguments)`](#manipulationstartedinteractionarguments)|
|[`MouseClick(InteractionArguments)`](#mouseclickinteractionarguments)|
|[`MouseDoubleClick(InteractionArguments)`](#mousedoubleclickinteractionarguments)|
|[`MouseDown(InteractionArguments)`](#mousedowninteractionarguments)|
|[`MouseEnter(InteractionArguments)`](#mouseenterinteractionarguments)|
|[`MouseLeave(InteractionArguments)`](#mouseleaveinteractionarguments)|
|[`MouseMove(InteractionArguments)`](#mousemoveinteractionarguments)|
|[`MouseUp(InteractionArguments)`](#mouseupinteractionarguments)|
|[`MouseWheel(InteractionArguments)`](#mousewheelinteractionarguments)|
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
|[`DrawTile(LayerTileView)`](#drawtilelayertileview)|
|[`DrawTileCore(GeoCanvas)`](#drawtilecoregeocanvas)|
|[`EndTracking()`](#endtracking)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetBufferedExtent(RectangleShape,Double)`](#getbufferedextentrectangleshapedouble)|
|[`GetTrackingShapeCore()`](#gettrackingshapecore)|
|[`InitializeCore(MapArguments)`](#initializecoremaparguments)|
|[`KeyDownCore(KeyEventInteractionArguments)`](#keydowncorekeyeventinteractionarguments)|
|[`KeyUpCore(KeyEventInteractionArguments)`](#keyupcorekeyeventinteractionarguments)|
|[`LoadStateCore(Byte[])`](#loadstatecorebyte[])|
|[`ManipulationCompletedCore(InteractionArguments)`](#manipulationcompletedcoreinteractionarguments)|
|[`ManipulationDeltaCore(InteractionArguments)`](#manipulationdeltacoreinteractionarguments)|
|[`ManipulationStartedCore(InteractionArguments)`](#manipulationstartedcoreinteractionarguments)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`MouseClickCore(InteractionArguments)`](#mouseclickcoreinteractionarguments)|
|[`MouseDoubleClickCore(InteractionArguments)`](#mousedoubleclickcoreinteractionarguments)|
|[`MouseDownCore(InteractionArguments)`](#mousedowncoreinteractionarguments)|
|[`MouseEnterCore(InteractionArguments)`](#mouseentercoreinteractionarguments)|
|[`MouseLeaveCore(InteractionArguments)`](#mouseleavecoreinteractionarguments)|
|[`MouseMoveCore(InteractionArguments)`](#mousemovecoreinteractionarguments)|
|[`MouseUpCore(InteractionArguments)`](#mouseupcoreinteractionarguments)|
|[`MouseWheelCore(InteractionArguments)`](#mousewheelcoreinteractionarguments)|
|[`OnDrawing(DrawingOverlayEventArgs)`](#ondrawingdrawingoverlayeventargs)|
|[`OnDrawingAttribution(DrawingAttributionOverlayEventArgs)`](#ondrawingattributiondrawingattributionoverlayeventargs)|
|[`OnDrawn(DrawnOverlayEventArgs)`](#ondrawndrawnoverlayeventargs)|
|[`OnDrawnAttribution(DrawnAttributionOverlayEventArgs)`](#ondrawnattributiondrawnattributionoverlayeventargs)|
|[`OnMapKeyDown(MapKeyDownInteractiveOverlayEventArgs)`](#onmapkeydownmapkeydowninteractiveoverlayeventargs)|
|[`OnMapKeyUp(MapKeyUpInteractiveOverlayEventArgs)`](#onmapkeyupmapkeyupinteractiveoverlayeventargs)|
|[`OnMapMouseClick(MapMouseClickInteractiveOverlayEventArgs)`](#onmapmouseclickmapmouseclickinteractiveoverlayeventargs)|
|[`OnMapMouseDoubleClick(MapMouseDoubleClickInteractiveOverlayEventArgs)`](#onmapmousedoubleclickmapmousedoubleclickinteractiveoverlayeventargs)|
|[`OnMapMouseDown(MapMouseDownInteractiveOverlayEventArgs)`](#onmapmousedownmapmousedowninteractiveoverlayeventargs)|
|[`OnMapMouseEnter(MapMouseEnterInteractiveOverlayEventArgs)`](#onmapmouseentermapmouseenterinteractiveoverlayeventargs)|
|[`OnMapMouseLeave(MapMouseLeaveInteractiveOverlayEventArgs)`](#onmapmouseleavemapmouseleaveinteractiveoverlayeventargs)|
|[`OnMapMouseMove(MapMouseMoveInteractiveOverlayEventArgs)`](#onmapmousemovemapmousemoveinteractiveoverlayeventargs)|
|[`OnMapMouseUp(MapMouseUpInteractiveOverlayEventArgs)`](#onmapmouseupmapmouseupinteractiveoverlayeventargs)|
|[`OnMapMouseWheel(MapMouseWheelInteractiveOverlayEventArgs)`](#onmapmousewheelmapmousewheelinteractiveoverlayeventargs)|
|[`OnMouseMoved(MouseMovedTrackInteractiveOverlayEventArgs)`](#onmousemovedmousemovedtrackinteractiveoverlayeventargs)|
|[`OnRefreshing(OverlayRefreshType)`](#onrefreshingoverlayrefreshtype)|
|[`OnTrackEnded(TrackEndedTrackInteractiveOverlayEventArgs)`](#ontrackendedtrackendedtrackinteractiveoverlayeventargs)|
|[`OnTrackEnding(TrackEndingTrackInteractiveOverlayEventArgs)`](#ontrackendingtrackendingtrackinteractiveoverlayeventargs)|
|[`OnTrackStarted(TrackStartedTrackInteractiveOverlayEventArgs)`](#ontrackstartedtrackstartedtrackinteractiveoverlayeventargs)|
|[`OnTrackStarting(TrackStartingTrackInteractiveOverlayEventArgs)`](#ontrackstartingtrackstartingtrackinteractiveoverlayeventargs)|
|[`OnVertexAdded(VertexAddedTrackInteractiveOverlayEventArgs)`](#onvertexaddedvertexaddedtrackinteractiveoverlayeventargs)|
|[`OnVertexAdding(VertexAddingTrackInteractiveOverlayEventArgs)`](#onvertexaddingvertexaddingtrackinteractiveoverlayeventargs)|
|[`OpenCore()`](#opencore)|
|[`PanToCore(RectangleShape)`](#pantocorerectangleshape)|
|[`RefreshCore(RectangleShape)`](#refreshcorerectangleshape)|
|[`RefreshCore()`](#refreshcore)|
|[`SaveStateCore()`](#savestatecore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`TrackEnded`](#trackended)|[`TrackEndedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackEndedTrackInteractiveOverlayEventArgs.md)|N/A|
|[`TrackEnding`](#trackending)|[`TrackEndingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackEndingTrackInteractiveOverlayEventArgs.md)|N/A|
|[`TrackStarted`](#trackstarted)|[`TrackStartedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackStartedTrackInteractiveOverlayEventArgs.md)|N/A|
|[`TrackStarting`](#trackstarting)|[`TrackStartingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackStartingTrackInteractiveOverlayEventArgs.md)|N/A|
|[`VertexAdded`](#vertexadded)|[`VertexAddedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddedTrackInteractiveOverlayEventArgs.md)|N/A|
|[`VertexAdding`](#vertexadding)|[`VertexAddingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddingTrackInteractiveOverlayEventArgs.md)|N/A|
|[`MouseMoved`](#mousemoved)|[`MouseMovedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MouseMovedTrackInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseDown`](#mapmousedown)|[`MapMouseDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDownInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseMove`](#mapmousemove)|[`MapMouseMoveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseMoveInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseUp`](#mapmouseup)|[`MapMouseUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseUpInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseClick`](#mapmouseclick)|[`MapMouseClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseClickInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseDoubleClick`](#mapmousedoubleclick)|[`MapMouseDoubleClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDoubleClickInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseWheel`](#mapmousewheel)|[`MapMouseWheelInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseWheelInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseLeave`](#mapmouseleave)|[`MapMouseLeaveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseLeaveInteractiveOverlayEventArgs.md)|N/A|
|[`MapMouseEnter`](#mapmouseenter)|[`MapMouseEnterInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseEnterInteractiveOverlayEventArgs.md)|N/A|
|[`MapKeyDown`](#mapkeydown)|[`MapKeyDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyDownInteractiveOverlayEventArgs.md)|N/A|
|[`MapKeyUp`](#mapkeyup)|[`MapKeyUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyUpInteractiveOverlayEventArgs.md)|N/A|
|[`Drawing`](#drawing)|[`DrawingOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingOverlayEventArgs.md)|N/A|
|[`Drawn`](#drawn)|[`DrawnOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnOverlayEventArgs.md)|N/A|
|[`DrawingAttribution`](#drawingattribution)|[`DrawingAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingAttributionOverlayEventArgs.md)|N/A|
|[`DrawnAttribution`](#drawnattribution)|[`DrawnAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnAttributionOverlayEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`TrackInteractiveOverlay()`](#trackinteractiveoverlay)|

### Protected Constructors


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
#### `DrawingMarginPercentage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

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

   *This property override the logic in its base class by watching the feature count in trackShapeLayer. If it is empty ,we can skip drawing it for better performance.*

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
#### `PolygonTrackMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PolygonTrackMode`](ThinkGeo.UI.Wpf.PolygonTrackMode.md)

---
#### `RenderMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)

---
#### `TrackMode`

**Summary**

   *Gets a mode of TrackOverlay.*

**Remarks**

   *The default mode is TrackMode.None which means you cannot draw or edit features. By setting the mode to TrackMode.Point, TrackMode.Line, TrackMode.Polygon etc., you could add point, line or polygon to the FeatureOverlay. Setting the mode to TrackMode.Edit, you could edit the shapes.*

**Return Value**

[`TrackMode`](ThinkGeo.UI.Wpf.TrackMode.md)

---
#### `TrackShapeLayer`

**Summary**

   *This property gets the TrackShape layers which holds the track shapes.*

**Remarks**

   *N/A*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---
#### `TrackShapesInProcessLayer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---
#### `VertexCountInQuarter`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

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
#### `MouseDownCount`

**Summary**

   *Gets and sets the the times of mouse click.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

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
#### `Vertices`

**Summary**

   *This property gets the vertices to make up the track shape. This is a protected property which probablly need to be used in its sub classes.*

**Remarks**

   *N/A*

**Return Value**

Collection<[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)>

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
#### `GetTrackingShape()`

**Summary**

   *This method gets the current Tracking shape.*

**Remarks**

   *This method is the concrete wrapper for the abstract method GetTrackingShapeCore. This method draws the representation of the overlay based on the extent you provided.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|Returns a shape represents the current status of tracking shape.|

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
#### `KeyDown(KeyEventInteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|N/A|

---
#### `KeyUp(KeyEventInteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|N/A|

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
#### `ManipulationCompleted(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `ManipulationDelta(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `ManipulationStarted(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseClick(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseDoubleClick(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseDown(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseEnter(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseLeave(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseMove(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseUp(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseWheel(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

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

   *This method disposes unmanaged resourece in this object.*

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
|overlayRefreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `DrawTile(LayerTileView)`

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
|layerTile|[`LayerTileView`](ThinkGeo.UI.Wpf.LayerTileView.md)|N/A|

---
#### `DrawTileCore(GeoCanvas)`

**Summary**

   *This methods draws tracking shapes on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|geoCanvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|The drawing context for drawing shapes.|

---
#### `EndTracking()`

**Summary**

   *This method ends the tracking shape by initialize some variables.*

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
#### `GetBufferedExtent(RectangleShape,Double)`

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
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|resolution|`Double`|N/A|

---
#### `GetTrackingShapeCore()`

**Summary**

   *This is the Core method of GetTrackingShape.You could overrides this method to have your own logic. This method gets the current Tracking shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|Returns a shape represents the current status of tracking shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `KeyDownCore(KeyEventInteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|N/A|

---
#### `KeyUpCore(KeyEventInteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|N/A|

---
#### `LoadStateCore(Byte[])`

**Summary**

   *This method restore the overlay state back from the specified state.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|state|`Byte[]`|This parameter indicates the state for restore the overlay.|

---
#### `ManipulationCompletedCore(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `ManipulationDeltaCore(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `ManipulationStartedCore(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

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
#### `MouseClickCore(InteractionArguments)`

**Summary**

   *This overrides the MouseClick logic in its base class.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseDoubleClickCore(InteractionArguments)`

**Summary**

   *This overrides the MouseDoubleClick logic in its base class InterativeOverlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseDownCore(InteractionArguments)`

**Summary**

   *This overrides the MouseDown logic in its base class InterativeOverlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseEnterCore(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseLeaveCore(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

---
#### `MouseMoveCore(InteractionArguments)`

**Summary**

   *This overrides the MouseMove logic in its base class InterativeOverlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseUpCore(InteractionArguments)`

**Summary**

   *This overrides the MouseUp logic in its base class InterativeOverlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseWheelCore(InteractionArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|N/A|

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
#### `OnMapKeyDown(MapKeyDownInteractiveOverlayEventArgs)`

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
|e|[`MapKeyDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyDownInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapKeyUp(MapKeyUpInteractiveOverlayEventArgs)`

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
|e|[`MapKeyUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyUpInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseClick(MapMouseClickInteractiveOverlayEventArgs)`

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
|e|[`MapMouseClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseClickInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseDoubleClick(MapMouseDoubleClickInteractiveOverlayEventArgs)`

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
|e|[`MapMouseDoubleClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDoubleClickInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseDown(MapMouseDownInteractiveOverlayEventArgs)`

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
|e|[`MapMouseDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDownInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseEnter(MapMouseEnterInteractiveOverlayEventArgs)`

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
|e|[`MapMouseEnterInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseEnterInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseLeave(MapMouseLeaveInteractiveOverlayEventArgs)`

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
|e|[`MapMouseLeaveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseLeaveInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseMove(MapMouseMoveInteractiveOverlayEventArgs)`

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
|e|[`MapMouseMoveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseMoveInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseUp(MapMouseUpInteractiveOverlayEventArgs)`

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
|e|[`MapMouseUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseUpInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMapMouseWheel(MapMouseWheelInteractiveOverlayEventArgs)`

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
|e|[`MapMouseWheelInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseWheelInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnMouseMoved(MouseMovedTrackInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired when mouse moved a vertex to the Tracking shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MouseMovedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MouseMovedTrackInteractiveOverlayEventArgs.md)|The MouseMovedTrackInteractiveOverlayEventArgs passed for the event raised.|

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
#### `OnTrackEnded(TrackEndedTrackInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after the end of Tracking a shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`TrackEndedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackEndedTrackInteractiveOverlayEventArgs.md)|The TrackEndedTrackInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnTrackEnding(TrackEndingTrackInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before the end of Tracking a shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`TrackEndingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackEndingTrackInteractiveOverlayEventArgs.md)|The TrackEndingTrackInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnTrackStarted(TrackStartedTrackInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after the start of Tracking a shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`TrackStartedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackStartedTrackInteractiveOverlayEventArgs.md)|The TrackStartedTrackInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnTrackStarting(TrackStartingTrackInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before the start of Tracking a shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`TrackStartingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackStartingTrackInteractiveOverlayEventArgs.md)|The TrackStartingTrackInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnVertexAdded(VertexAddedTrackInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after adding a vertex to the Tracking shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexAddedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddedTrackInteractiveOverlayEventArgs.md)|The VertexAddedTrackInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnVertexAdding(VertexAddingTrackInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before adding a vertex to the Tracking shape.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexAddingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddingTrackInteractiveOverlayEventArgs.md)|The VertexAddingTrackInteractiveOverlayEventArgs passed for the event raised.|

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

   *This method saves overlay state to a byte array.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|A byte array indicates current overlay state.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Events

#### TrackEnded

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`TrackEndedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackEndedTrackInteractiveOverlayEventArgs.md)

#### TrackEnding

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`TrackEndingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackEndingTrackInteractiveOverlayEventArgs.md)

#### TrackStarted

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`TrackStartedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackStartedTrackInteractiveOverlayEventArgs.md)

#### TrackStarting

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`TrackStartingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.TrackStartingTrackInteractiveOverlayEventArgs.md)

#### VertexAdded

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexAddedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddedTrackInteractiveOverlayEventArgs.md)

#### VertexAdding

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexAddingTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddingTrackInteractiveOverlayEventArgs.md)

#### MouseMoved

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MouseMovedTrackInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MouseMovedTrackInteractiveOverlayEventArgs.md)

#### MapMouseDown

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDownInteractiveOverlayEventArgs.md)

#### MapMouseMove

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseMoveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseMoveInteractiveOverlayEventArgs.md)

#### MapMouseUp

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseUpInteractiveOverlayEventArgs.md)

#### MapMouseClick

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseClickInteractiveOverlayEventArgs.md)

#### MapMouseDoubleClick

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseDoubleClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDoubleClickInteractiveOverlayEventArgs.md)

#### MapMouseWheel

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseWheelInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseWheelInteractiveOverlayEventArgs.md)

#### MapMouseLeave

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseLeaveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseLeaveInteractiveOverlayEventArgs.md)

#### MapMouseEnter

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapMouseEnterInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseEnterInteractiveOverlayEventArgs.md)

#### MapKeyDown

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapKeyDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyDownInteractiveOverlayEventArgs.md)

#### MapKeyUp

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapKeyUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyUpInteractiveOverlayEventArgs.md)

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


