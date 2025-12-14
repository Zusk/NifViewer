# Niobject `BSShaderPPLightingProperty`

Bethesda-specific property.

## Attributes
- **inherit**: `BSShaderLightingProperty`
- **module**: `BSMain`
- **name**: `BSShaderPPLightingProperty`
- **versions**: `#FO3#`

## Fields
- **Texture Set** (`Ref`)
  - Attributes: `template`=`BSShaderTextureSet`
  - Texture Set
- **Refraction Strength** (`float`)
  - Attributes: `default`=`0.0`, `range`=`#F0_1#`, `vercond`=`#BSVER# #GT# 14`
  - The amount of distortion. **Not based on physically accurate refractive index** (0=none) (0-1)
- **Refraction Fire Period** (`int`)
  - Attributes: `default`=`0`, `range`=`#FPM_1000#`, `vercond`=`#BSVER# #GT# 14`
  - Rate of texture movement for refraction shader.
- **Parallax Max Passes** (`float`)
  - Attributes: `default`=`4.0`, `range`=`1.0:320.0`, `vercond`=`#BSVER# #GT# 24`
  - The number of passes the parallax shader can apply.
- **Parallax Scale** (`float`)
  - Attributes: `default`=`1.0`, `range`=`#F0_10#`, `vercond`=`#BSVER# #GT# 24`
  - The strength of the parallax.
- **Emissive Color** (`Color4`)
  - Attributes: `vercond`=`#BS_GT_FO3#`
  - Glow color and alpha

