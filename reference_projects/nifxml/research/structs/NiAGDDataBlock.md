# Struct `NiAGDDataBlock`

## Attributes
- **module**: `NiMain`
- **name**: `NiAGDDataBlock`

## Fields
- **Block Size** (`uint`)
- **Num Blocks** (`uint`)
- **Block Offsets** (`uint`)
  - Attributes: `length`=`Num Blocks`
- **Num Data** (`uint`)
- **Data Sizes** (`uint`)
  - Attributes: `length`=`Num Data`
- **Data** (`byte`)
  - Attributes: `length`=`Num Data`, `width`=`Block Size`
- **Shader Index** (`uint`)
  - Attributes: `cond`=`#ARG# == 1`
- **Total Size** (`uint`)
  - Attributes: `cond`=`#ARG# == 1`

