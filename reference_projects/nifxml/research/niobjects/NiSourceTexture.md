# Niobject `NiSourceTexture`

Describes texture source and properties.

## Attributes
- **inherit**: `NiTexture`
- **module**: `NiMain`
- **name**: `NiSourceTexture`

## Fields
- **Use External** (`byte`)
  - Attributes: `default`=`1`
  - Is the texture external?
- **Use Internal** (`byte`)
  - Attributes: `cond`=`Use External == 0`, `default`=`1`, `until`=`10.0.1.3`
- **File Name** (`FilePath`)
  - Attributes: `cond`=`Use External == 1`
  - The external texture file name.
- **File Name** (`FilePath`)
  - Attributes: `cond`=`Use External == 0`, `since`=`10.1.0.0`
  - The original source filename of the image embedded by the referred NiPixelData object.
- **Pixel Data** (`Ref`)
  - Attributes: `cond`=`Use External == 1`, `since`=`10.1.0.0`, `template`=`NiPixelFormat`
- **Pixel Data** (`Ref`)
  - Attributes: `cond`=`Use External == 0 #AND# Use Internal == 1`, `template`=`NiPixelFormat`, `until`=`10.0.1.3`
  - NiPixelData or NiPersistentSrcTextureRendererData
- **Pixel Data** (`Ref`)
  - Attributes: `cond`=`Use External == 0`, `since`=`10.0.1.4`, `template`=`NiPixelFormat`
  - NiPixelData or NiPersistentSrcTextureRendererData
- **Format Prefs** (`FormatPrefs`)
  - A set of preferences for the texture format. They are a request only and the renderer may ignore them.
- **Is Static** (`byte`)
  - Attributes: `default`=`1`
  - If set, then the application cannot assume that any dynamic changes to the pixel data will show in the rendered image.
- **Direct Render** (`bool`)
  - Attributes: `default`=`true`, `since`=`10.1.0.103`
  - A hint to the renderer that the texture can be loaded directly from a texture file into a renderer-specific resource, bypassing the NiPixelData object.
- **Persist Render Data** (`bool`)
  - Attributes: `default`=`false`, `since`=`20.2.0.4`
  - Pixel Data is NiPersistentSrcTextureRendererData instead of NiPixelData.

