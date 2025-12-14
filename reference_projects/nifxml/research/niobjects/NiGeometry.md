# Niobject `NiGeometry`

Describes a visible scene element with vertices like a mesh, a particle system, lines, etc.
    Bethesda 20.2.0.7 NIFs: NiGeometry was changed to BSGeometry. 
    Most new blocks (e.g. BSTriShape) do not refer to NiGeometry except NiParticleSystem was changed to use BSGeometry.
    This causes massive inheritance problems so the rows below are doubled up to exclude NiParticleSystem for Bethesda Stream 100+
    and to add data exclusive to BSGeometry.

## Attributes
- **abstract**: `true`
- **inherit**: `NiAVObject`
- **module**: `NiMain`
- **name**: `NiGeometry`

## Fields
- **Bounding Sphere** (`NiBound`)
  - Attributes: `onlyT`=`NiParticleSystem`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_SSE#`
- **Bound Min Max** (`float`)
  - Attributes: `length`=`6`, `onlyT`=`NiParticleSystem`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_F76#`
- **Skin** (`Ref`)
  - Attributes: `onlyT`=`NiParticleSystem`, `since`=`20.2.0.7`, `template`=`NiObject`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_SSE#`
- **Data** (`Ref`)
  - Attributes: `template`=`NiGeometryData`, `vercond`=`#NI_BS_LT_SSE#`
  - Data index (NiTriShapeData/NiTriStripData).
- **Data** (`Ref`)
  - Attributes: `excludeT`=`NiParticleSystem`, `since`=`20.2.0.7`, `template`=`NiGeometryData`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_SSE#`
  - Data index (NiTriShapeData/NiTriStripData).
- **Skin Instance** (`Ref`)
  - Attributes: `since`=`3.3.0.13`, `template`=`NiSkinInstance`, `vercond`=`#NI_BS_LT_SSE#`
- **Skin Instance** (`Ref`)
  - Attributes: `excludeT`=`NiParticleSystem`, `since`=`20.2.0.7`, `template`=`NiSkinInstance`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_SSE#`
- **Material Data** (`MaterialData`)
  - Attributes: `since`=`10.0.1.0`, `vercond`=`#NI_BS_LT_SSE#`
- **Material Data** (`MaterialData`)
  - Attributes: `excludeT`=`NiParticleSystem`, `since`=`20.2.0.7`, `until`=`20.2.0.7`, `vercond`=`#BS_GTE_SSE#`
- **Shader Property** (`Ref`)
  - Attributes: `since`=`20.2.0.7`, `template`=`BSShaderProperty`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`
- **Alpha Property** (`Ref`)
  - Attributes: `since`=`20.2.0.7`, `template`=`NiAlphaProperty`, `until`=`20.2.0.7`, `vercond`=`#BS_GT_FO3#`

