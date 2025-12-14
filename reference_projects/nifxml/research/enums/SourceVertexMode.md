# Enum `SourceVertexMode`

Describes how to apply vertex colors for NiVertexColorProperty.

## Attributes
- **name**: `SourceVertexMode`
- **storage**: `uint`

## Values
- `option` `VERT_MODE_SRC_IGNORE` (`value`=`0`) – Emissive, ambient, and diffuse colors are all specified by the NiMaterialProperty.
- `option` `VERT_MODE_SRC_EMISSIVE` (`value`=`1`) – Emissive colors are specified by the source vertex colors. Ambient+Diffuse are specified by the NiMaterialProperty.
- `option` `VERT_MODE_SRC_AMB_DIF` (`value`=`2`) – Ambient+Diffuse colors are specified by the source vertex colors. Emissive is specified by the NiMaterialProperty. (Default)

