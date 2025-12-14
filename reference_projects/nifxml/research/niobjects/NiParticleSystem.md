# Niobject `NiParticleSystem`

A particle system.
Contains members to mimic inheritance shifts for Bethesda 20.2, where NiParticles switched to inheriting BSGeometry.
Until inheritance shifts are supported, the members are on NiParticleSystem instead of NiParticles for module reasons.

## Attributes
- **inherit**: `NiParticles`
- **module**: `NiParticle`
- **name**: `NiParticleSystem`

## Fields
- **Vertex Desc** (`BSVertexDesc`)
  - Attributes: `vercond`=`#BS_GTE_SSE#`
- **Far Begin** (`ushort`)
  - Attributes: `vercond`=`#BS_GTE_SKY#`
- **Far End** (`ushort`)
  - Attributes: `vercond`=`#BS_GTE_SKY#`
- **Near Begin** (`ushort`)
  - Attributes: `vercond`=`#BS_GTE_SKY#`
- **Near End** (`ushort`)
  - Attributes: `vercond`=`#BS_GTE_SKY#`
- **Data** (`Ref`)
  - Attributes: `template`=`NiPSysData`, `vercond`=`#BS_GTE_SSE#`
- **World Space** (`bool`)
  - Attributes: `default`=`true`, `since`=`10.1.0.0`
  - If true, Particles are birthed into world space.  If false, Particles are birthed into object space.
- **Num Modifiers** (`uint`)
  - Attributes: `since`=`10.1.0.0`
  - The number of modifier references.
- **Modifiers** (`Ref`)
  - Attributes: `length`=`Num Modifiers`, `since`=`10.1.0.0`, `template`=`NiPSysModifier`
  - The list of particle modifiers.

