# Niobject `NiLODNode`

Level of detail selector. Links to different levels of detail of the same model, used to switch a geometry at a specified distance.

## Attributes
- **inherit**: `NiSwitchNode`
- **module**: `NiMain`
- **name**: `NiLODNode`

## Fields
- **LOD Center** (`Vector3`)
  - Attributes: `since`=`4.0.0.2`, `until`=`10.0.1.0`
- **Num LOD Levels** (`uint`)
  - Attributes: `until`=`10.0.1.0`
- **LOD Levels** (`LODRange`)
  - Attributes: `length`=`Num LOD Levels`, `until`=`10.0.1.0`
- **LOD Level Data** (`Ref`)
  - Attributes: `since`=`10.1.0.0`, `template`=`NiLODData`

