# Niobject `NiTimeController`

Abstract base class that provides the base timing and update functionality for all the Gamebryo animation controllers.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiMain`
- **name**: `NiTimeController`

## Fields
- **Next Controller** (`Ref`)
  - Attributes: `template`=`NiTimeController`
  - Index of the next controller.
- **Flags** (`TimeControllerFlags`)
  - Attributes: `default`=`76`
- **Frequency** (`float`)
  - Attributes: `default`=`1.0`
  - Frequency (is usually 1.0).
- **Phase** (`float`)
  - Phase (usually 0.0).
- **Start Time** (`float`)
  - Attributes: `default`=`#FLT_MAX#`
  - Controller start time.
- **Stop Time** (`float`)
  - Attributes: `default`=`#FLT_MIN#`
  - Controller stop time.
- **Target** (`Ptr`)
  - Attributes: `since`=`3.3.0.13`, `template`=`NiObjectNET`
  - Controller target (object index of the first controllable ancestor of this object).
- **Unknown Integer** (`uint`)
  - Attributes: `until`=`3.1`

