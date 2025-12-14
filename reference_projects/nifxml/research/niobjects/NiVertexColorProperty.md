# Niobject `NiVertexColorProperty`

Property of vertex colors. This object is referred to by the root object of the NIF file whenever some NiTriShapeData object has vertex colors with non-default settings; if not present, vertex colors have vertex_mode=2 and lighting_mode=1.

## Attributes
- **inherit**: `NiProperty`
- **module**: `NiMain`
- **name**: `NiVertexColorProperty`

## Fields
- **Flags** (`VertexColorFlags`)
- **Vertex Mode** (`SourceVertexMode`)
  - Attributes: `until`=`20.0.0.5`
  - In Flags from 20.1.0.3 on.
- **Lighting Mode** (`LightingMode`)
  - Attributes: `until`=`20.0.0.5`
  - In Flags from 20.1.0.3 on.

