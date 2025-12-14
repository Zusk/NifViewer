# Enum `StencilDrawMode`

Describes the face culling options for NiStencilProperty.

## Attributes
- **name**: `StencilDrawMode`
- **storage**: `uint`

## Values
- `option` `DRAW_CCW_OR_BOTH` (`value`=`0`) – Application default, chooses between DRAW_CCW or DRAW_BOTH.
- `option` `DRAW_CCW` (`value`=`1`) – Draw only the triangles whose vertices are ordered CCW with respect to the viewer. (Standard behavior)
- `option` `DRAW_CW` (`value`=`2`) – Draw only the triangles whose vertices are ordered CW with respect to the viewer. (Effectively flips faces)
- `option` `DRAW_BOTH` (`value`=`3`) – Draw all triangles, regardless of orientation. (Effectively force double-sided)

