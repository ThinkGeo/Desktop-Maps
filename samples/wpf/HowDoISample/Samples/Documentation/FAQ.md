---
document_type: thinkgeo-howdoi-faq
generated_on: 2026-03-11
schema_version: 1.0
---

# Frequently Asked Questions

Common questions and answers for using ThinkGeo WPF maps.

## Async and Threading


### How do I cancel an async method?

There are 3 ways:

1. Cancel one specific method with your own `CancellationTokenSource`.

```csharp
CancellationTokenSource cts = new CancellationTokenSource();
var centerPoint = mapView.CurrentExtent.GetCenterPoint();
await mapView.ZoomToAsync(centerPoint, mapView.CurrentScale / 2, cts.Token);

// cancel
cts.Cancel();
```

2. Cancel ongoing map operations with `mapView.CancellationTokenSource`.

```csharp
mapView.CancellationTokenSource.Cancel();
```

3. Start another map interaction, such as panning, to interrupt the current operation.


### Why do some methods throw `TaskCanceledException` but some others don't?

There are 2 cases:

1. Methods that change map state, such as `ZoomToAsync`, throw `TaskCanceledException`.

Reason: when a new zoom cancels the previous zoom, ThinkGeo bubbles the exception on purpose so you know exactly when that state change was interrupted.

If you do not need that signal, catch and ignore it, or use fire-and-forget.

```csharp
try{    await mapView.ZoomToAsync(extent);}
catch (OperationCanceledException){    // Ignore canceled zoom.}
```

```csharp
_ = mapView.ZoomToAsync(extent);
```

2. Methods that do not change map state, such as `RefreshAsync()`, do not throw the cancellation exception.

Reason: ThinkGeo swallows the cancellation internally because the map extent and scale do not change.

---

## Events


### Why does `MapClick` sometimes feel delayed?

There are 2 modes. The default is `MapClickDoubleClickMode.MutuallyExclusive`.

1. `MapClick` and `MapDoubleClick` are mutually exclusive.

Reason: after a single click, ThinkGeo waits for the system double-click interval to see whether a second click will follow. That wait is why `MapClick` feels delayed. On a double click, the map raises only `MapDoubleClick` and does not raise `MapClick`.

```csharp
mapView.ClickDoubleClickMode = MapClickDoubleClickMode.MutuallyExclusive;
```

2. `MapClick` is raised before `MapDoubleClick`.

Reason: there is no delay timer, so `MapClick` feels immediate. On a double click, the map raises `MapClick`, `MapClick`, then `MapDoubleClick`, so you need your own logic to tell them apart.

```csharp
mapView.ClickDoubleClickMode = MapClickDoubleClickMode.RaiseClickThenDoubleClick;
```

If you need immediate press/release events, use `MapMouseDown` or `MapMouseUp`.

```csharp
mapView.ExtentOverlay.MapMouseDown += ExtentOverlay_MapMouseDown;
mapView.ExtentOverlay.MapMouseUp += ExtentOverlay_MapMouseUp;
```

---
