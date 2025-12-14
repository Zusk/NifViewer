# Niobject `NiZBufferProperty`

Allows applications to set the test and write modes of the renderer's Z-buffer and to set the comparison function used for the Z-buffer test.

## Attributes
- **inherit**: `NiProperty`
- **module**: `NiMain`
- **name**: `NiZBufferProperty`

## Fields
- **Flags** (`ZBufferFlags`)
  - Attributes: `default`=`3`
- **Function** (`TestFunction`)
  - Attributes: `default`=`TEST_LESS_EQUAL`, `since`=`4.1.0.12`, `until`=`20.0.0.5`
  - Z-Test function. In Flags from 20.1.0.3 on.

