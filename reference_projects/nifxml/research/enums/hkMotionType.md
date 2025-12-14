# Enum `hkMotionType`

hkpMotion::MotionType. Motion type of a rigid body determines what happens when it is simulated.

## Attributes
- **name**: `hkMotionType`
- **storage**: `byte`
- **versions**: `#BETHESDA#`

## Values
- `option` `MO_SYS_INVALID` (`value`=`0`) – Invalid
- `option` `MO_SYS_DYNAMIC` (`value`=`1`) – A fully-simulated, movable rigid body. At construction time the engine checks the input inertia and selects MO_SYS_SPHERE_INERTIA or MO_SYS_BOX_INERTIA as appropriate.
- `option` `MO_SYS_SPHERE_INERTIA` (`value`=`2`) – Simulation is performed using a sphere inertia tensor.
- `option` `MO_SYS_SPHERE_STABILIZED` (`value`=`3`) – This is the same as MO_SYS_SPHERE_INERTIA, except that simulation of the rigid body is "softened".
- `option` `MO_SYS_BOX_INERTIA` (`value`=`4`) – Simulation is performed using a box inertia tensor.
- `option` `MO_SYS_BOX_STABILIZED` (`value`=`5`) – This is the same as MO_SYS_BOX_INERTIA, except that simulation of the rigid body is "softened".
- `option` `MO_SYS_KEYFRAMED` (`value`=`6`) – Simulation is not performed as a normal rigid body. The keyframed rigid body has an infinite mass when viewed by the rest of the system. (used for creatures)
- `option` `MO_SYS_FIXED` (`value`=`7`) – This motion type is used for the static elements of a game scene, e.g. the landscape. Faster than MO_SYS_KEYFRAMED at velocity 0. (used for weapons)
- `option` `MO_SYS_THIN_BOX` (`value`=`8`) – A box inertia motion which is optimized for thin boxes and has less stability problems
- `option` `MO_SYS_CHARACTER` (`value`=`9`) – A specialized motion used for character controllers

