# Struct `BSGeometrySegmentSharedData`

## Attributes
- **module**: `BSMain`
- **name**: `BSGeometrySegmentSharedData`
- **versions**: `#FO4# #F76#`

## Fields
- **Num Segments** (`uint`)
- **Total Segments** (`uint`)
- **Segment Starts** (`uint`)
  - Attributes: `length`=`Num Segments`
- **Per Segment Data** (`BSGeometryPerSegmentSharedData`)
  - Attributes: `length`=`Total Segments`
- **SSF File** (`SizedString16`)

