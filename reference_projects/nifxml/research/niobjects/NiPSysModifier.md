# Niobject `NiPSysModifier`

Abstract base class for all particle system modifiers.

## Attributes
- **abstract**: `true`
- **inherit**: `NiObject`
- **module**: `NiParticle`
- **name**: `NiPSysModifier`

## Fields
- **Name** (`string`)
  - Used to locate the modifier.
- **Order** (`NiPSysModifierOrder`)
  - Attributes: `default`=`ORDER_GENERAL`
  - Modifier's priority in the particle modifier chain.
- **Target** (`Ptr`)
  - Attributes: `template`=`NiParticleSystem`
  - NiParticleSystem parent of this modifier.
- **Active** (`bool`)
  - Attributes: `default`=`true`
  - Whether or not the modifier is active.

