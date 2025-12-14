# Struct `bhkEntityCInfo`

## Attributes
- **module**: `BSHavok`
- **name**: `bhkEntityCInfo`
- **versions**: `#BETHESDA#`

## Fields
- **Collision Response** (`hkResponseType`)
  - Attributes: `default`=`RESPONSE_SIMPLE_CONTACT`
  - How the body reacts to collisions. See hkResponseType for hkpWorld default implementations.
- **Unused 01** (`byte`)
- **Process Contact Callback Delay** (`ushort`)
  - Attributes: `default`=`0xffff`
  - Lowers the frequency for processContactCallbacks. A value of 5 means that a callback is raised every 5th frame. The default is once every 65535 frames.

