# Niobject `NiPhysXPSParticleSystem`

Implements Gamebryo particle systems that use PhysX actors for the particles

## Attributes
- **inherit**: `NiPSParticleSystem`
- **module**: `NiPhysX`
- **name**: `NiPhysXPSParticleSystem`
- **since**: `V20_5_0_0`

## Fields
- **Prop** (`Ptr`)
  - Attributes: `template`=`NiPhysXPSParticleSystemProp`
- **Dest** (`Ptr`)
  - Attributes: `template`=`NiPhysXPSParticleSystemDest`
- **Scene** (`Ptr`)
  - Attributes: `template`=`NiPhysXScene`
- **PhysX Flags** (`byte`)
- **Default Actor Pool Size** (`uint`)
- **Generation Pool Size** (`uint`)
- **Actor Pool Center** (`Vector3`)
- **Actor Pool Dimensions** (`Vector3`)
- **Actor** (`Ptr`)
  - Attributes: `template`=`NiPhysXActorDesc`

