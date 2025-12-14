# Niobject `NiDynamicEffect`

Abstract base class for dynamic effects such as NiLights or projected texture effects.

## Attributes
- **abstract**: `true`
- **inherit**: `NiAVObject`
- **module**: `NiMain`
- **name**: `NiDynamicEffect`

## Fields
- **Switch State** (`bool`)
  - Attributes: `default`=`true`, `since`=`10.1.0.106`, `vercond`=`#NI_BS_LT_FO4#`
  - If true, then the dynamic effect is applied to affected nodes during rendering.
- **Num Affected Nodes** (`uint`)
  - Attributes: `until`=`4.0.0.2`
- **Affected Nodes** (`Ptr`)
  - Attributes: `length`=`Num Affected Nodes`, `template`=`NiNode`, `until`=`3.3.0.13`
  - If a node appears in this list, then its entire subtree will be affected by the effect.
- **Affected Node Pointers** (`uint`)
  - Attributes: `length`=`Num Affected Nodes`, `since`=`4.0.0.0`, `until`=`4.0.0.2`
  - As of 4.0 the pointer hash is no longer stored alongside each NiObject on disk, yet this node list still refers to the pointer hashes. Cannot leave the type as Ptr because the link will be invalid.
- **Num Affected Nodes** (`uint`)
  - Attributes: `since`=`10.1.0.0`, `vercond`=`#NI_BS_LT_FO4#`
- **Affected Nodes** (`Ptr`)
  - Attributes: `length`=`Num Affected Nodes`, `since`=`10.1.0.0`, `template`=`NiNode`, `vercond`=`#NI_BS_LT_FO4#`
  - If a node appears in this list, then its entire subtree will be affected by the effect.

