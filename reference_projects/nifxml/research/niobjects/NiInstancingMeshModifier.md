# Niobject `NiInstancingMeshModifier`

Mesh modifier that provides per-frame instancing capabilities in Gamebryo.

## Attributes
- **inherit**: `NiMeshModifier`
- **module**: `NiMesh`
- **name**: `NiInstancingMeshModifier`
- **since**: `V20_5_0_0`

## Fields
- **Has Instance Nodes** (`bool`)
- **Per Instance Culling** (`bool`)
- **Has Static Bounds** (`bool`)
- **Affected Mesh** (`Ref`)
  - Attributes: `template`=`NiMesh`
- **Bounding Sphere** (`NiBound`)
  - Attributes: `cond`=`Has Static Bounds`
- **Num Instance Nodes** (`uint`)
  - Attributes: `cond`=`Has Instance Nodes`
- **Instance Nodes** (`Ref`)
  - Attributes: `cond`=`Has Instance Nodes`, `length`=`Num Instance Nodes`, `template`=`NiMeshHWInstance`

