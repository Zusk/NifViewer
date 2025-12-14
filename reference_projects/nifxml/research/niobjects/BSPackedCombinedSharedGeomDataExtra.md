# Niobject `BSPackedCombinedSharedGeomDataExtra`

Fallout 4 Packed Combined Shared Geometry Data.
Geometry is NOT baked into the file. It is instead a reference to the geometry via a CSG filename hash and data offset.

## Attributes
- **inherit**: `NiExtraData`
- **module**: `BSMain`
- **name**: `BSPackedCombinedSharedGeomDataExtra`
- **versions**: `#FO4# #F76#`

## Fields
- **Vertex Desc** (`BSVertexDesc`)
- **Num Vertices** (`uint`)
- **Num Triangles** (`uint`)
- **Unknown Flags 1** (`uint`)
- **Unknown Flags 2** (`uint`)
- **Num Data** (`uint`)
- **Object** (`BSPackedGeomObject`)
  - Attributes: `length`=`Num Data`
- **Object Data** (`BSPackedSharedGeomData`)
  - Attributes: `length`=`Num Data`

