# Struct `BSGeometrySegmentData`

Bethesda-specific. Describes groups of triangles either segmented in a grid (for LOD) or by body part for skinned FO4 meshes.

## Attributes
- **module**: `BSMain`
- **name**: `BSGeometrySegmentData`
- **versions**: `#FO3_AND_LATER#`

## Fields
- **Flags** (`byte`)
  - Attributes: `vercond`=`#NI_BS_LT_FO4#`
- **Start Index** (`uint`)
  - Index = previous Index + previous Num Tris in Segment * 3
- **Num Primitives** (`uint`)
  - The number of triangles belonging to this segment
- **Parent Array Index** (`uint`)
  - Attributes: `vercond`=`#BS_GTE_130#`
- **Num Sub Segments** (`uint`)
  - Attributes: `vercond`=`#BS_GTE_130#`
- **Sub Segment** (`BSGeometrySubSegment`)
  - Attributes: `length`=`Num Sub Segments`, `vercond`=`#BS_GTE_130#`

