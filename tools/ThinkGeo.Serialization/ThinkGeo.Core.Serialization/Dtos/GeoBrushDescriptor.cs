using System.Text.Json.Serialization;

namespace ThinkGeo.Core.Serialization.Dtos;

/// <summary>
/// Base descriptor for any <c>GeoBrush</c>. Polymorphic JSON via <c>kind</c> discriminator.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(GeoSolidBrushDescriptor), "solid")]
[JsonDerivedType(typeof(GeoLinearGradientBrushDescriptor), "linearGradient")]
[JsonDerivedType(typeof(GeoHatchBrushDescriptor), "hatch")]
[JsonDerivedType(typeof(GeoTextureBrushDescriptor), "texture")]
public abstract record GeoBrushDescriptor;

/// <summary>Solid colour fill.</summary>
public sealed record GeoSolidBrushDescriptor(string Color) : GeoBrushDescriptor;

/// <summary>Linear gradient fill between two colours along an angle.</summary>
public sealed record GeoLinearGradientBrushDescriptor(
    string StartColor,
    string EndColor,
    float DirectionAngle) : GeoBrushDescriptor;

/// <summary>Hatch pattern fill — foreground + background + hatch style.</summary>
public sealed record GeoHatchBrushDescriptor(
    string ForegroundColor,
    string BackgroundColor,
    string HatchStyle) : GeoBrushDescriptor;

/// <summary>Texture brush — references an image on disk or embedded resource.</summary>
public sealed record GeoTextureBrushDescriptor(string ImagePath) : GeoBrushDescriptor;
