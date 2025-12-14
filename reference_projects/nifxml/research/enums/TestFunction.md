# Enum `TestFunction`

Describes Z-buffer test modes for NiZBufferProperty.
"Less than" = closer to camera, "Greater than" = further from camera.

## Attributes
- **name**: `TestFunction`
- **storage**: `uint`

## Values
- `option` `TEST_ALWAYS` (`value`=`0`) – Always true. Buffer is ignored.
- `option` `TEST_LESS` (`value`=`1`) – VRef ‹ VBuf
- `option` `TEST_EQUAL` (`value`=`2`) – VRef = VBuf
- `option` `TEST_LESS_EQUAL` (`value`=`3`) – VRef ≤ VBuf
- `option` `TEST_GREATER` (`value`=`4`) – VRef › VBuf
- `option` `TEST_NOT_EQUAL` (`value`=`5`) – VRef ≠ VBuf
- `option` `TEST_GREATER_EQUAL` (`value`=`6`) – VRef ≥ VBuf
- `option` `TEST_NEVER` (`value`=`7`) – Always false. Ref value is ignored.

