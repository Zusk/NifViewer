# Niobject `NiBSplineTransformInterpolator`

Supports the animation of position, rotation, and scale using an NiQuatTransform.
The NiQuatTransform can be an unchanging pose or interpolated from B-Spline control point channels.

## Attributes
- **inherit**: `NiBSplineInterpolator`
- **module**: `NiAnimation`
- **name**: `NiBSplineTransformInterpolator`

## Fields
- **Transform** (`NiQuatTransform`)
- **Translation Handle** (`uint`)
  - Attributes: `default`=`0xFFFF`
  - Handle into the translation data. (USHRT_MAX for invalid handle.)
- **Rotation Handle** (`uint`)
  - Attributes: `default`=`0xFFFF`
  - Handle into the rotation data. (USHRT_MAX for invalid handle.)
- **Scale Handle** (`uint`)
  - Attributes: `default`=`0xFFFF`
  - Handle into the scale data. (USHRT_MAX for invalid handle.)

