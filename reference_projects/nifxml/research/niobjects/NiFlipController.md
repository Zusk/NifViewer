# Niobject `NiFlipController`

Changes the image a Map (TexDesc) will use. Uses a float interpolator to animate the texture index.
Often used for performing flipbook animation.

## Attributes
- **inherit**: `NiFloatInterpController`
- **module**: `NiAnimation`
- **name**: `NiFlipController`

## Fields
- **Texture Slot** (`TexType`)
  - Target texture slot (0=base, 4=glow).
- **Accum Time** (`float`)
  - Attributes: `since`=`3.3.0.13`, `until`=`10.1.0.103`
- **Delta** (`float`)
  - Attributes: `until`=`10.1.0.103`
  - Time between two flips.
delta = (start_time - stop_time) / sources.num_indices
- **Num Sources** (`uint`)
- **Sources** (`Ref`)
  - Attributes: `length`=`Num Sources`, `since`=`3.3.0.13`, `template`=`NiSourceTexture`
  - The texture sources.
- **Images** (`Ref`)
  - Attributes: `length`=`Num Sources`, `template`=`NiImage`, `until`=`3.1`
  - The image sources

