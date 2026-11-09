namespace ThinkGeo.Core.Serialization.Dtos;

/// <summary>
/// Describes a <c>ProjectionConverter</c> — the source (internal) → target (external) projection
/// that a layer applies at render time. Each side can be specified as either an EPSG SRID (preferred
/// when available) or a PROJ4 string (for custom / local projections).
/// </summary>
/// <remarks>
/// When both <see cref="InternalSrid"/> and <see cref="InternalProjString"/> are set, the SRID
/// is used (more portable). Same rule for the external side. If both are null for a side, that
/// side is treated as "not configured" and the descriptor is effectively a no-op for that side.
/// </remarks>
public sealed record ProjectionDescriptor
{
    /// <summary>Source projection EPSG SRID (e.g. 4326, 2163).</summary>
    public int? InternalSrid { get; init; }

    /// <summary>Source projection PROJ4 string, used when SRID is not available.</summary>
    public string? InternalProjString { get; init; }

    /// <summary>Target projection EPSG SRID.</summary>
    public int? ExternalSrid { get; init; }

    /// <summary>Target projection PROJ4 string, used when SRID is not available.</summary>
    public string? ExternalProjString { get; init; }
}
