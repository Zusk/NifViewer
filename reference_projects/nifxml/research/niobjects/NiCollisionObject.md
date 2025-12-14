# Niobject `NiCollisionObject`

This is the most common collision object found in NIF files. It acts as a real object that
is visible and possibly (if the body allows for it) interactive. The node itself
is simple, it only has three properties.
For this type of collision object, bhkRigidBody or bhkRigidBodyT is generally used.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiCollisionObject`

## Fields
- **Target** (`Ptr`)
  - Attributes: `template`=`NiAVObject`
  - Index of the AV object referring to this collision object.

