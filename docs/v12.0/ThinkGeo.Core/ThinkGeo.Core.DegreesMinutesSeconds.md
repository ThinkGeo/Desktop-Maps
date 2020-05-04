# DegreesMinutesSeconds


## Inheritance Hierarchy

+ `Object`
  + `ValueType`
    + **`DegreesMinutesSeconds`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`DegreesMinutesSeconds(Int32,Int32,Double)`](#degreesminutessecondsint32int32double)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Degrees`](#degrees)|`Int32`|This property returns the degrees portion of the structure.|
|[`Minutes`](#minutes)|`Int32`|This property returns the minutes portion of the structure.|
|[`Seconds`](#seconds)|`Double`|This property returns the seconds portion of the structure.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Add(DegreesMinutesSeconds)`](#adddegreesminutesseconds)|
|[`Equals(Object)`](#equalsobject)|
|[`GetFormattedString(Int32)`](#getformattedstringint32)|
|[`GetFormattedString(DegreesMinutesSecondsFormatType)`](#getformattedstringdegreesminutessecondsformattype)|
|[`GetFormattedString(DegreesMinutesSecondsFormatType,Int32)`](#getformattedstringdegreesminutessecondsformattypeint32)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
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
|[`DegreesMinutesSeconds(Int32,Int32,Double)`](#degreesminutessecondsint32int32double)|

### Protected Constructors


### Public Properties

#### `Degrees`

**Summary**

   *This property returns the degrees portion of the structure.*

**Remarks**

   *None*

**Return Value**

`Int32`

---
#### `Minutes`

**Summary**

   *This property returns the minutes portion of the structure.*

**Remarks**

   *None*

**Return Value**

`Int32`

---
#### `Seconds`

**Summary**

   *This property returns the seconds portion of the structure.*

**Remarks**

   *None*

**Return Value**

`Double`

---

### Protected Properties


### Public Methods

#### `Add(DegreesMinutesSeconds)`

**Summary**

   *Add two DegreesMinutsSeconds together and return back a summary of the two.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`DegreesMinutesSeconds`](../ThinkGeo.Core/ThinkGeo.Core.DegreesMinutesSeconds.md)|The summary of the two DegreesMinutesSeconds.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetDegreesMinutesSeconds|[`DegreesMinutesSeconds`](../ThinkGeo.Core/ThinkGeo.Core.DegreesMinutesSeconds.md)|The target DegreesMinutesSeconds to be added together.|

---
#### `Equals(Object)`

**Summary**

   *Compares current DegreesMinutesSeconds with a passing object.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|True if the passing object satisfies the following two conditions:1) The object is of DegreesMinutesSeconds type.2) The Degrees, Minutes and Seconds of both DegreesMinutesSeconds should be the same.If both conditions are not met, will return false.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|obj|`Object`|The passing object which will be used to compare with current DegreesMinutesSeconds.|

---
#### `GetFormattedString(Int32)`

**Summary**

   *This method returns a formatted representation of the degrees, minutes and seconds value that has been rounded to the specified decimals.*

**Remarks**

   *The value will be formatted in the standard string format. For example, 75º 21' 2.1235" (when the decimals parameter is set to 4).*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a formatted representation of the degrees, minutes and seconds value.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|decimals|`Int32`|The target decimals that the degrees, minutes, seconds value will be rounded to.|

---
#### `GetFormattedString(DegreesMinutesSecondsFormatType)`

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
|formatType|[`DegreesMinutesSecondsFormatType`](../ThinkGeo.Core/ThinkGeo.Core.DegreesMinutesSecondsFormatType.md)|N/A|

---
#### `GetFormattedString(DegreesMinutesSecondsFormatType,Int32)`

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
|formatType|[`DegreesMinutesSecondsFormatType`](../ThinkGeo.Core/ThinkGeo.Core.DegreesMinutesSecondsFormatType.md)|N/A|
|decimals|`Int32`|N/A|

---
#### `GetHashCode()`

**Summary**

   *Serves as hash function for the particular type.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|The hash code for this particular type.|

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

   *This method returns a formatted representation of the degrees, minutes and seconds value.*

**Remarks**

   *The value will be formatted in the standard string format: 75º 21' 28"*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns a formatted representation of the degrees, minutes and seconds value.|

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


