# Niobject `BSXFlags`

Controls animation and collision.  Integer holds flags:
Bit 0 : enable havok, bAnimated(Skyrim)
Bit 1 : enable collision, bHavok(Skyrim)
Bit 2 : is skeleton nif?, bRagdoll(Skyrim)
Bit 3 : enable animation, bComplex(Skyrim)
Bit 4 : FlameNodes present, bAddon(Skyrim)
Bit 5 : EditorMarkers present, bEditorMarker(Skyrim)
Bit 6 : bDynamic(Skyrim)
Bit 7 : bArticulated(Skyrim)
Bit 8 : bIKTarget(Skyrim)/needsTransformUpdates
Bit 9 : bExternalEmit(Skyrim)
Bit 10: bMagicShaderParticles(Skyrim)
Bit 11: bLights(Skyrim)
Bit 12: bBreakable(Skyrim)
Bit 13: bSearchedBreakable(Skyrim) .. Runtime only?

## Attributes
- **inherit**: `NiIntegerExtraData`
- **module**: `BSMain`
- **name**: `BSXFlags`
- **versions**: `#BETHESDA#`

