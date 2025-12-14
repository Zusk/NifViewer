# Niobject `bhkPackedNiTriStripsShape`

Bethesda custom hkpShapeCollection using custom packed tri strips data.

## Attributes
- **inherit**: `bhkShapeCollection`
- **module**: `BSHavok`
- **name**: `bhkPackedNiTriStripsShape`
- **versions**: `#BETHESDA#`

## Fields
- **Num Sub Shapes** (`ushort`)
  - Attributes: `until`=`20.0.0.5`
- **Sub Shapes** (`hkSubPartData`)
  - Attributes: `length`=`Num Sub Shapes`, `until`=`20.0.0.5`
- **User Data** (`uint`)
  - Attributes: `default`=`0`
- **Unused 01** (`byte`)
  - Attributes: `binary`=`true`, `length`=`4`
- **Radius** (`float`)
  - Attributes: `default`=`0.1`
- **Unused 02** (`byte`)
  - Attributes: `binary`=`true`, `length`=`4`
- **Scale** (`Vector4`)
  - Attributes: `default`=`#VEC4_1110#`
- **Radius Copy** (`float`)
  - Attributes: `default`=`0.1`
  - Same as radius
- **Scale Copy** (`Vector4`)
  - Attributes: `default`=`#VEC4_1110#`
  - Same as scale.
- **Data** (`Ref`)
  - Attributes: `template`=`hkPackedNiTriStripsData`

