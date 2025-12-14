# Enum `PixelFormat`

Describes the pixel format used by the NiPixelData object to store a texture.

## Attributes
- **name**: `PixelFormat`
- **prefix**: `PX`
- **storage**: `uint`

## Values
- `option` `FMT_RGB` (`value`=`0`) – 24-bit RGB. 8 bits per red, blue, and green component.
- `option` `FMT_RGBA` (`value`=`1`) – 32-bit RGB with alpha. 8 bits per red, blue, green, and alpha component.
- `option` `FMT_PAL` (`value`=`2`) – 8-bit palette index.
- `option` `FMT_PALA` (`value`=`3`) – 8-bit palette index with alpha.
- `option` `FMT_DXT1` (`value`=`4`) – DXT1 compressed texture.
- `option` `FMT_DXT3` (`value`=`5`) – DXT3 compressed texture.
- `option` `FMT_DXT5` (`value`=`6`) – DXT5 compressed texture.
- `option` `FMT_RGB24NONINT` (`value`=`7`) – (Deprecated) 24-bit noninterleaved texture, an old PS2 format.
- `option` `FMT_BUMP` (`value`=`8`) – Uncompressed dU/dV gradient bump map.
- `option` `FMT_BUMPLUMA` (`value`=`9`) – Uncompressed dU/dV gradient bump map with luma channel representing shininess.
- `option` `FMT_RENDERSPEC` (`value`=`10`) – Generic descriptor for any renderer-specific format not described by other formats.
- `option` `FMT_1CH` (`value`=`11`) – Generic descriptor for formats with 1 component.
- `option` `FMT_2CH` (`value`=`12`) – Generic descriptor for formats with 2 components.
- `option` `FMT_3CH` (`value`=`13`) – Generic descriptor for formats with 3 components.
- `option` `FMT_4CH` (`value`=`14`) – Generic descriptor for formats with 4 components.
- `option` `FMT_DEPTH_STENCIL` (`value`=`15`) – Indicates the NiPixelFormat is meant to be used on a depth/stencil surface.
- `option` `FMT_UNKNOWN` (`value`=`16`)

