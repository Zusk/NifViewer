# Enum `hkQualityType`

hkpCollidableQualityType. Describes the priority and quality of collisions for a body,
    e.g. you may expect critical game play objects to have solid high-priority collisions so that they never sink into ground,
    or may allow penetrations for visual debris objects.
Notes:
    - Fixed and keyframed objects cannot interact with each other.
    - Debris can interpenetrate but still responds to Bullet hits.
    - Critical objects are forced to not interpenetrate.
    - Moving objects can interpenetrate slightly with other Moving or Debris objects but nothing else.

## Attributes
- **name**: `hkQualityType`
- **storage**: `byte`
- **versions**: `#BETHESDA#`

## Values
- `option` `MO_QUAL_INVALID` (`value`=`0`) – Automatically assigned to MO_QUAL_FIXED, MO_QUAL_KEYFRAMED or MO_QUAL_DEBRIS
- `option` `MO_QUAL_FIXED` (`value`=`1`) – Static body.
- `option` `MO_QUAL_KEYFRAMED` (`value`=`2`) – Animated body with infinite mass.
- `option` `MO_QUAL_DEBRIS` (`value`=`3`) – Low importance bodies adding visual detail.
- `option` `MO_QUAL_MOVING` (`value`=`4`) – Moving bodies which should not penetrate or leave the world, but can.
- `option` `MO_QUAL_CRITICAL` (`value`=`5`) – Gameplay critical bodies which cannot penetrate or leave the world under any circumstance.
- `option` `MO_QUAL_BULLET` (`value`=`6`) – Fast-moving bodies, such as projectiles.
- `option` `MO_QUAL_USER` (`value`=`7`) – For user.
- `option` `MO_QUAL_CHARACTER` (`value`=`8`) – For use with rigid body character controllers.
- `option` `MO_QUAL_KEYFRAMED_REPORT` (`value`=`9`) – Moving bodies with infinite mass which should report contact points and TOI collisions against all other bodies.

