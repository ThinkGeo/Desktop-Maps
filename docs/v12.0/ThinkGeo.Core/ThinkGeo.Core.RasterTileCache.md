# RasterTileCache


## Inheritance Hierarchy

+ `Object`
  + [`TileCache`](../ThinkGeo.Core/ThinkGeo.Core.TileCache.md)
    + **`RasterTileCache`**
      + [`FileRasterTileCache`](../ThinkGeo.Core/ThinkGeo.Core.FileRasterTileCache.md)
      + [`InMemoryRasterTileCache`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryRasterTileCache.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`RasterTileCache(String,RasterTileFormat)`](#rastertilecachestringrastertileformat)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CacheId`](#cacheid)|`String`|N/A|
|[`ExpirationTime`](#expirationtime)|`TimeSpan`|N/A|
|[`ImageFormat`](#imageformat)|[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)|Gets or sets the tileView image format.|
|[`JpegQuality`](#jpegquality)|`Int16`|Gets or sets the Jpeg quality , this property only take effects when setting the ImageFormat to Jpeg.|
|[`LoadingTileImage`](#loadingtileimage)|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This property returns back a preset image showing the Tile is loading.|
|[`NoDataTileImage`](#nodatatileimage)|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This property returns back a preset image showing the tileView data is missing.|
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
|N/A|

### Protected Constructors

#### `RasterTileCache(String,RasterTileFormat)`

**Summary**

   *This is the constructor of the class.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|N/A||

**Parameters**

|Name|Type|Description|
|---|---|---|
|cacheId|`String`|This is the cache identifier which marks its difference with other TileCache.|
|imageFormat|[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)|This is the imageFormate showing what kind of image we are trying to save.|

---

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

   *Gets or sets the tileView image format.*

**Remarks**

   *N/A*

**Return Value**

[`RasterTileFormat`](../ThinkGeo.Core/ThinkGeo.Core.RasterTileFormat.md)

---
#### `JpegQuality`

**Summary**

   *Gets or sets the Jpeg quality , this property only take effects when setting the ImageFormat to Jpeg.*

**Remarks**

   *N/A*

**Return Value**

`Int16`

---
#### `LoadingTileImage`

**Summary**

   *This property returns back a preset image showing the Tile is loading.*

**Remarks**

   *N/A*

**Return Value**

[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)

---
#### `NoDataTileImage`

**Summary**

   *This property returns back a preset image showing the tileView data is missing.*

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

   *N/A*

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
#### `DeleteTileCore(Tile)`

**Summary**

   *N/A*

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

   *N/A*

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


