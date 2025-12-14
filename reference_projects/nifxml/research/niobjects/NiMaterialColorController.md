# Niobject `NiMaterialColorController`

Time controller for material color. Flags are used for color selection in versions below 10.1.0.0.
Bits 4-5: Target Color (00 = Ambient, 01 = Diffuse, 10 = Specular, 11 = Emissive)
NiInterpController::GetCtlrID() string formats:
    ['AMB', 'DIFF', 'SPEC', 'SELF_ILLUM'] (Depending on "Target Color")

## Attributes
- **inherit**: `NiPoint3InterpController`
- **module**: `NiAnimation`
- **name**: `NiMaterialColorController`

## Fields
- **Target Color** (`MaterialColor`)
  - Attributes: `since`=`10.1.0.0`
  - Selects which color to control.
- **Data** (`Ref`)
  - Attributes: `template`=`NiPosData`, `until`=`10.1.0.103`

