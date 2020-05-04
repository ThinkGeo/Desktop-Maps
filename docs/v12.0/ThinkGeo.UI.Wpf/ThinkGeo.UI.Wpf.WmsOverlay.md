# WmsOverlay


## Inheritance Hierarchy

+ `Object`
  + [`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)
    + [`TileOverlay`](ThinkGeo.UI.Wpf.TileOverlay.md)
      + **`WmsOverlay`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`WmsOverlay()`](#wmsoverlay)|
|[`WmsOverlay(Uri)`](#wmsoverlayuri)|
|[`WmsOverlay(Uri,IWebProxy)`](#wmsoverlayuriiwebproxy)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`AutoRefreshInterval`](#autorefreshinterval)|`TimeSpan`|N/A|
|[`AxisOrder`](#axisorder)|[`WmsAxisOrder`](../ThinkGeo.Core/ThinkGeo.Core.WmsAxisOrder.md)|N/A|
|[`CanRefreshRegion`](#canrefreshregion)|`Boolean`|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`ImageFormat`](#imageformat)|[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)|Gets and sets drawing format for the tiles.|
|[`IsBase`](#isbase)|`Boolean`|N/A|
|[`IsCacheOnly`](#iscacheonly)|`Boolean`|N/A|
|[`IsEmpty`](#isempty)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`JpegQuality`](#jpegquality)|`Int32`|N/A|
|[`MapArguments`](#maparguments)|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|
|[`MaxExtent`](#maxextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|N/A|
|[`Parameters`](#parameters)|Dictionary<`String`,`String`>|Gets a dictionary whose items will be passed to the WMS server as parameters.|
|[`Projection`](#projection)|`String`|Gets or sets a string that will be sent to the WMS server to get the images in the specific projection.|
|[`RenderMode`](#rendermode)|[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)|N/A|
|[`ServerUri`](#serveruri)|`Uri`|Gets or Set a URI that specifies the location of the WMS server.|
|[`TileBuffer`](#tilebuffer)|`Int32`|N/A|
|[`TileCache`](#tilecache)|[`RasterTileCache`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileCache.md)|N/A|
|[`TileHeight`](#tileheight)|`Int32`|N/A|
|[`TileResolution`](#tileresolution)|[`TileResolution`](../ThinkGeo.Core/ThinkGeo.Core.TileResolution.md)|N/A|
|[`TileSizeMode`](#tilesizemode)|[`TileSizeMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSizeMode.md)|N/A|
|[`TileSnappingMode`](#tilesnappingmode)|[`TileSnappingMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSnappingMode.md)|N/A|
|[`TileType`](#tiletype)|[`TileType`](ThinkGeo.UI.Wpf.TileType.md)|N/A|
|[`TileWidth`](#tilewidth)|`Int32`|N/A|
|[`TimeoutInSeconds`](#timeoutinseconds)|`Int32`|Gets or sets the length of time, in seconds, before the request times out.|
|[`WebProxy`](#webproxy)|`IWebProxy`|This property gets or sets the proxy used for requesting a Web Response.|
|[`WrappingExtent`](#wrappingextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`WrappingMode`](#wrappingmode)|[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`DrawingCanvas`](#drawingcanvas)|`Canvas`|N/A|
|[`IsOverlayInitialized`](#isoverlayinitialized)|`Boolean`|N/A|
|[`PreviousExtent`](#previousextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`PreviousScale`](#previousscale)|`Double`|N/A|

### Public Methods Summary


|Name|
|---|
|[`ClearTiles()`](#cleartiles)|
|[`Close()`](#close)|
|[`Dispose()`](#dispose)|
|[`Draw(RectangleShape)`](#drawrectangleshape)|
|[`Draw(RectangleShape,OverlayRefreshType)`](#drawrectangleshapeoverlayrefreshtype)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetHashCode()`](#gethashcode)|
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
|[`DrawException(GeoCanvas,Exception)`](#drawexceptiongeocanvasexception)|
|[`DrawExceptionCore(GeoCanvas,Exception)`](#drawexceptioncoregeocanvasexception)|
|[`DrawTile(TileView,MapArguments)`](#drawtiletileviewmaparguments)|
|[`DrawTileAsync(TileView,MapArguments)`](#drawtileasynctileviewmaparguments)|
|[`DrawTileCore(GeoCanvas,TileView)`](#drawtilecoregeocanvastileview)|
|[`DrawTileCoreAsync(GeoCanvas,TileView)`](#drawtilecoreasyncgeocanvastileview)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetBufferedExtent(RectangleShape,Double)`](#getbufferedextentrectangleshapedouble)|
|[`GetDrawingCells(RectangleShape)`](#getdrawingcellsrectangleshape)|
|[`GetDrawingCellsCore(RectangleShape)`](#getdrawingcellscorerectangleshape)|
|[`GetSortedCells(Dictionary<String,MatrixCell>,RectangleShape)`](#getsortedcellsdictionary<stringmatrixcell>rectangleshape)|
|[`GetSortedCellsCore(Dictionary<String,MatrixCell>,RectangleShape)`](#getsortedcellscoredictionary<stringmatrixcell>rectangleshape)|
|[`GetTile(RectangleShape,Int32,Int32,Int64,Int64,Int32)`](#gettilerectangleshapeint32int32int64int64int32)|
|[`GetTileCore()`](#gettilecore)|
|[`GetTileMatrix(Double)`](#gettilematrixdouble)|
|[`InitializeCore(MapArguments)`](#initializecoremaparguments)|
|[`LoadStateCore(Byte[])`](#loadstatecorebyte[])|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnDrawing(DrawingOverlayEventArgs)`](#ondrawingdrawingoverlayeventargs)|
|[`OnDrawingAttribution(DrawingAttributionOverlayEventArgs)`](#ondrawingattributiondrawingattributionoverlayeventargs)|
|[`OnDrawingException(DrawingExceptionTileOverlayEventArgs)`](#ondrawingexceptiondrawingexceptiontileoverlayeventargs)|
|[`OnDrawingTile(DrawingTileTileOverlayEventArgs)`](#ondrawingtiledrawingtiletileoverlayeventargs)|
|[`OnDrawn(DrawnOverlayEventArgs)`](#ondrawndrawnoverlayeventargs)|
|[`OnDrawnAttribution(DrawnAttributionOverlayEventArgs)`](#ondrawnattributiondrawnattributionoverlayeventargs)|
|[`OnDrawnException(DrawnExceptionTileOverlayEventArgs)`](#ondrawnexceptiondrawnexceptiontileoverlayeventargs)|
|[`OnDrawnTile(DrawnTileTileOverlayEventArgs)`](#ondrawntiledrawntiletileoverlayeventargs)|
|[`OnDrawTilesProgressChanged(DrawTilesProgressChangedTileOverlayEventArgs)`](#ondrawtilesprogresschangeddrawtilesprogresschangedtileoverlayeventargs)|
|[`OnRefreshing(OverlayRefreshType)`](#onrefreshingoverlayrefreshtype)|
|[`OnSendingWebRequest(SendingWebRequestEventArgs)`](#onsendingwebrequestsendingwebrequesteventargs)|
|[`OnSentWebRequest(SentWebRequestEventArgs)`](#onsentwebrequestsentwebrequesteventargs)|
|[`OnTileTypeChanged(TileTypeChangedTileOverlayEventArgs)`](#ontiletypechangedtiletypechangedtileoverlayeventargs)|
|[`OnTileTypeChanging(TileTypeChangingTileOverlayEventArgs)`](#ontiletypechangingtiletypechangingtileoverlayeventargs)|
|[`OpenCore()`](#opencore)|
|[`PanToCore(RectangleShape)`](#pantocorerectangleshape)|
|[`PrefillDataToTiles(IEnumerable<TileView>)`](#prefilldatatotilesienumerable<tileview>)|
|[`PrefillDataToTilesCore(IEnumerable<TileView>)`](#prefilldatatotilescoreienumerable<tileview>)|
|[`RefreshCore()`](#refreshcore)|
|[`RefreshCore(RectangleShape)`](#refreshcorerectangleshape)|
|[`SaveStateCore()`](#savestatecore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`SendingWebRequest`](#sendingwebrequest)|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|
|[`SentWebRequest`](#sentwebrequest)|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|
|[`DrawTilesProgressChanged`](#drawtilesprogresschanged)|[`DrawTilesProgressChangedTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawTilesProgressChangedTileOverlayEventArgs.md)|N/A|
|[`DrawingTile`](#drawingtile)|[`DrawingTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingTileTileOverlayEventArgs.md)|N/A|
|[`DrawnTile`](#drawntile)|[`DrawnTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnTileTileOverlayEventArgs.md)|N/A|
|[`DrawingException`](#drawingexception)|[`DrawingExceptionTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingExceptionTileOverlayEventArgs.md)|N/A|
|[`DrawnException`](#drawnexception)|[`DrawnExceptionTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnExceptionTileOverlayEventArgs.md)|N/A|
|[`TileTypeChanged`](#tiletypechanged)|[`TileTypeChangedTileOverlayEventArgs`](ThinkGeo.UI.Wpf.TileTypeChangedTileOverlayEventArgs.md)|N/A|
|[`TileTypeChanging`](#tiletypechanging)|[`TileTypeChangingTileOverlayEventArgs`](ThinkGeo.UI.Wpf.TileTypeChangingTileOverlayEventArgs.md)|N/A|
|[`Drawing`](#drawing)|[`DrawingOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingOverlayEventArgs.md)|N/A|
|[`Drawn`](#drawn)|[`DrawnOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnOverlayEventArgs.md)|N/A|
|[`DrawingAttribution`](#drawingattribution)|[`DrawingAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingAttributionOverlayEventArgs.md)|N/A|
|[`DrawnAttribution`](#drawnattribution)|[`DrawnAttributionOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnAttributionOverlayEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`WmsOverlay()`](#wmsoverlay)|
|[`WmsOverlay(Uri)`](#wmsoverlayuri)|
|[`WmsOverlay(Uri,IWebProxy)`](#wmsoverlayuriiwebproxy)|

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
#### `AxisOrder`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`WmsAxisOrder`](../ThinkGeo.Core/ThinkGeo.Core.WmsAxisOrder.md)

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
#### `ImageFormat`

**Summary**

   *Gets and sets drawing format for the tiles.*

**Remarks**

   *N/A*

**Return Value**

[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)

---
#### `IsBase`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsCacheOnly`

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
#### `JpegQuality`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `MapArguments`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)

---
#### `MaxExtent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

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
#### `Parameters`

**Summary**

   *Gets a dictionary whose items will be passed to the WMS server as parameters.*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,`String`>

---
#### `Projection`

**Summary**

   *Gets or sets a string that will be sent to the WMS server to get the images in the specific projection.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `RenderMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)

---
#### `ServerUri`

**Summary**

   *Gets or Set a URI that specifies the location of the WMS server.*

**Remarks**

   *N/A*

**Return Value**

`Uri`

---
#### `TileBuffer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `TileCache`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RasterTileCache`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileCache.md)

---
#### `TileHeight`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `TileResolution`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TileResolution`](../ThinkGeo.Core/ThinkGeo.Core.TileResolution.md)

---
#### `TileSizeMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TileSizeMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSizeMode.md)

---
#### `TileSnappingMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TileSnappingMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSnappingMode.md)

---
#### `TileType`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TileType`](ThinkGeo.UI.Wpf.TileType.md)

---
#### `TileWidth`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `TimeoutInSeconds`

**Summary**

   *Gets or sets the length of time, in seconds, before the request times out.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `WebProxy`

**Summary**

   *This property gets or sets the proxy used for requesting a Web Response.*

**Remarks**

   *N/A*

**Return Value**

`IWebProxy`

---
#### `WrappingExtent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `WrappingMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)

---

### Protected Properties

#### `DrawingCanvas`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Canvas`

---
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

#### `ClearTiles()`

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
#### `DrawException(GeoCanvas,Exception)`

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
|e|`Exception`|N/A|

---
#### `DrawExceptionCore(GeoCanvas,Exception)`

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
|e|`Exception`|N/A|

---
#### `DrawTile(TileView,MapArguments)`

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
|tile|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|N/A|
|mapArguments|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|

---
#### `DrawTileAsync(TileView,MapArguments)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Task`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tile|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|N/A|
|mapArguments|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|

---
#### `DrawTileCore(GeoCanvas,TileView)`

**Summary**

   *This method redraws a tile by an extent and geography unit.*

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
|tile|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|A tile which needs to be redrawn.|

---
#### `DrawTileCoreAsync(GeoCanvas,TileView)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Task`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|geoCanvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|tile|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|N/A|

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
#### `GetDrawingCells(RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetDrawingCellsCore(RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetSortedCells(Dictionary<String,MatrixCell>,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|cells|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetSortedCellsCore(Dictionary<String,MatrixCell>,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|cells|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|N/A|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetTile(RectangleShape,Int32,Int32,Int64,Int64,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|tileScreenWidth|`Int32`|N/A|
|tileScreenHeight|`Int32`|N/A|
|tileColumnIndex|`Int64`|N/A|
|tileRowIndex|`Int64`|N/A|
|zoomLevelIndex|`Int32`|N/A|

---
#### `GetTileCore()`

**Summary**

   *This method gets a specific tile object to form an overlay.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|A TileView object to form an overlay.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetTileMatrix(Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TileMatrix`](../ThinkGeo.Core/ThinkGeo.Core.TileMatrix.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|N/A|

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
#### `OnDrawingException(DrawingExceptionTileOverlayEventArgs)`

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
|e|[`DrawingExceptionTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingExceptionTileOverlayEventArgs.md)|N/A|

---
#### `OnDrawingTile(DrawingTileTileOverlayEventArgs)`

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
|args|[`DrawingTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingTileTileOverlayEventArgs.md)|N/A|

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
#### `OnDrawnException(DrawnExceptionTileOverlayEventArgs)`

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
|e|[`DrawnExceptionTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnExceptionTileOverlayEventArgs.md)|N/A|

---
#### `OnDrawnTile(DrawnTileTileOverlayEventArgs)`

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
|args|[`DrawnTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnTileTileOverlayEventArgs.md)|N/A|

---
#### `OnDrawTilesProgressChanged(DrawTilesProgressChangedTileOverlayEventArgs)`

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
|args|[`DrawTilesProgressChangedTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawTilesProgressChangedTileOverlayEventArgs.md)|N/A|

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
#### `OnSendingWebRequest(SendingWebRequestEventArgs)`

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
|e|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|

---
#### `OnSentWebRequest(SentWebRequestEventArgs)`

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
|e|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|

---
#### `OnTileTypeChanged(TileTypeChangedTileOverlayEventArgs)`

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
|e|[`TileTypeChangedTileOverlayEventArgs`](ThinkGeo.UI.Wpf.TileTypeChangedTileOverlayEventArgs.md)|N/A|

---
#### `OnTileTypeChanging(TileTypeChangingTileOverlayEventArgs)`

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
|e|[`TileTypeChangingTileOverlayEventArgs`](ThinkGeo.UI.Wpf.TileTypeChangingTileOverlayEventArgs.md)|N/A|

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
#### `PrefillDataToTiles(IEnumerable<TileView>)`

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
|tiles|IEnumerable<[`TileView`](ThinkGeo.UI.Wpf.TileView.md)>|N/A|

---
#### `PrefillDataToTilesCore(IEnumerable<TileView>)`

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
|tiles|IEnumerable<[`TileView`](ThinkGeo.UI.Wpf.TileView.md)>|N/A|

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

#### SendingWebRequest

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)

#### SentWebRequest

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)

#### DrawTilesProgressChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawTilesProgressChangedTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawTilesProgressChangedTileOverlayEventArgs.md)

#### DrawingTile

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingTileTileOverlayEventArgs.md)

#### DrawnTile

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnTileTileOverlayEventArgs.md)

#### DrawingException

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingExceptionTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingExceptionTileOverlayEventArgs.md)

#### DrawnException

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnExceptionTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnExceptionTileOverlayEventArgs.md)

#### TileTypeChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`TileTypeChangedTileOverlayEventArgs`](ThinkGeo.UI.Wpf.TileTypeChangedTileOverlayEventArgs.md)

#### TileTypeChanging

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`TileTypeChangingTileOverlayEventArgs`](ThinkGeo.UI.Wpf.TileTypeChangingTileOverlayEventArgs.md)

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


