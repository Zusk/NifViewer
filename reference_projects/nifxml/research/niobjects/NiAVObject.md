# Niobject `NiAVObject`

Abstract audio-visual base class from which all of Gamebryo's scene graph objects inherit.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObjectNET`
- **module**: `NiMain`
- **name**: `NiAVObject`

## Fields
- **Flags** (`uint`)
  - Attributes: `default`=`0x8000E`, `vercond`=`#BSVER# #GT# 26`
  - Basic flags for AV objects. For Bethesda streams above 26 only.
ALL: FO4 lacks the 0x80000 flag always. Skyrim lacks it sometimes.
BSTreeNode: 0x8080E (pre-FO4), 0x400E (FO4)
BSLeafAnimNode: 0x808000E (pre-FO4), 0x500E (FO4)
BSDamageStage, BSBlastNode: 0x8000F (pre-FO4), 0x2000000F (FO4)
- **Flags** (`ushort`)
  - Attributes: `since`=`3.0`, `vercond`=`#BSVER# #LTE# 26`
  - Basic flags for AV objects.
- **Translation** (`Vector3`)
  - The translation vector.
- **Rotation** (`Matrix33`)
  - The rotation part of the transformation matrix.
- **Scale** (`float`)
  - Attributes: `default`=`1.0`
  - Scaling part (only uniform scaling is supported).
- **Velocity** (`Vector3`)
  - Attributes: `until`=`4.2.2.0`
  - Unknown function. Always seems to be (0, 0, 0)
- **Num Properties** (`uint`)
  - Attributes: `vercond`=`#NI_BS_LTE_FO3#`
- **Properties** (`Ref`)
  - Attributes: `length`=`Num Properties`, `template`=`NiProperty`, `vercond`=`#NI_BS_LTE_FO3#`
  - All rendering properties attached to this object.
- **Unknown 1** (`uint`)
  - Attributes: `length`=`4`, `until`=`2.3`
- **Unknown 2** (`byte`)
  - Attributes: `until`=`2.3`
- **Has Bounding Volume** (`bool`)
  - Attributes: `since`=`3.0`, `until`=`4.2.2.0`
- **Bounding Volume** (`BoundingVolume`)
  - Attributes: `cond`=`Has Bounding Volume`, `since`=`3.0`, `until`=`4.2.2.0`
- **Collision Object** (`Ref`)
  - Attributes: `since`=`10.0.1.0`, `template`=`NiCollisionObject`

