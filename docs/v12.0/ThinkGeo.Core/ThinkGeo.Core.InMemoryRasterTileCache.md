# InMemoryRasterTileCache


## Inheritance Hierarchy

+ `Object`
  + [`TileCache`](../ThinkGeo.Core/ThinkGeo.Core.TileCache.md)
    + [`RasterTileCache`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileCache.md)
      + **`InMemoryRasterTileCache`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`InMemoryRasterTileCache()`](#inmemoryrastertilecache)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CacheId`](#cacheid)|`String`|N/A|
|[`ExpirationTime`](#expirationtime)|`TimeSpan`|N/A|
|[`ImageFormat`](#imageformat)|[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)|N/A|
|[`JpegQuality`](#jpegquality)|`Int16`|N/A|
|[`LoadingTileImage`](#loadingtileimage)|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|[`MaximumTilesCount`](#maximumtilescount)|`Int32`|This property gets or sets the maximum tiles count, if it exceed,it will delete the old first cached tiles.|
|[`NoDataTileImage`](#nodatatileimage)|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|[`TileAccessMode`](#tileaccessmode)|[`TileAccessMode`](../ThinkGeo.Core/ThinkGeo.Core.TileAccessMode.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`ClearCache()`](#clearcache)|
|[`DeleteTile(Tile)`](#deletetiletile)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetTile(Int32,Int64,Int64)`](#gettileint32int64int64)|
|[`GetType()`](#gettype)|
|[`SaveTile(Tile)`](#savetiletile)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`ClearCacheCore()`](#clearcachecore)|
|[`DeleteTileCore(Tile)`](#deletetilecoretile)|
|[`Finalize()`](#finalize)|
|[`GetTileCore(Int32,Int64,Int64)`](#gettilecoreint32int64int64)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnGettingCacheTile(GettingCacheImageBitmapTileCacheEventArgs)`](#ongettingcachetilegettingcacheimagebitmaptilecacheeventargs)|
|[`OnGottenCacheTile(GottenCacheImageBitmapTileCacheEventArgs)`](#ongottencachetilegottencacheimagebitmaptilecacheeventargs)|
|[`SaveTileCore(Tile)`](#savetilecoretile)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`GottenCacheTile`](#gottencachetile)|[`GottenCacheImageBitmapTileCacheEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GottenCacheImageBitmapTileCacheEventArgs.md)|N/A|
|[`GettingCacheTile`](#gettingcachetile)|[`GettingCacheImageBitmapTileCacheEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingCacheImageBitmapTileCacheEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`InMemoryRasterTileCache()`](#inmemoryrastertilecache)|

### Protected Constructors


### Public Properties

#### `CacheId`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `ExpirationTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `ImageFormat`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)

---
#### `JpegQuality`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int16`

---
#### `LoadingTileImage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)

---
#### `MaximumTilesCount`

**Summary**

   *This property gets or sets the maximum tiles count, if it exceed,it will delete the old first cached tiles.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `NoDataTileImage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)

---
#### `TileAccessMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TileAccessMode`](../ThinkGeo.Core/ThinkGeo.Core.TileAccessMode.md)

---

### Protected Properties


### Public Methods

#### `ClearCache()`

**Summary**

   *N/A*

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
#### `DeleteTile(Tile)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tile|[`Tile`](../ThinkGeo.Core/ThinkGeo.Core.Tile.md)|N/A|

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
#### `GetTile(Int32,Int64,Int64)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Tile`](../ThinkGeo.Core/ThinkGeo.Core.Tile.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|zoom|`Int32`|N/A|
|column|`Int64`|N/A|
|row|`Int64`|N/A|

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
#### `SaveTile(Tile)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tile|[`Tile`](../ThinkGeo.Core/ThinkGeo.Core.Tile.md)|N/A|

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

#### `ClearCacheCore()`

**Summary**

   *This method will clear all the tiles in the tileCache.*

**Remarks**

   *This method will not take effect when the Read is set to true.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DeleteTileCore(Tile)`

**Summary**

   *This method will delete the target tileView passed in.*

**Remarks**

   *This method will not take effect when the Read is set to true.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tile|[`Tile`](../ThinkGeo.Core/ThinkGeo.Core.Tile.md)|The target tileView to be deleted.|

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
#### `GetTileCore(Int32,Int64,Int64)`

**Summary**

   *This method returns the BitmapTile corresponding to passed in row and column.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Tile`](../ThinkGeo.Core/ThinkGeo.Core.Tile.md)|Returns the BitmapTile corresponding to the passed in row and column.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|zoom|`Int32`|N/A|
|column|`Int64`|The target column for the tileView to fetch.|
|row|`Int64`|The target row for the tileView to fetch.|

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
#### `OnGettingCacheTile(GettingCacheImageBitmapTileCacheEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`GettingCacheImageBitmapTileCacheEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingCacheImageBitmapTileCacheEventArgs.md)|N/A|

---
#### `OnGottenCacheTile(GottenCacheImageBitmapTileCacheEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`GottenCacheImageBitmapTileCacheEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GottenCacheImageBitmapTileCacheEventArgs.md)|N/A|

---
#### `SaveTileCore(Tile)`

**Summary**

   *This method will save the target tileView passed in.*

**Remarks**

   *This method will not take effect when the Read is set to true.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|tile|[`Tile`](../ThinkGeo.Core/ThinkGeo.Core.Tile.md)|The target tileView to be saved.|

---

### Public Events

#### GottenCacheTile

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`GottenCacheImageBitmapTileCacheEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GottenCacheImageBitmapTileCacheEventArgs.md)

#### GettingCacheTile

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`GettingCacheImageBitmapTileCacheEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingCacheImageBitmapTileCacheEventArgs.md)


