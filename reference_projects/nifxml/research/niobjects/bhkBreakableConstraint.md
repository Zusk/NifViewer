# Niobject `bhkBreakableConstraint`

Bethesda extension of hkpBreakableConstraintData, a wrapper around hkpConstraintInstance.
The constraint can "break" i.e. stop applying the forces to each body to keep them constrained.

## Attributes
- **inherit**: `bhkConstraint`
- **module**: `BSHavok`
- **name**: `bhkBreakableConstraint`
- **versions**: `#BETHESDA#`

## Fields
- **Constraint Data** (`bhkWrappedConstraintData`)
  - The wrapped constraint.
- **Threshold** (`float`)
  - The larger the value, the harder to "break" the constraint.
- **Remove When Broken** (`bool`)
  - Attributes: `default`=`false`
  - No: Constraint stays active. Yes: Constraint gets removed when breaking threshold is exceeded.

