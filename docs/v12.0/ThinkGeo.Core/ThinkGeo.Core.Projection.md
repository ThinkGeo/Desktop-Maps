# Projection


## Inheritance Hierarchy

+ `Object`
  + **`Projection`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`Projection()`](#projection)|
|[`Projection(Int32)`](#projectionint32)|
|[`Projection(String)`](#projectionstring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ProjString`](#projstring)|`String`|N/A|
|[`Srid`](#srid)|`Int32`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`ConvertEpsgToProjString(Int32)`](#convertepsgtoprojstringint32)|
|[`ConvertEpsgToWkt(Int32)`](#convertepsgtowktint32)|
|[`ConvertProjStringToWkt(String)`](#convertprojstringtowktstring)|
|[`ConvertWktToProjString(String)`](#convertwkttoprojstringstring)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBingMapProjString()`](#getbingmapprojstring)|
|[`GetDecimalDegreesProjString()`](#getdecimaldegreesprojstring)|
|[`GetGeographyUnit(String)`](#getgeographyunitstring)|
|[`GetGeographyUnitFromProj(String)`](#getgeographyunitfromprojstring)|
|[`GetGeographyUnitFromWkb(String)`](#getgeographyunitfromwkbstring)|
|[`GetGoogleMapProjString()`](#getgooglemapprojstring)|
|[`GetHashCode()`](#gethashcode)|
|[`GetLatLongProjString()`](#getlatlongprojstring)|
|[`GetLocalUtmZoneNumber(Double,Double)`](#getlocalutmzonenumberdoubledouble)|
|[`GetLocalUtmZoneNumber(Feature,String)`](#getlocalutmzonenumberfeaturestring)|
|[`GetLocalUtmZoneNumber(Feature,Int32)`](#getlocalutmzonenumberfeatureint32)|
|[`GetLocalUtmZoneNumber(BaseShape,String)`](#getlocalutmzonenumberbaseshapestring)|
|[`GetLocalUtmZoneNumber(BaseShape,Int32)`](#getlocalutmzonenumberbaseshapeint32)|
|[`GetLocalUtmZoneProjString(Double,Double)`](#getlocalutmzoneprojstringdoubledouble)|
|[`GetLocalUtmZoneProjString(Feature,String)`](#getlocalutmzoneprojstringfeaturestring)|
|[`GetLocalUtmZoneProjString(Feature,Int32)`](#getlocalutmzoneprojstringfeatureint32)|
|[`GetLocalUtmZoneProjString(BaseShape,Projection)`](#getlocalutmzoneprojstringbaseshapeprojection)|
|[`GetLocalUtmZoneProjString(BaseShape,String)`](#getlocalutmzoneprojstringbaseshapestring)|
|[`GetLocalUtmZoneProjString(BaseShape,Int32)`](#getlocalutmzoneprojstringbaseshapeint32)|
|[`GetProjStringByEpsgSrid(Int32)`](#getprojstringbyepsgsridint32)|
|[`GetProjStringByEsriSrid(Int32)`](#getprojstringbyesrisridint32)|
|[`GetSphericalMercatorProjString()`](#getsphericalmercatorprojstring)|
|[`GetType()`](#gettype)|
|[`GetUnit()`](#getunit)|
|[`GetWgs84ProjString()`](#getwgs84projstring)|
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
|[`Projection()`](#projection)|
|[`Projection(Int32)`](#projectionint32)|
|[`Projection(String)`](#projectionstring)|

### Protected Constructors


### Public Properties

#### `ProjString`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Srid`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---

### Protected Properties


### Public Methods

#### `ConvertEpsgToProjString(Int32)`

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
|srid|`Int32`|N/A|

---
#### `ConvertEpsgToWkt(Int32)`

**Summary**

   *This method is a static API to get a Prj string by Epsg number*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|srid|`Int32`|Epsg number that reprents this projection|

---
#### `ConvertProjStringToWkt(String)`

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
|projString|`String`|N/A|

---
#### `ConvertWktToProjString(String)`

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
|wkt|`String`|N/A|

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
#### `GetBingMapProjString()`

**Summary**

   *This method is a static API to get a projection used by BingMaps.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|A text for a projection used by BingMaps , it should like this "+proj=merc +a=6378137 +b=6378137 +lat_ts=0.0 +lon_0=0.0 +x_0=0.0 +y_0=0 +k=1.0 +units=m +nadgrids=@null +wktext  +no_defs"|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetDecimalDegreesProjString()`

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
#### `GetGeographyUnit(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|projString|`String`|N/A|

---
#### `GetGeographyUnitFromProj(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|projString|`String`|N/A|

---
#### `GetGeographyUnitFromWkb(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wkt|`String`|N/A|

---
#### `GetGoogleMapProjString()`

**Summary**

   *This method is a static API to get a projection used by GoogleMap.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|A text for a projection used by GoogleMap , it should like this "+proj=merc +a=6378137 +b=6378137 +lat_ts=0.0 +lon_0=0.0 +x_0=0.0 +y_0=0 +k=1.0 +units=m +nadgrids=@null +wktext  +no_defs"|

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
#### `GetLatLongProjString()`

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
#### `GetLocalUtmZoneNumber(Double,Double)`

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
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|

---
#### `GetLocalUtmZoneNumber(Feature,String)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|projString|`String`|N/A|

---
#### `GetLocalUtmZoneNumber(Feature,Int32)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|srid|`Int32`|N/A|

---
#### `GetLocalUtmZoneNumber(BaseShape,String)`

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
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|projString|`String`|N/A|

---
#### `GetLocalUtmZoneNumber(BaseShape,Int32)`

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
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|

---
#### `GetLocalUtmZoneProjString(Double,Double)`

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
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|

---
#### `GetLocalUtmZoneProjString(Feature,String)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|projString|`String`|N/A|

---
#### `GetLocalUtmZoneProjString(Feature,Int32)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|srid|`Int32`|N/A|

---
#### `GetLocalUtmZoneProjString(BaseShape,Projection)`

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
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|projection|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|

---
#### `GetLocalUtmZoneProjString(BaseShape,String)`

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
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|projString|`String`|N/A|

---
#### `GetLocalUtmZoneProjString(BaseShape,Int32)`

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
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|

---
#### `GetProjStringByEpsgSrid(Int32)`

**Summary**

   *This method is a static API to get a projection text from EPSG(European Petroleum Survey Group).*

**Remarks**

   *More information about it can reference to EPSG.rtf in the documentation.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The project text corresponding to the srid.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|srid|`Int32`|The target Srid infromation to get the projection text from.|

---
#### `GetProjStringByEsriSrid(Int32)`

**Summary**

   *This method is a static API to get a projection text from ERSI.*

**Remarks**

   *More information about it can reference to ERSI.rtf in the documentation.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The project text corresponding to the srid.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|srid|`Int32`|The target Srid infromation to get the projection text from.|

---
#### `GetSphericalMercatorProjString()`

**Summary**

   *This method is a static API to get a projection of SphericalMercator.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|A text for a SphericalMercator projection, it should like this "+proj=merc +a=6378137 +b=6378137 +lat_ts=0.0 +lon_0=0.0 +x_0=0.0 +y_0=0 +k=1.0 +units=m +nadgrids=@null +wktext  +no_defs"|

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
#### `GetUnit()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWgs84ProjString()`

**Summary**

   *This method is a static API to get a projection text from WGS84.*

**Remarks**

   *More information about it can reference to EPSG.rtf in the documentation.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The project text corresponding to the srid.|

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


