# DrawingVisualGeoCanvas


## Inheritance Hierarchy

+ `Object`
  + [`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)
    + **`DrawingVisualGeoCanvas`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`DrawingVisualGeoCanvas()`](#drawingvisualgeocanvas)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CancellationTokenSource`](#cancellationtokensource)|`CancellationTokenSource`|N/A|
|[`CurrentScale`](#currentscale)|`Double`|N/A|
|[`CurrentWorldExtent`](#currentworldextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|[`Dpi`](#dpi)|`Single`|The DPI value for the final drawing result, only valid when HasDpi set to true.|
|[`DrawingQuality`](#drawingquality)|[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)|N/A|
|[`FontDisplayDensity`](#fontdisplaydensity)|`Double`|N/A|
|[`Height`](#height)|`Single`|N/A|
|[`IsDrawing`](#isdrawing)|`Boolean`|N/A|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|N/A|
|[`MapUnit`](#mapunit)|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|[`NativeImage`](#nativeimage)|`Object`|N/A|
|[`ScaleFactor`](#scalefactor)|`Single`|N/A|
|[`SupportKeyColor`](#supportkeycolor)|`Boolean`|N/A|
|[`Width`](#width)|`Single`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`OnlyDrawingCrossLabels`](#onlydrawingcrosslabels)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`BeginDrawing(Object,RectangleShape,GeographyUnit)`](#begindrawingobjectrectangleshapegeographyunit)|
|[`Clear(GeoBrush)`](#cleargeobrush)|
|[`DrawArc(GeoPen,DrawingRectangleF,Single,Single,DrawingLevel)`](#drawarcgeopendrawingrectanglefsinglesingledrawinglevel)|
|[`DrawArc(GeoPen,Single,Single,Single,Single,Single,Single,DrawingLevel)`](#drawarcgeopensinglesinglesinglesinglesinglesingledrawinglevel)|
|[`DrawArea(Feature,GeoPen,DrawingLevel)`](#drawareafeaturegeopendrawinglevel)|
|[`DrawArea(AreaBaseShape,GeoPen,DrawingLevel)`](#drawareaareabaseshapegeopendrawinglevel)|
|[`DrawArea(Feature,GeoBrush,DrawingLevel)`](#drawareafeaturegeobrushdrawinglevel)|
|[`DrawArea(AreaBaseShape,GeoBrush,DrawingLevel)`](#drawareaareabaseshapegeobrushdrawinglevel)|
|[`DrawArea(Feature,GeoPen,GeoBrush,DrawingLevel)`](#drawareafeaturegeopengeobrushdrawinglevel)|
|[`DrawArea(AreaBaseShape,GeoPen,GeoBrush,DrawingLevel)`](#drawareaareabaseshapegeopengeobrushdrawinglevel)|
|[`DrawArea(Feature,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawareafeaturegeopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawArea(AreaBaseShape,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawareaareabaseshapegeopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawArea(IEnumerable<ScreenPointF[]>,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawareaienumerable<screenpointf[]>geopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawEllipse(Feature,Single,Single,GeoPen,DrawingLevel)`](#drawellipsefeaturesinglesinglegeopendrawinglevel)|
|[`DrawEllipse(PointBaseShape,Single,Single,GeoPen,DrawingLevel)`](#drawellipsepointbaseshapesinglesinglegeopendrawinglevel)|
|[`DrawEllipse(Feature,Single,Single,GeoBrush,DrawingLevel)`](#drawellipsefeaturesinglesinglegeobrushdrawinglevel)|
|[`DrawEllipse(PointBaseShape,Single,Single,GeoBrush,DrawingLevel)`](#drawellipsepointbaseshapesinglesinglegeobrushdrawinglevel)|
|[`DrawEllipse(Feature,Single,Single,GeoPen,GeoBrush,DrawingLevel)`](#drawellipsefeaturesinglesinglegeopengeobrushdrawinglevel)|
|[`DrawEllipse(PointBaseShape,Single,Single,GeoPen,GeoBrush,DrawingLevel)`](#drawellipsepointbaseshapesinglesinglegeopengeobrushdrawinglevel)|
|[`DrawEllipse(Feature,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawellipsefeaturesinglesinglegeopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawEllipse(PointBaseShape,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawellipsepointbaseshapesinglesinglegeopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawEllipse(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawellipsescreenpointfsinglesinglegeopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawLine(Feature,GeoPen,DrawingLevel)`](#drawlinefeaturegeopendrawinglevel)|
|[`DrawLine(LineBaseShape,GeoPen,DrawingLevel)`](#drawlinelinebaseshapegeopendrawinglevel)|
|[`DrawLine(Feature,GeoPen,DrawingLevel,Single,Single)`](#drawlinefeaturegeopendrawinglevelsinglesingle)|
|[`DrawLine(LineBaseShape,GeoPen,DrawingLevel,Single,Single)`](#drawlinelinebaseshapegeopendrawinglevelsinglesingle)|
|[`DrawLine(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`](#drawlineienumerable<screenpointf>geopendrawinglevelsinglesingle)|
|[`DrawScreenImage(GeoImage,Single,Single,Single,Single,DrawingLevel,Single,Single,Single)`](#drawscreenimagegeoimagesinglesinglesinglesingledrawinglevelsinglesinglesingle)|
|[`DrawScreenImageWithoutScaling(GeoImage,Single,Single,DrawingLevel,Single,Single,Single)`](#drawscreenimagewithoutscalinggeoimagesinglesingledrawinglevelsinglesinglesingle)|
|[`DrawText(String,GeoFont,GeoBrush,IEnumerable<ScreenPointF>,DrawingLevel)`](#drawtextstringgeofontgeobrushienumerable<screenpointf>drawinglevel)|
|[`DrawText(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment)`](#drawtextstringgeofontgeobrushgeopenienumerable<screenpointf>drawinglevelsinglesingledrawingtextalignment)|
|[`DrawText(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment,Single)`](#drawtextstringgeofontgeobrushgeopenienumerable<screenpointf>drawinglevelsinglesingledrawingtextalignmentsingle)|
|[`DrawTextWithScreenCoordinate(String,GeoFont,GeoBrush,Single,Single,DrawingLevel)`](#drawtextwithscreencoordinatestringgeofontgeobrushsinglesingledrawinglevel)|
|[`DrawTextWithScreenCoordinate(String,GeoFont,GeoBrush,GeoPen,Single,Single,DrawingLevel)`](#drawtextwithscreencoordinatestringgeofontgeobrushgeopensinglesingledrawinglevel)|
|[`DrawTextWithWorldCoordinate(String,GeoFont,GeoBrush,Double,Double,DrawingLevel)`](#drawtextwithworldcoordinatestringgeofontgeobrushdoubledoubledrawinglevel)|
|[`DrawTextWithWorldCoordinate(String,GeoFont,GeoBrush,GeoPen,Double,Double,DrawingLevel)`](#drawtextwithworldcoordinatestringgeofontgeobrushgeopendoubledoubledrawinglevel)|
|[`DrawWorldImage(GeoImage,Double,Double,Single,Single,DrawingLevel)`](#drawworldimagegeoimagedoubledoublesinglesingledrawinglevel)|
|[`DrawWorldImage(GeoImage,Double,Double,Double,DrawingLevel,Single,Single,Single)`](#drawworldimagegeoimagedoubledoubledoubledrawinglevelsinglesinglesingle)|
|[`DrawWorldImage(GeoImage,Double,Double,Single,Single,DrawingLevel,Single,Single,Single)`](#drawworldimagegeoimagedoubledoublesinglesingledrawinglevelsinglesinglesingle)|
|[`DrawWorldImageWithoutScaling(GeoImage,Double,Double,DrawingLevel)`](#drawworldimagewithoutscalinggeoimagedoubledoubledrawinglevel)|
|[`DrawWorldImageWithoutScaling(GeoImage,Double,Double,DrawingLevel,Single,Single,Single)`](#drawworldimagewithoutscalinggeoimagedoubledoubledrawinglevelsinglesinglesingle)|
|[`EndDrawing()`](#enddrawing)|
|[`Equals(Object)`](#equalsobject)|
|[`Flush()`](#flush)|
|[`GetHashCode()`](#gethashcode)|
|[`GetType()`](#gettype)|
|[`MeasureText(String,GeoFont)`](#measuretextstringgeofont)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`BeginDrawingCore(Object,RectangleShape,GeographyUnit)`](#begindrawingcoreobjectrectangleshapegeographyunit)|
|[`CalculateYOffsetWithTextBaseline(GeoFont,DrawingTextBaseline,String)`](#calculateyoffsetwithtextbaselinegeofontdrawingtextbaselinestring)|
|[`ClearCore(GeoBrush)`](#clearcoregeobrush)|
|[`DrawArcCore(GeoPen,Single,Single,Single,Single,Single,Single,DrawingLevel)`](#drawarccoregeopensinglesinglesinglesinglesinglesingledrawinglevel)|
|[`DrawAreaCore(IEnumerable<ScreenPointF[]>,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawareacoreienumerable<screenpointf[]>geopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawEllipseCore(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawellipsecorescreenpointfsinglesinglegeopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawLineCore(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`](#drawlinecoreienumerable<screenpointf>geopendrawinglevelsinglesingle)|
|[`DrawScreenImageCore(GeoImage,Single,Single,Single,Single,DrawingLevel,Single,Single,Single)`](#drawscreenimagecoregeoimagesinglesinglesinglesingledrawinglevelsinglesinglesingle)|
|[`DrawScreenImageWithoutScalingCore(GeoImage,Single,Single,DrawingLevel,Single,Single,Single)`](#drawscreenimagewithoutscalingcoregeoimagesinglesingledrawinglevelsinglesinglesingle)|
|[`DrawTextCore(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment,Single)`](#drawtextcorestringgeofontgeobrushgeopenienumerable<screenpointf>drawinglevelsinglesingledrawingtextalignmentsingle)|
|[`EndDrawingCore()`](#enddrawingcore)|
|[`Finalize()`](#finalize)|
|[`FlushCore()`](#flushcore)|
|[`GetCanvasHeight()`](#getcanvasheight)|
|[`GetCanvasHeightCore()`](#getcanvasheightcore)|
|[`GetCanvasWidth()`](#getcanvaswidth)|
|[`GetCanvasWidthCore()`](#getcanvaswidthcore)|
|[`MeasureTextCore(String,GeoFont)`](#measuretextcorestringgeofont)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnDrawingProgressChanged(DrawingProgressChangedEventArgs)`](#ondrawingprogresschangeddrawingprogresschangedeventargs)|
|[`ToWorldCoordinate(DrawingRectangleF)`](#toworldcoordinatedrawingrectanglef)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`DrawingProgressChanged`](#drawingprogresschanged)|[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`DrawingVisualGeoCanvas()`](#drawingvisualgeocanvas)|

### Protected Constructors


### Public Properties

#### `CancellationTokenSource`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`CancellationTokenSource`

---
#### `CurrentScale`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `CurrentWorldExtent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `Dpi`

**Summary**

   *The DPI value for the final drawing result, only valid when HasDpi set to true.*

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
#### `FontDisplayDensity`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Height`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `IsDrawing`

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
#### `MapUnit`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)

---
#### `NativeImage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `ScaleFactor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `SupportKeyColor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Width`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---

### Protected Properties

#### `OnlyDrawingCrossLabels`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---

### Public Methods

#### `BeginDrawing(Object,RectangleShape,GeographyUnit)`

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
|geoImage|`Object`|N/A|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|drawingMapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `Clear(GeoBrush)`

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
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|

---
#### `DrawArc(GeoPen,DrawingRectangleF,Single,Single,DrawingLevel)`

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
|pen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|rect|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|N/A|
|startAngle|`Single`|N/A|
|sweepAngle|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArc(GeoPen,Single,Single,Single,Single,Single,Single,DrawingLevel)`

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
|pen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|x|`Single`|N/A|
|y|`Single`|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|startAngle|`Single`|N/A|
|sweepAngle|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArea(Feature,GeoPen,DrawingLevel)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArea(AreaBaseShape,GeoPen,DrawingLevel)`

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
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArea(Feature,GeoBrush,DrawingLevel)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArea(AreaBaseShape,GeoBrush,DrawingLevel)`

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
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArea(Feature,GeoPen,GeoBrush,DrawingLevel)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArea(AreaBaseShape,GeoPen,GeoBrush,DrawingLevel)`

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
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawArea(Feature,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|N/A|

---
#### `DrawArea(AreaBaseShape,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

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
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|N/A|

---
#### `DrawArea(IEnumerable<ScreenPointF[]>,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

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
|screenPoints|IEnumerable<[`ScreenPointF[]`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|N/A|

---
#### `DrawEllipse(Feature,Single,Single,GeoPen,DrawingLevel)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoPen,DrawingLevel)`

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
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawEllipse(Feature,Single,Single,GeoBrush,DrawingLevel)`

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
|centerPointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoBrush,DrawingLevel)`

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
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawEllipse(Feature,Single,Single,GeoPen,GeoBrush,DrawingLevel)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoPen,GeoBrush,DrawingLevel)`

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
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawEllipse(Feature,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|N/A|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

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
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|N/A|

---
#### `DrawEllipse(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

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
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|N/A|

---
#### `DrawLine(Feature,GeoPen,DrawingLevel)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawLine(LineBaseShape,GeoPen,DrawingLevel)`

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
|shape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawLine(Feature,GeoPen,DrawingLevel,Single,Single)`

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
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|

---
#### `DrawLine(LineBaseShape,GeoPen,DrawingLevel,Single,Single)`

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
|shape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|N/A|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|

---
#### `DrawLine(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`

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
|screenPoints|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|N/A|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|

---
#### `DrawScreenImage(GeoImage,Single,Single,Single,Single,DrawingLevel,Single,Single,Single)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|centerXInScreen|`Single`|N/A|
|centerYInScreen|`Single`|N/A|
|widthInScreen|`Single`|N/A|
|heightInScreen|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|rotateAngle|`Single`|N/A|

---
#### `DrawScreenImageWithoutScaling(GeoImage,Single,Single,DrawingLevel,Single,Single,Single)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|centerXInScreen|`Single`|N/A|
|centerYInScreen|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|rotateAngle|`Single`|N/A|

---
#### `DrawText(String,GeoFont,GeoBrush,IEnumerable<ScreenPointF>,DrawingLevel)`

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
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|textPathInScreen|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawText(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment)`

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
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|textPathInScreen|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|drawingTextAlignment|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|N/A|

---
#### `DrawText(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment,Single)`

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
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|textPathInScreen|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|drawingTextAlignment|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|N/A|
|rotateAngle|`Single`|N/A|

---
#### `DrawTextWithScreenCoordinate(String,GeoFont,GeoBrush,Single,Single,DrawingLevel)`

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
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|upperLeftXInScreen|`Single`|N/A|
|upperLeftYInScreen|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawTextWithScreenCoordinate(String,GeoFont,GeoBrush,GeoPen,Single,Single,DrawingLevel)`

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
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|upperLeftXInScreen|`Single`|N/A|
|upperLeftYInScreen|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawTextWithWorldCoordinate(String,GeoFont,GeoBrush,Double,Double,DrawingLevel)`

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
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|upperLeftXInWorld|`Double`|N/A|
|upperLeftYInWorld|`Double`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawTextWithWorldCoordinate(String,GeoFont,GeoBrush,GeoPen,Double,Double,DrawingLevel)`

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
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|upperLeftXInWorld|`Double`|N/A|
|upperLeftYInWorld|`Double`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawWorldImage(GeoImage,Double,Double,Single,Single,DrawingLevel)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|centerXInWorld|`Double`|N/A|
|centerYInWorld|`Double`|N/A|
|widthInScreen|`Single`|N/A|
|heightInScreen|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawWorldImage(GeoImage,Double,Double,Double,DrawingLevel,Single,Single,Single)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|centerXInWorld|`Double`|N/A|
|centerYInWorld|`Double`|N/A|
|imageScale|`Double`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|rotateAngle|`Single`|N/A|

---
#### `DrawWorldImage(GeoImage,Double,Double,Single,Single,DrawingLevel,Single,Single,Single)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|centerXInWorld|`Double`|N/A|
|centerYInWorld|`Double`|N/A|
|widthInScreen|`Single`|N/A|
|heightInScreen|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|rotateAngle|`Single`|N/A|

---
#### `DrawWorldImageWithoutScaling(GeoImage,Double,Double,DrawingLevel)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|centerXInWorld|`Double`|N/A|
|centerYInWorld|`Double`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawWorldImageWithoutScaling(GeoImage,Double,Double,DrawingLevel,Single,Single,Single)`

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
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|centerXInWorld|`Double`|N/A|
|centerYInWorld|`Double`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffset|`Single`|N/A|
|yOffset|`Single`|N/A|
|rotateAngle|`Single`|N/A|

---
#### `EndDrawing()`

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
#### `MeasureText(String,GeoFont)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|

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

#### `BeginDrawingCore(Object,RectangleShape,GeographyUnit)`

**Summary**

   *This method converts a NativeImage to a commonly-used object. In GdiPlus, this object is often an Image control in Wpf.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|nativeImage|`Object`|N/A|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|drawingMapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `CalculateYOffsetWithTextBaseline(GeoFont,DrawingTextBaseline,String)`

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
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|textBaseline|[`DrawingTextBaseline`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextBaseline.md)|N/A|
|text|`String`|N/A|

---
#### `ClearCore(GeoBrush)`

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
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|

---
#### `DrawArcCore(GeoPen,Single,Single,Single,Single,Single,Single,DrawingLevel)`

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
|pen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|x|`Single`|N/A|
|y|`Single`|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|startAngle|`Single`|N/A|
|sweepAngle|`Single`|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawAreaCore(IEnumerable<ScreenPointF[]>,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *This method draws the area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoints|IEnumerable<[`ScreenPointF[]`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter is the area point in screen coordinates.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the area.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the GeoBrush that will be used to draw the area.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the level for drawing.|
|xOffset|`Single`|This parameter determines the X offset for the area to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the area to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines pen and brush drawing order.|

---
#### `DrawEllipseCore(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *Draws the point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This parameter is the point in screen coordinates.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen or GeoBrush will draw on.|
|xOffset|`Single`|This parameter determines the X offset of the ellipse to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset of the ellipse to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines pen and brush drawing order.|

---
#### `DrawLineCore(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`

**Summary**

   *Draws a LineShape on the GeoCanvas.*

**Remarks**

   *This method is used to draw a line on the GeoCanvas using the specified GeoPen.The DrawingLevel allows you to specify the level you will draw on when drawing multiple lines. This is very useful when you want to draw a road, for example. You can draw the black background on the lowest level, then draw a slightly thinner white line on a higher level. This will result in a great effect for a road.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoints|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter represents the line points in screen coordinates.|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the GeoPen that will be used to draw the LineShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the line to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the line to be drawn.|

---
#### `DrawScreenImageCore(GeoImage,Single,Single,Single,Single,DrawingLevel,Single,Single,Single)`

**Summary**

   *Draws a scaled image on the GeoCanvas.*

**Remarks**

   *Drawing an image scaled is slower than using the API that draws it unscaled.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The image you want to draw.|
|centerXInScreen|`Single`|The X coordinate of the center point (in screen coordinates) of where you want to draw the image.|
|centerYInScreen|`Single`|The Y coordinate of the center point (in screen coordinates) of where you want to draw the image.|
|widthInScreen|`Single`|The width you want to scale the image to. This is the width at which the image will be drawn.|
|heightInScreen|`Single`|The height you want to scale the image to. This is the height at which the image will be drawn.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the image to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the image to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the image to be drawn.|

---
#### `DrawScreenImageWithoutScalingCore(GeoImage,Single,Single,DrawingLevel,Single,Single,Single)`

**Summary**

   *Draws an unscaled image on the GeoCanvas.*

**Remarks**

   *Drawing an image unscaled is faster than using the API that scales it.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The image you want to draw unscaled.|
|centerXInScreen|`Single`|The X coordinate of the center point (in screen coordinates) of where you want to draw the image.|
|centerYInScreen|`Single`|The Y coordinate of the center point (in screen coordinates) of where you want to draw the image.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the image to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the image to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the image to be drawn.|

---
#### `DrawTextCore(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment,Single)`

**Summary**

   *This method allows you to draw text at the specified location, using the specified brush and font parameters.*

**Remarks**

   *This method is used to draw text on the GeoCanvas.The DrawingLevel allows you to specify the level you will draw on when drawing multiple text items. This is very useful when you want to draw a drop shadow, for example. You can draw the black backdrop on the lowest level with an offset, then draw the normal text on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|text|`String`|This parameter specifies the text you wish to draw.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|This parameter represents the font you wish to use to draw the text.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter specifies the kind of fill you want to use to draw the text.|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter specifies the HaloPen that will be used to draw the text, when the HaloPen effect is needed.|
|textPathInScreen|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter specifies the path on which to draw the text.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter specifies the drawing level you wish to draw the text on. Higher levels overwrite lower levels.|
|xOffset|`Single`|This parameter determines the X offset for the text to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the text to be drawn.|
|drawingTextAlignment|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|N/A|
|rotateAngle|`Single`|This parameter determines the rotation angle for the text to be drawn.|

---
#### `EndDrawingCore()`

**Summary**

   *This method ends drawing and commits the drawing on the GeoCanvas.*

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
#### `FlushCore()`

**Summary**

   *This method flush drawing and commits the drawing on the GeoCanvas.*

**Remarks**

   *This method should be called when you are finished drawing. It will commit the image changes to the image you passed in on BeginDrawing. It will also set IsDrawing to false. After you call this method it will put the GeoCanvas into an invalid state, so if you then call any drawing methods it will raise an exception.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCanvasHeight()`

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
#### `GetCanvasHeightCore()`

**Summary**

   *This method gets the canvas height of the specified native image object.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Single`|The returning canvas height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCanvasWidth()`

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
#### `GetCanvasWidthCore()`

**Summary**

   *This method gets the canvas width of the specified native image object.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Single`|The returning canvas width.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `MeasureTextCore(String,GeoFont)`

**Summary**

   *This method returns the rectangle that contains the specified text, when that text is drawn with the specified font.*

**Remarks**

   *This method is typically used for labeling, to determine whether labels overlap.*

**Return Value**

|Type|Description|
|---|---|
|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|This method returns the rectangle that contains the specified text, when that text is drawn with the specified font.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|text|`String`|This parameter represents the text you want to measure.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|This parameter represents the font of the text you want to measure.|

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
#### `ToWorldCoordinate(DrawingRectangleF)`

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
|drawingRectangle|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|N/A|

---

### Public Events

#### DrawingProgressChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)


