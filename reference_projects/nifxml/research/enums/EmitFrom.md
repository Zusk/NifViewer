# Enum `EmitFrom`

Controls which parts of the mesh that the particles are emitted from.

## Attributes
- **name**: `EmitFrom`
- **storage**: `uint`

## Values
- `option` `EMIT_FROM_VERTICES` (`value`=`0`) – Emit particles from the vertices of the mesh.
- `option` `EMIT_FROM_FACE_CENTER` (`value`=`1`) – Emit particles from the center of the faces of the mesh.
- `option` `EMIT_FROM_EDGE_CENTER` (`value`=`2`) – Emit particles from the center of the edges of the mesh.
- `option` `EMIT_FROM_FACE_SURFACE` (`value`=`3`) – Perhaps randomly emit particles from anywhere on the faces of the mesh?
- `option` `EMIT_FROM_EDGE_SURFACE` (`value`=`4`) – Perhaps randomly emit particles from anywhere on the edges of the mesh?

