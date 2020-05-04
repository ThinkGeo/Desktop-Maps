# DecimalDegreesHelper


## Inheritance Hierarchy

+ `Object`
  + **`DecimalDegreesHelper`**

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
|[`ConvertFromMgrs(String)`](#convertfrommgrsstring)|
|[`ConvertToMgrs(Double,Double)`](#converttomgrsdoubledouble)|
|[`Equals(Object)`](#equalsobject)|
|[`GetDecimalDegreeFromDegreesMinutesSeconds(String)`](#getdecimaldegreefromdegreesminutessecondsstring)|
|[`GetDecimalDegreeFromDegreesMinutesSeconds(DegreesMinutesSeconds)`](#getdecimaldegreefromdegreesminutessecondsdegreesminutesseconds)|
|[`GetDecimalDegreeFromDegreesMinutesSeconds(Int32,Int32,Double)`](#getdecimaldegreefromdegreesminutessecondsint32int32double)|
|[`GetDegreesMinutesSecondsFromDecimalDegree(Double)`](#getdegreesminutessecondsfromdecimaldegreedouble)|
|[`GetDegreesMinutesSecondsStringFromDecimalDegree(Double)`](#getdegreesminutessecondsstringfromdecimaldegreedouble)|
|[`GetDegreesMinutesSecondsStringFromDecimalDegree(Double,Int32)`](#getdegreesminutessecondsstringfromdecimaldegreedoubleint32)|
|[`GetDegreesMinutesSecondsStringFromDecimalDegreePoint(PointShape)`](#getdegreesminutessecondsstringfromdecimaldegreepointpointshape)|
|[`GetDegreesMinutesSecondsStringFromDecimalDegreePoint(PointShape,Int32)`](#getdegreesminutessecondsstringfromdecimaldegreepointpointshapeint32)|
|[`GetDegreesMinutesSecondsStringFromDecimalDegreePoint(Feature,Int32)`](#getdegreesminutessecondsstringfromdecimaldegreepointfeatureint32)|
|[`GetDegreesMinutesSecondsStringFromDecimalDegreePoint(Feature)`](#getdegreesminutessecondsstringfromdecimaldegreepointfeature)|
|[`GetDegreesMinutesStringFromDecimalDegreePoint(Feature,Int32)`](#getdegreesminutesstringfromdecimaldegreepointfeatureint32)|
|[`GetDegreesMinutesStringFromDecimalDegreePoint(Feature)`](#getdegreesminutesstringfromdecimaldegreepointfeature)|
|[`GetDegreesMinutesStringFromDecimalDegreePoint(PointShape)`](#getdegreesminutesstringfromdecimaldegreepointpointshape)|
|[`GetDegreesMinutesStringFromDecimalDegreePoint(PointShape,Int32)`](#getdegreesminutesstringfromdecimaldegreepointpointshapeint32)|
|[`GetDistanceFromDecimalDegrees(PointShape,PointShape,DistanceUnit)`](#getdistancefromdecimaldegreespointshapepointshapedistanceunit)|
|[`GetDistanceFromDecimalDegrees(Feature,Feature,DistanceUnit)`](#getdistancefromdecimaldegreesfeaturefeaturedistanceunit)|
|[`GetDistanceFromDecimalDegrees(Double,Double,Double,Double,DistanceUnit)`](#getdistancefromdecimaldegreesdoubledoubledoubledoubledistanceunit)|
|[`GetHashCode()`](#gethashcode)|
|[`GetLatitudeDifferenceFromDistance(Double,DistanceUnit)`](#getlatitudedifferencefromdistancedoubledistanceunit)|
|[`GetLongitudeDifferenceFromDistance(Double,DistanceUnit,Double)`](#getlongitudedifferencefromdistancedoubledistanceunitdouble)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`GetDegreesMinutesStringFromDecimalDegree(Double)`](#getdegreesminutesstringfromdecimaldegreedouble)|
|[`GetDegreesMinutesStringFromDecimalDegree(Double,Int32)`](#getdegreesminutesstringfromdecimaldegreedoubleint32)|
|[`GetDistanceFromDecimalDegreesLine(Double,Double,Double,Double,PointShape,DistanceUnit)`](#getdistancefromdecimaldegreeslinedoubledoubledoubledoublepointshapedistanceunit)|
|[`GetGreatCircle(PointShape,PointShape,Int32)`](#getgreatcirclepointshapepointshapeint32)|
|[`GetLatitudeFromDistanceAndDegree(Double,Double,Double,DistanceUnit,Double)`](#getlatitudefromdistanceanddegreedoubledoubledoubledistanceunitdouble)|
|[`GetLongitudeFromDistanceAndDegree(Double,Double,Double,DistanceUnit,Double)`](#getlongitudefromdistanceanddegreedoubledoubledoubledistanceunitdouble)|
|[`GetNearestPointFromPointShapeDecimalDegreesLine(Double,Double,Double,Double,PointShape)`](#getnearestpointfrompointshapedecimaldegreeslinedoubledoubledoubledoublepointshape)|
|[`GetXFromDegreeOnSphere(Double,Double,DistanceUnit)`](#getxfromdegreeonspheredoubledoubledistanceunit)|
|[`GetYFromDegreeOnSphere(Double,DistanceUnit)`](#getyfromdegreeonspheredoubledistanceunit)|
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

#### `ConvertFromMgrs(String)`

**Summary**

   *Convert the MGRS string to latitude and longitude*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The converted coordinate.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|mgrs|`String`|MGRS string|

---
#### `ConvertToMgrs(Double,Double)`

**Summary**

   *Convert the input latitude and longitude to MGRS string*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|The MGRS corresponding to input latitude and longitude.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|latitude|`Double`|Latitude coordinate.|
|longitude|`Double`|Longitude coordinate.|

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
#### `GetDecimalDegreeFromDegreesMinutesSeconds(String)`

**Summary**

   *This method returns a decimal degree value based on a string containing degrees, minutes, and seconds.*

**Remarks**

   *If you pass in "75?21' 28''" as a string, then the result will be 75.35777777784.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns a decimal degree value based on a string containing degrees, minutes, and seconds.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|degreesMinutesSeconds|`String`|This parameter represents the degrees, minutes and seconds in a string.|

---
#### `GetDecimalDegreeFromDegreesMinutesSeconds(DegreesMinutesSeconds)`

**Summary**

   *This method returns a decimal degree value based on a degree, minute and second structure.*

**Remarks**

   *If you pass in 75, 21 and 28, the result passed back will be 75.2577777778.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns a decimal degree value based on a degree, minute and second structure.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|degreesMinutesSeconds|[`DegreesMinutesSeconds`](../ThinkGeo.Core/ThinkGeo.Core.DegreesMinutesSeconds.md)|This structure represents the degrees, minutes and seconds.|

---
#### `GetDecimalDegreeFromDegreesMinutesSeconds(Int32,Int32,Double)`

**Summary**

   *This method returns a decimal degree value based on a set of degrees, minutes, and seconds.*

**Remarks**

   *If you pass in 75, 21 and 28, the result passed back will be 75.2577777778.*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns a decimal degree value based on a set of degrees, minutes, and seconds.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|degrees|`Int32`|This parameter represents the degree component of the degrees, minutes and seconds.|
|minutes|`Int32`|This parameter represents the minute component of the degrees, minutes and seconds.|
|seconds|`Double`|This parameter represents the second component of the degrees, minutes and seconds.|

---
#### `GetDegreesMinutesSecondsFromDecimalDegree(Double)`

**Summary**

   *This method returns a degrees, minutes and seconds structure from a decimal degree value.*

**Remarks**

   *The method allows you pass in a decimal degree number and return the degree, minute, second as variables passed in on the method call.*

**Return Value**

|Type|Description|
|---|---|
|[`DegreesMinutesSeconds`](../ThinkGeo.Core/ThinkGeo.Core.DegreesMinutesSeconds.md)|This method returns a degrees, minutes and seconds structure from a decimal degree value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|decimalDegreesValue|`Double`|The decimal degree value you want to convert.|

---
#### `GetDegreesMinutesSecondsStringFromDecimalDegree(Double)`

**Summary**

   *This method returns a string representation in degrees, minutes and seconds from a decimal degree value.*

**Remarks**

   *Example: If you enter 75.358 as the number of decimal degrees, the result would be 75 degrees, 21 minutes, 28 seconds. Thus, the return string would be 75?21' 28".*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees, minutes and seconds from a decimal degree value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|decimalDegreesValue|`Double`|The number of decimal degrees to convert.|

---
#### `GetDegreesMinutesSecondsStringFromDecimalDegree(Double,Int32)`

**Summary**

   *This method returns a string representation in degrees, minutes and seconds from a decimal degree value and a specified precision.*

**Remarks**

   *Example: If you enter 75.358 as the number of decimal degree and 12 as decimals, the result would be 75 degrees, 21 minutes, 28.80000000015 seconds. Thus, the return string would be 75?21' 28.80000000015".*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees, minutes and seconds from a decimal degree value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|decimalDegreesValue|`Double`|The number of decimal degree to convert.|
|decimals|`Int32`|The number of float decision for the second.|

---
#### `GetDegreesMinutesSecondsStringFromDecimalDegreePoint(PointShape)`

**Summary**

   *This method returns a string representation in degrees, minutes and seconds from a decimal degree value.*

**Remarks**

   *Passing in a point will return the point's location represented in degrees, minutes, and seconds. For example, if the point's location in decimal degrees is (75.358, 36.345), the actual returned string would be "75?21' 29''E  36?20' 42''N".*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees, minutes and seconds from a decimal degree value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The point you want to convert.|

---
#### `GetDegreesMinutesSecondsStringFromDecimalDegreePoint(PointShape,Int32)`

**Summary**

   *This method returns a string representation in degrees, minutes and seconds from a decimal degree value.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees, minutes and seconds from a decimal degree value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The point of decimal degree to convert.|
|decimals|`Int32`|The number of decimal degree to convert.|

---
#### `GetDegreesMinutesSecondsStringFromDecimalDegreePoint(Feature,Int32)`

**Summary**

   *This method returns a string representation in degrees, minutes and seconds from a decimal degree value.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees, minutes and seconds from a decimal degree value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature whose decimal degrees to convert.|
|decimalPlaces|`Int32`|The number of decimal degree to convert.|

---
#### `GetDegreesMinutesSecondsStringFromDecimalDegreePoint(Feature)`

**Summary**

   *This method returns a string representation in degrees, minutes and seconds from a decimal degree value.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees, minutes and seconds from a decimal degree value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature whose decimal degrees to convert.|

---
#### `GetDegreesMinutesStringFromDecimalDegreePoint(Feature,Int32)`

**Summary**

   *This method returns a string representation in degrees and minutes from a decimal degree point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees and minutes from a decimal degree point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you want to convert.|
|decimals|`Int32`|Number of decimals for the Minutes value|

---
#### `GetDegreesMinutesStringFromDecimalDegreePoint(Feature)`

**Summary**

   *This method returns a string representation in degrees and minutes from a decimal degree point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees and minutes from a decimal degree point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The point you want to convert.|

---
#### `GetDegreesMinutesStringFromDecimalDegreePoint(PointShape)`

**Summary**

   *This method returns a string representation in degrees and minutes from a decimal degree point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees and minutes from a decimal degree point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetDegreesMinutesStringFromDecimalDegreePoint(PointShape,Int32)`

**Summary**

   *This method returns a string representation in degrees and minutes from a decimal degree point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a string representation in degrees and minutes from a decimal degree point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|decimals|`Int32`|Number of decimals for the Minutes value|

---
#### `GetDistanceFromDecimalDegrees(PointShape,PointShape,DistanceUnit)`

**Summary**

   *This method returns the distance between two decimal degree points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns the distance between two decimal degree points in the unit specified by the returningUnit parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The point shape you will measure from.|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|The point shape you will measure to.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit you would like your results back in, such as miles or kilometers.|

---
#### `GetDistanceFromDecimalDegrees(Feature,Feature,DistanceUnit)`

**Summary**

   *This method returns the distance between two decimal degree points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns the distance between two decimal degree points in the unit specified by the returningUnit parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you will measure from.|
|toPointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|The feature you will measure to.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit you would like your results back in, such as miles or kilometers.|

---
#### `GetDistanceFromDecimalDegrees(Double,Double,Double,Double,DistanceUnit)`

**Summary**

   *This method returns the distance between two decimal degree points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns the distance between two decimal degree points in the unit specified by the returningUnit parameter.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromLatitude|`Double`|This is the from latitude value.|
|fromLongitude|`Double`|This is the from longitude value.|
|toLatitude|`Double`|This is the to latitude value.|
|toLongitude|`Double`|This is the to longitude value.|
|returningUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the distance unit you would like to use in the return value. For example, if you select miles as your returningUnit, then the distance will be returned in miles.|

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
#### `GetLatitudeDifferenceFromDistance(Double,DistanceUnit)`

**Summary**

   *Calculate the amount of longitude change given a certain distance and longitude.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|Double representing the distance.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance over which you would like to know the change in longitude.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit the distance is in, such as miles or kilometers.|

---
#### `GetLongitudeDifferenceFromDistance(Double,DistanceUnit,Double)`

**Summary**

   *Calculate the amount of longitude change given a certain distance and latitude.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|Double representing the distance.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|distance|`Double`|The distance over which you would like to know the change in longitude.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|The unit the distance is in, such as miles or kilometers.|
|latitude|`Double`|The latitude on the globe that the distance is measured at.|

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
#### `GetDegreesMinutesStringFromDecimalDegree(Double)`

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
|decimalDegreesValue|`Double`|N/A|

---
#### `GetDegreesMinutesStringFromDecimalDegree(Double,Int32)`

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
|decimalDegreesValue|`Double`|N/A|
|decimals|`Int32`|N/A|

---
#### `GetDistanceFromDecimalDegreesLine(Double,Double,Double,Double,PointShape,DistanceUnit)`

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
|fromPointX|`Double`|N/A|
|fromPointY|`Double`|N/A|
|toPointX|`Double`|N/A|
|toPointY|`Double`|N/A|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|lengthUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetGreatCircle(PointShape,PointShape,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`MultilineShape`](../ThinkGeo.Core/ThinkGeo.Core.MultilineShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|count|`Int32`|N/A|

---
#### `GetLatitudeFromDistanceAndDegree(Double,Double,Double,DistanceUnit,Double)`

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
|fromLongitude|`Double`|N/A|
|fromLatitude|`Double`|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|degree|`Double`|N/A|

---
#### `GetLongitudeFromDistanceAndDegree(Double,Double,Double,DistanceUnit,Double)`

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
|fromLongitude|`Double`|N/A|
|fromLatitude|`Double`|N/A|
|distance|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|
|degree|`Double`|N/A|

---
#### `GetNearestPointFromPointShapeDecimalDegreesLine(Double,Double,Double,Double,PointShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPointX|`Double`|N/A|
|fromPointY|`Double`|N/A|
|toPointX|`Double`|N/A|
|toPointY|`Double`|N/A|
|pointShape|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetXFromDegreeOnSphere(Double,Double,DistanceUnit)`

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
|degreeX|`Double`|N/A|
|degreeY|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

---
#### `GetYFromDegreeOnSphere(Double,DistanceUnit)`

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
|degreeY|`Double`|N/A|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|N/A|

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


