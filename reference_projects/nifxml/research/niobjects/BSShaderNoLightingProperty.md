# Niobject `BSShaderNoLightingProperty`

Bethesda-specific property.

## Attributes
- **inherit**: `BSShaderLightingProperty`
- **module**: `BSMain`
- **name**: `BSShaderNoLightingProperty`
- **versions**: `#FO3#`

## Fields
- **File Name** (`SizedString`)
  - The texture glow map.
- **Falloff Start Angle** (`float`)
  - Attributes: `default`=`1.0`, `range`=`#F_NRM#`, `vercond`=`#BSVER# #GT# 26`
  - At this cosine of angle falloff will be equal to Falloff Start Opacity
- **Falloff Stop Angle** (`float`)
  - Attributes: `default`=`0.0`, `range`=`#F_NRM#`, `vercond`=`#BSVER# #GT# 26`
  - At this cosine of angle falloff will be equal to Falloff Stop Opacity
- **Falloff Start Opacity** (`float`)
  - Attributes: `default`=`1.0`, `range`=`#F0_1#`, `vercond`=`#BSVER# #GT# 26`
  - Alpha falloff multiplier at start angle
- **Falloff Stop Opacity** (`float`)
  - Attributes: `default`=`0.0`, `range`=`#F0_1#`, `vercond`=`#BSVER# #GT# 26`
  - Alpha falloff multiplier at end angle

