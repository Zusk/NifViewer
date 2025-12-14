# Niobject `NiSkinData`

Skinning data.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiSkinData`

## Fields
- **Skin Transform** (`NiTransform`)
  - Offset of the skin from this bone in bind position.
- **Num Bones** (`uint`)
  - Number of bones.
- **Skin Partition** (`Ref`)
  - Attributes: `since`=`4.0.0.2`, `template`=`NiSkinPartition`, `until`=`10.1.0.0`
  - This optionally links a NiSkinPartition for hardware-acceleration information.
- **Has Vertex Weights** (`bool`)
  - Attributes: `default`=`true`, `since`=`4.2.1.0`
  - Enables Vertex Weights for this NiSkinData.
- **Bone List** (`BoneData`)
  - Attributes: `arg`=`Has Vertex Weights`, `length`=`Num Bones`
  - Contains offset data for each node that this skin is influenced by.

