# Struct `KeyGroup`

Array of vector keys (anything that can be interpolated, except rotations).

## Attributes
- **generic**: `true`
- **module**: `NiMain`
- **name**: `KeyGroup`

## Fields
- **Num Keys** (`uint`)
  - Number of keys in the array.
- **Interpolation** (`KeyType`)
  - Attributes: `cond`=`Num Keys != 0`
  - The key type.
- **Keys** (`Key`)
  - Attributes: `arg`=`Interpolation`, `length`=`Num Keys`, `template`=`#T#`
  - The keys.

