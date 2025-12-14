# Niobject `NiPSMeshEmitter`

Emits particles from one or more NiMesh objects. A random mesh emitter is selected for each particle emission.

## Attributes
- **inherit**: `NiPSEmitter`
- **module**: `NiPSParticle`
- **name**: `NiPSMeshEmitter`
- **since**: `V20_5_0_0`

## Fields
- **Num Mesh Emitters** (`uint`)
- **Mesh Emitters** (`Ptr`)
  - Attributes: `length`=`Num Mesh Emitters`, `template`=`NiMesh`
- **Emit Axis** (`Vector3`)
  - Attributes: `until`=`20.6.0.0`
- **Emitter Object** (`Ptr`)
  - Attributes: `since`=`20.6.1.0`, `template`=`NiAVObject`
- **Mesh Emission Type** (`EmitFrom`)
- **Initial Velocity Type** (`VelocityType`)

