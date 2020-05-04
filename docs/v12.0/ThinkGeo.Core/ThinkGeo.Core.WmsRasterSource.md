# WmsRasterSource


## Inheritance Hierarchy

+ `Object`
  + [`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)
    + **`WmsRasterSource`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`WmsRasterSource()`](#wmsrastersource)|
|[`WmsRasterSource(Uri)`](#wmsrastersourceuri)|
|[`WmsRasterSource(Uri,IWebProxy)`](#wmsrastersourceuriiwebproxy)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ActiveLayerNames`](#activelayernames)|Collection<`String`>|This property allows the active layers requested from the client to be shown on the map.|
|[`ActiveStyleNames`](#activestylenames)|Collection<`String`>|This property allows the active styles requested from the client to be shown on the map.|
|[`AxisOrder`](#axisorder)|[`WmsAxisOrder`](../ThinkGeo.Core/ThinkGeo.Core.WmsAxisOrder.md)|N/A|
|[`BlueTranslation`](#bluetranslation)|`Single`|N/A|
|[`CapabilitesCacheTimeout`](#capabilitescachetimeout)|`TimeSpan`|N/A|
|[`Credentials`](#credentials)|`ICredentials`|This property gets or sets the base authentication interface for retrieving credentials for Web Client authentication.|
|[`Crs`](#crs)|`String`|This property gets or sets the projected or geographic coordinate reference system to be used.|
|[`Exceptions`](#exceptions)|`String`|This property indicates the format in which the client wishes to be notified of service exceptions.|
|[`GreenTranslation`](#greentranslation)|`Single`|N/A|
|[`IsGrayscale`](#isgrayscale)|`Boolean`|N/A|
|[`IsNegative`](#isnegative)|`Boolean`|N/A|
|[`IsOpen`](#isopen)|`Boolean`|N/A|
|[`IsTransparent`](#istransparent)|`Boolean`|This property gets or sets whether the response map image's background color is transparent or not.|
|[`OutputFormat`](#outputformat)|`String`|This property gets or sets the desired output format for the map being requested from the WMS.|
|[`Parameters`](#parameters)|Dictionary<`String`,`String`>|This property specifies a dictionary used to update the request sent from the client to the WMS server.|
|[`Projection`](#projection)|[`Projection`](../ThinkGeo.Core/ThinkGeo.Core.Projection.md)|N/A|
|[`ProjectionConverter`](#projectionconverter)|[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)|N/A|
|[`Proxy`](#proxy)|`IWebProxy`|This property gets or sets the proxy used for requesting a Web Response.|
|[`RedTranslation`](#redtranslation)|`Single`|N/A|
|[`ScaleFactor`](#scalefactor)|`Double`|N/A|
|[`TimeoutInSecond`](#timeoutinsecond)|`Int32`|This property specifies the timeout of the web request in seconds.  The default timeout value is 20 seconds.|
|[`Transparency`](#transparency)|`Single`|N/A|
|[`Uri`](#uri)|`Uri`|This property specifies the URI of the WMS server.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Close()`](#close)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBox()`](#getboundingbox)|
|[`GetFeatureInfo(ScreenPointF)`](#getfeatureinfoscreenpointf)|
|[`GetFeatureInfo(ScreenPointF,String)`](#getfeatureinfoscreenpointfstring)|
|[`GetFeatureInfo(ScreenPointF,Int32)`](#getfeatureinfoscreenpointfint32)|
|[`GetFeatureInfo(ScreenPointF,String,Int32)`](#getfeatureinfoscreenpointfstringint32)|
|[`GetHashCode()`](#gethashcode)|
|[`GetHorizontalResolution()`](#gethorizontalresolution)|
|[`GetImage(RectangleShape,Int32,Int32)`](#getimagerectangleshapeint32int32)|
|[`GetImageHeight()`](#getimageheight)|
|[`GetImageWidth()`](#getimagewidth)|
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
|[`GetWorldFileText()`](#getworldfiletext)|
|[`Open()`](#open)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`BuildWmsGetFeatureInfoUri(ScreenPointF,String,Int32)`](#buildwmsgetfeatureinfouriscreenpointfstringint32)|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`CloseCore()`](#closecore)|
|[`Finalize()`](#finalize)|
|[`GetBoundingBoxCore()`](#getboundingboxcore)|
|[`GetFeatureInfoCore(ScreenPointF,String,Int32)`](#getfeatureinfocorescreenpointfstringint32)|
|[`GetImageCore(RectangleShape,Int32,Int32)`](#getimagecorerectangleshapeint32int32)|
|[`GetImageHeightCore()`](#getimageheightcore)|
|[`GetImageWidthCore()`](#getimagewidthcore)|
|[`GetRequestUrlCore(RectangleShape,Int32,Int32)`](#getrequesturlcorerectangleshapeint32int32)|
|[`GetWrappingImageLeft(RectangleShape,Double,Double,RectangleShape)`](#getwrappingimageleftrectangleshapedoubledoublerectangleshape)|
|[`GetWrappingImageRight(RectangleShape,Double,Double,RectangleShape)`](#getwrappingimagerightrectangleshapedoubledoublerectangleshape)|
|[`HandleResponse(WebRequest)`](#handleresponsewebrequest)|
|[`HandleResponseCore(WebRequest)`](#handleresponsecorewebrequest)|
|[`HandleXmlInfoFormatResponse(XmlDocument)`](#handlexmlinfoformatresponsexmldocument)|
|[`HandleXmlInfoFormatResponseCore(XmlDocument)`](#handlexmlinfoformatresponsecorexmldocument)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnChangedSourceStatus(ChangedSourceStatusEventArgs)`](#onchangedsourcestatuschangedsourcestatuseventargs)|
|[`OnClosedRasterSource(ClosedRasterSourceEventArgs)`](#onclosedrastersourceclosedrastersourceeventargs)|
|[`OnClosingRasterSource(ClosingRasterSourceEventArgs)`](#onclosingrastersourceclosingrastersourceeventargs)|
|[`OnOpenedRasterSource(OpenedRasterSourceEventArgs)`](#onopenedrastersourceopenedrastersourceeventargs)|
|[`OnOpeningRasterSource(OpeningRasterSourceEventArgs)`](#onopeningrastersourceopeningrastersourceeventargs)|
|[`OnSendingWebRequest(SendingWebRequestEventArgs)`](#onsendingwebrequestsendingwebrequesteventargs)|
|[`OnSentWebRequest(SentWebRequestEventArgs)`](#onsentwebrequestsentwebrequesteventargs)|
|[`OpenCore()`](#opencore)|
|[`SendWebRequest(WebRequest)`](#sendwebrequestwebrequest)|
|[`SendWebRequestCore(WebRequest)`](#sendwebrequestcorewebrequest)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`SendingWebRequest`](#sendingwebrequest)|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|
|[`SentWebRequest`](#sentwebrequest)|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|
|[`OpeningRasterSource`](#openingrastersource)|[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)|N/A|
|[`OpenedRasterSource`](#openedrastersource)|[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)|N/A|
|[`ClosingRasterSource`](#closingrastersource)|[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)|N/A|
|[`ClosedRasterSource`](#closedrastersource)|[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`WmsRasterSource()`](#wmsrastersource)|
|[`WmsRasterSource(Uri)`](#wmsrastersourceuri)|
|[`WmsRasterSource(Uri,IWebProxy)`](#wmsrastersourceuriiwebproxy)|

### Protected Constructors


### Public Properties

#### `ActiveLayerNames`

**Summary**

   *This property allows the active layers requested from the client to be shown on the map.*

**Remarks**

   *When requesting a map, a client may specify the layers to be shown on the map.*

**Return Value**

Collection<`String`>

---
#### `ActiveStyleNames`

**Summary**

   *This property allows the active styles requested from the client to be shown on the map.*

**Remarks**

   *When requesting a map, a client may specify the styles to be shown on the map.*

**Return Value**

Collection<`String`>

---
#### `AxisOrder`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`WmsAxisOrder`](../ThinkGeo.Core/ThinkGeo.Core.WmsAxisOrder.md)

---
#### `BlueTranslation`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `CapabilitesCacheTimeout`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TimeSpan`

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
#### `OutputFormat`

**Summary**

   *This property gets or sets the desired output format for the map being requested from the WMS.*

**Remarks**

   *When requesting a map, a client may specify the output format in which to show the map. Formats are specified as MIME types such as "image/gif" or "image/png".*

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
#### `ProjectionConverter`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ProjectionConverter`](../ThinkGeo.Core/ThinkGeo.Core.ProjectionConverter.md)

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
#### `ScaleFactor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

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
#### `Uri`

**Summary**

   *This property specifies the URI of the WMS server.*

**Remarks**

   *N/A*

**Return Value**

`Uri`

---

### Protected Properties


### Public Methods

#### `CloneDeep()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)|N/A|

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
#### `GetImage(RectangleShape,Int32,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Int32`|N/A|
|canvasHeight|`Int32`|N/A|

---
#### `GetImageHeight()`

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
#### `GetImageWidth()`

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
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The world extent requested by the client to get the map.|
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
|Collection<`String`>|The exception format at the server side.|

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
|Collection<`String`>|The names of all layers at the server side.|

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
|Collection<`String`>|The output format at the server side.|

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
|Collection<`String`>|The names of all styles at the server side.|

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
|`String`|Returns a string reflecting the version of the service in WMS.|

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
#### `GetWorldFileText()`

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

#### `BuildWmsGetFeatureInfoUri(ScreenPointF,String,Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Uri`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPointF|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|infoFormat|`String`|N/A|
|maxFeatures|`Int32`|N/A|

---
#### `CloneDeepCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterSource`](../ThinkGeo.Core/ThinkGeo.Core.RasterSource.md)|N/A|

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

   *This method returns the bounding box of the RasterSource.*

**Remarks**

   *This method returns the bounding box of the RasterSource.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The bounding box of the RasterSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetFeatureInfoCore(ScreenPointF,String,Int32)`

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
#### `GetImageCore(RectangleShape,Int32,Int32)`

**Summary**

   *This method returns an image based on the worldExtent and image width and height.*

**Remarks**

   *This method is responsible for returning the image based on the parameters passed in. As the core version of this method is abstract, you will need to override it when creating our own RasterSource.*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This method returns an image based on the worldExtent and image width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter represents the worldExtent you want to draw.|
|canvasWidth|`Int32`|This parameter represents the width of the image you want to draw.|
|canvasHeight|`Int32`|This parameter represents the height of the image you want to draw.|

---
#### `GetImageHeightCore()`

**Summary**

   *This method returns the height of the image in screen coordinates.*

**Remarks**

   *This virtual method is called from the concrete method GetImageHeight. It returns the height of the image in screen coordinates.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|The height of the image in screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetImageWidthCore()`

**Summary**

   *This method returns the width of the image in screen coordinates.*

**Remarks**

   *This virtual method is called from the concrete method GetImageWidth. It returns the width of the image in screen coordinates.*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|The width of the image in screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetRequestUrlCore(RectangleShape,Int32,Int32)`

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
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Int32`|N/A|
|canvasHeight|`Int32`|N/A|

---
#### `GetWrappingImageLeft(RectangleShape,Double,Double,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|wrappingExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `GetWrappingImageRight(RectangleShape,Double,Double,RectangleShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|
|wrappingExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|

---
#### `HandleResponse(WebRequest)`

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
|reqeust|`WebRequest`|N/A|

---
#### `HandleResponseCore(WebRequest)`

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
|request|`WebRequest`|N/A|

---
#### `HandleXmlInfoFormatResponse(XmlDocument)`

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
|xmlDocument|`XmlDocument`|N/A|

---
#### `HandleXmlInfoFormatResponseCore(XmlDocument)`

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
|xmlDocument|`XmlDocument`|N/A|

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
#### `OnChangedSourceStatus(ChangedSourceStatusEventArgs)`

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
|e|[`ChangedSourceStatusEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ChangedSourceStatusEventArgs.md)|N/A|

---
#### `OnClosedRasterSource(ClosedRasterSourceEventArgs)`

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
|e|[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)|N/A|

---
#### `OnClosingRasterSource(ClosingRasterSourceEventArgs)`

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
|e|[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)|N/A|

---
#### `OnOpenedRasterSource(OpenedRasterSourceEventArgs)`

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
|e|[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)|N/A|

---
#### `OnOpeningRasterSource(OpeningRasterSourceEventArgs)`

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
|e|[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)|N/A|

---
#### `OnSendingWebRequest(SendingWebRequestEventArgs)`

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
|e|[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)|N/A|

---
#### `OnSentWebRequest(SentWebRequestEventArgs)`

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
|e|[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)|N/A|

---
#### `OpenCore()`

**Summary**

   *This method opens the RasterSource so that it is initialized and ready to use.*

**Remarks**

   *This protected virtual method is called from the concrete public method Open. The Open method plays an important role, as it is responsible for initializing the RasterSource. Most methods on the RasterSource will throw an exception if the state of the RasterSource is not opened. When the map draws each layer, it will open the RasterSource as one of its first steps; then, after it is finished drawing with that layer, it will close it. In this way, we are sure to release all resources used by the RasterSource. When implementing this abstract method, consider opening files for file-based sources, connecting to databases in the database-based sources and so on. You will get a chance to close these in the Close method of the RasterSource.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `SendWebRequest(WebRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`WebResponse`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|webRequest|`WebRequest`|N/A|

---
#### `SendWebRequestCore(WebRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`WebResponse`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|webRequest|`WebRequest`|N/A|

---

### Public Events

#### SendingWebRequest

*N/A*

**Remarks**

*This event is called before sending the reqeust for raster image. It is typical that user want to get the url of reqeust and modify it according to their requirements. For example, user could create a signiture for it, and verify it on the server side.*

**Event Arguments**

[`SendingWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SendingWebRequestEventArgs.md)

#### SentWebRequest

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`SentWebRequestEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.SentWebRequestEventArgs.md)

#### OpeningRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OpeningRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpeningRasterSourceEventArgs.md)

#### OpenedRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OpenedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.OpenedRasterSourceEventArgs.md)

#### ClosingRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClosingRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosingRasterSourceEventArgs.md)

#### ClosedRasterSource

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ClosedRasterSourceEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.ClosedRasterSourceEventArgs.md)


