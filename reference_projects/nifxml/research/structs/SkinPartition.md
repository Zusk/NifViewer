# Struct `SkinPartition`

Skinning data for a submesh, optimized for hardware skinning. Part of NiSkinPartition.

## Attributes
- **module**: `NiMain`
- **name**: `SkinPartition`
- **since**: `V4_2_1_0`

## Fields
- **Num Vertices** (`ushort`)
  - Number of vertices in this submesh.
- **Num Triangles** (`ushort`)
  - Attributes: `calc`=`(#LEN[Strips]# #GT# 0) #THEN# (#LEN2[Strips]# - 2) #ELSE# (#LEN[Triangles]#)`
  - Number of triangles in this submesh.
- **Num Bones** (`ushort`)
  - Number of bones influencing this submesh.
- **Num Strips** (`ushort`)
  - Number of strips in this submesh (zero if not stripped).
- **Num Weights Per Vertex** (`ushort`)
  - Attributes: `default`=`4`
  - Number of weight coefficients per vertex. The Gamebryo engine seems to work well only if this number is equal to 4, even if there are less than 4 influences per vertex.
- **Bones** (`ushort`)
  - Attributes: `length`=`Num Bones`
  - List of bones.
- **Has Vertex Map** (`bool`)
  - Attributes: `since`=`10.1.0.0`
  - Do we have a vertex map?
- **Vertex Map** (`ushort`)
  - Attributes: `length`=`Num Vertices`, `until`=`10.0.1.2`
  - Maps the weight/influence lists in this submesh to the vertices in the shape being skinned.
- **Vertex Map** (`ushort`)
  - Attributes: `cond`=`Has Vertex Map`, `length`=`Num Vertices`, `since`=`10.1.0.0`
  - Maps the weight/influence lists in this submesh to the vertices in the shape being skinned.
- **Has Vertex Weights** (`bool`)
  - Attributes: `since`=`10.1.0.0`
  - Do we have vertex weights?
- **Vertex Weights** (`float`)
  - Attributes: `length`=`Num Vertices`, `until`=`10.0.1.2`, `width`=`Num Weights Per Vertex`
  - The vertex weights.
- **Vertex Weights** (`float`)
  - Attributes: `cond`=`Has Vertex Weights`, `length`=`Num Vertices`, `since`=`10.1.0.0`, `width`=`Num Weights Per Vertex`
  - The vertex weights.
- **Strip Lengths** (`ushort`)
  - Attributes: `length`=`Num Strips`
  - The strip lengths.
- **Has Faces** (`bool`)
  - Attributes: `calc`=`(#LEN[Strips]# #ADD# #LEN[Triangles]#) #GT# 0 #THEN# true #ELSE# false`, `since`=`10.1.0.0`
  - Do we have triangle or strip data?
- **Strips** (`ushort`)
  - Attributes: `cond`=`Num Strips != 0`, `length`=`Num Strips`, `until`=`10.0.1.2`, `width`=`Strip Lengths`
  - The strips.
- **Strips** (`ushort`)
  - Attributes: `cond`=`(Has Faces) #AND# (Num Strips != 0)`, `length`=`Num Strips`, `since`=`10.1.0.0`, `width`=`Strip Lengths`
  - The strips.
- **Triangles** (`Triangle`)
  - Attributes: `cond`=`Num Strips == 0`, `length`=`Num Triangles`, `until`=`10.0.1.2`
  - The triangles.
- **Triangles** (`Triangle`)
  - Attributes: `cond`=`(Has Faces) #AND# (Num Strips == 0)`, `length`=`Num Triangles`, `since`=`10.1.0.0`
  - The triangles.
- **Has Bone Indices** (`bool`)
  - Do we have bone indices?
- **Bone Indices** (`byte`)
  - Attributes: `cond`=`Has Bone Indices`, `length`=`Num Vertices`, `width`=`Num Weights Per Vertex`
  - Bone indices, they index into 'Bones'.
- **LOD Level** (`byte`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
- **Global VB** (`bool`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
- **Vertex Desc** (`BSVertexDesc`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_SSE#`
- **Triangles Copy** (`Triangle`)
  - Attributes: `length`=`Num Triangles`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_SSE#`

