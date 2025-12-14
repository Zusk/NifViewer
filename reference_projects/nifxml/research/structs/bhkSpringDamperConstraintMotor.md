# Struct `bhkSpringDamperConstraintMotor`

Bethesda extension of hkpSpringDamperConstraintMotor.
Tries to reach a given target position using an angular spring which has a spring constant.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkSpringDamperConstraintMotor`
- **size**: `17`
- **versions**: `#BETHESDA#`

## Fields
- **Min Force** (`float`)
  - Attributes: `default`=`-1000000.0`
  - Minimum motor force
- **Max Force** (`float`)
  - Attributes: `default`=`1000000.0`
  - Maximum motor force
- **Spring Constant** (`float`)
  - Attributes: `default`=`0.0`
  - The spring constant in N/m
- **Spring Damping** (`float`)
  - Attributes: `default`=`0.0`
  - The spring damping in Nsec/m
- **Motor Enabled** (`bool`)
  - Attributes: `default`=`false`
  - Is Motor enabled

