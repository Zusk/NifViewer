# Niobject `NiPSysAirFieldModifier`

Particle system modifier, updates the particle velocity to simulate the effects of air movements like wind, fans, or wake.

## Attributes
- **inherit**: `NiPSysFieldModifier`
- **module**: `NiParticle`
- **name**: `NiPSysAirFieldModifier`

## Fields
- **Direction** (`Vector3`)
  - Attributes: `default`=`#NEG_X_AXIS#`
  - Direction of the particle velocity
- **Air Friction** (`float`)
  - Attributes: `range`=`#F0_1#`
  - How quickly particles will accelerate to the magnitude of the air field.
- **Inherit Velocity** (`float`)
  - Attributes: `range`=`#F0_1#`
  - How much of the air field velocity will be added to the particle velocity.
- **Inherit Rotation** (`bool`)
- **Component Only** (`bool`)
- **Enable Spread** (`bool`)
- **Spread** (`float`)
  - Attributes: `range`=`#F0_1#`
  - The angle of the air field cone if Enable Spread is true.

