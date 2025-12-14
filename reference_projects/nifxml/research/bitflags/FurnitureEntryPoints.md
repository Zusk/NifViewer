# Bitflags `FurnitureEntryPoints`

Bethesda Animation. Furniture entry points. It specifies the direction(s) from where the actor is able to enter (and leave) the position.

## Attributes
- **name**: `FurnitureEntryPoints`
- **storage**: `ushort`
- **versions**: `#BETHESDA#`

## Values
- `option` `Front` (`bit`=`0`) – front entry point
- `option` `Behind` (`bit`=`1`) – behind entry point
- `option` `Right` (`bit`=`2`) – right entry point
- `option` `Left` (`bit`=`3`) – left entry point
- `option` `Up` (`bit`=`4`) – up entry point - unknown function. Used on some beds in Skyrim, probably for blocking of sleeping position.

