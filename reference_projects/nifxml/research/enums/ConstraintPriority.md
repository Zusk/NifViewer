# Enum `ConstraintPriority`

hkpConstraintInstance::ConstraintPriority. Priority used for the constraint.
Values 2, 4, and 5 are unused or internal use only.

## Attributes
- **name**: `ConstraintPriority`
- **storage**: `uint`
- **versions**: `#BETHESDA#`

## Values
- `option` `PRIORITY_INVALID` (`value`=`0`)
- `option` `PRIORITY_PSI` (`value`=`1`) – Constraint is only solved at regular physics time steps.
- `option` `PRIORITY_TOI` (`value`=`3`) – Constraint is also solved at time of impact events.

