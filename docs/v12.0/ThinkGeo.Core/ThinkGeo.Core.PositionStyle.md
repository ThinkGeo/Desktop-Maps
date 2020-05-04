# PositionStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + **`PositionStyle`**
      + [`IconValueStyle`](../ThinkGeo.Core/ThinkGeo.Core.IconValueStyle.md)
      + [`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|[`PositionStyle()`](#positionstyle)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AbbreviationDictionary`](#abbreviationdictionary)|Dictionary<`String`,`String`>|N/A|
|[`AllowLineCarriage`](#allowlinecarriage)|`Boolean`|This property gets and sets whether the labeler will allow carriage returns to be inserted.|
|[`BestPlacementSymbolHeight`](#bestplacementsymbolheight)|`Single`|N/A|
|[`BestPlacementSymbolWidth`](#bestplacementsymbolwidth)|`Single`|N/A|
|[`DuplicateRule`](#duplicaterule)|[`LabelDuplicateRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelDuplicateRule.md)|This property gets and sets the rule that determines how duplicate labels are handled.|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`FittingPolygon`](#fittingpolygon)|`Boolean`|This property gets and sets whether the labeler will try to fit the label as best as it can within the boundary of a polygon.|
|[`FittingPolygonFactor`](#fittingpolygonfactor)|`Double`|This property gets and sets the factor to which it will keep the label inside of the polygon.|
|[`ForceLineCarriage`](#forcelinecarriage)|`Boolean`|This property gets and sets whether the labeler will force carriage returns to be inserted.|
|[`GridSize`](#gridsize)|`Int32`|This property gets and sets the grid size used for deterministic labeling.|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`LabelAllLineParts`](#labelalllineparts)|`Boolean`|This property gets and sets whether the labeler will label every part of a multi-part line.|
|[`LabelAllPolygonParts`](#labelallpolygonparts)|`Boolean`|This property gets and sets whether the labeler will label every part of a multi-part polygon.|
|[`LeaderLineMinimumLengthInPixels`](#leaderlineminimumlengthinpixels)|`Single`|N/A|
|[`LeaderLineRule`](#leaderlinerule)|[`LabelLeaderLinesRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelLeaderLinesRule.md)|N/A|
|[`LeaderLineStyle`](#leaderlinestyle)|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|N/A|
|[`MaskType`](#masktype)|[`MaskType`](../ThinkGeo.Core/ThinkGeo.Core.MaskType.md)|N/A|
|[`MaxNudgingInPixel`](#maxnudginginpixel)|`Int32`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`NudgingIntervalInPixel`](#nudgingintervalinpixel)|`Single`|N/A|
|[`OverlappingRule`](#overlappingrule)|[`LabelOverlappingRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelOverlappingRule.md)|This property gets and sets the rule that determines how overlapping labels are handled.|
|[`PolygonLabelingLocationMode`](#polygonlabelinglocationmode)|[`PolygonLabelingLocationMode`](../ThinkGeo.Core/ThinkGeo.Core.PolygonLabelingLocationMode.md)|This property gets and sets the mode that determines how to locate polygon's labeling|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`SuppressPartialLabels`](#suppresspartiallabels)|`Boolean`|This property gets and sets whether a partial label in the current extent will be drawn or not.|
|[`TextLineSegmentRatio`](#textlinesegmentratio)|`Double`|This property gets and sets the ratio required for the label length to match the line length.|
|[`TextPlacement`](#textplacement)|[`TextPlacement`](../ThinkGeo.Core/ThinkGeo.Core.TextPlacement.md)|This property gets and sets the location of the label for point features relative to the point.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Alignment`](#alignment)|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|N/A|
|[`AllowOverlapping`](#allowoverlapping)|`Boolean`|N/A|
|[`AllowSpline`](#allowspline)|`Boolean`|This property gets and sets whether line labels are allowed to spline around curved lines.|
|[`BasePoint`](#basepoint)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|
|[`CustomTextStyles`](#customtextstyles)|Collection<[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)>|This property returns a collection of area styles, allowing you to stack multiple area styles on top of each other.|
|[`DateFormat`](#dateformat)|`String`|This property gets and sets the format that will be applied to the text which can be parsed to DateTime type.|
|[`DrawBasePointWithoutText`](#drawbasepointwithouttext)|`Boolean`|N/A|
|[`DrawingLevel`](#drawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|Gets or sets the DrawingLavel for this style.|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`FittingLineInScreen`](#fittinglineinscreen)|`Boolean`|This property gets and sets whether the labeler will try to fit the label as best as it can on the visible part of a line on the screen.|
|[`FittingPolygonInScreen`](#fittingpolygoninscreen)|`Boolean`|This property gets and sets whether the labeler will try to fit the label as best as it can on the visible part of a polygon on the screen.|
|[`Font`](#font)|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|This property gets and sets the font that will be used to draw the text.|
|[`ForceHorizontalLabelForLine`](#forcehorizontallabelforline)|`Boolean`|This property gets and sets whether we should force horizontal labeling for lines.|
|[`HaloPen`](#halopen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|This property gets and sets the halo pen you may use to draw a halo around the text.|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|
|[`IsStyleJsonStyle`](#isstylejsonstyle)|`Boolean`|N/A|
|[`LabelFeatures`](#labelfeatures)|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|[`LabelPositions`](#labelpositions)|Dictionary<`String`,[`WorldLabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.WorldLabelingCandidate.md)>|Gets a value represents a keyValuepair which is a feature id and label position of the feature|
|[`LetterCase`](#lettercase)|[`DrawingTextLetterCase`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextLetterCase.md)|N/A|
|[`Mask`](#mask)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|This property gets and sets the AreaStyle used to draw a mask behind the text.|
|[`MaskMargin`](#maskmargin)|[`DrawingMargin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingMargin.md)|This property gets and sets the margin around the text that will be used for the mask.|
|[`MaxCharAngleDelta`](#maxcharangledelta)|`Double`|N/A|
|[`MinDistance`](#mindistance)|`Double`|N/A|
|[`NumericFormat`](#numericformat)|`String`|This property gets and sets the format that will be applied to the text which can be parsed to double type.|
|[`RotationAngle`](#rotationangle)|`Double`|This property gets and sets the rotation angle of the item being positioned.|
|[`Spacing`](#spacing)|`Double`|N/A|
|[`SplineType`](#splinetype)|[`SplineType`](../ThinkGeo.Core/ThinkGeo.Core.SplineType.md)|Gets or sets the SplineType for labeling.|
|[`TextBaseline`](#textbaseline)|[`DrawingTextBaseline`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextBaseline.md)|N/A|
|[`TextBrush`](#textbrush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|This property gets and sets the SolidBrush that will be used to draw the text.|
|[`TextColumnName`](#textcolumnname)|`String`|This property gets and sets the column name in the data that you want to get the text from.|
|[`TextContent`](#textcontent)|`String`|N/A|
|[`TextFormat`](#textformat)|`String`|This property gets and sets the format that will be applied to the text.|
|[`TextLetterSpacing`](#textletterspacing)|`Double`|N/A|
|[`TextLineSpacing`](#textlinespacing)|`Single`|N/A|
|[`WrapWidth`](#wrapwidth)|`Double`|N/A|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|This property gets and sets the X pixel offset used for drawing each feature.|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|This property gets and sets the Y pixel offset used for drawing each feature.|

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
|[`ConvertToScreenShape(Feature,GeoCanvas)`](#converttoscreenshapefeaturegeocanvas)|
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
|N/A|

### Protected Constructors

#### `PositionStyle()`

**Summary**

   *This is the default constructor for the class.*

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

#### `AbbreviationDictionary`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,`String`>

---
#### `AllowLineCarriage`

**Summary**

   *This property gets and sets whether the labeler will allow carriage returns to be inserted.*

**Remarks**

   *This property enables the labeler to split long labels into multiple lines if need be. For instance, if you have a lake whose name is "Southern Homestead Lake," then the labeler may try and break the name onto multiple lines in order to better label the feature.*

**Return Value**

`Boolean`

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
#### `DuplicateRule`

**Summary**

   *This property gets and sets the rule that determines how duplicate labels are handled.*

**Remarks**

   *There are three ways to handle duplicate label names. The first is to suppress all duplicates, which means if there are two street segments with the same name then only one will be drawn. The second way is to suppress duplicate labels only if they are in one quarter of the screen. In this way, the screen will be divided into four quadrants, and if the two duplicate labels are in different quadrants, then they will both draw. The last way is to draw all duplicates.*

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
#### `FittingPolygon`

**Summary**

   *This property gets and sets whether the labeler will try to fit the label as best as it can within the boundary of a polygon.*

**Remarks**

   *None*

**Return Value**

`Boolean`

---
#### `FittingPolygonFactor`

**Summary**

   *This property gets and sets the factor to which it will keep the label inside of the polygon.*

**Remarks**

   *None*

**Return Value**

`Double`

---
#### `ForceLineCarriage`

**Summary**

   *This property gets and sets whether the labeler will force carriage returns to be inserted.*

**Remarks**

   *This property forces the labeler to split long labels into multiple lines. For instance, if you have a lake whose name is "Southern Homestead Lake," then the labeler will break the name onto multiple lines in order to better label the feature.*

**Return Value**

`Boolean`

---
#### `GridSize`

**Summary**

   *This property gets and sets the grid size used for deterministic labeling.*

**Remarks**

   *The grid size determines how many labels will be considered as candidates for drawing. The smaller the grid size, the higher the density of candidates. Making the grid size too small may have a performance impact.*

**Return Value**

`Int32`

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

   *This property gets and sets whether the labeler will label every part of a multi-part line.*

**Remarks**

   *In some cases, you may want to label all of the parts of a multi-part line, while in other cases you may not.*

**Return Value**

`Boolean`

---
#### `LabelAllPolygonParts`

**Summary**

   *This property gets and sets whether the labeler will label every part of a multi-part polygon.*

**Remarks**

   *In some cases, you may want to label all of the parts of a multi-part polygon, while in other cases you may not. For example, you may have a series of lakes where you do want to label each polygon. In another case, you may have a country with many small islands and in this case you only want to label the largest polygon.*

**Return Value**

`Boolean`

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
#### `MaskType`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`MaskType`](../ThinkGeo.Core/ThinkGeo.Core.MaskType.md)

---
#### `MaxNudgingInPixel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

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
#### `OverlappingRule`

**Summary**

   *This property gets and sets the rule that determines how overlapping labels are handled.*

**Remarks**

   *This defines the rules for label overlapping. Currently, either we allow overlapping or we do not. In the future, we may extend this to allow some percentage of partial overlapping.*

**Return Value**

[`LabelOverlappingRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelOverlappingRule.md)

---
#### `PolygonLabelingLocationMode`

**Summary**

   *This property gets and sets the mode that determines how to locate polygon's labeling*

**Remarks**

   *There are two ways to handle polygon's labeling location. The first is to use polygon's centroid as the labeling location, the second way is to use polygon's boungdingbox center as the labeling location.*

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
#### `SuppressPartialLabels`

**Summary**

   *This property gets and sets whether a partial label in the current extent will be drawn or not.*

**Remarks**

   *This property provides a solution to the "cut off" label issue in Map Suite Web Edition and Desktop Edition, which occurs when multiple tiles exist. When you set this property to true, any labels outside of the current extent will not be drawn.*

**Return Value**

`Boolean`

---
#### `TextLineSegmentRatio`

**Summary**

   *This property gets and sets the ratio required for the label length to match the line length.*

**Remarks**

   *This allows you to suppress labels where the label length would greatly exceed the line length. For example, if you set the ratio to 1, then the label will be suppressed if it is longer than the line. If the ratio is lower, then the label would need to be shorter than the line. If higher, then the label is allowed to run past the length of the line. This allows you to control the look of things like road labeling.*

**Return Value**

`Double`

---
#### `TextPlacement`

**Summary**

   *This property gets and sets the location of the label for point features relative to the point.*

**Remarks**

   *This property allows you to choose where the labels are created relative to the point. For example, you can set the property to RightCenter, which would ensure that all labels are placed to the right of and vertically centered with the point. Different kinds of point layers can be positioned differently. If the point layer is dense and position is not a main concern, then you can try the BestPlacement property. That property overrides this property and tries to fit the label in the best location so that the minimum number of labels are suppressed due to overlapping issues.*

**Return Value**

[`TextPlacement`](../ThinkGeo.Core/ThinkGeo.Core.TextPlacement.md)

---

### Protected Properties

#### `Alignment`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)

---
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

   *This property gets and sets whether line labels are allowed to spline around curved lines.*

**Remarks**

   *This property will allow the labeler to spline the label around curved lines. This is useful for curved streets that need to be labeled. This can have a considerable performance impact, so we suggest you experiment with it to ensure it can meet your needs.*

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
#### `CustomTextStyles`

**Summary**

   *This property returns a collection of area styles, allowing you to stack multiple area styles on top of each other.*

**Remarks**

   *Using this collection you can stack multiple area styles on top of each other. When we draw the feature we will draw them in order in the collection. You can use these stacks to create drop shadow effects along with multiple colored outlines etc.*

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
#### `DrawBasePointWithoutText`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `DrawingLevel`

**Summary**

   *Gets or sets the DrawingLavel for this style.*

**Remarks**

   *N/A*

**Return Value**

[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)

---
#### `FiltersCore`

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
#### `HaloPen`

**Summary**

   *This property gets and sets the halo pen you may use to draw a halo around the text.*

**Remarks**

   *The halo pen allows you to draw a halo effect around the text, making it stand out more on a busy background.*

**Return Value**

[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)

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
#### `LabelPositions`

**Summary**

   *Gets a value represents a keyValuepair which is a feature id and label position of the feature*

**Remarks**

   *N/A*

**Return Value**

Dictionary<`String`,[`WorldLabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.WorldLabelingCandidate.md)>

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
#### `MaxCharAngleDelta`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MinDistance`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `NumericFormat`

**Summary**

   *This property gets and sets the format that will be applied to the text which can be parsed to double type.*

**Remarks**

   *With this property, you can apply formats to the text that is retrieved from the feature.*

**Return Value**

`String`

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
#### `TextBaseline`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`DrawingTextBaseline`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextBaseline.md)

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
#### `TextContent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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

   *This property gets and sets the X pixel offset used for drawing each feature.*

**Remarks**

   *This property allows you to specify an X offset. When combined with a Y offset, it is useful to allow you to achieve effects such as drop shadows, etc. There also may be times when you need to modify the location of feature data so as to better align it with raster satellite data.*

**Return Value**

`Single`

---
#### `YOffsetInPixel`

**Summary**

   *This property gets and sets the Y pixel offset used for drawing each feature.*

**Remarks**

   *This property allows you to specify a Y offset. When combined with an X offset, it is useful to allow you to achieve effects such as drop shadows, etc. There also may be times when you need to modify the location of feature data so as to better align it with raster satellite data.*

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

   *This method will determine whether the label will be suppressed because it is a duplicate.*

**Remarks**

   *This method is the concrete wrapper for the abstract method CheckDuplicateCore. This method will determine if the label will be suppressed because it is a duplicate. It also takes into consideration the duplicate rules for the class. So, for example, if we set to allow duplicates, then the method will always return false. If the class is set to not allow duplicates and this label is a duplicate, then it will return true and be suppressed. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns whether the label will be suppressed as a duplicate.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|This parameter is the labeling candidate that will be checked to determine if it is a duplicate.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view used for calculations.|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

---
#### `CheckDuplicateCore(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method will determine if the label will be suppressed because it is a duplicate.*

**Remarks**

   *This overridden method is called from the concrete public method CheckDuplicate. This method will determine if the label will be suppressed because it is a duplicate. It also takes into consideration the duplicate rules for the class. So, for example, if we set to allow duplicates, then the method will always return false. If the class is set to not allow duplicates and this label is a duplicate, then it will return true and be suppressed.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns whether the label will be suppressed as a duplicate.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|This parameter is the labeling candidate that will be checked to determine if it is a duplicate.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used for calculations.|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

---
#### `CheckOverlapping(LabelingCandidate,GeoCanvas,GeoFont,Single,Single,Double,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method will determine if the label will be suppressed because of overlapping.*

**Remarks**

   *This method is the concrete wrapper for the abstract method CheckOverlappingCore. This method will determine if the label will be suppressed because it is overlapping another label. It also takes into consideration the overlapping rules for the class. So, for example, if we set to allow overlap, then the method will always return false. If the class is set to not allow overlap and this label is overlapping, then it will return true and be suppressed. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns whether the label will be suppressed because of overlapping.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|This parameter is the labeling candidate that will be checked to determine if it is overlapping.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used for calculations.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|xOffsetInPixel|`Single`|N/A|
|yOffsetInPixel|`Single`|N/A|
|rotationAngle|`Double`|N/A|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

---
#### `CheckOverlappingCore(LabelingCandidate,GeoCanvas,Collection<SimpleCandidate>,Collection<SimpleCandidate>)`

**Summary**

   *This method will determine whether the label will be suppressed because of overlapping.*

**Remarks**

   *This overridden method is called from the concrete public method CheckOverlapping. This method will determine if the label will be suppressed because it is overlapping another label. It also takes into consideration the overlapping rules for the class. So, for example, if we set to allow overlap, then the method will always return false. If the class is set to not allow overlap and this label is overlapping, then it will return true and be suppressed.*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns whether the label will be suppressed because of overlapping.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|labelingCandidate|[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)|This parameter is the labeling candidate that will be checked to determine if it is overlapping.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used for calculations.|
|labelsInThisLayer|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in the current layer only.|
|labelsInAllLayers|Collection<[`SimpleCandidate`](../ThinkGeo.Core/ThinkGeo.Core.SimpleCandidate.md)>|The labels will be drawn in all layers.|

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
#### `ConvertToScreenShape(Feature,GeoCanvas)`

**Summary**

   *This method converts a feature in world coordinates to screen coordinates.*

**Remarks**

   *This overridden method can be called by this class and its sub concrete classes. In this method, we take the view and the feature in world coordinates and convert it to screen coordinates.*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|A screen coordinate shape.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature to be converted from world coordinates.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used to convert the world coordinate feature to a screen coorindate feature.|

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

   *This method filters the features based on the grid size to facilitate deterministic labeling.*

**Remarks**

   *This method is the concrete wrapper for the abstract method FilterFeaturesCore. In this method, we filter the features based on the grid size to facilitate deterministic labeling. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns the features that will be considered for labeling.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter represents the features that will be filtered.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used for calculating font sizes.|

---
#### `FilterFeaturesCore(IEnumerable<Feature>,GeoCanvas)`

**Summary**

   *This method filters the features based on the grid size to facilitate deterministic labeling.*

**Remarks**

   *This overridden method is called from the concrete public method FilterFeatures. In this method, we filter the features based on the grid size to facilitate deterministic labeling.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns the features that will be considered for labeling.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This parameter represents the features that will be filtered.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used for calculating font sizes.|

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

   *This method determines whether the specified feature is a good candidate to be labeled, based on the labeling properties set.*

**Remarks**

   *This overridden method is called from the concrete public method GetLabelingCanidate. In this method, we take the feature you passed in and determine if it is a candidate for labeling. If it is, then we will add it to the return collection. The algorithm to determine whether the label will draw is complex and determined by a number of properties and factors.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)>|A collection of labeling candidates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature that will be considered as a labeling candidate.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used to draw the feature. This method will not draw on this view, but rather will use it to determine font size, etc.|
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

   *This method determines whether the specified feature is a good candidate to be labeled, based on the labeling properties set.*

**Remarks**

   *This method is the concrete wrapper for the abstract method GetLabelingCanidatesCore. This method determines if the feature passed in is a good candidate to be labeled based on the labeling properties set. As this is a concrete public method that wraps a Core method, we reserve the right to add events and other logic to pre- or post-process data returned by the Core version of the method. In this way, we leave our framework open on our end, but also allow you the developer to extend our logic to suit your needs. If you have questions about this, please contact our support team as we would be happy to work with you on extending our framework.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)>|A collection of labeling candidates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|feature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature that will be considered as a labeling candidate.|
|canvas|[`GeoCanvas`](../ThinkGeo.Core/ThinkGeo.Core.GeoCanvas.md)|This parameter is the view that will be used to draw the feature. This method will not draw on this view, but rather will use it to determine font size, etc.|
|font|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|xOffsetInPixel|`Single`|N/A|
|yOffsetInPixel|`Single`|N/A|
|rotationAngle|`Double`|N/A|

---
#### `GetRequiredColumnNamesCore()`

**Summary**

   *This method returns the column data for each feature that is required for the style to properly draw.*

**Remarks**

   *This abstract method is called from the concrete public method GetRequiredFieldNames. In this method, we return the column names that are required for the style to draw the feature properly. For example, if you have a style that colors areas blue when a certain column value is over 100, then you need to be sure you include that column name. This will ensure that the column data is returned to you in the feature when it is ready to draw. In many of the styles, we add properties to allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore, we read that property and add it to the collection.*

**Return Value**

|Type|Description|
|---|---|
|Collection<`String`>|This method returns a collection of the column names that it needs.|

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


