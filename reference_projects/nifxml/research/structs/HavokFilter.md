# Struct `HavokFilter`

Bethesda Havok. Collision filter info representing Layer, Flags, Part Number, and Group all combined into one uint.

## Attributes
- **module**: `BSHavok`
- **name**: `HavokFilter`
- **size**: `4`
- **versions**: `#BETHESDA#`

## Fields
- **Layer** (`OblivionLayer`)
  - Attributes: `default`=`OL_STATIC`, `suffix`=`OB`, `until`=`20.0.0.5`, `vercond`=`#BSVER# #LT# 16`
  - The layer the collision belongs to.
- **Layer** (`Fallout3Layer`)
  - Attributes: `default`=`FOL_STATIC`, `suffix`=`FO`, `vercond`=`(#VER# == 20.2.0.7) #AND# #NI_BS_LTE_FO3#`
  - The layer the collision belongs to.
- **Layer** (`SkyrimLayer`)
  - Attributes: `default`=`SKYL_STATIC`, `suffix`=`SK`, `vercond`=`(#VER# == 20.2.0.7) #AND# #BS_GT_FO3#`
  - The layer the collision belongs to.
- **Flags** (`CollisionFilterFlags`)
  - Attributes: `default`=`0`
- **Group** (`ushort`)

