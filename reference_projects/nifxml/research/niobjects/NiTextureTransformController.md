# Niobject `NiTextureTransformController`

Used to animate a single member of an NiTextureTransform.
NiInterpController::GetCtlrID() string formats:
    ['%1-%2-TT_TRANSLATE_U', '%1-%2-TT_TRANSLATE_V', '%1-%2-TT_ROTATE', '%1-%2-TT_SCALE_U', '%1-%2-TT_SCALE_V']
(Depending on "Operation" enumeration, %1 = Value of "Shader Map", %2 = Value of "Texture Slot")

## Attributes
- **inherit**: `NiFloatInterpController`
- **module**: `NiAnimation`
- **name**: `NiTextureTransformController`

## Fields
- **Shader Map** (`bool`)
  - Is the target map a shader map?
- **Texture Slot** (`TexType`)
  - The target texture slot.
- **Operation** (`TransformMember`)
  - Controls which aspect of the texture transform to modify.
- **Data** (`Ref`)
  - Attributes: `template`=`NiFloatData`, `until`=`10.1.0.103`

