# FeatureCache


## Inheritance Hierarchy

+ `Object`
  + **`FeatureCache`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`FeatureCache()`](#featurecache)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsActive`](#isactive)|`Boolean`|This boolean property sepcifies whether cache system is active or not.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Add(RectangleShape,Collection<Feature>)`](#addrectangleshapecollection<feature>)|
|[`Clear()`](#clear)|
|[`Close()`](#close)|
|[`Equals(Object)`](#equalsobject)|
|[`GetFeatures(RectangleShape)`](#getfeaturesrectangleshape)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`IsExtentCached(RectangleShape)`](#isextentcachedrectangleshape)|
|[`Open()`](#open)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`AddCore(RectangleShape,Collection<Feature>)`](#addcorerectangleshapecollection<feature>)|
|[`CloseCore()`](#closecore)|
|[`Finalize()`](#finalize)|
|[`GetFeaturesCore(RectangleShape)`](#getfeaturescorerectangleshape)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OpenCore()`](#opencore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`FeatureCache()`](#featurecache)|

### Protected Constructors


### Public Properties

#### `IsActive`

**Summary**

   *This boolean property sepcifies whether cache system is active or not.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---

### Protected Properties


### Public Methods

#### `Add(RectangleShape,Collection<Feature>)`

**Summary**

   *This method will add an item to the FeatureCache system by passing a WorldExtent as "Key" and a collection of features as its corresponding cached features.*

**Remarks**

   *This method is the concrete wrapper for the virtual method AddCore. It will return whatever is returned by the AddCore method. To determine what the default implementation of the abstract AddCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|features|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `Clear()`

**Summary**

   *This method clears all of the CachedItems in this FeatureCache.*

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
#### `Close()`

**Summary**

   *The default implementation is to do nothing.*

**Remarks**

   *This method is the concrete wrapper for the virtual method CloseCore. It will return whatever is returned by the CloseCore method. To determine what the default implementation of the abstract CloseCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

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
#### `GetFeatures(RectangleShape)`

**Summary**

   *This method returns a Collection of Features cached in the FeatureCache system. All of the returning features are within the BoundingBox and were fetched from FileSystem or DBSystem in a previous operation.*

**Remarks**

   *This method is the concrete wrapper for the virtual method GetFeaturesCore. It will return whatever is returned by the GetBoundingBoxCore method. To determine what the default implementation of the abstract GetBoundingBoxCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a Collection of Features cached in the FeatureCache system.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `IsExtentCached(RectangleShape)`

**Summary**

   *This method determines whether the WorldExtent is already cached in the FeatureCache System.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|True if the specified WorldExtent is already cached. Otherwise, returns false.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The target WorldExtent which will be determined as already cached or not.|

---
#### `Open()`

**Summary**

   *The default implementation is to do nothing.*

**Remarks**

   *This method is the concrete wrapper for the virtual method OpenCore. It will return whatever is returned by the OpenCore method. To determine what the default implementation of the abstract OpenCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

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

#### `AddCore(RectangleShape,Collection<Feature>)`

**Summary**

   *This method will add an item to the FeatureCache system by passing a WorldExtent as "Key" and a collection of features as its corresponding cached features.*

**Remarks**

   *This method is the concrete wrapper for the virtual method AddCore. It will return whatever is returned by the AddCore method. To determine what the default implementation of the abstract AddCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|features|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `CloseCore()`

**Summary**

   *The default implementation is to do nothing.*

**Remarks**

   *This method is the concrete wrapper for the virtual method CloseCore. It will return whatever is returned by the CloseCore method. To determine what the default implementation of the abstract CloseCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `GetFeaturesCore(RectangleShape)`

**Summary**

   *This method returns a Collection of Features cached in the FeatureCache system. All of the returning features are within the BoundingBox and were fetched from FileSystem or DBSystem in a previous operation.*

**Remarks**

   *This method is the concrete wrapper for the virtual method GetFeaturesCore. It will return whatever is returned by the GetBoundingBoxCore method. To determine what the default implementation of the abstract GetBoundingBoxCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns a Collection of Features cached in the FeatureCache system.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `OpenCore()`

**Summary**

   *The default implementation is to do nothing.*

**Remarks**

   *This method is the concrete wrapper for the virtual method OpenCore. It will return whatever is returned by the OpenCore method. To determine what the default implementation of the abstract OpenCore method is, please see the documentation for it. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Events


