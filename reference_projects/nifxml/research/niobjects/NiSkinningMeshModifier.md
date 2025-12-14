# Niobject `NiSkinningMeshModifier`

## Attributes
- **inherit**: `NiMeshModifier`
- **module**: `NiMesh`
- **name**: `NiSkinningMeshModifier`
- **since**: `V20_5_0_0`

## Fields
- **Flags** (`ushort`)
  - USE_SOFTWARE_SKINNING = 0x0001
RECOMPUTE_BOUNDS = 0x0002
- **Skeleton Root** (`Ptr`)
  - Attributes: `template`=`NiAVObject`
  - The root bone of the skeleton.
- **Skeleton Transform** (`NiTransform`)
  - The transform that takes the root bone parent coordinate system into the skin coordinate system.
- **Num Bones** (`uint`)
  - The number of bones referenced by this mesh modifier.
- **Bones** (`Ptr`)
  - Attributes: `length`=`Num Bones`, `template`=`NiAVObject`
  - Pointers to the bone nodes that affect this skin.
- **Bone Transforms** (`NiTransform`)
  - Attributes: `length`=`Num Bones`
  - The transforms that go from bind-pose space to bone space.
- **Bone Bounds** (`NiBound`)
  - Attributes: `cond`=`(Flags #BITAND# 2) != 0`, `length`=`Num Bones`
  - The bounds of the bones.  Only stored if the RECOMPUTE_BOUNDS bit is set.

