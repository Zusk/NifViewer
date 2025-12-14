# Enum `hkSolverDeactivation`

hkpRigidBodyCinfo::SolverDeactivation.
A list of possible solver deactivation settings. This value defines how aggressively the solver deactivates objects.
Note: Solver deactivation does not save CPU, but reduces creeping of movable objects in a pile quite dramatically.

## Attributes
- **name**: `hkSolverDeactivation`
- **storage**: `byte`
- **versions**: `#BETHESDA#`

## Values
- `option` `SOLVER_DEACTIVATION_INVALID` (`value`=`0`) – Invalid
- `option` `SOLVER_DEACTIVATION_OFF` (`value`=`1`) – No solver deactivation.
- `option` `SOLVER_DEACTIVATION_LOW` (`value`=`2`) – Very conservative deactivation, typically no visible artifacts.
- `option` `SOLVER_DEACTIVATION_MEDIUM` (`value`=`3`) – Normal deactivation, no serious visible artifacts in most cases.
- `option` `SOLVER_DEACTIVATION_HIGH` (`value`=`4`) – Fast deactivation, visible artifacts.
- `option` `SOLVER_DEACTIVATION_MAX` (`value`=`5`) – Very fast deactivation, visible artifacts.

