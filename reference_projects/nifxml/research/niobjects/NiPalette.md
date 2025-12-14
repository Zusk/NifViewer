# Niobject `NiPalette`

NiPalette objects represent mappings from 8-bit indices to 24-bit RGB or 32-bit RGBA colors.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiPalette`

## Fields
- **Has Alpha** (`byte`)
  - Not boolean but used as one, always 8-bit.
- **Num Entries** (`uint`)
  - Attributes: `default`=`256`
  - The number of palette entries. Always 256 but can also be 16.
- **Palette** (`ByteColor4`)
  - Attributes: `cond`=`Num Entries == 16`, `length`=`16`
  - The color palette.
- **Palette** (`ByteColor4`)
  - Attributes: `cond`=`Num Entries != 16`, `length`=`256`
  - The color palette.

