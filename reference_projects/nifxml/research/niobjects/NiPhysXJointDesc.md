# Niobject `NiPhysXJointDesc`

A PhysX Joint abstract base class.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiPhysX`
- **name**: `NiPhysXJointDesc`
- **since**: `V20_2_0_8`

## Fields
- **Joint Type** (`NxJointType`)
- **Joint Name** (`NiFixedString`)
- **Actors** (`NiPhysXJointActor`)
  - Attributes: `length`=`2`
- **Max Force** (`float`)
- **Max Torque** (`float`)
- **Solver Extrapolation Factor** (`float`)
  - Attributes: `since`=`20.5.0.3`
- **Use Acceleration Spring** (`uint`)
  - Attributes: `since`=`20.5.0.3`
- **Joint Flags** (`uint`)
- **Limit Point** (`Vector3`)
- **Num Limits** (`uint`)
- **Limits** (`NiPhysXJointLimit`)
  - Attributes: `length`=`Num Limits`

