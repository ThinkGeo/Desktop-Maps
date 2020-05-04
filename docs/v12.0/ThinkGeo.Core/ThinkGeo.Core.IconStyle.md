# IconStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + [`PositionStyle`](../ThinkGeo.Core/ThinkGeo.Core.PositionStyle.md)
      + [`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)
        + **`IconStyle`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`IconStyle()`](#iconstyle)|
|[`IconStyle(String,String,GeoFont,GeoBrush)`](#iconstylestringstringgeofontgeobrush)|
|[`IconStyle(GeoImage,String,GeoFont,GeoBrush)`](#iconstylegeoimagestringgeofontgeobrush)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AbbreviationDictionary`](#abbreviationdictionary)|Dictionary<`String`,`String`>|N/A|
|[`Alignment`](#alignment)|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|N/A|
|[`AllowLineCarriage`](#allowlinecarriage)|`Boolean`|N/A|
|[`BasePoint`](#basepoint)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|
|[`BestPlacementSymbolHeight`](#bestplacementsymbolheight)|`Single`|N/A|
|[`BestPlacementSymbolWidth`](#bestplacementsymbolwidth)|`Single`|N/A|
|[`CustomTextStyles`](#customtextstyles)|Collection<[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)>|N/A|
|[`DateFormat`](#dateformat)|`String`|N/A|
|[`DrawingLevel`](#drawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`DuplicateRule`](#duplicaterule)|[`LabelDuplicateRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelDuplicateRule.md)|N/A|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`FittingLineInScreen`](#fittinglineinscreen)|`Boolean`|N/A|
|[`FittingPolygon`](#fittingpolygon)|`Boolean`|N/A|
|[`FittingPolygonFactor`](#fittingpolygonfactor)|`Double`|N/A|
|[`FittingPolygonInScreen`](#fittingpolygoninscreen)|`Boolean`|N/A|
|[`Font`](#font)|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|[`ForceHorizontalLabelForLine`](#forcehorizontallabelforline)|`Boolean`|N/A|
|[`ForceLineCarriage`](#forcelinecarriage)|`Boolean`|N/A|
|[`GridSize`](#gridsize)|`Int32`|N/A|
|[`HaloPen`](#halopen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|[`IconImage`](#iconimage)|[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)|This property gets and sets the NativeImage you want to use for the image.|
|[`IconImageScale`](#iconimagescale)|`Double`|This property gets and sets the scale of the image you want to draw.|
|[`IconPathFilename`](#iconpathfilename)|`String`|This property gets and sets the filename and path for the image you want to use in the style.|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`LabelAllLineParts`](#labelalllineparts)|`Boolean`|N/A|
|[`LabelAllPolygonParts`](#labelallpolygonparts)|`Boolean`|N/A|
|[`LabelPositions`](#labelpositions)|Dictionary<`String`,[`WorldLabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.WorldLabelingCandidate.md)>|N/A|
|[`LeaderLineMinimumLengthInPixels`](#leaderlineminimumlengthinpixels)|`Single`|N/A|
|[`LeaderLineRule`](#leaderlinerule)|[`LabelLeaderLinesRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelLeaderLinesRule.md)|N/A|
|[`LeaderLineStyle`](#leaderlinestyle)|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|N/A|
|[`LetterCase`](#lettercase)|[`DrawingTextLetterCase`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextLetterCase.md)|N/A|
|[`Mask`](#mask)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|N/A|
|[`MaskMargin`](#maskmargin)|[`DrawingMargin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingMargin.md)|N/A|
|[`MaskType`](#masktype)|[`MaskType`](../ThinkGeo.Core/ThinkGeo.Core.MaskType.md)|N/A|
|[`MaxCharAngleDelta`](#maxcharangledelta)|`Double`|N/A|
|[`MaxNudgingInPixel`](#maxnudginginpixel)|`Int32`|N/A|
|[`MinDistance`](#mindistance)|`Double`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`NudgingIntervalInPixel`](#nudgingintervalinpixel)|`Single`|N/A|
|[`NumericFormat`](#numericformat)|`String`|N/A|
|[`OverlappingRule`](#overlappingrule)|[`LabelOverlappingRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelOverlappingRule.md)|N/A|
|[`PolygonLabelingLocationMode`](#polygonlabelinglocationmode)|[`PolygonLabelingLocationMode`](../ThinkGeo.Core/ThinkGeo.Core.PolygonLabelingLocationMode.md)|N/A|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`RotationAngle`](#rotationangle)|`Double`|N/A|
|[`Spacing`](#spacing)|`Double`|N/A|
|[`SplineType`](#splinetype)|[`SplineType`](../ThinkGeo.Core/ThinkGeo.Core.SplineType.md)|N/A|
|[`SuppressPartialLabels`](#suppresspartiallabels)|`Boolean`|This property gets and sets whether a partial label in the current extent will be drawn or not.|
|[`TextBrush`](#textbrush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|[`TextColumnName`](#textcolumnname)|`String`|N/A|
|[`TextFormat`](#textformat)|`String`|N/A|
|[`TextLineSegmentRatio`](#textlinesegmentratio)|`Double`|N/A|
|[`TextPlacement`](#textplacement)|[`TextPlacement`](../ThinkGeo.Core/ThinkGeo.Core.TextPlacement.md)|N/A|
|[`WrapWidth`](#wrapwidth)|`Double`|N/A|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|N/A|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AllowOverlapping`](#allowoverlapping)|`Boolean`|N/A|
|[`AllowSpline`](#allowspline)|`Boolean`|N/A|
|[`DrawBasePointWithoutText`](#drawbasepointwithouttext)|`Boolean`|N/A|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|
|[`IsStyleDefault`](#isstyledefault)|`Boolean`|N/A|
|[`IsStyleJsonStyle`](#isstylejsonstyle)|`Boolean`|N/A|
|[`LabelFeatures`](#labelfeatures)|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|[`TextBaseline`](#textbaseline)|[`DrawingTextBaseline`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextBaseline.md)|N/A|
|[`TextContent`](#textcontent)|`String`|N/A|
|[`TextLetterSpacing`](#textletterspacing)|`Double`|N/A|
|[`TextLineSpacing`](#textlinespacing)|`Single`|N/A|

### Public Methods Summary


|Name|
|---|
|[`CloneDeep()`](#clonedeep)|
|[`Draw(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawienumerable<feature>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`Draw(IEnumerable<BaseShape>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawienumerable<baseshape>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawSample(GeoCanvas,DrawingRectangleF)`](#drawsamplegeocanvasdrawingrectanglef)|
|[`DrawSample(GeoCanvas)`](#drawsamplegeocanvas)|
|[`Equals(Object)`](#equalsobject)|
|[`GetHashCode()`](#gethashcode)|
|[`GetRequiredColumnNames()`](#getrequiredcolumnnames)|
|[`GetType()`](#gettype)|
|[`SaveStyle(String)`](#savestylestring)|
|[`SaveStyle(Stream)`](#savestylestream)|
|[`ToString()`](#tostring)|

### Protected Methods Summary


|Name|
|---|
|[`AbbreviateText(Feature,GeoCanvas)`](#abbreviatetextfeaturegeocanvas)|
|[`AbbreviateTextCore(Feature,GeoCanvas)`](#abbreviatetextcorefeaturegeocanvas)|
|[`CheckDuplicate(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#checkduplicatelabelingcandidategeocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`CheckDuplicateCore(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#checkduplicatecorelabelingcandidategeocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`CheckOverlapping(LabelingCandidate,GeoCanvas,GeoFont,Single,Single,Double,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#checkoverlappinglabelingcandidategeocanvasgeofontsinglesingledoublecollection<simplecandidate>collection<simplecandidate>)|
|[`CheckOverlappingCore(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#checkoverlappingcorelabelingcandidategeocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`CloneDeepCore()`](#clonedeepcore)|
|[`DrawCore(IEnumerable<Feature>,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawcoreienumerable<feature>geocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawMask(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawmasklabelingcandidategeocanvascollection<simplecandidate>collection<simplecandidate>)|
|[`DrawSampleCore(GeoCanvas,DrawingRectangleF)`](#drawsamplecoregeocanvasdrawingrectanglef)|
|[`DrawText(Feature,GeoCanvas,GeoFont,GeoBrush,GeoPen,DrawingLevel,Single,Single,DrawingTextAlignment,Double,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`](#drawtextfeaturegeocanvasgeofontgeobrushgeopendrawinglevelsinglesingledrawingtextalignmentdoublecollection<simplecandidate>collection<simplecandidate>)|
|[`FilterFeatures(IEnumerable<Feature>,GeoCanvas)`](#filterfeaturesienumerable<feature>geocanvas)|
|[`FilterFeaturesCore(IEnumerable<Feature>,GeoCanvas)`](#filterfeaturescoreienumerable<feature>geocanvas)|
|[`Finalize()`](#finalize)|
|[`Format(String,BaseShape)`](#formatstringbaseshape)|
|[`FormatCore(String,BaseShape)`](#formatcorestringbaseshape)|
|[`GetLabelingCandidateCore(Feature,GeoCanvas,GeoFont,Single,Single,Double)`](#getlabelingcandidatecorefeaturegeocanvasgeofontsinglesingledouble)|
|[`GetLabelingCandidateForOnePolygon(PolygonShape,String,GeoFont,Single,Single,GeoCanvas,Double)`](#getlabelingcandidateforonepolygonpolygonshapestringgeofontsinglesinglegeocanvasdouble)|
|[`GetLabelingCandidates(Feature,GeoCanvas,GeoFont,Single,Single,Double)`](#getlabelingcandidatesfeaturegeocanvasgeofontsinglesingledouble)|
|[`GetRequiredColumnNamesCore()`](#getrequiredcolumnnamescore)|
|[`MeasureText(GeoCanvas,String,GeoFont)`](#measuretextgeocanvasstringgeofont)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`OnFormatted(FormattedPositionStyleEventArgs)`](#onformattedformattedpositionstyleeventargs)|
|[`OnFormatting(FormattingPositionStyleEventArgs)`](#onformattingformattingpositionstyleeventargs)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`Formatting`](#formatting)|[`FormattingPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattingPositionStyleEventArgs.md)|N/A|
|[`Formatted`](#formatted)|[`FormattedPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattedPositionStyleEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`IconStyle()`](#iconstyle)|
|[`IconStyle(String,String,GeoFont,GeoBrush)`](#iconstylestringstringgeofontgeobrush)|
|[`IconStyle(GeoImage,String,GeoFont,GeoBrush)`](#iconstylegeoimagestringgeofontgeobrush)|

### Protected Constructors


### Public Properties

#### `AbbreviationDictionary`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,`String`>

---
#### `Alignment`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)

---
#### `AllowLineCarriage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `BasePoint`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)

---
#### `BestPlacementSymbolHeight`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `BestPlacementSymbolWidth`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `CustomTextStyles`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)>

---
#### `DateFormat`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `DrawingLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `DuplicateRule`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`LabelDuplicateRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelDuplicateRule.md)

---
#### `Filters`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<`String`>

---
#### `FittingLineInScreen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `FittingPolygon`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `FittingPolygonFactor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `FittingPolygonInScreen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Font`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)

---
#### `ForceHorizontalLabelForLine`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `ForceLineCarriage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `GridSize`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `HaloPen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

---
#### `IconImage`

**Summary**

   *This property gets and sets the NativeImage you want to use for the image.*

**Remarks**

   *You will typically want to use this property if your image is derived from a stream. Otherwise, you can use the IconFilePathName property to specify where the icon is located and we will handle the rest.*

**Return Value**

[`GeoImage`](../ThinkGeo.Core/ThinkGeo.Core.GeoImage.md)

---
#### `IconImageScale`

**Summary**

   *This property gets and sets the scale of the image you want to draw.*

**Remarks**

   *This property allows you to scale the image up and down depending on how large or small you want it. It can be changed dynamically, so you could change it at every scale level to resize the bitmap based on the current scale. A scale of 1 would be the original size, while a scale of 2 would double the size. A scale of .5 would reduce the size of the image by half, and so on.*

**Return Value**

`Double`

---
#### `IconPathFilename`

**Summary**

   *This property gets and sets the filename and path for the image you want to use in the style.*

**Remarks**

   *You can also optionally use the IconImage property if the image you want to use is derived from a stream.*

**Return Value**

`String`

---
#### `IsActive`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `LabelAllLineParts`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `LabelAllPolygonParts`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `LabelPositions`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,[`WorldLabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.WorldLabelingCandidate.md)>

---
#### `LeaderLineMinimumLengthInPixels`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `LeaderLineRule`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`LabelLeaderLinesRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelLeaderLinesRule.md)

---
#### `LeaderLineStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)

---
#### `LetterCase`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingTextLetterCase`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextLetterCase.md)

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
#### `MaxCharAngleDelta`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MaxNudgingInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `MinDistance`

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
#### `NudgingIntervalInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `NumericFormat`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `OverlappingRule`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`LabelOverlappingRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelOverlappingRule.md)

---
#### `PolygonLabelingLocationMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`PolygonLabelingLocationMode`](../ThinkGeo.Core/ThinkGeo.Core.PolygonLabelingLocationMode.md)

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

`Double`

---
#### `Spacing`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `SplineType`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`SplineType`](../ThinkGeo.Core/ThinkGeo.Core.SplineType.md)

---
#### `SuppressPartialLabels`

**Summary**

   *This property gets and sets whether a partial label in the current extent will be drawn or not.*

**Remarks**

   *This property provides a solution to the "cut off" label issue in Map Suite Web Edition and Desktop Edition, which occurs when multiple tiles exist. When you set this property to true, any labels outside of the current extent will not be drawn.*

**Return Value**

`Boolean`

---
#### `TextBrush`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)

---
#### `TextColumnName`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `TextFormat`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `TextLineSegmentRatio`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `TextPlacement`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`TextPlacement`](../ThinkGeo.Core/ThinkGeo.Core.TextPlacement.md)

---
#### `WrapWidth`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

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

#### `AllowOverlapping`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `AllowSpline`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `DrawBasePointWithoutText`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
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
#### `IsStyleDefault`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsStyleJsonStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `LabelFeatures`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>

---
#### `TextBaseline`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingTextBaseline`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextBaseline.md)

---
#### `TextContent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `TextLetterSpacing`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `TextLineSpacing`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

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

#### `AbbreviateText(Feature,GeoCanvas)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|

---
#### `AbbreviateTextCore(Feature,GeoCanvas)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|

---
#### `CheckDuplicate(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `CheckDuplicateCore(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `CheckOverlapping(LabelingCandidate,GeoCanvas,GeoFont,Single,Single,Double,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|xOffsetInPixel|`Single`|N/A|
|yOffsetInPixel|`Single`|N/A|
|rotationAngle|`Double`|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `CheckOverlappingCore(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
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
#### `DrawMask(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `DrawSampleCore(GeoCanvas,DrawingRectangleF)`

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
#### `DrawText(Feature,GeoCanvas,GeoFont,GeoBrush,GeoPen,DrawingLevel,Single,Single,DrawingTextAlignment,Double,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|textBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|haloPen|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|drawingLevel|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|xOffsetInPixel|`Single`|N/A|
|yOffsetInPixel|`Single`|N/A|
|alignment|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|N/A|
|rotationAngle|`Double`|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|N/A|

---
#### `FilterFeatures(IEnumerable<Feature>,GeoCanvas)`

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
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|

---
#### `FilterFeaturesCore(IEnumerable<Feature>,GeoCanvas)`

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
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|

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
#### `Format(String,BaseShape)`

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
|text|`String`|N/A|
|labeledShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `FormatCore(String,BaseShape)`

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
|text|`String`|N/A|
|labeledShape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|N/A|

---
#### `GetLabelingCandidateCore(Feature,GeoCanvas,GeoFont,Single,Single,Double)`

**Summary**

   *This method determines which labels will be candidates for drawing.*

**Remarks**

   *This overridden method is called from the concrete public method GetLabelingCandidate. In this method, we determine if the feature passed in will be a candidate for drawing. If you have the grid method enabled, then we determine this by ensuring that only one label will be eligible per grid cell. In this way, we can ensure that labels always draw in the same place at the same scale.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)>|This method returns a collection of labeling candidates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter represents the features you want to draw on the view.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter represents the view you want to draw the features on.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|xOffsetInPixel|`Single`|N/A|
|yOffsetInPixel|`Single`|N/A|
|rotationAngle|`Double`|N/A|

---
#### `GetLabelingCandidateForOnePolygon(PolygonShape,String,GeoFont,Single,Single,GeoCanvas,Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|polygon|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|text|`String`|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|xOffsetInPixel|`Single`|N/A|
|yOffsetInPixel|`Single`|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|rotationAngle|`Double`|N/A|

---
#### `GetLabelingCandidates(Feature,GeoCanvas,GeoFont,Single,Single,Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|xOffsetInPixel|`Single`|N/A|
|yOffsetInPixel|`Single`|N/A|
|rotationAngle|`Double`|N/A|

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
#### `MeasureText(GeoCanvas,String,GeoFont)`

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
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|N/A|
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
#### `OnFormatted(FormattedPositionStyleEventArgs)`

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
|e|[`FormattedPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattedPositionStyleEventArgs.md)|N/A|

---
#### `OnFormatting(FormattingPositionStyleEventArgs)`

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
|e|[`FormattingPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattingPositionStyleEventArgs.md)|N/A|

---

### Public Events

#### Formatting

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FormattingPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattingPositionStyleEventArgs.md)

#### Formatted

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`FormattedPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattedPositionStyleEventArgs.md)


