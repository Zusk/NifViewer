# Struct `FurniturePosition`

Bethesda Animation. Describes a furniture position?

## Attributes
- **module**: `BSMain`
- **name**: `FurniturePosition`
- **versions**: `#BETHESDA#`

## Fields
- **Offset** (`Vector3`)
  - Offset of furniture marker.
- **Orientation** (`ushort`)
  - Attributes: `vercond`=`#NI_BS_LTE_FO3#`
  - Furniture marker orientation.
- **Position Ref 1** (`byte`)
  - Attributes: `vercond`=`#NI_BS_LTE_FO3#`
  - Refers to a furnituremarkerxx.nif file. Always seems to be the same as Position Ref 2.
- **Position Ref 2** (`byte`)
  - Attributes: `vercond`=`#NI_BS_LTE_FO3#`
  - Refers to a furnituremarkerxx.nif file. Always seems to be the same as Position Ref 1.
- **Heading** (`float`)
  - Attributes: `vercond`=`#BS_GT_FO3#`
  - Rotation around z-axis in radians.
- **Animation Type** (`AnimationType`)
  - Attributes: `vercond`=`#BS_GT_FO3#`
- **Entry Properties** (`FurnitureEntryPoints`)
  - Attributes: `vercond`=`#BS_GT_FO3#`

