# Enum `SyncPoint`

A sync point corresponds to a particular stage in per-frame processing.

## Attributes
- **name**: `SyncPoint`
- **since**: `V20_5_0_0`
- **storage**: `ushort`

## Values
- `option` `SYNC_ANY` (`value`=`0x8000`) – Synchronize for any sync points that the modifier supports.
- `option` `SYNC_UPDATE` (`value`=`0x8010`) – Synchronize when an object is updated.
- `option` `SYNC_POST_UPDATE` (`value`=`0x8020`) – Synchronize when an entire scene graph has been updated.
- `option` `SYNC_VISIBLE` (`value`=`0x8030`) – Synchronize when an object is determined to be potentially visible.
- `option` `SYNC_RENDER` (`value`=`0x8040`) – Synchronize when an object is rendered.
- `option` `SYNC_PHYSICS_SIMULATE` (`value`=`0x8050`) – Synchronize when a physics simulation step is about to begin.
- `option` `SYNC_PHYSICS_COMPLETED` (`value`=`0x8060`) – Synchronize when a physics simulation step has produced results.
- `option` `SYNC_REFLECTIONS` (`value`=`0x8070`) – Synchronize after all data necessary to calculate reflections is ready.

