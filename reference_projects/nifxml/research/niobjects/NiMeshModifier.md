# Niobject `NiMeshModifier`

Base class for mesh modifiers.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiMesh`
- **name**: `NiMeshModifier`
- **since**: `V20_5_0_0`

## Fields
- **Num Submit Points** (`uint`)
- **Submit Points** (`SyncPoint`)
  - Attributes: `length`=`Num Submit Points`
  - The sync points supported by this mesh modifier for SubmitTasks.
- **Num Complete Points** (`uint`)
- **Complete Points** (`SyncPoint`)
  - Attributes: `length`=`Num Complete Points`
  - The sync points supported by this mesh modifier for CompleteTasks.

