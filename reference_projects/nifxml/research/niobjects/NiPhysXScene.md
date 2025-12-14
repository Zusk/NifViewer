# Niobject `NiPhysXScene`

Object which manages a NxScene object and the Gamebryo objects that interact with it.

## Attributes
- **inherit**: `NiObjectNET`
- **module**: `NiPhysX`
- **name**: `NiPhysXScene`
- **since**: `V20_2_0_8`

## Fields
- **Scene Transform** (`NiTransform`)
- **PhysX to World Scale** (`float`)
  - Attributes: `default`=`1.0`
- **Num Props** (`uint`)
  - Attributes: `since`=`20.3.0.2`
- **Props** (`Ref`)
  - Attributes: `length`=`Num Props`, `since`=`20.3.0.2`, `template`=`NiPhysXProp`
- **Num Sources** (`uint`)
- **Sources** (`Ref`)
  - Attributes: `length`=`Num Sources`, `template`=`NiPhysXSrc`
- **Num Dests** (`uint`)
- **Dests** (`Ref`)
  - Attributes: `length`=`Num Dests`, `template`=`NiPhysXDest`
- **Num Modified Meshes** (`uint`)
  - Attributes: `since`=`20.4.0.0`
- **Modified Meshes** (`Ref`)
  - Attributes: `length`=`Num Modified Meshes`, `since`=`20.4.0.0`, `template`=`NiMesh`
- **Time Step** (`float`)
  - Attributes: `default`=`0.016666`, `since`=`20.2.0.8`
- **Keep Meshes** (`bool`)
  - Attributes: `since`=`20.2.0.8`, `until`=`20.3.0.1`
- **Num Sub Steps** (`uint`)
  - Attributes: `default`=`1`, `since`=`20.3.0.9`
- **Max Sub Steps** (`uint`)
  - Attributes: `default`=`8`, `since`=`20.3.0.9`
- **Snapshot** (`Ref`)
  - Attributes: `template`=`NiPhysXSceneDesc`
- **Flags** (`ushort`)

