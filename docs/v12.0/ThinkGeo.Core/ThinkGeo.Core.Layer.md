# Layer


## Inheritance Hierarchy

+ `Object`
  + **`Layer`**
    + [`AdornmentLayer`](../ThinkGeo.Core/ThinkGeo.Core.AdornmentLayer.md)
    + [`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)
    + [`RasterLayer`](../ThinkGeo.Core/ThinkGeo.Core.RasterLayer.md)
    + [`PrinterLayer`](../ThinkGeo.Core/ThinkGeo.Core.PrinterLayer.md)
    + [`ArcGISServerRestLayer`](../ThinkGeo.Core/ThinkGeo.Core.ArcGISServerRestLayer.md)
    + [`BackgroundLayer`](../ThinkGeo.Core/ThinkGeo.Core.BackgroundLayer.md)
    + [`BingMapsLayer`](../ThinkGeo.Core/ThinkGeo.Core.BingMapsLayer.md)
    + [`GoogleMapsLayer`](../ThinkGeo.Core/ThinkGeo.Core.GoogleMapsLayer.md)
    + [`GroupLayer`](../ThinkGeo.Core/ThinkGeo.Core.GroupLayer.md)
    + [`IsoLineLayer`](../ThinkGeo.Core/ThinkGeo.Core.IsoLineLayer.md)
    + [`MapShapeLayer`](../ThinkGeo.Core/ThinkGeo.Core.MapShapeLayer.md)
    + [`OpenStreetMapLayer`](../ThinkGeo.Core/ThinkGeo.Core.OpenStreetMapLayer.md)
    + [`RestrictionLayer`](../ThinkGeo.Core/ThinkGeo.Core.RestrictionLayer.md)
    + [`ThinkGeoCloudRasterMapsLayer`](../ThinkGeo.Core/ThinkGeo.Core.ThinkGeoCloudRasterMapsLayer.md)
    + [`ThinkGeoCloudVectorMapsLayer`](../ThinkGeo.Core/ThinkGeo.Core.ThinkGeoCloudVectorMapsLayer.md)
    + [`ThinkGeoMBTilesLayer`](../ThinkGeo.Core/ThinkGeo.Core.ThinkGeoMBTilesLayer.md)
    + [`WmtsLayer`](../ThinkGeo.Core/ThinkGeo.Core.WmtsLayer.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`Layer()`](#layer)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`Background`](#background)|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|Gets and sets the background of the layer.|
|[`BlueTranslation`](#bluetranslation)|`Single`|This property gets and sets the amount of blue to apply to the image.|
|[`ColorMappings`](#colormappings)|Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|ColorMapping|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|Gets or sets the DrawExcpetionMode when exception happens.|
|[`DrawingTime`](#drawingtime)|`TimeSpan`|This property gets the last drawing time for the layer.|
|[`GreenTranslation`](#greentranslation)|`Single`|This property gets and sets the amount of green to apply to the image.|
|[`HasBoundingBox`](#hasboundingbox)|`Boolean`|This property indicates whether a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs.|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|This property gets and sets if the image should be converted to grayscale.|
|[`IsNegative`](#isnegative)|`Boolean`|This property gets and sets whether the image should be converted to negative (inverse colors).|
|[`IsOpen`](#isopen)|`Boolean`|This property returns true if the Layer is open and false if it is not.|
|[`IsVisible`](#isvisible)|`Boolean`|This property gets and set the visible state of the layer.|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|Gets a value represents a collection of key colors. If SupportKeyColor property is false, it will throw exception when you use KeyColors.|
|[`Name`](#name)|`String`|This property gets and sets the name for the layer.|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|Gets the projection of the layer.|
|[`RedTranslation`](#redtranslation)|`Single`|This property gets and sets the amount of red to apply to the image.|
|[`RequestDrawingInterval`](#requestdrawinginterval)|`TimeSpan`|N/A|
|[`ThreadSafe`](#threadsafe)|[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)|N/A|
|[`Transparency`](#transparency)|`Single`|This property gets and sets the amount of transparency to apply to the image.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsOpenCore`](#isopencore)|`Boolean`|This property returns true if the Layer is open and false if it is not.|

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
|[`LoadLayer(Uri)`](#loadlayeruri)|
|[`LoadLayer(Stream)`](#loadlayerstream)|
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
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnDrawingAttribution(DrawingAttributionLayerEventArgs)`](#ondrawingattributiondrawingattributionlayereventargs)|
|[`OnDrawingException(DrawingExceptionLayerEventArgs)`](#ondrawingexceptiondrawingexceptionlayereventargs)|
|[`OnDrawingProgressChanged(DrawingProgressChangedEventArgs)`](#ondrawingprogresschangeddrawingprogresschangedeventargs)|
|[`OnDrawnAttribution(DrawnAttributionLayerEventArgs)`](#ondrawnattributiondrawnattributionlayereventargs)|
|[`OnDrawnException(DrawnExceptionLayerEventArgs)`](#ondrawnexceptiondrawnexceptionlayereventargs)|
|[`OnRequestedDrawing(RequestedDrawingLayerEventArgs)`](#onrequesteddrawingrequesteddrawinglayereventargs)|
|[`OnRequestingDrawing(RequestingDrawingLayerEventArgs)`](#onrequestingdrawingrequestingdrawinglayereventargs)|
|[`OpenCore()`](#opencore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
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

#### `Layer()`

**Summary**

   *This is the default constructor for the Layer.*

**Remarks**

   *None*

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

   *Gets and sets the background of the layer.*

**Remarks**

   *N/A*

**Return Value**

[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)

---
#### `BlueTranslation`

**Summary**

   *This property gets and sets the amount of blue to apply to the image.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `ColorMappings`

**Summary**

   *ColorMapping*

**Remarks**

   *N/A*

**Return Value**

Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>

---
#### `DrawingExceptionMode`

**Summary**

   *Gets or sets the DrawExcpetionMode when exception happens.*

**Remarks**

   *N/A*

**Return Value**

[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)

---
#### `DrawingTime`

**Summary**

   *This property gets the last drawing time for the layer.*

**Remarks**

   *We track the drawing time for the layer and report it back in this method. This is useful for determining the speed of various layers.*

**Return Value**

`TimeSpan`

---
#### `GreenTranslation`

**Summary**

   *This property gets and sets the amount of green to apply to the image.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `HasBoundingBox`

**Summary**

   *This property indicates whether a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs.*

**Remarks**

   *The default value is false.*

**Return Value**

`Boolean`

---
#### `IsGrayscale`

**Summary**

   *This property gets and sets if the image should be converted to grayscale.*

**Remarks**

   *None*

**Return Value**

`Boolean`

---
#### `IsNegative`

**Summary**

   *This property gets and sets whether the image should be converted to negative (inverse colors).*

**Remarks**

   *None*

**Return Value**

`Boolean`

---
#### `IsOpen`

**Summary**

   *This property returns true if the Layer is open and false if it is not.*

**Remarks**

   *This method is the concrete wrapper for the abstract method IsOpenCore. Various methods on the Layer require that it be in an open state. If one of those methods is called when the state is not open, then the method will throw an exception. To enter the open state, you must call the Layer Open method. The method will raise an exception if the current Layer is already open. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

`Boolean`

---
#### `IsVisible`

**Summary**

   *This property gets and set the visible state of the layer.*

**Remarks**

   *If this property is set to false, the layer will not draw. We ensure this in the Draw method. This is useful for legends and other controls that control the visibility of layers.*

**Return Value**

`Boolean`

---
#### `KeyColors`

**Summary**

   *Gets a value represents a collection of key colors. If SupportKeyColor property is false, it will throw exception when you use KeyColors.*

**Remarks**

   *It will make these colors transparent when draw image.*

**Return Value**

Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>

---
#### `Name`

**Summary**

   *This property gets and sets the name for the layer.*

**Remarks**

   *The name is user defined. It is useful to set, as it may be used for higher level components such as legends, etc.*

**Return Value**

`String`

---
#### `Projection`

**Summary**

   *Gets the projection of the layer.*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `RedTranslation`

**Summary**

   *This property gets and sets the amount of red to apply to the image.*

**Remarks**

   *None*

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

   *This property gets and sets the amount of transparency to apply to the image.*

**Remarks**

   *None*

**Return Value**

`Single`

---

### Protected Properties

#### `IsOpenCore`

**Summary**

   *This property returns true if the Layer is open and false if it is not.*

**Remarks**

   *Various methods on the Layer require that it be in an open state. If one of those methods is called when the state is not open, then the method will throw an exception. To enter the open state, you must call the Layer Open method. The method will raise an exception if the current Layer is already open.*

**Return Value**

`Boolean`

---

### Public Methods

#### `CloneDeep()`

**Summary**

   *Create a copy of Layer using the deep clone process.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)|A cloned Layer.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Close()`

**Summary**

   *This method closes the Layer and releases any resources it was using.*

**Remarks**

   *This method is the concrete wrapper for the abstract method CloseCore. The close method plays an important role in the life cycle of the Layer. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override this method, it is recommended that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed Layer is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Draw(GeoCanvas,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the Layer.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DrawCore. This method draws the representation of the layer based on the extent you provided. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view object or a NativeImage to draw on.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter represents the labels used for collision detection and duplication checking.|

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

   *This method returns the bounding box of the Layer.*

**Remarks**

   *This method is the concrete wrapper for the abstract method GetBoundingBoxCore. This method returns the bounding box of the RasterLayer. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box of the Layer.|

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
#### `LoadLayer(Uri)`

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
|layerUri|`Uri`|N/A|

---
#### `LoadLayer(Stream)`

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
|layerStream|`Stream`|N/A|

---
#### `Open()`

**Summary**

   *This method opens the Layer so that it is initialized and ready to use.*

**Remarks**

   *This method is the concrete wrapper for the abstract method OpenCore. The open method plays an important role, as it is responsible for initializing the Layer. Most methods on the Layer will throw an exception if the state of the Layer is not opened. When the map draws each layer, the layer will be opened as one of its first steps; then, after it is finished drawing with that layer, it will close it. In this way, we are sure to release all resources used by the Layer. When implementing the abstract method, consider opening the FeatureSource or RasterSource. You will get a chance to close these in the Close method of the Layer. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

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

   *Create a copy of Layer using the deep clone process. The default implemenation uses serialization.*

**Remarks**

   *The difference between deep clone and shallow clone is as follows: In shallow cloning, only the object is copied; the objects within it are not. By contrast, deep cloning copies the cloned object as well as all the objects within.*

**Return Value**

|Type|Description|
|---|---|
|[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)|A cloned Layer.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CloseCore()`

**Summary**

   *This method closes the Layer and releases any resources it was using.*

**Remarks**

   *This abstract method is called from the concrete public method Close. The close method plays an important role in the life cycle of the Layer. It may be called after drawing to release any memory and other resources that were allocated since the Open method was called. If you override this method, it is recommended that you take the following things into account: This method may be called multiple times, so we suggest you write the method so that that a call to a closed Layer is ignored and does not generate an error. We also suggest that in the Close you free all resources that have been opened. Remember that the object will not be destroyed, but will be re-opened possibly in the near future.*

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

   *This method draws the Layer.*

**Remarks**

   *This method is the concrete wrapper for the abstract method DrawCore. This method draws the representation of the layer based on the extent you provided. When implementing this abstract method, consider each feature and its column data values. You can use the full power of the GeoCanvas to do the drawing. If you need column data for a feature, be sure to override the GetRequiredColumnNamesCore and add the columns you need to the collection. In many of the styles, we add properties that allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore, we read that property and add it to the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view object or a NativeImage to draw on.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|This parameter represents the labels used for collision detection and duplication checking.|

---
#### `DrawException(GeoCanvas,Exception)`

**Summary**

   *This method will draw on the view when the layer.Draw throw exception and the DrawExceptionMode is set to DrawException instead of ThrowException.*

**Remarks**

   *This method can be overriden its logic by rewrite the DrawExceptionCore.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|The target view to draw the layer.|
|e|`Exception`|The exception thrown when layer.Draw().|

---
#### `DrawExceptionCore(GeoCanvas,Exception)`

**Summary**

   *This method will draw on the view when the layer.Draw throw exception and the DrawExceptionMode is set to DrawException instead of ThrowException.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|The target view to draw the layer.|
|e|`Exception`|The exception thrown when layer.Draw().|

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

   *This method returns the bounding box of the Layer.*

**Remarks**

   *This method returns the bounding box of the Layer.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns the bounding box of the Layer.|

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

   *This method opens the Layer so that it is initialized and ready to use.*

**Remarks**

   *This abstract method is called from the concrete public method Open. The open method plays an important role, as it is responsible for initializing the Layer. Most methods on the Layer will throw an exception if the state of the Layer is not opened. When the map draws each layer, the layer will be opened as one of its first steps; then, after it is finished drawing with that layer, it will close it. In this way, we are sure to release all resources used by the Layer. When implementing the abstract method, consider opening the FeatureSource or RasterSource. You will get a chance to close these in the Close method of the Layer.*

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


