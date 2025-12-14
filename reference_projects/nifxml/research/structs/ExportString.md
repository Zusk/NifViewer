# Struct `ExportString`

Specific to Bethesda-specific header export strings.

## Attributes
- **module**: `BSMain`
- **name**: `ExportString`

## Fields
- **Length** (`byte`)
  - The string length.
- **Value** (`char`)
  - Attributes: `length`=`Length`
  - The string itself, null terminated (the null terminator is taken into account in the length byte).

