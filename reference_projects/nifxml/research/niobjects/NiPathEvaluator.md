# Niobject `NiPathEvaluator`

## Attributes
- **inherit**: `NiKeyBasedEvaluator`
- **module**: `NiAnimation`
- **name**: `NiPathEvaluator`
- **since**: `V20_5_0_0`

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

