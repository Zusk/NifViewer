# Niobject `NiTriStripsData`

Holds mesh data using strips of triangles.

## Attributes
- **inherit**: `NiTriBasedGeomData`
- **module**: `NiMain`
- **name**: `NiTriStripsData`

## Fields
- **Num Strips** (`ushort`)
  - Attributes: `default`=`1`
- **Strip Lengths** (`ushort`)
  - Attributes: `length`=`Num Strips`
  - The number of points in each triangle strip.
- **Has Points** (`bool`)
  - Attributes: `default`=`true`, `since`=`10.0.1.3`
  - Do we have strip point data?
- **Points** (`ushort`)
  - Attributes: `length`=`Num Strips`, `until`=`10.0.1.2`, `width`=`Strip Lengths`
  - The points in the Triangle strips.  Size is the sum of all entries in Strip Lengths.
- **Points** (`ushort`)
  - Attributes: `cond`=`Has Points`, `length`=`Num Strips`, `since`=`10.0.1.3`, `width`=`Strip Lengths`
  - The points in the Triangle strips. Size is the sum of all entries in Strip Lengths.

