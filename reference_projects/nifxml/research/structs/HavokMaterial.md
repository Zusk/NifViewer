# Struct `HavokMaterial`

Bethesda Havok. Material wrapper for varying material enums by game.

## Attributes
- **module**: `BSHavok`
- **name**: `HavokMaterial`
- **versions**: `#BETHESDA#`

## Fields
- **Unknown Int** (`uint`)
  - Attributes: `until`=`10.0.1.2`
- **Material** (`OblivionHavokMaterial`)
  - Attributes: `suffix`=`OB`, `until`=`20.0.0.5`, `vercond`=`#BSVER# #LT# 16`
  - The material of the shape.
- **Material** (`Fallout3HavokMaterial`)
  - Attributes: `suffix`=`FO`, `vercond`=`(#VER# == 20.2.0.7) #AND# #NI_BS_LTE_FO3#`
  - The material of the shape.
- **Material** (`SkyrimHavokMaterial`)
  - Attributes: `suffix`=`SK`, `vercond`=`(#VER# == 20.2.0.7) #AND# #BS_GT_FO3#`
  - The material of the shape.

