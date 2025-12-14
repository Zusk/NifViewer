# Enum `TextureType`

The type of information that is stored in a texture used by an NiTextureEffect.

## Attributes
- **name**: `TextureType`
- **storage**: `uint`

## Values
- `option` `TEX_PROJECTED_LIGHT` (`value`=`0`) – Apply a projected light texture. Each light effect is summed before multiplying by the base texture.
- `option` `TEX_PROJECTED_SHADOW` (`value`=`1`) – Apply a projected shadow texture. Each shadow effect is multiplied by the base texture.
- `option` `TEX_ENVIRONMENT_MAP` (`value`=`2`) – Apply an environment map texture. Added to the base texture and light/shadow/decal maps.
- `option` `TEX_FOG_MAP` (`value`=`3`) – Apply a fog map texture. Alpha channel is used to blend the color channel with the base texture.

