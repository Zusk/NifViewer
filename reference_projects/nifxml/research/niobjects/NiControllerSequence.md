# Niobject `NiControllerSequence`

Root node in Gamebryo .kf files (version 10.0.1.0 and up).

## Attributes
- **inherit**: `NiSequence`
- **module**: `NiAnimation`
- **name**: `NiControllerSequence`

## Fields
- **Weight** (`float`)
  - Attributes: `default`=`1.0`, `since`=`10.1.0.106`
  - The weight of a sequence describes how it blends with other sequences at the same priority.
- **Text Keys** (`Ref`)
  - Attributes: `since`=`10.1.0.106`, `template`=`NiTextKeyExtraData`
- **Cycle Type** (`CycleType`)
  - Attributes: `default`=`CYCLE_CLAMP`, `since`=`10.1.0.106`
- **Frequency** (`float`)
  - Attributes: `default`=`1.0`, `since`=`10.1.0.106`
- **Phase** (`float`)
  - Attributes: `since`=`10.1.0.106`, `until`=`10.4.0.1`
- **Start Time** (`float`)
  - Attributes: `default`=`#FLT_MAX#`, `since`=`10.1.0.106`
- **Stop Time** (`float`)
  - Attributes: `default`=`#FLT_MIN#`, `since`=`10.1.0.106`
- **Play Backwards** (`bool`)
  - Attributes: `since`=`10.1.0.106`, `until`=`10.1.0.106`
- **Manager** (`Ptr`)
  - Attributes: `since`=`10.1.0.106`, `template`=`NiControllerManager`
  - The owner of this sequence.
- **Accum Root Name** (`string`)
  - Attributes: `since`=`10.1.0.106`
  - The name of the NiAVObject serving as the accumulation root. This is where all accumulated translations, scales, and rotations are applied.
- **Accum Flags** (`AccumFlags`)
  - Attributes: `default`=`0x40`, `since`=`20.3.0.8`
- **String Palette** (`Ref`)
  - Attributes: `since`=`10.1.0.113`, `template`=`NiStringPalette`, `until`=`20.1.0.0`
- **Anim Notes** (`Ref`)
  - Attributes: `since`=`20.2.0.7`, `template`=`BSAnimNotes`, `vercond`=`(#BSVER# #GTE# 24) #AND# (#BSVER# #LTE# 28)`
- **Num Anim Note Arrays** (`ushort`)
  - Attributes: `since`=`20.2.0.7`, `vercond`=`#BSVER# #GT# 28`
- **Anim Note Arrays** (`Ref`)
  - Attributes: `length`=`Num Anim Note Arrays`, `since`=`20.2.0.7`, `template`=`BSAnimNotes`, `vercond`=`#BSVER# #GT# 28`

