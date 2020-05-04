# LineStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + **`LineStyle`**
      + [`MagneticNorthLineStyle`](../ThinkGeo.Core/ThinkGeo.Core.MagneticNorthLineStyle.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`LineStyle()`](#linestyle)|
|[`LineStyle(GeoPen)`](#linestylegeopen)|
|[`LineStyle(GeoPen,GeoPen)`](#linestylegeopengeopen)|
|[`LineStyle(GeoPen,GeoPen,GeoPen)`](#linestylegeopengeopengeopen)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CenterPen`](#centerpen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This property gets and sets the center pen for the line.|
|[`CenterPenDrawingLevel`](#centerpendrawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`CustomLineStyles`](#customlinestyles)|Collection<[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)>|This property returns a collection of line styles, allowing you to stack multiple line styles on top of each other.|
|[`DirectionPointInterval`](#directionpointinterval)|`Double`|N/A|
|[`DirectionPointStyle`](#directionpointstyle)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`InnerPen`](#innerpen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This property gets and sets the inner pen for the line.|
|[`InnerPenDrawingLevel`](#innerpendrawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OuterPen`](#outerpen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This property gets and sets the outer pen for the line.|
|[`OuterPenDrawingLevel`](#outerpendrawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|This property gets and sets the X pixel offset for drawing each feature.|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|This property gets and sets the Y pixel offset for drawing each feature.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`CreateSimpleLineStyle(GeoColor,Single,Boolean)`](#createsimplelinestylegeocolorsingleboolean)|
|[`CreateSimpleLineStyle(GeoColor,Single,LineDashStyle,Boolean)`](#createsimplelinestylegeocolorsinglelinedashstyleboolean)|
|[`CreateSimpleLineStyle(GeoColor,Single,GeoColor,Single,Boolean)`](#createsimplelinestylegeocolorsinglegeocolorsingleboolean)|
|[`CreateSimpleLineStyle(GeoColor,Single,LineDashStyle,GeoColor,Single,LineDashStyle,Boolean)`](#createsimplelinestylegeocolorsinglelinedashstylegeocolorsinglelinedashstyleboolean)|
|[`CreateSimpleLineStyle(GeoColor,Single,GeoColor,Single,GeoColor,Single,Boolean)`](#createsimplelinestylegeocolorsinglegeocolorsinglegeocolorsingleboolean)|
|[`CreateSimpleLineStyle(GeoColor,Single,LineDashStyle,GeoColor,Single,LineDashStyle,GeoColor,Single,LineDashStyle,Boolean)`](#createsimplelinestylegeocolorsinglelinedashstylegeocolorsinglelinedashstylegeocolorsinglelinedashstyleboolean)|
|[`Draw(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawienumerable<feature>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`Draw(IEnumerable<BaseShape>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawienumerable<baseshape>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawSample(GeoCanvas,DrawingRectangleF)`](#drawsamplegeocanvasdrawingrectanglef)|
|[`DrawSample(GeoCanvas)`](#drawsamplegeocanvas)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetRequiredColumnNames()`](#getrequiredcolumnnames)|
|[`GetType()`](#gettype)|
|[`Parse(String)`](#parsestring)|
|[`SaveStyle(String)`](#savestylestring)|
|[`SaveStyle(Stream)`](#savestylestream)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`CreateLinePen(String[])`](#createlinepenstring[])|
|[`DrawCore(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawcoreienumerable<feature>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawSampleCore(GeoCanvas,DrawingRectangleF)`](#drawsamplecoregeocanvasdrawingrectanglef)|
|[`Finalize()`](#finalize)|
|[`GetRequiredColumnNamesCore()`](#getrequiredcolumnnamescore)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnDrawingDirectionPoint(DrawingDirectionPointEventArgs)`](#ondrawingdirectionpointdrawingdirectionpointeventargs)|
|[`Parse(JObject)`](#parsejobject)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`DrawingDirectionPoint`](#drawingdirectionpoint)|[`DrawingDirectionPointEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingDirectionPointEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`LineStyle()`](#linestyle)|
|[`LineStyle(GeoPen)`](#linestylegeopen)|
|[`LineStyle(GeoPen,GeoPen)`](#linestylegeopengeopen)|
|[`LineStyle(GeoPen,GeoPen,GeoPen)`](#linestylegeopengeopengeopen)|

### Protected Constructors


### Public Properties

#### `CenterPen`

**Summary**

   *This property gets and sets the center pen for the line.*

**Remarks**

   *You can set an inner, outer and center pen to give you a nice effect. The outer pen draws first, and should typically be black and larger then the inner pen. The inner pen draws next, and should be set as the color of the road you want. It should be thinner than the outer pen. The center pen draws last, and is used to represent a centerline in the road. We suggest that you only use the center pen for highways at low zoom level, as the dashed pen has some performance penalties.*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `CenterPenDrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `CustomLineStyles`

**Summary**

   *This property returns a collection of line styles, allowing you to stack multiple line styles on top of each other.*

**Remarks**

   *Using this collection, you can stack multiple styles on top of each other. When we draw the feature, we will draw them in order that they exist in the collection. You can use these stacks to create drop shadow effects, multiple colored outlines, etc.*

**Return Value**

Collection<[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)>

---
#### `DirectionPointInterval`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `DirectionPointStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)

---
#### `Filters`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `InnerPen`

**Summary**

   *This property gets and sets the inner pen for the line.*

**Remarks**

   *You can set an inner, outer and center pen to give you a nice effect. The outer pen draws first, and should typically be black and larger then the inner pen. The inner pen draws next, and should be set as the color of the road you want. It should be thinner than the outer pen. The center pen draws last, and is used to represent a centerline in the road. We suggest that you only use the center pen for highways at low zoom level, as the dashed pen has some performance penalties.*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `InnerPenDrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `IsActive`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `OuterPen`

**Summary**

   *This property gets and sets the outer pen for the line.*

**Remarks**

   *You can set an inner, outer and center pen to give you a nice effect. The outer pen draws first, and should typically be black and larger then the inner pen. The inner pen draws next, and should be set as the color of the road you want. It should be thinner than the outer pen. The center pen draws last, and is used to represent a centerline in the road. We suggest that you only use the center pen for highways at low zoom level, as the dashed pen has some performance penalties.*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `OuterPenDrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `RequiredColumnNames`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `XOffsetInPixel`

**Summary**

   *This property gets and sets the X pixel offset for drawing each feature.*

**Remarks**

   *This property allows you to specify an X offset. When combined with a Y offset, it is useful to allow you to achieve effects such as drop shadows, etc. There also may be times when you need to modify the location of feature data so as to better align it with raster satellite data.*

**Return Value**

`Single`

---
#### `YOffsetInPixel`

**Summary**

   *This property gets and sets the Y pixel offset for drawing each feature.*

**Remarks**

   *This property allows you to specify an Y offset. When combined with an X offset, it is useful to allow you to achieve effects such as drop shadows, etc. There also may be times when you need to modify the location of feature data so as to better align it with raster satellite data.*

**Return Value**

`Single`

---

### Protected Properties

#### `FiltersCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `IsDefault`

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
|[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CreateSimpleLineStyle(GeoColor,Single,Boolean)`

**Summary**

   *This method returns a LineStyle based on the parameters passed in.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This method returns a LineStyle based on the parameters passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|centerlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|
|centerlineWidth|`Single`|N/A|
|roundCap|`Boolean`|N/A|

---
#### `CreateSimpleLineStyle(GeoColor,Single,LineDashStyle,Boolean)`

**Summary**

   *This method returns a LineStyle based on the parameters passed in.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This method returns a LineStyle based on the parameters passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|centerlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the center line color.|
|centerlineWidth|`Single`|This paramter is the the center line width.|
|centerlineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the dash style for the center line.|
|roundCap|`Boolean`|This parameter defines whether you want a rounded end cap.|

---
#### `CreateSimpleLineStyle(GeoColor,Single,GeoColor,Single,Boolean)`

**Summary**

   *This method returns a LineStyle based on the parameters passed in.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This method returns a LineStyle based on the parameters passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|innerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the inner line color.|
|innerLineWidth|`Single`|This parameter is the inner line width.|
|outerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outer line color.|
|outerLineWidth|`Single`|This parameter is the outer line width.|
|roundCap|`Boolean`|This parameter defines whether you want a rounded end cap.|

---
#### `CreateSimpleLineStyle(GeoColor,Single,LineDashStyle,GeoColor,Single,LineDashStyle,Boolean)`

**Summary**

   *This method returns a LineStyle based on the parameters passed in.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This method returns a LineStyle based on the parameters passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|innerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the inner line color.|
|innerLineWidth|`Single`|This parameter is the inner line width.|
|innerLineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the inner line dash style.|
|outerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outer line color.|
|outerLineWidth|`Single`|This parameter is the outer line width.|
|outerLineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the outer line dash style.|
|roundCap|`Boolean`|This parameter defines whether you want a rounded end cap.|

---
#### `CreateSimpleLineStyle(GeoColor,Single,GeoColor,Single,GeoColor,Single,Boolean)`

**Summary**

   *This method returns a LineStyle based on the parameters passed in.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This method returns a LineStyle based on the parameters passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|centerlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the center line color.|
|centerlineWidth|`Single`|This parameter is the center line width.|
|innerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the inner line color.|
|innerLineWidth|`Single`|This parameter is the inner line width.|
|outerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outer line color.|
|outerLineWidth|`Single`|This parameter is the outer line width.|
|roundCap|`Boolean`|This parameter defines whether you want a rounded end cap.|

---
#### `CreateSimpleLineStyle(GeoColor,Single,LineDashStyle,GeoColor,Single,LineDashStyle,GeoColor,Single,LineDashStyle,Boolean)`

**Summary**

   *This method returns a LineStyle based on the parameters passed in.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|This method returns a LineStyle based on the parameters passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|centerlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the center line color.|
|centerlineWidth|`Single`|This parameter is the center line width.|
|centerlineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the center line dash style.|
|innerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the inner line color.|
|innerLineWidth|`Single`|This parameter is the inner line width.|
|innerLineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the inner line dash style.|
|outerLineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outer line color.|
|outerLineWidth|`Single`|This parameter is the outer line width.|
|outerLineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the outer line dash style.|
|roundCap|`Boolean`|This parameter defines whether you want a rounded end cap.|

---
#### `Draw(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `Draw(IEnumerable<BaseShape>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|shapes|IEnumerable<[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)>|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `DrawSample(GeoCanvas,DrawingRectangleF)`

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
|drawingExtent|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|N/A|

---
#### `DrawSample(GeoCanvas)`

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
#### `GetRequiredColumnNames()`

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
#### `Parse(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|styleJson|`String`|N/A|

---
#### `SaveStyle(String)`

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
|filePathName|`String`|N/A|

---
#### `SaveStyle(Stream)`

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
|stream|`Stream`|N/A|

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
|[`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CreateLinePen(String[])`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|valueExpressions|`String[]`|N/A|

---
#### `DrawCore(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method draws the features on the view you provided.*

**Remarks**

   *This overridden method is called from the concrete public method Draw. In this method, we take the features you passed in and draw them on the view you provided. Each style (based on its properties) may draw each feature differently. When overriding this method, consider each feature and its column data values. You can use the full power of the GeoCanvas to do the drawing. If you need column data for a feature, be sure to override the GetRequiredColumnNamesCore and add the columns you need to the collection. In many of the styles, we add properties to allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore, we read that property and add it to the collection.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter represents the features you want to draw on the view.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the features on.|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

---
#### `DrawSampleCore(GeoCanvas,DrawingRectangleF)`

**Summary**

   *This method draws a sample feature on the view you provided.*

**Remarks**

   *This virtual method is called from the concrete public method Draw. In this method, we draw a sample style on the view you provided. This is typically used to display a legend or other sample area. When implementing this virtual method, consider the view size and draw the sample image appropriately. You should keep in mind that the sample typically shows up on a legend.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None|

**Parameters**

|Name|Type|Description|
|---|---|---|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the features on.|
|drawingExtent|[`DrawingRectangleF`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangleF.md)|N/A|

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
#### `GetRequiredColumnNamesCore()`

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
#### `OnDrawingDirectionPoint(DrawingDirectionPointEventArgs)`

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
|drawingDirectionPointEventArgs|[`DrawingDirectionPointEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingDirectionPointEventArgs.md)|N/A|

---
#### `Parse(JObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|jObject|[`JObject`](../ThinkGeo.Core/ThinkGeo.Core.JObject.md)|N/A|

---

### Public Events

#### DrawingDirectionPoint

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`DrawingDirectionPointEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.DrawingDirectionPointEventArgs.md)


