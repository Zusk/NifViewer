# Struct `NiParticleInfo`

Called NiPerParticleData in NiOldParticles.
Holds the state of a particle at the time the system was saved.

## Attributes
- **module**: `NiMain`
- **name**: `NiParticleInfo`
- **size**: `40`

## Fields
- **Velocity** (`Vector3`)
  - Particle direction and speed.
- **Rotation Axis** (`Vector3`)
  - Attributes: `until`=`10.4.0.1`
- **Age** (`float`)
- **Life Span** (`float`)
- **Last Update** (`float`)
  - Timestamp of the last update.
- **Spawn Generation** (`ushort`)
  - Attributes: `default`=`0`
- **Code** (`ushort`)
  - Usually matches array index

