using System;
using System.Collections.Generic;
using System.Linq;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="Feature"/> instances and <see cref="FeatureDescriptor"/>.
/// Geometry is routed through WKT (well-known text) — a schema-stable text representation
/// that survives SDK major-version changes.
/// </summary>
public static class FeatureMapper
{
    public static FeatureDescriptor ToDescriptor(Feature feature)
    {
        return new FeatureDescriptor(
            Id: feature.Id ?? string.Empty,
            Wkt: feature.GetWellKnownText() ?? string.Empty,
            ColumnValues: new Dictionary<string, string>(feature.ColumnValues));
    }

    public static Feature FromDescriptor(FeatureDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        var shape = BaseShape.CreateShapeFromWellKnownData(descriptor.Wkt);
        var feature = new Feature(shape, descriptor.ColumnValues ?? new Dictionary<string, string>())
        {
            Id = descriptor.Id ?? string.Empty,
        };
        return feature;
    }

    public static IReadOnlyList<FeatureDescriptor> ToDescriptors(IEnumerable<Feature> features)
    {
        if (features == null) throw new ArgumentNullException(nameof(features));
        return features.Select(ToDescriptor).ToList();
    }

    public static IReadOnlyList<Feature> FromDescriptors(IEnumerable<FeatureDescriptor> descriptors)
    {
        if (descriptors == null) throw new ArgumentNullException(nameof(descriptors));
        return descriptors.Select(FromDescriptor).ToList();
    }
}
