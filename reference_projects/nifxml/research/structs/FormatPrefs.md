# Struct `FormatPrefs`

NiTexture::FormatPrefs. These preferences are a request to the renderer to use a format the most closely matches the settings and may be ignored.

## Attributes
- **module**: `NiMain`
- **name**: `FormatPrefs`
- **size**: `12`

## Fields
- **Pixel Layout** (`PixelLayout`)
  - Requests the way the image will be stored.
- **Use Mipmaps** (`MipMapFormat`)
  - Attributes: `default`=`MIP_FMT_DEFAULT`
  - Requests if mipmaps are used or not.
- **Alpha Format** (`AlphaFormat`)
  - Attributes: `default`=`ALPHA_DEFAULT`
  - Requests no alpha, 1-bit alpha, or

