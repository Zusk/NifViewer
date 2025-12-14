# Niobject `NiPSysRotationModifier`

Particle modifier that adds rotations to particles.

## Attributes
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysRotationModifier`

## Fields
- **Rotation Speed** (`float`)
  - Initial Rotation Speed in radians per second.
- **Rotation Speed Variation** (`float`)
  - Attributes: `since`=`20.0.0.2`
  - Distributes rotation speed over the range [Speed - Variation, Speed + Variation].
- **Unknown Vector** (`Vector4`)
  - Attributes: `vercond`=`#BS_F76#`
- **Unknown Byte** (`byte`)
  - Attributes: `vercond`=`#BS_F76#`
- **Rotation Angle** (`float`)
  - Attributes: `since`=`20.0.0.2`
  - Initial Rotation Angle in radians.
- **Rotation Angle Variation** (`float`)
  - Attributes: `since`=`20.0.0.2`
  - Distributes rotation angle over the range [Angle - Variation, Angle + Variation].
- **Random Rot Speed Sign** (`bool`)
  - Attributes: `since`=`20.0.0.2`
  - Randomly negate the initial rotation speed?
- **Random Axis** (`bool`)
  - Attributes: `default`=`true`
  - Assign a random axis to new particles?
- **Axis** (`Vector3`)
  - Attributes: `default`=`#X_AXIS#`
  - Initial rotation axis.

