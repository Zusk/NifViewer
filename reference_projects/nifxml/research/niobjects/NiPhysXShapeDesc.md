# Niobject `NiPhysXShapeDesc`

For serializing NxShapeDesc objects

## Attributes
- **inherit**: `NiObject`
- **module**: `NiPhysX`
- **name**: `NiPhysXShapeDesc`
- **since**: `V20_2_0_8`

## Fields
- **Shape Type** (`NxShapeType`)
- **Local Pose** (`Matrix34`)
- **Flags** (`NxShapeFlag`)
  - Attributes: `default`=`0x120008`
- **Collision Group** (`ushort`)
- **Material Index** (`ushort`)
- **Density** (`float`)
  - Attributes: `default`=`1.0`
- **Mass** (`float`)
  - Attributes: `default`=`-1.0`
- **Skin Width** (`float`)
  - Attributes: `default`=`-1.0`
- **Shape Name** (`NiFixedString`)
- **Non Interacting Compartment Types** (`uint`)
  - Attributes: `since`=`20.4.0.0`
- **Collision Bits** (`uint`)
  - Attributes: `length`=`4`
- **Plane** (`NxPlane`)
  - Attributes: `cond`=`Shape Type == 0`
- **Sphere Radius** (`float`)
  - Attributes: `cond`=`Shape Type == 1`
- **Box Half Extents** (`Vector3`)
  - Attributes: `cond`=`Shape Type == 2`
- **Capsule** (`NxCapsule`)
  - Attributes: `cond`=`Shape Type == 3`
- **Mesh** (`Ref`)
  - Attributes: `cond`=`(Shape Type == 5) #OR# (Shape Type == 6)`, `template`=`NiPhysXMeshDesc`

