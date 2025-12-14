# Niobject `NiPathInterpolator`

Used to make an object follow a predefined spline path.

## Attributes
- **inherit**: `NiKeyBasedInterpolator`
- **module**: `NiAnimation`
- **name**: `NiPathInterpolator`

## Fields
- **Flags** (`PathFlags`)
  - Attributes: `default`=`3`
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

