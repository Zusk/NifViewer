# Struct `BSGeometryPerSegmentSharedData`

## Attributes
- **module**: `BSMain`
- **name**: `BSGeometryPerSegmentSharedData`
- **size**: `16`
- **versions**: `#FO4# #F76#`

## Fields
- **User Index** (`uint`)
  - If Bone ID is 0xffffffff, this value refers to the Segment at the listed index. Otherwise this is the "Biped Object", which is like the body part types in Skyrim and earlier.
- **Bone ID** (`uint`)
  - Attributes: `default`=`#UINT_MAX#`
  - A hash of the bone name string.
- **Num Cut Offsets** (`uint`)
  - Attributes: `range`=`0:8`
  - Maximum of 8.
- **Cut Offsets** (`float`)
  - Attributes: `length`=`Num Cut Offsets`

