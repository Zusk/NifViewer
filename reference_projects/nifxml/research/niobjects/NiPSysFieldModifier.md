# Niobject `NiPSysFieldModifier`

Base for all force field particle modifiers.

## Attributes
- **abstract**: `true`
- **inherit**: `NiPSysModifier`
- **module**: `NiParticle`
- **name**: `NiPSysFieldModifier`

## Fields
- **Field Object** (`Ref`)
  - Attributes: `template`=`NiAVObject`
  - The object whose position and orientation are the basis of the field.
- **Magnitude** (`float`)
  - Magnitude of the force.
- **Attenuation** (`float`)
  - How the magnitude diminishes with distance from the Field Object.
- **Use Max Distance** (`bool`)
  - Whether or not to use a distance from the Field Object after which there is no effect.
- **Max Distance** (`float`)
  - Maximum distance after which there is no effect.

