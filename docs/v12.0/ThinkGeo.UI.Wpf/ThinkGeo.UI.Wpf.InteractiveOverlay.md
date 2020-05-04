# InteractiveOverlay


## Inheritance Hierarchy

+ `Object`
  + [`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)
    + **`InteractiveOverlay`**
      + [`EditInteractiveOverlay`](ThinkGeo.UI.Wpf.EditInteractiveOverlay.md)
      + [`ExtentInteractiveOverlay`](ThinkGeo.UI.Wpf.ExtentInteractiveOverlay.md)
      + [`PrinterInteractiveOverlay`](ThinkGeo.UI.Wpf.PrinterInteractiveOverlay.md)
      + [`TrackInteractiveOverlay`](ThinkGeo.UI.Wpf.TrackInteractiveOverlay.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`InteractiveOverlay()`](#interactiveoverlay)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`AutoRefreshInterval`](#autorefreshinterval)|`TimeSpan`|N/A|
|[`CanRefreshRegion`](#canrefreshregion)|`Boolean`|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingMarginPercentage`](#drawingmarginpercentage)|`Double`|This property gets and sets the extra drawing margin as a percentage around the map that draw to ensure that labeling is correct.|
|[`IsBase`](#isbase)|`Boolean`|N/A|
|[`IsEmpty`](#isempty)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`MapArguments`](#maparguments)|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|N/A|
|[`RenderMode`](#rendermode)|[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)|This property gets and sets the render mode for drawing this overlay.|

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
|N/A|

### Protected Constructors

#### `InteractiveOverlay()`

**Summary**

   *Default constructor of this abstract class.*

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
#### `DrawingMarginPercentage`

**Summary**

   *This property gets and sets the extra drawing margin as a percentage around the map that draw to ensure that labeling is correct.*

**Remarks**

   *This extra margin that we draw exists so that labels match up if they are partially cut off.*

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
#### `RenderMode`

**Summary**

   *This property gets and sets the render mode for drawing this overlay.*

**Remarks**

   *Set GdiPlus to render map image with Gdi+. We recommend use this value with large data. Set DrawingVisual to render map image with DrawingVisual feature in WPF. Use it when the spatial data is small to get better responding.*

**Return Value**

[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)

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

   *Occurs when key down on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for key down on the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|Interaction arguments for key down on the map.|

---
#### `KeyUp(KeyEventInteractionArguments)`

**Summary**

   *Occurs when key up on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for key up on the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|Interaction arguments for key up on the map.|

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

   *This method will simulate the MouseClick interaction.*

**Remarks**

   *This method is the concrete wrapper for its virtual Core method. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseDoubleClick(InteractionArguments)`

**Summary**

   *This method will simulate the MouseDoubleClick interaction.*

**Remarks**

   *This method is the concrete wrapper for its virtual Core method. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseDown(InteractionArguments)`

**Summary**

   *This method will simulate the MouseDown interaction.*

**Remarks**

   *This method is the concrete wrapper for its virtual Core method. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseEnter(InteractionArguments)`

**Summary**

   *Occurs when the mouse enter the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for mouse entering the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|Interaction arguments for mouse entering the map.|

---
#### `MouseLeave(InteractionArguments)`

**Summary**

   *Occurs when the mouse leave the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for mouse leave the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|Interaction arguments for mouse leave the map.|

---
#### `MouseMove(InteractionArguments)`

**Summary**

   *This method will simulate the MouseMove interaction.*

**Remarks**

   *This method is the concrete wrapper for its virtual Core method. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseUp(InteractionArguments)`

**Summary**

   *This method will simulate the MouseUp interaction.*

**Remarks**

   *This method is the concrete wrapper for its virtual Core method. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

---
#### `MouseWheel(InteractionArguments)`

**Summary**

   *This method will simulate the MouseWheel interaction.*

**Remarks**

   *This method is the concrete wrapper for its virtual Core method. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|Interaction results of this method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|This parameter is the interaction auguments for the method.|

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

   *This method draws the InterativeInterativeOverlay abstract class. You have to override this API in its sub concrete classes, or it will throw NotImplementedException.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent to draw.|
|overlayRefreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|This parameter indicates whether the overlay needs to be refreshed.|

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

   *Occurs when key down on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for key down on the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|Interaction arguments for key down on the map.|

---
#### `KeyUpCore(KeyEventInteractionArguments)`

**Summary**

   *Occurs when key up on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for key up on the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`KeyEventInteractionArguments`](ThinkGeo.UI.Wpf.KeyEventInteractionArguments.md)|Interaction arguments for key up on the map.|

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

   *This protected virtual method is the Core method of MouseClick API.*

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

   *This protected virtual method is the Core method of MouseDoubleClick API.*

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

   *This protected virtual method is the Core method of MouseDown API.*

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

   *Occurs when the mouse enter the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for mouse entering the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|Interaction arguments for mouse entering the map.|

---
#### `MouseLeaveCore(InteractionArguments)`

**Summary**

   *Occurs when the mouse leave the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`InteractiveResult`](ThinkGeo.UI.Wpf.InteractiveResult.md)|A interactive result for mouse leaving the map.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|interactionArguments|[`InteractionArguments`](ThinkGeo.UI.Wpf.InteractionArguments.md)|Interaction arguments for mouse leaving the map.|

---
#### `MouseMoveCore(InteractionArguments)`

**Summary**

   *This protected virtual method is the Core method of MouseMove API.*

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

   *This protected virtual method is the Core method of MouseUp API.*

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

   *This protected virtual method is the Core method of MouseWheel API.*

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

   *Occurs when key down on the map object.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapKeyDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyDownInteractiveOverlayEventArgs.md)|Event argument for the MapKeyDown event.|

---
#### `OnMapKeyUp(MapKeyUpInteractiveOverlayEventArgs)`

**Summary**

   *Occurs when key up on the map object.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapKeyUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapKeyUpInteractiveOverlayEventArgs.md)|Event argument for the MapKeyUp event.|

---
#### `OnMapMouseClick(MapMouseClickInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired when MapMouseClick is called.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseClickInteractiveOverlayEventArgs.md)|The MapMouseClickInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnMapMouseDoubleClick(MapMouseDoubleClickInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired when MapMouseDoubleClick is called.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseDoubleClickInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDoubleClickInteractiveOverlayEventArgs.md)|The MapMouseDoubleClickInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnMapMouseDown(MapMouseDownInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired when MapMouseDown is called.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseDownInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseDownInteractiveOverlayEventArgs.md)|The MapMouseDownInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnMapMouseEnter(MapMouseEnterInteractiveOverlayEventArgs)`

**Summary**

   *Occurs when mouse enter on the map object.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseEnterInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseEnterInteractiveOverlayEventArgs.md)|Event argument for the MapMouseEnter event.|

---
#### `OnMapMouseLeave(MapMouseLeaveInteractiveOverlayEventArgs)`

**Summary**

   *Occurs when mouse leave from the map object.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseLeaveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseLeaveInteractiveOverlayEventArgs.md)|Event argument for the MapMouseLeave event.|

---
#### `OnMapMouseMove(MapMouseMoveInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired when MapMouseMove is called.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseMoveInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseMoveInteractiveOverlayEventArgs.md)|The MapMouseMoveInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnMapMouseUp(MapMouseUpInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired when MapMouseUp is called.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseUpInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseUpInteractiveOverlayEventArgs.md)|The MapMouseUpInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnMapMouseWheel(MapMouseWheelInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired when MapMouseWheel is called.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapMouseWheelInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.MapMouseWheelInteractiveOverlayEventArgs.md)|The MapMouseWheelInteractiveOverlayEventArgs passed for the event raised.|

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


