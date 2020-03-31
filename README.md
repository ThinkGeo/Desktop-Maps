# Desktop Maps

## Introduction

## Samples

[check out HowDoI samples](samples/wpf/HowDoISample/HowDoI)

## Display a simple map

### Intro: what you'll be doing, and priming them on Product Center

Set up a new project in vs/vscode for a .Net Core WPF application

Add NuGet package to project:

```shell
$ Intall-Package Thinkgeo.DesktopMaps
```

Add MapControl to `MainWindow.xaml`

```xml
<uc1:MapView ...>
```

Add usings to `MainWindow.xaml.cs`

```csharp
using ...
```

Setup `OnMapLoaded` event

```csharp
public void OnMapViewLoaded( .... )
```

Add `CloudMaps` to map

```csharp
var cloudLayer ...
```

Run application

Starting Evaluation

[link](URL)

![image](imageURL)

Add `FeatureSource` to map

```csharp
var capitolLayer ...
```

Add styling to feature layer

```csharp
var capitolStyle ...
```
