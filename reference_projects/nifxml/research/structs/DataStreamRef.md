# Struct `DataStreamRef`

## Attributes
- **module**: `NiMesh`
- **name**: `DataStreamRef`
- **since**: `V20_5_0_0`

## Fields
- **Stream** (`Ref`)
  - Attributes: `template`=`NiDataStream`
  - Reference to a data stream object which holds the data used by this reference.
- **Is Per Instance** (`bool`)
  - Attributes: `default`=`false`
  - Sets whether this stream data is per-instance data for use in hardware instancing.
- **Num Submeshes** (`ushort`)
  - Attributes: `default`=`1`
  - The number of submesh-to-region mappings that this data stream has.
- **Submesh To Region Map** (`ushort`)
  - Attributes: `length`=`Num Submeshes`
  - A lookup table that maps submeshes to regions.
- **Num Components** (`uint`)
  - Attributes: `default`=`1`
- **Component Semantics** (`SemanticData`)
  - Attributes: `length`=`Num Components`
  - Describes the semantic of each component.

