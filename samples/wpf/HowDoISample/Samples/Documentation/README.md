---
document_type: thinkgeo-howdoi-sample-index
generated_on: 2026-02-25
sample_count: 143
schema_version: 2.4
machine_readable: ../samples.json
group_by: samples_json_order
table_columns: [title, focus, key_api]
---

## Category Tables

### Documentation (1)

This category explains how the HowDoI sample index is organized and how to use it. It helps you quickly locate samples and understand the metadata for both humans and AI.

| Title | Focus | Key API |
| --- | --- | --- |
| HowDoI Sample Feature Index | Generated catalog of all samples with focus, categories, source path, and key APIs |  |

### Map Navigation (8)

This category demonstrates core map view interactions such as zooming, panning, rotating, and extent control. The samples focus on practical navigation behavior and responsive visual updates.

| Title | Focus | Key API |
| --- | --- | --- |
| Map Navigation | Zoom, pan, and rotate the map control | ZoomToAsync |
| Zoom to the Black Hole | Zoom into images by giving them scales and center points | GeoImageLayer |
| Zoom to Extents | Set the map extent in a variety of different ways | ZoomToAsync |
| Vehicle Navigation | Navigate to the Empire State Building using GPS points | FeatureLayerWpfDrawingOverlay |
| Resize the Map | Resize the map while preserving the world extent with various map resize modes | MapResizeMode |
| Dynamic Rendering | Dynamically refreshing the shapes | DynamicPointStyle |
| Overview Map | Overview map with rotating frame; drag to recenter the main map | EditOverlay |
| Restrict Map Extent | Restrict the map's panning area and zoom range using RestrictExtent, MaximumScale, and MinimumScale | RestrictExtent |

### Map Online Data (14)

This category demonstrates how to connect online basemaps and feature services. It covers ThinkGeo Cloud layers, OGC services, and third-party online providers.

| Title | Focus | Key API |
| --- | --- | --- |
| ThinkGeo Raster Map | Display ThinkGeo cloud raster background images | ThinkGeoCloudRasterMapsOverlay |
| Raster Map from XYZ Server | Display the tiles from a raster XYZ server. This demo server has 20 zoom levels (zoom 0~19) | ThinkGeoRasterMapsAsyncLayer |
| ThinkGeo Vector Map | Display ThinkGeo cloud vector background maps | ThinkGeoCloudVectorMapsOverlay |
| Azure Map | Display an Azure Maps layer | AzureMapsRasterOverlay |
| Google Map | Display a Google Maps layer | GoogleMapsOverlay |
| NOAA Weather Stations | Display NOAA weather stations current readings on your map | NoaaWeatherStationFeatureLayer |
| NOAA Weather Warnings | Display NOAA weather warnings on your map | NoaaWeatherWarningsFeatureLayer |
| OGC API Feature Server | Display a OGC API Feature layer | OgcApiFeatureLayer |
| Open Street Map | Display an OpenStreetMap layer | OpenStreetMapOverlay |
| MVT | Display a vector map from an MVT server | MvtTilesAsyncLayer |
| MVT with Local Fonts | Display a vector map from an MVT server, using local ttf fonts file | MvtTilesAsyncLayer |
| WFS | Display a WFS V2 Feature Layer.(This WFSV2 server may be slow ) | WfsV2AsyncLayer |
| WMS | Display a WMS layer. (This sample may be slow as we use a public WMS server) | WmsAsyncLayer |
| WMTS | Display a WMTS layer. (This sample may be slow as we use a public WMTS server) | WmtsOverlay |

### Map Offline Data (27)

This category shows how to load and render offline local data from common vector, raster, and database formats. The samples focus on layer setup, projection alignment, and display styling.

| Title | Focus | Key API |
| --- | --- | --- |
| Display a GeoPackage File | Show the GeoPackage File | GdalFeatureLayer |
| Display Raster from MBTiles | Display a raster MBTiles file. The demo file has 6 zoom levels (zoom 0â€“5) | RasterMbTilesAsyncLayer |
| Display Raster from File Tiles | Display the raster tiles created by QGIS. The demo tiles have 6 zoom levels (zoom 0~5) | XyzFileTilesAsyncLayer |
| Display Vector from MBTiles | Display an MBTiles file. The demo mbtile/json come from https://github.com/maplibre/demotiles?tab=readme-ov-file | VectorMbTilesAsyncLayer |
| Display a SQLite File | Display a SQLite layer | SqliteFeatureLayer |
| Display Common Raster Files | Display common image types such as PNG, JPEG, Bitmap etc | SkiaRasterLayer |
| Display an ECW File | Display a ECW image | EcwGdalRasterLayer |
| Display a Gdal File | Display raster format data supported by Gdal | GdalRasterLayer |
| Display a GeoTiff File | Display a GeoTiff image | GeoTiffRasterLayer |
| Display a JPEG2000 File | Display a JPEG2000 image | MrSidGdalRasterLayer |
| Display a MrSid File | Display a MrSids layer | MrSidGdalRasterLayer |
| Display a CAD File | Display a CAD layer using data from a .dwg file | CadFeatureLayer |
| Display a FileGeoDatabase | Display a file geodatabase layer | FileGeoDatabaseFeatureLayer |
| Display a GeoPdf File | Display a PDF file | GeoPdfGdalFeatureLayer |
| Display a GPX File | Display a GPX layer | GpxFeatureLayer |
| Display Graticule Lines | Display graticule lines | GraticuleLineStyle |
| Display an InMemory File | Display an in memory layer | InMemoryFeatureLayer |
| Display a KML File | Display a KML file | KmlGdalFeatureLayer |
| Display a GeoJson File | Display a simple GeoJson file of historical districts in Pittsburgh | Feature.CreateFeaturesFromGeoJson |
| Display a S57 (Nautical Charts) File | Display S57 nautical charts | NauticalChartsFeatureLayer |
| Display a Shapefile File | Display a shapefile layer | ShapeFileFeatureLayer |
| Display a TAB File | Display a TAB layer | TabFeatureLayer |
| Display a TinyGeo File | Display a TinyGeo layer | TinyGeoFeatureLayer |
| Display a table from Postgres | Display a Postgres layer | PostgreSqlFeatureLayer |
| Display a table from SQL Server | Display a SQL Server layer | SqlServerFeatureLayer |
| Generate an ESRI Grid File | Generate an ESRI .asc Grid File from point data and display it on the map | GenerateGrid |
| Grouping Layers Using LayerOverlay | Group layers into logical groups using LayerOverlays | LayerOverlay |

### XYZ Based Layers (9)

This category focuses on XYZ tile layer integration, reprojection, and caching. It helps compare loading patterns across tile sources and performance strategies.

| Title | Focus | Key API |
| --- | --- | --- |
| Display Raster from MBTiles | Display a raster MBTiles file. The demo file has 6 zoom levels (zoom 0â€“5) | RasterMbTilesAsyncLayer |
| Display Raster from File Tiles | Display the raster tiles created by QGIS. The demo tiles have 6 zoom levels (zoom 0~5) | XyzFileTilesAsyncLayer |
| Display Raster from XYZ Server | Display the tiles from a raster XYZ server. This demo server has 20 zoom levels (zoom 0~19) | ThinkGeoRasterMapsAsyncLayer |
| Display Projected OSM Layer | Apply projection to tiles from an OpenStreetMap XYZ layer. It demonstrates switching between coordinate systems using GdalProjectionConverter | OpenStreetMapAsyncLayer |
| Display Raster from WMTS Server | Display the tiles from a WMTS server. This demo server has 27 zoom levels (zoom 0~26) | WmtsAsyncLayer |
| Display Vector from MBTiles | Display an MBTiles file. The demo mbtile/json come from https://github.com/maplibre/demotiles?tab=readme-ov-file | VectorMbTilesAsyncLayer |
| Display Vector from MVT Server | Display a vector map from an MVT server | MvtTilesAsyncLayer |
| Display Vector from MVT with Local Fonts | Display a vector map from an MVT server, using local ttf fonts file | MvtTilesAsyncLayer |
| Pre-Generate Cache for XYZ Layers | Generate cache for XYZ layers | GenerateTileCacheAsync |

### Map Projection (5)

This category focuses on reprojection and coordinate system transformation. It demonstrates projection workflows for vector layers, raster layers, and world-scale map views.

| Title | Focus | Key API |
| --- | --- | --- |
| Project Features | Reproject features using the ProjectionConverter class | ProjectionConverter |
| Project a Raster | Reproject a raster layer using the ProjectionConverter class | GdalProjectionConverter |
| Setting the Projection of a Layer | Automatically reproject a layer using the ProjectionConverter class | ProjectionConverter |
| Project the World (Vector) | Demonstrates projecting vector data into different world projections | ProjectionConverter |
| Project the World (Raster) | Demonstrates reprojecting XYZ raster tile layers into different world projections using GDAL-based projection converters | GdalProjectionConverter |

### Map Tools and Adornments (4)

This category demonstrates map tools and adornment controls. It covers scale bars, compass-style elements, legends, and interactive adornment behavior.

| Title | Focus | Key API |
| --- | --- | --- |
| Map Tool Controls | Manage several built-in map tools (logo, mouse coordinates, scaleLine, and the PanZoomBar) from a single sample | MouseCoordinateMapTool |
| Drag/Resize Adornments | Toggle drag and resize behavior for multiple adornments | AdornmentLayer |
| ScaleLine and ScaleBar | Render ScaleLine and ScaleBar | ScaleLineAdornmentLayer |
| Magnetic North  | Display Magnetic North / Grid North / True North | MagneticDeclinationAdornmentLayer |

### Markers and Popups (4)

This category demonstrates marker and popup interaction patterns. It includes animated markers, draggable markers, text markers, and feature information popups.

| Title | Focus | Key API |
| --- | --- | --- |
| Markers | Add, edit, or remove markers on the map using the MarkerOverlay | SimpleMarkerOverlay |
| Animated Marker | Add an animated marker on the map using the custom icon | SimpleMarkerOverlay |
| Text Marker | Display a draggable text using marker | Marker |
| Popups | Add, edit, or remove popups on the map using the PopupOverlay | PopupOverlay |

### Vector Data Styling (17)

This category demonstrates thematic rendering and labeling techniques for vector layers. It includes class breaks, filters, regex/value styling, clustering, heat maps, and custom styles.

| Title | Focus | Key API |
| --- | --- | --- |
| Render Points | Render points using a PointStyle | PointStyle |
| Render Lines | Render lines using a LineStyle | LineStyle |
| Render Areas | Render polygons using an AreaStyle | AreaStyle |
| Render Labels | Render labels using a TextStyle | TextStyle |
| Render Based on Filters | Render certain features using a FilterStyle | FilterStyle |
| Render Based on Regex | Render certain features using regular expressions | RegexItem |
| Render Based on Scales | Render the data based on zoom levels. Zoom in and out to see the style difference | ApplyUntilZoomLevel |
| Render Based on Values | Render points based on their values using a ValueStyle | ValueStyle |
| Render Based on ClassBreaks | Render polygons based on their values using a ClassBreakStyle | ClassBreakStyle |
| Display Cluster Points | Cluster points using a ClusterPointStyle | ClusterPointStyle |
| Display Dot Density | Render polygons with dot densities based on their values | DotDensityStyle |
| Display HeatMap | Render a heatmap using a HeatStyle | HeatStyle |
| Display ISOLine | Display an ISOLine layer | ClassBreakStyle |
| Hatch Styles | Display area features using hatch fill patterns | GeoHatchStyle |
| Custom Styles | Render capitals using custom styles | TimeBasedPointStyle |
| Create a Flee Boolean Style | Style data based on data values using a FleeBooleanStyle | FleeBooleanStyle |
| Create a Multi-Column Text Style | Label a feature using the data from multiple columns | TextStyle |

### Vector Data Editing (3)

This category focuses on interactive vector editing workflows. It demonstrates drawing, modifying, deleting, snapping, and editing event handling.

| Title | Focus | Key API |
| --- | --- | --- |
| Edit Features | Draw, edit, or delete shapes | EditOverlay |
| Edit Features with Snapping | Drag a vertex into a circle, and it will snap to its center | VertexMovingEditInteractiveOverlayEventArgs |
| Edit Map Events | Recognize map editing events | EditOverlay |

### Vector Data Spatial Query (11)

This category demonstrates spatial query operations and relationship tests. It covers containing, intersecting, overlapping, touching, disjoint, and distance-based queries.

| Title | Focus | Key API |
| --- | --- | --- |
| Check if Features are Equal | Use layer query tools to find which features in a layer are topologically equal to a shape | GetFeaturesTopologicalEqual |
| Find Containing Features | Use layer query tools to find which features in a layer contain a shape | GetFeaturesContaining |
| Find Crossing Features | Use layer query tools to find which features in a layer a shape crosses | GetFeaturesCrossing |
| Find Disjoint Features | Use layer query tools to find which features in a layer are disjoint from a shape | GetFeaturesDisjointed |
| Find Features Within a Distance | Use layer query tools to find features in a layer within a given distance of a point | GetFeaturesWithinDistanceOf |
| Find Features Within a Feature | Use layer query tools to find which features in a layer are within a shape | GetFeaturesWithin |
| Find Intersecting Features | Use layer query tools to find which features in a layer intersect a shape | GetFeaturesIntersecting |
| Find Overlapping Features | Use layer query tools to find which features in a layer overlap a shape | GetFeaturesOverlapping |
| Find Touching Features | Use layer query tools to find which features in a layer are touching a shape | GetFeaturesTouching |
| Get Data from All Features | Get data from all features in a ShapeFile | GetAllFeatures |
| Get Data from One Feature | Get data from a feature in a ShapeFile | GetFeaturesContaining |

### Vector Data Geometric Operation (15)

This category demonstrates geometry operations for spatial analysis. It includes buffer, clip, difference, union, rotate, scale, simplify, and related shape operations.

| Title | Focus | Key API |
| --- | --- | --- |
| Union Shapes | Union shapes into a single shape | Union |
| Buffer a Shape | Buffer a shape | Buffer |
| Simplify a Shape | Simplify a shape's geometry | Simplify |
| Rotate a Shape | Rotate a shape | Rotate |
| Scale a Shape | Scale a shape | ScaleTo |
| Translate a Shape | Translate a shape | TranslateByDegree |
| Calculate the Center Point | Calculate the center point of a feature | GetCenterPoint |
| Calculate Area | Calculate the area of a feature | GetArea |
| Calculate Length | Calculate the length of a line | GetLength |
| Get Shortest Line | Calculate the shortest line between two shapes | GetShortestLineTo |
| Get Line on a Line | Get part of a line from an existing line | GetLineOnALine |
| Clip Shape | Clip a shape from another shape | GetIntersection |
| Get Shape Differences | Find the difference of shapes | GetDifference |
| Get Convex Hull | Get the convex hull of a shape | GetConvexHull |
| Get Envelope | Get the envelope of a shape | GetBoundingBox |

### Vector Data Topological Validation (3)

This category focuses on topology validation for point, line, and polygon datasets. It demonstrates how to detect and visualize topology rule issues.

| Title | Focus | Key API |
| --- | --- | --- |
| Validate Point Topology | Perform validation on points | TopologyValidator |
| Validate Line Topology | Perform validation on lines | TopologyValidator |
| Validate Polygon Topology | Perform validation on polygons | TopologyValidator |

### ThinkGeo Cloud Integration (9)

This category demonstrates ThinkGeo Cloud service client usage. It covers geocoding, reverse geocoding, routing, projection, elevation, timezone, and color services.

| Title | Focus | Key API |
| --- | --- | --- |
| Color Utilities | Use the ColorCloudClient class to access the ColorUtilities APIs available from the ThinkGeo Cloud | ColorCloudClient |
| Elevation | Use the ElevationCloudClient class to get elevation data from the ThinkGeo Cloud | ElevationCloudClient |
| Geocoding | Use the GeocodingCloudClient to access the Geocoding APIs available from the ThinkGeo Cloud | GeocodingCloudClient |
| Projection | Use the ProjectionCloudClient to access the Projection APIs available from the ThinkGeo Cloud | ProjectionCloudClient |
| Reverse Geocoding | Click anywhere on the map to use the ReverseGeocodingCloudClient to access the ReverseGeocoding APIs available from the ThinkGeo Cloud | ReverseGeocodingCloudClient |
| Routing | Use the RoutingCloudClient to route through a set of waypoints with the ThinkGeo Cloud | RoutingCloudClient |
| Routing - Service Area | Use the RoutingCloudClient to find the service area of a location with the ThinkGeo Cloud | RoutingCloudClient |
| Routing - Traveling Sales Person | Use the RoutingCloudClient to find an optimized route through a set of waypoints with the ThinkGeo Cloud | RoutingCloudClient |
| Timezone | Use the TimezoneCloudClient to access the Timezone APIs available from the ThinkGeo Cloud | TimeZoneCloudClient |

### Miscellaneous (12)

This category contains practical cross-cutting samples outside a single theme. It includes events, exceptions, printing, snapshots, caching, and other utility scenarios.

| Title | Focus | Key API |
| --- | --- | --- |
| Pre-Generate Cache for Tile Overlay | Generate cache for Tile Overlay | GenerateTileCacheAsync |
| Print the Map | Print maps using the PrintOverlay | MapPrinterLayer |
| Handle Exceptions | Throw an overlay exception, or draw the exception on the map | CustomWmsAsyncLayer |
| Wrap the DateLine | Say the world width is 0 ~ 1, here shows what the shapes with different length look like in the multi-world map | WrappingMode.WrapDateline |
| Draw the map on an Image | Draw your map onto an image file | ThinkGeoRasterMapsAsyncLayer |
| Get Map SnapShot | Get the snapshot of the current map | GetSnapshot |
| Perf Test: Refresh NDFD sky grid | Use the bundled local NDFD GRIB2 sky-cover grid as a fast bitmap-backed time sequence over a Florida-focused map view. | GridOverlay |
| Custom Background | Display a background overlay | GeoLinearGradientBrush |
| Custom Feature Layer | Use the Layer base class to create your own custom layers | RadiusLayer |
| Custom Feature Sources | Use the FeatureSource base class to create your own custom Feature Source | SimpleCsvFeatureSource |
| Navigate On TouchScreen | Enable the map for TouchScreen. See this: https://youtu.be/1LAROjqIrO8?si=O1zdrhpisj1vCIB8 | IsManipulationEnabled |
| Basic Map Events | Recognize fundamental map events | CurrentExtentChangedMapViewEventArgs |


