# Niobject `NiMesh`

## Attributes
- **inherit**: `NiRenderObject`
- **module**: `NiMesh`
- **name**: `NiMesh`
- **since**: `V20_5_0_0`

## Fields
- **Primitive Type** (`MeshPrimitiveType`)
  - The primitive type of the mesh, such as triangles or lines.
- **EM Data** (`MeshDataEpicMickey`)
  - Attributes: `since`=`20.6.5.0`, `until`=`20.6.5.0`
- **Num Submeshes** (`ushort`)
  - The number of submeshes contained in this mesh.
- **Instancing Enabled** (`bool`)
  - Sets whether hardware instancing is being used.
- **Bounding Sphere** (`NiBound`)
  - The combined bounding volume of all submeshes.
- **Num Datastreams** (`uint`)
- **Datastreams** (`DataStreamRef`)
  - Attributes: `length`=`Num Datastreams`
- **Num Modifiers** (`uint`)
- **Modifiers** (`Ref`)
  - Attributes: `length`=`Num Modifiers`, `template`=`NiMeshModifier`
- **Has Extra EM Data** (`bool`)
  - Attributes: `since`=`20.6.5.0`, `until`=`20.6.5.0`, `vercond`=`#USER# #GT# 9`
- **Extra EM Data** (`ExtraMeshDataEpicMickey`)
  - Attributes: `cond`=`Has Extra EM Data`, `since`=`20.6.5.0`, `until`=`20.6.5.0`, `vercond`=`#USER# #GT# 9`

