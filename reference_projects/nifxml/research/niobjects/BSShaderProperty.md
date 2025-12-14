# Niobject `BSShaderProperty`

Bethesda-specific property.

## Attributes
- **inherit**: `NiShadeProperty`
- **module**: `BSMain`
- **name**: `BSShaderProperty`
- **versions**: `#FO3_AND_LATER#`

## Fields
- **Shader Type** (`BSShaderType`)
  - Attributes: `default`=`SHADER_DEFAULT`, `vercond`=`#NI_BS_LTE_FO3#`
- **Shader Flags** (`BSShaderFlags`)
  - Attributes: `default`=`0x82000000`, `vercond`=`#NI_BS_LTE_FO3#`
- **Shader Flags 2** (`BSShaderFlags2`)
  - Attributes: `default`=`1`, `vercond`=`#NI_BS_LTE_FO3#`
- **Environment Map Scale** (`float`)
  - Attributes: `default`=`1.0`, `range`=`#F0_10#`, `vercond`=`#NI_BS_LTE_FO3#`
  - Scales the intensity of the environment/cube map.

