# Struct `bhkWrappedConstraintData`

A constraint wrapper for polymorphic hkpConstraintData serialization.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkWrappedConstraintData`
- **versions**: `#BETHESDA#`

## Fields
- **Type** (`hkConstraintType`)
  - Type of constraint.
- **Constraint Info** (`bhkConstraintCInfo`)
- **Ball and Socket** (`bhkBallAndSocketConstraintCInfo`)
  - Attributes: `cond`=`Type == 0`
- **Hinge** (`bhkHingeConstraintCInfo`)
  - Attributes: `cond`=`Type == 1`
- **Limited Hinge** (`bhkLimitedHingeConstraintCInfo`)
  - Attributes: `cond`=`Type == 2`
- **Prismatic** (`bhkPrismaticConstraintCInfo`)
  - Attributes: `cond`=`Type == 6`
- **Ragdoll** (`bhkRagdollConstraintCInfo`)
  - Attributes: `cond`=`Type == 7`
- **Stiff Spring** (`bhkStiffSpringConstraintCInfo`)
  - Attributes: `cond`=`Type == 8`
- **Malleable** (`bhkMalleableConstraintCInfo`)
  - Attributes: `cond`=`Type == 13`

