# Bitflags `BSShaderFlags`

Shader Property Flags

## Attributes
- **name**: `BSShaderFlags`
- **prefix**: `F3SF1`
- **storage**: `uint`
- **versions**: `#FO3#`

## Values
- `option` `Specular` (`bit`=`0`) – Enables Specularity
- `option` `Skinned` (`bit`=`1`) – Required For Skinned Meshes
- `option` `LowDetail` (`bit`=`2`) – Lowddetail (seems to use standard diff/norm/spec shader)
- `option` `Vertex_Alpha` (`bit`=`3`) – Vertex Alpha
- `option` `Unknown_1` (`bit`=`4`) – Unknown
- `option` `Single_Pass` (`bit`=`5`) – Single Pass
- `option` `Empty` (`bit`=`6`) – Unknown
- `option` `Environment_Mapping` (`bit`=`7`) – Environment mapping (uses Envmap Scale)
- `option` `Alpha_Texture` (`bit`=`8`) – Alpha Texture Requires NiAlphaProperty to Enable
- `option` `Unknown_2` (`bit`=`9`) – Unknown
- `option` `FaceGen` (`bit`=`10`) – FaceGen
- `option` `Parallax_Shader_Index_15` (`bit`=`11`) – Parallax
- `option` `Unknown_3` (`bit`=`12`) – Unknown/Crash
- `option` `Non_Projective_Shadows` (`bit`=`13`) – Non-Projective Shadows
- `option` `Unknown_4` (`bit`=`14`) – Unknown/Crash
- `option` `Refraction` (`bit`=`15`) – Refraction (switches on refraction power)
- `option` `Fire_Refraction` (`bit`=`16`) – Fire Refraction (switches on refraction power/period)
- `option` `Eye_Environment_Mapping` (`bit`=`17`) – Eye Environment Mapping (does not use envmap light fade or envmap scale)
- `option` `Hair` (`bit`=`18`) – Hair
- `option` `Dynamic_Alpha` (`bit`=`19`) – Dynamic Alpha
- `option` `Localmap_Hide_Secret` (`bit`=`20`) – Localmap Hide Secret
- `option` `Window_Environment_Mapping` (`bit`=`21`) – Window Environment Mapping
- `option` `Tree_Billboard` (`bit`=`22`) – Tree Billboard
- `option` `Shadow_Frustum` (`bit`=`23`) – Shadow Frustum
- `option` `Multiple_Textures` (`bit`=`24`) – Multiple Textures (base diff/norm become null)
- `option` `Remappable_Textures` (`bit`=`25`) – usually seen w/texture animation
- `option` `Decal_Single_Pass` (`bit`=`26`) – Decal
- `option` `Dynamic_Decal_Single_Pass` (`bit`=`27`) – Dynamic Decal
- `option` `Parallax_Occulsion` (`bit`=`28`) – Parallax Occlusion
- `option` `External_Emittance` (`bit`=`29`) – External Emittance
- `option` `Shadow_Map` (`bit`=`30`) – Shadow Map
- `option` `ZBuffer_Test` (`bit`=`31`) – ZBuffer Test (1=on)

