# Niobject `bhkNiTriStripsShape`

Bethesda custom hkpShapeCollection using NiTriStripsData for geometry storage.

## Attributes
- **inherit**: `bhkShapeCollection`
- **module**: `BSHavok`
- **name**: `bhkNiTriStripsShape`
- **versions**: `#BETHESDA#`

## Fields
- **Material** (`HavokMaterial`)
  - The material of the shape.
- **Radius** (`float`)
  - Attributes: `default`=`0.1`
- **Unused 01** (`byte`)
  - Attributes: `binary`=`true`, `length`=`20`
- **Grow By** (`uint`)
  - Attributes: `default`=`1`
- **Scale** (`Vector4`)
  - Attributes: `default`=`#VEC4_1110#`, `since`=`10.1.0.0`
- **Num Strips Data** (`uint`)
  - Attributes: `default`=`1`
- **Strips Data** (`Ref`)
  - Attributes: `length`=`Num Strips Data`, `template`=`NiTriStripsData`
- **Num Filters** (`uint`)
  - Attributes: `calc`=`#LEN[Strips Data]#`, `default`=`1`
- **Filters** (`HavokFilter`)
  - Attributes: `length`=`Num Filters`

