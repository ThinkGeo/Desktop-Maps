# GeoCanvas


## Inheritance Hierarchy

+ `Object`
  + **`GeoCanvas`**
    + [`SkiaGeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.SkiaGeoCanvas.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`GeoCanvas()`](#geocanvas)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CancellationTokenSource`](#cancellationtokensource)|`CancellationTokenSource`|Gets or sets the cancellation token source.|
|[`CurrentScale`](#currentscale)|`Double`|This property gets the current scale of the view.|
|[`CurrentWorldExtent`](#currentworldextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This property gets the adjusted current extent based on what was set when BeginDrawing was called.|
|[`Dpi`](#dpi)|`Single`|The DPI value for the final drawing result, only valid when HasDpi set to true.|
|[`DrawingQuality`](#drawingquality)|[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)|This property returns the drawing quality when rendering on the GeoCanvas.|
|[`FontDisplayDensity`](#fontdisplaydensity)|`Double`|This property gets and sets the current device pixel ratio of platform. It would be used by scale text and icon from map. Android: it's "Application.Context.Resources.DisplayMetrics.Density". iOS: it's "UIScreen.MainScreen.Scale". Others: it's default value "1".|
|[`Height`](#height)|`Single`|This property gets the height of the view.|
|[`IsDrawing`](#isdrawing)|`Boolean`|This property gets the drawing status of the GeoCanvas.|
|[`KeyColors`](#keycolors)|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|Gets a value represents a collection of key colors. If SupportKeyColor property is false, it will throw exception when you use KeyColors.|
|[`MapUnit`](#mapunit)|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This property returns the MapUnit passed in on the BeginDrawingAPI in the GeoCanvas.|
|[`NativeImage`](#nativeimage)|`Object`|The same reference to the parameter 'NativeImage' in BeginDrawing function.|
|[`ScaleFactor`](#scalefactor)|`Single`|Gets or sets the scale factor, this is a value number of device pixels per logical coordinate point.|
|[`SupportKeyColor`](#supportkeycolor)|`Boolean`|This property indicates whether a GeoCanvas has the KeyColor or not. If it has no KeyColor, it will throw an exception when you get or set the value of KeyColors property.|
|[`Width`](#width)|`Single`|This property gets the width of the view.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`IsCancelled`](#iscancelled)|`Boolean`|N/A|
|[`OnlyDrawingCrossLabels`](#onlydrawingcrosslabels)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`BeginDrawing(Object,RectangleShape,GeographyUnit)`](#begindrawingobjectrectangleshapegeographyunit)|
|[`Clear(GeoBrush)`](#cleargeobrush)|
|[`CreateDefaultGeoCanvas()`](#createdefaultgeocanvas)|
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
|N/A|

### Protected Constructors

#### `GeoCanvas()`

**Summary**

   *This method is the default constructor for the GeoCanvas.*

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

#### `CancellationTokenSource`

**Summary**

   *Gets or sets the cancellation token source.*

**Remarks**

   *N/A*

**Return Value**

`CancellationTokenSource`

---
#### `CurrentScale`

**Summary**

   *This property gets the current scale of the view.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `CurrentWorldExtent`

**Summary**

   *This property gets the adjusted current extent based on what was set when BeginDrawing was called.*

**Remarks**

   *The extent that gets passed in on BeginDrawing is adjusted for the height and width of the physical media being drawn on. For example if the current extent is wider than taller but the bitmap being drawn on is square then the current extent needs to be adjusted. The extent will be adjusted larger so that we ensure that the entire original extent will still be represented.*

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

   *This property returns the drawing quality when rendering on the GeoCanvas.*

**Remarks**

   *The DrawingQuality specifies whether BaseLineShapes, BasePointShapes, and BaseAreaShapes use anti-aliasing methods or other techniques that control the quality. In some cases you may want a higher quality rendering, and in other cases higher speed is more desirable. It is up to the implementer of the derived GeoCanvas class to control exactly what this setting means.*

**Return Value**

[`DrawingQuality`](../ThinkGeo.Core/ThinkGeo.Core.DrawingQuality.md)

---
#### `FontDisplayDensity`

**Summary**

   *This property gets and sets the current device pixel ratio of platform. It would be used by scale text and icon from map. Android: it's "Application.Context.Resources.DisplayMetrics.Density". iOS: it's "UIScreen.MainScreen.Scale". Others: it's default value "1".*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Height`

**Summary**

   *This property gets the height of the view.*

**Remarks**

   *This property reflects the height of the view image that was passed in on BeginDrawing.*

**Return Value**

`Single`

---
#### `IsDrawing`

**Summary**

   *This property gets the drawing status of the GeoCanvas.*

**Remarks**

   *This property is set to true when the BeginDrawing method is called, and false after the EndDrawing method is called.*

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
#### `MapUnit`

**Summary**

   *This property returns the MapUnit passed in on the BeginDrawingAPI in the GeoCanvas.*

**Remarks**

   *N/A*

**Return Value**

[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)

---
#### `NativeImage`

**Summary**

   *The same reference to the parameter 'NativeImage' in BeginDrawing function.*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `ScaleFactor`

**Summary**

   *Gets or sets the scale factor, this is a value number of device pixels per logical coordinate point.*

**Remarks**

   *The coordinate space used by application developers is measured in logical points. High-resolution (Retina) displays will have more than a single physical pixel per logical point and this property specifies the scale factor.*

**Return Value**

`Single`

---
#### `SupportKeyColor`

**Summary**

   *This property indicates whether a GeoCanvas has the KeyColor or not. If it has no KeyColor, it will throw an exception when you get or set the value of KeyColors property.*

**Remarks**

   *The default value is false.*

**Return Value**

`Boolean`

---
#### `Width`

**Summary**

   *This property gets the width of the view.*

**Remarks**

   *This property reflects the width of the view image that was passed in on BeginDrawing.*

**Return Value**

`Single`

---

### Protected Properties

#### `IsCancelled`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
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

   *This method begins the act of drawing on the GeoCanvas.*

**Remarks**

   *This is the first method that needs to be called before any drawing takes place. Calling this method will set the IsDrawing property to true. When you finish drawing, you must call EndDrawing to commit the changes to the image.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|geoImage|`Object`|This parameter represents the image you want the GeoCanvas to draw on.|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent of the canvasImage.|
|drawingMapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the map unit of the canvasImage.|

---
#### `Clear(GeoBrush)`

**Summary**

   *This method clears the current GeoCanvas using the color specified.*

**Remarks**

   *Use this method to clear the GeoCanvas.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|

---
#### `CreateDefaultGeoCanvas()`

**Summary**

   *Creates the default geo canvas.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)||

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DrawArc(GeoPen,DrawingRectangleF,Single,Single,DrawingLevel)`

**Summary**

   *Draws the arc.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|The pen.|
|rect|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|The rect.|
|startAngle|`Single`|The start angle.|
|sweepAngle|`Single`|The sweep angle.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|The drawing level.|

---
#### `DrawArc(GeoPen,Single,Single,Single,Single,Single,Single,DrawingLevel)`

**Summary**

   *Draws the arc.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|The pen.|
|x|`Single`|The x.|
|y|`Single`|The y.|
|width|`Single`|The width.|
|height|`Single`|The height.|
|startAngle|`Single`|The start angle.|
|sweepAngle|`Single`|The sweep angle.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|The drawing level.|

---
#### `DrawArea(Feature,GeoPen,DrawingLevel)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the area feature.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the area.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawArea(AreaBaseShape,GeoPen,DrawingLevel)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter is the area shape.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the area.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawArea(Feature,GeoBrush,DrawingLevel)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the AreaShape in well-known binary format.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawArea(AreaBaseShape,GeoBrush,DrawingLevel)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter is the area shape to be drawn.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill Brush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawArea(Feature,GeoPen,GeoBrush,DrawingLevel)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the AreaFeature to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the AreaShape.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawArea(AreaBaseShape,GeoPen,GeoBrush,DrawingLevel)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter is the area shape to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline pen that will be used to draw the AreaShape.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill Brush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawArea(Feature,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the AreaFeature to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the AreaShape.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature that will be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature that will be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines the PenBrushingDrawingOrder used when drawing the area type feature.|

---
#### `DrawArea(AreaBaseShape,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|This parameter is the area shape to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the AreaShape.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines the PenBrushDrawingOrder used when drawing the area type feature.|

---
#### `DrawArea(IEnumerable<ScreenPointF[]>,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoints|IEnumerable<[`ScreenPointF[]`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter is the AreaShape in well-known binary format.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the AreaShape.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines the PenBrushDrawingOrder used when drawing the area type feature.|

---
#### `DrawEllipse(Feature,Single,Single,GeoPen,DrawingLevel)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the center point feature.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoPen,DrawingLevel)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|This parameter is the center point shape.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawEllipse(Feature,Single,Single,GeoBrush,DrawingLevel)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|centerPointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the center point feature.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the the GeoBrush will draw on.|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoBrush,DrawingLevel)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|This parameter is the center point shape.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the the GeoBrush will draw on.|

---
#### `DrawEllipse(Feature,Single,Single,GeoPen,GeoBrush,DrawingLevel)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the center point feature.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen or GeoBrush will draw on.|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoPen,GeoBrush,DrawingLevel)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|This parameter is the center point shape.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen or GeoBrush will draw on.|

---
#### `DrawEllipse(Feature,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the center point feature.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen or GeoBrush will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the ellipse to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the ellipse to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines the PenBrushDrawingOrder used when drawing the ellipse.|

---
#### `DrawEllipse(PointBaseShape,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`PointBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseShape.md)|This parameter is the center point shape.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen or GeoBrush will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the ellipse to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the ellipse to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines the PenBrushDrawingOrder used when drawing the ellipse.|

---
#### `DrawEllipse(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This parameter is the center point in screen coordinate.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen or GeoBrush will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the ellipse to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the ellipse to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|N/A|

---
#### `DrawLine(Feature,GeoPen,DrawingLevel)`

**Summary**

   *Draws a line on the GeoCanvas.*

**Remarks**

   *This method is used to draw a line on the GeoCanvas using the specified GeoPen.The DrawingLevel allows you to specify the level you will draw on when drawing multiple lines. This is very useful when you want to draw a road, for example. You can draw the black background on the lowest level, then draw a slightly thinner white line on a higher level. This will result in a great effect for a road.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the line feature.|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the GeoPen that will be used to draw the line.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawLine(LineBaseShape,GeoPen,DrawingLevel)`

**Summary**

   *Draws a line on the GeoCanvas.*

**Remarks**

   *This method is used to draw a line on the GeoCanvas using the specified GeoPen.The DrawingLevel allows you to specify the level you will draw on when drawing multiple lines. This is very useful when you want to draw a road, for example. You can draw the black background on the lowest level, then draw a slightly thinner white line on a higher level. This will result in a great effect for a road.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This parameter is the line shape to be drawn by GeoCannvas.|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the GeoPen that will be used to draw the line.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|

---
#### `DrawLine(Feature,GeoPen,DrawingLevel,Single,Single)`

**Summary**

   *Draws a line on the GeoCanvas.*

**Remarks**

   *This method is used to draw a line on the GeoCanvas using the specified GeoPen.The DrawingLevel allows you to specify the level you will draw on when drawing multiple lines. This is very useful when you want to draw a road, for example. You can draw the black background on the lowest level, then draw a slightly thinner white line on a higher level. This will result in a great effect for a road.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the line feature to be drawn by GeoCannvas.|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the GeoPen that will be used to draw the line.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature to be drawn.|

---
#### `DrawLine(LineBaseShape,GeoPen,DrawingLevel,Single,Single)`

**Summary**

   *Draws a line on the GeoCanvas.*

**Remarks**

   *This method is used to draw a line on the GeoCanvas using the specified GeoPen.The DrawingLevel allows you to specify the level you will draw on when drawing multiple lines. This is very useful when you want to draw a road, for example. You can draw the black background on the lowest level, then draw a slightly thinner white line on a higher level. This will result in a great effect for a road.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`LineBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.LineBaseShape.md)|This parameter is the line shape to be drawn by GeoCannvas.|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the GeoPen that will be used to draw the line.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature to be drawn.|

---
#### `DrawLine(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`

**Summary**

   *Draws the LineShape on the GeoCanvas.*

**Remarks**

   *This method is used to draw a line on the GeoCanvas using the specified GeoPen.The DrawingLevel allows you to specify the level you will draw on when drawing multiple lines. This is very useful when you want to draw a road, for example. You can draw the black background on the lowest level, then draw a slightly thinner white line on a higher level. This will result in a great effect for a road.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoints|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter is the LineShape in well-known binary format.|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the GeoPen that will be used to draw the LineShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature to be drawn.|

---
#### `DrawScreenImage(GeoImage,Single,Single,Single,Single,DrawingLevel,Single,Single,Single)`

**Summary**

   *Draws a screen image on the GeoCanvas.*

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
|widthInScreen|`Single`|The width you want to scale the image to. This is the width of the image that will be drawn.|
|heightInScreen|`Single`|The height you want to scale the image to. This is the height of the image that will be drawn.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the image to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the image to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the image to be drawn.|

---
#### `DrawScreenImageWithoutScaling(GeoImage,Single,Single,DrawingLevel,Single,Single,Single)`

**Summary**

   *Draws an unscaled image on the GeoCanvas.*

**Remarks**

   *Drawing an image unscaled is faster than using the API that scales it.The X & Y in work coordinates is where the center of the image will be drawn.*

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
#### `DrawText(String,GeoFont,GeoBrush,IEnumerable<ScreenPointF>,DrawingLevel)`

**Summary**

   *This method allows you to draw text at the specified location, using the specified brush and font parameters.*

**Remarks**

   *N/A*

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
|textPathInScreen|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter specifies the path on which to draw the text.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter specifies the drawing level you wish to draw the text on. Higher levels overwrite lower levels.|

---
#### `DrawText(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment)`

**Summary**

   *Draws the text.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|text|`String`|The text.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|The font.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|The fill brush.|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|The halo pen.|
|textPathInScreen|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|The text path in screen.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|The drawing level.|
|xOffset|`Single`|The x offset.|
|yOffset|`Single`|The y offset.|
|drawingTextAlignment|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|The drawing text alignment.|

---
#### `DrawText(String,GeoFont,GeoBrush,GeoPen,IEnumerable<ScreenPointF>,DrawingLevel,Single,Single,DrawingTextAlignment,Single)`

**Summary**

   *This method allows you to draw text at the specified location, using the specified brush and font parameters.*

**Remarks**

   *N/A*

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
|drawingTextAlignment|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|This parameter determines text alignment for the text to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the text to be drawn.|

---
#### `DrawTextWithScreenCoordinate(String,GeoFont,GeoBrush,Single,Single,DrawingLevel)`

**Summary**

   *This method allows you to draw text at the specified location, using the specified brush and font parameters.*

**Remarks**

   *This method is used to draw text on the GeoCanvas at specific screen coordinates. It provides you with a number of overloads that allow you to control how the text is drawn. This is useful especially when adding things such as legends, titles, etc.The DrawingLevel allows you to specify the level you will draw on when drawing multiple text items. This is very useful when you want to draw a drop shadow, for example. You can draw the black backdrop on the lowest level with an offset, then draw the normal text on a higher level without an offset.*

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
|upperLeftXInScreen|`Single`|This parameter is the upper left horizontal point in screen coordinates of where you want to start drawing the text from.|
|upperLeftYInScreen|`Single`|This parameter is the upper left vertical point in screen coordinates of where you want to start drawing the text from.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter specifies the drawing level you wish to draw the text on. Higher levels overwrite lower levels.|

---
#### `DrawTextWithScreenCoordinate(String,GeoFont,GeoBrush,GeoPen,Single,Single,DrawingLevel)`

**Summary**

   *Draws the text with screen coordinate.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|text|`String`|The text.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|The font.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|The fill brush.|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|The halo pen.|
|upperLeftXInScreen|`Single`|The upper left x in screen.|
|upperLeftYInScreen|`Single`|The upper left y in screen.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|The drawing level.|

---
#### `DrawTextWithWorldCoordinate(String,GeoFont,GeoBrush,Double,Double,DrawingLevel)`

**Summary**

   *This method allows you to draw text at the specified location, using the specified brush and font parameters.*

**Remarks**

   *This method is used to draw text on the GeoCanvas at specific screen coordinates. It provides you with a number of overloads that allow you to control how the text is drawn. This is useful especially when adding things such as legends, titles, etc.The DrawingLevel allows you to specify the level you will draw on when drawing multiple text items. This is very useful when you want to draw a drop shadow, for example. You can draw the black backdrop on the lowest level with an offset, then draw the normal text on a higher level without an offset.*

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
|upperLeftXInWorld|`Double`|This parameter is the upper left horizontal point in world coordinates of where you want to start drawing the text from.|
|upperLeftYInWorld|`Double`|This parameter is the upper left horizontal point in world coordinates of where you want to start drawing the text from.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter specifies the drawing level you wish to draw the text on. Higher levels overwrite lower levels.|

---
#### `DrawTextWithWorldCoordinate(String,GeoFont,GeoBrush,GeoPen,Double,Double,DrawingLevel)`

**Summary**

   *Draws the text with world coordinate.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|text|`String`|The text.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|The font.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|The fill brush.|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|The halo pen.|
|upperLeftXInWorld|`Double`|The upper left x in world.|
|upperLeftYInWorld|`Double`|The upper left y in world.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|The drawing level.|

---
#### `DrawWorldImage(GeoImage,Double,Double,Single,Single,DrawingLevel)`

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
|centerXInWorld|`Double`|The X coordinate of the center point of where you want to draw the image.|
|centerYInWorld|`Double`|The Y coordinate of the center point of where you want to draw the image.|
|widthInScreen|`Single`|The width you want to scale the image to. This is the width of the image that will be drawn.|
|heightInScreen|`Single`|The height you want to scale the image to. This is the height of the image that will be drawn.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|

---
#### `DrawWorldImage(GeoImage,Double,Double,Double,DrawingLevel,Single,Single,Single)`

**Summary**

   *Draws a world image on the GeoCanvas.*

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
|centerXInWorld|`Double`|The X coordinate of the center point of where you want to draw the image.|
|centerYInWorld|`Double`|The Y coordinate of the center point of where you want to draw the image.|
|imageScale|`Double`|The scale at which you want to draw the image. The final width and height will be caculated based on the scale.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the image to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the image to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the image to be drawn.|

---
#### `DrawWorldImage(GeoImage,Double,Double,Single,Single,DrawingLevel,Single,Single,Single)`

**Summary**

   *Draws a world image on the GeoCanvas.*

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
|centerXInWorld|`Double`|The X coordinate of the center point of where you want to draw the image.|
|centerYInWorld|`Double`|The Y coordinate of the center point of where you want to draw the image.|
|widthInScreen|`Single`|The width you want to scale the image to. This is the width of the image that will be drawn.|
|heightInScreen|`Single`|The height you want to scale the image to. This is the height of the image that will be drawn.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the image to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the image to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the image to be drawn.|

---
#### `DrawWorldImageWithoutScaling(GeoImage,Double,Double,DrawingLevel)`

**Summary**

   *Draws an unscaled image on the GeoCanvas.*

**Remarks**

   *Drawing an image unscaled is faster than using the API that scales it.The X & Y in work coordinates is where the center of the image will be drawn.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The image you want to draw unscaled.|
|centerXInWorld|`Double`|The X coordinate of the center point of where you want to draw the image.|
|centerYInWorld|`Double`|The Y coordinate of the center point of where you want to draw the image.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|

---
#### `DrawWorldImageWithoutScaling(GeoImage,Double,Double,DrawingLevel,Single,Single,Single)`

**Summary**

   *Draws an unscaled image on the GeoCanvas.*

**Remarks**

   *Drawing an image unscaled is faster than using the API that scales it.The X & Y in work coordinates is where the center of the image will be drawn.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|image|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|The image you want to draw unscaled.|
|centerXInWorld|`Double`|The X coordinate of the center point (in world coordinates) of where you want to draw the image.|
|centerYInWorld|`Double`|The Y coordinate of the center point (in world coordinates) of where you want to draw the image.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel the image will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the image to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the image to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the image to be drawn.|

---
#### `EndDrawing()`

**Summary**

   *This method ends drawing and commits the drawing on the GeoCanvas.*

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

   *This method begins the act of drawing on the GeoCanvas.*

**Remarks**

   *This is the first method that needs to be called before any drawing takes place. Calling this method will set the IsDrawing property to true. When you finish drawing, you must call EndDrawing to commit the changes to the image.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|nativeImage|`Object`|This parameter represents the image you want the GeoCanvas to draw on.|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent of the canvasImage.|
|drawingMapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the map unit of the canvasImage.|

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

   *This method clears the current GeoCanvas using the color specified.*

**Remarks**

   *Use this method to clear the GeoCanvas.This method is designed to be overridden by the deriving class.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter specifies the the brush that will be used to clear the GeoCanvas.|

---
#### `DrawArcCore(GeoPen,Single,Single,Single,Single,Single,Single,DrawingLevel)`

**Summary**

   *Draws the arc core.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|The pen.|
|x|`Single`|The x.|
|y|`Single`|The y.|
|width|`Single`|The width.|
|height|`Single`|The height.|
|startAngle|`Single`|The start angle.|
|sweepAngle|`Single`|The sweep angle.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|The drawing level.|

---
#### `DrawAreaCore(IEnumerable<ScreenPointF[]>,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *This method draws an area on the GeoCanvas.*

**Remarks**

   *This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|This method is used to draw on the GeoCanvas. It provides you with a number of overloads that allow you to control how things are drawn. Specify the GeoBrush to fill in an area. Specify the GeoPen to outline an area using that GeoPen. You can also call an overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when you are drawing multiple areas. This is very useful when you want to draw a drop shadow, for example. In that case, you could draw the black backdrop on the lowest level with an offset, then draw the normal shape on a higher level without an offset.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoints|IEnumerable<[`ScreenPointF[]`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter is the AreaShape in well-known binary format.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the AreaShape.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the AreaShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines the PenBrushDrawingOrder used when drawing the area type feature.|

---
#### `DrawEllipseCore(ScreenPointF,Single,Single,GeoPen,GeoBrush,DrawingLevel,Single,Single,PenBrushDrawingOrder)`

**Summary**

   *Draws a point on the GeoCanvas.*

**Remarks**

   *This method is used to draw a point on the GeoCanvas. It provides you with a number of overloads that allow you to control how it is drawn. Specify the GeoBrush to fill in the area of the point. Specify the GeoPen to outline the point using that GeoPen. You can also call a overload that will allow you to specify both a GeoPen and a GeoBrush.The DrawingLevel allows you to specify the level you will draw on when drawing many points.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This parameter is the center point in well-known binary format.|
|width|`Single`|This parameter describes the width of the ellipse to be drawn.|
|height|`Single`|This parameter describes the height of the ellipse to be drawn.|
|outlinePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the outline GeoPen that will be used to draw the point.|
|fillBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This parameter describes the fill GeoBrush that will be used to draw the point.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen or GeoBrush will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the screenPoint to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the screenPoint to be drawn.|
|penBrushDrawingOrder|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This parameter determines the PenBrushDrawingOrder used when drawing the ellipse.|

---
#### `DrawLineCore(IEnumerable<ScreenPointF>,GeoPen,DrawingLevel,Single,Single)`

**Summary**

   *Draws the LineShape on the GeoCanvas.*

**Remarks**

   *This method is used to draw a line on the GeoCanvas using the specified GeoPen.The DrawingLevel allows you to specify the level you will draw on when drawing multiple lines. This is very useful when you want to draw a road, for example. You can draw the black background on the lowest level, then draw a slightly thinner white line on a higher level. This will result in a great effect for a road.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoints|IEnumerable<[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)>|This parameter is the LineShape in well-known binary format.|
|linePen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This parameter describes the GeoPen that will be used to draw the LineShape.|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|This parameter determines the DrawingLevel that the GeoPen will draw on.|
|xOffset|`Single`|This parameter determines the X offset for the feature to be drawn.|
|yOffset|`Single`|This parameter determines the Y offset for the feature to be drawn.|

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
|widthInScreen|`Single`|The width you want to scale the image to. This is the width of the image that will be drawn.|
|heightInScreen|`Single`|The height you want to scale the image to. This is the height of the image that will be drawn.|
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
|drawingTextAlignment|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|This parameter determines text alignment for the text to be drawn.|
|rotateAngle|`Single`|This parameter determines the rotation angle for the text to be drawn.|

---
#### `EndDrawingCore()`

**Summary**

   *This method ends drawing and commits the drawing on the GeoCanvas.*

**Remarks**

   *This methods should be called when you are finished drawing. It will commit the image changes to the image you passed in on BeginDrawing. It will also set IsDrawing to false. After you call this method it will put the GeoCanvas into an invalid state, so if you then call any drawing methods it will raise an exception.*

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

   *This method gets the view height of the passed-in native image object.*

**Remarks**

   *This method is a BaseClass API and will be implemented and used in its sub-concrete classes.*

**Return Value**

|Type|Description|
|---|---|
|`Single`|The returning view height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCanvasHeightCore()`

**Summary**

   *This method gets the view height of the passed-in native image object.*

**Remarks**

   *This method is a BaseClass API and will be implemented and used in its sub-concrete classes.*

**Return Value**

|Type|Description|
|---|---|
|`Single`|The returning view height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCanvasWidth()`

**Summary**

   *This method gets the view width of the passed-in native image object.*

**Remarks**

   *This method is a BaseClass API and will be implemented and used in its sub-concrete classes.*

**Return Value**

|Type|Description|
|---|---|
|`Single`|The returning view width.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetCanvasWidthCore()`

**Summary**

   *This method gets the view width of the passed-in native image object.*

**Remarks**

   *This method is a BaseClass API and will be implemented and used in its sub-concrete classes.*

**Return Value**

|Type|Description|
|---|---|
|`Single`|The returning view width.|

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

   *Raises the  event.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`DrawingProgressChangedEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingProgressChangedEventArgs.md)|The  instance containing the event data.|

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


