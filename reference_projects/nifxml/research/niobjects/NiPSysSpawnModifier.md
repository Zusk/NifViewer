# Niobject `NiPSysSpawnModifier`

Particle modifier that spawns additional copies of a particle.

## Attributes
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysSpawnModifier`

## Fields
- **Num Spawn Generations** (`ushort`)
  - Attributes: `default`=`0`
  - Number of allowed generations for spawning. Particles whose generations are >= will not be spawned.
- **Percentage Spawned** (`float`)
  - Attributes: `default`=`1.0`
  - The likelihood of a particular particle being spawned. Must be between 0.0 and 1.0.
- **Min Num to Spawn** (`ushort`)
  - Attributes: `default`=`1`
  - The minimum particles to spawn for any given original particle.
- **Max Num to Spawn** (`ushort`)
  - Attributes: `default`=`1`
  - The maximum particles to spawn for any given original particle.
- **Spawn Speed Variation** (`float`)
  - How much the spawned particle speed can vary.
- **Spawn Dir Variation** (`float`)
  - How much the spawned particle direction can vary.
- **Life Span** (`float`)
  - Lifespan assigned to spawned particles.
- **Life Span Variation** (`float`)
  - The amount the lifespan can vary.
- **WorldShift Spawn Speed Addition** (`float`)
  - Attributes: `since`=`10.2.0.1`, `until`=`10.4.0.1`

