# Niobject `NiRoomGroup`

NiRoomGroup represents a set of connected rooms i.e. a game level.

## Attributes
- **inherit**: `NiNode`
- **module**: `NiMain`
- **name**: `NiRoomGroup`

## Fields
- **Shell** (`Ptr`)
  - Attributes: `template`=`NiNode`
  - Object that represents the room group as seen from the outside.
- **Num Rooms** (`uint`)
- **Rooms** (`Ptr`)
  - Attributes: `length`=`Num Rooms`, `template`=`NiRoom`

