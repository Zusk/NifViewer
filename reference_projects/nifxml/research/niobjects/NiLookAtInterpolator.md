# Niobject `NiLookAtInterpolator`

NiLookAtInterpolator rotates an object so that it always faces a target object.

## Attributes
- **inherit**: `NiInterpolator`
- **module**: `NiAnimation`
- **name**: `NiLookAtInterpolator`

## Fields
- **Flags** (`LookAtFlags`)
- **Look At** (`Ptr`)
  - Attributes: `template`=`NiNode`
- **Look At Name** (`string`)
- **Transform** (`NiQuatTransform`)
  - Attributes: `until`=`20.4.0.12`
- **Interpolator: Translation** (`Ref`)
  - Attributes: `template`=`NiPoint3Interpolator`
- **Interpolator: Roll** (`Ref`)
  - Attributes: `template`=`NiFloatInterpolator`
- **Interpolator: Scale** (`Ref`)
  - Attributes: `template`=`NiFloatInterpolator`

