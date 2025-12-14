# Struct `MaterialData`

## Attributes
- **module**: `NiMain`
- **name**: `MaterialData`

## Fields
- **Has Shader** (`bool`)
  - Attributes: `since`=`10.0.1.0`, `until`=`20.1.0.3`
  - Shader.
- **Shader Name** (`string`)
  - Attributes: `cond`=`Has Shader`, `since`=`10.0.1.0`, `until`=`20.1.0.3`
  - The shader name.
- **Shader Extra Data** (`int`)
  - Attributes: `cond`=`Has Shader`, `since`=`10.0.1.0`, `until`=`20.1.0.3`
  - Extra data associated with the shader. A value of -1 means the shader is the default implementation.
- **Num Materials** (`uint`)
  - Attributes: `since`=`20.2.0.5`
- **Material Name** (`NiFixedString`)
  - Attributes: `length`=`Num Materials`, `since`=`20.2.0.5`
  - The name of the material.
- **Material Extra Data** (`int`)
  - Attributes: `length`=`Num Materials`, `since`=`20.2.0.5`
  - Extra data associated with the material. A value of -1 means the material is the default implementation.
- **Active Material** (`int`)
  - Attributes: `default`=`-1`, `since`=`20.2.0.5`
  - The index of the currently active material.
- **Cyanide Unknown** (`byte`)
  - Attributes: `default`=`255`, `since`=`10.2.0.0`, `until`=`10.2.0.0`, `vercond`=`#USER# == 1`
  - Cyanide extension (Blood Bowl).
- **WorldShift Unknown** (`int`)
  - Attributes: `since`=`10.3.0.1`, `until`=`10.4.0.1`
- **Material Needs Update** (`bool`)
  - Attributes: `since`=`20.2.0.7`
  - Whether the materials for this object always needs to be updated before rendering with them.

