# Niobject `bhkMeshShape`

Bethesda extension of hkpMeshShape, but using NiTriStripsData instead of Havok storage.
Appears in one old Oblivion NIF, but only in certain distributions. NIF version 10.0.1.0 only.

## Attributes
- **inherit**: `bhkShape`
- **module**: `BSHavok`
- **name**: `bhkMeshShape`
- **versions**: `V10_0_1_0`

## Fields
- **Unknown 01** (`uint`)
  - Attributes: `length`=`2`
- **Radius** (`float`)
- **Unknown 02** (`uint`)
  - Attributes: `length`=`2`
- **Scale** (`Vector4`)
- **Num Shape Properties** (`uint`)
- **Shape Properties** (`bhkWorldObjCInfoProperty`)
  - Attributes: `length`=`Num Shape Properties`
- **Unknown 03** (`uint`)
  - Attributes: `length`=`3`
- **Num Strips Data** (`uint`)
  - Attributes: `until`=`10.0.1.0`
- **Strips Data** (`Ref`)
  - Attributes: `length`=`Num Strips Data`, `template`=`NiTriStripsData`, `until`=`10.0.1.0`

