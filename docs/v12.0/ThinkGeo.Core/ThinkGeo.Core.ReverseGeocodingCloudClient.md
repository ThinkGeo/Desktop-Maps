# ReverseGeocodingCloudClient


## Inheritance Hierarchy

+ `Object`
  + [`CloudClient`](../ThinkGeo.Core/ThinkGeo.Core.CloudClient.md)
    + **`ReverseGeocodingCloudClient`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ReverseGeocodingCloudClient()`](#reversegeocodingcloudclient)|
|[`ReverseGeocodingCloudClient(String,String)`](#reversegeocodingcloudclientstringstring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`BaseUris`](#baseuris)|Collection<`Uri`>|N/A|
|[`ClientId`](#clientid)|`String`|N/A|
|[`ClientSecret`](#clientsecret)|`String`|N/A|
|[`TimeoutInSeconds`](#timeoutinseconds)|`Int32`|N/A|
|[`WebProxy`](#webproxy)|`IWebProxy`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Dispose()`](#dispose)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`SearchArea(PolygonShape,Int32)`](#searchareapolygonshapeint32)|
|[`SearchArea(PolygonShape,Int32,CloudReverseGeocodingOptions)`](#searchareapolygonshapeint32cloudreversegeocodingoptions)|
|[`SearchArea(PolygonShape,String)`](#searchareapolygonshapestring)|
|[`SearchArea(PolygonShape,String,CloudReverseGeocodingOptions)`](#searchareapolygonshapestringcloudreversegeocodingoptions)|
|[`SearchAreaAsync(PolygonShape,Int32)`](#searchareaasyncpolygonshapeint32)|
|[`SearchAreaAsync(PolygonShape,Int32,CloudReverseGeocodingOptions)`](#searchareaasyncpolygonshapeint32cloudreversegeocodingoptions)|
|[`SearchAreaAsync(PolygonShape,String)`](#searchareaasyncpolygonshapestring)|
|[`SearchAreaAsync(PolygonShape,String,CloudReverseGeocodingOptions)`](#searchareaasyncpolygonshapestringcloudreversegeocodingoptions)|
|[`SearchAreaInDecimalDegree(PolygonShape)`](#searchareaindecimaldegreepolygonshape)|
|[`SearchAreaInDecimalDegree(PolygonShape,CloudReverseGeocodingOptions)`](#searchareaindecimaldegreepolygonshapecloudreversegeocodingoptions)|
|[`SearchAreaInDecimalDegreeAsync(PolygonShape)`](#searchareaindecimaldegreeasyncpolygonshape)|
|[`SearchAreaInDecimalDegreeAsync(PolygonShape,CloudReverseGeocodingOptions)`](#searchareaindecimaldegreeasyncpolygonshapecloudreversegeocodingoptions)|
|[`SearchLine(LineShape,Int32,Double,DistanceUnit)`](#searchlinelineshapeint32doubledistanceunit)|
|[`SearchLine(LineShape,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchlinelineshapeint32doubledistanceunitcloudreversegeocodingoptions)|
|[`SearchLine(LineShape,String,Double,DistanceUnit)`](#searchlinelineshapestringdoubledistanceunit)|
|[`SearchLine(LineShape,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchlinelineshapestringdoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchLineAsync(LineShape,Int32,Double,DistanceUnit)`](#searchlineasynclineshapeint32doubledistanceunit)|
|[`SearchLineAsync(LineShape,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchlineasynclineshapeint32doubledistanceunitcloudreversegeocodingoptions)|
|[`SearchLineAsync(LineShape,String,Double,DistanceUnit)`](#searchlineasynclineshapestringdoubledistanceunit)|
|[`SearchLineAsync(LineShape,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchlineasynclineshapestringdoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchLineInDecimalDegree(LineShape,Double,DistanceUnit)`](#searchlineindecimaldegreelineshapedoubledistanceunit)|
|[`SearchLineInDecimalDegree(LineShape,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchlineindecimaldegreelineshapedoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchLineInDecimalDegreeAsync(LineShape,Double,DistanceUnit)`](#searchlineindecimaldegreeasynclineshapedoubledistanceunit)|
|[`SearchLineInDecimalDegreeAsync(LineShape,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchlineindecimaldegreeasynclineshapedoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPoint(Double,Double,Int32,Double,DistanceUnit)`](#searchpointdoubledoubleint32doubledistanceunit)|
|[`SearchPoint(Double,Double,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointdoubledoubleint32doubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPoint(Double,Double,String,Double,DistanceUnit)`](#searchpointdoubledoublestringdoubledistanceunit)|
|[`SearchPoint(Double,Double,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointdoubledoublestringdoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointAsync(Double,Double,Int32,Double,DistanceUnit)`](#searchpointasyncdoubledoubleint32doubledistanceunit)|
|[`SearchPointAsync(Double,Double,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointasyncdoubledoubleint32doubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointAsync(Double,Double,String,Double,DistanceUnit)`](#searchpointasyncdoubledoublestringdoubledistanceunit)|
|[`SearchPointAsync(Double,Double,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointasyncdoubledoublestringdoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointInDecimalDegree(Double,Double,Double,DistanceUnit)`](#searchpointindecimaldegreedoubledoubledoubledistanceunit)|
|[`SearchPointInDecimalDegree(Double,Double,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointindecimaldegreedoubledoubledoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointInDecimalDegreeAsync(Double,Double,Double,DistanceUnit)`](#searchpointindecimaldegreeasyncdoubledoubledoubledistanceunit)|
|[`SearchPointInDecimalDegreeAsync(Double,Double,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointindecimaldegreeasyncdoubledoubledoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPoints(IEnumerable<PointShape>,Int32,Double,DistanceUnit)`](#searchpointsienumerable<pointshape>int32doubledistanceunit)|
|[`SearchPoints(IEnumerable<PointShape>,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointsienumerable<pointshape>int32doubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPoints(IEnumerable<PointShape>,String,Double,DistanceUnit)`](#searchpointsienumerable<pointshape>stringdoubledistanceunit)|
|[`SearchPoints(IEnumerable<PointShape>,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointsienumerable<pointshape>stringdoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointsAsync(IEnumerable<PointShape>,Int32,Double,DistanceUnit)`](#searchpointsasyncienumerable<pointshape>int32doubledistanceunit)|
|[`SearchPointsAsync(IEnumerable<PointShape>,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointsasyncienumerable<pointshape>int32doubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointsAsync(IEnumerable<PointShape>,String,Double,DistanceUnit)`](#searchpointsasyncienumerable<pointshape>stringdoubledistanceunit)|
|[`SearchPointsAsync(IEnumerable<PointShape>,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointsasyncienumerable<pointshape>stringdoubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointsInDecimalDegree(IEnumerable<PointShape>,Double,DistanceUnit)`](#searchpointsindecimaldegreeienumerable<pointshape>doubledistanceunit)|
|[`SearchPointsInDecimalDegree(IEnumerable<PointShape>,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointsindecimaldegreeienumerable<pointshape>doubledistanceunitcloudreversegeocodingoptions)|
|[`SearchPointsInDecimalDegreeAsync(IEnumerable<PointShape>,Double,DistanceUnit)`](#searchpointsindecimaldegreeasyncienumerable<pointshape>doubledistanceunit)|
|[`SearchPointsInDecimalDegreeAsync(IEnumerable<PointShape>,Double,DistanceUnit,CloudReverseGeocodingOptions)`](#searchpointsindecimaldegreeasyncienumerable<pointshape>doubledistanceunitcloudreversegeocodingoptions)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`AuthenticateWebRequest(WebRequest)`](#authenticatewebrequestwebrequest)|
|[`Finalize()`](#finalize)|
|[`GetNextCandidateBaseUri()`](#getnextcandidatebaseuri)|
|[`GetNextCandidateBaseUriCore()`](#getnextcandidatebaseuricore)|
|[`GetToken()`](#gettoken)|
|[`GetTokenCore()`](#gettokencore)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnGettingAccessToken(GettingAccessTokenEventArgs)`](#ongettingaccesstokengettingaccesstokeneventargs)|
|[`OnSendingWebRequest(SendingWebRequestEventArgs)`](#onsendingwebrequestsendingwebrequesteventargs)|
|[`OnSentWebRequest(SentWebRequestEventArgs)`](#onsentwebrequestsentwebrequesteventargs)|
|[`SendWebRequest(WebRequest)`](#sendwebrequestwebrequest)|
|[`SendWebRequestAsync(WebRequest)`](#sendwebrequestasyncwebrequest)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`GettingAccessToken`](#gettingaccesstoken)|[`GettingAccessTokenEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingAccessTokenEventArgs.md)|N/A|
|[`SendingWebRequest`](#sendingwebrequest)|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|
|[`SentWebRequest`](#sentwebrequest)|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`ReverseGeocodingCloudClient()`](#reversegeocodingcloudclient)|
|[`ReverseGeocodingCloudClient(String,String)`](#reversegeocodingcloudclientstringstring)|

### Protected Constructors


### Public Properties

#### `BaseUris`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`Uri`>

---
#### `ClientId`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `ClientSecret`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `TimeoutInSeconds`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `WebProxy`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`IWebProxy`

---

### Protected Properties


### Public Methods

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
#### `SearchArea(PolygonShape,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInSrid|`Int32`|N/A|

---
#### `SearchArea(PolygonShape,Int32,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInSrid|`Int32`|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchArea(PolygonShape,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInProj4String|`String`|N/A|

---
#### `SearchArea(PolygonShape,String,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInProj4String|`String`|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchAreaAsync(PolygonShape,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInSrid|`Int32`|N/A|

---
#### `SearchAreaAsync(PolygonShape,Int32,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInSrid|`Int32`|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchAreaAsync(PolygonShape,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInProj4String|`String`|N/A|

---
#### `SearchAreaAsync(PolygonShape,String,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|areaProjectionInProj4String|`String`|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchAreaInDecimalDegree(PolygonShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|

---
#### `SearchAreaInDecimalDegree(PolygonShape,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchAreaInDecimalDegreeAsync(PolygonShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|

---
#### `SearchAreaInDecimalDegreeAsync(PolygonShape,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchLine(LineShape,Int32,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchLine(LineShape,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchLine(LineShape,String,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchLine(LineShape,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchLineAsync(LineShape,Int32,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchLineAsync(LineShape,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchLineAsync(LineShape,String,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchLineAsync(LineShape,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchLineInDecimalDegree(LineShape,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchLineInDecimalDegree(LineShape,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchLineInDecimalDegreeAsync(LineShape,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchLineInDecimalDegreeAsync(LineShape,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|searchBuffer|`Double`|N/A|
|unitOfsearchBuffer|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPoint(Double,Double,Int32,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPoint(Double,Double,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPoint(Double,Double,String,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPoint(Double,Double,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointAsync(Double,Double,Int32,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointAsync(Double,Double,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointAsync(Double,Double,String,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointAsync(Double,Double,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointInDecimalDegree(Double,Double,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointInDecimalDegree(Double,Double,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointInDecimalDegreeAsync(Double,Double,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointInDecimalDegreeAsync(Double,Double,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPoints(IEnumerable<PointShape>,Int32,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPoints(IEnumerable<PointShape>,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPoints(IEnumerable<PointShape>,String,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPoints(IEnumerable<PointShape>,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointsAsync(IEnumerable<PointShape>,Int32,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointsAsync(IEnumerable<PointShape>,Int32,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInSrid|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointsAsync(IEnumerable<PointShape>,String,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointsAsync(IEnumerable<PointShape>,String,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInProj4String|`String`|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointsInDecimalDegree(IEnumerable<PointShape>,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointsInDecimalDegree(IEnumerable<PointShape>,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

---
#### `SearchPointsInDecimalDegreeAsync(IEnumerable<PointShape>,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `SearchPointsInDecimalDegreeAsync(IEnumerable<PointShape>,Double,DistanceUnit,CloudReverseGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudReverseGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|searchRadius|`Double`|N/A|
|unitOfsearchRadius|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudReverseGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudReverseGeocodingOptions.md)|N/A|

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

#### `AuthenticateWebRequest(WebRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|webRequest|`WebRequest`|N/A|

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
#### `GetNextCandidateBaseUri()`

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
#### `GetNextCandidateBaseUriCore()`

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
#### `GetToken()`

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
#### `GetTokenCore()`

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
#### `OnGettingAccessToken(GettingAccessTokenEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`GettingAccessTokenEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingAccessTokenEventArgs.md)|N/A|

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
#### `SendWebRequest(WebRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`WebResponse`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|webRequest|`WebRequest`|N/A|

---
#### `SendWebRequestAsync(WebRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<`WebResponse`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|webRequest|`WebRequest`|N/A|

---

### Public Events

#### GettingAccessToken

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`GettingAccessTokenEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.GettingAccessTokenEventArgs.md)

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


