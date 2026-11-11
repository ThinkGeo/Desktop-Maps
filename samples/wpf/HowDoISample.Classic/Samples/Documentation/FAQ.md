---
document_type: thinkgeo-howdoi-faq
---

# Frequently Asked Questions

Common questions about the samples and about ThinkGeo Desktop Maps for WPF with the classic API. In the code below, `mapView` is the `MapView`.

## Getting Started

### Which gallery should I open?

This one if your application runs on .NET Framework 4.8, or keeps code built on `LayerOverlay`, `FeatureLayer`, `ZoomLevelSet` and the `Style` classes: this gallery builds for both .NET Framework 4.8 and .NET 8, and it is the reference for those classes.

Starting a new application on .NET 8 or later? Open the ThinkGeo 15 gallery (`HowDoISample`) instead. It draws the map from a style document, with tilt, terrain and the globe, and it has a page on bringing classic layers across.

### Where do the ThinkGeo Cloud keys go, and how do I use my own?

The samples share theirs in `SampleKeys.cs`. Get your own at cloud.thinkgeo.com and pass it where the samples pass `SampleKeys`.

ThinkGeo Cloud has two kinds of credential, and they are not interchangeable:

1. A client ID and secret (`ClientId`, `ClientSecret`) sign in the classic Cloud overlays and layers - `ThinkGeoCloudRasterMapsOverlay`, `ThinkGeoCloudVectorMapsOverlay` - and the Cloud service clients: geocoding, routing, elevation, time zones, colors, projection.
2. An API key (`ApiKey`) opens the ThinkGeo 15 vector and raster tile sources.

### Where are the map tiles cached?

The ThinkGeo Cloud overlays keep theirs under the system's temporary folder by default. To keep tiles somewhere else, give the overlay or layer a folder your application can write to - the folder the application is installed in usually is not one:

```csharp
overlay.TileCache = new FileRasterTileCache(folder, "my-cache-id");
```

Give each overlay its own cache id: two overlays that share one draw each other's tiles. See Pre-Generate a Tile Cache.

### Should a LayerOverlay be tiled or a single image?

`TileType.MultiTile` (the default) draws the overlay in tiles: they are drawn in parallel, cached, and reused while you pan. `TileType.SingleTile` draws the whole view as one image each time it changes: use it for data that changes often, or for labels that must not be cut at tile edges.

```csharp
var overlay = new LayerOverlay { TileType = TileType.SingleTile };
```

### Why does my layer only draw at some zoom levels?

A style on a `ZoomLevel` applies to that level only, until you say how far it reaches:

```csharp
layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightGreen);
layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
```

See Render Based on Scales.

### Does the map work on a touch screen?

Yes, once the map is allowed to take manipulation events:

```csharp
mapView.IsManipulationEnabled = true;
```

See Map Navigation.

### The printer layers are marked obsolete. What do I use?

The `*PrinterLayer` classes are superseded by the `*PrinterLayoutAsyncLayer` classes in ThinkGeo.Core (`MapPrinterLayoutAsyncLayer`, `LegendPrinterLayoutAsyncLayer`, `DataGridPrinterLayoutAsyncLayer` and the rest). The new layers draw on a `PdfGeoCanvas` for a vector PDF, and on `PrinterGeoCanvas` from ThinkGeo.Printers for a printer. See Print or Export to PDF.

---

## Async and Threading

### How do I cancel an async method?

There are 3 ways:

1. Cancel one specific method with your own `CancellationTokenSource`.

```csharp
var cts = new CancellationTokenSource();
var center = mapView.CurrentExtent.GetCenterPoint();
await mapView.ZoomToAsync(center, mapView.CurrentScale / 2, cts.Token);

// cancel
cts.Cancel();
```

2. Cancel ongoing map operations with `mapView.CancellationTokenSource`.

```csharp
mapView.CancellationTokenSource.Cancel();
```

3. Start another map interaction, such as panning, to interrupt the current operation.

Ways 2 and 3 both work by cancelling the map's internal `CancellationTokenSource`. Methods that change the map's state, such as `ZoomToAsync`, surface this as `OperationCanceledException` on the awaiter, so you can react to the interruption. `RefreshAsync` does not: the tile work stops, but the awaiter sees a normal completion. To observe a `RefreshAsync` cancel from the caller, use way 1: a token of your own that you cancel does throw.

### Why do some methods throw `OperationCanceledException` but others don't?

There are 2 cases:

1. Methods that change the map's state, such as `ZoomToAsync`, throw `OperationCanceledException` (or `TaskCanceledException`, which derives from it - catch the base class).

When a new zoom cancels the previous one, the exception tells you exactly when that state change was interrupted. If you do not need that signal, catch and ignore it, or do not await the call.

```csharp
try
{
    await mapView.ZoomToAsync(center, targetScale);
}
catch (OperationCanceledException)
{
    // A newer zoom replaced this one.
}
```

```csharp
_ = mapView.ZoomToAsync(center, targetScale);
```

2. Methods that do not change the map's state, such as `RefreshAsync()`, do not throw it when the map cancels them: the map's extent and scale stay as they were, so there is nothing to report. A token of your own that you cancel still throws.

---

## Events

### Why does `MapClick` sometimes feel delayed?

There are 2 modes. The default is `MapClickDoubleClickMode.MutuallyExclusive`.

1. `MapClick` and `MapDoubleClick` are mutually exclusive.

After a single click, the map waits for the system double-click interval to see whether a second click follows. That wait is the delay. On a double click, the map raises only `MapDoubleClick`.

```csharp
mapView.ClickDoubleClickMode = MapClickDoubleClickMode.MutuallyExclusive;
```

2. `MapClick` is raised before `MapDoubleClick`.

There is no wait, so `MapClick` is immediate. On a double click, the map raises `MapClick`, `MapClick`, then `MapDoubleClick`, so your code has to tell them apart.

```csharp
mapView.ClickDoubleClickMode = MapClickDoubleClickMode.RaiseClickThenDoubleClick;
```

For immediate press and release events, use `MapMouseDown` and `MapMouseUp`.

```csharp
mapView.MapMouseDown += MapView_MapMouseDown;
mapView.MapMouseUp += MapView_MapMouseUp;
```
