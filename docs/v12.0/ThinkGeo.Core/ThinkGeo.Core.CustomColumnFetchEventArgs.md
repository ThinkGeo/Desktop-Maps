# CustomColumnFetchEventArgs


## Inheritance Hierarchy

+ `Object`
  + `EventArgs`
    + **`CustomColumnFetchEventArgs`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`CustomColumnFetchEventArgs(String,String)`](#customcolumnfetcheventargsstringstring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ColumnName`](#columnname)|`String`|This property returns the column name that you need to return data for.|
|[`ColumnValue`](#columnvalue)|`String`|This parameter returns the field decimalDegreesValue that the event is seeking. It is intended to be set in the event.|
|[`Id`](#id)|`String`|This property returns the Id that you need to return data for.|

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
|[`CustomColumnFetchEventArgs(String,String)`](#customcolumnfetcheventargsstringstring)|

### Protected Constructors


### Public Properties

#### `ColumnName`

**Summary**

   *This property returns the column name that you need to return data for.*

**Remarks**

   *You will need to look up the Id in your external data source and find this column's data. CustomColumnFetch Event Background This event is used primarily when you have data relating to a particular feature or set of features that is not within source of the data. For example, you may have a shape file of the world whose .dbf component describes the area and population of each country. Additionally, in an outside SQL Server table, you may also have data about the countries, and it is this data that you wish to use for determining how you want to color each country. To integrate this SQL data, you simply create a file name that does not exist in the .dbf file.  Whenever Map Suite is queried to return records that specifically require this field, the FeatureSource will raise this event and allow you the developer to supply the data. In this way, you can query the SQL table and store the data in some sort of collection, and then when the event is raised, simply supply that data. As this is an event, it will raise for each feature and field combination requested. This means that the event can be raised quite often, and we suggest that you cache the data you wish to supply in memory. We recommend against sending out a new SQL query each time this event is raised. Image that you are supplementing two columns and your query returns 2,000 rows. This means that if you requested those fields, the event would be raised 4,000 times.*

**Return Value**

`String`

---
#### `ColumnValue`

**Summary**

   *This parameter returns the field decimalDegreesValue that the event is seeking. It is intended to be set in the event.*

**Remarks**

   *When you lookup the Id and FieldName, you should set this property with the data from your external data source. CustomColumnFetch Event Background It is used primarily when you have data relating to a particular feature or set of features that is not within source of the data. For example, you may have a shape file of the world whose .dbf component describes the area and population of each country. Additionally, in an outside SQL Server table, you may also have data about the countries, and it is this data that you wish to use for determining how you want to color each country. To integrate this SQL data, you simply create a file name that does not exist in the .dbf file.  Whenever Map Suite is queried to return records that specifically require this field, the FeatureSource will raise this event and allow you the developer to supply the data. In this way, you can query the SQL table and store the data in some sort of collection, and then when the event is raised, simply supply that data. As this is an event, it will raise for each feature and field combination requested. This means that the event can be raised quite often, and we suggest that you cache the data you wish to supply in memory. We recommend against sending out a new SQL query each time this event is raised. Image that you are supplementing two columns and your query returns 2,000 rows. This means that if you requested those fields, the event would be raised 4,000 times.*

**Return Value**

`String`

---
#### `Id`

**Summary**

   *This property returns the Id that you need to return data for.*

**Remarks**

   *You will need to look up the Id in your external data source and find this field's data. CustomColumnFetch Event Background It is used primarily when you have data relating to a particular feature or set of features that is not within source of the data. For example, you may have a shape file of the world whose .dbf component describes the area and population of each country. Additionally, in an outside SQL Server table, you may also have data about the countries, and it is this data that you wish to use for determining how you want to color each country. To integrate this SQL data, you simply create a file name that does not exist in the .dbf file.  Whenever Map Suite is queried to return records that specifically require this field, the FeatureSource will raise this event and allow you the developer to supply the data. In this way, you can query the SQL table and store the data in some sort of collection, and then when the event is raised, simply supply that data. As this is an event, it will raise for each feature and field combination requested. This means that the event can be raised quite often, and we suggest that you cache the data you wish to supply in memory. We recommend against sending out a new SQL query each time this event is raised. Image that you are supplementing two columns and your query returns 2,000 rows. This means that if you requested those fields, the event would be raised 4,000 times.*

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


