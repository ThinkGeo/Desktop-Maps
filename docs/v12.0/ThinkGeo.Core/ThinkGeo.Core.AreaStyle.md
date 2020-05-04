# AreaStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + **`AreaStyle`**
      + [`HueFamilyAreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.HueFamilyAreaStyle.md)
      + [`QualityFamilyAreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.QualityFamilyAreaStyle.md)
      + [`BuildingAreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.BuildingAreaStyle.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`AreaStyle()`](#areastyle)|
|[`AreaStyle(GeoBrush)`](#areastylegeobrush)|
|[`AreaStyle(GeoPen)`](#areastylegeopen)|
|[`AreaStyle(GeoPen,GeoBrush)`](#areastylegeopengeobrush)|
|[`AreaStyle(GeoPen,GeoBrush,PenBrushDrawingOrder)`](#areastylegeopengeobrushpenbrushdrawingorder)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CustomAreaStyles`](#customareastyles)|Collection<[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)>|This property returns a collection of area styles, allowing you to stack multiple area styles on top of each other.|
|[`DrawingLevel`](#drawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`FillBrush`](#fillbrush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This property gets and sets the solid brush you want to use to fill in the area features.|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OutlinePen`](#outlinepen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This property gets and sets the outline pen you want to use to outline the features.|
|[`PenBrushDrawingOrder`](#penbrushdrawingorder)|[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)|This property gets and sets the pen and brush drawing order.|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|This property gets and sets the X pixel offset for drawing each feature.|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|This property gets and sets the Y pixel offset for drawing each feature.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|
|[`Shadow`](#shadow)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`CreateHatchStyle(GeoHatchStyle,GeoColor,GeoColor)`](#createhatchstylegeohatchstylegeocolorgeocolor)|
|[`CreateHatchStyle(GeoHatchStyle,GeoColor,GeoColor,GeoColor)`](#createhatchstylegeohatchstylegeocolorgeocolorgeocolor)|
|[`CreateHatchStyle(GeoHatchStyle,GeoColor,GeoColor,GeoColor,Int32,LineDashStyle,Single,Single)`](#createhatchstylegeohatchstylegeocolorgeocolorgeocolorint32linedashstylesinglesingle)|
|[`CreateHueFamilyAreaStyle(GeoColor,GeoColor,Int32)`](#createhuefamilyareastylegeocolorgeocolorint32)|
|[`CreateHueFamilyLinearGradientAreaStyle(GeoColor,GeoColor,Int32,GeoColor,GeoColor,Single)`](#createhuefamilylineargradientareastylegeocolorgeocolorint32geocolorgeocolorsingle)|
|[`CreateLinearGradientStyle(GeoColor,GeoColor,Single)`](#createlineargradientstylegeocolorgeocolorsingle)|
|[`CreateLinearGradientStyle(GeoColor,GeoColor,Single,GeoColor)`](#createlineargradientstylegeocolorgeocolorsinglegeocolor)|
|[`CreateQualityFamilyAreaStyle(GeoColor,GeoColor,Int32)`](#createqualityfamilyareastylegeocolorgeocolorint32)|
|[`CreateQualityFamilyLinearGradientAreaStyle(GeoColor,GeoColor,Int32,GeoColor,GeoColor,Single)`](#createqualityfamilylineargradientareastylegeocolorgeocolorint32geocolorgeocolorsingle)|
|[`CreateSimpleAreaStyle(GeoColor)`](#createsimpleareastylegeocolor)|
|[`CreateSimpleAreaStyle(GeoColor,GeoColor)`](#createsimpleareastylegeocolorgeocolor)|
|[`CreateSimpleAreaStyle(GeoColor,GeoColor,Int32)`](#createsimpleareastylegeocolorgeocolorint32)|
|[`CreateSimpleAreaStyle(GeoColor,GeoColor,Int32,LineDashStyle)`](#createsimpleareastylegeocolorgeocolorint32linedashstyle)|
|[`CreateSimpleAreaStyle(GeoColor,Single,Single)`](#createsimpleareastylegeocolorsinglesingle)|
|[`CreateSimpleAreaStyle(GeoColor,GeoColor,Single,Single)`](#createsimpleareastylegeocolorgeocolorsinglesingle)|
|[`CreateSimpleAreaStyle(GeoColor,GeoColor,Int32,Single,Single)`](#createsimpleareastylegeocolorgeocolorint32singlesingle)|
|[`CreateSimpleAreaStyle(GeoColor,GeoColor,Int32,LineDashStyle,Single,Single)`](#createsimpleareastylegeocolorgeocolorint32linedashstylesinglesingle)|
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
|[`DrawCore(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawcoreienumerable<feature>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawSampleCore(GeoCanvas,DrawingRectangleF)`](#drawsamplecoregeocanvasdrawingrectanglef)|
|[`Finalize()`](#finalize)|
|[`GetRequiredColumnNamesCore()`](#getrequiredcolumnnamescore)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`Parse(JObject)`](#parsejobject)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`AreaStyle()`](#areastyle)|
|[`AreaStyle(GeoBrush)`](#areastylegeobrush)|
|[`AreaStyle(GeoPen)`](#areastylegeopen)|
|[`AreaStyle(GeoPen,GeoBrush)`](#areastylegeopengeobrush)|
|[`AreaStyle(GeoPen,GeoBrush,PenBrushDrawingOrder)`](#areastylegeopengeobrushpenbrushdrawingorder)|

### Protected Constructors


### Public Properties

#### `CustomAreaStyles`

**Summary**

   *This property returns a collection of area styles, allowing you to stack multiple area styles on top of each other.*

**Remarks**

   *Using this collection, you can stack multiple area styles on top of each other. When we draw the features, we will draw them in order that they exist in the collection. You can use these stacks to create drop shadow effects, multiple colored outlines, etc.*

**Return Value**

Collection<[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)>

---
#### `DrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `FillBrush`

**Summary**

   *This property gets and sets the solid brush you want to use to fill in the area features.*

**Remarks**

   *This solid brush is used to fill in the area features that will draw. You can also optionally specify an outline pen to give the area an outline. The default solid brush has a fill color of transparent, which means it will not draw anything.*

**Return Value**

[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)

---
#### `Filters`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

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
#### `OutlinePen`

**Summary**

   *This property gets and sets the outline pen you want to use to outline the features.*

**Remarks**

   *This outline pen is used to outline the features that will draw. You can also optionally specify a fill brush to give the area a solid fill. The default outline pen color is transparent, which means it will not draw anything.*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `PenBrushDrawingOrder`

**Summary**

   *This property gets and sets the pen and brush drawing order.*

**Remarks**

   *This property controls whether the outline pen or the fill brush is drawn first. The default is for the fill brush to be drawn first. If you have the outline pen draw first then the thickness of the pen will be smaller, creating a subtle but noticeable effect.*

**Return Value**

[`PenBrushDrawingOrder`](../ThinkGeo.Core/ThinkGeo.Core.PenBrushDrawingOrder.md)

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

   *This property allows you to specify an X offset. When combined with a Y offset, it enables you to achieve effects such as drop shadows, etc. There also may be times when you need to modify the location of feature data so as to align it with raster satellite data.*

**Return Value**

`Single`

---
#### `YOffsetInPixel`

**Summary**

   *This property gets and sets the Y pixel offset for drawing each feature.*

**Remarks**

   *This property allows you to specify a Y offset. When combined with an X offset, it enables you to achieve effects such as drop shadows, etc. There also may be times when you need to modify the location of feature data so as to align it with raster satellite data.*

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
#### `Shadow`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)

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
#### `CreateHatchStyle(GeoHatchStyle,GeoColor,GeoColor)`

**Summary**

   *This method returns an AreaStyle with a hatch pattern.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This property is the color of the foreground of the hatch pattern.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|hatchStyle|[`GeoHatchStyle`](../ThinkGeo.Core/ThinkGeo.Core.GeoHatchStyle.md)|This parameter is the hatch pattern to be used.|
|foregroundBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This property is the color of the foreground of the hatch pattern.|
|backgroundBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This property is the color of the background of the hatch pattern.|

---
#### `CreateHatchStyle(GeoHatchStyle,GeoColor,GeoColor,GeoColor)`

**Summary**

   *This method returns an AreaStyle with a hatch pattern.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle with a hatch pattern.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|hatchStyle|[`GeoHatchStyle`](../ThinkGeo.Core/ThinkGeo.Core.GeoHatchStyle.md)|This parameter is the hatch pattern to be used.|
|foregroundBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This property is the color of the foreground of the hatch pattern.|
|backgroundBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This property is the color of the background of the hatch pattern.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the border color for the area.|

---
#### `CreateHatchStyle(GeoHatchStyle,GeoColor,GeoColor,GeoColor,Int32,LineDashStyle,Single,Single)`

**Summary**

   *This method returns a GeoHatchStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle with a hatch pattern.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|hatchStyle|[`GeoHatchStyle`](../ThinkGeo.Core/ThinkGeo.Core.GeoHatchStyle.md)|This parameter is the hatch pattern to be used.|
|foregroundBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This property is the color of the foreground of the hatch pattern.|
|backgroundColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This property is the color of the background of the hatch pattern.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the border color for the area.|
|outlinePenWidth|`Int32`|This parameter is the border width for the area.|
|outlineDashStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the dahs style to be used for the border.|
|xOffsetInPixel|`Single`|This parameter is the pixel offset for X.|
|yOffsetInPixel|`Single`|This parameter is the pixel offset for Y.|

---
#### `CreateHueFamilyAreaStyle(GeoColor,GeoColor,Int32)`

**Summary**

   *This method returns an AreaStyle.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter specifies the GeoPen you want to use on the outline of the area style.|
|baseColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The base GeoColor of the hue family colors.|
|numberOfColors|`Int32`|The number of GeoColors in hue family to construct the areastyle.|

---
#### `CreateHueFamilyLinearGradientAreaStyle(GeoColor,GeoColor,Int32,GeoColor,GeoColor,Single)`

**Summary**

   *This method returns an AreaStyle in a family of hue-related colors drawn with a linear gradient.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle in a family of hue-related colors drawn with a linear gradient.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter specifies the GeoPen you want to use on the outline of the AreaStyle.|
|baseColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The base GeoColor for the hue family of colors.|
|numberOfColors|`Int32`|The number of GeoColors in the hue family to construct the AreaStyle.|
|fromColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the starting GeoColor for the gradient.|
|toColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the ending GeoColor for the gradient.|
|angle|`Single`|This parameter represents the angle for the gradient.|

---
#### `CreateLinearGradientStyle(GeoColor,GeoColor,Single)`

**Summary**

   *This method returns a linear gradient style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the starting GeoColor for the gradient.|
|toColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the ending GeoColor for the gradient.|
|angle|`Single`|This parameter represents the angle of the color changing from start to end.|

---
#### `CreateLinearGradientStyle(GeoColor,GeoColor,Single,GeoColor)`

**Summary**

   *This method returns a linear gradient style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the starting GeoColor for the gradient.|
|toColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the ending GeoColor for the gradient.|
|angle|`Single`|This parameter represents the angle of the color changing from start to end.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the outline pen color of the area style.|

---
#### `CreateQualityFamilyAreaStyle(GeoColor,GeoColor,Int32)`

**Summary**

   *This method returns an AreaStyle in a family of quality-realted colors.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle in a family of quality-realted colors.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter specifies the GeoPen you want to use on the outline of the AreaStyle.|
|baseColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The base GeoColor for the quality family of colors.|
|numberOfColors|`Int32`|The number of GeoColors in the quality-based family to construct the AreaStyle.|

---
#### `CreateQualityFamilyLinearGradientAreaStyle(GeoColor,GeoColor,Int32,GeoColor,GeoColor,Single)`

**Summary**

   *This method returns an AreaStyle in a family of quality-related colors drawn with a linear gradient.*

**Remarks**

   *None.*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method returns an AreaStyle in a family of quality-related colors drawn with a linear gradient.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter specifies the GeoPen you want to use on the outline of the AreaStyle.|
|baseColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The base GeoColor for the quality family of colors.|
|numberOfColors|`Int32`|The number of GeoColors in quality-based family to construct the AreaStyle.|
|fromColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the starting GeoColor for the gradient.|
|toColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the ending GeoColor for the gradient.|
|angle|`Single`|This parameter represents the angle for the gradient.|

---
#### `CreateSimpleAreaStyle(GeoColor)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color of the area.|

---
#### `CreateSimpleAreaStyle(GeoColor,GeoColor)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color for the area.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outline pen color for the area.|

---
#### `CreateSimpleAreaStyle(GeoColor,GeoColor,Int32)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color for the area.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outline pen color for the area.|
|outlinePenWidth|`Int32`|This parameter is the outline pen width for the area.|

---
#### `CreateSimpleAreaStyle(GeoColor,GeoColor,Int32,LineDashStyle)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color for the area.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outline pen color for the area.|
|outlinePenWidth|`Int32`|This parameter is the outline pen width for the area.|
|borderStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the BorderStyle for the area style.|

---
#### `CreateSimpleAreaStyle(GeoColor,Single,Single)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color for the area.|
|xOffsetInPixel|`Single`|This parameter is the X pixels offset for this area.|
|yOffsetInPixel|`Single`|This parameter is the Y pixels offset for this area.|

---
#### `CreateSimpleAreaStyle(GeoColor,GeoColor,Single,Single)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color for the area.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outline color for the area.|
|xOffsetInPixel|`Single`|This parameter is the X pixels offset for this area.|
|yOffsetInPixel|`Single`|This parameter is the Y pixels offset for this area.|

---
#### `CreateSimpleAreaStyle(GeoColor,GeoColor,Int32,Single,Single)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color for the area.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outline pen color for the area.|
|outlinePenWidth|`Int32`|This parameter is the outline pen width for the area.|
|xOffsetInPixel|`Single`|This parameter is the X pixels offset for this area.|
|yOffsetInPixel|`Single`|This parameter is the Y pixels offset for this area.|

---
#### `CreateSimpleAreaStyle(GeoColor,GeoColor,Int32,LineDashStyle,Single,Single)`

**Summary**

   *This method builds a simple area style.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This method builds a simple area style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillBrushColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the fill color for the area.|
|outlinePenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the outline pen color for the area.|
|outlinePenWidth|`Int32`|This parameter is the outline pen width for the area.|
|borderStyle|[`LineDashStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineDashStyle.md)|This parameter is the BorderStyle for the area.|
|xOffsetInPixel|`Single`|This parameter is the X pixels offset for this area.|
|yOffsetInPixel|`Single`|This parameter is the Y pixels offset for this area.|

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
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|N/A|

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
#### `Parse(JObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|jObject|[`JObject`](../ThinkGeo.Core/ThinkGeo.Core.JObject.md)|N/A|

---

### Public Events


