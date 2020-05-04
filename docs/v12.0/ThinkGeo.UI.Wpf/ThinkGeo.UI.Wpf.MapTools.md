# MapTools


## Inheritance Hierarchy

+ `Object`
  + Collection<[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)>
    + GeoCollection<[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)>
      + **`MapTools`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MapTools()`](#maptools)|
|[`MapTools(MapView)`](#maptoolsmapview)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Count`](#count)|`Int32`|N/A|
|[`Item`](#item)|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|
|[`Item`](#item)|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|
|[`Logo`](#logo)|[`LogoMapTool`](ThinkGeo.UI.Wpf.LogoMapTool.md)|Gets a shortcut of LogoMapTool object.|
|[`MouseCoordinate`](#mousecoordinate)|[`MouseCoordinateMapTool`](ThinkGeo.UI.Wpf.MouseCoordinateMapTool.md)|Gets a shortcut of MouseCoordinate object.|
|[`PanZoomBar`](#panzoombar)|[`PanZoomBarMapTool`](ThinkGeo.UI.Wpf.PanZoomBarMapTool.md)|Gets a shortcut of PanZoomBar object.|
|[`ScaleLine`](#scaleline)|[`ScaleLineMapTool`](ThinkGeo.UI.Wpf.ScaleLineMapTool.md)|Gets a shortcut of ScaleLine object.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Items`](#items)|IList<[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)>|N/A|

### Public Methods Summary


|Name|
|---|
|[`Add(String,MapTool)`](#addstringmaptool)|
|[`Add(MapTool)`](#addmaptool)|
|[`Add(MapTool)`](#addmaptool)|
|[`Clear()`](#clear)|
|[`Contains(String)`](#containsstring)|
|[`Contains(MapTool)`](#containsmaptool)|
|[`CopyTo(MapTool[],Int32)`](#copytomaptool[]int32)|
|[`Equals(Object)`](#equalsobject)|
|[`GetEnumerator()`](#getenumerator)|
|[`GetHashCode()`](#gethashcode)|
|[`GetKeys()`](#getkeys)|
|[`GetType()`](#gettype)|
|[`IndexOf(MapTool)`](#indexofmaptool)|
|[`Insert(Int32,MapTool)`](#insertint32maptool)|
|[`Insert(Int32,String,MapTool)`](#insertint32stringmaptool)|
|[`Insert(Int32,MapTool)`](#insertint32maptool)|
|[`MoveDown(Int32)`](#movedownint32)|
|[`MoveDown(String)`](#movedownstring)|
|[`MoveDown(MapTool)`](#movedownmaptool)|
|[`MoveTo(Int32,Int32)`](#movetoint32int32)|
|[`MoveTo(String,Int32)`](#movetostringint32)|
|[`MoveTo(MapTool,Int32)`](#movetomaptoolint32)|
|[`MoveToBottom(Int32)`](#movetobottomint32)|
|[`MoveToBottom(String)`](#movetobottomstring)|
|[`MoveToBottom(MapTool)`](#movetobottommaptool)|
|[`MoveToTop(Int32)`](#movetotopint32)|
|[`MoveToTop(String)`](#movetotopstring)|
|[`MoveToTop(MapTool)`](#movetotopmaptool)|
|[`MoveUp(Int32)`](#moveupint32)|
|[`MoveUp(String)`](#moveupstring)|
|[`MoveUp(MapTool)`](#moveupmaptool)|
|[`Refresh()`](#refresh)|
|[`Remove(String)`](#removestring)|
|[`Remove(MapTool)`](#removemaptool)|
|[`RemoveAt(Int32)`](#removeatint32)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`ClearItems()`](#clearitems)|
|[`Finalize()`](#finalize)|
|[`InsertItem(Int32,MapTool)`](#insertitemint32maptool)|
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
|[`SetItem(Int32,MapTool)`](#setitemint32maptool)|

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
|[`MapTools()`](#maptools)|
|[`MapTools(MapView)`](#maptoolsmapview)|

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

[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)

---
#### `Item`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)

---
#### `Logo`

**Summary**

   *Gets a shortcut of LogoMapTool object.*

**Remarks**

   *N/A*

**Return Value**

[`LogoMapTool`](ThinkGeo.UI.Wpf.LogoMapTool.md)

---
#### `MouseCoordinate`

**Summary**

   *Gets a shortcut of MouseCoordinate object.*

**Remarks**

   *N/A*

**Return Value**

[`MouseCoordinateMapTool`](ThinkGeo.UI.Wpf.MouseCoordinateMapTool.md)

---
#### `PanZoomBar`

**Summary**

   *Gets a shortcut of PanZoomBar object.*

**Remarks**

   *N/A*

**Return Value**

[`PanZoomBarMapTool`](ThinkGeo.UI.Wpf.PanZoomBarMapTool.md)

---
#### `ScaleLine`

**Summary**

   *Gets a shortcut of ScaleLine object.*

**Remarks**

   *N/A*

**Return Value**

[`ScaleLineMapTool`](ThinkGeo.UI.Wpf.ScaleLineMapTool.md)

---

### Protected Properties

#### `Items`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

IList<[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)>

---

### Public Methods

#### `Add(String,MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `Add(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `Add(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

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
|key|`String`|N/A|

---
#### `Contains(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `CopyTo(MapTool[],Int32)`

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
|array|[`MapTool[]`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|
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
|IEnumerator<[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)>|N/A|

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
#### `IndexOf(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `Insert(Int32,MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `Insert(Int32,String,MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `Insert(Int32,MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `MoveDown(Int32)`

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
#### `MoveDown(String)`

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

---
#### `MoveDown(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `MoveTo(Int32,Int32)`

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
|fromIndex|`Int32`|N/A|
|toIndex|`Int32`|N/A|

---
#### `MoveTo(String,Int32)`

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
|toIndex|`Int32`|N/A|

---
#### `MoveTo(MapTool,Int32)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|
|toIndex|`Int32`|N/A|

---
#### `MoveToBottom(Int32)`

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
#### `MoveToBottom(String)`

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

---
#### `MoveToBottom(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `MoveToTop(Int32)`

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
#### `MoveToTop(String)`

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

---
#### `MoveToTop(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `MoveUp(Int32)`

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
#### `MoveUp(String)`

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

---
#### `MoveUp(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

---
#### `Refresh()`

**Summary**

   *Refreshes all the map tools objects maintaining in the MapTools object.*

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
#### `Remove(String)`

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

---
#### `Remove(MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

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
#### `InsertItem(Int32,MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

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
#### `SetItem(Int32,MapTool)`

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
|item|[`MapTool`](ThinkGeo.UI.Wpf.MapTool.md)|N/A|

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


