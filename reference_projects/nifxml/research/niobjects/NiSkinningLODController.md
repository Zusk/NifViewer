# Niobject `NiSkinningLODController`

Defines the levels of detail for a given character and dictates the character's current LOD.

## Attributes
- **inherit**: `NiTimeController`
- **module**: `NiAnimation`
- **name**: `NiSkinningLODController`
- **since**: `V20_5_0_0`

## Fields
- **Current LOD** (`uint`)
- **Num Bones** (`uint`)
- **Bones** (`Ref`)
  - Attributes: `length`=`Num Bones`, `template`=`NiNode`
- **Num Skins** (`uint`)
- **Skins** (`Ref`)
  - Attributes: `length`=`Num Skins`, `template`=`NiMesh`
- **Num LOD Levels** (`uint`)
- **LODs** (`LODInfo`)
  - Attributes: `length`=`Num LOD Levels`

