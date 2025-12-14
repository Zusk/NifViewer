# Niobject `bhkBallSocketConstraintChain`

Bethesda extension of hkpBallSocketChainData. A chain of ball and socket constraints.

## Attributes
- **inherit**: `bhkSerializable`
- **module**: `BSHavok`
- **name**: `bhkBallSocketConstraintChain`
- **versions**: `#BETHESDA#`

## Fields
- **Num Pivots** (`uint`)
  - Attributes: `calc`=`#LEN[Pivots]# #MUL# 2`
  - Should equal (Num Chained Entities - 1) * 2
- **Pivots** (`bhkBallAndSocketConstraintCInfo`)
  - Attributes: `length`=`Num Pivots / 2`
  - Two pivot points A and B for each constraint.
- **Tau** (`float`)
  - Attributes: `default`=`1.0`
  - High values are harder and more reactive, lower values are smoother.
- **Damping** (`float`)
  - Attributes: `default`=`0.6`
  - Defines damping strength for the current velocity.
- **Constraint Force Mixing** (`float`)
  - Attributes: `default`=`1.1920929e-08`
  - Restitution (amount of elasticity) of constraints. Added to the diagonal of the constraint matrix. A value of 0.0 can result in a division by zero with some chain configurations.
- **Max Error Distance** (`float`)
  - Attributes: `default`=`0.1`
  - Maximum distance error in constraints allowed before stabilization algorithm kicks in. A smaller distance causes more resistance.
- **Constraint Chain Info** (`bhkConstraintChainCInfo`)

