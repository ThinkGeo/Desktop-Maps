# MapEngine


## Inheritance Hierarchy

+ `Object`
  + **`MapEngine`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MapEngine()`](#mapengine)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AdornmentLayers`](#adornmentlayers)|GeoCollection<[`AdornmentLayer`](../ThinkGeo.Core/ThinkGeo.Core.AdornmentLayer.md)>|This property holds a collection of AdornmentLayers to be drawn on the MapEngine.|
|[`BackgroundFillBrush`](#backgroundfillbrush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|Gets or sets the GeoBrush for the background of the MapEngine.|
|[`Canvas`](#canvas)|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|Gets and sets the GeoCanvas used to draw the Layers.|
|[`CurrentExtent`](#currentextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This property gets or sets the current extent of the MapEngine.|
|[`DynamicLayers`](#dynamiclayers)|GeoCollection<[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)>|This property holds a group of Layers to be drawn on the MapEngine.|
|[`ShowLogo`](#showlogo)|`Boolean`|This property specifies whether the logo is shown on the Map or not.|
|[`StaticLayers`](#staticlayers)|GeoCollection<[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)>|This property holds a group of Layers to be drawn on the MapEngine.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`CenterAt(PointShape,Single,Single)`](#centeratpointshapesinglesingle)|
|[`CenterAt(Feature,Single,Single)`](#centeratfeaturesinglesingle)|
|[`CenterAt(Single,Single,Single,Single)`](#centeratsinglesinglesinglesingle)|
|[`CenterAt(RectangleShape,PointShape,Single,Single)`](#centeratrectangleshapepointshapesinglesingle)|
|[`CenterAt(RectangleShape,Feature,Single,Single)`](#centeratrectangleshapefeaturesinglesingle)|
|[`CenterAt(RectangleShape,Single,Single,Single,Single)`](#centeratrectangleshapesinglesinglesinglesingle)|
|[`CloseAllLayers()`](#closealllayers)|
|[`Draw(IEnumerable<Layer>,GeoImage,GeographyUnit)`](#drawienumerable<layer>geoimagegeographyunit)|
|[`Draw(IEnumerable<Layer>,Int32,Int32,GeographyUnit)`](#drawienumerable<layer>int32int32geographyunit)|
|[`DrawAdornmentLayers(GeoImage,GeographyUnit)`](#drawadornmentlayersgeoimagegeographyunit)|
|[`DrawAdornmentLayers(Int32,Int32,GeographyUnit)`](#drawadornmentlayersint32int32geographyunit)|
|[`DrawDynamicLayers(GeoImage,GeographyUnit)`](#drawdynamiclayersgeoimagegeographyunit)|
|[`DrawDynamicLayers(Int32,Int32,GeographyUnit)`](#drawdynamiclayersint32int32geographyunit)|
|[`DrawStaticLayers(GeoImage,GeographyUnit)`](#drawstaticlayersgeoimagegeographyunit)|
|[`DrawStaticLayers(Int32,Int32,GeographyUnit)`](#drawstaticlayersint32int32geographyunit)|
|[`Equals(Object)`](#equalsobject)|
|[`FindDynamicFeatureLayer(String)`](#finddynamicfeaturelayerstring)|
|[`FindDynamicRasterLayer(String)`](#finddynamicrasterlayerstring)|
|[`FindStaticFeatureLayer(String)`](#findstaticfeaturelayerstring)|
|[`FindStaticRasterLayer(String)`](#findstaticrasterlayerstring)|
|[`GetBoundingBoxOfItems(IEnumerable<BaseShape>)`](#getboundingboxofitemsienumerable<baseshape>)|
|[`GetBoundingBoxOfItems(IEnumerable<Feature>)`](#getboundingboxofitemsienumerable<feature>)|
|[`GetCurrentScale(Single,Single,GeographyUnit)`](#getcurrentscalesinglesinglegeographyunit)|
|[`GetCurrentScale(RectangleShape,Single,Single,GeographyUnit)`](#getcurrentscalerectangleshapesinglesinglegeographyunit)|
|[`GetDrawingExtent(Single,Single)`](#getdrawingextentsinglesingle)|
|[`GetDrawingExtent(RectangleShape,Single,Single)`](#getdrawingextentrectangleshapesinglesingle)|
|[`GetHashCode()`](#gethashcode)|
|[`GetScreenDistanceBetweenTwoWorldPoints(PointShape,PointShape,Single,Single)`](#getscreendistancebetweentwoworldpointspointshapepointshapesinglesingle)|
|[`GetScreenDistanceBetweenTwoWorldPoints(Feature,Feature,Single,Single)`](#getscreendistancebetweentwoworldpointsfeaturefeaturesinglesingle)|
|[`GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,PointShape,PointShape,Single,Single)`](#getscreendistancebetweentwoworldpointsrectangleshapepointshapepointshapesinglesingle)|
|[`GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,Feature,Feature,Single,Single)`](#getscreendistancebetweentwoworldpointsrectangleshapefeaturefeaturesinglesingle)|
|[`GetType()`](#gettype)|
|[`GetVersion()`](#getversion)|
|[`GetWorldDistanceBetweenTwoScreenPoints(ScreenPointF,ScreenPointF,Single,Single,GeographyUnit,DistanceUnit)`](#getworlddistancebetweentwoscreenpointsscreenpointfscreenpointfsinglesinglegeographyunitdistanceunit)|
|[`GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,ScreenPointF,ScreenPointF,Single,Single,GeographyUnit,DistanceUnit)`](#getworlddistancebetweentwoscreenpointsrectangleshapescreenpointfscreenpointfsinglesinglegeographyunitdistanceunit)|
|[`GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,Single,Single,Single,Single,Single,Single,GeographyUnit,DistanceUnit)`](#getworlddistancebetweentwoscreenpointsrectangleshapesinglesinglesinglesinglesinglesinglegeographyunitdistanceunit)|
|[`LoadDataTable(Collection<Feature>,IEnumerable<String>)`](#loaddatatablecollection<feature>ienumerable<string>)|
|[`OpenAllLayers()`](#openalllayers)|
|[`Pan(PanDirection,Int32)`](#panpandirectionint32)|
|[`Pan(Single,Int32)`](#pansingleint32)|
|[`Pan(RectangleShape,PanDirection,Int32)`](#panrectangleshapepandirectionint32)|
|[`Pan(RectangleShape,Single,Int32)`](#panrectangleshapesingleint32)|
|[`SnapToZoomLevel(GeographyUnit,Single,Single,ZoomLevelSet)`](#snaptozoomlevelgeographyunitsinglesinglezoomlevelset)|
|[`SnapToZoomLevel(RectangleShape,GeographyUnit,Single,Single,ZoomLevelSet)`](#snaptozoomlevelrectangleshapegeographyunitsinglesinglezoomlevelset)|
|[`ToScreenCoordinate(Double,Double,Single,Single)`](#toscreencoordinatedoubledoublesinglesingle)|
|[`ToScreenCoordinate(PointShape,Single,Single)`](#toscreencoordinatepointshapesinglesingle)|
|[`ToScreenCoordinate(Feature,Single,Single)`](#toscreencoordinatefeaturesinglesingle)|
|[`ToScreenCoordinate(RectangleShape,Double,Double,Single,Single)`](#toscreencoordinaterectangleshapedoubledoublesinglesingle)|
|[`ToScreenCoordinate(RectangleShape,PointShape,Single,Single)`](#toscreencoordinaterectangleshapepointshapesinglesingle)|
|[`ToScreenCoordinate(RectangleShape,Feature,Single,Single)`](#toscreencoordinaterectangleshapefeaturesinglesingle)|
|[`ToString()`](#tostring)|
|[`ToWorldCoordinate(Single,Single,Single,Single)`](#toworldcoordinatesinglesinglesinglesingle)|
|[`ToWorldCoordinate(ScreenPointF,Single,Single)`](#toworldcoordinatescreenpointfsinglesingle)|
|[`ToWorldCoordinate(RectangleShape,Single,Single,Single,Single)`](#toworldcoordinaterectangleshapesinglesinglesinglesingle)|
|[`ToWorldCoordinate(RectangleShape,ScreenPointF,Single,Single)`](#toworldcoordinaterectangleshapescreenpointfsinglesingle)|
|[`ZoomIn(Int32)`](#zoominint32)|
|[`ZoomIn(RectangleShape,Int32)`](#zoominrectangleshapeint32)|
|[`ZoomIntoCenter(Int32,PointShape,Single,Single)`](#zoomintocenterint32pointshapesinglesingle)|
|[`ZoomIntoCenter(Int32,Feature,Single,Single)`](#zoomintocenterint32featuresinglesingle)|
|[`ZoomIntoCenter(Int32,Single,Single,Single,Single)`](#zoomintocenterint32singlesinglesinglesingle)|
|[`ZoomIntoCenter(RectangleShape,Int32,PointShape,Single,Single)`](#zoomintocenterrectangleshapeint32pointshapesinglesingle)|
|[`ZoomIntoCenter(RectangleShape,Int32,Feature,Single,Single)`](#zoomintocenterrectangleshapeint32featuresinglesingle)|
|[`ZoomIntoCenter(RectangleShape,Int32,Single,Single,Single,Single)`](#zoomintocenterrectangleshapeint32singlesinglesinglesingle)|
|[`ZoomOut(Int32)`](#zoomoutint32)|
|[`ZoomOut(RectangleShape,Int32)`](#zoomoutrectangleshapeint32)|
|[`ZoomOutToCenter(Int32,Single,Single,Single,Single)`](#zoomouttocenterint32singlesinglesinglesingle)|
|[`ZoomOutToCenter(Int32,PointShape,Single,Single)`](#zoomouttocenterint32pointshapesinglesingle)|
|[`ZoomOutToCenter(Int32,Feature,Single,Single)`](#zoomouttocenterint32featuresinglesingle)|
|[`ZoomOutToCenter(RectangleShape,Int32,Single,Single,Single,Single)`](#zoomouttocenterrectangleshapeint32singlesinglesinglesingle)|
|[`ZoomOutToCenter(RectangleShape,Int32,PointShape,Single,Single)`](#zoomouttocenterrectangleshapeint32pointshapesinglesingle)|
|[`ZoomOutToCenter(RectangleShape,Int32,Feature,Single,Single)`](#zoomouttocenterrectangleshapeint32featuresinglesingle)|
|[`ZoomToScale(Double,GeographyUnit,Single,Single)`](#zoomtoscaledoublegeographyunitsinglesingle)|
|[`ZoomToScale(Double,RectangleShape,GeographyUnit,Single,Single)`](#zoomtoscaledoublerectangleshapegeographyunitsinglesingle)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnAdornmentLayerDrawing(DrawingAdornmentLayerEventArgs)`](#onadornmentlayerdrawingdrawingadornmentlayereventargs)|
|[`OnAdornmentLayerDrawn(DrawnAdornmentLayerEventArgs)`](#onadornmentlayerdrawndrawnadornmentlayereventargs)|
|[`OnAdornmentLayersDrawing(DrawingAdornmentLayersEventArgs)`](#onadornmentlayersdrawingdrawingadornmentlayerseventargs)|
|[`OnAdornmentLayersDrawn(DrawnAdornmentLayersEventArgs)`](#onadornmentlayersdrawndrawnadornmentlayerseventargs)|
|[`OnLayerDrawing(LayerDrawingEventArgs)`](#onlayerdrawinglayerdrawingeventargs)|
|[`OnLayerDrawn(LayerDrawnEventArgs)`](#onlayerdrawnlayerdrawneventargs)|
|[`OnLayersDrawing(LayersDrawingEventArgs)`](#onlayersdrawinglayersdrawingeventargs)|
|[`OnLayersDrawn(LayersDrawnEventArgs)`](#onlayersdrawnlayersdrawneventargs)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`LayersDrawing`](#layersdrawing)|[`LayersDrawingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayersDrawingEventArgs.md)|N/A|
|[`LayersDrawn`](#layersdrawn)|[`LayersDrawnEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayersDrawnEventArgs.md)|N/A|
|[`LayerDrawing`](#layerdrawing)|[`LayerDrawingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayerDrawingEventArgs.md)|N/A|
|[`LayerDrawn`](#layerdrawn)|[`LayerDrawnEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayerDrawnEventArgs.md)|N/A|
|[`AdornmentLayersDrawing`](#adornmentlayersdrawing)|[`DrawingAdornmentLayersEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAdornmentLayersEventArgs.md)|N/A|
|[`AdornmentLayersDrawn`](#adornmentlayersdrawn)|[`DrawnAdornmentLayersEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAdornmentLayersEventArgs.md)|N/A|
|[`AdornmentLayerDrawing`](#adornmentlayerdrawing)|[`DrawingAdornmentLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAdornmentLayerEventArgs.md)|N/A|
|[`AdornmentLayerDrawn`](#adornmentlayerdrawn)|[`DrawnAdornmentLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAdornmentLayerEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`MapEngine()`](#mapengine)|

### Protected Constructors


### Public Properties

#### `AdornmentLayers`

**Summary**

   *This property holds a collection of AdornmentLayers to be drawn on the MapEngine.*

**Remarks**

   *This collection of Layers StaticLayers will be drawn when calling the DrawAdornmentLayers API.*

**Return Value**

GeoCollection<[`AdornmentLayer`](../ThinkGeo.Core/ThinkGeo.Core.AdornmentLayer.md)>

---
#### `BackgroundFillBrush`

**Summary**

   *Gets or sets the GeoBrush for the background of the MapEngine.*

**Remarks**

   *N/A*

**Return Value**

[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)

---
#### `Canvas`

**Summary**

   *Gets and sets the GeoCanvas used to draw the Layers.*

**Remarks**

   *N/A*

**Return Value**

[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)

---
#### `CurrentExtent`

**Summary**

   *This property gets or sets the current extent of the MapEngine.*

**Remarks**

   *The current extent is the rectangle that is currently being shown on the MapEngine.*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `DynamicLayers`

**Summary**

   *This property holds a group of Layers to be drawn on the MapEngine.*

**Remarks**

   *This collection of Layers StaticLayers will be drawn when calling the DrawDynamicLayers API.*

**Return Value**

GeoCollection<[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)>

---
#### `ShowLogo`

**Summary**

   *This property specifies whether the logo is shown on the Map or not.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `StaticLayers`

**Summary**

   *This property holds a group of Layers to be drawn on the MapEngine.*

**Remarks**

   *This collection of Layers StaticLayers will be drawn when calling the DrawStaticLayers API.*

**Return Value**

GeoCollection<[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)>

---

### Protected Properties


### Public Methods

#### `CenterAt(PointShape,Single,Single)`

**Summary**

   *This is a function that allows you to pass a world point to center on and a height and width in screen units.  The function will update the current extent by centering on the point and adjusting its ratio based on the height and width in screen coordinates.*

**Remarks**

   *This API will update the CurrentExtent.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CenterAt(Feature,Single,Single)`

**Summary**

   *This is a function that allows you to pass in a feature to center on, as well as a height and width in screen units.  The function will center the CurrentExtent based on the specified feature and adjust its ratio based on the height and width in screen coordinates.*

**Remarks**

   *This API will update the CurrentExtent.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world point feature you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CenterAt(Single,Single,Single,Single)`

**Summary**

   *This is a function that allows you to pass a screen point to center on and a height and width in screen units.  The function will update the current extent by centering on the point and adjusting its ratio based on the height and width in screen coordinates.*

**Remarks**

   *This API will update the CurrentExtent.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenX|`Single`|This parameter is the X coordinate on the screen to center on.|
|screenY|`Single`|This parameter is the Y coordinate on the screen to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CenterAt(RectangleShape,PointShape,Single,Single)`

**Summary**

   *This is a static function that allows you to pass in a world rectangle, a world point to center on, and a height and width in screen units.  The function will center the rectangle based on the point, then adjust the rectangle's ratio based on the height and width in screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent centered on a point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the current extent you want to center.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CenterAt(RectangleShape,Feature,Single,Single)`

**Summary**

   *This is a static function that allows you to pass in a world rectangle, a world point to center on, and a height and width in screen units.  The function will center the rectangle based on the point, then adjust the rectangle's ratio based on the height and width in screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent centered on a point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the current extent you want to center.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world point feature you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CenterAt(RectangleShape,Single,Single,Single,Single)`

**Summary**

   *This is a static function that allows you to pass in a world rectangle, a point in screen coordinates to center on, and a height and width in screen units.  The function will center the rectangle based on the screen point, then adjust the rectangle's ratio based on the height and width in screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent centered on a point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the current extent you want to center.|
|screenX|`Single`|This parameter is the X coordinate on the screen to center to.|
|screenY|`Single`|This parameter is the Y coordinate on the screen to center to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CloseAllLayers()`

**Summary**

   *This API allows you close all of the layers (either static or dynamic).*

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
#### `Draw(IEnumerable<Layer>,GeoImage,GeographyUnit)`

**Summary**

   *Draw a group of layers on the specified "background" image.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the target layers on the specified "background" image.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|layers|IEnumerable<[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)>|This parameter specifies the target layers to be drawn.|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This parameter specifies the "background" image of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `Draw(IEnumerable<Layer>,Int32,Int32,GeographyUnit)`

**Summary**

   *Draw a group of layers and return a new image with the specified width and height.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the target layers based on the specified width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|layers|IEnumerable<[`Layer`](../ThinkGeo.Core/ThinkGeo.Core.Layer.md)>|This parameter specifies the target layers to be drawn.|
|width|`Int32`|This parameter specifies the width of the returning image.|
|height|`Int32`|This parameter specifies the height of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `DrawAdornmentLayers(GeoImage,GeographyUnit)`

**Summary**

   *Draw a group of AdornmentLayers on the specified "background" image.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the group of AdornmentLayers on the specified "background" image.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This parameter specifies the "background" image of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `DrawAdornmentLayers(Int32,Int32,GeographyUnit)`

**Summary**

   *Draw a group of AdornmentLayers and return a new image with the specified width and height.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the group of AdornmentLayers based on the specified width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|width|`Int32`|This parameter specifies the width of the returning image.|
|height|`Int32`|This parameter specifies the height of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `DrawDynamicLayers(GeoImage,GeographyUnit)`

**Summary**

   *Draw a group of dynamic layers on the specified "background" image.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the group of dynamic layers on the specified "background" image.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This parameter specifies the "background" image of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `DrawDynamicLayers(Int32,Int32,GeographyUnit)`

**Summary**

   *Draw a group of dynamic layers and return a new image with the specified width and height.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the group of dynamic layers based on the specified width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|width|`Int32`|This parameter specifies the width of the returning image.|
|height|`Int32`|This parameter specifies the height of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `DrawStaticLayers(GeoImage,GeographyUnit)`

**Summary**

   *Draw a group of static layers on the specified "background" image.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the group of static layers on the specified "background" image.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This parameter specifies the "background" image of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `DrawStaticLayers(Int32,Int32,GeographyUnit)`

**Summary**

   *Draw a group of static layers and return a new image with the specified width and height.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The resulting image after drawing the group of static layers based on the specified width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|width|`Int32`|This parameter specifies the width of the returning image.|
|height|`Int32`|This parameter specifies the height of the returning image.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

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
#### `FindDynamicFeatureLayer(String)`

**Summary**

   *Find the feature layer by key (specified in the "name" parameter) within the collection of DynamicLayers.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)|The corresponding FeatureLayer with the specified key in the MapControl.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|The key to find the final result feature layer.|

---
#### `FindDynamicRasterLayer(String)`

**Summary**

   *Find the raster layer by key (specified in the "name" parameter) within the collection of DynamicLayers.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterLayer`](../ThinkGeo.Core/ThinkGeo.Core.RasterLayer.md)|The corresponding RasterLayer with the specified key in the MapControl.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|The key to find the final result raster layer.|

---
#### `FindStaticFeatureLayer(String)`

**Summary**

   *Finds a feature layer by key (specified in the "name" parameter) within the collection of StaticLayers.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)|The corresponding FeatureLayer with the specified key in the MapControl.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|The key to find the final result feature layer.|

---
#### `FindStaticRasterLayer(String)`

**Summary**

   *Find the raster layer by key (specified in the "name" parameter) within the collection of StaticLayers.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterLayer`](../ThinkGeo.Core/ThinkGeo.Core.RasterLayer.md)|The corresponding RasterLayer with the passing specified in the MapControl.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|The key to find the final result raster layer.|

---
#### `GetBoundingBoxOfItems(IEnumerable<BaseShape>)`

**Summary**

   *This API gets the BoundingBox of a group of BaseShapes.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The BoundingBox that contains all the shapes you passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapes|IEnumerable<[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)>|The target group of BaseShapes to get the BoundingBox for.|

---
#### `GetBoundingBoxOfItems(IEnumerable<Feature>)`

**Summary**

   *This API gets the BoundingBox of a group of Features.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The BoundingBox that contains all the features you passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The target group of Features to get the BoundingBox for.|

---
#### `GetCurrentScale(Single,Single,GeographyUnit)`

**Summary**

   *Get the current Scale responding to the CurrentExtent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|The calculated scale based on the CurrentExtent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenWidth|`Single`|This parameter specifies the screen width responding to the CurrentExtent.|
|screenHeight|`Single`|N/A|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|

---
#### `GetCurrentScale(RectangleShape,Single,Single,GeographyUnit)`

**Summary**

   *This Static API is used to calculate the scale based on the specified worldExtent and its corresponding ScreenWidth and MapUnit.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter specifies the worldExtent used to calculate the current scale.|
|screenWidth|`Single`|This parameter specifies the screenWidth corresponding to the worldExtent.|
|screenHeight|`Single`|N/A|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the unit for the extent, the result will be different if choose DecimalDegree as Unit and Meter as Unit.|

---
#### `GetDrawingExtent(Single,Single)`

**Summary**

   *This method returns an adjusted extent based on the ratio of the screen width and height.*

**Remarks**

   *This function is used because the extent to draw must be the rame ratio as the screen width and height. If they are not, then the image drawn will be stretched or compressed. We always adjust the extent upwards to ensure that no matter how we adjust it, the original extent will fit within the new extent. This ensures that everything you wanted to see in the first extent is visible and maybe a bit more.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent based on the ratio of the screen width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `GetDrawingExtent(RectangleShape,Single,Single)`

**Summary**

   *This method returns an adjusted extent based on the ratio of the screen width and height.*

**Remarks**

   *This function is used because the extent to draw must be the rame ratio as the screen width and height. If they are not, then the image drawn will be stretched or compressed. We always adjust the extent upwards to ensure that no matter how we adjust it, the original extent will fit within the new extent. This ensures that everything you wanted to see in the first extent is visible and maybe a bit more. This function takes a height and width in screen coordinates, then looks at a world extent passed, and returns an adjusted world rectangle so that the ratio to height and width in screen and world coordinates match.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent based on the ratio of the screen width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to adjust for drawing.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

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
#### `GetScreenDistanceBetweenTwoWorldPoints(PointShape,PointShape,Single,Single)`

**Summary**

   *This method returns the number of pixels between two world points using the CurrentExtent as reference.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the number of pixels between two world points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldPoint1|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the first point -- the one you want to measure from.|
|worldPoint2|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the second point -- the one you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `GetScreenDistanceBetweenTwoWorldPoints(Feature,Feature,Single,Single)`

**Summary**

   *This method returns the number of pixels between two world points using the CurrentExtent as reference.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the number of pixels between two world points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldPointFeature1|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the first pointFeture -- the one you want to measure from.|
|worldPointFeature2|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the second pointFeature -- the one you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,PointShape,PointShape,Single,Single)`

**Summary**

   *This method returns the number of pixels between two world points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the number of pixels between two world points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPoint1|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the first point -- the one you want to measure from.|
|worldPoint2|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the second point -- the one you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,Feature,Feature,Single,Single)`

**Summary**

   *This method returns the number of pixels between two world points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the number of pixels between two world points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPointFeature1|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the first point Feature -- the one you want to measure from.|
|worldPointFeature2|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the second point Feature -- the one you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

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
#### `GetVersion()`

**Summary**

   *Get the current MapSuiteCore.dll file version.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|A string representing the file version of MapSuiteCore.dll.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWorldDistanceBetweenTwoScreenPoints(ScreenPointF,ScreenPointF,Single,Single,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the distance in world units between two screen points by using the CurrentExtent as a reference.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns the distance in world units between two screen points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoint1|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This is the screen point you want to measure from.|
|screenPoint2|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This is the screen point you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter specifies the MapUnit used in the current map.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the geographic unit you want the result to show in.|

---
#### `GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,ScreenPointF,ScreenPointF,Single,Single,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the distance in world units between two screen points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns the distance in wold units between two screen points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenPoint1|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This is the screen point you want to measure from.|
|screenPoint2|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This is the screen point you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the world extent rectangle.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the geographic unit you want the result to show in.|

---
#### `GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,Single,Single,Single,Single,Single,Single,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the distance in wold units between two screen points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenPoint1X|`Single`|This parameter is the X of the point you want to measure from.|
|screenPoint1Y|`Single`|This parameter is the Y of the point you want to measure from.|
|screenPoint2X|`Single`|This parameter is the X of the point you want to measure to.|
|screenPoint2Y|`Single`|This parameter is the Y of the point you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the world extent you passed in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the geographic unit you want the result to show in.|

---
#### `LoadDataTable(Collection<Feature>,IEnumerable<String>)`

**Summary**

   *This method is a static API to get information about a group of passed-in features with the specified returningColumns, in the format of a DataTable.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DataTable`|A DateTable of information about those passed-in features and the returning columnNames.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter specifies the target features.|
|returningColumnNames|IEnumerable<`String`>|This parameter specifies the returning columnNames for the features.|

---
#### `OpenAllLayers()`

**Summary**

   *This API allows you to open all of the layers (either static or dynamic).*

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
#### `Pan(PanDirection,Int32)`

**Summary**

   *Update the CurrentExtent by using a panning operation.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|panDirection|[`PanDirection`](../ThinkGeo.Core/ThinkGeo.Core.PanDirection.md)|This parameter is the direction you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `Pan(Single,Int32)`

**Summary**

   *This method updates the CurrentExtent by using a panning operation.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|degree|`Single`|This parameter is the angle in degrees in which you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `Pan(RectangleShape,PanDirection,Int32)`

**Summary**

   *This method returns a panned extent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a panned extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to pan.|
|direction|[`PanDirection`](../ThinkGeo.Core/ThinkGeo.Core.PanDirection.md)|This parameter is the direction you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `Pan(RectangleShape,Single,Int32)`

**Summary**

   *This method returns a panned extent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a panned extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to pan.|
|degree|`Single`|This parameter is the angle in degrees in which you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `SnapToZoomLevel(GeographyUnit,Single,Single,ZoomLevelSet)`

**Summary**

   *This method updates the CurrentExtent by snapping to a zoom level in the provided zoom level set.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method updates the CurrentExtent by snapping to a zoom level in the provided zoom level set.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the geographic unit of the CurrentExtent.|
|screenWidth|`Single`|This parameter is the screen width.|
|screenHeight|`Single`|This parameter is the screen height.|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|This parameter is the set of zoom levels you want to snap to.|

---
#### `SnapToZoomLevel(RectangleShape,GeographyUnit,Single,Single,ZoomLevelSet)`

**Summary**

   *This method returns an extent that is snapped to a zoom level in the provided zoom level set.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is snapped to a zoom level in the provided zoom level set.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want snapped.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the geographic unit of the world extent parameter.|
|screenWidth|`Single`|This parameter is the screen width.|
|screenHeight|`Single`|This parameter is the screen height.|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|This parameter is the set of zoom levels you want to snap to.|

---
#### `ToScreenCoordinate(Double,Double,Single,Single)`

**Summary**

   *This method returns screen coordinates from the specified world coordinates, based on the CurrentExtent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from the specified world coordinates, based on the CurrentExtent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldX|`Double`|This parameter is the world point X you want converted to a screen point.|
|worldY|`Double`|This parameter is the world point Y you want converted to a screen point.|
|screenWidth|`Single`|This parameter is the width of the screen for the CurrentExtent.|
|screenHeight|`Single`|This parameter is the height of the screen for the CurrentExtent.|

---
#### `ToScreenCoordinate(PointShape,Single,Single)`

**Summary**

   *This method returns screen coordinates from the specified world coordinates, based on the CurrentExtent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from the specified world coordinates, based on the CurrentExtent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want converted to a screen point.|
|screenWidth|`Single`|This parameter is the width of the screen for the CurrentExtent.|
|screenHeight|`Single`|This parameter is the height of the screen for the CurrentExtent.|

---
#### `ToScreenCoordinate(Feature,Single,Single)`

**Summary**

   *This method returns screen coordinates from the specified world coordinate pointFeature, based on the CurrentExtent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from the specified world coordinate pointFeature, based on the CurrentExtent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldPointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world coordinate pointFeature you want converted to a screen point.|
|screenWidth|`Single`|This parameter is the width of the screen for the CurrentExtent.|
|screenHeight|`Single`|This parameter is the height of the screen for the CurrentExtent.|

---
#### `ToScreenCoordinate(RectangleShape,Double,Double,Single,Single)`

**Summary**

   *This method returns screen coordinates from world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldX|`Double`|This parameter is the world X you want converted to screen points.|
|worldY|`Double`|This parameter is the world Y you want converted to screen points.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToScreenCoordinate(RectangleShape,PointShape,Single,Single)`

**Summary**

   *This method returns screen coordinates from world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want converted to a screen point.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToScreenCoordinate(RectangleShape,Feature,Single,Single)`

**Summary**

   *This method returns screen coordinates from world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world point feature you want converted to a screen point.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

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
#### `ToWorldCoordinate(Single,Single,Single,Single)`

**Summary**

   *This method returns world coordinates from screen coordinates, based on the CurrentExtent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns world coordinates from screen coordinates, based on the CurrentExtent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenX|`Single`|This parameter is the X of the point you want converted to world coordinates.|
|screenY|`Single`|This parameter is the Y of the point you want converted to world coordinates.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToWorldCoordinate(ScreenPointF,Single,Single)`

**Summary**

   *This method returns world coordinates from screen coordinates, based on the CurrentExtent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns world coordinates from screen coordinates, based on the CurrentExtent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This parameter is the point you want converted to world coordinates.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToWorldCoordinate(RectangleShape,Single,Single,Single,Single)`

**Summary**

   *This method returns world coordinates from screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns world coordinates from screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenX|`Single`|This parameter is the X coordinate of the point you want converted to world coordinates.|
|screenY|`Single`|This parameter is the Y coordinate of the point you want converted to world coordinates.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToWorldCoordinate(RectangleShape,ScreenPointF,Single,Single)`

**Summary**

   *This method returns world coordinates from screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns world coordinates from screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This parameter is the screen point you want converted to a world point.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomIn(Int32)`

**Summary**

   *This method updates the CurrentExtent that is zoomed in by the percentage provided.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method updates the CurrentExtent that is zoomed in by the percentage provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|

---
#### `ZoomIn(RectangleShape,Int32)`

**Summary**

   *This method returns a new extent that is zoomed in by the percentage provided.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a new extent that is zoomed in by the percentage provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to zoom.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|

---
#### `ZoomIntoCenter(Int32,PointShape,Single,Single)`

**Summary**

   *This method will update the CurrentExtent by using the ZoomIntoCenter operation.*

**Remarks**

   *The CurrentExtent will be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width in screen coordinates.|
|screenHeight|`Single`|This parameter is the height in screen coordinates.|

---
#### `ZoomIntoCenter(Int32,Feature,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed in.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method returns an extent that is centered and zoomed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world point you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width in screen coordinates.|
|screenHeight|`Single`|This parameter is the height in screen coordinates.|

---
#### `ZoomIntoCenter(Int32,Single,Single,Single,Single)`

**Summary**

   *This method updates the CurrentExtent based on a calculated rectangle that is centered and zoomed in.*

**Remarks**

   *The CurrentExtent will be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|screenX|`Single`|This parameter is the screen X you want to center on.|
|screenY|`Single`|This parameter is the screen Y you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomIntoCenter(RectangleShape,Int32,PointShape,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed in.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent that you want centered and zoomed.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width in screen coordinates.|
|screenHeight|`Single`|This parameter is the height in screen coordinates.|

---
#### `ZoomIntoCenter(RectangleShape,Int32,Feature,Single,Single)`

**Summary**

   *This method returns a new extent that is zoomed in by the percentage provided.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a new extent that is zoomed in by the percentage provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to zoom.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world point feature you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width in screen coordinates.|
|screenHeight|`Single`|This parameter is the height in screen coordinates.|

---
#### `ZoomIntoCenter(RectangleShape,Int32,Single,Single,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed in.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|screenX|`Single`|This parameter is the screen X you want to center on.|
|screenY|`Single`|This parameter is the screen Y you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOut(Int32)`

**Summary**

   *This method will update the CurrentExtent by using the ZoomOut operation.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom.|

---
#### `ZoomOut(RectangleShape,Int32)`

**Summary**

   *This method returns a new extent that is zoomed out by the percentage provided.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a new extent that is zoomed out by the percentage provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to zoom.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|

---
#### `ZoomOutToCenter(Int32,Single,Single,Single,Single)`

**Summary**

   *This method updates the CurrentExtent by using the ZoomOutToCenter operation.*

**Remarks**

   *The CurrentExtent will  be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|screenX|`Single`|This parameter is the screen X you want to center on.|
|screenY|`Single`|This parameter is the screen Y you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOutToCenter(Int32,PointShape,Single,Single)`

**Summary**

   *This method updates the CurrentExtent by using the ZoomOutToCenter operation.*

**Remarks**

   *The CurrentExtent will be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOutToCenter(Int32,Feature,Single,Single)`

**Summary**

   *This method updates the CurrentExtent by using the ZoomOutToCenter operation.*

**Remarks**

   *The CurrentExtent will be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world point Feature you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOutToCenter(RectangleShape,Int32,Single,Single,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed out.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed out.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|screenX|`Single`|This parameter is the screen X you want to center on.|
|screenY|`Single`|This parameter is the screen Y you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOutToCenter(RectangleShape,Int32,PointShape,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed out.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed out.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOutToCenter(RectangleShape,Int32,Feature,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed out.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed out.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomToScale(Double,GeographyUnit,Single,Single)`

**Summary**

   *This method updates the CurrentExtent by zooming to a certain scale.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetScale|`Double`|This parameter is the scale you want to zoom into.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the geographic unit of the CurrentExtent.|
|screenWidth|`Single`|This parameter is the screen width.|
|screenHeight|`Single`|This parameter is the screen height.|

---
#### `ZoomToScale(Double,RectangleShape,GeographyUnit,Single,Single)`

**Summary**

   *This method returns a extent that has been zoomed into a certain scale.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a extent that has been zoomed into a certain scale.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetScale|`Double`|This parameter is the scale you want to zoom into.|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want zoomed into the scale.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the geographic unit of the world extent parameter.|
|screenWidth|`Single`|This parameter is the screen width.|
|screenHeight|`Single`|This parameter is the screen height.|

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
#### `OnAdornmentLayerDrawing(DrawingAdornmentLayerEventArgs)`

**Summary**

   *This event is raised before an AdornmentLayer is drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`DrawingAdornmentLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAdornmentLayerEventArgs.md)|The AdornmentLayerDrawingEventArgs passed for the event raised.|

---
#### `OnAdornmentLayerDrawn(DrawnAdornmentLayerEventArgs)`

**Summary**

   *This event is raised after an AdornmentLayer is drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`DrawnAdornmentLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAdornmentLayerEventArgs.md)|The AdornmentLayerDrawnEventArgs passed for the event raised.|

---
#### `OnAdornmentLayersDrawing(DrawingAdornmentLayersEventArgs)`

**Summary**

   *This event is raised before AdornmentLayers are drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`DrawingAdornmentLayersEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAdornmentLayersEventArgs.md)|The AdornmentLayersDrawingEventArgs passed for the event raised.|

---
#### `OnAdornmentLayersDrawn(DrawnAdornmentLayersEventArgs)`

**Summary**

   *This event is raised after AdornmentLayers are drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`DrawnAdornmentLayersEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAdornmentLayersEventArgs.md)|The AdornmentLayersDrawnEventArgs passed for the event raised.|

---
#### `OnLayerDrawing(LayerDrawingEventArgs)`

**Summary**

   *This event is raised before a Layer is drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`LayerDrawingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayerDrawingEventArgs.md)|The LayerDrawingEventArgs passed for the event raised.|

---
#### `OnLayerDrawn(LayerDrawnEventArgs)`

**Summary**

   *This event is raised after a Layer is drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`LayerDrawnEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayerDrawnEventArgs.md)|The LayerDrawnEventArgs passed for the event raised.|

---
#### `OnLayersDrawing(LayersDrawingEventArgs)`

**Summary**

   *This event is raised before Layers are drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`LayersDrawingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayersDrawingEventArgs.md)|The LayersDrawingEventArgs passed for the event raised.|

---
#### `OnLayersDrawn(LayersDrawnEventArgs)`

**Summary**

   *This event is raised after Layers are drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`LayersDrawnEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayersDrawnEventArgs.md)|The LayersDrawnEventArgs passed for the event raised.|

---

### Public Events

#### LayersDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`LayersDrawingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayersDrawingEventArgs.md)

#### LayersDrawn

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`LayersDrawnEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayersDrawnEventArgs.md)

#### LayerDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`LayerDrawingEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayerDrawingEventArgs.md)

#### LayerDrawn

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`LayerDrawnEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.LayerDrawnEventArgs.md)

#### AdornmentLayersDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingAdornmentLayersEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAdornmentLayersEventArgs.md)

#### AdornmentLayersDrawn

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnAdornmentLayersEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAdornmentLayersEventArgs.md)

#### AdornmentLayerDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingAdornmentLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingAdornmentLayerEventArgs.md)

#### AdornmentLayerDrawn

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawnAdornmentLayerEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawnAdornmentLayerEventArgs.md)


