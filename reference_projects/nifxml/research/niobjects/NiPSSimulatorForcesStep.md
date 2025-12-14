# Niobject `NiPSSimulatorForcesStep`

Encapsulates a floodgate kernel that simulates particle forces.

## Attributes
- **inherit**: `NiPSSimulatorStep`
- **module**: `NiPSParticle`
- **name**: `NiPSSimulatorForcesStep`
- **since**: `V20_5_0_0`

## Fields
- **Num Forces** (`uint`)
- **Forces** (`Ref`)
  - Attributes: `length`=`Num Forces`, `template`=`NiPSForce`
  - The forces affecting the particle system.

