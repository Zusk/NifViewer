# Niobject `BSWaterShaderProperty`

Skyrim water shader property, different from "WaterShaderProperty" seen in Fallout.

## Attributes
- **inherit**: `BSShaderProperty`
- **module**: `BSMain`
- **name**: `BSWaterShaderProperty`
- **versions**: `#SKY_AND_LATER#`

## Fields
- **Shader Flags 1** (`SkyrimShaderPropertyFlags1`)
  - Attributes: `default`=`0x80000008`, `vercond`=`!#BS_GTE_132#`
- **Shader Flags 2** (`SkyrimShaderPropertyFlags2`)
  - Attributes: `default`=`0x21`, `vercond`=`!#BS_GTE_132#`
- **Num SF1** (`uint`)
  - Attributes: `vercond`=`#BS_GTE_132#`
- **Num SF2** (`uint`)
  - Attributes: `vercond`=`#BS_GTE_152#`
- **SF1** (`BSShaderCRC32`)
  - Attributes: `length`=`Num SF1`, `vercond`=`#BS_GTE_132#`
- **SF2** (`BSShaderCRC32`)
  - Attributes: `length`=`Num SF2`, `vercond`=`#BS_GTE_152#`
- **UV Offset** (`TexCoord`)
  - Offset UVs. Seems to be unused, but it fits with the other Skyrim shader properties.
- **UV Scale** (`TexCoord`)
  - Attributes: `default`=`#VEC2_ONE#`
  - Offset UV Scale to repeat tiling textures, see above.
- **Water Shader Flags** (`WaterShaderPropertyFlags`)
  - Attributes: `default`=`0xC4`

