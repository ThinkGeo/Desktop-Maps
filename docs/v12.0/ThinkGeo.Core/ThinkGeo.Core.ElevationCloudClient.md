# ElevationCloudClient


## Inheritance Hierarchy

+ `Object`
  + [`CloudClient`](../ThinkGeo.Core/ThinkGeo.Core.CloudClient.md)
    + **`ElevationCloudClient`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ElevationCloudClient()`](#elevationcloudclient)|
|[`ElevationCloudClient(String,String)`](#elevationcloudclientstringstring)|

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
|[`GetElevationOfArea(AreaBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`](#getelevationofareaareabaseshapeint32doubledistanceunitdistanceunit)|
|[`GetElevationOfArea(AreaBaseShape,String,Double,DistanceUnit,DistanceUnit)`](#getelevationofareaareabaseshapestringdoubledistanceunitdistanceunit)|
|[`GetElevationOfAreaAsync(AreaBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`](#getelevationofareaasyncareabaseshapeint32doubledistanceunitdistanceunit)|
|[`GetElevationOfAreaAsync(AreaBaseShape,String,Double,DistanceUnit,DistanceUnit)`](#getelevationofareaasyncareabaseshapestringdoubledistanceunitdistanceunit)|
|[`GetElevationOfAreaInDecimalDegree(AreaBaseShape,Double,DistanceUnit,DistanceUnit)`](#getelevationofareaindecimaldegreeareabaseshapedoubledistanceunitdistanceunit)|
|[`GetElevationOfAreaInDecimalDegreeAsync(AreaBaseShape,Double,DistanceUnit,DistanceUnit)`](#getelevationofareaindecimaldegreeasyncareabaseshapedoubledistanceunitdistanceunit)|
|[`GetElevationOfLine(LineBaseShape,Int32,Int32,DistanceUnit)`](#getelevationoflinelinebaseshapeint32int32distanceunit)|
|[`GetElevationOfLine(LineBaseShape,String,Int32,DistanceUnit)`](#getelevationoflinelinebaseshapestringint32distanceunit)|
|[`GetElevationOfLine(LineBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`](#getelevationoflinelinebaseshapeint32doubledistanceunitdistanceunit)|
|[`GetElevationOfLine(LineBaseShape,String,Double,DistanceUnit,DistanceUnit)`](#getelevationoflinelinebaseshapestringdoubledistanceunitdistanceunit)|
|[`GetElevationOfLineAsync(LineBaseShape,Int32,Int32,DistanceUnit)`](#getelevationoflineasynclinebaseshapeint32int32distanceunit)|
|[`GetElevationOfLineAsync(LineBaseShape,String,Int32,DistanceUnit)`](#getelevationoflineasynclinebaseshapestringint32distanceunit)|
|[`GetElevationOfLineAsync(LineBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`](#getelevationoflineasynclinebaseshapeint32doubledistanceunitdistanceunit)|
|[`GetElevationOfLineAsync(LineBaseShape,String,Double,DistanceUnit,DistanceUnit)`](#getelevationoflineasynclinebaseshapestringdoubledistanceunitdistanceunit)|
|[`GetElevationOfLineInDecimalDegree(LineBaseShape,Int32,DistanceUnit)`](#getelevationoflineindecimaldegreelinebaseshapeint32distanceunit)|
|[`GetElevationOfLineInDecimalDegree(LineBaseShape,Double,DistanceUnit,DistanceUnit)`](#getelevationoflineindecimaldegreelinebaseshapedoubledistanceunitdistanceunit)|
|[`GetElevationOfLineInDecimalDegreeAsync(LineBaseShape,Int32,DistanceUnit)`](#getelevationoflineindecimaldegreeasynclinebaseshapeint32distanceunit)|
|[`GetElevationOfLineInDecimalDegreeAsync(LineBaseShape,Double,DistanceUnit,DistanceUnit)`](#getelevationoflineindecimaldegreeasynclinebaseshapedoubledistanceunitdistanceunit)|
|[`GetElevationOfPoint(Double,Double,Int32,DistanceUnit)`](#getelevationofpointdoubledoubleint32distanceunit)|
|[`GetElevationOfPoint(Double,Double,String,DistanceUnit)`](#getelevationofpointdoubledoublestringdistanceunit)|
|[`GetElevationOfPointAsync(Double,Double,Int32,DistanceUnit)`](#getelevationofpointasyncdoubledoubleint32distanceunit)|
|[`GetElevationOfPointAsync(Double,Double,String,DistanceUnit)`](#getelevationofpointasyncdoubledoublestringdistanceunit)|
|[`GetElevationOfPointInDecimalDegree(Double,Double,DistanceUnit)`](#getelevationofpointindecimaldegreedoubledoubledistanceunit)|
|[`GetElevationOfPointInDecimalDegreeAsync(Double,Double,DistanceUnit)`](#getelevationofpointindecimaldegreeasyncdoubledoubledistanceunit)|
|[`GetElevationOfPoints(IEnumerable<PointShape>,Int32,DistanceUnit)`](#getelevationofpointsienumerable<pointshape>int32distanceunit)|
|[`GetElevationOfPoints(IEnumerable<PointShape>,String,DistanceUnit)`](#getelevationofpointsienumerable<pointshape>stringdistanceunit)|
|[`GetElevationOfPointsAsync(IEnumerable<PointShape>,Int32,DistanceUnit)`](#getelevationofpointsasyncienumerable<pointshape>int32distanceunit)|
|[`GetElevationOfPointsAsync(IEnumerable<PointShape>,String,DistanceUnit)`](#getelevationofpointsasyncienumerable<pointshape>stringdistanceunit)|
|[`GetElevationOfPointsInDecimalDegree(IEnumerable<PointShape>,DistanceUnit)`](#getelevationofpointsindecimaldegreeienumerable<pointshape>distanceunit)|
|[`GetElevationOfPointsInDecimalDegreeAsync(IEnumerable<PointShape>,DistanceUnit)`](#getelevationofpointsindecimaldegreeasyncienumerable<pointshape>distanceunit)|
|[`GetGradeOfLine(LineShape,Int32,Int32,DistanceUnit)`](#getgradeoflinelineshapeint32int32distanceunit)|
|[`GetGradeOfLine(LineShape,String,Int32,DistanceUnit)`](#getgradeoflinelineshapestringint32distanceunit)|
|[`GetGradeOfLine(LineShape,Int32,Double,DistanceUnit,DistanceUnit)`](#getgradeoflinelineshapeint32doubledistanceunitdistanceunit)|
|[`GetGradeOfLine(LineShape,String,Double,DistanceUnit,DistanceUnit)`](#getgradeoflinelineshapestringdoubledistanceunitdistanceunit)|
|[`GetGradeOfLineAsync(LineShape,Int32,Double,DistanceUnit,DistanceUnit)`](#getgradeoflineasynclineshapeint32doubledistanceunitdistanceunit)|
|[`GetGradeOfLineAsync(LineShape,String,Double,DistanceUnit,DistanceUnit)`](#getgradeoflineasynclineshapestringdoubledistanceunitdistanceunit)|
|[`GetGradeOfLineAsync(LineShape,Int32,Int32,DistanceUnit)`](#getgradeoflineasynclineshapeint32int32distanceunit)|
|[`GetGradeOfLineAsync(LineShape,String,Int32,DistanceUnit)`](#getgradeoflineasynclineshapestringint32distanceunit)|
|[`GetGradeOfLineInDecimalDegree(LineShape,Int32,DistanceUnit)`](#getgradeoflineindecimaldegreelineshapeint32distanceunit)|
|[`GetGradeOfLineInDecimalDegree(LineShape,Double,DistanceUnit,DistanceUnit)`](#getgradeoflineindecimaldegreelineshapedoubledistanceunitdistanceunit)|
|[`GetGradeOfLineInDecimalDegreeAsync(LineShape,Int32,DistanceUnit)`](#getgradeoflineindecimaldegreeasynclineshapeint32distanceunit)|
|[`GetGradeOfLineInDecimalDegreeAsync(LineShape,Double,DistanceUnit,DistanceUnit)`](#getgradeoflineindecimaldegreeasynclineshapedoubledistanceunitdistanceunit)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
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
|[`ElevationCloudClient()`](#elevationcloudclient)|
|[`ElevationCloudClient(String,String)`](#elevationcloudclientstringstring)|

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
#### `GetElevationOfArea(AreaBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|areaProjectionInSrid|`Int32`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfArea(AreaBaseShape,String,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|areaProjectionInProj4String|`String`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfAreaAsync(AreaBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|areaProjectionInSrid|`Int32`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfAreaAsync(AreaBaseShape,String,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|areaProjectionInProj4String|`String`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfAreaInDecimalDegree(AreaBaseShape,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfAreaInDecimalDegreeAsync(AreaBaseShape,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|area|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLine(LineBaseShape,Int32,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLine(LineBaseShape,String,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLine(LineBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLine(LineBaseShape,String,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineAsync(LineBaseShape,Int32,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineAsync(LineBaseShape,String,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineAsync(LineBaseShape,Int32,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineAsync(LineBaseShape,String,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineInDecimalDegree(LineBaseShape,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineInDecimalDegree(LineBaseShape,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineInDecimalDegreeAsync(LineBaseShape,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfLineInDecimalDegreeAsync(LineBaseShape,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPoint(Double,Double,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInSrid|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPoint(Double,Double,String,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInProj4String|`String`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointAsync(Double,Double,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<`Double`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInSrid|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointAsync(Double,Double,String,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<`Double`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|pointProjectionInProj4String|`String`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointInDecimalDegree(Double,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointInDecimalDegreeAsync(Double,Double,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<`Double`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|latitude|`Double`|N/A|
|longitude|`Double`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPoints(IEnumerable<PointShape>,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInSrid|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPoints(IEnumerable<PointShape>,String,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInProj4String|`String`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointsAsync(IEnumerable<PointShape>,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInSrid|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointsAsync(IEnumerable<PointShape>,String,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|pointsProjectionInProj4String|`String`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointsInDecimalDegree(IEnumerable<PointShape>,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetElevationOfPointsInDecimalDegreeAsync(IEnumerable<PointShape>,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudElevationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudElevationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLine(LineShape,Int32,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLine(LineShape,String,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLine(LineShape,Int32,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLine(LineShape,String,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineAsync(LineShape,Int32,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineAsync(LineShape,String,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineAsync(LineShape,Int32,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInSrid|`Int32`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineAsync(LineShape,String,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|lineProjectionInProj4String|`String`|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineInDecimalDegree(LineShape,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineInDecimalDegree(LineShape,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineInDecimalDegreeAsync(LineShape,Int32,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|numberOfSegments|`Int32`|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGradeOfLineInDecimalDegreeAsync(LineShape,Double,DistanceUnit,DistanceUnit)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGradeResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGradeResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|line|[`LineShape`](../ThinkGeo.Core/ThinkGeo.Core.LineShape.md)|N/A|
|intervalDistance|`Double`|N/A|
|intervalDistanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|elevationUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

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


