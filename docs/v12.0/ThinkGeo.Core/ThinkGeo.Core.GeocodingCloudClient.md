# GeocodingCloudClient


## Inheritance Hierarchy

+ `Object`
  + [`CloudClient`](../ThinkGeo.Core/ThinkGeo.Core.CloudClient.md)
    + **`GeocodingCloudClient`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeocodingCloudClient()`](#geocodingcloudclient)|
|[`GeocodingCloudClient(String,String)`](#geocodingcloudclientstringstring)|

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
|[`Search(String)`](#searchstring)|
|[`Search(String,CloudGeocodingOptions)`](#searchstringcloudgeocodingoptions)|
|[`Search(IEnumerable<String>)`](#searchienumerable<string>)|
|[`Search(IEnumerable<String>,CloudGeocodingOptions)`](#searchienumerable<string>cloudgeocodingoptions)|
|[`SearchAsync(String)`](#searchasyncstring)|
|[`SearchAsync(String,CloudGeocodingOptions)`](#searchasyncstringcloudgeocodingoptions)|
|[`SearchAsync(IEnumerable<String>)`](#searchasyncienumerable<string>)|
|[`SearchAsync(IEnumerable<String>,CloudGeocodingOptions)`](#searchasyncienumerable<string>cloudgeocodingoptions)|
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
|[`GeocodingCloudClient()`](#geocodingcloudclient)|
|[`GeocodingCloudClient(String,String)`](#geocodingcloudclientstringstring)|

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
#### `Search(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|location|`String`|N/A|

---
#### `Search(String,CloudGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|location|`String`|N/A|
|options|[`CloudGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingOptions.md)|N/A|

---
#### `Search(IEnumerable<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|locations|IEnumerable<`String`>|N/A|

---
#### `Search(IEnumerable<String>,CloudGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|locations|IEnumerable<`String`>|N/A|
|options|[`CloudGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingOptions.md)|N/A|

---
#### `SearchAsync(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|location|`String`|N/A|

---
#### `SearchAsync(String,CloudGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|location|`String`|N/A|
|options|[`CloudGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingOptions.md)|N/A|

---
#### `SearchAsync(IEnumerable<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|locations|IEnumerable<`String`>|N/A|

---
#### `SearchAsync(IEnumerable<String>,CloudGeocodingOptions)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`CloudGeocodingResult`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingResult.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|locations|IEnumerable<`String`>|N/A|
|options|[`CloudGeocodingOptions`](../ThinkGeo.Core/ThinkGeo.Core.CloudGeocodingOptions.md)|N/A|

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


