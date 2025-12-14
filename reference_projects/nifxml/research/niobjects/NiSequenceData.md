# Niobject `NiSequenceData`

Root node in Gamebryo .kf files (20.5.0.1 and up).
For 20.5.0.0, "NiSequenceData" is an alias for "NiControllerSequence" and this is not handled in nifxml.
This was not found in any 20.5.0.0 KFs available and they instead use NiControllerSequence directly.
After 20.5.0.1, Controlled Blocks are no longer used and instead the sequences uses NiEvaluator.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiAnimation`
- **name**: `NiSequenceData`
- **since**: `V20_5_0_0`

## Fields
- **Name** (`NiFixedString`)
- **Num Controlled Blocks** (`uint`)
  - Attributes: `until`=`20.5.0.1`
- **Array Grow By** (`uint`)
  - Attributes: `until`=`20.5.0.1`
- **Controlled Blocks** (`ControlledBlock`)
  - Attributes: `length`=`Num Controlled Blocks`, `until`=`20.5.0.1`
- **Num Evaluators** (`uint`)
  - Attributes: `since`=`20.5.0.2`
- **Evaluators** (`Ref`)
  - Attributes: `length`=`Num Evaluators`, `since`=`20.5.0.2`, `template`=`NiEvaluator`
- **Text Keys** (`Ref`)
  - Attributes: `template`=`NiTextKeyExtraData`
- **Duration** (`float`)
- **Cycle Type** (`CycleType`)
- **Frequency** (`float`)
  - Attributes: `default`=`1.0`
- **Accum Root Name** (`NiFixedString`)
  - The name of the NiAVObject serving as the accumulation root. This is where all accumulated translations, scales, and rotations are applied.
- **Accum Flags** (`AccumFlags`)
  - Attributes: `default`=`0x40`

