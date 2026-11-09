namespace ThinkGeo.Core.Serialization.Dtos;

/// <summary>
/// Describes a single feature. Geometry is persisted as WKT (well-known text) — a stable,
/// schema-less format that round-trips through <c>BaseShape.CreateShapeFromWellKnownData</c>.
/// </summary>
public sealed record FeatureDescriptor(
    string Id,
    string Wkt,
    Dictionary<string, string> ColumnValues);
