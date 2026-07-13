# ThinkGeo Snapshot Tool

JSON-based serialization for `ThinkGeo.UI.Wpf.MapView` state. Captures overlays, layers, styles,
features, projections, and adornments to a portable string that can be persisted and later
re-applied to a fresh `MapView`.

Two scenarios motivated the tool:

- **Replace the deprecated `[Serializable]` / `BinaryFormatter` workflow** that older ThinkGeo code
  used for map-state persistence. `BinaryFormatter` is disabled in .NET 8+, and the ThinkGeo team
  is removing `[Serializable]` attributes from their types over the 14.x → 15.x transition.
- **Ship a map state** (server-side rendering, bug-repro kits, "save my workspace" UIs) without
  pulling in an opinionated domain model.

## Projects

| Project | Purpose |
|---|---|
| `ThinkGeo.Core.Serialization` | Platform-neutral DTOs + mappers for `ThinkGeo.Core` types (layers, styles, features, pens, brushes, projections). |
| `ThinkGeo.UI.Wpf.Serialization` | WPF-specific pieces: `MapView` extensions, `Overlay` / `AdornmentLayer` mappers. Builds on `Core.Snapshots`. |
| `ThinkGeo.UI.Wpf.Serialization.UnitTest` | 78 unit tests — synthetic round-trip coverage. |
| `ThinkGeo.UI.Wpf.Serialization.HowDoITest` | 148 integration tests — every HowDoI sample fixed-point verified against the real `MapView` bootstrap. |

## Usage

```csharp
using ThinkGeo.UI.Wpf.Serialization;

// Capture state from a live MapView
var json = MapSerializer.Serialize(mapView);
File.WriteAllText("workspace.json", json);

// Restore into a fresh MapView — overlays, adornments, layers, styles,
// projection converters, extent, rotation, zoom scales all come back.
var restored = new MapView();
MapSerializer.Deserialize(restored, File.ReadAllText("workspace.json"));
```

## Coverage

Tested against `ThinkGeo.Core` + `ThinkGeo.UI.Wpf` **15.0.0-beta019** (project was originally
built against 14.5.0-beta116 and upgraded with zero code changes — the API surface we hook into
is stable).

### Supported

- **Overlays**: `LayerOverlay`, `BackgroundOverlay`, `SimpleMarkerOverlay` (with `Marker` images +
  tooltips), `PopupOverlay`, `ThinkGeoCloudVectorMapsOverlay`, `ThinkGeoCloudRasterMapsOverlay`,
  `OpenStreetMapOverlay`, `WmsOverlay`, `WmtsOverlay`, `BingMapsOverlay`, `GoogleMapsOverlay`,
  `AzureMapsRasterOverlay`, `HereMapsRasterTileOverlay`, `MapBoxStaticTilesOverlay`,
  `EditInteractiveOverlay` (with edit shapes), `TrackInteractiveOverlay` (with track shapes),
  `ExtentInteractiveOverlay`.
- **Layers**: `ShapeFileFeatureLayer`, `MultipleShapeFileFeatureLayer`, `InMemoryFeatureLayer`
  (with inline features + columns), `WkbFileFeatureLayer`, `TabFeatureLayer`,
  `TinyGeoFeatureLayer`, `GpxFeatureLayer`, `SqliteFeatureLayer`, `GraticuleFeatureLayer`,
  `WmsAsyncLayer`, `GeoTiffRasterLayer`, `RasterMbTilesAsyncLayer`.
- **Styles**: `AreaStyle`, `LineStyle`, `PointStyle` (including icon-based), `TextStyle`,
  `IconStyle` (label over image), `HeatStyle` (with color palette + intensity column),
  `CompositeStyle`, `ClassBreakStyle`, `ValueStyle`, `RegexStyle`, `FilterStyle`, `DotDensityStyle`.
- **Adornments**: `LogoAdornmentLayer`, `ScaleLineAdornmentLayer`, `ScaleTextAdornmentLayer`,
  `ScaleBarAdornmentLayer`, `LegendAdornmentLayer` (with title / footer / legend items),
  `MagneticDeclinationAdornmentLayer`.
- **Projection**: `ProjectionConverter` on `FeatureSource`, `ImageSource`, `WmsAsyncLayer`, and
  `RasterXyzTileAsyncLayer` — captured as SRID (preferred) or PROJ4 string fallback.
- **MapView essentials**: center, scale, rotation, `MapUnit`, `ZoomScales`.
- **Feature data**: WKT geometry + id + column values (via `BaseShape.CreateShapeFromWellKnownData`
  — schema-stable across SDK versions).

### Known losses (what won't round-trip)

1. **WPF-bound content**: `Marker.Content`, `Popup.Content` when they hold a `UIElement`, data
   binding, or a template. String content is captured; everything richer is dropped.
2. **Credentials / secrets**: `AzureMapsRasterOverlay.SubscriptionKey` is not exposed by the
   overlay (it lives on an inner async layer) — capture returns `null`, callers must repopulate
   after `ApplyJson`. Other cloud credentials (`ThinkGeoCloud*.ClientSecret`, `WmsOverlay`
   auth headers) are captured as-is — **do not persist snapshots containing secrets to
   untrusted storage**.
3. **`ProjectionConverter` subclass identity**: a `GdalProjectionConverter` and a stock
   `Projection` built from the same SRID produce slightly different canonical PROJ4 strings.
   We capture SRID only when available, so the rebuilt converter's backend choice is "whatever
   `new Projection(srid)` gives you". Functionally equivalent; not byte-for-byte identical.
4. **`MagneticDeclinationAdornmentLayer.SampleDateTime` range**: the SDK only accepts dates in
   1900–2015 (the magnetic field data coverage window). Values outside that are silently clamped
   to the nearest edge during both capture and apply.
5. **`ExtentInteractiveOverlay.ExtentChangedType`**: setter is internal — captured for inspection
   but not applied. Value from the descriptor is ignored on restore.
6. **Event handlers**: CLR event subscriptions (click handlers, map events) are code, not state —
   rewire after `ApplyJson`.
7. **Runtime-built data**: `InMemoryFeatureLayer` captures `InternalFeatures` but styles/zoom
   levels depend on the live layer's state at capture time.
8. **Schema version**: the root snapshot has a `SchemaVersion` field (currently `1`), but no
   migration path is implemented yet. Breaking changes will bump this and require new-version
   readers.

### Not supported

- `MapPrinterLayer` + print workflow.
- `RasterStyle`.
- Unusual CRS: only SRID-addressable coordinate systems and PROJ4 strings are captured; custom
  WKT or ESRI-format CRS definitions fall through as PROJ4-only.

## Test strategy

Two complementary layers:

1. **Unit tests (78)** — synthetic `x → JSON → x'` round-trips for every descriptor kind. Fast
   (~2s suite), catches regressions in the mappers themselves.
2. **HowDoI fixed-point sweep (148)** — reflection-driven test that hosts every HowDoI WPF
   sample in an off-screen `Window`, pumps the dispatcher until its `MapView` bootstraps, then
   asserts `ToJson(mv) == ToJson(ApplyJson(fresh, ToJson(mv)))` — a fixed-point, stronger than
   property-level round-trip because any capture/apply asymmetry breaks the byte-level equality.
   Runs in ~10 minutes. Samples that can't be exercised (no MapView, overlay added only by
   button click, missing native dependency, self-refreshing perf test) report as `SKIP` in
   test output and don't fail the suite.

Two real bugs were found by the HowDoI sweep that the synthetic unit tests missed:

- `ProjectionMapper` non-idempotence when both SRID and PROJ string were captured.
- `MagneticDeclinationAdornmentLayer.SampleDateTime` accepting `DateTime.UtcNow` (outside the
  SDK's 1900–2015 valid range) on apply.

Both are fixed.

## Running tests

```powershell
# All 78 unit tests, ~2s
dotnet test tools/ThinkGeo.UI.Wpf.Serialization.UnitTest

# HowDoI fixed-point sweep, ~10min (spawns an off-screen WPF window per sample)
dotnet test tools/ThinkGeo.UI.Wpf.Serialization.HowDoITest
```

Both projects target `net8.0-windows` and require WPF + STA — they only run on Windows.

## Porting to other ThinkGeo UI flavors

### Why code sharing between WPF and WinForms stops where it does

`ThinkGeo.Core.Serialization` already covers ~60% of the codebase — layers, styles, features, pens,
brushes, projection converters. The remaining ~40% in `ThinkGeo.UI.Wpf.Serialization` is UI-specific
*by necessity*, not architectural accident. Concretely, as of ThinkGeo 15.0.0-beta019:

| Type category | Lives in | Shareable? |
|---|---|---|
| All `AdornmentLayer` subtypes (Logo, ScaleBar, MagneticDeclination, …) | `ThinkGeo.Core` | ✅ types are shared |
| All 15 concrete `Overlay` subtypes (Cloud, WMS, Bing, OSM, Google, Azure, Here, MapBox, WMTS, LayerOverlay, SimpleMarkerOverlay, PopupOverlay, Edit/Track/Extent Interactive) | `ThinkGeo.UI.Wpf` — and a **parallel set of same-named, different-type** classes in `ThinkGeo.UI.WinForms` | ❌ |
| `Marker`, `Popup`, `MapView`, `AdornmentOverlay` container | UI assembly | ❌ |

The hard wall is the overlay dispatch. `OverlayMapper` is fundamentally a runtime type switch:

```csharp
overlay switch {
    WmsOverlay wms => new WmsOverlayDescriptor { ServerUri = wms.Uri.ToString(), ... },
    ThinkGeoCloudVectorMapsOverlay cv => ...,
    // 15 branches
}
```

`WmsOverlay` in `ThinkGeo.UI.Wpf.dll` and `WmsOverlay` in `ThinkGeo.UI.WinForms.dll` are
**two separate CLR types**. A `switch` can't pattern-match both — so the dispatch layer has to
exist twice, once per flavor.

Three ways to force sharing and why each is worse than duplication:

1. **Reflection** (look up `Uri` / `ActiveLayerNames` by property name) — loses compile-time safety,
   harder to debug, slower.
2. **Generic adapter interfaces** — each flavor still implements the adapter per overlay type, so
   the duplication just moves. Adds indirection without saving lines.
3. **Source generator** — over-engineered for 15 types that change once per SDK major.

### What a port actually involves

Porting to WinForms (or MAUI, or Blazor) means creating a sibling package like
`ThinkGeo.UI.WinForms.Serialization` that contains:

1. A copy of `OverlayMapper.cs` (~340 lines) with the overlay type references re-pointed to the
   target flavor's `ThinkGeo.UI.WinForms` namespace. Image handling for markers differs
   (`BitmapImage` ↔ `System.Drawing.Image` ↔ `MAUI.ImageSource`) — ~20 lines to adjust.
2. A copy of `AdornmentMapper.cs`. Most of this is type-shared (`AdornmentLayer` subtypes are in
   Core), so only the `AdornmentOverlay` container wiring changes.
3. A `MapViewSnapshotExtensions.cs` that owns the in-place apply pattern: clear `Overlays`,
   mutate `BackgroundOverlay` and `AdornmentOverlay` in place (their instances belong to the
   `MapView`).
4. A `MapSerializer.cs` facade — the WPF one is ~30 lines; copy and retype the MapView
   parameter.
5. An integration sweep against that flavor's HowDoI sample project (same fixed-point pattern —
   byte-compare JSON across a snapshot → restore → snapshot cycle).

Estimated effort: ~1 day once the Core layer is solid. Subsequent flavors are faster because
the DTO surface and the edge cases we already found (projection non-idempotence, magnetic date
clamp, dispatcher teardown races) carry over directly.

### Why no sibling packages exist yet

Build one when a concrete user need shows up. WinForms / MAUI / Blazor users tend to pick one
flavor and stay there, so preemptively shipping all four costs roughly 4× the ongoing
maintenance for speculative value.
