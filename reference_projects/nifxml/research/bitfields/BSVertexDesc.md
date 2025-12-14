# Bitfield `BSVertexDesc`

## Attributes
- **name**: `BSVertexDesc`
- **storage**: `uint64`
- **versions**: `#SSE# #FO4# #F76#`

## Values
- `member` `Vertex Data Size` (`mask`=`0xF`, `pos`=`0`, `type`=`uint`, `width`=`4`)
- `member` `Dynamic Vertex Size` (`mask`=`0xF0`, `pos`=`4`, `type`=`uint`, `width`=`4`)
- `member` `UV1 Offset` (`mask`=`0xF00`, `pos`=`8`, `type`=`uint`, `width`=`4`)
- `member` `UV2 Offset` (`mask`=`0xF000`, `pos`=`12`, `type`=`uint`, `width`=`4`)
- `member` `Normal Offset` (`mask`=`0xF0000`, `pos`=`16`, `type`=`uint`, `width`=`4`)
- `member` `Tangent Offset` (`mask`=`0xF00000`, `pos`=`20`, `type`=`uint`, `width`=`4`)
- `member` `Color Offset` (`mask`=`0xF000000`, `pos`=`24`, `type`=`uint`, `width`=`4`)
- `member` `Skinning Data Offset` (`mask`=`0xF0000000`, `pos`=`28`, `type`=`uint`, `width`=`4`)
- `member` `Landscape Data Offset` (`mask`=`0xF00000000`, `pos`=`32`, `type`=`uint`, `width`=`4`)
- `member` `Eye Data Offset` (`mask`=`0xF000000000`, `pos`=`36`, `type`=`uint`, `width`=`4`)
- `member` `Unused 01` (`mask`=`0xF0000000000`, `pos`=`40`, `type`=`uint`, `width`=`4`)
- `member` `Vertex Attributes` (`mask`=`0xFFF00000000000`, `pos`=`44`, `type`=`VertexAttribute`, `width`=`12`)

