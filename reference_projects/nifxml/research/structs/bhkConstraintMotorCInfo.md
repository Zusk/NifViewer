# Struct `bhkConstraintMotorCInfo`

hkConstraintCinfo::SaveMotor(). Not a Bethesda extension of hkpConstraintMotor, but a wrapper for its serialization function.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkConstraintMotorCInfo`
- **versions**: `#BETHESDA#`

## Fields
- **Type** (`hkMotorType`)
  - Attributes: `default`=`MOTOR_NONE`
- **Position Motor** (`bhkPositionConstraintMotor`)
  - Attributes: `cond`=`Type == 1`
- **Velocity Motor** (`bhkVelocityConstraintMotor`)
  - Attributes: `cond`=`Type == 2`
- **Spring Damper Motor** (`bhkSpringDamperConstraintMotor`)
  - Attributes: `cond`=`Type == 3`

