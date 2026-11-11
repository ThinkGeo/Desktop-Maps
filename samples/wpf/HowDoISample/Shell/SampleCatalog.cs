using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// The one catalog. The sample menu, the search index and the home page are
    /// all projections of this list.
    /// <para>
    /// It replaces three catalogs that had to agree and did not - the menu
    /// (165 entries), the classic <c>samples.json</c> (143) and the generated
    /// overview table (140 rows, last generated on the classic side) - which is
    /// how 23 samples, including every GPU-only one, ended up missing from the
    /// table the app itself renders. See issue #954.
    /// </para>
    /// <para>
    /// Ordering is the menu order: groups in the order below, samples in the
    /// order within each group.
    /// </para>
    /// <para>
    /// Grouping is by two properties a dataset actually HAS, never by the role
    /// an application gives it. A service is not intrinsically a basemap - the
    /// same WMTS can be the ground in one app and a weather sheet over it in
    /// another - so "basemap versus overlay" cannot be an axis. What is
    /// intrinsic: who HOLDS the data (a service somebody else operates = Maps
    /// from a Service; a store you own, file or database = Your Data) and what
    /// the data IS (raster or vector, the sub-split of Maps from a Service).
    /// Everything else groups by task, and the renderer is not a group at all -
    /// it is a property of a row (see <see cref="SampleEntry.Draws"/>), because
    /// nobody browses for "the GPU samples", they browse for tilt or terrain or
    /// labels.
    /// </para>
    /// <para>
    /// Over the groups reads one more layer, the chapters (<see
    /// cref="ChapterStarts"/>): the journey from "get a map on screen" to
    /// "extend the SDK", printed by the sidebar as a label above the first
    /// group of each chapter. Chapters are narrative, not taxonomy - a task
    /// cuts across the intrinsic groups (an offline basemap is Your Data), so
    /// a chapter can mark where it begins but can never be a group.
    /// </para>
    /// </summary>
    internal static class SampleCatalog
    {
        internal static readonly IReadOnlyList<SampleEntry> All = new SampleEntry[]
        {
            // ---- Start Here ----
            // The pages about the gallery rather than samples of the SDK: the
            // whole catalog as one table, the renderer guide and the FAQ. One
            // group at the head of the menu, instead of a "Home" at the top and
            // a "Documentation" at the tail - the tail is where a new user never
            // scrolls.
            new SampleEntry("Start Here", "Start here", "HomePage.xaml.cs",
                "Every sample in one table, with what it does and the API to search for",
                "SampleCatalog", SampleDraws.Page, typeof(HomePage), () => new HomePage()),
            new SampleEntry("Start Here", "GPU or Classic?", "RendererGuidePage.xaml",
                "When the GPU tile path wins, when the per-frame classic overlays do, and the four rules that decide",
                "GpuBasemap", SampleDraws.Page, typeof(RendererGuidePage), () => new RendererGuidePage()),
            // Moving a classic application across is the first thing an existing
            // customer asks, so the bridge opens the menu rather than closing it.
            new SampleEntry("Start Here", "Migrate a Classic Layer", "MigrateAClassicLayer.xaml.cs",
                "Translate a FeatureLayer styled on its ZoomLevelSet and add it to a MapStyle, beside the classic renderer drawing the same layers",
                "AddFeatureLayer", SampleDraws.SideBySide, typeof(Samples.MigrateAClassicLayer), () => new Samples.MigrateAClassicLayer()),
            new SampleEntry("Start Here", "Frequently Asked Questions", "FaqViewer.cs",
                "Frequently asked questions about the samples and the SDK",
                "MarkdownDocumentViewer", SampleDraws.Page, typeof(Samples.FaqViewer), () => new Samples.FaqViewer()),

            // ---- Maps from a Service - Vector / Maps from a Service - Raster ----
            // Everything served from a URL somebody else operates. Whether an
            // application uses a service as the ground or lays it over the
            // ground is the application's decision, not the service's property,
            // so it is no axis here - the same WMTS can be a national basemap
            // or a weather sheet. What IS the service's own property is what it
            // sends: VECTOR (geometry - tiled with a style document, or raw
            // features you style yourself) or RASTER (pixels the server already
            // drew). Vector leads, because the style document it introduces is
            // what every later chapter builds on. Turn-key providers open each
            // side, protocol servers follow, and the rows about living with
            // tiled services close the raster side.
            // One row for the turn-key providers, where three (ThinkGeo Vector Map, MVT
            // Server, and a documentation page for the bring-your-own-key ones) split
            // one lesson by vendor: a vector basemap is its style document, and the map
            // follows it. The keyed providers stay a page, shown behind a radio button.
            new SampleEntry("Maps from a Service - Vector", "Vector Map Providers", "VectorMapProviders.xaml.cs",
                "ThinkGeo Maps (light, dark), OpenFreeMap's free keyless styles, the keyed providers - Mapbox, Esri, TomTom and more - as a page of their style URLs, and any MVT server by the URL of its style document",
                "ThinkGeoVectorTileSource", SampleDraws.Style, typeof(Samples.VectorMapProviders), () => new Samples.VectorMapProviders()),
            // From here down the server sends features rather than tiles -
            // geometry and attributes you style yourself, which is why these
            // rows lead straight into Styling.
            new SampleEntry("Maps from a Service - Vector", "WFS", "WFS.xaml.cs",
                "Merge a WFS V2 service into the basemap style - Dutch cadastral parcels from PDOK, numbered from their own attributes",
                "WfsV2AsyncFeatureSource", SampleDraws.Style, typeof(Samples.WFS), () => new Samples.WFS()),
            new SampleEntry("Maps from a Service - Vector", "OGC API Feature Server", "OGCAPIFeatureServer.xaml.cs",
                "Merge an OGC API Features collection into the basemap style, labeled from its own attributes",
                "OgcApiAsyncFeatureSource", SampleDraws.Style, typeof(Samples.OGCAPIFeatureServer), () => new Samples.OGCAPIFeatureServer()),
            new SampleEntry("Maps from a Service - Vector", "NOAA Weather Stations", "NOAAWeatherStations.xaml.cs",
                "NOAA weather stations' current readings - dots colored by temperature, labeled from the data",
                "NoaaWeatherStationFeatureSource", SampleDraws.Style, typeof(Samples.NOAAWeatherStations), () => new Samples.NOAAWeatherStations()),
            new SampleEntry("Maps from a Service - Vector", "NOAA Weather Warnings", "NOAAWeatherWarnings.xaml.cs",
                "NOAA weather warnings merged into the basemap style - polygons colored by severity, labeled by event",
                "NoaaWeatherWarningsFeatureSource", SampleDraws.Style, typeof(Samples.NOAAWeatherWarnings), () => new Samples.NOAAWeatherWarnings()),

            // One row for the turn-key providers, where three (ThinkGeo Raster Map, Open
            // Street Map, and a documentation page for the bring-your-own-key ones) split
            // the same one-line registration by vendor. The keyed providers stay a page,
            // shown behind a radio button: without a key none of them draws.
            new SampleEntry("Maps from a Service - Raster", "Raster Map Providers", "RasterMapProviders.xaml.cs",
                "ThinkGeo Cloud (light, dark, aerial, hybrid) and OpenStreetMap as one tile source each, and the keyed providers - Azure Maps, Google Maps, HERE and Mapbox - as a page of their classes and one-line usage",
                "ThinkGeoRasterTileSource", SampleDraws.Style, typeof(Samples.RasterMapProviders), () => new Samples.RasterMapProviders()),
            new SampleEntry("Maps from a Service - Raster", "XYZ Tile Server", "XyzTileServer.xaml.cs",
                "Draw any {z}/{x}/{y} tile server as one XyzRasterTileSource - here OpenStreetMap, with the user agent its tile policy asks for",
                "XyzRasterTileSource", SampleDraws.Style, typeof(Samples.XyzTileServer), () => new Samples.XyzTileServer()),
            new SampleEntry("Maps from a Service - Raster", "WMS", "WMS.xaml.cs",
                "NASA GIBS true-color imagery of yesterday's Earth over the basemap - one GetMap request per web-mercator tile",
                "WmsRasterTileSource", SampleDraws.Style, typeof(Samples.WMS), () => new Samples.WMS()),
            new SampleEntry("Maps from a Service - Raster", "WMTS", "WMTS.xaml.cs",
                "Display a WMTS server as the basemap through its web-mercator tile matrix set",
                "WmtsRasterTileSource", SampleDraws.Style, typeof(Samples.WMTS), () => new Samples.WMTS()),

            // ---- Your Data ----
            // The store you own: files on disk, your own databases, tiles you cut
            // yourself - the other side of the who-holds-it axis from the service
            // groups. Four rows open it - vector files, raster files, database
            // tables, offline tiles - each a picker over every format the SDK
            // reads, because the format is one line and everything after it is
            // the same. They replaced the one-row-per-format list (Display a
            // Shapefile, Display a SQLite File, Offline Raster Tiles on the GPU,
            // ...), a screen of menu rows demonstrating one mechanism; what those
            // rows did not absorb is the Misc group below.
            //
            // Every row here draws through the GPU renderer, with the data in the
            // same style document as the basemap rather than in a classic
            // LayerOverlay above it: FeatureSourceVectorTileSource cuts any
            // FeatureSource into vector tiles, ClassicRasterTileSource serves any
            // RasterSource, and an archive of tiles is a tile source already.
            new SampleEntry("Your Data", "Vector File Formats", "VectorFileFormats.xaml.cs",
                "Open a vector file - a shapefile, GeoJSON, KML, GPX, MapInfo TAB, TinyGeo, CAD, a GeoPDF - or build features in code; reading the format is the only line that differs",
                "FeatureSourceVectorTileSource", SampleDraws.Style, typeof(Samples.VectorFileFormats), () => new Samples.VectorFileFormats()),
            new SampleEntry("Your Data", "Raster File Formats", "RasterFileFormats.xaml.cs",
                "Open a raster file - GeoTIFF, ECW, JPEG 2000, MrSID or anything else GDAL reads - through the classic source named after the format, warped to Web Mercator once and served as tiles",
                "ClassicRasterTileSource", SampleDraws.Style, typeof(Samples.RasterFileFormats), () => new Samples.RasterFileFormats()),
            // A GeoPackage and an Esri file geodatabase sit here rather than with the
            // vector files: you open them by table, the way you open SQLite or Postgres,
            // and a GeoPackage IS a SQLite file.
            new SampleEntry("Your Data", "Databases", "Databases.xaml.cs",
                "Open a table from a database - SQLite, a GeoPackage, an Esri file geodatabase, PostgreSQL / PostGIS or SQL Server; the connection is the only line that differs",
                "FeatureSourceVectorTileSource", SampleDraws.Style, typeof(Samples.Databases), () => new Samples.Databases()),
            // One row for every archive on disk, where three (Offline Basemap from
            // Files, Offline Vector Tiles on the GPU, Offline Raster Tiles on the GPU)
            // split one lesson by renderer and by kind. Tiles are tiles already, so
            // the row is GPU; what the classic row taught besides - tiles reprojected
            // to degrees, tile caches - lives in Projections and Pre-Generate a Tile
            // Cache.
            new SampleEntry("Your Data", "Offline Tiles", "OfflineTiles.xaml.cs",
                "Serve tiles off disk with no network - an MBTiles or PMTiles archive, vector or raster, or a folder of z/x/y images written by QGIS; opening the archive is the only line that differs",
                "MbTilesVectorTileSource", SampleDraws.Style, typeof(Samples.OfflineTiles), () => new Samples.OfflineTiles()),
            // The one row in the group that keeps a classic overlay: an S-57 chart is
            // drawn by its S-52 symbology, which lives in NauticalChartsFeatureLayer,
            // not in a style document.
            new SampleEntry("Your Data", "Nautical Charts", "NauticalCharts.xaml.cs",
                "Draw an S-57 chart with its embedded S-52 styling, and ask it at every step of a ship's route about the depth under her keel, the aids to navigation and hazards nearby, and the restricted areas she is in",
                "NauticalChartsFeatureLayer", SampleDraws.StyleAndOverlay, typeof(Samples.NauticalCharts), () => new Samples.NauticalCharts()),
            // ---- Styling ----
            // One row, not four: this sample already styles a point layer, a line
            // layer and a polygon layer and labels each of them, so "Render Points",
            // "Render Lines" and "Render Areas" were three menu rows demonstrating a
            // strict subset of it.
            new SampleEntry("Styling", "Render Points, Lines and Polygons", "RenderPointsLinesAndPolygons.xaml.cs",
                "Style points, lines and polygons and label each of them - the style document sits beside the map, editable while it runs",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.RenderPointsLinesAndPolygons), () => new Samples.RenderPointsLinesAndPolygons()),
            // One row, not four. ValueStyle, ClassBreakStyle, FilterStyle and a boolean
            // expression style are four class names for one idea - an expression in the
            // layer's paint - so they are one sample over one dataset with a picker for
            // match / step / filter / case, and the document itself on screen to edit.
            new SampleEntry("Styling", "Render Based on Rules", "RenderBasedOnRules.xaml.cs",
                "One dataset, four expressions: match, step, filter and case - the ValueStyle, ClassBreakStyle, FilterStyle and boolean-expression styles as one mechanism, editable while the map runs",
                "SetStyleAsync", SampleDraws.Style, typeof(Samples.RenderBasedOnRules), () => new Samples.RenderBasedOnRules()),

            // Labels as the subject rather than a side effect: several samples bend a
            // name along a line or lose one to a collision incidentally; this is the
            // one place each behavior is set up on data crafted to show it.
            new SampleEntry("Styling", "Advanced Labels", "AdvancedLabels.xaml.cs",
                "Six label lessons on crafted data: text bends along its line, anchors from its own column, yields to priority, wraps and recases, keeps its icon when crowded, and writes CJK vertically - each document editable while it runs",
                "SetStyleAsync", SampleDraws.Style, typeof(Samples.AdvancedLabels), () => new Samples.AdvancedLabels()),
            // The other half of the pair above: a rule the document CAN hold belongs in
            // the document; what it does not have - the clock, or a C# lambda over the
            // feature - is a code-built layer. It replaces "Render Based on Regex" and
            // "Custom Styles", which customized conditions, not drawing, and carries
            // the choropleth-in-C# lesson as its second choice rather than as a row.
            new SampleEntry("Styling", "Render Based on Code", "RenderBasedOnCode.xaml.cs",
                "Two things a style document cannot hold: the clock - day and night sweep the world's capitals with nothing rebuilt - and a paint lambda that reads each state's density into a choropleth ramp, inserted under the basemap's roads and labels",
                "StyleLayer.CreateFill", SampleDraws.Style, typeof(Samples.RenderBasedOnCode), () => new Samples.RenderBasedOnCode()),
            new SampleEntry("Styling", "Display Cluster Points", "DisplayClusterPoints.xaml.cs",
                "Cluster points into markers recomputed for the view - clusters you can click, instead of clusters drawn into a tile",
                "SimpleMarkerOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.DisplayClusterPoints), () => new Samples.DisplayClusterPoints()),
            // Kept, and given the job only it can do. A single-variable dot density is
            // a worse heatmap; two colors mixing inside one areal unit is a composition,
            // which an intensity field cannot represent at all.
            new SampleEntry("Styling", "Display Dot Density", "DisplayDotDensity.xaml.cs",
                "Two colors of dot mixed inside each state - countable, and a composition a heatmap cannot show - with the dots generated once as data",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.DisplayDotDensity), () => new Samples.DisplayDotDensity()),
            // The GPU heatmap stands where the classic HeatStyle row did. The CPU style
            // and the GPU layer are two different algorithms - a flat-core kernel
            // accumulated in 8-bit alpha, against MapLibre's gaussian splats summed in a
            // float density target - and the GPU one is the model every current web map
            // uses, so it is the one this gallery teaches.
            new SampleEntry("Styling", "Heatmap", "Heatmap.xaml.cs",
                "Render point observations as a heatmap, with radius, intensity, opacity and color ramp on live controls",
                "SetHeatmapPaint", SampleDraws.Style, typeof(Samples.Heatmap), () => new Samples.Heatmap()),
            // The GPU isolines sample stands where the classic DisplayISOLine row
            // did: same mosquito data, same contours, but the field is generated and
            // contoured on the GPU instead of re-running IDW plus marching squares
            // on the CPU for every redraw. Beside the heatmap, because both draw
            // a field out of scattered points; nothing in it moves.
            new SampleEntry("Styling", "Isolines", "GpuIsolines.xaml.cs",
                "Generate and label isolines (contour lines) from scattered point values, the field and the contours redrawn every frame",
                "AddIsolines", SampleDraws.StyleAndOverlay, typeof(Samples.GpuIsolines), () => new Samples.GpuIsolines()),
            new SampleEntry("Styling", "Hatch Styles", "HatchStyles.xaml.cs",
                "Fill areas with the classic GeoHatchStyle patterns - each one stroked into an image, registered through style.Images, and named by fill-pattern",
                "MapStyle.Images", SampleDraws.Style, typeof(Samples.HatchStyles), () => new Samples.HatchStyles()),
            new SampleEntry("Styling", "Create a Multi-Column Text Style", "CreateAMultiColumnTextStyle.xaml.cs",
                "Build a label out of several columns - a concat expression in text-field, where the classic TextStyle used a {COLUMN} template; the document sits beside the map, editable while it runs",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.CreateAMultiColumnTextStyle), () => new Samples.CreateAMultiColumnTextStyle()),

            // The map's own style document, managed and composed at runtime.
            // Grouped here because their subject is the style, not any source -
            // a service is not intrinsically the ground, so these rows cannot
            // hang off a source group.
            new SampleEntry("Styling", "Style Layer Toggles", "StyleLayerToggles.xaml.cs",
                "Turn individual style.json layers on and off while the map is live",
                "MapStyle.SetLayerVisibility", SampleDraws.Style, typeof(Samples.StyleLayerToggles), () => new Samples.StyleLayerToggles()),
            new SampleEntry("Styling", "Insert a Layer Under the Labels", "InsertALayerUnderTheLabels.xaml.cs",
                "Aerial imagery under vector streets and labels, with a translucent wash inserted above the roads and below the labels - a slot no overlay can reach, because an overlay is always above everything",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.InsertALayerUnderTheLabels), () => new Samples.InsertALayerUnderTheLabels()),

            // ---- Dynamic Data ----
            // The source-object family: data lives in an object (DynamicPointSource,
            // VectorFieldSource, GeoImageRasterTileSource, GridSource), the style registers how
            // it draws once, and every animation frame after that is one Update call -
            // no recompile, no tile rebuilds. They all happen to wear weather and
            // traffic data because that is what naturally moves, but the group's
            // subject is the channel, not the theme.
            new SampleEntry("Dynamic Data", "Live Flights", "LiveFlights.xaml.cs",
                "Every airborne aircraft, live from OpenSky, dead-reckoned forward through one DynamicSource - ten thousand moving markers, one SetPoint per aircraft per tick",
                "DynamicSource", SampleDraws.Style, typeof(Samples.LiveFlights), () => new Samples.LiveFlights()),
            new SampleEntry("Dynamic Data", "Wind as a Vector Field", "NdfdWindField.xaml.cs",
                "The NDFD wind forecast through a VectorFieldSource: 65k tracer particles advected and faded every frame - the streaming weather-map look - or magnitude-scaled arrows from the same source",
                "VectorFieldSource", SampleDraws.Style, typeof(Samples.NdfdWindField), () => new Samples.NdfdWindField()),
            new SampleEntry("Dynamic Data", "NEXRAD Weather Radar", "NexradRadar.xaml.cs",
                "One georeferenced PNG draped over the basemap as a GeoImageRasterTileSource - no tile protocol - with the last half hour animating through the source object",
                "GeoImageRasterTileSource", SampleDraws.Style, typeof(Samples.NexradRadar), () => new Samples.NexradRadar()),
            new SampleEntry("Dynamic Data", "Animate a Weather Grid", "NdfdSkyGridAnimation.xaml.cs",
                "Animate an NDFD sky grid over the basemap - color ramps, resampling and projection",
                "InMemoryGridSource", SampleDraws.Style, typeof(Samples.NdfdSkyGridAnimation), () => new Samples.NdfdSkyGridAnimation()),
            // The GPU rewrite of the old CPU-overlay population sample: the circle is
            // an IMAGE instanced by the marker batch, which is what makes any shape
            // one registration instead of one renderer feature.
            new SampleEntry("Dynamic Data", "Dynamic Rendering", "DynamicRendering.xaml.cs",
                "A century of state populations as animated circles: size and tint are data, the circle is one registered disc image drawn at every point, and a year change is one SetPoint per state",
                "DynamicSource.SetPoint", SampleDraws.Style, typeof(Samples.DynamicRendering), () => new Samples.DynamicRendering()),
            // Moved from Map Navigation, where "navigation" meant panning the map: a
            // vehicle following GPS fixes is data that moves, which is this group.
            new SampleEntry("Dynamic Data", "Vehicle Navigation", "VehicleNavigation.xaml.cs",
                "Navigate to the Empire State Building using GPS points (vehicle tracking)",
                "FeatureLayerWpfDrawingOverlay", SampleDraws.Style, typeof(Samples.VehicleNavigation), () => new Samples.VehicleNavigation()),

            // ---- 3D & Camera ----
            // What the camera can do once a GpuBasemap is the basemap. Grouped by the
            // capability rather than by the renderer that provides it: nobody goes
            // looking for "the GPU samples", they go looking for tilt, for extruded
            // buildings, for terrain.
            new SampleEntry("3D & Camera", "Perspective Tilt", "PerspectiveTilt.xaml.cs",
                "Tilt the camera over vector tiles or over a raster basemap - the gestures are built into the MapView",
                "TiltAngle", SampleDraws.Style, typeof(Samples.PerspectiveTilt), () => new Samples.PerspectiveTilt()),
            new SampleEntry("3D & Camera", "3D Buildings", "Buildings3DInteractive.xaml.cs",
                "Extrude building footprints and move the camera around them",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.Buildings3DInteractive), () => new Samples.Buildings3DInteractive()),
            new SampleEntry("3D & Camera", "3D Terrain", "Terrain3D.xaml.cs",
                "Drape the map over elevation data",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.Terrain3D), () => new Samples.Terrain3D()),
            // Moved from Map Navigation: what it shows is the camera - a follow camera
            // the engine flies at street level, with buildings standing in front of
            // the road - not panning the map.
            new SampleEntry("3D & Camera", "Lane-Level Navigation", "LaneLevelNavigation.xaml.cs",
                "In-car view over an OpenDRIVE HD map: every lane as surveyed with its paint, a follow camera the engine flies, the vehicle in the style's marker batch, 3D buildings in front of the road",
                "FeatureSourceVectorTileSource", SampleDraws.Style, typeof(Samples.LaneLevelNavigation), () => new Samples.LaneLevelNavigation()),

            // ---- Markers & Popups ----
            // Three rows. "Markers and Popups on a Tilted Map" folded into Markers (its
            // map tilts); "Text Marker" was a subset of these two (any WPF element as
            // Content, dragging); "Custom Icon" pointed at the animated ellipse control
            // Animated Marker uses as its content, not at a sample.
            new SampleEntry("Markers & Popups", "Markers", "Markers.xaml.cs",
                "Add, drag and remove markers on the map with the MarkerOverlay - and tilt the map, the markers keep their ground position",
                "SimpleMarkerOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.Markers), () => new Samples.Markers()),
            new SampleEntry("Markers & Popups", "Animated Marker", "AnimatedMarker.xaml.cs",
                "A marker whose content is your own WPF control - here an animated icon",
                "SimpleMarkerOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.AnimatedMarker), () => new Samples.AnimatedMarker()),
            new SampleEntry("Markers & Popups", "Popups", "Popups.xaml.cs",
                "Add, edit, or remove popups on the map using the PopupOverlay",
                "PopupOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.Popups), () => new Samples.Popups()),

            // ---- Map Navigation ----
            new SampleEntry("Map Navigation", "Map Navigation", "MapNavigation.xaml.cs",
                "Zoom, pan, and rotate the map control",
                "ZoomToAsync", SampleDraws.StyleAndOverlay, typeof(Samples.MapNavigation), () => new Samples.MapNavigation()),
            new SampleEntry("Map Navigation", "Zoom to the Black Hole", "ZoomToBlackHole.xaml.cs",
                "Dive from the galaxy M87 down to its black hole, through ten pictures nested inside each other to illustrate the scale",
                "GeoImageRasterTileSource", SampleDraws.Style, typeof(Samples.ZoomToBlackHole), () => new Samples.ZoomToBlackHole()),
            new SampleEntry("Map Navigation", "Zoom to Extents", "ZoomToExtents.xaml.cs",
                "Set the map extent in a variety of different ways",
                "ZoomToAsync", SampleDraws.Style, typeof(Samples.ZoomToExtents), () => new Samples.ZoomToExtents()),
            new SampleEntry("Map Navigation", "Restrict Map Extent", "RestrictMapExtent.xaml.cs",
                "Restrict the map's panning area and zoom range using RestrictExtent, MaximumScale, and MinimumScale",
                "RestrictExtent", SampleDraws.Style, typeof(Samples.RestrictMapExtent), () => new Samples.RestrictMapExtent()),
            new SampleEntry("Map Navigation", "Resize the Map", "ResizeTheMap.xaml.cs",
                "Resize the map while preserving the world extent with various map resize modes",
                "MapResizeMode", SampleDraws.Style, typeof(Samples.ResizeTheMap), () => new Samples.ResizeTheMap()),
            new SampleEntry("Map Navigation", "Overview Map", "OverviewMap.xaml.cs",
                "Overview map with rotating frame; drag to recenter the main map",
                "EditOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.OverviewMap), () => new Samples.OverviewMap()),
            // Replaces "Wrap the DateLine", which taught the classic overlay's translated
            // copies over geometry stored past the world edge. The GPU map wraps by
            // itself; what is left to teach is the seam in the DATA, and a flight
            // across the Pacific is where every reader meets it.
            new SampleEntry("Map Navigation", "Cross the Dateline", "CrossTheDateline.xaml.cs",
                "A flight from Los Angeles to Shanghai over the antimeridian: route, corridor and aircraft stored with continuous longitudes, whole on the globe and whole on both sides of the seam in Web Mercator",
                "DisplayProjection.Globe", SampleDraws.Style, typeof(Samples.CrossTheDateline), () => new Samples.CrossTheDateline()),

            // ---- Map Tools & Adornments ----
            new SampleEntry("Map Tools & Adornments", "Map Tool Controls", "MapToolControls.xaml.cs",
                "Manage several built-in map tools (logo, mouse coordinates, scaleLine, and the PanZoomBar) from a single sample",
                "MouseCoordinateMapTool", SampleDraws.Style, typeof(Samples.MapToolControls), () => new Samples.MapToolControls()),
            new SampleEntry("Map Tools & Adornments", "Drag/Resize Adornments", "DragResizeAdornments.xaml.cs",
                "Toggle drag and resize behavior for multiple adornments",
                "AdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.DragResizeAdornments), () => new Samples.DragResizeAdornments()),
            // A legend is an adornment - it sits in the map's corner like the scale bar
            // and the north arrow, not in the style document - so it belongs here rather
            // than among the styling samples.
            new SampleEntry("Map Tools & Adornments", "Legend", "Legend.xaml.cs",
                "Build a legend whose swatches are the styles the map draws with",
                "LegendAdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.Legend), () => new Samples.Legend()),
            new SampleEntry("Map Tools & Adornments", "ScaleLine and ScaleBar", "ScaleLineScaleBar.xaml.cs",
                "Render ScaleLine and ScaleBar, and give them the projection that makes the distance they report the real one",
                "ScaleLineAdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.ScaleLineScaleBar), () => new Samples.ScaleLineScaleBar()),
            new SampleEntry("Map Tools & Adornments", "Magnetic North", "MagneticNorth.xaml.cs",
                "Display a compass (north arrow) adornment showing Magnetic North / Grid North / True North",
                "MagneticDeclinationAdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.MagneticNorth), () => new Samples.MagneticNorth()),
            new SampleEntry("Map Tools & Adornments", "Display Graticule Lines", "DisplayGraticuleLines.xaml.cs",
                "Display graticule lines - a latitude/longitude grid",
                "GraticuleLineStyle", SampleDraws.StyleAndOverlay, typeof(Samples.DisplayGraticuleLines), () => new Samples.DisplayGraticuleLines()),
            new SampleEntry("Map Tools & Adornments", "Measure Distance and Area", "MeasureDistanceAndArea.xaml.cs",
                "Measure distance and area on the map with the overlay that annotates as you draw",
                "MeasureInteractiveOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.MeasureDistanceAndArea), () => new Samples.MeasureDistanceAndArea()),

            // ---- Draw & Edit ----
            // "Draw" is in the name because it is the first thing the group does:
            // its lead sample starts with drawing shapes, and "how do I let the
            // user draw on the map" is the question that brings people here.
            new SampleEntry("Draw & Edit", "Edit Features", "EditFeatures.xaml.cs",
                "Draw, edit, or delete shapes",
                "EditOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.EditFeatures), () => new Samples.EditFeatures()),
            new SampleEntry("Draw & Edit", "Edit Features with Snapping", "EditWithSnapping.xaml.cs",
                "Drag a vertex into a circle, and it will snap to its center",
                "VertexMovingEditInteractiveOverlayEventArgs", SampleDraws.StyleAndOverlay, typeof(Samples.EditWithSnapping), () => new Samples.EditWithSnapping()),
            new SampleEntry("Draw & Edit", "Edit with Constraints", "EditWithConstraints.xaml.cs",
                "Enforce minimum vertex spacing, keep a rectangle rectangular, type exact coordinates",
                "VertexAddingEditInteractiveOverlayEventArgs", SampleDraws.StyleAndOverlay, typeof(Samples.EditWithConstraints), () => new Samples.EditWithConstraints()),
            new SampleEntry("Draw & Edit", "Edit Map Events", "EditMapEvents.xaml.cs",
                "Recognize map editing events",
                "EditOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.EditMapEvents), () => new Samples.EditMapEvents()),

            // ---- Shapes & Spatial Queries ----
            new SampleEntry("Shapes & Spatial Queries", "Get Data from One Feature", "GetDataFromOneFeature.xaml.cs",
                "Get data from a feature in a ShapeFile",
                "GetFeaturesContaining", SampleDraws.StyleAndOverlay, typeof(Samples.GetDataFromOneFeature), () => new Samples.GetDataFromOneFeature()),
            new SampleEntry("Shapes & Spatial Queries", "Find Features by Spatial Relation", "FindFeaturesBySpatialRelation.xaml.cs",
                "Query features by spatial relation - containing, crossing, disjoint, intersecting, overlapping, touching, within a shape, or within a distance",
                "FeatureSource", SampleDraws.StyleAndOverlay, typeof(Samples.FindFeaturesBySpatialRelation), () => new Samples.FindFeaturesBySpatialRelation()),
            new SampleEntry("Shapes & Spatial Queries", "Shape Operations", "ShapeOperations.xaml.cs",
                "Transform a shape - union, buffer, simplify, rotate, scale, translate, convex hull, envelope, intersection or difference - each one a single call on the shape",
                "BaseShape", SampleDraws.Style, typeof(Samples.ShapeOperations), () => new Samples.ShapeOperations()),
            new SampleEntry("Shapes & Spatial Queries", "Shape Measurements", "ShapeMeasurements.xaml.cs",
                "Measure a shape - area, length, center point, shortest line to somewhere else, or a stretch of a line starting a given distance along",
                "BaseShape", SampleDraws.Style, typeof(Samples.ShapeMeasurements), () => new Samples.ShapeMeasurements()),
            new SampleEntry("Shapes & Spatial Queries", "Validate Topology", "ValidateTopology.xaml.cs",
                "Every rule TopologyValidator checks - what points may touch, what lines may cross, what polygons may leave between them - on shapes small enough to see the answer",
                "TopologyValidator", SampleDraws.Style, typeof(Samples.ValidateTopology), () => new Samples.ValidateTopology()),
            new SampleEntry("Shapes & Spatial Queries", "Check if Features are Equal", "CheckIfFeaturesAreEqual.xaml.cs",
                "Use layer query tools to find which features in a layer are topologically equal to a shape",
                "GetFeaturesTopologicalEqual", SampleDraws.StyleAndOverlay, typeof(Samples.CheckIfFeaturesAreEqual), () => new Samples.CheckIfFeaturesAreEqual()),

            // ---- Projections ----
            // In route order: the four that project when the data is read, the
            // three the GPU projects as it draws, then the two that answer "what
            // if the shader has no family for mine" and "which should I use".
            new SampleEntry("Projections", "Project Features", "ProjectFeatures.xaml.cs",
                "Projected when the data is READ (CPU) - every vertex through proj4 as it comes off disk",
                "ProjectionConverter", SampleDraws.Style, typeof(Samples.ProjectFeatures), () => new Samples.ProjectFeatures()),
            new SampleEntry("Projections", "Project a Raster", "ProjectARaster.xaml.cs",
                "Projected when the data is READ (CPU) - a raster has no vertices, so it is resampled pixel by pixel",
                "GdalProjectionConverter", SampleDraws.Classic, typeof(Samples.ProjectARaster), () => new Samples.ProjectARaster()),
            new SampleEntry("Projections", "Setting the Projection of a Layer", "SettingTheProjectionOfALayer.xaml.cs",
                "Projected when the data is READ (CPU) - the converter lives on the layer, so every fetch comes back projected",
                "ProjectionConverter", SampleDraws.Style, typeof(Samples.SettingTheProjectionOfALayer), () => new Samples.SettingTheProjectionOfALayer()),
            // Classic rendering on purpose: reprojection lives in the layer's draw
            // path (native tiles are merged and warped as they draw), while the GPU
            // source path serves native-grid tiles only. Moved here from the service
            // groups when they went GPU-only - the lesson is the projection, not the
            // server. One sample, two servers, and swapping the server never moves
            // the view.
            new SampleEntry("Projections", "Reproject a Raster Tile Service", "ReprojectRasterTiles.xaml.cs",
                "Projected when the data is READ (CPU) - a tile service warped between EPSG:3857 and EPSG:4326, the warped tiles cached in their own second cache",
                "GdalProjectionConverter", SampleDraws.Classic, typeof(Samples.ReprojectRasterTiles), () => new Samples.ReprojectRasterTiles()),
            new SampleEntry("Projections", "Any Projection, Vector Tiles", "MorphBetweenProjectionsMvt.xaml.cs",
                "Projected when the map is DRAWN - a full map, zoom and pan and style, in any proj4 definition, from the same web-mercator tiles, and animated from one projection to the next",
                "DisplayProjection", SampleDraws.Style, typeof(Samples.MorphBetweenProjectionsMvt), () => new Samples.MorphBetweenProjectionsMvt()),
            new SampleEntry("Projections", "Any Projection, Satellite Imagery (ThinkGeo Cloud)", "AnyProjectionRaster.xaml.cs",
                "Projected when the map is drawn - satellite tiles warped per vertex, with no CPU resampling",
                "DisplayProjection", SampleDraws.Style, typeof(Samples.AnyProjectionRaster), () => new Samples.AnyProjectionRaster()),
            new SampleEntry("Projections", "The Globe", "TheGlobe.xaml.cs",
                "Projected when the map is drawn - the same tiles on a sphere, and what it does when you zoom in far enough to let go",
                "DisplayProjection.Globe", SampleDraws.Style, typeof(Samples.TheGlobe), () => new Samples.TheGlobe()),
            new SampleEntry("Projections", "CPU vs GPU projection, side by side", "CpuVsGpuProjection.xaml.cs",
                "Both routes at once - the same shapefile projected at READ time on the left and at DRAW time on the right, with what each costs",
                "ProjectionConverter", SampleDraws.SideBySide, typeof(Samples.CpuVsGpuProjection), () => new Samples.CpuVsGpuProjection()),

            // ---- Cloud Services ----
            new SampleEntry("Cloud Services", "Geocoding", "Geocoding.xaml.cs",
                "Use the GeocodingCloudClient to access the Geocoding APIs available from the ThinkGeo Cloud",
                "GeocodingCloudClient", SampleDraws.Style, typeof(Samples.Geocoding), () => new Samples.Geocoding()),
            new SampleEntry("Cloud Services", "Reverse Geocoding", "ReverseGeocoding.xaml.cs",
                "Click anywhere on the map to use the ReverseGeocodingCloudClient to access the ReverseGeocoding APIs available from the ThinkGeo Cloud",
                "ReverseGeocodingCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.ReverseGeocoding), () => new Samples.ReverseGeocoding()),
            new SampleEntry("Cloud Services", "Routing", "Routing.xaml.cs",
                "Use the RoutingCloudClient to route through a set of waypoints with the ThinkGeo Cloud",
                "RoutingCloudClient", SampleDraws.Style, typeof(Samples.Routing), () => new Samples.Routing()),
            new SampleEntry("Cloud Services", "Routing - Service Area", "RoutingServiceArea.xaml.cs",
                "Use the RoutingCloudClient to find the service area of a location with the ThinkGeo Cloud",
                "RoutingCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.RoutingServiceArea), () => new Samples.RoutingServiceArea()),
            new SampleEntry("Cloud Services", "Routing - Traveling Sales Person", "RoutingTravelingSalesPerson.xaml.cs",
                "Use the RoutingCloudClient to find an optimized route through a set of waypoints with the ThinkGeo Cloud",
                "RoutingCloudClient", SampleDraws.Style, typeof(Samples.RoutingTravelingSalesPerson), () => new Samples.RoutingTravelingSalesPerson()),
            new SampleEntry("Cloud Services", "Elevation", "Elevation.xaml.cs",
                "Use the ElevationCloudClient class to get elevation data from the ThinkGeo Cloud",
                "ElevationCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.Elevation), () => new Samples.Elevation()),
            new SampleEntry("Cloud Services", "Timezone", "Timezone.xaml.cs",
                "Use the TimeZoneCloudClient to access the Timezone APIs available from the ThinkGeo Cloud",
                "TimeZoneCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.Timezone), () => new Samples.Timezone()),
            new SampleEntry("Cloud Services", "Projection", "ProjectionCloudServices.xaml.cs",
                "Use the ProjectionCloudClient to access the Projection APIs available from the ThinkGeo Cloud",
                "ProjectionCloudClient", SampleDraws.Style, typeof(Samples.ProjectionCloudServices), () => new Samples.ProjectionCloudServices()),
            new SampleEntry("Cloud Services", "Color Utilities", "ColorUtilities.xaml.cs",
                "Use the ColorCloudClient class to access the ColorUtilities APIs available from the ThinkGeo Cloud",
                "ColorCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.ColorUtilities), () => new Samples.ColorUtilities()),
            new SampleEntry("Cloud Services", "World Maps Query", "WorldMapsQuery.xaml.cs",
                "Use the MapsQueryCloudClient to query the WorldMaps dataset available from the ThinkGeo Cloud",
                "MapsQueryCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.WorldMapsQuery), () => new Samples.WorldMapsQuery()),

            // ---- Printing & Export ----
            new SampleEntry("Printing & Export", "Print the Map", "PrintTheMap.xaml.cs",
                "Compose a page of map, legend, scale bar, title and data grid, then print it or export it as a vector PDF",
                "PrinterLayoutDocument", SampleDraws.Classic, typeof(Samples.PrintTheMap), () => new Samples.PrintTheMap()),
            new SampleEntry("Printing & Export", "Draw the map on an Image", "DrawTheMapOnAnImage.xaml.cs",
                "Draw your map onto an image file",
                "ThinkGeoRasterMapsAsyncLayer", SampleDraws.Classic, typeof(Samples.DrawTheMapOnAnImage), () => new Samples.DrawTheMapOnAnImage()),
            new SampleEntry("Printing & Export", "Get Map SnapShot", "GetMapSnapShot.xaml.cs",
                "Get a snapshot (screenshot) of the current map",
                "GetSnapshot", SampleDraws.StyleAndOverlay, typeof(Samples.GetMapSnapShot), () => new Samples.GetMapSnapShot()),

            // ---- Extending the SDK ----
            new SampleEntry("Extending the SDK", "Custom Background", "CustomBackground.xaml.cs",
                "Display a background overlay",
                "GeoLinearGradientBrush", SampleDraws.Classic, typeof(Samples.CustomBackground), () => new Samples.CustomBackground()),
            new SampleEntry("Extending the SDK", "Custom Feature Sources", "CustomFeatureSources.xaml.cs",
                "Use the FeatureSource base class to create your own custom Feature Source",
                "SimpleCsvFeatureSource", SampleDraws.StyleAndOverlay, typeof(Samples.CustomFeatureSources), () => new Samples.CustomFeatureSources()),
            // Deciding what happens when a tile fails is a hook into the SDK's own
            // loop, the same kind of thing as the two rows above.
            new SampleEntry("Extending the SDK", "Handle Exceptions", "HandleExceptions.xaml.cs",
                "A raster layer whose key is wrong fails every tile: the map leaves each one out and raises TileLoadFailed with the exception, and the handler decides whether the tile is asked for again",
                "TileLoadFailed", SampleDraws.Style, typeof(Samples.HandleExceptions), () => new Samples.HandleExceptions()),

        };

        /// <summary>
        /// The row of samples the home page opens with: what ThinkGeo 15 adds, one
        /// sample each. They stay in their own groups - this is a way in, not a
        /// group - because a reader who arrives for tilt still looks under 3D.
        /// </summary>
        internal static readonly IReadOnlyList<Type> Featured = new[]
        {
            typeof(Samples.PerspectiveTilt),
            typeof(Samples.Terrain3D),
            typeof(Samples.Buildings3DInteractive),
            typeof(Samples.TheGlobe),
            typeof(Samples.AdvancedLabels),
            typeof(Samples.LiveFlights),
        };

        /// <summary>The menu groups, in menu order.</summary>
        internal static IReadOnlyList<string> Categories =>
            All.Select(e => e.Category).Distinct(StringComparer.Ordinal).ToList();

        /// <summary>
        /// Where each chapter of the menu begins: the category that opens it,
        /// and the label the sidebar prints above that group. The stations of
        /// the journey - a programmer builds an application in roughly this
        /// order, and the groups in one flat column read better as a few
        /// spans - plus a legacy addendum at the end. A category not named
        /// here continues the chapter above it.
        /// </summary>
        internal static readonly IReadOnlyDictionary<string, string> ChapterStarts =
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["Start Here"] = "GET A MAP ON SCREEN",
                ["Styling"] = "MAKE IT LOOK RIGHT",
                ["Markers & Popups"] = "INTERACT WITH THE MAP",
                ["Shapes & Spatial Queries"] = "WORK WITH GEOMETRY",
                ["Cloud Services"] = "SERVICES, EXPORT & EXTENDING",
            };

        /// <summary>
        /// Sample controls compiled into this assembly that the catalog does not
        /// list, and catalog rows listed more than once. A sample that ships in
        /// the binary but not in the list is unreachable - which is exactly how the
        /// FAQ viewer went missing - so this is checked rather than trusted.
        /// </summary>
        internal static IReadOnlyList<string> FindDrift()
        {
            var problems = new List<string>();

            var listed = new HashSet<Type>(All.Select(e => e.SampleType));
            foreach (var group in All.GroupBy(e => e.SampleType).Where(g => g.Count() > 1))
            {
                problems.Add("listed " + group.Count() + " times: " + group.Key.Name);
            }

            var compiled = typeof(SampleCatalog).Assembly.GetTypes()
                .Where(t => !t.IsAbstract
                            && string.Equals(t.Namespace, "ThinkGeo.UI.Wpf.HowDoI.Samples", StringComparison.Ordinal)
                            && typeof(UserControl).IsAssignableFrom(t)
                            && !ExemptFromCatalogue.Contains(t.Name));

            foreach (var type in compiled.Where(t => !listed.Contains(t)).OrderBy(t => t.Name, StringComparer.Ordinal))
            {
                problems.Add("compiled but not in the catalog: " + type.Name);
            }

            // A chapter anchored to a category that no row carries would just
            // silently not print - which is exactly what renaming a group does.
            foreach (var category in ChapterStarts.Keys
                         .Where(c => !All.Any(e => string.Equals(e.Category, c, StringComparison.Ordinal)))
                         .OrderBy(c => c, StringComparer.Ordinal))
            {
                problems.Add("chapter starts at a category no sample is in: " + category);
            }

            foreach (var type in Featured.Where(t => !listed.Contains(t)))
            {
                problems.Add("featured but not in the catalog: " + type.Name);
            }

            foreach (var entry in All.Where(e => e.Draws != SampleDraws.Page))
            {
                var found = DrawsFromSource(entry);
                if (found != entry.Draws)
                {
                    problems.Add("Draws is " + entry.Draws + " but the code draws " + found + ": " + entry.Title);
                }
            }

            return problems;
        }

        private static readonly Regex StyleDocument = new Regex(@"\b(GpuBasemap|MapStyle)\b");

        // Overlays that draw: built in code, the MapView's own track and edit
        // overlays, and adornments. Map tools are controls, not drawing, and do
        // not count.
        private static readonly Regex DrawingOverlay = new Regex(
            @"\bnew\s+\w*Overlay\b|\.(TrackOverlay|EditOverlay|AdornmentOverlay)\b|\w+AdornmentLayer\b");

        private static readonly Regex MapViewElement = new Regex(@"<\w+:MapView[\s>]");

        /// <summary>What the embedded source of a row says it draws with. Two maps
        /// are not always a comparison - Overview Map has two - so side by side is
        /// only confirmed for the rows that claim it.</summary>
        private static SampleDraws DrawsFromSource(SampleEntry entry)
        {
            var code = ReadSource(entry.SourceFile);
            if (entry.Draws == SampleDraws.SideBySide
                && MapViewElement.Matches(ReadSource(Path.GetFileNameWithoutExtension(entry.SourceFile))).Count > 1)
            {
                return SampleDraws.SideBySide;
            }

            if (!StyleDocument.IsMatch(code))
            {
                return SampleDraws.Classic;
            }

            return DrawingOverlay.IsMatch(code) ? SampleDraws.StyleAndOverlay : SampleDraws.Style;
        }

        private static string ReadSource(string fileName)
        {
            using var stream = typeof(SampleCatalog).Assembly.GetManifestResourceStream("SampleSource." + fileName);
            if (stream == null)
            {
                return string.Empty;
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Sample-namespace controls that are deliberately not menu entries.
        /// Listing them explicitly keeps the drift check honest: an unexplained
        /// exemption is how a finished sample stays invisible.
        /// </summary>
        private static readonly HashSet<string> ExemptFromCatalogue = new HashSet<string>(StringComparer.Ordinal)
        {
            // Infrastructure - the base control the two document viewers render into.
            "MarkdownDocumentViewer",
            // Pages the two provider samples show behind a radio button, not rows.
            "ThirdPartyBasemaps",
            "OtherVectorProviders",
            // The animated icon Animated Marker puts inside its marker, not a sample.
            "CustomIcon",
        };
    }
}
