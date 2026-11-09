using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="ProjectionConverter"/> and <see cref="ProjectionDescriptor"/>.
/// </summary>
public static class ProjectionMapper
{
    /// <summary>Captures a converter; returns <c>null</c> when the input is null or both sides are empty.</summary>
    public static ProjectionDescriptor? ToDescriptor(ProjectionConverter? converter)
    {
        if (converter == null) return null;

        // Capture SRID xor ProjString, not both. Different converter backends (GDAL vs stock
        // Projection) emit slightly different canonical PROJ strings for the same SRID — keeping
        // both would make the descriptor unstable under round trip (the first capture sees the
        // source backend's string, the second capture sees whatever `new Projection(srid)`
        // generates). SRID alone is the authoritative identity; ProjString is the fallback only
        // when no SRID is available.
        var intSridNullable = converter.InternalProjection?.Srid is int intSrid and > 0 ? (int?)intSrid : null;
        var extSridNullable = converter.ExternalProjection?.Srid is int extSrid and > 0 ? (int?)extSrid : null;

        var descriptor = new ProjectionDescriptor
        {
            InternalSrid = intSridNullable,
            InternalProjString = intSridNullable != null || string.IsNullOrEmpty(converter.InternalProjection?.ProjString)
                ? null : converter.InternalProjection!.ProjString,
            ExternalSrid = extSridNullable,
            ExternalProjString = extSridNullable != null || string.IsNullOrEmpty(converter.ExternalProjection?.ProjString)
                ? null : converter.ExternalProjection!.ProjString,
        };

        // If every side is empty, the converter carries no info worth persisting.
        if (descriptor.InternalSrid == null && descriptor.InternalProjString == null
            && descriptor.ExternalSrid == null && descriptor.ExternalProjString == null)
        {
            return null;
        }

        return descriptor;
    }

    /// <summary>Rebuilds a <see cref="ProjectionConverter"/> from a descriptor. Returns <c>null</c> for a null descriptor.</summary>
    public static ProjectionConverter? FromDescriptor(ProjectionDescriptor? descriptor)
    {
        if (descriptor == null) return null;

        var internalProjection = BuildProjection(descriptor.InternalSrid, descriptor.InternalProjString);
        var externalProjection = BuildProjection(descriptor.ExternalSrid, descriptor.ExternalProjString);

        if (internalProjection == null && externalProjection == null) return null;

        return new ProjectionConverter(
            internalProjection ?? new Projection(),
            externalProjection ?? new Projection());
    }

    private static Projection? BuildProjection(int? srid, string? projString)
    {
        if (srid is int s and > 0) return new Projection(s);
        if (!string.IsNullOrEmpty(projString)) return new Projection(projString!);
        return null;
    }
}
