# Niobject `NiBSplineData`

Contains one or more sets of control points for use in interpolation of open, uniform B-Splines, stored as either float or compact.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiAnimation`
- **name**: `NiBSplineData`

## Fields
- **Num Float Control Points** (`uint`)
- **Float Control Points** (`float`)
  - Attributes: `length`=`Num Float Control Points`
  - Float values representing the control data.
- **Num Compact Control Points** (`uint`)
- **Compact Control Points** (`short`)
  - Attributes: `length`=`Num Compact Control Points`
  - Signed shorts representing the data from 0 to 1 (scaled by SHRT_MAX).

