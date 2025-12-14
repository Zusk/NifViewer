# Struct `Key`

A generic key with support for interpolation. Type 1 is normal linear interpolation, type 2 has forward and backward tangents, and type 3 has tension, bias and continuity arguments. Note that color4 and byte always seem to be of type 1.

## Attributes
- **generic**: `true`
- **module**: `NiMain`
- **name**: `Key`

## Fields
- **Time** (`float`)
  - Time of the key.
- **Value** (`#T#`)
  - The key value.
- **Forward** (`#T#`)
  - Attributes: `cond`=`#ARG# == 2`
  - Key forward tangent.
- **Backward** (`#T#`)
  - Attributes: `cond`=`#ARG# == 2`
  - The key backward tangent.
- **TBC** (`TBC`)
  - Attributes: `cond`=`#ARG# == 3`
  - The TBC of the key.

