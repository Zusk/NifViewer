# Niobject `NiPSysEmitter`

Abstract base class for all particle system emitters.

## Attributes
- **abstract**: `true`
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysEmitter`

## Fields
- **Speed** (`float`)
  - Speed / Inertia of particle movement.
- **Speed Variation** (`float`)
  - Adds an amount of randomness to Speed.
- **Declination** (`float`)
  - Declination / First axis.
- **Declination Variation** (`float`)
  - Declination randomness / First axis.
- **Planar Angle** (`float`)
  - Planar Angle / Second axis.
- **Planar Angle Variation** (`float`)
  - Planar Angle randomness / Second axis .
- **Initial Color** (`Color4`)
  - Attributes: `default`=`#VEC4_ONE#`
  - Defines color of a birthed particle.
- **Initial Radius** (`float`)
  - Attributes: `default`=`1.0`
  - Size of a birthed particle.
- **Radius Variation** (`float`)
  - Attributes: `since`=`10.4.0.1`
  - Particle Radius randomness.
- **Life Span** (`float`)
  - Duration until a particle dies.
- **Life Span Variation** (`float`)
  - Adds randomness to Life Span.
- **Unknown QQSpeed Floats** (`float`)
  - Attributes: `length`=`2`
  - Both 1.0 in example nif.

