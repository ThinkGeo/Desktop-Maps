
# Changelog

## Major changes for ThinkGeo 12.1

Release date: 11/29/2019

ThinkGeo.Core Namespace:

* Enhanced to support high resolution screen for ThinkGeo Cloud Maps.
* Enhanced to support high resolution screen for GoogleMapsLayer.
* Enhanced to support projection for WMTS layer.
* Fixed a bug where UnmanagedProjProjectionConverter converts extent to internal projection incorrectly.
* Fixed the font size issue for PointStyle.
* Fixed a bug where the WKB of MultiLine Empty can't parse.
* Fixed a bug where ThinkGeo Cloud Vector layer aren't rendered at zoom level 19.
* Fixed a bug where the alpha component of GeoColor style didn't work.
* Fixed a bug where TextBaseline can't support multi-line text.
* Enhanced to support cancel drawing line.
* Fixed a bug where the GeoImage being used is disposed for Noaa layer.
* Enhanced to support Cad.
* Enhanced to support multi-thread for projection converter.
* Fixed a bug where FeatureSource.GetColumns method throws exception in multi-threads.
* Enhanced to make SqliteFeatureLayer thread safe.
* Added DrawText method for PositionStyle.
* Added "PointStyle(GeoFont glyphFont, string glyphContent, GeoBrush fillBrush)" constructor for PointStyle.
* Published PointStyle.DrawGlyph method.
* Enhanced to support custom font for PointStyle.
* Changed the default tile size value of ZoomLevelSet from 256 to 512.
* Published PointStyle.DrawImage method.
* Published PointStyle.DrawSymbol method.
* Fixed a bug where the ProjectionConverter.ConvertToExternalProjection method doesn't work.
* Fixed a bug where SqliteFeatureSource querying data from R-Tree index table throws exception.
* Fixed a bug where the SqliteFeatureSource.GetFeaturesByIds method doesn't work.
* Changed longitude and latitude parameter order for some API.
* Enhanced to support GeoLinearGradientBrush for Printer and fixed bug the Printer get different result with the Map if polygon contains inner rings.
* Fixed a bug where the angle of label of GeoLinearGradientBrush is incorrect .
* Fixed an bug where when parsing tile format tiff image, throws "Array index is out of range" exception.
* Enhanced to support background for layer.
* Enhanced to support KeyColors for SkiaGeoCanvas.
* Changed the default web proxy from null to DefaultWebProxy of system.
* Fixed a bug where rendering tiff image is blurred.

ThinkGeo.UI.Wpf Namespace:

* Fixed a bug where LayerOverlay throws the exception when tile type is multiple tile.
* Enhanced to support high resolution screen for ThinkGeo Cloud Maps.
* Improved memory usage for Wpf.
* Changed the zoom animation time form 200ms to 100ms.
* Fixed a bug where ThinkGeocloudRasterMapsOverlay can't support to display map when custom zoom-level scale is smaller default min zoom-level scale.
* Fixed a bug where there are white gaps between tiles.
* Changed longitude and latitude parameter order for some API.
* Changed the default web proxy from null to DefaultWebProxy of system.
* Fixed a pinch zoom issue. When zoom in map, the target scale is smaller than expected.
* Fixed a bug where "map.TrackOverlay", "map.AdornmentOverlay", etc. were all null after Map initialized.
* Fixed a bug where InMemoryMarkerOverlay's marker can not display at first zoomlevel.

ThinkGeo.UI.Blazor Namespace:

* Fixed a bug where AdornmentOverlay.Redraw doesn't work.
* Fixed a bug where RestrictExtent doesn't work when zooming out for ThinkGeo.UI.Blazor.
* Renamed "MapView.MaxExtent" to "MapView.RestrectExtent" for ThinkGeo.UI.Blazor.
* Changed longitude and latitude parameter order for some API.

## Major changes for ThinkGeo 12.0

1. Merge all the managed layers to Core and used GDAL to redesign Unmanged Layers. Rename the packages name, here is latest package name;

* ThinkGeo.UI.Wpf

* ThinkGeo.UI.Blazor

* ThinkGeo.UI.WebApi

* ThinkGeo.Core
* ThinkGeo.Gdal
* ThinkGeo.Ecw 
* ThinkGeo.Cad
* ThinkGeo.GeoTiff
* ThinkGeo.Jpeg2000 
* ThinkGeo.MrSi  
* ThinkGeo.UmanagedProj 
* ThinkGeo.NuticalCharts  
* ThinkGeo.FileGeoDatabase  
* ThinkGeo.PersonalGeoDatabase  
* ThinkGeo.Oracle   
* ThinkGeo.Sqlite   
* ThinkGeo.MsSql   
* ThinkGeo.PostgreSql
* ThinkGeo.Printers  

* ThinkGeo.Dependency.SQLite
* ThinkGeo.Dependency.MicrosoftVisualCRunTime140 
* ThinkGeo.Dependency.NetTopologySuite
* ThinkGeo.Dependency.Npgsql 
* ThinkGeo.Dependency.Printers
* ThinkGeo.Dependency.SkiaSharp 
* ThinkGeo.Dependency.SqlClient
* ThinkGeo.Dependency.Jint
* ThinkGeo.Dependency.SQLite
* ThinkGeo.Dependency.WriteableBitmapEx

2. Redesign GeoCanvas based on SkiaSharp.

3. Updated all the C++ runtime to one single version, use MSVC140.

4. Use one GDAL C# wrap instead of raster layer wrappers, like JPEG2000, ECW, MrSid, Tiff, .etc.

5. Removed FDO, and SDF layer.

6. Renamed our all MapControl to MapView.

7. Used GDAL for S57 and FileGeoDatabase and PersonalGeoDatabase, this will inherit from GDAL Feature Layer.

8. MapSuite TileCache/Tile Matrix, based on current design, the TileView, TileCache, Tile, Matrix are thread safe. It will improve the performance greatly.

* Matrix: Create/Calculate the matrix system for TileLayer and TileOverlay.
* Tile: The tiles for the cached items, the tile contains the tile data and tile location (z,x,y)
* TileCache: Tile Cache for ThinkGeo, it work with one tile.
* TileView: TileOverlay each tile view, it only draw the layers or render the drawing result in UI.

9. Refine our MapSuite properties, make sure set one property won’t change other properties, like Map.Unit will update the map extent, .etc.

10. Support Rotation for WPF.

11. Support StyleJson.
