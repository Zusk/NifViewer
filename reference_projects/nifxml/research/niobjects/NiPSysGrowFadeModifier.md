# Niobject `NiPSysGrowFadeModifier`

Particle modifier that controls the time it takes to grow and shrink a particle.

## Attributes
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysGrowFadeModifier`

## Fields
- **Grow Time** (`float`)
  - The time taken to grow from 0 to their specified size.
- **Grow Generation** (`ushort`)
  - Specifies the particle generation to which the grow effect should be applied. This is usually generation 0, so that newly created particles will grow.
- **Fade Time** (`float`)
  - The time taken to shrink from their specified size to 0.
- **Fade Generation** (`ushort`)
  - Specifies the particle generation to which the shrink effect should be applied. This is usually the highest supported generation for the particle system.
- **Base Scale** (`float`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_FO3#`
  - A multiplier on the base particle scale.

