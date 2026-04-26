using ThinkGeo.Core;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Converts between <see cref="GeoColor"/> and an 8-digit ARGB hex string (e.g. <c>#FF0080FF</c>).
/// <c>GeoColor</c> is a value-type-ish primitive so no DTO — a string is enough.
/// </summary>
public static class ColorMapper
{
    public static string ToHexString(GeoColor color)
    {
        return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    public static GeoColor FromHexString(string? hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
        {
            return new GeoColor(0, 0, 0, 0);
        }

        var span = hex!.AsSpan().TrimStart('#');
        if (span.Length == 8)
        {
            return new GeoColor(
                byte.Parse(span.Slice(0, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(span.Slice(2, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(span.Slice(4, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(span.Slice(6, 2), System.Globalization.NumberStyles.HexNumber));
        }
        if (span.Length == 6)
        {
            return new GeoColor(
                255,
                byte.Parse(span.Slice(0, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(span.Slice(2, 2), System.Globalization.NumberStyles.HexNumber),
                byte.Parse(span.Slice(4, 2), System.Globalization.NumberStyles.HexNumber));
        }

        throw new System.FormatException(
            $"Expected 6- or 8-digit hex color (with optional leading '#'); got '{hex}'.");
    }
}
