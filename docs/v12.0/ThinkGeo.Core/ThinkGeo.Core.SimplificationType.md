# SimplificationType

**Summary**

N/A

**Remarks**

N/A

**Items**

|Name|Description|
|---|---|
|TopologyPreserving|Simplifies a point, ensuring that the result is a valid point having the same dimension and number of components as the input. The simplification uses a maximum distance difference algorithm similar to the one used in the Douglas-Peucker algorithm. In particular, if the input is an areal point ( Polygon or MultiPolygon ) The result has the same number of shells and holes (rings) as the input, in the same order The result rings touch at no more than the number of touching point in the input (although they may touch at fewer points).|
|DouglasPeucker|Simplifies a Geometry using the standard Douglas-Peucker algorithm. Ensures that any polygonal geometries returned are valid. Simple lines are not guaranteed to remain simple after simplification. Note that in general D-P does not preserve topology - e.g. polygons can be split, collapse to lines or disappear holes can be created or disappear, and lines can cross. To simplify point while preserving topology use TopologySafeSimplifier. (However, using D-P is significantly faster).|

