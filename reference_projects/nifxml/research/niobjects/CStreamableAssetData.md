# Niobject `CStreamableAssetData`

Divinity 2 specific block

## Attributes
- **inherit**: `NiObject`
- **module**: `NiCustom`
- **name**: `CStreamableAssetData`
- **versions**: `V20_3_0_9 V20_3_0_9_DIV2`

## Fields
- **Root** (`Ref`)
  - Attributes: `template`=`NiNode`
- **Has Data** (`bool`)
- **Data** (`ByteArray`)
  - Attributes: `cond`=`Has Data`
- **Num Refs** (`uint`)
- **Refs** (`Ref`)
  - Attributes: `length`=`Num Refs`, `template`=`NiObject`

