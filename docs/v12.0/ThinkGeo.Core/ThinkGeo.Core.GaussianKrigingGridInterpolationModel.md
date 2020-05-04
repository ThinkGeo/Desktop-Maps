# GaussianKrigingGridInterpolationModel


## Inheritance Hierarchy

+ `Object`
  + [`GridInterpolationModel`](../ThinkGeo.Core/ThinkGeo.Core.GridInterpolationModel.md)
    + [`KrigingGridInterpolationModel`](../ThinkGeo.Core/ThinkGeo.Core.KrigingGridInterpolationModel.md)
      + **`GaussianKrigingGridInterpolationModel`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GaussianKrigingGridInterpolationModel()`](#gaussiankriginggridinterpolationmodel)|
|[`GaussianKrigingGridInterpolationModel(IDictionary<PointShape,Double>)`](#gaussiankriginggridinterpolationmodelidictionary<pointshapedouble>)|
|[`GaussianKrigingGridInterpolationModel(IDictionary<PointShape,Double>,Int32)`](#gaussiankriginggridinterpolationmodelidictionary<pointshapedouble>int32)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`NumberOfReferencedPoints`](#numberofreferencedpoints)|`Int32`|N/A|
|[`Points`](#points)|Dictionary<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md),`Double`>|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`GetEmpiricalFunctionCoefficients(Int32,Double[,],Double[,],Double)`](#getempiricalfunctioncoefficientsint32double[]double[]double)|
|[`GetHashCode()`](#gethashcode)|
|[`GetSemivariance(Double,EmpiricalFunctionCoefficients)`](#getsemivariancedoubleempiricalfunctioncoefficients)|
|[`GetType()`](#gettype)|
|[`Interpolate(RectangleShape,GridDefinition)`](#interpolaterectangleshapegriddefinition)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`GetEmpiricalFunctionCoefficientsCore(Int32,Double[,],Double[,],Double)`](#getempiricalfunctioncoefficientscoreint32double[]double[]double)|
|[`GetSemivarianceCore(Double,EmpiricalFunctionCoefficients)`](#getsemivariancecoredoubleempiricalfunctioncoefficients)|
|[`GetSillSlope(Double,Double)`](#getsillslopedoubledouble)|
|[`GetSillSlopeCore(Double,Double)`](#getsillslopecoredoubledouble)|
|[`InterpolateCore(RectangleShape,GridDefinition)`](#interpolatecorerectangleshapegriddefinition)|
|[`MemberwiseClone()`](#memberwiseclone)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`GaussianKrigingGridInterpolationModel()`](#gaussiankriginggridinterpolationmodel)|
|[`GaussianKrigingGridInterpolationModel(IDictionary<PointShape,Double>)`](#gaussiankriginggridinterpolationmodelidictionary<pointshapedouble>)|
|[`GaussianKrigingGridInterpolationModel(IDictionary<PointShape,Double>,Int32)`](#gaussiankriginggridinterpolationmodelidictionary<pointshapedouble>int32)|

### Protected Constructors


### Public Properties

#### `NumberOfReferencedPoints`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Points`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md),`Double`>

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
#### `GetEmpiricalFunctionCoefficients(Int32,Double[,],Double[,],Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`EmpiricalFunctionCoefficients`](../ThinkGeo.Core/ThinkGeo.Core.EmpiricalFunctionCoefficients.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|numberOfPoints|`Int32`|N/A|
|distance|`Double[,]`|N/A|
|semivariances|`Double[,]`|N/A|
|maxRange|`Double`|N/A|

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
#### `GetSemivariance(Double,EmpiricalFunctionCoefficients)`

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
|distance|`Double`|N/A|
|coefficients|[`EmpiricalFunctionCoefficients`](../ThinkGeo.Core/ThinkGeo.Core.EmpiricalFunctionCoefficients.md)|N/A|

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
#### `Interpolate(RectangleShape,GridDefinition)`

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
|cellExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|gridDefinition|[`GridDefinition`](../ThinkGeo.Core/ThinkGeo.Core.GridDefinition.md)|N/A|

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
#### `GetEmpiricalFunctionCoefficientsCore(Int32,Double[,],Double[,],Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`EmpiricalFunctionCoefficients`](../ThinkGeo.Core/ThinkGeo.Core.EmpiricalFunctionCoefficients.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|points|`Int32`|N/A|
|distance|`Double[,]`|N/A|
|semivariances|`Double[,]`|N/A|
|maxRange|`Double`|N/A|

---
#### `GetSemivarianceCore(Double,EmpiricalFunctionCoefficients)`

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
|distance|`Double`|N/A|
|coefficients|[`EmpiricalFunctionCoefficients`](../ThinkGeo.Core/ThinkGeo.Core.EmpiricalFunctionCoefficients.md)|N/A|

---
#### `GetSillSlope(Double,Double)`

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
|distance|`Double`|N/A|
|range|`Double`|N/A|

---
#### `GetSillSlopeCore(Double,Double)`

**Summary**

   *Gets the sill slope core.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`||

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance.|
|range|`Double`|The range.|

---
#### `InterpolateCore(RectangleShape,GridDefinition)`

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
|cellExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|gridDefinition|[`GridDefinition`](../ThinkGeo.Core/ThinkGeo.Core.GridDefinition.md)|N/A|

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


