# TileOverlay


## Inheritance Hierarchy

+ `Object`
  + [`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)
    + **`TileOverlay`**
      + [`BingMapsOverlay`](ThinkGeo.UI.Wpf.BingMapsOverlay.md)
      + [`GoogleMapsOverlay`](ThinkGeo.UI.Wpf.GoogleMapsOverlay.md)
      + [`LayerOverlay`](ThinkGeo.UI.Wpf.LayerOverlay.md)
      + [`OpenStreetMapOverlay`](ThinkGeo.UI.Wpf.OpenStreetMapOverlay.md)
      + [`ThinkGeoCloudRasterMapsOverlay`](ThinkGeo.UI.Wpf.ThinkGeoCloudRasterMapsOverlay.md)
      + [`ThinkGeoCloudVectorMapsOverlay`](ThinkGeo.UI.Wpf.ThinkGeoCloudVectorMapsOverlay.md)
      + [`ThinkGeoMBTilesOverlay`](ThinkGeo.UI.Wpf.ThinkGeoMBTilesOverlay.md)
      + [`WmsOverlay`](ThinkGeo.UI.Wpf.WmsOverlay.md)
      + [`WmtsTiledOverlay`](ThinkGeo.UI.Wpf.WmtsTiledOverlay.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`TileOverlay()`](#tileoverlay)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`AutoRefreshInterval`](#autorefreshinterval)|`TimeSpan`|N/A|
|[`CanRefreshRegion`](#canrefreshregion)|`Boolean`|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`ImageFormat`](#imageformat)|[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)|Gets and sets drawing format for the tiles.|
|[`IsBase`](#isbase)|`Boolean`|N/A|
|[`IsCacheOnly`](#iscacheonly)|`Boolean`|Gets or sets a value indicating whether this instance is cache only.|
|[`IsEmpty`](#isempty)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|Gets or sets if this overlay is visible.|
|[`JpegQuality`](#jpegquality)|`Int32`|Gets or sets the image quality when the TileImageFormat is Jpeg; otherwise this property has no effects. Its default value is 80.|
|[`MapArguments`](#maparguments)|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|N/A|
|[`MaxExtent`](#maxextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This property gets or sets the max extent of matrix to calculate the tiles.|
|[`Name`](#name)|`String`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|N/A|
|[`RenderMode`](#rendermode)|[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)|This property gets and sets the render mode for drawing this overlay.|
|[`TileBuffer`](#tilebuffer)|`Int32`|This property gets and sets the number of extra rows and colums of tiles on each side which will surround the minimum grid tiles to cover the map.|
|[`TileCache`](#tilecache)|[`RasterTileCache`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileCache.md)|Gets and sets a tile cache object for saving the tiles.|
|[`TileHeight`](#tileheight)|`Int32`|Gets and sets the height of the tile.|
|[`TileResolution`](#tileresolution)|[`TileResolution`](../ThinkGeo.Core/ThinkGeo.Core.TileResolution.md)|Gets or sets the tile resolution.|
|[`TileSizeMode`](#tilesizemode)|[`TileSizeMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSizeMode.md)|Gets or sets the tile size mode.|
|[`TileSnappingMode`](#tilesnappingmode)|[`TileSnappingMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSnappingMode.md)|Gets or sets the tileView snapping mode.|
|[`TileType`](#tiletype)|[`TileType`](ThinkGeo.UI.Wpf.TileType.md)|Gets and sets the overlay is formed by multiple tiles or single tile.|
|[`TileWidth`](#tilewidth)|`Int32`|Gets and sets the width of the tile.|
|[`WrappingExtent`](#wrappingextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`WrappingMode`](#wrappingmode)|[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)|Thie property gets or sets whether allow wrap date line.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`DrawingCanvas`](#drawingcanvas)|`Canvas`|Gets current drawing canvas which holds actual tile controls.|
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
|N/A|

### Protected Constructors

#### `TileOverlay()`

**Summary**

   *Constructor of TileOverlay class.*

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

   *Gets or sets a value indicating whether this instance is cache only.*

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

   *Gets or sets if this overlay is visible.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `JpegQuality`

**Summary**

   *Gets or sets the image quality when the TileImageFormat is Jpeg; otherwise this property has no effects. Its default value is 80.*

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

   *This property gets or sets the max extent of matrix to calculate the tiles.*

**Remarks**

   *By default, MaxExtent is null; the matrix is created depending on the GeographyUnit of current map. When it's DecimalDegree, the matrix' is calculated as (-180, 90, 180, -90) as MaxExtent. While it's not DecimalDegree, the matrix is (-20037508.2314698, 20037508.2314698, 20037508.2314698, -20037508.2314698); It's allowed to modify it to customize the matrix.*

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
#### `RenderMode`

**Summary**

   *This property gets and sets the render mode for drawing this overlay.*

**Remarks**

   *Set Skia to render map image with Skia. We recommend use this value with large data. Set DrawingVisual to render map image with DrawingVisual feature in WPF. Use it when the spatial data is small to get better responding.*

**Return Value**

[`RenderMode`](ThinkGeo.UI.Wpf.RenderMode.md)

---
#### `TileBuffer`

**Summary**

   *This property gets and sets the number of extra rows and colums of tiles on each side which will surround the minimum grid tiles to cover the map.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `TileCache`

**Summary**

   *Gets and sets a tile cache object for saving the tiles.*

**Remarks**

   *N/A*

**Return Value**

[`RasterTileCache`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileCache.md)

---
#### `TileHeight`

**Summary**

   *Gets and sets the height of the tile.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `TileResolution`

**Summary**

   *Gets or sets the tile resolution.*

**Remarks**

   *N/A*

**Return Value**

[`TileResolution`](../ThinkGeo.Core/ThinkGeo.Core.TileResolution.md)

---
#### `TileSizeMode`

**Summary**

   *Gets or sets the tile size mode.*

**Remarks**

   *N/A*

**Return Value**

[`TileSizeMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSizeMode.md)

---
#### `TileSnappingMode`

**Summary**

   *Gets or sets the tileView snapping mode.*

**Remarks**

   *N/A*

**Return Value**

[`TileSnappingMode`](../ThinkGeo.Core/ThinkGeo.Core.TileSnappingMode.md)

---
#### `TileType`

**Summary**

   *Gets and sets the overlay is formed by multiple tiles or single tile.*

**Remarks**

   *N/A*

**Return Value**

[`TileType`](ThinkGeo.UI.Wpf.TileType.md)

---
#### `TileWidth`

**Summary**

   *Gets and sets the width of the tile.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

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

   *Thie property gets or sets whether allow wrap date line.*

**Remarks**

   *N/A*

**Return Value**

[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)

---

### Protected Properties

#### `DrawingCanvas`

**Summary**

   *Gets current drawing canvas which holds actual tile controls.*

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

   *Clears the tiles.*

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

   *Draws current overlay with provided world extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|A world extent for drawing.|
|overlayRefreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `DrawException(GeoCanvas,Exception)`

**Summary**

   *This method will draw on the canvas when the layer.Draw throw exception and the DrawExceptionMode is set to DrawException instead of ThrowException.*

**Remarks**

   *This method can be overriden its logic by rewrite the DrawExceptionCore.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|The target canvas to draw the layer.|
|e|`Exception`|The exception thrown when layer.Draw().|

---
#### `DrawExceptionCore(GeoCanvas,Exception)`

**Summary**

   *This method will draw on the canvas when the layer.Draw throw exception and the DrawExceptionMode is set to DrawException instead of ThrowException.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|The target canvas to draw the layer.|
|e|`Exception`|The exception thrown when layer.Draw().|

---
#### `DrawTile(TileView,MapArguments)`

**Summary**

   *Redraws a specified tile with the provided world extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tile|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|A Tile object that is created by the GetTile() method to draw.|
|mapArguments|[`MapArguments`](ThinkGeo.UI.Wpf.MapArguments.md)|A mapArguments for drawing the passed tile.|

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

   *Redraws a specified tile with the provided world extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|geoCanvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|A geoCanvas for drawing the passed tile.|
|tile|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|A TileView object that is created by the GetTile() method to draw.|

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

   *This method gets the cells for drawing in the passed world extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|A dictionary of cells for drawing in the passed world extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|A world extent for getting the drawing cells.|

---
#### `GetDrawingCellsCore(RectangleShape)`

**Summary**

   *This method gets the cells for drawing in the passed world extent.*

**Remarks**

   *When overriding this method, consider that the TileBuffer affects the passed world extent.*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,[`MatrixCell`](../ThinkGeo.Core/ThinkGeo.Core.MatrixCell.md)>|A dictionary of cells for drawing in the passed world extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|A world extent for getting the drawing cells.|

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

   *Chooses a tile object to form the TileOverlay. When overriding this method, consider the initialize parameters setting on the tile.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TileView`](ThinkGeo.UI.Wpf.TileView.md)|A tile object to form the TileOverlay.|

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

   *This method raises before a tile is drawing.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|[`DrawingTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawingTileTileOverlayEventArgs.md)|This is an event argument for DrawingTile event.|

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

   *This method raises after a tile is drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|[`DrawnTileTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawnTileTileOverlayEventArgs.md)|This is an event argument for DrawnTile event.|

---
#### `OnDrawTilesProgressChanged(DrawTilesProgressChangedTileOverlayEventArgs)`

**Summary**

   *Occurs when Tiles' download progress is changed.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|[`DrawTilesProgressChangedTileOverlayEventArgs`](ThinkGeo.UI.Wpf.DrawTilesProgressChangedTileOverlayEventArgs.md)|Event arguments for DownloadProgress event.|

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

   *Refreshes tiles on the overlay. When calling this mehtod, all the tiles including map tiles and stretched tiles will be cleared. And reform with new tiles.*

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


