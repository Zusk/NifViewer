# Niobject `bhkBoxShape`

A box.

## Attributes
- **inherit**: `bhkConvexShape`
- **module**: `BSHavok`
- **name**: `bhkBoxShape`
- **versions**: `#BETHESDA#`

## Fields
- **Unused 01** (`byte`)
  - Attributes: `binary`=`true`, `length`=`8`
- **Dimensions** (`Vector3`)
  - A cube stored in Half Extents. A unit cube (1.0, 1.0, 1.0) would be stored as 0.5, 0.5, 0.5.
- **Unused Float** (`float`)
  - Unused as Havok stores the Half Extents as hkVector4 with the W component unused.

