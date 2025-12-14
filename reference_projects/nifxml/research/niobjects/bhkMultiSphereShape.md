# Niobject `bhkMultiSphereShape`

A compound shape made up of spheres. This is useful as an approximation for complex shapes, as collision detection for spheres is very fast.
However, if two bhkMultiSphereShape collide, every sphere needs to be checked against every other sphere.
    Example: 10 spheres colliding with 10 spheres will result in 100 collision checks.
    Therefore shapes like bhkCapsuleShape or bhkConvexVerticesShape should be preferred.

## Attributes
- **inherit**: `bhkSphereRepShape`
- **module**: `BSHavok`
- **name**: `bhkMultiSphereShape`
- **versions**: `#BETHESDA#`

## Fields
- **Shape Property** (`bhkWorldObjCInfoProperty`)
- **Num Spheres** (`uint`)
  - Attributes: `default`=`2`, `range`=`1:8`
- **Spheres** (`NiBound`)
  - Attributes: `length`=`Num Spheres`
  - The spheres which make up the multi sphere shape. Max of 8.

