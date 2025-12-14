# Enum `KeyType`

The type of animation interpolation (blending) that will be used on the associated key frames.

## Attributes
- **name**: `KeyType`
- **storage**: `uint`

## Values
- `option` `LINEAR_KEY` (`value`=`1`) – Use linear interpolation.
- `option` `QUADRATIC_KEY` (`value`=`2`) – Use quadratic interpolation.  Forward and back tangents will be stored.
- `option` `TBC_KEY` (`value`=`3`) – Use Tension Bias Continuity interpolation.  Tension, bias, and continuity will be stored.
- `option` `XYZ_ROTATION_KEY` (`value`=`4`) – For use only with rotation data.  Separate X, Y, and Z keys will be stored instead of using quaternions.
- `option` `CONST_KEY` (`value`=`5`) – Step function. Used for visibility keys in NiBoolData.

