# EditInteractiveOverlay


## Inheritance Hierarchy

+ `Object`
  + [`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)
    + [`InteractiveOverlay`](ThinkGeo.UI.Wpf.InteractiveOverlay.md)
      + **`EditInteractiveOverlay`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`EditInteractiveOverlay()`](#editinteractiveoverlay)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`AutoRefreshInterval`](#autorefreshinterval)|`TimeSpan`|N/A|
|[`CanAddVertex`](#canaddvertex)|`Boolean`|Gets a value which indicates whether the shape can Add new vertex.|
|[`CanDrag`](#candrag)|`Boolean`|Gets a value which indicates whether the shape can be dragged.|
|[`CanRefreshRegion`](#canrefreshregion)|`Boolean`|N/A|
|[`CanRemoveVertex`](#canremovevertex)|`Boolean`|Gets a value which indicates whether the shape can remove a existing vertex.|
|[`CanReshape`](#canreshape)|`Boolean`|Gets a value which indicates whether the shape can be reshaped.|
|[`CanResize`](#canresize)|`Boolean`|Gets a value which indicates whether the shape can be resized.|
|[`CanRotate`](#canrotate)|`Boolean`|Gets a value which indicates whether the shape can be rotated.|
|[`DragControlPointsLayer`](#dragcontrolpointslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|This property gets the InMemoryFeatureLayer which holds the control points for drag.|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingMarginPercentage`](#drawingmarginpercentage)|`Double`|N/A|
|[`EditShapesLayer`](#editshapeslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|This property gets the InMemoryFeatureLayer which holds the edit shapes.|
|[`EditsInProcessLayer`](#editsinprocesslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|N/A|
|[`ExistingControlPointsLayer`](#existingcontrolpointslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|This property gets the InMemoryFeatureLayer which holds the control points which represents the existing vertices of the edit shapes.|
|[`IsBase`](#isbase)|`Boolean`|N/A|
|[`IsEmpty`](#isempty)|`Boolean`|This property indicates whether this overlay is empty or not.|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`MapArguments`](#maparguments)|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|N/A|
|[`PolygonTrackMode`](#polygontrackmode)|[`PolygonTrackMode`](ThinkGeo.UI.Wpf.PolygonTrackMode.md)|N/A|
|[`RenderMode`](#rendermode)|[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)|N/A|
|[`ResizeControlPointsLayer`](#resizecontrolpointslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|This property gets the InMemoryFeatureLayer which holds the control points for resize.|
|[`RotateControlPointsLayer`](#rotatecontrolpointslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|This property gets the InMemoryFeatureLayer which holds the control points for rotate.|
|[`SelectedControlPointLayer`](#selectedcontrolpointlayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|N/A|
|[`SelectedEditsInProcessLayer`](#selectededitsinprocesslayer)|[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ControlPointType`](#controlpointtype)|[`ControlPointType`](ThinkGeo.UI.Wpf.ControlPointType.md)|Gets or sets the ControlPointType for the control points of the edit shapes.|
|[`IsOverlayInitialized`](#isoverlayinitialized)|`Boolean`|N/A|
|[`OriginalEditingFeature`](#originaleditingfeature)|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This property gets the feature represents the original editing feature.|
|[`PreviousExtent`](#previousextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`PreviousScale`](#previousscale)|`Double`|N/A|
|[`SelectedControlPointFeature`](#selectedcontrolpointfeature)|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This property gets the feature represents the select control points of the edit shapes.|

### Public Methods Summary


|Name|
|---|
|[`CalculateAllControlPoints()`](#calculateallcontrolpoints)|
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
|[`AddVertex(PointShape,Double)`](#addvertexpointshapedouble)|
|[`AddVertexCore(Feature,PointShape,Double)`](#addvertexcorefeaturepointshapedouble)|
|[`CalculateDragControlPoints()`](#calculatedragcontrolpoints)|
|[`CalculateDragControlPointsCore(Feature)`](#calculatedragcontrolpointscorefeature)|
|[`CalculateResizeControlPoints()`](#calculateresizecontrolpoints)|
|[`CalculateResizeControlPointsCore(Feature)`](#calculateresizecontrolpointscorefeature)|
|[`CalculateRotateControlPoints()`](#calculaterotatecontrolpoints)|
|[`CalculateRotateControlPointsCore(Feature)`](#calculaterotatecontrolpointscorefeature)|
|[`CalculateVertexControlPoints()`](#calculatevertexcontrolpoints)|
|[`CalculateVertexControlPointsCore(Feature)`](#calculatevertexcontrolpointscorefeature)|
|[`ClearAllControlPoints()`](#clearallcontrolpoints)|
|[`CloseCore()`](#closecore)|
|[`Dispose(Boolean)`](#disposeboolean)|
|[`DragFeature(Feature,PointShape,PointShape)`](#dragfeaturefeaturepointshapepointshape)|
|[`DragFeatureCore(Feature,PointShape,PointShape)`](#dragfeaturecorefeaturepointshapepointshape)|
|[`DrawAttribution(GeoCanvas)`](#drawattributiongeocanvas)|
|[`DrawAttributionCore(GeoCanvas)`](#drawattributioncoregeocanvas)|
|[`DrawCore(RectangleShape,OverlayRefreshType)`](#drawcorerectangleshapeoverlayrefreshtype)|
|[`DrawTile(LayerTileView)`](#drawtilelayertileview)|
|[`DrawTileCore(GeoCanvas)`](#drawtilecoregeocanvas)|
|[`EndEditing(PointShape)`](#endeditingpointshape)|
|[`EndEditingCore(PointShape)`](#endeditingcorepointshape)|
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
|[`MoveVertex(Feature,PointShape,PointShape)`](#movevertexfeaturepointshapepointshape)|
|[`MoveVertexCore(Feature,PointShape,PointShape)`](#movevertexcorefeaturepointshapepointshape)|
|[`OnControlPointSelected(ControlPointSelectedEditInteractiveOverlayEventArgs)`](#oncontrolpointselectedcontrolpointselectededitinteractiveoverlayeventargs)|
|[`OnControlPointSelecting(ControlPointSelectingEditInteractiveOverlayEventArgs)`](#oncontrolpointselectingcontrolpointselectingeditinteractiveoverlayeventargs)|
|[`OnDrawing(DrawingOverlayEventArgs)`](#ondrawingdrawingoverlayeventargs)|
|[`OnDrawingAttribution(DrawingAttributionOverlayEventArgs)`](#ondrawingattributiondrawingattributionoverlayeventargs)|
|[`OnDrawn(DrawnOverlayEventArgs)`](#ondrawndrawnoverlayeventargs)|
|[`OnDrawnAttribution(DrawnAttributionOverlayEventArgs)`](#ondrawnattributiondrawnattributionoverlayeventargs)|
|[`OnFeatureDragged(FeatureDraggedEditInteractiveOverlayEventArgs)`](#onfeaturedraggedfeaturedraggededitinteractiveoverlayeventargs)|
|[`OnFeatureDragging(FeatureDraggingEditInteractiveOverlayEventArgs)`](#onfeaturedraggingfeaturedraggingeditinteractiveoverlayeventargs)|
|[`OnFeatureDropped(FeatureDroppedEditInteractiveOverlayEventArgs)`](#onfeaturedroppedfeaturedroppededitinteractiveoverlayeventargs)|
|[`OnFeatureEdited(FeatureEditedEditInteractiveOverlayEventArgs)`](#onfeatureeditedfeatureeditededitinteractiveoverlayeventargs)|
|[`OnFeatureEditing(FeatureEditingEditInteractiveOverlayEventArgs)`](#onfeatureeditingfeatureeditingeditinteractiveoverlayeventargs)|
|[`OnFeatureResized(FeatureResizedEditInteractiveOverlayEventArgs)`](#onfeatureresizedfeatureresizededitinteractiveoverlayeventargs)|
|[`OnFeatureResizing(FeatureResizingEditInteractiveOverlayEventArgs)`](#onfeatureresizingfeatureresizingeditinteractiveoverlayeventargs)|
|[`OnFeatureRotated(FeatureRotatedEditInteractiveOverlayEventArgs)`](#onfeaturerotatedfeaturerotatededitinteractiveoverlayeventargs)|
|[`OnFeatureRotating(FeatureRotatingEditInteractiveOverlayEventArgs)`](#onfeaturerotatingfeaturerotatingeditinteractiveoverlayeventargs)|
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
|[`OnVertexAdded(VertexAddedEditInteractiveOverlayEventArgs)`](#onvertexaddedvertexaddededitinteractiveoverlayeventargs)|
|[`OnVertexAdding(VertexAddingEditInteractiveOverlayEventArgs)`](#onvertexaddingvertexaddingeditinteractiveoverlayeventargs)|
|[`OnVertexMoved(VertexMovedEditInteractiveOverlayEventArgs)`](#onvertexmovedvertexmovededitinteractiveoverlayeventargs)|
|[`OnVertexMoving(VertexMovingEditInteractiveOverlayEventArgs)`](#onvertexmovingvertexmovingeditinteractiveoverlayeventargs)|
|[`OnVertexRemoved(VertexRemovedEditInteractiveOverlayEventArgs)`](#onvertexremovedvertexremovededitinteractiveoverlayeventargs)|
|[`OnVertexRemoving(VertexRemovingEditInteractiveOverlayEventArgs)`](#onvertexremovingvertexremovingeditinteractiveoverlayeventargs)|
|[`OpenCore()`](#opencore)|
|[`PanToCore(RectangleShape)`](#pantocorerectangleshape)|
|[`RefreshCore(RectangleShape)`](#refreshcorerectangleshape)|
|[`RefreshCore()`](#refreshcore)|
|[`RemoveVertex(PointShape,Double)`](#removevertexpointshapedouble)|
|[`RemoveVertexCore(Feature,Vertex,Double)`](#removevertexcorefeaturevertexdouble)|
|[`ResizeFeature(Feature,PointShape,PointShape)`](#resizefeaturefeaturepointshapepointshape)|
|[`ResizeFeatureCore(Feature,PointShape,PointShape)`](#resizefeaturecorefeaturepointshapepointshape)|
|[`RotateFeature(Feature,PointShape,PointShape)`](#rotatefeaturefeaturepointshapepointshape)|
|[`RotateFeatureCore(Feature,PointShape,PointShape)`](#rotatefeaturecorefeaturepointshapepointshape)|
|[`SaveStateCore()`](#savestatecore)|
|[`SetSelectedControlPoint(PointShape,Double)`](#setselectedcontrolpointpointshapedouble)|
|[`SetSelectedControlPointCore(PointShape,Double)`](#setselectedcontrolpointcorepointshapedouble)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`FeatureDragged`](#featuredragged)|[`FeatureDraggedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDraggedEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureDragging`](#featuredragging)|[`FeatureDraggingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDraggingEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureDropped`](#featuredropped)|[`FeatureDroppedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDroppedEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureResized`](#featureresized)|[`FeatureResizedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureResizedEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureResizing`](#featureresizing)|[`FeatureResizingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureResizingEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureRotated`](#featurerotated)|[`FeatureRotatedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureRotatedEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureRotating`](#featurerotating)|[`FeatureRotatingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureRotatingEditInteractiveOverlayEventArgs.md)|N/A|
|[`VertexAdded`](#vertexadded)|[`VertexAddedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddedEditInteractiveOverlayEventArgs.md)|N/A|
|[`VertexAdding`](#vertexadding)|[`VertexAddingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddingEditInteractiveOverlayEventArgs.md)|N/A|
|[`VertexMoved`](#vertexmoved)|[`VertexMovedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexMovedEditInteractiveOverlayEventArgs.md)|N/A|
|[`VertexMoving`](#vertexmoving)|[`VertexMovingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexMovingEditInteractiveOverlayEventArgs.md)|N/A|
|[`VertexRemoved`](#vertexremoved)|[`VertexRemovedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexRemovedEditInteractiveOverlayEventArgs.md)|N/A|
|[`VertexRemoving`](#vertexremoving)|[`VertexRemovingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexRemovingEditInteractiveOverlayEventArgs.md)|N/A|
|[`ControlPointSelected`](#controlpointselected)|[`ControlPointSelectedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.ControlPointSelectedEditInteractiveOverlayEventArgs.md)|N/A|
|[`ControlPointSelecting`](#controlpointselecting)|[`ControlPointSelectingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.ControlPointSelectingEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureEditing`](#featureediting)|[`FeatureEditingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureEditingEditInteractiveOverlayEventArgs.md)|N/A|
|[`FeatureEdited`](#featureedited)|[`FeatureEditedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureEditedEditInteractiveOverlayEventArgs.md)|N/A|
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
|[`EditInteractiveOverlay()`](#editinteractiveoverlay)|

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
#### `CanAddVertex`

**Summary**

   *Gets a value which indicates whether the shape can Add new vertex.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CanDrag`

**Summary**

   *Gets a value which indicates whether the shape can be dragged.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CanRefreshRegion`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CanRemoveVertex`

**Summary**

   *Gets a value which indicates whether the shape can remove a existing vertex.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CanReshape`

**Summary**

   *Gets a value which indicates whether the shape can be reshaped.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CanResize`

**Summary**

   *Gets a value which indicates whether the shape can be resized.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CanRotate`

**Summary**

   *Gets a value which indicates whether the shape can be rotated.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `DragControlPointsLayer`

**Summary**

   *This property gets the InMemoryFeatureLayer which holds the control points for drag.*

**Remarks**

   *Every control points for drag are not the existing vertex of the edit shapes.*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

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
#### `EditShapesLayer`

**Summary**

   *This property gets the InMemoryFeatureLayer which holds the edit shapes.*

**Remarks**

   *N/A*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---
#### `EditsInProcessLayer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---
#### `ExistingControlPointsLayer`

**Summary**

   *This property gets the InMemoryFeatureLayer which holds the control points which represents the existing vertices of the edit shapes.*

**Remarks**

   *Every control points in this layer are the existing vertices of the edit shapes.*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

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

   *This property indicates whether this overlay is empty or not.*

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
#### `ResizeControlPointsLayer`

**Summary**

   *This property gets the InMemoryFeatureLayer which holds the control points for resize.*

**Remarks**

   *Every control points for resize are not the existing vertex of the edit shapes.*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---
#### `RotateControlPointsLayer`

**Summary**

   *This property gets the InMemoryFeatureLayer which holds the control points for rotate.*

**Remarks**

   *Every control points for rotate are not the existing vertex of the edit shapes.*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---
#### `SelectedControlPointLayer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---
#### `SelectedEditsInProcessLayer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)

---

### Protected Properties

#### `ControlPointType`

**Summary**

   *Gets or sets the ControlPointType for the control points of the edit shapes.*

**Remarks**

   *N/A*

**Return Value**

[`ControlPointType`](ThinkGeo.UI.Wpf.ControlPointType.md)

---
#### `IsOverlayInitialized`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `OriginalEditingFeature`

**Summary**

   *This property gets the feature represents the original editing feature.*

**Remarks**

   *N/A*

**Return Value**

[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)

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
#### `SelectedControlPointFeature`

**Summary**

   *This property gets the feature represents the select control points of the edit shapes.*

**Remarks**

   *N/A*

**Return Value**

[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)

---

### Public Methods

#### `CalculateAllControlPoints()`

**Summary**

   *This method calculates all control points.*

**Remarks**

   *First, it will clear all control points. Then it will calculate each control points according to their settings.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
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

#### `AddVertex(PointShape,Double)`

**Summary**

   *This is the method to add vertex from a feature.*

**Remarks**

   *This method is the concrete wrapper for the abstract method AddVertexCore. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|True if add vertex succeed , other wise return false.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifies the point shape to search the vertex.|
|searchingTolerance|`Double`|This parameter specifes the searching tolerance to search the vetex.|

---
#### `AddVertexCore(Feature,PointShape,Double)`

**Summary**

   *This is the Core method of AddVertex which encapsulate the logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a vertex added feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature to be add vertex from.|
|targetPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifies the target vertex to be added.|
|searchingTolerance|`Double`|This parameter specifes the searching tolerance to search the vetex.|

---
#### `CalculateDragControlPoints()`

**Summary**

   *This method caculates the Drag control points for all the features in the EditShapesLayer. You can override its logic by rewrite its core method.*

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
#### `CalculateDragControlPointsCore(Feature)`

**Summary**

   *This is the core API for the CalculateDragControlPoints, you can override this method if you want to change its logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|A collection of features stands for the Drag control points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The target feature to caculate the control point.|

---
#### `CalculateResizeControlPoints()`

**Summary**

   *This method caculates the Resize control points for all the features in the EditShapesLayer. You can override its logic by rewrite its core method.*

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
#### `CalculateResizeControlPointsCore(Feature)`

**Summary**

   *This is the core API for the CalculateResizeControlPoints, you can override this method if you want to change its logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|A collection of features stands for the Resize control points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The target feature to caculate the control point.|

---
#### `CalculateRotateControlPoints()`

**Summary**

   *This method caculates the Rotate control points for all the features in the EditShapesLayer. You can override its logic by rewrite its core method.*

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
#### `CalculateRotateControlPointsCore(Feature)`

**Summary**

   *This is the core API for the CalculateRotateControlPoints, you can override this method if you want to change its logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|A collection of features stands for the Rotate control points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The target feature to caculate the control point.|

---
#### `CalculateVertexControlPoints()`

**Summary**

   *This method caculates the vertex control points for all the features in the EditShapesLayer. You can override its logic by rewrite its core method.*

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
#### `CalculateVertexControlPointsCore(Feature)`

**Summary**

   *This is the core API for the CalculateVertexControlPoints, you can override this method if you want to change its logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|A collection of features stands for the Vertex control points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The target feature to caculate the control point.|

---
#### `ClearAllControlPoints()`

**Summary**

   *This method clears all control points in corresponding layer.*

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
#### `DragFeature(Feature,PointShape,PointShape)`

**Summary**

   *This is the method to Drag a feature.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DragFeatureCore. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a dragged feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be dragged.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to drag the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to drag the feature.|

---
#### `DragFeatureCore(Feature,PointShape,PointShape)`

**Summary**

   *This is the Core method of DragFeature which encapsulate the logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a dragged feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be dragged.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to drag the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to drag the feature.|

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
#### `EndEditing(PointShape)`

**Summary**

   *This method End the editing for the interative editing on the feature in the EditShapesLayer. You can override its logic by rewrite its core method.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This is the targetPointShape possible be used when overriding.|

---
#### `EndEditingCore(PointShape)`

**Summary**

   *This is the core method of EndEditing method. This method End the editing for the interative editing on the feature in the EditShapesLayer.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This is the targetPointShape possible be used when overriding.|

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
#### `MoveVertex(Feature,PointShape,PointShape)`

**Summary**

   *This is the method to move vertex from a feature.*

**Remarks**

   *This method is the concrete wrapper for the abstract method MoveVertexCore. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a rotated feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be move vertex from.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to move vertex from the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to move vertex from the feature.|

---
#### `MoveVertexCore(Feature,PointShape,PointShape)`

**Summary**

   *This is the Core method of MoveVertex which encapsulate the logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a vertex moved feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be move vertex from.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to move vertex from the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to move vertex from the feature.|

---
#### `OnControlPointSelected(ControlPointSelectedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after control point selected.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ControlPointSelectedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.ControlPointSelectedEditInteractiveOverlayEventArgs.md)|The ControlPointSelectedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnControlPointSelecting(ControlPointSelectingEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before control point selected.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ControlPointSelectingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.ControlPointSelectingEditInteractiveOverlayEventArgs.md)|The ControlPointSelectingEditInteractiveOverlayEventArgs passed for the event raised.|

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
#### `OnFeatureDragged(FeatureDraggedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after dragging the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`FeatureDraggedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDraggedEditInteractiveOverlayEventArgs.md)|The FeatureDraggedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnFeatureDragging(FeatureDraggingEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before dragging the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`FeatureDraggingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDraggingEditInteractiveOverlayEventArgs.md)|The FeatureDraggingEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnFeatureDropped(FeatureDroppedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after drop the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`FeatureDroppedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDroppedEditInteractiveOverlayEventArgs.md)|The FeatureDroppedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnFeatureEdited(FeatureEditedEditInteractiveOverlayEventArgs)`

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
|e|[`FeatureEditedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureEditedEditInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnFeatureEditing(FeatureEditingEditInteractiveOverlayEventArgs)`

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
|e|[`FeatureEditingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureEditingEditInteractiveOverlayEventArgs.md)|N/A|

---
#### `OnFeatureResized(FeatureResizedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after resizing the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`FeatureResizedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureResizedEditInteractiveOverlayEventArgs.md)|The FeatureResizedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnFeatureResizing(FeatureResizingEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before resizing the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`FeatureResizingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureResizingEditInteractiveOverlayEventArgs.md)|The FeatureResizingEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnFeatureRotated(FeatureRotatedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after rotating the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`FeatureRotatedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureRotatedEditInteractiveOverlayEventArgs.md)|The FeatureRotatedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnFeatureRotating(FeatureRotatingEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before rotating the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`FeatureRotatingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureRotatingEditInteractiveOverlayEventArgs.md)|The FeatureRotatingEditInteractiveOverlayEventArgs passed for the event raised.|

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
#### `OnVertexAdded(VertexAddedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after vertex added to the edit feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexAddedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddedEditInteractiveOverlayEventArgs.md)|The VertexAddedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnVertexAdding(VertexAddingEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before vertex added to the edit feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexAddingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddingEditInteractiveOverlayEventArgs.md)|The VertexAddingEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnVertexMoved(VertexMovedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after moving the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexMovedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexMovedEditInteractiveOverlayEventArgs.md)|The VertexMovedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnVertexMoving(VertexMovingEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before moving the feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexMovingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexMovingEditInteractiveOverlayEventArgs.md)|The VertexMovingEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnVertexRemoved(VertexRemovedEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired after vertex removed from the edit feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexRemovedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexRemovedEditInteractiveOverlayEventArgs.md)|The VertexRemovedEditInteractiveOverlayEventArgs passed for the event raised.|

---
#### `OnVertexRemoving(VertexRemovingEditInteractiveOverlayEventArgs)`

**Summary**

   *This event will be fired before vertex removed from the edit feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`VertexRemovingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexRemovingEditInteractiveOverlayEventArgs.md)|The VertexRemovingEditInteractiveOverlayEventArgs passed for the event raised.|

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
#### `RemoveVertex(PointShape,Double)`

**Summary**

   *This is the method to remove vertex from a feature.*

**Remarks**

   *This method is the concrete wrapper for the abstract method RemoveVertexCore. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|True if remove vertex succeed , other wise return false.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifies the point shape to search the vertex.|
|searchingTolerance|`Double`|This parameter specifes the searching tolerance to search the vetex.|

---
#### `RemoveVertexCore(Feature,Vertex,Double)`

**Summary**

   *This is the Core method of RemoveVertex which encapsulate the logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a vertex removed feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|editShapeFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the target feature to be remove vertex from.|
|selectedVertex|[`Vertex`](../ThinkGeo.Core/ThinkGeo.Core.Vertex.md)|This parameter specifies the selected vertex to search the vertex.|
|searchingTolerance|`Double`|This parameter specifes the searching tolerance to search the vetex.|

---
#### `ResizeFeature(Feature,PointShape,PointShape)`

**Summary**

   *This is the method to Resize a feature.*

**Remarks**

   *This method is the concrete wrapper for the abstract method ResizeFeatureCore. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a resized feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be resized.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to resize the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to resize the feature.|

---
#### `ResizeFeatureCore(Feature,PointShape,PointShape)`

**Summary**

   *This is the Core method of ResizeFeature which encapsulate the logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a resized feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be resized.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to resize the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to resize the feature.|

---
#### `RotateFeature(Feature,PointShape,PointShape)`

**Summary**

   *This is the method to Rotate a feature.*

**Remarks**

   *This method is the concrete wrapper for the abstract method RotateFeatureCore. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a rotated feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be rotated.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to rotate the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to rotate the feature.|

---
#### `RotateFeatureCore(Feature,PointShape,PointShape)`

**Summary**

   *This is the Core method of RotateFeature which encapsulate the logic.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|Returns a resized feature.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sourceFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter specifies the source feature to be rotated.|
|sourceControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the source control point to rotate the feature.|
|targetControlPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter specifes the target control point to rotate the feature.|

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
#### `SetSelectedControlPoint(PointShape,Double)`

**Summary**

   *This protected method is to set the control point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|Returns true if control point are found and set correct, other wise, returns false.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is target point shape we determine to edit.|
|searchingTolerance|`Double`|This parameter is the searchinig tolerance to seach the control point.|

---
#### `SetSelectedControlPointCore(PointShape,Double)`

**Summary**

   *This protected virtual method is the Core method of SetSelectedControlPoint API.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|A feature stands for the selected control point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetPointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is target point shape we determine to edit.|
|searchingTolerance|`Double`|This parameter is the searchinig tolerance to seach the control point.|

---

### Public Events

#### FeatureDragged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureDraggedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDraggedEditInteractiveOverlayEventArgs.md)

#### FeatureDragging

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureDraggingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDraggingEditInteractiveOverlayEventArgs.md)

#### FeatureDropped

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureDroppedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureDroppedEditInteractiveOverlayEventArgs.md)

#### FeatureResized

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureResizedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureResizedEditInteractiveOverlayEventArgs.md)

#### FeatureResizing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureResizingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureResizingEditInteractiveOverlayEventArgs.md)

#### FeatureRotated

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureRotatedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureRotatedEditInteractiveOverlayEventArgs.md)

#### FeatureRotating

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureRotatingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureRotatingEditInteractiveOverlayEventArgs.md)

#### VertexAdded

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexAddedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddedEditInteractiveOverlayEventArgs.md)

#### VertexAdding

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexAddingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexAddingEditInteractiveOverlayEventArgs.md)

#### VertexMoved

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexMovedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexMovedEditInteractiveOverlayEventArgs.md)

#### VertexMoving

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexMovingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexMovingEditInteractiveOverlayEventArgs.md)

#### VertexRemoved

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexRemovedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexRemovedEditInteractiveOverlayEventArgs.md)

#### VertexRemoving

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`VertexRemovingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.VertexRemovingEditInteractiveOverlayEventArgs.md)

#### ControlPointSelected

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ControlPointSelectedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.ControlPointSelectedEditInteractiveOverlayEventArgs.md)

#### ControlPointSelecting

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ControlPointSelectingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.ControlPointSelectingEditInteractiveOverlayEventArgs.md)

#### FeatureEditing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureEditingEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureEditingEditInteractiveOverlayEventArgs.md)

#### FeatureEdited

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FeatureEditedEditInteractiveOverlayEventArgs`](ThinkGeo.UI.Wpf.FeatureEditedEditInteractiveOverlayEventArgs.md)

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


