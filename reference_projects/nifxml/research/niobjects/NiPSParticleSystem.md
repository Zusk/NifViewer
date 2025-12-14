# Niobject `NiPSParticleSystem`

Represents a particle system.

## Attributes
- **inherit**: `NiMesh`
- **module**: `NiPSParticle`
- **name**: `NiPSParticleSystem`
- **since**: `V20_5_0_0`

## Fields
- **Simulator** (`Ref`)
  - Attributes: `template`=`NiPSSimulator`
- **Generator** (`Ref`)
  - Attributes: `template`=`NiPSBoundUpdater`
- **Num Emitters** (`uint`)
- **Emitters** (`Ref`)
  - Attributes: `length`=`Num Emitters`, `template`=`NiPSEmitter`
- **Num Spawners** (`uint`)
- **Spawners** (`Ref`)
  - Attributes: `length`=`Num Spawners`, `template`=`NiPSSpawner`
- **Death Spawner** (`Ref`)
  - Attributes: `template`=`NiPSSpawner`
- **Max Num Particles** (`uint`)
- **Has Colors** (`bool`)
- **Has Rotations** (`bool`)
- **Has Rotation Axes** (`bool`)
- **Has Animated Textures** (`bool`)
  - Attributes: `since`=`20.6.1.0`
- **World Space** (`bool`)
  - Attributes: `default`=`true`
- **Normal Method** (`AlignMethod`)
  - Attributes: `default`=`ALIGN_CAMERA`, `since`=`20.6.1.0`
- **Normal Direction** (`Vector3`)
  - Attributes: `since`=`20.6.1.0`
- **Up Method** (`AlignMethod`)
  - Attributes: `default`=`ALIGN_CAMERA`, `since`=`20.6.1.0`
- **Up Direction** (`Vector3`)
  - Attributes: `since`=`20.6.1.0`
- **Living Spawner** (`Ref`)
  - Attributes: `since`=`20.6.1.0`, `template`=`NiPSSpawner`
- **Num Spawn Rate Keys** (`byte`)
  - Attributes: `since`=`20.6.1.0`
- **Spawn Rate Keys** (`PSSpawnRateKey`)
  - Attributes: `length`=`Num Spawn Rate Keys`, `since`=`20.6.1.0`
- **Pre RPI** (`bool`)
  - Attributes: `since`=`20.6.1.0`, `vercond`=`#USER# #EQ# 0 #OR# (#VER# #EQ# 20.6.5.0 #AND# #USER# #GTE# 11)`
- **DEM Unknown Int** (`uint`)
  - Attributes: `since`=`20.6.5.0`, `until`=`20.6.5.0`, `vercond`=`#USER# #GTE# 11`
- **DEM Unknown Byte** (`byte`)
  - Attributes: `since`=`20.6.5.0`, `until`=`20.6.5.0`, `vercond`=`#USER# #GTE# 11`

