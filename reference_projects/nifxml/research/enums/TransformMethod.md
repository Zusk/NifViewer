# Enum `TransformMethod`

Describes the order of scaling and rotation matrices. Translate, Scale, Rotation, Center are from TexDesc.
Back = inverse of Center. FromMaya = inverse of the V axis with a positive translation along V of 1 unit.

## Attributes
- **name**: `TransformMethod`
- **prefix**: `TM`
- **storage**: `uint`

## Values
- `option` `Maya Deprecated` (`value`=`0`) – Center * Rotation * Back * Translate * Scale
- `option` `Max` (`value`=`1`) – Center * Scale * Rotation * Translate * Back
- `option` `Maya` (`value`=`2`) – Center * Rotation * Back * FromMaya * Translate * Scale

