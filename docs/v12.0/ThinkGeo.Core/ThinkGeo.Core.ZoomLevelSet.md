# ZoomLevelSet


## Inheritance Hierarchy

+ `Object`
  + **`ZoomLevelSet`**
    + [`SphericalMercatorZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.SphericalMercatorZoomLevelSet.md)
    + [`BingMapsZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.BingMapsZoomLevelSet.md)
    + [`GoogleMapsZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.GoogleMapsZoomLevelSet.md)
    + [`OpenStreetMapsZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.OpenStreetMapsZoomLevelSet.md)
    + [`ThinkGeoCloudMapsZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ThinkGeoCloudMapsZoomLevelSet.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ZoomLevelSet()`](#zoomlevelset)|
|[`ZoomLevelSet(Int32)`](#zoomlevelsetint32)|
|[`ZoomLevelSet(Int32,RectangleShape)`](#zoomlevelsetint32rectangleshape)|
|[`ZoomLevelSet(Int32,RectangleShape,GeographyUnit)`](#zoomlevelsetint32rectangleshapegeographyunit)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CustomZoomLevels`](#customzoomlevels)|Collection<[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)>|This property gets the custom zoom levels from the zoomLevelSet.|
|[`Name`](#name)|`String`|This property gets and sets the name for the ZoomSet.|
|[`ZoomLevel01`](#zoomlevel01)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level01.|
|[`ZoomLevel02`](#zoomlevel02)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level02.|
|[`ZoomLevel03`](#zoomlevel03)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level03.|
|[`ZoomLevel04`](#zoomlevel04)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level04.|
|[`ZoomLevel05`](#zoomlevel05)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level05.|
|[`ZoomLevel06`](#zoomlevel06)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level06.|
|[`ZoomLevel07`](#zoomlevel07)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level07.|
|[`ZoomLevel08`](#zoomlevel08)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level08.|
|[`ZoomLevel09`](#zoomlevel09)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level09.|
|[`ZoomLevel10`](#zoomlevel10)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level10.|
|[`ZoomLevel11`](#zoomlevel11)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level11.|
|[`ZoomLevel12`](#zoomlevel12)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level12.|
|[`ZoomLevel13`](#zoomlevel13)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level13.|
|[`ZoomLevel14`](#zoomlevel14)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level14.|
|[`ZoomLevel15`](#zoomlevel15)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level15.|
|[`ZoomLevel16`](#zoomlevel16)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level16.|
|[`ZoomLevel17`](#zoomlevel17)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level17.|
|[`ZoomLevel18`](#zoomlevel18)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level18.|
|[`ZoomLevel19`](#zoomlevel19)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level19.|
|[`ZoomLevel20`](#zoomlevel20)|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This property gets the ZoomLevel for Level20.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetHigherZoomLevelScale(Double,ZoomLevelSet)`](#gethigherzoomlevelscaledoublezoomlevelset)|
|[`GetLowerZoomLevelScale(Double,ZoomLevelSet)`](#getlowerzoomlevelscaledoublezoomlevelset)|
|[`GetMaxExtent()`](#getmaxextent)|
|[`GetType()`](#gettype)|
|[`GetZoomLevel(RectangleShape,Double,GeographyUnit)`](#getzoomlevelrectangleshapedoublegeographyunit)|
|[`GetZoomLevel(RectangleShape,Double,GeographyUnit,Single)`](#getzoomlevelrectangleshapedoublegeographyunitsingle)|
|[`GetZoomLevelForDrawing(RectangleShape,Double,GeographyUnit,Single)`](#getzoomlevelfordrawingrectangleshapedoublegeographyunitsingle)|
|[`GetZoomLevelForDrawing(RectangleShape,Double,GeographyUnit)`](#getzoomlevelfordrawingrectangleshapedoublegeographyunit)|
|[`GetZoomLevels()`](#getzoomlevels)|
|[`Load(String)`](#loadstring)|
|[`Load(Uri)`](#loaduri)|
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
|[`ZoomLevelSet()`](#zoomlevelset)|
|[`ZoomLevelSet(Int32)`](#zoomlevelsetint32)|
|[`ZoomLevelSet(Int32,RectangleShape)`](#zoomlevelsetint32rectangleshape)|
|[`ZoomLevelSet(Int32,RectangleShape,GeographyUnit)`](#zoomlevelsetint32rectangleshapegeographyunit)|

### Protected Constructors


### Public Properties

#### `CustomZoomLevels`

**Summary**

   *This property gets the custom zoom levels from the zoomLevelSet.*

**Remarks**

   *None*

**Return Value**

Collection<[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)>

---
#### `Name`

**Summary**

   *This property gets and sets the name for the ZoomSet.*

**Remarks**

   *The name is user defined. It is useful to set, as it may be used for higher level components such as legends, etc.*

**Return Value**

`String`

---
#### `ZoomLevel01`

**Summary**

   *This property gets the ZoomLevel for Level01.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel02`

**Summary**

   *This property gets the ZoomLevel for Level02.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel03`

**Summary**

   *This property gets the ZoomLevel for Level03.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel04`

**Summary**

   *This property gets the ZoomLevel for Level04.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel05`

**Summary**

   *This property gets the ZoomLevel for Level05.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel06`

**Summary**

   *This property gets the ZoomLevel for Level06.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel07`

**Summary**

   *This property gets the ZoomLevel for Level07.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel08`

**Summary**

   *This property gets the ZoomLevel for Level08.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel09`

**Summary**

   *This property gets the ZoomLevel for Level09.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel10`

**Summary**

   *This property gets the ZoomLevel for Level10.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel11`

**Summary**

   *This property gets the ZoomLevel for Level11.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel12`

**Summary**

   *This property gets the ZoomLevel for Level12.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel13`

**Summary**

   *This property gets the ZoomLevel for Level13.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel14`

**Summary**

   *This property gets the ZoomLevel for Level14.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel15`

**Summary**

   *This property gets the ZoomLevel for Level15.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel16`

**Summary**

   *This property gets the ZoomLevel for Level16.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel17`

**Summary**

   *This property gets the ZoomLevel for Level17.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel18`

**Summary**

   *This property gets the ZoomLevel for Level18.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel19`

**Summary**

   *This property gets the ZoomLevel for Level19.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

---
#### `ZoomLevel20`

**Summary**

   *This property gets the ZoomLevel for Level20.*

**Remarks**

   *None*

**Return Value**

[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)

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
#### `GetHigherZoomLevelScale(Double,ZoomLevelSet)`

**Summary**

   *ZoomToScale out, the result is greater than input*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|currentScale|`Double`|N/A|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

---
#### `GetLowerZoomLevelScale(Double,ZoomLevelSet)`

**Summary**

   *ZoomToScale in, the result is less than input*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|currentScale|`Double`|N/A|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

---
#### `GetMaxExtent()`

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
#### `GetZoomLevel(RectangleShape,Double,GeographyUnit)`

**Summary**

   *This method returns the active ZoomLevel based on an extent, a map unit and a screen width.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This method returns the active ZoomLevel based on an extent, a map unit and a screen width.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is a world extent.|
|screenWidth|`Double`|This parameter is the width of the map in pixels.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of the map.|

---
#### `GetZoomLevel(RectangleShape,Double,GeographyUnit,Single)`

**Summary**

   *This method returns the active ZoomLevel based on an extent, a map unit and a screen width.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This method returns the active ZoomLevel based on an extent, a map unit and a screen width.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is a world extent.|
|screenWidth|`Double`|This parameter is the width of the map in pixels.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of the map.|
|dpi|`Single`|This parameter is the dpi of the extent.|

---
#### `GetZoomLevelForDrawing(RectangleShape,Double,GeographyUnit,Single)`

**Summary**

   *This method returns the active ZoomLevel based on an extent, a map unit and a view width.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This method returns the active ZoomLevel based on an extent, map unit and a view width.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is a world extent.|
|screenWidth|`Double`|This parameter is the width of the view in pixels.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of the map.|
|dpi|`Single`|This parameter is the dpi of the extent.|

---
#### `GetZoomLevelForDrawing(RectangleShape,Double,GeographyUnit)`

**Summary**

   *This method returns the active ZoomLevel based on an extent, a map unit and a view width.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)|This method returns the active ZoomLevel based on an extent, map unit and a view width.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is a world extent.|
|screenWidth|`Double`|This parameter is the width of the view in pixels.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the unit of the map.|

---
#### `GetZoomLevels()`

**Summary**

   *This method return all of the zoomLevels in the zoomLevelSet.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`ZoomLevel`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevel.md)>|This method return all of the zoomlevels in the zoomLevelSet.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Load(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|styleJsonPath|`String`|N/A|

---
#### `Load(Uri)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|styleJsonUri|`Uri`|N/A|

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


