# Niobject `bhkTransformShape`

Contains a bhkShape and an additional transform for that shape.

## Attributes
- **inherit**: `bhkShape`
- **module**: `BSHavok`
- **name**: `bhkTransformShape`
- **versions**: `#BETHESDA#`

## Fields
- **Shape** (`Ref`)
  - Attributes: `template`=`bhkShape`
  - The shape that this object transforms.
- **Material** (`HavokMaterial`)
  - The material of the shape.
- **Radius** (`float`)
- **Unused 01** (`byte`)
  - Attributes: `binary`=`true`, `length`=`8`
- **Transform** (`Matrix44`)
  - A transform matrix.

