# Niobject `NiBSplineInterpolator`

Abstract base class for interpolators storing data via a B-spline.

## Attributes
- **abstract**: `true`
- **inherit**: `NiInterpolator`
- **module**: `NiAnimation`
- **name**: `NiBSplineInterpolator`

## Fields
- **Start Time** (`float`)
  - Attributes: `default`=`#FLT_MAX#`
  - Animation start time.
- **Stop Time** (`float`)
  - Attributes: `default`=`#FLT_MIN#`
  - Animation stop time.
- **Spline Data** (`Ref`)
  - Attributes: `template`=`NiBSplineData`
- **Basis Data** (`Ref`)
  - Attributes: `template`=`NiBSplineBasisData`

