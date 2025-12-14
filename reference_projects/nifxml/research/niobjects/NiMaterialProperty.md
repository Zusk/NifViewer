# Niobject `NiMaterialProperty`

Describes the surface properties of an object e.g. translucency, ambient color, diffuse color, emissive color, and specular color.

## Attributes
- **inherit**: `NiProperty`
- **module**: `NiMain`
- **name**: `NiMaterialProperty`

## Fields
- **Flags** (`ushort`)
  - Attributes: `since`=`3.0`, `until`=`10.0.1.2`
  - Property flags.
- **Ambient Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ONE#`, `vercond`=`#BSVER# #LT# 26`
  - How much the material reflects ambient light.
- **Diffuse Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ONE#`, `vercond`=`#BSVER# #LT# 26`
  - How much the material reflects diffuse light.
- **Specular Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ONE#`
  - How much light the material reflects in a specular manner.
- **Emissive Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ZERO#`
  - How much light the material emits.
- **Glossiness** (`float`)
  - Attributes: `default`=`10.0`
  - The material glossiness.
- **Alpha** (`float`)
  - Attributes: `default`=`1.0`
  - The material transparency (1=non-transparant). Refer to a NiAlphaProperty object in this material's parent NiTriShape object, when alpha is not 1.
- **Emissive Mult** (`float`)
  - Attributes: `default`=`1.0`, `vercond`=`#BSVER# #GT# 21`

