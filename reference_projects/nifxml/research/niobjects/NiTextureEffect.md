# Niobject `NiTextureEffect`

Represents an effect that uses projected textures such as projected lights (gobos), environment maps, and fog maps.

## Attributes
- **inherit**: `NiDynamicEffect`
- **module**: `NiMain`
- **name**: `NiTextureEffect`

## Fields
- **Model Projection Matrix** (`Matrix33`)
  - Model projection matrix.  Always identity?
- **Model Projection Translation** (`Vector3`)
  - Model projection translation.  Always (0,0,0)?
- **Texture Filtering** (`TexFilterMode`)
  - Attributes: `default`=`FILTER_TRILERP`
  - Texture Filtering mode.
- **Max Anisotropy** (`ushort`)
  - Attributes: `since`=`20.5.0.4`
- **Texture Clamping** (`TexClampMode`)
  - Attributes: `default`=`WRAP_S_WRAP_T`
  - Texture Clamp mode.
- **Texture Type** (`TextureType`)
  - Attributes: `default`=`TEX_ENVIRONMENT_MAP`
  - The type of effect that the texture is used for.
- **Coordinate Generation Type** (`CoordGenType`)
  - Attributes: `default`=`CG_SPHERE_MAP`
  - The method that will be used to generate UV coordinates for the texture effect.
- **Image** (`Ref`)
  - Attributes: `template`=`NiImage`, `until`=`3.1`
  - Image index.
- **Source Texture** (`Ref`)
  - Attributes: `since`=`3.1`, `template`=`NiSourceTexture`
  - Source texture index.
- **Enable Plane** (`byte`)
  - Attributes: `default`=`0`
  - Determines whether a clipping plane is used. Always 8-bit.
- **Plane** (`NiPlane`)
- **PS2 L** (`short`)
  - Attributes: `default`=`0`, `until`=`10.2.0.0`
- **PS2 K** (`short`)
  - Attributes: `default`=`-75`, `until`=`10.2.0.0`
- **Unknown Short** (`ushort`)
  - Attributes: `default`=`0`, `until`=`4.1.0.12`

