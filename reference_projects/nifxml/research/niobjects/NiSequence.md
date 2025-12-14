# Niobject `NiSequence`

Root node in NetImmerse .kf files (until version 10.0).

## Attributes
- **inherit**: `NiObject`
- **module**: `NiAnimation`
- **name**: `NiSequence`

## Fields
- **Name** (`string`)
  - The sequence name by which the animation system finds and manages this sequence.
- **Accum Root Name** (`string`)
  - Attributes: `until`=`10.1.0.103`
  - The name of the NiAVObject serving as the accumulation root. This is where all accumulated translations, scales, and rotations are applied.
- **Text Keys** (`Ref`)
  - Attributes: `template`=`NiTextKeyExtraData`, `until`=`10.1.0.103`
- **Num DIV2 Ints** (`uint`)
  - Attributes: `since`=`20.3.0.9`, `until`=`20.3.0.9`, `vercond`=`#DIVINITY2#`
- **DIV2 Ints** (`int`)
  - Attributes: `length`=`Num DIV2 Ints`, `since`=`20.3.0.9`, `until`=`20.3.0.9`, `vercond`=`#DIVINITY2#`
- **DIV2 Ref** (`Ref`)
  - Attributes: `since`=`20.3.0.9`, `template`=`NiObject`, `until`=`20.3.0.9`, `vercond`=`#DIVINITY2#`
- **Num Controlled Blocks** (`uint`)
- **Array Grow By** (`uint`)
  - Attributes: `default`=`1`, `since`=`10.1.0.106`
- **Controlled Blocks** (`ControlledBlock`)
  - Attributes: `length`=`Num Controlled Blocks`

