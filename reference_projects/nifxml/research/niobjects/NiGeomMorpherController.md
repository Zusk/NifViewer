# Niobject `NiGeomMorpherController`

DEPRECATED (20.5), replaced by NiMorphMeshModifier.
Time controller for geometry morphing.

## Attributes
- **inherit**: `NiInterpController`
- **module**: `NiAnimation`
- **name**: `NiGeomMorpherController`

## Fields
- **Morpher Flags** (`GeomMorpherFlags`)
  - Attributes: `since`=`10.0.1.2`
- **Data** (`Ref`)
  - Attributes: `template`=`NiMorphData`
  - Geometry morphing data index.
- **Always Update** (`byte`)
  - Attributes: `since`=`4.0.0.2`
- **Num Interpolators** (`uint`)
  - Attributes: `since`=`10.1.0.106`
- **Interpolators** (`Ref`)
  - Attributes: `length`=`Num Interpolators`, `since`=`10.1.0.106`, `template`=`NiInterpolator`, `until`=`20.0.0.5`
- **Interpolator Weights** (`MorphWeight`)
  - Attributes: `length`=`Num Interpolators`, `since`=`20.1.0.3`
- **Num Unknown Ints** (`uint`)
  - Attributes: `since`=`10.2.0.0`, `until`=`20.0.0.5`, `vercond`=`#BSVER# #GT# 9`
- **Unknown Ints** (`uint`)
  - Attributes: `length`=`Num Unknown Ints`, `since`=`10.2.0.0`, `until`=`20.0.0.5`, `vercond`=`#BSVER# #GT# 9`

