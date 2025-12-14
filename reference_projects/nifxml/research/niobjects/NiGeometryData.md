# Niobject `NiGeometryData`

Mesh data: vertices, vertex normals, etc.
    Bethesda 20.2.0.7 NIFs: NiParticlesData no longer inherits from NiGeometryData and inherits NiObject directly. 
    "Num Vertices" is renamed to "BS Max Vertices" for Bethesda 20.2 because Vertices, Normals, Tangents, Colors, and UV arrays
    do not have length for NiPSysData regardless of "Num" or booleans.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiGeometryData`

## Fields
- **Group ID** (`int`)
  - Attributes: `since`=`10.1.0.114`
  - Always zero.
- **Num Vertices** (`ushort`)
  - Attributes: `excludeT`=`NiPSysData`
  - Number of vertices.
- **Num Vertices** (`ushort`)
  - Attributes: `onlyT`=`NiPSysData`, `vercond`=`#NI_BS_LT_FO3#`
  - Number of vertices.
- **BS Max Vertices** (`ushort`)
  - Attributes: `onlyT`=`NiPSysData`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_FO3#`
  - Bethesda uses this for max number of particles in NiPSysData.
- **Keep Flags** (`byte`)
  - Attributes: `since`=`10.1.0.0`
  - Used with NiCollision objects when OBB or TRI is set.
- **Compress Flags** (`byte`)
  - Attributes: `since`=`10.1.0.0`
- **Has Vertices** (`bool`)
  - Attributes: `default`=`true`
  - Is the vertex array present? (Always non-zero.)
- **Vertices** (`Vector3`)
  - Attributes: `cond`=`Has Vertices`, `length`=`Num Vertices`
  - The mesh vertices.
- **Data Flags** (`NiGeometryDataFlags`)
  - Attributes: `since`=`10.0.1.0`, `vercond`=`!#BS202#`
- **BS Data Flags** (`BSGeometryDataFlags`)
  - Attributes: `vercond`=`#BS202#`
- **Material CRC** (`uint`)
  - Attributes: `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
- **Has Normals** (`bool`)
  - Do we have lighting normals? These are essential for proper lighting: if not present, the model will only be influenced by ambient light.
- **Normals** (`Vector3`)
  - Attributes: `cond`=`Has Normals`, `length`=`Num Vertices`
  - The lighting normals.
- **Tangents** (`Vector3`)
  - Attributes: `cond`=`(Has Normals) #AND# (((Data Flags #BITOR# BS Data Flags) #BITAND# 4096) != 0)`, `length`=`Num Vertices`, `since`=`10.1.0.0`
  - Tangent vectors.
- **Bitangents** (`Vector3`)
  - Attributes: `cond`=`(Has Normals) #AND# (((Data Flags #BITOR# BS Data Flags) #BITAND# 4096) != 0)`, `length`=`Num Vertices`, `since`=`10.1.0.0`
  - Bitangent vectors.
- **Has DIV2 Floats** (`bool`)
  - Attributes: `since`=`20.3.0.9`, `until`=`20.3.0.9`, `vercond`=`#DIVINITY2#`
- **DIV2 Floats** (`float`)
  - Attributes: `cond`=`Has DIV2 Floats`, `length`=`Num Vertices`, `since`=`20.3.0.9`, `until`=`20.3.0.9`, `vercond`=`#DIVINITY2#`
- **Bounding Sphere** (`NiBound`)
- **Has Vertex Colors** (`bool`)
  - Do we have vertex colors? These are usually used to fine-tune the lighting of the model.

Note: how vertex colors influence the model can be controlled by having a NiVertexColorProperty object as a property child of the root node. If this property object is not present, the vertex colors fine-tune lighting.

Note 2: set to either 0 or 0xFFFFFFFF for NifTexture compatibility.
- **Vertex Colors** (`Color4`)
  - Attributes: `cond`=`Has Vertex Colors`, `default`=`#VEC4_ONE#`, `length`=`Num Vertices`
  - The vertex colors.
- **Data Flags** (`NiGeometryDataFlags`)
  - Attributes: `until`=`4.2.2.0`
  - The lower 6 bits of this field represent the number of UV texture sets. The rest is unused.
- **Has UV** (`bool`)
  - Attributes: `until`=`4.0.0.2`
  - Do we have UV coordinates?

Note: for compatibility with NifTexture, set this value to either 0x00000000 or 0xFFFFFFFF.
- **UV Sets** (`TexCoord`)
  - Attributes: `length`=`((Data Flags #BITAND# 63) #BITOR# (BS Data Flags #BITAND# 1))`, `width`=`Num Vertices`
  - The UV texture coordinates. They follow the OpenGL standard: some programs may require you to flip the second coordinate.
- **Consistency Flags** (`ConsistencyType`)
  - Attributes: `default`=`CT_MUTABLE`, `since`=`10.0.1.0`
  - Consistency Flags
- **Additional Data** (`Ref`)
  - Attributes: `since`=`20.0.0.4`, `template`=`AbstractAdditionalGeometryData`

