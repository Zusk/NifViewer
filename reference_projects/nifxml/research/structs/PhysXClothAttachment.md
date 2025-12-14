# Struct `PhysXClothAttachment`

## Attributes
- **module**: `NiPhysX`
- **name**: `PhysXClothAttachment`
- **since**: `V20_2_0_8`

## Fields
- **Shape** (`Ref`)
  - Attributes: `template`=`NiPhysXShapeDesc`
- **Num Vertices** (`uint`)
- **Flags** (`uint`)
  - Attributes: `cond`=`Num Vertices == 0`
- **Positions** (`PhysXClothAttachmentPosition`)
  - Attributes: `cond`=`Num Vertices #GT# 0`, `length`=`Num Vertices`

