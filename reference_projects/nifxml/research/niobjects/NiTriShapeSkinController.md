# Niobject `NiTriShapeSkinController`

Old version of skinning instance.

## Attributes
- **inherit**: `NiTimeController`
- **module**: `NiLegacy`
- **name**: `NiTriShapeSkinController`
- **until**: `V10_0_1_0`

## Fields
- **Num Bones** (`uint`)
  - The number of node bones referenced as influences.
- **Vertex Counts** (`uint`)
  - Attributes: `length`=`Num Bones`
  - The number of vertex weights stored for each bone.
- **Bones** (`Ptr`)
  - Attributes: `length`=`Num Bones`, `template`=`NiBone`
  - List of all armature bones.
- **Bone Data** (`OldSkinData`)
  - Attributes: `length`=`Num Bones`, `width`=`Vertex Counts`
  - Contains skin weight data for each node that this skin is influenced by.

