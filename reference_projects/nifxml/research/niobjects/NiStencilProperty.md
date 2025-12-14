# Niobject `NiStencilProperty`

Allows control of stencil testing.

## Attributes
- **inherit**: `NiProperty`
- **module**: `NiMain`
- **name**: `NiStencilProperty`

## Fields
- **Flags** (`ushort`)
  - Attributes: `until`=`10.0.1.2`
  - Property flags.
- **Stencil Enabled** (`byte`)
  - Attributes: `until`=`20.0.0.5`
  - Enables or disables the stencil test.
- **Stencil Function** (`StencilTestFunc`)
  - Attributes: `until`=`20.0.0.5`
  - Selects the compare mode function.
- **Stencil Ref** (`uint`)
  - Attributes: `until`=`20.0.0.5`
- **Stencil Mask** (`uint`)
  - Attributes: `default`=`#UINT_MAX#`, `until`=`20.0.0.5`
  - A bit mask. The default is 0xffffffff.
- **Fail Action** (`StencilAction`)
  - Attributes: `until`=`20.0.0.5`
- **Z Fail Action** (`StencilAction`)
  - Attributes: `until`=`20.0.0.5`
- **Pass Action** (`StencilAction`)
  - Attributes: `until`=`20.0.0.5`
- **Draw Mode** (`StencilDrawMode`)
  - Attributes: `default`=`DRAW_BOTH`, `until`=`20.0.0.5`
  - Used to enabled double sided faces. Default is 3 (DRAW_BOTH).
- **Flags** (`StencilFlags`)
  - Attributes: `default`=`19840`, `since`=`20.1.0.3`
- **Stencil Ref** (`uint`)
  - Attributes: `since`=`20.1.0.3`
- **Stencil Mask** (`uint`)
  - Attributes: `default`=`#UINT_MAX#`, `since`=`20.1.0.3`
  - A bit mask. The default is 0xffffffff.

