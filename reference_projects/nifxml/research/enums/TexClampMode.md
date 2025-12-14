# Enum `TexClampMode`

Describes the availiable texture clamp modes, i.e. the behavior of UV mapping outside the [0,1] range.

## Attributes
- **name**: `TexClampMode`
- **storage**: `uint`

## Values
- `option` `CLAMP_S_CLAMP_T` (`value`=`0`) – Clamp in both directions.
- `option` `CLAMP_S_WRAP_T` (`value`=`1`) – Clamp in the S(U) direction but wrap in the T(V) direction.
- `option` `WRAP_S_CLAMP_T` (`value`=`2`) – Wrap in the S(U) direction but clamp in the T(V) direction.
- `option` `WRAP_S_WRAP_T` (`value`=`3`) – Wrap in both directions.

