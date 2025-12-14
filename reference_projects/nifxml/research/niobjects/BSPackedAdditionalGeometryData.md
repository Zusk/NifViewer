# Niobject `BSPackedAdditionalGeometryData`

No longer appears to occur in any vanilla files. Older versions of FNV had the block in nvdlc01vaultposter01.nif.

## Attributes
- **inherit**: `AbstractAdditionalGeometryData`
- **module**: `BSMain`
- **name**: `BSPackedAdditionalGeometryData`
- **versions**: `#BETHESDA#`

## Fields
- **Num Vertices** (`ushort`)
- **Num Block Infos** (`uint`)
- **Block Infos** (`NiAGDDataStream`)
  - Attributes: `length`=`Num Block Infos`
- **Num Blocks** (`uint`)
  - Usually there is exactly one block.
- **Blocks** (`NiAGDDataBlocks`)
  - Attributes: `arg`=`1`, `length`=`Num Blocks`

