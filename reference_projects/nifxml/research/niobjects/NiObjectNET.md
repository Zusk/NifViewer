# Niobject `NiObjectNET`

Abstract base class for NiObjects that support names, extra data, and time controllers.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiObjectNET`

## Fields
- **Shader Type** (`BSLightingShaderType`)
  - Attributes: `onlyT`=`BSLightingShaderProperty`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_SKY# #AND# #NI_BS_LTE_FO4#`
  - Configures the main shader path
- **Name** (`string`)
  - Name of this controllable object, used to refer to the object in .kf files.
- **Legacy Extra Data** (`LegacyExtraData`)
  - Attributes: `until`=`2.3`
- **Extra Data** (`Ref`)
  - Attributes: `since`=`3.0`, `template`=`NiExtraData`, `until`=`4.2.2.0`
  - Extra data object index. (The first in a chain)
- **Num Extra Data List** (`uint`)
  - Attributes: `since`=`10.0.1.0`
  - The number of Extra Data objects referenced through the list.
- **Extra Data List** (`Ref`)
  - Attributes: `length`=`Num Extra Data List`, `since`=`10.0.1.0`, `template`=`NiExtraData`
  - List of extra data indices.
- **Controller** (`Ref`)
  - Attributes: `since`=`3.0`, `template`=`NiTimeController`
  - Controller object index. (The first in a chain)

