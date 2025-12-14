# Enum `PropagationMode`

The propagation mode controls scene graph traversal during collision detection operations for NiCollisionData.

## Attributes
- **name**: `PropagationMode`
- **storage**: `uint`

## Values
- `option` `PROPAGATE_ON_SUCCESS` (`value`=`0`) – Propagation only occurs as a result of a successful collision.
- `option` `PROPAGATE_ON_FAILURE` (`value`=`1`) – (Deprecated) Propagation only occurs as a result of a failed collision.
- `option` `PROPAGATE_ALWAYS` (`value`=`2`) – Propagation always occurs regardless of collision result.
- `option` `PROPAGATE_NEVER` (`value`=`3`) – Propagation never occurs regardless of collision result.
- `option` `PROPAGATE_UNKNOWN_6` (`value`=`6`) – Propagation mode found in Civ IV Chariot_Celtic.

