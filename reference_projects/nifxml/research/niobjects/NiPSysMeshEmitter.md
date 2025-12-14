# Niobject `NiPSysMeshEmitter`

Particle emitter that uses points on a specified mesh to emit from.

## Attributes
- **inherit**: `NiPSysEmitter`
- **module**: `NiParticle`
- **name**: `NiPSysMeshEmitter`

## Fields
- **Num Emitter Meshes** (`uint`)
- **Emitter Meshes** (`Ptr`)
  - Attributes: `length`=`Num Emitter Meshes`, `template`=`NiAVObject`
  - The meshes which are emitted from.
- **Initial Velocity Type** (`VelocityType`)
  - The method by which the initial particle velocity will be computed.
- **Emission Type** (`EmitFrom`)
  - The manner in which particles are emitted from the Emitter Meshes.
- **Emission Axis** (`Vector3`)
  - Attributes: `default`=`#X_AXIS#`
  - The emission axis if VELOCITY_USE_DIRECTION.

