# FeatureLayer


## Inheritance Hierarchy

+ `Object`
  + [`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)
    + **`FeatureLayer`**
      + [`InMemoryFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryFeatureLayer.md)
      + [`DelimitedFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.DelimitedFeatureLayer.md)
      + [`GpxFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.GpxFeatureLayer.md)
      + [`GraticuleFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.GraticuleFeatureLayer.md)
      + [`GridFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.GridFeatureLayer.md)
      + [`InMemoryGridFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.InMemoryGridFeatureLayer.md)
      + [`MultipleFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.MultipleFeatureLayer.md)
      + [`NoaaWeatherStationFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.NoaaWeatherStationFeatureLayer.md)
      + [`NoaaWeatherWarningsFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.NoaaWeatherWarningsFeatureLayer.md)
      + [`OsmBuildingOnlineFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.OsmBuildingOnlineFeatureLayer.md)
      + [`MultipleShapeFileFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.MultipleShapeFileFeatureLayer.md)
      + [`ShapeFileFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.ShapeFileFeatureLayer.md)
      + [`SqliteFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.SqliteFeatureLayer.md)
      + [`TabFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.TabFeatureLayer.md)
      + [`TinyGeoFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.TinyGeoFeatureLayer.md)
      + [`TobinBasFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.TobinBasFeatureLayer.md)
      + [`UsgsDemFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.UsgsDemFeatureLayer.md)
      + [`WfsFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.WfsFeatureLayer.md)
      + [`WkbFileFeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.WkbFileFeatureLayer.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`FeatureLayer()`](#featurelayer)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`Background`](#background)|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|
|[`BlueTranslation`](#bluetranslation)|`Single`|N/A|
|[`ColorMappings`](#colormappings)|Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingMarginInPixel`](#drawingmargininpixel)|`Single`|N/A|
|[`DrawingQuality`](#drawingquality)|[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)|This property gets and sets the general drawing quality for the FeatureLayer's view.|
|[`DrawingTime`](#drawingtime)|`TimeSpan`|N/A|
|[`EditTools`](#edittools)|[`EditTools`](../ThinkGeo.Core/ThinkGeo.Core.EditTools.md)|This property gets the EditTools that allow you to easily edit InternalFeatures in the Feature Layer.|
|[`FeatureIdsToExclude`](#featureidstoexclude)|Collection<`String`>|A collection of strings representing record id of features not to get in the Layer.|
|[`FeatureSource`](#featuresource)|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|This property gets the FeatureSource for the FeatureLayer.|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`HasBoundingBox`](#hasboundingbox)|`Boolean`|N/A|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|N/A|
|[`IsNegative`](#isnegative)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`QueryTools`](#querytools)|[`QueryTools`](../ThinkGeo.Core/ThinkGeo.Core.QueryTools.md)|This property gets the QueryTools that allow you to easily query Features from the Feature Layer.|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`RequestDrawingInterval`](#requestdrawinginterval)|`TimeSpan`|N/A|
|[`ThreadSafe`](#threadsafe)|[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)|N/A|
|[`Transparency`](#transparency)|`Single`|N/A|
|[`WrappingExtent`](#wrappingextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`WrappingMode`](#wrappingmode)|[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)|Thie property gets or sets whether allow wrap date line.|
|[`ZoomLevelSet`](#zoomlevelset)|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|This property gets and sets the ZoomLevelSet, which contains the specific zoom levels for the FeatureLayer.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FetchedCount`](#fetchedcount)|`Int64`|N/A|
|[`FetchTime`](#fetchtime)|`TimeSpan`|N/A|
|[`IsOpenCore`](#isopencore)|`Boolean`|This property returns true if the FeatureLayer is open and false if it is not.|
|[`StyleTime`](#styletime)|`TimeSpan`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Close()`](#close)|
|[`Draw(GeoCanvas,Collection<SimpleCandidate>)`](#drawgeocanvascollection<simplecandidate>)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`Open()`](#open)|
|[`RequestDrawing()`](#requestdrawing)|
|[`RequestDrawing(RectangleShape)`](#requestdrawingrectangleshape)|
|[`RequestDrawing(IEnumerable<RectangleShape>)`](#requestdrawingienumerable<rectangleshape>)|
|[`RequestDrawing(TimeSpan)`](#requestdrawingtimespan)|
|[`RequestDrawing(TimeSpan,RequestDrawingBufferTimeType)`](#requestdrawingtimespanrequestdrawingbuffertimetype)|
|[`RequestDrawing(RectangleShape,TimeSpan)`](#requestdrawingrectangleshapetimespan)|
|[`RequestDrawing(RectangleShape,TimeSpan,RequestDrawingBufferTimeType)`](#requestdrawingrectangleshapetimespanrequestdrawingbuffertimetype)|
|[`RequestDrawing(IEnumerable<RectangleShape>,TimeSpan)`](#requestdrawingienumerable<rectangleshape>timespan)|
|[`RequestDrawing(IEnumerable<RectangleShape>,TimeSpan,RequestDrawingBufferTimeType)`](#requestdrawingienumerable<rectangleshape>timespanrequestdrawingbuffertimetype)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`CloseCore()`](#closecore)|
|[`DrawAttributionCore(GeoCanvas,String)`](#drawattributioncoregeocanvasstring)|
|[`DrawCore(GeoCanvas,Collection<SimpleCandidate>)`](#drawcoregeocanvascollection<simplecandidate>)|
|[`DrawException(GeoCanvas,Exception)`](#drawexceptiongeocanvasexception)|
|[`DrawExceptionCore(GeoCanvas,Exception)`](#drawexceptioncoregeocanvasexception)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetWrappingFeaturesForDrawing(WrappingWorldDirection,RectangleShape,Double,Double,IEnumerable<String>,RectangleShape)`](#getwrappingfeaturesfordrawingwrappingworlddirectionrectangleshapedoubledoubleienumerable<string>rectangleshape)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnDrawingAttribution(DrawingAttributionLayerEventArgs)`](#ondrawingattributiondrawingattributionlayereventargs)|
|[`OnDrawingException(DrawingExceptionLayerEventArgs)`](#ondrawingexceptiondrawingexceptionlayereventargs)|
|[`OnDrawingFeatures(DrawingFeaturesEventArgs)`](#ondrawingfeaturesdrawingfeatureseventargs)|
|[`OnDrawingProgressChanged(DrawingProgressChangedEventArgs)`](#ondrawingprogresschangeddrawingprogresschangedeventargs)|
|[`OnDrawingWrappingFeatures(DrawingWrappingFeaturesFeatureLayerEventArgs)`](#ondrawingwrappingfeaturesdrawingwrappingfeaturesfeaturelayereventargs)|
|[`OnDrawnAttribution(DrawnAttributionLayerEventArgs)`](#ondrawnattributiondrawnattributionlayereventargs)|
|[`OnDrawnException(DrawnExceptionLayerEventArgs)`](#ondrawnexceptiondrawnexceptionlayereventargs)|
|[`OnRequestedDrawing(RequestedDrawingLayerEventArgs)`](#onrequesteddrawingrequesteddrawinglayereventargs)|
|[`OnRequestingDrawing(RequestingDrawingLayerEventArgs)`](#onrequestingdrawingrequestingdrawinglayereventargs)|
|[`OpenCore()`](#opencore)|
|[`SetupTools()`](#setuptools)|
|[`SetupToolsCore()`](#setuptoolscore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`DrawingFeatures`](#drawingfeatures)|[`DrawingFeaturesEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFeaturesEventArgs.md)|N/A|
|[`DrawingWrappingFeatures`](#drawingwrappingfeatures)|[`DrawingWrappingFeaturesFeatureLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingWrappingFeaturesFeatureLayerEventArgs.md)|N/A|
|[`DrawingProgressChanged`](#drawingprogresschanged)|[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)|N/A|
|[`DrawingException`](#drawingexception)|[`DrawingExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionLayerEventArgs.md)|N/A|
|[`DrawnException`](#drawnexception)|[`DrawnExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnExceptionLayerEventArgs.md)|N/A|
|[`DrawingAttribution`](#drawingattribution)|[`DrawingAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAttributionLayerEventArgs.md)|N/A|
|[`DrawnAttribution`](#drawnattribution)|[`DrawnAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAttributionLayerEventArgs.md)|N/A|
|[`RequestedDrawing`](#requesteddrawing)|[`RequestedDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDrawingLayerEventArgs.md)|N/A|
|[`RequestingDrawing`](#requestingdrawing)|[`RequestingDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDrawingLayerEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|N/A|

### Protected Constructors

#### `FeatureLayer()`

**Summary**

   *This is a constructor for this class.*

**Remarks**

   *This is a constructor for this class.*

**Return Value**

|Type|Description|
|---|---|
|N/A||

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Properties

#### `Attribution`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Background`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)

---
#### `BlueTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `ColorMappings`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>

---
#### `DrawingExceptionMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)

---
#### `DrawingMarginInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `DrawingQuality`

**Summary**

   *This property gets and sets the general drawing quality for the FeatureLayer's view.*

**Remarks**

   *The DrawingQuality enumeration allows you to control, in a macro sense, the drawing quality that will be used in the GeoCanvas. Each GeoCanvas, which is responsible for drawing of the features, may have its own specialized drawing quality properties. What the DrawingQuality enumeration does is define some general guidelines for each GeoCanvas. For example, if you set the DrawingQuality to HighSpeed, then inside of the PlatformGeoCanvas there is a profile for HighSpeed. This profile sets specific properties, such as the smoothing mode and composing drawing mode of the PlatformGeoCanvas. As each GeoCanvas may have different drawing quality properties, this offers a general way to control drawing quality and speed. If you need complete control over how a specific GeoCanvas will draw, then you can set the DrawingQuality to Custom. This will tell the specific GeoCanvas to use the properties on its own object instead of one of the pre-defined profiles. If one of the profiles -- such as HighSpeed or HighQuality -- is set, then the specific GeoCanvas ignores its own properties for drawing quality.*

**Return Value**

[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)

---
#### `DrawingTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `EditTools`

**Summary**

   *This property gets the EditTools that allow you to easily edit InternalFeatures in the Feature Layer.*

**Remarks**

   *The EditTools are supplied as an easily accessible wrapper for the editing methods of the FeatureSource.*

**Return Value**

[`EditTools`](../ThinkGeo.Core/ThinkGeo.Core.EditTools.md)

---
#### `FeatureIdsToExclude`

**Summary**

   *A collection of strings representing record id of features not to get in the Layer.*

**Remarks**

   *This string collection is a handy place to specify what records not to get from the source. Suppose you have a shape file of roads and you want to hide the roads within a particular rectangle, simply execute GetFeaturesInsideBoundingBox() and add the id of the return features to the collection and forget about them. Since you can set this by Layer it makes is easy to determine what to and what not to.*

**Return Value**

Collection<`String`>

---
#### `FeatureSource`

**Summary**

   *This property gets the FeatureSource for the FeatureLayer.*

**Remarks**

   *The FeatureSource is the provider of data to the FeatureLayer. There are different FeatureSource classes to match the various ways that feature data is stored. It is important that, when you inherit from the FeatureLayer, in the constructor you set the FeatureSource you want to use.*

**Return Value**

[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)

---
#### `GreenTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `HasBoundingBox`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsGrayscale`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsNegative`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsOpen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsVisible`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `KeyColors`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>

---
#### `MaxRecordsToDraw`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Projection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `QueryTools`

**Summary**

   *This property gets the QueryTools that allow you to easily query Features from the Feature Layer.*

**Remarks**

   *This property gets the QueryTools that allow you to easily query Features from the Feature Layer. The QueryTools are supplied as an easily accessible wrapper for the query methods of the FeatureSource.*

**Return Value**

[`QueryTools`](../ThinkGeo.Core/ThinkGeo.Core.QueryTools.md)

---
#### `RedTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `RequestDrawingInterval`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `ThreadSafe`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)

---
#### `Transparency`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `WrappingExtent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `WrappingMode`

**Summary**

   *Thie property gets or sets whether allow wrap date line.*

**Remarks**

   *N/A*

**Return Value**

[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)

---
#### `ZoomLevelSet`

**Summary**

   *This property gets and sets the ZoomLevelSet, which contains the specific zoom levels for the FeatureLayer.*

**Remarks**

   *The ZoomLevelSet is a class that contains all of the ZoomLevels for the FeatureLayer. Each ZoomLevel contains the styles that are used to determine how to draw the InternalFeatures.*

**Return Value**

[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)

---

### Protected Properties

#### `FetchedCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int64`

---
#### `FetchTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `IsOpenCore`

**Summary**

   *This property returns true if the FeatureLayer is open and false if it is not.*

**Remarks**

   *Various methods on the FeatureLayer require that it be in an open state. If one of those methods is called when the state is not open, then the method will throw an exception. To enter the open state, you must call the FeatureLayer Open method. The method will raise an exception if the current FeatureLayer is already open.*

**Return Value**

`Boolean`

---
#### `StyleTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---

### Public Methods

#### `CloneDeep()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

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
#### `Draw(GeoCanvas,Collection<SimpleCandidate>)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

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
#### `GetBoundingBox()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `RequestDrawing()`

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
#### `RequestDrawing(RectangleShape)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `RequestDrawing(IEnumerable<RectangleShape>)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|

---
#### `RequestDrawing(TimeSpan)`

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
|bufferTime|`TimeSpan`|N/A|

---
#### `RequestDrawing(TimeSpan,RequestDrawingBufferTimeType)`

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
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

---
#### `RequestDrawing(RectangleShape,TimeSpan)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|bufferTime|`TimeSpan`|N/A|

---
#### `RequestDrawing(RectangleShape,TimeSpan,RequestDrawingBufferTimeType)`

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
|extentToRefresh|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

---
#### `RequestDrawing(IEnumerable<RectangleShape>,TimeSpan)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|bufferTime|`TimeSpan`|N/A|

---
#### `RequestDrawing(IEnumerable<RectangleShape>,TimeSpan,RequestDrawingBufferTimeType)`

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
|extentsToRefresh|IEnumerable<[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)>|N/A|
|bufferTime|`TimeSpan`|N/A|
|bufferTimeType|[`RequestDrawingBufferTimeType`](../ThinkGeo.Core/ThinkGeo.Core.RequestDrawingBufferTimeType.md)|N/A|

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

#### `CloneDeepCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloseCore()`

**Summary**

   *This method closes the FeatureSource and releases any resources it was using.*

**Remarks**

   *This protected virtual method is called from the concrete public method Close. The close method plays an important role in the life cycle of the FeatureLayer. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override this method, it is recommended that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed FeatureLayer is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DrawAttributionCore(GeoCanvas,String)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|attribution|`String`|N/A|

---
#### `DrawCore(GeoCanvas,Collection<SimpleCandidate>)`

**Summary**

   *This method will draw the FeatureLayer source based on the parameters provided.*

**Remarks**

   *The DrawCore will be called when the layer is being drawn. It will check if the InternalFeatures are within the extent and whether it is within a defined zoom level. If these parameters are met, then it will apply the styles of the proper zoom level to the InternalFeatures for drawing. Lastly, it will draw InternalFeatures on the NativeImage or native image passed in to the method.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the GeoCanvas used to Draw the layer.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter is not used for ImageLayers.|

---
#### `DrawException(GeoCanvas,Exception)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|e|`Exception`|N/A|

---
#### `DrawExceptionCore(GeoCanvas,Exception)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|e|`Exception`|N/A|

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
#### `GetBoundingBoxCore()`

**Summary**

   *This method returns the bounding box of the FeatureLayer.*

**Remarks**

   *This method is called from the concrete public method GetBoundingBox. It returns the bounding box of the FeatureLayer.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box of the FeatureLayer.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWrappingFeaturesForDrawing(WrappingWorldDirection,RectangleShape,Double,Double,IEnumerable<String>,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|wrappingFeatureDirection|[`WrappingWorldDirection`](../ThinkGeo.Core/ThinkGeo.Core.WrappingWorldDirection.md)|N/A|
|drawingWorldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Double`|N/A|
|canvasHeight|`Double`|N/A|
|returningColumnNames|IEnumerable<`String`>|N/A|
|wrappingWorldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

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
#### `OnDrawingAttribution(DrawingAttributionLayerEventArgs)`

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
|args|[`DrawingAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAttributionLayerEventArgs.md)|N/A|

---
#### `OnDrawingException(DrawingExceptionLayerEventArgs)`

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
|e|[`DrawingExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionLayerEventArgs.md)|N/A|

---
#### `OnDrawingFeatures(DrawingFeaturesEventArgs)`

**Summary**

   *This method raises the DrawingFeatures event.*

**Remarks**

   *You can call this method from a derived class to enable it to raise the event. This may be useful if you plan to extend the FeatureLayer and you need access to the event. This event is meant to allow you to add or remove the features to be drawn.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`DrawingFeaturesEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFeaturesEventArgs.md)|This parameter represents the event arguments for the event.|

---
#### `OnDrawingProgressChanged(DrawingProgressChangedEventArgs)`

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
|e|[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)|N/A|

---
#### `OnDrawingWrappingFeatures(DrawingWrappingFeaturesFeatureLayerEventArgs)`

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
|e|[`DrawingWrappingFeaturesFeatureLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingWrappingFeaturesFeatureLayerEventArgs.md)|N/A|

---
#### `OnDrawnAttribution(DrawnAttributionLayerEventArgs)`

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
|args|[`DrawnAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAttributionLayerEventArgs.md)|N/A|

---
#### `OnDrawnException(DrawnExceptionLayerEventArgs)`

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
|e|[`DrawnExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnExceptionLayerEventArgs.md)|N/A|

---
#### `OnRequestedDrawing(RequestedDrawingLayerEventArgs)`

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
|eventArgs|[`RequestedDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDrawingLayerEventArgs.md)|N/A|

---
#### `OnRequestingDrawing(RequestingDrawingLayerEventArgs)`

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
|eventArgs|[`RequestingDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDrawingLayerEventArgs.md)|N/A|

---
#### `OpenCore()`

**Summary**

   *This method opens the FeatureLayer so that it is initialized and ready to use.*

**Remarks**

   *This abstract method is called from the concrete public method Open. The Open method plays an important role, as it is responsible for initializing the FeatureLayer. Most methods on the FeatureLayer will throw an exception if the state of the FeatureLayer is not opened. When the map draws each FeatureLayer, it will open the layer as one of its first steps, then after it is finished drawing with that FeatureLayer it will close it. In this way we are sure to release all resources used by the FeatureLayer.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `SetupTools()`

**Summary**

   *This method sets up the EditTools and QueryTools objects.*

**Remarks**

   *This method is the concrete wrapper for the abstract method SetupToolsCore. The SetupTools method allows you to create the QueryTool, EditTools and other tools you may need on your object. We created this method so that if you want to extend one of the tool classes, you can override the SetupToolsCore and create any class you want. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `SetupToolsCore()`

**Summary**

   *This method sets up the EditTools and QueryTools objects.*

**Remarks**

   *This method is the concrete wrapper for the abstract method SetupTools. The SetupTools method allows you to create the QueryTool, EditTools and other tools you may need on your object. We created this method so that if you want to extend one of the tool classes, you can override the SetupToolsCore and create any class you want.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Events

#### DrawingFeatures

*N/A*

**Remarks**

*This event is raised when features are about to be drawn in the layer. In the event arguments, there is a collection of features to be drawn. You can easily add or remove items from this collection so that extra items will draw or not draw.*

**Event Arguments**

[`DrawingFeaturesEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFeaturesEventArgs.md)

#### DrawingWrappingFeatures

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingWrappingFeaturesFeatureLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingWrappingFeaturesFeatureLayerEventArgs.md)

#### DrawingProgressChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)

#### DrawingException

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionLayerEventArgs.md)

#### DrawnException

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnExceptionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnExceptionLayerEventArgs.md)

#### DrawingAttribution

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAttributionLayerEventArgs.md)

#### DrawnAttribution

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnAttributionLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAttributionLayerEventArgs.md)

#### RequestedDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RequestedDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestedDrawingLayerEventArgs.md)

#### RequestingDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`RequestingDrawingLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.RequestingDrawingLayerEventArgs.md)


