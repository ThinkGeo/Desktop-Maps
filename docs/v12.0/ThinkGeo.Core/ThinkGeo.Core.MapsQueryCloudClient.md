# MapsQueryCloudClient


## Inheritance Hierarchy

+ `Object`
  + [`CloudClient`](../ThinkGeo.Core/ThinkGeo.Core.CloudClient.md)
    + **`MapsQueryCloudClient`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MapsQueryCloudClient()`](#mapsquerycloudclient)|
|[`MapsQueryCloudClient(String,String)`](#mapsquerycloudclientstringstring)|

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
|[`GetAttributesOfLayer(String)`](#getattributesoflayerstring)|
|[`GetAttributesOfLayerAsync(String)`](#getattributesoflayerasyncstring)|
|[`GetFeaturesContaining(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturescontainingstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesContaining(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturescontainingstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesContaining(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturescontainingstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesContainingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturescontainingasyncstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesContainingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturescontainingasyncstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesContainingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturescontainingasyncstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesCustom(CloudMapsQueryCustomQueryOptions)`](#getfeaturescustomcloudmapsquerycustomqueryoptions)|
|[`GetFeaturesCustomAsync(CloudMapsQueryCustomQueryOptions)`](#getfeaturescustomasynccloudmapsquerycustomqueryoptions)|
|[`GetFeaturesIntersecting(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesintersectingstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesIntersecting(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesintersectingstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesIntersecting(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesintersectingstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesIntersectingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesintersectingasyncstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesIntersectingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesintersectingasyncstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesIntersectingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesintersectingasyncstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesNearest(String,BaseShape,Int32,CloudMapsQueryNearestQueryOptions)`](#getfeaturesneareststringbaseshapeint32cloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearest(String,BaseShape,Int32,Int32,CloudMapsQueryNearestQueryOptions)`](#getfeaturesneareststringbaseshapeint32int32cloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearest(String,BaseShape,String,Int32,CloudMapsQueryNearestQueryOptions)`](#getfeaturesneareststringbaseshapestringint32cloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearest(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`](#getfeaturesneareststringbaseshapeint32doubledistanceunitcloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearest(String,BaseShape,Int32,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`](#getfeaturesneareststringbaseshapeint32int32doubledistanceunitcloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearest(String,BaseShape,String,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`](#getfeaturesneareststringbaseshapestringint32doubledistanceunitcloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearestAsync(String,BaseShape,Int32,CloudMapsQueryNearestQueryOptions)`](#getfeaturesnearestasyncstringbaseshapeint32cloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearestAsync(String,BaseShape,Int32,Int32,CloudMapsQueryNearestQueryOptions)`](#getfeaturesnearestasyncstringbaseshapeint32int32cloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearestAsync(String,BaseShape,String,Int32,CloudMapsQueryNearestQueryOptions)`](#getfeaturesnearestasyncstringbaseshapestringint32cloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearestAsync(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`](#getfeaturesnearestasyncstringbaseshapeint32doubledistanceunitcloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearestAsync(String,BaseShape,Int32,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`](#getfeaturesnearestasyncstringbaseshapeint32int32doubledistanceunitcloudmapsquerynearestqueryoptions)|
|[`GetFeaturesNearestAsync(String,BaseShape,String,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`](#getfeaturesnearestasyncstringbaseshapestringint32doubledistanceunitcloudmapsquerynearestqueryoptions)|
|[`GetFeaturesOverlapping(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesoverlappingstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesOverlapping(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesoverlappingstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesOverlapping(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesoverlappingstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesOverlappingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesoverlappingasyncstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesOverlappingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesoverlappingasyncstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesOverlappingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturesoverlappingasyncstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesTouching(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturestouchingstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesTouching(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturestouchingstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesTouching(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturestouchingstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesTouchingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeaturestouchingasyncstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesTouchingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeaturestouchingasyncstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesTouchingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeaturestouchingasyncstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithin(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithinstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithin(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithinstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithin(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithinstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithinasyncstringbaseshapecloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithinasyncstringbaseshapeint32cloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithinasyncstringbaseshapestringcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinDistance(String,BaseShape,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithindistancestringbaseshapedoubledistanceunitcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinDistance(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithindistancestringbaseshapeint32doubledistanceunitcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinDistance(String,BaseShape,String,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithindistancestringbaseshapestringdoubledistanceunitcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinDistanceAsync(String,BaseShape,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithindistanceasyncstringbaseshapedoubledistanceunitcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinDistanceAsync(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithindistanceasyncstringbaseshapeint32doubledistanceunitcloudmapsqueryspatialqueryoptions)|
|[`GetFeaturesWithinDistanceAsync(String,BaseShape,String,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`](#getfeatureswithindistanceasyncstringbaseshapestringdoubledistanceunitcloudmapsqueryspatialqueryoptions)|
|[`GetHashCode()`](#gethashcode)|
|[`GetLayers()`](#getlayers)|
|[`GetLayersAsync()`](#getlayersasync)|
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
|[`MapsQueryCloudClient()`](#mapsquerycloudclient)|
|[`MapsQueryCloudClient(String,String)`](#mapsquerycloudclientstringstring)|

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
#### `GetAttributesOfLayer(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryGetAttributesOfLayerResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryGetAttributesOfLayerResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|

---
#### `GetAttributesOfLayerAsync(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryGetAttributesOfLayerResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryGetAttributesOfLayerResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|

---
#### `GetFeaturesContaining(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesContaining(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesContaining(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesContainingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesContainingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesContainingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesCustom(CloudMapsQueryCustomQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|options|[`CloudMapsQueryCustomQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryCustomQueryOptions.md)|N/A|

---
#### `GetFeaturesCustomAsync(CloudMapsQueryCustomQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|options|[`CloudMapsQueryCustomQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryCustomQueryOptions.md)|N/A|

---
#### `GetFeaturesIntersecting(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesIntersecting(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesIntersecting(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesIntersectingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesIntersectingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesIntersectingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesNearest(String,BaseShape,Int32,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|maxResults|`Int32`|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearest(String,BaseShape,Int32,Int32,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|maxResults|`Int32`|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearest(String,BaseShape,String,Int32,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|maxResults|`Int32`|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearest(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|maxResults|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|searchRadiusUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearest(String,BaseShape,Int32,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|maxResults|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|searchRadiusUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearest(String,BaseShape,String,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|maxResults|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|searchRadiusUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearestAsync(String,BaseShape,Int32,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|maxResults|`Int32`|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearestAsync(String,BaseShape,Int32,Int32,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|maxResults|`Int32`|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearestAsync(String,BaseShape,String,Int32,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|maxResults|`Int32`|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearestAsync(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|maxResults|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|searchRadiusUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearestAsync(String,BaseShape,Int32,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|maxResults|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|searchRadiusUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesNearestAsync(String,BaseShape,String,Int32,Double,DistanceUnit,CloudMapsQueryNearestQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|maxResults|`Int32`|N/A|
|searchRadius|`Double`|N/A|
|searchRadiusUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQueryNearestQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryNearestQueryOptions.md)|N/A|

---
#### `GetFeaturesOverlapping(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesOverlapping(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesOverlapping(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesOverlappingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesOverlappingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesOverlappingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesTouching(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesTouching(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesTouching(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesTouchingAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesTouchingAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesTouchingAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithin(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithin(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithin(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinAsync(String,BaseShape,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinAsync(String,BaseShape,Int32,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinAsync(String,BaseShape,String,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinDistance(String,BaseShape,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinDistance(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinDistance(String,BaseShape,String,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinDistanceAsync(String,BaseShape,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinDistanceAsync(String,BaseShape,Int32,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|srid|`Int32`|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

---
#### `GetFeaturesWithinDistanceAsync(String,BaseShape,String,Double,DistanceUnit,CloudMapsQuerySpatialQueryOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|queryLayer|`String`|N/A|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|
|proj4String|`String`|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|options|[`CloudMapsQuerySpatialQueryOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQuerySpatialQueryOptions.md)|N/A|

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
#### `GetLayers()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudMapsQueryGetLayersResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryGetLayersResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetLayersAsync()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudMapsQueryGetLayersResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudMapsQueryGetLayersResult.md)>|N/A|

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


