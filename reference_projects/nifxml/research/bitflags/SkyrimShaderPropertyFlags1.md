# Bitflags `SkyrimShaderPropertyFlags1`

Skyrim Shader Property Flags 1

## Attributes
- **name**: `SkyrimShaderPropertyFlags1`
- **prefix**: `SLSF1`
- **storage**: `uint`
- **versions**: `#SKY# #SSE#`

## Values
- `option` `Specular` (`bit`=`0`) – Enables Specularity
- `option` `Skinned` (`bit`=`1`) – Required For Skinned Meshes.
- `option` `Temp_Refraction` (`bit`=`2`)
- `option` `Vertex_Alpha` (`bit`=`3`) – Enables using alpha component of vertex colors.
- `option` `Greyscale_To_PaletteColor` (`bit`=`4`) – in EffectShaderProperty
- `option` `Greyscale_To_PaletteAlpha` (`bit`=`5`) – in EffectShaderProperty
- `option` `Use_Falloff` (`bit`=`6`) – Use Falloff value in EffectShaderProperty
- `option` `Environment_Mapping` (`bit`=`7`) – Environment mapping (uses Envmap Scale).
- `option` `Recieve_Shadows` (`bit`=`8`) – Object can recieve shadows.
- `option` `Cast_Shadows` (`bit`=`9`) – Can cast shadows
- `option` `Facegen_Detail_Map` (`bit`=`10`) – Use a face detail map in the 4th texture slot.
- `option` `Parallax` (`bit`=`11`) – Unused?
- `option` `Model_Space_Normals` (`bit`=`12`) – Use Model space normals and an external Specular Map.
- `option` `Non_Projective_Shadows` (`bit`=`13`)
- `option` `Landscape` (`bit`=`14`)
- `option` `Refraction` (`bit`=`15`) – Use normal map for refraction effect.
- `option` `Fire_Refraction` (`bit`=`16`)
- `option` `Eye_Environment_Mapping` (`bit`=`17`) – Eye Environment Mapping (Must use the Eye shader and the model must be skinned)
- `option` `Hair_Soft_Lighting` (`bit`=`18`) – Keeps from going too bright under lights (hair shader only)
- `option` `Screendoor_Alpha_Fade` (`bit`=`19`)
- `option` `Localmap_Hide_Secret` (`bit`=`20`) – Object and anything it is positioned above will not render on local map view.
- `option` `FaceGen_RGB_Tint` (`bit`=`21`) – Use tintmask for Face.
- `option` `Own_Emit` (`bit`=`22`) – Provides its own emittance color. (will not absorb light/ambient color?)
- `option` `Projected_UV` (`bit`=`23`) – Used for decalling?
- `option` `Multiple_Textures` (`bit`=`24`)
- `option` `Remappable_Textures` (`bit`=`25`)
- `option` `Decal` (`bit`=`26`)
- `option` `Dynamic_Decal` (`bit`=`27`)
- `option` `Parallax_Occlusion` (`bit`=`28`)
- `option` `External_Emittance` (`bit`=`29`)
- `option` `Soft_Effect` (`bit`=`30`)
- `option` `ZBuffer_Test` (`bit`=`31`) – ZBuffer Test (1=on)

