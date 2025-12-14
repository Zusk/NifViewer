# Niobject `NiPSysGravityModifier`

Particle modifier that applies a gravitational force to particles.

## Attributes
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysGravityModifier`

## Fields
- **Gravity Object** (`Ptr`)
  - Attributes: `template`=`NiAVObject`
  - The object whose position and orientation are the basis of the force.
- **Gravity Axis** (`Vector3`)
  - Attributes: `default`=`#X_AXIS#`
  - The local direction of the force.
- **Decay** (`float`)
  - How the force diminishes by distance.
- **Strength** (`float`)
  - Attributes: `default`=`1.0`
  - The acceleration of the force.
- **Force Type** (`ForceType`)
  - The type of gravitational force.
- **Turbulence** (`float`)
  - Adds a degree of randomness.
- **Turbulence Scale** (`float`)
  - Attributes: `default`=`1.0`
  - Scale for turbulence.
- **World Aligned** (`bool`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`

