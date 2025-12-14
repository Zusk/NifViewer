# Struct `BoneData`

NiSkinData::BoneData. Skinning data component.

## Attributes
- **module**: `NiMain`
- **name**: `BoneData`

## Fields
- **Skin Transform** (`NiTransform`)
  - Offset of the skin from this bone in bind position.
- **Bounding Sphere** (`NiBound`)
  - Note that its a Sphere Containing Axis Aligned Box not a minimum volume Sphere
- **Num Vertices** (`ushort`)
  - Number of weighted vertices.
- **Vertex Weights** (`BoneVertData`)
  - Attributes: `length`=`Num Vertices`, `until`=`4.2.1.0`
  - The vertex weights.
- **Vertex Weights** (`BoneVertData`)
  - Attributes: `cond`=`#ARG# != 0`, `length`=`Num Vertices`, `since`=`4.2.2.0`
  - The vertex weights.

