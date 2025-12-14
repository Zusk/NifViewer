# Niobject `NiPSysDragModifier`

Particle modifier that applies a linear drag force to particles.

## Attributes
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysDragModifier`

## Fields
- **Drag Object** (`Ptr`)
  - Attributes: `template`=`NiAVObject`
  - The object whose position and orientation are the basis of the force.
- **Drag Axis** (`Vector3`)
  - Attributes: `default`=`#X_AXIS#`
  - The local direction of the force.
- **Percentage** (`float`)
  - Attributes: `default`=`0.05`
  - The amount of drag to apply to particles.
- **Range** (`float`)
  - Attributes: `default`=`#FLT_MAX#`
  - The distance up to which particles are fully affected.
- **Range Falloff** (`float`)
  - Attributes: `default`=`#FLT_MAX#`
  - The distance at which particles cease to be affected.

