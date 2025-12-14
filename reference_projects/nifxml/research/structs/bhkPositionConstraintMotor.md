# Struct `bhkPositionConstraintMotor`

Bethesda extension of hkpPositionConstraintMotor.
A motor which tries to reach a desired position/angle given a max force and recovery speed.
This motor is a good choice for driving a ragdoll to a given pose.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkPositionConstraintMotor`
- **size**: `25`
- **versions**: `#BETHESDA#`

## Fields
- **Min Force** (`float`)
  - Attributes: `default`=`-1000000.0`
  - Minimum motor force
- **Max Force** (`float`)
  - Attributes: `default`=`1000000.0`
  - Maximum motor force
- **Tau** (`float`)
  - Attributes: `default`=`0.8`
  - Relative stiffness
- **Damping** (`float`)
  - Attributes: `default`=`1.0`
  - Motor damping value
- **Proportional Recovery Velocity** (`float`)
  - Attributes: `default`=`2.0`
  - A factor of the current error to calculate the recovery velocity
- **Constant Recovery Velocity** (`float`)
  - Attributes: `default`=`1.0`
  - A constant velocity which is used to recover from errors
- **Motor Enabled** (`bool`)
  - Attributes: `default`=`false`
  - Is Motor enabled

