# GeoColor


## Inheritance Hierarchy

+ `Object`
  + **`GeoColor`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`GeoColor()`](#geocolor)|
|[`GeoColor(Byte,Byte,Byte)`](#geocolorbytebytebyte)|
|[`GeoColor(Byte,Byte,Byte,Byte)`](#geocolorbytebytebytebyte)|
|[`GeoColor(Byte,GeoColor)`](#geocolorbytegeocolor)|

### Protected Constructors Summary


|Name|
|---|
|[`GeoColor(String,ColorType,Byte,Byte,Byte,Byte)`](#geocolorstringcolortypebytebytebytebyte)|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`Ahsl`](#ahsl)|`String`|Gets or sets the ahsl.|
|[`AlphaComponent`](#alphacomponent)|`Byte`|This property returns the alpha component of the GeoColor.|
|[`Argb`](#argb)|`String`|Gets or sets the ARGB.|
|[`BlueComponent`](#bluecomponent)|`Byte`|This property returns the blue component of the GeoColor.|
|[`GreenComponent`](#greencomponent)|`Byte`|This property returns the green component of the GeoColor.|
|[`HtmlColor`](#htmlcolor)|`String`|Gets or sets the color of the HTML.|
|[`Hue`](#hue)|`Single`|This property returns the hue component of the GeoColor.|
|[`IsTransparent`](#istransparent)|`Boolean`|Verify if the GeoColor is transparent, it is considered to be transparent if the Alpha Value is 0.|
|[`Luminance`](#luminance)|`Single`|This property returns the luminance component of the GeoColor.|
|[`RedComponent`](#redcomponent)|`Byte`|This property returns the red component of the GeoColor.|
|[`Saturation`](#saturation)|`Single`|This property returns the saturation component of the GeoColor.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`NameAndARGBValue`](#nameandargbvalue)|`String`|N/A|

### Public Methods Summary


|Name|
|---|
|[`Equals(Object)`](#equalsobject)|
|[`FromAhsl(Int32,Single,Single,Single)`](#fromahslint32singlesinglesingle)|
|[`FromArgb(Byte,Byte,Byte,Byte)`](#fromargbbytebytebytebyte)|
|[`FromArgb(Byte,GeoColor)`](#fromargbbytegeocolor)|
|[`FromHtml(String)`](#fromhtmlstring)|
|[`FromOle(Int32)`](#fromoleint32)|
|[`FromWin32(Int32)`](#fromwin32int32)|
|[`GetColorsInHueFamily(GeoColor,Int32)`](#getcolorsinhuefamilygeocolorint32)|
|[`GetColorsInQualityFamily(GeoColor,Int32)`](#getcolorsinqualityfamilygeocolorint32)|
|[`GetColorsInQualityFamily(GeoColor,GeoColor,Int32,ColorWheelDirection)`](#getcolorsinqualityfamilygeocolorgeocolorint32colorwheeldirection)|
|[`GetHashCode()`](#gethashcode)|
|[`GetRandomGeoColor(RandomColorType)`](#getrandomgeocolorrandomcolortype)|
|[`GetRandomGeoColor(Byte,RandomColorType)`](#getrandomgeocolorbyterandomcolortype)|
|[`GetType()`](#gettype)|
|[`ToHtml(GeoColor)`](#tohtmlgeocolor)|
|[`ToOle(GeoColor)`](#toolegeocolor)|
|[`ToString()`](#tostring)|
|[`ToWin32(GeoColor)`](#towin32geocolor)|

### Protected Methods Summary


|Name|
|---|
|[`<Parse>g__GetNubmers|61_0(GroupCollection)`](#<parse>g__getnubmers|61_0groupcollection)|
|[`Finalize()`](#finalize)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`Parse(String)`](#parsestring)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|N/A|N/A|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`GeoColor()`](#geocolor)|
|[`GeoColor(Byte,Byte,Byte)`](#geocolorbytebytebyte)|
|[`GeoColor(Byte,Byte,Byte,Byte)`](#geocolorbytebytebytebyte)|
|[`GeoColor(Byte,GeoColor)`](#geocolorbytegeocolor)|

### Protected Constructors

#### `GeoColor(String,ColorType,Byte,Byte,Byte,Byte)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|N/A||

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|N/A|
|colorType|[`ColorType`](../ThinkGeo.Core/ThinkGeo.Core.ColorType.md)|N/A|
|alpha|`Byte`|N/A|
|red|`Byte`|N/A|
|green|`Byte`|N/A|
|blue|`Byte`|N/A|

---

### Public Properties

#### `Ahsl`

**Summary**

   *Gets or sets the ahsl.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `AlphaComponent`

**Summary**

   *This property returns the alpha component of the GeoColor.*

**Remarks**

   *None*

**Return Value**

`Byte`

---
#### `Argb`

**Summary**

   *Gets or sets the ARGB.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `BlueComponent`

**Summary**

   *This property returns the blue component of the GeoColor.*

**Remarks**

   *None*

**Return Value**

`Byte`

---
#### `GreenComponent`

**Summary**

   *This property returns the green component of the GeoColor.*

**Remarks**

   *None*

**Return Value**

`Byte`

---
#### `HtmlColor`

**Summary**

   *Gets or sets the color of the HTML.*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Hue`

**Summary**

   *This property returns the hue component of the GeoColor.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `IsTransparent`

**Summary**

   *Verify if the GeoColor is transparent, it is considered to be transparent if the Alpha Value is 0.*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Luminance`

**Summary**

   *This property returns the luminance component of the GeoColor.*

**Remarks**

   *None*

**Return Value**

`Single`

---
#### `RedComponent`

**Summary**

   *This property returns the red component of the GeoColor.*

**Remarks**

   *None*

**Return Value**

`Byte`

---
#### `Saturation`

**Summary**

   *This property returns the saturation component of the GeoColor.*

**Remarks**

   *None*

**Return Value**

`Single`

---

### Protected Properties

#### `NameAndARGBValue`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---

### Public Methods

#### `Equals(Object)`

**Summary**

   *This method is an override of the Equals functionality.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|This method returns the Equals functionality.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|obj|`Object`|This parameter is the object you want to check to see if it is equal to the current instance.|

---
#### `FromAhsl(Int32,Single,Single,Single)`

**Summary**

   *This parameter specifies the red component of the color. This method returns a GeoColor based on the Alpha, Hue, Saturation, and Luminosity components.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|alpha|`Int32`|This parameter specifies the alpha, or transparent, component of the color.|
|hue|`Single`|This parameter specifies the hue component of the color.|
|saturation|`Single`|This parameter specifies the saturation component of the color.|
|luminance|`Single`|This parameter specifies the luminance component of the color.|

---
#### `FromArgb(Byte,Byte,Byte,Byte)`

**Summary**

   *This method returns a GeoColor based on the Alpha, Red, Green, and Blue components.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This method returns a GeoColor based on the Alpha, Red, Green, and Blue components.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|alpha|`Byte`|This parameter specifies the alpha, or transparent, component of the color.|
|red|`Byte`|This parameter specifies the red component of the color.|
|green|`Byte`|This parameter specifies the green component of the color.|
|blue|`Byte`|This parameter specifies the blue component of the color.|

---
#### `FromArgb(Byte,GeoColor)`

**Summary**

   *This method returns a GeoColor based on the Alpha, Red, Green, and Blue components.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|You can use this overload to create a transparent version of another color.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|alpha|`Byte`|This parameter determines how transparent the color is. An alpha of 0 means it is totally transparent.|
|baseColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the color you want to apply the transparency to.|

---
#### `FromHtml(String)`

**Summary**

   *This method returns a GeoColor from an HTML color (either in hexadecimal or a named color).*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This method returns a GeoColor from an HTML color (either in hexadecimal or a named color).|

**Parameters**

|Name|Type|Description|
|---|---|---|
|htmlColor|`String`|This parameter represents the HTML color that you want to convert.|

---
#### `FromOle(Int32)`

**Summary**

   *This method returns a GeoColor from an OLE color.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This method returns a GeoColor from an OLE color.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oleColor|`Int32`|This parameter represents the OLE color you want to convert.|

---
#### `FromWin32(Int32)`

**Summary**

   *This method returns a GeoColor from a Win32 color.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This method returns a GeoColor from a Win32 color.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|win32Color|`Int32`|This parameter represents the Win32 color you want to convert.|

---
#### `GetColorsInHueFamily(GeoColor,Int32)`

**Summary**

   *This method returns a collection of GeoColors based on the same hue that is passed in.*

**Remarks**

   *This method is useful when you want to get a number of colors that have the same hue. For example, you can use this in maps that represent class breaks. If you passed in a red hue, then the method would return variations such as light red, dark red, pastel red, etc.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|This method returns a collection of GeoColors based on the same hue that is passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the color on which you want to base the color collection.|
|numbersOfColors|`Int32`|This parameter represents the number of colors you want returned from the method.|

---
#### `GetColorsInQualityFamily(GeoColor,Int32)`

**Summary**

   *This method returns a collection of GeoColors based on the same quality (luminosity and saturation) that is passed in.*

**Remarks**

   *This method is useful when you want to get a number of different colors that have the same quality. For example, you can use this in maps that represent countries or connected places. If you passed in bright red, then the method would return variants such as bright blue, bright green, etc. If you passed in dark red, you would get dark blue, dark green, etc.*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>|This method returns a collection of GeoColors based on the same quality (luminosity and saturation) that is passed in.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|baseColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter is the color on which you want to base the color collection.|
|numberOfColors|`Int32`|This parameter represents the number of colors you want returned from the method.|

---
#### `GetColorsInQualityFamily(GeoColor,GeoColor,Int32,ColorWheelDirection)`

**Summary**

   *Gets the colors in quality family.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|Collection<[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)>||

**Parameters**

|Name|Type|Description|
|---|---|---|
|fromColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|From color.|
|toColor|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|To color.|
|numberOfColors|`Int32`|The number of colors.|
|colorWheelDirection|[`ColorWheelDirection`](../ThinkGeo.Core/ThinkGeo.Core.ColorWheelDirection.md)|The color wheel direction.|

---
#### `GetHashCode()`

**Summary**

   *This method is an override of the GetHashCode functionality.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|This method returns the hash code.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetRandomGeoColor(RandomColorType)`

**Summary**

   *Creates a random GeoColor structure based on the specific ColorType*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|A GeoColor structure the method created.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|colorType|[`RandomColorType`](../ThinkGeo.Core/ThinkGeo.Core.RandomColorType.md)|A ColorType defines types of color.|

---
#### `GetRandomGeoColor(Byte,RandomColorType)`

**Summary**

   *Creates a random GeoColor structure*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|A GeoColor structure the method created.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|alpha|`Byte`|The alpha component. Valid values are 0 through 255.|
|colorType|[`RandomColorType`](../ThinkGeo.Core/ThinkGeo.Core.RandomColorType.md)|A ColorType defines types of color.|

---
#### `GetType()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Type`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ToHtml(GeoColor)`

**Summary**

   *This method returns an HTML color from a GeoColor.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`String`|This method returns an HTML color from a GeoColor.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|color|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the GeoColor you want to convert.|

---
#### `ToOle(GeoColor)`

**Summary**

   *This method returns an OLE color from a GeoColor.*

**Remarks**

   *None*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|This method returns an OLE color from a GeoColor.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|color|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the GeoColor you want to convert.|

---
#### `ToString()`

**Summary**

   *Converts to string.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|A  that represents this instance.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ToWin32(GeoColor)`

**Summary**

   *This method returns a Win32 color from a GeoColor.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|This method returns a Win32 color from a GeoColor.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|color|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|This parameter represents the GeoColor you want to convert.|

---

### Protected Methods

#### `<Parse>g__GetNubmers|61_0(GroupCollection)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double[]`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|groups|`GroupCollection`|N/A|

---
#### `Finalize()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `MemberwiseClone()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Parse(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`GeoColor`](../ThinkGeo.Core/ThinkGeo.Core.GeoColor.md)|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|colorExpression|`String`|N/A|

---

### Public Events


