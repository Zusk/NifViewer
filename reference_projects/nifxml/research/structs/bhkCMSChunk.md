# Struct `bhkCMSChunk`

Bethesda extension of hkpCompressedMeshShape::Chunk. A compressed chunk of hkpCompressedMeshShape geometry.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkCMSChunk`
- **versions**: `#SKY_AND_LATER#`

## Fields
- **Translation** (`Vector4`)
- **Material Index** (`uint`)
  - Index of material in bhkCompressedMeshShapeData::Chunk Materials
- **Reference** (`ushort`)
  - Attributes: `default`=`#USHRT_MAX#`
  - Index of another chunk in the chunks list.
- **Transform Index** (`ushort`)
  - Index of transformation in bhkCompressedMeshShapeData::Chunk Transforms
- **Num Vertices** (`uint`)
  - Number of vertices, multiplied by 3.
- **Vertices** (`UshortVector3`)
  - Attributes: `length`=`Num Vertices #DIV# 3`
  - Vertex positions in havok coordinates*1000.
- **Num Indices** (`uint`)
- **Indices** (`ushort`)
  - Attributes: `length`=`Num Indices`
  - Vertex indices as used by strips.
- **Num Strips** (`uint`)
- **Strips** (`ushort`)
  - Attributes: `length`=`Num Strips`
  - Length of strips longer than one triangle.
- **Num Welding Info** (`uint`)
  - Generally the same as Num Indices field.
- **Welding Info** (`bhkWeldInfo`)
  - Attributes: `length`=`Num Welding Info`

