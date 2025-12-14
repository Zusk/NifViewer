# Niobject `bhkPoseArray`

Found in Fallout 3 .psa files, extra ragdoll info for NPCs/creatures. (usually idleanims\deathposes.psa)
Defines different kill poses. The game selects the pose randomly and applies it to a skeleton immediately upon ragdolling.
Poses can be previewed in GECK Object Window-Actor Data-Ragdoll and selecting Pose Matching tab.

## Attributes
- **inherit**: `NiObject`
- **module**: `BSHavok`
- **name**: `bhkPoseArray`
- **versions**: `#FO3_AND_LATER#`

## Fields
- **Num Bones** (`uint`)
- **Bones** (`NiFixedString`)
  - Attributes: `length`=`Num Bones`
- **Num Poses** (`uint`)
- **Poses** (`BonePose`)
  - Attributes: `length`=`Num Poses`

