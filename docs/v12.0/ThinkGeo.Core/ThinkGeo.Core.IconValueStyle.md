# IconValueStyle


## Inheritance Hierarchy

+ `Object`
  + [`Style`](../ThinkGeo.Core/ThinkGeo.Core.Style.md)
    + [`PositionStyle`](../ThinkGeo.Core/ThinkGeo.Core.PositionStyle.md)
      + **`IconValueStyle`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`IconValueStyle()`](#iconvaluestyle)|
|[`IconValueStyle(String)`](#iconvaluestylestring)|
|[`IconValueStyle(String,IEnumerable<IconValueItem>)`](#iconvaluestylestringienumerable<iconvalueitem>)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AbbreviationDictionary`](#abbreviationdictionary)|Dictionary<`String`,`String`>|N/A|
|[`AllowLineCarriage`](#allowlinecarriage)|`Boolean`|N/A|
|[`BestPlacementSymbolHeight`](#bestplacementsymbolheight)|`Single`|N/A|
|[`BestPlacementSymbolWidth`](#bestplacementsymbolwidth)|`Single`|N/A|
|[`ColumnName`](#columnname)|`String`|This property gets and sets the column name that will be used for the drawing and matching.|
|[`DuplicateRule`](#duplicaterule)|[`LabelDuplicateRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelDuplicateRule.md)|N/A|
|[`Filters`](#filters)|Collection<`String`>|N/A|
|[`FittingPolygon`](#fittingpolygon)|`Boolean`|N/A|
|[`FittingPolygonFactor`](#fittingpolygonfactor)|`Double`|N/A|
|[`ForceLineCarriage`](#forcelinecarriage)|`Boolean`|N/A|
|[`GridSize`](#gridsize)|`Int32`|N/A|
|[`IconValueItems`](#iconvalueitems)|Collection<[`IconValueItem`](../ThinkGeo.Core/ThinkGeo.Core.IconValueItem.md)>|This property gets the collection of IconValueItems for matching.|
|[`IsActive`](#isactive)|`Boolean`|N/A|
|[`LabelAllLineParts`](#labelalllineparts)|`Boolean`|N/A|
|[`LabelAllPolygonParts`](#labelallpolygonparts)|`Boolean`|N/A|
|[`LeaderLineMinimumLengthInPixels`](#leaderlineminimumlengthinpixels)|`Single`|N/A|
|[`LeaderLineRule`](#leaderlinerule)|[`LabelLeaderLinesRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelLeaderLinesRule.md)|N/A|
|[`LeaderLineStyle`](#leaderlinestyle)|[`LineStyle`](../ThinkGeo.Core/ThinkGeo.Core.LineStyle.md)|N/A|
|[`MaskType`](#masktype)|[`MaskType`](../ThinkGeo.Core/ThinkGeo.Core.MaskType.md)|N/A|
|[`MaxNudgingInPixel`](#maxnudginginpixel)|`Int32`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`NudgingIntervalInPixel`](#nudgingintervalinpixel)|`Single`|N/A|
|[`OverlappingRule`](#overlappingrule)|[`LabelOverlappingRule`](../ThinkGeo.Core/ThinkGeo.Core.LabelOverlappingRule.md)|N/A|
|[`PolygonLabelingLocationMode`](#polygonlabelinglocationmode)|[`PolygonLabelingLocationMode`](../ThinkGeo.Core/ThinkGeo.Core.PolygonLabelingLocationMode.md)|N/A|
|[`RequiredColumnNames`](#requiredcolumnnames)|Collection<`String`>|N/A|
|[`SuppressPartialLabels`](#suppresspartiallabels)|`Boolean`|N/A|
|[`TextLineSegmentRatio`](#textlinesegmentratio)|`Double`|N/A|
|[`TextPlacement`](#textplacement)|[`TextPlacement`](../ThinkGeo.Core/ThinkGeo.Core.TextPlacement.md)|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Alignment`](#alignment)|[`DrawingTextAlignment`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextAlignment.md)|N/A|
|[`AllowOverlapping`](#allowoverlapping)|`Boolean`|N/A|
|[`AllowSpline`](#allowspline)|`Boolean`|N/A|
|[`BasePoint`](#basepoint)|[`PointStyle`](../ThinkGeo.Core/ThinkGeo.Core.PointStyle.md)|N/A|
|[`CustomTextStyles`](#customtextstyles)|Collection<[`TextStyle`](../ThinkGeo.Core/ThinkGeo.Core.TextStyle.md)>|N/A|
|[`DateFormat`](#dateformat)|`String`|N/A|
|[`DrawBasePointWithoutText`](#drawbasepointwithouttext)|`Boolean`|N/A|
|[`DrawingLevel`](#drawinglevel)|[`DrawingLevel`](../ThinkGeo.Core/ThinkGeo.Core.DrawingLevel.md)|N/A|
|[`FiltersCore`](#filterscore)|Collection<`String`>|N/A|
|[`FittingLineInScreen`](#fittinglineinscreen)|`Boolean`|N/A|
|[`FittingPolygonInScreen`](#fittingpolygoninscreen)|`Boolean`|N/A|
|[`Font`](#font)|[`GeoFont`](../ThinkGeo.Core/ThinkGeo.Core.GeoFont.md)|N/A|
|[`ForceHorizontalLabelForLine`](#forcehorizontallabelforline)|`Boolean`|N/A|
|[`HaloPen`](#halopen)|[`GeoPen`](../ThinkGeo.Core/ThinkGeo.Core.GeoPen.md)|N/A|
|[`IsDefault`](#isdefault)|`Boolean`|N/A|
|[`IsStyleDefault`](#isstyledefault)|`Boolean`|N/A|
|[`IsStyleJsonStyle`](#isstylejsonstyle)|`Boolean`|N/A|
|[`LabelFeatures`](#labelfeatures)|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|
|[`LabelPositions`](#labelpositions)|Dictionary<`String`,[`WorldLabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.WorldLabelingCandidate.md)>|N/A|
|[`LetterCase`](#lettercase)|[`DrawingTextLetterCase`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextLetterCase.md)|N/A|
|[`Mask`](#mask)|[`AreaStyle`](../ThinkGeo.Core/ThinkGeo.Core.AreaStyle.md)|N/A|
|[`MaskMargin`](#maskmargin)|[`DrawingMargin`](../ThinkGeo.Core/ThinkGeo.Core.DrawingMargin.md)|N/A|
|[`MaxCharAngleDelta`](#maxcharangledelta)|`Double`|N/A|
|[`MinDistance`](#mindistance)|`Double`|N/A|
|[`NumericFormat`](#numericformat)|`String`|N/A|
|[`RotationAngle`](#rotationangle)|`Double`|N/A|
|[`Spacing`](#spacing)|`Double`|N/A|
|[`SplineType`](#splinetype)|[`SplineType`](../ThinkGeo.Core/ThinkGeo.Core.SplineType.md)|N/A|
|[`TextBaseline`](#textbaseline)|[`DrawingTextBaseline`](../ThinkGeo.Core/ThinkGeo.Core.DrawingTextBaseline.md)|N/A|
|[`TextBrush`](#textbrush)|[`GeoBrush`](../ThinkGeo.Core/ThinkGeo.Core.GeoBrush.md)|N/A|
|[`TextColumnName`](#textcolumnname)|`String`|N/A|
|[`TextContent`](#textcontent)|`String`|N/A|
|[`TextFormat`](#textformat)|`String`|N/A|
|[`TextLetterSpacing`](#textletterspacing)|`Double`|N/A|
|[`TextLineSpacing`](#textlinespacing)|`Single`|N/A|
|[`WrapWidth`](#wrapwidth)|`Double`|N/A|
|[`XOffsetInPixel`](#xoffsetinpixel)|`Single`|N/A|
|[`YOffsetInPixel`](#yoffsetinpixel)|`Single`|N/A|

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
|[`IconValueStyle()`](#iconvaluestyle)|
|[`IconValueStyle(String)`](#iconvaluestylestring)|
|[`IconValueStyle(String,IEnumerable<IconValueItem>)`](#iconvaluestylestringienumerable<iconvalueitem>)|

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
#### `AllowLineCarriage`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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
#### `ColumnName`

**Summary**

   *This property gets and sets the column name that will be used for the drawing and matching.*

**Remarks**

   *This column name will be used to draw the text on the icon (if necessary) and to also match the value in the IconStyleItem.*

**Return Value**

`String`

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
#### `IconValueItems`

**Summary**

   *This property gets the collection of IconValueItems for matching.*

**Remarks**

   *You should create your IconValueItems and place them in this collection for consideration.*

**Return Value**

Collection<[`IconValueItem`](../ThinkGeo.Core/ThinkGeo.Core.IconValueItem.md)>

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
#### `SuppressPartialLabels`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

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

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

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
#### `HaloPen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

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
#### `LabelPositions`

**Summary**

   *N/A*

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

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

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

   *This method determines whether the specified feature is a good candidate to be labeled, based on the labeling properties set.*

**Remarks**

   *This overridden method is called from the concrete public method Draw. In this method, we take the feature you passed in and determine if it is a candidate for labeling. If it is, then we will add it to the return collection. The algorithm to determine whether the label will draw is complex and determined by a number of properties and factors.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`LabelingCandidate`](../ThinkGeo.Core/ThinkGeo.Core.LabelingCandidate.md)>|N/A|

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


