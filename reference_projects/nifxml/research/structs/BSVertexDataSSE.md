# Struct `BSVertexDataSSE`

## Attributes
- **module**: `BSMain`
- **name**: `BSVertexDataSSE`
- **versions**: `#SSE# #FO4#`

## Fields
- **Vertex** (`Vector3`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x1) != 0`
- **Bitangent X** (`float`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x11) == 0x11`
- **Unused W** (`uint`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x11) == 0x1`
- **UV** (`HalfTexCoord`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x2) != 0`
- **Normal** (`ByteVector3`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x8) != 0`
- **Bitangent Y** (`normbyte`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x8) != 0`
- **Tangent** (`ByteVector3`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x18) == 0x18`
- **Bitangent Z** (`normbyte`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x18) == 0x18`
- **Vertex Colors** (`ByteColor4`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x20) != 0`
- **Bone Weights** (`hfloat`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x40) != 0`, `length`=`4`
- **Bone Indices** (`byte`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x40) != 0`, `length`=`4`
- **Eye Data** (`float`)
  - Attributes: `cond`=`(#ARG# #BITAND# 0x100) != 0`

