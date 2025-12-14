# Niobject `NiBoneLODController`

DEPRECATED (20.5), Replaced by NiSkinningLODController.
Level of detail controller for bones.  Priority is arranged from low to high.

## Attributes
- **inherit**: `NiTimeController`
- **module**: `NiAnimation`
- **name**: `NiBoneLODController`

## Fields
- **LOD** (`uint`)
- **Num LODs** (`uint`)
  - Number of LODs.
- **Num Node Groups** (`uint`)
  - Number of node arrays.
- **Node Groups** (`NodeSet`)
  - Attributes: `length`=`Num LODs`
  - A list of node sets (each set a sequence of bones).
- **Num Shape Groups** (`uint`)
  - Attributes: `since`=`4.2.2.0`, `vercond`=`#NISTREAM#`
  - Number of shape groups.
- **Shape Groups 1** (`SkinInfoSet`)
  - Attributes: `length`=`Num Shape Groups`, `since`=`4.2.2.0`, `vercond`=`#NISTREAM#`
  - List of shape groups.
- **Num Shape Groups 2** (`uint`)
  - Attributes: `since`=`4.2.2.0`, `vercond`=`#NISTREAM#`
  - The size of the second list of shape groups.
- **Shape Groups 2** (`Ref`)
  - Attributes: `length`=`Num Shape Groups 2`, `since`=`4.2.2.0`, `template`=`NiTriBasedGeom`, `vercond`=`#NISTREAM#`
  - Group of NiTriShape indices.

