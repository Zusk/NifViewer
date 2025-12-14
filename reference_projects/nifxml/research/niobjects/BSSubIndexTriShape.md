# Niobject `BSSubIndexTriShape`

Fallout 4 Sub-Index Tri Shape

## Attributes
- **inherit**: `BSTriShape`
- **module**: `BSMain`
- **name**: `BSSubIndexTriShape`
- **versions**: `#SSE# #FO4# #F76#`

## Fields
- **Num Primitives** (`uint`)
  - Attributes: `calc`=`#LEN[Triangles]#`, `cond`=`Data Size #GT# 0`, `vercond`=`#BS_GTE_130#`
- **Num Segments** (`uint`)
  - Attributes: `cond`=`Data Size #GT# 0`, `vercond`=`#BS_GTE_130#`
- **Total Segments** (`uint`)
  - Attributes: `cond`=`Data Size #GT# 0`, `vercond`=`#BS_GTE_130#`
- **Segment** (`BSGeometrySegmentData`)
  - Attributes: `cond`=`Data Size #GT# 0`, `length`=`Num Segments`, `vercond`=`#BS_GTE_130#`
- **Segment Data** (`BSGeometrySegmentSharedData`)
  - Attributes: `cond`=`(Num Segments #LT# Total Segments) #AND# (Data Size #GT# 0)`, `vercond`=`#BS_GTE_130#`
- **Num Segments** (`uint`)
  - Attributes: `vercond`=`#BS_SSE#`
- **Segment** (`BSGeometrySegmentData`)
  - Attributes: `length`=`Num Segments`, `vercond`=`#BS_SSE#`

