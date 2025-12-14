# Niobject `NiPhysXActorDesc`

For serializing NxActor objects.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiPhysX`
- **name**: `NiPhysXActorDesc`
- **since**: `V20_2_0_8`

## Fields
- **Actor Name** (`NiFixedString`)
- **Num Poses** (`uint`)
- **Poses** (`Matrix34`)
  - Attributes: `length`=`Num Poses`
- **Body Desc** (`Ref`)
  - Attributes: `template`=`NiPhysXBodyDesc`
- **Density** (`float`)
- **Actor Flags** (`uint`)
- **Actor Group** (`ushort`)
- **Dominance Group** (`ushort`)
  - Attributes: `since`=`20.4.0.0`
- **Contact Report Flags** (`uint`)
  - Attributes: `since`=`20.4.0.0`
- **Force Field Material** (`ushort`)
  - Attributes: `since`=`20.4.0.0`
- **Dummy** (`uint`)
  - Attributes: `since`=`20.3.0.1`, `until`=`20.3.0.5`
- **Num Shape Descs** (`uint`)
- **Shape Descriptions** (`Ref`)
  - Attributes: `length`=`Num Shape Descs`, `template`=`NiPhysXShapeDesc`
- **Actor Parent** (`Ref`)
  - Attributes: `template`=`NiPhysXActorDesc`
- **Source** (`Ref`)
  - Attributes: `template`=`NiPhysXRigidBodySrc`
- **Dest** (`Ref`)
  - Attributes: `template`=`NiPhysXRigidBodyDest`

