---
document_type: thinkgeo-howdoi-faq
---

# Frequently Asked Questions

Common questions about the samples and about ThinkGeo Desktop Maps for WPF. In the code below, `Map` is the `MapView`, the name every sample gives it.

## Getting Started

### Which gallery should I open?

This one if your application runs on .NET 8 or later. If it runs on .NET Framework 4.8, or it keeps v14 code built on `LayerOverlay`, `FeatureLayer` and `ZoomLevelSet` styles, open the Classic gallery (`HowDoISample.Classic`) instead: it builds for both .NET Framework 4.8 and .NET 8, and it is the reference for those classic APIs.

### Where does the ThinkGeo Cloud key go, and how do I use my own?

The samples share theirs in `SampleShared.cs`. Get your own at [cloud.thinkgeo.com](https://cloud.thinkgeo.com) and pass it where the samples pass `SampleShared.CloudApiKey`.

ThinkGeo Cloud has two kinds of credential, and they are not interchangeable:

1. An API key (`CloudApiKey`) opens the vector and raster maps: `new ThinkGeoVectorTileSource(apiKey)`, `new ThinkGeoRasterTileSource(apiKey)`.
2. A client ID and secret (`ClientId`, `ClientSecret`) sign in the Cloud service clients - geocoding, routing, elevation, time zones, colors, projection, maps query - and the classic raster maps layers.

### Where are the map tiles cached?

Where you say. A tile source built in code has no cache until you give it one:

```csharp
var tiles = new ThinkGeoVectorTileSource(apiKey)
{
    VectorTileCache = new FileTileCache(folder)
};
```

The samples use a folder under `%LocalAppData%\ThinkGeo\HowDoI`. Pick a folder your application can write to; the folder the application is installed in usually is not one.

### Why does the map stay blank?

Listen to `TileLoadFailed` on the basemap. A tile that cannot be loaded - a wrong key, a server that does not answer, a file that is not there - is left out of the map and raises the event with the exception, for vector and raster tiles alike. See Handle Tile Failures.

```csharp
basemap.TileLoadFailed += (_, e) => Debug.WriteLine(e.Exception);
```

### What happens when my style.json has a mistake?

`new MapStyle(json, ...)` or `SetStyleAsync` throws with the reason; catch it and show it. The samples with a style editor show the message under the editor: type something wrong into one to see it.

### Does the map work on a touch screen?

Yes, once the map is allowed to take manipulation events:

```csharp
Map.IsManipulationEnabled = true;
```

Pinch to zoom, drag to pan. See Map Navigation.

### What do markers, popups and drawing do when the map is tilted?

Markers and popups stay on their ground position. Tile and layer overlays are laid onto the ground, above the style's labels. Drawing, editing and measuring are meant for the flat map. GPU or Classic?, in Start Here, has the details.

---

## Async and Threading

### How do I cancel an async method?

There are 3 ways:

1. Cancel one specific method with your own `CancellationTokenSource`.

```csharp
var cts = new CancellationTokenSource();
var center = Map.CurrentExtent.GetCenterPoint();
await Map.ZoomToAsync(center, Map.CurrentScale / 2, cts.Token);

// cancel
cts.Cancel();
```

2. Cancel ongoing map operations with `Map.CancellationTokenSource`.

```csharp
Map.CancellationTokenSource.Cancel();
```

3. Start another map interaction, such as panning, to interrupt the current operation.

Ways 2 and 3 both work by cancelling the map's internal `CancellationTokenSource`. Methods that change the map's state, such as `ZoomToAsync`, surface this as `OperationCanceledException` on the awaiter, so you can react to the interruption. `RefreshAsync` does not: the tile work stops, but the awaiter sees a normal completion. To observe a `RefreshAsync` cancel from the caller, use way 1.

### Why do some methods throw `OperationCanceledException` but others don't?

There are 2 cases:

1. Methods that change the map's state, such as `ZoomToAsync`, throw `OperationCanceledException` (or `TaskCanceledException`, which derives from it - catch the base class).

When a new zoom cancels the previous one, the exception tells you exactly when that state change was interrupted. If you do not need that signal, catch and ignore it, or do not await the call.

```csharp
try
{
    await Map.ZoomToAsync(center, targetScale);
}
catch (OperationCanceledException)
{
    // A newer zoom replaced this one.
}
```

```csharp
_ = Map.ZoomToAsync(center, targetScale);
```

2. Methods that do not change the map's state, such as `RefreshAsync()`, do not throw it: the map's extent and scale stay as they were, so there is nothing to report.

---

## Events

### Why does `MapClick` sometimes feel delayed?

There are 2 modes. The default is `MapClickDoubleClickMode.MutuallyExclusive`.

1. `MapClick` and `MapDoubleClick` are mutually exclusive.

After a single click, the map waits for the system double-click interval to see whether a second click follows. That wait is the delay. On a double click, the map raises only `MapDoubleClick`.

```csharp
Map.ClickDoubleClickMode = MapClickDoubleClickMode.MutuallyExclusive;
```

2. `MapClick` is raised before `MapDoubleClick`.

There is no wait, so `MapClick` is immediate. On a double click, the map raises `MapClick`, `MapClick`, then `MapDoubleClick`, so your code has to tell them apart.

```csharp
Map.ClickDoubleClickMode = MapClickDoubleClickMode.RaiseClickThenDoubleClick;
```

For immediate press and release events, use `MapMouseDown` and `MapMouseUp`.

```csharp
Map.MapMouseDown += Map_MapMouseDown;
Map.MapMouseUp += Map_MapMouseUp;
```
