# Struct `BSPackedGeomData`

## Attributes
- **module**: `BSMain`
- **name**: `BSPackedGeomData`
- **versions**: `#FO4# #F76#`

## Fields
- **Num Verts** (`uint`)
- **LOD Levels** (`uint`)
- **Tri Count LOD0** (`uint`)
- **Tri Offset LOD0** (`uint`)
- **Tri Count LOD1** (`uint`)
- **Tri Offset LOD1** (`uint`)
- **Tri Count LOD2** (`uint`)
- **Tri Offset LOD2** (`uint`)
- **Num Combined** (`uint`)
- **Combined** (`BSPackedGeomDataCombined`)
  - Attributes: `length`=`Num Combined`
- **Vertex Desc** (`BSVertexDesc`)
- **Vertex Data** (`BSVertexData`)
  - Attributes: `arg`=`Vertex Desc #RSH# 44`, `length`=`Num Verts`
- **Triangles** (`Triangle`)
  - Attributes: `length`=`Tri Count LOD0 + Tri Count LOD1 + Tri Count LOD2`

