# Enum `TexFilterMode`

Describes the availiable texture filter modes, i.e. the way the pixels in a texture are displayed on screen.

## Attributes
- **name**: `TexFilterMode`
- **storage**: `uint`

## Values
- `option` `FILTER_NEAREST` (`value`=`0`) – Nearest neighbor. Uses nearest texel with no mipmapping.
- `option` `FILTER_BILERP` (`value`=`1`) – Bilinear. Linear interpolation with no mipmapping.
- `option` `FILTER_TRILERP` (`value`=`2`) – Trilinear. Linear intepolation between 8 texels (4 nearest texels between 2 nearest mip levels).
- `option` `FILTER_NEAREST_MIPNEAREST` (`value`=`3`) – Nearest texel on nearest mip level.
- `option` `FILTER_NEAREST_MIPLERP` (`value`=`4`) – Linear interpolates nearest texel between two nearest mip levels.
- `option` `FILTER_BILERP_MIPNEAREST` (`value`=`5`) – Linear interpolates on nearest mip level.
- `option` `FILTER_ANISOTROPIC` (`value`=`6`) – Anisotropic filtering. One or many trilinear samples depending on anisotropy.

