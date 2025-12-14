# Niobject `NiFogProperty`

NiFogProperty allows the application to enable, disable and control the appearance of fog.

## Attributes
- **inherit**: `NiProperty`
- **module**: `NiMain`
- **name**: `NiFogProperty`

## Fields
- **Flags** (`FogFlags`)
- **Fog Depth** (`float`)
  - Attributes: `default`=`1.0`
  - Depth of the fog in normalized units. 1.0 = begins at near plane. 0.5 = begins halfway between the near and far planes.
- **Fog Color** (`Color3`)
  - Attributes: `default`=`#VEC3_ZERO#`
  - The color of the fog.

