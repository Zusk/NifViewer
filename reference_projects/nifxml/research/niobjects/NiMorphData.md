# Niobject `NiMorphData`

DEPRECATED (20.5), replaced by NiMorphMeshModifier.
Geometry morphing data.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiAnimation`
- **name**: `NiMorphData`

## Fields
- **Num Morphs** (`uint`)
  - Number of morphing object.
- **Num Vertices** (`uint`)
  - Number of vertices.
- **Relative Targets** (`byte`)
  - Attributes: `default`=`1`
  - This byte is always 1 in all official files.
- **Morphs** (`Morph`)
  - Attributes: `arg`=`Num Vertices`, `length`=`Num Morphs`
  - The geometry morphing objects.

