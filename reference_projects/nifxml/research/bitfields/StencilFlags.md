# Bitfield `StencilFlags`

Flags for NiStencilProperty

## Attributes
- **name**: `StencilFlags`
- **storage**: `ushort`

## Values
- `member` `Enable` (`mask`=`0x0001`, `pos`=`0`, `type`=`bool`, `width`=`1`)
- `member` `Fail Action` (`default`=`ACTION_KEEP`, `mask`=`0x000E`, `pos`=`1`, `type`=`StencilAction`, `width`=`3`)
- `member` `ZFail Action` (`default`=`ACTION_KEEP`, `mask`=`0x0070`, `pos`=`4`, `type`=`StencilAction`, `width`=`3`)
- `member` `Pass Action` (`default`=`ACTION_INCREMENT`, `mask`=`0x0380`, `pos`=`7`, `type`=`StencilAction`, `width`=`3`)
- `member` `Draw Mode` (`default`=`DRAW_BOTH`, `mask`=`0x0C00`, `pos`=`10`, `type`=`StencilDrawMode`, `width`=`2`)
- `member` `Test Func` (`default`=`TEST_GREATER`, `mask`=`0xF000`, `pos`=`12`, `type`=`StencilTestFunc`, `width`=`3`)

