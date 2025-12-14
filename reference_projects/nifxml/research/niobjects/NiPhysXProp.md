# Niobject `NiPhysXProp`

A PhysX prop which holds information about PhysX actors in a Gamebryo scene

## Attributes
- **inherit**: `NiObjectNET`
- **module**: `NiPhysX`
- **name**: `NiPhysXProp`
- **since**: `V20_2_0_8`

## Fields
- **PhysX to World Scale** (`float`)
  - Attributes: `default`=`1.0`
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
- **Temp Name** (`NiFixedString`)
  - Attributes: `since`=`30.1.0.2`, `until`=`30.2.0.2`
- **Keep Meshes** (`bool`)
- **Snapshot** (`Ref`)
  - Attributes: `template`=`NiPhysXPropDesc`

