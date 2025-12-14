# Enum `StencilTestFunc`

Describes stencil buffer test modes for NiStencilProperty.

## Attributes
- **name**: `StencilTestFunc`
- **prefix**: `STENCIL`
- **storage**: `uint`

## Values
- `option` `TEST_NEVER` (`value`=`0`) – Always false. Ref value is ignored.
- `option` `TEST_LESS` (`value`=`1`) – VRef ‹ VBuf
- `option` `TEST_EQUAL` (`value`=`2`) – VRef = VBuf
- `option` `TEST_LESS_EQUAL` (`value`=`3`) – VRef ≤ VBuf
- `option` `TEST_GREATER` (`value`=`4`) – VRef › VBuf
- `option` `TEST_NOT_EQUAL` (`value`=`5`) – VRef ≠ VBuf
- `option` `TEST_GREATER_EQUAL` (`value`=`6`) – VRef ≥ VBuf
- `option` `TEST_ALWAYS` (`value`=`7`) – Always true. Buffer is ignored.

