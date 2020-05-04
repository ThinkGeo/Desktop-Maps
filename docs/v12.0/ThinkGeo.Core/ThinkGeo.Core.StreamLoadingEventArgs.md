# StreamLoadingEventArgs


## Inheritance Hierarchy

+ `Object`
  + `EventArgs`
    + **`StreamLoadingEventArgs`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`StreamLoadingEventArgs()`](#streamloadingeventargs)|
|[`StreamLoadingEventArgs(String,String)`](#streamloadingeventargsstringstring)|
|[`StreamLoadingEventArgs(String,Stream,FileMode,FileAccess)`](#streamloadingeventargsstringstreamfilemodefileaccess)|
|[`StreamLoadingEventArgs(String,String,Stream,FileMode,FileAccess)`](#streamloadingeventargsstringstringstreamfilemodefileaccess)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AlternateStream`](#alternatestream)|`Stream`|This property gets and sets the alternate stream you want to use.|
|[`AlternateStreamName`](#alternatestreamname)|`String`|This property gets or sets the source of the stream you wish the user to pass you.|
|[`FileMode`](#filemode)|`FileMode`|This property gets and sets the file mode that the alternate stream need to function as.|
|[`ReadWriteMode`](#readwritemode)|`FileAccess`|This property gets and sets the file access that the alternate stream need to function as.|
|[`StreamType`](#streamtype)|`String`|This property gets the the stream type you wish the user to pass you. The value represents corresponding stream type: If it is "Image File": it represents you need to pass in a stream represents image file, such as .bmp file stream. If it is "World File": it represents you need to pass in a stream represents world file, such as .bpw file stream. If it is "SHP File": it represents you need to pass in a stream represents .shp file. If it is "SHX File": it represents you need to pass in a stream represents .shx file. If it is "DBF File": it represents you need to pass in a stream represents .dbf file. If it is "DBT File": it represents you need to pass in a stream represents .dbt file. If it is "IDX File": it represents you need to pass in a stream represents .idx file. If it is "IDS File": it represents you need to pass in a stream represents .ids file. If it is "NativeImage": it represents you need to pass in a stream represents NativeImage.|

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
|[`StreamLoadingEventArgs()`](#streamloadingeventargs)|
|[`StreamLoadingEventArgs(String,String)`](#streamloadingeventargsstringstring)|
|[`StreamLoadingEventArgs(String,Stream,FileMode,FileAccess)`](#streamloadingeventargsstringstreamfilemodefileaccess)|
|[`StreamLoadingEventArgs(String,String,Stream,FileMode,FileAccess)`](#streamloadingeventargsstringstringstreamfilemodefileaccess)|

### Protected Constructors


### Public Properties

#### `AlternateStream`

**Summary**

   *This property gets and sets the alternate stream you want to use.*

**Remarks**

   *When the event is raised the user should be responsible for either ignoring this property or setting it. It should default to null and if the user wishes to use an alternate stream instead of a file from the disk then they should replace it here. Alternate Streams Where possible we allow you to use stream in place of concreate files on the file system. This gives you the flexibility to retrieve data from isolated storage, encrypted files, compressed files, fast memory streams or any other stream origin. This is typically available where you would pass in a path and file name. Streams can be substituted in a number of places such as images, shape files etc. Below describes how the system works though specific places may have slightly different variations. You will create the object that will use the stream normally such as a ShapeFielFeatureSource and then subscribe to the StreamLoading event. For these classes you typically need to supply a path and file name, while this is still required you can pass in a valid path that does not exist such as "Z:\ShapeFile1.shp". When we need the stream from you we will give you this string so you can find the associated stream. In essence you can use the path and file name as a key to kink to the source of your stream. When we need the file we will raise the StreamLoading event and allow you to pass an alternate stream. In the StreamLoading event we provide you with the path and file name you used and we expect for you to create the stream and set it as the AlternatStream property along with setting the FileMode and File access appropriate to the stream. This helps us know our limits with the stream.*

**Return Value**

`Stream`

---
#### `AlternateStreamName`

**Summary**

   *This property gets or sets the source of the stream you wish the user to pass you.*

**Remarks**

   *This is always set by the person who created the event arguments and should not be changed by the user. The streamSource name just needs to be some unique string that will let the user know which file or stream you want. For example you could have a streamSourceName of "Z:\test.shp" and though the Z drive might not exist it might be a cue for you to load "test.shp" from isolated storage. We suggest you use a string that is in the file format of "?:\????.???" as this allows is to validate it though we do not check if it exists. Alternate Streams Where possible we allow you to use stream in place of concreate files on the file system. This gives you the flexibility to retrieve data from isolated storage, encrypted files, compressed files, fast memory streams or any other stream origin. This is typically available where you would pass in a path and file name. Streams can be substituted in a number of places such as images, shape files etc. Below describes how the system works though specific places may have slightly different variations. You will create the object that will use the stream normally such as a ShapeFielFeatureSource and then subscribe to the StreamLoading event. For these classes you typically need to supply a path and file name, while this is still required you can pass in a valid path that does not exist such as "Z:\ShapeFile1.shp". When we need the stream from you we will give you this string so you can find the associated stream. In essence you can use the path and file name as a key to kink to the source of your stream. When we need the file we will raise the StreamLoading event and allow you to pass an alternate stream. In the StreamLoading event we provide you with the path and file name you used and we expect for you to create the stream and set it as the AlternatStream property along with setting the FileMode and File access appropriate to the stream. This helps us know our limits with the stream.*

**Return Value**

`String`

---
#### `FileMode`

**Summary**

   *This property gets and sets the file mode that the alternate stream need to function as.*

**Remarks**

   *You should set the file mode to the mode that best describes the limitations inherent to your alternate stream. Alternate Streams Where possible we allow you to use stream in place of concreate files on the file system. This gives you the flexibility to retrieve data from isolated storage, encrypted files, compressed files, fast memory streams or any other stream origin. This is typically available where you would pass in a path and file name. Streams can be substituted in a number of places such as images, shape files etc. Below describes how the system works though specific places may have slightly different variations. You will create the object that will use the stream normally such as a ShapeFielFeatureSource and then subscribe to the StreamLoading event. For these classes you typically need to supply a path and file name, while this is still required you can pass in a valid path that does not exist such as "Z:\ShapeFile1.shp". When we need the stream from you we will give you this string so you can find the associated stream. In essence you can use the path and file name as a key to kink to the source of your stream. When we need the file we will raise the StreamLoading event and allow you to pass an alternate stream. In the StreamLoading event we provide you with the path and file name you used and we expect for you to create the stream and set it as the AlternatStream property along with setting the FileMode and File access appropriate to the stream. This helps us know our limits with the stream.*

**Return Value**

`FileMode`

---
#### `ReadWriteMode`

**Summary**

   *This property gets and sets the file access that the alternate stream need to function as.*

**Remarks**

   *You should set the file access to the mode that best describes the limitations inherent to your alternate stream. Alternate Streams Where possible we allow you to use stream in place of concreate files on the file system. This gives you the flexibility to retrieve data from isolated storage, encrypted files, compressed files, fast memory streams or any other stream origin. This is typically available where you would pass in a path and file name. Streams can be substituted in a number of places such as images, shape files etc. Below describes how the system works though specific places may have slightly different variations. You will create the object that will use the stream normally such as a ShapeFielFeatureSource and then subscribe to the StreamLoading event. For these classes you typically need to supply a path and file name, while this is still required you can pass in a valid path that does not exist such as "Z:\ShapeFile1.shp". When we need the stream from you we will give you this string so you can find the associated stream. In essence you can use the path and file name as a key to kink to the source of your stream. When we need the file we will raise the StreamLoading event and allow you to pass an alternate stream. In the StreamLoading event we provide you with the path and file name you used and we expect for you to create the stream and set it as the AlternatStream property along with setting the FileMode and File access appropriate to the stream. This helps us know our limits with the stream.*

**Return Value**

`FileAccess`

---
#### `StreamType`

**Summary**

   *This property gets the the stream type you wish the user to pass you. The value represents corresponding stream type: If it is "Image File": it represents you need to pass in a stream represents image file, such as .bmp file stream. If it is "World File": it represents you need to pass in a stream represents world file, such as .bpw file stream. If it is "SHP File": it represents you need to pass in a stream represents .shp file. If it is "SHX File": it represents you need to pass in a stream represents .shx file. If it is "DBF File": it represents you need to pass in a stream represents .dbf file. If it is "DBT File": it represents you need to pass in a stream represents .dbt file. If it is "IDX File": it represents you need to pass in a stream represents .idx file. If it is "IDS File": it represents you need to pass in a stream represents .ids file. If it is "NativeImage": it represents you need to pass in a stream represents NativeImage.*

**Remarks**

   *This is always set by the person who created the event arguments and should not be changed by the user. The streamSource name just needs to be some unique string that will let the user know which file or stream you want. For example you could have a streamSourceName of "Z:\test.shp" and though the Z drive might not exist it might be a cue for you to load "test.shp" from isolated storage. We suggest you use a string that is in the file format of "?:\????.???" as this allows is to validate it though we do not check if it exists. Alternate Streams Where possible we allow you to use stream in place of concreate files on the file system. This gives you the flexibility to retrieve data from isolated storage, encrypted files, compressed files, fast memory streams or any other stream origin. This is typically available where you would pass in a path and file name. Streams can be substituted in a number of places such as images, shape files etc. Below describes how the system works though specific places may have slightly different variations. You will create the object that will use the stream normally such as a ShapeFielFeatureSource and then subscribe to the StreamLoading event. For these classes you typically need to supply a path and file name, while this is still required you can pass in a valid path that does not exist such as "Z:\ShapeFile1.shp". When we need the stream from you we will give you this string so you can find the associated stream. In essence you can use the path and file name as a key to kink to the source of your stream. When we need the file we will raise the StreamLoading event and allow you to pass an alternate stream. In the StreamLoading event we provide you with the path and file name you used and we expect for you to create the stream and set it as the AlternatStream property along with setting the FileMode and File access appropriate to the stream. This helps us know our limits with the stream.*

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


