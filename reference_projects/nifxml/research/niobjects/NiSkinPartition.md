# Niobject `NiSkinPartition`

Skinning data, optimized for hardware skinning. The mesh is partitioned in submeshes such that each vertex of a submesh is influenced only by a limited and fixed number of bones.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiSkinPartition`

## Fields
- **Num Partitions** (`uint`)
- **Data Size** (`uint`)
  - Attributes: `calc`=`#LEN[Vertex Data]# #MUL# Vertex Size`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_SSE#`
- **Vertex Size** (`uint`)
  - Attributes: `calc`=`(Vertex Desc #BITAND# 0xF) #MUL# 4`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_SSE#`
- **Vertex Desc** (`BSVertexDesc`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_SSE#`
- **Vertex Data** (`BSVertexDataSSE`)
  - Attributes: `arg`=`Vertex Desc #RSH# 44`, `cond`=`Data Size #GT# 0`, `length`=`Data Size / Vertex Size`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_SSE#`
- **Partitions** (`SkinPartition`)
  - Attributes: `length`=`Num Partitions`

