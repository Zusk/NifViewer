# Niobject `NiKeyframeData`

DEPRECATED (10.2), RENAMED (10.2) to NiTransformData.
Wrapper for transformation animation keys.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiAnimation`
- **name**: `NiKeyframeData`

## Fields
- **Num Rotation Keys** (`uint`)
  - The number of quaternion rotation keys. If the rotation type is XYZ (type 4) then this *must* be set to 1, and in this case the actual number of keys is stored in the XYZ Rotations field.
- **Rotation Type** (`KeyType`)
  - Attributes: `cond`=`Num Rotation Keys != 0`
  - The type of interpolation to use for rotation.  Can also be 4 to indicate that separate X, Y, and Z values are used for the rotation instead of Quaternions.
- **Quaternion Keys** (`QuatKey`)
  - Attributes: `arg`=`Rotation Type`, `cond`=`Rotation Type != 4`, `length`=`Num Rotation Keys`, `template`=`Quaternion`
  - The rotation keys if Quaternion rotation is used.
- **Order** (`float`)
  - Attributes: `cond`=`Rotation Type == 4`, `until`=`10.1.0.0`
- **XYZ Rotations** (`KeyGroup`)
  - Attributes: `cond`=`Rotation Type == 4`, `length`=`3`, `template`=`float`
  - Individual arrays of keys for rotating X, Y, and Z individually.
- **Translations** (`KeyGroup`)
  - Attributes: `template`=`Vector3`
  - Translation keys.
- **Scales** (`KeyGroup`)
  - Attributes: `template`=`float`
  - Scale keys.

