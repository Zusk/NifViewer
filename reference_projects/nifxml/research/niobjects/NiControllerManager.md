# Niobject `NiControllerManager`

Controls animation sequences on a specific branch of the scene graph.

## Attributes
- **inherit**: `NiTimeController`
- **module**: `NiAnimation`
- **name**: `NiControllerManager`

## Fields
- **Cumulative** (`bool`)
  - Whether transformation accumulation is enabled. If accumulation is not enabled, the manager will treat all sequence data on the accumulation root as absolute data instead of relative delta values.
- **Num Controller Sequences** (`uint`)
- **Controller Sequences** (`Ref`)
  - Attributes: `length`=`Num Controller Sequences`, `template`=`NiControllerSequence`
- **Object Palette** (`Ref`)
  - Attributes: `template`=`NiDefaultAVObjectPalette`

