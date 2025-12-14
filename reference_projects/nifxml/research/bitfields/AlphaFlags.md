# Bitfield `AlphaFlags`

Flags for NiAlphaProperty

## Attributes
- **name**: `AlphaFlags`
- **storage**: `ushort`

## Values
- `member` `Alpha Blend` (`mask`=`0x0001`, `pos`=`0`, `type`=`bool`, `width`=`1`)
- `member` `Source Blend Mode` (`default`=`SRC_ALPHA`, `mask`=`0x001E`, `pos`=`1`, `type`=`AlphaFunction`, `width`=`4`)
- `member` `Destination Blend Mode` (`default`=`INV_SRC_ALPHA`, `mask`=`0x01E0`, `pos`=`5`, `type`=`AlphaFunction`, `width`=`4`)
- `member` `Alpha Test` (`default`=`true`, `mask`=`0x0200`, `pos`=`9`, `type`=`bool`, `width`=`1`)
- `member` `Test Func` (`default`=`TEST_GREATER`, `mask`=`0x1C00`, `pos`=`10`, `type`=`TestFunction`, `width`=`3`)
- `member` `No Sorter` (`mask`=`0x2000`, `pos`=`13`, `type`=`bool`, `width`=`1`)
- `member` `Clone Unique` (`mask`=`0x4000`, `pos`=`14`, `type`=`bool`, `width`=`1`) – Bethesda-only. Always true for weapon blood after FO3.
- `member` `Editor Alpha Threshold` (`mask`=`0x8000`, `pos`=`15`, `type`=`bool`, `width`=`1`) – Bethesda-only. True if the Alpha Threshold is externally controlled.

