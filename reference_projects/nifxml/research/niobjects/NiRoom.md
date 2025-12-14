# Niobject `NiRoom`

NiRoom objects represent cells in a cell-portal culling system.

## Attributes
- **inherit**: `NiNode`
- **module**: `NiMain`
- **name**: `NiRoom`

## Fields
- **Num Walls** (`uint`)
- **Walls** (`Ref`)
  - Attributes: `length`=`Num Walls`, `template`=`NiWall`, `until`=`3.3.0.13`
- **Wall Planes** (`NiPlane`)
  - Attributes: `length`=`Num Walls`, `since`=`4.0.0.0`
- **Num In Portals** (`uint`)
- **In Portals** (`Ptr`)
  - Attributes: `length`=`Num In Portals`, `template`=`NiPortal`
  - The portals which see into the room.
- **Num Out Portals** (`uint`)
- **Out Portals** (`Ptr`)
  - Attributes: `length`=`Num Out Portals`, `template`=`NiPortal`
  - The portals which see out of the room.
- **Num Fixtures** (`uint`)
- **Fixtures** (`Ptr`)
  - Attributes: `length`=`Num Fixtures`, `template`=`NiAVObject`
  - All geometry associated with the room. Seems to be Ref for legacy.

