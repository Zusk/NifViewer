# Struct `Morph`

Geometry morphing data component.

## Attributes
- **module**: `NiAnimation`
- **name**: `Morph`

## Fields
- **Frame Name** (`string`)
  - Attributes: `since`=`10.1.0.106`
  - Name of the frame.
- **Num Keys** (`uint`)
  - Attributes: `until`=`10.1.0.0`
  - The number of morph keys that follow.
- **Interpolation** (`KeyType`)
  - Attributes: `until`=`10.1.0.0`
  - Unlike most objects, the presense of this value is not conditional on there being keys.
- **Keys** (`Key`)
  - Attributes: `arg`=`Interpolation`, `length`=`Num Keys`, `template`=`float`, `until`=`10.1.0.0`
  - The morph key frames.
- **Legacy Weight** (`float`)
  - Attributes: `since`=`10.1.0.104`, `until`=`20.1.0.2`, `vercond`=`#BSVER# #LT# 10`
- **Vectors** (`Vector3`)
  - Attributes: `length`=`#ARG#`
  - Morph vectors.

