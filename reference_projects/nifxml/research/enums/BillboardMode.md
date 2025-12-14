# Enum `BillboardMode`

Determines the way the billboard will react to the camera.
Billboard mode is stored in lowest 3 bits although Oblivion vanilla nifs uses values higher than 7.

## Attributes
- **name**: `BillboardMode`
- **storage**: `ushort`

## Values
- `option` `ALWAYS_FACE_CAMERA` (`value`=`0`) – Align billboard and camera forward vector. Minimized rotation.
- `option` `ROTATE_ABOUT_UP` (`value`=`1`) – Align billboard and camera forward vector while allowing rotation around the up axis.
- `option` `RIGID_FACE_CAMERA` (`value`=`2`) – Align billboard and camera forward vector. Non-minimized rotation.
- `option` `ALWAYS_FACE_CENTER` (`value`=`3`) – Billboard forward vector always faces camera ceneter. Minimized rotation.
- `option` `RIGID_FACE_CENTER` (`value`=`4`) – Billboard forward vector always faces camera ceneter. Non-minimized rotation.
- `option` `BSROTATE_ABOUT_UP` (`value`=`5`) – The billboard will only rotate around its local Z axis (it always stays in its local X-Y plane).
- `option` `ROTATE_ABOUT_UP2` (`value`=`9`) – The billboard will only rotate around the up axis (same as ROTATE_ABOUT_UP?).
- `option` `UNKNOWN_8` (`value`=`8`) – Found in Civ IV Gravebringer and Gravebringer_FX
- `option` `UNKNOWN_10` (`value`=`10`) – Found in FO3 dlc04lighthouselightmech01
- `option` `UNKNOWN_11` (`value`=`11`) – Found in Civ IV Afterworld_Boss_FX
- `option` `UNKNOWN_12` (`value`=`12`) – Found in IRIS Online etc.

