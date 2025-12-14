# Niobject `bhkAabbPhantom`

Bethesda extension of hkpAabbPhantom. A non-physical object made up of only an AABB.
    - Very fast as they use only broadphase collision detection.
    - Used for triggers/regions where a shape is not necessary.

## Attributes
- **inherit**: `bhkPhantom`
- **module**: `BSHavok`
- **name**: `bhkAabbPhantom`
- **versions**: `#BETHESDA#`

## Fields
- **Unused 01** (`byte`)
  - Attributes: `binary`=`true`, `length`=`8`
- **AABB** (`hkAabb`)

