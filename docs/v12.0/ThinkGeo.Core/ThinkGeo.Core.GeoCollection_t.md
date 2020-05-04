# GeoCollection<T>


## Inheritance Hierarchy

+ `Object`
  + Collection<`T`>
    + **`GeoCollection<T>`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeoCollection`1()`](#geocollection`1)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Count`](#count)|`Int32`|N/A|
|[`Item`](#item)|`T`|N/A|
|[`Item`](#item)|`T`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Items`](#items)|IList<`T`>|N/A|

### Public Methods Summary


|Name|
|---|
|[`Add(String,T)`](#addstringt)|
|[`Add(T)`](#addt)|
|[`Add(T)`](#addt)|
|[`Clear()`](#clear)|
|[`Contains(String)`](#containsstring)|
|[`Contains(T)`](#containst)|
|[`CopyTo(T[],Int32)`](#copytot[]int32)|
|[`Equals(Object)`](#equalsobject)|
|[`GetEnumerator()`](#getenumerator)|
|[`GetHashCode()`](#gethashcode)|
|[`GetKeys()`](#getkeys)|
|[`GetType()`](#gettype)|
|[`IndexOf(T)`](#indexoft)|
|[`Insert(Int32,T)`](#insertint32t)|
|[`Insert(Int32,String,T)`](#insertint32stringt)|
|[`Insert(Int32,T)`](#insertint32t)|
|[`MoveDown(Int32)`](#movedownint32)|
|[`MoveDown(String)`](#movedownstring)|
|[`MoveDown(T)`](#movedownt)|
|[`MoveTo(Int32,Int32)`](#movetoint32int32)|
|[`MoveTo(String,Int32)`](#movetostringint32)|
|[`MoveTo(T,Int32)`](#movetotint32)|
|[`MoveToBottom(Int32)`](#movetobottomint32)|
|[`MoveToBottom(String)`](#movetobottomstring)|
|[`MoveToBottom(T)`](#movetobottomt)|
|[`MoveToTop(Int32)`](#movetotopint32)|
|[`MoveToTop(String)`](#movetotopstring)|
|[`MoveToTop(T)`](#movetotopt)|
|[`MoveUp(Int32)`](#moveupint32)|
|[`MoveUp(String)`](#moveupstring)|
|[`MoveUp(T)`](#moveupt)|
|[`Remove(String)`](#removestring)|
|[`Remove(T)`](#removet)|
|[`RemoveAt(Int32)`](#removeatint32)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`ClearItems()`](#clearitems)|
|[`Finalize()`](#finalize)|
|[`InsertItem(Int32,T)`](#insertitemint32t)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnAdded(AddedGeoCollectionEventArgs)`](#onaddedaddedgeocollectioneventargs)|
|[`OnAdding(AddingGeoCollectionEventArgs)`](#onaddingaddinggeocollectioneventargs)|
|[`OnClearedItems(ClearedItemsGeoCollectionEventArgs)`](#oncleareditemscleareditemsgeocollectioneventargs)|
|[`OnClearingItems(ClearingItemsGeoCollectionEventArgs)`](#onclearingitemsclearingitemsgeocollectioneventargs)|
|[`OnCollectionChanged(NotifyCollectionChangedEventArgs)`](#oncollectionchangednotifycollectionchangedeventargs)|
|[`OnInserted(InsertedGeoCollectionEventArgs)`](#oninsertedinsertedgeocollectioneventargs)|
|[`OnInserting(InsertingGeoCollectionEventArgs)`](#oninsertinginsertinggeocollectioneventargs)|
|[`OnPropertyChanged(PropertyChangedEventArgs)`](#onpropertychangedpropertychangedeventargs)|
|[`OnRemoved(RemovedGeoCollectionEventArgs)`](#onremovedremovedgeocollectioneventargs)|
|[`OnRemoving(RemovingGeoCollectionEventArgs)`](#onremovingremovinggeocollectioneventargs)|
|[`RemoveItem(Int32)`](#removeitemint32)|
|[`SetItem(Int32,T)`](#setitemint32t)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`Inserting`](#inserting)|[`InsertingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.InsertingGeoCollectionEventArgs.md)|N/A|
|[`Inserted`](#inserted)|[`InsertedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.InsertedGeoCollectionEventArgs.md)|N/A|
|[`Removing`](#removing)|[`RemovingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RemovingGeoCollectionEventArgs.md)|N/A|
|[`Removed`](#removed)|[`RemovedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RemovedGeoCollectionEventArgs.md)|N/A|
|[`Adding`](#adding)|[`AddingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.AddingGeoCollectionEventArgs.md)|N/A|
|[`Added`](#added)|[`AddedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.AddedGeoCollectionEventArgs.md)|N/A|
|[`ClearingItems`](#clearingitems)|[`ClearingItemsGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClearingItemsGeoCollectionEventArgs.md)|N/A|
|[`ClearedItems`](#cleareditems)|[`ClearedItemsGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClearedItemsGeoCollectionEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`GeoCollection`1()`](#geocollection`1)|

### Protected Constructors


### Public Properties

#### `Count`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Item`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`T`

---
#### `Item`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`T`

---

### Protected Properties

#### `Items`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

IList<`T`>

---

### Public Methods

#### `Add(String,T)`

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
|key|`String`|N/A|
|item|`T`|N/A|

---
#### `Add(T)`

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
|item|`T`|N/A|

---
#### `Add(T)`

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
|item|`T`|N/A|

---
#### `Clear()`

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
#### `Contains(String)`

**Summary**

   *This method returns whether an item is in the collection based on the specified key.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns whether an item is in the collection based on the specified key.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|This parameter is the key of the item you are searching for.|

---
#### `Contains(T)`

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
|item|`T`|N/A|

---
#### `CopyTo(T[],Int32)`

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
|array|`T[]`|N/A|
|index|`Int32`|N/A|

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
#### `GetEnumerator()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|IEnumerator<`T`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `GetKeys()`

**Summary**

   *This method returns a collection of the keys in the collection.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns a collection of the keys in the collection.|

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
#### `IndexOf(T)`

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
|item|`T`|N/A|

---
#### `Insert(Int32,T)`

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
|index|`Int32`|N/A|
|item|`T`|N/A|

---
#### `Insert(Int32,String,T)`

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
|index|`Int32`|N/A|
|key|`String`|N/A|
|item|`T`|N/A|

---
#### `Insert(Int32,T)`

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
|index|`Int32`|N/A|
|item|`T`|N/A|

---
#### `MoveDown(Int32)`

**Summary**

   *This method moves an item down in the collection.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|This parameter is the index of the item in the collection.|

---
#### `MoveDown(String)`

**Summary**

   *This method moves an item down in the collection.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|This parameter is the key of the item in the dictionary.|

---
#### `MoveDown(T)`

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
|item|`T`|N/A|

---
#### `MoveTo(Int32,Int32)`

**Summary**

   *This method moves the item at fromIndex to the location of toIndex in the collection.*

**Remarks**

   *This method moves the item at fromIndex to the location of toIndex in the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromIndex|`Int32`|This parameter is the index of the item you want move from in the collection.|
|toIndex|`Int32`|This parameter is the target index that you want to move the item to in the collection.|

---
#### `MoveTo(String,Int32)`

**Summary**

   *This method moves the item with the key you specified to the location of toIndex in the collection.*

**Remarks**

   *This method moves the item with the key you specified to the location of toIndex in the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|This parameter is the key of item you want to move in the collection.|
|toIndex|`Int32`|This parameter is the target index that you want to move the item to in the collection.|

---
#### `MoveTo(T,Int32)`

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
|item|`T`|N/A|
|toIndex|`Int32`|N/A|

---
#### `MoveToBottom(Int32)`

**Summary**

   *This method moves the item at the specified index to the bottom of the collection.*

**Remarks**

   *This method moves the item at the specified index to the bottom of the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|This parameter is the index of the item you want move to the bottom of the collection.|

---
#### `MoveToBottom(String)`

**Summary**

   *This method moves the item with the specified key to the bottom of the collection.*

**Remarks**

   *This method moves the item with the specified key to the bottom of the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|This parameter is the key of the item you want move to the bottom of the collection.|

---
#### `MoveToBottom(T)`

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
|item|`T`|N/A|

---
#### `MoveToTop(Int32)`

**Summary**

   *This method moves the item at the specified index to the top of the collection.*

**Remarks**

   *This method moves the item at the specified index to the top of the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|This parameter is the index of the item you want move to the top of the collection.|

---
#### `MoveToTop(String)`

**Summary**

   *This method moves the item with the specified key to the top of the collection.*

**Remarks**

   *This method moves the item with the specified key to the top of the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|This parameter is the key of the item you want move to the top of the collection.|

---
#### `MoveToTop(T)`

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
|item|`T`|N/A|

---
#### `MoveUp(Int32)`

**Summary**

   *This method moves an item up in the collection.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|This parameter is the index of the item in the collection.|

---
#### `MoveUp(String)`

**Summary**

   *This method moves an item up in the collection.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|This parameter is the key of the item in the collection.|

---
#### `MoveUp(T)`

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
|item|`T`|N/A|

---
#### `Remove(String)`

**Summary**

   *This method removes an item from the collection based on the specified key.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|This parameter is the key of the item you want to remove.|

---
#### `Remove(T)`

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
|item|`T`|N/A|

---
#### `RemoveAt(Int32)`

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
|index|`Int32`|N/A|

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

#### `ClearItems()`

**Summary**

   *This method clears the items from the collection.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

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
#### `InsertItem(Int32,T)`

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
|index|`Int32`|N/A|
|item|`T`|N/A|

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
#### `OnAdded(AddedGeoCollectionEventArgs)`

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
|e|[`AddedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.AddedGeoCollectionEventArgs.md)|N/A|

---
#### `OnAdding(AddingGeoCollectionEventArgs)`

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
|e|[`AddingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.AddingGeoCollectionEventArgs.md)|N/A|

---
#### `OnClearedItems(ClearedItemsGeoCollectionEventArgs)`

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
|e|[`ClearedItemsGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClearedItemsGeoCollectionEventArgs.md)|N/A|

---
#### `OnClearingItems(ClearingItemsGeoCollectionEventArgs)`

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
|e|[`ClearingItemsGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClearingItemsGeoCollectionEventArgs.md)|N/A|

---
#### `OnCollectionChanged(NotifyCollectionChangedEventArgs)`

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
|e|`NotifyCollectionChangedEventArgs`|N/A|

---
#### `OnInserted(InsertedGeoCollectionEventArgs)`

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
|e|[`InsertedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.InsertedGeoCollectionEventArgs.md)|N/A|

---
#### `OnInserting(InsertingGeoCollectionEventArgs)`

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
|e|[`InsertingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.InsertingGeoCollectionEventArgs.md)|N/A|

---
#### `OnPropertyChanged(PropertyChangedEventArgs)`

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
|e|`PropertyChangedEventArgs`|N/A|

---
#### `OnRemoved(RemovedGeoCollectionEventArgs)`

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
|e|[`RemovedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RemovedGeoCollectionEventArgs.md)|N/A|

---
#### `OnRemoving(RemovingGeoCollectionEventArgs)`

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
|e|[`RemovingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RemovingGeoCollectionEventArgs.md)|N/A|

---
#### `RemoveItem(Int32)`

**Summary**

   *This method removes an item from the collection based on the specified index.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|This parameter is the index of the item you want to remove.|

---
#### `SetItem(Int32,T)`

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
|index|`Int32`|N/A|
|item|`T`|N/A|

---

### Public Events

#### Inserting

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`InsertingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.InsertingGeoCollectionEventArgs.md)

#### Inserted

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`InsertedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.InsertedGeoCollectionEventArgs.md)

#### Removing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RemovingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RemovingGeoCollectionEventArgs.md)

#### Removed

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RemovedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RemovedGeoCollectionEventArgs.md)

#### Adding

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`AddingGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.AddingGeoCollectionEventArgs.md)

#### Added

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`AddedGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.AddedGeoCollectionEventArgs.md)

#### ClearingItems

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClearingItemsGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClearingItemsGeoCollectionEventArgs.md)

#### ClearedItems

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClearedItemsGeoCollectionEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClearedItemsGeoCollectionEventArgs.md)


