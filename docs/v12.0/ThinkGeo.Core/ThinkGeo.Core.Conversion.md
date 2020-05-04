# Conversion


## Inheritance Hierarchy

+ `Object`
  + **`Conversion`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`ConvertAngleUnits(Double,AngleUnit,AngleUnit)`](#convertangleunitsdoubleangleunitangleunit)|
|[`ConvertGeographyUnitToDistanceUnit(GeographyUnit)`](#convertgeographyunittodistanceunitgeographyunit)|
|[`ConvertMeasureUnits(Double,DistanceUnit,DistanceUnit)`](#convertmeasureunitsdoubledistanceunitdistanceunit)|
|[`ConvertMeasureUnits(Double,AreaUnit,AreaUnit)`](#convertmeasureunitsdoubleareaunitareaunit)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`DegreesToRadians(Single)`](#degreestoradianssingle)|
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
|N/A|

### Protected Constructors


### Public Properties


### Protected Properties


### Public Methods

#### `ConvertAngleUnits(Double,AngleUnit,AngleUnit)`

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
|angle|`Double`|N/A|
|fromUnit|[`AngleUnit`](../ThinkGeo.Core/ThinkGeo.Core.AngleUnit.md)|N/A|
|toUnit|[`AngleUnit`](../ThinkGeo.Core/ThinkGeo.Core.AngleUnit.md)|N/A|

---
#### `ConvertGeographyUnitToDistanceUnit(GeographyUnit)`

**Summary**

   *This method returns a DistanceUnit that has been converted from a GeographyUnit.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This method returns a DistanceUnit that has been converted from a GeographyUnit.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|unit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|The GeographyUnit you want to convert.|

---
#### `ConvertMeasureUnits(Double,DistanceUnit,DistanceUnit)`

**Summary**

   *This method converts from one unit of measure to another.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return length size, represented in the unit specified in the toUnit parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|amount|`Double`|The total length size, represented in the unit specified in the fromUnit parameter.|
|fromUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit of measure for the length in the Amount parameter.|
|toUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit of measure for the result.|

---
#### `ConvertMeasureUnits(Double,AreaUnit,AreaUnit)`

**Summary**

   *This method converts from one unit of measure to another.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The return area size, represented in the unit specified in the toUnit parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|amount|`Double`|The total area size, represented in the unit specified in the fromUnit parameter.|
|fromUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|The unit of measure for the area in the Amount parameter.|
|toUnit|[`AreaUnit`](../ThinkGeo.Core/ThinkGeo.Core.AreaUnit.md)|The unit of measure for the result.|

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

#### `DegreesToRadians(Single)`

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
|degrees|`Single`|N/A|

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


