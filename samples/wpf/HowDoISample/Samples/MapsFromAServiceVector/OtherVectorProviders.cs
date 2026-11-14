using System;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The keyed vector-tile providers - Mapbox, Esri, TomTom, MapTiler, Stadia, AWS - as a
    /// page of their style URLs. A page rather than a map: without your key most of them
    /// draw nothing.
    /// </summary>
    public class OtherVectorProviders : MarkdownDocumentViewer
    {
        public OtherVectorProviders()
            : base("OtherVectorProviders.md", PageMarkdown)
        { }
        private const string PageMarkdown = @"# Mapbox, Esri, TomTom and More

Any provider that publishes vector tiles behind a Mapbox-spec style document loads the
way OpenFreeMap does in this sample: its style URL, handed to a `MapStyle`. These are
hosted tile services - put your own API key on the style URL and the tiles stream from
the provider's servers. A downloaded .mbtiles or .pmtiles archive is not a service: see
the Offline Tiles sample.

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
Map.Basemap = new GpuBasemap(new MapStyle(
    ""https://api.mapbox.com/styles/v1/mapbox/streets-v12?access_token=your-mapbox-token""));

// Esri - ArcGIS basemap styles are Mapbox-spec documents over a VectorTileServer
Map.Basemap = new GpuBasemap(new MapStyle(
    ""https://basemapstyles-api.arcgis.com/arcgis/rest/services/styles/v2/styles/arcgis/streets?token=your-arcgis-key""));

// TomTom - key rides the querystring, URLs in the document are absolute
Map.Basemap = new GpuBasemap(new MapStyle(
    ""https://api.tomtom.com/style/1/style/22.2.1-9?map=2/basic_street-light&key=your-tomtom-key""));

// MapTiler
Map.Basemap = new GpuBasemap(new MapStyle(
    ""https://api.maptiler.com/maps/streets-v2/style.json?key=your-maptiler-key""));

// Stadia Maps
Map.Basemap = new GpuBasemap(new MapStyle(
    ""https://tiles.stadiamaps.com/styles/alidade_smooth.json?api_key=your-stadia-key""));

// AWS Location Service
Map.Basemap = new GpuBasemap(new MapStyle(
    ""https://maps.geo.us-east-1.amazonaws.com/v2/styles/Standard/descriptor?key=your-aws-key""));

// OpenFreeMap - free, no key at all
Map.Basemap = new GpuBasemap(new MapStyle(
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
