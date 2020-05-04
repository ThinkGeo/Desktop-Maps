# TextStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + [`PositionStyle`](../ThinkGeo.Core/ThinkGeo.Core.PositionStyle.md)
      + **`TextStyle`**
        + [`IconStyle`](../ThinkGeo.Core/ThinkGeo.Core.IconStyle.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`TextStyle()`](#textstyle)|
|[`TextStyle(String,GeoFont,GeoBrush)`](#textstylestringgeofontgeobrush)|

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
|[`CustomTextStyles`](#customtextstyles)|Collection<[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)>|This property returns a collection of area styles allowing you to stack multiple area styles on top of each other.|
|[`DateFormat`](#dateformat)|`String`|This property gets and sets the format that will be applied to the text which can be parsed to DateTime type.|
|[`DrawingLevel`](#drawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|Gets or sets the DrawingLavel for this style.|
|[`DuplicateRule`](#duplicaterule)|[`LabelDuplicateRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelDuplicateRule.md)|N/A|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`FittingLineInScreen`](#fittinglineinscreen)|`Boolean`|This property gets and sets whether the labeler will try to fit the label as best as it can on the visible part of a line on the screen.|
|[`FittingPolygon`](#fittingpolygon)|`Boolean`|N/A|
|[`FittingPolygonFactor`](#fittingpolygonfactor)|`Double`|N/A|
|[`FittingPolygonInScreen`](#fittingpolygoninscreen)|`Boolean`|This property gets and sets whether the labeler will try to fit the label as best as it can on the visible part of a polygon on the screen.|
|[`Font`](#font)|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|This property gets and sets the font that will be used to draw the text.|
|[`ForceHorizontalLabelForLine`](#forcehorizontallabelforline)|`Boolean`|This property gets and sets whether we should force horizontal labeling for lines.|
|[`ForceLineCarriage`](#forcelinecarriage)|`Boolean`|N/A|
|[`GridSize`](#gridsize)|`Int32`|N/A|
|[`HaloPen`](#halopen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This property gets and sets the halo pen you may use to draw a halo around the text.|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`LabelAllLineParts`](#labelalllineparts)|`Boolean`|N/A|
|[`LabelAllPolygonParts`](#labelallpolygonparts)|`Boolean`|N/A|
|[`LabelPositions`](#labelpositions)|Dictionary<`String`,[`WorldLabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.WorldLabelingCandidate.md)>|Gets a value represents a keyValuepair which is a feature id and label position of the feature|
|[`LeaderLineMinimumLengthInPixels`](#leaderlineminimumlengthinpixels)|`Single`|N/A|
|[`LeaderLineRule`](#leaderlinerule)|[`LabelLeaderLinesRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelLeaderLinesRule.md)|N/A|
|[`LeaderLineStyle`](#leaderlinestyle)|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|N/A|
|[`LetterCase`](#lettercase)|[`DrawingTextLetterCase`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextLetterCase.md)|N/A|
|[`Mask`](#mask)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This property gets and sets the AreaStyle used to draw a mask behind the text.|
|[`MaskMargin`](#maskmargin)|[`DrawingMargin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingMargin.md)|This property gets and sets the margin around the text that will be used for the mask.|
|[`MaskType`](#masktype)|[`MaskType`](../ThinkGeo.Core/ThinkGeo.Core.MaskType.md)|N/A|
|[`MaxCharAngleDelta`](#maxcharangledelta)|`Double`|N/A|
|[`MaxNudgingInPixel`](#maxnudginginpixel)|`Int32`|N/A|
|[`MinDistance`](#mindistance)|`Double`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`NudgingIntervalInPixel`](#nudgingintervalinpixel)|`Single`|N/A|
|[`NumericFormat`](#numericformat)|`String`|This property gets and sets the format that will be applied to the text which can be parsed to double type.|
|[`OverlappingRule`](#overlappingrule)|[`LabelOverlappingRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelOverlappingRule.md)|N/A|
|[`PolygonLabelingLocationMode`](#polygonlabelinglocationmode)|[`PolygonLabelingLocationMode`](../ThinkGeo.Core/ThinkGeo.Core.PolygonLabelingLocationMode.md)|N/A|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`RotationAngle`](#rotationangle)|`Double`|This property gets and sets the rotation angle of the item being positioned.|
|[`Spacing`](#spacing)|`Double`|N/A|
|[`SplineType`](#splinetype)|[`SplineType`](../ThinkGeo.Core/ThinkGeo.Core.SplineType.md)|Gets or sets the SplineType for labeling.|
|[`SuppressPartialLabels`](#suppresspartiallabels)|`Boolean`|N/A|
|[`TextBrush`](#textbrush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This property gets and sets the SolidBrush that will be used to draw the text.|
|[`TextColumnName`](#textcolumnname)|`String`|This property gets and sets the column name in the data that you want to get the text from.|
|[`TextFormat`](#textformat)|`String`|This property gets and sets the format that will be applied to the text.|
|[`TextLineSegmentRatio`](#textlinesegmentratio)|`Double`|N/A|
|[`TextPlacement`](#textplacement)|[`TextPlacement`](../ThinkGeo.Core/ThinkGeo.Core.TextPlacement.md)|N/A|
|[`WrapWidth`](#wrapwidth)|`Double`|N/A|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|This property gets and sets the X pixel offset for drawing each feature.|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|This property gets and sets the Y pixel offset for drawing each feature.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AllowOverlapping`](#allowoverlapping)|`Boolean`|N/A|
|[`AllowSpline`](#allowspline)|`Boolean`|N/A|
|[`DrawBasePointWithoutText`](#drawbasepointwithouttext)|`Boolean`|N/A|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|
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
|[`CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor)`](#createmasktextstylestringstringsingledrawingfontstylesgeocolorgeocolor)|
|[`CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single,Single)`](#createmasktextstylestringstringsingledrawingfontstylesgeocolorgeocolorsinglesingle)|
|[`CreateMaskTextStyle(String,GeoFont,GeoBrush,AreaStyle,Single,Single)`](#createmasktextstylestringgeofontgeobrushareastylesinglesingle)|
|[`CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single)`](#createmasktextstylestringstringsingledrawingfontstylesgeocolorgeocolorsingle)|
|[`CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single,Single,Single)`](#createmasktextstylestringstringsingledrawingfontstylesgeocolorgeocolorsinglesinglesingle)|
|[`CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor)`](#createsimpletextstylestringstringsingledrawingfontstylesgeocolor)|
|[`CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor,Single,Single)`](#createsimpletextstylestringstringsingledrawingfontstylesgeocolorsinglesingle)|
|[`CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single)`](#createsimpletextstylestringstringsingledrawingfontstylesgeocolorgeocolorsingle)|
|[`CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single,Single,Single)`](#createsimpletextstylestringstringsingledrawingfontstylesgeocolorgeocolorsinglesinglesingle)|
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
|[`Parse(JObject)`](#parsejobject)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`Formatting`](#formatting)|[`FormattingPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattingPositionStyleEventArgs.md)|N/A|
|[`Formatted`](#formatted)|[`FormattedPositionStyleEventArgs`](../ThinkGeo.Core/ThinkGeo.Core.FormattedPositionStyleEventArgs.md)|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`TextStyle()`](#textstyle)|
|[`TextStyle(String,GeoFont,GeoBrush)`](#textstylestringgeofontgeobrush)|

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

   *This property returns a collection of area styles allowing you to stack multiple area styles on top of each other.*

**Remarks**

   *Using this collection, you can stack multiple area styles on top of each other. When we draw the features, we will draw them in order that they exist in the collection. You can use these stacks to create drop shadow effects, multiple colored outlines, etc.*

**Return Value**

Collection<[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)>

---
#### `DateFormat`

**Summary**

   *This property gets and sets the format that will be applied to the text which can be parsed to DateTime type.*

**Remarks**

   *With this property, you can apply formats to the text that is retrieved from the feature.*

**Return Value**

`String`

---
#### `DrawingLevel`

**Summary**

   *Gets or sets the DrawingLavel for this style.*

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

   *This property gets and sets whether the labeler will try to fit the label as best as it can on the visible part of a line on the screen.*

**Remarks**

   *A label will normally be displayed in the center of a line. If only a small piece of the line is visible on the screen, we cannot see it's label by default. If we set this property to ture though, the label will be displayed in the center of that piece in screen.*

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

   *This property gets and sets whether the labeler will try to fit the label as best as it can on the visible part of a polygon on the screen.*

**Remarks**

   *A label will normally be displayed in the center of a polygon. If only a small piece of the polygon is visible on the screen, we cannot see it's label by default. If we set this property to ture though, the label will be displayed in the center of that piece in screen.*

**Return Value**

`Boolean`

---
#### `Font`

**Summary**

   *This property gets and sets the font that will be used to draw the text.*

**Remarks**

   *None*

**Return Value**

[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)

---
#### `ForceHorizontalLabelForLine`

**Summary**

   *This property gets and sets whether we should force horizontal labeling for lines.*

**Remarks**

   *Normally, lines are labeled in the direction of the line. There may be some cases, however, when you want to have the line labeled horizontally regardless of the line's direction. In such a case, you can set this property to force the lines to be labeled horizontally.*

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

   *This property gets and sets the halo pen you may use to draw a halo around the text.*

**Remarks**

   *The halo pen allows you to draw a halo effect around the text, making it stand out more on a busy background.*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

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

   *Gets a value represents a keyValuepair which is a feature id and label position of the feature*

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

   *This property gets and sets the AreaStyle used to draw a mask behind the text.*

**Remarks**

   *A mask is a plate behind the text that is rectangular and slightly larger than the width and height of the text. It allows the label to stand out well on a busy background. You can also try the HaloPen property instead of the mask, if the mask effect is too pronounced.*

**Return Value**

[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)

---
#### `MaskMargin`

**Summary**

   *This property gets and sets the margin around the text that will be used for the mask.*

**Remarks**

   *This determines how much larger the mask is than the text, in pixels.*

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

   *This property gets and sets the format that will be applied to the text which can be parsed to double type.*

**Remarks**

   *With this property, you can apply formats to the text that is retrieved from the feature.*

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

   *This property gets and sets the rotation angle of the item being positioned.*

**Remarks**

   *None*

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

   *Gets or sets the SplineType for labeling.*

**Remarks**

   *N/A*

**Return Value**

[`SplineType`](../ThinkGeo.Core/ThinkGeo.Core.SplineType.md)

---
#### `SuppressPartialLabels`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `TextBrush`

**Summary**

   *This property gets and sets the SolidBrush that will be used to draw the text.*

**Remarks**

   *You can use this property to draw a solid color; however, if you need to use other brushes, you can access them through the Advanced property of this class.*

**Return Value**

[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)

---
#### `TextColumnName`

**Summary**

   *This property gets and sets the column name in the data that you want to get the text from.*

**Remarks**

   *This property is used when retrieving text from a feature. You will want to specify the name of the column that contains the text you want to draw.*

**Return Value**

`String`

---
#### `TextFormat`

**Summary**

   *This property gets and sets the format that will be applied to the text.*

**Remarks**

   *With this property, you can apply formats to the text that is retrieved from the feature.*

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

   *This property allows you to specify a Y offset. When combined with an X offset, it is useful to allow you to achieve effects such as drop shadows, etc. There also may be times when you need to modify the location of feature data so as to better align it with raster satellite data.*

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
#### `CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|
|maskFillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the mask fill color.|

---
#### `CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single,Single)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|
|maskFillColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the mask fill color.|
|xOffset|`Single`|The float value stands for the xOffset of the font on the map in pixel|
|yOffset|`Single`|The float value stands for the yOffset of the font on the map in pixel|

---
#### `CreateMaskTextStyle(String,GeoFont,GeoBrush,AreaStyle,Single,Single)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|textFont|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|The GeoFont used to set the font of the text.|
|textBrush|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|The GeoSolidBrush used to set the brush of the text.|
|areaStyle|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|The areaStyle used as mask of of the TextStyle.|
|xOffset|`Single`|The float value stands for the xOffset of the font on the map in pixel|
|yOffset|`Single`|The float value stands for the yOffset of the font on the map in pixel|

---
#### `CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|
|maskPenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the mask pen color.|
|maskPenSize|`Single`|The float value used to set the mask pen size.|

---
#### `CreateMaskTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single,Single,Single)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|
|maskPenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the mask pen color.|
|maskPenSize|`Single`|The float value used to set the mask pen size.|
|xOffset|`Single`|The float value stands for the xOffset of the font on the map in pixel|
|yOffset|`Single`|The float value stands for the yOffset of the font on the map in pixel|

---
#### `CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|

---
#### `CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor,Single,Single)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|
|xOffset|`Single`|The float value stands for the xOffset of the font on the map in pixel|
|yOffset|`Single`|The float value stands for the yOffset of the font on the map in pixel|

---
#### `CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|
|haloPenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the halopen color.|
|haloPenWidth|`Single`|The float value to set the halopen width value.|

---
#### `CreateSimpleTextStyle(String,String,Single,DrawingFontStyles,GeoColor,GeoColor,Single,Single,Single)`

**Summary**

   *Get simple TextStyle.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|The desired TextStyle.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|textColumnName|`String`|The string stands for the column name.|
|fontFamilyName|`String`|The string stands for the font family name. For example : "Arial".|
|fontSize|`Single`|The float number stands for the font size.|
|drawingFontStyle|[`DrawingFontStyles`](../ThinkGeo.Core/ThinkGeo.Core.DrawingFontStyles.md)|The DrawingFontStyles used to set the style of the font.|
|fontColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the font color.|
|haloPenColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|The GeoColor used to set the halopen color.|
|haloPenWidth|`Single`|The float value to set the halopen width value.|
|xOffset|`Single`|The float value stands for the xOffset of the font on the map in pixel|
|yOffset|`Single`|The float value stands for the yOffset of the font on the map in pixel|

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
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|N/A|

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
#### `Parse(JObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|jObject|[`JObject`](../ThinkGeo.Core/ThinkGeo.Core.JObject.md)|N/A|

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


