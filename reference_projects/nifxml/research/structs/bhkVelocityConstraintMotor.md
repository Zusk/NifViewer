# Struct `bhkVelocityConstraintMotor`

Bethesda extension of hkpVelocityConstraintMotor. Tries to reach and keep a desired target velocity.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkVelocityConstraintMotor`
- **size**: `18`
- **versions**: `#BETHESDA#`

## Fields
- **Min Force** (`float`)
  - Attributes: `default`=`-1000000.0`
  - Minimum motor force
- **Max Force** (`float`)
  - Attributes: `default`=`1000000.0`
  - Maximum motor force
- **Tau** (`float`)
  - Attributes: `default`=`0.0`
  - Relative stiffness
- **Target Velocity** (`float`)
  - Attributes: `default`=`0.0`
- **Use Velocity Target** (`bool`)
  - Attributes: `default`=`false`
- **Motor Enabled** (`bool`)
  - Attributes: `default`=`false`
  - Is Motor enabled

