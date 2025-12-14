# Niobject `NiPathController`

DEPRECATED (10.2), REMOVED (20.5). Replaced by NiTransformController and NiPathInterpolator.
Time controller for a path.

## Attributes
- **inherit**: `NiTimeController`
- **module**: `NiAnimation`
- **name**: `NiPathController`
- **until**: `V20_5_0_0`

## Fields
- **Path Flags** (`PathFlags`)
  - Attributes: `since`=`10.1.0.0`
- **Bank Dir** (`int`)
  - Attributes: `default`=`1`
  - -1 = Negative, 1 = Positive
- **Max Bank Angle** (`float`)
  - Max angle in radians.
- **Smoothing** (`float`)
- **Follow Axis** (`short`)
  - 0, 1, or 2 representing X, Y, or Z.
- **Path Data** (`Ref`)
  - Attributes: `template`=`NiPosData`
- **Percent Data** (`Ref`)
  - Attributes: `template`=`NiFloatData`

