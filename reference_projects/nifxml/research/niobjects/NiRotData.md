# Niobject `NiRotData`

Wrapper for rotation animation keys.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiAnimation`
- **name**: `NiRotData`

## Fields
- **Num Rotation Keys** (`uint`)
- **Rotation Type** (`KeyType`)
  - Attributes: `cond`=`Num Rotation Keys != 0`
- **Quaternion Keys** (`QuatKey`)
  - Attributes: `arg`=`Rotation Type`, `cond`=`Rotation Type != 4`, `length`=`Num Rotation Keys`, `template`=`Quaternion`
- **XYZ Rotations** (`KeyGroup`)
  - Attributes: `cond`=`Rotation Type == 4`, `length`=`3`, `template`=`float`

