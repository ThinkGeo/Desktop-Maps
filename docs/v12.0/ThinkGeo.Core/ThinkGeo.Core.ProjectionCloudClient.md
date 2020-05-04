# ProjectionCloudClient


## Inheritance Hierarchy

+ `Object`
  + [`CloudClient`](../ThinkGeo.Core/ThinkGeo.Core.CloudClient.md)
    + **`ProjectionCloudClient`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`ProjectionCloudClient()`](#projectioncloudclient)|
|[`ProjectionCloudClient(String,String)`](#projectioncloudclientstringstring)|

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
|[`Project(Feature,Int32,Int32)`](#projectfeatureint32int32)|
|[`Project(Feature,Int32,String)`](#projectfeatureint32string)|
|[`Project(Feature,String,Int32)`](#projectfeaturestringint32)|
|[`Project(Feature,String,String)`](#projectfeaturestringstring)|
|[`Project(IEnumerable<Feature>,Int32,Int32)`](#projectienumerable<feature>int32int32)|
|[`Project(IEnumerable<Feature>,Int32,String)`](#projectienumerable<feature>int32string)|
|[`Project(IEnumerable<Feature>,String,Int32)`](#projectienumerable<feature>stringint32)|
|[`Project(IEnumerable<Feature>,String,String)`](#projectienumerable<feature>stringstring)|
|[`ProjectAsync(Feature,Int32,Int32)`](#projectasyncfeatureint32int32)|
|[`ProjectAsync(Feature,Int32,String)`](#projectasyncfeatureint32string)|
|[`ProjectAsync(Feature,String,Int32)`](#projectasyncfeaturestringint32)|
|[`ProjectAsync(Feature,String,String)`](#projectasyncfeaturestringstring)|
|[`ProjectAsync(IEnumerable<Feature>,Int32,Int32)`](#projectasyncienumerable<feature>int32int32)|
|[`ProjectAsync(IEnumerable<Feature>,Int32,String)`](#projectasyncienumerable<feature>int32string)|
|[`ProjectAsync(IEnumerable<Feature>,String,Int32)`](#projectasyncienumerable<feature>stringint32)|
|[`ProjectAsync(IEnumerable<Feature>,String,String)`](#projectasyncienumerable<feature>stringstring)|
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
|[`ProjectionCloudClient()`](#projectioncloudclient)|
|[`ProjectionCloudClient(String,String)`](#projectioncloudclientstringstring)|

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
#### `Project(Feature,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `Project(Feature,Int32,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInProj4String|`String`|N/A|

---
#### `Project(Feature,String,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `Project(Feature,String,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInProj4String|`String`|N/A|

---
#### `Project(IEnumerable<Feature>,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `Project(IEnumerable<Feature>,Int32,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInProj4String|`String`|N/A|

---
#### `Project(IEnumerable<Feature>,String,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `Project(IEnumerable<Feature>,String,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInProj4String|`String`|N/A|

---
#### `ProjectAsync(Feature,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `ProjectAsync(Feature,Int32,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInProj4String|`String`|N/A|

---
#### `ProjectAsync(Feature,String,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `ProjectAsync(Feature,String,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInProj4String|`String`|N/A|

---
#### `ProjectAsync(IEnumerable<Feature>,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `ProjectAsync(IEnumerable<Feature>,Int32,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInSrid|`Int32`|N/A|
|toProjectionInProj4String|`String`|N/A|

---
#### `ProjectAsync(IEnumerable<Feature>,String,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInSrid|`Int32`|N/A|

---
#### `ProjectAsync(IEnumerable<Feature>,String,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Task<Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|fromProjectionInProj4String|`String`|N/A|
|toProjectionInProj4String|`String`|N/A|

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


