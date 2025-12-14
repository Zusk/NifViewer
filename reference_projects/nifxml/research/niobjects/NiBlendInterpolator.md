# Niobject `NiBlendInterpolator`

Abstract base class for all NiInterpolators that blend the results of sub-interpolators together to compute a final weighted value.

## Attributes
- **abstract**: `true`
- **inherit**: `NiInterpolator`
- **module**: `NiAnimation`
- **name**: `NiBlendInterpolator`

## Fields
- **Flags** (`InterpBlendFlags`)
  - Attributes: `since`=`10.1.0.112`
- **Array Size** (`ushort`)
  - Attributes: `until`=`10.1.0.109`
- **Array Grow By** (`ushort`)
  - Attributes: `until`=`10.1.0.109`
- **Array Size** (`byte`)
  - Attributes: `since`=`10.1.0.110`
- **Weight Threshold** (`float`)
  - Attributes: `since`=`10.1.0.112`
- **Interp Count** (`byte`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `since`=`10.1.0.112`
- **Single Index** (`byte`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `default`=`#BYTE_MAX#`, `since`=`10.1.0.112`
- **High Priority** (`sbyte`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `default`=`#CHAR_MIN#`, `since`=`10.1.0.112`
- **Next High Priority** (`sbyte`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `default`=`#CHAR_MIN#`, `since`=`10.1.0.112`
- **Single Time** (`float`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `default`=`#FLT_MIN#`, `since`=`10.1.0.112`
- **High Weights Sum** (`float`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `default`=`#FLT_MIN#`, `since`=`10.1.0.112`
- **Next High Weights Sum** (`float`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `default`=`#FLT_MIN#`, `since`=`10.1.0.112`
- **High Ease Spinner** (`float`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `default`=`#FLT_MIN#`, `since`=`10.1.0.112`
- **Interp Array Items** (`InterpBlendItem`)
  - Attributes: `cond`=`!((Flags #BITAND# 1) != 0)`, `length`=`Array Size`, `since`=`10.1.0.112`
- **Interp Array Items** (`InterpBlendItem`)
  - Attributes: `length`=`Array Size`, `until`=`10.1.0.111`
- **Manager Controlled** (`bool`)
  - Attributes: `until`=`10.1.0.111`
- **Weight Threshold** (`float`)
  - Attributes: `until`=`10.1.0.111`
- **Only Use Highest Weight** (`bool`)
  - Attributes: `until`=`10.1.0.111`
- **Interp Count** (`ushort`)
  - Attributes: `until`=`10.1.0.109`
- **Single Index** (`ushort`)
  - Attributes: `default`=`#USHRT_MAX#`, `until`=`10.1.0.109`
- **Interp Count** (`byte`)
  - Attributes: `since`=`10.1.0.110`, `until`=`10.1.0.111`
- **Single Index** (`byte`)
  - Attributes: `default`=`#BYTE_MAX#`, `since`=`10.1.0.110`, `until`=`10.1.0.111`
- **Single Interpolator** (`Ref`)
  - Attributes: `since`=`10.1.0.108`, `template`=`NiInterpolator`, `until`=`10.1.0.111`
- **Single Time** (`float`)
  - Attributes: `default`=`#FLT_MIN#`, `since`=`10.1.0.108`, `until`=`10.1.0.111`
- **High Priority** (`int`)
  - Attributes: `default`=`#INT_MIN#`, `until`=`10.1.0.109`
- **Next High Priority** (`int`)
  - Attributes: `default`=`#INT_MIN#`, `until`=`10.1.0.109`
- **High Priority** (`sbyte`)
  - Attributes: `default`=`#CHAR_MIN#`, `since`=`10.1.0.110`, `until`=`10.1.0.111`
- **Next High Priority** (`sbyte`)
  - Attributes: `default`=`#CHAR_MIN#`, `since`=`10.1.0.110`, `until`=`10.1.0.111`

