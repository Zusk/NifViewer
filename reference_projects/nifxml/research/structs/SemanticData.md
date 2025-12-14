# Struct `SemanticData`

## Attributes
- **module**: `NiMesh`
- **name**: `SemanticData`
- **since**: `V20_5_0_0`
- **size**: `8`

## Fields
- **Name** (`NiFixedString`)
  - Type of data (POSITION, POSITION_BP, INDEX, NORMAL, NORMAL_BP,
TEXCOORD, BLENDINDICES, BLENDWEIGHT, BONE_PALETTE, COLOR, DISPLAYLIST,
MORPH_POSITION, BINORMAL_BP, TANGENT, TANGENT_BP).
- **Index** (`uint`)
  - Attributes: `default`=`0`
  - An extra index of the data. For example, if there are 3 uv maps,
then the corresponding TEXCOORD data components would have indices
0, 1, and 2, respectively.

