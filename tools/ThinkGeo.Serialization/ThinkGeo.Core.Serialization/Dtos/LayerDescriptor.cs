using System.Text.Json.Serialization;

namespace ThinkGeo.Core.Serialization.Dtos;

/// <summary>
/// Base descriptor for any ThinkGeo.Core <c>Layer</c>. Polymorphic JSON via <c>kind</c>.
/// Add a new <see cref="JsonDerivedTypeAttribute"/> here (and a corresponding subclass file
/// under <c>Dtos/Layers/</c>) when adding support for another layer type.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(ShapeFileLayerDescriptor), "shapefile")]
[JsonDerivedType(typeof(MultipleShapeFileLayerDescriptor), "multipleShapefile")]
[JsonDerivedType(typeof(InMemoryFeatureLayerDescriptor), "inmemoryFeature")]
[JsonDerivedType(typeof(WkbFileLayerDescriptor), "wkbFile")]
[JsonDerivedType(typeof(TabFileLayerDescriptor), "tabFile")]
[JsonDerivedType(typeof(TinyGeoFileLayerDescriptor), "tinyGeoFile")]
[JsonDerivedType(typeof(GpxFileLayerDescriptor), "gpxFile")]
[JsonDerivedType(typeof(SqliteFeatureLayerDescriptor), "sqliteFeature")]
[JsonDerivedType(typeof(GraticuleFeatureLayerDescriptor), "graticule")]
[JsonDerivedType(typeof(WmsLayerDescriptor), "wms")]
[JsonDerivedType(typeof(GeoTiffRasterLayerDescriptor), "geotiff")]
[JsonDerivedType(typeof(RasterMbTilesLayerDescriptor), "mbtiles")]
public abstract record LayerDescriptor
{
    public string Name { get; init; } = "";
    public bool IsVisible { get; init; } = true;

    /// <summary>Optional zoom-level ladder. Null means "use layer default".</summary>
    public ZoomLevelSetDescriptor? ZoomLevelSet { get; init; }

    /// <summary>
    /// Optional source → target projection. Null means "no projection" (layer renders in its
    /// native SRID). Critical for any multi-CRS map: without this, layers lose their source
    /// coordinate system on round-trip and will render in the wrong geographic location.
    /// </summary>
    public ProjectionDescriptor? Projection { get; init; }
}

/// <summary>Shapefile-backed feature layer — just the path + styling.</summary>
public sealed record ShapeFileLayerDescriptor : LayerDescriptor
{
    public string ShapePath { get; init; } = "";
    /// <summary>Optional alternate encoding name (e.g. <c>"utf-8"</c>).</summary>
    public string? Encoding { get; init; }
    /// <summary>Optional read/write mode: <c>"ReadOnly" | "ReadWrite"</c>.</summary>
    public string ReadWriteMode { get; init; } = "ReadOnly";
}

/// <summary>InMemory feature layer — carries all in-memory features inline.</summary>
public sealed record InMemoryFeatureLayerDescriptor : LayerDescriptor
{
    public List<FeatureDescriptor> Features { get; init; } = new();
    /// <summary>Optional feature-source column declarations (name + type: <c>"String"|"Integer"|"Double"|...</c>).</summary>
    public List<FeatureColumnDescriptor> Columns { get; init; } = new();
}

public sealed record FeatureColumnDescriptor(string Name, string Type, int Length);

/// <summary>
/// WMS layer — server URL, active layers, style / format / CRS overrides, and extra query parameters.
/// Maps to <c>ThinkGeo.Core.WmsAsyncLayer</c> (the current WMS implementation).
/// </summary>
public sealed record WmsLayerDescriptor : LayerDescriptor
{
    public string ServerUri { get; init; } = "";
    public List<string> ActiveLayerNames { get; init; } = new();
    public List<string> ActiveStyleNames { get; init; } = new();
    public string OutputFormat { get; init; } = "image/png";
    public string Version { get; init; } = "1.3.0";
    public string? Crs { get; init; }
    public bool IsTransparent { get; init; } = true;
    public Dictionary<string, string> Parameters { get; init; } = new();
}

/// <summary>
/// GeoTIFF raster layer — path, optional world-file path, and the resampling mode used when drawing.
/// Maps to <c>ThinkGeo.Core.GeoTiffRasterLayer</c>.
/// </summary>
public sealed record GeoTiffRasterLayerDescriptor : LayerDescriptor
{
    public string TiffPath { get; init; } = "";
    public string? WorldFilePath { get; init; }
    /// <summary><c>"NearestNeighbor" | "Bilinear" | "Cubic" | "Gaussian"</c>.</summary>
    public string ResamplingMode { get; init; } = "NearestNeighbor";
}

/// <summary>
/// A layer that aggregates many shapefiles behind one logical surface.
/// Maps to <c>ThinkGeo.Core.MultipleShapeFileFeatureLayer</c>.
/// </summary>
public sealed record MultipleShapeFileLayerDescriptor : LayerDescriptor
{
    /// <summary>Use either <see cref="Pattern"/> (glob/path template) or <see cref="ShapeFiles"/> (explicit list).</summary>
    public string? Pattern { get; init; }
    public string? IndexPattern { get; init; }
    public List<string> ShapeFiles { get; init; } = new();
    public List<string> Indexes { get; init; } = new();
}

/// <summary>
/// WKB-backed feature layer — path + read/write mode.
/// Maps to <c>ThinkGeo.Core.WkbFileFeatureLayer</c>.
/// </summary>
public sealed record WkbFileLayerDescriptor : LayerDescriptor
{
    public string WkbPath { get; init; } = "";
    public string ReadWriteMode { get; init; } = "Read";
}

/// <summary>
/// MapInfo TAB file — path, read/write mode, optional encoding / styling-type override.
/// Maps to <c>ThinkGeo.Core.TabFeatureLayer</c>.
/// </summary>
public sealed record TabFileLayerDescriptor : LayerDescriptor
{
    public string TabPath { get; init; } = "";
    public string ReadWriteMode { get; init; } = "Read";
    public string? EncodingName { get; init; }
    public string? StylingType { get; init; }
}

/// <summary>
/// TinyGeo binary format — path + optional password.
/// Maps to <c>ThinkGeo.Core.TinyGeoFeatureLayer</c>.
/// </summary>
public sealed record TinyGeoFileLayerDescriptor : LayerDescriptor
{
    public string TinyGeoPath { get; init; } = "";
    public string? Password { get; init; }
}

/// <summary>
/// GPX GPS track file.
/// Maps to <c>ThinkGeo.Core.GpxFeatureLayer</c>.
/// </summary>
public sealed record GpxFileLayerDescriptor : LayerDescriptor
{
    public string GpxPath { get; init; } = "";
}

/// <summary>
/// SQLite-backed spatial feature layer — connection string + table + id/geometry columns + optional where clause.
/// Maps to <c>ThinkGeo.Core.SqliteFeatureLayer</c>.
/// </summary>
public sealed record SqliteFeatureLayerDescriptor : LayerDescriptor
{
    public string ConnectionString { get; init; } = "";
    public string TableName { get; init; } = "";
    public string FeatureIdColumn { get; init; } = "";
    public string GeometryColumnName { get; init; } = "";
    public string? WhereClause { get; init; }
    public int CommandTimeoutSeconds { get; init; } = 30;
}

/// <summary>
/// MBTiles raster tile source (SQLite-backed).
/// Maps to <c>ThinkGeo.Core.RasterMbTilesAsyncLayer</c>.
/// </summary>
public sealed record RasterMbTilesLayerDescriptor : LayerDescriptor
{
    public string FilePath { get; init; } = "";
}

/// <summary>
/// Procedural graticule (lat/lon grid) — no source data, only rendering parameters.
/// Maps to <c>ThinkGeo.Core.GraticuleFeatureLayer</c>.
/// </summary>
public sealed record GraticuleFeatureLayerDescriptor : LayerDescriptor
{
    public int GraticuleDensity { get; init; } = 3;
}
