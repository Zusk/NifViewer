# Struct `QQSpeedLODEntry`

## Attributes
- **name**: `QQSpeedLODEntry`

## Fields
- **Unknown Bytes** (`byte`)
  - Attributes: `length`=`12`
  - Seemingly always zeros.
- **Num Levels** (`uint`)
  - Always 3 in tested mesh.
- **Unknown Values** (`float`)
  - Attributes: `length`=`Num Levels`
  - Middle entry seemingly always 10^8, other two the same as LOD Distances
- **LOD Distances** (`float`)
  - Attributes: `length`=`Num Levels`
  - Entries corresponding to LODDistance specification in the block's NiStringExtraData, in the order of 1, 3, 2

