# Enum `StencilAction`

Describes the actions which can occur as a result of tests for NiStencilProperty.

## Attributes
- **name**: `StencilAction`
- **storage**: `uint`

## Values
- `option` `ACTION_KEEP` (`value`=`0`) – Keep the current value in the stencil buffer.
- `option` `ACTION_ZERO` (`value`=`1`) – Write zero to the stencil buffer.
- `option` `ACTION_REPLACE` (`value`=`2`) – Write the reference value to the stencil buffer.
- `option` `ACTION_INCREMENT` (`value`=`3`) – Increment the value in the stencil buffer.
- `option` `ACTION_DECREMENT` (`value`=`4`) – Decrement the value in the stencil buffer.
- `option` `ACTION_INVERT` (`value`=`5`) – Bitwise invert the value in the stencil buffer.

