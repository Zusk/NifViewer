# Niobject `NiPSSimulatorGeneralStep`

Encapsulates a floodgate kernel that updates particle size, colors, and rotations.

## Attributes
- **inherit**: `NiPSSimulatorStep`
- **module**: `NiPSParticle`
- **name**: `NiPSSimulatorGeneralStep`
- **since**: `V20_5_0_0`

## Fields
- **Num Size Keys** (`byte`)
  - Attributes: `since`=`20.6.1.0`
- **Size Keys** (`Key`)
  - Attributes: `arg`=`1`, `length`=`Num Size Keys`, `since`=`20.6.1.0`, `template`=`float`
  - The particle size keys.
- **Size Loop Behavior** (`PSLoopBehavior`)
  - Attributes: `default`=`PS_LOOP_AGESCALE`, `since`=`20.6.1.0`
  - The loop behavior for the size keys.
- **Num Color Keys** (`byte`)
- **Color Keys** (`Key`)
  - Attributes: `arg`=`1`, `length`=`Num Color Keys`, `template`=`ByteColor4`
  - The particle color keys.
- **Color Loop Behavior** (`PSLoopBehavior`)
  - Attributes: `default`=`PS_LOOP_AGESCALE`, `since`=`20.6.1.0`
  - The loop behavior for the color keys.
- **Num Rotation Keys** (`byte`)
  - Attributes: `since`=`20.6.1.0`
- **Rotation Keys** (`QuatKey`)
  - Attributes: `arg`=`1`, `length`=`Num Rotation Keys`, `since`=`20.6.1.0`, `template`=`Quaternion`
  - The particle rotation keys.
- **Rotation Loop Behavior** (`PSLoopBehavior`)
  - Attributes: `default`=`PS_LOOP_AGESCALE`, `since`=`20.6.1.0`
  - The loop behavior for the rotation keys.
- **Grow Time** (`float`)
  - The the amount of time over which a particle's size is ramped from 0.0 to 1.0 in seconds
- **Shrink Time** (`float`)
  - The the amount of time over which a particle's size is ramped from 1.0 to 0.0 in seconds
- **Grow Generation** (`ushort`)
  - Specifies the particle generation to which the grow effect should be applied. This is usually generation 0, so that newly created particles will grow.
- **Shrink Generation** (`ushort`)
  - Specifies the particle generation to which the shrink effect should be applied. This is usually the highest supported generation for the particle system, so that particles will shrink immediately before getting killed.
- **DEM Unknown Byte** (`byte`)
  - Attributes: `since`=`20.6.5.0`, `until`=`20.6.5.0`, `vercond`=`#USER# #GTE# 14`

