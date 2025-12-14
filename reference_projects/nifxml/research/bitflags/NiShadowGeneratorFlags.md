# Bitflags `NiShadowGeneratorFlags`

Flags for NiShadowGenerator.
Bit Patterns:
    AUTO_CALC_NEARFAR = (AUTO_NEAR_DIST | AUTO_FAR_DIST) = 0xC0
    AUTO_CALC_FULL = (AUTO_NEAR_DIST | AUTO_FAR_DIST | AUTO_DIR_LIGHT_FRUSTUM_WIDTH | AUTO_DIR_LIGHT_FRUSTUM_POSITION) = 0x3C0

## Attributes
- **name**: `NiShadowGeneratorFlags`
- **storage**: `ushort`

## Values
- `option` `DIRTY_SHADOWMAP` (`bit`=`0`)
- `option` `DIRTY_RENDERVIEWS` (`bit`=`1`)
- `option` `GEN_STATIC` (`bit`=`2`)
- `option` `GEN_ACTIVE` (`bit`=`3`)
- `option` `RENDER_BACKFACES` (`bit`=`4`)
- `option` `STRICTLY_OBSERVE_SIZE_HINT` (`bit`=`5`)
- `option` `AUTO_NEAR_DIST` (`bit`=`6`)
- `option` `AUTO_FAR_DIST` (`bit`=`7`)
- `option` `AUTO_DIR_LIGHT_FRUSTUM_WIDTH` (`bit`=`8`)
- `option` `AUTO_DIR_LIGHT_FRUSTUM_POSITION` (`bit`=`9`)

