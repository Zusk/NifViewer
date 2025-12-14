# Niobject `bhkConvexTransformShape`

Contains a bhkConvexShape and an additional transform for that shape.
The advantage of using bhkConvexTransformShape over bhkTransformShape is that it does not require additional agents to be created as it is itself convex.

## Attributes
- **inherit**: `bhkConvexShapeBase`
- **module**: `BSHavok`
- **name**: `bhkConvexTransformShape`
- **versions**: `#BETHESDA#`

## Fields
- **Shape** (`Ref`)
  - Attributes: `template`=`bhkConvexShape`
  - The shape that this object transforms.
- **Material** (`HavokMaterial`)
  - The material of the shape.
- **Radius** (`float`)
- **Unused 01** (`byte`)
  - Attributes: `binary`=`true`, `length`=`8`
- **Transform** (`Matrix44`)
  - A transform matrix.

