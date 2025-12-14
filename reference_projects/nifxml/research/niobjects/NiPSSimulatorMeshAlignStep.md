# Niobject `NiPSSimulatorMeshAlignStep`

Encapsulates a floodgate kernel that updates mesh particle alignment and transforms.

## Attributes
- **inherit**: `NiPSSimulatorStep`
- **module**: `NiPSParticle`
- **name**: `NiPSSimulatorMeshAlignStep`
- **since**: `V20_5_0_0`

## Fields
- **Num Rotation Keys** (`byte`)
- **Rotation Keys** (`QuatKey`)
  - Attributes: `arg`=`1`, `length`=`Num Rotation Keys`, `template`=`Quaternion`
  - The particle rotation keys.
- **Rotation Loop Behavior** (`PSLoopBehavior`)
  - The loop behavior for the rotation keys.

