# Niobject `NiEvaluator`

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiAnimation`
- **name**: `NiEvaluator`
- **since**: `V20_5_0_0`

## Fields
- **Node Name** (`NiFixedString`)
  - The name of the animated NiAVObject.
- **Property Type** (`NiFixedString`)
  - The RTTI type of the NiProperty the controller is attached to, if applicable.
- **Controller Type** (`NiFixedString`)
  - The RTTI type of the NiTimeController.
- **Controller ID** (`NiFixedString`)
  - An ID that can uniquely identify the controller among others of the same type on the same NiObjectNET.
- **Interpolator ID** (`NiFixedString`)
  - An ID that can uniquely identify the interpolator among others of the same type on the same NiObjectNET.
- **Channel Types** (`byte`)
  - Attributes: `length`=`4`
  - Channel Indices are BASE/POS = 0, ROT = 1, SCALE = 2, FLAG = 3
Channel Types are:
 INVALID = 0, COLOR, BOOL, FLOAT, POINT3, ROT = 5
Any channel may be | 0x40 which means POSED
The FLAG (3) channel flags affects the whole evaluator:
 REFERENCED = 0x1, TRANSFORM = 0x2, ALWAYSUPDATE = 0x4, SHUTDOWN = 0x8

