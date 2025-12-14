# Niobject `bhkRigidBody`

This is the default body type for all "normal" usable and static world objects. The "T" suffix
marks this body as active for translation and rotation, a normal bhkRigidBody ignores those
properties. Because the properties are equal, a bhkRigidBody may be renamed into a bhkRigidBodyT and vice-versa.

## Attributes
- **inherit**: `bhkEntity`
- **module**: `BSHavok`
- **name**: `bhkRigidBody`
- **versions**: `#BETHESDA#`

## Fields
- **Rigid Body Info** (`bhkRigidBodyCInfo550_660`)
  - Attributes: `suffix`=`550_660`, `vercond`=`#NI_BS_LTE_FO3#`
- **Rigid Body Info** (`bhkRigidBodyCInfo2010`)
  - Attributes: `suffix`=`2010`, `vercond`=`#BS_GTE_SKY# #AND# (!#BS_FO4#)`
- **Rigid Body Info** (`bhkRigidBodyCInfo2014`)
  - Attributes: `suffix`=`2014`, `vercond`=`#BS_FO4#`
- **Num Constraints** (`uint`)
- **Constraints** (`Ref`)
  - Attributes: `length`=`Num Constraints`, `template`=`bhkSerializable`
- **Body Flags** (`uint`)
  - Attributes: `vercond`=`#BSVER# #LT# 76`
  - 1 = respond to wind
- **Body Flags** (`ushort`)
  - Attributes: `vercond`=`#BSVER# #GTE# 76`
  - 1 = respond to wind

