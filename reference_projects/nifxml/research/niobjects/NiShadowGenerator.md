# Niobject `NiShadowGenerator`

An NiShadowGenerator object is attached to an NiDynamicEffect object to inform the shadowing system that the effect produces shadows.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiShadowGenerator`

## Fields
- **Name** (`string`)
  - Attributes: `default`=`NiStandardShadowTechnique`
- **Flags** (`NiShadowGeneratorFlags`)
  - Attributes: `default`=`0x3DB`
- **Num Shadow Casters** (`uint`)
- **Shadow Casters** (`Ref`)
  - Attributes: `length`=`Num Shadow Casters`, `template`=`NiNode`
- **Num Shadow Receivers** (`uint`)
- **Shadow Receivers** (`Ref`)
  - Attributes: `length`=`Num Shadow Receivers`, `template`=`NiNode`
- **Target** (`Ptr`)
  - Attributes: `template`=`NiDynamicEffect`
- **Depth Bias** (`float`)
- **Size Hint** (`ushort`)
  - Attributes: `default`=`1024`
- **Near Clipping Distance** (`float`)
  - Attributes: `since`=`20.3.0.7`
- **Far Clipping Distance** (`float`)
  - Attributes: `since`=`20.3.0.7`
- **Directional Light Frustum Width** (`float`)
  - Attributes: `since`=`20.3.0.7`

