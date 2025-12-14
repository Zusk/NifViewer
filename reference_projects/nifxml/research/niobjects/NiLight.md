# Niobject `NiLight`

Abstract base class that represents light sources in a scene graph.
For Bethesda Stream 130 (FO4), NiLight now directly inherits from NiAVObject.

## Attributes
- **abstract**: `true`
- **inherit**: `NiDynamicEffect`
- **module**: `NiMain`
- **name**: `NiLight`

## Fields
- **Dimmer** (`float`)
  - Attributes: `default`=`1.0`
  - Scales the overall brightness of all light components.
- **Ambient Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ZERO#`
- **Diffuse Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ZERO#`
- **Specular Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ZERO#`

