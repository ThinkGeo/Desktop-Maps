using System;
using System.Linq;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;
using ThinkGeo.Core.Serialization.Mappers;
using ThinkGeo.UI.Wpf.Serialization.Dtos;

namespace ThinkGeo.UI.Wpf.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="Overlay"/> instances and <see cref="OverlayDescriptor"/>.
/// Implemented: LayerOverlay, BackgroundOverlay, SimpleMarkerOverlay, ThinkGeoCloudVectorMapsOverlay,
/// ThinkGeoCloudRasterMapsOverlay, OpenStreetMapOverlay, WmsOverlay, WmtsOverlay, BingMapsOverlay,
/// GoogleMapsOverlay, AzureMapsRasterOverlay, HereMapsRasterTileOverlay, MapBoxStaticTilesOverlay.
/// Unknown overlay types throw <see cref="NotSupportedException"/>.
/// </summary>
public static class OverlayMapper
{
    /// <summary>Produces a descriptor for a known overlay type; returns <c>null</c> for unknown types.</summary>
    public static OverlayDescriptor? ToDescriptor(Overlay overlay)
    {
        if (overlay == null) throw new ArgumentNullException(nameof(overlay));

        OverlayDescriptor? descriptor = overlay switch
        {
            ThinkGeoCloudVectorMapsOverlay cv => new ThinkGeoCloudVectorMapsOverlayDescriptor
            {
                ClientId = cv.ClientId ?? string.Empty,
                ClientSecret = cv.ClientSecret ?? string.Empty,
                MapType = cv.MapType.ToString(),
            },
            ThinkGeoCloudRasterMapsOverlay cr => new ThinkGeoCloudRasterMapsOverlayDescriptor
            {
                ClientId = cr.ClientId ?? string.Empty,
                ClientSecret = cr.ClientSecret ?? string.Empty,
                MapType = cr.MapType.ToString(),
            },
            OpenStreetMapOverlay osm => new OpenStreetMapOverlayDescriptor
            {
                UserAgent = osm.UserAgent,
            },
            WmsOverlay wms => new WmsOverlayDescriptor
            {
                ServerUri = wms.Uri?.ToString() ?? string.Empty,
                ActiveLayerNames = wms.ActiveLayerNames?.ToList() ?? new System.Collections.Generic.List<string>(),
                ActiveStyleNames = wms.ActiveStyleNames?.ToList() ?? new System.Collections.Generic.List<string>(),
                OutputFormat = wms.OutputFormat ?? "image/png",
                IsTransparent = wms.IsTransparent,
                Parameters = wms.Parameters != null
                    ? new System.Collections.Generic.Dictionary<string, string>(wms.Parameters)
                    : new System.Collections.Generic.Dictionary<string, string>(),
            },
            WmtsOverlay wmts => new WmtsOverlayDescriptor
            {
                ServerUri = wmts.ServerUri?.ToString() ?? string.Empty,
                ActiveLayerName = wmts.ActiveLayerName ?? string.Empty,
                ActiveStyleName = wmts.ActiveStyleName,
                OutputFormat = wmts.OutputFormat,
                WmtsServerEncodingType = wmts.WmtsSeverEncodingType.ToString(),
            },
            BingMapsOverlay bing => new BingMapsOverlayDescriptor
            {
                ApplicationId = bing.ApplicationId ?? string.Empty,
                MapType = bing.MapType.ToString(),
            },
            GoogleMapsOverlay google => new GoogleMapsOverlayDescriptor
            {
                ApiKey = google.ApiKey,
                ClientId = google.ClientId,
                PrivateKey = google.PrivateKey,
                UriSigningSecret = google.UriSigningSecret,
                MapType = google.MapType.ToString(),
            },
            AzureMapsRasterOverlay azure => new AzureMapsRasterOverlayDescriptor
            {
                // SubscriptionKey lives on the inner async layer and isn't exposed by the overlay;
                // capture returns null and callers must fill it in before applying the descriptor.
                SubscriptionKey = null,
                RasterTileSet = azure.RasterTileSet.ToString(),
                ApiVersion = azure.ApiVersion,
            },
            HereMapsRasterTileOverlay here => new HereMapsRasterTileOverlayDescriptor
            {
                ApiKey = here.ApiKey ?? string.Empty,
                MapType = here.MapType.ToString(),
                Format = here.Format.ToString(),
            },
            MapBoxStaticTilesOverlay mb => new MapBoxStaticTilesOverlayDescriptor
            {
                AccessToken = mb.AccessToken ?? string.Empty,
                StyleId = mb.StyleId.ToString(),
            },
            PopupOverlay popup => new PopupOverlayDescriptor
            {
                Popups = popup.Popups.Select(ToPopupDescriptor).ToList(),
            },
            BackgroundOverlay bg => new BackgroundOverlayDescriptor
            {
                BackgroundBrushColor = bg.BackgroundBrush is GeoSolidBrush sb
                    ? ColorMapper.ToHexString(sb.Color)
                    : "#00FFFFFF",
            },
            SimpleMarkerOverlay sm => new SimpleMarkerOverlayDescriptor
            {
                DragMode = sm.DragMode.ToString(),
                Markers = sm.Markers.Select(ToMarkerDescriptor).ToList(),
            },
            LayerOverlay lo => new LayerOverlayDescriptor
            {
                Layers = lo.Layers
                    .Select(LayerMapper.TryToDescriptor)
                    .Where(d => d != null)
                    .ToList()!,
            },
            EditInteractiveOverlay edit => new EditInteractiveOverlayDescriptor
            {
                CanDrag = edit.CanDrag,
                CanReshape = edit.CanReshape,
                CanResize = edit.CanResize,
                CanRotate = edit.CanRotate,
                CanAddVertex = edit.CanAddVertex,
                CanRemoveVertex = edit.CanRemoveVertex,
                EditShapes = FeatureMapper.ToDescriptors(edit.EditShapesLayer.InternalFeatures).ToList(),
            },
            TrackInteractiveOverlay track => new TrackInteractiveOverlayDescriptor
            {
                TrackMode = track.TrackMode.ToString(),
                VertexCountInQuarter = track.VertexCountInQuarter,
                // Drop the in-progress sentinel feature used mid-drag; only capture committed shapes.
                TrackShapes = FeatureMapper.ToDescriptors(
                    track.TrackShapeLayer.InternalFeatures
                        .Where(f => !string.Equals(f.Id, "InTrackingFeature", StringComparison.Ordinal)))
                    .ToList(),
            },
            ExtentInteractiveOverlay ext => new ExtentInteractiveOverlayDescriptor
            {
                PanMode = ext.PanMode.ToString(),
                MapZoomMode = ext.MapZoomMode.ToString(),
                MouseWheelMode = ext.MouseWheelMode.ToString(),
                DoubleLeftClickMode = ext.DoubleLeftClickMode.ToString(),
                DoubleRightClickMode = ext.DoubleRightClickMode.ToString(),
                ExtentChangedType = ext.ExtentChangedType.ToString(),
                ZoomSnapDirection = ext.ZoomSnapDirection.ToString(),
                ZoomPercentage = ext.ZoomPercentage,
                MinimumTrackZoomInExtentInPixels = ext.MinimumTrackZoomInExtentInPixels,
            },
            _ => null,
        };

        if (descriptor == null) return null;

        return descriptor with
        {
            Name = overlay.Name ?? string.Empty,
            IsVisible = overlay.IsVisible,
            Opacity = overlay.Opacity,
        };
    }

    /// <summary>Alias for <see cref="ToDescriptor"/> retained for API symmetry; both now return null on unknown types.</summary>
    public static OverlayDescriptor? TryToDescriptor(Overlay overlay) => ToDescriptor(overlay);

    /// <summary>
    /// Reconstructs a live <see cref="Overlay"/> from a descriptor. Returns <c>null</c> for
    /// descriptor types that cannot be constructed directly — currently only
    /// <see cref="BackgroundOverlayDescriptor"/>, whose real instance is owned by the MapView.
    /// Apply background state via <see cref="MapViewSnapshotExtensions.ApplySnapshot"/>.
    /// </summary>
    public static Overlay? FromDescriptor(OverlayDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        Overlay? overlay = descriptor switch
        {
            ThinkGeoCloudVectorMapsOverlayDescriptor cv => BuildThinkGeoCloudVectorOverlay(cv),
            ThinkGeoCloudRasterMapsOverlayDescriptor cr => BuildThinkGeoCloudRasterOverlay(cr),
            OpenStreetMapOverlayDescriptor osm => string.IsNullOrEmpty(osm.UserAgent)
                ? new OpenStreetMapOverlay()
                : new OpenStreetMapOverlay(osm.UserAgent),
            WmsOverlayDescriptor wms => BuildWmsOverlay(wms),
            WmtsOverlayDescriptor wmts => BuildWmtsOverlay(wmts),
            BingMapsOverlayDescriptor bing => BuildBingMapsOverlay(bing),
            GoogleMapsOverlayDescriptor google => BuildGoogleMapsOverlay(google),
            AzureMapsRasterOverlayDescriptor azure => BuildAzureMapsOverlay(azure),
            HereMapsRasterTileOverlayDescriptor here => BuildHereMapsOverlay(here),
            MapBoxStaticTilesOverlayDescriptor mb => BuildMapBoxOverlay(mb),
            PopupOverlayDescriptor popup => BuildPopupOverlay(popup),
            // BackgroundOverlay is owned by the MapView (its ctor is internal). Return null so
            // callers iterating a list can skip it; apply background state via ApplySnapshot.
            BackgroundOverlayDescriptor => null,
            SimpleMarkerOverlayDescriptor sm => BuildSimpleMarkerOverlay(sm),
            LayerOverlayDescriptor lo => BuildLayerOverlay(lo),
            EditInteractiveOverlayDescriptor edit => BuildEditInteractiveOverlay(edit),
            TrackInteractiveOverlayDescriptor track => BuildTrackInteractiveOverlay(track),
            ExtentInteractiveOverlayDescriptor ext => BuildExtentInteractiveOverlay(ext),
            _ => throw new NotSupportedException(
                $"Overlay descriptor '{descriptor.GetType().FullName}' is not supported yet."),
        };

        if (overlay == null) return null;

        overlay.Name = descriptor.Name;
        overlay.IsVisible = descriptor.IsVisible;
        overlay.Opacity = descriptor.Opacity;
        return overlay;
    }

    private static MarkerDescriptor ToMarkerDescriptor(Marker marker)
    {
        return new MarkerDescriptor(
            WorldX: marker.Position.X,
            WorldY: marker.Position.Y,
            XOffset: marker.XOffset,
            YOffset: marker.YOffset,
            RotationAngle: marker.RotationAngle,
            ImagePath: TryGetImagePath(marker),
            ToolTip: marker.ToolTip as string);
    }

    private static string? TryGetImagePath(Marker marker)
    {
        if (marker.ImageSource is BitmapImage bmp && bmp.UriSource is { IsAbsoluteUri: true } uri
            && uri.IsFile)
        {
            return uri.LocalPath;
        }
        return null;
    }

    private static ThinkGeoCloudVectorMapsOverlay BuildThinkGeoCloudVectorOverlay(ThinkGeoCloudVectorMapsOverlayDescriptor descriptor)
    {
        var mapType = Enum.TryParse<ThinkGeoCloudVectorMapsMapType>(descriptor.MapType, ignoreCase: true, out var mt)
            ? mt
            : ThinkGeoCloudVectorMapsMapType.Light;
        return new ThinkGeoCloudVectorMapsOverlay(descriptor.ClientId, descriptor.ClientSecret, mapType);
    }

    private static ThinkGeoCloudRasterMapsOverlay BuildThinkGeoCloudRasterOverlay(ThinkGeoCloudRasterMapsOverlayDescriptor descriptor)
    {
        var mapType = Enum.TryParse<ThinkGeoCloudRasterMapsMapType>(descriptor.MapType, ignoreCase: true, out var mt)
            ? mt
            : ThinkGeoCloudRasterMapsMapType.Light;
        return new ThinkGeoCloudRasterMapsOverlay(descriptor.ClientId, descriptor.ClientSecret, mapType);
    }

    private static WmsOverlay BuildWmsOverlay(WmsOverlayDescriptor descriptor)
    {
        var uri = string.IsNullOrEmpty(descriptor.ServerUri)
            ? null
            : new Uri(descriptor.ServerUri, UriKind.Absolute);
        var overlay = uri != null ? new WmsOverlay(uri) : new WmsOverlay();
        foreach (var name in descriptor.ActiveLayerNames) overlay.ActiveLayerNames.Add(name);
        foreach (var name in descriptor.ActiveStyleNames) overlay.ActiveStyleNames.Add(name);
        if (!string.IsNullOrEmpty(descriptor.OutputFormat)) overlay.OutputFormat = descriptor.OutputFormat;
        overlay.IsTransparent = descriptor.IsTransparent;
        foreach (var kv in descriptor.Parameters) overlay.Parameters[kv.Key] = kv.Value;
        return overlay;
    }

    private static SimpleMarkerOverlay BuildSimpleMarkerOverlay(SimpleMarkerOverlayDescriptor descriptor)
    {
        var overlay = new SimpleMarkerOverlay();
        if (Enum.TryParse<MarkerDragMode>(descriptor.DragMode, ignoreCase: true, out var dragMode))
        {
            overlay.DragMode = dragMode;
        }
        foreach (var markerDescriptor in descriptor.Markers)
        {
            var marker = new Marker(markerDescriptor.WorldX, markerDescriptor.WorldY)
            {
                XOffset = markerDescriptor.XOffset,
                YOffset = markerDescriptor.YOffset,
                RotationAngle = markerDescriptor.RotationAngle,
            };
            if (!string.IsNullOrEmpty(markerDescriptor.ImagePath))
            {
                marker.ImageSource = new BitmapImage(new Uri(markerDescriptor.ImagePath, UriKind.Absolute));
            }
            if (!string.IsNullOrEmpty(markerDescriptor.ToolTip))
            {
                marker.ToolTip = markerDescriptor.ToolTip;
            }
            overlay.Markers.Add(marker);
        }
        return overlay;
    }

    private static LayerOverlay BuildLayerOverlay(LayerOverlayDescriptor descriptor)
    {
        var overlay = new LayerOverlay();
        foreach (var layerDescriptor in descriptor.Layers)
        {
            overlay.Layers.Add(LayerMapper.FromDescriptor(layerDescriptor));
        }
        return overlay;
    }

    private static WmtsOverlay BuildWmtsOverlay(WmtsOverlayDescriptor descriptor)
    {
        var uri = new Uri(descriptor.ServerUri, UriKind.Absolute);
        var encoding = Enum.TryParse<WmtsServerEncodingType>(descriptor.WmtsServerEncodingType, ignoreCase: true, out var enc)
            ? enc
            : WmtsServerEncodingType.Kvp;
        var overlay = new WmtsOverlay(uri, encoding);
        if (!string.IsNullOrEmpty(descriptor.ActiveLayerName)) overlay.ActiveLayerName = descriptor.ActiveLayerName;
        if (!string.IsNullOrEmpty(descriptor.ActiveStyleName)) overlay.ActiveStyleName = descriptor.ActiveStyleName;
        if (!string.IsNullOrEmpty(descriptor.OutputFormat)) overlay.OutputFormat = descriptor.OutputFormat;
        return overlay;
    }

    private static BingMapsOverlay BuildBingMapsOverlay(BingMapsOverlayDescriptor descriptor)
    {
        var mapType = Enum.TryParse<BingMapsMapType>(descriptor.MapType, ignoreCase: true, out var mt)
            ? mt
            : BingMapsMapType.Road;
        return new BingMapsOverlay(descriptor.ApplicationId, mapType);
    }

    private static GoogleMapsOverlay BuildGoogleMapsOverlay(GoogleMapsOverlayDescriptor descriptor)
    {
        GoogleMapsOverlay overlay;
        if (!string.IsNullOrEmpty(descriptor.ApiKey))
        {
            overlay = !string.IsNullOrEmpty(descriptor.UriSigningSecret)
                ? new GoogleMapsOverlay(descriptor.ApiKey, descriptor.UriSigningSecret)
                : new GoogleMapsOverlay(descriptor.ApiKey);
        }
        else
        {
            overlay = new GoogleMapsOverlay();
            if (!string.IsNullOrEmpty(descriptor.ClientId)) overlay.ClientId = descriptor.ClientId;
            if (!string.IsNullOrEmpty(descriptor.PrivateKey)) overlay.PrivateKey = descriptor.PrivateKey;
            if (!string.IsNullOrEmpty(descriptor.UriSigningSecret)) overlay.UriSigningSecret = descriptor.UriSigningSecret;
        }
        if (Enum.TryParse<GoogleMapsMapType>(descriptor.MapType, ignoreCase: true, out var mt))
        {
            overlay.MapType = mt;
        }
        return overlay;
    }

    private static AzureMapsRasterOverlay BuildAzureMapsOverlay(AzureMapsRasterOverlayDescriptor descriptor)
    {
        var tileSet = Enum.TryParse<AzureMapsRasterTileSet>(descriptor.RasterTileSet, ignoreCase: true, out var ts)
            ? ts
            : AzureMapsRasterTileSet.BaseRoad;
        var overlay = !string.IsNullOrEmpty(descriptor.SubscriptionKey)
            ? new AzureMapsRasterOverlay(descriptor.SubscriptionKey, tileSet)
            : new AzureMapsRasterOverlay();
        if (!string.IsNullOrEmpty(descriptor.ApiVersion)) overlay.ApiVersion = descriptor.ApiVersion;
        return overlay;
    }

    private static HereMapsRasterTileOverlay BuildHereMapsOverlay(HereMapsRasterTileOverlayDescriptor descriptor)
    {
        var mapType = Enum.TryParse<HereMapsRasterType>(descriptor.MapType, ignoreCase: true, out var mt)
            ? mt
            : HereMapsRasterType.BaseMap;
        var overlay = new HereMapsRasterTileOverlay(descriptor.ApiKey, mapType);
        if (Enum.TryParse<HereMapsRasterTileFormat>(descriptor.Format, ignoreCase: true, out var fmt))
        {
            overlay.Format = fmt;
        }
        return overlay;
    }

    private static MapBoxStaticTilesOverlay BuildMapBoxOverlay(MapBoxStaticTilesOverlayDescriptor descriptor)
    {
        var styleId = Enum.TryParse<MapBoxStyleId>(descriptor.StyleId, ignoreCase: true, out var sid)
            ? sid
            : MapBoxStyleId.Streets;
        return new MapBoxStaticTilesOverlay(descriptor.AccessToken, styleId);
    }

    private static PopupDescriptor ToPopupDescriptor(Popup popup)
    {
        return new PopupDescriptor(
            WorldX: popup.Position.X,
            WorldY: popup.Position.Y,
            ArrowHeight: popup.ArrowHeight,
            RotateAngle: popup.RotateAngle,
            Content: popup.Content as string);
    }

    private static EditInteractiveOverlay BuildEditInteractiveOverlay(EditInteractiveOverlayDescriptor descriptor)
    {
        var overlay = new EditInteractiveOverlay
        {
            CanDrag = descriptor.CanDrag,
            CanReshape = descriptor.CanReshape,
            CanResize = descriptor.CanResize,
            CanRotate = descriptor.CanRotate,
            CanAddVertex = descriptor.CanAddVertex,
            CanRemoveVertex = descriptor.CanRemoveVertex,
        };
        if (descriptor.EditShapes.Count > 0)
        {
            overlay.EditShapesLayer.InternalFeatures.Clear();
            foreach (var f in descriptor.EditShapes)
            {
                overlay.EditShapesLayer.InternalFeatures.Add(FeatureMapper.FromDescriptor(f));
            }
        }
        return overlay;
    }

    private static TrackInteractiveOverlay BuildTrackInteractiveOverlay(TrackInteractiveOverlayDescriptor descriptor)
    {
        var overlay = new TrackInteractiveOverlay
        {
            TrackMode = Enum.TryParse<TrackMode>(descriptor.TrackMode, ignoreCase: true, out var tm)
                ? tm
                : TrackMode.None,
            VertexCountInQuarter = descriptor.VertexCountInQuarter,
        };
        if (descriptor.TrackShapes.Count > 0)
        {
            overlay.TrackShapeLayer.InternalFeatures.Clear();
            foreach (var f in descriptor.TrackShapes)
            {
                overlay.TrackShapeLayer.InternalFeatures.Add(FeatureMapper.FromDescriptor(f));
            }
        }
        return overlay;
    }

    private static ExtentInteractiveOverlay BuildExtentInteractiveOverlay(ExtentInteractiveOverlayDescriptor descriptor)
    {
        var overlay = new ExtentInteractiveOverlay();
        if (Enum.TryParse<MapPanMode>(descriptor.PanMode, ignoreCase: true, out var pan)) overlay.PanMode = pan;
        if (Enum.TryParse<MapZoomMode>(descriptor.MapZoomMode, ignoreCase: true, out var zoom)) overlay.MapZoomMode = zoom;
        if (Enum.TryParse<MapMouseWheelMode>(descriptor.MouseWheelMode, ignoreCase: true, out var wheel)) overlay.MouseWheelMode = wheel;
        if (Enum.TryParse<MapDoubleClickMode>(descriptor.DoubleLeftClickMode, ignoreCase: true, out var dl)) overlay.DoubleLeftClickMode = dl;
        if (Enum.TryParse<MapDoubleClickMode>(descriptor.DoubleRightClickMode, ignoreCase: true, out var dr)) overlay.DoubleRightClickMode = dr;
        // ExtentChangedType has an internal setter; capture-only. The descriptor value is ignored on apply.
        if (Enum.TryParse<ZoomSnapDirection>(descriptor.ZoomSnapDirection, ignoreCase: true, out var sd)) overlay.ZoomSnapDirection = sd;
        overlay.ZoomPercentage = descriptor.ZoomPercentage;
        overlay.MinimumTrackZoomInExtentInPixels = descriptor.MinimumTrackZoomInExtentInPixels;
        return overlay;
    }

    private static PopupOverlay BuildPopupOverlay(PopupOverlayDescriptor descriptor)
    {
        var overlay = new PopupOverlay();
        foreach (var popupDescriptor in descriptor.Popups)
        {
            var popup = new Popup(popupDescriptor.WorldX, popupDescriptor.WorldY)
            {
                ArrowHeight = popupDescriptor.ArrowHeight,
                RotateAngle = popupDescriptor.RotateAngle,
            };
            if (popupDescriptor.Content != null)
            {
                popup.Content = popupDescriptor.Content;
            }
            overlay.Popups.Add(popup);
        }
        return overlay;
    }
}
