using System;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The vector-tile counterpart of "Azure, Google &amp; Other Providers": Mapbox,
    /// Esri, TomTom, MapTiler, Stadia, AWS and OpenFreeMap all publish MVT tiles
    /// behind a Mapbox-spec style document, so with your own key they load through the
    /// same two objects every vector sample here uses - a style document and a
    /// VectorTileSource. A documentation page rather than a running map: without a key
    /// most of them draw nothing.
    /// </summary>
    public class OtherVectorProviders : MarkdownDocumentViewer
    {
        public OtherVectorProviders()
            : base("OtherVectorProviders.md", PageMarkdown)
        { }
        private const string PageMarkdown = @"# Mapbox, Esri, TomTom and More

The MVT protocol is the protocol - it does not care who runs the server. Any provider
that publishes vector tiles behind a Mapbox-spec style document loads through the same
two objects the other vector samples use: a **style document** and a **VectorTileSource**.
These are hosted tile services: point at the style URL with **your own API key** and
the tiles stream from the provider's servers. (Self-hosted data is covered elsewhere:
a downloaded .mbtiles archive goes straight into `MbTilesVectorTileSource` - see the
Offline Vector Tiles sample.)

| Provider | Style document |
|---|---|
| Mapbox | `api.mapbox.com/styles/v1/…?access_token=` (`mapbox://` refs resolve natively) |
| Esri / ArcGIS | `basemapstyles-api.arcgis.com/…/styles/arcgis/streets?token=` |
| TomTom | `api.tomtom.com/style/1/style/…?key=` |
| MapTiler | `api.maptiler.com/maps/streets-v2/style.json?key=` |
| Stadia Maps | `tiles.stadiamaps.com/styles/alidade_smooth.json?api_key=` |
| AWS Location | `maps.geo.<region>.amazonaws.com/v2/styles/Standard/descriptor?key=` |
| OpenFreeMap | `tiles.openfreemap.org/styles/liberty` (free, no key) |

```csharp
Map.MapUnit = GeographyUnit.Meter;

// Mapbox - mapbox:// source, sprite and glyph references resolve natively;
// the access token on the style URL is carried onto every sub-request
Map.Basemap = new GpuBasemap(new MapStyle().AddStyle(
    ""https://api.mapbox.com/styles/v1/mapbox/streets-v12?access_token=your-mapbox-token""));

// Esri - ArcGIS basemap styles are Mapbox-spec documents over a VectorTileServer
Map.Basemap = new GpuBasemap(new MapStyle().AddStyle(
    ""https://basemapstyles-api.arcgis.com/arcgis/rest/services/styles/v2/styles/arcgis/streets?token=your-arcgis-key""));

// TomTom - key rides the querystring, URLs in the document are absolute
Map.Basemap = new GpuBasemap(new MapStyle().AddStyle(
    ""https://api.tomtom.com/style/1/style/22.2.1-9?map=2/basic_street-light&key=your-tomtom-key""));

// MapTiler
Map.Basemap = new GpuBasemap(new MapStyle().AddStyle(
    ""https://api.maptiler.com/maps/streets-v2/style.json?key=your-maptiler-key""));

// Stadia Maps
Map.Basemap = new GpuBasemap(new MapStyle().AddStyle(
    ""https://tiles.stadiamaps.com/styles/alidade_smooth.json?api_key=your-stadia-key""));

// AWS Location Service
Map.Basemap = new GpuBasemap(new MapStyle().AddStyle(
    ""https://maps.geo.us-east-1.amazonaws.com/v2/styles/Standard/descriptor?key=your-aws-key""));

// OpenFreeMap - free, no key at all
Map.Basemap = new GpuBasemap(new MapStyle().AddStyle(
    ""https://tiles.openfreemap.org/styles/liberty""));

_ = Map.RefreshAsync();
```

No provider-specific class is involved: the style document names its tile source, and
the loader already speaks `mapbox://` URLs (styles, sprites, fonts and tilesets all
convert to their HTTP endpoints, and the style's own query - the token - is appended
to every resource fetched from the same service).

When a provider needs headers or a signed URL instead of a querystring key, handle the
`SendingHttpRequest` event on the `VectorTileSource` itself and rewrite the request -
that is exactly how `ThinkGeoVectorTileSource` injects its own apiKey.
";
    }
}
