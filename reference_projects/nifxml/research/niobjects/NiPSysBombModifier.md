# Niobject `NiPSysBombModifier`

Particle modifier that applies an explosive force to particles.

## Attributes
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysBombModifier`

## Fields
- **Bomb Object** (`Ptr`)
  - Attributes: `template`=`NiNode`
  - The object whose position and orientation are the basis of the force.
- **Bomb Axis** (`Vector3`)
  - The local direction of the force.
- **Decay** (`float`)
  - How the bomb force will decrease with distance.
- **Delta V** (`float`)
  - The acceleration the bomb will apply to particles.
- **Decay Type** (`DecayType`)
- **Symmetry Type** (`SymmetryType`)

