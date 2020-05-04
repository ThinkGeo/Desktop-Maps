# SkiaGeoCanvas


## Inheritance Hierarchy

+ `Object`
  + [`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)
    + **`SkiaGeoCanvas`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`SkiaGeoCanvas()`](#skiageocanvas)|

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
|[`Dpi`](#dpi)|`Single`|N/A|
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
|[`DrawColorizeImage(GeoImage,Int32,Int32,Int32,Int32,GeoColorMap[],DrawingLevel)`](#drawcolorizeimagegeoimageint32int32int32int32geocolormap[]drawinglevel)|
|[`DrawEllipseCore(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`](#drawellipsecorescreenpointfsinglesinglegeopengeobrushdrawinglevelsinglesinglepenbrushdrawingorder)|
|[`DrawLineCore(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`](#drawlinecoreienumerable<screenpointf>geopendrawinglevelsinglesingle)|
|[`DrawRadialGradient(ScreenPointF,Int32,Single[],GeoColor[],DrawingLevel)`](#drawradialgradientscreenpointfint32single[]geocolor[]drawinglevel)|
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
|[`SkiaGeoCanvas()`](#skiageocanvas)|

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
#### `DrawColorizeImage(GeoImage,Int32,Int32,Int32,Int32,GeoColorMap[],DrawingLevel)`

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
|srcX|`Int32`|N/A|
|srcY|`Int32`|N/A|
|srcWidth|`Int32`|N/A|
|srcHeight|`Int32`|N/A|
|colors|[`GeoColorMap[]`](../ThinkGeo.Core/ThinkGeo.Core.GeoColorMap.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawEllipseCore(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

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
#### `DrawLineCore(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`

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
#### `DrawRadialGradient(ScreenPointF,Int32,Single[],GeoColor[],DrawingLevel)`

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
|centerPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|radius|`Int32`|N/A|
|positions|`Single[]`|N/A|
|colors|[`GeoColor[]`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|

---
#### `DrawScreenImageCore(GeoImage,Single,Single,Single,Single,DrawingLevel,Single,Single,Single)`

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
#### `DrawScreenImageWithoutScalingCore(GeoImage,Single,Single,DrawingLevel,Single,Single,Single)`

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
#### `DrawTextCore(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment,Single)`

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
#### `EndDrawingCore()`

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
#### `FlushCore()`

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
#### `MeasureTextCore(String,GeoFont)`

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


