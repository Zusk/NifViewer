# Niobject `bhkConvexListShape`

A list of shapes. However,
    - The sub shapes must ALL be convex: Sphere, Capsule, Cylinder, Convex Vertices, Convex Transform
    - The radius of all shapes must be identical
    - The number of sub shapes is restricted to 255
    - The number of vertices of each sub shape is restricted to 255

For this reason you most likely cannot combine Sphere Shapes, Capsule Shapes, and Convex Vertices Shapes,
as their Radius values differ in function. (Sphere/Capsule radius = physical size, CVS radius = padding/shell)

Shapes collected in a bhkConvexListShape may not have the correct material noise.

## Attributes
- **inherit**: `bhkConvexShapeBase`
- **module**: `BSHavok`
- **name**: `bhkConvexListShape`
- **versions**: `#FO3#`

## Fields
- **Num Sub Shapes** (`uint`)
  - Attributes: `default`=`1`, `range`=`1:255`
- **Sub Shapes** (`Ref`)
  - Attributes: `length`=`Num Sub Shapes`, `template`=`bhkConvexShapeBase`
  - List of shapes. Max of 255.
- **Material** (`HavokMaterial`)
  - The material of the shape.
- **Radius** (`float`)
- **Unknown Int 1** (`uint`)
- **Unknown Float 1** (`float`)
- **Child Shape Property** (`bhkWorldObjCInfoProperty`)
- **Use Cached AABB** (`bool`)
  - If true, an AABB of the children's AABBs is used, which is faster but larger than building an AABB from each child.
- **Closest Point Min Distance** (`float`)
  - A distance which is used for getClosestPoint(). If the object being tested is closer, the children are recursed. Otherwise it returns this value.

