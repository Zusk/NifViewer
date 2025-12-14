# Niobject `NiTriShapeData`

Holds mesh data using a list of singular triangles.

## Attributes
- **inherit**: `NiTriBasedGeomData`
- **module**: `NiMain`
- **name**: `NiTriShapeData`

## Fields
- **Num Triangle Points** (`uint`)
  - Attributes: `range`=`0:196605`
  - Num Triangles times 3.
- **Has Triangles** (`bool`)
  - Attributes: `since`=`10.1.0.0`
  - Do we have triangle data?
- **Triangles** (`Triangle`)
  - Attributes: `length`=`Num Triangles`, `until`=`10.0.1.2`
  - Triangle data.
- **Triangles** (`Triangle`)
  - Attributes: `cond`=`Has Triangles`, `length`=`Num Triangles`, `since`=`10.0.1.3`
  - Triangle face data.
- **Num Match Groups** (`ushort`)
  - Attributes: `since`=`3.1`
  - Number of shared normals groups.
- **Match Groups** (`MatchGroup`)
  - Attributes: `length`=`Num Match Groups`, `since`=`3.1`
  - The shared normals.

