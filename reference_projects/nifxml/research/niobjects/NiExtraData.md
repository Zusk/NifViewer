# Niobject `NiExtraData`

A generic extra data object.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiExtraData`

## Fields
- **Name** (`string`)
  - Attributes: `excludeT`=`BSExtraData`, `since`=`10.0.1.0`
  - Name of this object.
- **Next Extra Data** (`Ref`)
  - Attributes: `template`=`NiExtraData`, `until`=`4.2.2.0`
  - Block number of the next extra data object.
- **Extra Data** (`ByteArray`)
  - Attributes: `until`=`3.3.0.13`
  - The extra data was sometimes stored as binary directly on NiExtraData.
- **Num Bytes** (`uint`)
  - Attributes: `since`=`4.0.0.0`, `until`=`4.2.2.0`
  - Ignore binary data after 4.x as the child block will cover it.

