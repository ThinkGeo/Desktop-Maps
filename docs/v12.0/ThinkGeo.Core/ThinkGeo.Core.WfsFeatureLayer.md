# WfsFeatureLayer


## Inheritance Hierarchy

+ `Object`
  + [`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)
    + [`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)
      + **`WfsFeatureLayer`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`WfsFeatureLayer()`](#wfsfeaturelayer)|
|[`WfsFeatureLayer(String,String)`](#wfsfeaturelayerstringstring)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Attribution`](#attribution)|`String`|N/A|
|[`Background`](#background)|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|
|[`BlueTranslation`](#bluetranslation)|`Single`|N/A|
|[`ColorMappings`](#colormappings)|Dictionary<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md),[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`DrawingExceptionMode`](#drawingexceptionmode)|[`DrawingExceptionMode`](../ThinkGeo.Core/ThinkGeo.Core.DrawingExceptionMode.md)|N/A|
|[`DrawingMarginInPixel`](#drawingmargininpixel)|`Single`|N/A|
|[`DrawingQuality`](#drawingquality)|[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)|N/A|
|[`DrawingTime`](#drawingtime)|`TimeSpan`|N/A|
|[`EditTools`](#edittools)|[`EditTools`](../ThinkGeo.Core/ThinkGeo.Core.EditTools.md)|N/A|
|[`FeatureIdsToExclude`](#featureidstoexclude)|Collection<`String`>|N/A|
|[`FeatureSource`](#featuresource)|[`FeatureSource`](../ThinkGeo.Core/ThinkGeo.Core.FeatureSource.md)|N/A|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`HasBoundingBox`](#hasboundingbox)|`Boolean`|This property indicates whether a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs.|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|N/A|
|[`IsNegative`](#isnegative)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`LastXmlResponse`](#lastxmlresponse)|`String`|The xml text represnets last respone, it will pass out by RequestedData event with parameter.|
|[`MaxRecordsToDraw`](#maxrecordstodraw)|`Int32`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`QueryTools`](#querytools)|[`QueryTools`](../ThinkGeo.Core/ThinkGeo.Core.QueryTools.md)|N/A|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`RequestDrawingInterval`](#requestdrawinginterval)|`TimeSpan`|N/A|
|[`ServiceLocationUrl`](#servicelocationurl)|`String`|The url of wfs service.|
|[`ThreadSafe`](#threadsafe)|[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)|N/A|
|[`TimeoutInSeconds`](#timeoutinseconds)|`Int32`|This property specifies the timeout of the web request in seconds.  The default timeout value is 20 seconds.|
|[`Transparency`](#transparency)|`Single`|N/A|
|[`TypeName`](#typename)|`String`|The typename in the specify wfs service.|
|[`WebProxy`](#webproxy)|`IWebProxy`|This property gets or sets the proxy used for requesting a Web Response.|
|[`WrappingExtent`](#wrappingextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`WrappingMode`](#wrappingmode)|[`WrappingMode`](../ThinkGeo.Core/ThinkGeo.Core.WrappingMode.md)|N/A|
|[`ZoomLevelSet`](#zoomlevelset)|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FetchedCount`](#fetchedcount)|`Int64`|N/A|
|[`FetchTime`](#fetchtime)|`TimeSpan`|N/A|
|[`IsOpenCore`](#isopencore)|`Boolean`|N/A|
|[`StyleTime`](#styletime)|`TimeSpan`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Close()`](#close)|
|[`Draw(GeoCanvas,Collection<SimpleCandidate>)`](#drawgeocanvascollection<simplecandidate>)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetCapabilities(String)`](#getcapabilitiesstring)|
|[`GetCapabilities(Uri)`](#getcapabilitiesuri)|
|[`GetCapabilities(Uri,IWebProxy)`](#getcapabilitiesuriiwebproxy)|
|[`GetHashCode()`](#gethashcode)|
|[`GetLayers(String)`](#getlayersstring)|
|[`GetLayers(Uri,IWebProxy)`](#getlayersuriiwebproxy)|
|[`GetLayers(Uri)`](#getlayersuri)|
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
|[`SendingWebRequest`](#sendingwebrequest)|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|
|[`SentWebRequest`](#sentwebrequest)|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|
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
|[`WfsFeatureLayer()`](#wfsfeaturelayer)|
|[`WfsFeatureLayer(String,String)`](#wfsfeaturelayerstringstring)|

### Protected Constructors


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

   *N/A*

**Remarks**

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`EditTools`](../ThinkGeo.Core/ThinkGeo.Core.EditTools.md)

---
#### `FeatureIdsToExclude`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `FeatureSource`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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

   *This property indicates whether a Layer has a BoundingBox or not. If it has no BoundingBox, it will throw an exception when you call the GetBoundingBox() and GetFullExtent() APIs.*

**Remarks**

   *The default value is false.*

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
#### `LastXmlResponse`

**Summary**

   *The xml text represnets last respone, it will pass out by RequestedData event with parameter.*

**Remarks**

   *N/A*

**Return Value**

`String`

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

   *N/A*

**Remarks**

   *N/A*

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
#### `ServiceLocationUrl`

**Summary**

   *The url of wfs service.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `ThreadSafe`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ThreadSafetyLevel`](../ThinkGeo.Core/ThinkGeo.Core.ThreadSafetyLevel.md)

---
#### `TimeoutInSeconds`

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
#### `TypeName`

**Summary**

   *The typename in the specify wfs service.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `WebProxy`

**Summary**

   *This property gets or sets the proxy used for requesting a Web Response.*

**Remarks**

   *N/A*

**Return Value**

`IWebProxy`

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
#### `ZoomLevelSet`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

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
#### `GetCapabilities(String)`

**Summary**

   *Get capabilities from the specific wfs service url.*

**Remarks**

   *Every OGC Web Service (OWS), including a Web Feature Service, must have the ability to describe its capabilities by returning service metadata in response to a GetCapabilities request. Specifically, every web feature service must support the KVP encoded form of the GetCapabilities request over HTTP GET so that a client can always know how to obtain a capabilities document. The capabilities response document contains the following sections: 1. Service Identification section The service identification section provides information about the WFS service itself. 2. Service Provider section The service provider section provides metadata about the organization operating the WFS server. 3. Operation Metadata section The operations metadata section provides metadata about the operations defined in this specification and implemented by a particular WFS server. This metadata includes the DCP, parameters and constraints for each operation. 4. FeatureType list section This section defines the list of feature types (and operations on each feature type) that are available from a web feature service. Additional information, such as the default SRS, any other supported SRSs, or no SRS whatsoever (for non-spatial feature types), for WFS requests is provided for each feature type. 5. ServesGMLObjectType list section This section defines the list of GML Object types, not derived from gml:AbstractFeatureType, that are available from a web feature service that supports the GetGMLObject operation. These types may be defined in a base GML schema, or in an application schema using its own namespace. 6. SupportsGMLObjectType list section The Supports GML Object Type section defines the list of GML Object types that a WFS server would be capable of serving if it was deployed to serve data. described by an application schema that either used those GML Object types directly (for non-abstract types), or defined derived types based on those types. 7. Filter capabilities section The schema of the Filter Capabilities Section is defined in the Filter Encoding Implementation Specification [3]. This is an optional section. If it exists, then the WFS should support the operations advertised therein. If the Filter Capabilities Section is not defined, then the client should assume that the server only supports the minimum default set of filter operators.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The xml text represents capabilities of this wfs server.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serviceLocationUrl|`String`|The url of wfs service.|

---
#### `GetCapabilities(Uri)`

**Summary**

   *Get capabilities from the specific wfs service url.*

**Remarks**

   *Every OGC Web Service (OWS), including a Web Feature Service, must have the ability to describe its capabilities by returning service metadata in response to a GetCapabilities request. Specifically, every web feature service must support the KVP encoded form of the GetCapabilities request over HTTP GET so that a client can always know how to obtain a capabilities document. The capabilities response document contains the following sections: 1. Service Identification section The service identification section provides information about the WFS service itself. 2. Service Provider section The service provider section provides metadata about the organization operating the WFS server. 3. Operation Metadata section The operations metadata section provides metadata about the operations defined in this specification and implemented by a particular WFS server. This metadata includes the DCP, parameters and constraints for each operation. 4. FeatureType list section This section defines the list of feature types (and operations on each feature type) that are available from a web feature service. Additional information, such as the default SRS, any other supported SRSs, or no SRS whatsoever (for non-spatial feature types), for WFS requests is provided for each feature type. 5. ServesGMLObjectType list section This section defines the list of GML Object types, not derived from gml:AbstractFeatureType, that are available from a web feature service that supports the GetGMLObject operation. These types may be defined in a base GML schema, or in an application schema using its own namespace. 6. SupportsGMLObjectType list section The Supports GML Object Type section defines the list of GML Object types that a WFS server would be capable of serving if it was deployed to serve data. described by an application schema that either used those GML Object types directly (for non-abstract types), or defined derived types based on those types. 7. Filter capabilities section The schema of the Filter Capabilities Section is defined in the Filter Encoding Implementation Specification [3]. This is an optional section. If it exists, then the WFS should support the operations advertised therein. If the Filter Capabilities Section is not defined, then the client should assume that the server only supports the minimum default set of filter operators.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The xml text represents capabilities of this wfs server.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|

---
#### `GetCapabilities(Uri,IWebProxy)`

**Summary**

   *Get capabilities from the specific wfs service url.*

**Remarks**

   *Every OGC Web Service (OWS), including a Web Feature Service, must have the ability to describe its capabilities by returning service metadata in response to a GetCapabilities request. Specifically, every web feature service must support the KVP encoded form of the GetCapabilities request over HTTP GET so that a client can always know how to obtain a capabilities document. The capabilities response document contains the following sections: 1. Service Identification section The service identification section provides information about the WFS service itself. 2. Service Provider section The service provider section provides metadata about the organization operating the WFS server. 3. Operation Metadata section The operations metadata section provides metadata about the operations defined in this specification and implemented by a particular WFS server. This metadata includes the DCP, parameters and constraints for each operation. 4. FeatureType list section This section defines the list of feature types (and operations on each feature type) that are available from a web feature service. Additional information, such as the default SRS, any other supported SRSs, or no SRS whatsoever (for non-spatial feature types), for WFS requests is provided for each feature type. 5. ServesGMLObjectType list section This section defines the list of GML Object types, not derived from gml:AbstractFeatureType, that are available from a web feature service that supports the GetGMLObject operation. These types may be defined in a base GML schema, or in an application schema using its own namespace. 6. SupportsGMLObjectType list section The Supports GML Object Type section defines the list of GML Object types that a WFS server would be capable of serving if it was deployed to serve data. described by an application schema that either used those GML Object types directly (for non-abstract types), or defined derived types based on those types. 7. Filter capabilities section The schema of the Filter Capabilities Section is defined in the Filter Encoding Implementation Specification [3]. This is an optional section. If it exists, then the WFS should support the operations advertised therein. If the Filter Capabilities Section is not defined, then the client should assume that the server only supports the minimum default set of filter operators.*

**Return Value**

|Type|Description|
|---|---|
|`String`|The xml text represents capabilities of this wfs server.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|
|webProxy|`IWebProxy`|The proxy of the wfs service.|

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
#### `GetLayers(String)`

**Summary**

   *Get layer names from specific wfs service url.*

**Remarks**

   *Typically, it will call WfsFeatureSource.GetLayers(serviceLocationUrl) internally.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The collection represent layer names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serviceLocationUrl|`String`|The url of wfs service.|

---
#### `GetLayers(Uri,IWebProxy)`

**Summary**

   *Get layer names from specific wfs service url.*

**Remarks**

   *Typically, it will call WfsFeatureSource.GetLayers(serverUri) internally.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The collection represent layer names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|
|webProxy|`IWebProxy`|The proxy of the wfs service.|

---
#### `GetLayers(Uri)`

**Summary**

   *Get layer names from specific wfs service url.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|The collection represent layer names.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|serverUri|`Uri`|The url of wfs service.|

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
|e|[`DrawingFeaturesEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFeaturesEventArgs.md)|N/A|

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
#### `SetupTools()`

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
#### `SetupToolsCore()`

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

#### DrawingFeatures

*N/A*

**Remarks**

*N/A*

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


