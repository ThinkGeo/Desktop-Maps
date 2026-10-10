using System.Collections.Generic;
using ThinkGeo.Core;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A DynamicSource keeps each feature under an id and draws it one way per kind -
    /// AddPoint, AddLine, AddArea. Several samples just want "show these shapes, whatever
    /// they are, in this colour", so this is that in one call: clear the source, then add
    /// each shape by its kind. One Clear and a run of Adds land as one rebuild.
    /// </summary>
    internal static class DynamicSourceExtensions
    {
        /// <summary>
        /// Replaces everything in the source with these shapes: points as markers, lines as
        /// ribbons, areas as fills, each under an id made from <paramref name="idPrefix"/>.
        /// </summary>
        /// <param name="color">Marker tint, line colour and area fill; null takes the source's defaults.</param>
        /// <param name="outlineColor">Area outline; null draws none.</param>
        /// <param name="widthInPixels">Line width, and area outline width; null draws the defaults.</param>
        /// <param name="pointSizeInPixels">Marker length on screen.</param>
        /// <param name="iconName">The marker image, registered in <c>MapStyle.Images</c>; null draws the built-in triangle.</param>
        public static void Show(this DynamicSource source, IEnumerable<BaseShape> shapes, GeoColor color = null,
            GeoColor outlineColor = null, float? widthInPixels = null, float pointSizeInPixels = 14f, string iconName = null,
            string idPrefix = "shape")
        {
            source.Clear();
            var index = 0;
            foreach (var shape in shapes)
            {
                var id = idPrefix + "-" + index++;
                var feature = new Feature(shape);
                switch (shape)
                {
                    case PointBaseShape _:
                        source.AddPoint(id, feature, color: color, sizeInPixels: pointSizeInPixels, iconName: iconName);
                        break;
                    case LineBaseShape _:
                        source.AddLine(id, feature, color, widthInPixels);
                        break;
                    case AreaBaseShape _:
                        source.AddArea(id, feature, color, outlineColor, widthInPixels);
                        break;
                }
            }
        }

        /// <summary>The same for features: the shape of each is drawn by its kind.</summary>
        public static void Show(this DynamicSource source, IEnumerable<Feature> features, GeoColor color = null,
            GeoColor outlineColor = null, float? widthInPixels = null, float pointSizeInPixels = 14f, string iconName = null,
            string idPrefix = "feature")
        {
            source.Clear();
            var index = 0;
            foreach (var feature in features)
            {
                var id = idPrefix + "-" + index++;
                switch (feature.GetShape())
                {
                    case PointBaseShape _:
                        source.AddPoint(id, feature, color: color, sizeInPixels: pointSizeInPixels, iconName: iconName);
                        break;
                    case LineBaseShape _:
                        source.AddLine(id, feature, color, widthInPixels);
                        break;
                    case AreaBaseShape _:
                        source.AddArea(id, feature, color, outlineColor, widthInPixels);
                        break;
                }
            }
        }
    }
}
