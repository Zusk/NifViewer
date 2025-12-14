# Niobject `NiPersistentSrcTextureRendererData`

## Attributes
- **inherit**: `NiPixelFormat`
- **module**: `NiMain`
- **name**: `NiPersistentSrcTextureRendererData`

## Fields
- **Palette** (`Ref`)
  - Attributes: `template`=`NiPalette`
- **Num Mipmaps** (`uint`)
- **Bytes Per Pixel** (`uint`)
- **Mipmaps** (`MipMap`)
  - Attributes: `length`=`Num Mipmaps`
- **Num Pixels** (`uint`)
- **Pad Num Pixels** (`uint`)
  - Attributes: `since`=`20.2.0.6`
- **Num Faces** (`uint`)
- **Platform** (`PlatformID`)
  - Attributes: `until`=`30.1.0.0`
- **Renderer** (`RendererID`)
  - Attributes: `since`=`30.1.0.1`
- **Pixel Data** (`byte`)
  - Attributes: `binary`=`true`, `length`=`Num Pixels #MUL# Num Faces`

