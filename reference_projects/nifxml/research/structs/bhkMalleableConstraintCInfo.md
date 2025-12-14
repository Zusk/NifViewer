# Struct `bhkMalleableConstraintCInfo`

bhkMalleableConstraint serialization data. A constraint wrapper used to soften or harden constraints.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkMalleableConstraintCInfo`
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
- **Tau** (`float`)
  - Attributes: `until`=`20.0.0.5`
- **Damping** (`float`)
  - Attributes: `until`=`20.0.0.5`
- **Strength** (`float`)
  - Attributes: `since`=`20.2.0.7`

