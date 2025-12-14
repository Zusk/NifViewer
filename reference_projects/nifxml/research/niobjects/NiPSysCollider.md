# Niobject `NiPSysCollider`

Particle system collider.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiParticle`
- **name**: `NiPSysCollider`

## Fields
- **Bounce** (`float`)
  - Attributes: `default`=`1.0`
  - Amount of bounce for the collider.
- **Spawn on Collide** (`bool`)
  - Spawn particles on impact?
- **Die on Collide** (`bool`)
  - Kill particles on impact?
- **Spawn Modifier** (`Ref`)
  - Attributes: `template`=`NiPSysSpawnModifier`
  - Spawner to use for the collider.
- **Parent** (`Ptr`)
  - Attributes: `template`=`NiPSysColliderManager`
  - Link to parent.
- **Next Collider** (`Ref`)
  - Attributes: `template`=`NiPSysCollider`
  - The next collider.
- **Collider Object** (`Ptr`)
  - Attributes: `template`=`NiAVObject`
  - The object whose position and orientation are the basis of the collider.

