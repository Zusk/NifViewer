# Niobject `bhkNiCollisionObject`

Abstract base class to merge NiCollisionObject with Bethesda Havok.

## Attributes
- **abstract**: `true`
- **inherit**: `NiCollisionObject`
- **module**: `BSHavok`
- **name**: `bhkNiCollisionObject`

## Fields
- **Flags** (`bhkCOFlags`)
  - Attributes: `default`=`1`
  - OB-FO3: Add 0x28 (SET_LOCAL | USE_VEL) for ANIM_STATIC layer objects.
Post-FO3: Always add 0x80 (SYNC_ON_UPDATE).
- **Body** (`Ref`)
  - Attributes: `template`=`bhkWorldObject`

