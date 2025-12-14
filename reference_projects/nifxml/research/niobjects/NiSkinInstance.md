# Niobject `NiSkinInstance`

Skinning instance.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiSkinInstance`

## Fields
- **Data** (`Ref`)
  - Attributes: `template`=`NiSkinData`
  - Skinning data reference.
- **Skin Partition** (`Ref`)
  - Attributes: `since`=`10.1.0.101`, `template`=`NiSkinPartition`
  - Refers to a NiSkinPartition objects, which partitions the mesh such that every vertex is only influenced by a limited number of bones.
- **Skeleton Root** (`Ptr`)
  - Attributes: `template`=`NiNode`
  - Armature root node.
- **Num Bones** (`uint`)
  - The number of node bones referenced as influences.
- **Bones** (`Ptr`)
  - Attributes: `length`=`Num Bones`, `template`=`NiNode`
  - List of all armature bones.

