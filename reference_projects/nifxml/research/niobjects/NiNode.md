# Niobject `NiNode`

Generic node object for grouping.

## Attributes
- **inherit**: `NiAVObject`
- **module**: `NiMain`
- **name**: `NiNode`

## Fields
- **Num Children** (`uint`)
  - The number of child objects.
- **Children** (`Ref`)
  - Attributes: `length`=`Num Children`, `template`=`NiAVObject`
  - List of child node object indices.
- **Num Effects** (`uint`)
  - Attributes: `vercond`=`#NI_BS_LT_FO4#`
  - The number of references to effect objects that follow.
- **Effects** (`Ref`)
  - Attributes: `length`=`Num Effects`, `template`=`NiDynamicEffect`, `vercond`=`#NI_BS_LT_FO4#`
  - List of node effects. ADynamicEffect?

