# Enum `ConsistencyType`

Used by NiGeometryData to control the volatility of the mesh.
Consistency Type is masked to only the upper 4 bits (0xF000). Dirty mask is the lower 12 (0x0FFF) but only used at runtime.

## Attributes
- **name**: `ConsistencyType`
- **storage**: `ushort`

## Values
- `option` `CT_MUTABLE` (`value`=`0x0000`) – Mutable Mesh
- `option` `CT_STATIC` (`value`=`0x4000`) – Static Mesh
- `option` `CT_VOLATILE` (`value`=`0x8000`) – Volatile Mesh

