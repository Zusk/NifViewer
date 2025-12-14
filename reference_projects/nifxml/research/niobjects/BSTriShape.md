# Niobject `BSTriShape`

Fallout 4 Tri Shape

## Attributes
- **inherit**: `NiAVObject`
- **module**: `BSMain`
- **name**: `BSTriShape`
- **versions**: `#SSE# #FO4# #F76#`

## Fields
- **Bounding Sphere** (`NiBound`)
- **Bound Min Max** (`float`)
  - Attributes: `length`=`6`, `vercond`=`#BS_F76#`
- **Skin** (`Ref`)
  - Attributes: `template`=`NiObject`
- **Shader Property** (`Ref`)
  - Attributes: `template`=`BSShaderProperty`
- **Alpha Property** (`Ref`)
  - Attributes: `template`=`NiAlphaProperty`
- **Vertex Desc** (`BSVertexDesc`)
- **Num Triangles** (`uint`)
  - Attributes: `vercond`=`#BS_GTE_130#`
- **Num Triangles** (`ushort`)
  - Attributes: `vercond`=`#NI_BS_LT_FO4#`
- **Num Vertices** (`ushort`)
- **Data Size** (`uint`)
  - Attributes: `calc`=`((Vertex Desc #BITAND# 0xF) #MUL# Num Vertices #MUL# 4) #ADD# (Num Triangles #MUL# 6)`
- **Vertex Data** (`BSVertexData`)
  - Attributes: `arg`=`Vertex Desc #RSH# 44`, `cond`=`Data Size #GT# 0`, `length`=`Num Vertices`, `vercond`=`#BS_GTE_130#`
- **Vertex Data** (`BSVertexDataSSE`)
  - Attributes: `arg`=`Vertex Desc #RSH# 44`, `cond`=`Data Size #GT# 0`, `length`=`Num Vertices`, `vercond`=`#BS_SSE#`
- **Triangles** (`Triangle`)
  - Attributes: `cond`=`Data Size #GT# 0`, `length`=`Num Triangles`
- **Particle Data Size** (`uint`)
  - Attributes: `calc`=`(Num Vertices #MUL# 6) #ADD# (Num Triangles #MUL# 3)`, `vercond`=`#BS_SSE#`
- **Particle Vertices** (`HalfVector3`)
  - Attributes: `cond`=`Particle Data Size #GT# 0`, `length`=`Num Vertices`, `vercond`=`#BS_SSE#`
- **Particle Normals** (`HalfVector3`)
  - Attributes: `cond`=`Particle Data Size #GT# 0`, `length`=`Num Vertices`, `vercond`=`#BS_SSE#`
- **Particle Triangles** (`Triangle`)
  - Attributes: `cond`=`Particle Data Size #GT# 0`, `length`=`Num Triangles`, `vercond`=`#BS_SSE#`

