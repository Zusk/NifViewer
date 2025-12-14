# Niobject `NiPSysPartSpawnModifier`

WorldShift-specific Particle Spawn modifier

## Attributes
- **inherit**: `NiPSysModifier`
- **module**: `NiCustom`
- **name**: `NiPSysPartSpawnModifier`
- **versions**: `V10_2_0_1 V10_3_0_1 V10_4_0_1`

## Fields
- **Particles Per Second** (`float`)
  - Attributes: `default`=`40.0`
- **Time** (`float`)
  - Attributes: `default`=`#FLT_MIN#`
  - Default of FLT_MIN would indicate End Time but it seems more like Frequency/Tick Rate or Start Time.
- **Spawner** (`Ref`)
  - Attributes: `template`=`NiPSysSpawnModifier`

