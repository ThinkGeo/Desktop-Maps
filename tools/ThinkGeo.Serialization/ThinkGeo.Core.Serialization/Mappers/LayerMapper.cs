using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="LayerBase"/> instances and <see cref="LayerDescriptor"/> DTOs.
/// Implemented: ShapeFileFeatureLayer, MultipleShapeFileFeatureLayer, InMemoryFeatureLayer,
/// WkbFileFeatureLayer, TabFeatureLayer, TinyGeoFeatureLayer, GpxFeatureLayer,
/// SqliteFeatureLayer, GraticuleFeatureLayer, WmsAsyncLayer, GeoTiffRasterLayer,
/// RasterMbTilesAsyncLayer. Unknown layer types throw <see cref="NotSupportedException"/>.
/// </summary>
public static class LayerMapper
{
    /// <summary>
    /// Produces a descriptor for a known layer type; returns <c>null</c> for unknown types so
    /// callers iterating a layer collection can silently skip unsupported layers.
    /// </summary>
    public static LayerDescriptor? ToDescriptor(LayerBase layer)
    {
        if (layer == null) throw new ArgumentNullException(nameof(layer));

        LayerDescriptor? descriptor = layer switch
        {
            ShapeFileFeatureLayer sf => new ShapeFileLayerDescriptor
            {
                ShapePath = sf.ShapePathFilename ?? string.Empty,
                ReadWriteMode = sf.ReadWriteMode.ToString(),
            },
            MultipleShapeFileFeatureLayer msf => new MultipleShapeFileLayerDescriptor
            {
                Pattern = string.IsNullOrEmpty(msf.MultipleShapeFilePattern) ? null : msf.MultipleShapeFilePattern,
                IndexPattern = string.IsNullOrEmpty(msf.IndexFilePattern) ? null : msf.IndexFilePattern,
                ShapeFiles = msf.ShapeFiles.ToList(),
                Indexes = msf.Indexes.ToList(),
            },
            InMemoryFeatureLayer im => CaptureInMemoryLayer(im),
            WkbFileFeatureLayer wkb => new WkbFileLayerDescriptor
            {
                WkbPath = wkb.WkbPathFilename ?? string.Empty,
                ReadWriteMode = wkb.ReadWriteMode.ToString(),
            },
            TabFeatureLayer tab => new TabFileLayerDescriptor
            {
                TabPath = tab.TabPathFilename ?? string.Empty,
                ReadWriteMode = tab.ReadWriteMode.ToString(),
                EncodingName = tab.Encoding?.WebName,
                StylingType = tab.StylingType.ToString(),
            },
            TinyGeoFeatureLayer ting => new TinyGeoFileLayerDescriptor
            {
                TinyGeoPath = ting.TinyGeoPathFilename ?? string.Empty,
                Password = ting.Password,
            },
            GpxFeatureLayer gpx => new GpxFileLayerDescriptor
            {
                GpxPath = gpx.GpxPathFilename ?? string.Empty,
            },
            SqliteFeatureLayer sq => new SqliteFeatureLayerDescriptor
            {
                ConnectionString = sq.ConnectionString ?? string.Empty,
                TableName = sq.TableName ?? string.Empty,
                FeatureIdColumn = sq.FeatureIdColumn ?? string.Empty,
                GeometryColumnName = sq.GeometryColumnName ?? string.Empty,
                WhereClause = string.IsNullOrEmpty(sq.WhereClause) ? null : sq.WhereClause,
                CommandTimeoutSeconds = sq.CommandTimeout,
            },
            GraticuleFeatureLayer grat => new GraticuleFeatureLayerDescriptor
            {
                // The internal density field isn't exposed; default the snapshot to 3 and callers
                // can override by resetting the descriptor before applying.
                GraticuleDensity = 3,
            },
            WmsAsyncLayer wms => CaptureWmsLayer(wms),
            GeoTiffRasterLayer tif => CaptureGeoTiffLayer(tif),
            RasterMbTilesAsyncLayer mb => new RasterMbTilesLayerDescriptor
            {
                FilePath = mb.FilePath ?? string.Empty,
            },
            _ => null,
        };

        if (descriptor == null) return null;

        return descriptor with
        {
            Name = layer.Name ?? string.Empty,
            IsVisible = layer.IsVisible,
            ZoomLevelSet = TryCaptureZoomLevelSet(layer),
            Projection = TryCaptureProjection(layer),
        };
    }

    /// <summary>Alias for <see cref="ToDescriptor"/> retained for API symmetry; both now return null on unknown types.</summary>
    public static LayerDescriptor? TryToDescriptor(LayerBase layer) => ToDescriptor(layer);

    public static LayerBase FromDescriptor(LayerDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        LayerBase layer = descriptor switch
        {
            ShapeFileLayerDescriptor s => BuildShapeFileLayer(s),
            MultipleShapeFileLayerDescriptor msf => BuildMultipleShapeFileLayer(msf),
            InMemoryFeatureLayerDescriptor im => BuildInMemoryFeatureLayer(im),
            WkbFileLayerDescriptor wkb => new WkbFileFeatureLayer(wkb.WkbPath,
                Enum.TryParse<FileAccess>(wkb.ReadWriteMode, ignoreCase: true, out var wm) ? wm : FileAccess.Read),
            TabFileLayerDescriptor tab => BuildTabLayer(tab),
            TinyGeoFileLayerDescriptor ting => string.IsNullOrEmpty(ting.Password)
                ? new TinyGeoFeatureLayer(ting.TinyGeoPath)
                : new TinyGeoFeatureLayer(ting.TinyGeoPath, ting.Password!),
            GpxFileLayerDescriptor gpx => new GpxFeatureLayer(gpx.GpxPath),
            SqliteFeatureLayerDescriptor sq => BuildSqliteLayer(sq),
            GraticuleFeatureLayerDescriptor grat => new GraticuleFeatureLayer(grat.GraticuleDensity),
            WmsLayerDescriptor wms => BuildWmsLayer(wms),
            GeoTiffRasterLayerDescriptor tif => BuildGeoTiffLayer(tif),
            RasterMbTilesLayerDescriptor mb => new RasterMbTilesAsyncLayer(mb.FilePath),
            _ => throw new NotSupportedException(
                $"Layer descriptor '{descriptor.GetType().FullName}' is not supported yet."),
        };

        layer.Name = descriptor.Name;
        layer.IsVisible = descriptor.IsVisible;
        TryApplyZoomLevelSet(layer, descriptor.ZoomLevelSet);
        TryApplyProjection(layer, descriptor.Projection);
        return layer;
    }

    private static InMemoryFeatureLayerDescriptor CaptureInMemoryLayer(InMemoryFeatureLayer layer)
    {
        // Columns and InternalFeatures require the feature source to be open. Open idempotently
        // so callers don't have to remember — mirrors what the real drawing pipeline does anyway.
        if (!layer.IsOpen) layer.Open();

        var columns = layer.GetColumns()
            .Select(c => new FeatureColumnDescriptor(c.ColumnName ?? string.Empty, c.TypeName ?? string.Empty, c.MaxLength))
            .ToList();

        return new InMemoryFeatureLayerDescriptor
        {
            Features = FeatureMapper.ToDescriptors(layer.InternalFeatures).ToList(),
            Columns = columns,
        };
    }

    private static ShapeFileFeatureLayer BuildShapeFileLayer(ShapeFileLayerDescriptor descriptor)
    {
        var access = Enum.TryParse<FileAccess>(descriptor.ReadWriteMode, ignoreCase: true, out var m)
            ? m
            : FileAccess.Read;
        return new ShapeFileFeatureLayer(descriptor.ShapePath, null, access);
    }

    private static MultipleShapeFileFeatureLayer BuildMultipleShapeFileLayer(MultipleShapeFileLayerDescriptor descriptor)
    {
        if (descriptor.ShapeFiles.Count > 0)
        {
            if (descriptor.Indexes.Count > 0)
            {
                return new MultipleShapeFileFeatureLayer(descriptor.ShapeFiles, descriptor.Indexes);
            }
            return new MultipleShapeFileFeatureLayer(descriptor.ShapeFiles);
        }
        if (!string.IsNullOrEmpty(descriptor.Pattern))
        {
            return !string.IsNullOrEmpty(descriptor.IndexPattern)
                ? new MultipleShapeFileFeatureLayer(descriptor.Pattern!, descriptor.IndexPattern!)
                : new MultipleShapeFileFeatureLayer(descriptor.Pattern!);
        }
        return new MultipleShapeFileFeatureLayer();
    }

    private static TabFeatureLayer BuildTabLayer(TabFileLayerDescriptor descriptor)
    {
        var access = Enum.TryParse<FileAccess>(descriptor.ReadWriteMode, ignoreCase: true, out var m)
            ? m
            : FileAccess.Read;
        var layer = new TabFeatureLayer(descriptor.TabPath, access);
        if (!string.IsNullOrEmpty(descriptor.EncodingName))
        {
            try { layer.Encoding = Encoding.GetEncoding(descriptor.EncodingName!); } catch { /* best-effort */ }
        }
        if (Enum.TryParse<TabStylingType>(descriptor.StylingType, ignoreCase: true, out var styling))
        {
            layer.StylingType = styling;
        }
        return layer;
    }

    private static SqliteFeatureLayer BuildSqliteLayer(SqliteFeatureLayerDescriptor descriptor)
    {
        var layer = new SqliteFeatureLayer(
            descriptor.ConnectionString,
            descriptor.TableName,
            descriptor.FeatureIdColumn,
            descriptor.GeometryColumnName);
        if (!string.IsNullOrEmpty(descriptor.WhereClause)) layer.WhereClause = descriptor.WhereClause!;
        layer.CommandTimeout = descriptor.CommandTimeoutSeconds;
        return layer;
    }

    private static WmsLayerDescriptor CaptureWmsLayer(WmsAsyncLayer layer)
    {
        return new WmsLayerDescriptor
        {
            ServerUri = layer.Uri?.ToString() ?? string.Empty,
            ActiveLayerNames = layer.ActiveLayerNames?.ToList() ?? new List<string>(),
            ActiveStyleNames = layer.ActiveStyleNames?.ToList() ?? new List<string>(),
            OutputFormat = layer.OutputFormat ?? "image/png",
            Version = layer.Version ?? "1.3.0",
            Crs = layer.Crs,
            IsTransparent = layer.IsTransparent,
            Parameters = layer.Parameters != null
                ? new Dictionary<string, string>(layer.Parameters)
                : new Dictionary<string, string>(),
        };
    }

    private static WmsAsyncLayer BuildWmsLayer(WmsLayerDescriptor descriptor)
    {
        var uri = string.IsNullOrEmpty(descriptor.ServerUri)
            ? null
            : new Uri(descriptor.ServerUri, UriKind.Absolute);

        var layer = uri != null ? new WmsAsyncLayer(uri) : new WmsAsyncLayer();
        foreach (var name in descriptor.ActiveLayerNames) layer.ActiveLayerNames.Add(name);
        foreach (var name in descriptor.ActiveStyleNames) layer.ActiveStyleNames.Add(name);
        if (!string.IsNullOrEmpty(descriptor.OutputFormat)) layer.OutputFormat = descriptor.OutputFormat;
        if (!string.IsNullOrEmpty(descriptor.Crs)) layer.Crs = descriptor.Crs;
        layer.IsTransparent = descriptor.IsTransparent;
        foreach (var kv in descriptor.Parameters) layer.Parameters[kv.Key] = kv.Value;
        return layer;
    }

    private static GeoTiffRasterLayerDescriptor CaptureGeoTiffLayer(GeoTiffRasterLayer layer)
    {
        return new GeoTiffRasterLayerDescriptor
        {
            TiffPath = layer.ImagePathFilename ?? string.Empty,
            ResamplingMode = layer.ResamplingMode.ToString(),
        };
    }

    private static GeoTiffRasterLayer BuildGeoTiffLayer(GeoTiffRasterLayerDescriptor descriptor)
    {
        var layer = !string.IsNullOrEmpty(descriptor.WorldFilePath)
            ? new GeoTiffRasterLayer(descriptor.TiffPath, descriptor.WorldFilePath)
            : new GeoTiffRasterLayer(descriptor.TiffPath);

        if (Enum.TryParse<RasterResamplingMode>(descriptor.ResamplingMode, ignoreCase: true, out var mode))
        {
            layer.ResamplingMode = mode;
        }
        return layer;
    }

    private static InMemoryFeatureLayer BuildInMemoryFeatureLayer(InMemoryFeatureLayerDescriptor descriptor)
    {
        var columns = descriptor.Columns
            .Select(c => new FeatureSourceColumn(c.Name, c.Type, c.Length))
            .ToList();
        var features = FeatureMapper.FromDescriptors(descriptor.Features).ToList();
        var layer = new InMemoryFeatureLayer(columns, features);
        // Open so the caller can immediately read Columns / InternalFeatures without an
        // InvalidOperationException. Real rendering pipelines re-open as needed.
        layer.Open();
        return layer;
    }

    private static ProjectionDescriptor? TryCaptureProjection(LayerBase layer)
    {
        // ProjectionConverter is attached differently depending on the layer kind:
        //   FeatureLayer  → FeatureSource.ProjectionConverter
        //   RasterLayer   → ImageSource.ProjectionConverter
        //   WmsAsyncLayer / RasterXyzTileAsyncLayer → layer.ProjectionConverter (direct)
        try
        {
            var converter = layer switch
            {
                FeatureLayer fl => fl.FeatureSource?.ProjectionConverter,
                RasterLayer rl => rl.ImageSource?.ProjectionConverter,
                WmsAsyncLayer wms => wms.ProjectionConverter,
                RasterXyzTileAsyncLayer xyz => xyz.ProjectionConverter,
                _ => null,
            };
            return ProjectionMapper.ToDescriptor(converter);
        }
        catch { return null; /* best-effort */ }
    }

    private static void TryApplyProjection(LayerBase layer, ProjectionDescriptor? descriptor)
    {
        if (descriptor == null) return;
        try
        {
            var converter = ProjectionMapper.FromDescriptor(descriptor);
            if (converter == null) return;

            switch (layer)
            {
                case FeatureLayer fl when fl.FeatureSource != null:
                    fl.FeatureSource.ProjectionConverter = converter;
                    break;
                case RasterLayer rl when rl.ImageSource != null:
                    rl.ImageSource.ProjectionConverter = converter;
                    break;
                case WmsAsyncLayer wms:
                    wms.ProjectionConverter = converter;
                    break;
                case RasterXyzTileAsyncLayer xyz:
                    xyz.ProjectionConverter = converter;
                    break;
            }
        }
        catch { /* best-effort */ }
    }

    private static ZoomLevelSetDescriptor? TryCaptureZoomLevelSet(LayerBase layer)
    {
        // FeatureLayer-derived types have ZoomLevelSet but accessing it before Open() can be
        // awkward for some layers. Bracket with try/catch to keep this best-effort.
        try
        {
            if (layer is FeatureLayer fl && fl.ZoomLevelSet != null)
            {
                return ZoomLevelMapper.ToDescriptor(fl.ZoomLevelSet);
            }
        }
        catch { /* best-effort */ }
        return null;
    }

    private static void TryApplyZoomLevelSet(LayerBase layer, ZoomLevelSetDescriptor? descriptor)
    {
        if (descriptor == null) return;
        try
        {
            if (layer is FeatureLayer fl)
            {
                fl.ZoomLevelSet = ZoomLevelMapper.FromDescriptor(descriptor);
            }
        }
        catch { /* best-effort */ }
    }
}
