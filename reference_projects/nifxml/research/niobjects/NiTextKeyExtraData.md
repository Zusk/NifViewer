# Niobject `NiTextKeyExtraData`

Extra data that holds an array of NiTextKey objects for use in animation sequences.

## Attributes
- **inherit**: `NiExtraData`
- **module**: `NiMain`
- **name**: `NiTextKeyExtraData`

## Fields
- **Num Text Keys** (`uint`)
  - The number of text keys that follow.
- **Text Keys** (`Key`)
  - Attributes: `arg`=`1`, `length`=`Num Text Keys`, `template`=`string`
  - List of textual notes and at which time they take effect. Used for designating the start and stop of animations and the triggering of sounds.

