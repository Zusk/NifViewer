# Niobject `NiPixelFormat`

NiPixelFormat is not the parent to NiPixelData/NiPersistentSrcTextureRendererData,
but actually a member class loaded at the top of each. The two classes are not related.
However, faking this inheritance is useful for several things.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiPixelFormat`

## Fields
- **Pixel Format** (`PixelFormat`)
  - The format of the pixels in this internally stored image.
- **Red Mask** (`uint`)
  - Attributes: `until`=`10.4.0.1`
  - 0x000000ff (for 24bpp and 32bpp) or 0x00000000 (for 8bpp)
- **Green Mask** (`uint`)
  - Attributes: `until`=`10.4.0.1`
  - 0x0000ff00 (for 24bpp and 32bpp) or 0x00000000 (for 8bpp)
- **Blue Mask** (`uint`)
  - Attributes: `until`=`10.4.0.1`
  - 0x00ff0000 (for 24bpp and 32bpp) or 0x00000000 (for 8bpp)
- **Alpha Mask** (`uint`)
  - Attributes: `until`=`10.4.0.1`
  - 0xff000000 (for 32bpp) or 0x00000000 (for 24bpp and 8bpp)
- **Bits Per Pixel** (`uint`)
  - Attributes: `until`=`10.4.0.1`
  - Bits per pixel, 0 (Compressed), 8, 24 or 32.
- **Old Fast Compare** (`byte`)
  - Attributes: `length`=`8`, `until`=`10.4.0.1`
  - [96,8,130,0,0,65,0,0] if 24 bits per pixel
[129,8,130,32,0,65,12,0] if 32 bits per pixel
[34,0,0,0,0,0,0,0] if 8 bits per pixel
[X,0,0,0,0,0,0,0] if 0 (Compressed) bits per pixel where X = PixelFormat
- **Tiling** (`PixelTiling`)
  - Attributes: `since`=`10.1.0.0`, `until`=`10.4.0.1`
  - Seems to always be zero.
- **Bits Per Pixel** (`byte`)
  - Attributes: `since`=`10.4.0.2`
  - Bits per pixel, 0 (Compressed), 8, 24 or 32.
- **Renderer Hint** (`uint`)
  - Attributes: `since`=`10.4.0.2`
- **Extra Data** (`uint`)
  - Attributes: `since`=`10.4.0.2`
- **Flags** (`byte`)
  - Attributes: `since`=`10.4.0.2`
- **Tiling** (`PixelTiling`)
  - Attributes: `since`=`10.4.0.2`
- **sRGB Space** (`bool`)
  - Attributes: `since`=`20.3.0.4`
- **Channels** (`PixelFormatComponent`)
  - Attributes: `length`=`4`, `since`=`10.4.0.2`
  - Channel Data

