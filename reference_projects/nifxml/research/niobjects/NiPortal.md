# Niobject `NiPortal`

NiPortal objects are grouping nodes that support aggressive visibility culling.
They represent flat polygonal regions through which a part of a scene graph can be viewed.

## Attributes
- **inherit**: `NiAVObject`
- **module**: `NiMain`
- **name**: `NiPortal`

## Fields
- **Portal Flags** (`ushort`)
- **Plane Count** (`ushort`)
  - Unused in 20.x, possibly also 10.x.
- **Num Vertices** (`ushort`)
- **Vertices** (`Vector3`)
  - Attributes: `length`=`Num Vertices`
- **Adjoiner** (`Ptr`)
  - Attributes: `template`=`NiNode`
  - Root of the scenegraph which is to be seen through this portal.

