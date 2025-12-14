# Bitfield `CollisionFilterFlags`

## Attributes
- **name**: `CollisionFilterFlags`
- **storage**: `byte`

## Values
- `member` `Biped Part` (`mask`=`0x001F`, `pos`=`0`, `type`=`BipedPart`, `width`=`5`) – Used only if the Layer is 8 (or 32/33 for Skyrim and later).
- `member` `MOPP Scaled` (`mask`=`0x0020`, `pos`=`5`, `type`=`bool`, `width`=`1`)
- `member` `No Collision` (`mask`=`0x0040`, `pos`=`6`, `type`=`bool`, `width`=`1`)
- `member` `Linked Group` (`mask`=`0x0080`, `pos`=`7`, `type`=`bool`, `width`=`1`) – If Layer is CHARCONTROLLER (CC), true means "CC Trigger Only".

