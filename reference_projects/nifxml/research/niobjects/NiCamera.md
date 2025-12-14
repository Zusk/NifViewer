# Niobject `NiCamera`

Camera object.

## Attributes
- **inherit**: `NiAVObject`
- **module**: `NiMain`
- **name**: `NiCamera`

## Fields
- **Camera Flags** (`ushort`)
  - Attributes: `since`=`10.1.0.0`
  - Obsolete flags.
- **Frustum Left** (`float`)
  - Attributes: `default`=`-0.63707`
  - Frustrum left.
- **Frustum Right** (`float`)
  - Attributes: `default`=`0.63707`
  - Frustrum right.
- **Frustum Top** (`float`)
  - Attributes: `default`=`0.385714`
  - Frustrum top.
- **Frustum Bottom** (`float`)
  - Attributes: `default`=`-0.385714`
  - Frustrum bottom.
- **Frustum Near** (`float`)
  - Attributes: `default`=`1.0`
  - Frustrum near.
- **Frustum Far** (`float`)
  - Attributes: `default`=`5000.0`
  - Frustrum far.
- **Use Orthographic Projection** (`bool`)
  - Attributes: `since`=`10.1.0.0`
  - Determines whether perspective is used.  Orthographic means no perspective.
- **Viewport Left** (`float`)
  - Viewport left.
- **Viewport Right** (`float`)
  - Attributes: `default`=`1.0`
  - Viewport right.
- **Viewport Top** (`float`)
  - Attributes: `default`=`1.0`
  - Viewport top.
- **Viewport Bottom** (`float`)
  - Viewport bottom.
- **LOD Adjust** (`float`)
  - Attributes: `default`=`1.0`
  - Level of detail adjust.
- **Scene** (`Ref`)
  - Attributes: `template`=`NiAVObject`
- **Num Screen Polygons** (`uint`)
  - Attributes: `calc`=`0`
  - Deprecated. Array is always zero length on disk write.
- **Num Screen Textures** (`uint`)
  - Attributes: `calc`=`0`, `since`=`4.2.1.0`
  - Deprecated. Array is always zero length on disk write.
- **Unknown Int 3** (`uint`)
  - Attributes: `until`=`3.1`

