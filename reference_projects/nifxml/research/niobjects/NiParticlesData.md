# Niobject `NiParticlesData`

Generic rotating particles data object.
Bethesda 20.2.0.7 NIFs: NiParticlesData no longer inherits from NiGeometryData and inherits NiObject directly.

## Attributes
- **inherit**: `NiGeometryData`
- **module**: `NiMain`
- **name**: `NiParticlesData`

## Fields
- **Num Particles** (`ushort`)
  - Attributes: `until`=`4.0.0.2`
  - The maximum number of particles (matches the number of vertices).
- **Particle Radius** (`float`)
  - Attributes: `until`=`10.0.1.0`
  - The particles' size.
- **Has Radii** (`bool`)
  - Attributes: `since`=`10.1.0.0`
  - Is the particle size array present?
- **Radii** (`float`)
  - Attributes: `cond`=`Has Radii`, `length`=`Num Vertices`, `since`=`10.1.0.0`, `vercond`=`!#BS202#`
  - The individual particle sizes.
- **Num Active** (`ushort`)
  - The number of active particles at the time the system was saved. This is also the number of valid entries in the following arrays.
- **Has Sizes** (`bool`)
  - Is the particle size array present?
- **Sizes** (`float`)
  - Attributes: `cond`=`Has Sizes`, `length`=`Num Vertices`, `vercond`=`!#BS202#`
  - The individual particle sizes.
- **Has Rotations** (`bool`)
  - Attributes: `since`=`10.0.1.0`
  - Is the particle rotation array present?
- **Rotations** (`Quaternion`)
  - Attributes: `cond`=`Has Rotations`, `length`=`Num Vertices`, `since`=`10.0.1.0`, `vercond`=`!#BS202#`
  - The individual particle rotations.
- **Has Rotation Angles** (`bool`)
  - Attributes: `since`=`20.0.0.4`
  - Are the angles of rotation present?
- **Rotation Angles** (`float`)
  - Attributes: `cond`=`Has Rotation Angles`, `length`=`Num Vertices`, `vercond`=`!#BS202#`
  - Angles of rotation
- **Has Rotation Axes** (`bool`)
  - Attributes: `since`=`20.0.0.4`
  - Are axes of rotation present?
- **Rotation Axes** (`Vector3`)
  - Attributes: `cond`=`Has Rotation Axes`, `length`=`Num Vertices`, `since`=`20.0.0.4`, `vercond`=`!#BS202#`
  - Axes of rotation.
- **Has Texture Indices** (`bool`)
  - Attributes: `vercond`=`#BS202#`
- **Num Subtexture Offsets** (`uint`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
  - How many quads to use in BSPSysSubTexModifier for texture atlasing
- **Num Subtexture Offsets** (`byte`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BSSTREAM# #AND# #NI_BS_LTE_FO3#`
  - 2,4,8,16,32,64 are potential values. If "Has" was no then this should be 256, which represents a 16x16 framed image, which is invalid
- **Subtexture Offsets** (`Vector4`)
  - Attributes: `length`=`Num Subtexture Offsets`, `vercond`=`#BS202#`
  - Defines UV offsets
- **Aspect Ratio** (`float`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
  - Sets aspect ratio for Subtexture Offset UV quads
- **Aspect Flags** (`AspectFlags`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
- **Speed to Aspect Aspect 2** (`float`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
- **Speed to Aspect Speed 1** (`float`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
- **Speed to Aspect Speed 2** (`float`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`

