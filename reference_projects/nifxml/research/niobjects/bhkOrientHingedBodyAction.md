# Niobject `bhkOrientHingedBodyAction`

Bethesda extension of hkpReorientAction (or similar). Will try to reorient a body to stay facing a given direction.

## Attributes
- **inherit**: `bhkUnaryAction`
- **module**: `BSHavok`
- **name**: `bhkOrientHingedBodyAction`
- **versions**: `#BETHESDA#`

## Fields
- **Unused 02** (`byte`)
  - Attributes: `binary`=`true`, `length`=`8`
- **Hinge Axis LS** (`Vector4`)
  - Attributes: `default`=`#VEC4_X_AXIS#`
- **Forward LS** (`Vector4`)
  - Attributes: `default`=`#VEC4_Y_AXIS#`
- **Strength** (`float`)
  - Attributes: `default`=`1.0`
- **Damping** (`float`)
  - Attributes: `default`=`0.1`
- **Unused 03** (`byte`)
  - Attributes: `binary`=`true`, `length`=`8`

