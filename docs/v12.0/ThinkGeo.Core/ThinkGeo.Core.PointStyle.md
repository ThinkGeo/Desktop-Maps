# PointStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + [`PointBaseStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseStyle.md)
      + **`PointStyle`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`PointStyle()`](#pointstyle)|
|[`PointStyle(GeoImage)`](#pointstylegeoimage)|
|[`PointStyle(GeoFont,String,GeoBrush)`](#pointstylegeofontstringgeobrush)|
|[`PointStyle(GeoFont,String,GeoBrush,GeoPen)`](#pointstylegeofontstringgeobrushgeopen)|
|[`PointStyle(PointSymbolType,Int32,GeoBrush)`](#pointstylepointsymboltypeint32geobrush)|
|[`PointStyle(PointSymbolType,Int32,GeoBrush,GeoPen)`](#pointstylepointsymboltypeint32geobrushgeopen)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`CustomPointStyles`](#custompointstyles)|Collection<[`PointBaseStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseStyle.md)>|N/A|
|[`DrawingLevel`](#drawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`FillBrush`](#fillbrush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`GlyphContent`](#glyphcontent)|`String`|This property gets and sets the content of the character you want to use from the font you selected in the CharacterFont property.|
|[`GlyphFont`](#glyphfont)|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|This property gets and sets the font that is used for the character if the PointType is Glyph.|
|[`Image`](#image)|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This property gets and sets the image used if the PointType property is Image.|
|[`ImageScale`](#imagescale)|`Double`|This property gets and sets the scale of the image you want to draw.|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`Mask`](#mask)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|N/A|
|[`MaskMargin`](#maskmargin)|[`DrawingMargin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingMargin.md)|N/A|
|[`MaskType`](#masktype)|[`MaskType`](../ThinkGeo.Core/ThinkGeo.Core.MaskType.md)|N/A|
|[`Name`](#name)|`String`|N/A|
|[`OutlinePen`](#outlinepen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|[`PointType`](#pointtype)|[`PointType`](../ThinkGeo.Core/ThinkGeo.Core.PointType.md)|This property gets and sets the type of point you want to draw.|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`RotationAngle`](#rotationangle)|`Single`|N/A|
|[`SymbolSize`](#symbolsize)|`Single`|N/A|
|[`SymbolType`](#symboltype)|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|This property gets and sets the type of symbol you want to use if the PointType is Symbol.|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|N/A|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`CreateCompoundCircleStyle(GeoColor,Single,GeoColor,Single,GeoColor,Single)`](#createcompoundcirclestylegeocolorsinglegeocolorsinglegeocolorsingle)|
|[`CreateCompoundCircleStyle(GeoColor,Single,GeoColor,Single,GeoColor,Single,GeoColor,Single)`](#createcompoundcirclestylegeocolorsinglegeocolorsinglegeocolorsinglegeocolorsingle)|
|[`CreateCompoundPointStyle(PointSymbolType,GeoColor,GeoColor,Single,Single,PointSymbolType,GeoColor,GeoColor,Single,Single)`](#createcompoundpointstylepointsymboltypegeocolorgeocolorsinglesinglepointsymboltypegeocolorgeocolorsinglesingle)|
|[`CreateSimpleCircleStyle(GeoColor,Single)`](#createsimplecirclestylegeocolorsingle)|
|[`CreateSimpleCircleStyle(GeoColor,Single,GeoColor,Single)`](#createsimplecirclestylegeocolorsinglegeocolorsingle)|
|[`CreateSimpleCircleStyle(GeoColor,Single,GeoColor)`](#createsimplecirclestylegeocolorsinglegeocolor)|
|[`CreateSimplePointStyle(PointSymbolType,GeoColor,Single)`](#createsimplepointstylepointsymboltypegeocolorsingle)|
|[`CreateSimplePointStyle(PointSymbolType,GeoColor,GeoColor,Single,Single)`](#createsimplepointstylepointsymboltypegeocolorgeocolorsinglesingle)|
|[`CreateSimplePointStyle(PointSymbolType,GeoColor,GeoColor,Single)`](#createsimplepointstylepointsymboltypegeocolorgeocolorsingle)|
|[`CreateSimpleSquareStyle(GeoColor,Single)`](#createsimplesquarestylegeocolorsingle)|
|[`CreateSimpleSquareStyle(GeoColor,Single,GeoColor)`](#createsimplesquarestylegeocolorsinglegeocolor)|
|[`CreateSimpleSquareStyle(GeoColor,Single,GeoColor,Single)`](#createsimplesquarestylegeocolorsinglegeocolorsingle)|
|[`CreateSimpleStarStyle(GeoColor,Single)`](#createsimplestarstylegeocolorsingle)|
|[`CreateSimpleStarStyle(GeoColor,Single,GeoColor)`](#createsimplestarstylegeocolorsinglegeocolor)|
|[`CreateSimpleStarStyle(GeoColor,Single,GeoColor,Single)`](#createsimplestarstylegeocolorsinglegeocolorsingle)|
|[`CreateSimpleTriangleStyle(GeoColor,Single)`](#createsimpletrianglestylegeocolorsingle)|
|[`CreateSimpleTriangleStyle(GeoColor,Single,GeoColor)`](#createsimpletrianglestylegeocolorsinglegeocolor)|
|[`CreateSimpleTriangleStyle(GeoColor,Single,GeoColor,Single)`](#createsimpletrianglestylegeocolorsinglegeocolorsingle)|
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
|[`DrawGlyph(GeoFont,String,Feature,GeoCanvas,GeoBrush,GeoPen,Single,Single)`](#drawglyphgeofontstringfeaturegeocanvasgeobrushgeopensinglesingle)|
|[`DrawImage(GeoImage,Feature,GeoCanvas,Single,Single)`](#drawimagegeoimagefeaturegeocanvassinglesingle)|
|[`DrawSampleCore(GeoCanvas,DrawingRectangleF)`](#drawsamplecoregeocanvasdrawingrectanglef)|
|[`DrawSymbol(PointSymbolType,Feature,GeoCanvas,GeoBrush,GeoPen,Single,Single)`](#drawsymbolpointsymboltypefeaturegeocanvasgeobrushgeopensinglesingle)|
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
|[`PointStyle()`](#pointstyle)|
|[`PointStyle(GeoImage)`](#pointstylegeoimage)|
|[`PointStyle(GeoFont,String,GeoBrush)`](#pointstylegeofontstringgeobrush)|
|[`PointStyle(GeoFont,String,GeoBrush,GeoPen)`](#pointstylegeofontstringgeobrushgeopen)|
|[`PointStyle(PointSymbolType,Int32,GeoBrush)`](#pointstylepointsymboltypeint32geobrush)|
|[`PointStyle(PointSymbolType,Int32,GeoBrush,GeoPen)`](#pointstylepointsymboltypeint32geobrushgeopen)|

### Protected Constructors


### Public Properties

#### `CustomPointStyles`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`PointBaseStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointBaseStyle.md)>

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

   *N/A*

**Remarks**

   *N/A*

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
#### `GlyphContent`

**Summary**

   *This property gets and sets the content of the character you want to use from the font you selected in the CharacterFont property.*

**Remarks**

   *You need to specify the content of the character you want to use from the font you selected. For example, if you choose 1, then we will use the first character in the font you set in the CharacterFont property.*

**Return Value**

`String`

---
#### `GlyphFont`

**Summary**

   *This property gets and sets the font that is used for the character if the PointType is Glyph.*

**Remarks**

   *This property allows you to set the font from which to select a character index if you choose the Glyph PointType.*

**Return Value**

[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)

---
#### `Image`

**Summary**

   *This property gets and sets the image used if the PointType property is Image.*

**Remarks**

   *This property is where you can set the image for the points if the PointType is Image. It uses a NativeImage, so you can either reference a file or supply a stream.*

**Return Value**

[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)

---
#### `ImageScale`

**Summary**

   *This property gets and sets the scale of the image you want to draw.*

**Remarks**

   *This property allows you to scale the image up and down depending on how large or small you want it. It can be changed dynamically, so you could change it at every scale level to resize the bitmap based on the current scale. A scale of 1 would be the original size, while a scale of 2 would double the size. A scale of .5 would reduce the size of the image by half, and so on.*

**Return Value**

`Double`

---
#### `IsActive`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Mask`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)

---
#### `MaskMargin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingMargin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingMargin.md)

---
#### `MaskType`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`MaskType`](../ThinkGeo.Core/ThinkGeo.Core.MaskType.md)

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `PointType`

**Summary**

   *This property gets and sets the type of point you want to draw.*

**Remarks**

   *When using the PointStyle you choose between a bitmap, a font or a predefined symbol to represent the point. Each of these options has corresponding properties on the point symbol. If you set the type to character, then you need to set the properties that start with "Glyph," such as "CharacterFont." The same is true for the symbol.*

**Return Value**

[`PointType`](../ThinkGeo.Core/ThinkGeo.Core.PointType.md)

---
#### `RequiredColumnNames`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `RotationAngle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `SymbolSize`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `SymbolType`

**Summary**

   *This property gets and sets the type of symbol you want to use if the PointType is Symbol.*

**Remarks**

   *You can choose between a number of predefined symbols. The symbols are simple geometric objects that are typically used for abstract representations on a map. If there is a specific symbol you need that is not part of our symbol collection, you can submit it to us and we will consider adding it.*

**Return Value**

[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)

---
#### `XOffsetInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `YOffsetInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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
#### `CreateCompoundCircleStyle(GeoColor,Single,GeoColor,Single,GeoColor,Single)`

**Summary**

   *Static API to create a compound circle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor1|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the outer circle's PointStyle fill color.|
|size1|`Single`|This parameter determines the outer circle's PointStyle size.|
|outlineColor1|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the outer circle's PointStyle outline color.|
|outlineWidth1|`Single`|This parameter determines the outer circle's PointStyle outline width.|
|fillColor2|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the inner circle's PointStyle fill color.|
|size2|`Single`|This parameter determines the inner circle's PointStyle size.|

---
#### `CreateCompoundCircleStyle(GeoColor,Single,GeoColor,Single,GeoColor,Single,GeoColor,Single)`

**Summary**

   *Static API to create a compound circle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor1|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the outer circle's PointStyle fill color.|
|size1|`Single`|This parameter determines the outer circle's PointStyle size.|
|outlineColor1|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the outer circle's PointStyle outline color.|
|outlineWidth1|`Single`|This parameter determines the outer circle's PointStyle outline width.|
|fillColor2|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the inner circle's PointStyle fill color.|
|size2|`Single`|This parameter determines the inner circle's PointStyle size.|
|outlineColor2|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the inner circle's PointStyle outline color.|
|outlineWidth2|`Single`|This parameter determines the inner circle's PointStyle outline width.|

---
#### `CreateCompoundPointStyle(PointSymbolType,GeoColor,GeoColor,Single,Single,PointSymbolType,GeoColor,GeoColor,Single,Single)`

**Summary**

   *Static API to create a compound circle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointStyle1|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|This parameter determines the outer circle's pointstyle symbol type.|
|fillColor1|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the outer circle's PointStyle fill color.|
|outlineColor1|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the outer circle's PointStyle outline color.|
|outlineWidth1|`Single`|This parameter determines the outer circle's PointStyle outline width.|
|size1|`Single`|This parameter determines the outer circle's PointStyle size.|
|pointStyle2|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|This parameter determines the inner circle's pointstyle symbol type.|
|fillColor2|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the inner circle's PointStyle fill color.|
|outlineColor2|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the inner circle's PointStyle outline color.|
|outlineWidth2|`Single`|This parameter determines the inner circle's PointStyle outline width.|
|size2|`Single`|This parameter determines the inner circle PointStyle size.|

---
#### `CreateSimpleCircleStyle(GeoColor,Single)`

**Summary**

   *Static API to create a circle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|

---
#### `CreateSimpleCircleStyle(GeoColor,Single,GeoColor,Single)`

**Summary**

   *Static API to create a circle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|
|outlineWidth|`Single`|This parameter determines the PointStyle outline width.|

---
#### `CreateSimpleCircleStyle(GeoColor,Single,GeoColor)`

**Summary**

   *Static API to create a circle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|

---
#### `CreateSimplePointStyle(PointSymbolType,GeoColor,Single)`

**Summary**

   *Static API to create a point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointStyle|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|This parameter determines the PointStyle symbol type.|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|

---
#### `CreateSimplePointStyle(PointSymbolType,GeoColor,GeoColor,Single,Single)`

**Summary**

   *Static API to create a point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointStyle|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|This parameter determines the PointStyle symbol type.|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|
|outlineWidth|`Single`|This parameter determines the PointStyle outline width.|
|size|`Single`|This parameter determines the PointStyle size.|

---
#### `CreateSimplePointStyle(PointSymbolType,GeoColor,GeoColor,Single)`

**Summary**

   *Static API to create a point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pointStyle|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|This parameter determines the PointStyle symbol type.|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|
|size|`Single`|This parameter determines the PointStyle size.|

---
#### `CreateSimpleSquareStyle(GeoColor,Single)`

**Summary**

   *Static API to create a square point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|

---
#### `CreateSimpleSquareStyle(GeoColor,Single,GeoColor)`

**Summary**

   *Static API to create a square point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|

---
#### `CreateSimpleSquareStyle(GeoColor,Single,GeoColor,Single)`

**Summary**

   *Static API to create a square point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|
|outlineWidth|`Single`|This parameter determines the PointStyle outline width.|

---
#### `CreateSimpleStarStyle(GeoColor,Single)`

**Summary**

   *Static API to create a star point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|

---
#### `CreateSimpleStarStyle(GeoColor,Single,GeoColor)`

**Summary**

   *Static API to create a star point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|

---
#### `CreateSimpleStarStyle(GeoColor,Single,GeoColor,Single)`

**Summary**

   *Static API to create a star point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|
|outlineWidth|`Single`|This parameter determines the PointStyle outline width.|

---
#### `CreateSimpleTriangleStyle(GeoColor,Single)`

**Summary**

   *Static API to create a triangle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|

---
#### `CreateSimpleTriangleStyle(GeoColor,Single,GeoColor)`

**Summary**

   *Static API to create a triangle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|

---
#### `CreateSimpleTriangleStyle(GeoColor,Single,GeoColor,Single)`

**Summary**

   *Static API to create a triangle point style.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|The created point style.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle fill color.|
|size|`Single`|This parameter determines the PointStyle size.|
|outlineColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter determines the PointStyle outline color.|
|outlineWidth|`Single`|This parameter determines the PointStyle outline width.|

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
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|

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
#### `DrawGlyph(GeoFont,String,Feature,GeoCanvas,GeoBrush,GeoPen,Single,Single)`

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
|glyphFont|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|glyphConent|`String`|N/A|
|pointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|geoBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|geoPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|symbolSize|`Single`|N/A|
|rotationAngle|`Single`|N/A|

---
#### `DrawImage(GeoImage,Feature,GeoCanvas,Single,Single)`

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
|geoImage|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|N/A|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|symbolSize|`Single`|N/A|
|rotationAngle|`Single`|N/A|

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
#### `DrawSymbol(PointSymbolType,Feature,GeoCanvas,GeoBrush,GeoPen,Single,Single)`

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
|symbolType|[`PointSymbolType`](../ThinkGeo.Core/ThinkGeo.Core.PointSymbolType.md)|N/A|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|geoBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|geoPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|symbolSize|`Single`|N/A|
|rotationAngle|`Single`|N/A|

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
|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|jObject|[`JObject`](../ThinkGeo.Core/ThinkGeo.Core.JObject.md)|N/A|

---

### Public Events


