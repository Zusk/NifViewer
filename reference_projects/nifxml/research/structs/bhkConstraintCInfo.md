# Struct `bhkConstraintCInfo`

Bethesda extension of hkpConstraintInstance.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkConstraintCInfo`
- **size**: `16`
- **versions**: `#BETHESDA#`

## Fields
- **Num Entities** (`uint`)
  - Attributes: `default`=`2`
  - Always 2 (Hardcoded). Number of bodies affected by this constraint.
- **Entity A** (`Ptr`)
  - Attributes: `template`=`bhkEntity`
  - The entity affected by this constraint.
- **Entity B** (`Ptr`)
  - Attributes: `template`=`bhkEntity`
  - The entity affected by this constraint.
- **Priority** (`ConstraintPriority`)
  - Attributes: `default`=`PRIORITY_PSI`
  - Either PSI or TOI priority. TOI is higher priority.

