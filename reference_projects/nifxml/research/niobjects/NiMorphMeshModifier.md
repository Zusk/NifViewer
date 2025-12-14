# Niobject `NiMorphMeshModifier`

Performs linear-weighted blending between a set of target data streams.

## Attributes
- **inherit**: `NiMeshModifier`
- **module**: `NiMesh`
- **name**: `NiMorphMeshModifier`
- **since**: `V20_5_0_0`

## Fields
- **Flags** (`byte`)
  - FLAG_RELATIVETARGETS = 0x01
FLAG_UPDATENORMALS   = 0x02
FLAG_NEEDSUPDATE     = 0x04
FLAG_ALWAYSUPDATE    = 0x08
FLAG_NEEDSCOMPLETION = 0x10
FLAG_SKINNED         = 0x20
FLAG_SWSKINNED       = 0x40
- **Num Targets** (`ushort`)
  - The number of morph targets.
- **Num Elements** (`uint`)
  - The number of morphing data stream elements.
- **Elements** (`ElementReference`)
  - Attributes: `length`=`Num Elements`
  - Semantics and normalization of the morphing data stream elements.

