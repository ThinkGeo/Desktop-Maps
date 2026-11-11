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
                "Every sample in one table, with what it does, the API to search for and what it draws with",
                "SampleCatalog", SampleDraws.Page, typeof(HomePage), () => new HomePage(),
                aliases: "home, index, which gallery, .NET Framework, Classic gallery"),
            new SampleEntry("Start Here", "GPU or Classic?", "RendererGuidePage.xaml",
                "Why a map is drawn with a style document, which way each kind of data goes, where the classic overlays still belong, and what overlays do when the map tilts",
                "GpuBasemap", SampleDraws.Page, typeof(RendererGuidePage), () => new RendererGuidePage(),
                aliases: "renderer, style document, classic overlay, DynamicSource, paint delegate, tilt, drape"),
            // Moving a classic application across is the first thing an existing
            // customer asks, so the bridge opens the menu rather than closing it.
            new SampleEntry("Start Here", "Migrate a Classic Layer", "MigrateAClassicLayer.xaml.cs",
                "Translate FeatureLayers styled on their ZoomLevelSets and add them to a MapStyle, beside the classic renderer drawing the same layers, with what does not translate listed",
                "AddFeatureLayer", SampleDraws.SideBySide, typeof(Samples.MigrateAClassicLayer), () => new Samples.MigrateAClassicLayer(),
                aliases: "FeatureLayerTranslator, Translate, Warnings, ZoomLevelSet, LayerOverlay, migrate, v14, Custom Background, CustomBackground, background, BackgroundOverlay, SetBackground, GeoLinearGradientBrush"),
            new SampleEntry("Start Here", "Frequently Asked Questions", "FaqViewer.cs",
                "Which gallery to open, API keys, tile caches, blank maps, style.json errors, touch, cancelling async calls and click events",
                "MarkdownDocumentViewer", SampleDraws.Page, typeof(Samples.FaqViewer), () => new Samples.FaqViewer(),
                aliases: "FAQ, CloudApiKey, ClientId, ClientSecret, VectorTileCache, FileTileCache, TileLoadFailed, OperationCanceledException, MapClickDoubleClickMode"),

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
            // side, protocol servers follow.
            // One row for the turn-key providers, where three (ThinkGeo Vector Map, MVT
            // Server, and a documentation page for the bring-your-own-key ones) split
            // one lesson by vendor: a vector basemap is its style document, and the map
            // follows it. The keyed providers stay a page, shown behind a radio button.
            new SampleEntry("Maps from a Service - Vector", "Vector Map Providers", "VectorMapProviders.xaml.cs",
                "ThinkGeo Cloud (light, dark) and OpenFreeMap's free keyless styles, any MVT server by the URL of its style document, and the keyed providers - Mapbox, Esri, TomTom and more - as a page of their style URLs",
                "ThinkGeoVectorTileSource", SampleDraws.Style, typeof(Samples.VectorMapProviders), () => new Samples.VectorMapProviders(),
                aliases: "ThinkGeo Vector Map, MVT Server, MapStyle, style.json, style URL, OpenFreeMap, Mapbox, Esri, TomTom, MapTiler, Stadia, dark, dark theme, light theme, switch style, ThinkGeoVectorStyles, SetStyleAsync",
                service: "OpenFreeMap"),
            // From here down the server sends features rather than tiles -
            // geometry and attributes you style yourself, which is why these
            // rows lead straight into Styling.
            new SampleEntry("Maps from a Service - Vector", "WFS", "WFS.xaml.cs",
                "Dutch cadastral parcels from PDOK's WFS V2 service merged into the basemap style, numbered from their own attributes once you zoom in",
                "WfsV2AsyncFeatureSource", SampleDraws.Style, typeof(Samples.WFS), () => new Samples.WFS(),
                aliases: "Web Feature Service, WfsV2AsyncFeatureSource, WfsV2FeatureSource, MinDataZoom, FeatureSourceVectorTileSource, PDOK, cadastre",
                service: "PDOK"),
            new SampleEntry("Maps from a Service - Vector", "OGC API Feature Server", "OGCAPIFeatureServer.xaml.cs",
                "The named places of Spain from IGN's OGC API Features service, merged into the basemap style as dots labeled from their own attributes",
                "OgcApiAsyncFeatureSource", SampleDraws.Style, typeof(Samples.OGCAPIFeatureServer), () => new Samples.OGCAPIFeatureServer(),
                aliases: "OGC API Features, OgcApiAsyncFeatureSource, OgcApiFeatureSource, MinDataZoom, IGN, named places",
                service: "IGN Spain"),
            new SampleEntry("Maps from a Service - Vector", "NOAA Weather", "NOAAWeather.xaml.cs",
                "NOAA's live feeds over the basemap - weather stations as dots colored by their current temperature, or the active weather warnings as areas colored by severity",
                "NoaaWeatherStationFeatureSource", SampleDraws.Style, typeof(Samples.NOAAWeather), () => new Samples.NOAAWeather(),
                aliases: "NOAA Weather Stations, NOAA Weather Warnings, NOAAWeatherStations, NOAAWeatherWarnings, NoaaWeatherStationFeatureSource, NoaaWeatherWarningsFeatureSource, temperature, step, circle-color, severity, match",
                service: "NOAA"),

            // One row for the turn-key providers, where four (ThinkGeo Raster Map, Open
            // Street Map, XYZ Tile Server, and a documentation page for the bring-your-own-key
            // ones) split the same one-line registration by vendor. The keyed providers stay
            // a page, shown behind a radio button: without a key none of them draws.
            new SampleEntry("Maps from a Service - Raster", "Raster Map Providers", "RasterMapProviders.xaml.cs",
                "ThinkGeo Cloud (light, dark, aerial, hybrid), OpenStreetMap and any {z}/{x}/{y} tile server you type the URL of, one tile source each, and the keyed providers - Azure Maps, Google Maps, HERE and Mapbox - as a page of their classes and one-line usage",
                "ThinkGeoRasterTileSource", SampleDraws.Style, typeof(Samples.RasterMapProviders), () => new Samples.RasterMapProviders(),
                aliases: "XYZ Tile Server, XyzTileServer, XyzRasterTileSource, OpenStreetMapRasterTileSource, custom XYZ URL, tile URL template, UserAgent, AddRaster, ThinkGeo Raster Map, Azure Maps, Google Maps, HERE, Mapbox",
                service: "OpenStreetMap"),
            new SampleEntry("Maps from a Service - Raster", "WMS", "WMS.xaml.cs",
                "NASA GIBS true-color imagery of the Earth two days ago over the basemap - one GetMap request per web-mercator tile",
                "WmsRasterTileSource", SampleDraws.Style, typeof(Samples.WMS), () => new Samples.WMS(),
                aliases: "Web Map Service, WmsRasterTileSource, WmsClient, SendingHttpRequest, authentication, credentials, StyleLayerSlot, NASA GIBS",
                service: "NASA GIBS"),
            new SampleEntry("Maps from a Service - Raster", "WMTS", "WMTS.xaml.cs",
                "The Swiss national map from swisstopo's WMTS server as the basemap, read through its web-mercator tile matrix set",
                "WmtsRasterTileSource", SampleDraws.Style, typeof(Samples.WMTS), () => new Samples.WMTS(),
                aliases: "Web Map Tile Service, WmtsRasterTileSource, TileMatrixSetName, swisstopo",
                service: "swisstopo"),

            // ---- Your Data ----
            // The store you own: files on disk, your own databases, tiles you cut
            // yourself - the other side of the who-holds-it axis from the service
            // groups. Four rows open it - vector files, raster files, database
            // tables, offline tiles - each a picker over every format the SDK
            // reads, because the format is one line and everything after it is
            // the same.
            //
            // Every row here but Nautical Charts draws with the data in the same
            // style document as the basemap: FeatureSourceVectorTileSource cuts any
            // FeatureSource into vector tiles, ClassicRasterTileSource serves any
            // RasterSource, and an archive of tiles is a tile source already.
            new SampleEntry("Your Data", "Vector File Formats", "VectorFileFormats.xaml.cs",
                "Open a vector file - a shapefile, GeoJSON, KML, GPX, MapInfo TAB, TinyGeo, CAD, a GeoPDF - or features built in code, all drawn by one style; reading the format is the only line that differs",
                "FeatureSourceVectorTileSource", SampleDraws.Style, typeof(Samples.VectorFileFormats), () => new Samples.VectorFileFormats(),
                aliases: "ShapeFileFeatureSource, GeoJsonFeatureSource, KmlGdalFeatureSource, GpxFeatureSource, TabFeatureSource, TinyGeoFeatureSource, CadFeatureSource, DWG, GeoPdfGdalFeatureSource, InMemoryFeatureSource, shapefile, build features in code"),
            new SampleEntry("Your Data", "Raster File Formats", "RasterFileFormats.xaml.cs",
                "Open a raster file - GeoTIFF, JPEG 2000, a plain JPEG, MrSID or ECW, or a plain JPEG read by GdalRasterSource itself - warped to Web Mercator once and served as tiles",
                "ClassicRasterTileSource", SampleDraws.Style, typeof(Samples.RasterFileFormats), () => new Samples.RasterFileFormats(),
                aliases: "GdalRasterSource, GeoTiffGdalRasterSource, Jpeg2000GdalRasterSource, MrSidRasterSource, EcwGdalRasterSource, WarpProjection, world file, GeoTIFF"),
            // A GeoPackage and an Esri file geodatabase sit here rather than with the
            // vector files: you open them by table, the way you open SQLite or Postgres,
            // and a GeoPackage IS a SQLite file.
            new SampleEntry("Your Data", "Databases", "Databases.xaml.cs",
                "Open a table from a database - SQLite, a GeoPackage, an Esri file geodatabase, PostgreSQL / PostGIS or SQL Server; the connection is the only line that differs",
                "FeatureSourceVectorTileSource", SampleDraws.Style, typeof(Samples.Databases), () => new Samples.Databases(),
                aliases: "SqliteFeatureSource, GdalFeatureSource, GeoPackage, FileGeoDatabaseFeatureSource, PostgreSqlFeatureSource, PostGIS, SqlServerFeatureSource, connection string"),
            new SampleEntry("Your Data", "Offline Tiles", "OfflineTiles.xaml.cs",
                "Serve tiles off disk with no network - an MBTiles or PMTiles archive, vector or raster, or a folder of z/x/y images written by QGIS; opening the archive is the only line that differs",
                "MbTilesVectorTileSource", SampleDraws.Style, typeof(Samples.OfflineTiles), () => new Samples.OfflineTiles(),
                aliases: "Offline Basemap from Files, Offline Vector Tiles, Offline Raster Tiles, MbTilesVectorTileSource, PmTilesVectorTileSource, MbTilesRasterTileSource, PmTilesRasterTileSource, MBTiles, PMTiles, z/x/y folder, RasterTileSource"),
            new SampleEntry("Your Data", "Pre-Generate a Tile Cache", "PreGenerateTileCache.xaml.cs",
                "Render Frisco's city limits, parks and streets into a folder of z/x/y PNG tiles, with progress and cancel, then draw the map from that folder",
                "CpuTileRenderer", SampleDraws.Style, typeof(Samples.PreGenerateTileCache), () => new Samples.PreGenerateTileCache(),
                aliases: "PreGenerateTileCache, tile cache, generate tiles, seed a cache, RenderTileAsync, GenerateTileCacheAsync, FileRasterTileCache, z/x/y, offline"),
            // The one row in the group that keeps a classic overlay: an S-57 chart is
            // drawn by its S-52 symbology, which lives in NauticalChartsFeatureLayer,
            // not in a style document.
            new SampleEntry("Your Data", "Nautical Charts", "NauticalCharts.xaml.cs",
                "Draw an S-57 chart with its embedded S-52 styling, colored for the ship's draft, and ask it at every step of a ship's route about the depth under her keel, the aids to navigation and hazards nearby, and the restricted areas she is in",
                "NauticalChartsFeatureLayer", SampleDraws.StyleAndOverlay, typeof(Samples.NauticalCharts), () => new Samples.NauticalCharts(),
                aliases: "NauticalChartsFeatureSource, S-57, S-52, ENC, safety contour, safety depth, draft, under-keel clearance"),

            // ---- Styling ----
            // One row, not four: this sample already styles a point layer, a line
            // layer and a polygon layer and labels each of them. The PDF export stays here
            // until Print the Map gets a style-document mode (plan G1).
            new SampleEntry("Styling", "Render Points, Lines and Polygons", "RenderPointsLinesAndPolygons.xaml.cs",
                "Parks, streets and hotels styled and labelled from the style document beside the map, editable while it runs, and exported from the same document to a vector PDF",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.RenderPointsLinesAndPolygons), () => new Samples.RenderPointsLinesAndPolygons(),
                aliases: "Render Points, Render Lines, Render Areas, circle, line, fill, symbol, export PDF, PdfGeoCanvas, MapStyleFeatureAsyncLayer"),
            // One row for ValueStyle, ClassBreakStyle, FilterStyle, boolean-expression styles
            // and zoom-level styling (classic ZoomLevelSet): all are expressions in the document.
            new SampleEntry("Styling", "Render Based on Rules", "RenderBasedOnRules.xaml.cs",
                "One world map styled five ways - by value, by range, by filter, by condition and by zoom level - each document editable while the map runs",
                "SetStyleAsync", SampleDraws.Style, typeof(Samples.RenderBasedOnRules), () => new Samples.RenderBasedOnRules(),
                aliases: "ValueStyle, ClassBreakStyle, FilterStyle, match, step, filter, case, zoom, minzoom, maxzoom, interpolate, render based on zoom level, ZoomLevelSet"),
            new SampleEntry("Styling", "Hatch Styles", "HatchStyles.xaml.cs",
                "Parks filled with any classic GeoHatchStyle pattern - the picked brush is registered through MapStyle.Images under the name the document's fill-pattern uses; hover a row to preview it",
                "MapStyle.Images", SampleDraws.Style, typeof(Samples.HatchStyles), () => new Samples.HatchStyles(),
                aliases: "GeoHatchStyle, GeoHatchBrush, HatchStyle, fill-pattern"),
            // Labels as the subject: each behavior set up on data crafted to show it.
            new SampleEntry("Styling", "Advanced Labels", "AdvancedLabels.xaml.cs",
                "Seven label lessons on crafted data: text bends along its line, anchors from its own column, yields to priority, wraps and recases, keeps its icon when crowded, writes CJK vertically, and joins several columns into one label - each document editable while it runs",
                "SetStyleAsync", SampleDraws.Style, typeof(Samples.AdvancedLabels), () => new Samples.AdvancedLabels(),
                aliases: "Create a Multi-Column Text Style, CreateAMultiColumnTextStyle, multi-column, TextStyle {COLUMN}, concat, text-field, symbol-placement, text-anchor, symbol-sort-key, text-max-width, text-transform, text-optional"),
            // What a document cannot hold - the clock, or a C# lambda over the feature - is a
            // code-built layer. Replaces "Render Based on Regex" and "Custom Styles".
            new SampleEntry("Styling", "Render Based on Code", "RenderBasedOnCode.xaml.cs",
                "Day and night sweep the world's capitals as the clock switches code-built layers on and off, and a C# paint lambda shades the US states by population density under the basemap's roads and labels",
                "StyleLayer.CreateFill", SampleDraws.Style, typeof(Samples.RenderBasedOnCode), () => new Samples.RenderBasedOnCode(),
                aliases: "Custom Styles, Render Based on Regex, paint delegate, StyleLayer.CreateCircle, SetLayerVisibility, choropleth"),
            // The map's own style document, managed and composed at runtime.
            new SampleEntry("Styling", "Style Layer Toggles", "StyleLayerToggles.xaml.cs",
                "Turn individual style.json layers on and off while the map is live",
                "MapStyle.SetLayerVisibility", SampleDraws.Style, typeof(Samples.StyleLayerToggles), () => new Samples.StyleLayerToggles(),
                aliases: "layer visibility, IsVisible"),
            new SampleEntry("Styling", "Insert a Layer Under the Labels", "InsertALayerUnderTheLabels.xaml.cs",
                "Aerial imagery under vector streets and labels, with a translucent wash inserted above the roads and below the labels - a slot no overlay can reach, because an overlay is always above everything",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.InsertALayerUnderTheLabels), () => new Samples.InsertALayerUnderTheLabels(),
                aliases: "StyleLayerSlot.UnderLabels, AddVectorTiles"),
            new SampleEntry("Styling", "Heatmap", "Heatmap.xaml.cs",
                "Render point observations as a heatmap, with radius, intensity, opacity and color ramp on live controls",
                "SetHeatmapPaint", SampleDraws.Style, typeof(Samples.Heatmap), () => new Samples.Heatmap(),
                aliases: "HeatStyle, HeatLayer"),
            // Stands where the classic DisplayISOLine row did: the field is interpolated and
            // contoured as the map draws, instead of IDW plus marching squares per redraw.
            new SampleEntry("Styling", "Isolines", "Isolines.xaml.cs",
                "Contour lines interpolated from mosquito counts at scattered points, labelled, with filled bands, levels, line width and interpolation on live controls",
                "AddIsolines", SampleDraws.Style, typeof(Samples.Isolines), () => new Samples.Isolines(),
                aliases: "GpuIsolines, DisplayISOLine, DynamicIsoLineLayer, IsoLineLayer, contour lines, PointValueSource, IsolineStyle"),
            new SampleEntry("Styling", "Display Dot Density", "DisplayDotDensity.xaml.cs",
                "Two colors of dot mixed inside each state - countable, and a composition a heatmap cannot show - with the dots generated once as data",
                "GpuBasemap", SampleDraws.Style, typeof(Samples.DisplayDotDensity), () => new Samples.DisplayDotDensity(),
                aliases: "DotDensityStyle"),

            // ---- Dynamic Data ----
            new SampleEntry("Dynamic Data", "Dynamic Rendering", "DynamicRendering.xaml.cs",
                "State populations from 1950 to 2020 as circles that grow and change color as you drag or play the year slider",
                "DynamicSource.SetPoint", SampleDraws.Style, typeof(Samples.DynamicRendering), () => new Samples.DynamicRendering(),
                aliases: "population, animated circles, marker image, Images.Add, PointStyle disc"),
            new SampleEntry("Dynamic Data", "Live Flights", "LiveFlights.xaml.cs",
                "Every aircraft OpenSky reports airborne, flying across the map at a speed you set; hover one for its callsign, altitude and trail",
                "DynamicSource", SampleDraws.Style, typeof(Samples.LiveFlights), () => new Samples.LiveFlights(),
                aliases: "aircraft, flights, dead reckoning, DynamicSource.SetPoint, DynamicSource.SetLine, moving markers",
                service: "OpenSky"),
            new SampleEntry("Dynamic Data", "Vehicle Navigation", "VehicleNavigation.xaml.cs",
                "A car drives a recorded GPS route to the Empire State Building, the camera following and turning with it, or holding still over the whole route",
                "DynamicSource.SetLine", SampleDraws.Style, typeof(Samples.VehicleNavigation), () => new Samples.VehicleNavigation(),
                aliases: "vehicle tracking, GPS track, follow camera, ZoomToAsync, CurrentExtentChangedInAnimation, DynamicSource.SetPoint, DynamicSource.SetArea"),
            new SampleEntry("Dynamic Data", "NEXRAD Weather Radar", "NexradRadar.xaml.cs",
                "The latest US radar composite over the basemap, looping through the last half hour",
                "GeoImageRasterTileSource", SampleDraws.Style, typeof(Samples.NexradRadar), () => new Samples.NexradRadar(),
                aliases: "radar, weather, georeferenced image, GeoImageRasterTileSource.Update, AddRaster",
                service: "Iowa Environmental Mesonet"),
            new SampleEntry("Dynamic Data", "Wind as a Vector Field", "NdfdWindField.xaml.cs",
                "NDFD surface wind over the US as streaming particles colored by speed, or as arrows from the same field",
                "VectorFieldSource", SampleDraws.Style, typeof(Samples.NdfdWindField), () => new Samples.NdfdWindField(),
                aliases: "wind, NDFD, AddParticleFlow, AddVectorField, FlowVector, particles, arrows"),
            new SampleEntry("Dynamic Data", "Animate a Weather Grid", "NdfdSkyGridAnimation.xaml.cs",
                "An NDFD sky-cover forecast over the US, one forecast hour at a time with its valid time; play it, drag through the hours, or switch color ramps",
                "InMemoryGridSource", SampleDraws.Style, typeof(Samples.NdfdSkyGridAnimation), () => new Samples.NdfdSkyGridAnimation(),
                aliases: "GridSource, AddGridFill, SetValues, GdalGrid, NDFD, GRIB, sky cover, color ramp, ColorStop"),

            // ---- 3D & Camera ----
            // 3D Buildings merged in: the check box draws or skips the fill-extrusion layer the
            // basemap style already carries; it does not extrude footprints of your own.
            new SampleEntry("3D & Camera", "Perspective Tilt", "PerspectiveTilt.xaml.cs",
                "Tilt the camera over midtown Manhattan in vector tiles, with the basemap style's buildings on or off, or over a raster basemap of the San Francisco Bay",
                "TiltAngle", SampleDraws.Style, typeof(Samples.PerspectiveTilt), () => new Samples.PerspectiveTilt(),
                aliases: "3D Buildings, Buildings3DInteractive, GpuBasemap, GpuBasemap.IsExtrusionDrawingEnabled, IsExtrusionDrawingEnabled, fill-extrusion, buildings, TiltToAsync, IsTiltEnabled, pitch"),
            new SampleEntry("3D & Camera", "3D Terrain", "Terrain3D.xaml.cs",
                "Aerial imagery draped over the Grand Canyon's terrain with relief shading; click two points for an elevation profile along the ground",
                "MapStyle.SetTerrain", SampleDraws.Style, typeof(Samples.Terrain3D), () => new Samples.Terrain3D(),
                aliases: "RasterDemSource, DemEncoding.Terrarium, hillshade, DEM, elevation profile, GetElevationProfileAsync, GetElevationAsync, RemoveTerrain, GpuBasemap",
                service: "AWS Terrain Tiles"),
            new SampleEntry("3D & Camera", "Lane-Level Navigation", "LaneLevelNavigation.xaml.cs",
                "An in-car view driving 3.7 km through the Wolfsburg OpenDRIVE HD map: every surveyed lane and its paint, a follow camera, the basemap's buildings, and a card with the current lane and the distance to the next lane change",
                "FeatureSourceVectorTileSource", SampleDraws.Style, typeof(Samples.LaneLevelNavigation), () => new Samples.LaneLevelNavigation(),
                aliases: "OpenDRIVE, HD map, lanes, Wolfsburg, follow camera, ZoomToAsync, CurrentExtentChangedInAnimation, StyleLayerType.FillExtrusion, InMemoryFeatureSource"),

            // ---- Map Navigation ----
            new SampleEntry("Map Navigation", "Map Navigation", "MapNavigation.xaml.cs",
                "Pan, zoom, rotate and tilt around the Empire State Building with the mouse - or with fingers once Touch screen is on - and watch MapClick, MapDoubleClick, CurrentScaleChanged and CurrentExtentChanged arrive in a log",
                "ZoomToAsync", SampleDraws.StyleAndOverlay, typeof(Samples.MapNavigation), () => new Samples.MapNavigation(),
                aliases: "pan, zoom, rotate, tilt, compass, reset north, touch, touch screen, Navigate on Touch Screen, NavigateOnTouchScreen, IsManipulationEnabled, map events, Basic Map Events, MapClick, MapDoubleClick, CurrentExtentChanged, CurrentScaleChanged, RotationAngle"),
            new SampleEntry("Map Navigation", "Zoom to Extents and Scales", "ZoomToExtents.xaml.cs",
                "Zoom in and out, to a typed scale, to the Frisco boundary's extent and to a point, rotate with a slider, and make the wheel stop at printed-map scales",
                "ZoomToAsync", SampleDraws.Style, typeof(Samples.ZoomToExtents), () => new Samples.ZoomToExtents(),
                aliases: "Zoom to Extents, ZoomToExtents, ZoomScales, CurrentExtent, CenterAtAsync, ZoomInAsync, ZoomOutAsync, GetBoundingBox, zoom to scale, zoom to layer"),
            new SampleEntry("Map Navigation", "Restrict Map Extent", "RestrictMapExtent.xaml.cs",
                "Pan and zoom inside a red box around Dallas: RestrictExtent keeps the view inside it, MaximumScale and MinimumScale bound the zoom, and each can be switched off",
                "RestrictExtent", SampleDraws.Style, typeof(Samples.RestrictMapExtent), () => new Samples.RestrictMapExtent(),
                aliases: "MaximumScale, MinimumScale, limit panning, zoom range, map bounds"),
            new SampleEntry("Map Navigation", "Resize the Map", "ResizeTheMap.xaml.cs",
                "Drag the bar beside the map to resize it and see what each MapResizeMode keeps: the scale, the scale and the center, or the extent",
                "MapResizeMode", SampleDraws.Style, typeof(Samples.ResizeTheMap), () => new Samples.ResizeTheMap(),
                aliases: "PreserveScale, PreserveScaleAndCenter, PreserveExtent, resize"),
            new SampleEntry("Map Navigation", "Overview Map", "OverviewMap.xaml.cs",
                "A small overview map whose red frame follows and turns with the main map's view; click the overview to recenter the main map there",
                "CurrentExtentChanged", SampleDraws.Style, typeof(Samples.OverviewMap), () => new Samples.OverviewMap(),
                aliases: "overview, mini map, inset map, locator map, DynamicSource, MapMouseDown, RotationAngleChanging"),
            new SampleEntry("Map Navigation", "Zoom to the Black Hole", "ZoomToBlackHole.xaml.cs",
                "Fly with an animated ZoomToAsync from the galaxy M87 down to the shadow of its black hole, through ten pictures nested inside each other",
                "ZoomToAsync", SampleDraws.Style, typeof(Samples.ZoomToBlackHole), () => new Samples.ZoomToBlackHole(),
                aliases: "fly to, animation, MapAnimationSettings, DefaultAnimationSettings, GeoImageRasterTileSource, powers of ten, M87"),

            // ---- Markers & Popups ----
            // Three rows. "Markers and Popups on a Tilted Map" and "Animated Marker" folded
            // into Markers (its map tilts; one of its markers has an animated WPF control
            // as Content); "Text Marker" was a subset of Markers and Popups. "Custom Icon"
            // is the animated control Markers uses as that content, not a sample.
            new SampleEntry("Markers & Popups", "Markers", "Markers.xaml.cs",
                "Click the map to add a marker, drag one to move it, click one to remove it, beside a marker whose content is an animated WPF control - and tilt the map, the markers keep their ground position",
                "SimpleMarkerOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.Markers), () => new Samples.Markers(),
                aliases: "Animated Marker, AnimatedMarker, Marker.Content, custom marker, WPF element marker, Text Marker, Markers and Popups on a Tilted Map, MarkerOverlay, MarkerDragMode, PositionChanged, MarkerDragged, remove marker"),
            new SampleEntry("Markers & Popups", "Popups", "Popups.xaml.cs",
                "Hotel names in popups you can turn off, and a popup that opens where you click the map with that point's latitude and longitude",
                "PopupOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.Popups), () => new Samples.Popups(),
                aliases: "Popup, info window, callout, click to show popup, remove popup"),
            // Moved from Styling: it is a SimpleMarkerOverlay with the clustering done in
            // the sample, no styling API.
            new SampleEntry("Markers & Popups", "Cluster Markers", "ClusterMarkers.xaml.cs",
                "Points clustered into markers recomputed for the view: zoom and the clusters split, click one to zoom into it",
                "SimpleMarkerOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.ClusterMarkers), () => new Samples.ClusterMarkers(),
                aliases: "Display Cluster Points, DisplayClusterPoints, cluster, clustering, point clustering, ClusterPointStyle"),

            // ---- Map Tools & Adornments ----
            new SampleEntry("Map Tools & Adornments", "Map Tool Controls", "MapToolControls.xaml.cs",
                "Turn the map's built-in tools on and off - pan zoom bar, logo, scale line, mouse coordinates - format the coordinates, and read how MapTools differ from adornment layers and the measure overlay",
                "MapTools", SampleDraws.Style, typeof(Samples.MapToolControls), () => new Samples.MapToolControls(),
                aliases: "MouseCoordinateMapTool, PanZoomBar, Logo, ScaleLine, MouseCoordinateType, CustomFormatted, AdornmentLayer, MeasureInteractiveOverlay, map tools vs adornments"),
            new SampleEntry("Map Tools & Adornments", "Drag/Resize Adornments", "DragResizeAdornments.xaml.cs",
                "Let the user drag and resize the scale line, scale bar, logo, legend and a compass showing true north and magnetic north",
                "AdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.DragResizeAdornments), () => new Samples.DragResizeAdornments(),
                aliases: "Magnetic North, MagneticNorth, MagneticDeclinationAdornmentLayer, compass, north arrow, True North, Grid North, AdornmentDragMode, AdornmentResizeMode"),
            new SampleEntry("Map Tools & Adornments", "ScaleLine and ScaleBar", "ScaleLineScaleBar.xaml.cs",
                "A scale line and a scale bar given the map's projection so they report ground distance; switch the scale line's unit system and text styles",
                "ScaleLineAdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.ScaleLineScaleBar), () => new Samples.ScaleLineScaleBar(),
                aliases: "ScaleBarAdornmentLayer, ScaleLineUnitSystem, scale bar, scale line units"),
            // A legend is an adornment - it sits in the map's corner like the scale bar
            // and the north arrow, not in the style document - so it belongs here rather
            // than among the styling samples.
            new SampleEntry("Map Tools & Adornments", "Legend", "Legend.xaml.cs",
                "Build a legend whose swatches are the styles the map draws with",
                "LegendAdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.Legend), () => new Samples.Legend(),
                aliases: "LegendItem, ClassBreakStyle legend, map key"),
            new SampleEntry("Map Tools & Adornments", "Display Graticule Lines", "DisplayGraticuleLines.xaml.cs",
                "Display graticule lines - a latitude/longitude grid",
                "GraticuleLineStyle", SampleDraws.StyleAndOverlay, typeof(Samples.DisplayGraticuleLines), () => new Samples.DisplayGraticuleLines(),
                aliases: "GraticuleFeatureLayer, lat/lon grid, grid lines"),
            new SampleEntry("Map Tools & Adornments", "Measure Distance and Area", "MeasureDistanceAndArea.xaml.cs",
                "Measure distance and area on the map with the overlay that annotates as you draw",
                "MeasureInteractiveOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.MeasureDistanceAndArea), () => new Samples.MeasureDistanceAndArea(),
                aliases: "ruler, measure, TrackInteractiveOverlay, area, distance"),

            // ---- Draw & Edit ----
            // "Draw" is in the name because it is the first thing the group does:
            // its lead sample starts with drawing shapes, and "how do I let the
            // user draw on the map" is the question that brings people here.
            new SampleEntry("Draw & Edit", "Edit Features", "EditFeatures.xaml.cs",
                "Draw points, lines and polygons, edit them with handles and vertices, delete them, measure them, and watch the TrackOverlay and EditOverlay events in a log",
                "EditOverlay", SampleDraws.StyleAndOverlay, typeof(Samples.EditFeatures), () => new Samples.EditFeatures(),
                aliases: "Edit Map Events, EditMapEvents, TrackOverlay, TrackMode, TrackEnded, VertexMoved, FeatureDragged, FeatureResized, FeatureRotated, draw shapes, editing events"),
            new SampleEntry("Draw & Edit", "Edit Features with Snapping", "EditWithSnapping.xaml.cs",
                "Drag a vertex into a circle, and it will snap to its center",
                "VertexMovingEditInteractiveOverlayEventArgs", SampleDraws.StyleAndOverlay, typeof(Samples.EditWithSnapping), () => new Samples.EditWithSnapping(),
                aliases: "snap, snapping, VertexMoving, tolerance"),
            new SampleEntry("Draw & Edit", "Edit with Constraints", "EditWithConstraints.xaml.cs",
                "Keep vertices at least 300 m apart on the ground, keep a rectangle rectangular, type exact coordinates",
                "VertexAddingEditInteractiveOverlayEventArgs", SampleDraws.StyleAndOverlay, typeof(Samples.EditWithConstraints), () => new Samples.EditWithConstraints(),
                aliases: "VertexAdding, Cancel, minimum vertex spacing, rectangle constraint, exact coordinates"),

            // ---- Shapes & Spatial Queries ----
            new SampleEntry("Shapes & Spatial Queries", "Get Data from Features", "GetDataFromFeatures.xaml.cs",
                "Click a Frisco park to read its column values in a popup, or list every park's columns and pick one",
                "ColumnValues", SampleDraws.StyleAndOverlay, typeof(Samples.GetDataFromFeatures), () => new Samples.GetDataFromFeatures(),
                aliases: "identify feature on click, Get Data from One Feature, GetDataFromOneFeature, Get Data from All Features, GetDataFromAllFeatures, GetFeaturesContaining, SpatialQuery, GetAllFeatures, PopupOverlay, attributes"),
            new SampleEntry("Shapes & Spatial Queries", "Find Features by Spatial Relation", "FindFeaturesBySpatialRelation.xaml.cs",
                "Pick a relation and see which of Frisco's zoning parcels match the point, line or polygon you draw - and, for equal, which subdivision plats were recorded again with the same boundary",
                "QueryTools", SampleDraws.StyleAndOverlay, typeof(Samples.FindFeaturesBySpatialRelation), () => new Samples.FindFeaturesBySpatialRelation(),
                aliases: "Check if Features are Equal, CheckIfFeaturesAreEqual, GetFeaturesTopologicalEqual, topological equality, duplicate features, GetFeaturesContaining, GetFeaturesCrossing, GetFeaturesDisjointed, GetFeaturesIntersecting, GetFeaturesOverlapping, GetFeaturesTouching, GetFeaturesWithin, GetFeaturesWithinDistanceOf, spatial query"),
            new SampleEntry("Shapes & Spatial Queries", "Shape Operations", "ShapeOperations.xaml.cs",
                "Transform a shape - union, buffer, simplify, rotate, scale, translate, convex hull, envelope, intersection or difference - each one a single call on the shape",
                "BaseShape", SampleDraws.Style, typeof(Samples.ShapeOperations), () => new Samples.ShapeOperations(),
                aliases: "Buffer, Simplify, Rotate, ScaleTo, TranslateByOffset, GetConvexHull, GetBoundingBox, GetIntersection, GetDifference, Union"),
            new SampleEntry("Shapes & Spatial Queries", "Shape Measurements", "ShapeMeasurements.xaml.cs",
                "Click a park or a trail and measure it - area, length, center point in longitude and latitude, the shortest line to the stadium, or a stretch of a line starting a given distance along",
                "BaseShape", SampleDraws.Style, typeof(Samples.ShapeMeasurements), () => new Samples.ShapeMeasurements(),
                aliases: "GetArea, GetLength, GetCenterPoint, GetShortestLineTo, GetLineOnALine"),
            new SampleEntry("Shapes & Spatial Queries", "Validate Topology", "ValidateTopology.xaml.cs",
                "Every rule TopologyValidator checks - what points may touch, what lines may cross, what polygons may leave between them - on shapes small enough to see the answer",
                "TopologyValidator", SampleDraws.Style, typeof(Samples.ValidateTopology), () => new Samples.ValidateTopology()),

            // ---- Projections ----
            // Projected when the data is read first (features, rasters), then projected
            // as the map is drawn (any projection, the globe, the dateline), then the two
            // side by side.
            new SampleEntry("Projections", "Project Features", "ProjectFeatures.xaml.cs",
                "Frisco's subdivisions projected by their source as it reads them, or longitude/latitude WKT you type converted to web mercator by ProjectionConverter or by the ThinkGeo Cloud",
                "ProjectionConverter", SampleDraws.Style, typeof(Samples.ProjectFeatures), () => new Samples.ProjectFeatures(),
                aliases: "Setting the Projection of a Layer, SettingTheProjectionOfALayer, Projection (Cloud), ProjectionCloudServices, ProjectionCloudClient, ProjectAsync, ConvertToExternalProjection, reproject features"),
            // Classic rendering on purpose: reprojection lives in the layer's draw path
            // (the image is resampled, native tiles are merged and warped as they draw),
            // while the basemap's raster sources serve native-grid tiles only.
            new SampleEntry("Projections", "Reproject a Raster", "ReprojectARaster.xaml.cs",
                "World.tif warped into EPSG:9311, or ThinkGeo Cloud or OpenStreetMap tiles warped into EPSG:4326 - the same GdalProjectionConverter on the layer either way",
                "GdalProjectionConverter", SampleDraws.Classic, typeof(Samples.ReprojectARaster), () => new Samples.ReprojectARaster(),
                aliases: "Project a Raster, ProjectARaster, Reproject a Raster Tile Service, ReprojectRasterTiles, GeoTiffRasterLayer, RasterXyzTileAsyncLayer, OpenStreetMapAsyncLayer, ThinkGeoRasterMapsAsyncLayer, SingleTile",
                service: "OpenStreetMap"),
            new SampleEntry("Projections", "Any Projection", "AnyProjection.xaml.cs",
                "A vector or satellite map in any of ten projections - zoom and pan as usual - animated from one projection to the next",
                "DisplayProjection", SampleDraws.Style, typeof(Samples.AnyProjection), () => new Samples.AnyProjection(),
                aliases: "Any Projection, Vector Tiles, Any Projection, Satellite Imagery, MorphBetweenProjectionsMvt, AnyProjectionRaster, AnimateDisplayProjectionAsync, FromProjString, proj4, Robinson, Mollweide, Albers, morph"),
            new SampleEntry("Projections", "The Globe", "TheGlobe.xaml.cs",
                "The same tiles on a sphere, and what it does when you zoom in far enough to let go",
                "DisplayProjection.Globe", SampleDraws.Style, typeof(Samples.TheGlobe), () => new Samples.TheGlobe()),
            // Replaces "Wrap the DateLine", which taught the classic overlay's translated
            // copies over geometry stored past the world edge. What is left to teach is
            // the seam in the DATA, and a flight across the Pacific is where every
            // reader meets it.
            new SampleEntry("Projections", "Cross the Dateline", "CrossTheDateline.xaml.cs",
                "A flight from Los Angeles to Shanghai over the antimeridian: route, corridor and aircraft stored with continuous longitudes, whole on the globe and whole on both sides of the seam in Web Mercator",
                "DisplayProjection.Globe", SampleDraws.Style, typeof(Samples.CrossTheDateline), () => new Samples.CrossTheDateline(),
                aliases: "Wrap the DateLine, antimeridian, 180th meridian, great circle"),
            new SampleEntry("Projections", "CPU vs GPU Projection", "CpuVsGpuProjection.xaml.cs",
                "The same world shapefile projected by the CPU as it is read on the left and by the GPU as it is drawn on the right, with what each switch costs",
                "ProjectionConverter", SampleDraws.SideBySide, typeof(Samples.CpuVsGpuProjection), () => new Samples.CpuVsGpuProjection(),
                aliases: "CPU vs GPU projection, side by side, CpuVsGpuProjection, DisplayProjection, datum"),

            // ---- Cloud Services ----
            new SampleEntry("Cloud Services", "Geocoding", "Geocoding.xaml.cs",
                "Type an address to see the matches listed and drawn on the map, or click the map to list the addresses and places around that point",
                "GeocodingCloudClient", SampleDraws.Style, typeof(Samples.Geocoding), () => new Samples.Geocoding(),
                aliases: "Reverse Geocoding, ReverseGeocoding, ReverseGeocodingCloudClient, SearchPointAsync, CloudGeocodingOptions, CloudReverseGeocodingOptions, address search, geocode, reverse geocode"),
            new SampleEntry("Cloud Services", "Routing", "Routing.xaml.cs",
                "Driving directions through five stops, in the order given or in the shortest order, or the areas you can reach in 15 to 60 minutes from a point you click",
                "RoutingCloudClient", SampleDraws.Style, typeof(Samples.Routing), () => new Samples.Routing(),
                aliases: "Routing - Service Area, RoutingServiceArea, GetServiceAreaAsync, service area, drive time, isochrone, Routing - Traveling Sales Person, RoutingTravelingSalesPerson, Traveling Salesperson, traveling salesman, TSP, GetOptimizedRouteAsync, optimized route, GetRouteAsync, turn-by-turn, directions"),
            new SampleEntry("Cloud Services", "Elevation", "Elevation.xaml.cs",
                "Draw a point, a line or an area and see the elevation along it, with the average, highest and lowest",
                "ElevationCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.Elevation), () => new Samples.Elevation(),
                aliases: "GetElevationOfLineAsync, GetElevationOfAreaAsync, GetElevationOfPointAsync, height, elevation profile"),
            new SampleEntry("Cloud Services", "Time Zone", "Timezone.xaml.cs",
                "Click anywhere to see its time zone drawn on the map, with the local time and whether daylight saving time is on",
                "TimeZoneCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.Timezone), () => new Samples.Timezone(),
                aliases: "Timezone, GetTimeZoneByCoordinateAsync, local time"),
            new SampleEntry("Cloud Services", "World Maps Query", "WorldMapsQuery.xaml.cs",
                "Draw a point, a line or an area and see the buildings, roads, places or other World Maps features it contains, is nearest to or intersects",
                "MapsQueryCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.WorldMapsQuery), () => new Samples.WorldMapsQuery(),
                aliases: "GetFeaturesIntersectingAsync, GetFeaturesNearestAsync, GetFeaturesWithinAsync, GetFeaturesContainingAsync, spatial query, World Maps"),
            new SampleEntry("Cloud Services", "Color Utilities", "ColorUtilities.xaml.cs",
                "Frisco's census blocks shaded by housing units in a family of colors - hue, triad, analogous and more - generated by the ThinkGeo Cloud",
                "ColorCloudClient", SampleDraws.StyleAndOverlay, typeof(Samples.ColorUtilities), () => new Samples.ColorUtilities(),
                aliases: "GetColorsInHueFamilyAsync, GetColorsInTriadFamilyAsync, color scheme, palette, color family, ClassBreakStyle"),

            // ---- Printing & Export ----
            new SampleEntry("Printing & Export", "Print the Map", "PrintTheMap.xaml.cs",
                "A page of map, legend, scale bar, title and table you can rearrange on screen, then print, save as a PDF, or save as a five-page map book; the map can come from feature layers or from a style document",
                "PrinterLayoutDocument", SampleDraws.StyleAndOverlay, typeof(Samples.PrintTheMap), () => new Samples.PrintTheMap(),
                aliases: "PrintDialog, PrinterGeoCanvas, ExportPdfAsync, PDF, multi-page PDF, map book, atlas, MapPrinterLayoutAsyncLayer, MapStyleAsyncLayer, print style.json, LegendPrinterLayoutAsyncLayer, ScaleBarPrinterLayoutAsyncLayer, DataGridPrinterLayoutAsyncLayer, PrinterInteractiveOverlay"),
            new SampleEntry("Printing & Export", "Export an Image", "ExportAnImage.xaml.cs",
                "Save the map as a PNG - a snapshot of what is on screen, or layers drawn off screen into an image - with a world file if you want it placed in a GIS",
                "GetSnapshot", SampleDraws.StyleAndOverlay, typeof(Samples.ExportAnImage), () => new Samples.ExportAnImage(),
                aliases: "Get Map SnapShot, GetMapSnapShot, snapshot, screenshot, Draw the map on an Image, DrawTheMapOnAnImage, GeoCanvas, GeoImage, BeginDrawing, off-screen rendering, ThinkGeoRasterMapsAsyncLayer, world file, pgw, PNG"),

            // ---- Extending the SDK ----
            new SampleEntry("Extending the SDK", "Custom Feature Source", "CustomFeatureSources.xaml.cs",
                "Points read from a CSV file by a FeatureSource subclass, drawn by the style document like any built-in source",
                "GetAllFeaturesCore", SampleDraws.Style, typeof(Samples.CustomFeatureSources), () => new Samples.CustomFeatureSources(),
                aliases: "Custom Feature Sources, SimpleCsvFeatureSource, FeatureSource, FeatureSourceVectorTileSource, CSV, custom data source"),
            // Deciding what happens when a tile fails is a hook into the SDK's own
            // loop, the same kind of thing as the custom source and layer around it.
            new SampleEntry("Extending the SDK", "Handle Tile Failures", "HandleTileFailures.xaml.cs",
                "An aerial layer whose key is wrong fails every tile; TileLoadFailed reports each failure, raster and vector tiles alike (e.Kind), and the handler chooses what is drawn in its place and whether it is asked for again",
                "TileLoadFailed", SampleDraws.Style, typeof(Samples.HandleTileFailures), () => new Samples.HandleTileFailures(),
                aliases: "Handle Exceptions, HandleExceptions, TileLoadFailedEventArgs, TileLoadKind, DrawException, DrawExceptionCore, tile error, retry, blank tiles"),
            new SampleEntry("Extending the SDK", "Custom Adornment Layer", "CustomAdornmentLayer.xaml.cs",
                "Range rings around the center of the view, drawn over the map by an AdornmentLayer subclass that paints straight onto the canvas",
                "AdornmentLayer", SampleDraws.StyleAndOverlay, typeof(Samples.CustomAdornmentLayer), () => new Samples.CustomAdornmentLayer(),
                aliases: "DrawCore, RangeRingsAdornmentLayer, RadiusLayer, Custom Feature Layer, CustomFeatureLayer, custom layer, range rings"),
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
            typeof(Samples.AnyProjection),
            typeof(Samples.TheGlobe),
            typeof(Samples.AdvancedLabels),
            typeof(Samples.LiveFlights),
            typeof(Samples.NdfdWindField),
            typeof(Samples.NdfdSkyGridAnimation),
        };

        /// <summary>The menu groups, in menu order.</summary>
        internal static IReadOnlyList<string> Categories =>
            All.Select(e => e.Category).Distinct(StringComparer.Ordinal).ToList();

        /// <summary>
        /// Where each chapter of the menu begins: the category that opens it,
        /// and the label the sidebar prints above that group. The stations of
        /// the journey - a programmer builds an application in roughly this
        /// order, and the groups in one flat column read better as a few
        /// spans. A category not named here continues the chapter above it.
        /// </summary>
        internal static readonly IReadOnlyDictionary<string, string> ChapterStarts =
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["Start Here"] = "START HERE",
                ["Maps from a Service - Vector"] = "GET A MAP ON SCREEN",
                ["Styling"] = "MAKE IT LOOK RIGHT",
                ["Map Navigation"] = "INTERACT WITH THE MAP",
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

            // A group moved above the one that opened its chapter takes the label's
            // place without it: the first group has to open a chapter.
            var first = All.Select(e => e.Category).FirstOrDefault();
            if (first != null && !ChapterStarts.ContainsKey(first))
            {
                problems.Add("the first group opens no chapter: " + first);
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
            // The animated icon Markers puts inside one of its markers, not a sample.
            "CustomIcon",
        };
    }
}
