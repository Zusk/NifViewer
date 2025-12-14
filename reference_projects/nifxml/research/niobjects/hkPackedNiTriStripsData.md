# Niobject `hkPackedNiTriStripsData`

Bethesda custom tri strips data block for bhkPackedNiTriStripsShape.

## Attributes
- **inherit**: `bhkShapeCollection`
- **module**: `BSHavok`
- **name**: `hkPackedNiTriStripsData`
- **versions**: `#BETHESDA#`

## Fields
- **Num Triangles** (`uint`)
- **Triangles** (`TriangleData`)
  - Attributes: `length`=`Num Triangles`
- **Num Vertices** (`uint`)
- **Compressed** (`bool`)
  - Attributes: `since`=`20.2.0.7`
- **Vertices** (`Vector3`)
  - Attributes: `cond`=`Compressed == 0`, `length`=`Num Vertices`
- **Compressed Vertices** (`HalfVector3`)
  - Attributes: `cond`=`Compressed != 0`, `length`=`Num Vertices`
  - Compression on read may not be supported. Vertices may be packed in ushort that are not IEEE standard half-precision.
- **Num Sub Shapes** (`ushort`)
  - Attributes: `since`=`20.2.0.7`
- **Sub Shapes** (`hkSubPartData`)
  - Attributes: `length`=`Num Sub Shapes`, `since`=`20.2.0.7`

