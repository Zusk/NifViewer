# Niobject `NiRawImageData`

LEGACY (pre-10.1)
Raw image data.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiLegacy`
- **name**: `NiRawImageData`
- **until**: `V10_0_1_0`

## Fields
- **Width** (`uint`)
  - Image width
- **Height** (`uint`)
  - Image height
- **Image Type** (`ImageType`)
  - The format of the raw image data.
- **RGB Image Data** (`ByteColor3`)
  - Attributes: `cond`=`Image Type == 1`, `length`=`Width`, `width`=`Height`
  - Image pixel data.
- **RGBA Image Data** (`ByteColor4`)
  - Attributes: `cond`=`Image Type == 2`, `length`=`Width`, `width`=`Height`
  - Image pixel data.

