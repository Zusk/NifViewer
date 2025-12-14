# Struct `bhkRigidBodyCInfo2010`

## Attributes
- **module**: `BSHavok`
- **name**: `bhkRigidBodyCInfo2010`
- **versions**: `#BETHESDA#`

## Fields
- **Unused 01** (`byte`)
  - Attributes: `binary`=`true`, `length`=`4`
- **Havok Filter** (`HavokFilter`)
- **Unused 02** (`byte`)
  - Attributes: `binary`=`true`, `length`=`4`
- **Unknown Int 1** (`uint`)
- **Collision Response** (`hkResponseType`)
  - Attributes: `default`=`RESPONSE_SIMPLE_CONTACT`
- **Unused 03** (`byte`)
- **Process Contact Callback Delay** (`ushort`)
  - Attributes: `default`=`0xffff`
- **Translation** (`Vector4`)
  - A vector that moves the body by the specified amount. Only enabled in bhkRigidBodyT objects.
- **Rotation** (`hkQuaternion`)
  - The rotation Yaw/Pitch/Roll to apply to the body. Only enabled in bhkRigidBodyT objects.
- **Linear Velocity** (`Vector4`)
  - Linear velocity.
- **Angular Velocity** (`Vector4`)
  - Angular velocity.
- **Inertia Tensor** (`hkMatrix3`)
  - Defines how the mass is distributed among the body, i.e. how difficult it is to rotate around any given axis.
- **Center** (`Vector4`)
  - The body's center of mass.
- **Mass** (`float`)
  - Attributes: `default`=`1.0`
  - The body's mass in kg. A mass of zero represents an immovable object.
- **Linear Damping** (`float`)
  - Attributes: `default`=`0.1`
  - Reduces the movement of the body over time. A value of 0.1 will remove 10% of the linear velocity every second.
- **Angular Damping** (`float`)
  - Attributes: `default`=`0.05`
  - Reduces the movement of the body over time. A value of 0.05 will remove 5% of the angular velocity every second.
- **Time Factor** (`float`)
  - Attributes: `default`=`1.0`
- **Gravity Factor** (`float`)
  - Attributes: `default`=`1.0`
- **Friction** (`float`)
  - Attributes: `default`=`0.5`
  - How smooth its surfaces is and how easily it will slide along other bodies.
- **Rolling Friction Multiplier** (`float`)
- **Restitution** (`float`)
  - Attributes: `default`=`0.4`
  - How "bouncy" the body is, i.e. how much energy it has after colliding. Less than 1.0 loses energy, greater than 1.0 gains energy.
If the restitution is not 0.0 the object will need extra CPU for all new collisions.
- **Max Linear Velocity** (`float`)
  - Attributes: `default`=`104.4`
  - Maximal linear velocity.
- **Max Angular Velocity** (`float`)
  - Attributes: `default`=`31.57`
  - Maximal angular velocity.
- **Penetration Depth** (`float`)
  - Attributes: `default`=`0.15`
  - The maximum allowed penetration for this object.
This is a hint to the engine to see how much CPU the engine should invest to keep this object from penetrating.
A good choice is 5% - 20% of the smallest diameter of the object.
- **Motion System** (`hkMotionType`)
  - Attributes: `default`=`MO_SYS_DYNAMIC`
  - Motion system? Overrides Quality when on Keyframed?
- **Deactivator Type** (`hkDeactivatorType`)
  - Attributes: `default`=`DEACTIVATOR_NEVER`
  - The initial deactivator type of the body.
- **Solver Deactivation** (`hkSolverDeactivation`)
  - Attributes: `default`=`SOLVER_DEACTIVATION_OFF`
  - How aggressively the engine will try to zero the velocity for slow objects. This does not save CPU.
- **Quality Type** (`hkQualityType`)
  - Attributes: `default`=`MO_QUAL_FIXED`
  - The type of interaction with other objects.
- **Auto Remove Level** (`byte`)
- **Response Modifier Flags** (`byte`)
- **Num Shape Keys in Contact Point** (`byte`)
  - Attributes: `default`=`3`
- **Force Collided Onto PPU** (`bool`)
- **Unused 04** (`byte`)
  - Attributes: `binary`=`true`, `length`=`12`

