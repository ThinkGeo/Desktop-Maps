using System;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// ThinkGeo also supports the other commercial basemap providers - Azure, Bing,
    /// Google, Here, Mapbox - each behind its own overlay/layer pair and each needing
    /// your own API key. A documentation page rather than a running map: without a key
    /// none of them draws, so the page names the classes and shows the one-line usage.
    /// </summary>
    public class ThirdPartyBasemaps : MarkdownDocumentViewer
    {
        public ThirdPartyBasemaps()
            : base("ThirdPartyBasemaps.md", PageMarkdown)
        { }
        private const string PageMarkdown = @"# Azure, Google & Other Map Providers

ThinkGeo also supports the commercial basemap providers below. Each needs **your own
API key** - with a key in hand, putting one on the map is one line.

| Provider | Turn-key overlay | Async layer |
|---|---|---|
| Azure Maps | `AzureMapsRasterOverlay` | `AzureMapsRasterAsyncLayer` |
| Google Maps | `GoogleMapsOverlay` | `GoogleMapsAsyncLayer` |
| Google Maps Static | `GoogleMapsStaticOverlay` | `GoogleMapsStaticAsyncLayer` |
| Google Map Tiles | `GoogleMapTilesOverlay` | `GoogleMapTilesAsyncLayer` |
| Here Maps | `HereMapsRasterTileOverlay` | `HereMapsRasterTileAsyncLayer` |
| Mapbox | `MapBoxStaticTilesOverlay` | `MapBoxStaticTilesAsyncLayer` |

## One-line usage

```csharp
Map.MapUnit = GeographyUnit.Meter;

// Azure Maps
Map.Overlays.Add(new AzureMapsRasterOverlay(""your-azure-maps-key"", AzureMapsRasterTileSet.Imagery));

// Google Maps
Map.Overlays.Add(new GoogleMapsOverlay(""your-google-api-key"", string.Empty));

_ = Map.RefreshAsync();
```

The async layer of any provider can also go inside a `LayerOverlay` when it needs a
tile cache or a projection converter, or feed the basemap the way the XYZ Tile
Server sample shows.
";
    }
}
