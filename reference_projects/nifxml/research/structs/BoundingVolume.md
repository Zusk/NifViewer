# Struct `BoundingVolume`

## Attributes
- **module**: `NiMain`
- **name**: `BoundingVolume`

## Fields
- **Collision Type** (`BoundVolumeType`)
  - Type of collision data.
- **Sphere** (`NiBound`)
  - Attributes: `cond`=`Collision Type == 0`
- **Box** (`BoxBV`)
  - Attributes: `cond`=`Collision Type == 1`
- **Capsule** (`CapsuleBV`)
  - Attributes: `cond`=`Collision Type == 2`
- **Union BV** (`UnionBV`)
  - Attributes: `cond`=`Collision Type == 4`, `recursive`=`True`
- **Half Space** (`HalfSpaceBV`)
  - Attributes: `cond`=`Collision Type == 5`

