# Struct `NiTFixedStringMap`

A mapping or hash table between NiFixedString keys and a generic value.
Currently, #T# must be a basic type due to nif.xml restrictions.

## Attributes
- **generic**: `true`
- **module**: `NiMain`
- **name**: `NiTFixedStringMap`

## Fields
- **Num Strings** (`uint`)
- **Strings** (`NiTFixedStringMapItem`)
  - Attributes: `length`=`Num Strings`, `template`=`#T#`

