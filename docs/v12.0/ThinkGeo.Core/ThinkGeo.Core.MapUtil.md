# MapUtil


## Inheritance Hierarchy

+ `Object`
  + **`MapUtil`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|N/A|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|N/A|N/A|N/A|

### Public Methods Summary


|Name|
|---|
|[`AdjustExtentByRestrictions(RectangleShape,Single,Single,RectangleShape,Double,Double,GeographyUnit)`](#adjustextentbyrestrictionsrectangleshapesinglesinglerectangleshapedoubledoublegeographyunit)|
|[`ApplyDrawingMarginToExtent(RectangleShape,Single,Single,Single)`](#applydrawingmargintoextentrectangleshapesinglesinglesingle)|
|[`BuildFourColorColumn(String,Collection<Feature>)`](#buildfourcolorcolumnstringcollection<feature>)|
|[`CalculateExtent(PointShape,Double,GeographyUnit,Double,Double)`](#calculateextentpointshapedoublegeographyunitdoubledouble)|
|[`CenterAt(RectangleShape,PointShape,Single,Single)`](#centeratrectangleshapepointshapesinglesingle)|
|[`CenterAt(RectangleShape,Feature,Single,Single)`](#centeratrectangleshapefeaturesinglesingle)|
|[`CenterAt(RectangleShape,Single,Single,Single,Single)`](#centeratrectangleshapesinglesinglesinglesingle)|
|[`Equals(Object)`](#equalsobject)|
|[`GetBoundingBoxOfItems(IEnumerable<BaseShape>)`](#getboundingboxofitemsienumerable<baseshape>)|
|[`GetBoundingBoxOfItems(IEnumerable<Feature>)`](#getboundingboxofitemsienumerable<feature>)|
|[`GetDistance(PointShape,PointShape)`](#getdistancepointshapepointshape)|
|[`GetDrawingExtent(RectangleShape,Single,Single)`](#getdrawingextentrectangleshapesinglesingle)|
|[`GetHashCode()`](#gethashcode)|
|[`GetResolution(RectangleShape,Double,Double)`](#getresolutionrectangleshapedoubledouble)|
|[`GetResolutionFromScale(Double,GeographyUnit,Single)`](#getresolutionfromscaledoublegeographyunitsingle)|
|[`GetRotatedExtent(RectangleShape,Single,PointShape)`](#getrotatedextentrectangleshapesinglepointshape)|
|[`GetRotatedPoint(Double,Double,Single,PointShape)`](#getrotatedpointdoubledoublesinglepointshape)|
|[`GetRotatedPolygon(RectangleShape,Single,PointShape)`](#getrotatedpolygonrectangleshapesinglepointshape)|
|[`GetRotatedScreenPoint(Double,Double,Single,ScreenPointF)`](#getrotatedscreenpointdoubledoublesinglescreenpointf)|
|[`GetScale(RectangleShape,Single,GeographyUnit,Single)`](#getscalerectangleshapesinglegeographyunitsingle)|
|[`GetScale(GeographyUnit,RectangleShape,Double,Double,Single)`](#getscalegeographyunitrectangleshapedoubledoublesingle)|
|[`GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,PointShape,PointShape,Single,Single)`](#getscreendistancebetweentwoworldpointsrectangleshapepointshapepointshapesinglesingle)|
|[`GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,Feature,Feature,Single,Single)`](#getscreendistancebetweentwoworldpointsrectangleshapefeaturefeaturesinglesingle)|
|[`GetSnappedExtent(RectangleShape,GeographyUnit,Single,Single,ZoomLevelSet)`](#getsnappedextentrectangleshapegeographyunitsinglesinglezoomlevelset)|
|[`GetSnappedScale(RectangleShape,Single,GeographyUnit,ZoomLevelSet)`](#getsnappedscalerectangleshapesinglegeographyunitzoomlevelset)|
|[`GetSnappedScale(Double,ZoomLevelSet)`](#getsnappedscaledoublezoomlevelset)|
|[`GetSnappedZoomLevelIndex(Double,Collection<Double>)`](#getsnappedzoomlevelindexdoublecollection<double>)|
|[`GetSnappedZoomLevelIndex(RectangleShape,GeographyUnit,Collection<Double>,Double,Double)`](#getsnappedzoomlevelindexrectangleshapegeographyunitcollection<double>doubledouble)|
|[`GetSnappedZoomLevelIndex(Double,ZoomLevelSet)`](#getsnappedzoomlevelindexdoublezoomlevelset)|
|[`GetSnappedZoomLevelIndex(Double,Collection<Double>,Double,Double)`](#getsnappedzoomlevelindexdoublecollection<double>doubledouble)|
|[`GetType()`](#gettype)|
|[`GetVersion()`](#getversion)|
|[`GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,ScreenPointF,ScreenPointF,Single,Single,GeographyUnit,DistanceUnit)`](#getworlddistancebetweentwoscreenpointsrectangleshapescreenpointfscreenpointfsinglesinglegeographyunitdistanceunit)|
|[`GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,Single,Single,Single,Single,Single,Single,GeographyUnit,DistanceUnit)`](#getworlddistancebetweentwoscreenpointsrectangleshapesinglesinglesinglesinglesinglesinglegeographyunitdistanceunit)|
|[`Pan(RectangleShape,PanDirection,Int32)`](#panrectangleshapepandirectionint32)|
|[`Pan(RectangleShape,Single,Int32)`](#panrectangleshapesingleint32)|
|[`ToScreenCoordinate(BaseShape,RectangleShape,Single,Single)`](#toscreencoordinatebaseshaperectangleshapesinglesingle)|
|[`ToScreenCoordinate(RectangleShape,RectangleShape,Single,Single)`](#toscreencoordinaterectangleshaperectangleshapesinglesingle)|
|[`ToScreenCoordinate(RectangleShape,Double,Double,Single,Single)`](#toscreencoordinaterectangleshapedoubledoublesinglesingle)|
|[`ToScreenCoordinate(RectangleShape,PointShape,Single,Single)`](#toscreencoordinaterectangleshapepointshapesinglesingle)|
|[`ToScreenCoordinate(RectangleShape,Feature,Single,Single)`](#toscreencoordinaterectangleshapefeaturesinglesingle)|
|[`ToString()`](#tostring)|
|[`ToWorldCoordinate(RectangleShape,Double,Double,Double,Double)`](#toworldcoordinaterectangleshapedoubledoubledoubledouble)|
|[`ToWorldCoordinate(RectangleShape,Single,Single,Single,Single)`](#toworldcoordinaterectangleshapesinglesinglesinglesingle)|
|[`ToWorldCoordinate(RectangleShape,ScreenPointF,Single,Single)`](#toworldcoordinaterectangleshapescreenpointfsinglesingle)|
|[`ToWorldCoordinate(PolygonShape,RectangleShape,Single,Single)`](#toworldcoordinatepolygonshaperectangleshapesinglesingle)|
|[`ZoomIn(RectangleShape,Int32)`](#zoominrectangleshapeint32)|
|[`ZoomIntoCenter(RectangleShape,Int32,PointShape,Single,Single)`](#zoomintocenterrectangleshapeint32pointshapesinglesingle)|
|[`ZoomIntoCenter(RectangleShape,Int32,Feature,Single,Single)`](#zoomintocenterrectangleshapeint32featuresinglesingle)|
|[`ZoomIntoCenter(RectangleShape,Int32,Single,Single,Single,Single)`](#zoomintocenterrectangleshapeint32singlesinglesinglesingle)|
|[`ZoomOut(RectangleShape,Int32)`](#zoomoutrectangleshapeint32)|
|[`ZoomOutToCenter(RectangleShape,Int32,PointShape,Single,Single)`](#zoomouttocenterrectangleshapeint32pointshapesinglesingle)|
|[`ZoomOutToCenter(RectangleShape,Int32,Feature,Single,Single)`](#zoomouttocenterrectangleshapeint32featuresinglesingle)|
|[`ZoomOutToCenter(RectangleShape,Int32,Single,Single,Single,Single)`](#zoomouttocenterrectangleshapeint32singlesinglesinglesingle)|
|[`ZoomToScale(Double,RectangleShape,GeographyUnit,Single,Single)`](#zoomtoscaledoublerectangleshapegeographyunitsinglesingle)|
|[`ZoomToScale(Double,RectangleShape,GeographyUnit,Single,Single,ScreenPointF)`](#zoomtoscaledoublerectangleshapegeographyunitsinglesinglescreenpointf)|

### Protected Methods Summary


|Name|
|---|
|[`Finalize()`](#finalize)|
|[`MemberwiseClone()`](#memberwiseclone)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|N/A|

### Protected Constructors


### Public Properties


### Protected Properties


### Public Methods

#### `AdjustExtentByRestrictions(RectangleShape,Single,Single,RectangleShape,Double,Double,GeographyUnit)`

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
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|width|`Single`|N/A|
|height|`Single`|N/A|
|restrictExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|maximumScale|`Double`|N/A|
|minimumScale|`Double`|N/A|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|

---
#### `ApplyDrawingMarginToExtent(RectangleShape,Single,Single,Single)`

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
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|marginInPixel|`Single`|N/A|
|screenWidth|`Single`|N/A|
|screenHeight|`Single`|N/A|

---
#### `BuildFourColorColumn(String,Collection<Feature>)`

**Summary**

   *This method returns all features in the FeatureSource, the features contain the "Color" column. The column has a range of 1, 2, 3, 4, and each value represents a color.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|This method returns four color features in the FeatureSource.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|columnName|`String`|N/A|
|features|Collection<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|N/A|

---
#### `CalculateExtent(PointShape,Double,GeographyUnit,Double,Double)`

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
|worldCenter|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|scale|`Double`|N/A|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|mapWidth|`Double`|N/A|
|mapHeight|`Double`|N/A|

---
#### `CenterAt(RectangleShape,PointShape,Single,Single)`

**Summary**

   *This is a static function that allows you to pass in a world rectangle, a world point to center on, and a height and width in screen units.  The function will center the rectangle based on the point, then adjust the rectangle's ratio based on the height and width in screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent centered on a point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the current extent you want to center.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CenterAt(RectangleShape,Feature,Single,Single)`

**Summary**

   *This is a static function that allows you to pass in a world rectangle, a world point to center on, and a height and width in screen units.  The function will center the rectangle based on the point, then adjust the rectangle's ratio based on the height and width in screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent centered on a point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the current extent you want to center.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the world point you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `CenterAt(RectangleShape,Single,Single,Single,Single)`

**Summary**

   *This method returns an adjusted extent centered on a point.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent centered on a point.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the current extent you want to center.|
|screenX|`Single`|This parameter is the X coordinate on the screen to center on.|
|screenY|`Single`|This parameter is the Y coordinate on the screen to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

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
#### `GetBoundingBoxOfItems(IEnumerable<BaseShape>)`

**Summary**

   *This API gets the BoundingBox of a group of BaseShapes.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The BoundingBox that contains all of the shapes you passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shapes|IEnumerable<[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)>|The target group of BaseShapes to get the BoundingBox for.|

---
#### `GetBoundingBoxOfItems(IEnumerable<Feature>)`

**Summary**

   *This API gets the BoundingBox of a group of Features.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|The BoundingBox that contains all the features you passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|features|IEnumerable<[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)>|The target group of Features to get the BoundingBox for.|

---
#### `GetDistance(PointShape,PointShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|
|toPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetDrawingExtent(RectangleShape,Single,Single)`

**Summary**

   *This method returns an adjusted extent based on the ratio of the screen width and height.*

**Remarks**

   *This function is used because the extent to draw must be the rame ratio as the screen width and height. If they are not, then the image drawn will be stretched or compressed. We always adjust the extent upwards to ensure that no matter how we adjust it, the original extent will fit within the new extent. This ensures that everything you wanted to see in the first extent is visible and maybe a bit more.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an adjusted extent based on the ratio of the screen width and height.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to adjust for drawing.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

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
#### `GetResolution(RectangleShape,Double,Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|widthInPixel|`Double`|N/A|
|heightInPixel|`Double`|N/A|

---
#### `GetResolutionFromScale(Double,GeographyUnit,Single)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|N/A|
|unit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|dpi|`Single`|N/A|

---
#### `GetRotatedExtent(RectangleShape,Single,PointShape)`

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
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|rotatedAngle|`Single`|N/A|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetRotatedPoint(Double,Double,Single,PointShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|rotatedAngle|`Single`|N/A|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetRotatedPolygon(RectangleShape,Single,PointShape)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`AreaBaseShape`](../ThinkGeo.Core/ThinkGeo.Core.AreaBaseShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|rotatedAngle|`Single`|N/A|
|pivotPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

---
#### `GetRotatedScreenPoint(Double,Double,Single,ScreenPointF)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|x|`Double`|N/A|
|y|`Double`|N/A|
|rotatedAngle|`Single`|N/A|
|pivotPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|

---
#### `GetScale(RectangleShape,Single,GeographyUnit,Single)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Single`|N/A|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|dpi|`Single`|N/A|

---
#### `GetScale(GeographyUnit,RectangleShape,Double,Double,Single)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|boundingBox|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|widthInPixel|`Double`|N/A|
|heightInPixel|`Double`|N/A|
|dpi|`Single`|N/A|

---
#### `GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,PointShape,PointShape,Single,Single)`

**Summary**

   *This method returns the number of pixels between two world points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the number of pixels between two world points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPoint1|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the first point -- the one you want to measure from.|
|worldPoint2|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the second point -- the one you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `GetScreenDistanceBetweenTwoWorldPoints(RectangleShape,Feature,Feature,Single,Single)`

**Summary**

   *This method returns the number of pixels between two features.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Single`|This method returns the number of pixels between two features.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPointFeature1|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the first feature -- the one you want to measure from.|
|worldPointFeature2|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the second feature -- the one you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `GetSnappedExtent(RectangleShape,GeographyUnit,Single,Single,ZoomLevelSet)`

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
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|screenWidth|`Single`|N/A|
|screenHeight|`Single`|N/A|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

---
#### `GetSnappedScale(RectangleShape,Single,GeographyUnit,ZoomLevelSet)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenWidth|`Single`|N/A|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

---
#### `GetSnappedScale(Double,ZoomLevelSet)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|N/A|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

---
#### `GetSnappedZoomLevelIndex(Double,Collection<Double>)`

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
|scale|`Double`|N/A|
|zoomLevelScales|Collection<`Double`>|N/A|

---
#### `GetSnappedZoomLevelIndex(RectangleShape,GeographyUnit,Collection<Double>,Double,Double)`

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
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|mapUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|N/A|
|zoomLevelScales|Collection<`Double`>|N/A|
|actualWidth|`Double`|N/A|
|actualHeight|`Double`|N/A|

---
#### `GetSnappedZoomLevelIndex(Double,ZoomLevelSet)`

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
|scale|`Double`|N/A|
|zoomLevelSet|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|N/A|

---
#### `GetSnappedZoomLevelIndex(Double,Collection<Double>,Double,Double)`

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
|scale|`Double`|N/A|
|zoomLevelScales|Collection<`Double`>|N/A|
|minimumScale|`Double`|N/A|
|maximumScale|`Double`|N/A|

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
#### `GetVersion()`

**Summary**

   *Get the current ThinkGeo.Core.dll file version.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|A string representing the file version of MapSuiteCore.dll.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,ScreenPointF,ScreenPointF,Single,Single,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the distance in world units between two screen points.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Double`|This method returns the distance in world units between two screen points.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenPoint1|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This is the screen point you want to measure from.|
|screenPoint2|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This is the screen point you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the world extent rectangle.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the geographic unit you want the result to show in.|

---
#### `GetWorldDistanceBetweenTwoScreenPoints(RectangleShape,Single,Single,Single,Single,Single,Single,GeographyUnit,DistanceUnit)`

**Summary**

   *This method returns the distance in world units between two screen points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenPoint1X|`Single`|This parameter is the X of the point you want to measure from.|
|screenPoint1Y|`Single`|This parameter is the Y of the point you want to measure from.|
|screenPoint2X|`Single`|This parameter is the X of the point you want to measure to.|
|screenPoint2Y|`Single`|This parameter is the Y of the point you want to measure to.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This is the geographic unit of the world extent you passed in.|
|distanceUnit|[`DistanceUnit`](../ThinkGeo.Core/ThinkGeo.Core.DistanceUnit.md)|This is the geographic unit you want the result to show in.|

---
#### `Pan(RectangleShape,PanDirection,Int32)`

**Summary**

   *This method returns a panned extent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a panned extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to pan.|
|direction|[`PanDirection`](../ThinkGeo.Core/ThinkGeo.Core.PanDirection.md)|This parameter is the direction in which you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `Pan(RectangleShape,Single,Int32)`

**Summary**

   *This method returns a panned extent.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a panned extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to pan.|
|degree|`Single`|This parameter is the degree you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `ToScreenCoordinate(BaseShape,RectangleShape,Single,Single)`

**Summary**

   *This method returns BaseShape in screen coordinates from BaseShape in world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This method returns BaseShape in screen coordinates from BaseShape in world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|shape|[`BaseShape`](../ThinkGeo.Core/ThinkGeo.Core.BaseShape.md)|This parameter is the shape in world coordinate you want converted to a shape in screen coordinate.|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToScreenCoordinate(RectangleShape,RectangleShape,Single,Single)`

**Summary**

   *This method returns Rectangle in screen coordinates from RectangleShape in world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`DrawingRectangle`](../ThinkGeo.Core/ThinkGeo.Core.DrawingRectangle.md)|This method returns Rectangle in screen coordinates from RectangleShape in world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|targetWorldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the rectangle shape in world coordinate you want converted to a rectangle in screen coordinate.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToScreenCoordinate(RectangleShape,Double,Double,Single,Single)`

**Summary**

   *This method returns screen coordinates from world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldX|`Double`|This parameter is the world X you want converted to screen points.|
|worldY|`Double`|This parameter is the world Y you want converted to screen points.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToScreenCoordinate(RectangleShape,PointShape,Single,Single)`

**Summary**

   *This method returns screen coordinates from world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want converted to a screen point.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToScreenCoordinate(RectangleShape,Feature,Single,Single)`

**Summary**

   *This method returns screen coordinates from world coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This method returns screen coordinates from world coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|worldPointFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature you want converted to a screen point.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

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
#### `ToWorldCoordinate(RectangleShape,Double,Double,Double,Double)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|currentExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|screenX|`Double`|N/A|
|screenY|`Double`|N/A|
|screenWidth|`Double`|N/A|
|screenHeight|`Double`|N/A|

---
#### `ToWorldCoordinate(RectangleShape,Single,Single,Single,Single)`

**Summary**

   *This method returns world coordinates from screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns world coordinates from screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenX|`Single`|This parameter is the X of the point you want converted to world coordinates.|
|screenY|`Single`|This parameter is the Y of the point you want converted to world coordinates.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToWorldCoordinate(RectangleShape,ScreenPointF,Single,Single)`

**Summary**

   *This method returns world coordinates from screen coordinates.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This method returns world coordinates from screen coordinates.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent.|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This parameter is the screen point you want converted to a world point.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ToWorldCoordinate(PolygonShape,RectangleShape,Single,Single)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|simplyPolygon|[`PolygonShape`](../ThinkGeo.Core/ThinkGeo.Core.PolygonShape.md)|N/A|
|currentWorldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|canvasWidth|`Single`|N/A|
|canvasHeight|`Single`|N/A|

---
#### `ZoomIn(RectangleShape,Int32)`

**Summary**

   *This method returns a new extent that is zoomed in by the percentage provided.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a new extent that is zoomed in by the percentage provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to zoom to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|

---
#### `ZoomIntoCenter(RectangleShape,Int32,PointShape,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed in.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent that you want centered and zoomed to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width in screen coordinates.|
|screenHeight|`Single`|This parameter is the height in screen coordinates.|

---
#### `ZoomIntoCenter(RectangleShape,Int32,Feature,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent that you want centered and zoomed to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width in screen coordinates.|
|screenHeight|`Single`|This parameter is the height in screen coordinates.|

---
#### `ZoomIntoCenter(RectangleShape,Int32,Single,Single,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed in.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom in.|
|screenX|`Single`|This parameter is the screen X you want to center on.|
|screenY|`Single`|This parameter is the screen Y you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOut(RectangleShape,Int32)`

**Summary**

   *This method returns a new extent that is zoomed out by the percentage provided.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a new extent that is zoomed out by the percentage provided.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to zoom out to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|

---
#### `ZoomOutToCenter(RectangleShape,Int32,PointShape,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed out.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed out.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom out to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|This parameter is the world point you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOutToCenter(RectangleShape,Int32,Feature,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed out.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterwards.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed out.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom out to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|This parameter is the feature you want the extent to be centered on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomOutToCenter(RectangleShape,Int32,Single,Single,Single,Single)`

**Summary**

   *This method returns an extent that is centered and zoomed out.*

**Remarks**

   *The resulting rectangle will already be adjusted for the ratio of the screen. You do not need to call GetDrawingExtent afterward.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns an extent that is centered and zoomed out.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want to center and zoom out to.|
|percentage|`Int32`|This parameter is the percentage by which you want to zoom out.|
|screenX|`Single`|This parameter is the screen X you want to center on.|
|screenY|`Single`|This parameter is the screen Y you want to center on.|
|screenWidth|`Single`|This parameter is the width of the screen.|
|screenHeight|`Single`|This parameter is the height of the screen.|

---
#### `ZoomToScale(Double,RectangleShape,GeographyUnit,Single,Single)`

**Summary**

   *This method returns a extent that has been zoomed into a certain scale.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a extent that has been zoomed into a certain scale.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetScale|`Double`|This parameter is the scale you want to zoom into.|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want zoomed into the scale.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the geographic unit of the world extent parameter.|
|screenWidth|`Single`|This parameter is the screen width.|
|screenHeight|`Single`|This parameter is the screen height.|

---
#### `ZoomToScale(Double,RectangleShape,GeographyUnit,Single,Single,ScreenPointF)`

**Summary**

   *This method returns a extent that has been zoomed into a certain scale.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This method returns a extent that has been zoomed into a certain scale.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetScale|`Double`|This parameter is the scale you want to zoom into.|
|worldExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This parameter is the world extent you want zoomed into the scale.|
|worldExtentUnit|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|This parameter is the geographic unit of the world extent parameter.|
|screenWidth|`Single`|This parameter is the screen width.|
|screenHeight|`Single`|This parameter is the screen height.|
|offsetScreenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|This parameter is the offsetScreenPoint.|

---

### Protected Methods

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

### Public Events


