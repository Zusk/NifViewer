# Niobject `NiPhysXPropDesc`

For serialization of PhysX objects and to attach them to the scene.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiPhysX`
- **name**: `NiPhysXPropDesc`
- **since**: `V20_2_0_8`

## Fields
- **Num Actors** (`uint`)
- **Actors** (`Ref`)
  - Attributes: `length`=`Num Actors`, `template`=`NiPhysXActorDesc`
- **Num Joints** (`uint`)
- **Joints** (`Ref`)
  - Attributes: `length`=`Num Joints`, `template`=`NiPhysXJointDesc`
- **Num Clothes** (`uint`)
  - Attributes: `since`=`20.3.0.5`
- **Clothes** (`Ref`)
  - Attributes: `length`=`Num Clothes`, `since`=`20.3.0.5`, `template`=`NiPhysXClothDesc`
- **Num Materials** (`uint`)
- **Materials** (`NiPhysXMaterialDescMap`)
  - Attributes: `length`=`Num Materials`
- **Num States** (`uint`)
- **State Names** (`NiTFixedStringMap`)
  - Attributes: `since`=`20.4.0.0`, `template`=`uint`
- **Flags** (`byte`)
  - Attributes: `since`=`20.4.0.0`

