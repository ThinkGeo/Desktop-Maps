# ExtentInteractiveOverlay


## Inheritance Hierarchy

+ `Object`
  + [`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)
    + [`InteractiveOverlay`](ThinkGeo.UI.Wpf.InteractiveOverlay.md)
      + **`ExtentInteractiveOverlay`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ExtentInteractiveOverlay()`](#extentinteractiveoverlay)|

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
|[`DoubleLeftClickMode`](#doubleleftclickmode)|[`MapDoubleClickMode`](ThinkGeo.UI.Wpf.MapDoubleClickMode.md)|Gets or sets the DoubleLeftClickMode used for the ExtentInteractiveOverlay.|
|[`DoubleRightClickMode`](#doublerightclickmode)|[`MapDoubleClickMode`](ThinkGeo.UI.Wpf.MapDoubleClickMode.md)|Gets or sets the DoubleRightClickMode used for the ExtentInteractiveOverlay.|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingMarginPercentage`](#drawingmarginpercentage)|`Double`|N/A|
|[`ExtentChangedType`](#extentchangedtype)|[`ExtentChangedType`](ThinkGeo.UI.Wpf.ExtentChangedType.md)|Gets or sets the ExtentChangedType for the ExtentInteractiveOverlay.|
|[`IsBase`](#isbase)|`Boolean`|N/A|
|[`IsEmpty`](#isempty)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`MapArguments`](#maparguments)|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|
|[`MapZoomMode`](#mapzoommode)|[`MapZoomMode`](ThinkGeo.UI.Wpf.MapZoomMode.md)|N/A|
|[`MinimumTrackZoomInExtentInPixels`](#minimumtrackzoominextentinpixels)|`Single`|N/A|
|[`MouseWheelMode`](#mousewheelmode)|[`MapMouseWheelMode`](ThinkGeo.UI.Wpf.MapMouseWheelMode.md)|Gets or sets the MouseWheelMode used for the ExtentInteractiveOverlay.|
|[`Name`](#name)|`String`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|N/A|
|[`PanMode`](#panmode)|[`MapPanMode`](ThinkGeo.UI.Wpf.MapPanMode.md)|Gets or sets the PanMode used for the ExtentInteractiveOverlay.|
|[`RenderMode`](#rendermode)|[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)|N/A|
|[`RotationKey`](#rotationkey)|`Key`|Gets or sets the Keys used in LeftClickDrag for the ExtentInteractiveOverlay.|
|[`RotationMouseButton`](#rotationmousebutton)|[`MapMouseButton`](ThinkGeo.UI.Wpf.MapMouseButton.md)|Gets or sets the LeftClickDragMode used for the ExtentInteractiveOverlay.|
|[`TrackZoomInKey`](#trackzoominkey)|`Key`|Gets or sets the Keys used in RightClickDrag for the ExtentInteractiveOverlay.|
|[`TrackZoomInMouseButton`](#trackzoominmousebutton)|[`MapMouseButton`](ThinkGeo.UI.Wpf.MapMouseButton.md)|Gets or sets the RightClickDragMode used for the ExtentInteractiveOverlay.|
|[`ZoomPercentage`](#zoompercentage)|`Int32`|Gets or sets the zoom percentage when using the mouse wheel or double-clicking to zoom the MapControl.|
|[`ZoomSnapDirection`](#zoomsnapdirection)|[`ZoomSnapDirection`](ThinkGeo.UI.Wpf.ZoomSnapDirection.md)|N/A|

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
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetBufferedExtent(RectangleShape,Double)`](#getbufferedextentrectangleshapedouble)|
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
|[`OnRefreshing(OverlayRefreshType)`](#onrefreshingoverlayrefreshtype)|
|[`OpenCore()`](#opencore)|
|[`PanToCore(RectangleShape)`](#pantocorerectangleshape)|
|[`RefreshCore(RectangleShape)`](#refreshcorerectangleshape)|
|[`RefreshCore()`](#refreshcore)|
|[`SaveStateCore()`](#savestatecore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
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
|[`ExtentInteractiveOverlay()`](#extentinteractiveoverlay)|

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
#### `DoubleLeftClickMode`

**Summary**

   *Gets or sets the DoubleLeftClickMode used for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

[`MapDoubleClickMode`](ThinkGeo.UI.Wpf.MapDoubleClickMode.md)

---
#### `DoubleRightClickMode`

**Summary**

   *Gets or sets the DoubleRightClickMode used for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

[`MapDoubleClickMode`](ThinkGeo.UI.Wpf.MapDoubleClickMode.md)

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
#### `ExtentChangedType`

**Summary**

   *Gets or sets the ExtentChangedType for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

[`ExtentChangedType`](ThinkGeo.UI.Wpf.ExtentChangedType.md)

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
#### `MapZoomMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`MapZoomMode`](ThinkGeo.UI.Wpf.MapZoomMode.md)

---
#### `MinimumTrackZoomInExtentInPixels`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `MouseWheelMode`

**Summary**

   *Gets or sets the MouseWheelMode used for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

[`MapMouseWheelMode`](ThinkGeo.UI.Wpf.MapMouseWheelMode.md)

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
#### `PanMode`

**Summary**

   *Gets or sets the PanMode used for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

[`MapPanMode`](ThinkGeo.UI.Wpf.MapPanMode.md)

---
#### `RenderMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)

---
#### `RotationKey`

**Summary**

   *Gets or sets the Keys used in LeftClickDrag for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

`Key`

---
#### `RotationMouseButton`

**Summary**

   *Gets or sets the LeftClickDragMode used for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

[`MapMouseButton`](ThinkGeo.UI.Wpf.MapMouseButton.md)

---
#### `TrackZoomInKey`

**Summary**

   *Gets or sets the Keys used in RightClickDrag for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

`Key`

---
#### `TrackZoomInMouseButton`

**Summary**

   *Gets or sets the RightClickDragMode used for the ExtentInteractiveOverlay.*

**Remarks**

   *N/A*

**Return Value**

[`MapMouseButton`](ThinkGeo.UI.Wpf.MapMouseButton.md)

---
#### `ZoomPercentage`

**Summary**

   *Gets or sets the zoom percentage when using the mouse wheel or double-clicking to zoom the MapControl.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `ZoomSnapDirection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ZoomSnapDirection`](ThinkGeo.UI.Wpf.ZoomSnapDirection.md)

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

   *This method actually draws this overlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The bounding box to draw.|
|overlayRefreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|The refresh type.|

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
|geoCanvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|

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

   *This overrides the MouseEnter logic in its base class InterativeOverlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)||

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

   *This overrides the MouseWheel logic in its base class InterativeOverlay.*

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


