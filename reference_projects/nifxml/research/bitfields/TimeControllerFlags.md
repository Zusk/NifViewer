# Bitfield `TimeControllerFlags`

Flags for NiTimeController

## Attributes
- **name**: `TimeControllerFlags`
- **storage**: `ushort`

## Values
- `member` `Anim Type` (`mask`=`0x0001`, `pos`=`0`, `type`=`AnimType`, `width`=`1`)
- `member` `Cycle Type` (`default`=`CYCLE_CLAMP`, `mask`=`0x0006`, `pos`=`1`, `type`=`CycleType`, `width`=`2`)
- `member` `Active` (`default`=`true`, `mask`=`0x0008`, `pos`=`3`, `type`=`bool`, `width`=`1`)
- `member` `Play Backwards` (`mask`=`0x0010`, `pos`=`4`, `type`=`bool`, `width`=`1`)
- `member` `Manager Controlled` (`mask`=`0x0020`, `pos`=`5`, `type`=`bool`, `width`=`1`)
- `member` `Compute Scaled Time` (`default`=`true`, `mask`=`0x0040`, `pos`=`6`, `type`=`bool`, `width`=`1`)
- `member` `Forced Update` (`mask`=`0x0080`, `pos`=`7`, `type`=`bool`, `width`=`1`)

