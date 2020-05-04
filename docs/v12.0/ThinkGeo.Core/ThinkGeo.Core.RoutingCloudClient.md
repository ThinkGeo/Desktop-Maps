# RoutingCloudClient


## Inheritance Hierarchy

+ `Object`
  + [`CloudClient`](../ThinkGeo.Core/ThinkGeo.Core.CloudClient.md)
    + **`RoutingCloudClient`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`RoutingCloudClient()`](#routingcloudclient)|
|[`RoutingCloudClient(String,String)`](#routingcloudclientstringstring)|

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
|[`GetDistanceCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`](#getdistancecostmatrixienumerable<pointshape>ienumerable<pointshape>cloudroutinggetcostmatrixoptions)|
|[`GetDistanceCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`](#getdistancecostmatrixienumerable<pointshape>ienumerable<pointshape>int32cloudroutinggetcostmatrixoptions)|
|[`GetDistanceCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`](#getdistancecostmatrixienumerable<pointshape>ienumerable<pointshape>stringcloudroutinggetcostmatrixoptions)|
|[`GetDistanceCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`](#getdistancecostmatrixasyncienumerable<pointshape>ienumerable<pointshape>cloudroutinggetcostmatrixoptions)|
|[`GetDistanceCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`](#getdistancecostmatrixasyncienumerable<pointshape>ienumerable<pointshape>int32cloudroutinggetcostmatrixoptions)|
|[`GetDistanceCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`](#getdistancecostmatrixasyncienumerable<pointshape>ienumerable<pointshape>stringcloudroutinggetcostmatrixoptions)|
|[`GetHashCode()`](#gethashcode)|
|[`GetOptimizedRoute(IEnumerable<PointShape>,CloudRoutingOptimizationOptions)`](#getoptimizedrouteienumerable<pointshape>cloudroutingoptimizationoptions)|
|[`GetOptimizedRoute(IEnumerable<PointShape>,Int32,CloudRoutingOptimizationOptions)`](#getoptimizedrouteienumerable<pointshape>int32cloudroutingoptimizationoptions)|
|[`GetOptimizedRoute(IEnumerable<PointShape>,String,CloudRoutingOptimizationOptions)`](#getoptimizedrouteienumerable<pointshape>stringcloudroutingoptimizationoptions)|
|[`GetOptimizedRouteAsync(IEnumerable<PointShape>,CloudRoutingOptimizationOptions)`](#getoptimizedrouteasyncienumerable<pointshape>cloudroutingoptimizationoptions)|
|[`GetOptimizedRouteAsync(IEnumerable<PointShape>,Int32,CloudRoutingOptimizationOptions)`](#getoptimizedrouteasyncienumerable<pointshape>int32cloudroutingoptimizationoptions)|
|[`GetOptimizedRouteAsync(IEnumerable<PointShape>,String,CloudRoutingOptimizationOptions)`](#getoptimizedrouteasyncienumerable<pointshape>stringcloudroutingoptimizationoptions)|
|[`GetRoute(IEnumerable<PointShape>,CloudRoutingGetRouteOptions)`](#getrouteienumerable<pointshape>cloudroutinggetrouteoptions)|
|[`GetRoute(IEnumerable<PointShape>,Int32,CloudRoutingGetRouteOptions)`](#getrouteienumerable<pointshape>int32cloudroutinggetrouteoptions)|
|[`GetRoute(IEnumerable<PointShape>,String,CloudRoutingGetRouteOptions)`](#getrouteienumerable<pointshape>stringcloudroutinggetrouteoptions)|
|[`GetRouteAsync(IEnumerable<PointShape>,CloudRoutingGetRouteOptions)`](#getrouteasyncienumerable<pointshape>cloudroutinggetrouteoptions)|
|[`GetRouteAsync(IEnumerable<PointShape>,Int32,CloudRoutingGetRouteOptions)`](#getrouteasyncienumerable<pointshape>int32cloudroutinggetrouteoptions)|
|[`GetRouteAsync(IEnumerable<PointShape>,String,CloudRoutingGetRouteOptions)`](#getrouteasyncienumerable<pointshape>stringcloudroutinggetrouteoptions)|
|[`GetServiceArea(PointShape,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`](#getserviceareapointshapeienumerable<double>cloudroutinggetserviceareaoptions)|
|[`GetServiceArea(PointShape,Int32,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`](#getserviceareapointshapeint32ienumerable<double>cloudroutinggetserviceareaoptions)|
|[`GetServiceArea(PointShape,String,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`](#getserviceareapointshapestringienumerable<double>cloudroutinggetserviceareaoptions)|
|[`GetServiceArea(PointShape,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`](#getserviceareapointshapeienumerable<timespan>cloudroutinggetserviceareaoptions)|
|[`GetServiceArea(PointShape,Int32,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`](#getserviceareapointshapeint32ienumerable<timespan>cloudroutinggetserviceareaoptions)|
|[`GetServiceArea(PointShape,String,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`](#getserviceareapointshapestringienumerable<timespan>cloudroutinggetserviceareaoptions)|
|[`GetServiceAreaAsync(PointShape,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`](#getserviceareaasyncpointshapeienumerable<double>cloudroutinggetserviceareaoptions)|
|[`GetServiceAreaAsync(PointShape,Int32,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`](#getserviceareaasyncpointshapeint32ienumerable<double>cloudroutinggetserviceareaoptions)|
|[`GetServiceAreaAsync(PointShape,String,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`](#getserviceareaasyncpointshapestringienumerable<double>cloudroutinggetserviceareaoptions)|
|[`GetServiceAreaAsync(PointShape,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`](#getserviceareaasyncpointshapeienumerable<timespan>cloudroutinggetserviceareaoptions)|
|[`GetServiceAreaAsync(PointShape,Int32,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`](#getserviceareaasyncpointshapeint32ienumerable<timespan>cloudroutinggetserviceareaoptions)|
|[`GetServiceAreaAsync(PointShape,String,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`](#getserviceareaasyncpointshapestringienumerable<timespan>cloudroutinggetserviceareaoptions)|
|[`GetTimeCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`](#gettimecostmatrixienumerable<pointshape>ienumerable<pointshape>cloudroutinggetcostmatrixoptions)|
|[`GetTimeCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`](#gettimecostmatrixienumerable<pointshape>ienumerable<pointshape>int32cloudroutinggetcostmatrixoptions)|
|[`GetTimeCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`](#gettimecostmatrixienumerable<pointshape>ienumerable<pointshape>stringcloudroutinggetcostmatrixoptions)|
|[`GetTimeCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`](#gettimecostmatrixasyncienumerable<pointshape>ienumerable<pointshape>cloudroutinggetcostmatrixoptions)|
|[`GetTimeCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`](#gettimecostmatrixasyncienumerable<pointshape>ienumerable<pointshape>int32cloudroutinggetcostmatrixoptions)|
|[`GetTimeCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`](#gettimecostmatrixasyncienumerable<pointshape>ienumerable<pointshape>stringcloudroutinggetcostmatrixoptions)|
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
|[`RoutingCloudClient()`](#routingcloudclient)|
|[`RoutingCloudClient(String,String)`](#routingcloudclientstringstring)|

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
#### `GetDistanceCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetDistanceCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetDistanceCostMatrixResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetDistanceCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetDistanceCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetDistanceCostMatrixResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetDistanceCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetDistanceCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetDistanceCostMatrixResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetDistanceCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetDistanceCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetDistanceCostMatrixResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetDistanceCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetDistanceCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetDistanceCostMatrixResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetDistanceCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetDistanceCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetDistanceCostMatrixResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

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
#### `GetOptimizedRoute(IEnumerable<PointShape>,CloudRoutingOptimizationOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingOptimizationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|coordinates|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingOptimizationOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationOptions.md)|N/A|

---
#### `GetOptimizedRoute(IEnumerable<PointShape>,Int32,CloudRoutingOptimizationOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingOptimizationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|coordinates|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingOptimizationOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationOptions.md)|N/A|

---
#### `GetOptimizedRoute(IEnumerable<PointShape>,String,CloudRoutingOptimizationOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingOptimizationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|coordinates|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingOptimizationOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationOptions.md)|N/A|

---
#### `GetOptimizedRouteAsync(IEnumerable<PointShape>,CloudRoutingOptimizationOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingOptimizationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|coordinates|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingOptimizationOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationOptions.md)|N/A|

---
#### `GetOptimizedRouteAsync(IEnumerable<PointShape>,Int32,CloudRoutingOptimizationOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingOptimizationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|coordinates|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingOptimizationOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationOptions.md)|N/A|

---
#### `GetOptimizedRouteAsync(IEnumerable<PointShape>,String,CloudRoutingOptimizationOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingOptimizationResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|coordinates|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingOptimizationOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingOptimizationOptions.md)|N/A|

---
#### `GetRoute(IEnumerable<PointShape>,CloudRoutingGetRouteOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetRouteResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|waypoints|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingGetRouteOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteOptions.md)|N/A|

---
#### `GetRoute(IEnumerable<PointShape>,Int32,CloudRoutingGetRouteOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetRouteResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|waypoints|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingGetRouteOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteOptions.md)|N/A|

---
#### `GetRoute(IEnumerable<PointShape>,String,CloudRoutingGetRouteOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetRouteResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|waypoints|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingGetRouteOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteOptions.md)|N/A|

---
#### `GetRouteAsync(IEnumerable<PointShape>,CloudRoutingGetRouteOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetRouteResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|waypoints|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingGetRouteOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteOptions.md)|N/A|

---
#### `GetRouteAsync(IEnumerable<PointShape>,Int32,CloudRoutingGetRouteOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetRouteResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|waypoints|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingGetRouteOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteOptions.md)|N/A|

---
#### `GetRouteAsync(IEnumerable<PointShape>,String,CloudRoutingGetRouteOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetRouteResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|waypoints|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingGetRouteOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetRouteOptions.md)|N/A|

---
#### `GetServiceArea(PointShape,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|serviceLimits|IEnumerable<`Double`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceArea(PointShape,Int32,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|srid|`Int32`|N/A|
|serviceLimits|IEnumerable<`Double`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceArea(PointShape,String,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|proj4String|`String`|N/A|
|serviceLimits|IEnumerable<`Double`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceArea(PointShape,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|serviceLimits|IEnumerable<`TimeSpan`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceArea(PointShape,Int32,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|srid|`Int32`|N/A|
|serviceLimits|IEnumerable<`TimeSpan`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceArea(PointShape,String,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|proj4String|`String`|N/A|
|serviceLimits|IEnumerable<`TimeSpan`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceAreaAsync(PointShape,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|serviceLimits|IEnumerable<`Double`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceAreaAsync(PointShape,Int32,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|srid|`Int32`|N/A|
|serviceLimits|IEnumerable<`Double`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceAreaAsync(PointShape,String,IEnumerable<Double>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|proj4String|`String`|N/A|
|serviceLimits|IEnumerable<`Double`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceAreaAsync(PointShape,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|serviceLimits|IEnumerable<`TimeSpan`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceAreaAsync(PointShape,Int32,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|srid|`Int32`|N/A|
|serviceLimits|IEnumerable<`TimeSpan`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetServiceAreaAsync(PointShape,String,IEnumerable<TimeSpan>,CloudRoutingGetServiceAreaOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetServiceAreaResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|proj4String|`String`|N/A|
|serviceLimits|IEnumerable<`TimeSpan`>|N/A|
|options|[`CloudRoutingGetServiceAreaOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetServiceAreaOptions.md)|N/A|

---
#### `GetTimeCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetTimeCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetTimeCostMatrixResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetTimeCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetTimeCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetTimeCostMatrixResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetTimeCostMatrix(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudRoutingGetTimeCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetTimeCostMatrixResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetTimeCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetTimeCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetTimeCostMatrixResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetTimeCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,Int32,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetTimeCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetTimeCostMatrixResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|srid|`Int32`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

---
#### `GetTimeCostMatrixAsync(IEnumerable<PointShape>,IEnumerable<PointShape>,String,CloudRoutingGetCostMatrixOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudRoutingGetTimeCostMatrixResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetTimeCostMatrixResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|origins|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|destinations|IEnumerable<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)>|N/A|
|proj4String|`String`|N/A|
|options|[`CloudRoutingGetCostMatrixOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudRoutingGetCostMatrixOptions.md)|N/A|

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


