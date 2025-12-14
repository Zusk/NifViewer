# Niobject `BSProceduralLightningController`

Skyrim, Paired with dummy TriShapes, this controller generates lightning shapes for special effects.
First interpolator controls Generation.

## Attributes
- **inherit**: `NiTimeController`
- **module**: `BSAnimation`
- **name**: `BSProceduralLightningController`
- **versions**: `#BETHESDA#`

## Fields
- **Interpolator 1: Generation** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References generation interpolator.
- **Interpolator 2: Mutation** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References interpolator for Mutation of strips
- **Interpolator 3: Subdivision** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References subdivision interpolator.
- **Interpolator 4: Num Branches** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References branches interpolator.
- **Interpolator 5: Num Branches Var** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References branches variation interpolator.
- **Interpolator 6: Length** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References length interpolator.
- **Interpolator 7: Length Var** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References length variation interpolator.
- **Interpolator 8: Width** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References width interpolator.
- **Interpolator 9: Arc Offset** (`Ref`)
  - Attributes: `template`=`NiInterpolator`
  - References interpolator for amplitude control. 0=straight, 50=wide
- **Subdivisions** (`ushort`)
  - Attributes: `default`=`6`, `range`=`0:12`
- **Num Branches** (`ushort`)
  - Attributes: `default`=`1`, `range`=`0:10`
- **Num Branches Variation** (`ushort`)
  - Attributes: `default`=`1`, `range`=`0:10`
- **Length** (`float`)
  - Attributes: `default`=`512.0`, `range`=`#F0_10000#`
  - How far lightning will stretch to.
- **Length Variation** (`float`)
  - Attributes: `default`=`30.0`, `range`=`#F0_10000#`
  - How far lightning variation will stretch to.
- **Width** (`float`)
  - Attributes: `default`=`16.0`, `range`=`#F0_100#`
  - How wide the bolt will be.
- **Child Width Mult** (`float`)
  - Attributes: `default`=`0.75`, `range`=`#F0_10#`
  - Influences forking behavior with a multiplier.
- **Arc Offset** (`float`)
  - Attributes: `default`=`20.0`, `range`=`#F0_1000#`
- **Fade Main Bolt** (`bool`)
  - Attributes: `default`=`true`
- **Fade Child Bolts** (`bool`)
  - Attributes: `default`=`true`
- **Animate Arc Offset** (`bool`)
  - Attributes: `default`=`true`
- **Shader Property** (`Ref`)
  - Attributes: `template`=`BSShaderProperty`
  - Reference to a shader property.

