# FeatureSourceColumn


## Inheritance Hierarchy

+ `Object`
  + **`FeatureSourceColumn`**
    + [`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)
    + [`SqliteColumn`](../ThinkGeo.Core/ThinkGeo.Core.SqliteColumn.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`FeatureSourceColumn()`](#featuresourcecolumn)|
|[`FeatureSourceColumn(String)`](#featuresourcecolumnstring)|
|[`FeatureSourceColumn(String,String,Int32)`](#featuresourcecolumnstringstringint32)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ColumnName`](#columnname)|`String`|This property returns the name of the column.|
|[`MaxLength`](#maxlength)|`Int32`|This property returns the maximum length of the column.|
|[`TypeName`](#typename)|`String`|This property returns the type name of the column.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
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
|[`FeatureSourceColumn()`](#featuresourcecolumn)|
|[`FeatureSourceColumn(String)`](#featuresourcecolumnstring)|
|[`FeatureSourceColumn(String,String,Int32)`](#featuresourcecolumnstringstringint32)|

### Protected Constructors


### Public Properties

#### `ColumnName`

**Summary**

   *This property returns the name of the column.*

**Remarks**

   *None*

**Return Value**

`String`

---
#### `MaxLength`

**Summary**

   *This property returns the maximum length of the column.*

**Remarks**

   *The maximum length is user defined and not in any way enforced in our default FeatureSource implementation. It is mainly for display purposes or when dealing with a known Feature Source type.*

**Return Value**

`Int32`

---
#### `TypeName`

**Summary**

   *This property returns the type name of the column.*

**Remarks**

   *This property is freeform and the type name is not tied to anything. It is wise to try and use familiar type names such as string, integer, date, etc., although it may not fit every different type of FeatureSource. We suggest thar you avoid using this property unless you know the types beforehand or simply want them for display purposes.*

**Return Value**

`String`

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

   *Returns column name of FeatureSourceColumn*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|Returns column name of FeatureSourceColumn|

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


