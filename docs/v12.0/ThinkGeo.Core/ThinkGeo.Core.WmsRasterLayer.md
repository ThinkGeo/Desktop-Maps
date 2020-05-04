# WmsRasterLayer


## Inheritance Hierarchy

+ `Object`
  + [`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)
    + [`RasterLayer`](../ThinkGeo.Core/ThinkGeo.Core.RasterLayer.md)
      + **`WmsRasterLayer`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`WmsRasterLayer()`](#wmsrasterlayer)|
|[`WmsRasterLayer(Uri)`](#wmsrasterlayeruri)|
|[`WmsRasterLayer(Uri,IWebProxy)`](#wmsrasterlayeruriiwebproxy)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ActiveLayerNames`](#activelayernames)|Collection<`String`>|This property represents the available layers that can be requested from the client and shown on the map.|
|[`ActiveStyleNames`](#activestylenames)|Collection<`String`>|This property represents the available styles that can be requested from the client and shown on the map.|
|[`Attribution`](#attribution)|`String`|N/A|
|[`AxisOrder`](#axisorder)|[`WmsAxisOrder`](../ThinkGeo.Core/ThinkGeo.Core.WmsAxisOrder.md)|N/A|
|[`Background`](#background)|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|
|[`BlueTranslation`](#bluetranslation)|`Single`|N/A|
|[`CapabilitesCacheTimeOut`](#capabilitescachetimeout)|`TimeSpan`|N/A|
|[`ColorMappings`](#colormappings)|Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`Credentials`](#credentials)|`ICredentials`|This property gets or sets the base authentication interface for retrieving credentials for Web Client authentication.|
|[`Crs`](#crs)|`String`|This property gets or sets the projected or geographic coordinate reference system to be used.|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingTime`](#drawingtime)|`TimeSpan`|N/A|
|[`Exceptions`](#exceptions)|`String`|This property indicates the format in which the client wishes to be notified of service exceptions.|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`HasBoundingBox`](#hasboundingbox)|`Boolean`|This property checks to see if a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs.|
|[`ImageSource`](#imagesource)|[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)|N/A|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|N/A|
|[`IsNegative`](#isnegative)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsTransparent`](#istransparent)|`Boolean`|This property gets or sets whether the response map image's background color is transparent or not.|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`LowerThreshold`](#lowerthreshold)|`Double`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OutputFormat`](#outputformat)|`String`|This property gets or sets the desired output format for the map requested from the WMS.|
|[`Parameters`](#parameters)|Dictionary<`String`,`String`>|This property specifies a dictionary used to update the request sent from the client to the WMS server.|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`Proxy`](#proxy)|`IWebProxy`|This property gets or sets the proxy used for requesting a Web Response.|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`RequestDrawingInterval`](#requestdrawinginterval)|`TimeSpan`|N/A|
|[`ScaleFactor`](#scalefactor)|`Double`|N/A|
|[`ThreadSafe`](#threadsafe)|[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)|N/A|
|[`TimeoutInSecond`](#timeoutinsecond)|`Int32`|This property specifies the timeout of the web request in seconds.  The default timeout value is 20 seconds.|
|[`Transparency`](#transparency)|`Single`|N/A|
|[`UpperThreshold`](#upperthreshold)|`Double`|N/A|
|[`Uri`](#uri)|`Uri`|This property specifies the URI of the WMS server.|
|[`WrappingExtent`](#wrappingextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`WrappingMode`](#wrappingmode)|[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsOpenCore`](#isopencore)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Close()`](#close)|
|[`Draw(GeoCanvas,Collection<SimpleCandidate>)`](#drawgeocanvascollection<simplecandidate>)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetFeatureInfo(ScreenPointF)`](#getfeatureinfoscreenpointf)|
|[`GetFeatureInfo(ScreenPointF,String)`](#getfeatureinfoscreenpointfstring)|
|[`GetFeatureInfo(ScreenPointF,Int32)`](#getfeatureinfoscreenpointfint32)|
|[`GetFeatureInfo(ScreenPointF,String,Int32)`](#getfeatureinfoscreenpointfstringint32)|
|[`GetHashCode()`](#gethashcode)|
|[`GetHorizontalResolution()`](#gethorizontalresolution)|
|[`GetRequestUrl(RectangleShape,Int32,Int32)`](#getrequesturlrectangleshapeint32int32)|
|[`GetServerCapabilitiesXml()`](#getservercapabilitiesxml)|
|[`GetServerCrss()`](#getservercrss)|
|[`GetServerExceptionFormats()`](#getserverexceptionformats)|
|[`GetServerFeatureInfoFormats()`](#getserverfeatureinfoformats)|
|[`GetServerLayerNames()`](#getserverlayernames)|
|[`GetServerOutputFormats()`](#getserveroutputformats)|
|[`GetServerStyleNames()`](#getserverstylenames)|
|[`GetServiceVersion()`](#getserviceversion)|
|[`GetType()`](#gettype)|
|[`GetVerticalResolution()`](#getverticalresolution)|
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
|[`GetRequestUrlCore(RectangleShape,Int32,Int32)`](#getrequesturlcorerectangleshapeint32int32)|
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
|[`SendingWebRequest`](#sendingwebrequest)|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|
|[`SentWebRequest`](#sentwebrequest)|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|
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
|[`WmsRasterLayer()`](#wmsrasterlayer)|
|[`WmsRasterLayer(Uri)`](#wmsrasterlayeruri)|
|[`WmsRasterLayer(Uri,IWebProxy)`](#wmsrasterlayeruriiwebproxy)|

### Protected Constructors


### Public Properties

#### `ActiveLayerNames`

**Summary**

   *This property represents the available layers that can be requested from the client and shown on the map.*

**Remarks**

   *When requesting a map, a client may specify the layers to be shown on the map.*

**Return Value**

Collection<`String`>

---
#### `ActiveStyleNames`

**Summary**

   *This property represents the available styles that can be requested from the client and shown on the map.*

**Remarks**

   *When requesting a map, a client may specify the styles to be shown on the map.*

**Return Value**

Collection<`String`>

---
#### `Attribution`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `AxisOrder`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`WmsAxisOrder`](../ThinkGeo.Core/ThinkGeo.Core.WmsAxisOrder.md)

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
#### `CapabilitesCacheTimeOut`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `ColorMappings`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>

---
#### `Credentials`

**Summary**

   *This property gets or sets the base authentication interface for retrieving credentials for Web Client authentication.*

**Remarks**

   *N/A*

**Return Value**

`ICredentials`

---
#### `Crs`

**Summary**

   *This property gets or sets the projected or geographic coordinate reference system to be used.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `DrawingExceptionMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)

---
#### `DrawingTime`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

---
#### `Exceptions`

**Summary**

   *This property indicates the format in which the client wishes to be notified of service exceptions.*

**Remarks**

   *Upon receiving a request that is invalid according to the OGC standard, the server shall issue a service exception report. The service report is meant to describe to the client application or its human user the reason(s) that the request is invalid.*

**Return Value**

`String`

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

   *This property checks to see if a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs.*

**Remarks**

   *The override in the WmsRasterLayer sets it to true.*

**Return Value**

`Boolean`

---
#### `ImageSource`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)

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
#### `IsTransparent`

**Summary**

   *This property gets or sets whether the response map image's background color is transparent or not.*

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
#### `LowerThreshold`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `OutputFormat`

**Summary**

   *This property gets or sets the desired output format for the map requested from the WMS.*

**Remarks**

   *When requesting a map, a client may specify the output format in which to show the map. Format are specified as MIME types such as "image/gif" or "image/png".*

**Return Value**

`String`

---
#### `Parameters`

**Summary**

   *This property specifies a dictionary used to update the request sent from the client to the WMS server.*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,`String`>

---
#### `Projection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)

---
#### `Proxy`

**Summary**

   *This property gets or sets the proxy used for requesting a Web Response.*

**Remarks**

   *N/A*

**Return Value**

`IWebProxy`

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
#### `ScaleFactor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `ThreadSafe`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)

---
#### `TimeoutInSecond`

**Summary**

   *This property specifies the timeout of the web request in seconds.  The default timeout value is 20 seconds.*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Transparency`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `UpperThreshold`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Uri`

**Summary**

   *This property specifies the URI of the WMS server.*

**Remarks**

   *N/A*

**Return Value**

`Uri`

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)

---

### Protected Properties

#### `IsOpenCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

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
#### `GetFeatureInfo(ScreenPointF)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPointF|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|

---
#### `GetFeatureInfo(ScreenPointF,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPointF|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|infoFormat|`String`|N/A|

---
#### `GetFeatureInfo(ScreenPointF,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPointF|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|maxFeatures|`Int32`|N/A|

---
#### `GetFeatureInfo(ScreenPointF,String,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Dictionary<`String`,Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPointF|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|infoFormat|`String`|N/A|
|maxFeatures|`Int32`|N/A|

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
#### `GetHorizontalResolution()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Single`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetRequestUrl(RectangleShape,Int32,Int32)`

**Summary**

   *Get the request URL from the client to the WMS.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|The request URL from the client to the WMS.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Int32`|The returning map width, as well as the drawing view width.|
|canvasHeight|`Int32`|The returning map height, as well as the drawing view height.|

---
#### `GetServerCapabilitiesXml()`

**Summary**

   *This API gets the GetCapabilites document of the service.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|This API returns the GetCapabilites document of the service.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetServerCrss()`

**Summary**

   *This method returns the projected or geographic coordinate reference systems to be used.*

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
#### `GetServerExceptionFormats()`

**Summary**

   *This method returns the exception format at the server side.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns the exception format at the server side.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetServerFeatureInfoFormats()`

**Summary**

   *This API gets the WMS server FeatureInfo formats of the service.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This API returns the FeatureInfo formats supported on the server-side.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetServerLayerNames()`

**Summary**

   *This method returns the names of all layers at the server side.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns the names of all layers at the server side.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetServerOutputFormats()`

**Summary**

   *This method returns the output format at the server side.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns the output format at the server side.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetServerStyleNames()`

**Summary**

   *This method returns the names of all styles at the server side.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns the names of all styles at the server side.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetServiceVersion()`

**Summary**

   *This API gets the WMS server version of the service.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|Returning a string reflecting the version of the service in WMS.|

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
#### `GetVerticalResolution()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Single`|N/A|

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
#### `GetRequestUrlCore(RectangleShape,Int32,Int32)`

**Summary**

   *Get the request URL from the client to the WMS.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|The request URL from the client to the WMS.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Int32`|The returning map width, as well as the drawing view width.|
|canvasHeight|`Int32`|The returning map height, as well as the drawing view height.|

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

### Public Events

#### SendingWebRequest

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)

#### SentWebRequest

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)

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


