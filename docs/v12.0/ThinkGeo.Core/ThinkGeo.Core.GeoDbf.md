# GeoDbf


## Inheritance Hierarchy

+ `Object`
  + **`GeoDbf`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeoDbf()`](#geodbf)|
|[`GeoDbf(String)`](#geodbfstring)|
|[`GeoDbf(String,FileAccess)`](#geodbfstringfileaccess)|
|[`GeoDbf(String,FileAccess,Encoding)`](#geodbfstringfileaccessencoding)|
|[`GeoDbf(String,FileAccess,Encoding,CultureInfo)`](#geodbfstringfileaccessencodingcultureinfo)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ColumnCount`](#columncount)|`Int32`|N/A|
|[`CultureInfo`](#cultureinfo)|`CultureInfo`|N/A|
|[`Encoding`](#encoding)|`Encoding`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`PathFilename`](#pathfilename)|`String`|N/A|
|[`ReadWriteMode`](#readwritemode)|`FileAccess`|N/A|
|[`RecordCount`](#recordcount)|`Int32`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`AddBooleanColumn(String)`](#addbooleancolumnstring)|
|[`AddColumn(String,DbfColumnType,Int32,Int32)`](#addcolumnstringdbfcolumntypeint32int32)|
|[`AddDateColumn(String)`](#adddatecolumnstring)|
|[`AddDoubleColumn(String,Int32,Int32)`](#adddoublecolumnstringint32int32)|
|[`AddEmptyRecord()`](#addemptyrecord)|
|[`AddIntegerColumn(String,Int32)`](#addintegercolumnstringint32)|
|[`AddMemoColumn(String,Int32)`](#addmemocolumnstringint32)|
|[`AddStringColumn(String,Int32)`](#addstringcolumnstringint32)|
|[`Close()`](#close)|
|[`CopyDbfHeader(String,String)`](#copydbfheaderstringstring)|
|[`CopyDbfHeader(String,String,OverwriteMode)`](#copydbfheaderstringstringoverwritemode)|
|[`CreateDbfFile(String,IEnumerable<DbfColumn>)`](#createdbffilestringienumerable<dbfcolumn>)|
|[`CreateDbfFile(String,IEnumerable<DbfColumn>,OverwriteMode)`](#createdbffilestringienumerable<dbfcolumn>overwritemode)|
|[`CreateDbfFile(String,IEnumerable<DbfColumn>,OverwriteMode,Encoding)`](#createdbffilestringienumerable<dbfcolumn>overwritemodeencoding)|
|[`DeleteRecord(Int32)`](#deleterecordint32)|
|[`Dispose()`](#dispose)|
|[`Equals(Object)`](#equalsobject)|
|[`Flush()`](#flush)|
|[`GetColumn(String)`](#getcolumnstring)|
|[`GetColumn(Int32)`](#getcolumnint32)|
|[`GetColumnName(Int32)`](#getcolumnnameint32)|
|[`GetColumnNumber(String)`](#getcolumnnumberstring)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`GetValidColumnNames(IEnumerable<String>)`](#getvalidcolumnnamesienumerable<string>)|
|[`GetValidColumnNames(IEnumerable<String>,Encoding)`](#getvalidcolumnnamesienumerable<string>encoding)|
|[`GetValidColumns(IEnumerable<DbfColumn>)`](#getvalidcolumnsienumerable<dbfcolumn>)|
|[`GetValidColumns(IEnumerable<DbfColumn>,Encoding)`](#getvalidcolumnsienumerable<dbfcolumn>encoding)|
|[`IsRecordDeleted(Int32)`](#isrecorddeletedint32)|
|[`Open()`](#open)|
|[`Pack()`](#pack)|
|[`ReadFieldAsBoolean(Int32,Int32)`](#readfieldasbooleanint32int32)|
|[`ReadFieldAsBoolean(Int32,String)`](#readfieldasbooleanint32string)|
|[`ReadFieldAsDateTime(Int32,String)`](#readfieldasdatetimeint32string)|
|[`ReadFieldAsDateTime(Int32,Int32)`](#readfieldasdatetimeint32int32)|
|[`ReadFieldAsDouble(Int32,String)`](#readfieldasdoubleint32string)|
|[`ReadFieldAsDouble(Int32,Int32)`](#readfieldasdoubleint32int32)|
|[`ReadFieldAsInteger(Int32,Int32)`](#readfieldasintegerint32int32)|
|[`ReadFieldAsInteger(Int32,String)`](#readfieldasintegerint32string)|
|[`ReadFieldAsString(Int32,String)`](#readfieldasstringint32string)|
|[`ReadFieldAsString(Int32,Int32)`](#readfieldasstringint32int32)|
|[`ReadRecord(Int32)`](#readrecordint32)|
|[`ReadRecordAsString(Int32)`](#readrecordasstringint32)|
|[`ToString()`](#tostring)|
|[`UndeleteRecord(Int32)`](#undeleterecordint32)|
|[`UpdateColumnName(Int32,String)`](#updatecolumnnameint32string)|
|[`UpdateDbcFilename(String)`](#updatedbcfilenamestring)|
|[`WriteField(Int32,String,Double)`](#writefieldint32stringdouble)|
|[`WriteField(Int32,Int32,Int32)`](#writefieldint32int32int32)|
|[`WriteField(Int32,String,Boolean)`](#writefieldint32stringboolean)|
|[`WriteField(Int32,Int32,Double)`](#writefieldint32int32double)|
|[`WriteField(Int32,String,Int32)`](#writefieldint32stringint32)|
|[`WriteField(Int32,Int32,String)`](#writefieldint32int32string)|
|[`WriteField(Int32,String,String)`](#writefieldint32stringstring)|
|[`WriteField(Int32,Int32,Boolean)`](#writefieldint32int32boolean)|
|[`WriteField(Int32,Int32,DateTime)`](#writefieldint32int32datetime)|
|[`WriteField(Int32,String,DateTime)`](#writefieldint32stringdatetime)|
|[`WriteRecord(Int32,IEnumerable<Object>)`](#writerecordint32ienumerable<object>)|

### Protected Methods Summary


|Name|
|---|
|[`AddDateTimeField(String)`](#adddatetimefieldstring)|
|[`Finalize()`](#finalize)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnStreamLoading(StreamLoadingEventArgs)`](#onstreamloadingstreamloadingeventargs)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`StreamLoading`](#streamloading)|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`GeoDbf()`](#geodbf)|
|[`GeoDbf(String)`](#geodbfstring)|
|[`GeoDbf(String,FileAccess)`](#geodbfstringfileaccess)|
|[`GeoDbf(String,FileAccess,Encoding)`](#geodbfstringfileaccessencoding)|
|[`GeoDbf(String,FileAccess,Encoding,CultureInfo)`](#geodbfstringfileaccessencodingcultureinfo)|

### Protected Constructors


### Public Properties

#### `ColumnCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `CultureInfo`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`CultureInfo`

---
#### `Encoding`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Encoding`

---
#### `IsOpen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `PathFilename`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `ReadWriteMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FileAccess`

---
#### `RecordCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---

### Protected Properties


### Public Methods

#### `AddBooleanColumn(String)`

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
|columnName|`String`|N/A|

---
#### `AddColumn(String,DbfColumnType,Int32,Int32)`

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
|columnName|`String`|N/A|
|columnType|[`DbfColumnType`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumnType.md)|N/A|
|length|`Int32`|N/A|
|decimalLength|`Int32`|N/A|

---
#### `AddDateColumn(String)`

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
|columnName|`String`|N/A|

---
#### `AddDoubleColumn(String,Int32,Int32)`

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
|columnName|`String`|N/A|
|length|`Int32`|N/A|
|decimalLength|`Int32`|N/A|

---
#### `AddEmptyRecord()`

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
#### `AddIntegerColumn(String,Int32)`

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
|columnName|`String`|N/A|
|length|`Int32`|N/A|

---
#### `AddMemoColumn(String,Int32)`

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
|columnName|`String`|N/A|
|length|`Int32`|N/A|

---
#### `AddStringColumn(String,Int32)`

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
|columnName|`String`|N/A|
|length|`Int32`|N/A|

---
#### `Close()`

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
#### `CopyDbfHeader(String,String)`

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
|sourcePathFilename|`String`|N/A|
|destinationPathFilename|`String`|N/A|

---
#### `CopyDbfHeader(String,String,OverwriteMode)`

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
|sourcePathFilename|`String`|N/A|
|destinationPathFilename|`String`|N/A|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|N/A|

---
#### `CreateDbfFile(String,IEnumerable<DbfColumn>)`

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
|dbfPathFilename|`String`|N/A|
|dbfColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|

---
#### `CreateDbfFile(String,IEnumerable<DbfColumn>,OverwriteMode)`

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
|dbfPathFilename|`String`|N/A|
|dbfColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|N/A|

---
#### `CreateDbfFile(String,IEnumerable<DbfColumn>,OverwriteMode,Encoding)`

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
|dbfPathFilename|`String`|N/A|
|dbfColumns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|
|overwriteMode|[`OverwriteMode`](../ThinkGeo.Core/ThinkGeo.Core.OverwriteMode.md)|N/A|
|encoding|`Encoding`|N/A|

---
#### `DeleteRecord(Int32)`

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
|recordNumber|`Int32`|N/A|

---
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
#### `Flush()`

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
#### `GetColumn(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|N/A|

---
#### `GetColumn(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnNumber|`Int32`|N/A|

---
#### `GetColumnName(Int32)`

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
|columnNumber|`Int32`|N/A|

---
#### `GetColumnNumber(String)`

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
|columnName|`String`|N/A|

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
#### `GetValidColumnNames(IEnumerable<String>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnNames|IEnumerable<`String`>|N/A|

---
#### `GetValidColumnNames(IEnumerable<String>,Encoding)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnNames|IEnumerable<`String`>|N/A|
|encoding|`Encoding`|N/A|

---
#### `GetValidColumns(IEnumerable<DbfColumn>)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|

---
#### `GetValidColumns(IEnumerable<DbfColumn>,Encoding)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columns|IEnumerable<[`DbfColumn`](../ThinkGeo.Core/ThinkGeo.Core.DbfColumn.md)>|N/A|
|encoding|`Encoding`|N/A|

---
#### `IsRecordDeleted(Int32)`

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
|recordNumber|`Int32`|N/A|

---
#### `Open()`

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
#### `Pack()`

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
#### `ReadFieldAsBoolean(Int32,Int32)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|

---
#### `ReadFieldAsBoolean(Int32,String)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|

---
#### `ReadFieldAsDateTime(Int32,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DateTime`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|

---
#### `ReadFieldAsDateTime(Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DateTime`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|

---
#### `ReadFieldAsDouble(Int32,String)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|

---
#### `ReadFieldAsDouble(Int32,Int32)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|

---
#### `ReadFieldAsInteger(Int32,Int32)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|

---
#### `ReadFieldAsInteger(Int32,String)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|

---
#### `ReadFieldAsString(Int32,String)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|

---
#### `ReadFieldAsString(Int32,Int32)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|

---
#### `ReadRecord(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,`Object`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|recordNumber|`Int32`|N/A|

---
#### `ReadRecordAsString(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,`String`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|recordNumber|`Int32`|N/A|

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
#### `UndeleteRecord(Int32)`

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
|recordNumber|`Int32`|N/A|

---
#### `UpdateColumnName(Int32,String)`

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
|columnNumber|`Int32`|N/A|
|newColumnName|`String`|N/A|

---
#### `UpdateDbcFilename(String)`

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
|newDbcFilename|`String`|N/A|

---
#### `WriteField(Int32,String,Double)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|
|value|`Double`|N/A|

---
#### `WriteField(Int32,Int32,Int32)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|
|value|`Int32`|N/A|

---
#### `WriteField(Int32,String,Boolean)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|
|value|`Boolean`|N/A|

---
#### `WriteField(Int32,Int32,Double)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|
|value|`Double`|N/A|

---
#### `WriteField(Int32,String,Int32)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|
|value|`Int32`|N/A|

---
#### `WriteField(Int32,Int32,String)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|
|value|`String`|N/A|

---
#### `WriteField(Int32,String,String)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|
|value|`String`|N/A|

---
#### `WriteField(Int32,Int32,Boolean)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|
|value|`Boolean`|N/A|

---
#### `WriteField(Int32,Int32,DateTime)`

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
|recordNumber|`Int32`|N/A|
|columnNumber|`Int32`|N/A|
|value|`DateTime`|N/A|

---
#### `WriteField(Int32,String,DateTime)`

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
|recordNumber|`Int32`|N/A|
|columnName|`String`|N/A|
|value|`DateTime`|N/A|

---
#### `WriteRecord(Int32,IEnumerable<Object>)`

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
|recordNumber|`Int32`|N/A|
|values|IEnumerable<`Object`>|N/A|

---

### Protected Methods

#### `AddDateTimeField(String)`

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
|columnName|`String`|N/A|

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
#### `OnStreamLoading(StreamLoadingEventArgs)`

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
|e|[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)|N/A|

---

### Public Events

#### StreamLoading

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`StreamLoadingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.StreamLoadingEventArgs.md)


