# Niobject `BSShaderTextureSet`

Bethesda-specific Texture Set.

## Attributes
- **inherit**: `NiObject`
- **module**: `BSMain`
- **name**: `BSShaderTextureSet`
- **versions**: `#BETHESDA#`

## Fields
- **Num Textures** (`uint`)
  - Attributes: `default`=`6`
- **Textures** (`SizedString`)
  - Attributes: `length`=`Num Textures`
  - Textures.
            0: Diffuse
            1: Normal/Gloss
            2: Glow(SLSF2_Glow_Map)/Skin/Hair/Rim light(SLSF2_Rim_Lighting)
            3: Height/Parallax
            4: Environment
            5: Environment Mask
            6: Subsurface for Multilayer Parallax
            7: Back Lighting Map (SLSF2_Back_Lighting)

