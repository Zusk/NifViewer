# Niobject `NiPixelData`

A texture.

## Attributes
- **inherit**: `NiPixelFormat`
- **module**: `NiMain`
- **name**: `NiPixelData`

## Fields
- **Palette** (`Ref`)
  - Attributes: `template`=`NiPalette`
- **Num Mipmaps** (`uint`)
- **Bytes Per Pixel** (`uint`)
- **Mipmaps** (`MipMap`)
  - Attributes: `length`=`Num Mipmaps`
- **Num Pixels** (`uint`)
- **Num Faces** (`uint`)
  - Attributes: `default`=`1`, `since`=`10.4.0.2`
- **Pixel Data** (`byte`)
  - Attributes: `binary`=`true`, `length`=`Num Pixels`, `until`=`10.4.0.1`
- **Pixel Data** (`byte`)
  - Attributes: `binary`=`true`, `length`=`Num Pixels #MUL# Num Faces`, `since`=`10.4.0.2`

