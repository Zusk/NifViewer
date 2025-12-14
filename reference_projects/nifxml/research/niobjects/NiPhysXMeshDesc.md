# Niobject `NiPhysXMeshDesc`

Holds mesh data for streaming.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiPhysX`
- **name**: `NiPhysXMeshDesc`
- **since**: `V20_2_0_8`

## Fields
- **Is Convex** (`bool`)
  - Attributes: `until`=`20.3.0.4`
- **Mesh Name** (`NiFixedString`)
- **Mesh Data** (`ByteArray`)
- **Back Compat Vertex Map Size** (`ushort`)
  - Attributes: `since`=`20.3.0.5`, `until`=`30.2.0.2`
- **Back Compat Vertex Map** (`ushort`)
  - Attributes: `length`=`Back Compat Vertex Map Size`, `since`=`20.3.0.5`, `until`=`30.2.0.2`
- **Mesh Flags** (`uint`)
- **Mesh Paging Mode** (`uint`)
  - Attributes: `since`=`20.3.0.1`
- **Is Hardware** (`bool`)
  - Attributes: `since`=`20.3.0.2`, `until`=`20.3.0.4`
- **Flags** (`byte`)
  - Attributes: `since`=`20.3.0.5`

