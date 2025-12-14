# Enum `hkDeactivatorType`

hkpRigidBodyDeactivator::DeactivatorType. Deactivator Type determines which mechanism Havok will use to classify the body as deactivated.

## Attributes
- **name**: `hkDeactivatorType`
- **storage**: `byte`
- **versions**: `#BETHESDA#`

## Values
- `option` `DEACTIVATOR_INVALID` (`value`=`0`) – Invalid
- `option` `DEACTIVATOR_NEVER` (`value`=`1`) – This will force the rigid body to never deactivate.
- `option` `DEACTIVATOR_SPATIAL` (`value`=`2`) – Tells Havok to use a spatial deactivation scheme. This makes use of high and low frequencies of positional motion to determine when deactivation should occur.

