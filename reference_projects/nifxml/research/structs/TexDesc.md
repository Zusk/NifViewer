# Struct `TexDesc`

NiTexturingProperty::Map. Texture description.

## Attributes
- **module**: `NiMain`
- **name**: `TexDesc`

## Fields
- **Image** (`Ref`)
  - Attributes: `template`=`NiImage`, `until`=`3.1`
  - Link to the texture image.
- **Source** (`Ref`)
  - Attributes: `since`=`3.3.0.13`, `template`=`NiSourceTexture`
  - NiSourceTexture object index.
- **Clamp Mode** (`TexClampMode`)
  - Attributes: `default`=`WRAP_S_WRAP_T`, `until`=`20.0.0.5`
  - 0=clamp S clamp T, 1=clamp S wrap T, 2=wrap S clamp T, 3=wrap S wrap T
- **Filter Mode** (`TexFilterMode`)
  - Attributes: `default`=`FILTER_TRILERP`, `until`=`20.0.0.5`
  - 0=nearest, 1=bilinear, 2=trilinear, 3=..., 4=..., 5=...
- **Flags** (`TexturingMapFlags`)
  - Attributes: `since`=`20.1.0.3`
  - Texture mode flags; clamp and filter mode stored in upper byte with 0xYZ00 = clamp mode Y, filter mode Z.
- **Max Anisotropy** (`ushort`)
  - Attributes: `since`=`20.5.0.4`
- **UV Set** (`uint`)
  - Attributes: `default`=`0`, `until`=`20.0.0.5`
  - The texture coordinate set in NiGeometryData that this texture slot will use.
- **PS2 L** (`short`)
  - Attributes: `default`=`0`, `until`=`10.4.0.1`
  - L can range from 0 to 3 and are used to specify how fast a texture gets blurry.
- **PS2 K** (`short`)
  - Attributes: `default`=`-75`, `until`=`10.4.0.1`
  - K is used as an offset into the mipmap levels and can range from -2047 to 2047. Positive values push the mipmap towards being blurry and negative values make the mipmap sharper.
- **Unknown Short 1** (`ushort`)
  - Attributes: `until`=`4.1.0.12`
- **Has Texture Transform** (`bool`)
  - Attributes: `default`=`false`, `since`=`10.1.0.0`
  - Whether or not the texture coordinates are transformed.
- **Translation** (`TexCoord`)
  - Attributes: `cond`=`Has Texture Transform`, `since`=`10.1.0.0`
  - The UV translation.
- **Scale** (`TexCoord`)
  - Attributes: `cond`=`Has Texture Transform`, `default`=`#VEC2_ONE#`, `since`=`10.1.0.0`
  - The UV scale.
- **Rotation** (`float`)
  - Attributes: `cond`=`Has Texture Transform`, `default`=`0.0`, `since`=`10.1.0.0`
  - The W axis rotation in texture space.
- **Transform Method** (`TransformMethod`)
  - Attributes: `cond`=`Has Texture Transform`, `since`=`10.1.0.0`
  - Depending on the source, scaling can occur before or after rotation.
- **Center** (`TexCoord`)
  - Attributes: `cond`=`Has Texture Transform`, `since`=`10.1.0.0`
  - The origin around which the texture rotates.

