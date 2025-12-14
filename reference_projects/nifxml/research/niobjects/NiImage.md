# Niobject `NiImage`

LEGACY (pre-10.1)

## Attributes
- **inherit**: `NiObject`
- **module**: `NiLegacy`
- **name**: `NiImage`
- **until**: `V10_0_1_0`

## Fields
- **Use External** (`byte`)
  - 0 if the texture is internal to the NIF file.
- **File Name** (`FilePath`)
  - Attributes: `cond`=`Use External != 0`
  - The filepath to the texture.
- **Image Data** (`Ref`)
  - Attributes: `cond`=`Use External == 0`, `template`=`NiRawImageData`
  - Link to the internally stored image data.
- **Unknown Int** (`uint`)
  - Attributes: `default`=`7`
- **Unknown Float** (`float`)
  - Attributes: `default`=`128.5`, `since`=`3.1`

