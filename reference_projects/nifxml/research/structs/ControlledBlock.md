# Struct `ControlledBlock`

In a .kf file, this links to a controllable object, via its name (or for version 10.2.0.0 and up, a link and offset to a NiStringPalette that contains the name), and a sequence of interpolators that apply to this controllable object, via links.
For Controller ID, NiInterpController::GetCtlrID() virtual function returns a string formatted specifically for the derived type.
For Interpolator ID, NiInterpController::GetInterpolatorID() virtual function returns a string formatted specifically for the derived type.
The string formats are documented on the relevant niobject blocks.

## Attributes
- **module**: `NiAnimation`
- **name**: `ControlledBlock`

## Fields
- **Target Name** (`SizedString`)
  - Attributes: `until`=`10.1.0.103`
  - Name of a controllable object in another NIF file.
- **Interpolator** (`Ref`)
  - Attributes: `since`=`10.1.0.106`, `template`=`NiInterpolator`
- **Controller** (`Ref`)
  - Attributes: `template`=`NiTimeController`, `until`=`20.5.0.0`
- **Blend Interpolator** (`Ref`)
  - Attributes: `since`=`10.1.0.104`, `template`=`NiBlendInterpolator`, `until`=`10.1.0.110`
- **Blend Index** (`ushort`)
  - Attributes: `since`=`10.1.0.104`, `until`=`10.1.0.110`
- **Priority** (`byte`)
  - Attributes: `since`=`10.1.0.106`, `vercond`=`#BSSTREAM#`
  - Idle animations tend to have low values for this, and high values tend to correspond with the important parts of the animations.
- **Node Name** (`string`)
  - Attributes: `since`=`10.1.0.104`, `until`=`10.1.0.113`
  - The name of the animated NiAVObject.
- **Property Type** (`string`)
  - Attributes: `since`=`10.1.0.104`, `until`=`10.1.0.113`
  - The RTTI type of the NiProperty the controller is attached to, if applicable.
- **Controller Type** (`string`)
  - Attributes: `since`=`10.1.0.104`, `until`=`10.1.0.113`
  - The RTTI type of the NiTimeController.
- **Controller ID** (`string`)
  - Attributes: `since`=`10.1.0.104`, `until`=`10.1.0.113`
  - An ID that can uniquely identify the controller among others of the same type on the same NiObjectNET.
- **Interpolator ID** (`string`)
  - Attributes: `since`=`10.1.0.104`, `until`=`10.1.0.113`
  - An ID that can uniquely identify the interpolator among others of the same type on the same NiObjectNET.
- **String Palette** (`Ref`)
  - Attributes: `since`=`10.2.0.0`, `template`=`NiStringPalette`, `until`=`20.1.0.0`
  - Refers to the NiStringPalette which contains the name of the controlled NIF object.
- **Node Name Offset** (`StringOffset`)
  - Attributes: `since`=`10.2.0.0`, `until`=`20.1.0.0`
  - Offset in NiStringPalette to the name of the animated NiAVObject.
- **Property Type Offset** (`StringOffset`)
  - Attributes: `since`=`10.2.0.0`, `until`=`20.1.0.0`
  - Offset in NiStringPalette to the RTTI type of the NiProperty the controller is attached to, if applicable.
- **Controller Type Offset** (`StringOffset`)
  - Attributes: `since`=`10.2.0.0`, `until`=`20.1.0.0`
  - Offset in NiStringPalette to the RTTI type of the NiTimeController.
- **Controller ID Offset** (`StringOffset`)
  - Attributes: `since`=`10.2.0.0`, `until`=`20.1.0.0`
  - Offset in NiStringPalette to an ID that can uniquely identify the controller among others of the same type on the same NiObjectNET.
- **Interpolator ID Offset** (`StringOffset`)
  - Attributes: `since`=`10.2.0.0`, `until`=`20.1.0.0`
  - Offset in NiStringPalette to an ID that can uniquely identify the interpolator among others of the same type on the same NiObjectNET.
- **Node Name** (`string`)
  - Attributes: `since`=`20.1.0.1`
  - The name of the animated NiAVObject.
- **Property Type** (`string`)
  - Attributes: `since`=`20.1.0.1`
  - The RTTI type of the NiProperty the controller is attached to, if applicable.
- **Controller Type** (`string`)
  - Attributes: `since`=`20.1.0.1`
  - The RTTI type of the NiTimeController.
- **Controller ID** (`string`)
  - Attributes: `since`=`20.1.0.1`
  - An ID that can uniquely identify the controller among others of the same type on the same NiObjectNET.
- **Interpolator ID** (`string`)
  - Attributes: `since`=`20.1.0.1`
  - An ID that can uniquely identify the interpolator among others of the same type on the same NiObjectNET.

